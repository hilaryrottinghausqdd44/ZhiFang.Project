/**
 * 条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.App', {
	extend: 'Ext.tab.Panel',
	
	title: '条码规则维护',
	header: false,
	border: false,
	bodyPadding: 1,
	activeTab: 0,
	
	/**应用类型:是否平台:是:1,否:0或null*/
	APPTYPE:"1",
	
	initComponent: function() {
		var me = this;
		//me.createTabBar();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	createTabBar: function() {
		var me = this;
		me.tabBar = {
			items: [{
				xtype: 'tbfill'
			}, {
				xtype: 'button',
				iconCls: 'button-exp',
				style: {
					padding: '1px',
					marginRight: "15px"
				},
				text: '导出',
				tooltip: '导出选择的供应商所属条码规则及公共的条码规则给离线客户端',
				handler: function() {
					//me.onExpBarCodeFormat();
				}
			}, {
				xtype: 'button',
				iconCls: 'button-import',
				style: {
					padding: '1px',
					marginRight: "15px"
				},
				text: '导入',
				tooltip: '客户端导入条码规则(覆盖式导入,先删除原有的条码)',
				handler: function() {
					//me.onImportBarCodeFormat();
				}
			}]
		};
	},
	createItems: function() {
		var me = this;
		me.CommonApp = Ext.create('Shell.class.rea.client.barcodeformat.common.App', {
			title: '公共条码规则维护',
			header: true,
			itemId: 'CommonApp',
			APPTYPE: me.APPTYPE
		});
		me.CenorgApp = Ext.create('Shell.class.rea.client.barcodeformat.cenorg.App', {
			title: '供应商条码规则维护',
			header: true,
			itemId: 'CenorgApp',
			APPTYPE: me.APPTYPE
		});
		return [me.CommonApp, me.CenorgApp];
	},
	/**页签切换事件处理*/
	ontabchange: function() {
		var me = this;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	}
});