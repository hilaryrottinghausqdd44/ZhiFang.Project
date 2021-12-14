/**
 * 客户端库存初始化(手工入库)
 * @author longfc
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.stock.manualinput.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '手工入库',
	OTYPE: "manualinput",
	/**新增/编辑/查看*/
	formtype: 'show',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DocGrid.on({
			onAddClick: function() {
				me.isAdd();
			},
			onInContinueClick: function(grid, record) {
				me.showAddPanel(record);
			},
			select: function(RowModel, record) {
				me.isShow(record);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
		me.ShowPanel.on({
			save: function(p, id) {
				me.DocGrid.autoSelect = id;
				me.DocGrid.onSearch();
			},
			onLaunchFullScreen: function() {
				me.DocGrid.collapse();
			},
			onExitFullScreen: function() {
				me.DocGrid.expand();
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
		me.DocGrid = Ext.create('Shell.class.rea.client.stock.manualinput.DocGrid', {
			header: false,
			itemId: 'DocGrid',
			region: 'west',
			width: 360,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false
		});
		me.ShowPanel = Ext.create('Shell.class.rea.client.stock.manualinput.ShowPanel', {
			header: false,
			itemId: 'ShowPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.DocGrid, me.ShowPanel];
		return appInfos;
	},
	clearData: function() {
		var me = this;
	},
	nodata: function(record) {
		var me = this;
		me.setFormType("show");
		me.ShowPanel.clearData();
		me.clearData();
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.ShowPanel.formtype = formtype;
		me.ShowPanel.DocForm.formtype = formtype;
		me.ShowPanel.DtlPanel.formtype = formtype;
	},
	isAdd: function() {
		var me = this;
		me.setFormType("add");
		me.showAddPanel(null);
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.ShowPanel.isShow(record, me.DocGrid);
	},
	/**@description 弹出录入信息*/
	showAddPanel: function(record) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var id = null;
		if(record) id = record.get(me.DocGrid.PKField);

		var config = {
			resizable: true,
			PK: null,
			//SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				beforeclose: function(p, eOpts) {
					p.cancelEdit();
				},
				close: function(p, eOpts) {
					p.closeDtlPanel();
				},
				save: function(p, pk, isPrint) {
					id = pk;
					p.close();
					p.closeDtlPanel();
					me.DocGrid.onSearch();
					if(isPrint){
						me.DocGrid.onShowPrintPanelById(id);
					}
				}
			}
		};
		if(!id)
			config.formtype = 'add';
		else {
			config.formtype = 'edit';
			config.PK = id;
		}
		var win = JShell.Win.open('Shell.class.rea.client.stock.manualinput.add.App', config);
		win.show();
		if(id)
			win.isEdit(id);
		else
			win.isAdd();
	}
});