/**
 * 出库审批
 * @author longfc
 * @version 2019-03-18
 */
Ext.define('Shell.class.rea.client.out.approval.ShowDtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.ShowDtlGrid',

	/**用户UI配置Key*/
	userUIKey: 'out.approval.ShowDtlGrid',
	/**用户UI配置Name*/
	userUIName: "出库审批明细查看列表",

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(10, 0, {
			dataIndex: 'ReaBmsOutDtl_ReqGoodsQty',
			text: '申请数',
			sortable: false,
			width: 80,
			defaultRenderer: true
		});
		return columns;
	}
});