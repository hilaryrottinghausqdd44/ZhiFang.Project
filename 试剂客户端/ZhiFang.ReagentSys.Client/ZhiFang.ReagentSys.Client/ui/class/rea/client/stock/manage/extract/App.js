/**
 * 入库管理
 * 提取物资接口的出库信息进行入库确认处理
 * 将提取的物资接口的出库信息的入库单进行退库操作?
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.stock.manage.extract.App', {
	extend: 'Shell.ux.panel.AppPanel',
	
	title: '提取入库',
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	
	/**扫码提取服务*/
	scanCodeUrl: '/ReaManageService.svc/RS_UDTO_InputSaleDocInterface',
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddReaBmsInDocAndDtlByInterface',
	/**修改服务地址*/
	//editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocAndDtlOfManualInput',

	formtype: "add",
	/**当前选择的入库主单Id*/
	PK: null,
	/**是否显示手工同步字典的按钮*/
	hasDict: true,
	/**接口数据是否需要重新生成条码*/
	iSNeedCreateBarCode: "2",
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DtlGrid.on({
			onChangeSumTotal: function(grid, totalPrice) {
				me.DocForm.setTotalPrice(totalPrice);
			}
		});
		me.DocForm.on({
			load: function(form, data) {
				if(data && data.value) {}
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
		me.initReaComInfo();
		me.addEvents('save');
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocForm = Ext.create('Shell.class.rea.client.stock.manage.extract.DocForm', {
			title: '入库信息',
			itemId: 'DocGrid',
			region: 'north',
			header: false,
			split: false,
			collapsible: false,
			collapsed: false
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.stock.manage.extract.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			defaultLoad: false,
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DocForm, me.DtlGrid];
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		if(me.hasDict) {
			items.push({
				xtype: 'splitbutton',
				textAlign: 'left',
				iconCls: 'button-save',
				text: '字典手工同步',
				handler: function(btn, e) {
					btn.overMenuTrigger = true;
					btn.onClick(e);
				},
				menu: [{
					text: '机构货品&供货商同步',
					iconCls: 'button-save',
					tooltip: '调用物资接口将机构货品及供货商信息手工同步',
					listeners: {
						click: function(but) {
							me.onDictSyncClick("1");
						}
					}
				}, {
					iconCls: 'button-save',
					text: '库房同步',
					tooltip: '调用物资接口将库房信息手工同步',
					listeners: {
						click: function(but) {
							me.onDictSyncClick("2");
						}
					}
				}]
			}, '-');
		}
		items.push({
			fieldLabel: '供货商选择',
			emptyText: '选择提取单所属供货商',
			name: 'ReaBmsInDoc_CompanyName',
			itemId: 'ReaBmsInDoc_CompanyName',
			xtype: 'uxCheckTrigger',
			width: 260,
			labelWidth: 80,
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			value: me.CompanyName,
			hidden: true,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					resizable: false,
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType: "0",
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onCompAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			xtype: 'textfield',
			width: 210,
			style: {
				marginLeft: "5px"
			},
			emptyText: '提取单号',
			fieldLabel: '',
			name: 'txtOtherDocNo',
			itemId: 'txtOtherDocNo',
			labelAlign: 'right',
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER)
						me.onOtherDocNoScanCode(field, e);
				}
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-save',
			itemId: "btnTemp",
			text: '入库暂存',
			tooltip: '入库暂存',
			hidden: true,
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
		}, {
			iconCls: 'button-reset',
			text: '退库',
			tooltip: '将提取未保存的入库信息进行退库处理',
			handler: function() {
				me.onReturnLibraryClick();
			}
		}, '-', {
			iconCls: 'button-refresh',
			text: '重置',
			tooltip: '清空当前提取信息',
			handler: function() {
				me.onRefreshClick();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
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
	nodata: function() {
		var me = this;
		var me = this;
		me.PK = null;
		me.formtype = "add";

		me.DocForm.PK = null;
		me.DocForm.formtype = "show";
		me.DocForm.StatusName = "";
		me.DocForm.isShow();
		me.DocForm.getForm().reset();

		me.DtlGrid.PK = null;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.Status = null;
		me.DtlGrid.store.removeAll();
		//me.DtlGrid.disableControl();
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
		me.DocForm.Status = "0";
		me.DocForm.formtype = "add";
		me.DocForm.isAdd();

		me.DtlGrid.PK = null;
		me.DtlGrid.formtype = "add";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.store.removeAll();
		//me.DtlGrid.enableControl();
	},
	isEdit: function(id) {
		var me = this;
		if(id) me.PK = id;
		me.formtype = "edit";

		me.DocForm.PK = me.PK;
		me.DocForm.Status = "0";
		me.DocForm.formtype = "edit";
		me.DocForm.isEdit(me.PK);

		me.DtlGrid.PK = me.PK;
		me.DtlGrid.formtype = "edit";
		//只显示暂存未验收的验收货品明细
		me.DtlGrid.defaultWhere = "reabmsindtl.InDocID=" + me.PK;
		me.DtlGrid.Status = "0";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.enableControl();
		me.DtlGrid.onSearch();
	},
	/**@description 供货商选择后处理*/
	onCompAccept: function(record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var ComName = buttonsToolbar.getComponent('ReaBmsInDoc_CompanyName');

		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
		var orgNo = record ? record.data.value.OrgNo : '';

		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		me.ReaCompanyID = id;
		me.ReaServerCompCode = platformOrgNo;
		me.CompanyName = text;
		me.ReaCompCode = orgNo;

		ComName.setValue(text);
		var objValue = {
			"ReaCompanyID": id,
			"ReaCompCName": text,
			"ReaCompCode": orgNo,
			"ReaServerCompCode": platformOrgNo,
			"PlatformOrgNo": platformOrgNo
		};
		me.setReaComInfo();
	},
	/**@description 缓存供货商信息*/
	initReaComInfo: function() {
		var me = this;
		//获取还原当前用户在手工入库时最后一次供应商选择的值
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var key = "stock.manage.extract." + userId;
		var reaCom = JcallShell.LocalStorage.get(key);
		if(reaCom) {
			reaCom = JcallShell.JSON.decode(reaCom);
			/**供应商ID*/
			me.ReaCompanyID = reaCom.ReaCompanyID;
			me.ReaServerCompCode = reaCom.ReaServerCompCode;
			me.ReaCompCode = reaCom.ReaCompCode;
			me.CompanyName = reaCom.CompanyName;
		}
	},
	/**@description 设置供货商的缓存信息*/
	setReaComInfo: function() {
		var me = this;
		//更新当前用户在手工入库时最后一次供应商选择的值
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var key = "stock.manage.extract." + userId;
		var reaCom = {
			"ReaCompanyID": me.ReaCompanyID,
			"ReaServerCompCode": me.ReaServerCompCode,
			"ReaCompCode": me.ReaCompCode,
			"CompanyName": me.CompanyName,
		};
		reaCom = JcallShell.JSON.encode(reaCom);
		JcallShell.LocalStorage.set(key, reaCom);
	},
	/**@description 货品扫码输入框*/
	getOtherDocNo: function() {
		var me = this;
		var txtScanCode = me.getComponent("buttonsToolbar").getComponent("txtOtherDocNo");
		return txtScanCode;
	},
	/**@description 供货商信息及机构货品信息同步*/
	onDictSyncClick: function(type) {
		var me = this;
		var url = "";
		switch(type) {
			case "2":
				url = JShell.System.Path.ROOT + "/ReaInternalInterface.svc/RS_GetReaStorageInfo";
				break;
			default:
				url = JShell.System.Path.ROOT + "/ReaInternalInterface.svc/RS_GetReaGoodsInfo";
				break;
		}
		me.showMask(me.DocForm.saveText); //显示遮罩层
		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, "手工同步成功!");
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	onTempSaveClick: function() {
		var me = this;
		me.DocForm.Status = "1";
		me.DtlGrid.Status = "1";
		me.onSave("1", null);
	},
	/**@description 入库确认处理*/
	onConfirmClick: function() {
		var me = this;
		me.DocForm.Status = "2";
		me.DtlGrid.Status = "2";
		me.onSave("2", null);
	},
	/**@description 退库处理*/
	onReturnLibraryClick: function() {
		var me = this;
		me.onReturnLibrary("1");
	},
	/**@description 保存提取的入库信息*/
	onSave: function(status, confirmData) {
		var me = this;
		if(!me.DocForm.getForm().isValid()) return;
		var docEntity = me.DocForm.getAddParams();

		if(!docEntity.entity) {
			JShell.Msg.alert("获取封装入库信息为空", null, 2000);
			return;
		}
		var result = me.validatorSave();
		if(result.isValid == false) {
			JShell.Msg.error(result.info);
			return;
		}
		var entity = docEntity.entity;
		entity.Status = status;
		var dtlInfo = me.getSaveDtlParams();
		entity.TotalPrice = dtlInfo.TotalPrice;
		entity.Id = -1;
		var params = {
			"entity": entity,
			"dtAddList": dtlInfo.dtAddList,
			"iSNeedCreateBarCode":me.iSNeedCreateBarCode
		};

		if(!params.dtAddList) params.dtAddList = [];
		params = Ext.JSON.encode(params);
		if(!params) {
			JShell.Msg.alert("封装入库信息出错,不能保存!", null, 2000);
			return;
		}
		//是否需要保存后打印
		var isPrint = me.DtlGrid.getIsPrint();
		var url = me.addUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		me.showMask(me.DocForm.saveText); //显示遮罩层
		if (!me.BUTTON_CAN_CLICK) return;
		me.BUTTON_CAN_CLICK = false; //不可点击
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				//先清空列表信息,防止后续处理出现异常,用户又进行第二次保存
				me.DtlGrid.store.removeAll();
				
				me.PK = me.formtype == 'add' ? data.value.id : me.PK;
				if(status == '1') isPrint = false;
				me.fireEvent('save', me, me.PK, isPrint);
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
		if(isValid == true)
			var result = {
				"isValid": true,
				"info": ""
			};
		if(me.DtlGrid.store.getCount() <= 0) {
			result.info = "待入库货品明细为空!";
			result.isValid = false;
			return result;
		}
		var info = "";
		me.DtlGrid.store.each(function(record) {
			var LotNo = record.get("ReaBmsInDtl_LotNo");
			var GoodsQty = record.get("ReaBmsInDtl_GoodsQty");
			var Price = record.get("ReaBmsInDtl_Price");
			var SumTotal = record.get("ReaBmsInDtl_SumTotal");
			var BarCodeType = record.get("ReaBmsInDtl_BarCodeType");
			var ReaGoodsName = record.get("ReaBmsInDtl_GoodsCName");

			var StorageID = record.get("ReaBmsInDtl_StorageID");
			var PlaceID = record.get("ReaBmsInDtl_PlaceID");
			var ReaCompanyID = record.get("ReaBmsInDtl_ReaCompanyID");
			var ReaGoodsId = record.get("ReaBmsInDtl_ReaGoods_Id");
			var InvalidDate = record.get("ReaBmsInDtl_InvalidDate");

			if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
			else GoodsQty = 0;

			if(!Price)
				Price = 0;
			Price = parseFloat(Price);

			if(!ReaGoodsId) {
				info = "货品ID不能为空!";
				return false;
			}
			if(!GoodsQty || GoodsQty < 0) {
				info = "货品为" + ReaGoodsName + ",入库数不能为空或小于等于0!";
				return false;
			}
			if(Price < 0) {
				info = "货品为" + ReaGoodsName + ",单价小于零，不能验收！";
				return false;
			}
			if(!LotNo) {
				info = "货品为" + ReaGoodsName + ",待入库货品的货品批号为空!";
				return false;
			}
			if(!StorageID) {
				info = "货品为" + ReaGoodsName + ",库房不能为空!";
				return false;
			}
			if(!ReaCompanyID) {
				info = "货品为" + ReaGoodsName + ",所属供应商不能为空!";
				return false;
			}
			if(!InvalidDate) {
				info = "货品为" + ReaGoodsName + ",有效期至不能为空!";
				return false;
			}
			if(BarCodeType == "1") {}
		});
		if(info) {
			result.isValid = false;
			result.info = info;
		}
		return result;
	},
	/**@description 封装保存的信息*/
	getSaveDtlParams: function() {
		var me = this;
		var saveParams = {
			"TotalPrice": 0,
			"dtAddList": []
		};
		var dtAddList = me.getSaveDtlAddList(true);
		if(!dtAddList) dtAddList = [];
		var totalPrice = 0;
		if(dtAddList.length > 0) saveParams.dtAddList = dtAddList;
		saveParams.TotalPrice = totalPrice;
		return saveParams;
	},
	/**@description 获取单个的验收明细的基本封装信息*/
	getSaveDtlAddList: function(isVo) {
		var me = this;
		var dtAddList = [];
		var operationAddList = [];
		var totalPrice = 0;
		me.DtlGrid.store.each(function(record) {
			operationAddList = [];
			totalPrice += parseFloat(record.get("ReaBmsInDtl_SumTotal"));

			var id = record.get(me.DtlGrid.PKField);
			var obj = me.getSaveDtlOne(record);

			var BarCodeType = "" + record.get("ReaBmsInDtl_BarCodeType");
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
			} else if(BarCodeType == "0") {
				//批条码
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
		var id = record.get(me.DtlGrid.PKField);
		if(!id) id = -1;

		var entity = {
			Id: -1,
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
			LotQRCode: record.get("ReaBmsInDtl_LotQRCode"),
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
		var SaleDtlID = record.get("ReaBmsInDtl_SaleDtlID");
		if(SaleDtlID) {
			entity.SaleDtlID = SaleDtlID;
		}
		var SaleDtlConfirmID = record.get("ReaBmsInDtl_SaleDtlConfirmID");
		if(SaleDtlConfirmID) {
			entity.SaleDtlConfirmID = SaleDtlConfirmID;
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
	},
	/**@description 退库处理*/
	onReturnLibrary: function(status) {
		var me = this;
		var url = '/ReaManageService.svc/RS_UDTO_ReaGoodsBackStorageInterface';
		if(!me.DocForm.getForm().isValid()) return;
		var docEntity = me.DocForm.getAddParams();

		if(!docEntity.entity) {
			JShell.Msg.alert("获取封装退库信息为空", null, 2000);
			return;
		}

		var entity = docEntity.entity;
		entity.Status = status;
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
		me.showMask(me.DocForm.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			data = Ext.JSON.decode(data.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, ''));
			//console.log("data:"+Ext.JSON.encode(data));			
			if(data.success) {
				JShell.Msg.alert("退库操作成功!", null, 2000);
			} else {
				JShell.Msg.error(data.msg);
			}
			me.getOtherDocNo().setValue("");
			me.getOtherDocNo().focus();
		}, true, null, true);
	},
	/**@description 出库单号扫码输入框*/
	onOtherDocNoScanCode: function(field, e) {
		var me = this;
		me.formtype = "add";

		var saleDocNo = field.getValue();
		if(!saleDocNo) {
			JShell.Msg.error("请输入提取单号后再扫码!");
			me.getOtherDocNo().setValue("");
			me.getOtherDocNo().focus();
			return;
		}
		me.clearData();

		var mainFields = "ReaBmsInDoc_DeptID,ReaBmsInDoc_DeptName,ReaBmsInDoc_InDocNo,ReaBmsInDoc_InType,ReaBmsInDoc_Carrier,ReaBmsInDoc_Status,ReaBmsInDoc_OperDate,ReaBmsInDoc_CreaterName,ReaBmsInDoc_CreaterID,ReaBmsInDoc_Id,ReaBmsInDoc_OtherDocNo,ReaBmsInDoc_InvoiceNo,ReaBmsInDoc_TotalPrice,ReaBmsInDoc_Memo,ReaBmsInDoc_ZX1,ReaBmsInDoc_ZX2,ReaBmsInDoc_ZX3";
		var childFields = "ReaBmsInDtl_ReaGoodsNo,ReaBmsInDtl_ProdGoodsNo,ReaBmsInDtl_CenOrgGoodsNo,ReaBmsInDtl_GoodsNo,ReaBmsInDtl_BarCodeType,ReaBmsInDtl_GoodsCName,ReaBmsInDtl_GoodsUnit,ReaBmsInDtl_UnitMemo,ReaBmsInDtl_StorageName,ReaBmsInDtl_PlaceName,ReaBmsInDtl_LotNo,ReaBmsInDtl_InvalidDate,ReaBmsInDtl_Price,ReaBmsInDtl_GoodsQty,ReaBmsInDtl_Memo,ReaBmsInDtl_SumTotal,ReaBmsInDtl_InDtlNo,ReaBmsInDtl_ProdDate,ReaBmsInDtl_BiddingNo,ReaBmsInDtl_TaxRate,ReaBmsInDtl_RegisterNo,ReaBmsInDtl_RegisterInvalidDate,ReaBmsInDtl_CompanyName,ReaBmsInDtl_ReaCompCode,ReaBmsInDtl_Id,ReaBmsInDtl_LotSerial,ReaBmsInDtl_LotQRCode,ReaBmsInDtl_SysLotSerial,ReaBmsInDtl_ReaGoods_Id,ReaBmsInDtl_CompGoodsLinkID,ReaBmsInDtl_ReaCompanyID,ReaBmsInDtl_ReaServerCompCode,ReaBmsInDtl_ApproveDocNo,ReaBmsInDtl_StorageID,ReaBmsInDtl_PlaceID,ReaBmsInDtl_GoodsSerial,ReaBmsInDtl_GoodsSort,ReaBmsInDtl_OtherSerialNoStr,ReaBmsInDtl_OtherDtlNo,ReaBmsInDtl_ZX1,ReaBmsInDtl_ZX2,ReaBmsInDtl_ZX3";
		var url = (me.scanCodeUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.scanCodeUrl;
		var entityType = "3"; //转入库单
		var params = "?entityType=" + entityType;
		var labOrgId = JcallShell.REA.System.CENORG_ID;
		if(labOrgId) params = params + "&labOrgId=" + labOrgId;
		if(saleDocNo) params = params + "&saleDocNo=" + saleDocNo;
		if(me.ReaCompanyID) params = params + "&reaCompID=" + me.ReaCompanyID;
		params = params + "&mainFields=" + mainFields + "&childFields=" + childFields;

		url = url + params;
		me.showMask("提取信息中...");
		JShell.Server.get(url, function(response) {
			me.hideMask();
			response = Ext.JSON.decode(response.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, ''));
			if(response.success) {
				if(response.data) {
					var result = response.data;
					if(result) {
						result = Ext.JSON.decode(result);
					}
					//返回的入库主单及入库明细
					var inDoc = null;
					var inDtlList = [];
					if(result.Master) {
						inDoc = result.Master;
					}
					if(result.Detail) {
						inDtlList = result.Detail;
					}
					if(inDoc && inDtlList && inDtlList.length > 0) {
						me.loadData(inDoc, inDtlList);
						me.getOtherDocNo().setValue("");
						me.getOtherDocNo().focus();
					} else {
						JShell.Msg.error(response.message);
						me.getOtherDocNo().setValue("");
						me.getOtherDocNo().focus();
					}
				} else {
					JShell.Msg.error("错误信息为:" + response.message);
					me.getOtherDocNo().setValue("");
					me.getOtherDocNo().focus();
				}
			} else {
				JShell.Msg.error("错误信息为:" + response.message);
				me.getOtherDocNo().setValue("");
				me.getOtherDocNo().focus();
			}
		}, true, null, true);
	},
	/**@description 将提取的入库信息填充到入库表单及入库明细*/
	loadData: function(inDoc, inDtlList) {
		var me = this;
		me.DocForm.isAdd();
		me.DocForm.getForm().setValues(inDoc);
		me.DtlGrid.store.removeAll();
		me.DtlGrid.store.loadData(inDtlList);
	},
	/**@description 重置入库表单及入库明细*/
	onRefreshClick: function() {
		var me = this;
		me.clearData();
	}
});