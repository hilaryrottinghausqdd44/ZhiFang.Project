/**
 * @description 订单上传
 * @author longfc
 * @version 2017-11-21
 */
Ext.define('Shell.class.rea.client.order.uploadplatform.OrderDtlGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderDtlGrid',
	title: '订货明细列表',

	/**当前选择的供应商Id*/
	ReaCompID: null,
	/**录入:entry/审核:check*/
	OTYPE: "upload",
	/**是否多选行*/
	checkOne: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		if(!me.checkOne) me.setCheckboxModel();
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品中文名',
			fields: ['bmscenorderdtl.GoodsName']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	}
});