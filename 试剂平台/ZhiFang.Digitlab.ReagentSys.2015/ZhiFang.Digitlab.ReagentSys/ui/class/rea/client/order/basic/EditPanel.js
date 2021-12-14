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
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsCenOrderDocAndDt',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenOrderDocAndDt',
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//供应商选择改变后
		me.DocForm.on({
			reacompcheck: function(v, record) {
				var ReaCompID = record ? record.get('ReaCenOrg_Id') : '';
				me.OrderDtlGrid.ReaCompID = ReaCompID;
				me.OrderDtlGrid.ReaCompCName =record ? record.get('ReaCenOrg_CName') : '';
				me.OrderDtlGrid.setGoodstemplateClassConfig();
			},
			//表单供货方编辑状态处理
			isEditAfter: function(form) {
				var bo = true;
				if(me.OrderDtlGrid.store.getCount() <= 0) bo = false;
				me.setReaCompNameReadOnly(bo);
			}
		});
		//明细列表 的货品明细添加,删除,数量改变后,需要重新计算总价格并联动更新表单总价格及表单供货方编辑状态处理
		me.OrderDtlGrid.on({
			onAddAfter: function(grid) {
				me.setFromTotalPrice();
				me.setReaCompNameReadOnly(true);
			},
			onDelAfter: function(grid) {
				me.setFromTotalPrice();
				var bo = true;
				if(me.OrderDtlGrid.store.getCount() <= 0) bo = false;
				me.setReaCompNameReadOnly(bo);
			},
			onEditAfter: function(grid) {
				JShell.Action.delay(function() {
					me.setFromTotalPrice();
					me.setReaCompNameReadOnly(true);
				}, null, 500);
			},
			nodata: function(p) {
				me.setReaCompNameReadOnly(false);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
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
			formtype: me.formtype,
			OTYPE: me.OTYPE
		});
		me.DocForm = Ext.create('Shell.class.rea.client.order.basic.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 180,
			split: false,
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: me.OTYPE
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
		me.OrderDtlGrid.ReaCompCName =null;
		me.OrderDtlGrid.Status = null;
		me.OrderDtlGrid.store.removeAll();
		me.OrderDtlGrid.disableControl();
		me.OrderDtlGrid.buttonsDisabled = true;
		me.OrderDtlGrid.setButtonsDisabled(true);
		me.OrderDtlGrid.setGoodstemplateClassConfig();
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
		me.OrderDtlGrid.ReaCompCName =null;
		me.OrderDtlGrid.formtype = "add";
		me.OrderDtlGrid.defaultWhere = "";
		me.OrderDtlGrid.Status = "0";
		me.OrderDtlGrid.store.removeAll();
		me.OrderDtlGrid.enableControl();
		me.OrderDtlGrid.setBtnDisabled("btnAdd", false);
		me.OrderDtlGrid.setBtnDisabled("btnDel", false);
		me.OrderDtlGrid.setBtnDisabled("btnSave", true);
		me.OrderDtlGrid.setGoodstemplateClassConfig();
	},
	isEdit: function(record, applyGrid) {
		var me = this;
		var id = record.get("BmsCenOrderDoc_Id");
		me.PK = id;
		me.formtype = "edit";

		me.DocForm.formtype = "edit";
		me.DocForm.PK = id;
		var statusName = "",
			color = "#1c8f36";
		if(applyGrid.StatusEnum != null) statusName = applyGrid.StatusEnum[record.get("BmsCenOrderDoc_Status")];
		if(applyGrid.StatusBGColorEnum != null) color = applyGrid.StatusBGColorEnum[record.get("BmsCenOrderDoc_Status")];
		statusName = '<b style="color:' + color + ';">' + statusName + '</b> ';
		me.DocForm.StatusName = statusName;
		me.DocForm.isEdit(id);
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.OrderDtlGrid.PK = id;
		me.OrderDtlGrid.formtype = "edit";
		me.OrderDtlGrid.Status = record.get("BmsCenOrderDoc_Status");
		me.OrderDtlGrid.ReaCompID = record.get("BmsCenOrderDoc_ReaCompID");
		me.OrderDtlGrid.ReaCompCName = record.get("BmsCenOrderDoc_ReaCompName");
		var defaultWhere = "";
		if(me.PK) defaultWhere = "bmscenorderdtl.BmsCenOrderDoc.Id=" + me.PK;
		me.OrderDtlGrid.defaultWhere = defaultWhere;
		me.OrderDtlGrid.onSearch();

		me.OrderDtlGrid.buttonsDisabled = false;
		me.OrderDtlGrid.setButtonsDisabled(false);
		me.OrderDtlGrid.setGoodstemplateClassConfig();
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {

		var me = this;
		var id = record.get("BmsCenOrderDoc_Id");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.formtype = "show";
		var statusName = "",
			color = "#1c8f36";
		if(applyGrid.StatusEnum != null) statusName = applyGrid.StatusEnum[record.get("BmsCenOrderDoc_Status")];
		if(applyGrid.StatusBGColorEnum != null) color = applyGrid.StatusBGColorEnum[record.get("BmsCenOrderDoc_Status")];
		statusName = '<b style="color:' + color + ';">' + statusName + '</b> ';
		me.DocForm.StatusName = statusName;
		me.DocForm.isShow(id);

		me.OrderDtlGrid.PK = id;
		me.OrderDtlGrid.formtype = "show";
		me.OrderDtlGrid.Status = record.get("BmsCenOrderDoc_Status");
		me.OrderDtlGrid.ReaCompID = record.get("BmsCenOrderDoc_ReaCompID");
		me.OrderDtlGrid.ReaCompCName = record.get("BmsCenOrderDoc_ReaCompName");
		var defaultWhere = "";
		if(me.PK) defaultWhere = "bmscenorderdtl.BmsCenOrderDoc.Id=" + me.PK;
		me.OrderDtlGrid.defaultWhere = defaultWhere;
		me.OrderDtlGrid.onSearch();
		me.OrderDtlGrid.buttonsDisabled = true;
		me.OrderDtlGrid.setGoodstemplateClassConfig();
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
			var goodsQty = record.get('BmsCenOrderDtl_GoodsQty');
			if(goodsQty) goodsQty = parseInt(goodsQty);
			var price = record.get('BmsCenOrderDtl_Price');
			if(price) price = parseFloat(price);
			totalPrice += parseFloat(price * goodsQty);
		});
		totalPrice = totalPrice.toFixed(2);
		return totalPrice;
	},
	/**@description 供货方是可编辑还是只读处理*/
	setReaCompNameReadOnly: function(bo) {
		var me = this;
		var com = me.DocForm.getComponent('BmsCenOrderDoc_ReaCompName');
		com.setReadOnly(bo);
	},
	/**@description 重新计算表单的订单总价格*/
	setFromTotalPrice: function() {
		var me = this;
		var com = me.DocForm.getComponent('BmsCenOrderDoc_TotalPrice');
		var totalPrice = me.calcTotalPrice();
		com.setValue(totalPrice);
	}
});