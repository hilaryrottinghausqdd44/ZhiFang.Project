/**
 * 科室人员维护
 * @author longfc
 * @version 2020-04-03
 */
Ext.define('Shell.class.sysbase.jurisdiction.userrole.DeptUserGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],

	title: '科室人员信息',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDepartmentUserByHQL?isPlanish=true',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'DepartmentUser_PUser_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '人员编码/人员名称/员Code1',
			isLike: true,
			fields: ['departmentuser.PUser.Id', 'departmentuser.PUser.CName', 'departmentuser.PUser.Code1']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
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
			width: 100,
			hidden: true,
			isKey: true,
			hideable: false
		},{
			text: '身份类型',
			dataIndex: 'DepartmentUser_PUser_Usertype',
			width: 65,
			defaultRenderer: true
		},  {
			text: '人员编码',
			dataIndex: 'DepartmentUser_PUser_Id',
			width: 95,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '人员名称',
			dataIndex: 'DepartmentUser_PUser_CName',
			width: 95,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '人员Code1',
			dataIndex: 'DepartmentUser_PUser_Code1',
			width: 95,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'DepartmentUser_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '次序',
			dataIndex: 'DepartmentUser_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	}
});
