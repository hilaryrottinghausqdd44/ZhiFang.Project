/**
 * 机构与货品管理维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.admin.App', {
	extend: 'Ext.panel.Panel',

	title: '机构与货品管理维护',
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

		me.CenOrgTree.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.loadGridData(record);
				}, null, 500);
			},
			select: function(rowModel, record, index, eOpts) {
				JShell.Action.delay(function() {
					me.loadGridData(record);
				}, null, 500);
			},
			nodata: function(p) {
				me.OrderGoodsGrid.defaultWhere = "";
				me.OrderGoodsGrid.ReaCenOrgId = null;
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
		me.CenOrgTree = Ext.create('Shell.class.rea.client.goodsorglink.admin.Tree', {
			header: false,
			itemId: 'CenOrgTree',
			region: 'west',
			width: 360,
			split: true,
			rootVisible: false,
			collapsible: true,
			collapsed: false,
			animCollapse: false,
			animate: false
		});

		me.OrderGoodsGrid = Ext.create('Shell.class.rea.client.goodsorglink.admin.OrderGoodsGrid', {
			header: false,
			itemId: 'OrderGoodsGrid',
			region: 'center',
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.CenOrgTree, me.OrderGoodsGrid];
		return appInfos;
	},
	loadGridData: function(record) {
		var me = this;
		var id = record.get("tid");
		var text = record.get("text");
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		var orgType=record ? record.data.value.OrgType : '';
		//新增表单时的默认值处理
		me.OrderGoodsGrid.addFormDefault = {
			"ReaGoodsOrgLink_CenOrg_Id": id,
			"ReaGoodsOrgLink_CenOrg_CName": text,
			"ReaGoodsOrgLink_CenOrg_OrgType": orgType
		};
		me.OrderGoodsGrid.ReaCenOrgId = id;
		me.OrderGoodsGrid.defaultWhere = "reagoodsorglink.CenOrg.Id=" + id;
		me.OrderGoodsGrid.onSearch();
	}
});