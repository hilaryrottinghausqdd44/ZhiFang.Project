/**
 * 业务接口关系配置信息
 * @author longfc	
 * @version 2018-11-19
 */
Ext.define('Shell.class.rea.client.businessinterfacelink.App', {
	extend: 'Ext.panel.Panel',
	title: '业务接口关系配置信息',
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
					me.EditPanel.BusinessID = id;
					me.EditPanel.BusinessCName = BusinessCName;
					me.EditPanel.loadData();
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.SimpleGrid.PKField);
					var BusinessCName = record.get('ReaBusinessInterface_CName');
					me.EditPanel.BusinessID = id;
					me.EditPanel.BusinessCName = BusinessCName;
					me.EditPanel.loadData();
				}, null, 500);
			},
			nodata: function(p) {
				me.EditPanel.clearData();
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
			title: '业务接口',
			width: 380,
			header: false,
			itemId: 'SimpleGrid',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});

		me.EditPanel = Ext.create('Shell.class.rea.client.businessinterfacelink.EditPanel', {
			region: 'center',
			header: false,
			itemId: 'EditPanel'
		});
		return [me.SimpleGrid, me.EditPanel];
	}
});