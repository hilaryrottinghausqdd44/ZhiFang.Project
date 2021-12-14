/**
 * 物资试剂对照关系
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.material.App', {
	extend: 'Ext.panel.Panel',
	
	title: '物资试剂对照关系',
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
		me.SimpleGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.SimpleGrid.PKField);
					var BusinessCName = record.get('ReaBusinessInterface_CName');
					me.AddPanel.BusinessID = id;
					me.AddPanel.BusinessCName = BusinessCName;
					me.AddPanel.loadDataById(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.SimpleGrid.PKField);
					var BusinessCName = record.get('ReaBusinessInterface_CName');
					me.AddPanel.BusinessID = id;
					me.AddPanel.BusinessCName = BusinessCName;
					me.AddPanel.loadDataById(id);
				}, null, 500);
			},
			nodata: function(p) {
				me.AddPanel.clearData();
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
		me.SimpleGrid = Ext.create('Shell.class.rea.client.businessinterface.SimpleGrid', {
			region: 'west',
			title: '业务接口列表',
			width: 530,
			header: false,
			itemId: 'SimpleGrid',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});

		me.AddPanel = Ext.create('Shell.class.rea.client.material.AddPanel', {
			region: 'center',
			header: false,
			itemId: 'AddPanel'
		});
		return [me.SimpleGrid, me.AddPanel];
	}
});