/**
 * 销售经销商维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.seller.DealerApp', {
	extend: 'Ext.panel.Panel',
	title: '销售经销商维护',

	layout: 'border',
	bodyPadding: 1,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		var SellerGrid = me.getComponent('SellerGrid');
		var SellDealerGrid = me.getComponent('SellDealerGrid');
		SellerGrid.on({
			select: function(rowModel, record) {
				var id = record.get(SellerGrid.PKField);
				JShell.Action.delay(function() {
					SellDealerGrid.SellerId = id;
					SellDealerGrid.SellerDataTimeStamp = record.get('BSeller_DataTimeStamp');
					SellDealerGrid.loadBySellerId(id);
				});
			}
		});
		SellDealerGrid.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				var id = record.get(SellDealerGrid.PKField);
				SellDealerGrid.openSellerForm(id);
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

		items.push(Ext.create('Shell.class.pki.seller.Grid', {
			region: 'west',
			split: true,
			collapsible: true,
			itemId: 'SellerGrid',
			header: false
		}));
		items.push(Ext.create('Shell.class.pki.seller.DealerGrid', {
			region: 'center',
			itemId: 'SellDealerGrid',
			header: false
		}));

		return items;
	}
});