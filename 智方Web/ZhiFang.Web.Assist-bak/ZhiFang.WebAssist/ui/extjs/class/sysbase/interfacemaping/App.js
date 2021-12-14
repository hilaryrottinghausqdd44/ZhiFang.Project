/**
 * 字典对照维护
 * @author longfc	
 * @version 2020-07-31
 */
Ext.define('Shell.class.sysbase.interfacemaping.App', {
	extend: 'Ext.panel.Panel',
	title: '字典对照维护',
	
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
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.loadData(record);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.loadData(record);
				}, null, 500);
			},
			nodata: function(p) {
				me.TabPanel.clearData();
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
		me.Grid = Ext.create('Shell.class.sysbase.dict.SimpleGrid', {
			region: 'west',
			title: '对照字典',
			width: 280,
			header: false,
			itemId: 'Grid',
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			defaultWhere:"bdict.IsUse=1 and bdict.BDictType.DictTypeCode='BloodInterfaceMaping'"
		});
		me.TabPanel = Ext.create('Shell.class.sysbase.interfacemaping.TabPanel', {
			region: 'center',
			header: false,
			itemId: 'TabPanel'
		});
		return [me.Grid, me.TabPanel];
	},
	loadData:function(record){
		var me = this;
		var id = record.get(me.Grid.PKField);
		me.TabPanel.PK = id;
		me.TabPanel.loadData(record);
	}
});
