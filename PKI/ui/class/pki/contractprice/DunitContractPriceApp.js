/**
 * 送检单位合同价格设置
 */
Ext.define('Shell.class.pki.contractprice.DunitContractPriceApp', {
	extend: 'Ext.panel.Panel',
	title: '送检单位合同价格设置',
	layout: 'border',
	bodyPadding: 1,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var LaboratoryGrid = me.getComponent('LaboratoryGrid');
		var ContractGrid = me.getComponent('ContractGrid');
		var StepPriceGrid = me.getComponent('StepPriceGrid');
		var DataTimeStamp='';
		LaboratoryGrid.on({
			select: function(rowModel, record) {
				var id = record.get('BLaboratory_Id');
			    DataTimeStamp = record.get('BLaboratory_DataTimeStamp');
				JShell.Action.delay(function() {
					ContractGrid.BLaboratoryId = id;
					ContractGrid.BLaboratoryDataTimeStamp = DataTimeStamp;
	                if(StepPriceGrid.store.getCount()>0){
	                StepPriceGrid.clearData();
	                }
//                  StepPriceGrid.ContractPriceType='1';
                    StepPriceGrid.titleForm='送检单位阶梯价格表单';
					StepPriceGrid.BLaboratoryId = id;
					StepPriceGrid.BLaboratoryDataTimeStamp = DataTimeStamp;
					ContractGrid.loadByBLaboratoryId(id);
				
				});
			}
		});
		ContractGrid.on({
			select: function(rowModel, record) {
				var contractNo = record.get('DContractPrice_ContractNo');
//				var DataTimeStamp = record.get('DContractPrice_BLaboratory_DataTimeStamp');
				JShell.Action.delay(function() {
					if (contractNo) {
//						StepPriceGrid.ContractPriceType='1';
						StepPriceGrid.titleForm='送检单位阶梯价格表单';
						StepPriceGrid.BLaboratoryDataTimeStamp = DataTimeStamp;
						StepPriceGrid.BeginDate = record.get('DContractPrice_BeginDate');
						StepPriceGrid.EndDate = record.get('DContractPrice_EndDate');
						StepPriceGrid.ContractNo = contractNo;
						StepPriceGrid.loadByBLaboratoryContractNo(contractNo, ContractGrid.DealerId);
					} else {
						StepPriceGrid.clearData();
					}
				});
			}
		});
		LaboratoryGrid.store.on({
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

		items.push(Ext.create('Shell.class.pki.laboratory.Grid',{
			region:'west',
			width: 303,
			split:true,collapsible:true,
			itemId:'LaboratoryGrid',
			header:false
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
		items.push(Ext.create('Shell.class.pki.contractprice.StepPriceGrid', {
			region: 'center',
			//PKField:'DContractPrice_Id',
			itemId: 'StepPriceGrid',
			header: false
		}));

		return items;
	}
});