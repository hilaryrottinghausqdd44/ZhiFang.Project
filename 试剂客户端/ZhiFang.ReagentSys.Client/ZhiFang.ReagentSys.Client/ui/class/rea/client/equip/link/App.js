/**
 * 仪器试剂维护
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.equip.link.App', {
	extend: 'Ext.panel.Panel',

	title: '仪器试剂维护',
	width: 700,
	height: 480,
	autoScroll: false,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding: '1px',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.onListeners();
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
		me.SimpleGrid = Ext.create('Shell.class.rea.client.equip.link.SimpleGrid', {
			region: 'west',
			title: '仪器列表',
			width: 320,
			header: false,
			itemId: 'SimpleGrid',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});

		me.Grid = Ext.create('Shell.class.rea.client.equip.link.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.SimpleGrid, me.Grid];
	},
	onListeners: function() {
		var me = this;
		me.SimpleGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('ReaTestEquipLab_Id');
					me.Grid.EquipID = id;
					me.Grid.onSearch();
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('ReaTestEquipLab_Id');
					me.Grid.EquipID = id;
					me.Grid.onSearch();
				}, null, 500);
			},
			nodata: function(p) {
				me.Grid.clearData();
			}
		});
	}
});
