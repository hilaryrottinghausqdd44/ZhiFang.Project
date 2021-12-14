/**
 * 月份下拉框
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.form.field.MonthComboBox',{
    extend:'Shell.ux.form.field.SimpleComboBox',
    alias:'widget.uxMonthComboBox',
	
	/**默认一月*/
	value:1,
	
	initComponent:function(){
		var me = this;
		
		me.data = [
			[1,'一月'],
			[2,'二月'],
			[3,'三月'],
			[4,'四月'],
			[5,'五月'],
			[6,'六月'],
			[7,'七月'],
			[8,'八月'],
			[9,'九月'],
			[10,'十月'],
			[11,'十一月'],
			[12,'十二月']
		];
		
		me.callParent(arguments);
	}
});