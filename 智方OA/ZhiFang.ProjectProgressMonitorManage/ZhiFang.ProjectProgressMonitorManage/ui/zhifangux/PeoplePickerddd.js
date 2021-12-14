//非构建类--通用型组件或控件--部门人员选择器
//适用1.单个列表里的单个人员选择; 2.单个列表的多个人员选择
//未完成或者存在问题:1.需要绑定部门下拉列表树组件
//2.查询文本框不能用itemId获取
/**
 * 修改中
 * 
 * 对外公开属性
 *  title: '',//标题,默认值为''
 *  border : false,//边框线显示 true,或隐藏false
 *  titleAlign :"center",//标题显示位置
 *  bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
 *  cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white 
 *  width:600,//控件宽度,默认值为600
 *  height:580,//控件高度,默认值为580
 *  DataServerUrl:'', //部门人员后台服务地址,默认值为''
 *  DeptDataServerUrl:'', //部门后台服务地址,默认值为''
 *  multiSelect:false,//单选或多选开关; false:单选,true:多选;默认值为false
 *  simpleSelect:false,//单选或多选开关;false:单选,true:多选,必须要设置该值,单选或多选才生效;默认值为false
 *  DeptId:'',      //人员部门Id,默认值为''
 *  DeptpName:'',   //人员部门名称,默认值为''
 *  UserRoot:'',//部门人员获取/读取后台数据时的根节点,默认值为''
 *  DeptRoot:'',//部门获取/读取后台数据时的根节点,默认值为''
 *  ValueField:[],//人员部门数据列表值字段,是外面做好数据适配后传进来
 *  DeptValueField:[],//部门数据列表值字段,是外面做好数据适配后传进来
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
    Value:0,
    DataServerUrl:'', //部门人员后台服务地址,默认值为''
    DeptDataServerUrl:'', //部门后台服务地址,默认值为''
    multiSelect:false,//单选或多选开关; false:单选,true:多选;默认值为false
    simpleSelect:false,//单选或多选开关;false:单选,true:多选,必须要设置该值,单选或多选才生效;默认值为false
    DeptId:'',      //人员部门Id,默认值为''
    DeptpName:'',   //人员部门名称,默认值为''
    DisplayField:'text',
    UserRoot:'ResultDataValue',//部门人员获取/读取后台数据时的根节点,默认值为''
    DeptRoot:'ResultDataValue',//部门获取/读取后台数据时的根节点,默认值为''
   
    ValueField:[],//人员部门数据列表值字段,可以是外面做好数据适配后传进来
    DeptValueField:[],//部门数据列表值字段,可以是外面做好数据适配后传进来

    Field:[],//"name","email","phone"  
    DeptField:[],//fields:['DeptpId','DeptpName']
    lastValues:[],//最终所有选中的人员数据信息(json)
    DeptNewValue:'',//部门选择id值临时变量
    setMyRowClass:false,//设置gridpanel的匹配行背景色
    
    
    /**
     * 创建列表对象数据源
     * 实现数据项适配的功能
     * 
     */
    treestore: function () {
        var me = this;
        var store = new Ext.create('Ext.data.TreeStore', {
            async: false, //非异步
            method: 'GET',
            timeout: 5000,
            proxy: {//数据代理
                type: 'ajax', //请求方式
                url:'server/PeoplePicker2.json' 
            },
            defaultRootProperty:me.UserRoot, //子节点的属性名
            root: {
                text: '树菜单',
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
     * setTitle 获取组件标题
     * @param {} title
     */
    getTitle:function(title){
        var me=this;
       return me.title;
    },
    
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
     * 解析处理传入人员的部门数据列表值字段,
     * 封装生成人员的部门store数据的Field数组
     * 如:fields:['DeptpId','DeptpName','name', 'email', 'phone','DispOrder']
     * 
     */
    SetmyGridField:function(){
        var me=this;
       if(me.ValueField.length>0){
	       for(var i=0;i<me.ValueField.length;i++){
		       var test=me.ValueField[i].dataIndex;
		       me.Field.push(test);
	       }
       }
    },
    /**
     * 解析处理传入部门数据列表值字段,
     * 封装生成部门store数据的Field数组
     * fields:['DeptpId','DeptpName']
     * 
     */
    SetmyDeptField:function(){
        var me=this;
       if(me.DeptValueField.length>0){
	       for(var i=0;i<me.DeptValueField.length;i++){
		       var test=me.DeptValueField[i].dataIndex;
		       me.DeptField.push(test);
	       }
       }
    },
    /**
     * 设置单选或多选方式
     * false:单选,true:多选
     * @param {} value
     */
    SetmultiSelect:function(value){
        var me=this;
        me.multiSelect=value;
    },
       /**
     * 设置单选或多选方式
     * false:单选,true:多选
     * @param {} value
     */
    SesimpleSelect:function(value){
        var me=this;
        me.simpleSelect=value;
    },
    /**
     * 获取数据列表选中的数据行
     * 返回json数据
     * @return {}
     */
    getAllValue:function(){
	    var me=this;
	    return me.lastValues;
    },
    /**
     * 获取数据列表选中的数据行
     * 返回json数据
     * @return {}
     */
    getValue:function(){
	    var me=this;
	    return me.lastValues;
    },
    /**
     * 获取单选或多选方式
     * false:单选,true:多选
     * @param {} value
     * @return {}
     */
     GetmultiSelect:function(value){
         return this.multiSelect;
    },
       /**
     * 获取单选或多选方式
     * false:单选,true:多选
     * @param {} value
     * @return {}
     */
     GetsimpleSelect:function(value){
         return this.simpleSelect;
    },
    initComponent:function(){
	    var me = this;
	    //解析处理传入部门人员数据列表值字段
	    if(me.Field.length==0){
	        me.SetmyGridField();
	    }
	    //解析处理传入部门数据列表值字段
	    if(me.DeptField.length==0){
	        me.SetmyDeptField();
	    } 
	    
        me.items = [
        {
	        xtype:'treepicker',
	        height:30,
	        maxWidth : 450,
	        region:'north',
	        displayField :me.DisplayField,//这个地方也需要注意一下，这个是告诉源码tree用json数据里面的什么字段来显示，我测试出来是只能写'text'才有效果  
		    fieldLabel : '选择部门',  
		    labelAlign :'right',
		    itemId:'MyPeoplePicker_DeptItemId',
		    labelWidth:60,
		    value:0, 
		    forceSelection : true,// 只能选择下拉框里面的内容  
     	    editable : false,// 不能编辑  
            store : me.treestore(), //这个store必须是在controller里面动态创建出来的，如果用引入controller时加载的那种store会报错  
            listeners:{
			    select:function(newValue,oldValue, eOpts ){
        	
                	me.DeptNewValue = newValue.value;  
                	
        	       
        	       
        	       // alert(t);
        	      //   Ext.getCmp('MyPeoplePicker_DeptItemId').getStore().load({params:me.DeptNewValue});  

//                 
//				     //过滤部门人员信息
				    me.filterFn(me.DeptNewValue);
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
	            id:'myfuzzyquery',//用itemId不能获取到
	            fieldLabel: '模糊查询 ',
	            x:5,y:1,
	            enableKeyEvents:true,
	            listeners:{
		            change:function(textfield, newValue,oldValue, eOpts ){
            	        //alert('rwew');
                        var value=newValue;
			            var MyDeptItemValue=me.getComponent("MyPeoplePicker_DeptItemId").getValue();
//			            if(MyDeptItemValue!=null&&MyDeptItemValue!=undefined){
//				            newValue=MyDeptItemValue+" "+newValue;
//				            me.filterFn(newValue);
//			            }
//                        
                        if(MyDeptItemValue!=null&&MyDeptItemValue!=undefined){
                            if(value!=null&&value!=undefined){
                                MyDeptItemValue=""+MyDeptItemValue+" "+newValue;
                                me.filterFn(MyDeptItemValue);
                                //alert("MyDeptItemValue"+MyDeptItemValue);
                                }else{
                                me.filterFn(MyDeptItemValue);
                                alert("two"+MyDeptItemValue);
                                }   
                           
                        }else if(value!=null&&value!=undefined){
                             me.filterFn(newValue);
                             alert("three"+MyDeptItemValue);
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
		                var value=Ext.getCmp("myfuzzyquery").getValue();
		                var MyDeptItemValue=me.getComponent("MyPeoplePicker_DeptItemId").getValue();
		                if(MyDeptItemValue!=null&&MyDeptItemValue!=undefined){
		                    if(value!=null&&value!=undefined){
		                        MyDeptItemValue=""+MyDeptItemValue+" "+value;
		                        me.filterFn(MyDeptItemValue);
                                //alert("MyDeptItemValue"+MyDeptItemValue);
		                        }else{
		                        me.filterFn(MyDeptItemValue);
                               //alert("two"+MyDeptItemValue);
		                        }	
			               
		                }else if(value!=null&&value!=undefined){
		                     me.filterFn(value);
                              //alert("three");
		                 }
                    }
               }
            },{
	            xtype:'label',
	            text: '(请输入姓名或用帐号)',
	            x:335,y:1
	        }]
	    },{
	        xtype:me.createMyGrid(),
	        region : 'center'
        },{
	       region:'south',
	       border : false,
	       layout:'absolute',
	       height:20,
	       items:[{ 
               xtype:'button',
               itemId:'btnok',
               x:255,y:0,
               text: '确 定' ,
               listeners : { 
	               click:{
		               element:'el', fn:function(){
						   var grid=me.getComponent('MyPeoplePicker_itemId90');
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
						       Ext.Msg.alert("请先选择人员");
						       return;
						   }
						   me.fireEvent('onOKCilck'); 
					   }
				  }
	          } 
          },{ 
             xtype:'button',
             itemId:'btncancel',
             text: '取 消',
             x:305,y:0,
             handler: function () {
                 me.fireEvent('onCancelCilck');
             }
         }]
     }];
                 
        //添加事件，别的地方就能对这个事件进行监听
        this.addEvents('onOKCilck');
        this.addEvents('onCancelCilck');
        this.callParent(arguments);
    },

    afterRender: function() {
        var me = this;
        me.callParent(arguments);
    },
     /**
      * 创建gridPanel列表对象
      * @return {}
      */
    createMyGrid:function(){
        var me=this;
	    var myGrid=Ext.create('Ext.grid.Panel', {
		    itemId:"MyPeoplePicker_itemId90",
		    store:me.getMyGridStore(), //Ext.data.StoreManager.lookup('myGridStore'),
		    width:me.width-10,
		    height:me.height-95,
		    selType:'checkboxmodel',//复选框
		    multiSelect:me.multiSelect,//允许多选
		    simpleSelect: me.simpleSelect,    //简单选择功能开启  
		    columns:me.ValueField,
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
    getMyGridStore:function(){
        var me = this;
        var myGridStore=null;
        if(me.DataServerUrl==""){
	        Ext.Msg.alert('提示','没有配置数据服务地址或者配置失败！');
	        return null;
        }
        Ext.Ajax.request({
            async:false,//非异步
            url:me.DataServerUrl,
            //部门参数
            params: {
                 Deptparams: me.DeptId
            },
            method:'GET',
            timeout:5000,
            success:function(response,opts){
	            var result = Ext.JSON.decode(response.responseText);
	            if(result.success){
		            myGridStore=Ext.create('Ext.data.Store', {
			            fields:me.Field,
			            data:result,
			            proxy: {
				            type:'memory',
				            reader: {
					            type: 'json',
					            root: me.UserRoot
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
   
     /**
     * 创建部门列表对象数据源
     * 实现数据项适配的功能
     * 
     */
    getMyDeptGridStore:function(){
        var me = this;
        var myGridStore=null;
        if(me.DeptDataServerUrl==""){
	         Ext.Msg.alert('提示','没有配置数据服务地址或者配置失败！');
	         return null;
        }
        Ext.Ajax.request({
            async:false,//非异步
            url:me.DeptDataServerUrl,
            //部门参数
            params: {
                 Deptparams: me.DeptId
            },
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
	            myGridStore=Ext.create('Ext.data.Store', {  
		            fields:me.DeptField,//实现数据项适配的功能,需要调用时传入DeptpId,DeptpName
		            data:result,
		            proxy: {
			            type:'memory',
			            reader: {
				            type: 'json',
				            root: me.UserRoot
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
   
   /**
    * 模糊查询过滤匹配gridpanel行记录的函数:模糊匹配某一个gridpanel的store里的所有列字段信息
    * gridpanel可以修改为在外面传入进来,这样更具通用信息
    * 字符串分割符也可以是外部传入
    * 如需要同时支持gridpanel的store匹配多列时,输入的字符串需要用空格分开,如"aaa bbb",那"aaa bbb都"会去gridpanel的store里的所有列字段信息
    * @param {} value,外部传入的查询字符串条件
    */
    filterFn:function(value){
	    var me=this,valtemp=value;
	    var grid=me.getComponent('MyPeoplePicker_itemId90');
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