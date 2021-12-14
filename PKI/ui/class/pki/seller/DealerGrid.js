/**
 * 销售经销商列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.seller.DealerGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '销售经销商列表',
	width: 300,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDDealSellerByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelDDealSeller',
	/**新增数据服务路径*/
	saveUrl: '/BaseService.svc/ST_UDTO_AddDDealSeller',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DDealSeller_BeginDate',
		direction: 'DESC'
	}],
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,

	/**销售ID*/
	SellerId: null,
	/**销售时间戳*/
	SellerDataTimeStamp: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 165,
			emptyText: '经销商',
			isLike: true,
			fields: ['ddealseller.BDealer.Name']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = ['refresh', 'add', 'edit', 'del', '->', {
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = [{
			dataIndex: 'DDealSeller_BeginDate',
			text: '开始日期',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DDealSeller_EndDate',
			text: '截止日期',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DDealSeller_BDealer_Name',
			text: '经销商',
			width: 100,
			sortable: false
		}, {
			dataIndex: 'DDealSeller_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.openSellerForm(null);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		me.openSellerForm(id);
	},
	openSellerForm: function(id) {
		var me = this;
		var config = {
			formtype: 'add',
			resizable: false,
			showSuccessInfo: false,
			SellerId: me.SellerId,
			SellerDataTimeStamp: me.SellerDataTimeStamp,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
					//					if(win.formtype == 'edit') {
					//						
					//					}
				}
			}
		};
		if(id) {
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.pki.seller.DealerForm', config).show();
	},
	/**根据销售ID获取数据*/
	loadBySellerId: function(id) {
		var me = this;
		me.DealerId = id;
		me.defaultWhere = 'ddealseller.BSeller.Id=' + id;
		me.onSearch();
	}
});