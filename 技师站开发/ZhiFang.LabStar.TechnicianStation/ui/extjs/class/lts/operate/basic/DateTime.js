/**
 * 日期+时间
 * @author liangyl
 * @version 2020-05-08
 */
Ext.define('Shell.class.lts.operate.basic.DateTime', {
	extend: 'Shell.ux.form.Panel',
	alias:'widget.datatimefield',
	bodyPadding: '0px 0px 0px 0px',
	border:false,
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
    autoScroll : false,
	header:false,
    /**带功能按钮栏*/
	hasButtontoolbar:false,
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchETempletById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/QMS_UDTO_AddExcelTemplet',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/QMS_UDTO_UpdateExcelTemplet',
	/**显示成功信息*/
	showSuccessInfo: false,
	height: 30,
	width: 265,
	fieldLabel:' ',
	title: "日期+时间",
	formtype: 'add',
	/**默认日期*/
	defaultDateValue:null,
	/**默认时间*/
	defaultTimeValue:null,
	allowBlank:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
	    me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(items){
		var me = this;
		return [ {
			xtype: 'datefield',fieldLabel:me.fieldLabel,
			labelSeparator: '', labelWidth: 5,colspan :1,
			width : 100,editable:true,
			format: 'Y-m-d',
			allowBlank: me.allowBlank, //不允许为空
			value:me.defaultDateValue,name: 'Data',itemId: 'Data'
		},{
			xtype: 'timefield',labelAlign: 'right',
			width : 75,value:me.defaultTimeValue,
			allowBlank: me.allowBlank, //不允许为空
			format: 'H:i:s',name: 'Time',itemId: 'Time'
		}];
	},
	setValue:function(value){
		var me=this;
	    var Sysdate=me.getComponent('Data');
	    var Time=me.getComponent('Time');
	    var operatedate =JShell.Date.toString(value, true);
	    Sysdate.setValue(operatedate);
	    var operatedate2 = JShell.Date.toString(value);
	    var time=me.toString(operatedate2);
	    if(time){
	    	time=time.replace(/(^\s*)|(\s*$)/g,"");
	    }
	     //只有为空时
	    if(time && time.length==1)time='';
	    
	   	Time.setValue(time);
	},
	/**获取时间字符串
	 *返回时分秒
	 */
	toString: function(value) {
		var v = JShell.Date.getDate(value);
		if (!v) return null;

		var info = '',
			year = v.getFullYear() + '',
			month = (v.getMonth() + 1) + '',
			date = v.getDate() + '';

		month = month.length == 1 ? '0' + month : month;
		date = date.length == 1 ? '0' + date : date;
		var hours = v.getHours() + '',
			minutes = v.getMinutes() + '',
			seconds = v.getSeconds() + '';

		hours = hours.length == 1 ? '0' + hours : hours;
		minutes = minutes.length == 1 ? '0' + minutes : minutes;
		seconds = seconds.length == 1 ? '0' + seconds : seconds;

		info += hours + ':' + minutes + ':' + seconds;
		return info;
	},
	getValue:function(){
		var me=this;
		var Data=me.getComponent('Data').getValue();
	    var Time=me.getComponent('Time').getValue();
	    var dataValue= Ext.util.Format.date(Data, 'Y-m-d');
	    var timeValue= Ext.util.Format.date(Time, 'H:i:s');
	     var data=dataValue + ' '+ timeValue;
	    if(Data && !Time)data=dataValue+' 00:00:00';
	    var Sysdate = JShell.System.Date.getDate();
		var operatedate = JShell.Date.toString(Sysdate, true);
	    if(!Data && Time && operatedate)data=operatedate+' '+Time;
	    //只有为空时
	    if(data && data.length==1)data='';
	    return data;
	},
	isAllowBlank:function(bo){
		var me = this;
		if(bo){
			me.getComponent('Data').allowBlank = true;
			me.getComponent('Data').emptyText = "";
	        me.getComponent('Data').reset();  
	        me.getComponent('Time').allowBlank = true;
			me.getComponent('Time').emptyText = "";
	        me.getComponent('Time').reset();  
		}else{
			me.getComponent('Data').allowBlank = false;
			me.getComponent('Data').emptyText = "必填项";
	        me.getComponent('Data').reset();  
	        me.getComponent('Time').allowBlank = false;
			me.getComponent('Time').emptyText = "必填项";
	        me.getComponent('Time').reset();  
			
		}
	}
});