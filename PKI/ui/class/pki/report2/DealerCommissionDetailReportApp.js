/**
 * 经销商佣金明细报表
 * @author longfc
 * @version 2016-05-30
 */
Ext.define('Shell.class.pki.report2.DealerCommissionDetailReportApp', {
	extend: 'Shell.class.pki.report2.ReportApp2',
	title: '经销商佣金明细报表',

	/**默认加载*/
	defaultLoad: true,
	/**报表类型*/
	reportType: '14',
	reportTypeTotal: '15', //汇总报表类型
	hasCalcItemCommission:true,
	ReportTotalGridClass: 'Shell.class.pki.report2.details.Grid',
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
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_DealerName',
			text: '经销商',
			width: 200,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_ContractPriceSum',
			text: '收入合计',
			width: 160,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_StepPriceSum',
			text: '佣金-个人',
			width: 160,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_EditPriceSum',
			text: '佣金-送检单位',
			width: 160,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_CommissionPriceSum',
			text: '经销商佣金合计',
			width: 160,
			sortable: false,
			defaultRenderer: true
		}];

		return columns;
	},
	/*
	 *汇总报表的列表的列创建
	 * */
	createGridTotalColumns: function() {
		var columns = [{
			dataIndex: 'DStatDetailClass_NFClientName',
			text: '送检单位',
			width: 160,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				//汇总行显示处理
				var sLabID = record.get("DStatDetailClass_SLabID");
				if (sLabID == '999999') {
					return '<span style="color:red;font-weight:bold;">' + "汇总" + '</span>';
				} else {
					return value;
				}
			}
		}, {
			dataIndex: 'DStatDetailClass_ItemName',
			text: '项目',
			sortable: false,
			width: 160,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				//汇总行显示处理
				var sLabID = record.get("DStatDetailClass_SLabID");
				if (sLabID == '999999') {
					return '<span style="color:red;font-weight:bold;">' + value + '</span>';
				} else {
					return value;
				}
			}
		}, {
			dataIndex: 'DStatDetailClass_SLabID',
			text: '汇总行标志值',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_SerialNo',
			text: '样本预制条码',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_BarCode',
			text: '实验室条码',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_BillingUnitType',
			align: 'center',
			text: '开票方类型',
			width: 75,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				//汇总行显示处理
				var sLabID = record.get("DStatDetailClass_SLabID");
				if (sLabID == '999999') {
					return '<span style="color:red;font-weight:bold;">' + "" + '</span>';
				} else {
					var v = JShell.PKI.Enum.UnitType['E' + value] || '';
					if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
					return v;
				}
			}
		}, {
			dataIndex: 'DStatDetailClass_BillingUnitName',
			text: '开票方名称',
			width: 135,
			sortable: false,
			defaultRenderer: true
		},  {
			dataIndex: 'DStatDetailClass_ItemPrice',
			text: '应收价',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_ItemStepPrice',
			text: '代理价',
			width: 80,
			sortable: false,
			defaultRenderer: true
		},{
			dataIndex: 'DStatDetailClass_ItemEditPrice',
			text: '佣金',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}];

		return columns;
	}
});