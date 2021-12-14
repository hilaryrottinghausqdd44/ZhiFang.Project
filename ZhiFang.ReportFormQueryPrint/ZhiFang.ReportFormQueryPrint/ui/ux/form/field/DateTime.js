/**
 * 日期时间组件
 * @author Jcall
 * @version 2014-08-04
 */
Ext.define('Shell.ux.form.field.DateTime',{
	extend:'Ext.container.Container',
	alias:['widget.uxdatetimefield','widget.uxdatetime'],
	
	/**组件宽度*/
	width:215,
	/**文字宽度*/
	labelWidth:60,
	/**日期框宽度*/
	dateWidth:95,
	/**对齐方式*/
	labelAlign:'left',
    
	/**初始化组件属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('change');
		me.layout = 'hbox';
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			timeWidth = me.width - me.labelWidth - me.dateWidth - 1,
			date = {xtype:'datefield',itemId:'date',format:'Y-m-d',labelAlign:me.labelAlign},
			time = {xtype:'timefield',itemId:'time',format:'H:i',margin:'0 0 0 1'};
		
		date.labelWidth = me.labelWidth;
		date.width = me.labelWidth + me.dateWidth;
		time.width = timeWidth;
		
		if(me.fieldLabel){date.fieldLabel = me.fieldLabel};
		
		date.listeners = {
			blur:function(field){
				if(field.getValue() == null){
					field.ownerCt.getComponent('time').setValue(null);
				}
			}
		};
		
		return [date,time];
	},
	/**赋值*/
	setValue:function(value){
		var me = this,
			date = me.getComponent('date'),
			time = me.getComponent('time'),
			da = Shell.util.Date.getDate(value),
			d = t = da ? da : null;
			
		date.setValue(d);
		time.setValue(t);
	},
	/**取值*/
	getValue:function(){
		var me = this,
			date = me.getComponent('date'),
			time = me.getComponent('time');
			
		if(!date.isValid() || !time.isValid()) return null;
			
		var d = date.getValue(),
			t = time.getValue();
		
		if(d == null) return null;
		
		var hours = 0,
			minutes = 0,
			seconds = 0,
			milliseconds = 0;
			
		if(t){
			hours = t.getHours();
			minutes = t.getMinutes();
			seconds = t.getSeconds();
			milliseconds = t.getMilliseconds();
		}
			
		d.setHours(hours);
		d.setMinutes(minutes);
		d.setSeconds(seconds);
		d.setMilliseconds(milliseconds);
		
		return d;
	},
	/**校验数据有效性*/
	isValid:function(){
		var me = this,
			date = me.getComponent('date'),
			time = me.getComponent('time');
        	
        return (date.isValid() && time.isValid());
	}
});