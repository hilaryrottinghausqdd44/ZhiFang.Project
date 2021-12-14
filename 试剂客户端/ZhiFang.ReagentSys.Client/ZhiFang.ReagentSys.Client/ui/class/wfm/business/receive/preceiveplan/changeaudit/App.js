/**
 * 收款计划变更审核
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.changeaudit.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '收款计划变更审核',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.AddPanel.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var PPReceivePlanID = record.get(me.AddPanel.Grid.PKField);
					me.AddPanel.ChangeGrid.PPReceivePlanID = PPReceivePlanID;
					me.AddPanel.ChangeGrid.onSearch();
					//合同
					var ContractID = record.get('PReceivePlan_PContractID');
			        me.ContentPanel.load(ContractID);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var PPReceivePlanID = record.get(me.AddPanel.Grid.PKField);
					me.AddPanel.ChangeGrid.PPReceivePlanID = PPReceivePlanID;
					me.AddPanel.ChangeGrid.onSearch();
					//合同
					var ContractID = record.get('PReceivePlan_PContractID');
			        me.ContentPanel.load(ContractID);
				}, null, 500);
			},
			nodata: function(p) {
				me.AddPanel.ChangeGrid.clearData();
				me.ContentPanel.clearData();
			}
		});
		me.AddPanel.ChangeGrid.on({
			save:function(p){
				me.AddPanel.Grid.onSearch();
			}
		});
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ContentPanel = Ext.create('Shell.class.wfm.business.contract.basic.ContentPanel', {
			region: 'east',
			collapsible: true,
			width: 450,
			split: true,
			header: false,
			title: '合同信息',
			itemId: 'ContentPanel'
		});
		me.AddPanel = Ext.create('Shell.class.wfm.business.receive.preceiveplan.changeaudit.AddPanel', {
			region: 'center',
			header: false,
			border:false,
			title: '变更内容',
			itemId: 'AddPanel'
		});
		return [me.ContentPanel, me.AddPanel];
	}
});