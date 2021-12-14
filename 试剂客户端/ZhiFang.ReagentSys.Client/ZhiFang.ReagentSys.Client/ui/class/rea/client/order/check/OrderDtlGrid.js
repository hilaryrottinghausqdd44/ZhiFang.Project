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
	/**用户UI配置Key*/
	userUIKey: 'order.check.OrderDtlGrid',
	/**用户UI配置Name*/
	userUIName: "订单审核明细列表",
	
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.push({
			dataIndex: 'ReaBmsCenOrderDtl_IOFlag',
			text: '提取标志',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				if(!value)value=0;
				if(value)value=parseInt(value);
				var info = JShell.REA.Enum.ReaBmsCenOrderDoc_IOFlag['E' + value] || {};
				var v = info|| value;
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
			
				return v;
			}});
		return columns;
	}
});