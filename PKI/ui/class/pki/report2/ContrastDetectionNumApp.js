/**
 * 当月与前3个月检测数量对比
 * @author longfc
 * @version 2016-05-25
 */
Ext.define('Shell.class.pki.report2.ContrastDetectionNumApp', {
	extend: 'Shell.class.pki.report2.ReportApp2',
	title: '当月与前3个月检测数量对比报表',
	/**默认加载*/
	defaultLoad: true,
	/**报表类型*/
	reportType: '10',
	reportTypeTotal: '11', //汇总报表类型
	isChangeColumnsText: true, //是否在每次查询时动态修改列标题显示名称

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var FilterToolbar = me.getComponent('topToolbar').getComponent('FilterToolbar');
		var ReportGrid = me.getComponent('ReportGrid');
		var ReportTotalGrid = me.getComponent('ReportTotalGrid');
		FilterToolbar.on({
			search: function(p, params) {
				if (me.isChangeColumnsText) {
					me.changeColumnsText(ReportGrid);
					me.changeColumnsText(ReportTotalGrid);
				}
			}
		});
	},
	/*在每次查询时动态修改列表的列标题显示名称**/
	changeColumnsText: function(gridPanel) {
		var me = this;
		var now = new Date();
		var StartDate = gridPanel.params.StartDate;
		if (StartDate && StartDate.toString().length > 0) {
			now = Ext.Date.parse(StartDate, 'Y-m-d', true);
		}
		//var date = Ext.Date.add(now, Ext.Data.MONTH,-1);
		var sMonth = now.getMonth() + 1;
		Ext.Array.each(gridPanel.columns, function(column) {
			switch (column.dataIndex) {
				case "DStatClass_StepPriceSum":
					sMonth = now.getMonth() + 1;
					column.setText(sMonth + "月样本量");
					break;
				case "DStatClass_EditPriceSum":
					sMonth = Ext.Date.add(now, Ext.Date.MONTH, -1).getMonth() + 1;
					column.setText(sMonth + "月样本量");
					break;
				case "DStatClass_FreePriceSum":
					sMonth = Ext.Date.add(now, Ext.Date.MONTH, -2).getMonth() + 1;
					column.setText(sMonth + "月样本量");
					break;
				case "DStatClass_AllItemCount":
					sMonth = Ext.Date.add(now, Ext.Date.MONTH, -3).getMonth() + 1;
					column.setText(sMonth + "月样本量");
					break;
				default:
					break;
			}

		});
	},

	/**创建数据列*/
	createGridColumns: function() {
		var columns = [{
			dataIndex: 'DStatClass_SellerName',
			text: '销售',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_SellerArea',
			text: '销售区域',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_DealerName',
			text: '经销商',
			width: 160,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_NFClientName',
			text: '送检单位',
			width: 160,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_ItemName',
			text: '项目',
			width: 140,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_StepPriceSum',
			text: '当月样本量',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_EditPriceSum',
			text: '当月的前第一个月的样本量',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_FreePriceSum',
			text: '当月的前第二个月的样本量',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_AllItemCount',
			text: '当月的前第三个月的样本量',
			width: 120,
			defaultRenderer: true
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
			width: 160,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				if (value != '' && value != null && (store.getCount() - 1 == rowIndex)) {
					return '<span style="color:red;font-weight:bold;">' + value + '</span>';
				} else {
					return value;
				}
			}
		}, {
			dataIndex: 'DStatClass_StepPriceSum',
			text: '当月样本量',
			width: 160,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				if (value != '' && value != null && (store.getCount() - 1 == rowIndex)) {
					return '<span style="color:red;font-weight:bold;">' + value + '</span>';
				} else {
					return value;
				}
			}
		}, {
			dataIndex: 'DStatClass_EditPriceSum',
			width: 120,
			sortable: false,
			text: '当月的前第一个月的样本量',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				if (value != '' && value != null && (store.getCount() - 1 == rowIndex)) {
					return '<span style="color:red;font-weight:bold;">' + value + '</span>';
				} else {
					return value;
				}
			}
		}, {
			dataIndex: 'DStatClass_FreePriceSum',
			width: 120,
			sortable: false,
			text: '当月的前第二个月的样本量',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				if (value != '' && value != null && (store.getCount() - 1 == rowIndex)) {
					return '<span style="color:red;font-weight:bold;">' + value + '</span>';
				} else {
					return value;
				}
			}
		}, {
			dataIndex: 'DStatClass_AllItemCount',
			width: 120,
			sortable: false,
			text: '当月的前第三个月的样本量',
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