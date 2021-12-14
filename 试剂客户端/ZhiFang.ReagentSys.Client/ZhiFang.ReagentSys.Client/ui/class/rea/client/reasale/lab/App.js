/**
 * 实验室(订货方)供货管理
 * @author longfc
 * @version 2018-05-08
 */
Ext.define('Shell.class.rea.client.reasale.lab.App', {
	extend: 'Shell.class.rea.client.reasale.basic.add.App',

	OTYPE: "lab",
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DocGrid.on({
			onAddClick: function() {
				me.isAdd();
			},
			onAddClick: function() {
				me.isAdd();
			},
			onConfirmClick: function(grid, record) {
				me.onConfirm(record);
			},
			onUnConfirmClick: function(grid, record) {
				me.onUnConfirm(record);
			},
			onCheckClick: function(grid, record) {
				me.onCheck(record);
			},
			onUnCheckClick: function(grid, record) {
				me.onUnCheck(record);
			},
			onCreateBarCodeClick: function(p, id) {
				me.onCreateBarCode(id);
			},
			select: function(RowModel, record) {
				me.loadData(record);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
		me.EditPanel.on({
			save: function(p, id) {
				me.DocGrid.autoSelect = id;
				me.DocGrid.expand();
				JShell.Action.delay(function() {
					me.DocGrid.onSearch();
				}, null, 200);
			},
			createBarCode: function(p, id) {
				me.onCreateBarCode(id);
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
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.reasale.lab.DocGrid', {
			header: false,
			itemId: 'DocGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.reasale.lab.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DocGrid, me.EditPanel];
		return appInfos;
	},
	loadData: function(record) {
		var me = this;
		var status = "" + record.get("ReaBmsCenSaleDoc_Status");
		//暂存,取消提交,取消审核
		if(status == "1" || status == "3"|| status == "5") {
			me.isEdit(record);
		} else {
			me.isShow(record);
		}
	},
	isAdd: function() {
		var me = this;
		me.DocGrid.collapse();
		me.setFormType("add");
		me.EditPanel.isAdd();
	},
	isEdit: function(record) {
		var me = this;
		me.setFormType("edit");
		me.EditPanel.isEdit(record, me.DocGrid);
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.EditPanel.isShow(record, me.DocGrid);
	}
});