/**
 * 消耗比对分析
 * @author longfc	
 * @version 2019-02-25
 */
Ext.define('Shell.class.rea.client.statistics.consume.comparison.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.grid.MergeCells'
	],

	title: '消耗比对分析',
	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchConsumptionComparisonAnalysisVOListByHql?isPlanish=true&statisticType=2&sortType=1',
	/**用户UI配置Key*/
	userUIKey: 'statistics.consume.comparison.Grid',
	/**用户UI配置Name*/
	userUIName: "消耗比对分析列表",
	/**当前查询日期范围*/
	dateArea: null,
	/**选择仪器的IDStr*/
	equipIDStr: "",
	/**选择试剂的IDStr*/
	goodsIdStr: "",
	/**Lis仪器编码*/
	lisEquipCodeStr: "",
	/**同一仪器相同试剂不同项目的结果按项目合并*/
	isMergeOfItem: true,
	/**后台排序*/
	remoteSort: false,
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getView().on("refresh", function() {
			me.uxMergeCells.mergeCells(me, [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20])
		});
	},
	initComponent: function() {
		var me = this;
		me.uxMergeCells = Ext.create("Shell.ux.grid.MergeCells", {
			itemId: 'uxMergeCells'
		});
		//数据列
		me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ConsumptionComparisonAnalysisVO_StartDate',
			text: '开始日期',
			width: 85,
			hidden: true,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_EndDate',
			text: '结束日期',
			width: 85,
			isDate: true,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_EquipCode',
			text: '仪器编号',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_EquipCName',
			text: '仪器',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_ReaGoodsNo',
			text: '试剂编码',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_GoodsCName',
			text: '所使用的试剂',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_GoodsSName',
			text: '试剂简称',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TestEquipOutCount',
			text: '实际使用量',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_Price',
			text: '试剂单价',
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TestEquipOutAmount',
			text: '试剂成本',
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_DetectionQuantitySum',
			text: '实际测试总数',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TheoreticalConsumptionSum',
			text: '理论消耗总量',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TestItemIncomeSum',
			text: '项目总收入',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_CostRatio',
			text: '成本占比',
			width: 90,
			renderer: function(value, meta, record) {
				var v = value;
				if (!v) v = 0;
				v = Number(v);
				if (v != 0) v = v + '%';
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_CostMargin',
			text: '成本利润率',
			width: 90,
			renderer: function(value, meta, record) {
				var v = value;
				if (!v) v = 0;
				v = Number(v);
				if (v != 0) v = v + '%';
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_GrossProfitMargin',
			text: '项目毛利率',
			width: 90,
			renderer: function(value, meta, record) {
				var v = value;
				if (!v) v = 0;
				v = Number(v);
				if (v != 0) v = v + '%';
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_ExtraConsumption',
			text: '额外消耗',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_ExtraConsumptionRatio',
			text: '额外消耗比',
			width: 90,
			renderer: function(value, meta, record) {
				var v = value;
				if (!v) v = 0;
				v = Number(v);
				if (v != 0) v = v + '%';
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_ConsumptionPercentSum',
			text: '<b style="color:blue;">消耗比</b>',
			width: 80,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			renderer: function(value, meta, record) {
				var v = value;
				if (!v) v = 0;
				v = Number(v);
				var str = '';
				if (v > 110) {
					meta.style = 'color:red';
				} else if (v < 80) {
					meta.style = 'color:blue';
				}
				if (v != 0) v = v + '%';
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_LisTestItemSName',
			text: '项目简称',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TestTypeName',
			text: '检测类型',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_DetectionQuantity',
			text: '实际测试数',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TestItemPrice',
			text: '检验项目单价',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TestItemIncome',
			text: '项目收入',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_ConsumptionPercent',
			text: '<b style="color:blue;">消耗占比</b>',
			width: 80,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			renderer: function(value, meta, record) {
				var v = value;
				if (!v) v = 0;
				v = Number(v);
				if (v != 0) v = v + '%';
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_GoodsUnit',
			text: '包装单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_UnitTestCount',
			text: '单位包装检测量',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TheoreticalConsumption',
			text: '理论消耗量',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_UnitMemo',
			text: '规格',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TheoreticalConsumptionAmount',
			text: '理论消耗金额',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TestEquipID',
			text: '仪器Id',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}];

		return columns;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		if (!me.dateArea) {
			JShell.Msg.alert('日期范围不能为空！');
			return;
		}
		me.load(null, true, autoSelect);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);

		var where = [];
		where.push("groupType=1");
		where.push("isMergeOfItem=" + me.isMergeOfItem);
		if (me.equipIDStr)
			where.push("equipIDStr=" + me.equipIDStr);
		if (me.goodsIdStr)
			where.push("goodsIdStr=" + me.goodsIdStr);
		if (me.dateArea) {
			if (me.dateArea.start) {
				where.push("startDate=" + JShell.Date.toString(me.dateArea.start, true));
			}
			if (me.dateArea.end) {
				where.push("endDate=" + JShell.Date.toString(me.dateArea.end, true) + "");
			}
		}
		url = url + "&" + where.join("&");
		return url;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		if (me.dateArea && data && data.list) {
			var list = data.list;
			for (var i = 0; i < list.length; i++) {
				if (me.dateArea.start)
					list[i].ConsumptionComparisonAnalysisVO_StartDate = JShell.Date.toString(me.dateArea.start, true);
				if (me.dateArea.end)
					list[i].ConsumptionComparisonAnalysisVO_EndDate = JShell.Date.toString(me.dateArea.end, true);
			}
			data.list = list;
		}
		return data;
	}
});
