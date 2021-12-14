/**
 * @description 订单查看
 * @author liangyl	
 * @version 2018-10-22
 */
Ext.define('Shell.class.rea.client.order.show.App', {
	extend: 'Ext.panel.Panel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '订单申请',
	header: false,
	border: false,

	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},

	/**新增/编辑/查看*/
	formtype: 'show',


	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.OrderGrid.on({
			select: function(RowModel, record) {
				JShell.Action.delay(function(){
				     me.loadData(record);
				},null,200);
			},
			nodata: function(p) {
				me.nodata();
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
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.show.OrderGrid', {
			header: false,
			collapseMode:'mini',
			itemId: 'OrderGrid',
			region: 'west',
			width: 325,
			split: true,
			collapsible: false,
			collapsed: false
		});
		me.ShowPanel = Ext.create('Shell.class.rea.client.order.show.ShowPanel', {
			header: false,
			itemId: 'OrderPanel',
			region: 'center',
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.OrderGrid, me.ShowPanel];
		return appInfos;
	},
	nodata: function(record) {
		var me = this;
		me.ShowPanel.clearData();
	},
	isShow: function(record) {
		var me = this;
		me.ShowPanel.isShow(record, me.OrderGrid);
		me.ShowPanel.OrderDtlGrid.buttonsDisabled = true;
	},

	loadData: function(record) {
		var me = this;
		me.isShow(record);
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