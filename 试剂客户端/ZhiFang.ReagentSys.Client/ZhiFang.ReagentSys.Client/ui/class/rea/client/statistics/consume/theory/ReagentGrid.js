/**
 * 理论消耗量-按试剂项目列表
 * 
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.statistics.consume.theory.ReagentGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.grid.MergeCells'
	],

	title: '按试剂项目列表',
	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchConsumptionComparisonAnalysisVOListByHql?isPlanish=true&statisticType=1&sortType=1',
	/**试剂选择框的宽度*/
	goodsNameWidth: "94%",
	/**用户UI配置Key*/
	userUIKey: 'statistics.consume.theory.ReagentGrid',
	/**用户UI配置Name*/
	userUIName: "理论消耗量-按试剂项目列表",
	/**当前查询日期范围*/
	dateArea: null,
	/**选择仪器的IDStr*/
	equipIDStr: "",
	/**选择试剂的IDStr*/
	goodsIdStr: "",
	/**Lis仪器编码*/
	lisEquipCodeStr: "",
	/**后台排序*/
	remoteSort: false,
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getView().on("refresh", function() {
			me.uxMergeCells.mergeCells(me, [1, 2, 3, 4, 5, 6, 7,8,9])
		});
	},
	initComponent: function() {
		var me = this;
		me.uxMergeCells = Ext.create("Shell.ux.grid.MergeCells", {
			itemId: 'uxMergeCells'
		});
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ConsumptionComparisonAnalysisVO_StartDate',
			text: '开始日期',
			width: 85,
			isDate: true,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_EndDate',
			text: '结束日期',
			width: 85,
			isDate: true,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_EquipCName',
			text: '仪器名称',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_EquipCode',
			text: '仪器编号',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_ReaGoodsNo',
			text: '货品编码',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_GoodsCName',
			text: '所用试剂',
			width: 140,
			//hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_GoodsSName',
			text: '货品简称',
			width: 80,
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ConsumptionComparisonAnalysisVO_DetectionQuantitySum',
			text: '实际测试数总和',
			width: 95,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TheoreticalConsumptionSum',
			text: '理论消耗量总和',
			width: 95,
			hidden: true,
			defaultRenderer: true
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
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_GoodsUnit',
			text: '包装单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_UnitMemo',
			text: '规格',
			width: 80,
			hidden: true,
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
			dataIndex: 'ConsumptionComparisonAnalysisVO_Price',
			text: '单价',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ConsumptionComparisonAnalysisVO_TheoreticalConsumptionAmount',
			text: '理论消耗金额',
			width: 90,
			defaultRenderer: true
		},{
			dataIndex: 'ConsumptionComparisonAnalysisVO_TestItemID',
			text: '项目Id',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}];

		return columns;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		if(!me.dateArea) {
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
		if(me.equipIDStr)
			where.push("equipIDStr=" + me.equipIDStr);
		if(me.goodsIdStr)
			where.push("goodsIdStr=" + me.goodsIdStr);
		if(me.dateArea) {
			if(me.dateArea.start) {
				where.push("startDate=" + JShell.Date.toString(me.dateArea.start, true));
			}
			if(me.dateArea.end) {
				where.push("endDate=" + JShell.Date.toString(me.dateArea.end, true) + "");
			}
		}
		url = url + "&" + where.join("&");
		return url;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		if(me.dateArea && data && data.list) {
			var list = data.list;
			for(var i = 0; i < list.length; i++) {
				if(me.dateArea.start)
					list[i].ConsumptionComparisonAnalysisVO_StartDate = JShell.Date.toString(me.dateArea.start, true);
				if(me.dateArea.end)
					list[i].ConsumptionComparisonAnalysisVO_EndDate = JShell.Date.toString(me.dateArea.end, true);
			}
			data.list = list;
		}
		return data;
	}
});