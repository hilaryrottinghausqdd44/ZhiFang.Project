/**
 * 实际送样客户数报表
 * @author longfc
 * @version 2016-05-25
 */
Ext.define('Shell.class.pki.report2.CustomersToSendSamplesApp', {
	extend: 'Shell.class.pki.report2.ReportApp2',
	title: '实际送样客户数报表',

	/**默认加载*/
	defaultLoad: true,
	/**报表类型*/
	reportType: '6',
	reportTypeTotal: '7', //汇总报表类型
	/**创建数据列*/
	createGridColumns: function() {

		var columns = [{
			dataIndex: 'DStatClass_SellerName',
			text: '销售',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_SellerArea',
			text: '销售区域',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_DealerName',
			text: '经销商',
			width:200,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_NFClientName',
			text: '送检单位',
			width:200,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_ItemName',
			text: '项目',
			width:160,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_AllItemCount',
			text: '样本量',
			defaultRenderer: true
		}];

		return columns;
	},
	/*
	 *汇总报表的列表的列创建
	 * */
	createGridTotalColumns: function() {
		var columns = [{
			dataIndex: 'DStatClass_ItemName',
			text: '项目',
			width:200,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				if (value != '' && value != null && (store.getCount() - 1 == rowIndex)) {
					return '<span style="color:red;font-weight:bold;">' + value + '</span>';
				} else {
					return value;
				}
			}
		}, {
			dataIndex: 'DStatClass_PersonalCount',
			text: '实际送样经销商数量',
			width:180,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				if (value != '' && value != null && (store.getCount() - 1 == rowIndex)) {
					return '<span style="color:red;font-weight:bold;">' + value + '</span>';
				} else {
					return value;
				}
			}
		}, {
			dataIndex: 'DStatClass_HospitalCount',
			text: '实际送样医院数量',
			width:180,
			sortable: false,
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