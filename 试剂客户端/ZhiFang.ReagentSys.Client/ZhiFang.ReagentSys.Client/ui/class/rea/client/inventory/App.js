/**
 * 盘库管理
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.App', {
	//extend: 'Shell.ux.panel.AppPanel',
	extend: 'Ext.panel.Panel',

	layout: 'border',
	/**盘库时实盘数是否取库存数 1:是;2:否;*/
	isTakenFromQty: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.inventory.DocGrid', {
			header: false,
			itemId: 'DocGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false
		});
		me.TabPanel = Ext.create('Shell.class.rea.client.inventory.TabPanel', {
			header: false,
			itemId: 'TabPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DocGrid, me.TabPanel];
		return appInfos;
	},

	onListeners: function() {
		var me = this;

		me.DocGrid.on({
			onAddClick: function() {
				me.isAdd();
			},
			onPrint: function() {
				me.onPrintClick();
			},
			select: function(RowModel, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
		me.TabPanel.on({
			save: function(p, id) {
				//id为盘库单Id
				me.DocGrid.autoSelect = id;
				me.DocGrid.expand();
				JShell.Action.delay(function() {
					me.DocGrid.onSearch();
				}, null, 200);
			}
		});
		//系统运行参数"盘库时实盘数是否取库存数"
		var value1 = JcallShell.REA.RunParams.Lists.InventoryIsTakenFromQty.Value;
		if (!value1) {
			JShell.REA.RunParams.getRunParamsValue("InventoryIsTakenFromQty", false, function(result) {
				var value1 = "" + JcallShell.REA.RunParams.Lists.InventoryIsTakenFromQty.Value;
				if (value1 == 1 || value1 == "1" || value1 == "true") {
					me.isTakenFromQty = true;
				}
			});
		} else {
			if (value1 == "1" || value1 == "true") {
				me.isTakenFromQty = true;
			}
		}
	},
	clearData: function() {
		var me = this;
	},
	nodata: function() {
		var me = this;
		me.TabPanel.nodata();
		me.clearData();
	},
	loadData: function(record) {
		var me = this;
		me.TabPanel.loadData(record);
	},
	isAdd: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.94;

		JShell.Win.open('Shell.class.rea.client.inventory.add.Panel', {
			resizable: true,
			width: maxWidth,
			height: height,
			isTakenFromQty: me.isTakenFromQty,
			listeners: {
				save: function(p, id) {
					p.close();
					//id为盘库单Id
					me.DocGrid.autoSelect = id;
					//me.DocGrid.expand();
					JShell.Action.delay(function() {
						me.DocGrid.onSearch();
					}, null, 200);
				}
			}
		}).show();
	},
	getCurOrderBy: function() {
		var me = this;
		return me.TabPanel.getCurOrderBy();
	},
	/**@description 预览PDF盘库信息*/
	onPrintClick: function() {
		var me = this;
		var records = me.DocGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		if (!me.DocGrid.pdfFrx) {
			JShell.Msg.error("请先选择清单模板后再操作!");
			return;
		}
		//var frx = me.getComponent("buttonsToolbar4").getComponent("cboTemplate").getValue();
		var id = records[0].get("ReaBmsCheckDoc_Id");
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_GetReaBmsCheckDocAndDtlOfPdf");
		url += '?operateType=1&id=' + id;
		var sort = me.getCurOrderBy();
		if (!sort) {
			sort = [{
				property: 'ReaBmsCheckDtl_StorageID',
				direction: 'ASC'
			}, {
				property: 'ReaBmsCheckDtl_PlaceID',
				direction: 'ASC'
			}, {
				property: 'ReaBmsCheckDtl_ReaGoodsNo',
				direction: 'ASC'
			}, {
				property: 'ReaGoods_DispOrder',
				direction: 'ASC'
			}, {
				property: 'ReaBmsCheckDtl_LotNo',
				direction: 'ASC'
			}];
		}
		if (sort) {
			sort = JShell.JSON.encode(sort);
			url += '&sort=' + sort;
		}
		if (me.DocGrid.pdfFrx) {
			url += '&frx=' + JShell.String.encode(me.DocGrid.pdfFrx);
		}
		window.open(url);
	}
});
