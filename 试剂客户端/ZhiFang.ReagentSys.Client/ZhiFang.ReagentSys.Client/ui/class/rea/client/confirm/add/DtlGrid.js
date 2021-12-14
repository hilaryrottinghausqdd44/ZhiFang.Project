/**
 * 客户端验收验货单明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.add.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '验货单明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaManageService.svc/ST_UDTO_DelReaBmsCenSaleDtlConfirm',
	/**货品扫码服务*/
	scanCodeUrl: '/ReaManageService.svc/ST_UDTO_SearchReaGoodsScanCodeVOOfConfirm',
	/**获取批号数据服务路径*/
	selectLotUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLotByHQL?isPlanish=true',
	/**获取货品数据服务路径*/
	selectGoodsUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认每页数量*/
	defaultPageSize: 10000,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[10000, 10000]
	],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**明细按钮的启用状态*/
	buttonsDisabled: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCenSaleDtlConfirm_GoodsUnit',
		direction: 'ASC'
	}],
	/**默认选中*/
	autoSelect: true,
	/**主单信息*/
	DocInfo: {},
	/**默认选中*/
	autoSelect: false,

	/**是否可编辑*/
	canEdit: true,
	/**是否多选行*/
	checkOne: true,
	StatusList: [],
	/**申请单状态枚举*/
	StatusEnum: {},
	/**申请单状态背景颜色枚举*/
	StatusBGColorEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	/**供应商ID*/
	ReaCompID: null,
	ReaCompCName: null,
	/**封装保存信息的验收明细状态*/
	Status: "0",
	/**是否隐藏复制列*/
	hiddenCopy: false,
	/**货品明细弹出消息框消失时间*/
	hideTimes: 5000,
	/**是否显示货品信息(双击批号单元格选择时隐藏)*/
	IsShowDtlInfo: true,
	/**是否不返回聚焦到扫码框*/
	IsShowScan: true,
	/**扫码模式(严格模式:strict,混合模式：mixing)*/
	CodeScanningMode: "mixing",
	OTYPE: "",
	/**编辑单元格pluginId*/
	cellpluginId: 'cellpluginId',

	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		me.on('select', function (rowModel, record, index, e) {
			if (me.IsShowDtlInfo == true)
				me.onShowDtlInfo(record,me.IsShowScan);
		});
		me.store.on({
			update: function (store, record) {
				if (record.dirty) {
					var changedObj = record.getChanges();
					for (var modified in changedObj) {
						if (modified == "ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount")
							me.onAcceptCountChanged(record);
						else if (modified == "ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount")
							me.onRefuseCountChanged(record);
						else if (modified == "ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price")
							me.onPriceChanged(record);
					}
				}
			}
		});
		//验收货品扫码 严格模式:1,混合模式：2"
		JcallShell.REA.RunParams.getRunParamsValue("AcceptanceScanCode", false, function (data) {
			if (data.success) {
				var paraValue = "2";
				var obj = data.value;
				if (obj.ParaValue) {
					paraValue = obj.ParaValue;
				}
				if (paraValue == "1") {
					me.CodeScanningMode = "strict";
				} else {
					me.CodeScanningMode = "mixing";
				}
			}
		});
	},
	initComponent: function () {
		var me = this;
		me.addEvents('onScanCodeShowDtl', 'onDelAfter');
		if (me.canEdit == true) {
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				pluginId: me.cellpluginId,
				clicksToEdit: 1
			});
		}
		if (!me.checkOne) me.setCheckboxModel();
		me.callParent(arguments);
	},
	setCheckboxModel: function () {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	/**创建数据列*/
	createGridColumns: function () {
		var me = this;
		var columns = [];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function () {
		var me = this;
		var items = [];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**@description 刷新数据*/
	onSearch: function () {
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = true;
		this.load(null, true);
	},
	/**加载数据前*/
	onBeforeLoad: function () {
		var me = this;
		me.store.removeAll();
		//me.getView().update();
		if (!me.PK && me.formtype == "edit") return false;
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function (data) {
		var me = this;
		if (!data || data.list.length <= 0) return data;
		for (var i = 0; i < data.list.length; i++) {
			//验货已扫码记录集合
			var dtlConfirmLinkStr = data.list[i]["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr"];

			if (dtlConfirmLinkStr) {
				var id = data.list[i]["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id"];
				var tempList = [],
					curReaGoodsScanCodeList = [];
				if (dtlConfirmLinkStr) tempList = JcallShell.JSON.decode(dtlConfirmLinkStr);
				//当次扫码记录集合重置为验货已扫码记录集合,需要将不是当前验收明细ID的扫码过滤(同一供货单可能存在几个验收单,已扫码记录集合合并了其他验收单的扫码记录)
				Ext.Array.each(tempList, function (item, index, countriesItSelf) {
					if (item.BDtlID == id) curReaGoodsScanCodeList.push(item);
				});

				if (curReaGoodsScanCodeList) dtlConfirmLinkStr = JcallShell.JSON.encode(curReaGoodsScanCodeList);
			}
			if (!dtlConfirmLinkStr) dtlConfirmLinkStr = "";
			data.list[i]["ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList"] = dtlConfirmLinkStr;
		}
		return data;
	},
	/**按钮的启用或或禁用*/
	setButtonsDisabled: function (disabled) {
		var me = this;
	},
	/**@description 接收数量值改变后联动*/
	onAcceptCountChanged: function (record) {
		JShell.Msg.overwrite('onAcceptCountChanged');
	},
	/**@description 拒收数量值改变后联动*/
	onRefuseCountChanged: function (record) {
		JShell.Msg.overwrite('onRefuseCountChanged');
	},
	/**@description 保存前验证*/
	validatorSave: function () {
		JShell.Msg.overwrite('validatorSave');
	},
	/**
	 * @description 货品为盒条码时的接收拒收数量输入框的处理
	 * @description 货品为批条码时,在"严格模式"下,也不强制必须货品扫码
	 * */
	comSetReadOnlyOfBarCodeType: function (field, e) {
		var me = this;
		var record = field.ownerCt.editingPlugin.context.record;
		var barCodeMgr = "" + record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType");
		//如果扫码模式为严格模式,批条码及盒条码需要扫码&&barCodeMgr=="1"
		if (me.CodeScanningMode == "strict" && barCodeMgr == "1") {
			field.setReadOnly(true);
			//return;
		} else {
			field.setReadOnly(false);
		}
	},
	/**@description 设置按钮的调用或禁用*/
	setBtnDisabled: function (com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if (buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if (btn) btn.setDisabled(disabled);
		}
	},
	/**@description 复制拆分*/
	onCopyRecord: function (dataOne) {
		var me = this;
		var record = {};
		record = Ext.apply(record, dataOne);
		record.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id = -1;
		record.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty = 0;
		record.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount = 0;
		record.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount = 0;
		record.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal = 0;
		record.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo = "";
		record.ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList = "";
		record.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr = "";
		me.store.add(record);
	},
	deleteOne: function (record) {
		var me = this;
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = 1;
		var showMask = false;
		var id = record.get(me.PKField);
		if (!id || id == "-1") {
			me.delCount++;
			me.store.remove(record);
			if ((me.delCount + me.delErrorCount) == me.delLength && me.delErrorCount == 0) {
				me.fireEvent('onDelAfter', me);
			}
		} else {
			if (showMask == false) {
				showMask = true;
				me.showMask(me.delText); //显示遮罩层
			}
			me.delOneById(record, 1, id);
		}
	},
	/**@description 删除一条数据*/
	delOneById: function (record, index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		var confirmSourceType = me.OTYPE;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id + "&confirmSourceType=" + confirmSourceType;
		setTimeout(function () {
			JShell.Server.get(url, function (data) {
				if (data.success) {
					me.store.remove(record);
					me.delCount++;
				} else {
					record.set(me.DelField, false);
					record.set('ErrorInfo', data.msg);
					me.delErrorCount++;
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.delErrorCount != 0) {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					} else {
						me.fireEvent('onDelAfter', me);
					}
				}
			});
		}, 100 * index);
	},
	/**@description 单价值改变后联动*/
	onPriceChanged: function (record) {
		var me = this;
		var Price = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price');
		var AcceptCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount');
		if (AcceptCount) AcceptCount = parseFloat(AcceptCount);
		else AcceptCount = 0;
		if (Price) Price = parseFloat(Price);
		else Price = 0;

		var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		SumTotal = SumTotal ? SumTotal : 0;
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal', SumTotal);
		record.commit();
	},
	/**@description 货品扫码输入框*/
	gettxtScanCode: function () {
		var me = this;
		var txtScanCode = me.getComponent("buttonsToolbar").getComponent("txtScanCode");
		return txtScanCode;
	},
	/**@description 货品扫码时获取扫码方式值*/
	getScanCodeValue: function () {
		var me = this;
		var rboScanCode = me.getComponent("buttonsToolbar").getComponent("rboScanCode");
		var scanCode = rboScanCode.getValue().ScanCode;
		return scanCode;
	},
	/**@description 货品扫码时是否显示浮动窗值*/
	getIShowDtlInfoValue: function () {
		var me = this;
		var iShowDtlInfo = me.getComponent("buttonsToolbar").getComponent("cboIShowDtlInfo");
		return iShowDtlInfo.getValue();
	},
	/**货品扫码显示货品浮动窗体信息*/
	onShowDtlInfo: function (rec,IsShowScan) {
		var me = this;
		var info = {
			"CName": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName") : "",
			"EName": rec ? rec.get("ReaSaleDtlConfirmVO_ReaGoodsEName") : "",
			"SName": rec ? rec.get("ReaSaleDtlConfirmVO_ReaGoodsSName") : "",
			"Unit": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsUnit") : "",
			"UnitMemo": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_UnitMemo") : "",
			"LotNo": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo") : "",
			"InvalidDate": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate") : "",
			"AcceptCount": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount") : "",
			"RefuseCount": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount") : "",
			"Price": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price") : "",
			"SumTotal": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal") : "",
			"ReaGoodsNo": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo") : "",
			"ProdGoodsNo": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdGoodsNo") : "",
			"CenOrgGoodsNo": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CenOrgGoodsNo") : "",
			"GoodsNo": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsNo") : ""
		};
		//重置消息框的消失隐藏时间
		me.hideTimes = 5000;
		me.fireEvent('onScanCodeShowDtl', me, info , IsShowScan);
	},
	/**@description 选择货品批号*/
	onChooseLotNo: function () {
		var me = this;
		var selected = me.getSelectionModel().getSelection();
		if (!selected || selected.length <= 0) return;
		var record = selected[0];
		var lotNo = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo");
		var reaGoodsID = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID");
		var reaGoodsNo = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo");
		var reaGoodsName = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName");
		var maxWidth = 860; //document.body.clientWidth * 0.68;
		var height = document.body.clientHeight * 0.88;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			GoodsID: reaGoodsID,
			ReaGoodsNo: reaGoodsNo,
			GoodsCName: reaGoodsName,
			CurLotNo: lotNo,
			listeners: {
				accept: function (p, rec) {
					me.IsShowDtlInfo = true;
					me.IsShowScan=true;
					if (rec) {
						record.set("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo", rec.get("ReaGoodsLot_LotNo"));
						record.set("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate", rec.get("ReaGoodsLot_ProdDate"));
						record.set("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate", rec.get("ReaGoodsLot_InvalidDate"));
						record.commit();
					}
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.goodslot.CheckGrid', config);
		win.show();
	},
	/***
	 * @description 货品扫码调用服务后,获取到条码货品信息为多个(货品编码相同,单位不相同)时处理
	 * @param {Object} reaBarCodeVOList
	 * @param {Object} barCode
	 */
	onChooseReaBarCodeVO: function (reaBarCodeVOList, callback) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.68;
		var height = document.body.clientHeight * 0.78;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			listeners: {
				accept: function (p, record) {
					var reaBarCodeVO = null;
					if (record)
						reaBarCodeVO = JcallShell.JSON.decode(record.data.ReaBarCodeVO);
					p.close();
					if (callback) callback(reaBarCodeVO);
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.confirm.choose.reabarcode.CheckGrid', config);
		win.show();
		win.loadData(reaBarCodeVOList);
	},
	/***
	 * @description 货品扫码调用服务处理后,条码类型为盒条码,封装条码操作记录基本信息
	 * @param {Object} reaBarCodeVO 货品条码信息
	 * @param {Object} scanCode 接收扫码或拒收扫码值
	 */
	getBarcodeOperationVO: function (reaBarCodeVO, scanCode) {
		var me = this;
		var docNo = reaBarCodeVO.BDocNo;
		if (!docNo) docNo = "";

		var docID = reaBarCodeVO.BDocID;
		if (!docID) docID = -1;

		var dtlID = reaBarCodeVO.BDtlID;
		if (!dtlID) dtlID = -1;

		var sysPackSerial = reaBarCodeVO.SysPackSerial;
		if (!sysPackSerial) sysPackSerial = "";
		var operationVO = {
			"Id": -1,
			"OperTypeID": scanCode,
			"ReceiveFlag": scanCode,
			"BDocNo": docNo,
			"BDocID": docID,
			"BDtlID": dtlID,
			"SysPackSerial": sysPackSerial,
			"OtherPackSerial": reaBarCodeVO.OtherPackSerial,
			"UsePackSerial": reaBarCodeVO.UsePackSerial,
			"UsePackQRCode": reaBarCodeVO.UsePackQRCode,
			"LotNo": reaBarCodeVO.LotNo
		};
		if (reaBarCodeVO.GoodsSort) {
			operationVO.GoodsSort = reaBarCodeVO.GoodsSort;
		}
		return operationVO;
	},
	/**@description 货品扫码*/
	onReaGoodsScanCode: function (field, e) {
		var me = this;
		var barCode = field.getValue();
		var indexOf = -1; //条码所在验收明细列表的行索引
		var curRecord = null; //条码所在的行记录
		var curReaGoodsScanCodeList = []; //当前条码为盒条码时的条码明细关系
		var isExec = true; //是否继续执行条码解析处理
		var info = "";
		me.store.each(function (rec) {
			indexOf++;
			var barCodeType = "" + rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType");
			switch (barCodeType) {
				case "0": //批条码
					var lotSerial = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotSerial");
					if (lotSerial == barCode) {
						curRecord = rec;
						return false;
					}
					break;
				case "1": //盒条码
					curReaGoodsScanCodeList = rec.get("ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList");
					if (curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);
					if (curReaGoodsScanCodeList) {
						Ext.Array.each(curReaGoodsScanCodeList, function (model) {
							//一维盒条码或二维盒条码
							if (model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
								curRecord = rec;
								return false;
							}
						});
					} else {
						curReaGoodsScanCodeList = [];
					}
					//验收已扫码记录集合
					if (!curRecord) {
						dtlConfirmLinkVOList = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr");
						if (dtlConfirmLinkVOList) dtlConfirmLinkVOList = JcallShell.JSON.decode(dtlConfirmLinkVOList);
						Ext.Array.each(dtlConfirmLinkVOList, function (model) {
							//一维盒条码或二维盒条码
							if (model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
								isExec = false;
								info = "该条码已被扫码验收过,请不要重复扫码!";
								return false;
							}
						});
					}
					//如果是供货验收,还需要判断供货明细条码是否存在
					if (isExec == true && me.OTYPE == "reasale") {
						var reaBmsCenSaleDtlLinkVOList = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlLinkVOListStr");
						if (reaBmsCenSaleDtlLinkVOList) reaBmsCenSaleDtlLinkVOList = JcallShell.JSON.decode(reaBmsCenSaleDtlLinkVOList);
						Ext.Array.each(reaBmsCenSaleDtlLinkVOList, function (model) {
							//一维盒条码或二维盒条码
							if (model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
								curRecord = rec;
								return false;
							}
						});
					}
					if (curRecord || isExec == false) return false;
					break;
				default:
					break;
			}
		});
		//条码已存在当前的验收明细记录里
		if (curRecord) {
			me.getSelectionModel().select(indexOf);
			var barCodeMgr = "" + curRecord.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType");
			switch (barCodeMgr) {
				case "0": //批条码
					me.onScanCodeBatchBarCodeExist(curRecord);
					break;
				case "1": //盒条码
					me.onScanCodeOfBoxBarCodeExist(barCode, curRecord, curReaGoodsScanCodeList);
					break;
				default:
					break;
			}
		} else if (isExec == true) {
			me.onScanCodeUrl(barCode);
		} else {
			JShell.Msg.error(info);
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}
	},
	/**
	 * @description 货品扫码,条码不存在验收明细列表,调用服务处理
	 * @param {Object} barCode
	 */
	onScanCodeUrl: function (barCode) {
		var me = this;
	},
	/***
	 * @description 确认验收保存
	 */
	onSaveClick: function () {
		var me = this;
		var saveParams = me.getSaveParams();
		me.fireEvent('onConfirm', me, saveParams);
	},
	/**@description 封装保存的信息*/
	getSaveParams: function () {
		var me = this;
		var saveParams = {},
			dtAddList = [],
			dtEditList = [];
		me.store.each(function (record) {
			var id = record.get(me.PKField);
			var obj = me.getSaveOneInfo(record);
			var operationList = [];
			var barCodeMgr = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType");
			if (barCodeMgr == "1") {
				//当次扫码记录集合(验货已扫码记录集合+新扫码的条码信息)
				var curReaGoodsScanCodeList = record.get("ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList");
				if (curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);

				//验货已扫码记录集合
				var dtlConfirmLinkVOList = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr");
				if (dtlConfirmLinkVOList) dtlConfirmLinkVOList = JcallShell.JSON.decode(dtlConfirmLinkVOList);

				if (!dtlConfirmLinkVOList) dtlConfirmLinkVOList = [];
				if (!curReaGoodsScanCodeList) curReaGoodsScanCodeList = [];
				//当次扫码的条码操作记录是否需要新增到扫码操作记录表
				var isAdd = true;
				//封装当次的条码操作记录
				Ext.Array.each(curReaGoodsScanCodeList, function (curModel, index1) {
					isAdd = true;
					Ext.Array.each(dtlConfirmLinkVOList, function (model, index2) {
						//当次的条码操作记录是否存在验货已扫码记录集合里
						if (curModel.UsePackSerial == model.UsePackSerial || curModel.UsePackQRCode == model.UsePackQRCode) {
							//如果验货已扫码记录集合验收标志与当次扫码的验收标志与原来相同
							if (parseInt(curModel.ReceiveFlag) == parseInt(model.ReceiveFlag))
								isAdd = false;
							return false;
						}
					});
					if (isAdd == true) {
						var operation = {};
						operation = Ext.apply(operation, curModel);
						operation["OperTypeID"] = curModel["ReceiveFlag"];
						operation["Id"] = "-1";
						delete operation["ReceiveFlag"];
						operationList.push(operation);
					}
				});
			}
			var objvo = {
				"ReaBmsCenSaleDtlConfirm": obj, //验收明细信息
				"ReaGoodsBarcodeOperationList": operationList, //当次扫码记录集合
			};
			if (!id || id == "-1")
				dtAddList.push(objvo);
			else
				dtEditList.push(objvo);
		});
		if (dtAddList.length > 0) saveParams.dtAddList = dtAddList;
		if (dtEditList.length > 0) saveParams.dtEditList = dtEditList;
		return saveParams;
	},
	getUpdateFields: function () {
		var me = this;
		var fields = [
			'Id', 'Status', 'BarCodeType', 'GoodsNo', 'ReaGoodsName', 'GoodsUnit', 'UnitMemo', 'ProdGoodsNo', 'ApproveDocNo', 'LotNo', 'InvalidDate',
			'Price', 'GoodsQty', 'SumTotal', 'AcceptCount', 'RefuseCount', 'AcceptMemo', 'ProdDate', 'BiddingNo', 'TaxRate', 'RegisterNo',
			'RegisterInvalidDate', 'GoodsSerial', 'LotSerial', 'SysLotSerial', 'FactoryOutTemperature', 'ArrivalTemperature', 'AppearanceAcceptance'
		];
		return fields.join(',');
	},
	/**@description 获取单个的验收明细的基本封装信息*/
	getSaveOneInfo: function (record) {
		var me = this;
		var id = record.get(me.PKField);
		var entity = {
			Id: id,
			Status: me.Status,
			SaleDocConfirmNo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDocConfirmNo"),
			BarCodeType: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType"),
			ReaGoodsID: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID"),

			ReaGoodsName: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName"),
			GoodsUnit: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsUnit"),
			UnitMemo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_UnitMemo"),
			BiddingNo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BiddingNo"),
			LotNo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo"),

			ApproveDocNo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ApproveDocNo"),
			RegisterNo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterNo"),
			AcceptMemo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptMemo"),
			GoodsSerial: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSerial"),
			LotSerial: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotSerial"),

			LotQRCode: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotQRCode"),
			SysLotSerial: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SysLotSerial"),
			ReaGoodsNo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo"),
			ProdGoodsNo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdGoodsNo"),
			CenOrgGoodsNo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CenOrgGoodsNo"),

			GoodsNo: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsNo"),
			StorageType: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_StorageType"),
			//冷链信息
			FactoryOutTemperature: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_FactoryOutTemperature"),
			ArrivalTemperature: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ArrivalTemperature"),
			AppearanceAcceptance: record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AppearanceAcceptance")
		};
		var compGoodsLinkID = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CompGoodsLinkID");
		if (compGoodsLinkID) {
			entity.CompGoodsLinkID = compGoodsLinkID;
		}
		var labcGoodsLinkID = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LabcGoodsLinkID");
		if (labcGoodsLinkID) {
			entity.LabcGoodsLinkID = labcGoodsLinkID;
		}
		var ProdDate = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate");
		var InvalidDate = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate");
		var RegisterInvalidDate = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterInvalidDate");

		if (ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		if (InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);
		if (RegisterInvalidDate) entity.RegisterInvalidDate = JShell.Date.toServerDate(RegisterInvalidDate);

		var GoodsQty = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty");
		var Price = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price");
		var AcceptCount = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount");
		var RefuseCount = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount");
		var SumTotal = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal");
		var TaxRate = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TaxRate");
		var GoodsSort = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSort");

		if (GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if (AcceptCount) AcceptCount = parseFloat(AcceptCount);
		else AcceptCount = 0;
		if (RefuseCount) RefuseCount = parseFloat(RefuseCount);
		else RefuseCount = 0;
		if (SumTotal) SumTotal = parseFloat(SumTotal);
		else SumTotal = Price * GoodsQty;

		if (GoodsSort) entity.GoodsSort = GoodsSort;
		if (Price) Price = parseFloat(Price);
		else Price = 0;
		entity.Price = Price;

		if (GoodsQty) entity.GoodsQty = GoodsQty;
		entity.AcceptCount = AcceptCount;
		entity.RefuseCount = RefuseCount;
		entity.SumTotal = SumTotal;

		if (TaxRate) entity.TaxRate = TaxRate;
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		//验收主单
		if (me.PK) {
			entity.ReaBmsCenSaleDocConfirm = {
				Id: me.PK,
				DataTimeStamp: strDataTimeStamp.split(',')
			};
		}
		return entity;
	},
	/***
	 * 计算总计金额
	 */
	getSumTotal: function () {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var SumTotal = 0;
		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var total = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal');
			if (!total) total = 0;
			SumTotal += Number(total);
		}
		return SumTotal;
	},
	/**检验批号是否存在*/
	onCheckLotNo: function (LotNo, GoodsID) {
		var me = this;
		var IsExist = false;
		var url = JShell.System.Path.ROOT + me.selectLotUrl;
		var lotNo2=JShell.String.encode(LotNo);
		url += "&fields=ReaGoodsLot_Id&where=reagoodslot.LotNo='" + lotNo2 + "' and reagoodslot.ReaGoodsNo='" + GoodsID + "'";
		JShell.Server.get(url, function (data) {
			if (data.success) {
				if (data && data.value) {
					var list = data.value.list;
					if (list.length > 0) IsExist = true;
				}
			} else {
				IsExist = false;
			}
		}, false);
		return IsExist;
	},
	/**根据货品id 返回是否 性能验证*/
	getGoodsByID: function (ids, callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectGoodsUrl;
		url += '&fields=ReaGoods_Id,ReaGoods_IsNeedPerformanceTest';
		url += '&where=reagoods.Visible=1 and reagoods.Id in (' + ids + ')';
		JShell.Server.get(url, function (data) {
			if (data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**还原行是否需要性能验证
	 * bo true 只还原知否验证列，不去查批号是否最新
	 * */
	onSetRecIsNeedPerformanceTest: function (records, bo) {
		var me = this;
		var idStr = '';
		me.GoodsEnum = {};
		for (var i = 0; i < records.length; i++) {
			var id = records[i].get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID');
			if (i > 0) idStr += ',';
			idStr += id;
		}
		if (!idStr) return;
		me.getGoodsByID(idStr, function (data) {
			if (data && data.value) {
				var list = data.value.list;
				for (var i = 0; i < list.length; i++) {
					me.GoodsEnum[list[i].ReaGoods_Id] = list[i].ReaGoods_IsNeedPerformanceTest;
				}
			}
		});
		me.store.each(function (record) {
			var id = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID');
			var val = '';
			if (me.GoodsEnum != null) val = me.GoodsEnum[id];
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_IsNeedPerformanceTest', val);
			var LotNo = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo');
			if (!bo) me.onSetLotNo(record, LotNo);
		});
	},
	//批次号判断（是否为新批次)
	onSetLotNo: function (record, LotNo) {
		var me = this;
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_IsLotNoExist', '1');
		var IsNeedPerformanceTest = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_IsNeedPerformanceTest');
		if (IsNeedPerformanceTest == 'true') {
			var ReaGoodsID = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo');
			var IsExist = me.onCheckLotNo(LotNo, ReaGoodsID);
			if (!IsExist) record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_IsLotNoExist', '0');
		}
	},
	//默认的冷链信息
	addNewOfColdInfo: function (addRecord) {
		var me = this;
		//厂家出库温度
		addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_FactoryOutTemperature"] = "5℃";
		//到货温度
		addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ArrivalTemperature"] = "5℃";
		//外观验收
		addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AppearanceAcceptance"] = "完好";
		return addRecord;
	},
	/**
	 * 条码扫码框重新置空及获取焦点
	 */
	setScanCodeFocus: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (buttonsToolbar) {
			txtScanCode = buttonsToolbar.getComponent('txtScanCode');
			if (txtScanCode) {
				txtScanCode.focus(true, 350);
			}
		}
	}
});