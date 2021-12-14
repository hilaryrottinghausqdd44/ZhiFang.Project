/**
 * @description 效期预警
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.validitywarning.App', {
	extend: 'Ext.tab.Panel',

	title: '效期预警',
	header: false,
	border: false,
	bodyPadding: 1,
	//activeTab: 0,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.onListeners();
		me.activeTab = 0;
		me.ExpiredPanel.loadData();
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		//me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ExpiredPanel = Ext.create('Shell.class.rea.client.validitywarning.expired.Panel', {
			title: '效期已过期报警',
			header: true,
			itemId: 'ExpiredPanel'
		});
		me.WillExpirePanel = Ext.create('Shell.class.rea.client.validitywarning.willexpire.Panel', {
			title: '效期将过期报警',
			header: true,
			itemId: 'WillExpirePanel'
		});
		var appInfos = [me.ExpiredPanel, me.WillExpirePanel];
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		return null;
	},
	onListeners: function() {
		var me = this;
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch (newCard.itemId) {
					case 'ExpiredPanel':
						me.ExpiredPanel.loadData();
						break;
					case 'WillExpirePanel':
						me.WillExpirePanel.loadData();
						break;
					default:

						break
				}
			}
		});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		}
	}
});
