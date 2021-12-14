/**
 * 供货管理
 * @author longfc
 * @version 2018-05-08
 */
Ext.define('Shell.class.rea.client.reasale.basic.add.DocForm', {
	extend: 'Shell.class.rea.client.reasale.basic.DocForm',

	title: '供货信息',

	width: 680,
	height: 195,

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	buttonDock: "top",
	/**供货单数据来源默认值*/
	defaultSourceValue: "",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('setReaCompInfo', 'onSave', 'isEditAfter');
		
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];

		items.push({
			xtype: 'button',
			itemId: 'btnTempSave',
			iconCls: 'button-save',
			text: "暂存",
			tooltip: "供货单暂存",
			hidden: true,
			handler: function(btn, e) {
				me.onTempSaveClick(btn, e);
			}
		}, {
			xtype: 'button',
			itemId: 'btnSave',
			iconCls: 'button-save',
			text: "确认提交",
			tooltip: "确认提交",
			handler: function(btn, e) {
				me.onApplyClick(btn, e);
			}
		});
		if(me.hasReset) items.push('reset');
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: me.buttonDock,
			itemId: 'buttonsToolbar',
			items: items,
			hidden: true
		});
	},
	/**订货方选择*/
	onLabcAccept: function(record) {
		JShell.Msg.overwrite('onLabcAccept');
	},
	/**供货商选择*/
	onCompAccept: function(record) {
		JShell.Msg.overwrite('onCompAccept');
	},
	setTotalPrice: function(totalPrice) {
		var me = this;
		var TotalPrice = me.getComponent('ReaBmsCenSaleDoc_TotalPrice');
		TotalPrice.setValue(totalPrice);
	},
	onTempSaveClick: function(btn, e) {
		JShell.Msg.overwrite('onTempSaveClick');
	},
	/**
	 * @description 确认提交
	 */
	onApplyClick: function(btn, e) {
		JShell.Msg.overwrite('onApplyClick');
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		JShell.Msg.overwrite('onSaveClick');
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			Status: values.ReaBmsCenSaleDoc_Status,
			SaleDocNo: values.ReaBmsCenSaleDoc_SaleDocNo,
			LabcName: values.ReaBmsCenSaleDoc_LabcName, //订货方
			ReaServerLabcCode: values.ReaBmsCenSaleDoc_ReaServerLabcCode, //订货方平台编码
			CompanyName: values.ReaBmsCenSaleDoc_CompanyName, //供货商
			ReaServerCompCode: values.ReaBmsCenSaleDoc_ReaServerCompCode, //供货商平台编码
			ReaCompanyName: values.ReaBmsCenSaleDoc_ReaCompanyName, //本地供货商			
			UserName: values.ReaBmsCenSaleDoc_UserName,
			InvoiceNo: values.ReaBmsCenSaleDoc_InvoiceNo,
			UrgentFlag: values.ReaBmsCenSaleDoc_UrgentFlag,
			DeptName: values.ReaBmsCenSaleDoc_DeptName,
			ReaCompCode: values.ReaBmsCenSaleDoc_ReaCompCode,
			ReaLabcCode: values.ReaBmsCenSaleDoc_ReaLabcCode
		};
		
		if(values.ReaBmsCenSaleDoc_LabID) entity.LabID = values.ReaBmsCenSaleDoc_LabID;
		
		entity.Memo = values.ReaBmsCenSaleDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');

		if(values.ReaBmsCenSaleDoc_Source) entity.Source = values.ReaBmsCenSaleDoc_Source;
		if(values.ReaBmsCenSaleDoc_UserID) entity.UserID = values.ReaBmsCenSaleDoc_UserID;

		if(values.ReaBmsCenSaleDoc_LabcID) entity.LabcID = values.ReaBmsCenSaleDoc_LabcID;
		if(values.ReaBmsCenSaleDoc_CompID) entity.CompID = values.ReaBmsCenSaleDoc_CompID;
		if(values.ReaBmsCenSaleDoc_ReaCompID) entity.ReaCompID = values.ReaBmsCenSaleDoc_ReaCompID;

		if(values.ReaBmsCenSaleDoc_TotalPrice) entity.TotalPrice = parseFloat(values.ReaBmsCenSaleDoc_TotalPrice);
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
			'Id', 'Status', 'SaleDocNo', 'LabcID', 'LabcName', 'OrderDocNo','ReaServerLabcCode', 'CompID', 'CompanyName','ReaServerCompCode', 'ReaCompID', 'ReaCompanyName', 'InvoiceNo', 'UrgentFlag', 'Memo', 'TotalPrice'
		];
		entity.fields = fields.join(',');
		entity.entity.OrderDocNo=values.ReaBmsCenSaleDoc_OrderDocNo;
		entity.entity.Id = values.ReaBmsCenSaleDoc_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me=this;
		
		var data=me.callParent(arguments);
		var status=""+data.ReaBmsCenSaleDoc_Status;
		//如果供货单状态为暂存,取消提交
		if(status=="1"||status=="3"){
			me.getComponent('buttonsToolbar').getComponent("btnTempSave").setVisible(true);
		}
		return data;
	}
});