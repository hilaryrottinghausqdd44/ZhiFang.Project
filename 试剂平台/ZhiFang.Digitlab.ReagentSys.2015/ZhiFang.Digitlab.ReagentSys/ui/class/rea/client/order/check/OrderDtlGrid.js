/**
 * @description 订单审核
 * @author longfc
 * @version 2017-11-17
 */
Ext.define('Shell.class.rea.client.order.check.OrderDtlGrid', {
	extend: 'Shell.class.rea.client.order.basic.EditDtlGrid',

	title: '订单审核',
	width: 800,
	height: 500,

	/**录入:entry/审核:check*/
	OTYPE: "check",
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	}
});