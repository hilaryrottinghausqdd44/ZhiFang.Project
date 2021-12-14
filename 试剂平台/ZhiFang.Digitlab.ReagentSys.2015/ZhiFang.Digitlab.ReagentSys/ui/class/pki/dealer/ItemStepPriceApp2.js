/**
 * 经销商项目阶梯价格维护(不能维护经销商和经销商项目)
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dealer.ItemStepPriceApp2',{
    extend:'Ext.panel.Panel',
    title:'经销商项目阶梯价格维护',
    
    layout:'border',
    bodyPadding:1,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var DealerGrid = me.getComponent('DealerGrid');
		var DealerItemGrid = me.getComponent('DealerItemGrid');
		var StepPriceGrid = me.getComponent('StepPriceGrid');
		DealerGrid.on({
			select:function(rowModel,record){
				var id = record.get(DealerGrid.PKField);
				var DataTimeStamp = record.get('BDealer_DataTimeStamp');
				JShell.Action.delay(function(){
					DealerItemGrid.DealerId = id;
					DealerItemGrid.DealerDataTimeStamp = DataTimeStamp;
					DealerItemGrid.loadByDealerId(id);
				});
			}
		});
		DealerItemGrid.on({
			select:function(rowModel,record){
				var ItemId = record.get('DUnitItem_BTestItem_Id');
				var DataTimeStamp = record.get('DUnitItem_DataTimeStamp');
				var IsStepPrice = record.get('DUnitItem_IsStepPrice');
				JShell.Action.delay(function(){
					if(IsStepPrice){
						StepPriceGrid.DealerDataTimeStamp = DealerItemGrid.DealerDataTimeStamp;
						StepPriceGrid.ItemDataTimeStamp = DataTimeStamp;
						StepPriceGrid.loadByDealerIdAndItemId(DealerItemGrid.DealerId,ItemId);
					}else{
						StepPriceGrid.clearData();
					}
				});
			}
		});
		DealerGrid.store.on({
			load:function(store,records,successful){
				if(!successful || !records || records.length <= 0){
					DealerItemGrid.clearData();
					StepPriceGrid.clearData();
				}
			}
		});
		DealerItemGrid.store.on({
			load:function(store,records,successful){
				if(!successful || !records || records.length <= 0){
					StepPriceGrid.clearData();
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
			
		items.push(Ext.create('Shell.class.pki.dealer.Grid',{
			region:'west',
			width: 303,
			split:true,collapsible:true,
			itemId:'DealerGrid',
			readOnly:true,
			header:false
		}));
		items.push(Ext.create('Shell.class.pki.dealer.ItemGrid',{
			region:'west',
			width: 293,
			split:true,collapsible:true,
			itemId:'DealerItemGrid',
			readOnly:true,
			header:false
		}));
		items.push(Ext.create('Shell.class.pki.stepprice.Grid',{
			region:'center',
			itemId:'StepPriceGrid',
			header:false
		}));
			
		return items;
	}
});