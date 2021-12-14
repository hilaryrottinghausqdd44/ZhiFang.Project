//非构建类--通用型组件或控件--单选按钮组件
/**
    *单选按钮组件公共属性
    *labField       文本
    *labFieldWidth      文本宽度
    *labFieldAlign      文本对齐方式
    *value      当前选中值
    *itemList       项目列表
    *dataSourceType     数据源类型（本地、服务器）
    
    *serverUrl      后台服务地址
    *keyField       数据项值字段
    *textField      数据项显示字段
    *height     高度像素
    *width      宽度像素
    *layoutType     布局类型
    *colRowCount        行（列）数量

    *公共方法
    *setLabField    LabField    LabField：文本
    *getLabField        获取LabField
    *setValue   value   根据value设置并选中项目
    *getValue       获取当前选中的项目的值。
    *setItemList    ItemList    设置项目列表
    *getItemList        获取项目列表
    *setLayoutType  LayoutType  行、列布局类型设置
    *getLayoutType      获取行、列布局类型设置
    *setColRowCount ColRowCount 设置行（列）数量
    *getColRowCount     获取行（列）数量
    *
    *
    *公共事件
    *onChanged      当改变选择项时触发
    *OnClick        单击时触发
    *onOKCilck  单击确定按钮触发事件
    *onCancelCilck  单击取消按钮触发事件
*/

 Ext.ns('Ext.zhifangux');
 Ext.define('Ext.zhifangux.UXRadioGroup', {
    extend: 'Ext.form.Panel', //CheckboxGroup  UXCheckboxGroup  //Ext.zhifangux.UXCheckboxGroup
    alias: 'widget.uxradiogroup',
    requires: ['Ext.layout.container.CheckboxGroup', 'Ext.form.field.Base'],

    title: '',//标题
    border : false,//边框线显示 true,或隐藏false
    //公共属性
    labField: 'labField1',  //文本
    labFieldWidth: 100,     //文本宽度
    labFieldAlign: 'left',  //文本对齐方式
    radiogroupName:'',//单选组里子项名称
    value:[],   //当前选中值
    itemList:[],    //项目列表
    dataSourceType: '',     //数据源类型（本地、服务器）
    localData: '',  //当前页面数据集合
    serverUrl: '',  //后台服务地址
    keyField:'inputValue' , //数据项值字段,默认值为inputValue
    textField:'boxLabel',   //数据项显示字段,默认值为boxLabel
    
    iKeyField:'inputValue' , //keyField数据项匹配字段,
    iTextField:'boxLabel',   //textField数据项匹配字段
    height:260,        //容器高度像素,默认值为260
    width:600,       //容器宽度像素,,默认值为600
    layoutType:'columns', 
    colRowCount: 2,     //行（列）数量
    columnWidth : 120,  //每一项的宽度
    allowBlank: true,
    lastValue:[],       //返回选择项的key,value
    
    vertical:true,       //是否显示垂直滚动条
    btnHidden: false,//确定或者取消按钮的显示false或者隐藏true,默认值为显示false
    
    butOkBool:false,     //是否隐藏确定按钮
    butCancleBoll:false, //是否隐藏取消按钮
    autoScroll: true,   //添加滚动条  
    
    myCom:null,//内部调用处理,生成的单选组控件
  
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
    /*设置单选组行/列数量
     * @public
     * colRowCount
     * */
    setColRowCount:function(rows)
    {
        this.myCom.colunms=rows;
        return this.myCom.colunms;
    },
    /*//获取单选组行/列数量
     *@public 
     */  
    setColRowCount:function()
    {
        var columns=this.myCom.columns;
       this.colRowCount=columns; 
       return this.myCom.columns;
        
    },
    /**
     * 【设置Border】
     * @public
     */
    setBorder:function(boolborder)
    {
        //是否显示线框线条
        return this.myCom.setBorder(boolborder);
        
    },
   
    /**
     * 【设置LabField】
     * @public
     */
    setLabField:function(labField3 ){
       return this.myCom.setFieldLabel(labField3);
       
    },
        /**
     * 【获取LabField】
     * @public
     */
    setLabField:function(){
        //var me=this;
        var textlabfiels=this.myCom.getFieldLabel();
        this.labField=textlabfiels;
       return this.labField;
    },
    
    /**
     * 【设置行、列布局类型】
     * @public
     */
    setLayoutType:function(LayoutType){
        
    },
    /**
     *返回选择项的key,value
     *inputValue
     *boxLabel
     * @public
     */
    getLastValue:function(){
        var me=this;
        var itemList=me.getBoxes();
        var ItemValue=[];
        for(var i=0;i<itemList.length;i++)
        {
            var textfield1=Ext.getCmp(itemList[i].id).inputValue;
            var keyfield1=Ext.getCmp(itemList[i].id).boxLabel;
            ItemValue.push({inputValue :textfield1 ,boxLabel :keyfield1})
        }
        
        return ItemValue;
    },
     /**
     * 【获取行、列布局类型】
     * @public
     */
    getLayoutType:function(){
        
    },
    /**
     * 【设置单选组的项目列表】
     * @public
     */
    setItemList:function(itemList){
    this.itemList=itemList;
    },
    
     /**
     * 返RadioGroup回容器内的所有Radio子项
     * @param {} RadioGroup
     * @return {} 获取项目列表
     */
    getItemList:function(){
        var me=this;
        var itemList=me.getBoxes();
        var ItemValue=[];
        for(var i=0;i<itemList.length;i++)
        {
            var textfield1=Ext.getCmp(itemList[i].id).inputValue;
            var keyfield1=Ext.getCmp(itemList[i].id).boxLabel;
            ItemValue.push({inputValue :textfield1 ,boxLabel :keyfield1})
        }
        
        return ItemValue;
    }, 
    
    /**
     * 得到RadioGroup容器内的所有Radio子项
     * @param {} query
     * @return {}
     */
     GetRadios: function(query) {
        return this.query('[isRadio]' + (query||''));
    },
     
    /**
     * 【根据value设置并选中项目】
     * @public
     */
    setValue:function(value){   
        var me    = this,
            radios = me.getBoxes(),
            b,
            bLen  = radios.length,
            box, name,
            cbValue;            
            for(b = 0; b < bLen; b++)
            {
                if(Ext.getCmp(radios[b].id).inputValue==value)
                {
                    Ext.getCmp(radios[b].id).setValue(true);
                    return;                    
                };                  
                
                //子项名字设置默认值
                if(Ext.getCmp(radios[b].id).boxLabel==value)
                {
                   Ext.getCmp(radios[b].id).setValue(true);
                    return;
                };
             }
    },
    /**
     * 【获取当前选中的项目的值】
     * @param {} radiogroup:单选组控件
     * @return {}返回值为所有选中的单选项的属性inputValue的所有值,如”1,2,3,4,5”
     */
    getValue:function(){
        var me=this;
        var itemList=me.getBoxes();
        var ItemValue=[];
        for(var i=0;i<itemList.length;i++)
        {
            var textfield1=Ext.getCmp(itemList[i].id).inputValue;
            var keyfield1=Ext.getCmp(itemList[i].id).boxLabel;
            ItemValue.push({inputValue :textfield1 ,boxLabel :keyfield1})
        }
        return ItemValue;
    },
//    /**
//     * 【获取当前选中的项目的值】
//     * @param {} radiogroup:单选组控件
//     * @return {}返回值为所有选中的单选项的属性inputValue的所有值,如”1,2,3,4,5”
//     */
//    getValue:function(){
//         var values = {},
//         boxes  = this.getBoxes(),
//            b,
//            bLen   = boxes.length,
//            box, name, inputValue, bucket;
//
//        for (b = 0; b < bLen; b++) {
//            box        = boxes[b];
//            name       = box.getName();
//            inputValue = box.inputValue;
//
//            if (box.getValue()) {
//                if (values.hasOwnProperty(name)) {
//                    bucket = values[name];
//                    if (!Ext.isArray(bucket)) {
//                        bucket = values[name] = [bucket];
//                    }
//                    bucket.push(inputValue);
//                } else {
//                    values[name] = inputValue;
//                }
//            }
//        }
//        return values;
//    },
    
    blankText: 'You must select one item in this group',
    defaultType: 'radiofield',    
    
	/**
	 * 生成单选组的子项数据
	 * @param {} name2:单选项的名称
	 * @return {} myCheckboxItems:单选组的子项数组
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
                       itemId: me.keyField //'checkbox_id'+i
                    };
                 //当前页面数据集合
                me.localData.push(tempItem);

                }
                }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息请求失败！');
            }
        });
        return me.localData;
    },
    /**
    * 生成单选组
    * @param {} name2:单选项的名称
    * @return {}Radiogroup
    */
    createCom:function(){
	    var me = this;
	    var myRadioItems2 =[];
	    myRadioItems2 = this.getStore();
	        //单选组   
	    me.myCom=null;
	    me.myCom = new Ext.form.RadioGroup({
	            xtype: 'fieldcontainer',
                x:1,y:(me.btnHidden==true)?1:26,
	            fieldLabel:me.labField,
	            labelAlign:me.labFieldAlign,//"left"
	            defaultType: 'radiofield',
	            labelWidth:me.labFieldWidth,
	            columnWidth :me.columnWidth,
	            columns:me.colRowCount,  //colRowCount
	            vertical: me.vertical,
	            items:myRadioItems2,//
	        listeners: {
	           //原有的可以监听到
		        change: function(newValue, oldValue, eOpts){
			        var id = this.id.substring(0,this.id.length-4);
			        var arr=me.GetChecked();
			         me.lastValue=[];
			        Ext.each(arr,function(item,index,itemAll){
			            var inputValue=item.inputValue;
			            var boxLabel=item.boxLabel;
			            var dd={"inputValue":inputValue,"boxLabel":boxLabel};
			            me.lastValue.push(dd);
			        });
			         me.fireEvent('onChanged');
			        }
		        }  
	        });
        return me.myCom;
    },
    
    /**
     * 常量设置
     * @private
     */
    initConstants:function(){
        var me = this;
    },
    // private
    groupCls: Ext.baseCSSPrefix + 'form-radio-group',

    getBoxes: function () {
        return this.query('[isRadio]');
    },
    /**
     * 得到容器内的所有Radio组件
     * @param {} query
     * @return {}
     */
     GetBoxesTwo: function(query) {
        return this.query('[isRadio]' + (query||''));
    }, 
    /**
     * 
     * @return 返回{Ext.form.field.Radio[]} Ext.form.field.Radio所有选中的组件
     */
    GetChecked: function() {
        return this.GetBoxesTwo('[checked]');
    },
     createToolbar:function(){
        var me = this;
        var myToolbar=Ext.create('Ext.toolbar.Toolbar', {
        height:25,
        hidden:me.btnHidden,
        bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
        cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
        //border : 0,//边框线显示 true,或隐藏false
        x:(me.getWidth()/2>300)?(me.getWidth()/2-100):(10),y:1,
        items: [
            {
              xtype : me.createbtnOK()
            },
             {
              xtype: me.createbtnCancel()
            }
        ]
        }); 
        return myToolbar;
    },
    /**
     * 确定按钮
     * @private
     */
    createbtnOK:function(){
        var me = this;
       var btnOK=Ext.create('Ext.Button', 
       {//确定
            width:80,
            text:'确定',
            border : 1,//边框线显示 true,或隐藏false
            itemId:'mybtn_ok',
            x:me.getWidth()/2-60,y:1,//x轴的me.getWidth()/2+50值未生效
            hidden:me.butOkBool,
            listeners:{
                'click': function(){
                    me.fireEvent('onOKCilck');
                }
           }
        });
        return btnOK;
    },
    
    /**
     *取消按钮
     * @private
     */
    createbtnCancel:function(){
        var me = this;
        var btnCancel=Ext.create('Ext.Button', 
       {
            width:80, text:'取消',itemId:'mybtn_Cancel',
            hidden:me.butCancleBoll,
            border : 1,//边框线显示 true,或隐藏false
            x:me.getWidth()/2+50,y:1,//x轴的me.getWidth()/2+50值未生效
            listeners:{
                'click': function(){
                   me.fireEvent('onCancelCilck');
                }
            }
       });
       return btnCancel;
    },
    /**
     * 组装组件内容
     * @private
     */
    setAppItems:function(){
        var me = this;
        var items = [];
        me.myCom=me.createCom();    //新增单选组控件方便操作
        
        me.itemList=me.myCom.items;       //项目所有列表   新增功能
        me.setItemList(me.myCom.items);   //项目所有列表   新增功能
        
        var toolbar=me.createToolbar();
        if(me.btnHidden==false){
            me.items=null;
        if(toolbar)
             items.push(toolbar);
             if(me.myCom)
             items.push(me.myCom);
        }
       else if(me.btnHidden==true&&me.myCom){
            me.items=null;
            if(me.myCom)
            items.push(me.myCom);
        }else{
            me.items=null;
            if(me.myCom)
            items.push(me.myCom);
        }
        me.items = items;
        
    },

    /**
     * 注册事件
     * @private
     */
    addAppEvents:function(){
        var me = this;
        me.addEvents('onChanged');//单选改变按钮事件
        me.addEvents('onOKCilck');   //  单击确定按钮触发事件
        me.addEvents('onCancelCilck');  //  单击取消按钮触发事件
        
    },
     /**
     * 初始化组件
     */
      initComponent:function(){
        var me=this;
        //初始化窗体
        me.initConstants();
        //注册事件
        me.addAppEvents();
        me.setAppItems();
         //加载数据的方法
        me.load=function(where){ 
            me.externalWhere=where;
            var w='';
            if(me.internalWhere){ 
                w+=me.internalWhere;
            }
            if(where&&where!=''){
                if(w!=''){ 
                    w+=' and '+where;
                }else{ 
                    w+=where;
                }
            }
            me.store.proxy.url=me.serverUrl+'&where='+w;
            me.store.load();
        };
        this.callParent(arguments);
      }
});