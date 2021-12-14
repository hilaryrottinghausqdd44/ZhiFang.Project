/**
 * 合同快照查询列表
 * @author longfc
 * @version 2016-07-25
 */
Ext.define('Shell.class.pki.contractprice.SearchDContractPriceBakGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '合同快照查询',
	showSuccessInfo: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 100,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**导出功能*/
	buttonToolbarItems: ['exp_excel'],
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DContractPriceBak_BeginDate',
		direction: 'ASC'
	}, {
		property: 'DContractPriceBak_EndDate',
		direction: 'ASC'
	}, {
		property: 'DContractPriceBak_ContractNo',
		direction: 'ASC'
	}, {
		property: 'DContractPriceBak_OperTime',
		direction: 'ASC'
	}],
	/**下载EXCEL文件服务地址*/
	downLoadExcelUrl: '/StatService.svc/Stat_UDTO_DContractPriceBakToExcel',
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDContractPriceBakByHQL?isPlanish=true',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.getComponent('filterToolbar').on({
			search: function(p, params) {
				me.params = params;
				me.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.contractprice.SearchToolbar', {
			itemId: 'filterToolbar',
			dock: 'top',
			isLocked: true,
			height: 80
		})];

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			dataIndex: 'DContractPriceBak_BeginDate',
			text: '开始日期',
			width: 80,
			isDate: true,
			sortable: true
		}, {
			dataIndex: 'DContractPriceBak_EndDate',
			text: '截止日期',
			width: 80,
			isDate: true,
			sortable: true
		}, {
			dataIndex: 'DContractPriceBak_ContractNo',
			text: '合同编号',
			width: 120,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPriceBak_ContractType',
			text: '合同类型',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.ContractType2['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DContractPriceBak_BLaboratory_CName',
			text: '送检单位',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPriceBak_BDealer_Name',
			text: '经销商',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPriceBak_BTestItem_CName',
			text: '项目',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPriceBak_IsStepPrice',
			text: '是否阶梯价',
			width: 80,
			align: 'center',
			isBool: true,
			type: 'bool'
		}, {
			dataIndex: 'DContractPriceBak_SampleCount',
			text: '数量',
			width: 60,
			sortable: false
		});
		columns.push({
			dataIndex: 'DContractPriceBak_StepPrice',
			text: '价格',
			width: 80,
			type: 'float',
			align: 'right',
			renderer: function(value, meta) {
				var v = value == null ? '' : value;

				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';

				if(!value || value == 0 || value == '0') {
					meta.style = 'background-color:red;';
				}

				return v;
			}
		});
		columns.push({
			dataIndex: 'DContractPriceBak_BDealer_BBillingUnit_Name',
			hidden: true,
			text: '开票方',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPriceBak_BillingUnitType',
			text: '开票方类型',
			width: 80,
			hidden: true,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DContractPriceBak_AddUser',
			hidden: true,
			text: '合同录入人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPriceBak_ConfirmUser',
			hidden: true,
			text: '合同确认人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPriceBak_OperUser',
			hidden: false,
			text: '修改人',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPriceBak_OperTime',
			text: '修改时间',
			width: 130,
			isDate: true,
			hasTime: true,
			sortable: false
		}, {
			dataIndex: 'DContractPriceBak_OperMemo',
			text: '修改备注',
			width: 110,
			sortable: false
		}, {
			dataIndex: 'DContractPriceBak_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		});
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		me.params = me.params || me.getComponent('filterToolbar').getParams();

		var strWhere = "(1=1)";
		//做处理
		if(me.params.StartDate && me.params.EndDate) {//BeginDate
			strWhere = strWhere + " and ((dcontractpricebak.BeginDate>='" + me.params.StartDate.trim() + "'" +
				" and dcontractpricebak.BeginDate<='" + me.params.EndDate.trim() + "') or(dcontractpricebak.EndDate>='" + me.params.StartDate.trim() + "'" +
				" and dcontractpricebak.EndDate<='" + me.params.EndDate.trim() + "'))"; // 00:00:00.000
		};
		if(me.params.Laboratory_Id) {
			strWhere = strWhere + " and (dcontractpricebak.BLaboratory.Id=" + me.params.Laboratory_Id + ")";
		};
		if(me.params.TestItem_Id) {
			strWhere = strWhere + " and (dcontractpricebak.BTestItem.Id=" + me.params.TestItem_Id + ")";
		};
		if(me.params.Dealer_Id) {
			strWhere = strWhere + " and (dcontractpricebak.BDealer.Id=" + me.params.Dealer_Id + ")";
		};
		if(me.params.BillingUnit_Id) {
			strWhere = strWhere + " and (dcontractpricebak.BillingUnit.Id=" + me.params.BillingUnit_Id + ")";
		};

		if(me.params.CoopLevel) {
			strWhere = strWhere + " and (dcontractpricebak.ContractType=" + me.params.CoopLevel + ")";
		};
		if(me.params.BillingUnitType) {
			strWhere = strWhere + " and (dcontractpricebak.BillingUnitType=" + me.params.BillingUnitType + ")";
		};
		if(me.params.IsStepPrice) {
			strWhere = strWhere + " and (dcontractpricebak.IsStepPrice=" + me.params.IsStepPrice + ")";
		};
		if(me.params.ContractNo) {
			strWhere = strWhere + " and (dcontractpricebak.ContractNo='" + me.params.ContractNo + "')";
		};
		me.externalWhere = strWhere;

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";

		if(where) {
			url += '&where=' + JShell.String.encode(where);
		}

		return url;
	}
});