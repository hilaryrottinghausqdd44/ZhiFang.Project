/**
 * 经销商合同价格维护(不能维护经销商和经销商项目)
 * @author longfc
 * @version 2016-05-11
 */
Ext.define('Shell.class.pki.dealer.contractprice.ContractPriceApp', {
	extend: 'Ext.panel.Panel',
	title: '经销商合同价格维护',

	layout: 'border',
	bodyPadding: 1,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		var DealerGrid = me.getComponent('DealerGrid');
		var ContractGrid = me.getComponent('ContractGrid');
		var StepPriceGrid = me.getComponent('StepPriceGrid');
		DealerGrid.on({
			itemclick:function(v, record) {
//				var DataTimeStamp = record.get('BDealer_DataTimeStamp');
				JShell.Action.delay(function(){
					var id = record.get('tid');
					ContractGrid.DealerId = id;
					ContractGrid.DealerDataTimeStamp = '0,0,0,0,0,0,0,0';
                    if(StepPriceGrid.store.getCount()>0){
                     	StepPriceGrid.clearData();
                    }
					StepPriceGrid.DealerId = id;
					StepPriceGrid.DealerDataTimeStamp = '0,0,0,0,0,0,0,0';
					ContractGrid.loadByDealerId(id);
				},null,500);
			},
			select:function(RowModel, record){
//				var DataTimeStamp = record.get('BDealer_DataTimeStamp');
				JShell.Action.delay(function(){
					var id = record.get('tid');
					ContractGrid.DealerId = id;
					ContractGrid.DealerDataTimeStamp = '0,0,0,0,0,0,0,0';
                    if(StepPriceGrid.store.getCount()>0){
                     	StepPriceGrid.clearData();
                    }
					StepPriceGrid.DealerId = id;
					StepPriceGrid.DealerDataTimeStamp = '0,0,0,0,0,0,0,0';
					ContractGrid.loadByDealerId(id);
				},null,500);
			}
		});
		ContractGrid.on({
			select: function(rowModel, record) {
				var contractNo = record.get('DContractPrice_ContractNo');
				JShell.Action.delay(function() {
					if (contractNo) {
//						StepPriceGrid.DealerDataTimeStamp = ContractGrid.DealerDataTimeStamp;
//						StepPriceGrid.BeginDate = record.get('DContractPrice_BeginDate');
//						StepPriceGrid.EndDate = record.get('DContractPrice_EndDate');
						StepPriceGrid.ContractNo = contractNo;
						StepPriceGrid.loadByContractNo(contractNo, ContractGrid.DealerId);
					} else {
						StepPriceGrid.clearData();
					}
				});
			}
		});
		StepPriceGrid.on({
			select: function(rowModel, record) {
				var contractNo = record.get('DContractPrice_ContractNo');
				JShell.Action.delay(function() {
					if (contractNo) {
						StepPriceGrid.ContractNo = contractNo;
						StepPriceGrid.DealerDataTimeStamp = ContractGrid.DealerDataTimeStamp;
						StepPriceGrid.BeginDate = record.get('DContractPrice_BeginDate');
						StepPriceGrid.EndDate = record.get('DContractPrice_EndDate');
					}
				});
			}
		});
		DealerGrid.store.on({
			load: function(store, records, successful) {
				if (!successful || !records || records.length <= 0) {
					ContractGrid.clearData();
					StepPriceGrid.clearData();
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

		items.push(Ext.create('Shell.class.pki.dealer.GridTree', {
			region: 'west',
			width: 303,
			split: true,
			collapsible: true,
			hasbottomBtntoolbar:false,
			itemId: 'DealerGrid',
			readOnly: true,
			header: false
		}));
		items.push(Ext.create('Shell.class.pki.dealer.contractprice.ContractGrid', {
			region: 'west',
			width: 283,
			split: true,
			collapsible: true,
			itemId: 'ContractGrid',
			readOnly: true,
			header: false
		}));
		items.push(Ext.create('Shell.class.pki.dealer.contractprice.StepPriceGrid', {
			region: 'center',
			PKField:'DContractPrice_Id',
			itemId: 'StepPriceGrid',
			header: false
		}));

		return items;
	}
});