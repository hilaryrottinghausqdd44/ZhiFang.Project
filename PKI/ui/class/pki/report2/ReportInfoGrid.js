/**
 * 报表明细基础列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.report2.ReportInfoGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '报表明细基础列表',

	width: 1600,
	height: 400,

	/**获取数据服务路径*/
	selectUrl: '/StatService.svc/Stat_UDTO_ReportReconciliationDetail',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,

	/**报表类型*/
	reportType: '1',

	initComponent: function() {
		var me = this;

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	rendererIsFinanceLockedAndIsLocked: function(value, meta, record, rowIndex, colIndex, store, view) {
		/**
		 * 对账状态有3种：待对账、销售锁定（即：待财务锁定）、财务锁定
		 * IsFinanceLocked=1 财务锁定;  IsLocked=1 销售锁定; IsLocked=0 待对账
		 * 原来逻辑的不变,在列表里新增一个显示列,原来的两列隐藏
		 * 依IsFinanceLocked值和IsLocked值判断处理
		 * IsFinanceLocked财务锁定等于1,就显示为财务锁定,
		 * 如果IsFinanceLocked不为1,判断IsLocked并显示IsLocked值
		 */
		var IsFinanceLocked = record.get("DStatDetailClass_IsFinanceLocked");
		var IsLocked = record.get("DStatDetailClass_IsLocked");
		var v = "",
			color = "#FFFFFF";
		if(IsFinanceLocked == "1") {
			v = "财务锁定";
			color = JcallShell.PKI.Enum.Color['E7'];
		} else {
			switch(IsLocked) {
				case "1":
					v = "销售锁定";
					color = JcallShell.PKI.Enum.Color['E10'];
					break;
				case "0":
					v = "待对账";
					color = JcallShell.PKI.Enum.Color['E0'];
					break;
				default:
					v = "待对账";
					color = JcallShell.PKI.Enum.Color['E0'];
					break;
			}
		}
		value = v;
		if(value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
		meta.style = 'background-color:' + color || '#FFFFFF';
		record.set("DStatDetailClass_ReconciliationState", value);
		//record.commit();
		return value;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me=this;
		var columns = [ {
			dataIndex: 'DStatDetailClass_IsLocked',
			align: 'center',
			text: '对帐状态(IsLocked)',
			sortable: false,
			hideable: false,
			menuDisabled: true,
			hidden: true,
			width: 75,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.IsLocked['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.IsLockedColor['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DStatDetailClass_IsFinanceLocked',
			align: 'center',
			sortable: false,
			hideable: false,
			menuDisabled: false,
			text: '财务锁定标志',
			hidden: true,
			width: 80,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.IsFinanceLocked['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.IsFinanceLockedColor['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DStatDetailClass_SellerName',
			text: '销售',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_DealerName',
			text: '经销商',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_NFClientName',
			text: '送检单位',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_NFDeptName',
			text: '科室',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_CoopLevel',
			align: 'center',
			text: '合作分级',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.CoopLevel['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DStatDetailClass_ItemName',
			text: '项目名称',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_BillingUnitType',
			align: 'center',
			text: '开票方类型',
			width: 75,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DStatDetailClass_BillingUnitName',
			text: '开票方(付款单位)',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_ItemContPrice',
			text: '合同价',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_IsStepPrice',
			text: '取阶梯价格',
			width: 90,
			align: 'center',
			isBool: true,
			type: 'bool'
		}, {
			dataIndex: 'DStatDetailClass_SerialNo',
			text: '样本预制条码',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_BarCode',
			text: '实验室条码',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_CName',
			text: '病人名',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_CollectDate',
			text: '采样时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'DStatDetailClass_OperDate',
			text: '录入时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'DStatDetailClass_ReceiveDate',
			text: '核收时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'DStatDetailClass_CheckDate',
			text: '报告时间',
			width: 130,
			isDate: true,
			hasTime: true
		},{
			dataIndex: 'DStatDetailClass_ReconciliationState',
			align: 'center',
			text: '对帐状态',
			sortable: true,
			width: 80,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.rendererIsFinanceLockedAndIsLocked(value, meta, record, rowIndex, colIndex, store, view);
			}
		}, 
//		{
//			dataIndex: 'DStatDetailClass_SampleStatus',
//			align: 'center',
//			text: '样本状态',
//			hidden:true,
//			width: 60,
//			renderer: function(value, meta) {
//				var v = JShell.PKI.Enum.IsLocked['E' + value] || '';
//				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
//				meta.style = 'background-color:' + JcallShell.PKI.Enum.IsLockedColor['E' + value] || '#FFFFFF';
//				return v;
//			}
//		}, 
		{
			dataIndex: 'DStatDetailClass_IsFree',
			text: '是否免单',
			width: 60,
			align: 'center',
			isBool: true,
			type: 'bool'
		}, {
			dataIndex: 'DStatDetailClass_IsFreeType',
			text: '免单类型',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_ItemFreePrice',
			text: '免单价格',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_ItemEditPrice',
			text: '终端价',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_ItemStepPrice',
			text: '阶梯价',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_ItemPrice',
			text: '应收价',
			defaultRenderer: true
		}];

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.postParams.fields = me.getStoreFields(true).join(',');
		//(string entityJson, int reportType, string fields, bool isPlanish, int page, int limit)
		var url = (me.selectUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl;
		
		url += '?isPlanish=' + me.postParams.isPlanish + 
			'&reportType=' + me.postParams.reportType + 
			'&fields=' + me.postParams.fields + 
			'&entityJson=' + Ext.JSON.encode(me.postParams.entity);
		
		return url;
	}
});