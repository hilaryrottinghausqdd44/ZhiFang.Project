/**
 * 订货方货品维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.lab.App', {
	extend: 'Ext.panel.Panel',

	title: '订货方货品维护',
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
		
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.CenOrgTree = Ext.create('Shell.class.rea.client.reacenorg.Tree', {
			header: false,
			/**机构类型*/
			OrgType: "1",
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
		me.OrderGoodsGrid = Ext.create('Shell.class.rea.client.goodsorglink.lab.OrderGoodsGrid', {
			header: false,
			itemId: 'OrderGoodsGrid',
			region: 'center',
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.CenOrgTree, me.OrderGoodsGrid];
		return appInfos;
	},
	onListeners:function(){
		var me=this;
		me.CenOrgTree.on({
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
				me.OrderGoodsGrid.ReaCenOrgId = null;
				me.OrderGoodsGrid.addFormDefault = null;
				me.OrderGoodsGrid.clearData();
			}
		});
	},
	loadGridData: function(record) {
		var me = this;
		var id = record.get("tid");
		var text = record.get("text");
		if(text&&text.indexOf("]")>=0){
			text=text.split("]")[1];
			text=Ext.String.trim(text);
		}
		me.OrderGoodsGrid.addFormDefault = {
			"ReaGoodsOrgLink_CenOrg_Id": id,
			"ReaGoodsOrgLink_CenOrg_CName": text,
			"ReaGoodsOrgLink_CenOrg_OrgType": 1
		};
		me.OrderGoodsGrid.ReaCenOrgId = id;
		me.OrderGoodsGrid.defaultWhere = "reagoodsorglink.CenOrg.OrgType=1 and reagoodsorglink.CenOrg.Id=" + id;
		me.OrderGoodsGrid.onSearch();
	}
});