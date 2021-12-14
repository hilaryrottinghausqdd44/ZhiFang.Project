/**
 * 合同、发票、商务收款记录
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceive.EndPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '财务收款',
		/**付款单位ID*/
	PayOrgID:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.ShowTabPanel = Ext.create('Shell.class.wfm.business.receive.preceive.ShowTabPanel', {
			region: 'center',
			header: false,
			title: '合同和发票页签',
			itemId: 'ShowTabPanel'
		});
		me.Grid = Ext.create('Shell.class.wfm.business.receive.preceive.Grid', {
			region: 'east',
			split: true,
			header: false,
			width: 400,
			title: '商务收款记录',
			collapsible: true,
			itemId: 'Grid'
		});
		return [me.ShowTabPanel, me.Grid];
	}
});