/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.basic.EditPanel', {
	extend: 'Ext.panel.Panel',

	title: '验收信息',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,

	layout: {
		type: 'border'
	},
	OTYPE: "",
	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//明细列表 的货品明细添加,删除,数量改变后,需要重新计算总价格并联动更新表单总价格及表单供货方编辑状态处理
		me.DtlGrid.on({
			nodata: function(p) {}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onFullScreenClick');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.confirm.basic.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			collapsible: false,
			PK: me.PK,
			defaultLoad: false,
			collapsed: false,
			formtype: me.formtype,
			OTYPE: me.OTYPE
		});
		me.DocForm = Ext.create('Shell.class.rea.client.confirm.basic.DocForm', {
			header: true,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 180,
			split: false,
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	nodata: function() {
		var me = this;
		var me = this;
		me.PK = null;
		me.formtype = "show";

		me.DocForm.PK = null;
		me.DocForm.formtype = "show";
		me.DocForm.StatusName = "";
		me.DocForm.isShow();
		me.DocForm.getForm().reset();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = null;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.Status = null;
		me.DtlGrid.store.removeAll();
		me.DtlGrid.disableControl();
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";

		me.DocForm.PK = null;
		me.DocForm.formtype = "add";
		me.DocForm.isAdd();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = null;
		me.DtlGrid.formtype = "add";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.Status = "1";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.enableControl();
	},
	isEdit: function(record, applyGrid) {
		var me = this;
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {
		var me = this;
	},
	loadDtlGrid: function(id) {
		var me = this;
		if(!id)
			var defaultWhere = "";
		if(id) defaultWhere = "bmscensaledtlconfirm.BmsCenSaleDocConfirm.Id=" + id;
		me.DtlGrid.defaultWhere = defaultWhere;
		me.DtlGrid.onSearch();
	},
	onSave: function() {},
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
	/**@description 供货方是可编辑还是只读处理*/
	setReaCompNameReadOnly: function(bo) {
		var me = this;
		var com = me.DocForm.getComponent('BmsCenSaleDocConfirm_ReaCompName');
		if(com)com.setReadOnly(bo);
	}
});