/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.basic.add.EditPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '供货信息',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,

	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddReaBmsCenSaleDocAndDtl',
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/RS_UDTO_UpdateReaBmsCenSaleDocAndDtl',
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save', 'createBarCode', 'onLaunchFullScreen', 'onExitFullScreen');
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var appInfos = [];
		return appInfos;
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		me.formtype = "";
		me.DtlPanel.formtype = "";

		me.DocForm.PK = null;
		me.setFormType("show");
		me.DocForm.getForm().reset();

		me.DtlPanel.clearData(); //清空数据
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.DocForm.formtype = formtype;
		me.DtlPanel.setFormType(formtype);
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.setFormType("add");
		me.DocForm.isAdd();
		me.DtlPanel.isAdd();
	},
	isEdit: function(record) {
		var me = this;
		var id = record.get("ReaBmsCenSaleDoc_Id");
		me.PK = id;

		me.setFormType("edit");

		me.DocForm.PK = id;
		me.DocForm.isEdit(id);

		me.DtlPanel.PK = id;
		me.DtlPanel.isEdit(record);
	},
	/**主订单联动明细及表单*/
	isShow: function(record) {
		var me = this;
		var id = record.get("ReaBmsCenSaleDoc_Id");
		me.PK = id;

		me.setFormType("show");
		me.DocForm.PK = id;
		me.DocForm.isShow(id);

		me.DtlPanel.PK = id;
		me.DtlPanel.isShow(record);
	},
	/**@description 明细列表重新计算总价格*/
	calcTotalPrice: function(grid) {
		var me = this;
		var totalPrice = 0;
		grid.store.each(function(record) {
			var goodsQty = record.get('ReaBmsCenSaleDtl_GoodsQty');
			if(goodsQty) goodsQty = parseFloat(goodsQty);
			var price = record.get('ReaBmsCenSaleDtl_Price');
			if(price) price = parseFloat(price);
			totalPrice += parseFloat(price * goodsQty);
		});
		return totalPrice;
	},
	/**@description 重新计算表单的总价格*/
	setFromTotalPrice: function(grid) {
		var me = this;
		var com = me.DocForm.getComponent('ReaBmsCenSaleDoc_TotalPrice');
		var totalPrice = me.calcTotalPrice(grid);
		if(totalPrice) totalPrice = parseFloat(totalPrice);
		com.setValue(totalPrice);
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function(params) {
		var me = this;
		if(me.PK) me.setFormType("edit");
		me.onSave(params);
	},
	/**@description 保存处理方法*/
	onSave: function(params, status) {
		var me = this;		
	}
});