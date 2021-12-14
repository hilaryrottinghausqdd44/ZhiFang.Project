/**
 * 科室人员选择
 * @author longfc
 * @version 2020-03-27
 */
Ext.define('Shell.class.sysbase.deptuser.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '科室人员选择',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentUserByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'DepartmentUser_PUser_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,
	searchInfoVal: "",

	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '人员编码/人员名称/员Code1',
			isLike: true,
			fields: ['departmentuser.PUser.Id', 'departmentuser.PUser.CName', 'departmentuser.PUser.Code1']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '科室人员编码',
			dataIndex: 'DepartmentUser_Id',
			width: 85,
			hidden: true,
			isKey: true,
			hideable: false
		}, {
			text: '科室编码',
			hidden:true,
			dataIndex: 'DepartmentUser_Department_Id',
			width: 85,
			hideable: false
		}, {
			text: '科室名称',
			dataIndex: 'DepartmentUser_Department_CName',
			width: 85,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '人员编码',
			dataIndex: 'DepartmentUser_PUser_Id',
			width: 85,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '人员名称',
			dataIndex: 'DepartmentUser_PUser_CName',
			width: 85,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '人员Code1',
			dataIndex: 'DepartmentUser_PUser_Code1',
			width: 85,
			menuDisabled: true,
			defaultRenderer: true
		},  {
			text: '次序',
			dataIndex: 'DepartmentUser_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}]

		return columns;
	},
	setSearchValue: function(value, isSearch) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		if (search) {
			search.setValue(value);
			if (isSearch) me.onSearch();
		}
	}
});
