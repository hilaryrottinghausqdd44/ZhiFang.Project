/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.basic.add.App', {
	extend: 'Shell.ux.panel.AppPanel',

	/**新增/编辑/查看*/
	formtype: 'show',
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	clearData: function() {
		var me = this;
		me.nodata();
	},
	nodata: function() {
		var me = this;
		me.setFormType("show");
		me.EditPanel.clearData();
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;

		me.DocGrid.formtype = formtype;
		me.EditPanel.setFormType(formtype);
	},
	loadData: function(record) {
		var me = this;
		var status = "" + record.get("ReaBmsCenSaleDoc_Status");
		//暂存,取消审核
		if(status == "1" || status == "5") {
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
	},
	/**@description 供货确认提交*/
	onConfirm: function(record) {
		var me = this;
		me.setFormType("edit");
		var status = "2";
		me.EditPanel.onSave(null, status);
	},
	/**@description 供货确认提交*/
	onUnConfirm: function(record) {
		var me = this;
		me.setFormType("edit");
		var status = "3";
		me.EditPanel.onSave(null, status);
	},
	/**@description 供货审核通过*/
	onCheck: function(record) {
		var me = this;
		me.setFormType("edit");
		var status = "4";
		me.EditPanel.onSave(null, status);
		//审核成功后继续处理生成条码(通过监听EditPanel.onSave公开的createBarCode事件)	
	},
	/**@description 供货审核通过后继续处理生成条码*/
	onCreateBarCode: function(id) {
		var me = this;
		if(!id) return;		
		if (!me.BUTTON_CAN_CLICK) return;
		
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/RS_UDTO_AddCreateBarcodeInfoOfSaleDocId?saleDocId=" + id;
		me.BUTTON_CAN_CLICK = false; //不可点击
		JShell.Server.get(url, function(data) {
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				me.DocGrid.autoSelect = id;
				me.DocGrid.expand();
				me.DocGrid.onSearch();
				JShell.Msg.alert("供货审核成功!", null, 2000);
			} else {
				JShell.Msg.error('供货审核后,处理生成条码失败！' + data.msg);
			}
		});
	},
	/**@description 供货取消审核*/
	onUnCheck: function(record) {
		var me = this;
		me.setFormType("edit");
		var status = "5";
		me.EditPanel.onSave(null, status);
	}
});