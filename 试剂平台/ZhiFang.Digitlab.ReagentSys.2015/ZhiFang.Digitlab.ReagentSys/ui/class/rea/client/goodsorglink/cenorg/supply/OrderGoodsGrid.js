/**
 * 供货方货品维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.cenorg.supply.OrderGoodsGrid', {
	extend: 'Shell.class.rea.client.goodsorglink.cenorg.basic.OrderGoodsGrid',

	/**当前选择的机构Id*/
	ReaCenOrgId: null,
	/**应用类型:产品:goods;订货/供货:cenorg*/
	appType: "cenorg",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: 'btnCheck',
			text: '产品选择新增',
			tooltip: '产品选择',
			handler: function() {
				me.onCheckGoodsClick();
			}
		});
		items.push('-', 'edit', 'save', 'del');

		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	}
});