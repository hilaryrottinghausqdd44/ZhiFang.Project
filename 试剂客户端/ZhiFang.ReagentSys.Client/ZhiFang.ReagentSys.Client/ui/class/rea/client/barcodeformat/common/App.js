/**
 * 公共条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.common.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '公共条码规则维护',
	header: false,
	border: false,

	layout: {
		type: 'border'
	},
	/**应用类型:是否平台:是:1,否:0或null*/
	APPTYPE: "1",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.loadData(record);
				}, null, 500);
			},
			select: function(rowModel, record, index, eOpts) {
				JShell.Action.delay(function() {
					me.loadData(record);
				}, null, 500);
			},
			addclick: function(p) {
				var count = me.Grid.getStore().count();
				if(!count) count = 0;
				me.Form.DispOrder = count + 1;
				me.Form.isAdd();
			},
			editclick: function(p, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				me.Form.PK = "";
				me.Form.PlatformOrgNo = null;
				me.Form.clearData();
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch();
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

		me.Grid = Ext.create('Shell.class.rea.client.barcodeformat.common.Grid', {
			header: false,
			itemId: 'Grid',
			region: 'center',
			collapsible: true,
			collapsed: false,
			APPTYPE: me.APPTYPE
		});
		me.Form = Ext.create('Shell.class.rea.client.barcodeformat.basic.Form', {
			header: true,
			/**条码规则分类为按公共部分和按供应商部分*/
			Category: 1,
			itemId: 'Form',
			region: 'east',
			width: 535,
			split: true,
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.Grid, me.Form, ];
		return appInfos;
	},
	loadData: function(record) {
		var me = this;
		var id = record.get("ReaCenBarCodeFormat_Id");
		me.Form.Category = 1;
		me.Form.PK = id;
		if(me.APPTYPE == "1")
			me.Form.isEdit(id);
		else
			me.Form.isShow(id);
	}
});