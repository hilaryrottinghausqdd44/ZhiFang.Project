/**
 * 年份下拉框
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.form.field.YearComboBox',{
    extend:'Shell.ux.form.field.SimpleComboBox',
    alias:'widget.uxYearComboBox',
	
	/**最小年份*/
	minValue:1900,
	
	initComponent:function(){
		var me = this;
		var now = new Date().getFullYear();
		
		me.value = me.value || now;//默认当年
		me.data = [];
		for(var i=now;i>=me.minValue;i--){
			me.data.push([i,i+'年']);
		}
		
		me.callParent(arguments);
	}
});