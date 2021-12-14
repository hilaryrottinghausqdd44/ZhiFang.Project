/**
 * 客户端订单明细信息列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.apply.OrderDtlGrid', {
	extend: 'Shell.class.rea.client.order.basic.EditDtlGrid',

	height: 300,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	}
});