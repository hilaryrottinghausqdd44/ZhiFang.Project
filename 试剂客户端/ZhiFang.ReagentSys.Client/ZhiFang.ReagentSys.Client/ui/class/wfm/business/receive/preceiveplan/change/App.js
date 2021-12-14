/**
 * 收款计划变更
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.change.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '收款计划变更',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ContractPanel.on({
			checkClick: function(ContractId, ContractName, PrincipalID, Principal, Amount,PayOrgID,PayOrg,PClientID,PClientName,p ) {
				me.Grid.PContractID = ContractId;
				me.Grid.PContractName = ContractName;
				me.Grid.Amount = Amount;
				me.Grid.PrincipalID = PrincipalID;
				me.Grid.Principal = Principal;
				me.Grid.PayOrgID = PayOrgID;
				me.Grid.PayOrg = PayOrg;
				me.Grid.PClientID = PClientID;
				me.Grid.PClientName = PClientName;
				me.Grid.load();
			    p.close();
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
		me.Grid = Ext.create('Shell.class.wfm.business.receive.preceiveplan.change.GridTree', {
			region: 'center',
			header: false,
			title: '收款计划',
			itemId: 'Grid'
		});
		me.ContractPanel = Ext.create('Shell.class.wfm.business.receive.preceiveplan.basic.ContractPanel', {
			region: 'west',
			collapsible: true,
			width: 350,
			split: true,
			header: false,
			className:'Shell.class.wfm.business.receive.preceiveplan.change.CheckGrid',
			itemId: 'ContractPanel'
		});
		return [me.ContractPanel, me.Grid];
	}
});