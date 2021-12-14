/**
 * 发票
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceive.InvoiceGrid',{
	extend: 'Shell.ux.grid.Panel',
    title: '发票',
    /**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoiceByExportType?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPInvoice',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePInvoiceByField',
	
    /**带功能按钮栏*/
	hasButtontoolbar: false,
	/**默认加载数据*/
	defaultLoad: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**付款单位ID*/
	PayOrgID:null,
	defaultWhere:'pinvoice.IsUse=1',//and pinvoice.Status=7  
	hasRefresh:false,
		/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PInvoice_ApplyDate',
		direction: 'DESC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},

	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			text: '开票时间',
			dataIndex: 'PInvoice_ApplyDate',
			width: 85,
			isDate: true
		}, {
			text: '金额',
			dataIndex: 'PInvoice_InvoiceAmount',
			width: 80,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		},{
			text: '开票负责人',
			dataIndex: 'PInvoice_InvoiceManName',
			width: 70,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		},{
			text: '发票类型',
			dataIndex: 'PInvoice_InvoiceTypeName',
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		},{
			text: '执行公司',
			dataIndex: 'PInvoice_ComponeName',
			width: 120,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}];

		return columns;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
        me.load(null, true, autoSelect);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			PClient = null,
			search = null,
			params = [];
		//付款单位
		if(me.PayOrgID) {
			params.push("pinvoice.PayOrgID='" + me.PayOrgID + "'");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	}
});