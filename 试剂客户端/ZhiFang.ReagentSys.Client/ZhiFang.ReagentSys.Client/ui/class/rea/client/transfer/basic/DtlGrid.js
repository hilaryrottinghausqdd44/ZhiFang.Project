/**
 * 移库登记的移库明细父类
 * @author longfc
 * @version 2019-04-24
 */
Ext.define('Shell.class.rea.client.transfer.basic.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.class.rea.client.shelves.place.ButtonsTab'
	],
	title: '目标库列表',
	width: 800,
	height: 500,
	/**查询库存数据*/
	selectStoreUrl: '/ReaManageService.svc/RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL',
	selectLinkUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaPlaceByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'ReaBmsTransferDtl_InvalidDate',
		direction: 'DESC'
	}],
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultDisableControl: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**当前选择的库房*/
	StorageArr: [],
	/**当前选择的货架*/
	PlaceArr: [],
	/**表单选中的目的库房*/
	DStorageObj: {},
	/**领用人*/
	TakerID: null,
	/**默认货架,只有一个货架时默认选择*/
	defalutPlace: '',
	PlaceData: [],
	PK: null,
	isLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**移库扫码模式(严格模式:1,混合模式：2)*/
	TransferScanCode: '2',
	/**弹出消息框消失时间*/
	hideTimes: 6000,
	/**
	 * 重复扫码时是否显示自动关闭的提示信息
	 */
	isScanCodeShowDtl:false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onScanCodeShowDtl');
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		return columns;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: [{
				text: '刷新库存量',
				tooltip: '刷新库存量',
				iconCls: 'button-search',
				handler: function() {
					me.getGoodsQty();
				}
			}]
		});
	},
	/**
	 * @description 货品为盒条码时的移库数输入框的处理
	 * @description 货品为批条码时,在"严格模式"下,也不强制必须货品扫码
	 * */
	comSetReadOnlyOfBarCodeType: function(field, e) {
		var me = this;
		var record = field.ownerCt.editingPlugin.context.record;
		var barCodeMgr = "" + record.get("ReaBmsTransferDtl_BarCodeType");
		//如果扫码模式为严格模式,批条码及盒条码需要扫码&&barCodeMgr=="1"
		if (me.TransferScanCode == "1" && barCodeMgr == "1") {
			field.setReadOnly(true);
			//return;
		} else {
			field.setReadOnly(false);
		}
	},
	/**单位改变*/
	changeGoodsUnit: function(com, newValue, oldValue) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		//移库数量   
		var GoodsQty2 = records[0].get('ReaBmsTransferDtl_GoodsQty');
		var info = com.displayTplData[0].id;
		if (info) {
			records[0].set('ReaBmsTransferDtl_ReaGoodsNo', info.ReaGoods_ReaGoodsNo);
			records[0].set('ReaBmsTransferDtl_GoodsCName', info.ReaGoods_CName);
			records[0].set('ReaBmsTransferDtl_UnitMemo', info.ReaGoods_UnitMemo);
			records[0].set('ReaBmsTransferDtl_Price', info.ReaGoods_Price);
			records[0].set('ReaBmsTransferDtl_RegistNo', info.ReaGoods_RegistNo);
			records[0].set('ReaBmsTransferDtl_GoodsID', info.ReaGoods_Id);
			records[0].set('ReaBmsTransferDtl_ProdGoodsNo', info.ReaGoods_ProdGoodsNo);
			records[0].set('ReaBmsTransferDtl_GoodsNo', info.ReaGoods_GoodsNo);
			if (!GoodsQty2) GoodsQty2 = 0;
			//货品金额
			var Price = info.ReaGoods_Price;
			if (!Price) Price = 0;
		}

		//总额=数量*单价
		records[0].set('ReaBmsTransferDtl_SumTotal', Number(GoodsQty2) * Number(Price));
		//换算系数
		var GonvertQty = info.ReaGoods_GonvertQty;
		//原始库存量
		var DefaulteGoodsQty = records[0].get('ReaBmsTransferDtl_DefaulteGoodsQty2');
		if (!GonvertQty) GonvertQty = 0;
		records[0].set('ReaBmsTransferDtl_GonvertQty', GonvertQty);
		if (!DefaulteGoodsQty) DefaulteGoodsQty = 0;
		var GoodsQty = 0;
		if ((GonvertQty != '0' || GonvertQty != 0) && GonvertQty) {
			//库存量=现有库存/单位换算系数
			GoodsQty = Number(DefaulteGoodsQty) / Number(GonvertQty);
		}
		var GoodsUnitTab = records[0].get('ReaBmsQtyDtl_GoodsUnitTab');
		if (!GoodsUnitTab) return;
		var UnitObj = Ext.JSON.decode(GoodsUnitTab);
		if (newValue != UnitObj.GoodsUnit) {
			records[0].set('ReaBmsTransferDtl_SumCurrentQty', GoodsQty);
		} else { //改变为原来单位时，还原值
			records[0].set('ReaBmsTransferDtl_SumCurrentQty', UnitObj.GoodsQty);
			records[0].set('ReaBmsTransferDtl_ReaGoodsNo', UnitObj.ReaGoodsNo);
			records[0].set('ReaBmsTransferDtl_GoodsID', UnitObj.GoodsID);
			records[0].set('ReaBmsTransferDtl_GoodsNo', UnitObj.GoodsNo);
		}
		me.getQtyCount(records[0]);
	},
	onChangePlace: function(StorageID) {
		var me = this;
		me.PlaceData = [];
		me.defalutPlace = {};
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		me.getJurisdiction(StorageID, function(data) {
			var list = data.value.list;
			me.PlaceData = list;
			//只有一个货架时，默认赋值
			if (list && list.length == 1) {
				me.defalutPlace = {
					PlaceID: list[0].ReaPlace_Id,
					PlaceName: list[0].ReaPlace_CName
				}
			}
		});
		JShell.Action.delay(function() {
			if (!StorageID) {
				buttonsToolbar.removeAll();
				buttonsToolbar.add({
					text: '刷新库存量',
					tooltip: '刷新库存量',
					iconCls: 'button-search',
					handler: function() {
						me.getGoodsQty();
					}
				});
				return;
			}
			buttonsToolbar.removeAll();
			buttonsToolbar.add({
				text: '刷新库存量',
				tooltip: '刷新库存量',
				iconCls: 'button-search',
				handler: function() {
					me.getGoodsQty();
				}
			});
			if (me.PlaceData) {
				for (var i = 0; i < me.PlaceData.length; i++) {
					var Id = me.PlaceData[i].ReaPlace_Id;
					var CName = me.PlaceData[i].ReaPlace_CName;
					var btn = {
						xtype: 'button',
						margin: '0 0 0 5',
						itemId: 'btn' + i,
						name: 'btn',
						text: CName,
						PlaceID: Id,
						PlaceName: CName,
						tooltip: CName
					};
					buttonsToolbar.add(btn);
				}
				//如果只有一个货架
				if (me.PlaceData.length == 1) {
					if (me.getStore().getCount() > 0) {
						me.setStoragePlace(Id, CName);
						me.getView().refresh();
						me.getSelectionModel().selectAll();
					}
				}
			}

			for (var i = 0; i < buttonsToolbar.items.length; i++) {
				//'-' 不处理
				if (buttonsToolbar.items.items[i].itemId) {
					buttonsToolbar.items.items[i].on({
						click: function(com, e, eOpts) {
							me.setStoragePlace(com.PlaceID, com.PlaceName);
							me.getView().refresh();
							me.getSelectionModel().selectAll();
						}
					});
				}
			}
		}, null, 300);
	},
	/**获取库房货架权限的货架信息（按领用人）*/
	getJurisdiction: function(StorageID, callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectLinkUrl;
		url += '&fields=ReaPlace_CName,ReaPlace_Id';
		url += '&where=reaplace.Visible=1 and reaplace.ReaStorage.Id=' + StorageID + " and reaplace.ReaStorage.Visible=1";
		JShell.Server.get(url, function(data) {
			if (data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**获取货架列表*/
	getPlaceData: function(list) {
		var me = this,
			data = [];
		for (var i in list) {
			var obj = list[i];
			data.push([obj.ReaPlace_Id, obj.ReaPlace_CName]);
		}
		return data;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		return items;
	},
	/**勾选货品赋值*/
	setStoragePlace: function(PlaceID, PlaceCName) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error('请选择需要设置库房货架的数据');
			return;
		}
		me.StorageArr = [];
		me.PlaceArr = [];
		var len = records.length;
		me.PlaceArr.push({
			PlaceID: PlaceID,
			PlaceCName: PlaceCName
		});
		for (var i = 0; i < len; i++) {
			records[i].set('ReaBmsTransferDtl_DPlaceID', PlaceID);
			records[i].set('ReaBmsTransferDtl_DPlaceName', PlaceCName);
		}
	},
	/***
	 * 根据本次入库数量计算总计金额
	 */
	setSumTotal: function(goodsQty, record) {
		var me = this;
		var price = record.get('ReaBmsTransferDtl_Price');
		if (!price) price = 0;
		var sumTotal = Number(goodsQty) * Number(price);
		record.set('ReaBmsTransferDtl_SumTotal', sumTotal);
	},
	/***
	 * 计算货品总额
	 */
	getSumTotal: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var count = 0;
		for (var i = 0; i < len; i++) {
			var SumTotal = records[i].get('ReaBmsTransferDtl_SumTotal');
			if (!SumTotal) SumTotal = 0;

			count += Number(SumTotal);
		}
		return count;
	},
	onSetQty: function(record) {
		var me = this;
		//出库数量+1
		var goodsQty = record.get('ReaBmsTransferDtl_GoodsQty');
		if (!goodsQty) goodsQty = 0;
		goodsQty = Number(goodsQty) + 1;
		var GoodsCName = record.get('ReaBmsTransferDtl_GoodsCName');
		//现有库存量
		var defaulteGoodsQty = record.get('ReaBmsTransferDtl_SumCurrentQty');
		if (!defaulteGoodsQty) defaulteGoodsQty = 0;
		defaulteGoodsQty = Number(defaulteGoodsQty);
		if (defaulteGoodsQty < goodsQty) {
			JShell.Msg.alert('产品名称:【' + GoodsCName + '】的移库数量不能不能大于现有库存量', null, 2000);
			return;
		}
		var Price = record.get('ReaBmsTransferDtl_Price');
		if (!Price) Price = 0;
		var SumTotal = Number(Price) * goodsQty;
		record.set('ReaBmsTransferDtl_GoodsQty', goodsQty);
		record.set('ReaBmsTransferDtl_SumTotal', SumTotal);
		me.getSelectionModel().selectAll();
	},
	/**
	 * @判断选择的库存货品是否存在移库明细里
	 * */
	doesIsExistRec: function(rec) {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var isExist = true,
			record = null;
		var ReaBmsQtyDtlTab = rec.get('ReaBmsQtyDtl_Tab');
		//根据供应商+货品+批号+库房+货架判断
		for (var i = 0; i < len; i++) {
			var ReaBmsOutDtlTab = records[i].get('ReaBmsTransferDtl_Tab');
			if (ReaBmsOutDtlTab == ReaBmsQtyDtlTab) {
				record = records[i];
				isExist = false;
				break;
			}
		}
		if (isExist) record = null;
		return record;
	},
	changeJObjectBarCode: function(JObjectBarCode, obj, rec) {
		var me = this;
		var UsePackSerial = JObjectBarCode ? JObjectBarCode.UsePackSerial : rec.get('ReaBmsQtyDtl_UsePackSerial');
		var GoodsID = JObjectBarCode ? JObjectBarCode.GoodsID : rec.get('ReaBmsQtyDtl_GoodsID');
		var GoodsCName = JObjectBarCode ? JObjectBarCode.GoodsCName : rec.get('ReaBmsQtyDtl_GoodsName');
		var ReaGoodsNo = JObjectBarCode ? JObjectBarCode.ReaGoodsNo : rec.get('ReaBmsQtyDtl_ReaGoodsNo');
		var ProdGoodsNo = JObjectBarCode ? JObjectBarCode.ProdGoodsNo : rec.get('ReaBmsQtyDtl_ProdGoodsNo');
		var BarCodeType = JObjectBarCode ? JObjectBarCode.BarCodeType : rec.get('ReaBmsQtyDtl_BarCodeType');
		var GoodsUnit = JObjectBarCode ? JObjectBarCode.GoodsUnit : rec.get('ReaBmsQtyDtl_GoodsUnit');
		var UnitMemo = JObjectBarCode ? JObjectBarCode.UnitMemo : rec.get('ReaBmsQtyDtl_UnitMemo');
		var GonvertQty = JObjectBarCode ? JObjectBarCode.GonvertQty : rec.get('ReaBmsQtyDtl_GonvertQty');
		var Price = JObjectBarCode ? JObjectBarCode.Price : rec.get('ReaBmsQtyDtl_Price');
		var QtyDtlID = JObjectBarCode ? JObjectBarCode.QtyDtlID : rec.get('ReaBmsQtyDtl_Id');

		obj.ReaBmsTransferDtl_UsePackSerial = UsePackSerial;
		obj.ReaBmsTransferDtl_GoodsID = GoodsID;
		obj.ReaBmsTransferDtl_GoodsCName = GoodsCName;
		obj.ReaBmsTransferDtl_ReaGoodsNo = ReaGoodsNo;
		obj.ReaBmsTransferDtl_ProdGoodsNo = ProdGoodsNo;
		obj.ReaBmsTransferDtl_BarCodeType = BarCodeType;
		obj.ReaBmsTransferDtl_GoodsUnit = GoodsUnit;
		obj.ReaBmsTransferDtl_UnitMemo = UnitMemo;
		obj.ReaBmsTransferDtl_GonvertQty = GonvertQty;
		obj.ReaBmsTransferDtl_Price = Price;
		obj.ReaBmsTransferDtl_QtyDtlID = QtyDtlID;
		obj.ReaBmsTransferDtl_DefaulteGoodsID = GoodsID;
		obj.ReaBmsTransferDtl_BarCodeQtyDtlID = QtyDtlID;
		obj.ReaBmsTransferDtl_SQtyDtlID = QtyDtlID;
		return obj;
	},
	createUnit: function(obj) {
		var me = this;
		var UnitTabobj = {
			GoodsUnit: obj.ReaBmsTransferDtl_GoodsUnit,
			GoodsQty: obj.ReaBmsTransferDtl_GoodsQty,
			Price: obj.ReaBmsTransferDtl_Price,
			ReaGoodsNo: obj.ReaBmsTransferDtl_ReaGoodsNo,
			GoodsName: obj.ReaBmsTransferDtl_GoodsName,
			RegistNo: obj.ReaBmsTransferDtl_RegistNo,
			UnitMemo: obj.ReaBmsTransferDtl_UnitMemo,
			GoodsID: obj.ReaBmsTransferDtl_GoodsID,
			ProdGoodsNo: obj.ReaBmsTransferDtl_ProdGoodsNo,
			GoodsNo: obj.ReaBmsTransferDtl_GoodsNo,
			SumTotal: obj.ReaBmsTransferDtl_SumTotal
		}
		return UnitTabobj;
	},
	createObj: function(obj, rec, UnitArr, UnitTabobj, JObjectBarCode) {
		var me = this;
		obj.ReaBmsTransferDtl_Tab = rec.get('ReaBmsQtyDtl_Tab'),
			obj.ReaBmsTransferDtl_GoodsNo = rec.get('ReaBmsQtyDtl_GoodsNo'),
			obj.ReaBmsTransferDtl_LotNo = rec.get('ReaBmsQtyDtl_LotNo'),
			obj.ReaBmsTransferDtl_InvalidDate = rec.get('ReaBmsQtyDtl_InvalidDate'),
			obj.ReaBmsTransferDtl_ReaCompanyName = rec.get('ReaBmsQtyDtl_CompanyName'),
			obj.ReaBmsTransferDtl_ReaCompanyID = rec.get('ReaBmsQtyDtl_ReaCompanyID'),
			obj.ReaBmsTransferDtl_RegisterNo = rec.get('ReaBmsQtyDtl_RegisterNo'),
			obj.ReaBmsTransferDtl_SStorageName = rec.get('ReaBmsQtyDtl_StorageName'),
			obj.ReaBmsTransferDtl_SPlaceName = rec.get('ReaBmsQtyDtl_PlaceName'),
			obj.ReaBmsTransferDtl_SStorageID = rec.get('ReaBmsQtyDtl_StorageID'),
			obj.ReaBmsTransferDtl_SPlaceID = rec.get('ReaBmsQtyDtl_PlaceID'),
			obj.ReaBmsTransferDtl_SumCurrentQty = rec.get('ReaBmsQtyDtl_GoodsQty'),
			obj.ReaBmsTransferDtl_TaxRate = rec.get('ReaBmsQtyDtl_TaxRate'),
			obj.ReaBmsTransferDtl_CompGoodsLinkID = rec.get('ReaBmsQtyDtl_CompGoodsLinkID'),
			obj.ReaBmsTransferDtl_ReaServerCompCode = rec.get('ReaBmsQtyDtl_ReaServerCompCode'),
			obj.ReaBmsTransferDtl_SysLotSerial = rec.get('ReaBmsQtyDtl_SysLotSerial'),
			obj.ReaBmsTransferDtl_LotSerial = rec.get('ReaBmsQtyDtl_LotSerial'),
			obj.ReaBmsTransferDtl_GoodsSerial = rec.get('ReaBmsQtyDtl_GoodsSerial'),
			obj.ReaBmsTransferDtl_CenOrgGoodsNo = rec.get('ReaBmsQtyDtl_CenOrgGoodsNo'),
			obj.ReaBmsTransferDtl_LotQRCode = rec.get('ReaBmsQtyDtl_LotQRCode'),
			obj.ReaBmsTransferDtl_ReaCompCode = rec.get('ReaBmsQtyDtl_ReaCompCode'),
			obj.ReaBmsTransferDtl_GoodsSort = rec.get('ReaBmsQtyDtl_GoodsSort'),
			obj.ReaBmsTransferDtl_DefaulteLotNo = rec.get('ReaBmsQtyDtl_LotNo'),
			obj.ReaBmsTransferDtl_ProdDate = rec.get('ReaBmsQtyDtl_ProdDate'),
			obj.ReaBmsTransferDtl_DStorageName = me.DStorageObj.StorageName,
			obj.ReaBmsTransferDtl_DStorageID = me.DStorageObj.StorageID,
			obj.ReaBmsQtyDtl_GoodsUnitTab = Ext.encode(UnitTabobj)

		if (obj.ReaBmsTransferDtl_BarCodeType == '1') {
			var CurReaGoodsScanCodeList = [];
			CurReaGoodsScanCodeList.push(JObjectBarCode);
			if (JObjectBarCode) obj.ReaBmsTransferDtl_CurReaGoodsScanCodeList = Ext.encode(CurReaGoodsScanCodeList);
		}
		if (me.defalutPlace.PlaceID) {
			obj.ReaBmsTransferDtl_DPlaceID = me.defalutPlace.PlaceID;
			obj.ReaBmsTransferDtl_DPlaceName = me.defalutPlace.PlaceName;
		}
		return obj;
	},
	createScanCode: function(obj, barcode) {
		var me = this;
		/**移库扫码模式(严格模式:1,混合模式：2)*/
		if (me.TransferScanCode == '1') {
			var GoodsQty = 0;
			if (barcode) GoodsQty = 1;
			obj.ReaBmsTransferDtl_GoodsQty = GoodsQty;
		}
		return obj;
	},
	/**移库新增行*/
	createRowObj: function(rec, UnitArr, barcode) {
		var me = this;
		var JObjectBarCode = rec.get('ReaBmsQtyDtl_JObjectBarCode');
		if (JObjectBarCode) var JObjectBarCode = Ext.JSON.decode(JObjectBarCode);
		var obj = {};
		obj = me.changeJObjectBarCode(JObjectBarCode, obj, rec);
		obj.ReaBmsTransferDtl_GoodsQty = 1;
		if (!obj.ReaBmsTransferDtl_Price) obj.ReaBmsTransferDtl_Price = 0;
		var SumTotal = Number(obj.ReaBmsTransferDtl_Price) * obj.ReaBmsTransferDtl_GoodsQty;
		obj.ReaBmsTransferDtl_SumTotal = SumTotal;
		//临时变量
		var UnitTabobj = me.createUnit(obj);
		/**移库扫码模式(严格模式:1,混合模式：2)*/
		obj = me.createScanCode(obj, barcode);
		obj = me.createObj(obj, rec, UnitArr, UnitTabobj, JObjectBarCode);
		return obj;
	},
	/**获取本次扫码记录数组*/
	getScanCodeList: function(record) {
		var me = this;
		var ScanCodeList = record.get('ReaBmsTransferDtl_CurReaGoodsScanCodeList');
		if (!ScanCodeList) var CurScanCodeList = [];
		if (ScanCodeList) var CurScanCodeList = Ext.JSON.decode(ScanCodeList);
		return CurScanCodeList;
	},
	/**
	 * 盒条码扫码时,检查当次扫码是否已经存在移移库明细列表里
	 * @param {Object} record
	 * @param {Object} rec
	 * @param {Object} barcode
	 */
	checkBarCode: function(record, rec, barcode) {
		var me = this;
		var isExec = true;
		var codeList = me.getScanCodeList(record);
		var jObjectBarCode = rec.get('ReaBmsQtyDtl_JObjectBarCode');
		if (jObjectBarCode) jObjectBarCode = Ext.decode(jObjectBarCode);
		//0 批条码，1盒条码
		var barCodeType = record.get('ReaBmsTransferDtl_BarCodeType');
		//判断盒条码是否已经存在
		if (barCodeType == '1') {
			for (var i = 0; i < codeList.length; i++) {
				if (barcode == codeList[i].UsePackSerial || barcode == codeList[i].SysPackSerial) {
					isExec = false;
					if (me.isScanCodeShowDtl == true) {
						/**
						 * 2.直接移库出库重复扫码时的提示处理调整
						 * 在直接移库登记时,当移库货品出现重复扫码时,提示信息调整为在右下角弹出提示后在5秒后自动关闭,
						 * 在弹出提示信息时,不影响继续进行移库扫码操作;
						 */
						var info = {
							"CName": rec.get('ReaBmsQtyDtl_GoodsName'),
							"BarCode": barcode,
							"Info": "已扫码,请不要重复扫码!",
						};
						//重置消息框的消失隐藏时间
						me.hideTimes = 6000;
						me.fireEvent('onScanCodeShowDtl', me, info);
					} else {
						var info = "条码为:" + barcode + "已扫码,请不要重复扫码!";
						JShell.Msg.alert(info, null, 2000);
					}
					return;
				}
			}
		}
		//console.log(isExec);
		if (isExec == true) {
			//批条码扫码多次
			if (barCodeType == '0' && barcode) {
				me.onSetQty(record);
			} else if (barcode && barCodeType == '1') {
				var barcodeCodeList = me.getScanCodeList(record);
				for (var i = 0; i < barcodeCodeList.length; i++) {
					if (barcode == barcodeCodeList[i].UsePackSerial || barcode == barcodeCodeList[i].SysPackSerial) {
						return;
					}
				}
				var goodsQty = record.get('ReaBmsTransferDtl_GoodsQty');
				if (!goodsQty) goodsQty = 0;
				goodsQty = Number(goodsQty);
				if (!barcodeCodeList) barcodeCodeList = [];
				if (jObjectBarCode) barcodeCodeList.push(jObjectBarCode);
				record.set('ReaBmsTransferDtl_CurReaGoodsScanCodeList', Ext.encode(barcodeCodeList));
				var scanCodeList = me.getScanCodeList(record);
				//当次扫码记录为空，把条码添加到行
				if (scanCodeList.length == goodsQty) return;
				me.onSetQty(record);
			}
			me.getSelectionModel().selectAll();
			if (record && !barcode) {
				JShell.Msg.alert('当前行数据已选择', null, 2000);
				return;
			}
		}
	},
	/**找到行新增和删除，对外公开*/
	onAddOne: function(rec, barcode, UnitArr) {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		//根据供应商+货品+批号+库房+货架判断
		var record = me.doesIsExistRec(rec);
		if (record) {
			me.checkBarCode(record, rec, barcode);
		} else if (!record) {
			var obj = me.createRowObj(rec, UnitArr, barcode);
			me.store.insert(me.store.getCount(), obj);
			me.fireEvent('changeSumTotal');
			me.getSelectionModel().selectAll();
		}
	},
	/**根据供应商+货品+批号+库房+货架，查库存量*/
	getGoodsQty: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		for (var i = 0; i < len; i++) {
			var rec = records[i];
			me.getQtyCount(rec);
		}
	},
	getSumQtyObj: function(rec) {
		var me = this;
		var GoodsID = rec.get('ReaBmsTransferDtl_GoodsID');
		var LotNo = rec.get('ReaBmsTransferDtl_LotNo');
		var ReaCompanyID = rec.get('ReaBmsTransferDtl_ReaCompanyID');
		var StorageID = rec.get('ReaBmsTransferDtl_SStorageID');
		var PlaceID = rec.get('ReaBmsTransferDtl_SPlaceID');
		var InvalidDate = rec.get('ReaBmsTransferDtl_InvalidDate');
		var ReaGoodsNo = rec.get('ReaBmsTransferDtl_ReaGoodsNo');
		var QtyDtlID = rec.get('ReaBmsTransferDtl_QtyDtlID');
		var BarCodeQtyList = me.getScanCodeList(rec);
		var obj = {
			ReaGoodsNo: ReaGoodsNo,
			GoodsID: GoodsID,
			LotNo: LotNo,
			ReaCompanyID: ReaCompanyID,
			StorageID: StorageID,
			PlaceID: PlaceID,
			InvalidDate: InvalidDate
		};
		if (BarCodeQtyList.length > 0) obj.QtyDtlID = QtyDtlID;
		return obj;
	},
	getSumQtyObj2: function(entity) {
		var me = this;
		var GoodsID = entity.ReaBmsTransferDtl_GoodsID;
		var ReaGoodsNo = entity.ReaBmsTransferDtl_ReaGoodsNo;
		var LotNo = entity.ReaBmsTransferDtl_LotNo;
		var ReaCompanyID = entity.ReaBmsTransferDtl_ReaCompanyID;
		var StorageID = entity.ReaBmsTransferDtl_SStorageID;
		var PlaceID = entity.ReaBmsTransferDtl_SPlaceID;
		var InvalidDate = entity.ReaBmsTransferDtl_InvalidDate;
		var obj = {
			ReaGoodsNo: ReaGoodsNo,
			LotNo: LotNo,
			ReaCompanyID: ReaCompanyID,
			StorageID: StorageID,
			PlaceID: PlaceID,
			InvalidDate: InvalidDate,
			GoodsID: GoodsID
		};
		return obj;
	},
	/**
	 * @description 获取移库货品当前的库存数的Url
	 * */
	getSumQtyUrl: function(obj) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectStoreUrl;
		var qtyHql = "reabmsqtydtl.ReaGoodsNo='" + obj.ReaGoodsNo + "'" +
			" and reabmsqtydtl.LotNo='" + obj.LotNo + "'" +
			" and reabmsqtydtl.ReaCompanyID='" + obj.ReaCompanyID + "'" +
			" and reabmsqtydtl.StorageID='" + obj.StorageID + "'" +
			" and reabmsqtydtl.GoodsQty>0";

		if (obj.PlaceID) {
			qtyHql += " and reabmsqtydtl.PlaceID='" + obj.PlaceID + "'";
		}
		var dtlHql = "reabmstransferdtl.ReaGoodsNo='" + obj.ReaGoodsNo + "'" +
			" and reabmstransferdtl.LotNo='" + obj.LotNo + "'" +
			" and reabmstransferdtl.ReaCompanyID='" + obj.ReaCompanyID + "'" +
			" and reabmstransferdtl.SStorageID='" + obj.StorageID + "'" +
			" and reabmstransferdtl.GoodsQty>0";
		if (obj.PlaceID) {
			dtlHql += " and reabmstransferdtl.SPlaceID='" + obj.PlaceID + "'";
		}
		qtyHql=JShell.String.encode(qtyHql);
		dtlHql=JShell.String.encode(dtlHql);
		url += '?dtlType=ReaBmsTransferDtl&qtyHql=' + qtyHql + '&dtlHql=' + dtlHql + '&goodsId=' + obj.GoodsID;
		return url;
	},
	/**
	 * @description 获取移库货品当前的库存数
	 * */
	getQtyCount: function(rec) {
		var me = this;
		var obj = me.getSumQtyObj(rec);
		var url = me.getSumQtyUrl(obj);
		JShell.Server.get(url, function(data) {
			if (data.success) {
				if (!data.value) return;
				me.setSumQty(rec, data.value);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**
	 * @description 设置当前库存数及已申请数
	 * */
	setSumQty: function(rec, list) {
		var me = this;
		//当前库存
		var sumCurrentQty = list.SumCurrentQty;
		//已申请数
		var sumDtlGoodsQty = list.SumDtlGoodsQty;
		if (!sumCurrentQty) sumCurrentQty = 0;
		if (!sumDtlGoodsQty) sumDtlGoodsQty = 0;
		sumCurrentQty = parseFloat(sumCurrentQty);
		sumDtlGoodsQty = parseFloat(sumDtlGoodsQty);
		rec.set('ReaBmsTransferDtl_SumCurrentQty', sumCurrentQty); //当前库存数
		rec.set('ReaBmsTransferDtl_SumDtlGoodsQty', sumDtlGoodsQty); //已申请数
		rec.set('ReaBmsTransferDtl_DefaulteGoodsQty2', sumCurrentQty); //原始库存数
		rec.commit();
	},
	getAddQtyCount: function(entity, callback) {
		var me = this;
		var obj = me.getSumQtyObj2(entity);
		var url = me.getSumQtyUrl(obj);
		JShell.Server.get(url, function(data) {
			if (data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**目标库房改变后改变库房和货架*/
	onChangeStorageAndPlace: function(DStorageObj) {
		var me = this;
		var records = me.store.data.items,
			len = records.length;
		if (me.PK && me.isLoad) return;
		for (var i = 0; i < len; i++) {
			records[i].set('ReaBmsTransferDtl_DStorageName', DStorageObj.StorageName);
			records[i].set('ReaBmsTransferDtl_DStorageID', DStorageObj.StorageID);
			records[i].set('ReaBmsTransferDtl_DPlaceName', me.defalutPlace.PlaceName);
			records[i].set('ReaBmsTransferDtl_DPlaceID', me.defalutPlace.PlaceID);
		}
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		me.isLoad = true;
		me.load(null, true, autoSelect);
	},
	/**判断批条码是否需要调用扫码服务*/
	getLotNoIsScanCode: function(barcode, grid) {
		var me = this;
		//批条码处理,根据条码号先从明细找，如果已存在相同批条码 不再去调用条码扫码服务
		var records = me.store.data.items,
			len = records.length;
		var isExec = false;
		//库存表是否有数据
		var len2 = grid.getStore().getCount();
		for (var i = 0; i < len; i++) {
			//条码类型,0批条码，1盒条码
			var BarCodeType = records[i].get('ReaBmsTransferDtl_BarCodeType');
			var LotSerial = records[i].get('ReaBmsTransferDtl_LotSerial');
			var SysLotSerial = records[i].get('ReaBmsTransferDtl_SysLotSerial');
			if (BarCodeType == '0' && len2 > 0) {
				if (barcode == LotSerial || barcode == SysLotSerial) {
					isExec = true;
					me.onSetQty(records[i]);
					continue;
				}
			}
		}
		return isExec;
	},
	showMemoText: function(value, meta, record) {
		var me = this;
		var v = "" + value;
		if (v.length > 0) v = (v.length > 20 ? v.substring(0, 20) : v);
		if (value.length > 20) {
			v = v + "...";
		}
		return v
	},
	/**
	 * 保存校验
	 * 移库数量，不能为0
	 */
	onSaveCheck: function() {},
	getEntity: function(rec) {
		var me = this;
		var GoodsUnit = rec.get('ReaBmsTransferDtl_GoodsUnit');
		var ReaCompanyID = rec.get('ReaBmsTransferDtl_ReaCompanyID');
		var CompanyName = rec.get('ReaBmsTransferDtl_ReaCompanyName');
		var TaxRate = rec.get('ReaBmsTransferDtl_TaxRate');
		var LotNo = rec.get('ReaBmsTransferDtl_LotNo');
		var GoodsID = rec.get('ReaBmsTransferDtl_GoodsID');
		var GoodsCName = rec.get('ReaBmsTransferDtl_GoodsCName');
		var QtyDtlID = rec.get('ReaBmsTransferDtl_QtyDtlID');
		var SStorageID = rec.get('ReaBmsTransferDtl_SStorageID');
		var SPlaceID = rec.get('ReaBmsTransferDtl_SPlaceID');
		var SStorageName = rec.get('ReaBmsTransferDtl_SStorageName');
		var SPlaceName = rec.get('ReaBmsTransferDtl_SPlaceName');
		var DStorageID = rec.get('ReaBmsTransferDtl_DStorageID');
		var DStorageName = rec.get('ReaBmsTransferDtl_DStorageName');
		var DPlaceName = rec.get('ReaBmsTransferDtl_DPlaceName');
		var DPlaceID = rec.get('ReaBmsTransferDtl_DPlaceID');
		var GoodsSerial = rec.get('ReaBmsTransferDtl_GoodsSerial');
		var SysLotSerial = rec.get('ReaBmsTransferDtl_SysLotSerial');
		var LotSerial = rec.get('ReaBmsTransferDtl_LotSerial');
		var Memo = rec.get('ReaBmsTransferDtl_Memo');
		var GoodsNo = rec.get('ReaBmsTransferDtl_GoodsNo');
		var CompGoodsLinkID = rec.get('ReaBmsTransferDtl_CompGoodsLinkID');
		var ReaServerCompCode = rec.get('ReaBmsTransferDtl_ReaServerCompCode');
		var BarCodeType = rec.get('ReaBmsTransferDtl_BarCodeType');
		var InvalidDate = rec.get('ReaBmsTransferDtl_InvalidDate');
		var ProdGoodsNo = rec.get('ReaBmsTransferDtl_ProdGoodsNo');
		var ReaGoodsNo = rec.get('ReaBmsTransferDtl_ReaGoodsNo');
		var CenOrgGoodsNo = rec.get('ReaBmsTransferDtl_CenOrgGoodsNo');
		var LotQRCode = rec.get('ReaBmsTransferDtl_LotQRCode');
		var UnitMemo = rec.get('ReaBmsTransferDtl_UnitMemo');
		var ReaCompCode = rec.get('ReaBmsTransferDtl_ReaCompCode');
		var GoodsSort = rec.get('ReaBmsTransferDtl_GoodsSort');
		var SQtyDtlID = rec.get('ReaBmsTransferDtl_SQtyDtlID');
		var DefaulteGoodsQty = rec.get('ReaBmsTransferDtl_SumCurrentQty');
		var ProdDate = rec.get('ReaBmsTransferDtl_ProdDate');

		var entity = {
			GoodsID: GoodsID,
			GoodsCName: GoodsCName,
			GoodsUnit: GoodsUnit,
			SStorageName: SStorageName,
			SPlaceName: SPlaceName,
			DStorageName: DStorageName,
			DPlaceName: DPlaceName,
			ReaCompanyName: CompanyName,
			ReaServerCompCode: ReaServerCompCode,
			LotNo: LotNo,
			GoodsNo: GoodsNo,
			ProdGoodsNo: ProdGoodsNo,
			GoodsSerial: GoodsSerial,
			SysLotSerial: SysLotSerial,
			LotSerial: LotSerial,
			Memo: Memo,
			Visible: 1,
			CenOrgGoodsNo: CenOrgGoodsNo,
			ReaGoodsNo: ReaGoodsNo,
			LotQRCode: LotQRCode,
			UnitMemo: UnitMemo,
			ReaCompCode: ReaCompCode,
			GoodsSort: GoodsSort,
			DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
		}
		if (DefaulteGoodsQty) {
			entity.ReqCurrentQty = DefaulteGoodsQty;
		}
		var Id = rec.get('ReaBmsTransferDtl_Id');
		if (Id) entity.Id = Id;
		var price = rec.get('ReaBmsTransferDtl_Price');
		if (!price) price = 0;
		price = parseFloat(price);
		entity.Price = 0;
		var reqGoodsQty = rec.get('ReaBmsTransferDtl_ReqGoodsQty');
		if (!reqGoodsQty) reqGoodsQty = 0;
		reqGoodsQty = parseFloat(reqGoodsQty);
		entity.ReqGoodsQty = reqGoodsQty;
		var goodsQty = rec.get('ReaBmsTransferDtl_GoodsQty');
		if (!goodsQty) goodsQty = 0;
		reqGoodsQty = parseFloat(reqGoodsQty);
		goodsQty = parseFloat(goodsQty);
		entity.GoodsQty = goodsQty;

		if (ReaCompanyID) entity.ReaCompanyID = ReaCompanyID;
		if (SStorageID) entity.SStorageID = SStorageID;
		if (DStorageID) entity.DStorageID = DStorageID;
		if (SPlaceID) entity.SPlaceID = SPlaceID;
		if (DPlaceID) entity.DPlaceID = DPlaceID;
		if (TaxRate) entity.TaxRate = TaxRate;
		if (CompGoodsLinkID) entity.CompGoodsLinkID = CompGoodsLinkID;
		if (BarCodeType) entity.BarCodeType = BarCodeType;
		if (InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);
		if (ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (userId) {
			entity.CreaterID = userId;
			entity.CreaterName = userName;
		}
		var reaBmsInDtlLink = [];
		//扫码明细
		var scanCodeList = rec.get('ReaBmsTransferDtl_CurReaGoodsScanCodeList');
		if (scanCodeList.length > 0) {
			reaBmsInDtlLink = Ext.JSON.decode(scanCodeList);
		}
		var barCodeQtyDtlID = rec.get('ReaBmsTransferDtl_BarCodeQtyDtlID');
		//盒条码并扫码时记录原库存id
		if (BarCodeType == '1' && barCodeQtyDtlID) {
			entity.SQtyDtlID = barCodeQtyDtlID;
		}
		entity.ReaBmsTransferDtlLinkList = reaBmsInDtlLink;
		return entity;
	}
});
