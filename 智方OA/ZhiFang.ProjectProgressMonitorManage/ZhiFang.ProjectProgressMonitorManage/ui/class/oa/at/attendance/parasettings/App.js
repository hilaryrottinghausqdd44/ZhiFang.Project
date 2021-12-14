/**
 * 员工考勤设置
 * @author longfc	
 * @version 2016-09-13
 */
Ext.define('Shell.class.oa.at.attendance.parasettings.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '员工考勤设置',
	width: 1200,
	height: 400,
	EmpId: '',
	SHOWROOT: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Tree.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					me.Grid.loadByDeptId(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					me.Grid.loadByDeptId(id);
				}, null, 500);
			}
		});
		
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Tree = Ext.create('Shell.class.sysbase.org.Tree', {
			region: 'west',
			header: false,
			collapsible: true,
			split: true,
			itemId: 'Tree',
			width: 220,
			rootVisible: (me.SHOWROOT === true || me.SHOWROOT === "true") ? true : false	
		});
		me.Grid = Ext.create('Shell.class.oa.at.attendance.parasettings.Grid', {
			region: 'center',
			header: false,
			/**是否单选*/
			checkOne: true,
			collapsible: true,
			itemId: 'Grid',
			/**默认加载*/
			defaultLoad: false
		});
		return [me.Tree, me.Grid];
	}
});