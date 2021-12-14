/**
 * 大区销售额及经销商佣金（测算）
 *
 * @version 2016-05-25
 */

Ext.define('Shell.class.pki.report2.RegionSalesApp', {
	extend: 'Shell.class.pki.report2.ReportApp2',
	title: '大区销售额及经销商佣金(测算)报表',

	/**报表类型*/
	reportType: '8',
	reportTypeTotal: '9', //汇总报表类型
	/**创建数据列*/
	createGridColumns: function() {
		//销售，录入时间的月份
		//合计：应收价合计，样本数量合计，免单数量合计，个人数量合计

		var columns = [{
			dataIndex: 'DStatClass_SellerName',
			text: '销售',
			width:150,
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
			dataIndex: 'DStatClass_ItemPriceSum',
			text: '应收合计',
			width:150,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_ContractPriceSum',
			text: '合同金额合计',
			width:150,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_CommissionPriceSum',
			width:150,
			text: '经销商佣金(测算)'
		}];

		return columns;
	},
	/*
	 *汇总报表的列表的列创建
	 * */
	createGridTotalColumns: function() {
		var columns = [{
			dataIndex: 'DStatClass_SellerArea',
			text: '销售区域',
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
			dataIndex: 'DStatClass_ItemPriceSum',
			text: '应收合计',
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
			dataIndex: 'DStatClass_ContractPriceSum',
			text: '合同金额合计',
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
			text: '经销商佣金(测试)',
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