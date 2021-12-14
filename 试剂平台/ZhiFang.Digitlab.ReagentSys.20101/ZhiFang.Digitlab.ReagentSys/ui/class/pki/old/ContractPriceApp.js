/**
 * 合同价格设置
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.ContractPriceApp',{
    extend:'Ext.panel.Panel',
    title:'合同价格设置',
    
    layout:'border',
    padding:1,
	
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
			
		items.push(Ext.create('Shell.class.pki.LaboratoryGrid',{
			region:'west',
			width:360,
			split:true,collapsible:true,
			itemId:'LaboratoryGrid'
		}));
		items.push(Ext.create('Shell.class.pki.ContractPriceGrid',{
			region:'center',
			itemId:'ContractPriceGrid'
		}));
			
		return items;
	}
});