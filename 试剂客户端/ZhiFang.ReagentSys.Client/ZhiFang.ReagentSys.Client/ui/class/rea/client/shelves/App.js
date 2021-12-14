/**
 * 货位货架维护
 * @author liangyl
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '库房货架维护',
	width: 700,
	height: 480,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.storageApp.on({
			select: function(record) {
				me.placeApp.onSearch(record);
			},
			nodata: function(p) {
				me.placeApp.clearData();
			}
		});
		me.placeApp.on({
			LinkGridSave: function(com) {
				//me.storageApp.onLoadData()
			}
		});
	},
	initComponent: function() {
		var me = this;
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.storageApp = Ext.create('Shell.class.rea.client.shelves.storage.LinkApp', {
			region: 'west',
			header: false,
			split: true,
			border: false,
			collapsible: true,
			collapseMode: 'mini',
			width: 600,
			itemId: 'storageApp'
		});
		me.placeApp = Ext.create('Shell.class.rea.client.shelves.place.LinkApp', {
			region: 'center',
			header: false,
			border: false,
			itemId: 'placeApp'
		});
		return [me.storageApp, me.placeApp];
	}
});