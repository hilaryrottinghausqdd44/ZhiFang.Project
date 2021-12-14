/**
 * 发票开具列表
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.invoice.opener.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '发票开具列表',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var ContractID = record.get('PInvoice_ContractID');
					me.ContentPanel.load(ContractID);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var ContractID = record.get('PInvoice_ContractID');
					me.ContentPanel.load(ContractID);
				}, null, 500);
			},
			nodata: function(p) {
				me.ContentPanel.clearData();
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
		me.Grid = Ext.create('Shell.class.wfm.business.invoice.opener.Grid', {
			region: 'center',
			header: false,
			title: '发票',
			itemId: 'Grid'
		});
		me.ContentPanel = Ext.create('Shell.class.wfm.business.contract.basic.ContentPanel', {
			//			header: false,
			title: '合同信息',
			region: 'east',
			width: 240,
			split: true,
			collapsible: true,

			itemId: 'ContentPanel'
		});
		return [me.ContentPanel, me.Grid];
	}
});