/**
 * 环境卫生学及消毒灭菌效果监测--按全部科室
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.statistics.infection.ofalldept.OfDateTimeGrid', {
	extend: 'Shell.class.assist.statistics.infection.basic.OfDateTimeGrid',
	
	/**感控评估查询项*/
	IsHaveDept:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	}
});
