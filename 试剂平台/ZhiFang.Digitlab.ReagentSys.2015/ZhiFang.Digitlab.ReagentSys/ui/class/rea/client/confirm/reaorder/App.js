/**
 * 客户端订单验收
 * @author longfc
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.confirm.reaorder.App', {
	extend: 'Shell.class.rea.client.confirm.basic.App',

	OTYPE: "reaorder",
	/**新增/编辑/查看*/
	formtype: 'show',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DocGrid.on({
			onAddClick: function() {
				me.isAdd();
			},
			onContinueToAcceptClick: function(grid, record) {
				me.showAddPanel(record);
			},
			onStorageClick: function(grid, record) {
				me.onStorage(record);
			},
			select: function(RowModel, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
		me.EditPanel.on({
			onFullScreenClick: function() {
				me.onFullScreen();
			},
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
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.confirm.reaorder.DocGrid', {
			header: false,
			itemId: 'DocGrid',
			region: 'west',
			width: 385,
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.confirm.reaorder.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.DocGrid, me.EditPanel];
		return appInfos;
	},
	isAdd: function() {
		var me = this;
		me.setFormType("add");
		me.EditPanel.isAdd();
		me.showAddPanel();
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.EditPanel.isShow(record, me.DocGrid);
		me.DocGrid.setBtnDisabled("btnSave", true);
	},
	isEdit: function(record) {
		var me = this;
		me.setFormType("edit");
		me.EditPanel.isEdit(record, me.DocGrid);
		me.DocGrid.setBtnDisabled("btnSave", false);
	},
	showAddPanel: function(record) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var id = null;
		if(record) id = record.get(me.DocGrid.PKField);

		var config = {
			resizable: true,
			SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				close: function(p, eOpts) {
					var dtlWin = Ext.WindowManager.get(me.OTYPE);
					if(dtlWin) dtlWin.close();
				},
				save: function(p) {
					p.close();
					var dtlWin = Ext.WindowManager.get(me.OTYPE);
					if(dtlWin) dtlWin.close();
					me.DocGrid.onSearch();
				}
			}
		};
		if(!id)
			config.formtype = 'add';
		else {
			config.formtype = 'edit';
			config.PK = id;
			config.DocOrderId = record.get("BmsCenSaleDocConfirm_BmsCenOrderDoc_Id");
			config.ReaCompID = record.get("BmsCenSaleDocConfirm_ReaCompID");
		}
		var win = JShell.Win.open('Shell.class.rea.client.confirm.reaorder.add.AddPanel', config);
		win.show();

		if(id)
			win.isEdit(id);
		else
			win.isAdd();
	},
	/**实验室订单验收*/
	showExtractOfRea: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;
		if(maxWidth < 1080) maxWidth = 1080;
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
	onFullScreen: function() {
		var me = this;
	},
	onSave: function() {
		var me = this;
		if(me.formtype == "edit") {
			me.EditPanel.onSave();
		}
	},
	onStorage: function(record) {
		var me = this;
		var id = null;
		if(record) id = record.get(me.DocGrid.PKField);
		me.showStockPanel(id);
	}
});