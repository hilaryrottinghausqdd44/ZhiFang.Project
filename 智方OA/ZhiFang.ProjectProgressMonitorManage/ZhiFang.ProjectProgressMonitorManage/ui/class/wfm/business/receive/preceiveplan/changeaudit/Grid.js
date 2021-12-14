/**
 * 变更中的收款计划
 * @author liangyl	
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.changeaudit.Grid', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.Grid',
	title: '变更中的收款计划',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	defaultLoad: true,
	/**带分页栏*/
	hasPagingtoolbar: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(0, 0, {
			text: '合同',
			hidden:true,
			dataIndex: 'PReceivePlan_PContractName',
			width: 150,
			hideable: false
		},{
			text: '合同Id',
			dataIndex: 'PReceivePlan_PContractID',
			hidden:true,
			width: 150,
			hideable: false
		});
		columns.push({
			text: '录入人',
			dataIndex: 'PReceivePlan_InputerName',
			hideable: false
		});
		return columns;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'PReceivePlanStatus', function() {
			if(!JShell.System.ClassDict.PReceivePlanStatus) {
				JShell.Msg.error('未获取到收款计划状态，请刷新列表');
				return;
			}
			//默认显示变更中的数据
			me.defaultWhere = "preceiveplan.Status=5 and preceiveplan.IsUse=1";
			me.load(null, true, autoSelect);
		});
	}

});