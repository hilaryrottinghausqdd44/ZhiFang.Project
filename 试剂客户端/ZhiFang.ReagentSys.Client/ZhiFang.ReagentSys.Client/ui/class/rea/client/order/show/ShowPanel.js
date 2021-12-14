/**
 * @description 给医院采购中心提供订单查看功能
 * @author liangyl
 * @version 2018-10-22
 */
Ext.define('Shell.class.rea.client.order.show.ShowPanel', {
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
		me.OrderDtlGrid = Ext.create('Shell.class.rea.client.order.show.OrderDtlGrid', {
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
	clearData: function() {
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
		me.OrderDtlGrid.clearData();
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