/**
 * EChart图表统计-品牌列表
 * @author longfc
 * @version 2019-02-18
 */
Ext.define('Shell.class.rea.client.statistics.echart.basic.brand.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Shell.ux.form.field.SimpleComboBox'],

	title: '品牌列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true',
	/**默认数据条件*/
	defaultWhere: "bdict.IsUse=1 and bdict.BDictType.DictTypeCode='ProdOrg'",
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
	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BDict_DispOrder',
		direction: 'ASC'
	}],
	features: [{
		ftype: 'summary'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var me = this;
		var columns = [{
			text: '品牌名称',
			dataIndex: 'BDict_CName',
			width: 100,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BDict_Id',
			isKey: true,
			hidden: true,
			hideable: false
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
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		if(data.list == null) data.list = [];
		data.list.push({
			"BDict_Id": "其他",
			"BDict_CName": "其他"
		});
		return data;
	}
});