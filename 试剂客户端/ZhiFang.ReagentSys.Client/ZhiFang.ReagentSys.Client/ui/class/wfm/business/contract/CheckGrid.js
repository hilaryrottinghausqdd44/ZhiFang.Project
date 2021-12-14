/**
 * 合同选择列表
 * 默认条件是合同启用并且为合同状态为评审通过
 * @author longfc
 * @version 2017-03-16
 */
Ext.define('Shell.class.wfm.business.contract.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '合同选择列表',
	height: 460,
	width: 350,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPContractByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'PContract_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'pcontract.IsUse=1 and pcontract.ContractStatus=6';
		me.searchInfo = {
			width: 165,
			emptyText: '名称',
			isLike: true,
			fields: ['pcontract.Name']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '合同编号',
			dataIndex: 'PContract_ContractNumber',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '客户名称',
			dataIndex: 'PContract_PClientName',
			width: 150,
			sortable: false,
			defaultRenderer: true
		},  {
			text: '客户Id',
			dataIndex: 'PContract_PClientID',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '合同名称',
			dataIndex: 'PContract_Name',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'PContract_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '时间戳',
			dataIndex: 'PContract_DataTimeStamp',
			hidden: true,
			hideable: false
		}];
		return columns;
	}
});