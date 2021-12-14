/***
 * 模块服务管理
 * @author longfc
 * @version 2017-05-17
 */
Ext.define('Shell.class.sysbase.moduleoper.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '模块服务管理',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	layout: {
		type: 'border'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var ModuleTree = me.getComponent("ModuleTree");
		me.ModuleTree.on({
			select: function(rowModel, record, index, eOpts) {
				JShell.Action.delay(function() {
					me.Form.moduleId = record.get("tid");
					me.loadGridData(record.get("tid"));
				}, null, 500);
			},
			itemclick: function(view, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {
					me.Form.moduleId = record.get("tid");
					me.loadGridData(record.get("tid"));
				}, null, 500);
			}
		});
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
			},
			addclick: function(p) {
				me.Form.isAdd();
			},
			editclick: function(p, record) {
				var id = record.get(me.Grid.PKField);
				me.Form.isEdit(id);
			},
			nodata: function(p) {
				//me.Form.disableControl();
				me.Form.clearData();
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ModuleTree = Ext.create('Shell.class.sysbase.moduleoper.ModuleTree', {
			width: 220,
			itemId: 'ModuleTree',
			name: 'ModuleTree',
			title: '模块树',
			region: 'west',
			split: true
		});
		me.Grid = Ext.create('Shell.class.sysbase.moduleoper.Grid', {
			itemId: 'Grid',
			region: 'center',
			split: true
		});
		me.Form = Ext.create('Shell.class.sysbase.moduleoper.Form', {
			region: 'east',
			itemId: 'Form',
			split: true,
			width: 460
		});
		var appInfos = [me.ModuleTree, me.Grid, me.Form];
		return appInfos;
	},
	loadGridData: function(moduleId) {
		var me = this;
		var hqlWhere = 'rbacmoduleoper.RBACModule.Id=' + moduleId;
		me.Grid.moduleId=moduleId;
		me.Grid.defaultWhere = hqlWhere;
		me.Grid.onSearch();
	}
});