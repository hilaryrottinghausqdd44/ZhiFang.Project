/**
 * EChart图表统计-按供应商
 * @author longfc
 * @version 2019-02-18
 */
Ext.define('Shell.class.rea.client.statistics.echart.basic.comp.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Shell.ux.form.field.SimpleComboBox'],

	title: '供货商列表',
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
	/**默认数据条件*/
	defaultWhere: 'reacenorg.OrgType=0 and reacenorg.Visible=1',

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
	//排序字段
	defaultOrderBy: [{
		property: 'ReaCenOrg_DispOrder',
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
			dataIndex: 'ReaCenOrg_Id',
			text: '机构Id',
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_OrgNo',
			text: '机构编号',
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '供应商',
			width: 100,
			dataIndex: 'ReaCenOrg_CName'
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