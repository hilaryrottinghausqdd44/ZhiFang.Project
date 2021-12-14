/**
 * 科室自动核收关系
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.deptautochecklink.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '科室自动核收关系',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.LinkGrid.on({
			itemclick: function(v, record) {
				me.loadData(record);
			},
			select: function(RowModel, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				
			}
		});

		me.LinkGrid.onSearch();
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.LinkGrid = Ext.create('Shell.class.sysbase.deptautochecklink.LinkGrid', {
			region: 'center',
			header: false,
			itemId: 'LinkGrid'
		});
		return [ me.LinkGrid];
	},
	loadData: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			
		}, null, 500);
	}
});