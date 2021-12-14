/**
 * 发票查询
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.invoice.show.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '发票查询',
	/**是否是管理员,不是管理员ISADMIN==0  是管理员ISADMIN=1*/
	ISADMIN: 0,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var ContractID = record.get('PInvoice_ContractID');
					me.ContentPanel.load(ContractID);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var ContractID = record.get('PInvoice_ContractID');
					me.ContentPanel.load(ContractID);
				}, null, 500);
			},
			nodata: function(p) {
				me.ContentPanel.clearData();
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
		var calssName='Shell.class.wfm.business.invoice.show.SimpleGrid';
		//管理员
		if(me.ISADMIN==1){
			calssName='Shell.class.wfm.business.invoice.show.Grid';
		}
		
		me.Grid = Ext.create(calssName, {
			region: 'center',
			header: false,
			ISADMIN:me.ISADMIN,
			title: '发票',
			itemId: 'Grid'
		});
		me.ContentPanel = Ext.create('Shell.class.wfm.business.contract.basic.ContentPanel', {
			//			header: false,
			title: '合同信息',
			region: 'east',
			width: 240,
			split: true,
			collapsible: true,

			itemId: 'ContentPanel'
		});
		return [me.ContentPanel, me.Grid];
	}
});