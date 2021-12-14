/**
 * 盘库管理
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.show.Panel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '盘库信息',
	header: false,
	border: false,

	/**盘库单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DocForm.on({
			save: function(form, id) {
				me.fireEvent('save', form, id);
			},
			onEditClick: function(form, entity) {
				me.onEditSave(entity);
			},
			onConfirmClick: function(form, entity) {
				me.onConfirm(entity);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.inventory.show.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			defaultLoad: false
		});
		me.DocForm = Ext.create('Shell.class.rea.client.inventory.show.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 245,
			split: true,
			animCollapse: false,
			animate: false
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	clearData: function() {
		var me = this;
		me.DtlGrid.clearData();
		me.DocForm.clearData();
	},
	nodata: function() {
		var me = this;
		var me = this;
		me.PK = null;
		me.formtype = "show";

		me.DocForm.PK = null;
		me.DocForm.formtype = "show";
		//me.DocForm.isShow();
		me.DocForm.getForm().reset();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = null;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.disableControl();
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isEdit: function(record) {
		var me = this;
		var id = record.get("ReaBmsCheckDoc_Id");
		var status = record.get("ReaBmsCheckDoc_Status");
		me.PK = id;
		me.formtype = "edit";
		me.DocForm.PK = id;
		me.DocForm.Status = status;
		me.DocForm.isEdit(id);

		me.DtlGrid.PK = id;
		me.DtlGrid.canEdit = true;
		me.DtlGrid.formtype = "edit";
		me.DtlGrid.Status = status;
		me.loadDtlGrid(me.PK);
	},
	/**主订单联动明细及表单*/
	isShow: function(record) {
		var me = this;
		var id = record.get("ReaBmsCheckDoc_Id");
		var status = record.get("ReaBmsCheckDoc_Status");
		me.PK = id;
		me.formtype = "show";
		me.DocForm.Status = status;
		me.DocForm.PK = id;
		me.DocForm.isShow(id);

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.Status = status;
		me.DtlGrid.canEdit = false;
		me.loadDtlGrid(me.PK);
	},
	loadData: function(id) {
		var me = this;

	},
	loadDtlGrid: function(id) {
		var me = this;
		if(!id)
			var defaultWhere = "";
		if(id) defaultWhere = "reabmscheckdtl.CheckDocID=" + id;
		me.DtlGrid.defaultWhere = defaultWhere;
		me.DtlGrid.onSearch();
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	},
	/**@description 封装所有明细的保存信息*/
	getSaveDtlAll: function() {
		var me = this;
		var dtEditList = [];
		me.DtlGrid.store.each(function(record) {
			var dtl = me.getSaveDtlOne(record);
			dtEditList.push(dtl);
		});
		return dtEditList;
	},
	/**@description 获取单个的明细的基本封装信息*/
	getSaveDtlOne: function(record) {
		var me = this;
		var id = record.get(me.DtlGrid.PKField);
		if(!id) id = -1;

		var entity = {
			Id: id,
			CheckDocID: record.get("ReaBmsCheckDtl_CheckDocID"),
			CompanyName: record.get("ReaBmsCheckDtl_CompanyName"),
			StorageName: record.get("ReaBmsCheckDtl_StorageName"),
			PlaceName: record.get("ReaBmsCheckDtl_PlaceName"),
			GoodsName: record.get("ReaBmsCheckDtl_GoodsName"),
			LotNo: record.get("ReaBmsCheckDtl_LotNo"),
			GoodsUnit: record.get("ReaBmsCheckDtl_GoodsUnit"),
			UnitMemo: record.get("ReaBmsCheckDtl_UnitMemo"),
			Memo: record.get("ReaBmsCheckDtl_Memo"),
			ReaGoodsNo: record.get("ReaBmsCheckDtl_ReaGoodsNo"),
			ProdGoodsNo: record.get("ReaBmsCheckDtl_ProdGoodsNo"),
			CenOrgGoodsNo: record.get("ReaBmsCheckDtl_CenOrgGoodsNo"),
			GoodsNo: record.get("ReaBmsCheckDtl_GoodsNo"),
			LotSerial: record.get("ReaBmsCheckDtl_LotSerial"),
			LotQRCode: record.get("ReaBmsCheckDtl_LotQRCode")
		};

		var ReaCompanyID = record.get("ReaBmsCheckDtl_ReaCompanyID");
		var StorageID = record.get("ReaBmsCheckDtl_StorageID");
		var PlaceID = record.get("ReaBmsCheckDtl_PlaceID");
		var GoodsID = record.get("ReaBmsCheckDtl_GoodsID");
		if(ReaCompanyID) entity.ReaCompanyID = ReaCompanyID;
		if(StorageID) entity.StorageID = StorageID;
		if(PlaceID) entity.PlaceID = PlaceID;
		if(GoodsID) entity.GoodsID = GoodsID;

		var GoodsQty = record.get("ReaBmsCheckDtl_GoodsQty");
		var SumTotal = record.get("ReaBmsCheckDtl_SumTotal");
		var CheckQty = record.get("ReaBmsCheckDtl_CheckQty");
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		if(GoodsQty) entity.GoodsQty = GoodsQty;
		
		if(CheckQty) CheckQty = parseFloat(CheckQty);
		else CheckQty = 0;
		entity.CheckQty = CheckQty;
		
		if(SumTotal) SumTotal = parseFloat(SumTotal);
		if(SumTotal) entity.SumTotal = SumTotal;

		var IsException = record.get("ReaBmsCheckDtl_IsException");
		var IsHandleException = record.get("ReaBmsCheckDtl_IsHandleException");
		if(IsException == "true" || IsException == true || IsException == "1" || IsException == 1)
			IsException = 1;
		else
			IsException = 0;
		entity.IsException = IsException;

		if(IsHandleException == "true" || IsHandleException == true || IsHandleException == "1" || IsHandleException == 1)
			IsHandleException = 1;
		else
			IsHandleException = 0;
		entity.IsHandleException = IsHandleException;
		
		var BarCodeType = record.get("ReaBmsCheckDtl_BarCodeType");
		if(BarCodeType || BarCodeType == 0) entity.BarCodeType = BarCodeType;
		var GoodsLotID = record.get("ReaBmsCheckDtl_GoodsLotID");
		if(GoodsLotID) entity.GoodsLotID = GoodsLotID;
		var CompGoodsLinkID = record.get("ReaBmsCheckDtl_CompGoodsLinkID");
		if(CompGoodsLinkID) entity.CompGoodsLinkID = CompGoodsLinkID;
		var ProdDate = record.get("ReaBmsCheckDtl_ProdDate");
		var InvalidDate = record.get("ReaBmsCheckDtl_InvalidDate");
		if(ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		if(InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);
		return entity;
	},
	/**@description 编辑更新盘库及明细信息*/
	onEditSave: function(entity) {
		var me = this;
		me.onSave(entity, function() {
			me.fireEvent('save', me.DocForm, me.PK);
		});
	},
	/**@description 确认盘库*/
	onConfirm: function(entity) {
		var me = this;
		var isSave = true,
			info = "";
		//验证盘库明细的实盘数是否存在为空记录行
		me.DtlGrid.store.each(function(record) {
			var checkQty = record.get("ReaBmsCheckDtl_CheckQty");
			if(!checkQty && checkQty <0) {
				isSave = false;
				info = "货品为:" + record.get("ReaBmsCheckDtl_GoodsName") + "的实盘数小于0,不能确认盘库!";
				return false;
			}
		});
		if(isSave == false) {
			JShell.Msg.error(info);
			return;
		}
		entity.entity.Status = 2;
		me.onSave(entity, function() {
			me.fireEvent('save', me.DocForm, me.PK);
		});
	},
	/**@description 编辑保存盘库信息*/
	onSave: function(entity, callback) {
		var me = this;
		var dtEditList = me.getSaveDtlAll();
		if(dtEditList.Length <= 0) {
			JShell.Msg.error("盘库明细信息为空,不能操作!");
			return;
		}
		var params = {
			"entity": entity.entity,
			"fields": entity.fields,
			"dtEditList": dtEditList,
			"fieldsDtl": "Id,CheckQty,IsException,IsHandleException,Memo,DataUpdateTime"
		};
		params = JcallShell.JSON.encode(params);
		me.DocForm.showMask(me.saveText); //显示遮罩层
		var url = JShell.System.Path.ROOT + me.DocForm.editUrl;
		JShell.Server.post(url, params, function(data) {
			me.DocForm.hideMask(); //隐藏遮罩层
			if(data.success) {
				if(callback) callback();
				if(me.DocForm.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.DocForm.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});