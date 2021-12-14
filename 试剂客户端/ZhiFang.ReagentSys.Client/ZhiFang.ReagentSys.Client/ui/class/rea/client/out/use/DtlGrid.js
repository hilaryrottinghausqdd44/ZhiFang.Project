/**
 * 出库明细
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.use.DtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.DtlGrid',

	title: '出库明细列表',
	/**试剂单位信息*/
	ReaGoodsList: [],
	/**出库类型默认值*/
	defaluteOutType: '1',
	/**是否开启浮动框*/
	isOpenDtlPanel: true,
	/**是否显示弹框(双击入库数单元格和删除数据时隐藏)*/
	isShowDtlPanel: false,
	/**是否不返回聚焦到扫码框*/
	IsShowScan: true,
	/**浮动框设置*/
	OTYPE: 'DtlGrid',
	
	afterRender: function() {
		var me = this;
		me.store.on({
			remove: function(store,records,index) {
				me.isShowDtlPanel = false;
				me.IsShowScan = true;
				me.store.fireEvent('onshowpanel',me,me.isShowDtlPanel,"",me.IsShowScan);
			},
			// 扫码时，明细表中没有数据条数增加，但是出库数改变时考虑用update事件
			update:function(store,record,modifiedFieldNames) {
				me.isShowDtlPanel = true;
				var info = me.getShowDtlInfo(record);
				me.store.fireEvent('onshowpanel',me,me.isShowDtlPanel,info,me.IsShowScan);
			}
		});
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('delclick','onshowpanel');
		
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			xtype: 'rownumberer',
			text: me.Shell_ux_grid_Panel.NumberText,
			width: me.rowNumbererWidth,
			align: 'center'
		}, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					// var rec = grid.getStore().getAt(rowIndex);
					// me.fireEvent('delclick', rec.get('ReaBmsOutDtl_Tab'));
					// me.store.remove(rec);
					
					var rec = grid.getStore().getAt(rowIndex);
					me.fireEvent('delclick', rec.get('ReaBmsOutDtl_Tab'));
					var id = rec.get(me.PKField);
					if(id&&id!="-1") {
						me.delOneById(id, rec);
					} else {
						me.store.remove(rec);
					}
					me.fireEvent('changeSumTotal');
				}
			}]
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsNo',
			text: '平台编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsCName',
			text: '货品名称',
			sortable: false,
			width: 120,
			width: 100,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsOutDtl_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_Price',
			text: '单价',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_UnitMemo',
			text: '规格',
			sortable: false,
			width: 100,
			defaultRenderer: true
		});
		columns.push({
			dataIndex: 'ReaBmsOutDtl_DefaulteGoodsQty',
			text: '货品库存',
			sortable: false,
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_SumCurrentQty',
			text: '剩余总库存',
			sortable: false,
			xtype: 'numbercolumn',
			format: '0.00',
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_ReqGoodsQty',
			text: '申请数',
			sortable: false,
			width: 70,
			hidden: me.PK ? false : true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsQty',
			text: '<b style="color:blue;">出库数</b>',
			sortable: false,
			width: 70,
			xtype: 'numbercolumn',
			format: '0.00',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				listeners: {
					focus: function(field, e, eOpts) {
						me.comSetReadOnlyOfBarCodeType(field, e);
					},
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						if(newValue < 0) newValue = 0;
						me.setSumTotal(newValue, records[0]);
						me.fireEvent('changeSumTotal');
						//  需求调整：双击输入入库数时，隐藏弹框
						me.IsShowDtlInfo = false;
						me.IsShowScan = true;
						me.store.fireEvent('onshowpanel',me,me.IsShowDtlInfo,"",me.IsShowScan);
						com.focus(false, 100);
					}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsUnit',
			text: '单位',
			sortable: false,
			defaultRenderer: true
		});
		columns.push({
			dataIndex: 'ReaBmsOutDtl_ReaTestEquipVOList',
			text: '试剂所属仪器信息',
			sortable: false,
			hidden: true,
			width: 100,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TestEquipID',
			text: '仪器Id',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		});
		columns.push(me.createEquipNameColumn());
		columns.push({
			dataIndex: 'ReaBmsOutDtl_BarCodeType',
			text: '条码类型',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SumTotal',
			text: '金额',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotNo',
			text: '批号',
			sortable: false,
			width: 120,
			/**如果上次出了这个批号，则置为红底白字*/
			renderer: function(value,meta, record) {
				if( record.get('isChanageLotNo') == 'Change') {
					meta.style = 'font-weight:bold;color:black;background:pink;';
				}
				return value; // 这里的v就是要重新渲染的HTML
			}
		}, { // 双击调用服务后，用来存储服务返回的上次批号
			dataIndex: 'ReaBmsOutDtl_LastLotNo',
			text: '上次的批号',
			width: 120,
			hideable: false,
			hidden: true
		}, { // 条件列，用于判断
			dataIndex: 'isChanageLotNo',
			text: '是否改变批号',
			width: 120,
			hideable: false,
			hidden: true
		},
		{ // 需求调整：在明细表中增加货运单号
			dataIndex: 'ReaBmsOutDtl_TransportNo',
			text: '货运单号',
			sortable: false,
			width: 120,
			renderer: function(value,meta, record) {
				if( record.get('isChanageTransportNo') == 'Change') {
					meta.style = 'font-weight:bold;color:black;background:pink;';
				}
				return value; 
			}
		}, { // 双击调用服务后，用来存储服务返回的上次货运单号
			dataIndex: 'ReaBmsOutDtl_LastTransportNo',
			text: '上次的货运单号',
			width: 120,
			hideable: false,
			hidden: true
		}, { // 条件列，用于判断
			 dataIndex: 'isChanageTransportNo',
			 text: '是否改变货运单号',
			 width: 120,
			 hideable: false,
			 hidden: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageName',
			text: '库房名称',
			sortable: false,
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceName',
			text: '货架名称',
			sortable: false,
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_InvalidDate',
			text: '效期',
			sortable: false,
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaCompanyID',
			text: '供应商Id',
			hidden: true,
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompanyName',
			text: '供应商',
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_RegisterNo',
			text: '注册证号',
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageID',
			text: '库房ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceID',
			text: '货架ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsSerial',
			text: '货品条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotSerial',
			text: '批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SysLotSerial',
			text: '系统内部批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaServerCompCode',
			text: '供应商机平台构码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdDate',
			text: '生产日期',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsID',
			text: '货品iD',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Memo',
			text: 'Memo',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TaxRate',
			text: 'TaxRate',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_QtyDtlID',
			text: 'QtyDtlID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Tab',
			text: '合并标签', //供应商Id+货品Id+批号+库房Id+货架Id
			editor: {
				readOnly: true
			},
			hideable: false,
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_DefaulteGoodsID',
			text: '原货品ID',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaGoodsNo',
			text: '货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotQRCode',
			text: '二维批条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaCompCode',
			text: '供货方编码',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsSort',
			text: '货品序号',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CurReaGoodsScanCodeList',
			text: '当前扫码记录',
			sortable: false,
			editor: {
				readOnly: true
			},
			hidden: true,
			width: 100,
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta, record);
				return v;
			}
		},{
			dataIndex: 'ReaBmsOutDtl_SName',
			text: '简称',
			width: 90,
			defaultRenderer: true,
			sortable: false
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdOrgName',
			text: '品牌',
			width: 90,
			defaultRenderer: true,
			sortable: false
		}, {
			dataIndex: 'ReaBmsOutDtl_BarCodeQtyDtlID',
			text: '本次扫码库存ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ISmallUnit',
			text: '当前条码是否是小包装',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		});

		return columns;
	},
	/**删除一条数据*/
	delOneById: function(id, record) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(record) {
					me.store.remove(record);
				}
			} else {
				if(record) {
					record.set(me.DelField, false);
					record.set('ErrorInfo', data.msg);
					JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					record.commit();
				}
			}
		}, false);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		items = me.createTestEquipItems(items);
		// 鄞州需求调整：开关控制浮动框
		items.push('-',{
			xtype: 'checkboxfield',
			boxLabel: '是否显示浮动窗',
			checked: true,
			inputValue: 1,
			name: 'cboIShowDtlInfo',
			itemId: 'cboIShowDtlInfo',
			listeners: {
				change: function(field, newValue, oldValue, e) {
					if(oldValue == true) {
						me.isOpenDtlPanel = false;
					} else {
						me.isOpenDtlPanel = true;
					}
					if(Ext.WindowManager.get(me.OTYPE)) {
						Ext.WindowManager.get(me.OTYPE).close();
					}
				}
			}
		});
		return items;
	},
	/**
	 * 出库保存校验
	 * 直接出库时:出库数量不能为0;
	 * 确认出库时:如果是对出库申请进行确认出库,出库数量可以为0;
	 */
	onSaveCheck: function(isAllowZero) {
		var me = this;
		var isExec = me.callParent(arguments);
		return isExec;
	},
	/**获取明细列表数据*/
	getOutDtlInfo: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var dtlArr = [],
			dtAddList = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var obj = me.getEntity(rec);
			var barCodeQtyDtlID = rec.get('ReaBmsOutDtl_BarCodeQtyDtlID');
			if(barCodeQtyDtlID) {
				obj.QtyDtlID = barCodeQtyDtlID;
			}
			//扫码明细
			var reaBmsInDtlLink = [];
			var scanCodeList = rec.get('ReaBmsOutDtl_CurReaGoodsScanCodeList');
			if(scanCodeList.length > 0) {
				reaBmsInDtlLink = Ext.JSON.decode(scanCodeList);
			}
			obj.ReaBmsOutDtlLinkList = reaBmsInDtlLink;
			dtlArr.push(obj);
		}
		return dtlArr;
	},
	/**创建新增行数据*/
	createRowObj: function(recQty, barcode) {
		var me = this;
		var outDtlObj = me.createOutDtlObj(recQty.data);
		outDtlObj = me.createBarcodeJObject(outDtlObj, recQty, barcode);
		//出库扫码模式判断
		outDtlObj = me.createScanCodeModel(outDtlObj, barcode);
		//仪器处理
		outDtlObj = me.setOutDtlObjTestEquipValue(recQty, outDtlObj);
		return outDtlObj;
	},
	/**
	 * 鄞州需求调整：出库时试剂名，该试剂出库数，出库货品种类的数量，所有试剂出库总数
	 * 这些重要数据以弹框的形式明示给用户，改善用户使用出库时看不清，提高用户体验
	 * */
	/**用来得到要填写在弹框中的数据信息*/
	getShowDtlInfo: function(rec) {
		var me = this;
		var info = {
			// 货品名称
			"CName": rec ? rec.get("ReaBmsOutDtl_GoodsCName") : "",
			// 当前选中货品出库数
			"GoodsQty": rec ? rec.get("ReaBmsOutDtl_GoodsQty") : "",
			// 货品数量（货品种类数，不同批次的同一货品算为一种试剂）
			"TypesOfGoods": me.getTypesOfGoods(), 
			// 明细表中涉及到的所有试剂的出库总数
			"OutSumTotal": me.getOutSumTotal(), 
		};
		return info;
	},
	/**获取明细列表中货品种类的数量
	 * */
	getTypesOfGoods: function() {
		 var me = this,
		 records = me.store.data.items,
		 len = records.length;
		 // 存放store中加入货品的id
		 var goodsIdList = [];
		 for(var i = 0; i < len; i++) {
			 goodsIdList.push(records[i].get('ReaBmsOutDtl_GoodsID'));
		 }
		 var noRepeatIdList = Ext.Array.unique(goodsIdList);
		 return noRepeatIdList.length;
	 },
	 /**获取明细列表中货品出库总数量*/
	getOutSumTotal: function() {
		 var me = this,
		 records = me.store.data.items,
		 len = records.length;
		 var sumTotal = 0;
		 for(var i = 0; i < len; i++) {
			 sumTotal += parseInt(records[i].get('ReaBmsOutDtl_GoodsQty'));
		 }
		 return sumTotal;
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
	 			me.msgShow(msg,function(buttonId, text, opt){
	 				if(buttonId=="yes"){
	 					me.ownerCt.StockPanel.QtyDtlGrid.setScanCodeFocus();
	 				}
	 			});	
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
	 msgShow: function(msg, callback) {
	 	var me = this;
	 	var view=Ext.Msg.show({
	 		title: JcallShell.Msg.ERROR_TITLE,
	 		msg: msg,
	 		modal: true,
	 		icon: Ext.Msg.ERROR,
	 		buttons: Ext.Msg.YES,
	 		fn: function(buttonId, text, opt) {
	 			if (callback) callback(buttonId, text, opt);
	 		}
	 	});
	 }
	
});