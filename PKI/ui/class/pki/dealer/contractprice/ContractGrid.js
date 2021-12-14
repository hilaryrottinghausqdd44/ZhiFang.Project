/**
 * 经销商合同列表
 * @author longfc
 * @version 2016-05-11
 */
Ext.define('Shell.class.pki.dealer.contractprice.ContractGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '经销商合同列表',

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDContractPriceByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DContractPrice_BeginDate',
		direction: 'DESC'
	}, {
		property: 'DContractPrice_SampleCount',
		direction: 'DESC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			load: function(store, records, successful) {
//				if (!successful || !records || records.length <= 0) {
//					me.clearData();
//				}
				if (store && store != null&&records!=null) {
					me.store = me.changContractGridStore(store, records);
				}
			}
		});
	},
	/***
	 * 过滤经销商的合同列表的合同编重复的数据
	 * @param {} s
	 * @param {} records
	 * @return {}
	 */
	changContractGridStore: function(s, records) {
		var a = {},
			b = {};
		var len = records.length;
		for (var i = 0; i < len; i++) {
			if (typeof a[records[i].get('DContractPrice_ContractNo')] == 'undefined') {
				a[records[i].get('DContractPrice_ContractNo')] = 1;
				b[records[i]] = 1;
			} else {
				s.remove(records[i]);
			}
		}
		return s;
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		//查询框信息
		me.searchInfo = {
			width: '100%',
			emptyText: '合同编号',
			isLike: true,
			fields: ['dcontractprice.ContractNo']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			type: 'search',
			info: me.searchInfo
		}];
		if (!me.readOnly) {
			//创建挂靠功能栏
			me.dockedItems = [Ext.create('Shell.ux.toolbar.Button', {
				dock: 'bottom',
				items: ['add', 'edit', 'del', '-', 'import_excel']
			})];
			me.hasDel = true;
		}
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var columns = [{
			dataIndex: 'DContractPrice_BDealer_Name',
			text: '经销商',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_ContractNo',
			text: '合同号',
			width: 230,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_BeginDate',
			text: '合同起始日',
			width: 80,
			hidden: true,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_EndDate',
			text: '合同终止日',
			width: 80,
			hidden: true,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_ConfirmUser',
			text: '合同确认人',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_ConfirmTime',
			text: '合同确认时间',
			hidden: true,
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'DContractPrice_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'DContractPrice_SLabID',
			text: '送检单位ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**根据经销商ID获取数据*/
	loadByDealerId: function(id) {
		var me = this;
		me.DealerId = id;
		me.defaultWhere = 'dcontractprice.BDealer.Id=' + id;
		me.onSearch();
	},
    /**根据送检单位ID获取数据*/
	loadByBLaboratoryId: function(id) {
		var me = this;
		me.DealerId = id;
		me.defaultWhere = 'dcontractprice.BLaboratory.Id=' + id;
		me.onSearch();
	}
});