/**
 * 财务收款
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.finance.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '财务收款',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var PClientId = me.Grid.getComponent('buttonsToolbar').getComponent('PFinanceReceive_PClient_Id');
		var PClientName = me.Grid.getComponent('buttonsToolbar').getComponent('PFinanceReceive_PClient_Name');
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					var PayOrgId = record.get('PFinanceReceive_PayOrgID');
					var PayOrgName = record.get('PFinanceReceive_PayOrgName');
					me.SetPatOrg(PayOrgId, PayOrgName);
					me.Form.isEdit(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					var PayOrgId = record.get('PFinanceReceive_PayOrgID');
					var PayOrgName = record.get('PFinanceReceive_PayOrgName');
					me.SetPatOrg(PayOrgId, PayOrgName);
					me.Form.isEdit(id);
				}, null, 500);
			},
			addclick: function(p) {
				me.SetPatOrg(PClientId.getValue(), PClientName.getValue());
				me.Form.isAdd();
			},
			nodata: function(p) {
				me.Form.clearData();
			},
			checkclick: function(val, CName) {
				me.SetPatOrg(val, CName);
			}
		});
		me.Form.on({
			save: function(p, id) {
				me.Grid.onSearch();
			}
		});
	},
	SetPatOrg: function(id, name) {
		var me = this;
		var PClientName = me.Form.getComponent('PFinanceReceive_PClient_Name');
		PClientName.setText(name);
		me.Form.PayOrgID = id;
		me.Form.PayOrgName = name;
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Form = Ext.create('Shell.class.wfm.business.receive.finance.Form', {
			region: 'east',
			split: true,
			header: false,
			width: 230,
			title: '财务收款详情',
			collapsible: true,
			itemId: 'Form'
		});
		me.Grid = Ext.create('Shell.class.wfm.business.receive.finance.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.Form, me.Grid];
	}
});