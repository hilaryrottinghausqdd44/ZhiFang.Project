/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.apply.App', {
	extend: 'Shell.class.rea.client.confirm.basic.App',

	OTYPE: "apply",
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.OTYPE = me.OTYPE || "";
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.confirm.apply.DocGrid', {
			header: false,
			itemId: 'DocGrid',
			region: 'west',
			width: 385,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.confirm.apply.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			OTYPE: me.OTYPE,
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DocGrid, me.EditPanel];
		return appInfos;
	},
	/**
	 * @description 联动处理
	 */
	onListeners: function() {
		var me = this;
		me.DocGrid.on({
			select: function(RowModel, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
		me.EditPanel.on({
			save: function(p, isRefresh) {
				if (isRefresh == true) {
					me.DocGrid.onSearch();
				}
			}
		});
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];

		items.push({
			xtype: 'button',
			iconCls: 'button-import',
			itemId: "btnExtractBms",
			text: '供货验收',
			tooltip: '平台供货单验收',
			handler: function() {
				me.showExtractOfBms();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-import',
			itemId: "btnExtractRea",
			text: '订货验收',
			tooltip: '订单验收',
			handler: function() {}
		}, {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '手工验收',
			tooltip: '手工新增验货单',
			handler: function() {

			}
		});
		items.push("-", {
			xtype: 'button',
			itemId: 'btnEdit',
			iconCls: 'button-edit',
			text: "继续验收",
			tooltip: "对待继续验收继续验货",
			handler: function() {
				me.onContinueToAcceptClick();
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '确认验收',
			tooltip: '确认验收',
			handler: function() {
				me.onConfirmClick();
			}
		});
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-accept',
			itemId: "btnStorage",
			text: '入库',
			tooltip: '入库',
			handler: function() {
				me.onStorageClick();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	onStorageClick: function() {
		var me = this;
	}
});
