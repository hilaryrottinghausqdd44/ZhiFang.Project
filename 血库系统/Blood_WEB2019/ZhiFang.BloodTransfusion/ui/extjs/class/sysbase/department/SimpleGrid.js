/**
 * 科室信息列表
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.department.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',

	title: '科室信息列表',
	width: 225,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentByHQL?isPlanish=true',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	/**查询栏默认查询值*/
	searchValue:"",
	
	defaultOrderBy: [{
		property: 'Department_DispOrder',
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
			width: 170,
			emptyText: '名称/Code1/Code2/Code3',
			value:me.searchValue,
			isLike: true,
			fields: ['department.CName','department.Code1', 'department.Code2', 'department.Code3']
		};

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '编码',
			dataIndex: 'Department_Id',
			width: 65,
			isKey: true,
			hideable: false
		}, {
			text: '名称',
			dataIndex: 'Department_CName',
			width: 140,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '次序',
			dataIndex: 'Department_DispOrder',
			width: 65,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}];

		return columns;
	}
});
