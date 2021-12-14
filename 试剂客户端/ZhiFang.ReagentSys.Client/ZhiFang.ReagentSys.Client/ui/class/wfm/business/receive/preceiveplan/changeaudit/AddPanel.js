/**
 * 收款计划变更审核
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.changeaudit.AddPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '收款计划变更审核',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
//		me.Grid.on({
//			itemclick: function(v, record) {
//				JShell.Action.delay(function() {
//					var PPReceivePlanID = record.get(me.Grid.PKField);
//					me.ChangeGrid.PPReceivePlanID = PPReceivePlanID;
//					me.ChangeGrid.onSearch();
//				}, null, 500);
//			},
//			select: function(RowModel, record) {
//				JShell.Action.delay(function() {
//					var PPReceivePlanID = record.get(me.Grid.PKField);
//					me.ChangeGrid.PPReceivePlanID = PPReceivePlanID;
//					me.ChangeGrid.onSearch();
//				}, null, 500);
//			},
//			nodata: function(p) {
//				me.ChangeGrid.clearData();
//			}
//		});
//		me.ChangeGrid.on({
//			save:function(p){
//				me.Grid.onSearch();
//			}
//		});
//		
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Grid = Ext.create('Shell.class.wfm.business.receive.preceiveplan.changeaudit.Grid', {
			region: 'north',
			split: true,
			header: false,
			height: 280,
			title: '变更中的计划',
			collapsible: true,
			itemId: 'Grid'
		});
		me.ChangeGrid = Ext.create('Shell.class.wfm.business.receive.preceiveplan.changeaudit.ChangeGrid', {
			region: 'center',
			header: false,
			title: '变更后的计划',
			itemId: 'ChangeGrid'
		});
		return [me.Grid, me.ChangeGrid];
	}
});