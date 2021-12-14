/**
 * 出库库房权限
 * @author liangyl
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.storage.LinkApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '出库库房权限',
	width: 700,
	height: 480,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.onLoadData(record);
					me.fireEvent('select', record);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.onLoadData(record);
					me.fireEvent('select', record);
				}, null, 500);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('nodata', 'select');
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.TabPanel = Ext.create('Shell.class.rea.client.shelves.storagelink.TabPanel', {
			title: '库房管理权限',
			header: false,
			itemId: 'TabPanel',
			split: true,
			collapsible: true,
			height: 320,
			region: 'south',
			collapseMode: 'mini'
		});
		me.Grid = Ext.create('Shell.class.rea.client.shelves.storage.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.TabPanel, me.Grid];
	},
	/**库房选中行后处理*/
	onLoadData: function(record) {
		var me = this;
		if(record) {
			var id = record.get(me.Grid.PKField);
			var storageName = record.get('ReaStorage_CName');
			me.TabPanel.storageID = id;
			me.TabPanel.storageName = storageName;
		}
		me.TabPanel.loadData();		
	},
	nodata: function() {
		var me = this;
		me.TabPanel.nodata();
		me.fireEvent('nodata');
	}
});