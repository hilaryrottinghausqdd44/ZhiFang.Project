/**
 * 待选货品列表(中间列表,库存表)
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.enrollment.QtyDtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '待选货品列表',
	width: 800,
	height: 500,

	/**查询库存表数据*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtl?isPlanish=true',
	defaultOrderBy: [{
		property: 'ReaBmsQtyDtl_InvalidDate',
		direction: 'ASC'
	}],
	/**仪器试剂服务查询服务*/
	selectRELinkUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaEquipReagentLinkByHQL?isPlanish=true',
	/**仪器查询服务*/
	selectLabUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLabByHQL?isPlanish=true',
	/**根据货品条码获取货品相关信息*/
	scanCodeUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlByBarCode?isPlanish=true',
	/**获取获取库房货架权限服务路径*/
	selectStorageLinkUrl: '/ReaManageService.svc/RS_UDTO_SearchListByStorageAndLinHQL?isPlanish=true',
	/**默认加载数据*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultDisableControl: false,
	/**复选框*/
	//multiSelect: true,
	//selType: 'checkboxmodel',
	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**当前扫码的条码号*/
	curBarCode: '',
	/**条码类型*/
	barcodeOperType: '7',
	/**后台排序*/
	remoteSort: false,
	/**表单选中的库房*/
	StorageObj: {},
	/**权限库房数据*/
	StorageData: [],
	/**用户UI配置Key*/
	userUIKey: 'out.enrollment.QtyDtlGrid',
	/**用户UI配置Name*/
	userUIName: "出库库存货品列表",
	/**移库或出库扫码是否允许从所有库房获取库存货品*/
	barCodeIsAllowOfALLStorage:false,
	//开启近效期复选框选择状态
	neareffectChecked:true,
	//开启近效期复选框是否禁用
	neareffectDisabled:false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onOneBarCode();
		//获取库房并默认
		me.loadStorage();
		//移库或出库扫码是否允许从所有库房获取库存货品
		JShell.REA.RunParams.getRunParamsValue("TranOrOutBarCodeIsAllowOfALLStorage", true, function(data) {
			if (data.value && data.value.ParaValue && data.value.ParaValue == 1) {
				me.barCodeIsAllowOfALLStorage=true;
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('dbitemclick', 'NObarcode', 'dbselectclick', 'scanCodeClick', 'setDefaultStorage', 'testClick');
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
		var columns = [{
			dataIndex: 'ReaBmsQtyDtl_StorageName',
			text: '库房',
			sortable: true,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceName',
			text: '货架',
			sortable: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CompanyName',
			text: '供应商',
			sortable: true,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: true,
			width: 100,
			minWidth: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsName',
			text: '货品名称',
			sortable: true,
			width: 140,
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
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsNo',
			text: '平台编码',
			hidden: true,
			sortable: true,
			width: 100,
			minWidth: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsQty',
			text: '库存量',
			sortable: true,
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsUnit',
			text: '单位',
			sortable: true,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_UnitMemo',
			text: '规格',
			sortable: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Price',
			text: '单价',
			sortable: true,
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InvalidDate',
			text: '效期',
			sortable: true,
			width: 85,
			isDate: true,
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
			text: '本地供应商',
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
			dataIndex: 'ReaBmsQtyDtl_TaxRate',
			text: 'TaxRate',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsSerial',
			text: 'GoodsSerial',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotSerial',
			text: 'LotSerial',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_SysLotSerial',
			text: 'SysLotSerial',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CompGoodsLinkID',
			text: 'CompGoodsLinkID',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaServerCompCode',
			text: 'ReaServerCompCode',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_BarCodeType',
			text: 'BarCodeType',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdDate',
			text: 'ProdDate',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InvalidDate',
			text: 'InvalidDate',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Tab',
			text: '供应商+货品+批号+库房+货架',
			sortable: true,
			hidden: true,
			width: 100,
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
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_JObjectBarCode',
			sortable: true,
			hidden: true,
			text: '(移库/出库)货品扫码时封装条码的相关信息',
			width: 100,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			xtype: 'label',
			text: '待选货品列表',
			hidden: true,
			style: "font-weight:bold;color:blue;",
			margin: '0 0 10 10'
		}, 'refresh', '-', {
			fieldLabel: '',
			name: 'ReaBmsOutDoc_StorageID',
			itemId: 'ReaBmsOutDoc_StorageID',
			xtype: 'uxSimpleComboBox',
			value: '',
			labelAlign: 'right',
			hasStyle: true,
			emptyText: '库房选择',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.fireEvent('setDefaultStorage', com.getValue(), com.getRawValue());
				}
			},
			labelWidth: 0,
			width: 105
		}, {
			emptyText: '一级分类',
			labelWidth: 0,
			width: 105,
			fieldLabel: '',
			itemId: 'GoodsClass',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
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
			width: 105,
			fieldLabel: '',
			itemId: 'GoodsClassType',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
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
			emptyText: '检索条件选择',
			fieldLabel: '检索',
			labelWidth: 35,
			width: 125,
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
							txtSearch.emptyText = '货品名称/编码/拼音字头/简称';
						}
						txtSearch.applyEmptyText();
						if(txtSearch.getValue()) me.onSearch();
					}
				}
			}
		}, {
			name: 'txtSearch',
			itemId: 'txtSearch',
			emptyText: '货品名称/编码/拼音字头/简称',
			width: 165,
			xtype: 'textfield',
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						var txtScanCode = me.getTxtScanCode();
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
			emptyText: '条码号扫码', //margin: '0 0 0 5',//labelSeparator:'',
			width: 145,
			hidden: false,
			labelAlign: 'right',
			xtype: 'textfield',
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					me.onBarCodeClick(field, e);
				}
			}
		}, '-', {
			xtype: 'checkboxfield',
			boxLabel: '开启近效期检测',
			name: 'testCheck',
			itemId: 'testCheck',
			isLocked:true,
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.fireEvent('testClick', me, newValue);
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
				if(!field.getValue()) {
					var info = "请输入条码号!";
					JShell.Msg.alert(info, null, 2000);
					me.store.removeAll();
					me.fireEvent('nodata', me);
					return;
				}
				me.fireEvent('scanCodeClick', field.getValue(), me);
			}, null, 30);
		}
	},
	/**获取当前条码框*/
	getTxtScanCode: function() {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var txtScanCode = buttonsToolbar.getComponent("txtScanCode");
		return txtScanCode;
	},
	/**调用服务返回货品
	 * 货品只有一条数据 不需要弹出列表选择
	 * 货品存在多条数据，需要用户选择
	 * */
	onScanCode: function(barcode, bo) {
		var me = this;
		var txtScanCode = me.getTxtScanCode();
		txtScanCode.setValue('');
		//批条码已存在明细中不需要调用扫码服务
		if(bo) return;
		if(!me.StorageObj.StorageID) {
			JShell.Msg.alert('库房不能为空,请先选择!', null, 2000);
			return;
		}
		var barcode2=JShell.String.encode(barcode);
		var url = JShell.System.Path.ROOT + me.scanCodeUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		url += '&barcode=' + barcode2 + '&barcodeOperType=' + me.barcodeOperType;		
		url += '&storageId=' + me.StorageObj.StorageID;
		//移库或出库扫码是否允许从所有库房获取库存货品
		url += '&isAllowOfALLStorage=' + me.barCodeIsAllowOfALLStorage;
		
		me.store.removeAll();
		me.curBarCode = '';
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data && data.value) {
					var list = data.value.list,
						len = list.length;
					var info = "库房没有此货品,请重新选择库房后再扫码!";
					if(len == 0) JShell.Msg.alert(info, null, 2000);
					for(var i = 0; i < len; i++) {
						var ItemPrice = 0;
						var QtyDtlID = list[i].ReaBmsQtyDtl_Id;
						//供应商+货品+批号+库房+货架
						var ReaCompanyID = list[i].ReaBmsQtyDtl_ReaCompanyID;
						var GoodsID = list[i].ReaBmsQtyDtl_GoodsID;
						var LotNo = list[i].ReaBmsQtyDtl_LotNo;
						var StorageID = list[i].ReaBmsQtyDtl_StorageID;
						var PlaceID = list[i].ReaBmsQtyDtl_PlaceID;
						var str = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
						var obj1 = {
							ReaBmsQtyDtl_Tab: str,
							ReaBmsQtyDtl_BarCodeQtyDtlID: QtyDtlID
						};
						me.curBarCode = barcode;
						var obj2 = Ext.Object.merge(list[i], obj1);
						me.store.insert(me.store.getCount(), obj2);
					}
					var records = me.store.data.items,
						len = records.length;
					//只存在一行数据时，默认选择
					if(len == 1) me.oneRecSelect(barcode, QtyDtlID);
				}
			} else {
				JShell.Msg.error('库房没有此货品,请重新选择库房后再扫码!</br>' + data.msg);
				me.fireEvent('NObarcode', me);
				me.store.removeAll();
			}
		}, false);
	},
	/**只有一行数据时选择行出来*/
	oneRecSelect: function(barcode, QtyDtlID) {
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

		var reabmsqtydtlHql = "reabmsqtydtl.Visible=1 and reabmsqtydtl.GoodsQty>0" +
			" and reabmsqtydtl.StorageID=" + me.StorageObj.StorageID;
		if(txtSearch && cboSearch == "2") {
			reabmsqtydtlHql += " and (reabmsqtydtl.LotNo like'%" + txtSearch + "%')";
		}
		return reabmsqtydtlHql;

	},
	getReaGoodsHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var GoodsClassType = buttonsToolbar.getComponent('GoodsClassType').getValue();
		var GoodsClass = buttonsToolbar.getComponent('GoodsClass').getValue();
		var cboSearch = buttonsToolbar.getComponent('cboSearch').getValue();
		var txtSearch = buttonsToolbar.getComponent('txtSearch').getValue();

		var reaGoodsHql = [];
		if(GoodsClass) {
			reaGoodsHql.push("reagoods.GoodsClass='" + GoodsClass + "'");
		}
		if(GoodsClassType) {
			reaGoodsHql.push("reagoods.GoodsClassType='" + GoodsClassType + "'");
		}
		if(txtSearch && cboSearch == "1") {
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
			JShell.System.Path.ROOT) + me.selectUrl;

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
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		if(!me.StorageObj.StorageID) {
			var info = "库房不能为空！";
			JShell.Msg.alert(info, null, 2000);
			return;
		}
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
	/**扫码处理*/
	onOneBarCode: function() {
		var me = this;
		var txtScanCode = me.getTxtScanCode();
		//扫码只有一行数据时，自动添加到明细列表
		me.store.on({
			load: function(com, records, successful, eOpts) {
				if(records.length == 1 && txtScanCode.getValue()) {
					me.fireEvent('dbitemclick', me, records[0]);
				}
			}
		});
	},
	/**获取库房*/
	getStorageObj: function() {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var StorageName = buttonsToolbar.getComponent('ReaBmsOutDoc_StorageID');

		var StorageObj = {
			StorageID: StorageName.getValue(),
			StorageName: StorageName.getRawValue()
		};
		return StorageObj;
	},
	/**按登录者权限获取库房*/
	loadStorage: function() {
		var me = this;
		me.StorageData = [];
		var empId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || "";
		me.getStorageJurisdiction(empId, function(data) {
			if(data && data.value) {
				me.StorageData = data.value.list;
				me.setStorageValue();
			}
		});
	},
	setStorageValue: function() {
		var me = this;
		//默认显示
		var id = '',
			name = '';
		if(me.StorageData.length > 0) {
			id = me.StorageData[0].ReaStorage_Id;
			name = me.StorageData[0].ReaStorage_CName;
		}
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var storageID = buttonsToolbar.getComponent('ReaBmsOutDoc_StorageID');
		if(storageID) {
			storageID.loadData(me.getStorageData(me.StorageData));
			storageID.setValue(id);
		}
		me.StorageObj = {
			StorageID: id,
			StorageName: name
		};
		if(id) me.onSearch();
		if(!id) {
			JShell.Msg.alert('登陆者没有出库权限,请先设置库房权限');
			return;
		}
	},
	/**获取库房列表*/
	getStorageData: function(list) {
		var me = this,
			data = [];
		for(var i in list) {
			var obj = list[i];
			data.push([obj.ReaStorage_Id, obj.ReaStorage_CName]);
		}
		return data;
	},
	/**获取库房权限关系的Hql*/
	getStorageLinkHql: function(takerId) {
		var me = this;
		var operType = "1";
		var linkHql = "reauserstoragelink.OperType=" + operType;
		linkHql += ' and reauserstoragelink.OperID=' + takerId;
		return linkHql;
	},
	/**获取库房权限关系的Url*/
	getStorageLinkUrl: function(takerId) {
		var me = this;
		var params = [];
		params.push(JShell.System.Path.ROOT + me.selectStorageLinkUrl);
		params.push("fields=ReaStorage_CName,ReaStorage_Id,ReaStorage_IsMainStorage");
		params.push("storageHql=reastorage.Visible=1");
		params.push("linkHql=" + me.getStorageLinkHql(takerId));
		params.push("sort=[{property:'ReaStorage_IsMainStorage',direction:'DESC'},{property:'ReaStorage_DispOrder',direction:'ASC'}]");
		params.push("operType=1");
		return params;
	},
	/**获取库房货架权限的库房信息（按领用人）*/
	getStorageJurisdiction: function(takerId, callback) {
		var me = this;
		if(!takerId) {
			JShell.Msg.alert('登陆者没有权限,请先设置库房权限');
			return;
		}
		var url = me.getStorageLinkUrl(takerId);
		if(url) url = url.join("&");
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**获取开启近效期复选框*/
	getNeareffectCheck: function() {
		var me = this;
		var com = '';
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			com = buttonsToolbar.getComponent('testCheck');
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
			//console.log(checked+" "+disabled);
			me.neareffectChecked=checked;
			me.neareffectDisabled=disabled;
			com.setValue(checked);
			com.setDisabled(disabled);
		}
		return com;
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this;
		me.callParent(arguments);
		//重新调用
		//me.setNeareffectCheck();
	}
});