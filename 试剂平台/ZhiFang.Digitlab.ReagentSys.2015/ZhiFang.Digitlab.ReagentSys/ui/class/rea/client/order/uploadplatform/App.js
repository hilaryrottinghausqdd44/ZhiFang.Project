/**
 * @description 订单上传平台
 * @author longfc
 * @version 2017-11-21
 */
Ext.define('Shell.class.rea.client.order.uploadplatform.App', {
	extend: 'Shell.class.rea.client.order.basic.App',

	title: '订单上传',

	/**录入:entry/审核:check*/
	OTYPE: "upload",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.OrderGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					//me.loadData(record);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					//me.loadData(record);
				}, null, 500);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.OTYPE = me.OTYPE || "upload";
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.uploadplatform.OrderGrid', {
			header: false,
			itemId: 'OrderGrid',
			region: 'west',
			width: 415,
			split: true,
			OTYPE: me.OTYPE,
			collapsible: false,
			collapsed: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.order.uploadplatform.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			collapsible: false,
			collapsed: false,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.OrderGrid, me.EditPanel];
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];

		if(items.length > 0) items.push('-');
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnSync",
			hidden:true,
			text: '平台编码同步',
			tooltip: '将当前选择订单与平台同步',
			handler: function() {
				//me.onSyncClick();
			}
		},{
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnUpload",
			text: '订单上传',
			tooltip: '将当前选择订单上传平台',
			handler: function() {
				me.onUploadClick();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	clearData: function(record) {
		var me = this;
		me.setBtnDisabled("btnUpload", true);
	},
	isShow: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setBtnDisabled("btnUpload", true);
		var status = record.get("BmsCenOrderDoc_Status");
		switch(status) {
			case "3": //已审核
				me.setBtnDisabled("btnUpload", false);
				break;
			default:
				//明细的新增,删除,保存默认禁用
				me.EditPanel.OrderDtlGrid.buttonsDisabled = true;
				me.EditPanel.OrderDtlGrid.setButtonsDisabled(true);
				break;
		}
	},
	/**@description 平台编码同步按钮点击处理方法*/
	onSyncClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
	},
	/**@description 订单上传按钮点击处理方法*/
	onUploadClick: function() {
		var me = this;
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
	}
});