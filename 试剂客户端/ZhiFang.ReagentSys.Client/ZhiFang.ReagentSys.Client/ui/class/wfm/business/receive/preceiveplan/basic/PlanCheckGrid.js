/**
 * 收款计划选择列表
 * @author liangyl	
 * @version 2015-11-10
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.basic.PlanCheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '收款计划选择列表',
	width:500,
	height:400,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPReceivePlanByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'PReceivePlan_ExpectReceiveDate',
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
		me.defaultWhere = 'preceiveplan.IsUse=1';
		me.searchInfo = {
			width: '76%',
			emptyText: '收款分期',
			isLike: true,
			fields: ['preceiveplan.ReceiveGradationName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '收款分期',
			dataIndex: 'PReceivePlan_ReceiveGradationName',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '收款时间',
			dataIndex: 'PReceivePlan_ExpectReceiveDate',
			width: 150,
			sortable: false,
			defaultRenderer: true
		},{
			text: '收款金额',
			dataIndex: 'PReceivePlan_ReceivePlanAmount',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'PReceivePlan_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}];
		return columns;
	}

});