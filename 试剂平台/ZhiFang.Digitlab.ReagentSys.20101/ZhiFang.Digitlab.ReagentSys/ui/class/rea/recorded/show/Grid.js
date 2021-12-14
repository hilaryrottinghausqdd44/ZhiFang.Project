/**
 * 入账明细
 * @author liangyl
 * @version 2017-06-02
 */
Ext.define('Shell.class.rea.recorded.show.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '入账明细',
	width: 850,
	height: 400,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsAccountSaleDocByHQL?isPlanish=true',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**默认每页数量*/
	defaultPageSize: 50,
	/**分页栏下拉框数据*/
	//pageSizeList:[[50,50]],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BmsAccountSaleDoc_DataAddTime',
		direction: 'ASC'
	}],
	/**入账单Id*/
	BmsAccountInputID: null,
	/**默认选中*/
	autoSelect: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onRemoveClick');
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '供货单号',
			itemId: 'Search',
			isLike: true,
			fields: ['bmsaccountsaledoc.BmsCenSaleDoc.SaleDocNo']
		};
		me.buttonToolbarItems = me.buttonToolbarItems || ['refresh', '->', {
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
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_SaleDocNo',
			text: '供货单号',
			width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_TotalPrice',
			text: '总计金额',
			align: 'center',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_Lab_CName',
			text: '订货方',
			width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_Comp_CName',
			text: '供货方',
			minWidth: 180,
			flex:1,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_Id',
			text: '供货ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'BmsAccountSaleDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BmsAccountSaleDoc_BmsCenSaleDoc_OperDate',
			text: '操作时间',
			align: 'center',
			width: 130,
			isDate: true,
			hasTime: true
		}];
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		if(me.BmsAccountInputID) {
			params.push('bmsaccountsaledoc.BmsAccountInput.Id=' + me.BmsAccountInputID);
		}
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		}
		return me.callParent(arguments);
	}
});