/**
 * 客户端订单验收
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
		me.DocGrid.on({
			onAddClick: function() {
				//me.showExtractOfBms();
				me.isAdd();
			},
			onSaveClick: function() {
				me.onSave();
			},
			onStorageClick: function() {
				me.onStorage();
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
		me.DocGrid = Ext.create('Shell.class.rea.client.confirm.reasale.DocGrid', {
			header: false,
			itemId: 'DocGrid',
			region: 'west',
			width: 385,
			split: true,
			collapsible: true,
			collapsed: false
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
	showAddPanel: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
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
		config.formtype = 'add';
		var win = JShell.Win.open('Shell.class.rea.client.confirm.reasale.add.AddPanel', config);
		win.show();
	},
	/**供货单导入*/
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
	onFullScreen: function() {
		var me = this;
	},
	onSave: function() {
		var me = this;
		if(me.formtype == "edit") {
			me.EditPanel.onSave();
		}
	},
	onStorage: function() {
		var me = this;
	}
});