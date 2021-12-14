/**
 * 送检单位合同维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.contractprice.App1',{
    extend:'Ext.panel.Panel',
    title:'送检单位合同维护',
    
    layout:'border',
    bodyPadding:1,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var LaboratoryGrid = me.getComponent('LaboratoryGrid');
		var ContractPriceGrid = me.getComponent('ContractPriceGrid');
		LaboratoryGrid.on({
			select:function(rowModel,record){
				var id = record.get(LaboratoryGrid.PKField);
				JShell.Action.delay(function(){
					ContractPriceGrid.LaboratoryId = id;
					ContractPriceGrid.LaboratoryDataTimeStamp = record.get('BLaboratory_DataTimeStamp');
					
					ContractPriceGrid.LaboratoryBillingUnitId = record.get('BLaboratory_BBillingUnit_Id');;
					ContractPriceGrid.LaboratoryBillingUnitName = record.get('BLaboratory_BBillingUnit_Name');
					ContractPriceGrid.LaboratoryBillingUnitDataTimeStamp = record.get('BLaboratory_BBillingUnit_DataTimeStamp');
					
					ContractPriceGrid.loadByLaboratoryId(id);
				});
			}
		});
		LaboratoryGrid.store.on({
			load:function(store,records,successful){
				if(!successful || !records || records.length <= 0){
					ContractPriceGrid.clearData();
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(Ext.create('Shell.class.pki.laboratory.Grid',{
			region:'west',
			split:true,collapsible:true,
			itemId:'LaboratoryGrid',
			readOnly:true,
			header:false
		}));
		items.push(Ext.create('Shell.class.pki.contractprice.Grid',{
			region:'center',
			itemId:'ContractPriceGrid',
			/**是否带功能按钮*/
			hasButtons:false,
			/**是否带修改价格功能*/
			canEditPrice:true,
			header:false
		}));
			
		return items;
	}
});