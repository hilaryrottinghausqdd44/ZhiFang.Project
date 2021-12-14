/**
 * 产品采购供应维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.admin.cenorg.App', {
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
		me.CenOrgGrid.on({
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
				me.OrderGoodsGrid.ReaCenOrgId=null;
				me.OrderGoodsGrid.addFormDefault=null;
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
		me.CenOrgGrid = Ext.create('Shell.class.rea.client.goodsorglink.admin.cenorg.CenOrgGrid', {
			header: false,
			itemId: 'CenOrgGrid',
			region: 'west',
			width: 360,
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.OrderGoodsGrid = Ext.create('Shell.class.rea.client.goodsorglink.admin.cenorg.OrderGoodsGrid', {
			header: false,
			itemId: 'OrderGoodsGrid',
			region: 'center',
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.CenOrgGrid, me.OrderGoodsGrid];
		return appInfos;
	},
	loadGridData: function(record) {
		var me = this;
		var id=record.get("ReaCenOrg_Id");
		//新增表单时的默认值处理
		me.OrderGoodsGrid.addFormDefault={
			"ReaGoodsOrgLink_CenOrg_Id":id,
			"ReaGoodsOrgLink_CenOrg_CName":record.get("ReaCenOrg_CName"),
			"ReaGoodsOrgLink_CenOrg_OrgType":record.get("ReaCenOrg_OrgType")
		};
		me.OrderGoodsGrid.ReaCenOrgId=id;
		me.OrderGoodsGrid.defaultWhere = "reagoodsorglink.CenOrg.Id=" + id;
		me.OrderGoodsGrid.onSearch();
	}
});