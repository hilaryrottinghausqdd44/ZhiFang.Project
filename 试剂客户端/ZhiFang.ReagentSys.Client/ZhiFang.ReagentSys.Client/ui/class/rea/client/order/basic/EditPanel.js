/**
 * 客户端订单维护
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.basic.EditPanel', {
	extend: 'Ext.panel.Panel',

	title: '订单信息',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,

	layout: {
		type: 'border'
	},
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/ST_UDTO_AddReaBmsCenOrderDocAndDt',
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaBmsCenOrderDocAndDt',
	
	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//供应商选择改变后
		me.DocForm.on({
			reacompcheck: function(v, objValue) {
				me.OrderDtlGrid.ReaCompID = objValue["ReaCompID"];
				me.OrderDtlGrid.ReaCompCName = objValue["ReaCompCName"];
				me.OrderDtlGrid.setGoodstemplateClassConfig(true);
				if(me.formtype=="add"){
					me.OrderDtlGrid.store.removeAll();
					me.DocForm.setCompReadOnlys(false);
				}
			},
			//表单供货商编辑状态处理
			isEditAfter: function(form) {
				var bo = true;
				if(me.OrderDtlGrid.store.getCount() <= 0) bo = false;
				me.DocForm.setCompReadOnlys(bo);
			}
		});
		//明细列表 的货品明细添加,删除,数量改变后,需要重新计算总价格并联动更新表单总价格及表单供货商编辑状态处理
		me.OrderDtlGrid.on({
			onAddAfter: function(grid) {
				me.setFromTotalPrice();
				me.DocForm.setCompReadOnlys(true);
			},
			onDelAfter: function(grid) {
				me.setFromTotalPrice();
				var bo = true;
				if(me.OrderDtlGrid.store.getCount() <= 0) bo = false;
				me.DocForm.setCompReadOnlys(bo);
			},
			onEditAfter: function(grid) {
				JShell.Action.delay(function() {
					me.setFromTotalPrice();
					me.DocForm.setCompReadOnlys(true);
				}, null, 500);
			},
			nodata: function(p) {
				me.DocForm.setCompReadOnlys(false);
			}
		});
	},
	initComponent: function() {
		var me = this;
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.OrderDtlGrid = Ext.create('Shell.class.rea.client.order.basic.OrderDtlGrid', {
			header: false,
			itemId: 'OrderDtlGrid',
			region: 'center',
			collapsible: false,
			PK: me.PK,
			defaultLoad: false,
			collapsed: false,
			formtype: me.formtype
		});
		me.DocForm = Ext.create('Shell.class.rea.client.order.basic.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 230,
			split: false,
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype
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
		me.OrderDtlGrid.setGoodstemplateClassConfig(true);
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";

		me.DocForm.PK = null;
		me.formtype = "add";
		me.DocForm.StatusName = "新增申请";
		me.DocForm.isAdd();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.OrderDtlGrid.PK = null;
		me.OrderDtlGrid.ReaCompID = null;
		me.OrderDtlGrid.ReaCompCName = null;
		me.OrderDtlGrid.formtype = "add";
		me.OrderDtlGrid.defaultWhere = "";
		me.OrderDtlGrid.Status = "0";
		me.OrderDtlGrid.store.removeAll();
		me.OrderDtlGrid.enableControl();
		me.OrderDtlGrid.buttonsDisabled = false;
		me.OrderDtlGrid.setButtonsDisabled(false);
		me.OrderDtlGrid.setGoodstemplateClassConfig(true);
	},
	isEdit: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsCenOrderDoc_Id");
		me.PK = id;
		me.formtype = "edit";

		me.DocForm.formtype = "edit";
		me.DocForm.PK = id;
		
		me.DocForm.isEdit(id);
		me.DocForm.getComponent('buttonsToolbar').hide();
		me.DocForm.setCompReadOnlys(true);
		
		me.OrderDtlGrid.PK = id;
		me.OrderDtlGrid.formtype = "edit";
		me.OrderDtlGrid.Status = record.get("ReaBmsCenOrderDoc_Status");
		me.OrderDtlGrid.ReaCompID = record.get("ReaBmsCenOrderDoc_ReaCompID");
		me.OrderDtlGrid.ReaCompCName = record.get("ReaBmsCenOrderDoc_ReaCompanyName");
		var defaultWhere = "";
		if(me.PK) defaultWhere = "reabmscenorderdtl.OrderDocID=" + me.PK;
		me.OrderDtlGrid.defaultWhere = defaultWhere;
		me.OrderDtlGrid.onSearch();

		me.OrderDtlGrid.buttonsDisabled = false;
		me.OrderDtlGrid.setButtonsDisabled(false);
		me.OrderDtlGrid.setGoodstemplateClassConfig(true);
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsCenOrderDoc_Id");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.formtype = "show";
		var status = record.get("ReaBmsCenOrderDoc_Status");
		me.DocForm.isShow(id);
		me.DocForm.setCompReadOnlys(true);
		
		me.OrderDtlGrid.PK = id;
		me.OrderDtlGrid.formtype = "show";
		me.OrderDtlGrid.Status = record.get("ReaBmsCenOrderDoc_Status");
		me.OrderDtlGrid.ReaCompID = record.get("ReaBmsCenOrderDoc_ReaCompID");
		me.OrderDtlGrid.ReaCompCName = record.get("ReaBmsCenOrderDoc_ReaCompanyName");
		var defaultWhere = "";
		if(me.PK) defaultWhere = "reabmscenorderdtl.OrderDocID=" + me.PK;
		me.OrderDtlGrid.defaultWhere = defaultWhere;
		me.OrderDtlGrid.onSearch();
		me.OrderDtlGrid.buttonsDisabled = true;
		me.OrderDtlGrid.setGoodstemplateClassConfig(true);
	},
	/**@description 获取明细的保存提交数据*/
	getSaveParams: function() {
		var me = this;
		var result = me.OrderDtlGrid.getDtSaveParams();
		if(me.fromtype == "add") {
			me.PK = -1;
			me.Status = "1";
		}
		var entity = me.formtype == 'add' ? me.DocForm.getAddParams() : me.DocForm.getEditParams();
		if(result.TotalPrice) {
			entity.entity.TotalPrice = result.TotalPrice;
			if(me.formtype == 'edit' && entity.fields && entity.fields.indexOf("TotalPrice") < 0)
				entity.fields = entity.fields + ",TotalPrice";
		}
		var params = {
			entity: entity.entity,
			"dtAddList": result.dtAddList
		};
		if(me.formtype == 'add') {
			params.otype = 1;
		}
		if(me.formtype == 'edit') {
			params.fields = entity.fields;
			params.dtEditList = result.dtEditList;
		}
		return params;
	},
	/**@description 明细列表重新计算订单总价格*/
	calcTotalPrice: function() {
		var me = this;
		var totalPrice = 0;
		me.OrderDtlGrid.store.each(function(record) {
			var goodsQty = record.get('ReaBmsCenOrderDtl_GoodsQty');
			if(goodsQty) goodsQty = parseFloat(goodsQty);
			var price = record.get('ReaBmsCenOrderDtl_Price');
			if(price) price = parseFloat(price);
			totalPrice += parseFloat(price * goodsQty);
		});
		//totalPrice = totalPrice;
		return totalPrice;
	},
	/**@description 重新计算表单的订单总价格*/
	setFromTotalPrice: function() {
		var me = this;
		var com = me.DocForm.getComponent('ReaBmsCenOrderDoc_TotalPrice');
		var totalPrice = me.calcTotalPrice();
		if(totalPrice) totalPrice = parseFloat(totalPrice);
		com.setValue(totalPrice);
	}
});