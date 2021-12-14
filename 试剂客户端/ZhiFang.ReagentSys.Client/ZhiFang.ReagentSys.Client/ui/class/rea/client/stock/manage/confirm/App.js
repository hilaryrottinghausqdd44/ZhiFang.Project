/**
 * 入库确认
 * @author longfc
 * @version 2019-03-08
 */
Ext.define('Shell.class.rea.client.stock.manage.confirm.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '入库确认',

	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	/**货品明细弹出消息框消失时间*/
	hideTimes: 5000,
	/**是否显示货品信息(双击批号单元格选择时隐藏)*/
	IsShowDtlInfo: true,
	/**扫码模式(严格模式:strict,混合模式：mixing)*/
	CodeScanningMode: "mixing",
	formtype: "",
	/**新增服务地址*/
	addUrl: '',
	
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/RS_UDTO_UpdateReaBmsInDocAndInDtlListByInterface',

	formtype: "edit",
	/**当前选择的主单Id*/
	PK: null,
	/**接口数据是否需要重新生成条码*/
	iSNeedCreateBarCode: "2",
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//盒条码浮动窗体信息关闭
		me.on({
			close: function() {
				me.closeDtlPanel();
			}
		});
		me.DtlGrid.on({
			nodata: function() {
				me.closeDtlPanel();
			},
			onChangeSumTotal: function(grid, totalPrice) {
				me.DocForm.setTotalPrice(totalPrice);
			},
			onScanCodeShowDtl: function(grid, info) {
				me.showDtlPanel(grid, info);
			}
		});
		me.DocForm.on({
			load: function(form, data) {
				if (data && data.value) {

				}
			}
		});
		//接口数据是否需要重新生成条码 1:是;2:否;
		JcallShell.REA.RunParams.getRunParamsValue("InterfaceDataISNeedCreateBarCode", false, function(data) {
			if (data.success) {
				var obj = data.value;
				if (obj.ParaValue) me.iSNeedCreateBarCode = "" + obj.ParaValue;
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocForm = Ext.create('Shell.class.rea.client.stock.manage.confirm.DocForm', {
			title: '入库信息',
			itemId: 'DocGrid',
			region: 'north',
			header: false,
			split: false,
			collapsible: false,
			collapsed: false,
			formtype: "edit",
			PK: me.PK
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.stock.manage.confirm.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			defaultLoad: false,
			split: true,
			collapsible: true,
			collapsed: false,
			PK: me.PK
		});
		var appInfos = [me.DocForm, me.DtlGrid];
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			iconCls: 'button-save',
			itemId: "btnTemp",
			text: '入库暂存',
			tooltip: '入库暂存',
			handler: function() {
				JShell.Action.delay(function() {
					me.onTempSaveClick();
				}, null, 500);
			}
		}, {
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '入库确认',
			tooltip: '入库确认',
			handler: function() {
				JShell.Action.delay(function() {
					me.onConfirmClick();
				}, null, 500);
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	clearData: function() {
		var me = this;
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

		me.DtlGrid.PK = null;
		me.DtlGrid.ReaCompID = null;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.Status = null;
		me.DtlGrid.store.removeAll();
		me.DtlGrid.disableControl();
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isEdit: function(id) {
		var me = this;
		if (id) me.PK = id;
		me.formtype = "edit";

		me.DocForm.PK = me.PK;
		me.DocForm.formtype = "edit";
		me.DocForm.isEdit(me.PK);

		me.DtlGrid.PK = me.PK;
		me.DtlGrid.formtype = "edit";
		//只显示暂存未验收的验收货品明细
		me.DtlGrid.defaultWhere = "reabmsindtl.InDocID=" + me.PK;
		me.DtlGrid.Status = "1";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.enableControl();
		me.DtlGrid.onSearch();
	},
	showDtlPanel: function(grid, info) {
		var me = this;

		var itemId = "stock.manualinput";
		var win = Ext.WindowManager.get(itemId);
		if (!win) {
			var config = {
				title: "货品信息(5秒后会自动隐藏)",
				resizable: false,
				maximizable: false,
				modal: false,
				closable: true, //关闭功能
				draggable: true, //移动功能
				floating: true, //浮动模式
				width: 280,
				height: 380,
				alwaysOnTop: true,
				itemId: itemId,
				id: itemId
			};
			win = JShell.Win.open('Shell.class.rea.client.stock.manualinput.basic.DtlInfo', config);
			Ext.WindowManager.register(win);
		}
		if (win) {
			//WIN宽高、位置
			var winHeight = me.getHeight();
			var winWidth = me.getWidth();
			var zIndex = me.zIndexManager.zseed + 100;
			var position = me.getPosition();
			var winPosition = [position[0] + winWidth - win.width - 20, winHeight - win.height - 25];
			win.initData(info);
			if (grid.getIShowDtlInfoValue() == true) {
				win.showAt(winPosition);
			} else {
				win.hide();
			}
			if (grid.hideTimes && grid.hideTimes > 0) {
				JcallShell.Action.delay(function() {
					win.hide();
				}, null, grid.hideTimes);
			}
		}
	},
	closeDtlPanel: function() {
		var me = this;
		var itemId = "stock.manualinput";
		var win = Ext.WindowManager.get(itemId);
		if (win) win.close();
	},
	onTempSaveClick: function() {
		var me = this;
		me.DocForm.Status = "1";
		me.DtlGrid.Status = "1";
		me.onSave("1", null);
	},
	onConfirmClick: function() {
		var me = this;
		me.DocForm.Status = "2";
		me.DtlGrid.Status = "2";
		me.onSave("2", null);
	},
	onSave: function(status, confirmData) {
		var me = this;
		if (!me.DocForm.getForm().isValid()) return;
		var docEntity = me.formtype == 'add' ? me.DocForm.getAddParams() : me.DocForm.getEditParams();

		if (!docEntity.entity) {
			JShell.Msg.alert("获取封装验入库信息为空", null, 2000);
			return;
		}
		var result = me.validatorSave();
		if (result.isValid == false) {
			JShell.Msg.error(result.info);
			return;
		}
		var entity = docEntity.entity;
		entity.Status = status;
		var dtlInfo = me.getSaveDtlParams();
		entity.TotalPrice = dtlInfo.TotalPrice;

		var params = {
			"entity": entity,
			"iSNeedCreateBarCode":me.iSNeedCreateBarCode
		};
		if (me.formtype == "edit") {
			params.fieldsDtl =
				"Id,GoodsUnit,LotNo,StorageID,PlaceID,StorageName,PlaceName,BiddingNo,ApproveDocNo,LotSerial,RegisterNo,Memo,ProdDate,InvalidDate,RegisterInvalidDate,GoodsQty,Price,SumTotal,TaxRate,ReaGoodsNo,ProdGoodsNo,CenOrgGoodsNo,GoodsNo,FactoryOutTemperature,ArrivalTemperature,AppearanceAcceptance";
			params.fields = docEntity.fields;
			params.inDtlList = dtlInfo.inDtlList;
			if (!params.inDtlList) params.inDtlList = [];
		}

		params = Ext.JSON.encode(params);
		if (!params) {
			JShell.Msg.alert("封装入库信息出错,不能保存!", null, 2000);
			return;
		}
		//是否需要保存后打印
		var IsPrint = me.DtlGrid.getIsPrint();
		var url = JShell.System.Path.ROOT + me.editUrl;
		me.showMask(me.DocForm.saveText); //显示遮罩层
		if (!me.BUTTON_CAN_CLICK) return;
		me.BUTTON_CAN_CLICK = false; //不可点击
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			me.BUTTON_CAN_CLICK = true;
			if (data.success) {
				//先清空列表信息,防止后续处理出现异常,用户又进行第二次保存
				me.DtlGrid.store.removeAll();
				
				me.PK = me.formtype == 'add' ? data.value.id : me.PK;
				if (status == '1') IsPrint = false;
				me.fireEvent('save', me, me.PK, IsPrint);
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.DocForm.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/***
	 * @description 保存前验证
	 */
	validatorSave: function() {
		var me = this;
		if (isValid == true)
			var result = {
				"isValid": true,
				"info": ""
			};
		if (me.DtlGrid.store.getCount() <= 0) {
			result.info = "入库货品明细为空!";
			result.isValid = false;
			return result;
		}
		var info = "";
		me.DtlGrid.store.each(function(record) {
			var LotNo = record.get("ReaBmsInDtlVO_ReaBmsInDtl_LotNo");

			var GoodsQty = record.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty");
			var Price = record.get("ReaBmsInDtlVO_ReaBmsInDtl_Price");
			var SumTotal = record.get("ReaBmsInDtlVO_ReaBmsInDtl_SumTotal");
			var BarCodeType = record.get("ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType");
			var ReaGoodsName = record.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsCName");

			var StorageID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_StorageID");
			var PlaceID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_PlaceID");
			var ReaCompanyID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaCompanyID");
			var ReaGoodsId = record.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_Id");
			var InvalidDate = record.get("ReaBmsInDtlVO_ReaBmsInDtl_InvalidDate");

			if (GoodsQty) GoodsQty = parseFloat(GoodsQty);
			else GoodsQty = 0;

			if (!Price)
				Price = 0;
			Price = parseFloat(Price);

			if (!ReaGoodsId) {
				info = "试剂不能为空!";
				return false;
			}
			if (!GoodsQty || GoodsQty < 0) {
				info = "试剂为" + ReaGoodsName + ",入库数不能为空或小于等于0!";
				return false;
			}
			if (Price < 0) {
				info = "试剂为" + ReaGoodsName + ",单价小于零，不能验收！";
				return false;
			}
			if (!LotNo) {
				info = "试剂为" + ReaGoodsName + ",待入库货品的货品批号为空!";
				return false;
			}
			if (!StorageID) {
				info = "试剂为" + ReaGoodsName + ",库房不能为空!";
				return false;
			}
			if (!ReaCompanyID) {
				info = "试剂为" + ReaGoodsName + ",所属供应商不能为空!";
				return false;
			}
			if (!InvalidDate) {
				info = "试剂为" + ReaGoodsName + ",有效期至不能为空!";
				return false;
			}
			if (BarCodeType == "1") {
				var curReaGoodsScanCodeList = [];
				curReaGoodsScanCodeList = record.get("ReaBmsInDtlVO_CurReaBmsInDtlLinkListStr");
				if (curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);
				//严格模式下
				if (me.CodeScanningMode == "strict") {
					if (curReaGoodsScanCodeList.length <= 0) {
						info = "试剂为" + ReaGoodsName + ",盒条码信息为空!";
						return false;
					}
					if (curReaGoodsScanCodeList.length != GoodsQty) {
						info = "试剂为" + ReaGoodsName + ",入库数与扫码数不相等!";
						return false;
					}
				}
			}
		});
		if (info) {
			result.isValid = false;
			result.info = info;
		}
		return result;
	},
	/**@description 封装保存的信息*/
	getSaveDtlParams: function() {
		var me = this;
		var saveParams = {
			"TotalPrice": 0
		};
		var inDtlList = [];
		var totalPrice = 0;

		me.DtlGrid.store.each(function(record) {
			totalPrice += parseFloat(record.get("ReaBmsInDtlVO_ReaBmsInDtl_SumTotal"));

			var id = record.get(me.DtlGrid.PKField);
			var obj = me.getSaveDtlOne(record);
			var operationAddList = [];
			var operationEditList = [];
			var barCodeType = record.get("ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType");
			if (barCodeType == "1") {
				//当次入库的所有扫码记录集合
				var curReaGoodsScanCodeList = record.get("ReaBmsInDtlVO_CurReaBmsInDtlLinkListStr");
				if (curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);
				//封装当次的条码操作记录
				if (curReaGoodsScanCodeList && curReaGoodsScanCodeList.length > 0)
					Ext.Array.each(curReaGoodsScanCodeList, function(curModel, index1) {
						if (!curModel["Id"] || curModel["Id"] == "-1") {
							var operation = {};
							operation = Ext.apply(operation, curModel);
							operation["OperTypeID"] = "5"; //库存初始化
							operation["Id"] = "-1";
							operationAddList.push(operation);
						} else {
							delete curModel["DataTimeStamp"];
							delete curModel["DataAddTime"];
							delete curModel["DataUpdateTime"];
							operationEditList.push(curModel);
						}
					});
			}
			var objvo = {
				"ReaBmsInDtl": obj, //验收明细信息
				"BarCodeType": barCodeType,
				"ReaBmsInDtlLinkList": operationAddList, //当次新增的入库条码扫码操作集合
				"EditReaBmsInDtlLinkList": operationEditList, //入库已扫码条码操作集合
			};
			inDtlList.push(objvo);
		});
		if (inDtlList.length > 0) saveParams.inDtlList = inDtlList;
		saveParams.TotalPrice = totalPrice;

		return saveParams;
	},
	/**@description 获取单个的验收明细的基本封装信息*/
	getSaveDtlOne: function(record) {
		var me = this;
		var id = record.get(me.DtlGrid.PKField);
		if (!id) id = -1;

		var entity = {
			Id: id,
			BarCodeType: record.get("ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType"),
			InDtlNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_InDtlNo"),
			InDocNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_InDocNo"),
			GoodsCName: record.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsCName"),
			GoodsUnit: record.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsUnit"),

			LotNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_LotNo"),
			StorageName: record.get("ReaBmsInDtlVO_ReaBmsInDtl_StorageName"),
			PlaceName: record.get("ReaBmsInDtlVO_ReaBmsInDtl_PlaceName"),
			CompanyName: record.get("ReaBmsInDtlVO_ReaBmsInDtl_CompanyName"),
			BiddingNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_BiddingNo"),

			ApproveDocNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_ApproveDocNo"),
			GoodsSerial: record.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsSerial"),
			LotSerial: record.get("ReaBmsInDtlVO_ReaBmsInDtl_LotSerial"),
			SysLotSerial: record.get("ReaBmsInDtlVO_ReaBmsInDtl_SysLotSerial"),
			RegisterNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_RegisterNo"),

			ReaServerCompCode: record.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaServerCompCode"),
			Memo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_Memo"),
			ReaGoodsNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaGoodsNo"),
			ProdGoodsNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_ProdGoodsNo"),
			CenOrgGoodsNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_CenOrgGoodsNo"),
			GoodsNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsNo"),
			OtherDtlNo: record.get("ReaBmsInDtlVO_ReaBmsInDtl_OtherDtlNo"),
			ZX1: record.get("ReaBmsInDtlVO_ReaBmsInDtl_ZX1"),
			ZX2: record.get("ReaBmsInDtlVO_ReaBmsInDtl_ZX2"),
			ZX3: record.get("ReaBmsInDtlVO_ReaBmsInDtl_ZX3"),
			FactoryOutTemperature: record.get("ReaBmsInDtlVO_ReaBmsInDtl_FactoryOutTemperature"),
			ArrivalTemperature: record.get("ReaBmsInDtlVO_ReaBmsInDtl_ArrivalTemperature"),
			AppearanceAcceptance: record.get("ReaBmsInDtlVO_ReaBmsInDtl_AppearanceAcceptance")
		};

		var SaleDtlID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_SaleDtlID");
		if (SaleDtlID) {
			entity.SaleDtlID = SaleDtlID;
		}
		var SaleDtlConfirmID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_SaleDtlConfirmID");
		if (SaleDtlConfirmID) {
			entity.SaleDtlConfirmID = SaleDtlConfirmID;
		}
		var InDocID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_InDocID");
		if (InDocID) {
			entity.InDocID = InDocID;
		}
		var StorageID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_StorageID");
		if (StorageID) {
			entity.StorageID = StorageID;
		}
		var PlaceID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_PlaceID");
		if (PlaceID) {
			entity.PlaceID = PlaceID;
		}
		var ReaCompanyID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaCompanyID");
		if (ReaCompanyID) {
			entity.ReaCompanyID = ReaCompanyID;
		}
		var CompGoodsLinkID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_CompGoodsLinkID");
		if (CompGoodsLinkID) {
			entity.CompGoodsLinkID = CompGoodsLinkID;
		}
		var ReaCompCode = record.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaCompCode");
		if (ReaCompCode) {
			entity.ReaCompCode = ReaCompCode;
		}
		var ProdDate = record.get("ReaBmsInDtlVO_ReaBmsInDtl_ProdDate");
		var InvalidDate = record.get("ReaBmsInDtlVO_ReaBmsInDtl_InvalidDate");
		var RegisterInvalidDate = record.get("ReaBmsInDtlVO_ReaBmsInDtl_RegisterInvalidDate");

		if (ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		if (InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);
		if (RegisterInvalidDate) entity.RegisterInvalidDate = JShell.Date.toServerDate(RegisterInvalidDate);

		var GoodsQty = record.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty");
		var Price = record.get("ReaBmsInDtlVO_ReaBmsInDtl_Price");
		var SumTotal = record.get("ReaBmsInDtlVO_ReaBmsInDtl_SumTotal");
		var TaxRate = record.get("ReaBmsInDtlVO_ReaBmsInDtl_TaxRate");

		var GoodsSort = record.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsSort");
		if (GoodsSort) {
			entity.GoodsSort = GoodsSort;
		}
		if (GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if (Price) Price = parseFloat(Price);
		else Price = 0;
		if (SumTotal) SumTotal = parseFloat(SumTotal);
		else SumTotal = Price * GoodsQty;

		entity.Price = Price;
		entity.GoodsQty = GoodsQty;
		entity.SumTotal = SumTotal;
		if (TaxRate) entity.TaxRate = TaxRate;
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		//入库主单
		if (me.PK) {
			entity.InDocID = me.PK;
		}
		var reaGoodsId = record.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_Id");
		if (reaGoodsId) {
			entity.ReaGoods = {
				Id: reaGoodsId,
				DataTimeStamp: strDataTimeStamp.split(',')
			};
		}
		return entity;
	},
	/**UI应用关闭前将编辑列表的编辑状态取消,防止关闭时UI错乱*/
	cancelEdit: function() {
		var me = this;
		var plugin = me.DtlGrid.getPlugin(me.DtlGrid.cellpluginId);
		if (plugin) {
			plugin.cancelEdit();
		}
	}
});
