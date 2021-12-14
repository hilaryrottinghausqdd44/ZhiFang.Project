/**
 * 入库移库
 * @author longfc
 * @version 2019-03-28
 */
Ext.define('Shell.class.rea.client.transfer.ofin.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '入库移库',
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
			select: function(RowModel, record) {
				me.isAdd(record);
			},
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.isAdd(record);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.isAdd(record);
				}, null, 500);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
		me.EditPanel.on({
			onLaunchFullScreen: function() {
				me.DocGrid.collapse();
			},
			onExitFullScreen: function() {
				me.DocGrid.expand();
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
		me.DocGrid = Ext.create('Shell.class.rea.client.transfer.ofin.DocGrid', {
			header: false,
			title: '入库信息',
			itemId: 'DocGrid',
			region: 'west',
			width: 360,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.transfer.ofin.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DocGrid, me.EditPanel];
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
	clearData: function() {
		var me = this;
	},
	nodata: function(record) {
		var me = this;
		me.EditPanel.clearData();
		me.clearData();
	},
	isAdd: function(record) {
		var me = this;
		me.EditPanel.isAdd(record);
	},
	isShow: function(record) {
		var me = this;
		me.EditPanel.isShow(record);
	}
});