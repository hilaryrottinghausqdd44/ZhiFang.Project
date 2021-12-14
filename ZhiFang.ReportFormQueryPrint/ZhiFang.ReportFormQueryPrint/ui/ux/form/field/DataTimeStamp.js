/**
 * 时间戳组件
 */
Ext.define('Shell.ux.form.field.DataTimeStamp',{
	extend:'Ext.form.field.Hidden',
	alias:'widget.uxdatatimestamp',
	
	getValue:function(bo){
		var value = this.callParent(arguments);
		if(bo){
			value = value ? value.split(',') : null; 
		}
		
		return value;
	}
});