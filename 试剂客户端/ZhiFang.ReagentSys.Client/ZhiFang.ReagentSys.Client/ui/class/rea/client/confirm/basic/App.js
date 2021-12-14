/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.basic.App', {
	extend: 'Ext.panel.Panel',

	title: '验货单信息',
	header: false,
	border: false,

	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},
	OTYPE: "",
	/**@description 新增/编辑/查看*/
	formtype: 'show',
	/**获取入库明细数据路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtlByHQL',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.OTYPE = me.OTYPE || "";
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.confirm.basic.DocGrid', {
			header: true,
			title: '供货单信息',
			itemId: 'DocGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: false,
			collapsed: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.confirm.basic.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			collapsible: false,
			collapsed: false,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.DocGrid, me.EditPanel];
		return appInfos;
	},
	/**@description 创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	clearData: function() {
		var me = this;
	},
	nodata: function(record) {
		var me = this;
		me.setFormType("show");
		me.EditPanel.clearData();
		me.clearData();
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.EditPanel.formtype = formtype;
		me.EditPanel.DocForm.formtype = formtype;
		me.EditPanel.DtlGrid.formtype = formtype;
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.EditPanel.isShow(record, me.DocGrid);
	},
	isEdit: function(record) {
		var me = this;
		me.setFormType("edit");
		me.EditPanel.isEdit(record, me.DocGrid);
	},
	loadData: function(record) {
		var me = this;
		var status = record.get("ReaBmsCenSaleDocConfirm_Status");
		me.DocGrid.setBtnDisabled("btnStorage", true);
		if(status == "0") {
			me.isEdit(record);
		} else {
			me.isShow(record);
			//已验收,部分入库
			if(status == "1" || status == "2")
				me.DocGrid.setBtnDisabled("btnStorage", false);
		}
	},
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	},
	/**@description 弹出入库信息*/
	showStockPanel: function(confirmID) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: true,
			BmsCenSaleDocConfirmID: confirmID,
			SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				close: function(p, eOpts) {
					var dtlWin = Ext.WindowManager.get(me.OTYPE);
					if(dtlWin) dtlWin.close();
				},
				save: function(p, id, isPrint) {
					p.close();
					var dtlWin = Ext.WindowManager.get(me.OTYPE);
					if(dtlWin) dtlWin.close();
					me.DocGrid.onSearch();
					if(isPrint){//
						me.onShowPrintPanel(id);
					}
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.stock.confirm.App', config);
		win.show();
	},
	onShowPrintPanel: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: true,
			PK: id,
			//SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				beforeclose: function(p, eOpts) {
					var plugin = p.getPlugin(p.cellpluginId);
					if(plugin) {
						plugin.cancelEdit();
					}
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.printbarcode.indoc.Grid', config);
		win.show();
	}
	
});