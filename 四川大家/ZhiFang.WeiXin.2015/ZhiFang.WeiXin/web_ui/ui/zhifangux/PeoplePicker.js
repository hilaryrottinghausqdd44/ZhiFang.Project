//非构建类--通用型组件或控件--部门人员选择器
//适用1.单个列表里的单个人员选择; 2.单个列表的多个人员选择
//未完成或者存在问题:1.需要绑定部门下拉列表树组件
//2.查询文本框不能用itemId获取
/**
 * 对外公开属性
 *  title: '',//标题,默认值为''
 *  border : false,//边框线显示 true,或隐藏false
 *  titleAlign :"center",//标题显示位置
 *  bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
 *  cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white 
 *  width:600,//控件宽度,默认值为600
 *  height:580,//控件高度,默认值为580
 *  dataServerUrl:'', //部门人员后台服务地址,默认值为''
 *  deptDataServerUrl:'', //部门后台服务地址,默认值为''
 *  multiSelect:false,//单选或多选开关; false:单选,true:多选;默认值为false
 *  simpleSelect:false,//单选或多选开关;false:单选,true:多选,必须要设置该值,单选或多选才生效;默认值为false
 *  deptId:'',      //人员部门Id,默认值为''
 *  deptpName:'',   //人员部门名称,默认值为''
 *  userRoot:'',//部门人员获取/读取后台数据时的根节点,默认值为''
 *  deptRoot:'',//部门获取/读取后台数据时的根节点,默认值为''
 *  valueField:[],//人员部门数据列表值字段,是外面做好数据适配后传进来
 *  deptValueField:[],//部门数据列表值字段,是外面做好数据适配后传进来
 *  
 * 对外公开事件
 * onOKCilck 确定事件
 * OnChanged 当列表项被改变后触发
 * onCancelCilck 取消事件
 * 
 * 对外公开方法
 * getAllValue 返回给后台的数据方法
 * getValue 返回给后台的数据方法
 * setWidth 设置组件宽度
 * getWidth 返回组件宽度
 * setHeight 设置组件高度
 * getHeight 返回组件高度
 * setTitle 设置组件标题
 * getTitle 返回组件标题
 */
Ext.Loader.setConfig({enabled: true});
Ext.Loader.setPath('Ext.zhifangux', getRootPath()+'/ui/zhifangux');
Ext.Loader.setPath('Ext.ux', getRootPath()+'/ui/extjs/ux');
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.PeoplePicker', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.peoplepicker',
    requires: [
        'Ext.ux.TreePicker'
    ],
    layout:'border',
    frame:true,
    title: '',//标题,默认值为''
    border : false,//边框线显示 true,或隐藏false
    titleAlign :"center",//标题显示位置
    bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
    cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
    
    width:600,//控件宽度,默认值为600
    height:580,//控件高度,默认值为580
    value:0,
    isbtnOK:false,//确定按钮显示:false,隐藏true,
    isbtnCancel:false,//取消按钮显示:false,隐藏true,
    
    gridServerUrl:'', //部门人员后台服务地址,默认值为''
    treeServerUrl:'', //部门后台服务地址,默认值为''
    gridHeight:480,
    multiSelect:false,//单选或多选开关; false:单选,true:多选;默认值为false
    simpleSelect:false,//单选或多选开关;false:单选,true:多选,必须要设置该值,单选或多选才生效;默认值为false
    
    displayField:'text',
    userRoot:'list',//部门人员获取/读取后台数据时的根节点,默认值为''
    treeRoot:'list',//部门获取/读取后台数据时的根节点,默认值为''
   
    gridValueField:[],//人员部门数据列表值字段,可以是外面做好数据适配后传进来
    treeValueField:[],//部门数据列表值字段,可以是外面做好数据适配后传进来

    gridFields:[],//内部处理;"name","email","phone"  
    treeField:[],//内部处理fields:['DeptpId','DeptpName']
    
    lastValues:[],//最终所有选中的人员数据信息(json)
    
    deptNewValue:'',//部门选择id值临时变量
    setMyRowClass:false,//设置gridpanel的匹配行背景色
    
    gridExternalWhere:'',//列表外部hql
    gridInternalWhere:'',//列表内部hql
    
    treeExternalWhere:'',//下拉树外部hql
    treeInternalWhere:'',//下拉树内部hql
    /**
     * 设置组件宽度
     * @param {} width
     */
    setWidth:function(width){
        var me=this;
        return me.setSize(width);
    },
    /**
     * 返回组件宽度
     * @return {}
     */
    getWidth:function(){
        var me=this;
        return me.width;
    },
    /**
     * 设置组件高度
     * @param {} height
     */
    setHeight:function(height){
        var me=this;
        return me.setSize(undefined, height);
    },
    /**
     * 返回组件高度
     * @return {}
     */
    getHeight:function(){
        var me=this;
        return me.height;
    },
    /**
     * setTitle 获取组件标题
     * @param {} title
     */
    getTitle:function(title){
       var me=this;
       return me.title;
    },
   
    /**
     * 解析处理传入人员的部门数据列表值字段,
     * 封装生成人员的部门store数据的Field数组
     * 如:fields:['DeptpId','DeptpName','name', 'email', 'phone','DispOrder']
     * 
     */
    setmyGridField:function(){
        var me=this;
       if(me.gridValueField.length>0){
	       for(var i=0;i<me.gridValueField.length;i++){
		       var test=me.gridValueField[i].dataIndex;
		       me.gridFields.push(test);
	       }
       }
    },
    /**
     * 解析处理传入部门数据列表值字段,
     * 封装生成部门store数据的Field数组
     * fields:['DeptpId','DeptpName']
     */
    setmyDeptField:function(){
        var me=this;
       if(me.treeValueField.length>0){
	       for(var i=0;i<me.treeValueField.length;i++){
		       var test=me.treeValueField[i].dataIndex;
		       me.treeField.push(test);
	       }
       }
    },
    /**
     * 设置单选或多选方式
     * false:单选,true:多选
     * @param {} value
     */
    setmultiSelect:function(value){
        var me=this;
        me.multiSelect=value;
    },
    /**
     * 设置单选或多选方式
     * false:单选,true:多选
     * @param {} value
     */
    sesimpleSelect:function(value){
        var me=this;
        me.simpleSelect=value;
    },

    /**
     * 获取数据列表选中的数据行
     * 返回json数据
     * @return {}
     */
    getValue:function(){
	   var me=this;
       var grid=me.getGridPanel();
       var store=grid.getStore();//reload()
        //得到列表数据所有选中的行数据
       var records = grid.getSelectionModel().getSelection();
       if (records.length > 0) {//当有选择的数据的时候   
            me.lastValues=[];
            for (var i = 0; i < records.length; i++) {
                //循环迭代所有的选择的row            
                me.lastValues.push(records[i].data);
            }   
       }else{
           me.lastValues=[];
           Ext.Msg.alert("请先选择人员");
       }
       return me.lastValues;
    },
    /**
     * 获取单选或多选方式
     * false:单选,true:多选
     * @param {} value
     * @return {}
     */
     getmultiSelect:function(value){
         return this.multiSelect;
    },
    /**
     * 获取单选或多选方式
     * false:单选,true:多选
     * @param {} value
     * @return {}
     */
     getsimpleSelect:function(value){
         return this.simpleSelect;
     },
    initComponent:function(){
	    var me = this;
	    //解析处理传入部门人员数据列表值字段
	    if(me.gridFields.length==0){
	        me.setmyGridField();
	    }
	    //解析处理传入部门数据列表值字段
	    if(me.treeField.length==0){
	        me.setmyDeptField();
	    } 
        me.items = [
        {
	        xtype:'treepicker',
	        height:30,
	        maxWidth : 450,
	        region:'north',
	        displayField :me.displayField,//这个地方也需要注意一下，这个是告诉源码tree用json数据里面的什么字段来显示，我测试出来是只能写'text'才有效果  
		    fieldLabel : '请选择',  
		    labelAlign :'right',
		    itemId:'treePickerItem',
		    labelWidth:60,
		    value:0, 
		    forceSelection : true,// 只能选择下拉框里面的内容  
     	    editable : false,// 不能编辑  
            store : me.getTreeStore(''), //这个store必须是在controller里面动态创建出来的，如果用引入controller时加载的那种store会报错  
            listeners:{
			    select:function(newValue,oldValue, eOpts ){
                	me.deptNewValue = newValue.value;
                    var MyDeptItemValue=newValue.value;
                    var value=me.getfilterItem().getValue();
				    //过滤部门人员信息
				    if(MyDeptItemValue!=null&&MyDeptItemValue!=undefined){
                        if(value!=null&&value!=undefined){
                            MyDeptItemValue=""+MyDeptItemValue+" "+value;
                            me.filterFn(MyDeptItemValue,1);
                        }else{
                            me.filterFn(me.deptNewValue,1);
                        }   
                    }else if(value!=null&&value!=undefined){
                         me.filterFn(value,1);
                     }
			    }
		    }
       },
        {//模糊查询输入框
            region:'north',
            layout:'absolute',
            border : false,
            height:25,
            items:[
            {
	            xtype:'textfield',
                itemId:'filterItem',
	            fieldLabel: '模糊查询 ',
	            x:5,y:1,
	            enableKeyEvents:true,
	            listeners:{
		            change:function(textfield, newValue,oldValue, eOpts ){
			            var MyDeptItemValue=me.gettreePickerItem().getValue();
                        if(MyDeptItemValue!=null&&MyDeptItemValue!=undefined){
                            if(newValue!=null&&newValue!=undefined){
                                MyDeptItemValue=""+MyDeptItemValue+" "+newValue;
                                me.filterFn(MyDeptItemValue,2);
                            }else{
                                me.filterFn(me.deptNewValue,2);
                            }   
                        }else if(value!=null&&value!=undefined){
                             me.filterFn(newValue,2);
                         }
                    },
		            keyup:function(e, eOpts ){
                        
		            }
                }
            },{
                xtype:'button',
                itemId:'btnfindfds',
                text: '查找',
                x:285,y:1,
                listeners : { 
	            	click:function(){
		                var value=me.getfilterItem().getValue();
		                var MyDeptItemValue=me.gettreePickerItem().getValue();
		                if(MyDeptItemValue!=null&&MyDeptItemValue!=undefined){
		                    if(value!=null&&value!=undefined){
		                    	MyDeptItemValue=""+MyDeptItemValue+" "+value;
		                        me.filterFn(MyDeptItemValue,2);
		                    }else{
		                        me.filterFn(me.deptNewValue,2);
		                    }	
		                }else if(value!=null&&value!=undefined){
		                      me.filterFn(value,2);
		                 }
                    }
               }
            },
            { 
               xtype:'button',
               itemId:'btnOK',
               hidden:me.isbtnOK,
               x:340,y:1,
               text: '确 定' ,
               listeners : { 
                   click:{
                       element:'el', fn:function(){
                           me.fireEvent('onOKCilck'); 
                       }
                  }
              } 
          },{ 
             xtype:'button',
             itemId:'btnCancel',
             hidden:me.isbtnCancel,
             text: '取 消',
             x:395,y:1,
             handler: function () {
                 me.fireEvent('onCancelCilck');
             }
         }
            ]
	    },{
	        xtype:me.createMyGrid(),
	        region : 'center'
        },{
	       region:'south',
	       border : false,
	       layout:'absolute',
	       height:20,
	       items:[]
     }];
                 
        //添加事件，别的地方就能对这个事件进行监听
        this.addEvents('onOKCilck');
        this.addEvents('onCancelCilck');
        this.callParent(arguments);
    },

    afterRender: function() {
        var me = this;
        me.callParent(arguments);
        if(Ext.typeOf(me.callback)=='function'){me.callback(me);}
    },
    /**
     * 创建gridPanel列表对象
     * @return {}
     */
    createMyGrid:function(){
        var me=this;
	    var myGrid=Ext.create('Ext.grid.Panel', {
		    itemId:"MyPeoplePicker",
		    store:me.getGridStore(''), //Ext.data.StoreManager.lookup('myGridStore'),
		    width:me.width-10,
		    height:me.gridHeight,
		    selType:'checkboxmodel',//复选框
		    multiSelect:me.multiSelect,//允许多选
		    simpleSelect: me.simpleSelect,    //简单选择功能开启  
		    columns:me.gridValueField,
		    viewConfig:{
			    forceFit : true, 
			    getRowClass : 
			    function(record,rowIndex,rowParams,store){
			        //设置gridpanel匹配行的背景色,匹配某一行时,行背景色为"css/icon.css"里的.row-s
			        return (me.setMyRowClass==true)?("row-s"):("");
			    }   
		    }
	    });
       return myGrid;
    },
    /**
     * 创建列表对象数据源
     * 实现数据项适配的功能
     * 
     */
    getTreeStore: function (where) {
        var me = this;
        if(me.treeServerUrl == ""){
            Ext.Msg.alert("提示","没有配置下拉树的获取数据的服务！");
        }
        me.treeExternalWhere=where;
        var w=''; 
        if(me.treeInternalWhere){ 
            w+=me.treeInternalWhere;
        }
        if(where&&where!=''){
            if(w!=''){
                w+=' and '+where;
            }else{
                w+=where;
            }
        }
        var myUrl = '';
        if(w==''){
            myUrl = me.treeServerUrl;
        }else{
            myUrl = me.treeServerUrl+'&where='+w;
        }
        var store = new Ext.create('Ext.data.TreeStore', {
            async: false, //非异步
            method: 'GET',
            timeout: 5000,
            proxy: {//数据代理
                type: 'ajax', //请求方式
                url:myUrl //'server/PeoplePicker2.json' 
            },
            defaultRootProperty:me.treeRoot, //子节点的属性名
            root: {
                text: '请选择',
                rootVisible:false,
                leaf: false,      //是否是页子节点
                expanded: true//是否默认打开
            },
            folderSort: true,
            sorters: [{
                property: 'text',
                direction: 'ASC'
            }]
        });
        return store;
    },
    /**
     * 创建列表对象数据源
     * 实现数据项适配的功能
     * 
     */
   getGridStore:function(where){
        var me = this;
        var myGridStore=null;
        
        if(me.gridServerUrl==""){
	        Ext.Msg.alert('提示','没有配置数据服务地址或者配置失败！');
	        return null;
        }
        me.gridExternalWhere=where;
        var w=''; 
        if(me.gridInternalWhere){ 
            w+=me.gridInternalWhere;
        }
        if(where&&where!=''){
            if(w!=''){
                w+=' and '+where;
            }else{
                w+=where;
            }
        }
        
        var myUrl = '';
        if(w==''){
            myUrl = me.gridServerUrl;
        }else{
            myUrl = me.gridServerUrl+'&where='+w;
        }
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            //部门参数

            method:'GET',
            timeout:5000,
            success:function(response,opts){
	            var result = Ext.JSON.decode(response.responseText);
	                
	            if(result.success){
                    var ResultDataValue = {count:0,list:[]};
		            if(result["ResultDataValue"] && result["ResultDataValue"] != ""){
		                //ResultDataValue = Ext.JSON.decode(result["ResultDataValue"]);
                        ResultDataValue =result["ResultDataValue"];
		            }
		            var count = ResultDataValue['count'];
                    
		            myGridStore=Ext.create('Ext.data.Store', {
			            fields:me.gridFields,
			            data:ResultDataValue,
			            proxy: {
				            type:'memory',
				            reader: {
					            type: 'json',
					            root: me.userRoot
				            }
                        }
                    });
                }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息请求失败！');
            }
        });
       return myGridStore;
   },
	 /***
	  * 重新加载列表数据
	  * @param {} where
	  */
	 gridLoad:function(where){
	    var me=this;
	    me.gridExternalWhere=where;
	    var w=''; 
	    if(me.gridInternalWhere){ 
	        w+=me.gridInternalWhere;
	    }
	    if(where&&where!=''){
	        if(w!=''){
	            w+=' and '+where;
	        }else{
	            w+=where;
	        }
	    }
	    var grid=me.getGridPanel();
        var myUrl = '';
        if(w==''){
            myUrl = me.gridServerUrl;
        }else{
            myUrl = me.gridServerUrl+'&where='+w;
        }
	    if(grid){
	        grid.store.proxy.url=myUrl; 
	        grid.store.load();
	    }
	 },
     /***
      * 重新加载下拉树数据
      * @param {} where
      */
     treeLoad:function(where){
        var me=this;
        me.treeExternalWhere=where;
        var w=''; 
        if(me.treeInternalWhere){ 
            w+=me.treeInternalWhere;
        }
        if(where&&where!=''){
            if(w!=''){
                w+=' and '+where;
            }else{
                w+=where;
            }
        }
        var grid=me.gettreePickerItem();
        var myUrl = '';
        if(w==''){
            myUrl = me.treeServerUrl;
        }else{
            myUrl = me.treeServerUrl+'&where='+w;
        }
        if(grid){
            grid.store.proxy.url=myUrl; 
            grid.store.load();
        }
     },
   getGridPanel:function(){
     var me=this;
     var grid=me.getComponent('MyPeoplePicker');
     return grid;
   },
   getfilterItem:function(){
     var me=this;
     var filterItem=me.getComponent('filterItem');
     return filterItem;
   },
   gettreePickerItem:function(){
     var me=this;
     var treePickerItem=me.getComponent('treePickerItem');
     return treePickerItem;
   },
   /**
    * 模糊查询过滤匹配gridpanel行记录的函数:模糊匹配某一个gridpanel的store里的所有列字段信息
    * gridpanel可以修改为在外面传入进来,这样更具通用信息
    * 字符串分割符也可以是外部传入
    * 如需要同时支持gridpanel的store匹配多列时,输入的字符串需要用空格分开,如"aaa bbb",那"aaa bbb都"会去gridpanel的store里的所有列字段信息
    * @param {} value,外部传入的查询字符串条件,num:1代表下拉树调用,2代表其他调用该方法
    */
    filterFn:function(value,num){
	    var me=this,valtemp=value;
	    var grid=me.getGridPanel();
	    var strSplit=" ";//字符串分割符
	    var store=grid.getStore();//取gridpanel的store
	    if(!valtemp){
	       store.clearFilter();
	       return;
	    }
	    //字符串不为数字
	    if(isNaN(parseInt(valtemp,10))){
	    //字符串里的字母统一转换为小写格式,以匹配支持gridpanel的store里所有大小写字母字符串
	       valtemp=String(value.toUpperCase()).trim().split(strSplit);
	    }else{
	       valtemp=String(value).trim().split(strSplit);
	    }

         //调用filterBy匹配
	    store.filterBy(function(record, id) {
	        var data=record.data;
	        for(var p in data){
		        var porp=String(data[p]);
		        for(var i=0;i<valtemp.length;i++){
			        var macther=valtemp[i];
			        var macther2='^'+Ext.escapeRe(macther);
			        mathcer=new RegExp(macther2);
			        if(mathcer.test(porp)){
				         //设置gridpanel匹配行的背景色
				        //me.setMyRowClass=true;
				        return true;
			        }
	            }
		   }
	       return false;
	   });
       
   }
});