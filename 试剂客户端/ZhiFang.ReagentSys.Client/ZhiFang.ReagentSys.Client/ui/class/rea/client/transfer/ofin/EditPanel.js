/**
 * 入库移库
 * @author longfc
 * @version 2019-03-28
 */
Ext.define('Shell.class.rea.client.transfer.ofin.EditPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '入库信息',
	header: false,
	border: false,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},
	/**当前选择的入库主单Id*/
	InDocID: null,
	/**当前选择的入库主单号*/
	InDocNo: null,
	/**新增入库移库服务*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddTransferDocOfInDoc',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.QtyDtlGrid = Ext.create('Shell.class.rea.client.transfer.ofin.QtyDtlGrid', {
			header: false,
			itemId: 'QtyDtlGrid',
			region: 'center',
			defaultLoad: false
		});
		me.DocForm = Ext.create('Shell.class.rea.client.transfer.ofin.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 125,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		var appInfos = [me.QtyDtlGrid, me.DocForm];
		return appInfos;
	},
	/**根据选择的入库单加载*/
	isAdd: function(record) {
		var me = this;
		if(!record) return;

		var id = record.get("ReaBmsInDoc_Id");
		var inDocNo = record.get("ReaBmsInDoc_InDocNo");
		me.InDocID = id;
		me.InDocNo = inDocNo;
		me.DocForm.isAdd();
		me.QtyDtlGrid.InDocID = id;
		me.QtyDtlGrid.InDocNo = inDocNo;
		me.QtyDtlGrid.onSearch();
	},
	clearData: function() {
		var me = this;
		me.InDocID = null;
		me.InDocNo = null;
		me.QtyDtlGrid.InDocID = null;
		me.QtyDtlGrid.InDocNo = null;
		me.QtyDtlGrid.clearData();
		me.DocForm.clearData();
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
	/**事件监听*/
	onListeners: function(record) {
		var me = this;
		me.DocForm.on({
			onDStorageChange: function(form, dStorageID, dStorageName) {
				me.onSetQtyDtlDStorage(dStorageID, dStorageName);
			}
		});
		me.QtyDtlGrid.on({
			onConfirmTransfer: function(grid) {
				me.onConfirm();
			}
		});
	},
	/**表单的目的库房选择改变后*/
	onSetQtyDtlDStorage: function(dStorageID, dStorageName) {
		var me = this;
		me.QtyDtlGrid.store.each(function(record) {
			record.set("ReaBmsQtyDtl_DStorageID", dStorageID);
			record.set("ReaBmsQtyDtl_DStorageName", dStorageName);
			record.commit();
		});
	},
	/**确认移库*/
	onConfirm: function() {
		var me = this;
		//表单验证
		if(!me.DocForm.getForm().isValid()) return;
		//库存货品验证
		var isValid = me.QtyDtlGrid.onSaveValid();;
		if(!isValid) return;

		var transferDoc = me.DocForm.getAddParams();
		var inDoc = {
			"Id": me.InDocID,
			"InDocNo": me.InDocNo
		};
		var transferDtlList = [];
		me.QtyDtlGrid.store.each(function(record) {
			transferDtlList.push(me.getTransferDtlObj(record));
		});
		//移库主单的源库房处理
		transferDoc.SStorageID = transferDtlList[0].SStorageID;
		transferDoc.SStorageName = transferDtlList[0].SStorageName;

		var isEmpTransfer = false;
		var params = {
			"inDoc": inDoc,
			"transferDoc": transferDoc,
			"transferDtlList": transferDtlList,
			"isEmpTransfer": isEmpTransfer
		};
		var url = JShell.System.Path.getUrl(me.addUrl);

		me.showMask("确认移库保存中...");
		JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
			me.hideMask();
			if(data.success) {
				me.QtyDtlGrid.onSearch();
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**确认移库选择的库存货品*/
	getTransferDtlObj: function(record) {
		var me = this;
		var transferDtlObj = {
			"Visible": 1,
			"InDocNo": record.get("ReaBmsQtyDtl_InDocNo"),
			"InDtlID": record.get("ReaBmsQtyDtl_InDtlID"),
			"SQtyDtlID": record.get("ReaBmsQtyDtl_Id"), //源库存ID
			"QtyDtlID": record.get("ReaBmsQtyDtl_Id"), //移库的库存IDStr
			"SStorageName": record.get("ReaBmsQtyDtl_StorageName"),
			"SPlaceName": record.get("ReaBmsQtyDtl_PlaceName"),
			"DStorageName": record.get("ReaBmsQtyDtl_DStorageName"),
			"DPlaceName": record.get("ReaBmsQtyDtl_DPlaceName"),

			"GoodsID": record.get("ReaBmsQtyDtl_GoodsID"),
			"BarCodeType": record.get("ReaBmsQtyDtl_BarCodeType"),
			"ReaGoodsNo": record.get("ReaBmsQtyDtl_ReaGoodsNo"),
			"CenOrgGoodsNo": record.get("ReaBmsQtyDtl_CenOrgGoodsNo"),
			"GoodsNo": record.get("ReaBmsQtyDtl_GoodsNo"),
			"ProdGoodsNo": record.get("ReaBmsQtyDtl_ProdGoodsNo"),
			"GoodsCName": record.get("ReaBmsQtyDtl_GoodsName"),
			"GoodsSort": record.get("ReaBmsQtyDtl_GoodsSort"),
			"GoodsUnit": record.get("ReaBmsQtyDtl_GoodsUnit"),
			"UnitMemo": record.get("ReaBmsQtyDtl_UnitMemo"),
			"ReqGoodsQty": record.get("ReaBmsQtyDtl_GoodsQty"), //申请数量
			"ReqCurrentQty": record.get("ReaBmsQtyDtl_GoodsQty"), //申请时库存数
			"GoodsQty": record.get("ReaBmsQtyDtl_GoodsQty"),
			"LotNo": record.get("ReaBmsQtyDtl_LotNo"),
			"CompanyName": record.get("ReaBmsQtyDtl_CompanyName"),
			"ReaServerCompCode": record.get("ReaBmsQtyDtl_ReaServerCompCode"),
			"ReaCompCode": record.get("ReaBmsQtyDtl_ReaCompCode"),
			"GoodsSerial": record.get("ReaBmsQtyDtl_GoodsSerial"),
			"LotSerial": record.get("ReaBmsQtyDtl_LotSerial"),
			"SysLotSerial": record.get("ReaBmsQtyDtl_SysLotSerial"),
			"LotQRCode": record.get("ReaBmsQtyDtl_LotQRCode")
		};
		if(record.get("ReaBmsQtyDtl_StorageID")) {
			transferDtlObj.SStorageID = record.get("ReaBmsQtyDtl_StorageID");
		}
		if(record.get("ReaBmsQtyDtl_PlaceID")) {
			transferDtlObj.SPlaceID = record.get("ReaBmsQtyDtl_PlaceID");
		}
		if(record.get("ReaBmsQtyDtl_DStorageID")) {
			transferDtlObj.DStorageID = record.get("ReaBmsQtyDtl_DStorageID");
		}
		if(record.get("ReaBmsQtyDtl_DPlaceID")) {
			transferDtlObj.DPlaceID = record.get("ReaBmsQtyDtl_DPlaceID");
		}
		if(record.get("ReaBmsQtyDtl_CompGoodsLinkID")) {
			transferDtlObj.CompGoodsLinkID = record.get("ReaBmsQtyDtl_CompGoodsLinkID");
		}
		if(record.get("ReaBmsQtyDtl_ReaCompanyID")) {
			transferDtlObj.ReaCompanyID = record.get("ReaBmsQtyDtl_ReaCompanyID");
		}
		if(record.get("ReaBmsQtyDtl_GoodsLotID")) {
			transferDtlObj.GoodsLotID = record.get("ReaBmsQtyDtl_GoodsLotID");
		}
		var price = record.get("ReaBmsQtyDtl_Price");
		var goodsQty = record.get("ReaBmsQtyDtl_GoodsQty");
		if(!price) price = 0;
		if(!goodsQty) goodsQty = 0;
		//单价,总计金额
		transferDtlObj.Price = parseFloat(price);
		transferDtlObj.GoodsQty = parseFloat(goodsQty);
		transferDtlObj.SumTotal = transferDtlObj.Price * transferDtlObj.GoodsQty;
		//生产日期,有效期
		return transferDtlObj;
	}
});