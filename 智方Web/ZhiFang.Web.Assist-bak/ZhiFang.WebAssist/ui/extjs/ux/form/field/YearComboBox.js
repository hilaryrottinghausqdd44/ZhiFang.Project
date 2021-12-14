/**
 * 年份下拉框
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.form.field.YearComboBox',{
    extend:'Shell.ux.form.field.SimpleComboBox',
    alias:'widget.uxYearComboBox',
	/**最小年份*/
	minYearValue: 1900,
	/**最大年份*/
	maxYearValue: null,
	
	initComponent:function(){
		var me = this;
		var date = new Date();
		if(!me.maxYearValue)
			me.maxYearValue = date.getFullYear();
			
		var now = new Date().getFullYear();
		
		me.value = me.value || now;//默认当年
		me.data = [];
		//for(var i=now;i>=0;i--){
		for(var i = me.maxYearValue; i >= me.minYearValue; i--) {
			me.data.push([i,i+'年']);
		}
		
		me.callParent(arguments);
	}
});