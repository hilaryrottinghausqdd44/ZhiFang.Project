/**
 * 院感统计--环境卫生学监测报告
 * @author longfc
 * @version 2020-12-15
 */
Ext.define('Shell.class.assist.statistics.infection.ofalldept.OfHRegistrChecklist', {
	extend: 'Shell.class.assist.statistics.infection.basic.OfHRegistrChecklist',
	
	/**感控评估查询项*/
	IsHaveDept:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onSearch();
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	}
});
