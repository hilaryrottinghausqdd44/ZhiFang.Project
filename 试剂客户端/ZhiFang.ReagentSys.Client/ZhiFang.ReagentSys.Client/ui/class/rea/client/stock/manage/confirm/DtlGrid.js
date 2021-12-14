/**
 * 入库确认
 * @author longfc
 * @version 2019-03-08
 */
Ext.define('Shell.class.rea.client.stock.manage.confirm.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '明细列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtlVOByHQL?isPlanish=true',

	/**扫码服务*/
	scanCodeUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsInDtl',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**默认选中*/
	autoSelect: false,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**带分页栏*/
	hasPagingtoolbar: false,

	/**货品明细弹出消息框消失时间*/
	hideTimes: 5000,
	/**是否显示货品信息(双击批号单元格选择时隐藏)*/
	IsShowDtlInfo: true,
	/**扫码模式(严格模式:strict,混合模式：mixing)*/
	CodeScanningMode: "mixing",
	formtype: "add",

	/**供应商ID*/
	ReaCompanyID: null,
	ReaServerCompCode: null,
	CompanyName: null,
	ReaCompCode: null,
	/**编辑单元格pluginId*/
	cellpluginId: 'cellpluginId',
	/**用户UI配置Key*/
	userUIKey: 'stock.manage.confirm.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "入库明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			select: function(RowModel, record) {
				if(me.IsShowDtlInfo == true)
					me.onShowDtlInfo(record);
			}
		});
		me.store.on({
			update: function(store, record) {
				if(record.dirty) {
					var changedObj = record.getChanges();
					for(var modified in changedObj) {
						if(modified == "ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty")
							me.onGoodsQtyChanged(record);
						else if(modified == "ReaBmsInDtlVO_ReaBmsInDtl_Price")
							me.onPriceChanged(record);
					}
				}
			}
		});
		//入库货品扫码 严格模式:1,混合模式：2"
		JcallShell.REA.RunParams.getRunParamsValue("InScanCode", false, function(data) {
			if(data.success) {
				var paraValue = "2";
				var obj = data.value;
				if(obj.ParaValue) {
					paraValue = "" + obj.ParaValue;
				}
				if(paraValue == "1") {
					me.CodeScanningMode = "strict";
				} else {
					me.CodeScanningMode = "mixing";
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.initReaComInfo();
		me.addEvents('onScanCodeShowDtl', 'onChangeSumTotal');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			pluginId: me.cellpluginId,
			clicksToEdit: 1
		});
		//创建功能按钮栏Items
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
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoodsNo',
			text: '货品编码',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ProdGoodsNo',
			sortable: false,
			text: '厂商货品编码',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_CenOrgGoodsNo',
			text: '供货商货品码',
			hidden: false,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_GoodsNo',
			sortable: false,
			text: '货品平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType',
			text: '条码类型',
			hidden: true,
			width: 85,
			renderer: function(value, meta, record) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_GoodsCName',
			text: '货品名称',
			width: 150,
			columnCountKey: me.columnCountKey,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_GoodsUnit',
			text: '包装单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_StorageName',
			text: '所属库房',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_PlaceName',
			text: '所属货架',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_LotNo',
			text: '货品批号',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_InvalidDate',
			text: '有效期至',
			width: 80,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_Price',
			text: '单价',
			width: 80,
			type: 'float',
			align: 'right',
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty',
			text: '入库数',
			width: 85,
			type: 'float',
			align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_SumTotal',
			sortable: false,
			text: '总计金额',
			align: 'right',
			type: 'float',
			width: 80,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_Memo',
			sortable: false,
			text: '<b style="color:red;">备注信息</b>',
			width: 80,
			hidden: false,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ProdDate',
			text: '<b style="color:blue;">生产日期</b>',
			align: 'center',
			width: 90,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			}
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_BiddingNo',
			text: '<b style="color:blue;">招标号</b>',
			width: 80,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_TaxRate',
			text: '<b style="color:blue;">税率</b>',
			align: 'right',
			width: 60,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_RegisterNo',
			sortable: false,
			text: '<b style="color:blue;">注册证编号</b>',
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_RegisterInvalidDate',
			text: '<b style="color:blue;">注册证有效期</b>',
			width: 85,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_CompanyName',
			text: '所属供应商',
			width: 105,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ReaCompCode',
			text: '供应商编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_SysLotSerial',
			sortable: false,
			text: '系统内部批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_Id',
			sortable: false,
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_CompGoodsLinkID',
			sortable: false,
			text: '供应商与货品关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ReaCompanyID',
			sortable: false,
			text: '供应商ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ReaServerCompCode',
			sortable: false,
			text: '供应商机平台构码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ApproveDocNo',
			sortable: false,
			text: '批准文号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_CurReaBmsInDtlLinkListStr',
			sortable: false,
			text: '当次入库条码扫码操作集合Str',
			hidden: true,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_StorageID',
			sortable: false,
			text: '库房ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_PlaceID',
			sortable: false,
			text: '货架ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_GoodsSerial',
			sortable: false,
			text: 'GoodsSerial',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_GoodsSort',
			sortable: false,
			text: '货品序号',
			hidden: true,
			defaultRenderer: true
		}];
		columns.push({
			dataIndex: me.DelField,
			text: '',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v = '';
				if(value === 'true') {
					v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
				}
				if(value === 'false') {
					v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
				}
				var msg = record.get('ErrorInfo');
				if(msg) {
					meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
				}

				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_OtherDtlNo',
			sortable: false,
			text: '提取第三方数据的明细单号',
			hidden: true,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ZX1',
			sortable: false,
			text: 'ZX1',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ZX2',
			sortable: false,
			text: 'ZX2',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ZX3',
			sortable: false,
			text: 'ZX3',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_SaleDtlID',
			sortable: false,
			text: '供货明细ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_SaleDtlConfirmID',
			sortable: false,
			text: '供货验收明细单ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_InDtlNo',
			sortable: false,
			text: '入库明细单号',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_InDocID',
			sortable: false,
			text: '入库单ID',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_InDocNo',
			sortable: false,
			text: '入库总单号',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_FactoryOutTemperature',
			text: '厂家出库温度',
			editor: {
				readOnly: false
			},
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_ArrivalTemperature',
			text: '到货温度',
			width: 80,
			editor: {
				readOnly: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_AppearanceAcceptance',
			text: '外观验收',
			width: 80,
			editor: {
				readOnly: false
			},
			defaultRenderer: true
		});
		return columns;
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
		if(!me.PK && me.formtype == "edit") return false;

		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		if(records && records.length > 0) {
			me.getSelectionModel().selectAll();
		}
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());
		return items;
	},
	/**货架工具栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = {
			xtype: 'toolbar',
			dock: 'top',
			hidden: true,
			itemId: 'buttonsToolbar2',
			items: []
		};
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		items.push({
			iconCls: 'button-refresh',
			text: '重置',
			tooltip: '重置',
			handler: function() {
				me.onRefreshClick();
			}
		}, '-', {
			fieldLabel: '',
			emptyText: '库房选择',
			name: 'StorageName',
			itemId: 'StorageName',
			labelWidth: 0,
			width: 160,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.shelves.storage.CheckGrid',
			classConfig: {
				title: '库房选择',
				/**是否单选*/
				checkOne: true,
				width: 300
			},
			listeners: {
				check: function(p, record) {
					var buttonsToolbar = me.getComponent('buttonsToolbar');
					var StorageID = buttonsToolbar.getComponent('StorageID');
					var StorageName = buttonsToolbar.getComponent('StorageName');
					StorageID.setValue(record ? record.get('ReaStorage_Id') : '');
					StorageName.setValue(record ? record.get('ReaStorage_CName') : '');
					var id = record ? record.get('ReaStorage_Id') : '';
					var name = record ? record.get('ReaStorage_CName') : '';
					me.onStorageCheck(id, name);
					me.onPlaceLoadData(id, name);
					p.close();
				}
			}
		}, {
			xtype: 'textfield',
			itemId: 'StorageID',
			name: 'StorageID',
			fieldLabel: '库房ID',
			hidden: true
		});

		items.push('-', {
			xtype: 'checkboxfield',
			boxLabel: '是否显示浮动窗',
			checked: true,
			inputValue: 1,
			name: 'cboIShowDtlInfo',
			itemId: 'cboIShowDtlInfo',
			listeners: {
				change: function(field, newValue, oldValue, e) {
					var selected = me.getSelectionModel().getSelection();
					if(selected && selected.length > 0)
						me.onShowDtlInfo(selected[0]);
				}
			}
		}, '-', {
			xtype: 'checkboxfield',
			boxLabel: '入库确认后条码打印',
			checked: true,
			inputValue: 1,
			name: 'cbIsPrint',
			itemId: 'cbIsPrint'
		});

		return items;
	},
	initReaComInfo: function() {
		var me = this;
		//获取还原当前用户在手工入库时最后一次供应商选择的值
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var key = "stock.manualinput." + userId;
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
	setReaComInfo: function() {
		var me = this;
		//更新当前用户在手工入库时最后一次供应商选择的值
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var key = "stock.manualinput." + userId;
		var reaCom = {
			"ReaCompanyID": me.ReaCompanyID,
			"ReaServerCompCode": me.ReaServerCompCode,
			"ReaCompCode": me.ReaCompCode,
			"CompanyName": me.CompanyName,
		};
		reaCom = JcallShell.JSON.encode(reaCom);
		JcallShell.LocalStorage.set(key, reaCom);
	},
	/**@description 货品为盒条码时的入库数量输入框的处理*/
	comSetReadOnlyOfBarCodeType: function(field, e) {
		var me = this;
		var record = field.ownerCt.editingPlugin.context.record;
		var barCodeMgr = "" + record.get("ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType");
		//如果扫码模式为严格模式,批条码及盒条码需要扫码&&barCodeMgr=="1"
		if(me.CodeScanningMode == "strict" && barCodeMgr == "1") {
			field.setReadOnly(true);
			//return;
		} else {
			field.setReadOnly(false);
		}
	},
	/***
	 * 对选择行只设置库房
	 * @param {Object} id
	 * @param {Object} cname
	 */
	onStorageCheck: function(id, cname) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) return;

		var len = records.length;
		for(var i = 0; i < len; i++) {
			records[i].set('ReaBmsInDtlVO_ReaBmsInDtl_StorageName', cname);
			records[i].set('ReaBmsInDtlVO_ReaBmsInDtl_StorageID', id);
			records[i].commit();
		}
	},
	/**选中库房加载货架*/
	onPlaceLoadData: function(storageID, storageCName) {
		var me = this;
		me.hideMask();
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		buttonsToolbar.removeAll();
		if(!storageID) {
			me.noPlaceTip(buttonsToolbar);
			buttonsToolbar.hide();
			return;
		}

		var arr = [];
		me.getPlaceById(storageID, function(data) {
			if(data && data.value) {
				if(data.value.list.length == 0) {
					me.noPlaceTip(buttonsToolbar);
				}
				for(var i = 0; i < data.value.list.length; i++) {
					var placeCName = data.value.list[i].ReaPlace_CName;
					var placeID = data.value.list[i].ReaPlace_Id;
					var btn = {
						xtype: 'button',
						itemId: 'btn' + i,
						text: placeCName,
						tooltip: placeCName,
						enableToggle: false,
						StorageCName: storageCName,
						StorageID: storageID,
						PlaceID: placeID,
						PlaceCName: placeCName
					};
					buttonsToolbar.add(btn, '-');
				}
			} else {
				me.noPlaceTip(buttonsToolbar);
			}
		});

		buttonsToolbar.show();
		for(var i = 0; i < buttonsToolbar.items.length; i++) {
			//'-' 不处理
			if(buttonsToolbar.items.items[i].itemId) {
				buttonsToolbar.items.items[i].on({
					click: function(com, e, eOpts) {
						me.cleartogglebuttonsToolbar(buttonsToolbar, com);
						com.toggle(true);
						me.setRecStoragePlace(com);
						me.getSelectionModel().deselectAll();
					}
				});
			}
		}
	},
	/***不选中的按钮清空选中状态     */
	cleartogglebuttonsToolbar: function(buttonsToolbar, com) {
		for(var i = 0; i < buttonsToolbar.items.length; i++) {
			if(buttonsToolbar.items.items[i].itemId) {
				if(com.itemId != buttonsToolbar.items.items[i].itemId) {
					buttonsToolbar.items.items[i].toggle(false);
				}
			}
		}
	},
	/**给勾选的记录行的库房及货架赋值*/
	setRecStoragePlace: function(com) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error('请选择需要设置库房货架的数据');
			return;
		}
		var len = records.length;
		var storageCName = com.StorageCName;
		var storageID = com.StorageID;
		var placeID = com.PlaceID;
		var placeCName = com.PlaceCName;
		for(var i = 0; i < len; i++) {
			records[i].set('ReaBmsInDtlVO_ReaBmsInDtl_StorageName', storageCName);
			records[i].set('ReaBmsInDtlVO_ReaBmsInDtl_StorageID', storageID);
			records[i].set('ReaBmsInDtlVO_ReaBmsInDtl_PlaceID', placeID);
			records[i].set('ReaBmsInDtlVO_ReaBmsInDtl_PlaceName', placeCName);
			records[i].commit();
		}
	},
	/**没有货架提示*/
	noPlaceTip: function(buttonsToolbar) {
		var me = this;
		var label = {
			xtype: 'label',
			text: '没有货架',
			style: 'color: #FF0000',
			margins: '0 0 0 10'
		};
		buttonsToolbar.add(label);
	},
	/**根据库房id获取货架*/
	getPlaceById: function(id, callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + '/ReaSysManageService.svc/ST_UDTO_SearchReaPlaceByHQL?isPlanish=true';
		url += '&fields=ReaPlace_Id,ReaPlace_CName&where=reaplace.Visible=1 and reaplace.ReaStorage.Id=' + id;
		url += '&sort=[{"property":"ReaPlace_DispOrder","direction":"ASC"}]'
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**@description 入库数值改变后联动*/
	onGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsInDtlVO_ReaBmsInDtl_Price');
		var GoodsQty = record.get('ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if(Price) Price = parseFloat(Price);
		else Price = 0;

		var SumTotal = parseFloat(Price) * parseFloat(GoodsQty);
		SumTotal = SumTotal ? SumTotal : 0;
		record.set('ReaBmsInDtlVO_ReaBmsInDtl_SumTotal', SumTotal);
		record.commit();
		me.onChangeSumTotal();
	},
	/**@description 单价值改变后联动*/
	onPriceChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsInDtlVO_ReaBmsInDtl_Price');
		var GoodsQty = record.get('ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		else GoodsQty = 0;
		if(Price) Price = parseFloat(Price);
		else Price = 0;

		var SumTotal = parseFloat(Price) * parseFloat(GoodsQty);
		SumTotal = SumTotal ? SumTotal : 0;
		record.set('ReaBmsInDtlVO_ReaBmsInDtl_SumTotal', SumTotal);
		record.commit();
		me.onChangeSumTotal();
	},
	onChangeSumTotal: function() {
		var me = this;
		var totalPrice = 0;
		me.store.each(function(record) {
			var sumTotal = record.get("ReaBmsInDtlVO_ReaBmsInDtl_SumTotal");
			if(!sumTotal) sumTotal = 0;
			totalPrice = totalPrice + parseFloat(sumTotal);
		});
		me.fireEvent('onChangeSumTotal', me, totalPrice);
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
			"CName": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsCName") : "",
			"EName": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_EName") : "",
			"SName": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_SName") : "",
			"Unit": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsUnit") : "",
			"UnitMemo": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_UnitMemo") : "",
			"LotNo": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_LotNo") : "",
			"InvalidDate": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_InvalidDate") : "",
			"GoodsQty": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty") : "",
			"Price": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_Price") : "",
			"SumTotal": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_SumTotal") : "",
			"CompanyName": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_CompanyName") : "",
			"ReaGoodsNo": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaGoodsNo") : "",
			"ProdGoodsNo": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_ProdGoodsNo") : "",
			"CenOrgGoodsNo": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_CenOrgGoodsNo") : "",
			"GoodsNo": rec ? rec.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsNo") : ""
		};
		//重置消息框的消失隐藏时间
		me.hideTimes = 5000;
		me.fireEvent('onScanCodeShowDtl', me, info);
	},
	/**@description 选择货品批号*/
	onChooseLotNo: function() {
		var me = this;
		var selected = me.getSelectionModel().getSelection();
		if(!selected || selected.length <= 0) return;
		var record = selected[0];
		var lotNo = record.get("ReaBmsInDtlVO_ReaBmsInDtl_LotNo");
		var reaGoodsID = record.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_Id");
		var goodsCName = record.get("ReaBmsInDtlVO_ReaBmsInDtl_GoodsCName");
		var reaGoodsNo = record.get("ReaBmsInDtlVO_ReaBmsInDtl_ReaGoodsNo");
		var maxWidth = 620; //document.body.clientWidth * 0.68;
		var height = document.body.clientHeight * 0.78;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			GoodsID: reaGoodsID,
			ReaGoodsNo: reaGoodsNo,
			GoodsCName: goodsCName,
			CurLotNo: lotNo,
			listeners: {
				accept: function(p, rec) {
					me.IsShowDtlInfo = true;
					if(rec) {
						record.set("ReaBmsInDtlVO_ReaBmsInDtl_LotNo", rec.get("ReaGoodsLot_LotNo"));
						record.set("ReaBmsInDtlVO_ReaBmsInDtl_ProdDate", rec.get("ReaGoodsLot_ProdDate"));
						record.set("ReaBmsInDtlVO_ReaBmsInDtl_InvalidDate", rec.get("ReaGoodsLot_InvalidDate"));
						record.commit();
					}
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.goodslot.CheckGrid', config);
		win.show();
	},
	/**获取是否需要入库确认后条码打印*/
	getIsPrint: function() {
		var me = this;
		var cbIsPrint = me.getComponent("buttonsToolbar").getComponent("cbIsPrint");
		return cbIsPrint.getValue();
	}
});