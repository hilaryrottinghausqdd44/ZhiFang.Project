/**
 * 入库确认
 * @author longfc
 * @version 2019-03-08
 */
Ext.define('Shell.class.rea.client.stock.manage.confirm.DocForm', {
	extend: 'Shell.class.rea.client.stock.manualinput.basic.DocForm',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '入库信息',
	height: 165,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	formtype: "edit",
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/RS_UDTO_UpdateReaBmsInDocAndInDtlListByInterface',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	setTotalPrice: function(totalPrice) {
		var me = this;
		var TotalPrice = me.getComponent('ReaBmsInDoc_TotalPrice');
		TotalPrice.setValue(totalPrice);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			Status: values.ReaBmsInDoc_Status,
			InDocNo: values.ReaBmsInDoc_InDocNo,
			Carrier: values.ReaBmsInDoc_Carrier,
			InType: values.ReaBmsInDoc_InType,
			CreaterName: values.ReaBmsInDoc_CreaterName,
			InvoiceNo: values.ReaBmsInDoc_InvoiceNo,
			SaleDocNo: values.ReaBmsInDoc_SaleDocNo,
			OtherDocNo: values.ReaBmsInDoc_OtherDocNo,
			ZX1: values.ReaBmsInDoc_ZX1,
			ZX2: values.ReaBmsInDoc_ZX2,
			ZX3: values.ReaBmsInDoc_ZX3
		};
		if(values.ReaBmsInDoc_DeptID)entity.DeptID=values.ReaBmsInDoc_DeptID;
		if(values.ReaBmsInDoc_DeptName)entity.DeptName=values.ReaBmsInDoc_DeptName;
		entity.Memo = values.ReaBmsInDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		if(values.ReaBmsInDoc_SourceType) entity.SourceType = values.ReaBmsInDoc_SourceType;
		if(values.ReaBmsInDoc_SaleDocID) entity.SaleDocID = values.ReaBmsInDoc_SaleDocID;
		if(values.ReaBmsInDoc_SaleDocConfirmID) entity.SaleDocConfirmID = values.ReaBmsInDoc_SaleDocConfirmID;
		if(values.ReaBmsInDoc_CreaterID) entity.CreaterID = values.ReaBmsInDoc_CreaterID;
		if(values.ReaBmsInDoc_OperDate) entity.OperDate = JShell.Date.toServerDate(values.ReaBmsInDoc_OperDate);
		if(values.ReaBmsInDoc_TotalPrice) entity.TotalPrice = parseFloat(values.ReaBmsInDoc_TotalPrice);
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		// 'OperDate', 'TotalPrice',后台处理
		var fields = [
			'Id', 'Status', 'InDocNo', 'OtherDocNo', 'Carrier', 'InType', 'CreaterName', 'InvoiceNo', 'Memo', 'CreaterID', 'SaleDocNo', 'ZX1', 'ZX2', 'ZX3'
		];
		if(values.ReaBmsInDoc_DeptID)fields.push("DeptID");
		if(values.ReaBmsInDoc_DeptName)fields.push("DeptName");
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaBmsInDoc_Id;
		return entity;
	}
});