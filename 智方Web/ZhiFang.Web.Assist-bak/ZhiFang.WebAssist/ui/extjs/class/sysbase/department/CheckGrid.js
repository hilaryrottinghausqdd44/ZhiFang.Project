/**
 * 科室选择列表
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.department.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '科室选择列表',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDepartmentByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'Department_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,
	/**默认每页数量*/
	defaultPageSize: 100,
	
	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'department.Visible=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '快捷码/名称',
			isLike: true,
			fields: ['department.Id','department.CName','department.ShortCode']
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
			hidden:true,
			dataIndex: 'Department_Id',
			width: 100,
			isKey: true,
			hideable: false
		}, {
			text: '名称',
			dataIndex: 'Department_CName',
			width: 100,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '快捷码',
			dataIndex: 'Department_ShortCode',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '对照码1',
			dataIndex: 'Department_Code1',
			width: 65,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '对照码2',
			dataIndex: 'Department_Code2',
			width: 65,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '对照码3',
			dataIndex: 'Department_Code3',
			width: 65,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		}]

		return columns;
	}
});
