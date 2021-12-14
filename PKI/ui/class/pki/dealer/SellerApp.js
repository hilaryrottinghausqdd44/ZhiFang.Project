/**
 * 经销商销售维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dealer.SellerApp', {
	extend: 'Ext.panel.Panel',
	title: '经销商销售维护',

	layout: 'border',
	bodyPadding: 1,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		var DealerGrid = me.getComponent('DealerGrid');
		var DealSellerGrid = me.getComponent('DealSellerGrid');
		DealerGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
                    DealSellerGrid.DealerId = id;
					DealSellerGrid.loadByDealerId(id);
				},null,500);
			},
			select: function(rowModel, record) {
				var id = record.get('tid');
				JShell.Action.delay(function() {
					DealSellerGrid.DealerId = id;
//					DealSellerGrid.DealerDataTimeStamp = record.get('DataTimeStamp');
					DealSellerGrid.loadByDealerId(id);
				},null,500);
			}
		});
		DealSellerGrid.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				var id = record.get(DealSellerGrid.PKField);
				DealSellerGrid.openSellerForm(id);
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
			width: 285,
			split: true,
			collapsible: true,
			itemId: 'DealerGrid',
			header: false
		}));
		items.push(Ext.create('Shell.class.pki.dealer.SellerGrid', {
			region: 'center',
			itemId: 'DealSellerGrid',
			header: false
		}));

		return items;
	}
});