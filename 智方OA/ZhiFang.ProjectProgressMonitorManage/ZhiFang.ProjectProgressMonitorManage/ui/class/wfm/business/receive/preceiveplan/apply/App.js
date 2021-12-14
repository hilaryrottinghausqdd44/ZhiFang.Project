/**
 * 商务二 合同计划（新增)
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.apply.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '收款计划',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ContractPanel.on({
			checkClick: function(ContractId, ContractName, PrincipalID, Principal, Amount,PayOrgID,PayOrg,PClientID,PClientName,p) {
				me.Grid.PContractID = ContractId;
				me.Grid.PContractName = ContractName;
				me.Grid.Amount = Amount;
				me.Grid.changeAmountText(Amount,'合同金额:');
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
		me.Grid.on({
			save:function(){
				JShell.Action.delay(function() {
					me.Grid.load();
				}, null, 500);
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
		me.Grid = Ext.create('Shell.class.wfm.business.receive.preceiveplan.apply.GridTree', {
			region: 'center',
			header: false,
			title: '收款计划',
			itemId: 'Grid'
		});
		me.ContractPanel = Ext.create('Shell.class.wfm.business.receive.preceiveplan.basic.ContractPanel', {
			region: 'west',
			collapsible: true,
			width: 320,
			split: true,
			header: false,
			itemId: 'ContractPanel'
		});
		return [me.ContractPanel, me.Grid];
	}
});