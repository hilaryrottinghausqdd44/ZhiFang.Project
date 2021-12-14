/**
 * 经销商销售价格查询列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.stepprice.SearchGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '经销商销售价格查询列表',

	/**获取数据服务路径*/
	selectUrl: '/StatService.svc/Stat_UDTO_SearchDealerStepPrice?isPlanish=true',

	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.getComponent('filterToolbar').on({
			search: function(p, params) {
				me.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this;

		//数据列
		me.columns = me.createGridColumns();

		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.stepprice.SearchToolbar', {
			itemId: 'filterToolbar',
			dock: 'top',
			isLocked: true,
			height: 55
		})];

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var columns = [{
			dataIndex: 'DContractPrice_SellerName',
			text: '销售',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_SellerArea',
			text: '销售区域',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BDealer_Name',
			text: '经销商',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BDealer_Shortcode',
			text: '经销商代码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BDealer_EMail',
			text: '经销商联系1',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BDealer_EMail2',
			text: '经销商联系2',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BDealer_EMail3',
			text: '经销商联系方式3（E-mail）',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BTestItem_CName',
			text: '项目',
			width: 100,
			defaultRenderer: true
		},  {
			dataIndex: 'DContractPrice_SampleCount',
			text: '数量',
			width: 80,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_StepPrice',
			text: '价格',
			width: 80,
			sortable: false,
			renderer: function(value, meta) {
				var v = value == null ? '' : value;

				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';

				if (!value || value == 0 || value == '0') {
					meta.style = 'background-color:red;';
				}

				return v;
			}
		}, {
			dataIndex: 'DContractPrice_ContractNo',
			text: '合同号',
			width: 80,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_BeginDate',
			text: '合同起始日',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_EndDate',
			text: '合同终止日',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_ConfirmUser',
			text: '合同确认人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_ConfirmTime',
			text: '合同确认时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'DContractPrice_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];

		me.params = me.getComponent('filterToolbar').getParams();

		var arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true);

		//		startDate={STARTDATE}&
		//		endDate={ENDDATE}&
		//		dealerID={DEALERID}&
		//		sellerID={SELLERID}&
		//		sellerArea={SELLERAREA}&

		//做处理
		if (me.params.StartDate) params.push("&startDate=" + me.params.StartDate);
		if (me.params.EndDate) params.push("&endDate=" + me.params.EndDate);

		if (me.params.Dealer_Id) params.push("&dealerID=" + me.params.Dealer_Id);
		if (me.params.Seller_Id) params.push("&sellerID=" + me.params.Seller_Id);
		if (me.params.Seller_AreaIn) params.push("&sellerArea=" + me.params.Seller_AreaIn);
		if (me.params.ItemPriceType) params.push("&itemPriceType=" + me.params.ItemPriceType);
		url += params.join("");

		return url;
	}
});