/**
 * 订货方货品维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.cenorg.order.CenOrgGrid', {
	extend: 'Shell.class.rea.client.goodsorglink.cenorg.basic.CenOrgGrid',

	defaultWhere:'reacenorg.Visible=1 and reacenorg.OrgType=1',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];

		//查询框信息
		me.searchInfo = {
			width: 205,
			emptyText: '机构名称/机构编码',
			isLike: true,
			itemId: 'Search',
			fields: ['reacenorg.CName', 'reacenorg.OrgNo'] // 'reacenorg.EName',
		};
		items.push('refresh', '-', {
			type: 'search',
			info: me.searchInfo
		});

		return items;
	}
});