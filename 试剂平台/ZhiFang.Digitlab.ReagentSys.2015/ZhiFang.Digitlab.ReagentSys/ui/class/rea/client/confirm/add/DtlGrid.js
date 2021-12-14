/**
 * 客户端验收验货单明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.add.DtlGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '验货单明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlConfirmByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsCenSaleDtlConfirm',
	/**货品扫码服务*/
	scanCodeUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsScanCodeVOOfConfirmByCompIDAndSerialNo',
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
			property: 'BmsCenSaleDtlConfirm_ProdGoodsNo',
			direction: 'ASC'
		},
		{
			property: 'BmsCenSaleDtlConfirm_GoodsUnit',
			direction: 'ASC'
		}
	],
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
	/**封装保存信息的验收明细状态*/
	Status: "0",
	/**是否隐藏复制列*/
	hiddenCopy: false,
	/**货品明细弹出消息框消失时间*/
	hideTimes: 5000,
	/**是否显示货品信息(双击批号单元格选择时隐藏)*/
	IsShowDtlInfo: true,
	/**扫码模式(严格模式:strict,混合模式：mixing)*/
	CodeScanningMode: "mixing",
	OTYPE: "",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on('edit', function(editor, e) {
			//e.record.commit();
			if(me.IsShowDtlInfo == true)
				me.onShowDtlInfo(e.record);
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onScanCodeShowDtl');
		if(me.canEdit == true) {
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				clicksToEdit: 1
			});
		}
		if(!me.checkOne) me.setCheckboxModel();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**@description 刷新数据*/
	onSearch: function() {
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = true;
		this.load(null, true);
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		//me.getView().update();
		if(!me.PK) return false;
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		if(!data || data.list.length <= 0) return data;
		for(var i = 0; i < data.list.length; i++) {
			//验货已扫码记录集合
			var dtlConfirmLinkStr = data.list[i]["BmsCenSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr"];
			if(!dtlConfirmLinkStr) dtlConfirmLinkStr = "";
			//当次扫码记录集合重置为验货已扫码记录集合
			data.list[i]["BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList"] = dtlConfirmLinkStr;
		}
		return data;
	},
	/**按钮的启用或或禁用*/
	setButtonsDisabled: function(disabled) {
		var me = this;
	},
	onFullScreenClick: function() {
		var me = this;
		me.fireEvent('onFullScreenClick', me);
	},
	/**@description 接收数量值改变后联动*/
	onAcceptCountChanged: function(record) {
		JShell.Msg.overwrite('onAcceptCountChanged');
	},
	/**@description 拒收数量值改变后联动*/
	onRefuseCountChanged: function(record) {
		JShell.Msg.overwrite('onRefuseCountChanged');
	},
	/**@description 保存前验证*/
	validatorSave: function() {
		JShell.Msg.overwrite('validatorSave');
	},
	/**@description 获取单个的修改封装信息*/
	getSaveOneInfo: function(record) {
		JShell.Msg.overwrite('getSaveOneInfo');
	},
	/**@description 货品为盒条码时的接收拒收数量输入框的处理*/
	comSetReadOnlyOfBarCodeMgr: function(field, e) {
		var me = this;
		//如果扫码模式为混合模式
		if(me.CodeScanningMode == "mixing") return;

		var isReadOnly = false;
		var record = field.ownerCt.editingPlugin.context.record;
		var barCodeMgr = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
		if(!barCodeMgr) barCodeMgr = "";
		if(barCodeMgr == "1") isReadOnly = true;
		field.setReadOnly(isReadOnly);
	},
	/**@description 设置按钮的调用或禁用*/
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	},
	/**@description 复制拆分*/
	onCopyRecord: function(dataOne) {
		var me = this;
		dataOne.BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id = -1;
		dataOne.BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty = 0;
		dataOne.BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount = 0;
		dataOne.BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount = 0;
		dataOne.BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal = 0;
		dataOne.BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo = "";
		dataOne.BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList = "";
		dataOne.BmsCenSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr = "";
		me.store.add(dataOne);
	},
	deleteOne: function(record) {
		var me = this;
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = 1;
		var showMask = false;
		var id = record.get(me.PKField);
		if(!id || id == "-1") {
			me.delCount++;
			me.store.remove(record);
			if((me.delCount + me.delErrorCount) == me.delLength && me.delErrorCount == 0) {
				me.fireEvent('onDelAfter', me);
			}
		} else {
			if(showMask == false) {
				showMask = true;
				me.showMask(me.delText); //显示遮罩层
			}
			me.delOneById(record, 1, id);
		}
	},
	/**@description 删除一条数据*/
	delOneById: function(record, index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		var confirmSourceType = me.OTYPE;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id + "&confirmSourceType=" + confirmSourceType;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if(data.success) {
					me.store.remove(record);
					me.delCount++;
				} else {
					record.set(me.DelField, false);
					record.set('ErrorInfo', data.msg);
					me.delErrorCount++;
				}
				if(me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.delErrorCount != 0) {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					} else {
						me.fireEvent('onDelAfter', me);
					}
				}
			});
		}, 100 * index);
	},
	/**@description 单价值改变后联动*/
	onPriceChanged: function(record) {
		var me = this;
		var Price = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price');
		var AcceptCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount');
		if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
		else AcceptCount = 0;
		if(Price) Price = parseFloat(Price);
		else Price = 0;

		var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		SumTotal = SumTotal ? SumTotal.toFixed(2) : 0;
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal', SumTotal);
		record.commit();
	},
	/**@description 货品扫码输入框*/
	gettxtScanCode: function() {
		var me = this;
		var txtScanCode = me.getComponent("buttonsToolbar").getComponent("txtScanCode");
		return txtScanCode;
	},
	/**@description 货品扫码时获取扫码方式值*/
	getScanCodeValue: function() {
		var me = this;
		var rboScanCode = me.getComponent("buttonsToolbar").getComponent("rboScanCode");
		var scanCode = rboScanCode.getValue().ScanCode;
		return scanCode;
	},
	/**@description 货品扫码时是否显示浮动窗值*/
	getIShowDtlInfoValue: function() {
		var me = this;
		var iShowDtlInfo = me.getComponent("buttonsToolbar").getComponent("cboIShowDtlInfo");
		return iShowDtlInfo.getValue();
	},
	/**货品扫码显示货品浮动窗体信息*/
	onShowDtlInfo: function(rec) {
		var me = this;
		var info = {
			"CName": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName") : "",
			"EName": rec ? rec.get("BmsCenSaleDtlConfirmVO_ReaGoodsEName") : "",
			"SName": rec ? rec.get("BmsCenSaleDtlConfirmVO_ReaGoodsSName") : "",
			"Unit": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsUnit") : "",
			"UnitMemo": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_UnitMemo") : "",
			"LotNo": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo") : "",
			"InvalidDate": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InvalidDate") : "",
			"AcceptCount": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount") : "",
			"RefuseCount": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount") : "",
			"Price": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price") : "",
			"SumTotal": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal") : ""
		};
		//重置消息框的消失隐藏时间
		me.hideTimes = 5000;
		me.fireEvent('onScanCodeShowDtl', me, info);
	},
	/**@description 选择产品批号*/
	onChooseLotNo: function() {
		var me = this;
		var selected = me.getSelectionModel().getSelection();
		if(!selected || selected.length <= 0) return;
		var record = selected[0];
		var LotNo = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo");
		var ReaGoodsID = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsID");
		var ReaGoodsName = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName");
		var maxWidth = document.body.clientWidth * 0.68;
		var height = document.body.clientHeight * 0.78;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			GoodsID: ReaGoodsID,
			GoodsCName: ReaGoodsName,
			CurLotNo: LotNo,
			listeners: {
				accept: function(p, rec) {
					me.IsShowDtlInfo = true;
					if(rec) {
						record.set("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo", rec.get("ReaGoodsLot_LotNo"));
						record.set("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdDate", rec.get("ReaGoodsLot_ProdDate"));
						record.set("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InvalidDate", rec.get("ReaGoodsLot_InvalidDate"));
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
	 * @description 货品扫码调用服务后,获取到条码货品信息为多个时处理
	 * @param {Object} reaBarCodeVOList
	 * @param {Object} barCode
	 */
	onChooseReaBarCodeVO: function(reaBarCodeVOList, callback) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.68;
		var height = document.body.clientHeight * 0.78;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			listeners: {
				accept: function(p, record) {
					var reaBarCodeVO = null;
					if(record)
						reaBarCodeVO = record.data.ReaBarCodeVO;
					p.close();
					if(callback) callback(reaBarCodeVO);
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
	getBarcodeOperationVO: function(reaBarCodeVO, scanCode) {
		var me = this;
		var operationVO = {
			"Id": -1,
			"OperTypeID": scanCode,
			"ReceiveFlag": scanCode,
			"BDocNo": reaBarCodeVO.BDocNo,
			"BDocID": reaBarCodeVO.BDocID,
			"BDtlID": reaBarCodeVO.BDtlID,
			"SysPackSerial": reaBarCodeVO.SysPackSerial,
			"OtherPackSerial": reaBarCodeVO.OtherPackSerial,
			"UsePackSerial": reaBarCodeVO.UsePackSerial,
			"LotNo": reaBarCodeVO.LotNo
		};
		return operationVO;
	},
	/**@description 货品扫码*/
	onReaGoodsScanCode: function(field, e) {
		var me = this;
		var barCode = field.getValue();
		var indexOf = -1; //条码所在验收明细列表的行索引
		var curRecord = null; //条码所在的行记录
		var curReaGoodsScanCodeList = []; //当前条码为盒条码时的条码明细关系
		me.store.each(function(rec) {
			indexOf++;
			var barCodeMgr = rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
			switch(barCodeMgr) {
				case "0": //批条码
					var lotSerial = rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotSerial");
					if(lotSerial == barCode) {
						curRecord = rec;
						return false;
					}
					break;
				case "1": //盒条码
					curReaGoodsScanCodeList = rec.get("BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList");
					if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);
					if(curReaGoodsScanCodeList) {
						Ext.Array.each(curReaGoodsScanCodeList, function(model) {
							//使用盒条码或系统内部盒条码
							if(model["UsePackSerial"] == barCode || model["SysPackSerial"] == barCode) {
								curRecord = rec;
								return false;
							}
						});
					} else {
						curReaGoodsScanCodeList = [];
					}
					if(curRecord) return false;
					break;
				default:
					break;
			}
		});

		if(curRecord) {
			me.getSelectionModel().select(indexOf);
			var barCodeMgr = curRecord.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
			switch(barCodeMgr) {
				case "0": //批条码
					me.onScanCodeBatchBarCodeExist(curRecord);
					break;
				case "1": //盒条码
					me.onScanCodeOfBoxBarCodeExist(barCode, curRecord, curReaGoodsScanCodeList);
					break;
				default:
					break;
			}
		} else {
			me.onScanCodeUrl(barCode);
		}
	},
	/***
	 * @description 确认验收保存
	 */
	onSaveClick: function() {
		var me = this;
		var saveParams = me.getSaveParams();
		me.fireEvent('onConfirm', me, saveParams);
	},
	/**@description 封装保存的信息*/
	getSaveParams: function() {
		var me = this;
		var saveParams = {},
			dtAddList = [],
			dtEditList = [];
		me.store.each(function(record) {
			var id = record.get(me.PKField);
			var obj = me.getSaveOneInfo(record);
			var operationList = [];
			var barCodeMgr = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
			if(barCodeMgr == "1") {
				//当次扫码记录集合(验货已扫码记录集合+新扫码的条码信息)
				var curReaGoodsScanCodeList = record.get("BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList");
				if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);

				//验货已扫码记录集合
				var dtlConfirmLinkVOList = record.get("BmsCenSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr");
				if(dtlConfirmLinkVOList) dtlConfirmLinkVOList = JcallShell.JSON.decode(dtlConfirmLinkVOList);

				if(!dtlConfirmLinkVOList) dtlConfirmLinkVOList = [];
				if(!curReaGoodsScanCodeList) curReaGoodsScanCodeList = [];
				//当次扫码的条码操作记录是否需要新增到扫码操作记录表
				var isAdd = true;
				//封装当次的条码操作记录
				Ext.Array.each(curReaGoodsScanCodeList, function(curModel, index1) {
					isAdd = true;
					Ext.Array.each(dtlConfirmLinkVOList, function(model, index2) {
						//当次的条码操作记录是否存在验货已扫码记录集合里
						if(curModel.UsePackSerial == model.UsePackSerial) {
							//如果验货已扫码记录集合验收标志与当次扫码的验收标志与原来相同
							if(parseInt(curModel.ReceiveFlag) == parseInt(model.ReceiveFlag))
								isAdd = false;
							return false;
						}
					});
					if(isAdd == true) {
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
				"BmsCenSaleDtlConfirm": obj, //验收明细信息
				"ReaGoodsBarcodeOperationList": operationList, //当次扫码记录集合
			};
			if(!id || id == "-1")
				dtAddList.push(objvo);
			else
				dtEditList.push(objvo);
		});
		if(dtAddList.length > 0) saveParams.dtAddList = dtAddList;
		if(dtEditList.length > 0) saveParams.dtEditList = dtEditList;
		return saveParams;
	},
	getUpdateFields: function(record) {
		var me = this;
		var fields = [
			'Id', 'Status', 'BarCodeMgr', 'GoodsNo', 'ReaGoodsName', 'GoodsUnit', 'UnitMemo', 'ProdGoodsNo', 'ApproveDocNo', 'LotNo', 'InvalidDate', 'Price', 'GoodsQty', 'SumTotal', 'AcceptCount', 'RefuseCount', 'AcceptMemo', 'ProdDate', 'BiddingNo', 'TaxRate', 'RegisterNo', 'RegisterInvalidDate', 'GoodsSerial', 'LotSerial', 'SysLotSerial'
		];
		return fields.join(',');
	},
	/**@description 获取单个的验收明细的基本封装信息*/
	getSaveOneInfo: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		var entity = {
			Id: id,
			Status: me.Status,
			SaleDocConfirmNo: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SaleDocConfirmNo"),
			BarCodeMgr: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr"),
			ReaGoodsID: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsID"),
			ReaGoodsName: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName"),
			OrderGoodsID: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderGoodsID"),
			GoodsNo: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsNo"),
			GoodsUnit: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsUnit"),
			UnitMemo: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_UnitMemo"),
			BiddingNo: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BiddingNo"),
			LotNo: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo"),
			ProdGoodsNo: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdGoodsNo"),
			ApproveDocNo: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ApproveDocNo"),
			RegisterNo: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RegisterNo"),
			AcceptMemo: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptMemo"),

			GoodsSerial: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsSerial"),
			LotSerial: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotSerial"),
			SysLotSerial: record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SysLotSerial")
		};

		var ProdDate = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdDate");
		var InvalidDate = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InvalidDate");
		var RegisterInvalidDate = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RegisterInvalidDate");

		if(ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		if(InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);
		if(RegisterInvalidDate) entity.RegisterInvalidDate = JShell.Date.toServerDate(RegisterInvalidDate);

		var GoodsQty = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty");
		var Price = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price");
		var AcceptCount = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount");
		var RefuseCount = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount");
		var SumTotal = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal");
		var TaxRate = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_TaxRate");

		if(GoodsQty) entity.GoodsQty = GoodsQty;
		if(Price) entity.Price = Price;
		if(AcceptCount) entity.AcceptCount = AcceptCount;
		if(RefuseCount) entity.RefuseCount = RefuseCount;
		if(SumTotal) entity.SumTotal = SumTotal;

		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
		else AcceptCount = 0;
		if(RefuseCount) RefuseCount = parseFloat(RefuseCount);
		else RefuseCount = 0;
		if(Price) Price = parseFloat(Price);
		else Price = 0;
		if(SumTotal) SumTotal = parseFloat(SumTotal);
		else SumTotal = Price * GoodsQty;
		if(TaxRate) entity.TaxRate = TaxRate;
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		//验收主单
		if(me.PK) {
			entity.BmsCenSaleDocConfirm = {
				Id: me.PK,
				DataTimeStamp: strDataTimeStamp.split(',')
			};
		}
		return entity;
	}
});