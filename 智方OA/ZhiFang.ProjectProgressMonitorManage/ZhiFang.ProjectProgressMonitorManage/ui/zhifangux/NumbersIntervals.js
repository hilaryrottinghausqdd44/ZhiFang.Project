//非构建类--通用型组件或控件--数字区间(两个数字)控件
/**
 * 数字区间(两个数字)控件
 * 将两个数字控件封装在一个组件内，单击不同的控件展示不同的下拉内容
 * @param
 * fieldLabel：第一个控件的显示名称设置文本,默认为空
 * fieldLabelTwo:第二个控件的显示名称设置,默认为空
 * labelWidth：文本宽度，默认为60
 * labelAlign：文本对齐方式,默认右对齐
 * value：第一个控件数字默认值，默认为数字时间格式
 * valueTwo:第二个控件数字默认值
 * dateFormat:两个数字区间格式
 * layoutType:'hbox'控件布局设置
 * labelSeparator:':' 分隔符
 * 对外公开事件
 * OnChanged 当数字改变时触发
 * 
 * 对外公开方法
 * setFieldLabel 设置LabField：文本
 * getLabField  获取LabField
 * getValue 返回给后台的数据方法
 * setWidth 设置组件宽度
 * getWidth 返回组件宽度
 * setHeight 设置组件高度
 * getHeight 返回组件高度

 * 注意:getValue的结果值如为两个数字区间时,其的结果值用'|'作分割并合并,取该结果值时需要分割处理为两个结果
 */

Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.NumbersIntervals', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.numbersintervals', 
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
    
    maxValueOne:99999,
    maxValueTwo:99999,
    minValueOne:0.00001,
    minValueTwo:0.00001,
    value:'',
    valueTwo:'',
    bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
    cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white

    width:325,
    height:28,
    /**
     * 获第二个控件的取值
     * @return {}
     */
    getOneLabField: function () {
        var me = this;
        var dateTwo =this.getComponent('dateTwo');
        var dateTwo1 =dateTwo.getComponent('dateTwo1');
    	return dateTwo1.getFieldLabel();
    },
    /***
     * 获第二个控件的LabField取值
     * @return {}
     */
    getTwoLabField: function () {
        var me = this;
        var dateTwo =this.getComponent('dateTwo');
        var dateTwo1 =dateTwo.getComponent('dateTwo2');
        return dateTwo1.getFieldLabel();
    },
    /**
     * 将日期默认值参数设置到当前控件中
     * @param {} value1
     * @param {} value2
     */
    setValue: function (value1,value2) {
        var me = this;
        var dateTwo =this.getComponent('dateTwo');
        var dateTwo1 =dateTwo.getComponent('dateTwo1');
        var dateTwo2 =dateTwo.getComponent('dateTwo2');
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
        var dateTwo =this.getComponent('dateTwo');
        var dateTwo1 =dateTwo.getComponent('dateTwo1');
        var dateTwo2 =dateTwo.getComponent('dateTwo2');
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
    //获得当前结果值
    /***
     * 的结果值如为两个数字区间时,其的结果值用'|'作分割并合并,取该结果值时需要分割处理为两个结果
     * @return {}
     */
    getValue: function () {
        var me = this;
        var dateTwo =this.getComponent('dateTwo');
        var dateTwo1 =dateTwo.getComponent('dateTwo1');
        var dateTwo2 =dateTwo.getComponent('dateTwo2');
        var DateValue1 =dateTwo1.getValue();
        var DateValue2 =dateTwo2.getValue();
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
      if(me.layoutType=="hbox")//横向盒
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
      }else if(me.layoutType=="vbox"){
          me.height=56;
          if (me.fieldLabel==""||me.fieldLabelTwo=="")
          {
              me.labelWidth=0;
              me.width=100;
          }
          else if (me.fieldLabel!=""&&me.fieldLabelTwo!="")
          {
              me.labelWidth=55;
              me.width=105+me.labelWidth;
          }
      }else{
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

    //生成数字区间:两个数字控件
    createNumberfield:function(){
        var me = this;
        var datetime = {
            layout:me.layoutType,//vbox,hbox
            border:false,
            dateTimeValue:me.value+me.valueTwo,
            itemId:'dateTwo',
            items: [{
                xtype: 'numberfield',
		        maxValue:me.maxValueOne,
                minValue:me.minValueOne,
                itemId:'dateTwo1',
                fieldLabel:me.fieldLabel,
                labelWidth:me.labelWidth,
                labelAlign: me.labelAlign,
                labelSeparator :me.labelSeparator,

                width:96+me.labelWidth,
                listeners:{
                    change:function(){
                        me.fireEvent('OnChanged');
                    }
                 }                
            },
            {
                xtype: 'numberfield',
                maxValue:me.maxValueTwo,
                minValue:me.minValueTwo,
                itemId:'dateTwo2',
                fieldLabel:me.fieldLabelTwo,
                labelWidth:me.labelWidth,
                labelAlign: me.labelAlign,
                labelSeparator :me.labelSeparator,
                value:me.valueTwo,
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
        //两个数字
	    var numberfieldTwo = me.createNumberfield();
	    if(numberfieldTwo)
		items.push(numberfieldTwo);
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
