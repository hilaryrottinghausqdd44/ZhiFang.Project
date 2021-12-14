/**
 * 项目（实际）检测量
 * 从Lis系统获取数据,统计每台仪器每个项目实际检测量
 * @author longfc	
 * @version 2019-02-22
 */
Ext.define('Shell.class.rea.client.statistics.consume.item.App', {
	extend: 'Ext.panel.Panel',

	title: '项目检测量',
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

		me.EquipGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.TabPanel.equipIDStr = record.get('ReaTestEquipLab_Id');
					me.TabPanel.equipCName = record.get('ReaTestEquipLab_CName');
					me.TabPanel.lisEquipCodeStr = record.get('ReaTestEquipLab_LisCode');
					me.TabPanel.onSearch();
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.TabPanel.equipIDStr = record.get('ReaTestEquipLab_Id');;
					me.TabPanel.lisEquipCodeStr = record.get('ReaTestEquipLab_LisCode');
					me.TabPanel.onSearch();
				}, null, 500);
			},
			nodata: function(p) {
				//				me.EditPanel.clearData();
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
		me.EquipGrid = Ext.create('Shell.class.rea.client.statistics.consume.basic.EquipGrid', {
			header: false,
			title: '仪器',
			itemId: 'EquipGrid',
			region: 'west',
			width: 320,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.TabPanel = Ext.create('Shell.class.rea.client.statistics.consume.item.TabPanel', {
			header: false,
			border: false,
			itemId: 'TabPanel',
			region: 'center',
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.EquipGrid, me.TabPanel];
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