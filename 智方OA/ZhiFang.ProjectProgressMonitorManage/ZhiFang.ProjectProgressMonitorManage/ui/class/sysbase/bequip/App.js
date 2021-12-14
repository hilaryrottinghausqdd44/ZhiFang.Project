/**
 * 仪器管理
 * @author longfc
 * @version 2015-09-29
 */
Ext.define('Shell.class.sysbase.bequip.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '仪器管理',
	/*传入的字典类型的仪器厂商品牌ID**/
	ETYPEID: '5724611581318422977',
	/*传入字典类型的仪器分类**/
	EBRADID: '4777630349498328266',

	/**当前联动的仪器类型ID*/
	EquipTypeId: '',
	EquipTypeCName: '',
	/**当前联动的仪器厂商品牌ID*/
	EquipFactoryBrandId: "",
	EquipFactoryBrandCName: '',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//按仪器类型联动
		me.TabPanel.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				me.Grid.clearData();
				switch(newCard.itemId) {
					case "EquipType":
						if(me.TabPanel.EquipType.getStore().count() > 0) {
							var records = me.TabPanel.EquipType.getSelectionModel().getSelection();
							if(!records || records.length != 1) {
								me.TabPanel.EquipType.getSelectionModel().select(0);
								records = me.TabPanel.EquipType.getSelectionModel().getSelection();
							}
							if(!records || records.length != 1) {
								me.TabPanel.EquipType.onSearch();
							} else {
								var record = records[0];
								me.loadEquipGrid(record);
							}
						} else {
							me.TabPanel.EquipType.onSearch();
						}
						break;
					case "EquipFactoryBrand":
						if(me.TabPanel.EquipFactoryBrand.getStore().count() > 0) {
							var records = me.TabPanel.EquipFactoryBrand.getSelectionModel().getSelection();
							if(!records || records.length != 1) {
								me.TabPanel.EquipFactoryBrand.getSelectionModel().select(0);
								records = me.TabPanel.EquipFactoryBrand.getSelectionModel().getSelection();
							}
							if(!records || records.length != 1) {
								me.TabPanel.EquipFactoryBrand.onSearch();
							} else {
								var record = records[0];
								me.loadEquipFactoryBrandGrid(record);
							}
						}
						break;
					default:
						break;
				}
			}
		});

		//按仪器类型联动
		me.TabPanel.EquipType.on({
			itemclick: function(v, record) {
				me.loadEquipGrid(record);
			},
			select: function(RowModel, record) {
				me.loadEquipGrid(record);
			},
			nodata: function(p) {
				me.EquipTypeId = "";
				me.EquipTypeCName = "";
				me.Grid.clearData();
			}
		});
		me.TabPanel.EquipFactoryBrand.on({
			itemclick: function(v, record) {
				me.loadEquipFactoryBrandGrid(record);
			},
			select: function(RowModel, record) {
				me.loadEquipFactoryBrandGrid(record);
			},
			nodata: function(p) {
				me.EquipFactoryBrandId = "";
				me.EquipFactoryBrandCName = "";
				me.Grid.clearData();
			}
		});
		me.Grid.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.openShowTabPanel(record);
			},
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);

				}, null, 500);
			},
			onAddClick: function(p) {
				me.openAddTabPanel(null);
			},
			onEditClick: function(p, recerd) {
				me.openAddTabPanel(recerd);
			},
			onOperationRecordClick: function(grid, rec, rowIndex, colIndex) {
				me.openOperationGrid(rec);
			},
			onHistoryVersionClick: function(grid, rec, rowIndex, colIndex) {
				me.openHistoryVersionGrid(rec);
			},
			onShowClick: function(grid, rec, rowIndex, colIndex) {
				me.openShowTabPanel(rec);
			},
			onInteractionClick: function(grid, rec, rowIndex, colIndex) {
				me.showInteractionById(rec);
			},
			nodata: function(p) {}
		});
	},
	loadEquipFactoryBrandGrid: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			me.EquipFactoryBrandId = record.get(me.TabPanel.EquipFactoryBrand.PKField);
			me.EquipFactoryBrandCName = record.get("PDict_CName");
			me.loadGridByBrandID(me.EquipFactoryBrandId);
		}, null, 500);
	},
	loadEquipGrid: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			me.EquipTypeId = record.get(me.TabPanel.EquipType.PKField);
			me.EquipTypeCName = record.get("PDict_CName");
			me.loadGridByEquipTypeID(me.EquipTypeId);
		}, null, 500);
	},
	/**根据ID查看交流*/
	showInteractionById: function(record) {
		var me = this;
		var id = record.get('BEquip_Id');
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.sysbase.scinteraction.App', {
			PK: id,
			height: height,
			width: maxWidth
		}).show();
	},
	/**打开历史版本列表*/
	openHistoryVersionGrid: function(rec) {
		var me = this;
		var id = "";
		if(rec != null)
			id = rec.get(me.Grid.PKField);
		var config = {
			showSuccessInfo: false,
			resizable: false,
			hasButtontoolbar: false,
			width: 660,
			PK: id
		};
		var win = JShell.Win.open('Shell.class.sysbase.bequip.show.PGMProgramGrid', config).show();
		//win.loadByEEquipId(id);
	},
	/**打开操作记录列表*/
	openOperationGrid: function(rec) {
		var me = this;
		var id = "";
		if(rec != null)
			id = rec.get(me.Grid.PKField);
		var config = {
			showSuccessInfo: false,
			resizable: false,
			hasButtontoolbar: false,
			PK: id
		};
		var win = JShell.Win.open('Shell.class.oa.sc.operation.Grid', config).show();
		//win.loadByBobjectID(id);
	},
	initComponent: function() {
		var me = this;
		/*仪器厂商品牌ID**/
		me.ETYPEID = me.ETYPEID || '5724611581318422977';
		/*仪器分类**/
		me.EBRADID = me.EBRADID || '4777630349498328266';
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.TabPanel = Ext.create('Shell.class.sysbase.bequip.TabPanel', {
			region: 'west',
			header: false,
			split: true,
			/*仪器厂商品牌ID**/
			ETYPEID: me.ETYPEID,
			/*仪器分类**/
			EBRADID: me.EBRADID,
			collapsible: true,
			itemId: 'TabPanel'
		});
		me.Grid = Ext.create('Shell.class.sysbase.bequip.Grid', {
			title: '仪器信息',
			region: 'center',
			header: true,
			/*仪器厂商品牌ID**/
			ETYPEID: me.ETYPEID,
			/*仪器分类**/
			EBRADID: me.EBRADID,
			itemId: 'Grid'
		});

		return [me.TabPanel, me.Grid];
	},
	openShowTabPanel: function(record) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight - 10;
		var id = record.get("BEquip_Id");
		var config = {
			showSuccessInfo: false,
			height: height,
			width: maxWidth,
			zindex: 10,
			zIndex: 10,
			resizable: false,
			title: "仪器详情信息",
			PK: id,
			formtype: 'show'
		};
		var form = 'Shell.class.sysbase.bequip.show.BEquipDetailedPanel';
		var win = JShell.Win.open(form, config).show();
		//win.isShow(id);
	},
	/*
	 * 按仪器厂商品牌ID获取仪器信息
	 **/
	loadGridByBrandID: function(brandID) {
		var me = this;
		if(brandID == "-1") {
			me.Grid.defaultWhere = "";
		} else {
			me.Grid.defaultWhere = 'bequip.EquipFactoryBrand.Id=' + brandID;
		}
		me.Grid.onSearch();
	},
	/*
	 * 按仪器类型获取仪器信息
	 **/
	loadGridByEquipTypeID: function(equipTypeID) {
		var me = this;
		if(equipTypeID == "-1") {
			me.Grid.defaultWhere = "";
		} else {
			me.Grid.defaultWhere = 'bequip.EquipType.Id=' + equipTypeID;
		}
		me.Grid.onSearch();
	},
	/*
	 * 打开新增或编辑TabPanel
	 */
	openAddTabPanel: function(record) {
		var me = this;
		var Grid = me.getComponent('Grid');
		var id = "";
		if(record != null) {
			id = record.get('BEquip_Id');
		}
		var maxWidth = document.body.clientWidth * 0.80;
		var height = document.body.clientHeight - 10;

		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: '1',
			height: height,
			width: maxWidth,
			zindex: 10,
			zIndex: 10,
			resizable: false,
			hasReset: Grid.hasReset,
			title: Grid.title || "编辑仪器信息",
			formtype: 'add',
			/**字典类型里的仪器类型ID*/
			EquipTypeId: me.EquipTypeId,
			EquipTypeCName: me.EquipTypeCName,
			/**字典类型里的仪器厂商品牌ID*/
			EquipFactoryBrandId: me.EquipFactoryBrandId,
			EquipFactoryBrandCName: me.EquipFactoryBrandCName,
			/*仪器厂商品牌ID**/
			ETYPEID: me.ETYPEID,
			/*仪器分类**/
			EBRADID: me.EBRADID,
			listeners: {
				save: function(win) {
					Grid.onSearch();
					//win.close();
				}
			}
		};
		var form = 'Shell.class.sysbase.bequip.AddTabPanel';
		if(id && id != null) {
			config.formtype = 'edit';
			config.PK = id;
			title: me.title || "编辑仪器信息";
		}
		JShell.Win.open(form, config).show();
	}
});