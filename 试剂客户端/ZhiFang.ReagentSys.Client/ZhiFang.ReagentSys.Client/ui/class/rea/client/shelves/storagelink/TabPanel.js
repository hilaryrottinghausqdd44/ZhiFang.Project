/**
 * 库房人员权限关系维护
 * @author longfc	
 * @version 2019-04-02
 */
Ext.define('Shell.class.rea.client.shelves.storagelink.TabPanel', {
	extend: 'Ext.tab.Panel',

	title: '库房人员权限',
	header: false,
	border: false,
	bodyPadding: 1,
	//activeTab: 0,
	storageID: null,
	storageName: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'OfManageGrid':
						me.OfManageGrid.onSearch();
						break;
					case 'OfTransferApplyGrid':
						me.OfTransferApplyGrid.onSearch();
						break;
					case 'OfDirectTransferGrid':
						me.OfDirectTransferGrid.onSearch();
						break;
					case 'OfOutApplyGrid':
						me.OfOutApplyGrid.onSearch();
						break;
					default:

						break
				}
			}
		});
		me.activeTab = 0;
		//me.OfManageGrid.onSearch();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.OfManageGrid = Ext.create('Shell.class.rea.client.shelves.storagelink.OfManageGrid', {
			title: '库房管理权限',
			header: true,
			itemId: 'OfManageGrid'
		});
		me.OfTransferApplyGrid = Ext.create('Shell.class.rea.client.shelves.storagelink.OfTransferApplyGrid', {
			title: '移库申请源库房',
			header: true,
			itemId: 'OfTransferApplyGrid'
		});
		me.OfDirectTransferGrid = Ext.create('Shell.class.rea.client.shelves.storagelink.OfDirectTransferGrid', {
			title: '直接移库源库房',
			header: true,
			itemId: 'OfDirectTransferGrid'
		});
		me.OfOutApplyGrid = Ext.create('Shell.class.rea.client.shelves.storagelink.OfOutApplyGrid', {
			title: '出库申请权限',
			header: true,
			itemId: 'OfOutApplyGrid'
		});
		var appInfos = [me.OfManageGrid, me.OfTransferApplyGrid, me.OfDirectTransferGrid, me.OfOutApplyGrid];
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		return null;
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
	loadData: function() {
		var me = this;
		me.OfManageGrid.storageID = me.storageID;
		me.OfManageGrid.storageName = me.storageName;
		me.OfTransferApplyGrid.storageID = me.storageID;
		me.OfTransferApplyGrid.storageName = me.storageName;
		me.OfDirectTransferGrid.storageID = me.storageID;
		me.OfDirectTransferGrid.storageName = me.storageName;
		me.OfOutApplyGrid.storageID = me.storageID;
		me.OfOutApplyGrid.storageName = me.storageName;

		switch(me.getActiveTab().itemId) {
			case 'OfManageGrid':
				me.OfManageGrid.onSearch();
				break;
			case 'OfTransferApplyGrid':
				me.OfTransferApplyGrid.onSearch();
				break;
			case 'OfDirectTransferGrid':
				me.OfDirectTransferGrid.onSearch();
				break;
			case 'OfOutApplyGrid':
				me.OfOutApplyGrid.onSearch();
				break;
			default:
				break
		}
	},
	nodata: function() {
		var me = this;
		me.storageID = null;
		me.storageName = null;

		me.OfManageGrid.storageID = me.storageID;
		me.OfManageGrid.storageName = me.storageName;
		me.OfTransferApplyGrid.storageID = me.storageID;
		me.OfTransferApplyGrid.storageName = me.storageName;
		me.OfDirectTransferGrid.storageID = me.storageID;
		me.OfDirectTransferGrid.storageName = me.storageName;
		me.OfOutApplyGrid.storageID = me.storageID;
		me.OfOutApplyGrid.storageName = me.storageName;

		me.OfManageGrid.store.removeAll();
		me.OfTransferApplyGrid.store.removeAll();
		me.OfDirectTransferGrid.store.removeAll();
		me.OfOutApplyGrid.store.removeAll();
	}
});