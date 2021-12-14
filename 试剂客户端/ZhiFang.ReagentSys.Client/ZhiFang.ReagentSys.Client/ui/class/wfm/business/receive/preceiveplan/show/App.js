/**
 * 收款计划查询
 * @author liangyl
 * @version 2017-03-16
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.show.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '收款计划查询',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var ContractID = record.get('PContractID');
					me.ContentPanel.load(ContractID);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var ContractID = record.get('PContractID');
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
		me.Grid = Ext.create('Shell.class.wfm.business.receive.preceiveplan.show.GridTree', {
			region: 'center',
			header: false,
			ISADMIN:me.ISADMIN,
			title: '收款计划',
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