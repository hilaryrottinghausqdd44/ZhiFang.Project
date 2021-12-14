/**
 * 员工选择列表
 * 根据员工所属的角色(角色名称)获取员工信息
 * @author longfc
 * @version 2016-07-07
 */
Ext.define('Shell.class.sysbase.user.role.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '员工选择列表',
	width: 425,
	height: 485,
	searchInfoWidth:'70%',
	/**员工所属的角色(角色名称)*/
	RoleHREmployeeCName: "",

	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true',
	PKCheckField: 'RBACEmpRoles_HREmployee_Id',
	/**是否单选*/
	checkOne: false,
	/**隐藏当前角色选择项*/
	HiddenRoleCNameType: true,
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		me.RoleHREmployeeCName = me.RoleHREmployeeCName || "";
		me.checkOne = me.checkOne || false;
		if(!me.checkOne) {
			//复选框
			me.multiSelect = true;
			me.selType = 'checkboxmodel';
		}
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'rbacemproles.HREmployee.IsUse=1';
		//查询框信息
		me.searchInfo ={
			width: me.searchInfoWidth,
			emptyText: '名称',
			isLike: true,
			fields: ['rbacemproles.HREmployee.CName']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**
	 * 渲染完后处理
	 * @private
	 */
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		//滤重处理
		me.store.on({
			load: function(s, records, successful, eOpts) {
				if(!successful) {
					Ext.Msg.alert("提示", "获取数据错误！");
				} else {
					if(s.getCount() > 0) {
						me.store = me.changStore(s, records);
					}
				}
			}
		});
	},
	/***
	 * 过滤参数编码重复的数据
	 * @param {} s
	 * @param {} records
	 * @return {}
	 */
	changStore: function(s, records) {
		var a = {},
			b = {};
		var len = records.length;
		for(var i = 0; i < len; i++) {
			if(typeof a[records[i].get('RBACEmpRoles_HREmployee_Id')] == 'undefined') {
				a[records[i].get('RBACEmpRoles_HREmployee_Id')] = 1;
				b[records[i]] = 1;
			} else {
				s.remove(records[i]);
			}
		}
		return s;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '员工姓名',
			dataIndex: 'RBACEmpRoles_HREmployee_CName',
			//width: 120,
			flex: 1,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '员工代码',
			dataIndex: 'RBACEmpRoles_HREmployee_UseCode',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '隶属角色',
			dataIndex: 'RBACEmpRoles_RBACRole_CName',
			width: 110,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'RBACEmpRoles_HREmployee_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '时间戳',
			dataIndex: 'RBACEmpRoles_HREmployee_DataTimeStamp',
			hidden: true,
			hideable: false
		}]

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
	},
	loadByRoleCName: function(roleCName) {
		var me = this;
		me.RoleCName = roleCName;
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			url = '';
		//根据员工所属的角色(角色名称)查询模式
		url += JShell.System.Path.getUrl(me.selectUrl);
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";

		//根据员工所属的角色(角色名称)查询模式
		if(me.RoleHREmployeeCName.toString().length > 0) {
			var roleCName=JShell.String.encode(me.RoleHREmployeeCName);
			url += "&where=rbacemproles.RBACRole.CName in(" + roleCName + ")";
			if(where) {
				url += ' and ' + JShell.String.encode(where);
			}
		} else {
			if(where) {
				url += '&where=' + JShell.String.encode(where);
			}
		}
		return url;
	}
});