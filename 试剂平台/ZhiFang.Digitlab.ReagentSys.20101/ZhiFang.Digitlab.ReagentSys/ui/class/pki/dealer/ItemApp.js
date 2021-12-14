/**
 * 经销商项目维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dealer.ItemApp',{
    extend:'Ext.panel.Panel',
    title:'经销商项目维护',
    
    layout:'border',
    bodyPadding:1,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var DealerGrid = me.getComponent('DealerGrid');
		var DealerItemGrid = me.getComponent('DealerItemGrid');
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
		DealerGrid.store.on({
			load:function(store,records,successful){
				if(!successful || !records || records.length <= 0){
					DealerItemGrid.clearData();
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
			split:true,collapsible:true,
			itemId:'DealerGrid',
			header:false
		}));
		items.push(Ext.create('Shell.class.pki.dealer.ItemGrid',{
			region:'center',
			itemId:'DealerItemGrid',
			header:false
		}));
			
		return items;
	}
});