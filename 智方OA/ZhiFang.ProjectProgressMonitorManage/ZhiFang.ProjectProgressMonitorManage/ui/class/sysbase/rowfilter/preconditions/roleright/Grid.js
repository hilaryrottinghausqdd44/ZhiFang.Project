/**
 * 角色权限的角色列表
 * @author longfc
 * @version 2017-05-04
 */
Ext.define('Shell.class.sysbase.rowfilter.preconditions.roleright.Grid', {
	extend: 'Shell.class.sysbase.rowfilter.roleright.Grid',
	title: '角色列表',
	width: 280,
	height: 380,
	defaultLoad: false,
	/**模块ID*/
	moduleId: null,
	//预置条件Id
	preconditionsId: null,
	//行数据条件Id
	rowFilterId: null,
	
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'rbacroleright.RBACRole.IsUse=1 and rbacroleright.RBACRole.Id is not null';
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];
		if(me.preconditionsId && me.rowFilterId) {
			params.push("rbacroleright.RBACPreconditions.Id=" + me.preconditionsId);
			params.push("rbacroleright.RBACRowFilter.Id=" + me.rowFilterId);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		for(var i = 0; i < data.length; i++) {
			data[i]["IsAdd"] = false;
		}
		return data;
	},
	//打开分配角色应用效果窗口
	openAppShowWin: function() {
		var me = this;
		var maxHeight = document.body.clientHeight * 0.68;
		var maxWidth = document.body.clientWidth * 0.78;
		//查询该模块操作下的角色数据集,且该角色的数据过滤条件为空
		var hqlWhere = 'rbacroleright.RBACPreconditions.Id=' + me.preconditionsId;
		var config = {
			maxWidth: maxWidth,
			width: 320,
			height: maxHeight,
			autoScroll: true,
			closable: true, //有关闭按钮
			resizable: true, //可变大小
			draggable: true,
			moduleId: me.moduleId,
			preconditionsId: me.preconditionsId,
			checkOne: false,
			listeners: {
				accept: function(win, records) {
					if(records) {
						if(me.store.getCount() == 0) me.getView().update();
						Ext.Array.each(records, function(record) {
							var record1 = me.store.findRecord('RBACRoleRight_RBACRole_Id', record.get('RBACRoleVO_Id'));
							if(record1 == null || record1 == -1) {
								var obj = {
									RBACRoleRight_RBACRole_Id: record.data.RBACRoleVO_Id,
									RBACRoleRight_RBACRole_CName: record.data.RBACRoleVO_CName,
									IsAdd: true,
									RBACRoleRight_Id: ''
								};
								me.store.add(obj);
							}
						});
					}
					win.close();
				}
			}
		};
		if(config.height > maxHeight) {
			config.height = maxHeight;
		}
		JShell.Win.open("Shell.class.sysbase.rowfilter.preconditions.roleright.CheckGrid", config).show();
	}
});