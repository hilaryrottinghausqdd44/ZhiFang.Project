/**
 * 图表统计-部门列表
 * @author longfc
 * @version 2019-02-26
 */
Ext.define('Shell.class.rea.client.statistics.echart.basic.dept.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Shell.ux.form.field.SimpleComboBox'],

	title: '部门列表',
	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?isPlanish=true',
	/**默认数据条件*/
	defaultWhere: 'hrdept.IsUse=1',

	defaultLoad: true,
	//序号列宽度
	rowNumbererWidth: 35,
	//后台排序
	remoteSort: false,
	/**默认每页数量*/
	defaultPageSize: 200,
	//带分页栏
	hasPagingtoolbar: true,
	//带功能按钮栏
	hasButtontoolbar: false,
	//不加载时默认禁用功能按钮
	defaultDisableControl: false,
	defaultOrderBy: [{
		property: 'HRDept_DispOrder',
		direction: 'ASC'
	}],
	features: [{
		ftype: 'summary'
	}],
	
	initComponent: function() {
		var me = this;
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'HRDept_Id',
			text: '机构Id',
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'HRDept_UseCode',
			text: '机构编号',
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '部门名称',
			dataIndex: 'HRDept_CName'
		}, {
			text: '总金额（元）',
			dataIndex: 'SumTotal',
			align: 'right',
			type: 'float',
			summaryType: 'sum',
			renderer: function(value,meta) {
				var v=Ext.util.Format.currency(value, '￥', 2) + '元';
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return value;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">' + Ext.util.Format.currency(value, '￥', 2) + '</span>';
			}
		}, {
			text: '金额占总比(%)',
			dataIndex: 'SumTotalPercent',
			align: 'right',
			width: 90,
			renderer: function(value, meta) {
				var v = "";
				if(value) v = value + '%';
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}];
		return columns;
	}
});