/**
 * 盘库管理-新增盘库
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.add.Panel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '新增盘库',

	/**盘库单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'add',
	//按钮是否可点击
	BUTTON_CAN_CLICK: true,
	saveText: "保存中",
	/**盘库时实盘数是否取库存数 1:是;2:否;*/
	isTakenFromQty: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocForm = Ext.create('Shell.class.rea.client.inventory.add.DocForm', {
			header: false,
			border: false,
			itemId: 'DocForm',
			region: 'north',
			height: 165,
			split: true,
			animCollapse: false,
			animate: false
		});
		me.ChoosePanel = Ext.create('Shell.class.rea.client.inventory.add.choose.App', {
			header: false,
			border: false,
			itemId: 'ChoosePanel',
			region: 'center',
			defaultLoad: false
		});
		var appInfos = [me.ChoosePanel, me.DocForm];
		return appInfos;
	},
	onListeners: function() {
		var me = this;
		me.DocForm.on({
			onSearch: function(form, leftGrid) {
				JShell.Action.delay(function() {
					me.setZeroQtyDays();
					me.onLoadData();
				}, null, 1000);
			},
			onSave: function(form) {
				me.onSave();
			}
		});
		me.ChoosePanel.on({
			onSearch: function(choosePanel, leftGrid) {
				JShell.Action.delay(function() {
					me.onLoadData();
				}, null, 500);
			},
			onIsLocked: function(choosePanel, isLocked) {
				me.DocForm.onSetReadOnlyOfLocked(isLocked);
			}
		});
	},
	clearData: function() {
		var me = this;
		me.ChoosePanel.clearData();
		me.DocForm.clearData();
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";
		me.DocForm.isAdd();
		me.DocForm.getComponent('buttonsToolbar').show();
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		}
	},
	/**@description 获取符合盘库条件的盘库库存货品*/
	onLoadData: function() {
		var me = this;
		var params = me.DocForm.getAddParams();
		//左列表查询项处理
		var reaGoodHql = me.getReaGoodHql(params.entity);
		var where = {
			"docEntity": JShell.JSON.encode(params.entity),
			"reaGoodHql": reaGoodHql
		};
		me.ChoosePanel.loadData(where);
	},
	/**@description 左列表查询项处理*/
	setZeroQtyDays: function() {
		var me = this;
		var params = me.DocForm.getAddParams();
		me.ChoosePanel.LeftGrid.setZeroQtyDays(params.entity.IsHasZeroQty);
	},
	/**@description 获取盘库库存货品的机构货品条件*/
	getReaGoodHql: function(entity) {
		var me = this;
		var reaGoodHql = "";
		var where = [];
		//盘库条件的机构货品
		if (entity.GoodsClass) {
			where.push("reagoods.GoodsClass='" + entity.GoodsClass + "'");
		}
		if (entity.GoodsClassType) {
			where.push("reagoods.GoodsClassType='" + entity.GoodsClassType + "'");
		}
		reaGoodHql = where.join(" and ");
		//盘库明细列表的机构货品条件
		var reaGoodHql2 = me.ChoosePanel.LeftGrid.getReaGoodHql();
		if (reaGoodHql2.length > 0 && reaGoodHql.length > 0) {
			reaGoodHql = reaGoodHql + " and " + reaGoodHql2;
		} else if (reaGoodHql2.length > 0 && reaGoodHql.length <= 0) {
			reaGoodHql = reaGoodHql2;
		}
		//右边列表已选择的盘库货品
		if (me.ChoosePanel.leftReaGoodHql.length > 0 && reaGoodHql.length > 0) {
			reaGoodHql = reaGoodHql + " and " + me.ChoosePanel.leftReaGoodHql;
		} else if (me.ChoosePanel.leftReaGoodHql.length > 0) {
			reaGoodHql = me.ChoosePanel.leftReaGoodHql;
		}
		return reaGoodHql;
	},
	/**@description 封装所有明细的保存信息*/
	getSaveDtlList: function() {
		var me = this;
		var saveDtlList = [];
		me.ChoosePanel.RightGrid.store.each(function(record) {
			var dtl = me.getSaveDtlOne(record);
			saveDtlList.push(dtl);
		});
		return saveDtlList;
	},
	/**@description 获取单个的明细的基本封装信息*/
	getSaveDtlOne: function(record) {
		var me = this;
		var id = record.get("ReaBmsCheckDtl_Id");
		if (!id) id = -1;

		var entity = {
			Id: id,
			CheckDocID: me.formtype == "add" ? null : record.get("ReaBmsCheckDtl_CheckDocID"),
			CompanyName: record.get("ReaBmsCheckDtl_CompanyName"),
			ReaCompCode: record.get("ReaBmsCheckDtl_ReaCompCode"),
			ReaServerCompCode: record.get("ReaBmsCheckDtl_ReaServerCompCode"),
			StorageName: record.get("ReaBmsCheckDtl_StorageName"),
			PlaceName: record.get("ReaBmsCheckDtl_PlaceName"),
			GoodsName: record.get("ReaBmsCheckDtl_GoodsName"),
			GoodsSName:record.get("ReaBmsCheckDtl_GoodsSName"),
			ProdOrgName:record.get("ReaBmsCheckDtl_ProdOrgName"),
			LotNo: record.get("ReaBmsCheckDtl_LotNo"),
			GoodsUnit: record.get("ReaBmsCheckDtl_GoodsUnit"),
			UnitMemo: record.get("ReaBmsCheckDtl_UnitMemo"),
			ReaGoodsNo: record.get("ReaBmsCheckDtl_ReaGoodsNo"),
			ProdGoodsNo: record.get("ReaBmsCheckDtl_ProdGoodsNo"),
			CenOrgGoodsNo: record.get("ReaBmsCheckDtl_CenOrgGoodsNo"),
			GoodsNo: record.get("ReaBmsCheckDtl_GoodsNo"),
			LotSerial: record.get("ReaBmsCheckDtl_LotSerial"),
			LotQRCode: record.get("ReaBmsCheckDtl_LotQRCode"),
			ZX1: record.get("ReaBmsCheckDtl_ZX1"),
			ZX2: record.get("ReaBmsCheckDtl_ZX2"),
			ZX3: record.get("ReaBmsCheckDtl_ZX3"),
			Memo: record.get("ReaBmsCheckDtl_Memo")
		};

		var ReaCompanyID = record.get("ReaBmsCheckDtl_ReaCompanyID");
		var StorageID = record.get("ReaBmsCheckDtl_StorageID");
		var PlaceID = record.get("ReaBmsCheckDtl_PlaceID");
		var GoodsID = record.get("ReaBmsCheckDtl_GoodsID");
		if (ReaCompanyID) entity.ReaCompanyID = ReaCompanyID;
		if (StorageID) entity.StorageID = StorageID;
		if (PlaceID) entity.PlaceID = PlaceID;
		if (GoodsID) entity.GoodsID = GoodsID;
		var Price = record.get("ReaBmsCheckDtl_Price");
		var GoodsQty = record.get("ReaBmsCheckDtl_GoodsQty");
		var SumTotal = record.get("ReaBmsCheckDtl_SumTotal");
		var CheckQty = record.get("ReaBmsCheckDtl_CheckQty");
		if (Price) Price = parseFloat(Price);
		else Price = 0;
		entity.Price = Price;
		if (GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		entity.GoodsQty = GoodsQty;
		if (CheckQty) CheckQty = parseFloat(CheckQty);
		else CheckQty = 0;
		entity.CheckQty = CheckQty;
		if (SumTotal) SumTotal = parseFloat(SumTotal);
		else SumTotal = 0;
		entity.SumTotal = SumTotal;

		var IsException = record.get("ReaBmsCheckDtl_IsException");
		var IsHandleException = record.get("ReaBmsCheckDtl_IsHandleException");
		if (IsException == "true" || IsException == true || IsException == "1" || IsException == 1)
			IsException = 1;
		else
			IsException = 0;
		entity.IsException = IsException;

		if (IsHandleException == "true" || IsHandleException == true || IsHandleException == "1" || IsHandleException == 1)
			IsHandleException = 1;
		else
			IsHandleException = 0;
		entity.IsHandleException = IsHandleException;

		var BarCodeType = record.get("ReaBmsCheckDtl_BarCodeType");
		if (BarCodeType || BarCodeType == 0) entity.BarCodeType = BarCodeType;
		var GoodsLotID = record.get("ReaBmsCheckDtl_GoodsLotID");
		if (GoodsLotID) entity.GoodsLotID = GoodsLotID;
		var CompGoodsLinkID = record.get("ReaBmsCheckDtl_CompGoodsLinkID");
		if (CompGoodsLinkID) entity.CompGoodsLinkID = CompGoodsLinkID;
		var ProdDate = record.get("ReaBmsCheckDtl_ProdDate");
		var InvalidDate = record.get("ReaBmsCheckDtl_InvalidDate");
		if (ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		if (InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);

		var DispOrder = record.get("ReaBmsCheckDtl_DispOrder");
		if (DispOrder) entity.DispOrder = DispOrder;
		return entity;
	},
	/**@description 编辑保存盘库信息*/
	onSave: function() {
		var me = this;
		if (!me.DocForm.getForm().isValid()) return;

		var entity = me.DocForm.getAddParams();
		var saveDtlList = me.getSaveDtlList();
		if (saveDtlList.Length <= 0) {
			JShell.Msg.error("盘库明细信息为空,不能操作!");
			return;
		}
		if (!me.BUTTON_CAN_CLICK) return;

		var params = {
			"entity": entity.entity,
			"dtAddList": saveDtlList,
			/**盘库时实盘数是否取库存数 1:是;2:否;*/
			"isTakenFromQty": me.isTakenFromQty
		};
		params = JcallShell.JSON.encode(params);
		var url = JShell.System.Path.ROOT + me.DocForm.addUrl;
		me.showMask(me.saveText); //显示遮罩层
		me.BUTTON_CAN_CLICK = false; //不可点击

		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			me.BUTTON_CAN_CLICK = true;

			if (data.success) {
				me.ChoosePanel.clearData();

				var id = me.formtype == 'add' ? data.value.id : id;
				id += '';
				me.fireEvent('save', me, id);
				if (me.DocForm.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.DocForm.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});
