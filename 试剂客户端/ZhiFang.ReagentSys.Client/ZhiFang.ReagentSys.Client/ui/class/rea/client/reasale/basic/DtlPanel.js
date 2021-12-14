/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.basic.DtlPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '供货信息',
	header: false,
	border: false,

	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},
	initComponent: function() {
		var me = this;
		me.addEvents('onLaunchFullScreen', 'onExitFullScreen', 'onAddAfter', 'onDelAfter', 'onEditAfter');
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		var appInfos = [];
		return appInfos;
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		
		me.TabPanel.nodata();
		me.TabPanel.collapse();

		me.DtlGrid.PK = null;		
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.getComponent('buttonsToolbar').getComponent('DocBarCodeType').setValue("");
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.DtlGrid.formtype = formtype;
		me.TabPanel.formtype = formtype;
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.setFormType("add");
		me.DtlGrid.canEdit = true;
		
		me.clearData();
		me.TabPanel.collapse();
	},
	isEdit: function(record) {
		var me = this;
		var id = record.get("ReaBmsCenSaleDoc_Id");
		me.PK = id;
		me.setFormType("edit");
		me.TabPanel.clearData();

		me.DtlGrid.PK = id;
		me.DtlGrid.canEdit = true;
		me.loadDtl(record);
	},
	/**主订单联动明细及表单*/
	isShow: function(record) {
		var me = this;
		var id = record.get("ReaBmsCenSaleDoc_Id");
		me.PK = id;
		me.setFormType("show");

		me.TabPanel.clearData();

		me.DtlGrid.PK = id;
		me.DtlGrid.canEdit = false;
		me.loadDtl(record);
	},
	/**加载供货明细列表*/
	loadDtl: function(record) {
		var me = this;
		var id = record.get("ReaBmsCenSaleDoc_Id");
		if(!id)
			var defaultWhere = "";
		if(id) defaultWhere = "reabmscensaledtl.SaleDocID=" + id;
		me.DtlGrid.defaultWhere = defaultWhere;
		me.DtlGrid.getComponent('buttonsToolbar').getComponent('DocBarCodeType').setValue("");
		me.DtlGrid.onSearch();

	},
	/**加载tab页签内容*/
	loadTabPanel: function(record) {
		var me = this;
		me.TabPanel.expand();
		me.TabPanel.loadData(record);
	},
	/**表单的订货方选择后联动供货明细列表*/
	setReaLabInfo: function(reaLab) {
		var me = this;
		me.DtlGrid.setReaLabInfo(reaLab);
	}
});