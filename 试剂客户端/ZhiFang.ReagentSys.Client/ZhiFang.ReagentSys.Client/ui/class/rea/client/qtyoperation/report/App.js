/**
 * 库存变化统计报表
 * @author longfc
 * @version 2019-06-06
 */
Ext.define('Shell.class.rea.client.qtyoperation.report.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '库存变化统计报表',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DocGrid.on({
			onAddClick: function() {
				me.isAdd();
			},
			select: function(RowModel, record) {
				me.isShow(record);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
		me.ShowPanel.on({
			save: function(p, id) {
				me.DocGrid.expand();
				me.DocGrid.autoSelect = id;
				me.DocGrid.onSearch();
			},
			onLaunchFullScreen: function() {
				me.DocGrid.collapse();
			},
			onExitFullScreen: function() {
				me.DocGrid.expand();
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
		me.DocGrid = Ext.create('Shell.class.rea.client.qtyoperation.report.DocGrid', {
			header: false,
			itemId: 'DocGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		me.ShowPanel = Ext.create('Shell.class.rea.client.qtyoperation.report.ShowPanel', {
			header: false,
			itemId: 'ShowPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DocGrid, me.ShowPanel];
		return appInfos;
	},
	clearData: function() {
		var me = this;
	},
	nodata: function(record) {
		var me = this;
		me.setFormType("show");
		me.ShowPanel.clearData();
		me.clearData();
	},
	setFormType: function(formtype) {
		var me = this;
		me.ShowPanel.formtype = formtype;
		me.ShowPanel.DocForm.formtype = formtype;
	},
	isAdd: function() {
		var me = this;
		me.DocGrid.collapse();
		me.ShowPanel.isAdd();
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.ShowPanel.isShow(record, me.DocGrid);
	}
});