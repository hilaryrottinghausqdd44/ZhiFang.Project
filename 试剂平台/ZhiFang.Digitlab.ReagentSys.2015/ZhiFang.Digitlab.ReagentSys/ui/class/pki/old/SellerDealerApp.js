/**
 * 销售经销商维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.SellerDealerApp',{
    extend:'Ext.panel.Panel',
    title:'销售经销商维护',
    
    layout:'border',
    padding:1,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var SellerGrid = me.getComponent('SellerGrid');
		var SellDealerGrid = me.getComponent('SellDealerGrid');
		SellerGrid.on({
			select:function(rowModel,record){
				var id = record.get(SellerGrid.PKField);
				JShell.Action.delay(function(){
					SellDealerGrid.SellerId = id;
					SellDealerGrid.SellerDataTimeStamp = record.get('BSeller_DataTimeStamp');
					SellDealerGrid.loadBySellerId(id);
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
			
		items.push(Ext.create('Shell.class.pki.SellerGrid',{
			region:'west',
			width:285,
			split:true,collapsible:true,
			itemId:'SellerGrid'
		}));
		items.push(Ext.create('Shell.class.pki.SellDealerGrid',{
			region:'center',
			itemId:'SellDealerGrid'
		}));
			
		return items;
	}
});