/**
 * 日期组件扩展，重写了setValue方法，
 * 方法参数格式：2013-10-10 12:20:00
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.DateField',{
	extend:'Ext.form.field.Date',
	alias:'widget.zhifangux_datefield',
	setValue:function(value){
		var val = value;
		//数据处理
		if(Ext.typeOf(value) === 'string' && value.length == 19){
			val = new Date(value.slice(0,10));
		}
		this.callParent([val]);
	}
});