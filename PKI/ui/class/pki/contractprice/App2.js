/**
 * 送检单位合同维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.contractprice.App2',{
    extend:'Ext.panel.Panel',
    title:'送检单位合同维护',
    layout:'border',
    bodyPadding:1,
    LaboratoryId:'',
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		var LaboratoryGrid = me.getComponent('LaboratoryGrid');
		var AppTab= me.getComponent('AppTab');
		var ContractPriceGrid = AppTab.getComponent('ContractPriceGrid');
		var DUnitDealerRelationGrid = AppTab.getComponent('DUnitDealerRelationGrid');
		var comtab = AppTab.getActiveTab(AppTab.items.items[0]);
		
		LaboratoryGrid.on({
			select:function(rowModel,record){
				var id = record.get(LaboratoryGrid.PKField);
					me.LaboratoryId= id;
				JShell.Action.delay(function(){
					ContractPriceGrid.LaboratoryId = id;
					ContractPriceGrid.LaboratoryDataTimeStamp = record.get('BLaboratory_DataTimeStamp');
					ContractPriceGrid.LaboratoryBillingUnitId = record.get('BLaboratory_BBillingUnit_Id');;
					ContractPriceGrid.LaboratoryBillingUnitName = record.get('BLaboratory_BBillingUnit_Name');
					ContractPriceGrid.LaboratoryBillingUnitDataTimeStamp = record.get('BLaboratory_BBillingUnit_DataTimeStamp');
					if(comtab==ContractPriceGrid){
					    ContractPriceGrid.loadByLaboratoryId(me.LaboratoryId);
					}else{
						DUnitDealerRelationGrid.LaboratoryId=me.LaboratoryId;
						DUnitDealerRelationGrid.onSearch();
					}
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
		AppTab.on({
            tabchange: function(tabPanel, newCard, oldCard, eOpts) {
                var oldItemId = null;
                if (oldCard != null) {
                    oldItemId = oldCard.itemId
                }
                switch (newCard.itemId) {
                case "ContractPriceGrid":
                    comtab = ContractPriceGrid;
                    ContractPriceGrid.loadByLaboratoryId(me.LaboratoryId);
                    break;
                case "DUnitDealerRelationGrid":
                    comtab = DUnitDealerRelationGrid;
                    DUnitDealerRelationGrid.LaboratoryId=me.LaboratoryId;
					DUnitDealerRelationGrid.onSearch();
                    break;
                default:
                    break
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
			header:false
		}));
		items.push(Ext.create('Shell.class.pki.contractprice.AppTab',{
			region:'center',
			itemId:'AppTab',
//			AddType:'AddDunitContractPrice',
			/**是否带功能按钮*/
//			hasButtons:true,
			/**是否带修改价格功能*/
//			canEditPrice:false,
			header:false
		}));
			
		return items;
	}
});