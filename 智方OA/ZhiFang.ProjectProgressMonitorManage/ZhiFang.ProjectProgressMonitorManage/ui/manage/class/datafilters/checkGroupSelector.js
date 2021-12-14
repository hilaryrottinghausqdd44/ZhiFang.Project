//复选组选择器
/**
 * 修改完成
 * 
 * 对外公开属性
 *  title: '',//标题,默认值为''
 *  border : true,//边框线显示 true,或隐藏false
 *  labField:'',        //数据组的label名称,默认值为''
 *  labFieldWidth:100,   //数据组的label文本宽度,默认值为100
 *  labFieldAlign:'left',   //文本对齐方式,默认值为left
 *  checkboxgroupName:"checkboxgroupName",//+Ext.idSeed,//复选组组名
 *  itemList:[],        //项目列表
 *  dataSourceType:'', //数据源类型（本地、服务器）
 *  localData:[],   // 当前页面数据集合
 *  serverUrl:'',   //后台服务地址
    
 *  bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
 *  cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
 *  keyField:'inputValue' , //数据项值字段,默认值为inputValue
 *  textField:'boxLabel',   //数据项显示字段,默认值为boxLabel
 *  height:600,        //容器高度像素,默认值为600
 *  width:800,       //容器宽度像素,,默认值为800
 *  layoutType:'columns',  //容器布局类型(行rows/列布局columns),默认值为columns列布局
 *  colRowCount:5,     //容器行/列数量,默认值为5
 *  autoScroll : true,  //容器滚动条自动显示,默认值为true
 *  columnWidth : 120,  //每一项的宽度,默认值为120
 *  
 * 对外公开事件
 * onChanged 选择项选择事件
 * 
 * saveClick 确定事件
 * comeBackClick 取消事件
 * 
 * 对外公开方法

 * getValue 获取设置当前控件的最终结果值,如""
 * getDisplayValue
 * setWidth 设置组件宽度
 * getWidth 返回组件宽度
 * setHeight 设置组件高度
 * getHeight 返回组件高度
 * setTitle 设置组件标题
 * getTitle 返回组件标题
 * 方法设置:按钮显示/隐藏--未实现
 * setChecked SetChecked方法调用:需要注意:
 * 先实例化好复选组控件,values为传入的某一项或者多项的的inputValue数组值,
 * values={checkboxgroupName: ['4',  '5', '6','8']};
 * 其中 checkboxgroupName为该复选组控件的子项名称(name),
 * '4',  '5', '6','8'为复选组控件子项里  的KeyField(inputValue)的值
 * 
 * serverUrl:
 * 
 */
 
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.checkGroupSelector', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.checkGroupSelector',
    layout:'absolute',
    frame:true,
    padding:0,
    type:'',
    mouseoverCls:'bg-orange',
    vertical:true,
    id:'',
    border : false,//边框线显示 true,或隐藏false
    title: '',//标题,默认值为''
    bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
    cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
    
    labField:'',        //数据项的label名称,默认值为''
    labFieldWidth:100,   //数据组的label文本宽度,默认值为100
    labFieldAlign:'left',   //文本对齐方式,默认值为left
    checkboxgroupName:"checkboxgroupName",//复选组组名
    itemList:[],        //项目列表
    dataSourceType:'', //数据源类型（本地、服务器）
    localData:[],   // 当前页面数据集合
    serverUrl:'',   //后台服务地址
    
    keyField:'inputValue' , //数据项值字段,默认值为inputValue
    textField:'boxLabel',   //数据项显示字段,默认值为boxLabel
    
    iKeyField:'inputValue' , //keyField数据项匹配字段,
    iTextField:'boxLabel',   //textField数据项匹配字段
    
    layoutType:'columns',//容器布局类型(行rows/列布局columns),默认值为columns列布局
    colRowCount:5,     //容器行/列数量,默认值为5
    autoScroll : true,  //容器滚动条自动显示,默认值为true
    columnWidth : 120,  //每一项的宽度,默认值为120
    
    lastValues:[],       //返回选择项的key,value
    
    myCom:null,//内部调用处理,生成的复选组控件
    btnHidden: false,//确定或者取消按钮的显示false或者隐藏true,默认值为显示false
    butOkBool:false,     //是否隐藏确定按钮
    butCancleBoll:false, //是否隐藏取消按钮
    
    internalWhere:'',//内部hql
    externalWhere:'',//外部hql
    
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
     * 【获取当前选中的项目的值】
     * @param {} checkboxgroup:复选组控件
     * @return {}返回值为所有选中的复选项的属性inputValue的所有值,如”1,2,3,4,5”
     */
    getValue:function(){
        var me=this;
	    var arr=me.getChecked();
	        me.lastValues=[];
	        Ext.each(arr,function(item,index,itemAll){
	            var inputValue=item.inputValue;
	            var boxLabel=item.boxLabel;
	            var dd={"inputValue":inputValue,"boxLabel":boxLabel};
	            me.lastValues.push(dd);
	            
	        });
        return this.lastValues;
    },
    /**
     * 获得当前选中的提交值
     * @param {} checkboxgroup:复选组控件
     * @return {}返回值为所有选中的复选项的属性inputValue的所有值,如”1,2,3,4,5”
     */
    getSubmitValue:function(){
        var me=this;
        var arr=me.getChecked();
        var lastValues='';
        Ext.each(arr,function(item,index,itemAll){
            var inputValue=item.inputValue;
            //var boxLabel=item.boxLabel;
            //var dd={"inputValue":inputValue,"boxLabel":boxLabel};
            lastValues=lastValues+inputValue+',';
            
        });
        lastValues=lastValues.substring(0,lastValues.length-1); 
        return lastValues;
    },
    /**
     * 获得当前选中的显示值
     * @param {} checkboxgroup:复选组控件
     * @return {}返回值为所有选中的复选项的属性inputValue的所有值,如”1,2,3,4,5”
     */
    getDisplayValue:function(){
        var me=this;
        var arr=me.getChecked();
        var lastValues='';
        Ext.each(arr,function(item,index,itemAll){
            var boxLabel=item.boxLabel;
            lastValues=lastValues+"'"+boxLabel+"',";
            
        });
        lastValues=lastValues.substring(0,lastValues.length-1); 
        return lastValues;
    },
    /**
     * 【获取LabField】
     * @public
     */
    setLabField:function(labField ){
       this.labField=labField;
       return this.myCom.setFieldLabel(labField);
    },
    /**
     * 【获取LabField】
     * @public
     */
    getLabField:function(){
       var labelText=this.myCom.getFieldLabel();
       this.labField=labelText;
       return this.labField;
    },
   /**
     * 【ColRowCount设置行（列）数量】
     * @public
     */
    setColRowCount:function(columns){
       this.myCom.columns=columns;
       this.colRowCount=columns;
       return this.myCom.columns;
    },
    /**
     * 【获取行（列）数量】
     * @public
     */
    getColRowCount:function(){
       var columns=this.myCom.columns;
       this.colRowCount=columns; 
       return this.myCom.columns;
    },
	/**
	 * 生成复选组的子项数据
	 * @param {} name2:复选项的名称
	 * @return {} myCheckboxItems:复选组的子项数组
	 */
    getStore:function(){
        var me = this;
        if(me.serverUrl==""){
	        Ext.Msg.alert('提示','没有配置数据服务地址或者配置失败！');
	        return null;
        }
        Ext.Ajax.request({
            async:false,//非异步
            url:me.serverUrl,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
	            var result = Ext.JSON.decode(response.responseText);
	            if(result.success){
		            me.localData=[];
		            var ResultDataValue = {count:0,list:[]};
		            if(result["ResultDataValue"] && result["ResultDataValue"] != ""){
		                ResultDataValue = Ext.JSON.decode(result["ResultDataValue"]);
		            }
		            var count = ResultDataValue['count'];
		            
		            for (var i = 0; i < ResultDataValue.list.length; i++) {  
		                var value=me.iKeyField;
		                var text=me.iTextField;
				        me.textField =ResultDataValue.list[i][text];  
				        me.keyField = ResultDataValue.list[i][value];
			            var tempItem={  
			                   boxLabel : me.textField,  
			                   inputValue : me.keyField,
			                   name:me.checkboxgroupName,//复选子项的名称
			                   itemId: me.keyField //
			                };
		                 //当前页面数据集合
		                me.localData.push(tempItem);
		                }
		             }else{
		                Ext.Msg.alert('提示','获取信息失败！');
		             }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息服务请求失败！');
            }
        });
        return me.localData;
    },
   /**
    * 生成复选组
    * @param {} name2:复选项的名称
    * @return {}checkboxgroup
    */
    createCom:function(){
        var me = this;
        me.myCom=null;
        //复选组
        me.myCom = new Ext.form.CheckboxGroup({
            xtype: 'fieldcontainer',
            x:1,y:(me.btnHidden==true)?1:26,
            fieldLabel: me.labField,
            labelAlign:me.labFieldAlign,//"left"
            defaultType: 'checkboxfield',
            labelWidth:me.labFieldWidth,
            itemId:me.keyField,
            columnWidth :me.columnWidth,
            columns:me.colRowCount, //[200, 100],
            vertical: me.vertical,
            items:me.getStore(),//
        listeners: {
        change: function(newValue, oldValue, eOpts){
	        var arr=me.getChecked();
	        me.lastValues=[];
	        Ext.each(arr,function(item,index,itemAll){
	            var inputValue=item.inputValue;
	            var boxLabel=item.boxLabel;
	            var dd={"inputValue":inputValue,"boxLabel":boxLabel};
	            me.lastValues.push(dd);
	            
	        });
	        //需要放在这里才生效,不能往前放
	        me.fireEvent('onChanged');
        }
        }  
        }) ;
        return me.myCom;
    },

    getChecked: function() {
        return this.GetBoxes('[checked]');
    },
    /**
     * 【设置复选组的项目列表】
     * @public
     */
    setItemList:function(ItemList){
        this.itemList=ItemList;
    },
    /**
     * 返回容器内的所有CheckBox组件
     * @param {} checkboxgroup
     * @return {} 获取项目列表
     */
    getItemList:function(){
        return this.GetBoxes();
    }, 
    /**
     * 得到容器内的所有CheckBox组件
     * @param {} query
     * 
     * @return {}
     */
     GetBoxes: function(query) {
        return this.query('[isCheckbox]' + (query||''));
    },

    /**
     * 默认选中项设置:
     * 需要注意:先实例化好复选组控件
     * @param {} values:传入的某一项或者多项的的inputValue数组值,
     * 如 values={checkboxgroupName: ['4',  '5', '6','8']};
     * 其中 checkboxgroupName为该复选组控件的名称,'4',  '5', '6','8'为复选组控件子项里的KeyField(inputValue)的值
     */
    setChecked:function(values){
		var me=this;
		me.myCom.setValue(values);
    },
     /**
     * 全选(true)及全不选(false),生效
     * 允许设置单个或者多个默认选中项
     * @param {} value:true/false,
     */
    setValue:function(value){
      var me2=this, me= me2.myCom,
            boxes = me2.GetBoxes(),//boxes为所有复选组里的子项
            b,bLen  = boxes.length,
            box, name, cbValue;//box为所有复选组里的某一具体子项
        me.batchChanges(function() {
            for (b = 0; b < bLen; b++) {
                box = boxes[b];
                name = box.getName();
                cbValue = value;
                if (value && value.hasOwnProperty(name)) {
                    if (Ext.isArray(value[name])) {
                        cbValue = Ext.Array.contains(value[name], box.inputValue);
                    } else {
                        cbValue = value[name];
                    }
                }
                box.setValue(cbValue);
            }
        });
        return me;
    },
    /**
     * 组装组件内容
     * @private
     */
    setAppItems:function(){
        var me = this;
        var items = [];
        //生成复选组数据
        me.myCom= me.createCom();
        //项目列表
        me.itemList=me.myCom.items;
        me.setItemList(me.myCom.items);
        items.push(me.myCom);
        me.items = items;
    },
   
    /**
     * 注册事件
     * @private
     */
    addAppEvents:function(){
        var me = this;
        me.addEvents('onChanged');//按钮点击
        me.addEvents('saveClick');//按钮点击
        me.addEvents('comeBackClick');//按钮点击
    },

     /**
     * 初始化
     * @private
     */
    initComponent:function(){ 
        var me = this;
        //注册事件
        me.addAppEvents();
        //组装组件内容
        me.setAppItems();
	     me.dockedItems=[{
	            xtype:'toolbar',
	            dock:'bottom',//bottom
	            itemId:'dockedItemsbuttons', 
	            items:[
	            {text:'　确定　',xtype:'button',iconCls:'build-button-save',
	            width:80,height:22,
	            itemId:'save',handler:function(button){
	                    me.fireEvent('saveClick');
	                }
	            },
	            {text:'　返回　',xtype:'button',iconCls:'build-button-refresh',
	                width:80,height:22,
	                itemId:'comeBack',handler:function(button){
	                me.fireEvent('comeBackClick');
	            }
	            }
	           ]
	        }];
		//组件监听
    	this.callParent(arguments);
    }
});