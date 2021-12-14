/**
 * 日期组件
 * @author Jcall
 * @version 2014-08-06
 */
Ext.define('Shell.ux.form.field.Date',{
	extend:'Ext.form.field.Date',
	alias:'widget.uxdatefield',
	
	width:157,
	labelWidth:60,
	format:'Y-m-d',
	
	/**赋值*/
	setValue:function(value,bo){
		if(bo){
			value = Shell.util.Date.getDate(value);
			this.callParent(value);
		}else{
			this.callParent(arguments);
		}
	}
});