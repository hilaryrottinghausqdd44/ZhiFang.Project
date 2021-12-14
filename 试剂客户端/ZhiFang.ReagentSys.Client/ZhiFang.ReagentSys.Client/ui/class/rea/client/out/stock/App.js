/**
 * 库存货品选择
 * @author liangyl
 * @version 2018-03-12
 * 
 * @author zq
 * @version 2020-11-10
 * @description 需求调整给明细表（DtlGrid）的批号的货运单号数据单元格增加样式变化，
 * 批号（货运单号）上次没有就将该单元格标记为红底白字
 */
Ext.define('Shell.class.rea.client.out.stock.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '库存货品选择',
	header: false,
	border: false,
	layout: {
		type: 'border'
	},
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	
	/**获取货品数据服务路径*/
	selectReaGoodsUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true',
	/*近效期提示需根据右边列表数据判断,右边列表是否已加载*/
	isLoad: true,
	/**表单选中的库房*/
	StorageObj: {},
	/**是否开启近效期 1:开启;2:不开启;3:界面选择默认开启;4:界面选择默认不开启;*/
	isOpenNearEffectPeriod: "",
	/**是否强制近效期出库 1:强制;2:不强制;3:界面选择默认强制;4:界面选择默认不强制;*/
	isOutOfStockInNeartermPeriod: "",
	/**是否开启供应批次合并 1:后台默认合并;2:后台默认不合并;3:界面默认选择合并;4:界面选择默认不合并*/
	isOpenMergeInDocNo: "",
	/**执行的运行参数总数*/
	runParamsCount: 3,
	/**开启近效期扫码操作*/
	codeLotNo: '',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
		var counts = 0;
		//是否开启近效期
		JShell.REA.RunParams.getRunParamsValue("IsOpenNearEffectPeriod", false, function(data1) {
			me.isOpenNearEffectPeriod = JcallShell.REA.RunParams.Lists.IsOpenNearEffectPeriod.Value;
			counts += 1;
			me.onRunParams(counts);
		});
		//是否强制近效期出库
		JShell.REA.RunParams.getRunParamsValue("IsOutOfStockInNeartermPeriod", false, function(data1) {
			me.isOutOfStockInNeartermPeriod = JcallShell.REA.RunParams.Lists.IsOutOfStockInNeartermPeriod.Value;
			counts += 1;
			me.onRunParams(counts);
			
		});
		// 是否开启供应批次合并
		JShell.REA.RunParams.getRunParamsValue("IsOpenMergeInDocNo", false, function(data1) {
			me.isOpenMergeInDocNo = JcallShell.REA.RunParams.Lists.IsOpenMergeInDocNo.Value;
			counts += 1;
			me.onRunParams(counts);
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('itemdblclick', 'itemdblclick2', 'itemdbselectlclick', 'dbselectclick', 'scanCodeClick');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.QtyDtlGrid = Ext.create('Shell.class.rea.client.out.use.QtyDtlGrid', {
			header: false,
			itemId: 'QtyDtlGrid',
			region: 'center',
			StorageObj: me.StorageObj,
			collapsible: false,
			collapsed: false
		});
		me.NearTermPeriodGrid = Ext.create('Shell.class.rea.client.out.stock.Grid', {
			header: false,
			title: '近效期库存货品',
			itemId: 'NearTermPeriodGrid',
			region: 'east',
			width: 280,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		var appInfos = [me.QtyDtlGrid, me.NearTermPeriodGrid];
		return appInfos;
	},
	/**运行参数的近效期处理*/
	onRunParams: function(counts) {
		var me = this;
		if (counts == me.runParamsCount) {
			/**是否开启近效期 1:开启;2:不开启;3:界面选择默认开启;4:界面选择默认不开启;*/
			var value1 = "" + me.isOpenNearEffectPeriod;
			var check1= true; //开启近效期检测复选框的默认值
			var disable1 = false; //开启近效期检测复选框的是否禁用
			switch (value1) {
				case "1": //开启
					check1 = true; //勾选
					disable1 = true; //禁用
					break;
				case "2": //不开启
					check1 = false; //不勾选
					disable1 = true; //禁用
					break;
				case "3": //界面选择默认开启
					check1 = true; //勾选
					disable1 = false; //不禁用
					break;
				case "4": //界面选择默认不开启
					check1 = false; //不勾选
					disable1 = false; //不禁用
					break;
				default:
					break;
			}
			/**是否强制近效期出库 1:强制;2:不强制;3:界面选择默认强制;4:界面选择默认不强制;*/
			var value2= "" + me.isOutOfStockInNeartermPeriod;
			var check2 = false; //强制近效期出库复选框的默认值
			var disable2 = false; //强制近效期出库复选框的是否禁用
			//是否开启近效期为不开启
			if (value1 == "2") {
				check2 = false; //不勾选
				disable2= true; //禁用
			} else {
				switch (value2) {
					case "1": //强制
						check2 = true; //勾选
						disable2 = true; //禁用
						break;
					case "2": //不强制
						check2 = false; //不勾选
						disable2 = true; //禁用
						break;
					case "3": //界面选择默认强制
						check2= true; //勾选
						disable2= false; //不禁用
						break;
					case "4": //界面选择默认不强制
						check2 = false; //不勾选
						disable2 = false; //不禁用
						break;
					default:
						break;
				}
			}
			/**调整：是否合并供应批次：1：后台默认合并；2：后台默认不合并；3：界面选择默认合并；4：界面选择默认不合并*/
			var value3 = "" + me.isOpenMergeInDocNo;
			var check3 = true; // 后台默认合并
			var visible1 = false; // 后台默认时，复选框的状态是否隐藏
			switch (value3) {
				case "1": // 后台默认合并
					check3 = true; // 勾选
					visible1 = false; // 隐藏
					break;
				case "2": // 后台默认不合并
					check3 = false; // 不勾选
					visible1 = false; // 隐藏
					break;
				case "3": // 界面选择默认开启
					check3 = true; // 勾选
					visible1 = true; // 显示
					break;
				case "4": //界面选择默认不开启
					check3 = false; // 不勾选
					visible1 = true; // 显示
					break;
				default:
					break;
			}
			me.QtyDtlGrid.setNeareffectCheck(check1, disable1);
			me.QtyDtlGrid.setInDocNoMergeCheck(check3,visible1);
			me.NearTermPeriodGrid.setOutNeartermPeriod(check2, disable2);
		
		}
	},
	onListeners: function() {
		var me = this;
		me.QtyDtlGrid.on({
			select: function(rowModel, record) {
				me.onQtySelect(record);
			},
			nodata: function(p) {
				me.NearTermPeriodGrid.clearData();
			},
			searchnodata: function(p) {
				me.NearTermPeriodGrid.clearData();
			},
			itemdblclick: function(v, record, barcode) {
				var unitArr = [];
				var barCode = me.QtyDtlGrid.curBarCode;
				me.QtyDtlGrid.curBarCode = '';
				me.codeLotNo='';
				//开启近效期提醒
				me.ondbSelect(record, unitArr, barCode);
			},
			dbselectclick: function(v, record, barcode) {
				var unitArr = [];
				var barCode = me.QtyDtlGrid.curBarCode;
				me.QtyDtlGrid.curBarCode = '';
				me.codeLotNo='';
				//开启近效期提醒
				me.ondbSelect(record, unitArr, barcode);
			},
			scanCodeClick: function(barcode, qtyGrid) {
				me.fireEvent('scanCodeClick', barcode, qtyGrid);
			},
			testClick: function(grid, value) {
				//开启近效期检测，加载右边列表
				if (value) {
					me.NearTermPeriodGrid.onSearch();
				} else {
					me.NearTermPeriodGrid.enableControl(false);
					me.NearTermPeriodGrid.store.removeAll();
				}
			},
			dbitemclick: function(grid, record) {
				var unitArr = [];
				var records = me.QtyDtlGrid.getSelectionModel().getSelection();
				var barCode = me.QtyDtlGrid.curBarCode;
				me.QtyDtlGrid.curBarCode = '';
				me.codeLotNo='';
				me.fireEvent('itemdblclick', records[0], unitArr, barCode);
			},
			NObarcode: function() {
				me.NearTermPeriodGrid.clearData();
			},
			//扫码操作开启近效期判断
			scanCodeDate:function(v, record, barcode){
				me.codeLotNo=record.data.ReaBmsQtyDtl_LotNo;
			}
		});
		me.QtyDtlGrid.store.on({
			add: function(store, eOpts) {
				me.NearTermPeriodGrid.clearData();
			}
		});

		me.NearTermPeriodGrid.on({
			itemdblclick: function(com, record, item, index, e, eOpts) {
				me.fireEvent('itemdbselectlclick', record, me.QtyDtlGrid.curBarCode);
				
			},
			nodata: function(p) {
				me.NearTermPeriodGrid.enableControl(false);
			},
			changeResult: function(data) {}
		});

		me.NearTermPeriodGrid.store.on({
			refresh: function(store, eOpts) {
				var barCode = me.QtyDtlGrid.curBarCode;
				//判断，开启近效期并且只有一个盒条码时，默认选择
				var neareffectCheck = me.QtyDtlGrid.getNeareffectCheck();
				if (!store) return;
				if (store.getCount() == 1 && neareffectCheck.getValue() && barCode) {
					me.QtyDtlGrid.curBarCode = '';
					me.codeLotNo='';
					var recs = me.QtyDtlGrid.getSelectionModel().getSelection();
					var unitArr = [];
					me.fireEvent('itemdblclick', recs[0], unitArr, barCode);
				}
			},
			load: function(store, records, successful, eOpts) {
				me.setCurScanCodeList(records);
				if(me.codeLotNo!=''){
					for(var j=0;j<records.length;j++){
						if(me.codeLotNo==records[j].data.ReaBmsQtyDtl_LotNo){
							var unitArr = [];
							var barCode = me.QtyDtlGrid.curBarCode;
							me.QtyDtlGrid.curBarCode = '';
							me.codeLotNo='';
							//开启近效期提醒
							me.ondbSelect(records[j], unitArr, barCode);
						}
					}
				}
			}
		});
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
	/**删除已选标记*/
	onDelOne: function(tab) {
		var me = this;
		var records = me.NearTermPeriodGrid.store.data.items,
			len = records.length;
		for (var i = 0; i < len; i++) {
			var tab1 = records[i].get('ReaBmsQtyDtl_Tab');
			if (tab1 == tab) {
				records[i].set('ReaBmsQtyDtl_SelectTag', '0');
			}
		}
	},
	onQtySelect: function(record) {
		var me = this;
		//开启近效期检测
		var buttonsToolbar = me.QtyDtlGrid.getComponent('buttonsToolbar');
		var val = buttonsToolbar.getComponent('testCheck').getValue();
		var id = record.get('ReaBmsQtyDtl_GoodsID');
		var StorageID = record.get('ReaBmsQtyDtl_StorageID');
		//选择行效期
		var InvalidDate = record.get('ReaBmsQtyDtl_InvalidDate');
		me.NearTermPeriodGrid.StorageID = StorageID
		me.NearTermPeriodGrid.GoodsID = id;
		me.NearTermPeriodGrid.InvalidDate = InvalidDate;

		//供应商+货品+批号+库房+货架
		var ReaCompanyID = record.get('ReaBmsQtyDtl_ReaCompanyID');
		var GoodsID = record.get('ReaBmsQtyDtl_GoodsID');
		var LotNo = record.get('ReaBmsQtyDtl_LotNo');
		var StorageID = record.get('ReaBmsQtyDtl_StorageID');
		var PlaceID = record.get('ReaBmsQtyDtl_PlaceID');
		var str = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
		me.NearTermPeriodGrid.TabID = str;

		if (val) {
			me.NearTermPeriodGrid.onSearch();
		} else {
			me.NearTermPeriodGrid.store.removeAll(); //清空数据
		}
	},
	/**是否开启近效期检测
	 * 是否双击当前列表做检测,如果不做检测，双击行，数据添加到明细列表
	 * */
	ondbSelect: function(record, unitArr, barcode) {
		var me = this;
		//开启近效期检测
		var buttonsToolbar = me.QtyDtlGrid.getComponent('buttonsToolbar');
		var val = buttonsToolbar.getComponent('testCheck').getValue();
		var bo = true;
		
		if (me.NearTermPeriodGrid.getStore().getCount() == 0) {
			//如果开启近效期检测按钮开启，并且右边列表不存在数据，选择当前列表
			bo = false;
		}else{
			for(var lotnum=0;lotnum<me.NearTermPeriodGrid.getStore().getCount();lotnum++){
				if(record.data.ReaBmsQtyDtl_LotNo==me.NearTermPeriodGrid.getStore().getAt(lotnum).get("ReaBmsQtyDtl_LotNo")){
					me.fireEvent('itemdbselectlclick', me.NearTermPeriodGrid.getStore().getAt(lotnum), me.QtyDtlGrid.curBarCode);
				}
			}
		}
		if (!bo) me.fireEvent('itemdblclick', record, unitArr, barcode);
	},
	/**是否开启近效期检测
	 *1.勾选 开启近效期检测，右边才会有数据，只显示选择行和当前选择行较早的数据 （选右边数据）
	 *2.勾选强制近效期出库，只能选右边数据（只能选早的）
	 *3.都不勾选，只选左边的列表，不做效期检测
	 * */
	ondbSelect2: function(record, unitArr, itemsTab) {
		var me = this;
		//开启近效期检测
		var buttonsToolbar = me.QtyDtlGrid.getComponent('buttonsToolbar');
		var val = buttonsToolbar.getComponent('testCheck').getValue();
		//强制近效期出出库
		var buttonsToolbar2 = me.NearTermPeriodGrid.getComponent('buttonsToolbar');
		var forceval = buttonsToolbar2.getComponent('forceCheck').getValue();
		var bo = false;
		var SelectTag = record.get('ReaBmsQtyDtl_SelectTag');

		//只做开启近效期出库
		if (val && !forceval && SelectTag != '1') {
			me.checkMsg2(record, unitArr);
		}
		//强制效期出库选择最近效期才能出库
		if (val && forceval && SelectTag != '1') {
			var isExect = me.checkMsg(record, SelectTag);
			bo = true;
			if (isExect) {
				record.set('ReaBmsQtyDtl_SelectTag', '1');
				var barCode = me.QtyDtlGrid.curBarCode;
				me.QtyDtlGrid.curBarCode = '';
				me.codeLotNo='';
				me.fireEvent('itemdblclick', record, unitArr, barCode);
			}
		}
	},
	/**强制效期出库
	 * 只能选最早的效期出库
	 */
	checkMsg: function(record) {
		var me = this;
		var records = me.NearTermPeriodGrid.store.data.items,
			len = records.length;
		var InvalidDate = record.get('ReaBmsQtyDtl_InvalidDate');
		var isExec = true,
			msg = '';
		for (var i = 0; i < len; i++) {
			var InvalidDate2 = records[i].get('ReaBmsQtyDtl_InvalidDate');
			InvalidDate2 = JcallShell.Date.toString(InvalidDate2, true);
			InvalidDate = JcallShell.Date.toString(InvalidDate, true);
			var SelectTag = records[i].get('ReaBmsQtyDtl_SelectTag');
			if (InvalidDate > InvalidDate2 && SelectTag != '1') {
				isExec = false;
				msg = '有比选中试剂更前的批号,不能选择!';
				break;
			}
		}
		if (!isExec) {
			//JShell.Msg.error(msg);
			Ext.Msg.show({
				title: '错误信息',
				msg: msg,
				modal: true,
				buttons: Ext.Msg.YES,
				fn: function(){
					me.QtyDtlGrid.setScanCodeFocus();
				},
				icon: Ext.Msg.ERROR
			});
		}
		return isExec;
	},
	/**近效期出库
	 * 有提示，可选择
	 */
	checkMsg2: function(record, unitArr) {
		var me = this;
		var records = me.NearTermPeriodGrid.store.data.items,
			len = records.length;
		var InvalidDate = record.get('ReaBmsQtyDtl_InvalidDate');
		var isExec = true,
			msg = '';
		for (var i = 0; i < len; i++) {
			var InvalidDate2 = records[i].get('ReaBmsQtyDtl_InvalidDate');
			InvalidDate2 = JcallShell.Date.toString(InvalidDate2, true);
			InvalidDate = JcallShell.Date.toString(InvalidDate, true);
			var SelectTag = records[i].get('ReaBmsQtyDtl_SelectTag');

			if (InvalidDate > InvalidDate2 && SelectTag != '1') {
				isExec = false;
				msg = '有比选中试剂更前的批号,是否强制选择?';
				JShell.Msg.confirm({
					msg: msg
				}, function(but) {
					if (but == "ok") {
						var barCode = me.QtyDtlGrid.curBarCode;
						me.QtyDtlGrid.curBarCode = '';
						me.codeLotNo='';
						record.set('ReaBmsQtyDtl_SelectTag', '1');
						me.fireEvent('itemdblclick', record, unitArr, barCode);
					}
					me.QtyDtlGrid.setScanCodeFocus();
				});
				break;
			}
		}
		if (isExec) {
			var barCode = me.QtyDtlGrid.curBarCode;
			me.QtyDtlGrid.curBarCode = '';
			me.codeLotNo='';
			record.set('ReaBmsQtyDtl_SelectTag', '1');
			me.fireEvent('itemdblclick', record, unitArr, barCode);
		}
	},
	//条码明细表赋值
	setCurScanCodeList: function(records) {
		var me = this;
		if (me.QtyDtlGrid.curBarCode) {
			if (records.length > 0) {
				//获取选择行
				var recs = me.QtyDtlGrid.getSelectionModel().getSelection();
				if (recs.length > 0) {
					//供应商+货品+批号+库房+货架
					var ReaCompanyID = recs[0].get('ReaBmsQtyDtl_ReaCompanyID');
					var GoodsID = recs[0].get('ReaBmsQtyDtl_GoodsID');
					var LotNo = recs[0].get('ReaBmsQtyDtl_LotNo');
					var StorageID = recs[0].get('ReaBmsQtyDtl_StorageID');
					var PlaceID = recs[0].get('ReaBmsQtyDtl_PlaceID');
					var str = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
					var CurReaGoodsScanCodeList = recs[0].get('ReaBmsQtyDtl_CurReaGoodsScanCodeList');
				}
				for (var i = 0; i < records.length; i++) {
					//供应商+货品+批号+库房+货架
					var ReaCompanyID2 = records[i].get('ReaBmsQtyDtl_ReaCompanyID');
					var GoodsID2 = records[i].get('ReaBmsQtyDtl_GoodsID');
					var LotNo2 = records[i].get('ReaBmsQtyDtl_LotNo');
					var StorageID2 = records[i].get('ReaBmsQtyDtl_StorageID');
					var PlaceID2 = records[i].get('ReaBmsQtyDtl_PlaceID');
					var str2 = ReaCompanyID2 + GoodsID2 + LotNo2 + StorageID2 + PlaceID2;
					if (str2 == str) {
						records[i].set('ReaBmsQtyDtl_CurReaGoodsScanCodeList', CurReaGoodsScanCodeList);
					}
				}
			}
		}
	},
	clearData: function() {
		var me = this;
		me.NearTermPeriodGrid.store.removeAll();
		me.QtyDtlGrid.store.removeAll();
	},
	//加载库存
	loadData: function(StorageObj) {
		var me = this;
		me.StorageObj = StorageObj;
		me.QtyDtlGrid.StorageObj = StorageObj;
		me.QtyDtlGrid.onSearch();
	},
	/**扫码*/
	onScanCode: function(barcode, bo) {
		var me = this;
		me.QtyDtlGrid.onScanCode(barcode, bo);
	}
});
