/**
 * 入库
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.stock.basic.App', {
	extend: 'Ext.panel.Panel',

	title: '入库',
	header: false,
	border: false,

	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},
	OTYPE: "confirm",
	/**@description 新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.OTYPE = me.OTYPE || "";

		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.stock.basic.DocGrid', {
			header: false,
			title: '入库主单',
			itemId: 'DocGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: false,
			collapsed: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.stock.basic.EditPanel', {
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
		me.isShow(record);
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
	}
});