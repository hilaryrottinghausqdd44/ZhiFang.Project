/**
 * 出库明细
 * @author longfc
 * @version 2019-03-26
 */
Ext.define('Shell.class.rea.client.out.basic.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '出库明细列表',
	width: 800,
	height: 500,
	/**查询库存数据*/
	selectStoreUrl: '/ReaManageService.svc/RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL',
	defaultOrderBy: [{
		property: 'ReaBmsOutDtl_InvalidDate',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultDisableControl: false,
	/**库存新增仪器是否允许为空,1是,0否*/
	IsEquip: '0',
	/**试剂化仪器关系信息*/
	ReaTestEquipVOList: [],
	/**出库类型默认值*/
	defaluteOutType: '1',
	/**出库扫码模式(严格模式:1,混合模式：2)*/
	OutScanCodeModel: '2',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('changeSumTotal');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		return columns;
	},
	/**创建使用仪器列*/
	createEquipNameColumn: function() {
		var me = this;
		var column = {
			dataIndex: 'ReaBmsOutDtl_TestEquipName',
			width: 120,
			hidden: me.defaluteOutType == '1' ? false : true,
			sortable: false,
			text: '<b style="color:blue;">使用仪器</b>',
			editor: new Ext.form.field.ComboBox({
				mode: 'local',
				editable: false,
				displayField: 'TestEquipName',
				valueField: 'TestEquipName',
				listClass: 'x-combo-list-small',
				store: new Ext.data.Store({
					autoLoad: true,
					fields: ['GoodsID', 'GoodsCName', 'TestEquipID', 'TestEquipName'],
					data: []
				}),
				listeners: {
					render: function(field, eOpts) {
						field.getEl().on('click', function(p, el, e) {
							var record = field.ownerCt.editingPlugin.context.record;
							var equipVOList = [];
							var testEquipVOList = record.get("ReaBmsOutDtl_ReaTestEquipVOList");
							if (testEquipVOList) {
								equipVOList = Ext.JSON.decode(testEquipVOList);
							}
							if (equipVOList) field.store.loadData(equipVOList);
						});
					},
					select: function(field, records, eOpts) {
						var record = field.ownerCt.editingPlugin.context.record;
						record.set('ReaBmsOutDtl_TestEquipID', records[0].get("TestEquipID"));
						record.set('ReaBmsOutDtl_TestEquipName', records[0].get("TestEquipName"));
						me.getView().refresh();
					}
				}
			}),
			renderer: function(value, meta, record) {
				return value;
			}
		};
		return column;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			xtype: 'label',
			text: '出库明细',
			style: "font-weight:bold;color:blue;",
			margin: '0 0 5 5'
		}, '-', {
			text: '刷新库存量',
			tooltip: '刷新库存量',
			iconCls: 'button-search',
			handler: function() {
				me.getGoodsQty();
			}
		}];
		return items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.callParent(arguments);
		if (!me.PK) {
			me.store.removeAll();
			var error = me.errorFormat.replace(/{msg}/, "主单ID为空或无出库货品明细信息!");
			me.getView().update(error);
			return false;
		}
	},
	/**数据改变时刷新现有库存量*/
	onAddListeners: function() {
		var me = this;
		me.store.on({
			datachanged: function() {
				me.getGoodsQty();
			}
		});
	},
	/**@description 获取仪器试剂信息*/
	getReaTestEquipVOList: function(isRefresh) {
		var me = this;
		if (isRefresh == true) me.ReaTestEquipVOList = null;
		if (me.ReaTestEquipVOList != null && me.ReaTestEquipVOList.length > 0) return;

		var url = JShell.System.Path.ROOT + '/ReaManageService.svc/ST_UDTO_SearchReaEquipReagentLinkVOList';
		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.ReaTestEquipVOList = data.value;
			} else {
				me.ReaTestEquipVOList = null;
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/***
	 * 根据本次入库数量计算总计金额
	 */
	setSumTotal: function(goodsQty, record) {
		var me = this;
		var price = record.get('ReaBmsOutDtl_Price');
		if (!price) price = 0;
		if (!goodsQty) goodsQty = 0;
		var sumTotal = parseFloat(goodsQty) * parseFloat(price);
		if(!sumTotal)sumTotal=0;
		record.set('ReaBmsOutDtl_SumTotal', sumTotal);
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
			var sumTotal = records[i].get('ReaBmsOutDtl_SumTotal');
			if (!sumTotal) sumTotal = 0;
			count += parseFloat(sumTotal);
		}
		return count;
	},
	/**货品扫码改变行数量、单价、总额*/
	onBarCodeByGoodsQty: function(record, barcode) {
		var me = this;
		//出库数量+1
		var goodsQty = record.get('ReaBmsOutDtl_GoodsQty');
		if (!goodsQty) goodsQty = 0;
		var barcodeCodeList = me.getScanCodeList(record);
		if (!barcodeCodeList) return;

		//是否是大包装
		var iSmallUnit = record.get('ReaBmsOutDtl_ISmallUnit');
		if (!iSmallUnit) iSmallUnit = 0;
		//小单位扫码判读
		if (parseFloat(iSmallUnit) == 1 && barcodeCodeList.length == parseFloat(goodsQty)) return;

		me.onChangeBarCodeQty(record, barcode);
		goodsQty = parseFloat(goodsQty) + 1;
		//条码类型,0批条码，1盒条码
		var barCodeType = record.get('ReaBmsOutDtl_BarCodeType');
		//批条码根据现有库存量判断
		if (barCodeType == '0') {
			//现有库存量
			var GoodsUnit = record.get('ReaBmsOutDtl_GoodsUnit');
			var LotNo = record.get('ReaBmsOutDtl_LotNo');
			var sumCurrentQty = record.get('ReaBmsOutDtl_SumCurrentQty');
			if (!sumCurrentQty) sumCurrentQty = 0;
			sumCurrentQty = parseFloat(sumCurrentQty);
			goodsQty = parseFloat(goodsQty);

			//xx货品出库数为15瓶,货品批号为****的现有库存数为10瓶,不能出库
			if (sumCurrentQty < goodsQty) {
				var msg = '货品名称:【' + record.get('ReaBmsOutDtl_GoodsCName') + '】的出库数为' + goodsQty + GoodsUnit + '货品批号为【' + LotNo +
					'】的现有库存数为' + sumCurrentQty + GoodsUnit + ',不能出库<br>';
				JShell.Msg.alert(msg);
				return;
			}
		}

		var price = record.get('ReaBmsOutDtl_Price');
		if (!price) price = 0;
		var sumTotal = parseFloat(price) * goodsQty;
		record.set('ReaBmsOutDtl_SumTotal', sumTotal);
		record.set('ReaBmsOutDtl_GoodsQty', goodsQty);
		record.commit();
	},
	/**扫码大单位，当前条码，记录扫码次数,可扫码次数*/
	onChangeBarCodeQty: function(rec, barcode, obj) {
		var me = this;
		//条码类型,0批条码，1盒条码
		var barCodeType = rec.get('ReaBmsOutDtl_BarCodeType');
		if (barCodeType == '0') return;

		var scanCodeList = me.getScanCodeList(rec);
		if (!scanCodeList) return;
		for (var i = 0; i < scanCodeList.length; i++) {
			//大单位扫码时 ,条码号相同
			if ((scanCodeList[i].UsePackSerial == barcode) || (scanCodeList[i].SysPackSerial == barcode)) {
				//当前条码剩余的可扫次数:OverageQty
				scanCodeList[i].OverageQty = parseFloat(scanCodeList[i].OverageQty) - 1;
				var scanCodeQty = 0;
				//parseFloat(scanCodeList[i].OverageQty == 9) || 
				if ((parseFloat(scanCodeList[i].OverageQty == 0) && parseFloat(scanCodeList[i].GonvertQty) == 1)) {
					scanCodeQty = parseFloat(scanCodeList[i].ScanCodeQty);
				} else {
					scanCodeQty = parseFloat(scanCodeList[i].ScanCodeQty) + 1;
				}
				scanCodeList[i].ScanCodeQty = scanCodeQty;
			}
		}
		rec.set('ReaBmsOutDtl_CurReaGoodsScanCodeList', Ext.encode(scanCodeList));
	},
	/**获取本次扫码记录数组*/
	getScanCodeList: function(record) {
		var me = this;
		var curScanCodeList = [];
		var scanCodeList = record.get('ReaBmsOutDtl_CurReaGoodsScanCodeList');
		if (scanCodeList) curScanCodeList = Ext.JSON.decode(scanCodeList);
		return curScanCodeList;
	},
	/**新增出库货品记录
	 * 1.根据供应商+货品+批号+库房+货架判断，
	 * 明细存在一样的就不再添加
	 * */
	addRecordOne: function(qtyRec, barcode) {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		if (!qtyRec) return;

		var record = me.doesItExistRec(qtyRec); // 存在record就为空，不出在就将record赋值为选中的数据
		if (record) {
			//原来的处理
			//me.onSetJObjectBarCode(record, barcode, qtyRec.data);

			//条码类型,0批条码,1盒条码
			var barCodeType = record.get('ReaBmsOutDtl_BarCodeType');
			//盒条码扫码验证
			if (barCodeType == '1') {
				var isExec2 = me.checkBarCode(record, barcode);
				if (!isExec2) return;
			}
			me.onCheckBarCode(record, barcode, qtyRec);
		}
		if (!record) { // 当明细表中没有这条选中的数据（qtyRec是库存表中的）
			var outDtlObj = me.createRowObj(qtyRec, barcode);
			me.store.insert(me.store.getCount(), outDtlObj);
			me.fireEvent('changeSumTotal');
			if (me.store.getCount() > 0) {
				var rowIndex = me.getStore().getCount() - 1;
				var record2 = me.getStore().getAt(rowIndex);
				me.getQtyCount(record2);
			}
		}
	},
	/**判断选择的库存货品是否存在出库明细列表里 */
	doesItExistRec: function(rec, reaBmsQtyDtlTab) {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var isExist = true,
			record = null;
		if (!reaBmsQtyDtlTab) {
			//简捷出库时,qtyRec是obj
			if (rec['ReaBmsQtyDtl_Tab']) {
				reaBmsQtyDtlTab = rec['ReaBmsQtyDtl_Tab'];
			} else {
				reaBmsQtyDtlTab = rec.get('ReaBmsQtyDtl_Tab');
			}
		}
		//根据供应商+货品+批号+库房+货架判断
		for (var i = 0; i < len; i++) {
			var reaBmsOutDtlTab = records[i].get('ReaBmsOutDtl_Tab');
			if (reaBmsOutDtlTab == reaBmsQtyDtlTab) {
				record = records[i];
				isExist = false;
				break;
			}
		}
		return record;
	},
	/**
	 * @description 处理出库明细的货品扫码信息
	 * */
	onSetJObjectBarCode: function(record, barcode, qtyObj) {
		var me = this;
		var jObjectBarCode = "";
		//简捷出库时,qtyRec是obj
		if (qtyObj && qtyObj.data) {
			jObjectBarCode = qtyObj.get('ReaBmsQtyDtl_JObjectBarCode');
		} else if (qtyObj) {
			jObjectBarCode = qtyObj['ReaBmsQtyDtl_JObjectBarCode'];
		}
		if (!jObjectBarCode) jObjectBarCode = "";
		if (jObjectBarCode) jObjectBarCode = Ext.decode(jObjectBarCode);

		var goodsUnit = record.get('ReaBmsOutDtl_GoodsUnit');
		var lotNo = record.get('ReaBmsOutDtl_LotNo');
		var sumCurrentQty = record.get('ReaBmsOutDtl_SumCurrentQty');
		if (!sumCurrentQty) sumCurrentQty = 0;
		sumCurrentQty = parseFloat(sumCurrentQty);

		var goodsQty = record.get('ReaBmsOutDtl_GoodsQty');
		if (!goodsQty) goodsQty = 0;
		goodsQty = parseFloat(goodsQty);
		//xx货品出库数为15瓶,货品批号为****的现有库存数为10瓶,不能出库
		if (sumCurrentQty == goodsQty || sumCurrentQty < goodsQty) {
			var msg = '货品名称:【' + record.get('ReaBmsOutDtl_GoodsCName') + '】的出库数为' + goodsQty + goodsUnit + '货品批号为【' + lotNo +
				'】的现有库存数为' + sumCurrentQty + goodsUnit + ',不能出库<br>';
			JShell.Msg.alert(msg);
			return;
		}

		//出库明细JObjectBarCode不存在并且库存数据存在时,从选择行取值赋值给出库明细
		var scanCodeList = me.getScanCodeList(record);
		//条码类型,0批条码,1盒条码
		var barCodeType = record.get('ReaBmsOutDtl_BarCodeType');
		if (jObjectBarCode && scanCodeList.length == 0 && barCodeType == '1') {
			var gonvertQty = jObjectBarCode.GonvertQty;
			//根据扫码得到的换算系数，判断当前条码是大包装还是小包装
			var ISmallUnit = parseFloat(gonvertQty) > 1 ? 0 : 1;
			record.set('ReaBmsOutDtl_ISmallUnit', ISmallUnit);
			//当前条码的本次扫码次数:ScanCodeQty
			var scanCodeQty = jObjectBarCode.ScanCodeQty;
			if (!scanCodeQty) scanCodeQty = 1;
			//当前条码剩余的可扫次数:OverageQty
			var overageQty = jObjectBarCode.OverageQty;
			if (!overageQty) overageQty = 1;
			//现有库存量=当前条码的OverageQty（剩余量）
			record.set('ReaBmsOutDtl_DefaulteGoodsQty', overageQty);
			overageQty = parseFloat(overageQty) - 1;
			jObjectBarCode.OverageQty = overageQty;
			var scanCodeList2 = [];
			if (jObjectBarCode) scanCodeList2.push(jObjectBarCode);
			record.set('ReaBmsOutDtl_CurReaGoodsScanCodeList', Ext.encode(scanCodeList2));
			record.set('ReaBmsOutDtl_GoodsQty', goodsQty + 1);
			//if(goodsQty == 1) return;
		}
		me.onCheckBarCode(record, barcode, qtyObj);
		if (!barcode) {
			JShell.Msg.alert('当前行数据已选择', null, 2000);
			return;
		}
	},
	/**创建新增行前条码处理*/
	onCheckBarCode: function(record, barcode, qtyObj) {
		var me = this;
		if (!barcode) return;

		var barCodeType = record.get('ReaBmsOutDtl_BarCodeType');
		//		//条码类型,0批条码,1盒条码		
		//		//盒条码扫码验证
		//		if(barCodeType == '1') {
		//			var isExec2 = me.checkBarCode(record, barcode);
		//			if(!isExec2) return;
		//		}		
		//盒条码处理
		if (barCodeType == '1') {
			//简捷出库时,qtyRec是obj
			var jObjectBarCode = "";
			if (qtyObj['ReaBmsQtyDtl_JObjectBarCode']) {
				jObjectBarCode = qtyObj['ReaBmsQtyDtl_JObjectBarCode'];
			} else {
				jObjectBarCode = qtyObj.get('ReaBmsQtyDtl_JObjectBarCode');
			}
			if (jObjectBarCode) jObjectBarCode = Ext.decode(jObjectBarCode);
			me.setCurReaGoodsScanCodeList(record, barcode, jObjectBarCode);
		} else if (barCodeType == '0') { //批条码处理
			me.onBarCodeByGoodsQty(record, barcode);
		}
	},
	/**判断是否已扫码
	 * */
	checkBarCode: function(record, barcode) {
		var me = this;
		var isExec = true;
		//判断是否已扫码
		if (!barcode) return;
		//条码类型,0批条码，1盒条码
		var barCodeType = record.get('ReaBmsOutDtl_BarCodeType');
		//是否是大包装
		var iSmallUnit = record.get('ReaBmsOutDtl_ISmallUnit');
		if (!iSmallUnit) iSmallUnit = 0;
		var isExec = true;
		var scanCodeList = me.getScanCodeList(record);
		if (barCodeType == '1') {
			for (var i = 0; i < scanCodeList.length; i++) {
				//当前条码可剩余扫码次数计算 || barcode == scanCodeList[i].UsePackQRCode
				if ((barcode == scanCodeList[i].UsePackSerial || barcode == scanCodeList[i].SysPackSerial || barcode ==
						scanCodeList[i].UsePackQRCode)) {
					//parseFloat(iSmallUnit) == 0 && 
					if (scanCodeList[i].OverageQty == 0) { //大单位
						var info = "条码为:" + barcode + ",已扫码,并且剩余可扫码次数为0,请不要重复扫码!";
					} else {
						var info = "条码为:" + barcode + ",已扫码,请不要重复扫码!";
					}
					JShell.Msg.error(info);
					isExec = false;
					break;
				}
			}
		}
		return isExec;
	},
	/**
	 * 创建新增行前条码处理
	 * 盒条码处理
	 * */
	setCurReaGoodsScanCodeList: function(record, barcode, jObjectBarCode) {
		var me = this;
		//用于记录扫大包装单位时计算每盒扫码次数
		var barcodeCodeList = me.getScanCodeList(record);
		var isExec = true;
		for (var i = 0; i < barcodeCodeList.length; i++) {
			if (barcode == barcodeCodeList[i].UsePackSerial || barcode == barcodeCodeList[i].UsePackQRCode || barcode ==
				barcodeCodeList[i].SysPackSerial) {
				isExec = false;
				break;
			}
		}
		if (!isExec == true) return;

		//当前条码的本次扫码次数:ScanCodeQty
		var scanCodeQty = jObjectBarCode.ScanCodeQty;
		if (!scanCodeQty) scanCodeQty = 1;
		jObjectBarCode.ScanCodeQty = scanCodeQty;
		//当前条码剩余的可扫次数:OverageQty
		var overageQty = jObjectBarCode.OverageQty;
		if (!overageQty) overageQty = 1;
		record.set('ReaBmsOutDtl_DefaulteGoodsQty', overageQty);
		jObjectBarCode.OverageQty = overageQty;
		barcodeCodeList.push(jObjectBarCode);
		record.set('ReaBmsOutDtl_CurReaGoodsScanCodeList', Ext.encode(barcodeCodeList));
		me.onBarCodeByGoodsQty(record, barcode);
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
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
		var goodsID = rec.get('ReaBmsOutDtl_DefaulteGoodsID');
		if (!goodsID) goodsID = rec.get('ReaBmsOutDtl_GoodsID');
		var lotNo = rec.get('ReaBmsOutDtl_LotNo');
		var reaCompanyID = rec.get('ReaBmsOutDtl_ReaCompanyID');
		var storageID = rec.get('ReaBmsOutDtl_StorageID');
		var placeID = rec.get('ReaBmsOutDtl_PlaceID');
		var invalidDate = rec.get('ReaBmsOutDtl_InvalidDate');
		var qtyDtlID = rec.get('ReaBmsOutDtl_QtyDtlID');
		var reaGoodsNo = rec.get('ReaBmsOutDtl_ReaGoodsNo');

		var barCodeQtyList = me.getScanCodeList(rec);
		var obj = {
			GoodsID: goodsID,
			LotNo: lotNo,
			ReaCompanyID: reaCompanyID,
			StorageID: storageID,
			PlaceID: placeID,
			InvalidDate: invalidDate,
			ReaGoodsNo: reaGoodsNo
		};
		if (barCodeQtyList.length > 0) obj.QtyDtlID = qtyDtlID;
		return obj;
	},
	getSumQtyObj2: function(entity) {
		var me = this;
		var goodsID = entity.ReaBmsOutDtl_DefaulteGoodsID;
		if (!goodsID && entity.ReaBmsOutDtl_GoodsID) goodsID = entity.ReaBmsOutDtl_GoodsID;
		var lotNo = entity.ReaBmsOutDtl_LotNo;
		var reaCompanyID = entity.ReaBmsOutDtl_ReaCompanyID;
		var storageID = entity.ReaBmsOutDtl_StorageID;
		var placeID = entity.ReaBmsOutDtl_PlaceID;
		var invalidDate = entity.ReaBmsOutDtl_InvalidDate;
		var reaGoodsNo = entity.ReaBmsOutDtl_ReaGoodsNo;
		var obj = {
			GoodsID: goodsID,
			ReaGoodsNo: reaGoodsNo,
			LotNo: lotNo,
			ReaCompanyID: reaCompanyID,
			StorageID: storageID,
			PlaceID: placeID,
			InvalidDate: invalidDate
		};
		return obj;
	},
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
		var dtlHql = "reabmsoutdtl.ReaGoodsNo='" + obj.ReaGoodsNo + "'" +
			" and reabmsoutdtl.LotNo='" + obj.LotNo + "'" +
			" and reabmsoutdtl.ReaCompanyID='" + obj.ReaCompanyID + "'" +
			" and reabmsoutdtl.StorageID='" + obj.StorageID + "'" +
			" and reabmsoutdtl.GoodsQty>0";
		if (obj.PlaceID) {
			dtlHql += " and reabmsoutdtl.PlaceID='" + obj.PlaceID + "'";
		}
		qtyHql=JShell.String.encode(qtyHql);
		dtlHql=JShell.String.encode(dtlHql);
		url += '?dtlType=ReaBmsOutDtl&qtyHql=' + qtyHql + '&dtlHql=' + dtlHql + '&goodsId=' + obj.GoodsID;
		return url;
	},
	/**
	 * @description 获取当前剩余总库存值
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
	 * @description 设置当前剩余总库存值
	 * */
	setSumQty: function(rec, list) {
		var me = this;
		if (!list) return;
		//当前剩余总库存
		var sumCurrentQty = list.SumCurrentQty;
		if (!sumCurrentQty) sumCurrentQty = 0;
		rec.set('ReaBmsOutDtl_SumCurrentQty', sumCurrentQty);
		//已申请数
		if (rec.data.ReaBmsOutDtl_SumDtlGoodsQty) {
			var sumDtlGoodsQty = list.SumDtlGoodsQty;
			if (!sumDtlGoodsQty) sumDtlGoodsQty = 0;
			rec.set('ReaBmsOutDtl_SumDtlGoodsQty', sumDtlGoodsQty);
		}
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
	/**将选择的库存记录行转换为出库明细记录行*/
	createRowObj: function(recQty, barcode) {
		var me = this;
		//创建行对象
		var outDtlObj = me.createOutDtlObj(recQty.data);
		//出库扫码模式判断
		outDtlObj = me.createScanCodeModel(outDtlObj, barcode);
		outDtlObj = me.createBarcodeJObject(outDtlObj, recQty, barcode);
		return outDtlObj;
	},
	/**将选择的库存记录行转换为出库明细记录行
	*里面添加了货运单号以及两个条件列
	* 还添加了"上次的批号"和"上次的货运单号"
	*/
	createOutDtlObj: function(qtyDtlObj) {
		var me = this;
		var Price = qtyDtlObj['ReaBmsQtyDtl_Price'];
		if (!Price) Price = 0;
		var SumTotal = parseFloat(Price) * 1;
		var outDtlObj = {
			ReaBmsOutDtl_Tab: qtyDtlObj['ReaBmsQtyDtl_Tab'],
			ReaBmsOutDtl_QtyDtlID: qtyDtlObj['ReaBmsQtyDtl_Id'],
			ReaBmsOutDtl_GoodsNo: qtyDtlObj['ReaBmsQtyDtl_GoodsNo'],
			ReaBmsOutDtl_GoodsID: qtyDtlObj['ReaBmsQtyDtl_GoodsID'],
			ReaBmsOutDtl_GoodsCName: qtyDtlObj['ReaBmsQtyDtl_GoodsName'],
			ReaBmsOutDtl_SName: qtyDtlObj['ReaBmsQtyDtl_SName'],
			ReaBmsOutDtl_ProdOrgName: qtyDtlObj['ReaBmsQtyDtl_ProdOrgName'],
			ReaBmsOutDtl_UnitMemo: qtyDtlObj['ReaBmsQtyDtl_UnitMemo'],
			ReaBmsOutDtl_GoodsUnit: qtyDtlObj['ReaBmsQtyDtl_GoodsUnit'],
			ReaBmsOutDtl_Price: qtyDtlObj['ReaBmsQtyDtl_Price'],
			ReaBmsOutDtl_GoodsQty: 1,
			ReaBmsOutDtl_SumTotal: SumTotal,
			ReaBmsOutDtl_DefaulteGoodsID: qtyDtlObj['ReaBmsQtyDtl_GoodsID'],
			ReaBmsOutDtl_ReaCompanyID: qtyDtlObj['ReaBmsQtyDtl_ReaCompanyID'],
			ReaBmsOutDtl_LotNo: qtyDtlObj['ReaBmsQtyDtl_LotNo'],
			ReaBmsOutDtl_LastLotNo: qtyDtlObj['ReaBmsQtyDtl_LastLotNo'],// 上一次的批号
			isChanageLotNo: qtyDtlObj['isChanageLotNo'],// 新加的，判断改变的标志列
			ReaBmsOutDtl_TransportNo: qtyDtlObj['ReaBmsQtyDtl_TransportNo'], // 货运单号，加上的
			ReaBmsOutDtl_LastTransportNo: qtyDtlObj['ReaBmsQtyDtl_LastTransportNo'], // 上次的货运单号，加上的
			isChanageTransportNo: qtyDtlObj['isChanageTransportNo'], // 新加的，判断改变的标志列
			ReaBmsOutDtl_InvalidDate: qtyDtlObj['ReaBmsQtyDtl_InvalidDate'],
			ReaBmsOutDtl_CompanyName: qtyDtlObj['ReaBmsQtyDtl_CompanyName'],
			ReaBmsOutDtl_RegisterNo: qtyDtlObj['ReaBmsQtyDtl_RegisterNo'],
			ReaBmsOutDtl_DefaulteGoodsQty: qtyDtlObj['ReaBmsQtyDtl_GoodsQty'],
			ReaBmsOutDtl_StorageID: qtyDtlObj['ReaBmsQtyDtl_StorageID'],
			ReaBmsOutDtl_PlaceID: qtyDtlObj['ReaBmsQtyDtl_PlaceID'],
			ReaBmsOutDtl_StorageName: qtyDtlObj['ReaBmsQtyDtl_StorageName'],
			ReaBmsOutDtl_PlaceName: qtyDtlObj['ReaBmsQtyDtl_PlaceName'],
			ReaBmsOutDtl_GoodsSerial: qtyDtlObj['ReaBmsQtyDtl_GoodsSerial'],
			ReaBmsOutDtl_LotSerial: qtyDtlObj['ReaBmsQtyDtl_LotSerial'],
			ReaBmsOutDtl_SysLotSerial: qtyDtlObj['ReaBmsQtyDtl_SysLotSerial'],
			ReaBmsOutDtl_CompGoodsLinkID: qtyDtlObj['ReaBmsQtyDtl_CompGoodsLinkID'],
			ReaBmsOutDtl_ReaServerCompCode: qtyDtlObj['ReaBmsQtyDtl_ReaServerCompCode'],
			ReaBmsOutDtl_ProdDate: qtyDtlObj['ReaBmsQtyDtl_ProdDate'],
			ReaBmsOutDtl_BarCodeType: qtyDtlObj['ReaBmsQtyDtl_BarCodeType'],
			ReaBmsOutDtl_Memo: qtyDtlObj['ReaBmsQtyDtl_Memo'],
			ReaBmsOutDtl_TaxRate: qtyDtlObj['ReaBmsQtyDtl_TaxRate'],
			ReaBmsOutDtl_ReaGoodsNo: qtyDtlObj['ReaBmsQtyDtl_ReaGoodsNo'],
			ReaBmsOutDtl_CenOrgGoodsNo: qtyDtlObj['ReaBmsQtyDtl_CenOrgGoodsNo'],
			ReaBmsOutDtl_LotQRCode: qtyDtlObj['ReaBmsQtyDtl_LotQRCode'],
			ReaBmsOutDtl_ReaCompCode: qtyDtlObj['ReaBmsQtyDtl_ReaCompCode'],
			ReaBmsOutDtl_GoodsSort: qtyDtlObj['ReaBmsQtyDtl_GoodsSort'],
			ReaBmsOutDtl_BarCodeQtyDtlID: qtyDtlObj['ReaBmsQtyDtl_Id'],
			ReaBmsOutDtl_JObjectBarCode: qtyDtlObj['ReaBmsQtyDtl_JObjectBarCode'] 
		};
		return outDtlObj;
	},
	createScanCodeModel: function(outDtlObj, barcode) {
		var me = this;
		/**出库扫码模式(严格模式:1,混合模式：2)*/
		var type = me.OutScanCodeModel + '';
		if (type == '1') {
			var goodsQty = 0
			if (barcode) goodsQty = 1;
			outDtlObj.ReaBmsOutDtl_GoodsQty = goodsQty;
		}
		return outDtlObj;
	},
	/**扫码存在返回值，处理*/
	createBarcodeJObject: function(outDtlObj, recQty, barcode, jObjectBarCode) {
		var me = this;
		if (!jObjectBarCode) jObjectBarCode = recQty.get('ReaBmsQtyDtl_JObjectBarCode');
		//盒条码 && 扫码有返回值
		if (outDtlObj.ReaBmsOutDtl_BarCodeType == '1' && jObjectBarCode && barcode) {
			jObjectBarCode = Ext.JSON.decode(jObjectBarCode);
			var gonvertQty = 1;
			if (jObjectBarCode) gonvertQty = jObjectBarCode.GonvertQty;
			//根据扫码得到的换算系数，判断当前条码是大包装还是小包装
			outDtlObj.ReaBmsOutDtl_ISmallUnit = parseFloat(gonvertQty) > 1 ? 0 : 1;
			//当前条码的本次扫码次数:ScanCodeQty
			var scanCodeQty = jObjectBarCode.ScanCodeQty;
			if (!scanCodeQty) scanCodeQty = 1;
			//现有库存量=当前条码的OverageQty（剩余量）
			outDtlObj.ReaBmsOutDtl_DefaulteGoodsQty = scanCodeQty;
			//当前条码剩余的可扫次数:OverageQty
			var overageQty = jObjectBarCode.OverageQty;
			if (!overageQty) overageQty = 1;
			//现有库存量=当前条码的OverageQty（剩余量）
			outDtlObj.ReaBmsOutDtl_DefaulteGoodsQty = overageQty;
			overageQty = parseFloat(overageQty) - 1;
			jObjectBarCode.OverageQty = overageQty;
			var curReaGoodsScanCodeList = [];
			curReaGoodsScanCodeList.push(jObjectBarCode);
			outDtlObj.ReaBmsOutDtl_CurReaGoodsScanCodeList = Ext.encode(curReaGoodsScanCodeList);
		}
		return outDtlObj;
	},
	/**判断批条码是否需要调用扫码服务*/
	getLotNoIsScanCode: function(barcode, qtyGrid) {
		var me = this;
		//批条码处理,根据条码号先从明细找，如果已存在相同批条码 不再去调用条码扫码服务
		var records = me.store.data.items,
			len = records.length;
		var isExec = false;
		//库存表是否有数据
		var len2 = 0;
		if (qtyGrid) len2 = qtyGrid.getStore().getCount();
		for (var i = 0; i < len; i++) {
			//条码类型,0批条码，1盒条码
			var barCodeType = records[i].get('ReaBmsOutDtl_BarCodeType'); // 条码类型
			var lotSerial = records[i].get('ReaBmsOutDtl_LotSerial'); // 批号条码
			var sysLotSerial = records[i].get('ReaBmsOutDtl_SysLotSerial'); // 系统内部批号条码 
			if (barCodeType == '0') { // && len2 > 0
				if (barcode == lotSerial || barcode == sysLotSerial) { // 如果文本域中的值为系统内部批号条码或系统内部批号条码
					isExec = true;
					me.onBarCodeByGoodsQty(records[i], barcode);
					//continue; // 跳出本次循环
					break; // 跳出整个循环
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
	/**@description 出库数编辑单元格是否可编辑*/
	comSetReadOnlyOfBarCodeType: function(field, e) {
		var me = this;
		var record = field.ownerCt.editingPlugin.context.record;
		var barCodeType = record.get("ReaBmsOutDtl_BarCodeType");
		//如果扫码模式为严格模式,批条码及盒条码需要扫码&&barCodeMgr=="1"
		if (me.OutScanCodeModel == "1" && barCodeType == "1") {
			field.setReadOnly(true);
			return;
		} else {
			field.setReadOnly(false);
		}
	},
	/**创建使用仪器*/
	createTestEquipItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push('-', {
			fieldLabel: '使用仪器',
			name: 'TestEquipName',
			itemId: 'TestEquipName',
			xtype: 'uxCheckTrigger',
			emptyText: '使用仪器',
			width: 195,
			labelWidth: 65,
			className: 'Shell.class.rea.client.equip.lab.CheckGrid',
			classConfig: {
				checkOne: true,
				width: 280,
				height: 450,
				/**按登录帐号所属部门获取部门仪器信息*/
				selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaTestEquipLabByEmpDeptHQL?isPlanish=true',
			},
			listeners: {
				check: function(p, record) {
					me.onTestEquipAccept(p, record);
				}
			}
		}, {
			fieldLabel: '使用仪器ID',
			hidden: true,
			xtype: 'textfield',
			name: 'TestEquipID',
			itemId: 'TestEquipID'
		});
		return items;
	},
	/**使用仪器选择后处理*/
	onTestEquipAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var testEquipID = buttonsToolbar.getComponent('TestEquipID');
		var testEquipName = buttonsToolbar.getComponent('TestEquipName');
		if (record == null) {
			testEquipName.setValue('');
			testEquipID.setValue('');
			p.close();
			return;
		}
		testEquipName.setValue(record.get("ReaTestEquipLab_CName"));
		testEquipID.setValue(record.get("ReaTestEquipLab_Id"));
		p.close();
	},
	/**将库存货品添加到出库明细时,使用仪器赋值处理*/
	setOutDtlObjTestEquipValue: function(recQty, outDtlObj, goodsID) {
		var me = this;
		//仪器默认值（存在多个时返回第一个）
		var equipList = [];
		var defaultId = "",
			defaultName = "";
		if (!goodsID) {
			goodsID = recQty.get('ReaBmsQtyDtl_GoodsID');
		}
		if (!me.ReaTestEquipVOList || me.ReaTestEquipVOList.length == 0) me.getReaTestEquipVOList(true);
		if (me.ReaTestEquipVOList && me.ReaTestEquipVOList.length > 0) {
			for (var i = 0; i < me.ReaTestEquipVOList.length; i++) {
				equipList = [];
				var item = me.ReaTestEquipVOList[i];
				if (goodsID == item.GoodsID) {
					equipList = item.ReaTestEquipVOList;
					if (equipList.length > 0) {
						defaultId = equipList[0].TestEquipID;
						defaultName = equipList[0].TestEquipName;
						outDtlObj.ReaBmsOutDtl_ReaTestEquipVOList = Ext.encode(equipList);
						break;
					}
				}
			}
		}
		//如果存在手工指定了使用仪器,优先考虑手工指定的使用仪器
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (buttonsToolbar) {
			var testEquipID = buttonsToolbar.getComponent('TestEquipID');
			var testEquipName = buttonsToolbar.getComponent('TestEquipName');
			if (testEquipID && testEquipID.getValue() && equipList.length > 0) {
				//判断手工指定的仪器是否在部门仪器中
				for (var i = 0; i < equipList.length; i++) {
					if (testEquipID.getValue() == equipList[i].TestEquipID) {
						defaultId = testEquipID.getValue();
						defaultName = testEquipName.getValue();
						break;
					}
				}
			}
		}
		outDtlObj.ReaBmsOutDtl_TestEquipID = defaultId;
		outDtlObj.ReaBmsOutDtl_TestEquipName = defaultName;
		return outDtlObj;
	},
	/**
	 * 出库保存校验
	 * 直接出库时:出库数量不能为0;
	 * 确认出库时:如果是对出库申请进行确认出库,出库数量可以为0;
	 */
	onSaveCheck: function(isAllowZero) {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		if (isAllowZero != true) isAllowZero = false;
		var isExec = true;
		var msg = '';
		if (len == 0) {
			msg = '出库明细,不能为空';
			isExec = false;
		}
		for (var i = 0; i < len; i++) {
			var goodsCName = records[i].get('ReaBmsOutDtl_GoodsCName');
			//实际出库数量
			var godsQty = records[i].get('ReaBmsOutDtl_GoodsQty');
			var reqGoodsQty = records[i].get('ReaBmsOutDtl_ReqGoodsQty');
			if (!godsQty) godsQty = 0;
			if (!reqGoodsQty) reqGoodsQty = godsQty;
			godsQty = parseFloat(godsQty);
			if (isAllowZero == false) {
				if (godsQty <= 0) {
					msg += '货品名称:【' + goodsCName + '】的出库数量小于或等于0,不能出库<br>';
					isExec = false;
				}
			}
			var goodsUnit = records[i].get('ReaBmsOutDtl_GoodsUnit');
			var lotNo = records[i].get('ReaBmsOutDtl_LotNo');
			var sumCurrentQty = records[i].get('ReaBmsOutDtl_SumCurrentQty');
			if (!sumCurrentQty) sumCurrentQty = 0;
			sumCurrentQty = parseFloat(sumCurrentQty);
			//如果现有库存量数大于0小于1,出库数与现有库存量数比较时,现有库存量数统一按等于1处理
			if (sumCurrentQty > 0 && sumCurrentQty < 1) sumCurrentQty = 1;

			//xx货品出库数为15瓶,货品批号为****的现有库存数为10瓶,不能出库
			if (sumCurrentQty < godsQty) {
				msg += '货品名称:【' + goodsCName + '】的出库数为' + godsQty + goodsUnit + ',货品批号为【' + lotNo + '】的现有库存数为' + sumCurrentQty +
					goodsUnit + ',不能出库<br>';
				isExec = false;
			}
			var EquipID = records[i].get('ReaBmsOutDtl_TestEquipID');
			//使用出库才需要填写仪器
			if (me.IsEquip == '1' && !EquipID && me.defaluteOutType == '1') {
				msg += '货品名称:【' + goodsCName + '】的仪器为空,不能出库<br>';
				isExec = false;
			}
		}
		if (!isExec) {
			JShell.Msg.error(msg);
		}
		return isExec;
	},
	
	// 新增货运单号，上次批号，上次货运单号
	getEntity: function(rec) {
		var me = this;
		var Id = rec.get('ReaBmsOutDtl_Id');
		var ReaCompanyID = rec.get('ReaBmsOutDtl_ReaCompanyID');
		var TaxRate = rec.get('ReaBmsOutDtl_TaxRate');
		var GoodsID = rec.get('ReaBmsOutDtl_GoodsID');
		//var QtyDtlID = rec.get('ReaBmsOutDtl_QtyDtlID');
		var reqCurrentQty = rec.get('ReaBmsOutDtl_DefaulteGoodsQty');
		var godsQty = rec.get('ReaBmsOutDtl_GoodsQty');
		var reqGoodsQty = rec.get('ReaBmsOutDtl_ReqGoodsQty');
		var price = rec.get('ReaBmsOutDtl_Price');
		var sumTotal = rec.get('ReaBmsOutDtl_SumTotal');
		if (!sumTotal) sumTotal = 0;
		var StorageID = rec.get('ReaBmsOutDtl_StorageID');
		var PlaceID = rec.get('ReaBmsOutDtl_PlaceID');
		var CompGoodsLinkID = rec.get('ReaBmsOutDtl_CompGoodsLinkID');
		var BarCodeType = rec.get('ReaBmsOutDtl_BarCodeType');
		var ProdDate = rec.get('ReaBmsOutDtl_ProdDate');
		var InvalidDate = rec.get('ReaBmsOutDtl_InvalidDate');
		var TestEquipID = rec.get('ReaBmsOutDtl_TestEquipID');
		var Sysdate = JcallShell.System.Date.getDate();
		var DataAddTime = JcallShell.Date.toString(Sysdate);
		var GoodsSort = rec.get('ReaBmsOutDtl_GoodsSort');
		var Memo = rec.get('ReaBmsOutDtl_Memo');
		var obj = {
			StorageName: rec.get('ReaBmsOutDtl_StorageName'),
			PlaceName: rec.get('ReaBmsOutDtl_PlaceName'),
			CompanyName: rec.get('ReaBmsOutDtl_CompanyName'),
			ReaCompCode: rec.get('ReaBmsOutDtl_ReaCompCode'),
			ReaServerCompCode: rec.get('ReaBmsOutDtl_ReaServerCompCode'),
			GoodsCName: rec.get('ReaBmsOutDtl_GoodsCName'),
			GoodsUnit: rec.get('ReaBmsOutDtl_GoodsUnit'),
			GoodsNo: rec.get('ReaBmsOutDtl_GoodsNo'),
			CenOrgGoodsNo: rec.get('ReaBmsOutDtl_CenOrgGoodsNo'),
			ReaGoodsNo: rec.get('ReaBmsOutDtl_ReaGoodsNo'),
			UnitMemo: rec.get('ReaBmsOutDtl_UnitMemo'),
			LotNo: rec.get('ReaBmsOutDtl_LotNo'),
			LastLotNo: rec.get('ReaBmsOutDtl_LastLotNo'), // 上次的批号
			TransportNo: rec.get('ReaBmsOutDtl_TransportNo'), // 新增货运单号
			LastTransportNo: rec.get('ReaBmsOutDtl_LastTransportNo'), // 上一次的货运单号
			ProdGoodsNo: rec.get('ReaBmsOutDtl_ProdGoodsNo'),
			GoodsSerial: rec.get('ReaBmsOutDtl_GoodsSerial'),
			SysLotSerial: rec.get('ReaBmsOutDtl_SysLotSerial'),
			LotSerial: rec.get('ReaBmsOutDtl_LotSerial'),
			LotQRCode: rec.get('ReaBmsOutDtl_LotQRCode'),
			TestEquipName: rec.get('ReaBmsOutDtl_TestEquipName'),
			Visible: 1,
			Memo: Memo
		}
		if (Id) obj.Id = Id;
		if (ReaCompanyID) obj.ReaCompanyID = ReaCompanyID;
		if (StorageID) obj.StorageID = StorageID;
		if (PlaceID) obj.PlaceID = PlaceID;
		if (GoodsID) obj.GoodsID = GoodsID;
		if (CompGoodsLinkID) obj.CompGoodsLinkID = CompGoodsLinkID;
		if (BarCodeType) obj.BarCodeType = BarCodeType;
		if (TestEquipID) obj.TestEquipID = TestEquipID;
		if (reqCurrentQty) obj.ReqCurrentQty = reqCurrentQty;
		if (!reqGoodsQty) reqGoodsQty = 0;
		reqGoodsQty = parseFloat(reqGoodsQty);
		if (!godsQty) godsQty = godsQty;
		godsQty = parseFloat(godsQty);
		obj.ReqGoodsQty = reqGoodsQty;
		obj.GoodsQty = godsQty;
		if (!price) price = 0;
		price = parseFloat(price);
		obj.Price = price;
		sumTotal = price * godsQty;
		if (!sumTotal) sumTotal = 0;
		sumTotal = parseFloat(sumTotal);
		obj.SumTotal = sumTotal;
		if (TaxRate) obj.TaxRate = TaxRate ? TaxRate : 0;
		if (GoodsSort) obj.GoodsSort = parseFloat(GoodsSort);
		if (DataAddTime) {
			obj.DataUpdateTime = JShell.Date.toServerDate(DataAddTime);
			obj.DataAddTime = JShell.Date.toServerDate(DataAddTime);
		}
		if (InvalidDate) obj.InvalidDate = JShell.Date.toServerDate(InvalidDate);
		if (ProdDate) obj.ProdDate = JShell.Date.toServerDate(ProdDate);
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (userId) {
			obj.CreaterID = userId;
			obj.CreaterName = userName;
		}

		//判断当前出库货品的库存数是否完全出库(出库数=剩余库存总数)
		var sumCurrentQty = rec.get('ReaBmsOutDtl_SumCurrentQty');
		if (!sumCurrentQty) sumCurrentQty = 0;
		sumCurrentQty = parseFloat(sumCurrentQty);
		if (sumCurrentQty == godsQty) {
			obj.IsAllOut = 1;
		} else {
			obj.IsAllOut = 0;
		}

		return obj;
	}
 
});
