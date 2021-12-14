/***
 * 预置条件
 * @author longfc
 * @version 2017-06-14
 */
Ext.define('Shell.class.sysbase.preconditions.App', {
	extend: 'Ext.panel.Panel',

	title: '预置条件',
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
				setTimeout(function() {
					me.loadModuleoperGrid(record.get("tid"));
				}, 500);
			}
		});
		me.ModuleoperGrid.on({
			itemclick: function(v, record) {},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.ModuleoperGrid.PKField);
					me.Form.moduleOpeId = id;
					me.Form.changeClassConfig(id);
					me.loadPreconditionsGrid(id);
				}, null, 500);
			},
			nodata: function(p) {
				me.PreconditionsGrid.clearData();
			}
		});

		me.PreconditionsGrid.on({
			itemclick: function(v, record) {},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.PreconditionsGrid.PKField);
					me.Form.isEdit(id);
				}, null, 500);
			},
			addclick: function(p) {
				me.Form.isAdd();
				var DispOrder = me.PreconditionsGrid.getStore().count() + 1;
				me.Form.getForm().setValues({
					RBACPreconditions_IsUse: true,
					RBACPreconditions_DispOrder: DispOrder
				});
			},
			copyClick: function(p, record) {
				var id = record.get(me.PreconditionsGrid.PKField);
				me.Form.isAdd();
				var DispOrder = me.PreconditionsGrid.getStore().count() + 1;
				var entity = {
					RBACPreconditions_CName: record.get("RBACPreconditions_CName"),
					RBACPreconditions_ValueType: record.get("RBACPreconditions_ValueType"),
					RBACPreconditions_BaseIBLL: record.get("RBACPreconditions_BaseIBLL"),
					RBACPreconditions_ExecHQL: record.get("RBACPreconditions_ExecHQL"),
					RBACPreconditions_RBACModuleOper_Id: record.get("RBACPreconditions_RBACModuleOper_Id"),
					RBACPreconditions_RBACModuleOper_CName: record.get("RBACPreconditions_RBACModuleOper_CName"),
					RBACPreconditions_IsUse: true,
					RBACPreconditions_DispOrder: DispOrder
				};
				me.Form.getForm().setValues(entity);
			},
			editclick: function(p, record) {
				var id = record.get(me.PreconditionsGrid.PKField);
				me.Form.isEdit(id);
			},
			nodata: function(p) {
				me.Form.clearData();
				//me.Form.showButtonsToolbar(false);
				//me.Form.getForm().reset();
			}
		});
		me.Form.on({
			beforesave: function(form) {
				if(!form.moduleId) {
					JcallShell.Msg.alert("所属模块Id为空,不能保存!", null, 1000);
					me.Form.hideMask(); //隐藏遮罩层
					return;
				}
			},
			save: function(p, id) {
				me.PreconditionsGrid.onSearch();
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
			header: true,
			itemId: 'ModuleTree',
			name: 'ModuleTree',
			title: '模块树',
			region: 'west',
			split: true
		});
		me.ModuleoperGrid = Ext.create('Shell.class.sysbase.preconditions.ModuleoperGrid', {
			itemId: 'ModuleoperGrid',
			region: 'west',
			hasAdd: false,
			hasEdit: false,
			hasCheckIsUse: false,
			width: 360,
			split: true
		});
		me.PreconditionsGrid = Ext.create('Shell.class.sysbase.preconditions.Grid', {
			itemId: 'PreconditionsGrid',
			region: 'center',
			split: true
		});
		me.Form = Ext.create('Shell.class.sysbase.preconditions.Form', {
			region: 'east',
			itemId: 'Form',
			split: true,
			width: 360
		});
		var appInfos = [me.ModuleTree, me.ModuleoperGrid, me.PreconditionsGrid, me.Form];
		return appInfos;
	},
	loadModuleoperGrid: function(moduleId) {
		var me = this;
		var hqlWhere = 'rbacmoduleoper.IsUse=1 and rbacmoduleoper.RBACModule.Id=' + moduleId;
		me.ModuleoperGrid.moduleId = moduleId;
		me.ModuleoperGrid.defaultWhere = hqlWhere;
		me.ModuleoperGrid.onSearch();
	},
	loadPreconditionsGrid: function(moduleOpeId) {
		var me = this;
		var hqlWhere = 'rbacpreconditions.RBACModuleOper.Id=' + moduleOpeId;
		me.PreconditionsGrid.defaultWhere = hqlWhere;
		me.PreconditionsGrid.onSearch();
	}
});