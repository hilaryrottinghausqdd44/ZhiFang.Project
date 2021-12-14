/**
 * 货品选择
 * @author liangyl
 * @version 2018-11-05
 */
Ext.define('Shell.class.rea.client.transfer.stock.App', {
	extend: 'Ext.panel.Panel',
	
	title: '货品选择',
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
	/**表单选中的源库房*/
	SStorageObj: {},
	/**表单选中的目的库房*/
	DStorageObj: {},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('itemdblclick', 'itemdblclick2', 'itemdbselectlclick', 'dbselectclick', 'scanCodeClick');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.QtyDtlGrid = Ext.create('Shell.class.rea.client.transfer.QtyDtlGrid', {
			header: false,
			itemId: 'QtyDtlGrid',
			region: 'center',
			collapsible: false,
			collapsed: false,
			/**表单选中的源库房*/
			SStorageObj: me.SStorageObj,
			/**表单选中的目的库房*/
			DStorageObj: me.DStorageObj
		});
		me.NearTermPeriodGrid = Ext.create('Shell.class.rea.client.out.stock.Grid', {
			header: false,
			title: '近效期提示',
			itemId: 'NearTermPeriodGrid',
			region: 'east',
			width: 280,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		var appInfos = [me.NearTermPeriodGrid, me.QtyDtlGrid];
		return appInfos;
	},
	/**
	 * 条码扫码框重新置空及获取焦点
	 */
	setScanCodeFocus:function(){
		var me = this;
		me.QtyDtlGrid.setScanCodeFocus();
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
	onListeners: function(tab) {
		var me = this;
		me.QtyDtlGrid.on({
			scanCodeClick: function(barcode, grid) {
				me.fireEvent('scanCodeClick', barcode, grid);
			},
			select: function(RowModel, record) {
				me.onSelect(record);
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
				//开启近效期提醒
				me.ondbSelect(record, unitArr, barCode);
			},
			dbselectclick: function(v, record, barcode) {
				me.QtyDtlGrid.curBarCode = '';
				var UnitArr = [];
				//开启近效期提醒
				me.ondbSelect(record, UnitArr, barcode);
			},
			testClick: function(grid, value) {
				//开启近效期检测，加载右边列表
				if(value && grid.getStore().getCount() > 0) {
					var recs = grid.getSelectionModel().getSelection();
					if(recs.length == 0) return;
					me.NearTermPeriodGrid.onSearch();
				} else {
					me.NearTermPeriodGrid.enableControl(false);
					me.NearTermPeriodGrid.store.removeAll();
				}
			},
			dbitemclick: function(grid, record) {
				//只有一行数据时
				var records = me.QtyDtlGrid.getSelectionModel().getSelection();
				var barCode = me.QtyDtlGrid.curBarCode;
				me.QtyDtlGrid.curBarCode = '';
				var UnitArr = [];
				me.fireEvent('itemdblclick', records[0], UnitArr, barCode, me.DStorageObj);
			},
			NObarcode: function() {
				me.NearTermPeriodGrid.clearData();
			}
		});
		me.QtyDtlGrid.store.on({
			add: function(store, eOpts) {
				me.NearTermPeriodGrid.clearData();
			}
		});
		me.NearTermPeriodGrid.on({
			itemdblclick: function(com, record, item, index, e, eOpts) {
				var barCode = me.QtyDtlGrid.curBarCode;
				me.fireEvent('itemdbselectlclick', record, barCode, me.DStorageObj);
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
				var NeareffectCheck = me.QtyDtlGrid.getNeareffectCheck();
				if(!store) return;
				if(store.getCount() == 1 && NeareffectCheck.getValue() && barCode) {
					me.QtyDtlGrid.curBarCode = '';
					var recs = me.QtyDtlGrid.getSelectionModel().getSelection();
					var UnitArr = [];
					me.fireEvent('itemdblclick', recs[0], UnitArr, barCode, me.DStorageObj);
				}
			},
			load: function(store, records, successful, eOpts) {
				me.setCurScanCodeList(records);
			}
		});
	},
	/**删除已选标记*/
	onDelOne: function(tab) {
		var me = this;
		var records = me.NearTermPeriodGrid.store.data.items,
			len = records.length;
		for(var i = 0; i < len; i++) {
			var val = records[i].get('ReaBmsQtyDtl_Tab');
			if(val == tab) {
				records[i].set('ReaBmsQtyDtl_SelectTag', '0');
			}
		}
	},
	onSelect: function(record) {
		var me = this;
		//开启近效期检测
		var buttonsToolbar = me.QtyDtlGrid.getComponent('buttonsToolbar');
		var val = buttonsToolbar.getComponent('testCheck').getValue();
		var id = record.get('ReaBmsQtyDtl_GoodsID');
		var goodsID = record.get('ReaBmsQtyDtl_GoodsID');
		var storageID = record.get('ReaBmsQtyDtl_StorageID');
		//选择行效期
		var invalidDate = record.get('ReaBmsQtyDtl_InvalidDate');
		me.NearTermPeriodGrid.StorageID = storageID
		me.NearTermPeriodGrid.GoodsID = goodsID;
		me.NearTermPeriodGrid.InvalidDate = invalidDate;

		//供应商+货品+批号+库房+货架
		var reaCompanyID = record.get('ReaBmsQtyDtl_ReaCompanyID');
		var lotNo = record.get('ReaBmsQtyDtl_LotNo');
		var placeID = record.get('ReaBmsQtyDtl_PlaceID');
		var str = ""+reaCompanyID + goodsID + lotNo + storageID + placeID;
		me.NearTermPeriodGrid.TabID = str;
		if(val) {
			me.NearTermPeriodGrid.onSearch();
		} else {
			me.NearTermPeriodGrid.store.removeAll(); //清空数据
		}
	},
	/**是否开启近效期检测
	 * 是否双击当前列表做检测,如果不做检测，双击行，数据添加到明细列表
	 * */
	ondbSelect: function(record, UnitArr, barcode) {
		var me = this;
		//开启近效期检测
		var buttonsToolbar = me.QtyDtlGrid.getComponent('buttonsToolbar');
		var val = buttonsToolbar.getComponent('testCheck').getValue();
		var bo = true;
		if(me.NearTermPeriodGrid.getStore().getCount() == 0) {
			//如果开启近效期检测按钮开启，并且右边列表不存在数据，选择当前列表
			bo = false;
		}
		if(!bo) me.fireEvent('itemdblclick', record, UnitArr, barcode, me.DStorageObj);
	},
	/**是否开启近效期检测
	 *1.勾选 开启近效期检测，右边才会有数据，只显示选择行和当前选择行较早的数据 （选右边数据）
	 *2.勾选强制近效期出库，只能选右边数据（只能选早的）
	 *3.都不勾选，只选左边的列表，不做效期检测
	 * */
	ondbSelect2: function(record, UnitArr, itemsTab) {
		var me = this;
		//开启近效期检测
		var buttonsToolbar = me.QtyDtlGrid.getComponent('buttonsToolbar');
		var val = buttonsToolbar.getComponent('testCheck').getValue();
		//强制近效期出出库
		var buttonsToolbar2 = me.NearTermPeriodGrid.getComponent('buttonsToolbar');
		var forceval = buttonsToolbar2.getComponent('forceCheck').getValue();
		var bo = false;
		var SelectTag = record.get('ReaBmsQtyDtl_SelectTag');
		var records = me.NearTermPeriodGrid.store.data.items;
		var len = records.length;
		//只做开启近效期出库
		if(val && !forceval && SelectTag != '1') {
			me.checkMsg2(record, UnitArr);
		}
		//强制效期出库选择最近效期才能出库
		if(val && forceval && SelectTag != '1') {
			var isExect = me.checkMsg(record, SelectTag);
			bo = true;
			if(isExect) {
				record.set('ReaBmsQtyDtl_SelectTag', '1');
				var barCode = me.QtyDtlGrid.curBarCode;
				me.QtyDtlGrid.curBarCode = '';
				me.fireEvent('itemdblclick', record, UnitArr, barCode, me.DStorageObj);
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
		for(var i = 0; i < len; i++) {
			var InvalidDate2 = records[i].get('ReaBmsQtyDtl_InvalidDate');
			InvalidDate2 = JcallShell.Date.toString(InvalidDate2, true);
			InvalidDate = JcallShell.Date.toString(InvalidDate, true);
			var SelectTag = records[i].get('ReaBmsQtyDtl_SelectTag');
			if(InvalidDate > InvalidDate2 && SelectTag != '1') {
				isExec = false;
				msg = '有比选中试剂更前的批号,不能选择!';
				break;
			}
		}
		if(!isExec) {
			JShell.Msg.error(msg);
		}
		return isExec;
	},
	/**近效期出库
	 * 有提示，可选择
	 */
	checkMsg2: function(record, UnitArr) {
		var me = this;
		var records = me.NearTermPeriodGrid.store.data.items,
			len = records.length;
		var InvalidDate = record.get('ReaBmsQtyDtl_InvalidDate');
		var isExec = true,
			msg = '';
		for(var i = 0; i < len; i++) {
			var InvalidDate2 = records[i].get('ReaBmsQtyDtl_InvalidDate');
			InvalidDate2 = JcallShell.Date.toString(InvalidDate2, true);
			InvalidDate = JcallShell.Date.toString(InvalidDate, true);
			var SelectTag = records[i].get('ReaBmsQtyDtl_SelectTag');

			if(InvalidDate > InvalidDate2 && SelectTag != '1') {
				isExec = false;
				msg = '有比选中试剂更前的批号,是否强制选择?';
				JShell.Msg.confirm({
					msg: msg
				}, function(but) {
					if(but == "ok") {
						var barCode = me.QtyDtlGrid.curBarCode;
						me.QtyDtlGrid.curBarCode = '';
						record.set('ReaBmsQtyDtl_SelectTag', '1');
						me.fireEvent('itemdblclick', record, UnitArr, barCode, me.DStorageObj);

					}
				});
				break;
			}
		}
		if(isExec) {
			var barCode = me.QtyDtlGrid.curBarCode;
			me.QtyDtlGrid.curBarCode = '';
			record.set('ReaBmsQtyDtl_SelectTag', '1');
			me.fireEvent('itemdblclick', record, UnitArr, barCode, me.DStorageObj);
		}
	},
	//条码明细表赋值
	setCurScanCodeList: function(records) {
		var me = this;
		if(me.QtyDtlGrid.curBarCode) {
			if(records.length > 0) {
				//获取选择行
				var recs = me.QtyDtlGrid.getSelectionModel().getSelection();
				var CurReaGoodsScanCodeList = '';
				if(recs.length > 0) {
					//供应商+货品+批号+库房+货架
					var ReaCompanyID = recs[0].get('ReaBmsQtyDtl_ReaCompanyID');
					var GoodsID = recs[0].get('ReaBmsQtyDtl_GoodsID');
					var LotNo = recs[0].get('ReaBmsQtyDtl_LotNo');
					var StorageID = recs[0].get('ReaBmsQtyDtl_StorageID');
					var PlaceID = recs[0].get('ReaBmsQtyDtl_PlaceID');
					var str = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
					CurReaGoodsScanCodeList = recs[0].get('ReaBmsQtyDtl_CurReaGoodsScanCodeList');
				}
				for(var i = 0; i < records.length; i++) {
					//供应商+货品+批号+库房+货架
					var ReaCompanyID2 = records[i].get('ReaBmsQtyDtl_ReaCompanyID');
					var GoodsID2 = records[i].get('ReaBmsQtyDtl_GoodsID');
					var LotNo2 = records[i].get('ReaBmsQtyDtl_LotNo');
					var StorageID2 = records[i].get('ReaBmsQtyDtl_StorageID');
					var PlaceID2 = records[i].get('ReaBmsQtyDtl_PlaceID');
					var str2 = ReaCompanyID2 + GoodsID2 + LotNo2 + StorageID2 + PlaceID2;
					if(str2 == str) {
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
		me.setScanCodeFocus();
	},
	//加载库存
	loadData: function(StorageObj) {
		var me = this;
		me.SStorageObj = StorageObj;
		me.QtyDtlGrid.SStorageObj = StorageObj;
		me.QtyDtlGrid.onSearch();
	},
	onSetDStorageObj: function(StorageObj) {
		var me = this;
		me.DStorageObj = StorageObj;
	},
	/**扫码*/
	onScanCode: function(barcode, bo) {
		var me = this;
		me.QtyDtlGrid.onScanCode(barcode, bo);
	}
});