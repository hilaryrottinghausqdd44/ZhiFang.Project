/**
 * 经销商项目阶梯价格维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.DealerItemStepPriceApp',{
    extend:'Ext.panel.Panel',
    title:'经销商项目阶梯价格维护',
    
    layout:'border',
    padding:1,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var DealerGrid = me.getComponent('DealerGrid');
		var DealerItemGrid = me.getComponent('DealerItemGrid');
		var StepPriceGrid = me.getComponent('StepPriceGrid');
		DealerGrid.on({
			select:function(rowModel,record){
				var id = record.get(DealerGrid.PKField);
				JShell.Action.delay(function(){
					DealerItemGrid.DealerId = id;
					DealerItemGrid.DealerDataTimeStamp = record.get('BDealer_DataTimeStamp');
					DealerItemGrid.loadByDealerId(id);
				});
			}
		});
		DealerItemGrid.on({
			select:function(rowModel,record){
				var ItemId = record.get('DUnitItem_BTestItem_Id');
				JShell.Action.delay(function(){
					StepPriceGrid.DealerDataTimeStamp = DealerItemGrid.DealerDataTimeStamp;
					StepPriceGrid.ItemDataTimeStamp = record.get('DUnitItem_DataTimeStamp');
					StepPriceGrid.loadByDealerIdAndItemId(DealerItemGrid.DealerId,ItemId);
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
			
		items.push(Ext.create('Shell.class.pki.DealerGrid',{
			region:'west',
			width:335,
			split:true,collapsible:true,
			itemId:'DealerGrid'
		}));
		items.push(Ext.create('Shell.class.pki.DealerItemGrid',{
			region:'west',
			width:285,
			split:true,collapsible:true,
			itemId:'DealerItemGrid'
		}));
		items.push(Ext.create('Shell.class.pki.StepPriceGrid',{
			region:'center',
			itemId:'StepPriceGrid'
		}));
			
		return items;
	}
});