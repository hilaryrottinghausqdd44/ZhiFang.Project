/**
 * 经销商与送检单位维护
 * @author longfc
 * @version 2016-05-13
 */
Ext.define('Shell.class.pki.dunitdealer.App', {
	extend: 'Ext.panel.Panel',
	title: '经销商与送检单位维护',

	layout: 'border',
	bodyPadding: 1,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		var DealerGrid = me.getComponent('DealerGrid');
		var DUnitDealerGrid = me.getComponent('DUnitDealerGrid');
		DealerGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
					DUnitDealerGrid.BDealerId = id;
					DUnitDealerGrid.BDealerDataTimeStamp = '0,0,0,0,0,0,0,0';
					DUnitDealerGrid.BDealerName = record.get('text');

					DUnitDealerGrid.BDealerBBillingUnitId = record.get('BDealer_BBillingUnit_Id');
					DUnitDealerGrid.BDealerBBillingUnitName = record.get('BDealer_BBillingUnit_Name');
//					DUnitDealerGrid.BDealerBBillingUnitDataTimeStamp = record.get('BDealer_BBillingUnit_DataTimeStamp');
                    DUnitDealerGrid.BDealerBBillingUnitDataTimeStamp ='0,0,0,0,0,0,0,0';
					DUnitDealerGrid.loadByBDealerId(id);
				},null,500);
			},
			select: function(rowModel, record) {
				JShell.Action.delay(function() {
				    var id = record.get('tid');
					DUnitDealerGrid.BDealerId = id;
//					DUnitDealerGrid.BDealerDataTimeStamp = record.get('BDealer_DataTimeStamp');
					DUnitDealerGrid.BDealerName = record.get('text');

					DUnitDealerGrid.BDealerBBillingUnitId = record.get('BDealer_BBillingUnit_Id');
					DUnitDealerGrid.BDealerBBillingUnitName = record.get('BDealer_BBillingUnit_Name');
//					DUnitDealerGrid.BDealerBBillingUnitDataTimeStamp = record.get('BDealer_BBillingUnit_DataTimeStamp');
					DUnitDealerGrid.loadByBDealerId(id);
				});
			}
		});

		DealerGrid.store.on({
			load: function(store, records, successful) {
				if (!successful || !records || records.length <= 0) {
					DUnitDealerGrid.clearData();
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
		items.push(Ext.create('Shell.class.pki.dunitdealer.DealerGrid', {
			region: 'west',
			split: true,
			collapsible: true,
			itemId: 'DealerGrid',
			header: false
		}));
		items.push(Ext.create('Shell.class.pki.dunitdealer.DUnitDealerGrid', {
			region: 'center',
			itemId: 'DUnitDealerGrid',
			/**是否带功能按钮*/
			hasButtons: true,
			PKField:'DUnitDealerRelation_Id',
			header: false
		}));

		return items;
	}
});