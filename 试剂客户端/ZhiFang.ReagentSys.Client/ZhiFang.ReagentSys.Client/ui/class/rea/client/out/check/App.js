/**
 * 出库审核
 * @author longfc
 * @version 2019-03-18
 */
Ext.define('Shell.class.rea.client.out.check.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '出库审批',
	layout: {
		type: 'border'
	},
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	/**出库审核类型:1:出库审核;2:出库审核+;*/
	TYPE: "1",
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.DocGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('ReaBmsOutDoc_Id');
					me.ShowPanel.onSearch(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('ReaBmsOutDoc_Id');
					me.ShowPanel.onSearch(id);
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
		me.DocGrid = Ext.create('Shell.class.rea.client.out.check.DocGrid', {
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
		me.ShowPanel = Ext.create('Shell.class.rea.client.out.check.ShowPanel', {
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