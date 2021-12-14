/**
 * 客户端订单验收
 * @author longfc
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.confirm.reasale.EditPanel', {
	extend: 'Shell.class.rea.client.confirm.basic.EditPanel',

	OTYPE: "reasale",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DtlGrid.on({
			onLaunchFullScreen: function() {
				me.fireEvent('onLaunchFullScreen', me);
			},
			onExitFullScreen: function() {
				me.fireEvent('onExitFullScreen', me);
			},
			nodata: function(p) {

			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save','onLaunchFullScreen','onExitFullScreen');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.confirm.reasale.DtlGrid', {
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
		me.DocForm = Ext.create('Shell.class.rea.client.confirm.reasale.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 165,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false,
			animate: false,
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	isEdit: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsCenSaleDocConfirm_Id");
		var status=record.get("ReaBmsCenSaleDocConfirm_Status");
		me.PK = id;
		me.formtype = "edit";

		me.DocForm.PK = id;
		me.DocForm.isEdit(id);
		me.DocForm.getComponent('buttonsToolbar').hide();
		me.setReaCompNameReadOnly(true);
		
		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "edit";
		me.DtlGrid.Status = status;
		me.DtlGrid.buttonsDisabled = false;
		me.loadDtlGrid(me.PK);
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsCenSaleDocConfirm_Id");
		var status=record.get("ReaBmsCenSaleDocConfirm_Status");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.isShow(id);

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.Status =status;
		me.DtlGrid.buttonsDisabled = true;
		me.loadDtlGrid(me.PK);
	}
});