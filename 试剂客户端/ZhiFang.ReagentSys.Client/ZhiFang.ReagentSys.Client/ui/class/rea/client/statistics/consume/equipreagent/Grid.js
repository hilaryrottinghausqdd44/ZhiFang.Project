/**
 * 某仪器的试剂使用量信息
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.statistics.consume.equipreagent.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.grid.MergeCells'
	],
	
	title: '仪器试剂使用量',
	
	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchReaBmsOutDtEntityListByJoinHql?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsOutDtl_TestEquipName',
		direction: 'ASC'
	}, {
		property: 'ReaBmsOutDtl_ReaGoodsNo',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'statistics.consume.equipreagent.Grid',
	/**用户UI配置Name*/
	userUIName: "仪器试剂使用量列表",
	/**业务主单查询条件*/
	docHql: "",
	/**业务明细查询条件*/
	dtlHql: "",
	/**机构货品查询条件*/
	reaGoodsHql: "",
	/**当前查询日期范围*/
	dateArea: null,
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getView().on("refresh", function() {
			me.uxMergeCells.mergeCells(me, [1, 2, 3])
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
			dataIndex: 'ReaBmsOutDtl_StartDate',
			text: '开始日期',
			width: 85,
			isDate: true,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_EndDate',
			text: '结束日期',
			width: 85,
			hidden: true,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TestEquipName',
			text: '所用仪器',
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaGoodsNo',
			text: '所用试剂编码',
			width: 100,
			//hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsCName',
			text: '所用试剂',
			width: 160,
			//hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_EName',
			text: '英文名称',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SName',
			text: '试剂简称',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsUnit',
			text: '单位',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Price',
			text: '单价',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsQty',
			text: '实际使用量',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SumTotal',
			text: '实际使用金额',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsId',
			text: '试剂Id',
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
		if (me.docHql)
			where.push("docHql=" + me.docHql);
		if (me.dtlHql)
			where.push("dtlHql=" + JShell.String.encode(me.dtlHql));
		if (me.reaGoodsHql)
			where.push("reaGoodsHql=" + JShell.String.encode(me.reaGoodsHql));

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
					list[i].ReaBmsOutDtl_StartDate = JShell.Date.toString(me.dateArea.start, true);
				if (me.dateArea.end)
					list[i].ReaBmsOutDtl_EndDate = JShell.Date.toString(me.dateArea.end, true);
			}
			data.list = list;
		}
		return data;
	}
});
