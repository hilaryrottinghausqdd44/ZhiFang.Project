/***
 * 将模块服务的预置条件项复制新增到选择的模块服务
 * @author longfc
 * @version 2017-08-22
 */
Ext.define('Shell.class.sysbase.preconditions.copy.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '将模块服务的预置条件项复制新增到选择的模块服务',
	header: true,
	border: false,
	width: 680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	layout: {
		type: 'border'
	},
	//选择需要复制的模块ID
	moduleId: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var ModuleTree = me.getComponent("ModuleTree");
		me.ModuleTree.on({
			itemclick: function(rowModel, record, index, eOpts) {
				JShell.Action.delay(function() {
					me.loadGridData(record.get("tid"));
				}, null, 500);
			},
			select: function(rowModel, record, index, eOpts) {
				JShell.Action.delay(function() {
					me.loadGridData(record.get("tid"));
				}, null, 500);
			}
		});
		me.Grid.on({
			accept: function(p, records) {
				me.fireEvent('accept', me, records);
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
			width: 240,
			itemId: 'ModuleTree',
			name: 'ModuleTree',
			title: '模块树',
			region: 'west',
			hideNodeId: me.moduleId,
			split: true
		});
		me.Grid = Ext.create('Shell.class.sysbase.moduleoper.CheckGrid', {
			itemId: 'Grid',
			region: 'center',
			title: '模块服务选择',
			defaultLoad: false,
			checkOne: false,
			split: true
		});
		var appInfos = [me.ModuleTree, me.Grid];
		return appInfos;
	},
	loadGridData: function(moduleId) {
		var me = this;
		var hqlWhere = 'rbacmoduleoper.RBACModule.Id=' + moduleId;
		me.Grid.defaultWhere = hqlWhere;
		me.Grid.onSearch();
	}
});