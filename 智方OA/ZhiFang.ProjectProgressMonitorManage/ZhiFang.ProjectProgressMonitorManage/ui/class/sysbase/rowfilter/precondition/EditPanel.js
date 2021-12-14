/***
 * 预置条件
 * @author longfc
 * @version 2017-06-14
 */
Ext.define('Shell.class.sysbase.rowfilter.precondition.EditPanel', {
	extend: 'Ext.panel.Panel',

	title: '预置条件',
	width: 560,
	layout: {
		type: 'border'
	},
	/**模块ID*/
	moduleId: null,
	//预置条件Id
	preconditionId: null,
	//预置条件选中行记录
	preconditionSelect: null,
	formtype:'',
	/**行过滤条件ID*/
	PK: null,
	afterRender: function() {
		var me = this;
		me.addEvents('saveClick');
		me.callParent(arguments);
		me.Form.on({
			beforesave: function(form, params) {
				params.preconditionsId = me.preconditionId;
				params.addRoleIdStr = "";
				params.editRoleRightIdStr = "";
				//角色选择处理
				params = me.getRoleIdStrAndRoleRightIdStr(params);
				form.saveing(params);
			},
			save: function(p, id) {
				me.PK = id;
				me.fireEvent('saveClick', me, me.PK);
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
		me.Form = Ext.create('Shell.class.sysbase.rowfilter.precondition.Form', {
			itemId: 'Form',
			PK: me.PK,
			formtype: (me.PK&&me.PK!=-1)? "edit" : 'add',
			preconditionId: me.preconditionId,
			moduleOperId:me.preconditionSelect.get("RBACPreconditions_RBACModuleOper_Id"),
			//预置条件选中行记录
			preconditionSelect: me.preconditionSelect,			
			region: 'center'
		});
		me.RoleRightGrid = Ext.create('Shell.class.sysbase.rowfilter.precondition.roleright.Grid', {
			region: 'east',
			title:'角色选择',
			itemId: 'RoleRightGrid',
			width: 320,
			moduleId: me.moduleId,
			rowFilterId: me.PK,
			preconditionId: me.preconditionId,
			defaultLoad: (me.PK&&me.PK!=-1) ? true : false
		});
		var appInfos = [me.RoleRightGrid, me.Form];
		return appInfos;
	},
	getRoleIdStrAndRoleRightIdStr: function(params) {
		var me = this;
		var addRoleIdStr = "",
			roleRightIdStr = "";
		me.RoleRightGrid.store.each(function(record) {
			var IsAdd = "" + record.get('IsAdd');
			//在该模块操作下,角色已经存在,更新行过滤条件关系
			var id = record.get('RBACRoleRight_Id');
			if(!id && IsAdd.toLowerCase() == "true") {
				addRoleIdStr = addRoleIdStr + record.get('RBACRoleRight_RBACRole_Id') + ",";
			} else {
				roleRightIdStr = roleRightIdStr + record.get('RBACRoleRight_Id') + ",";
			}
		});
		if(addRoleIdStr && addRoleIdStr.length > 0) addRoleIdStr = addRoleIdStr.substring(0, addRoleIdStr.length - 1);
		if(roleRightIdStr && roleRightIdStr.length > 0) roleRightIdStr = roleRightIdStr.substring(0, roleRightIdStr.length - 1);
		params["addRoleIdStr"] = addRoleIdStr;
		params["editRoleRightIdStr"] = ""; //roleRightIdStr;
		return params;
	}
});