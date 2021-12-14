/**
 * 出库申请
 * @author liangyl
 * @version 2018-10-29
 */
Ext.define('Shell.class.rea.client.out.apply.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '出库申请',
	header: false,
	border: false,
	layout: {
		type: 'border'
	},
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	/**@description 新增/编辑/查看*/
	formtype: 'show',
	/**出库申请类型:1:出库申请;2:出库申请+;*/
	TYPE: "1",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.DocGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.ShowPanel.onSearch(record.get('ReaBmsOutDoc_Id'));
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.ShowPanel.onSearch(record.get('ReaBmsOutDoc_Id'));
				}, null, 500);
			},
			nodata: function(p) {
				me.ShowPanel.clearData();
			}
		});
		//预加载是否开启近效期
		JShell.REA.RunParams.getRunParamsValue("IsOpenNearEffectPeriod", false, function(data1) {});
		//预加载是否强制近效期出库
		JShell.REA.RunParams.getRunParamsValue("IsOutOfStockInNeartermPeriod", false, function(data1) {});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.out.apply.DocGrid', {
			header: false,
			title: '出库主单',
			itemId: 'DocGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			TYPE: me.TYPE
		});
		me.ShowPanel = Ext.create('Shell.class.rea.client.out.apply.ShowPanel', {
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
	}
});