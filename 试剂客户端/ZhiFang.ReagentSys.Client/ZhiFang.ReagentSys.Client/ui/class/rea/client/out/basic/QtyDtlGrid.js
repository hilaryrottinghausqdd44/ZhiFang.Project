/**
 * 库存货品选择列表
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.basic.QtyDtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn'
	],
	title: '库存货品选择列表',
	width: 800,
	height: 500,

	/**查询库存表数据*/
	// 需求调整，后台参数变化，加上isMergeInDocNo=true方便测试，后台写好后，前台动态传值
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtl?isPlanish=true',
	/**仪器试剂服务查询服务*/
	selectRELinkUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaEquipReagentLinkByHQL?isPlanish=true',
	/**仪器查询服务*/
	selectLabUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLabByHQL?isPlanish=true',
	/**根据货品条码获取货品相关信息*/
	ScanCodeUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlByBarCode?isPlanish=true',
	defaultOrderBy: [{
		property: 'ReaBmsQtyDtl_InvalidDate',
		direction: 'ASC'
	}],
	/**是否启用序号列*/
	hasRownumberer: true,
	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultDisableControl: false,
	/**当前扫码的条码号*/
	curBarCode: '',
	/**货品条码操作类型*/
	barcodeOperType: '7',
	/**后台排序*/
	remoteSort: false,
	/**表单选中的库房*/
	StorageObj: {},
	/**调整供应批次合并,(相同供货商+相同库房+相同货架+相同货品ID+相同货品批号+效期+入库批次)是否按入库批次合并显示库存货品*/
	isMergeInDocNo: false,
	/**移库或出库扫码是否允许从所有库房获取库存货品*/
	barCodeIsAllowOfALLStorage:false,
	//开启近效期复选框选择状态
	neareffectChecked:true,
	//开启近效期复选框是否禁用
	neareffectDisabled:false,
	// 开启供应批次合并复选框选择状态
	inDocNoMergeChecked: true,
	// 开启供应批次合并复选框是否隐藏
	inDocNoMergeVisible: false,
	/**是否开启近效期 1:开启;2:不开启;3:界面选择默认开启;4:界面选择默认不开启;*/
	isOpenNearEffectPeriod: "",
	
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//移库或出库扫码是否允许从所有库房获取库存货品
		JShell.REA.RunParams.getRunParamsValue("TranOrOutBarCodeIsAllowOfALLStorage", true, function(data) {
			if (data.value && data.value.ParaValue && data.value.ParaValue == 1) {
				me.barCodeIsAllowOfALLStorage=true;
			}
		});
		//是否开启近效期
		JShell.REA.RunParams.getRunParamsValue("IsOpenNearEffectPeriod", false, function(data1) {
			me.isOpenNearEffectPeriod = JcallShell.REA.RunParams.Lists.IsOpenNearEffectPeriod.Value;
		});
	},
	initComponent: function() {
		var me = this;
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsQtyDtl_CompanyName',
			text: '供应商',
			sortable: true,
			width: 110,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InDocNo',
			text: '入库批次',
			hidden: true,
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: true,
			width: 100,
			minWidth: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaGoodsNo',
			text: '货品编码',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsNo',
			text: '货品平台编码',
			hidden: true,
			sortable: true,
			width: 100,
			minWidth: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_BarCodeType',
			text: '条码类型',
			hideable: false,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsName',
			text: '货品名称',
			sortable: true,
			width: 145,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsQtyDtl_BarCodeType");
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
			dataIndex: 'ReaBmsQtyDtl_LotNo',
			text: '批号',
			sortable: true,
			width: 120,
			defaultRenderer: true
		}, { // 双击调用服务后，用来存储服务返回的上次批号
			dataIndex: 'ReaBmsQtyDtl_LastLotNo',
			text: '上次的批号',
			width: 120,
			hideable: false,
			hidden: true
		}, {
			// isChanageLotNo
			dataIndex: 'isChanageLotNo',
			text: '是否改变批号',
			width: 120,
			hideable: false,
			hidden: true
		}, { // 需求调整：在库存表中增加货运单号
			dataIndex: 'ReaBmsQtyDtl_TransportNo',
			text: '货运单号',
			sortable: true,
			width: 120,
			defaultRenderer: true
		}, { // 双击调用服务后，用来存储服务返回的上次货运单号
			dataIndex: 'ReaBmsQtyDtl_LastTransportNo',
			text: '上次的货运单号',
			width: 120,
			hideable: false,
			hidden: true
		}, { // 是否一致isChanageTransportNo
			 dataIndex: 'isChanageTransportNo',
			 text: '是否改变货运单号',
			 width: 120,
			 hideable: false,
			 hidden: true
		},
		{
			dataIndex: 'ReaBmsQtyDtl_GoodsUnit',
			text: '单位',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_UnitMemo',
			text: '规格',
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsQty',
			text: '库存量',
			sortable: true,
			width: 80,
			xtype: 'numbercolumn',
			//format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_SumCurrentQty',
			text: '剩余总库存',
			sortable: false,
			hidden: true,
			xtype: 'numbercolumn',
			//format: '0.00',
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				meta.tdAttr = 'data-qtip="<b>货品扫码时,如果存在多条库存记录系统将(库房货架及供货商货品批号和包装单位)都相同的库存货品,自动合并为一行显示</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_Price',
			text: '单价',
			sortable: true,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InvalidDate',
			text: '效期',
			sortable: true,
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageName',
			text: '库房',
			sortable: true,
			//hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceName',
			text: '货架',
			sortable: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdDate',
			text: '生产日期',
			//hidden: true,
			isDate: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_RegisterNo',
			text: '注册证号',
			sortable: true,
			hidden: true,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Id',
			text: '库存Id',
			sortable: true,
			hidden: true,
			isKey: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaCompanyID',
			text: '本地供应商ID',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageID',
			text: '库房ID',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceID',
			text: '货架ID',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsID',
			text: '货品ID',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_TaxRate',
			text: 'TaxRate',
			hideable: false,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsSerial',
			text: 'GoodsSerial',
			hideable: false,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotSerial',
			text: '批号条码',
			hideable: false,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_SysLotSerial',
			text: '系统条码',
			hideable: false,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CompGoodsLinkID',
			text: '供货商货品关系ID',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaServerCompCode',
			text: '供货商平台编码',
			hideable: false,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotQRCode',
			text: '二维批条码',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaCompCode',
			text: '供货方编码',
			hideable: false,
			sortable: true,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsSort',
			text: '货品序号',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CurReaGoodsScanCodeList',
			sortable: true,
			hidden: true,
			text: '当次扫码记录集合',
			width: 100,
			editor: {
				readOnly: true,
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_BarCodeQtyDtlID',
			sortable: true,
			hidden: true,
			text: '当次扫码库存ID',
			width: 100,
			editor: {
				readOnly: true,
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_JObjectBarCode',
			sortable: true,
			hidden: true,
			text: '货品扫码条码相关信息',
			width: 100,
			editor: {
				readOnly: true,
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Tab',
			text: '供应商Id+货品Id+批号+库房Id+货架Id',
			hideable: false,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', {
			emptyText: '一级分类',
			labelWidth: 0,
			width: 85,
			fieldLabel: '',
			itemId: 'GoodsClass',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClass');
				}
			}
		}, {
			emptyText: '二级分类',
			labelWidth: 0,
			width: 85,
			fieldLabel: '',
			itemId: 'GoodsClassType',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClassType');
				}
			}
		}, {
			xtype: 'uxSimpleComboBox',
			itemId: 'cboSearch',
			margin: '0 0 0 5',
			emptyText: '检索条件选择',
			fieldLabel: '检索',
			labelWidth: 35,
			width: 130,
			value: "1",
			data: [
				["1", "按机构货品"],
				["2", "按货品批号"]
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue) {
						var buttonsToolbar = me.getComponent('buttonsToolbar');
						var txtSearch = buttonsToolbar.getComponent('txtSearch');
						if(newValue == "2") {
							txtSearch.emptyText = '货品批号';
						} else {
							txtSearch.emptyText = '货品名称/货品编码/拼音字头/简称';
						}
						txtSearch.applyEmptyText();
						if(txtSearch.getValue()) me.onSearch();
					}
				}
			}
		}, {
			name: 'txtSearch',
			itemId: 'txtSearch',
			emptyText: '货品名称/货品编码/拼音字头/简称',
			width: 160,
			xtype: 'textfield',
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						var buttonsToolbar = me.getComponent("buttonsToolbar");
						var txtScanCode = buttonsToolbar.getComponent("txtScanCode");
						if(!me.StorageObj.StorageID) {
							var info = "请选择库房!";
							JShell.Msg.alert(info, null, 2000);
							me.store.removeAll();
							me.fireEvent('nodata', me);
							return;
						}
						txtScanCode.setValue('');
						me.onSearch();
					}
				}
			}
		}, {
			name: 'txtScanCode',
			itemId: 'txtScanCode',
			emptyText: '扫货品条码后选库存货品出库',
			margin: '0 0 0 5',
			width: 160,
			xtype: 'textfield',
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					me.onBarCodeClick(field, e);
				}
			}
		}];
		return items;
	},
	/**货品扫码事件处理*/
	onBarCodeClick: function(field, e) {
		var me = this;
		if(e.getKey() == Ext.EventObject.ENTER) {
			//防止扫码时,自动出现触发多个回车事件
			JShell.Action.delay(function() {
				if(!me.StorageObj.StorageID) {
					JShell.Msg.alert('库房不能为空!', null, 2000);
					return;
				}
				me.clearToolVal();
				//"\s"匹配任何不可见字符，包括空格、制表符、换页符等等
				var barCode = field.getValue().trim().replace(/\s+/g, '');
				if(!barCode) {
					/* JShell.Msg.alert("请输入条码号!", null, 2000);
					me.store.removeAll();
					me.fireEvent('nodata', me); */
					var msg="请输入条码号!";
					me.msgShow(msg,function(buttonId, text, opt){
						if(buttonId=="yes"){
							me.store.removeAll();
							me.fireEvent('nodata', me);
							me.setScanCodeFocus();
						}
					});	
					return;
				}
				me.fireEvent('scanCodeClick', barCode, me);
			}, null, 30);
		}
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
				txtScanCode.focus(true, 400);
			}
		}
	},
	/**
	 * 弹出提示后,执行回调处理
	 * @param {Object} msg
	 * @param {Object} callback
	 */
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
	},
	/**货品扫码调用服务返回库存货品
	 * 货品只有一条数据 不需要弹出列表选择
	 * 货品存在多条数据，需要用户选择
	 * */
	onScanCode: function(barcode, bo) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			txtScanCode = buttonsToolbar.getComponent('txtScanCode');

		//批条码已存在明细中不需要调用扫码服务
		if(bo) {
			txtScanCode.setValue('');
			return;
		}
		if(!me.StorageObj.StorageID) {
			txtScanCode.setValue('');
			JShell.Msg.alert('请先选择库房后再扫码!', null, 2000);
			return;
		}
		var url = JShell.System.Path.ROOT + me.ScanCodeUrl;
		var barcode2=JShell.String.encode(barcode);
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		url += '&barcode=' + barcode2 + '&barcodeOperType=' + me.barcodeOperType + '&isMergeInDocNo=' + me.isMergeInDocNo;		
		url += '&storageId=' + me.StorageObj.StorageID;
		//移库或出库扫码是否允许从所有库房获取库存货品
		url += '&isAllowOfALLStorage=' + me.barCodeIsAllowOfALLStorage;
		
		me.store.removeAll();
		me.curBarCode = '';
		JShell.Server.get(url, function(data) {
			if(data.success) {
				txtScanCode.setValue('');
				if(data && data.value) {
					var list = data.value.list;
					var info = "当前选择的库房没有此库存货品,请重新选择库房后再扫码!";
					if(list.length == 0) {
						JShell.Msg.alert(info, null, 2000);
					}
					me.insertStore(barcode, list);
				}
			} else {
				/* JShell.Msg.error('当前选择的库房没有此库存货品,请重新选择库房后再扫码!</br>' + data.msg);	
				txtScanCode.setValue('');			
				me.fireEvent('NObarcode', me);
				me.store.removeAll(); */
				
				var msg='当前选择的库房没有此库存货品,请重新选择库房后再扫码!</br>' + data.msg;
				me.msgShow(msg,function(buttonId, text, opt){
					if(buttonId=="yes"){
						txtScanCode.setValue('');
						me.fireEvent('NObarcode', me);
						me.store.removeAll();
						me.setScanCodeFocus();
					}
				});
			}
		}, false);
	},
	/**货品扫码调用服务返回库存货品
	 * 将返回的库存货品添加到库存列表的store
	 * */
	insertStore: function(barcode, list) {
		var me = this;
		for(var i = 0; i < list.length; i++) {
			var qtyDtlID = list[i].ReaBmsQtyDtl_Id;
			//供应商+货品+批号+库房+货架
			var reaCompanyID = list[i].ReaBmsQtyDtl_ReaCompanyID;
			var goodsID = list[i].ReaBmsQtyDtl_GoodsID;
			var lotNo = list[i].ReaBmsQtyDtl_LotNo;
			var storageID = list[i].ReaBmsQtyDtl_StorageID;
			var placeID = list[i].ReaBmsQtyDtl_PlaceID;
			var str = reaCompanyID + goodsID + lotNo + storageID + placeID;
			me.curBarCode = barcode;
			list[i]["ReaBmsQtyDtl_Tab"] = str; // 供应商Id+货品Id+批号+库房Id+货架Id
			list[i]["ReaBmsQtyDtl_BarCodeQtyDtlID"] = qtyDtlID; // 当次扫码库存id
			me.store.insert(me.store.getCount(), list[i]);
		}
		me.hiddenColumns(false);
		var records = me.store.data.items;
		if(records.length == 1) me.oneRecSelect(barcode, qtyDtlID);
	},
	/**
	 * 显示或隐藏"剩余总库存"列
	 * 货品扫码时,显示"剩余总库存"列;其他条件检索库存货品时,隐藏"剩余总库存"列
	 * */
	hiddenColumns: function(isHidden) {
		var me = this;
		var counts = 0;
		Ext.Array.each(me.columns, function(column, index, countriesItSelf) {
			if(counts == 1) return false;
			if(column.dataIndex == "ReaBmsQtyDtl_SumCurrentQty") {
				if(isHidden == true) me.columns[index].hide();
				else me.columns[index].show();
				counts = counts + 1;
			}
		});
	},
	/**
	 * 货品扫码调用服务返回库存货品
	 * 只有一条库存记录数据时选择行出来
	 * */
	oneRecSelect: function(barcode, qtyDtlID) {
		var me = this;
		var records = me.store.data.items,
			len = records.length;
		//记录当次扫码操作
		var curArr = me.getCurReaGoodsScanCodeList(barcode);
		records[0].set('ReaBmsQtyDtl_CurReaGoodsScanCodeList', curArr);
		//ui默认选择一行(第一行)
		if(me.getStore().getCount() > 0) {
			me.getSelectionModel().select(0);
		}
		me.fireEvent('dbselectclick', me, records[0], barcode);
	},
	/**返回当次扫码记录集合*/
	getCurReaGoodsScanCodeList: function(barcode) {
		var me = this;
		//记录当次扫码操作
		var obj = {
			SysPackSerial: barcode,
			OtherPackSerial: barcode,
			UsePackSerial: barcode,
			UsePackQRCode: barcode
		};
		var arr = [];
		arr.push(obj);
		var curArr = Ext.encode(arr);
		return curArr;
	},
	getQtyHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;

		var cboSearch = buttonsToolbar.getComponent('cboSearch').getValue();
		var txtSearch = buttonsToolbar.getComponent('txtSearch').getValue();
		txtScanCode = buttonsToolbar.getComponent('txtScanCode').getValue();

		var qtydtlHql = "reabmsqtydtl.Visible=1 and reabmsqtydtl.GoodsQty>0" +
			" and reabmsqtydtl.StorageID=" + me.StorageObj.StorageID;
		if(txtSearch && cboSearch == "2") {
			qtydtlHql += " and (reabmsqtydtl.LotNo like'%" + txtSearch + "%')";
		}
		return qtydtlHql;
	},
	getReaGoodsHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var GoodsClassType = buttonsToolbar.getComponent('GoodsClassType').getValue();
		var GoodsClass = buttonsToolbar.getComponent('GoodsClass').getValue();
		var cboSearch = buttonsToolbar.getComponent('cboSearch').getValue();
		var txtSearch = buttonsToolbar.getComponent('txtSearch').getValue();

		var reaGoodsHql = [];
		reaGoodsHql.push("1=1");
		if(GoodsClass) {
			reaGoodsHql.push("reagoods.GoodsClass='" + GoodsClass + "'");
		}
		if(GoodsClassType) {
			reaGoodsHql.push("reagoods.GoodsClassType='" + GoodsClassType + "'");
		}
		if(txtSearch && cboSearch == "1") {
			// or reagoods.SName like '%"+txtSearch+"%'
			reaGoodsHql.push("(reagoods.PinYinZiTou like '%" + txtSearch.toUpperCase() + "%' or reagoods.CName like'%" + txtSearch + "%'" +
				" or reagoods.ReaGoodsNo like'%" + txtSearch + "%' or reagoods.SName like'%" + txtSearch + "%')");
		}
		if(reaGoodsHql && reaGoodsHql.length > 0) {
			reaGoodsHql = reaGoodsHql.join(" and ");
		} else {
			reaGoodsHql = "";
		}
		return reaGoodsHql;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl + '&isMergeInDocNo=' + me.isMergeInDocNo;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		var reaGoodsHql = me.getReaGoodsHql();
		var qtyHql = me.getQtyHql();
		url += '&reaGoodsHql=' + JShell.String.encode(reaGoodsHql) + '&qtyHql=' + JShell.String.encode(qtyHql);
		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		if(where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		for(var i = 0; i < data.list.length; i++) {
			var ItemPrice = 0;
			//供应商+货品+批号+库房+货架
			var ReaCompanyID = data.list[i].ReaBmsQtyDtl_ReaCompanyID;
			var GoodsID = data.list[i].ReaBmsQtyDtl_GoodsID;
			var LotNo = data.list[i].ReaBmsQtyDtl_LotNo;
			var StorageID = data.list[i].ReaBmsQtyDtl_StorageID;
			var PlaceID = data.list[i].ReaBmsQtyDtl_PlaceID;
			var str = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
			data.list[i]["ReaBmsQtyDtl_Tab"] = str;
		}
		return data;
	},
	/**条码号扫码框*/
	getBarCode: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var txtScanCode = buttonsToolbar.getComponent('txtScanCode');
		return txtScanCode;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		if(!me.StorageObj.StorageID) {
			var info = "请选择库房后再检索库存货品信息！";
			JShell.Msg.alert(info, null, 3000);
			return;
		}
		me.hiddenColumns(true);
		me.load(null, true, autoSelect);
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		if(!me.StorageObj.StorageID) {
			var info = "请选择库房!";
			JShell.Msg.alert(info, null, 2000);
			me.store.removeAll();
			me.fireEvent('nodata', me);
			return;
		}
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	/**清除查询栏数据*/
	clearToolVal: function() {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var txtSearch = buttonsToolbar.getComponent("txtSearch");
		var GoodsClassType = buttonsToolbar.getComponent('GoodsClassType');
		var GoodsClass = buttonsToolbar.getComponent('GoodsClass');
		txtSearch.setValue('');
		GoodsClassType.setValue('');
		GoodsClass.setValue('');
	},
	/**获取开启近效期复选框*/
	getNeareffectCheck: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var com = buttonsToolbar.getComponent('testCheck');
		return com;
	},
	/**
	 * @description 按运行参数设置开启近效期复选框状态
	 * @param {Object} checked 选择默认值
	 * @param {Object} disabled 是否禁用
	 */
	setNeareffectCheck: function(checked, disabled) {
		var me = this;
		var com = me.getNeareffectCheck();
		if (com) {
			me.neareffectChecked=checked;
			me.neareffectDisabled=disabled;
			com.setValue(checked);
			com.setDisabled(disabled);
			//com.setReadOnly(true);
		}
		return com;
	},
	/**调整：获取开启供应批次合并复选框*/
	getInDocNoMergeCheck: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var com = buttonsToolbar.getComponent('isMergeInDocNo');
		return com;
	},
	/**调整
	 * @description 按运行参数设置开启供应批次合并复选框状态
	 * @param {Object} checked 选择默认值
	 * @param {Object} visible 是否隐藏
	 */
	setInDocNoMergeCheck: function(checked,visible) {
		var me = this;
		var com = me.getInDocNoMergeCheck();
		if(com) {
			me.inDocNoMergeChecked = checked; // 这条数据没有用，使用的是isMergeInDocNo这条数据
			me.isMergeInDocNo = checked;
			me.inDocNoMergeVisible = visible;
			com.setValue(checked); // 设置这个组件为选中
			com.setVisible(visible); // 隐藏该组件 true显示；false隐藏
		}
	},
	
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this;
		me.callParent(arguments);
		if(me.isOpenNearEffectPeriod!="1"&&me.isOpenNearEffectPeriod!="2"){
			
		}else{
			//重新调用
			me.setNeareffectCheck(me.neareffectChecked,me.neareffectDisabled);
		}
	}
});