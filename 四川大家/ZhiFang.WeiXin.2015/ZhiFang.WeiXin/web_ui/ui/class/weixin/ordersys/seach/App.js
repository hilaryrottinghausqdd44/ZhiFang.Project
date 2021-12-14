/**
 * 订单查询
 * @author liangyl	
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.seach.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'订单查询',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.ItemGrid.OrderID=record.get(me.Grid.PKField);
					me.ItemGrid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.ItemGrid.OrderID=record.get(me.Grid.PKField);
					me.ItemGrid.onSearch();
				},null,500);
			},
			nodata:function(p){
				me.ItemGrid.clearData();
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Grid = Ext.create('Shell.class.weixin.ordersys.seach.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			title:'订单'
		});
		me.ItemGrid = Ext.create('Shell.class.weixin.ordersys.seach.ItemGrid', {
			region: 'east',
			width:465,
			title:'订单明细',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'ItemGrid'
		});
		return [me.Grid,me.ItemGrid];
	}
});