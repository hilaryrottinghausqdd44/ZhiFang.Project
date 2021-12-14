/**
 * 经销商合同维护
 * @author longfc
 * @version 2016-05-13
 */
Ext.define('Shell.class.pki.dealercontract.App', {
	extend: 'Ext.panel.Panel',
	title: '经销商合同维护',

	layout: 'border',
	bodyPadding: 1,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		var DealerGrid = me.getComponent('DealerGrid');
		var ContractPriceGrid = me.getComponent('ContractPriceGrid');
		DealerGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
                    ContractPriceGrid.BDealerId = id;
//					ContractPriceGrid.BDealerDataTimeStamp = record.get('BDealer_DataTimeStamp');
					ContractPriceGrid.BDealerCName = record.get('text');
					
					ContractPriceGrid.LaboratoryBillingUnitId = record.get('BDealer_BBillingUnit_Id');
//					ContractPriceGrid.LaboratoryBillingUnitDataTimeStamp = record.get('BDealer_BBillingUnit_DataTimeStamp');
					ContractPriceGrid.LaboratoryBillingUnitName = record.get('BDealer_BBillingUnit_Name');
					ContractPriceGrid.loadByBDealerId(id);
				},null,500);
			},
			select: function(rowModel, record) {
				var id = record.get('tid');
				JShell.Action.delay(function() {
					ContractPriceGrid.BDealerId = id;
//					ContractPriceGrid.BDealerDataTimeStamp = record.get('BDealer_DataTimeStamp');
					ContractPriceGrid.BDealerCName = record.get('text');
					
					ContractPriceGrid.LaboratoryBillingUnitId = record.get('BDealer_BBillingUnit_Id');
//					ContractPriceGrid.LaboratoryBillingUnitDataTimeStamp = record.get('BDealer_BBillingUnit_DataTimeStamp');
					ContractPriceGrid.LaboratoryBillingUnitName = record.get('BDealer_BBillingUnit_Name');
					ContractPriceGrid.loadByBDealerId(id);
				});
			}
		});
		DealerGrid.store.on({
			load: function(store, records, successful) {
				if (!successful || !records || records.length <= 0) {
					ContractPriceGrid.clearData();
				}
			}
		});
	},
	initComponent: function() {
		var me = this;

		me.items = me.createItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
			//Shell.class.pki.dealercontract.DealerGrid
		items.push(Ext.create('Shell.class.pki.dealer.GridTree', {
			region: 'west',
			split: true,
			collapsible: true,
			itemId: 'DealerGrid',
			header: false
		}));
		items.push(Ext.create('Shell.class.pki.dealercontract.Grid', {
			region: 'center',
			itemId: 'ContractPriceGrid',
			/*新增合同打开的UI*/
			AddType: "AddContractPrice",
			/**是否带功能按钮*/
			hasButtons: true,
			/**是否带修改价格功能*/
			canEditPrice: false,
			header: false
		}));

		return items;
	}
});