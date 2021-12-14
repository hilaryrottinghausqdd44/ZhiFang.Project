/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.apply.EditPanel', {
	extend: 'Shell.class.rea.client.confirm.basic.EditPanel',

	/**录入:apply/审核:check*/
	OTYPE: "apply",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//明细列表 的货品明细添加,删除,数量改变后,需要重新计算总价格并联动更新表单总价格及表单供货方编辑状态处理
		me.DtlGrid.on({
			nodata: function(p) {

			}
		});
		//供应商选择改变后
		me.DocForm.on({
			reacompcheck: function(v, record) {
				var ReaCompID = record ? record.get('ReaCenOrg_Id') : '';
				me.DtlGrid.ReaCompID = ReaCompID;
			},
			//表单供货方编辑状态处理
			isEditAfter: function(form) {
				var bo = true;
				if(me.DtlGrid.store.getCount() <= 0) bo = false;
				me.setReaCompNameReadOnly(bo);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.confirm.apply.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			PK: me.PK,
			defaultLoad: false,
			formtype: me.formtype,
			OTYPE: me.OTYPE,
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.DocForm = Ext.create('Shell.class.rea.client.confirm.apply.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 180,
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: me.OTYPE,
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	isEdit: function(record, applyGrid) {
		var me = this;
		var id = record.get("BmsCenSaleDocConfirm_Id");
		me.PK = id;
		me.formtype = "edit";

		me.DocForm.formtype = "edit";
		me.DocForm.PK = id;
		var statusName = "",
			color = "#1c8f36";
		if(applyGrid.StatusEnum != null) statusName = applyGrid.StatusEnum[record.get("BmsCenSaleDocConfirm_Status")];
		if(applyGrid.StatusBGColorEnum != null) color = applyGrid.StatusBGColorEnum[record.get("BmsCenSaleDocConfirm_Status")];
		statusName = '<b style="color:' + color + ';">' + statusName + '</b> ';
		me.DocForm.StatusName = statusName;
		me.DocForm.isEdit(id);
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "edit";
		me.DtlGrid.Status = record.get("BmsCenSaleDocConfirm_Status");
		me.DtlGrid.buttonsDisabled = false;
		me.loadDtlGrid(me.PK);		
		me.DtlGrid.setButtonsDisabled(false);
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {
		var me = this;
		var id = record.get("BmsCenSaleDocConfirm_Id");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.formtype = "show";
		var statusName = "",
			color = "#1c8f36";
		if(applyGrid.StatusEnum != null) statusName = applyGrid.StatusEnum[record.get("BmsCenSaleDocConfirm_Status")];
		if(applyGrid.StatusBGColorEnum != null) color = applyGrid.StatusBGColorEnum[record.get("BmsCenSaleDocConfirm_Status")];
		statusName = '<b style="color:' + color + ';">' + statusName + '</b> ';
		me.DocForm.StatusName = statusName;
		me.DocForm.isShow(id);

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.Status = record.get("BmsCenSaleDocConfirm_Status");

		me.DtlGrid.buttonsDisabled = true;
		me.loadDtlGrid(me.PK);
		me.DtlGrid.setButtonsDisabled(true);
	}
});