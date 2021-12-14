/**
 * @description 用户订单(给供应商查看客户端用户已上传的订单信息)
 * @author longfc
 * @version 2018-03-06
 */
Ext.define('Shell.class.rea.client.order.comp.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '用户订单',
	header: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.OrderDtlGrid.on({
			nodata: function(p) {
				me.OrderDtlGrid.enableControl();
			}
		});
		me.DocForm.getComponent('buttonsToolbar').hide();
		me.OrderDtlGrid.on({
			onLaunchFullScreen: function() {
				me.fireEvent('onLaunchFullScreen', me);
			},
			onExitFullScreen: function() {
				me.fireEvent('onExitFullScreen', me);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.formtype = me.formtype || "show";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.OrderDtlGrid = Ext.create('Shell.class.rea.client.order.comp.OrderDtlGrid', {
			header: false,
			itemId: 'OrderDtlGrid',
			region: 'center',
			PK: me.PK,
			formtype: me.formtype,
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.DocForm = Ext.create('Shell.class.rea.client.order.basic.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 240,
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: "comp",
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		var appInfos = [me.OrderDtlGrid, me.DocForm];
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

		me.OrderDtlGrid.PK = null;
		me.OrderDtlGrid.formtype = "show";
		me.OrderDtlGrid.defaultWhere = "";
		me.OrderDtlGrid.ReaCompID = null;
		me.OrderDtlGrid.ReaCompCName = null;
		me.OrderDtlGrid.Status = null;
		me.OrderDtlGrid.store.removeAll();
		me.OrderDtlGrid.disableControl();
		me.OrderDtlGrid.buttonsDisabled = true;
		me.OrderDtlGrid.setButtonsDisabled(true);
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {

		var me = this;
		var id = record.get("ReaBmsCenOrderDoc_Id");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.isShow(id);

		me.OrderDtlGrid.PK = id;
		me.OrderDtlGrid.formtype = "show";
		me.OrderDtlGrid.Status = record.get("ReaBmsCenOrderDoc_Status");
		me.OrderDtlGrid.ReaCompID = record.get("ReaBmsCenOrderDoc_ReaCompID");
		me.OrderDtlGrid.ReaCompCName = record.get("ReaBmsCenOrderDoc_ReaCompanyName");
		var defaultWhere = "";

		//数据标志为不等于未提取,不等于取消上传
		if(me.PK) defaultWhere = "reabmscenorderdtl.OrderDocID=" + me.PK;
		me.OrderDtlGrid.defaultWhere = defaultWhere;
		me.OrderDtlGrid.onSearch();
		me.OrderDtlGrid.buttonsDisabled = true;
	}
});