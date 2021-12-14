/**
 * 经销商项目维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dealer.ItemApp', {
	extend: 'Ext.panel.Panel',
	title: '经销商项目维护',

	layout: 'border',
	bodyPadding: 1,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		var DealerGrid = me.getComponent('DealerGrid');
		var DealerItemGrid = me.getComponent('DealerItemGrid');
		
		DealerGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
                    DealerItemGrid.DealerId = id;
					DealerItemGrid.loadByDealerId(id);
				},null,500);
			},
			select: function(rowModel, record) {
				var id = record.get('tid');
				JShell.Action.delay(function() {
					DealerItemGrid.DealerId = id;
//					DealSellerGrid.DealerDataTimeStamp = record.get('DataTimeStamp');
					DealerItemGrid.loadByDealerId(id);
				},null,500);
			}
		});
		
		DealerGrid.store.on({
			load: function(store, records, successful) {
				if(!successful || !records || records.length <= 0) {
					DealerItemGrid.clearData();
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

		items.push(Ext.create('Shell.class.pki.dealer.GridTree', {
			region: 'west',
			split: true,
			collapsible: true,
			width: 360,
			itemId: 'DealerGrid',
			header: false
		}));
		items.push(Ext.create('Shell.class.pki.dealer.ItemGrid', {
			region: 'center',
			itemId: 'DealerItemGrid',
			header: false
		}));

		return items;
	}
});