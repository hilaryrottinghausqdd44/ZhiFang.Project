/**
 * 合同选择列表
 * @author liangyl	
 * @version 2015-11-10
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.basic.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '合同选择列表',
	width:500,
	height:400,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractByHQL?isPlanish=true',
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
		me.defaultWhere = 'pcontract.IsUse=1 and  pcontract.ContractStatus>1';
		me.searchInfo = {
			width: '76%',
			emptyText: '合同名称/客户',
			isLike: true,
			fields: ['pcontract.Name','pcontract.PClientName']
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
		}, {
			text: '金额',
			dataIndex: 'PContract_Amount',
			hidden: true,
			hideable: false
		}, {
			text: '销售负责人ID',
			dataIndex: 'PContract_PrincipalID',
			hidden: true,
			hideable: false
		}, {
			text: '销售负责人',
			dataIndex: 'PContract_Principal',
			hidden: true,
			hideable: false
		}, {
			text: '付款单位ID',
			dataIndex: 'PContract_PayOrgID',
			hidden: true,
			hideable: false
		}, {
			text: '付款单位',
			dataIndex: 'PContract_PayOrg',
			hidden: true,
			hideable: false
		} ,{
			text: '客户ID',
			dataIndex: 'PContract_PClientID',
			hidden: true,
			hideable: false
		}, {
			text: '客户',
			dataIndex: 'PContract_PClientName',
			hidden: true,
			hideable: false
		}];
		return columns;
	}

});