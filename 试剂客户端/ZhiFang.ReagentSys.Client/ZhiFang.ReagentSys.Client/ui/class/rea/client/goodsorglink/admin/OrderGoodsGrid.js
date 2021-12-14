/**
 * 机构与货品管理维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.admin.OrderGoodsGrid', {
	extend: 'Shell.class.rea.client.goodsorglink.basic.OrderGoodsGrid',

	/**当前选择的机构Id*/
	ReaCenOrgId: null,
	/**应用类型:货品:goods;订货/供货:cenorg*/
	appType: "cenorg",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		me.callParent(arguments);
	}
});