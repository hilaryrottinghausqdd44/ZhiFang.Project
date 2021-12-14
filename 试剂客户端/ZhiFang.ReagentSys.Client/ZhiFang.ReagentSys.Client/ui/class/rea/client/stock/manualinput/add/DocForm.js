/**
 * 客户端库存初始化(手工入库)
 * @author longfc
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.stock.manualinput.add.DocForm', {
	extend: 'Shell.class.rea.client.stock.manualinput.basic.DocForm',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '入库信息',
	width: 1086,
	height: 155,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);

		var createrID = me.getComponent('ReaBmsInDoc_CreaterID');
		var createrName = me.getComponent('ReaBmsInDoc_CreaterName');
		createrID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		createrName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));
		
		var deptIdV = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		var deptNameV = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		var deptId = me.getComponent('ReaBmsInDoc_DeptID');
		var deptName = me.getComponent('ReaBmsInDoc_DeptName');
		if(deptId)deptId.setValue(deptIdV);
		if(deptName)deptName.setValue(deptNameV);
		
		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate, true);
		var OperDate = me.getComponent('ReaBmsInDoc_OperDate');
		OperDate.setValue(curDateTime);
		var InType = me.getComponent('ReaBmsInDoc_InType');
		InType.setValue("2");

		me.getComponent('buttonsToolbar').hide();
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
			Status: me.Status,
			InDocNo: values.ReaBmsInDoc_InDocNo,
			Carrier: values.ReaBmsInDoc_Carrier,
			InType: values.ReaBmsInDoc_InType,
			CreaterName: values.ReaBmsInDoc_CreaterName,
			InvoiceNo: values.ReaBmsInDoc_InvoiceNo,
			SaleDocNo: values.ReaBmsInDoc_SaleDocNo
		};
		if(values.ReaBmsInDoc_DeptID)entity.DeptID=values.ReaBmsInDoc_DeptID;
		if(values.ReaBmsInDoc_DeptName)entity.DeptName=values.ReaBmsInDoc_DeptName;		
		entity.Memo = values.ReaBmsInDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
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
			'Id', 'Status', 'InDocNo', 'Carrier', 'InType', 'CreaterName', 'InvoiceNo', 'Memo', 'CreaterID','SaleDocNo'
		];
		if(values.ReaBmsInDoc_DeptID)fields.push("DeptID");
		if(values.ReaBmsInDoc_DeptName)fields.push("DeptName");
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaBmsInDoc_Id;
		return entity;
	}
});