/**
 * EChart图表统计-货品分类列表
 * @author longfc
 * @version 2019-02-18
 */
Ext.define('Shell.class.rea.client.statistics.echart.basic.goodsclass.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Shell.ux.form.field.SimpleComboBox'],
	width: 320,
	height: 500,

	title: '货品分类',
	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchGoodsClassEntityListByClassTypeAndHQL?isPlanish=true',
	/**货品分类*/
	ClassType: "goodsclass",
	defaultLoad: false,
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
	features: [{
		ftype: 'summary'
	}],

	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
				dataIndex: 'ReaGoodsClassVO_CName',
				text: '货品分类',
				width: 100,
				hideable: false
			},
			{
				dataIndex: 'ReaGoodsClassVO_Id',
				text: '主键ID',
				hidden: true,
				hideable: false,
				isKey: true
			}, {
				text: '总金额（元）',
				dataIndex: 'SumTotal',
				align: 'right',
				type: 'float',
				summaryType: 'sum',
				renderer: function(value, meta) {
					var v = Ext.util.Format.currency(value, '￥', 2) + '元';
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
			}
		];

		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();
		if(!me.ClassType) return false;

		me.selectUrl = "/ReaManageService.svc/RS_UDTO_SearchGoodsClassEntityListByClassTypeAndHQL?isPlanish=true&classType=" + me.ClassType;
		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = ["reagoods.Visible=1"];
		me.internalWhere = params.join(' and ');
		return me.callParent(arguments);
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		if(data.list == null) data.list = [];
		data.list.push({
			"ReaGoodsClassVO_Id": "其他",
			"ReaGoodsClassVO_CName": "其他"
		});
		return data;
	}
});