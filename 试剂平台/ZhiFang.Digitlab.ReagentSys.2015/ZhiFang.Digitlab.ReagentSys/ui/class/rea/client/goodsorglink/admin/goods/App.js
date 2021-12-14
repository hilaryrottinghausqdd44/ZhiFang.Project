/**
 * 产品采购供应维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.admin.goods.App', {
	extend: 'Ext.panel.Panel',

	title: '订货信息维护',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.GoodsGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.loadGridData(record);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.loadGridData(record);
				}, null, 500);
			},
			nodata: function(p) {
				me.OrderGoodsGrid.defaultWhere = "";
				me.OrderGoodsGrid.GoodsId = null;
				me.OrderGoodsGrid.addFormDefault = null;
				me.OrderGoodsGrid.clearData();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.GoodsGrid = Ext.create('Shell.class.rea.client.goodsorglink.admin.goods.GoodsGrid', {
			header: false,
			itemId: 'GoodsGrid',
			region: 'west',
			width: 360,
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.OrderGoodsGrid = Ext.create('Shell.class.rea.client.goodsorglink.admin.goods.OrderGoodsGrid', {
			header: false,
			itemId: 'OrderGoodsGrid',
			region: 'center',
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.GoodsGrid, me.OrderGoodsGrid];
		return appInfos;
	},
	loadGridData: function(record) {
		var me = this;
		var id = record.get("ReaGoods_Id");
		//新增表单时的默认值处理
		me.OrderGoodsGrid.addFormDefault = {
			"ReaGoodsOrgLink_ReaGoods_Id": id,
			"ReaGoodsOrgLink_ReaGoods_CName": record.get("ReaGoods_CName")
		};
		me.OrderGoodsGrid.GoodsId = id;
		me.OrderGoodsGrid.defaultWhere = "reagoodsorglink.ReaGoods.Id=" + id;
		me.OrderGoodsGrid.onSearch();
	}
});