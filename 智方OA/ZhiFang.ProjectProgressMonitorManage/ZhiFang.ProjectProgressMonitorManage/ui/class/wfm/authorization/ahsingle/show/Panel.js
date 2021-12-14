/**
 * 合同信息
 * @author longfc
 * @version 2016-12-28
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.show.Panel', {
	extend: 'Shell.ux.panel.AppPanel',
	requires: [
		'Shell.class.wfm.business.contract.basic.ContentPanel'
	],
	
	title: '合同信息',
	width:650,
	height: 500,

	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	PClientID: null,
	PK: null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
	},
	initListeners: function() {
		var me = this;
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ContractPanel = Ext.create('Shell.class.wfm.authorization.contract.ShowPanel', {
			region: 'west',
			width: 340,
			header: true,
			split: true,
			itemId: 'ContractPanel',
			PClientID: me.PClientID
		});
		me.ShowTabPanel = Ext.create('Shell.class.wfm.authorization.ahsingle.show.ShowTabPanel', {
			region: 'center',
			border:false,
			header: false,
			itemId: 'ShowTabPanel',
			PK:me.PK
		});
		return [me.ContractPanel, me.ShowTabPanel];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			items = me.createButtontoolbar();
			return Ext.create('Ext.toolbar.Toolbar', {
				dock: 'bottom',
				itemId: 'buttonsToolbar',
				items: items
			});
		}
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		return items;
	}
});