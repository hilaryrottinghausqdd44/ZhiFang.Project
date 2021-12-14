/**
 * 销售收入及佣金报表
 *
 * @version 2016-05-25
 */

Ext.define('Shell.class.pki.report2.SalesRevenueApp', {
	extend: 'Shell.class.pki.report2.ReportApp2',
	title: '销售收入及佣金报表',
	/**报表类型*/
	reportType: '12',
	reportTypeTotal: '13', //汇总报表类型
	hasCalcItemCommission: true,
	/**创建数据列*/
	createGridColumns: function() {
		//销售，录入时间的月份
		//合计：应收价合计，样本数量合计，免单数量合计，个人数量合计

		var columns = [{
			dataIndex: 'DStatClass_SellerName',
			text: '销售',
			width:120,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_SellerArea',
			text: '销售区域',
			width:150,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_DealerName',
			text: '经销商',
			width:200,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_ContractPriceSum',
			text: '收入合计',
			width:150,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_StepPriceSum',
			text: '佣金-个人',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_EditPriceSum',
			text: '佣金-送检单位',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_CommissionPriceSum',
			text: '经销商佣金合计',
			width:150,
			defaultRenderer: true
		}];

		return columns;
	},
	/*
	 *汇总报表的列表的列创建
	 * */
	createGridTotalColumns: function() {
		var columns = [{
			dataIndex: 'DStatClass_SellerName',
			text: '销售',
			width:150,
			sortable:false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_ContractPriceSum',
			text: '收入合计',
			width:150,
			sortable:false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				if (value != '' && value != null && (store.getCount() - 1 == rowIndex)) {
					return '<span style="color:red;font-weight:bold;">' + value + '</span>';
				} else {
					return value;
				}
			}

		}, {
			dataIndex: 'DStatClass_CommissionPriceSum',
			text: '经销商佣金合计',
			width:150,
			sortable:false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				if (value != '' && value != null && (store.getCount() - 1 == rowIndex)) {
					return '<span style="color:red;font-weight:bold;">' + value + '</span>';
				} else {
					return value;
				}
			}
		}];

		return columns;
	}
});