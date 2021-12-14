/**
 * @description 库存预警
 * @author longfc
 * @version 2018-03-23
 */
Ext.define('Shell.class.rea.client.qtywarning.App', {
	extend: 'Ext.tab.Panel',

	title: '库存预警',
	header: false,
	border: false,
	bodyPadding: 1,
	//activeTab: 0,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.onListeners();
		me.activeTab = 0;
		me.StoreLowerPanel.loadData();
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
		me.StoreLowerPanel = Ext.create('Shell.class.rea.client.qtywarning.storelower.Panel', {
			title: '低库存预警',
			header: true,
			border: false,
			itemId: 'StoreLowerPanel'
		});
		me.StoreUpperPanel = Ext.create('Shell.class.rea.client.qtywarning.storeupper.Panel', {
			title: '高库存预警',
			header: true,
			border: false,
			itemId: 'StoreUpperPanel'
		});
		var appInfos = [me.StoreLowerPanel, me.StoreUpperPanel];
		return appInfos;
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
	},
	onListeners:function(){
		var me = this;
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'StoreLowerPanel':
						me.StoreLowerPanel.loadData();
						break;
					case 'StoreUpperPanel':
						me.StoreUpperPanel.loadData();
						break;
					default:
		
						break
				}
			}
		});
	}
});