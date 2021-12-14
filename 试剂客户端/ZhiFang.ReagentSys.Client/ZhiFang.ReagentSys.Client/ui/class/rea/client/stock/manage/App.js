/**
 * 入库管理
 * 提取物资接口的出库信息进行入库确认处理
 * 将提取的物资接口的出库信息的入库单进行退库操作?
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.stock.manage.App', {
	extend: 'Ext.panel.Panel',

	title: '入库管理',
	header: false,
	border: false,
	layout: {
		type: 'border'
	},
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	/**@description 新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.DocGrid.on({
			select: function(RowModel, record) {
				me.isShow(record);
			},
			nodata: function(p) {
				me.nodata();
			},
			onReturnLibrary: function(p, record) {
				me.onReturnLibrary(record);
			}
		});
		me.ShowPanel.on({
			onLaunchFullScreen: function() {
				me.DocGrid.collapse();
			},
			onExitFullScreen: function() {
				me.DocGrid.expand();
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
		me.DocGrid = Ext.create('Shell.class.rea.client.stock.manage.DocGrid', {
			header: false,
			title: '入库主单',
			itemId: 'DocGrid',
			region: 'west',
			width: 360,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.ShowPanel = Ext.create('Shell.class.rea.client.stock.manualinput.ShowPanel', {
			header: false,
			itemId: 'ShowPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.DocGrid, me.ShowPanel];
		return appInfos;
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
	clearData: function() {
		var me = this;
	},
	nodata: function(record) {
		var me = this;
		me.setFormType("show");
		me.ShowPanel.clearData();
		me.clearData();
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.ShowPanel.formtype = formtype;
		me.ShowPanel.DocForm.formtype = formtype;
		me.ShowPanel.DtlPanel.formtype = formtype;
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.ShowPanel.isShow(record, me.DocGrid);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.ShowPanel.DocForm.getForm().getValues();
		var entity = {
			Id: values.ReaBmsInDoc_Id,
			Status: values.ReaBmsInDoc_Status,
			InDocNo: values.ReaBmsInDoc_InDocNo,
			Carrier: values.ReaBmsInDoc_Carrier,
			InType: values.ReaBmsInDoc_InType,
			CreaterName: values.ReaBmsInDoc_CreaterName,
			InvoiceNo: values.ReaBmsInDoc_InvoiceNo,
			OtherDocNo: values.ReaBmsInDoc_OtherDocNo,
			ZX1: values.ReaBmsInDoc_ZX1,
			ZX2: values.ReaBmsInDoc_ZX2,
			ZX3: values.ReaBmsInDoc_ZX3
		};
		entity.Memo = values.ReaBmsInDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		if(values.ReaBmsInDoc_SourceType) entity.SourceType = values.ReaBmsInDoc_SourceType;
		if(values.ReaBmsInDoc_SaleDocID) entity.SaleDocID = values.ReaBmsInDoc_SaleDocID;
		if(values.ReaBmsInDoc_CreaterID) entity.CreaterID = values.ReaBmsInDoc_CreaterID;
		if(values.ReaBmsInDoc_OperDate) entity.OperDate = JShell.Date.toServerDate(values.ReaBmsInDoc_OperDate);
		if(values.ReaBmsInDoc_TotalPrice) entity.TotalPrice = parseFloat(values.ReaBmsInDoc_TotalPrice);
		return {
			entity: entity
		};
	},
	/**@description 退库处理*/
	onReturnLibrary: function(record) {
		var me = this;

		var otherDocNo = record.get("ReaBmsInDoc_OtherDocNo");
		if(!otherDocNo) {
			JShell.Msg.alert("获取入库提取单号信息为空", null, 2000);
			return;
		}
		var url = '/ReaManageService.svc/RS_UDTO_ReaGoodsBackStorageInterface';
		var docEntity = me.getAddParams();
		if(!docEntity.entity) {
			JShell.Msg.error("获取封装退库信息为空,不能调用奶库接口!");
			return;
		}

		var entity = docEntity.entity;
		var dtAddList = [];
		var dtAddList = me.getSaveDtlAddList(false);
		if(!dtAddList) dtAddList = [];

		var params = {
			"inDoc": entity,
			"inDtlList": dtAddList
		};

		params = Ext.JSON.encode(params);
		if(!params) {
			JShell.Msg.error("封装退库信息出错,不能退库!");
			return;
		}
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		me.showMask(me.ShowPanel.DocForm.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			data = Ext.JSON.decode(data.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, ''));
			//console.log("data:"+Ext.JSON.encode(data));
			if(data.success) {
				JShell.Msg.alert("退库操作成功!", null, 2000);
			} else {
				JShell.Msg.error(data.message);
			}
		}, true, null, true);
	},
	/**@description 获取单个的验收明细的基本封装信息*/
	getSaveDtlAddList: function(isVo) {
		var me = this;
		var dtAddList = [];
		var operationAddList = [];
		var totalPrice = 0;
		me.ShowPanel.DtlPanel.InDtlGrid.store.each(function(record) {
			operationAddList = [];
			totalPrice += parseFloat(record.get("ReaBmsInDtl_SumTotal"));

			var id = record.get(me.ShowPanel.DtlPanel.InDtlGrid.PKField);
			var obj = me.getSaveDtlOne(record);

			var BarCodeType = record.get("ReaBmsInDtl_BarCodeType");
			if(BarCodeType == "1") {
				//当次入库的所有扫码记录集合
				var otherSerialList = record.get("ReaBmsInDtl_OtherSerialNoStr");
				if(otherSerialList) otherSerialList = otherSerialList.split(';');
				//封装当次的条码操作记录
				if(otherSerialList && otherSerialList.length > 0) {
					Ext.Array.each(otherSerialList, function(otherSerial, index1) {
						var operationObj = {
							"Id": "-1",
							"BDocNo": "",
							"BDocID": "-1",
							"BDtlID": "-1",
							"OperTypeID": "1", //验货入库
							"SysPackSerial": otherSerial,
							"OtherPackSerial": otherSerial,
							"UsePackSerial": otherSerial
						};
						operationAddList.push(operationObj);
					});
				}
			}
			if(isVo == true) {
				var objvo = {
					"ReaBmsInDtl": obj, //入库明细信息
					"BarCodeType": BarCodeType,
					"ReaBmsInDtlLinkList": operationAddList //当次新增的入库条码扫码操作集合
				};
				dtAddList.push(objvo);
			} else {
				dtAddList.push(obj);
			}
		});
		return dtAddList;
	},
	/**@description 获取单个的验收明细的基本封装信息*/
	getSaveDtlOne: function(record) {
		var me = this;
		var id = record.get(me.ShowPanel.DtlPanel.InDtlGrid.PKField);
		if(!id) id = -1;

		var entity = {
			Id: record.get("ReaBmsInDtl_Id"),
			BarCodeType: record.get("ReaBmsInDtl_BarCodeType"),
			InDtlNo: record.get("ReaBmsInDtl_InDtlNo"),
			//InDocNo: record.get("ReaBmsInDtl_InDocNo"),
			GoodsCName: record.get("ReaBmsInDtl_GoodsCName"),
			GoodsUnit: record.get("ReaBmsInDtl_GoodsUnit"),

			LotNo: record.get("ReaBmsInDtl_LotNo"),
			StorageName: record.get("ReaBmsInDtl_StorageName"),
			PlaceName: record.get("ReaBmsInDtl_PlaceName"),
			CompanyName: record.get("ReaBmsInDtl_CompanyName"),
			BiddingNo: record.get("ReaBmsInDtl_BiddingNo"),

			ApproveDocNo: record.get("ReaBmsInDtl_ApproveDocNo"),
			GoodsSerial: record.get("ReaBmsInDtl_GoodsSerial"),
			LotSerial: record.get("ReaBmsInDtl_LotSerial"),
			SysLotSerial: record.get("ReaBmsInDtl_SysLotSerial"),
			RegisterNo: record.get("ReaBmsInDtl_RegisterNo"),

			ReaServerCompCode: record.get("ReaBmsInDtl_ReaServerCompCode"),
			Memo: record.get("ReaBmsInDtl_Memo"),
			ReaGoodsNo: record.get("ReaBmsInDtl_ReaGoodsNo"),
			ProdGoodsNo: record.get("ReaBmsInDtl_ProdGoodsNo"),
			CenOrgGoodsNo: record.get("ReaBmsInDtl_CenOrgGoodsNo"),
			GoodsNo: record.get("ReaBmsInDtl_GoodsNo"),
			OtherDtlNo: record.get("ReaBmsInDtl_OtherDtlNo"),
			ZX1: record.get("ReaBmsInDtl_ZX1"),
			ZX2: record.get("ReaBmsInDtl_ZX2"),
			ZX3: record.get("ReaBmsInDtl_ZX3")
		};
		var SaleDtlID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_SaleDtlID");
		if(SaleDtlID) {
			entity.SaleDtlID = SaleDtlID;
		}
		var StorageID = record.get("ReaBmsInDtl_StorageID");
		if(StorageID) {
			entity.StorageID = StorageID;
		}
		var PlaceID = record.get("ReaBmsInDtl_PlaceID");
		if(PlaceID) {
			entity.PlaceID = PlaceID;
		}
		var ReaCompanyID = record.get("ReaBmsInDtl_ReaCompanyID");
		if(ReaCompanyID) {
			entity.ReaCompanyID = ReaCompanyID;
		}
		var CompGoodsLinkID = record.get("ReaBmsInDtl_CompGoodsLinkID");
		if(CompGoodsLinkID) {
			entity.CompGoodsLinkID = CompGoodsLinkID;
		}
		var ReaCompCode = record.get("ReaBmsInDtl_ReaCompCode");
		if(ReaCompCode) {
			entity.ReaCompCode = ReaCompCode;
		}
		var ProdDate = record.get("ReaBmsInDtl_ProdDate");
		var InvalidDate = record.get("ReaBmsInDtl_InvalidDate");
		var RegisterInvalidDate = record.get("ReaBmsInDtl_RegisterInvalidDate");

		if(ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		if(InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);
		if(RegisterInvalidDate) entity.RegisterInvalidDate = JShell.Date.toServerDate(RegisterInvalidDate);

		var GoodsQty = record.get("ReaBmsInDtl_GoodsQty");
		var Price = record.get("ReaBmsInDtl_Price");
		var SumTotal = record.get("ReaBmsInDtl_SumTotal");
		var TaxRate = record.get("ReaBmsInDtl_TaxRate");
		var GoodsSort = record.get("ReaBmsInDtl_GoodsSort");
		if(GoodsSort) {
			entity.GoodsSort = GoodsSort;
		}
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if(Price) Price = parseFloat(Price);
		else Price = 0;
		if(SumTotal) SumTotal = parseFloat(SumTotal);
		else SumTotal = Price * GoodsQty;

		entity.Price = Price;
		entity.GoodsQty = GoodsQty;
		entity.SumTotal = SumTotal;
		if(TaxRate) entity.TaxRate = TaxRate;
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		//入库主单
		if(me.PK) {
			entity.InDocID = me.PK;
		}
		var reaGoodsId = record.get("ReaBmsInDtl_ReaGoods_Id");
		if(reaGoodsId) {
			entity.ReaGoods = {
				Id: reaGoodsId,
				DataTimeStamp: strDataTimeStamp.split(',')
			};
		}
		return entity;
	}
});