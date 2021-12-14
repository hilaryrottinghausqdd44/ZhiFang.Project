/**
 * 源库货品列表(中间列表)
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.transfer.QtyDtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '源库货品列表',
	width: 800,
	height: 500,

	/**查询数据*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtl?isPlanish=true',
	/**根据货品条码获取货品相关信息*/
	ScanCodeUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlByBarCode?isPlanish=true',

	defaultOrderBy: [{
		property: 'ReaBmsQtyDtl_InvalidDate',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 5000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultDisableControl: false,
	/**序号列宽度*/
	rowNumbererWidth: 40,
	barcodeOperType: 6,
	/**后台排序*/
	remoteSort: false,
	curBarCode: '',
	/**表单选中的源库房*/
	SStorageObj: {},
	/**表单选中的目的库房*/
	DStorageObj: {},
	/**用户UI配置Key*/
	userUIKey: 'transfer.QtyDtlGrid',
	/**用户UI配置Name*/
	userUIName: "移库待选库存货品列表",
	/**移库或出库扫码是否允许从所有库房获取库存货品*/
	barCodeIsAllowOfALLStorage:false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var txtScanCode = buttonsToolbar.getComponent('txtScanCode');
		//扫码只有一行数据时，自动添加到明细列表
		me.store.on({
			load: function(com, records, successful, eOpts) {
				if (records && records.length == 1 && txtScanCode.getValue()) {
					me.fireEvent('dbitemclick', me, records[0]);
				}
			}
		});
		//移库或出库扫码是否允许从所有库房获取库存货品
		JShell.REA.RunParams.getRunParamsValue("TranOrOutBarCodeIsAllowOfALLStorage", true, function(data) {
			if (data.value && data.value.ParaValue && data.value.ParaValue == 1) {
				me.barCodeIsAllowOfALLStorage=true;
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('checkchange', 'dbitemclick', 'NObarcode', 'dbselectclick', 'scanCodeClick');
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
			hidden: false,
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
			dataIndex: 'ReaBmsQtyDtl_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			sortable: true,
			width: 100,
			minWidth: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsName',
			text: '货品名称',
			sortable: true,
			width: 120,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsQtyDtl_BarCodeType");
				if (!barCodeMgr) barCodeMgr = "";
				if (barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if (barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if (barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if (value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_UnitMemo',
			text: '规格',
			sortable: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsUnit',
			text: '单位',
			sortable: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsQty',
			text: '库存量',
			sortable: true,
			width: 65,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_SumCurrentQty',
			text: '剩余总库存',
			sortable: false,
			hidden: true,
			xtype: 'numbercolumn',
			format: '0.00',
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				meta.tdAttr = 'data-qtip="<b>货品扫码时,如果存在多条库存记录系统将(库房货架及供货商货品批号和包装单位)都相同的库存货品,自动合并为一行显示</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_Price',
			text: '单价',
			sortable: true,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotNo',
			text: '批号',
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageName',
			text: '源库房',
			sortable: true,
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceName',
			text: '源货架',
			sortable: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageID',
			text: '源库房ID',
			hidden: true,
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceID',
			text: '源库房ID',
			hidden: true,
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InvalidDate',
			text: '效期',
			sortable: true,
			width: 85,
			isDate: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CompanyName',
			text: '供应商',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_RegisterNo',
			text: '注册证号',
			sortable: true,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_QtyDtlID',
			text: 'QtyDtlID',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Id',
			text: '主键',
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
			dataIndex: 'ReaBmsQtyDtl_ReaCompanyID',
			text: '本地供应商Id',
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
			dataIndex: 'ReaBmsQtyDtl_ProdDate',
			text: 'ProdDate',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Tab',
			text: '合并标签',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotQRCode',
			text: '二维条码号',
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
			text: '源货品列表',
			hidden: true,
			style: "font-weight:bold;color:blue;",
			margin: '0 0 5 5'
		}, 'refresh', '-', {
			emptyText: '一级分类',
			labelWidth: 0,
			width: 95,
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
			width: 95,
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
					if (newValue) {
						var buttonsToolbar = me.getComponent('buttonsToolbar');
						var txtSearch = buttonsToolbar.getComponent('txtSearch');
						if (newValue == "2") {
							txtSearch.emptyText = '货品批号';
						} else {
							txtSearch.emptyText = '货品名称/货品编码/拼音字头';
						}
						txtSearch.applyEmptyText();
						if (txtSearch.getValue()) me.onSearch();
					}
				}
			}
		}, {
			name: 'txtSearch',
			itemId: 'txtSearch',
			emptyText: '货品名称/货品编码/拼音字头',
			width: 160,
			xtype: 'textfield',
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						var buttonsToolbar = me.getComponent("buttonsToolbar");
						var txtScanCode = buttonsToolbar.getComponent("txtScanCode");
						if (!me.SStorageObj.StorageID) {
							var info = "源库房不能为空!";
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
		}, '-', {
			name: 'txtScanCode',
			itemId: 'txtScanCode',
			emptyText: '按货品条码扫码',
			margin: '0 0 0 5',
			width: 180,
			labelAlign: 'right',
			xtype: 'textfield',
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						//防止扫码时,自动出现触发多个回车事件
						JShell.Action.delay(function() {
							if (!me.SStorageObj.StorageID) {
								JShell.Msg.alert('源库房不能为空!', null, 2000);
								return;
							}
							me.clearToolVal();
							if (!field.getValue()) {
								var info = "请输入条码号!";
								JShell.Msg.alert(info, null, 2000);
								me.store.removeAll();
								me.fireEvent('nodata', me);
								return;
							}
							me.fireEvent('scanCodeClick', field.getValue(), me);
						}, null, 30);
					}
				}
			}
		},
		{
			xtype: 'checkboxfield',
			margin: '0 0 0 10',
			boxLabel: '开启近效期',
			name: 'testCheck',
			itemId: 'testCheck',
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.fireEvent('testClick', me, newValue);
				}
			}
		}];
		return items;
	},
	/**
	 * 条码扫码框重新置空及获取焦点
	 */
	setScanCodeFocus: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (buttonsToolbar) {
			txtScanCode = buttonsToolbar.getComponent('txtScanCode');
			//txtScanCode.setValue('');
			if (txtScanCode) {
				txtScanCode.focus(true, 100);
			}
		}
	},
	/**调用服务返回货品
	 * 货品只有一条数据 不需要弹出列表选择
	 * 货品存在多条数据，需要用户选择
	 * */
	onScanCode: function(barCode, bo) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			txtScanCode = buttonsToolbar.getComponent('txtScanCode');
		txtScanCode.setValue('');
		//批条码已存在明细中不需要调用扫码服务
		if (bo) return;
		
		var url = JShell.System.Path.ROOT + me.ScanCodeUrl;
		var barCode2=JShell.String.encode(barCode);
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		url += '&barcode=' + barCode2 + '&barcodeOperType=' + me.barcodeOperType;
		url += '&storageId=' + me.SStorageObj.StorageID;
		//移库或出库扫码是否允许从所有库房获取库存货品
		url += '&isAllowOfALLStorage=' + me.barCodeIsAllowOfALLStorage;
		
		me.store.removeAll();
		me.curBarCode = '';

		JShell.Server.get(url, function(data) {
			if (data.success) {
				if (data && data.value) {
					var list = data.value.list;
					var msg = "源库房没有此货品,请重新选择库房后再扫码!";
					if (list.length == 0) {
						JShell.Msg.alert(msg, null, 2000);
						/* me.msgShow(msg,function(buttonId, text, opt){
							if(buttonId=="yes"){
								me.setScanCodeFocus();
							}
						}); */
					}
					me.insertStore(barCode, list);
				}
			} else {
				//JShell.Msg.error('源库房没有此货品,请重新选择源库房后再扫码!</BR>' + data.msg);
				var msg='源库房没有此货品,请重新选择源库房后再扫码!</BR>' + data.msg;
				me.msgShow(msg,function(buttonId, text, opt){
					if(buttonId=="yes"){
						me.fireEvent('NObarcode', me);
						me.store.removeAll();
						me.setScanCodeFocus();
					}
				});				
			}
		}, false);
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
	 * 将返回的库存货品添加到库存列表的store
	 * */
	insertStore: function(barcode, list) {
		var me = this;
		for (var i = 0; i < list.length; i++) {
			var qtyDtlID = list[i].ReaBmsQtyDtl_Id;
			var goodsID = list[i].ReaBmsQtyDtl_GoodsID;
			//供应商+货品+批号+库房+货架
			var JObjectBarCode = list[i].ReaBmsQtyDtl_JObjectBarCode;
			if (JObjectBarCode) var JObjectBarCode = Ext.JSON.decode(JObjectBarCode);
			if (JObjectBarCode) {
				goodsID = JObjectBarCode.GoodsID;
			}
			var str = list[i].ReaBmsQtyDtl_ReaCompanyID;
			str = str + goodsID;
			str = str + list[i].ReaBmsQtyDtl_LotNo;
			str = str + list[i].ReaBmsQtyDtl_StorageID;
			str = str + list[i].ReaBmsQtyDtl_PlaceID;

			var obj1 = {
				ReaBmsQtyDtl_Tab: str,
				ReaBmsQtyDtl_BarCodeQtyDtlID: qtyDtlID
			};
			me.curBarCode = barcode;
			var obj2 = Ext.Object.merge(list[i], obj1);
			me.store.insert(me.store.getCount(), obj2);
		}
		me.hiddenColumns(false);
		var records = me.store.data.items;
		//只存在一行数据时，默认选择
		if (records.length == 1) {
			//记录当次扫码操作
			var CurArr = me.getCurReaGoodsScanCodeList(barcode);
			records[0].set('ReaBmsQtyDtl_CurReaGoodsScanCodeList', CurArr);
			//ui默认选择一行(第一行)
			if (records.length > 0) me.getSelectionModel().select(0);
			//不开启近效期时，只有一行数据，默认数据到出库明细中
			var NeareffectCheck = me.getNeareffectCheck();
			if (!NeareffectCheck.getValue()) {
				me.fireEvent('dbselectclick', me, records[0], barcode);
			}
		}
	},
	/**
	 * 显示或隐藏"剩余总库存"列
	 * 货品扫码时,显示"剩余总库存"列;其他条件检索库存货品时,隐藏"剩余总库存"列
	 * */
	hiddenColumns: function(isHidden) {
		var me = this;
		var counts = 0;
		Ext.Array.each(me.columns, function(column, index, countriesItSelf) {
			if (counts == 1) return false;
			if (column.dataIndex == "ReaBmsQtyDtl_SumCurrentQty") {
				if (isHidden == true) me.columns[index].hide();
				else me.columns[index].show();
				counts = counts + 1;
			}
		});
	},
	getQtyHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (!buttonsToolbar) return;

		var cboSearch = buttonsToolbar.getComponent('cboSearch').getValue();
		var txtSearch = buttonsToolbar.getComponent('txtSearch').getValue();
		txtScanCode = buttonsToolbar.getComponent('txtScanCode').getValue();

		var qtydtlHql = "reabmsqtydtl.Visible=1 and reabmsqtydtl.GoodsQty>0" +
			" and reabmsqtydtl.StorageID=" + me.SStorageObj.StorageID;
		if (txtSearch && cboSearch == "2") {
			qtydtlHql += " and (reabmsqtydtl.LotNo like '%" + txtSearch + "')";
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
		if (GoodsClass) {
			reaGoodsHql.push("reagoods.GoodsClass='" + GoodsClass + "'");
		}
		if (GoodsClassType) {
			reaGoodsHql.push("reagoods.GoodsClassType='" + GoodsClassType + "'");
		}
		if (txtSearch && cboSearch == "1") {
			// or reagoods.SName like '%"+txtSearch+"%'
			reaGoodsHql.push("(reagoods.PinYinZiTou like '%" + txtSearch.toUpperCase() + "%' or reagoods.CName like'%" +
				txtSearch + "%'" +
				" or reagoods.ReaGoodsNo like'%" + txtSearch + "%')");
		}
		if (reaGoodsHql && reaGoodsHql.length > 0) {
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
		// 出库与移库，使用同一个服务，在移库这里强制加上isMergeInDocNo=true，给后台判断
		url += '&reaGoodsHql=' + JShell.String.encode(reaGoodsHql) + '&qtyHql=' + JShell.String.encode(qtyHql)+'&isMergeInDocNo=true';
		
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this,
			result = {},
			list = [],
			arr = [];
		for (var i = 0; i < data.list.length; i++) {
			var ItemPrice = 0;
			//供应商+货品+批号+库房+货架
			var ReaCompanyID = data.list[i].ReaBmsQtyDtl_ReaCompanyID;
			var GoodsID = data.list[i].ReaBmsQtyDtl_GoodsID;
			var LotNo = data.list[i].ReaBmsQtyDtl_LotNo;
			var StorageID = data.list[i].ReaBmsQtyDtl_StorageID;
			var PlaceID = data.list[i].ReaBmsQtyDtl_PlaceID;

			var JObjectBarCode = data.list[i].ReaBmsQtyDtl_JObjectBarCode;
			if (JObjectBarCode) var JObjectBarCode = Ext.JSON.decode(JObjectBarCode);
			var QtyDtlID = data.list[i].ReaBmsQtyDtl_Id;

			if (JObjectBarCode) {
				GoodsID = JObjectBarCode.GoodsID;
				QtyDtlID = JObjectBarCode.QtyDtlID;
			}
			var str = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
			var obj1 = {
				ReaBmsQtyDtl_Tab: str
			};
			var obj2 = Ext.Object.merge(data.list[i], obj1);
			arr.push(obj2);
		}
		result.list = arr;
		return data;
	},
	/**获取开启近效期复选框*/
	getNeareffectCheck: function() {
		var me = this;
		var com = '';
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			com = buttonsToolbar.getComponent('testCheck');
		return com;
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
		var CurArr = Ext.encode(arr);
		return CurArr;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		if (!me.SStorageObj.StorageID) {
			var info = "源库房不能为空";
			JShell.Msg.alert(info, null, 2000);
			return;
		}
		me.hiddenColumns(true);
		me.load(null, true, autoSelect);
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		if (!me.SStorageObj.StorageID) {
			var info = "请选择源库房!";
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
	}
});
