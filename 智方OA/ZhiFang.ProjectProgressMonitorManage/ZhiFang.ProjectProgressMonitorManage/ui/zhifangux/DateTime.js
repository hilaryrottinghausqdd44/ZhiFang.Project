﻿//非构建类--通用型组件或控件--日期+时间控件
/**
 * 日期+时间控件
 * 将日期控件和时间控件封装在一个组件内，单击不同的控件展示不同的下拉内容
 * @param
 * labField：文本,默认为空
 * labFieldWidth：文本宽度，默认为60
 * labFieldAlign：文本对齐方式,默认右对齐
 * DateTimeValue：日期时间值，默认为日期时间格式
 * dateFormat：日期格式
 * timeFormat：时间格式
 * timeStep：时间布距
 * 
 * 对外公开事件
 * OnChanged//当日期时间改变时触发
 * 
 * 对外公开方法
 * setFieldLabel //设置LabField：文本
 * GetLabField//获取LabField
 * SetValue//将日期时间参数设置到当前控件中
 * GetValue//获取当前控件的值(日期+时间)
 * getValue//获取当前控件的值(日期+时间)
 */

Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.DateTime', {
    extend: 'Ext.container.Container',
    alias: 'widget.datetimefield', 
    layout:'absolute',
    border:false,
    name:'',
    labField:'',
    labFieldWidth:60,
    labFieldAlign:'right',
    DateTimeValue:new Date(),
    DateTimeValue1:new Date(),
    dateFormat: 'Y-m-d',
    timeFormat: 'H:i',
    width:155,
    timeStep:30,
    editable: true,//是否允许编辑
    setType:'datetime',//控件类型:datetime(日期时间),date(日期),time(时间),
    
    setReadOnly:function(obj){
        var me=this;
        if(me.setType=='date'){
            var itemDate =me.getitemDate();
            itemDate.setReadOnly(obj);
        } 
        if (me.setType=='time'){
            var itemTime =me.getitemTime();
            itemTime.setReadOnly(obj);
        }if (me.setType=='datetime'){
            var itemDate =me.getdatetimeOne();
            itemDate.setReadOnly(obj);
            var itemTime =me.getdatetimeTwo();
            itemTime.setReadOnly(obj);
        }  
    },
    getitemDate: function () {
      var me = this;
      var itemDate=me.getComponent('itemDate');
      return itemDate;
    },
    getitemTime: function () {
      var me = this;
      var itemTime=me.getComponent('itemTime');
      return itemTime;
    },
    getdatetime: function () {
      var me = this;
      var datetime=me.getComponent('dateTime');
      return datetime;
    },
    getdatetimeOne: function () {
      var me = this;
      var datetimeOne=me.getdatetime().getComponent('itemDate');
      return datetimeOne;
    },
    getdatetimeTwo: function () {
      var me = this;
      var datetimeTwo=me.getdatetime().getComponent('itemTime');
      return datetimeTwo;
    },
    //方法
    //获LabField取值
    getLabField: function () {
        var me = this;
        var y =me.getitemDate();
    	return y.getFieldLabel();
    	var y4 =me.getitemTime();
    	return y4.getFieldLabel();
    	var y1 =me.getdatetimeOne();
    	return y1.getFieldLabel();
    },

    //将日期时间参数设置到当前控件中
    setValue: function (value) {
        var me = this;
        if(me.setType=='date'){
            var y =me.getitemDate();
            y.setValue(value);
        } 
        if (me.setType=='time'){
            var y4 =me.getitemTime();
            y4.setValue(value);
        }if (me.setType=='datetime'){
            var y1 =me.getdatetimeOne();
            y1.setValue(value);
            var y2 =me.getdatetimeTwo();
            y2.setValue(value);
        }
    },
    //写入LabField
    setFieldLabel: function (value) {
    	var me = this;
        if(me.setType=='date'){
    		var y =me.getitemDate();
	    	y.setFieldLabel(value);  
	    }
        if (me.setType=='time'){
	    	var y4 =me.getitemTime();
	    	y4.setFieldLabel(value);
        }if (me.setType=='datetime'){
	    	var y1 =me.getdatetimeOne();
	    	y1.setFieldLabel(value);
        }

    },
    //获得当前时间值 
    getValue: function () {
        var me = this;
        var dateValue ='';
        if(me.setType=='date'){
            var y =me.getitemDate();
            dateValue =Ext.util.Format.date(y.getValue(), 'Y-m-d');
        }
        if (me.setType=='time'){
            var y4 =me.getitemTime();
            dateValue =Ext.util.Format.date(y4.getValue(), 'H:i');
        }
        if (me.setType=='datetime'){
            var y1 =me.getdatetimeOne();
            var DateValue1 =Ext.util.Format.date(y1.getValue(), 'Y-m-d');
            var y2 =me.getdatetimeTwo();
            var DateValue2 =Ext.util.Format.date(y2.getValue(), 'H:i');
            dateValue=DateValue1+ ' ' +DateValue2;
        }
        
        return dateValue;
    },
    //获得当前时间值 
    getRawValue: function () {
        var me = this;
        var dateValue ='';
        if(me.setType=='date'){
            var y =me.getitemDate();
            dateValue =Ext.util.Format.date(y.getValue(), 'Y-m-d');
        }
        if (me.setType=='time'){
            var y4 =me.getitemTime();
            dateValue =Ext.util.Format.date(y4.getValue(), 'H:i');
        }
        if (me.setType=='datetime'){
            var y1 =me.getdatetimeOne();
            var DateValue1 =Ext.util.Format.date(y1.getValue(), 'Y-m-d');
            var y2 =me.getdatetimeTwo();
            var DateValue2 =Ext.util.Format.date(y2.getValue(), 'H:i');
            dateValue=DateValue1+ ' ' +DateValue2;
        }
    
        return dateValue;
    },
    /**
	 * 常量设置
	 * @private
	 */
    initConstants: function () {
         var me = this;
       if(me.setType=='date'){
    	  if (me.labField==""){
    		  me.width=95;
    	  }else if (me.labField!=""){
    		  me.width=100+me.labFieldWidth;
    	  }
      }
      if (me.setType=='time'){
    	  if(me.timeFormat=="h:i" || me.timeFormat=="H:i" ||me.timeFormat=="g:i"||me.timeFormat=="gi" ) 
    	  {
    		  if (me.labField==""){
        		  me.width=60;
        	  }else if (me.labField!=""){
        		  me.width=65+me.labFieldWidth;
        	  }
    	  }else {
    		  if (me.labField==""){
        		  me.width=90;
        	  }else if (me.labField!=""){
        		  me.width=95+me.labFieldWidth;
        	  }
    	 }
      }
      if(me.setType=='datetime'){
    	  if (me.labField==""){
    		  me.width=180;
    	  }else if (me.labField!=""){
    		  me.width=160+me.labFieldWidth;
    	  }
      }
    },
    //生成日期格式
    createDate:function(){
    	var me = this;
    	var date = {
    	    //itemId:'data',
            itemId:'itemDate',
            editable:me.editable,
    		xtype: 'datefield',
    		fieldLabel:me.labField,
    		labelWidth:me.labFieldWidth,
    		labelAlign: me.labFieldAlign,
    		value: me.DateTimeValue,
    		labelSeparator :'',
    		name:me.name,
            format:'Y-m-d',
            width:me.width,
            enableKeyEvents: true
    	};
        date.listeners = { 
    	   change:function(){
     	   //注册监听
     	       me.fireEvent('OnChanged');
           },
           keyup:function(){   

           }
    	};
    	return date;
    },
    //生成时间格式
    createTime:function(){
    	var me = this;
    	var time = {
    		xtype: 'timefield',
    		//itemId:'t',
            itemId:'itemTime',
            editable:me.editable,
    		fieldLabel:me.labField,
    		labelWidth:me.labFieldWidth,
    		labelAlign: me.labFieldAlign,
    		value: me.DateTimeValue,
            format:'H:i',
            name:me.name,
            increment:me.timeStep,
            labelSeparator :'',
            width:me.width
    	};
        time.listeners = { 
    	   change:function(){
     	   //注册监听
     	        me.fireEvent('OnChanged');
           } 
    	};
    	return time;
    },
    /***
     * 生成日期时间
     * getform.getValues()获取值有问题,日期时间相同的name相同时返回一个数组
     * @return {}
     */
    createDateTime:function(){
    	var me = this;
    	var datetime = {
    		layout:'hbox',
            //xtpye:'form',
    		border:false,
    		itemId:'dateTime',
            name:me.name,
            
    		DateTimeValue:me.DateTimeValue+me.DateTimeValue,
    		items: [{
    			xtype: 'datefield',
                editable:me.editable,
    			//name:'itemDate',
                itemId:'itemDate',
                name:me.name,
        		fieldLabel:me.labField,
        		labelWidth:me.labFieldWidth,
        		labelAlign: me.labFieldAlign,
        		labelSeparator :'',
        		value:me.DateTimeValue,
                format:'Y-m-d',
                width:95+me.labFieldWidth,
                listeners:{
    			    change:function(){
    			        me.fireEvent('OnChanged');
    	           	}
    		     }                
    		},
    		{
    			xtype: 'timefield',
    			value:me.DateTimeValue,
                editable:me.editable,
                format:'H:i',
                increment:me.timeStep,
                name:'itemTime',
                itemId:'itemTime',
                //name:me.name,
                width:60,
                listeners:{
			        change:function(){
			            me.fireEvent('OnChanged');
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
    },
    /**
     * 组装组件内容
     * @private
     */
    setAppItems:function(){
    	var me = this;
    	var items = [];
    	//日期
        if(me.setType=='date')
         {
    	    var date = me.createDate();
    	    if(date)
    		items.push(date);
         }
    	//时间
        if (me.setType=='time'){
    	    var time = me.createTime();
    	    if(time)
    		items.push(time);
        }
        if (me.setType=='datetime'){
            var dateTime = me.createDateTime();
            
            if(dateTime){
	            //获得当前时间值
                dateTime.setValue=function (valuestr) {
                    if(valuestr&&valuestr!=undefined){
                        dateTime.value=valuestr;
                    }else{
	                    var y1 =dateTime.getComponent('itemDate');
	                    var DateValue1 =Ext.util.Format.date(y1.getValue(), 'Y-m-d');
	                    var y2 =dateTime.getComponent('itemTime');
	                    var DateValue2 =Ext.util.Format.date(y2.getValue(), 'H:i');
	                    dateValue=DateValue1+ ' ' +DateValue2;
                        dateTime.value=dateValue;
                    }
                   return dateValue;
                };
	            dateTime.getValue=function () {
	                var y1 =dateTime.getComponent('itemDate');
	                var DateValue1 =Ext.util.Format.date(y1.getValue(), 'Y-m-d');
	                var y2 =dateTime.getComponent('itemTime');
	                var DateValue2 =Ext.util.Format.date(y2.getValue(), 'H:i');
	                dateValue=DateValue1+ ' ' +DateValue2;
	               return dateValue;
	            };
	            dateTime.getRawValue=function () {
	                var y1 =dateTime.getComponent('itemDate');
	                var DateValue1 =Ext.util.Format.date(y1.getValue(), 'Y-m-d');
	                var y2 =dateTime.getComponent('itemTime');
	                var DateValue2 =Ext.util.Format.date(y2.getValue(), 'H:i');
	                dateValue=DateValue1+ ' ' +DateValue2;
	               return dateValue;
	            };
            }
            items.push(dateTime);
        }
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
