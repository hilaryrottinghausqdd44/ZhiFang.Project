/**
 * 商务收款界面
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceive.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '商务收款界面',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.AddPanel.FinanceReceiveGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var PayOrgID = record.get('PFinanceReceive_PayOrgID');
					var PayOrgName = record.get('PFinanceReceive_PayOrgName');
					
					//付款单位Id
//					me.AddPanel.FinanceReceiveGrid.PayOrgID=PayOrgID;
					me.AddPanel.ReceivePlanGrid.PayOrgID=PayOrgID;
					me.AddPanel.ReceivePlanGrid.PayOrgName=PayOrgName;

				   //合同和发票
					me.AddPanel.EndPanel.ShowTabPanel.IsInvoiceLoad=false;
					me.AddPanel.EndPanel.ShowTabPanel.ContractGrid.PayOrgID=PayOrgID;
					me.AddPanel.EndPanel.ShowTabPanel.InvoiceGrid.PayOrgID=PayOrgID;
					
					//根据付款单位查询
					me.Info.load(PayOrgID);
					me.AddPanel.ReceivePlanGrid.onSearch();
				    me.AddPanel.EndPanel.ShowTabPanel.setActiveTab(0);
					me.AddPanel.EndPanel.ShowTabPanel.ContractGrid.onSearch();
					
					//商务收款记录
					var FinanceReceiveGridId = record.get('PFinanceReceive_Id');
					me.AddPanel.EndPanel.Grid.PReceivePlanId=FinanceReceiveGridId;
					me.AddPanel.EndPanel.Grid.PayOrgID=PayOrgID;
					me.AddPanel.EndPanel.Grid.PayOrgName=PayOrgName;				
					me.AddPanel.EndPanel.Grid.onSearch();
					
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var PayOrgID = record.get('PFinanceReceive_PayOrgID');
					var PayOrgName = record.get('PFinanceReceive_PayOrgName');
					
					//付款单位Id
//					me.AddPanel.FinanceReceiveGrid.PayOrgID=PayOrgID;
					me.AddPanel.ReceivePlanGrid.PayOrgID=PayOrgID;
					me.AddPanel.ReceivePlanGrid.PayOrgName=PayOrgName;

				   //合同和发票
					me.AddPanel.EndPanel.ShowTabPanel.IsInvoiceLoad=false;
					me.AddPanel.EndPanel.ShowTabPanel.ContractGrid.PayOrgID=PayOrgID;
					me.AddPanel.EndPanel.ShowTabPanel.InvoiceGrid.PayOrgID=PayOrgID;
					
					//根据付款单位查询
					me.Info.load(PayOrgID);
					me.AddPanel.ReceivePlanGrid.onSearch();
				    me.AddPanel.EndPanel.ShowTabPanel.setActiveTab(0);
					me.AddPanel.EndPanel.ShowTabPanel.ContractGrid.onSearch();
					
					//商务收款记录
					var FinanceReceiveGridId = record.get('PFinanceReceive_Id');
					me.AddPanel.EndPanel.Grid.PReceivePlanId=FinanceReceiveGridId;
					me.AddPanel.EndPanel.Grid.PayOrgID=PayOrgID;
					me.AddPanel.EndPanel.Grid.PayOrgName=PayOrgName;				
					me.AddPanel.EndPanel.Grid.onSearch();
					
				}, null, 500);
			},
			nodata: function(p) {
				me.Info.clearData();
				me.AddPanel.ReceivePlanGrid.clearData();
				me.AddPanel.EndPanel.Grid.clearData();
				me.AddPanel.EndPanel.ShowTabPanel.ContractGrid.clearData();
				me.AddPanel.EndPanel.ShowTabPanel.InvoiceGrid.clearData();
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
		me.Info = Ext.create('Shell.class.wfm.business.receive.preceive.Info', {
			region: 'west',
			split: true,
			header: false,
			width: 250,
			title: '付款单位',
			collapsible: true,
			itemId: 'Info'
		});
		me.AddPanel = Ext.create('Shell.class.wfm.business.receive.preceive.AddPanel', {
			region: 'center',
			header: false,
			border:false,
			title: '发票',
			itemId: 'AddPanel'
		});
		return [me.Info,me.AddPanel];
	}
});