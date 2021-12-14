/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.choose.sale.DocGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '供货总单列表',

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocByHQL?isPlanish=true',
	/**获取数据服务路径*/
	selectDtlUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDoc',
	/**修改服务地址*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDocByField',

	/**默认加载*/
	defaultLoad: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'BmsCenSaleDoc_OperDate',
		direction: 'DESC'
	}],

	/**默认单据状态*/
	defaultStatusValue: 0,
	StatusList: [],
	/**申请单状态枚举*/
	StatusEnum: {},
	/**申请单状态背景颜色枚举*/
	StatusBGColorEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);	
	},
	
	initComponent: function() {
		var me = this;
		
		me.addEvents('checkclick');
		
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '供货单号/供货方',
			itemId:'search',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo', 'bmscensaledoc.ReaCompanyName']
		};
		me.buttonToolbarItems = ['refresh','->', {
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'BmsCenSaleDoc_OperDate',
			text: '操作时间',
			align: 'center',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'BmsCenSaleDoc_SaleDocNo',
			text: '供货单号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_OrderDocNo',
			text: '订货单号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_TotalPrice',
			text: '总价',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_UrgentFlag',
			text: '紧急标志',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_UrgentFlag['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDoc_Status',
			text: '单据状态',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_Status['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDoc_IOFlag',
			text: '提取标志',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_IOFlag['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDoc_ReaCompanyName',
			text: '供货方',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_UserName',
			text: '操作人员',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_Memo',
			text: '备注',
			hidden: true,
			width: 200,
			renderer: function(value, meta) {
				return "";
			}
		}, {
			dataIndex: 'BmsCenSaleDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BmsCenSaleDoc_ReaCompID',
			text: '供货方ID',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		me.internalWhere = me.getInternalWhere();

		return me.callParent(arguments);;
	},
	/**获取内部条件*/
	getInternalWhere:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('search'),
			where = [];
		if(search) {
			var value = search.getValue();
			if(value) {
				var searchWhere = me.getSearchWhere(value);
				if(searchWhere) {
					where.push('(' + searchWhere + ')');
				}

			}
		}
		where.push('bmscensaledoc.Status=2');
		return where.join(" and ");
	}
});