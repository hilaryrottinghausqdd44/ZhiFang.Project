/**
 * 阶梯价格明细列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.StepPriceInfoGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '阶梯价格明细列表 ',
	width: 1400,
	height: 800,

	/**获取数据服务路径*/
	selectUrl: '/StatService.svc/Stat_UDTO_SearchDealerStepPriceDetail?isPlanish=true',

	/**默认加载数据*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,

	/**开始日期*/
	startDate: null,
	/**结束日期*/
	endDate: null,
	/**送检项目ID*/
	itemID: null,
	/**经销商ID*/
	dealerID: null,
	/**阶梯价*/
	stepPrice: null,


	initComponent: function() {
		var me = this;

		me.columns = [{
			dataIndex: 'DDealerStepPriceDetail_DealerName',
			text: '经销商',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_NFClientName',
			text: '送检单位',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_NFDeptName',
			text: '科室',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_CoopLevel',
			text: '合作分级',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DDealerStepPriceDetail_ItemName',
			text: '项目名称',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_BillingUnitType',
			text: '开票方类型',
			width: 70,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DDealerStepPriceDetail_BillingUnitName',
			text: '开票方(付款单位)',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_ItemContPrice',
			text: '合同价格',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_IsStepPrice',
			text: '是否有阶梯价',
			width: 80,
			isBool: true,
			align: 'center',
			type: 'bool',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_BarCode',
			text: '条码号',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_CName',
			text: '病人名',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_IsFree',
			text: '是否免单',
			width: 60,
			isBool: true,
			align: 'center',
			type: 'bool',
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_IsFreeType',
			text: '免单类型',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_ItemFreePrice',
			text: '免单价格',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_ItemEditPrice',
			text: '终端价',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_ItemStepPrice',
			text: '阶梯价',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'DDealerStepPriceDetail_ItemPrice',
			text: '应收价',
			width: 60,
			defaultRenderer: true
		}];
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];

		var arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true);
		//dateType={dateType}&startDate={startDate}&endDate={endDate}&itemID={itemID}&
		//dealerID={dealerID}&stepPrice={stepPrice}&fields={fields}&isPlanish={isPlanish}
		//做处理
		if (me.dateType) params.push("&dateType=" + me.dateType);
		if (me.startDate) params.push("&startDate=" + me.startDate);
		if (me.endDate) params.push("&endDate=" + me.endDate);
		if (me.itemID) params.push("&itemID=" + me.itemID);
		if (me.dealerID) params.push("&dealerID=" + me.dealerID);
		if (me.stepPrice) params.push("&stepPrice=" + me.stepPrice);

		url += params.join("");

		return url;
	}
});