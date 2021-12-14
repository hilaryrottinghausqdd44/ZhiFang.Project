/**
 * 移库申请
 * @author liangyl
 * @version 2018-11-05
 */
Ext.define('Shell.class.rea.client.transfer.apply.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '移库申请',
	header: false,
	border: false,
	layout: {
		type: 'border'
	},
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.DocGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.DocGrid.onBtnChange(record);
					var id = record.get('ReaBmsTransferDoc_Id');
					me.ShowPanel.loadData(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.DocGrid.onBtnChange(record);
					var id = record.get('ReaBmsTransferDoc_Id');
					me.ShowPanel.loadData(id);
				},null,500);
			},
			nodata:function(p){
				me.ShowPanel.clearData();
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
		me.DocGrid = Ext.create('Shell.class.rea.client.transfer.apply.DocGrid', {
			header: false,
			title: '移库主单',
			itemId: 'DocGrid',
			region: 'west',
			width: 365,
			split: true,
			collapsible: true,
			collapseMode:'mini'	
		});
		me.ShowPanel = Ext.create('Shell.class.rea.client.transfer.apply.ShowPanel', {
			header: false,
			itemId: 'ShowPanel',
			region: 'center',
			border:false,
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.DocGrid, me.ShowPanel];
		return appInfos;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	}
});