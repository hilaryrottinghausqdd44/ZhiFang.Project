/**
 * 血液分类单位换算
 * @author panjie	
 * @version 2020-08-18
 */
Ext.define('Shell.class.sysbase.classunitlink.App', {
	extend: 'Ext.panel.Panel',
	title: '收费类型费用项目关系',
	
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
				me.LinkGrid.clearData();
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
		me.Grid = Ext.create('Shell.class.sysbase.classunitlink.SimpleGrid', {
			region: 'west',
			title: '血液单位换算',
			width: 280,
			header: false,
			itemId: 'Grid',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.LinkGrid = Ext.create('Shell.class.sysbase.classunitlink.LinkGrid', {
			region: 'center',
			header: false,
			itemId: 'LinkGrid'
		});
		return [me.Grid, me.LinkGrid];
	},
	loadData:function(record){
		var me = this;
		var id = record.get(me.Grid.PKField);
		me.LinkGrid.BCNo = id;
		me.LinkGrid.onSearch();
	}
});
