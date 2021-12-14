/**
 * 发票申请(销售)
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.invoice.apply.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '发票申请',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ContractGrid.on({
			onAcceptClick: function(com, PayOrgID, PayOrgName,VAT) {
				me.Grid.PayOrgID = PayOrgID;
				me.Grid.PayOrgName = PayOrgName;
				me.Grid.VAT=VAT;
				if(!PayOrgID){
					me.Grid.clearData();
				}
			},
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.ContractGrid.PKField);
					var Name = record.get('PContract_Name');
//					var InvoiceMoney = record.get('PContract_InvoiceMoney');
					var Amount = record.get('PContract_Amount');
//					me.Grid.InvoiceMoney = InvoiceMoney;
					me.Grid.ContractInvoiceMoney = Amount;
					me.Grid.ContractID = id;
					me.Grid.ContractName = Name;
					me.Grid.onSearch();
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.ContractGrid.PKField);
					var Name = record.get('PContract_Name');
					me.Grid.ContractName = Name;
					me.Grid.ContractID = id;
//					var InvoiceMoney = record.get('PContract_InvoiceMoney');
					var Amount = record.get('PContract_Amount');
					me.Grid.ContractInvoiceMoney = Amount;
//					me.Grid.InvoiceMoney = InvoiceMoney;
					me.Grid.onSearch();
				}, null, 500);
			},
			nodata: function(p) {
				me.Grid.clearData();
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
		me.Grid = Ext.create('Shell.class.wfm.business.invoice.apply.Grid', {
			region: 'center',
			header: false,
			title: '发票',
			itemId: 'Grid'
		});
		me.ContractGrid = Ext.create('Shell.class.wfm.business.invoice.apply.ContractGrid', {
			header: false,
			title: '合同',
			region: 'west',
			width: 492,
			split: true,
			collapsible: true,
			itemId: 'ContractGrid'
		});
		return [me.ContractGrid, me.Grid];
	}
});