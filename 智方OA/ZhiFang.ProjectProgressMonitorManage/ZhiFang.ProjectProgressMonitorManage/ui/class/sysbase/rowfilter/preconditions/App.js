/***
 * 数据过滤条件APP
 * @author longfc
 * @version 2017-05-02
 */
Ext.define('Shell.class.sysbase.rowfilter.preconditions.App', {
	extend: 'Ext.panel.Panel',
	title: '行数据过滤条件(预置条件)',
	header: false,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	layout: {
		type: 'border'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ModuleTree.on({
			select: function(rowModel, record, index, eOpts) {
				me.loadModuleOperGrid(record.get("tid"));
			}
		});
		me.ModuleOperGrid.on({
			select: function(rowModel, record, index, eOpts) {
				me.moduleOperRecord(record, index);
			},
			nodata: function(p) {
				me.RowFilterTree.clearOperateInfo();
				me.RowFilterTree.clearTreeData();
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
			name: 'ModuleTree',
			itemId: 'ModuleTree',
			title: '模块树',
			region: 'west',
			split: true
		});
		me.ModuleOperGrid = Ext.create('Shell.class.sysbase.rowfilter.datafilters.ModuleOperGrid', {
			width: 360,
			header: true,
			itemId: 'ModuleOperGrid',
			title: '模块操作信息',
			region: 'west',
			split: true
		});
		me.RowFilterTree = Ext.create('Shell.class.sysbase.rowfilter.preconditions.RowFilterTree', {
			itemId: 'RowFilterTree',
			title: '行数据过滤条件(预置条件)&角色树',
			region: 'center',
			split: true
		});
		var appInfos = [me.ModuleTree, me.ModuleOperGrid, me.RowFilterTree];
		return appInfos;
	},
	loadModuleOperGrid: function(moduleId) {
		var me = this;
		me.ModuleOperGrid.moduleId = moduleId;
		me.RowFilterTree.moduleId = moduleId;
		if(moduleId) {
			me.ModuleOperGrid.selectIndex = 0;
			var hqlWhere = 'rbacmoduleoper.RBACModule.Id=' + moduleId;
			me.ModuleOperGrid.defaultWhere = hqlWhere;
			me.ModuleOperGrid.onSearch();
		}
	},
	//模块操作选择行后联动
	moduleOperRecord: function(record, index) {
		var me = this;
		var id = record.get('RBACModuleOper_Id');
		if(id) {
			setTimeout(function() {
				me.ModuleOperGrid.selectIndex = index;
				var moduleOperId = id;
				var objectName = record.get('RBACModuleOper_RowFilterBase'); //数据对象	
				var objectCName = record.get('RBACModuleOper_RowFilterBaseCName');
				if(me.RowFilterTree.operateInfo && me.RowFilterTree.operateInfo != null & me.RowFilterTree.operateInfo != '' && me.ismodulePaste == true) {
					//粘贴时选择模块操作行
					var moduleCopy = me.RowFilterTree.operateInfo["moduleCopy"];
					var modulePaste = me.RowFilterTree.operateInfo["modulePaste"];
					if(moduleCopy && moduleCopy != "" && moduleCopy != null) {
						var moduleOperIdCopy = moduleCopy["Id"];
						var objectCNameCopy = me.RowFilterTree.operateInfo["objectCName"];
						var cName = record.get('RBACModuleOper_CName');
						//复制已经有信息
						if(objectCNameCopy.length > 0 && objectCNameCopy == objectName) {
							me.RowFilterTree.operateInfo["modulePaste"] = {
								Id: moduleOperId,
								CName: cName
							};
							//粘贴的模块中文名称
							me.RowFilterTree.operateInfo["modulePasteCName"] = record.get('RBACModuleOper_RBACModule_CName');
						} else {
							//复制的模块中文名称
							me.RowFilterTree.operateInfo["moduleCopyCName"] = record.get('RBACModuleOper_RBACModule_CName');
						}
					}
				}

				me.RowFilterTree.moduleOperId = moduleOperId;
				me.RowFilterTree.moduleOperSelect = record;
				me.RowFilterTree.objectName = objectName;
				me.RowFilterTree.objectCName = objectCName;
				me.RowFilterTree.loadData(moduleOperId, record);
			}, 500);
		}
	},
});