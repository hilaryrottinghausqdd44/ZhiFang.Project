/**
 * 经销商销售维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dealer.SellerApp',{
    extend:'Ext.panel.Panel',
    title:'经销商销售维护',
    
    layout:'border',
    bodyPadding:1,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var DealerGrid = me.getComponent('DealerGrid');
		var DealSellerGrid = me.getComponent('DealSellerGrid');
		DealerGrid.on({
			select:function(rowModel,record){
				var id = record.get(DealerGrid.PKField);
				JShell.Action.delay(function(){
					DealSellerGrid.DealerId = id;
					DealSellerGrid.DealerDataTimeStamp = record.get('BDealer_DataTimeStamp');
					DealSellerGrid.loadByDealerId(id);
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
			
		items.push(Ext.create('Shell.class.pki.dealer.Grid',{
			region:'west',
			width:285,
			split:true,collapsible:true,
			itemId:'DealerGrid',
			header:false
		}));
		items.push(Ext.create('Shell.class.pki.dealer.SellerGrid',{
			region:'center',
			itemId:'DealSellerGrid',
			header:false
		}));
			
		return items;
	}
});