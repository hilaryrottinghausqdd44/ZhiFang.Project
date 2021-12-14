/**
 * 客户端供货验收
 * @author longfc
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.confirm.reasale.App', {
	extend: 'Shell.class.rea.client.confirm.basic.App',

	OTYPE: "reasale",
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
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.confirm.reasale.DocGrid', {
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
		me.EditPanel = Ext.create('Shell.class.rea.client.confirm.reasale.EditPanel', {
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
			onLaunchFullScreen: function() {
				me.DocGrid.collapse();
			},
			onExitFullScreen: function() {
				me.DocGrid.expand();
			},
			save: function(p, isRefresh) {
				if (isRefresh == true) {
					me.DocGrid.onSearch();
				}
			}
		});
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
		if (record) id = record.get(me.DocGrid.PKField);

		var config = {
			resizable: true,
			SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				beforeclose: function(p, eOpts) {
					p.cancelEdit();
				},
				close: function(p, eOpts) {
					var dtlWin = Ext.WindowManager.get(me.OTYPE);
					if (dtlWin) dtlWin.close();
				},
				save: function(p, confirmId, isStorage) {
					var dtlWin = Ext.WindowManager.get(me.OTYPE);
					if (dtlWin) dtlWin.close();
					p.close();
					//是否显示验收入库
					if (isStorage) {
						me.showStockPanel(confirmId);
					} else {
						me.DocGrid.autoSelect = confirmId;
						me.DocGrid.onSearch();
					}
				}
			}
		};
		if (!id)
			config.formtype = 'add';
		else {
			config.formtype = 'edit';
			config.PK = id;
			config.SaleDocID = record.get("ReaBmsCenSaleDocConfirm_SaleDocID");
			config.ReaCompID = record.get("ReaBmsCenSaleDocConfirm_ReaCompID");
		}
		var win = JShell.Win.open('Shell.class.rea.client.confirm.reasale.add.AddPanel', config);
		win.show();

		if (id)
			win.isEdit(id);
		else
			win.isAdd();
	},
	onSave: function() {
		var me = this;
		if (me.formtype == "edit") {
			me.EditPanel.onSave();
		}
	},
	onStorage: function(record) {
		var me = this;
		var id = null;
		if (record) id = record.get(me.DocGrid.PKField);
		me.showStockPanel(id);
	}
});
