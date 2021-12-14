/**
 * 合同
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceive.ContractGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '合同',

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPContractByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePContractByField',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelPContract',
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**默认加载数据*/
	defaultLoad: false,
	/**付款单位ID*/
	PayOrgID: null,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PContract_SignDate',
		direction: 'DESC'
	}],
	defaultWhere:'pcontract.IsUse=1',
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
		var columns = [{
			text: '合同名称',
			dataIndex: 'PContract_Name',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '签署时间',
			dataIndex: 'PContract_SignDate',
			width: 85,
			isDate: true,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '总额',
			dataIndex: 'PContract_Amount',
			width: 80,
			sortable: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '已收款',
			dataIndex: 'PContract_PayedMoney',
			width: 80,
			sortable: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		},{
			text: '签署人',
			dataIndex: 'PContract_SignMan',
			width: 70,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'PContract_Id',
			isKey: true,
			hidden: true,
			hideable: false
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
			params.push("pcontract.PayOrgID='" + me.PayOrgID + "'");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**@overwrite 改变返回的数据
	 * 显示未全部收款的合同
	 * */
	changeResult: function(data) {
		var list = [],
		result = {};
		if(data.value) {
			var redata = data.value.list;
			for(var i = 0; i < redata.length; i++) {
				var Amount=redata[i].PContract_Amount;
                var PayedMoney=redata[i].PContract_PayedMoney;
				if(Amount.toString() != PayedMoney.toString()) {
					list.push(redata[i]);
				}
			}
			result.list = list;
		}
		return result;
	}
});