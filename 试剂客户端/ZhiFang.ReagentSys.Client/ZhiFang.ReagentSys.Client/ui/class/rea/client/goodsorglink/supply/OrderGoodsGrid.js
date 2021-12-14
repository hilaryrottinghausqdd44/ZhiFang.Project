/**
 * 供货商货品维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.supply.OrderGoodsGrid', {
	extend: 'Shell.class.rea.client.goodsorglink.basic.OrderGoodsGrid',

	/**当前选择的机构Id*/
	ReaCenOrgId: null,
	/**应用类型:货品:goods;订货/供货:cenorg*/
	appType: "cenorg",
	/**是否为供货商货品维护:是:true;否:false*/
	hasSupply: true,
	/**用户UI配置Key*/
	userUIKey: 'goodsorglink.supply.OrderGoodsGrid',
	/**用户UI配置Name*/
	userUIName: "供货商货品维护列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});