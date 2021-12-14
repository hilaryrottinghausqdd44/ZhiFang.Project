//非构建类--通用型组件或控件--日期区间(两个日期)控件
/**
 * 日期区间(两个日期)控件
 * 将两个日期控件封装在一个组件内，单击不同的控件展示不同的下拉内容
 * @param
 * fieldLabel：第一个控件的显示名称设置文本,默认为空
 * fieldLabelTwo:第二个控件的显示名称设置,默认为空
 * labelWidth：文本宽度，默认为60
 * labelAlign：文本对齐方式,默认右对齐
 * value：第一个控件日期默认值，默认为日期时间格式
 * valueTwo:第二个控件日期默认值
 * dateFormat:两个日期区间格式
 * layoutType:'hbox'控件布局设置
 * labelSeparator:':' 分隔符
 * 对外公开事件
 * OnChanged 当日期时间改变时触发
 * 
 * 对外公开方法
 * setFieldLabel 设置LabField：文本
 * getLabField  获取LabField
 * getValue 返回给后台的数据方法
 * setWidth 设置组件宽度
 * getWidth 返回组件宽度
 * setHeight 设置组件高度
 * getHeight 返回组件高度

 * 注意:getValue的结果值如为两个日期区间时,其的结果值用'|'作分割并合并,取该结果值时需要分割处理为两个结果
 */

Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.DateIntervals', {
    extend: 'Ext.container.Container',
    alias: 'widget.dateintervals', 
    layout:'absolute',
    border:false,
    padding:0,
    name:'',
    fieldLabel:'',//第一个控件的显示名称设置
    fieldLabelTwo:'',//第二个控件的显示名称设置
    layoutType:'hbox',//控件布局设置
    labelSeparator:':',//分隔符
    labelWidth:55,
    labelAlign:'left',
    value:new Date(),
    valueTwo:new Date(),
    bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
    cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
    dateFormat:'Y-m-d',
    format:'Y-m-d',
    width:325,
    height:28,
    editable:true,
    
    setReadOnly:function(obj){
        var me=this;
        var dateOne =me.getdateOne();
        var dateTwo =me.getdateTwo();
        dateOne.setReadOnly(obj);
        dateTwo.setReadOnly(obj);
    },
    /**
     * 获第二个控件的取值
     * @return {}
     */
    getOneLabField: function () {
        var me = this;
        var dateTwo1 =me.getdateOne();
    	return dateTwo1.getFieldLabel();
    },
    /***
     * 获第二个控件的LabField取值
     * @return {}
     */
    getTwoLabField: function () {
        var me = this;
        var dateTwo1 =me.getdateTwo();
        return dateTwo1.getFieldLabel();
    },
    /**
     * 将日期默认值参数设置到当前控件中
     * @param {} value1
     * @param {} value2
     */
    setValue: function (value1,value2) {
        var me = this;
        var dateTwo1 =me.getdateOne();
        var dateTwo2 =me.getdateTwo();
        if(value1!==null&&value1!== undefined){
        dateTwo1.setValue(value1);
        }
        if(value2!==null&&value2!== undefined){
        dateTwo2.setValue(value2);
        }
    },
    /**
     * 将日期LabField默认值参数设置到当前控件中
     * @param {} value1
     * @param {} value2
     */
    setFieldLabel: function (value1,value2) {
    	var me = this;
        var dateTwo1 =me.getdateOne();
        var dateTwo2 =me.getdateTwo();
        if(value1!==null&&value1!== undefined){
        dateTwo1.setFieldLabel(value1);
        }
        if(value2!==null&&value2!== undefined){
        dateTwo2.setFieldLabel(value2);
        }
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
    //获得当前结果值 longfc
    /***
     * 的结果值如为两个日期区间时,其的结果值用'|'作分割并合并,取该结果值时需要分割处理为两个结果
     * @return {}
     */
    getValue: function () {
        var me = this;
        var dateTwo1 =me.getdateOne();
        var dateTwo2 =me.getdateTwo();
        var DateValue1 =Ext.util.Format.date(dateTwo1.getValue(), 'Y-m-d');
        var DateValue2 =Ext.util.Format.date(dateTwo2.getValue(), 'Y-m-d');
        //两个日期区间的结果值用'|'作分割并合并,取该结果值时需要分割处理为两个结果
        var DateValue="";
        DateValue=DateValue1+ '|' +DateValue2;
        return DateValue;
    },
    /**
	 * 常量设置
	 * @private
	 */
    initConstants: function () {
      var me = this;
      if(me.layoutType=="hbox")
      {
          me.height=28;
    	  if (me.fieldLabel=="")
    	  {
              me.labelWidth=0;
    		  me.width=205;
    	  }
    	  else if (me.fieldLabel!="")
    	  {
              me.labelWidth=55;
    		  me.width=260+me.labelWidth;
    	  }
      }else if(me.layoutType=="vbox")
      {
          me.height=56;
          if ((me.fieldLabel==""||me.fieldLabelTwo=="")&&(me.width<160))
          {
             me.labelWidth=60;
              me.width=160;
          }
          else if (me.fieldLabel!=""&&me.fieldLabelTwo!="")
          {
              me.labelWidth=65;
              me.width=105+me.labelWidth;
          }
      }
      else{
          me.height=56;
    	  if (me.fieldLabel=="")
    	  {
              me.labelWidth=0;
    		  me.width=90;
    	  }
    	  else if (me.fieldLabel!="")
    	  {
              me.labelWidth=55;
    		  me.width=260+me.labelWidth;
    	  }
      }
    },
    gettwoItems:function(){
        var me=this;
        var dateTwo =me.getComponent('twoItems');
        return dateTwo;
    },
    getdateOne:function(){
        var me=this;
        var dateTwo =me.gettwoItems().getComponent('dateOne');
        return dateTwo;
    },
    getdateTwo:function(){
        var me=this;
        var dateTwo =me.gettwoItems().getComponent('dateTwo');
        return dateTwo;
    },
    //生成日期区间:两个日期控件
    createDateTwo:function(){
        var me = this;
        var datetime = {
            layout:me.layoutType,//vbox,hbox
            border:false,
            dateTimeValue:me.value+me.valueTwo,
            itemId:'twoItems',
            name:me.name,
            items: [{
                xtype: 'datefield',
                itemId:'dateOne',
                //name:'dateOne',
                name:me.name,
                fieldLabel:me.fieldLabel,
                labelWidth:me.labelWidth,
                labelAlign: me.labelAlign,
                labelSeparator :me.labelSeparator,
                value:me.value,
                editable:me.editable,
                format:'Y-m-d',
                width:96+me.labelWidth,
                listeners:{
                    change:function(){
                        me.fireEvent('OnChanged');
                    }
                 }                
            },
            {
                xtype: 'datefield',
                itemId:'dateTwo',
                //name:'dateTwo',
                name:me.name,
                fieldLabel:me.fieldLabelTwo,
                labelWidth:me.labelWidth,
                labelAlign: me.labelAlign,
                labelSeparator :me.labelSeparator,
                value:me.valueTwo,
                editable:me.editable,
                format:'Y-m-d',
                width:96+me.labelWidth,
                listeners:{
                    change:function(){
                        me.fireEvent('TwoOnChanged');
                    }
                 }                
            }]
        };
        listeners:{
        }
        return datetime;
    },
    
    /**
     * 注册事件
     * @private
     */
    addAppEvents:function(){
    	var me = this;
    	me.addEvents('OnChanged');//当日期时间改变时触发
        me.addEvents('TwoOnChanged');//当日期时间改变时触发
    },
    /**
     * 组装组件内容
     * @private
     */
    setAppItems:function(){
    	var me = this;
    	var items = [];
        //两个日期
    	    var dateTwo = me.createDateTwo();
    	    if(dateTwo)
    		items.push(dateTwo);
  
    	me.items = items; 
    },
    /**
     * 初始化组件
     */
    initComponent:function(){
    	var me = this;
    	//常量设置

    	me.initConstants();
    	//注册事件
		me.addAppEvents();
    	//组装组件内容
    	me.setAppItems();
    	this.callParent(arguments);
    }
});
