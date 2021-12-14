/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.choose.sale.EditPanel', {
	extend: 'Ext.panel.Panel',

	title: '供货单验收',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,

	layout: {
		type: 'border'
	},
	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//明细列表 的货品明细添加,删除,数量改变后,需要重新计算总价格并联动更新表单总价格及表单供货方编辑状态处理
		me.DtlGrid.on({
			nodata: function(p) {
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
		me.DtlGrid = Ext.create('Shell.class.rea.client.confirm.choose.sale.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			collapsible: false,
			PK: me.PK,
			defaultLoad: false,
			collapsed: false,
			formtype: me.formtype
		});
		me.DocForm = Ext.create('Shell.class.rea.client.confirm.choose.sale.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 160,
			split: false,
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype
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
		me.DtlGrid.buttonsDisabled = true;
		me.DtlGrid.setButtonsDisabled(true);
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isEdit: function(record, applyGrid) {
		var me = this;
		var id = record.get("BmsCenSaleDoc_Id");
		me.PK = id;
		me.formtype = "edit";

		me.DocForm.formtype = "edit";
		me.DocForm.PK = id;
		var statusName = "",
			color = "#1c8f36";
		if(applyGrid.StatusEnum != null) statusName = applyGrid.StatusEnum[record.get("BmsCenSaleDoc_Status")];
		if(applyGrid.StatusBGColorEnum != null) color = applyGrid.StatusBGColorEnum[record.get("BmsCenSaleDoc_Status")];
		statusName = '<b style="color:' + color + ';">' + statusName + '</b> ';
		me.DocForm.StatusName = statusName;
		me.DocForm.isEdit(id);
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "edit";
		me.DtlGrid.Status = record.get("BmsCenSaleDoc_Status");
		var defaultWhere = "";
		if(me.PK) defaultWhere = "bmscensaledtl.BmsCenSaleDoc.Id=" + me.PK;
		me.DtlGrid.defaultWhere = defaultWhere;
		me.DtlGrid.onSearch();
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {

		var me = this;
		var id = record.get("BmsCenSaleDoc_Id");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.formtype = "show";
		var statusName = "",
			color = "#1c8f36";
		if(applyGrid.StatusEnum != null) statusName = applyGrid.StatusEnum[record.get("BmsCenSaleDoc_Status")];
		if(applyGrid.StatusBGColorEnum != null) color = applyGrid.StatusBGColorEnum[record.get("BmsCenSaleDoc_Status")];
		statusName = '<b style="color:' + color + ';">' + statusName + '</b> ';
		me.DocForm.StatusName = statusName;
		me.DocForm.isShow(id);

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.Status = record.get("BmsCenSaleDoc_Status");

		var defaultWhere = "";
		if(me.PK) defaultWhere = "bmscensaledtl.BmsCenSaleDoc.Id=" + me.PK;
		me.DtlGrid.defaultWhere = defaultWhere;
		me.DtlGrid.onSearch();
		me.DtlGrid.buttonsDisabled = true;
	},
	/**@description 获取明细的保存提交数据*/
	getSaveParams: function() {
		var me = this;
		return params;
	}
});