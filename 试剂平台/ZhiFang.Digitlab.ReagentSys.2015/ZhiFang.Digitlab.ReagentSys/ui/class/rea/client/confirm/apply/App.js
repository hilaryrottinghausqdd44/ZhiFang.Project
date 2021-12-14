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
				if(isRefresh == true) {
					me.DocGrid.onSearch();
				}
			}
		});
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
			collapsed: false
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
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];

		items.push({
			xtype: 'button',
			iconCls: 'button-import',
			itemId: "btnExtractBms",
			text: '供货单验收',
			tooltip: '平台供货单验收',
			handler: function() {
				me.showExtractOfBms();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-import',
			itemId: "btnExtractRea",
			text: '订单验收',
			tooltip: '订单验收',
			handler: function() {
				me.showExtractOfRea();
			}
		});
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '手工新增',
			tooltip: '手工新增验货单',
			handler: function() {
				me.onAddClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnSave',
			iconCls: 'button-save',
			text: "保存",
			tooltip: "验货单保存提交",
			handler: function(btn, e) {
				me.onSaveClick(btn, e);
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
	isAdd: function() {
		var me = this;
		me.setFormType("add");
		me.EditPanel.isAdd();
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.EditPanel.isShow(record, me.DocGrid);
		me.setBtnDisabled("btnSave", true);
	},
	isEdit: function(record) {
		var me = this;
		me.setFormType("edit");
		me.EditPanel.isEdit(record, me.DocGrid);
		me.setBtnDisabled("btnSave", false);
	},
	loadData: function(record) {
		var me = this;
		//没入库可以修改
		var status = record.get("BmsCenSaleDocConfirm_Status");
		me.setBtnDisabled("btnStorage", true);
		if(status == "4") {
			me.isEdit(record);
		} else {
			me.isShow(record);
			if(status == "1") me.setBtnDisabled("btnStorage", false);
		}
	},
	onAddClick: function() {
		var me = this;
		me.isAdd();
	},
	onSaveClick: function() {
		var me = this;
		if(me.formtype == "edit") {
			me.EditPanel.onSave();
		}
	},
	/**平台供货单导入*/
	showExtractOfBms: function() {
		var me = this;
		var maxWidth = document.body.clientWidth - 60;
		var height = document.body.clientHeight - 80;
		var config = {
			resizable: true,
			SUB_WIN_NO: '1', //内部窗口编号
			width: maxWidth,
			height: height,
			listeners: {
				save: function(p, isClose) {
					if(isClose) p.close();
					me.DocGrid.onSearch();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.confirm.choose.sale.App', config);
		win.show();
	},
	/**实验室订单导入*/
	showExtractOfRea: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.92;
		var height = document.body.clientHeight * 0.86;
		if(maxWidth > 1080) maxWidth = 1080;
		if(height > 680) height = 680;
		var config = {
			resizable: true,
			SUB_WIN_NO: '2',
			width: maxWidth,
			height: height,
			listeners: {
				save: function(p, isClose) {
					if(isClose) p.close();
					me.DocGrid.onSearch();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.confirm.choose.order.App', config);
		win.show();
	},
	onStorageClick: function() {
		var me = this;
	}
});