/**
 * 统计每台仪器每个项目实际检测量
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.statistics.consume.item.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.grid.MergeCells'
	],

	title: '项目检测量',
	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestStatisticalResultsListByJoinHql?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaLisTestStatisticalResults_TestEquipName',
		direction: 'ASC'
	}, {
		property: 'ReaLisTestStatisticalResults_TestItemCode',
		direction: 'ASC'
	}],
	/**检验项目选择框的宽度*/
	reaTestItemWidth: "94%",
	/**用户UI配置Key*/
	userUIKey: 'statistics.consume.item.Grid',
	/**用户UI配置Name*/
	userUIName: "项目检测量列表",
	/**业务主单查询条件*/
	docHql: "",
	/**业务明细查询条件*/
	dtlHql: "",
	/**机构货品查询条件*/
	reaGoodsHql: "",
	/**当前查询日期范围*/
	dateArea: null,
	/**选择仪器的IDStr*/
	equipIDStr: null,
	/**Lis仪器编码*/
	lisEquipCodeStr: "",
	/**后台排序*/
	remoteSort: true,
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getView().on("refresh", function() {
			me.uxMergeCells.mergeCells(me, [1, 2, 3, 4, 5])
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
			dataIndex: 'ReaLisTestStatisticalResults_StartDate',
			text: '开始日期',
			width: 85,
			hidden:true,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaLisTestStatisticalResults_EndDate',
			text: '结束日期',
			width: 85,
			hidden:true,
			isDate: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaLisTestStatisticalResults_TestEquipName',
			text: '仪器名称',
			minWidth: 160,
			defaultRenderer: true
		},{
			dataIndex: 'ReaLisTestStatisticalResults_TestItemCode',
			text: '项目编号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaLisTestStatisticalResults_TestItemSName',
			text: '项目简称',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaLisTestStatisticalResults_TestItemCName',
			text: '项目名称',
			width: 160,
			defaultRenderer: true
		},  {
			dataIndex: 'ReaLisTestStatisticalResults_TestTypeName',
			text: '检测类型',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaLisTestStatisticalResults_TestCount',
			text: '实际测试数',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaLisTestStatisticalResults_Price',
			text: '单价',
			width: 100,
			hidden:true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaLisTestStatisticalResults_SumTotal',
			text: '金额',
			hidden:true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaLisTestStatisticalResults_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'ReaLisTestStatisticalResults_TestItemID',
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
		if(me.docHql)
			where.push("docHql=" + me.docHql);
		if(me.dtlHql)
			where.push("dtlHql=" + JShell.String.encode(me.dtlHql));
		if(me.reaGoodsHql)
			where.push("reaGoodsHql=" + JShell.String.encode(me.reaGoodsHql));

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
					list[i].ReaLisTestStatisticalResults_StartDate = JShell.Date.toString(me.dateArea.start, true);
				if(me.dateArea.end)
					list[i].ReaLisTestStatisticalResults_EndDate = JShell.Date.toString(me.dateArea.end, true);
			}
			data.list = list;
		}
		return data;
	}
});