/**
 * 移库管理
 * @author liangyl
 * @version 2018-03-19
 */
Ext.define('Shell.class.rea.client.transfer.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '移库管理',
	header: false,
	border: false,
	layout: {
		type: 'border'
	},
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	/**1,只支持直接按库存试剂进行移库   2,支持直接按库存试剂进行移库,支持按移库申请进行移库 3,支持两种方式两个按钮都显示*/
	TYPE: '1',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DocGrid.on({
			itemclick: function(v, record) {
				me.onSelect(record);
			},
			select: function(RowModel, record) {
				me.onSelect(record);
			},
			nodata: function(p) {
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
		var DocGridPanel = 'Shell.class.rea.client.transfer.DocGrid';
		if(me.TYPE == '2') { //申请	
			DocGridPanel = 'Shell.class.rea.client.transfer.accept.DocGrid';
		}
		me.DocGrid = Ext.create(DocGridPanel, {
			header: false,
			title: '移库主单',
			itemId: 'DocGrid',
			region: 'west',
			width: 365,
			TYPE: me.TYPE,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.ShowPanel = Ext.create('Shell.class.rea.client.transfer.show.ShowPanel', {
			header: false,
			itemId: 'ShowPanel',
			region: 'center',
			border: false,
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
	},
	onSelect: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			me.DocGrid.onBtnChange(record);
			var id = record.get('ReaBmsTransferDoc_Id');
			me.ShowPanel.loadData(id);
		}, null, 500);
	}
});