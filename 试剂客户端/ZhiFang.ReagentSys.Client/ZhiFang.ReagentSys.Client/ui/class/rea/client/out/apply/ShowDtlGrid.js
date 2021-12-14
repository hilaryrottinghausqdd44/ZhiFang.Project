/**
 * 出库申请明细
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.apply.ShowDtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.ShowDtlGrid',

	/**用户UI配置Key*/
	userUIKey: 'out.apply.ShowDtlGrid',
	/**用户UI配置Name*/
	userUIName: "出库申请明细查看列表",

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(10, 0, {
			dataIndex: 'ReaBmsOutDtl_ReqGoodsQty',
			text: '申请数',
			sortable: false,
			width: 75,
			defaultRenderer: true
		});
		return columns;
	}
});