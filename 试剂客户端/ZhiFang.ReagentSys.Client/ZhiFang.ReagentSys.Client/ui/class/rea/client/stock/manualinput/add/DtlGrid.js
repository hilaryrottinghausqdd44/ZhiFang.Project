/**
 * 客户端库存初始化(手工入库)
 * @author longfc
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.stock.manualinput.add.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',	
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '明细列表',
	
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
	userUIKey: 'stock.manualinput.add.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "库存初始化明细列表",
	
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
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			items: [{
				getClass: function(v, meta, record) {
					meta.tdAttr = 'data-qtip="<b>删除</b>"';
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.deleteOne(rec);
				}
			}]
		}, {
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
			text: '<b style="color:blue;">货品批号</b>',
			width: 85,
			editor: {
				allowBlank: false,
				emptyText: '双击选择批号',
				listeners: {
					render: function(field, eOpts) {
						field.getEl().on('dblclick', function(p, el, e) {
							me.IsShowDtlInfo = false;
							me.onChooseLotNo();
						});
					}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_InvalidDate',
			text: '<b style="color:blue;">有效期至</b>',
			width: 80,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_Price',
			text: '<b style="color:blue;">单价</b>',
			width: 80,
			type: 'float',
			align: 'right',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
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
			text: '<b style="color:blue;">入库数</b>',
			width: 85,
			type: 'float',
			align: 'center',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false,
				listeners: {
					focus: function(field, e, eOpts) {
						me.comSetReadOnlyOfBarCodeType(field, e);
					}
				}
			},
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
			dataIndex: 'ReaBmsInDtlVO_ReaBmsInDtl_InDtlNo',
			sortable: false,
			text: '入库明细单号',
			width: 100,
			hidden: true,
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
				xtype: 'textarea',
				height: 60
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
		me.load(null, true);
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
		}, '-',{
			fieldLabel: '',
			emptyText: '供货商选择',
			name: 'ReaBmsInDoc_CompanyName',
			itemId: 'ReaBmsInDoc_CompanyName',
			xtype: 'uxCheckTrigger',
			width: 160,
			labelWidth: 0,
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			value: me.CompanyName,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					resizable: false,
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType: "0",
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onCompAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			iconCls: 'button-add',
			name: "btnAdd",
			itemId: "btnAdd",
			text: '货品选择',
			tooltip: '货品选择',
			handler: function() {
				me.onAddReaGoods();
			}
		});

		items.push('-',  {
			xtype: 'textfield',
			width: 210,
			style: {
				marginLeft: "5px"
			},
			emptyText: '货品扫码',
			fieldLabel: '',
			name: 'txtScanCode',
			itemId: 'txtScanCode',
			labelAlign: 'right',
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER)
						me.onReaGoodsScanCode(field, e);
				}
			}
		}, '-', {
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
	/**供货商选择*/
	onCompAccept: function(record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var ComName = buttonsToolbar.getComponent('ReaBmsInDoc_CompanyName');

		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
		var orgNo = record ? record.data.value.OrgNo : '';

		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		me.ReaCompanyID = id;
		me.ReaServerCompCode = platformOrgNo;
		me.CompanyName = text;
		me.ReaCompCode = orgNo;

		ComName.setValue(text);
		var objValue = {
			"ReaCompanyID": id,
			"ReaCompCName": text,
			"ReaCompCode": orgNo,
			"ReaServerCompCode": platformOrgNo,
			"PlatformOrgNo": platformOrgNo
		};
		me.setReaComInfo();
		me.fireEvent('reacompcheck', me, objValue);
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
	/**
	 * @description 货品为盒条码时的入库数量输入框的处理
	 * @description 货品为批条码时,在"严格模式"下,也不强制必须货品扫码
	 * */
	comSetReadOnlyOfBarCodeType: function(field, e) {
		var me = this;
		var record = field.ownerCt.editingPlugin.context.record;
		var barCodeMgr = ""+record.get("ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType");
		//如果扫码模式为严格模式,批条码及盒条码需要扫码&&barCodeMgr=="1"
		if(me.CodeScanningMode == "strict"&&barCodeMgr=="1") {
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
	onAddReaGoods: function() {
		var me = this;
		if(me.ReaCompanyID) {
			//获取某一供应商的机构货品关系信息
			var defaultWhere = " reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.OrgType=0 and reagoodsorglink.CenOrg.Id=" + me.ReaCompanyID;
			//'Shell.class.rea.client.confirm.manualinput.GoodsOrgLinkCheck'
			var maxWidth = document.body.clientWidth * 0.98;
			var height = document.body.clientHeight * 0.92;
			JShell.Win.open('Shell.class.rea.client.goodsorglink.supply.choose.App1', {
				resizable: false,
				width: maxWidth,
				height: height,
				defaultWhere: defaultWhere,
				/*左列表默认条件*/
				leftDefaultWhere: defaultWhere,
				/*右列表默认条件*/
				rightDefaultWhere: '',
				listeners: {
					accept: function(p, records) {
						if(records && records.length > 0) me.onReaGoodAccept(records);
						p.close();
					}
				}
			}).show();
		} else {
			JShell.Msg.alert("请选择供货商后再操作", null, 1000);
		}
	},
	onReaGoodAccept: function(records) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var storageID = buttonsToolbar.getComponent('StorageID').getValue();
		var storageName = buttonsToolbar.getComponent('StorageName').getValue();
		for(var i = 0; i < records.length; i++) {
			var record = records[i];
			var addRecord = {
				'ReaBmsInDtlVO_ReaBmsInDtl_Id': "-1",
				"ReaBmsInDtlVO_CurReaBmsInDtlLinkListStr": "",
				"ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType": record.get("ReaGoodsOrgLink_BarCodeType"),
				'ReaBmsInDtlVO_ReaBmsInDtl_GoodsUnit': record.get("ReaGoodsOrgLink_ReaGoods_UnitName"),
				'ReaBmsInDtlVO_ReaBmsInDtl_GoodsCName': record.get("ReaGoodsOrgLink_ReaGoods_CName"),

				'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_EName': record.get("ReaGoodsOrgLink_ReaGoods_EName"),
				'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_SName': record.get("ReaGoodsOrgLink_ReaGoods_SName"),
				'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_Id': record.get("ReaGoodsOrgLink_ReaGoods_Id"),
				'ReaBmsInDtlVO_ReaBmsInDtl_CompGoodsLinkID': record.get("ReaGoodsOrgLink_Id"),
				'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_UnitMemo': record.get("ReaGoodsOrgLink_ReaGoods_UnitMemo"),

				'ReaBmsInDtlVO_ReaBmsInDtl_ProdDate': record.get("ReaGoodsOrgLink_ReaGoods_RegistDate"),
				'ReaBmsInDtlVO_ReaBmsInDtl_InvalidDate': record.get("ReaGoodsOrgLink_ReaGoods_InvalidDate"),
				"ReaBmsInDtlVO_ReaBmsInDtl_Price": record.get("ReaGoodsOrgLink_Price"),
				"ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty": 0,
				'ReaBmsInDtlVO_ReaBmsInDtl_BiddingNo': record.get("ReaGoodsOrgLink_BiddingNo"),

				'ReaBmsInDtlVO_ReaBmsInDtl_LotNo': "", //批号
				'ReaBmsInDtlVO_ReaBmsInDtl_ApproveDocNo': record.get("ReaGoodsOrgLink_ReaGoods_ApproveDocNo"),
				'ReaBmsInDtlVO_ReaBmsInDtl_RegisterInvalidDate': record.get("ReaGoodsOrgLink_ReaGoods_RegistNoInvalidDate"),
				'ReaBmsInDtlVO_ReaBmsInDtl_RegisterNo': record.get("ReaGoodsOrgLink_ReaGoods_RegistNo"),
				'ReaBmsInDtlVO_ReaBmsInDtl_ReaCompanyID': me.ReaCompanyID, //当前选择的供应商

				'ReaBmsInDtlVO_ReaBmsInDtl_ReaServerCompCode': me.ReaServerCompCode,
				'ReaBmsInDtlVO_ReaBmsInDtl_CompanyName': me.CompanyName, //当前选择的供应商
				"ReaBmsInDtlVO_ReaBmsInDtl_ReaCompCode": me.ReaCompCode,
				'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoodsNo': record.get("ReaGoodsOrgLink_ReaGoods_ReaGoodsNo"),
				'ReaBmsInDtlVO_ReaBmsInDtl_ProdGoodsNo': record.get("ReaGoodsOrgLink_ReaGoods_ProdGoodsNo"),
				'ReaBmsInDtlVO_ReaBmsInDtl_CenOrgGoodsNo': record.get("ReaGoodsOrgLink_CenOrgGoodsNo"),
				'ReaBmsInDtlVO_ReaBmsInDtl_GoodsNo': record.get("ReaGoodsOrgLink_ReaGoods_GoodsNo")
			};
			var goodsSort = record.get("ReaGoodsOrgLink_ReaGoods_GoodsSort");
			if(!goodsSort) {
				goodsSort = 0;
			}
			addRecord["ReaBmsInDtlVO_ReaBmsInDtl_GoodsSort"] = goodsSort;
			if(storageID) {
				addRecord["ReaBmsInDtlVO_ReaBmsInDtl_StorageID"] = storageID;
				addRecord["ReaBmsInDtlVO_ReaBmsInDtl_StorageName"] = storageName;
			}
			addRecord=me.addNewOfColdInfo(addRecord);
			me.store.add(addRecord);
		}
	},
	/**@description 货品扫码输入框*/
	gettxtScanCode: function() {
		var me = this;
		var txtScanCode = me.getComponent("buttonsToolbar").getComponent("txtScanCode");
		return txtScanCode;
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
			ReaGoodsNo:reaGoodsNo,
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
	/**@description 货品扫码*/
	onReaGoodsScanCode: function(field, e) {
		var me = this;
		var barCode = field.getValue();
		var indexOf = -1; //条码所在验收明细列表的行索引
		var curRecord = null; //条码所在的行记录
		var curReaGoodsScanCodeList = []; //当前条码为盒条码时的条码明细关系
		var isExec = true;
		var info = "";
		me.store.each(function(rec) {
			indexOf++;
			var barCodeMgr = rec.get("ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType");
			switch(barCodeMgr) {
				case "0": //批条码
					var lotSerial = rec.get("ReaBmsInDtlVO_ReaBmsInDtl_LotSerial");
					if(lotSerial == barCode) {
						curRecord = rec;
						return false;
					}
					break;
				case "1": //盒条码
					curReaGoodsScanCodeList = rec.get("ReaBmsInDtlVO_CurReaBmsInDtlLinkListStr");
					if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);
					if(curReaGoodsScanCodeList) {
						Ext.Array.each(curReaGoodsScanCodeList, function(model) {
							//一维盒条码或二维盒条码
							if(model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
								curRecord = rec;
								return false;
							}
						});
					} else {
						curReaGoodsScanCodeList = [];
					}

					if(curRecord || isExec == false) return false;
					break;
				default:
					break;
			}
		});

		if(curRecord) {
			me.getSelectionModel().select(indexOf);
			var barCodeMgr = curRecord.get("ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType");
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
		} else if(isExec == true) {
			me.onScanCodeUrl(barCode);
		} else {
			JShell.Msg.error(info);
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}
	},
	/***
	 * @description 货品扫码时货品存在明细列表中,条码类型为批条码处理
	 * @param {Object} record
	 */
	onScanCodeBatchBarCodeExist: function(record) {
		var me = this;
		var GoodsQty = record.get('ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		GoodsQty = GoodsQty + 1;
		record.set('ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty', GoodsQty);
		record.commit();
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
		me.onShowDtlInfo(record);
	},
	/***
	 * @description 货品扫码时条码类型为盒条码,货品及条码都存在明细列表的处理*
	 * @param {Object} record 条码所在的行记录
	 * @param {Object} curReaGoodsScanCodeList 当前条码为盒条码时的条码明细关系
	 */
	onScanCodeOfBoxBarCodeExist: function(barCode, record, curReaGoodsScanCodeList) {
		var me = this;
		if(me.CodeScanningMode == "mixing") {
			JShell.Msg.alert("请不要重复扫码!", null, 2000); //扫码模式为严格模式时,
		} else {
			var GoodsQty = record.get('ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty');
			if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
			GoodsQty = GoodsQty + 1;
			record.set('ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty', GoodsQty);
			record.commit();
		}

		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
		me.onShowDtlInfo(record);
	},
	/**
	 * @description 货品扫码,条码不存在明细列表,调用服务处理
	 * @param {Object} barCode
	 */
	onScanCodeUrl: function(barCode) {
		var me = this;
		if(!me.ReaCompanyID) {
			JShell.Msg.error("获取供应商信息失败!请选择供应商后再扫码!");
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}

		var url = (me.scanCodeUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.scanCodeUrl;
		var barCode2=JShell.String.encode(barCode);
		var params = "?reaCompID=" + me.ReaCompanyID + "&serialNo=" + barCode2 + "&scanCodeType=manualinput";
		url = url + params;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value) {
					var barCodeInfo = data.value;
					if(barCodeInfo.BoolFlag == false) {
						JShell.Msg.error(barCodeInfo.ErrorInfo);
						me.gettxtScanCode().setValue("");
						me.gettxtScanCode().focus();
					} else {
						me.onScanCodeUrlAfter(barCodeInfo, barCode);
					}
				} else {
					JShell.Msg.error("货调用货品条码解码规则服务失败,错误信息为:" + data.msg);
					me.gettxtScanCode().setValue("");
					me.gettxtScanCode().focus();
				}
			} else {
				JShell.Msg.error("货调用货品条码解码规则服务失败,错误信息为:" + data.msg);
				me.gettxtScanCode().setValue("");
				me.gettxtScanCode().focus();
			}
		});
	},
	/***
	 * @description 货品扫码调用服务后,获取到条码货品信息后的处理
	 * @param {Object} barCodeInfo
	 * @param {Object} barCode
	 */
	onScanCodeUrlAfter: function(barCodeInfo, barCode) {
		var me = this;
		var reaBarCodeVOList = barCodeInfo.ReaBarCodeVOList;
		if(!reaBarCodeVOList) return;
		if(reaBarCodeVOList.length <= 0) return;

		var callback = function(reaBarCodeVO) {
			if(!reaBarCodeVO) return;
			//先判断该条码的货品是否存在于明细列表中
			var record = null;
			me.store.each(function(rec) {
				var compGoodsLinkID = rec.get("ReaBmsInDtlVO_ReaBmsInDtl_CompGoodsLinkID");
				var lotNo = rec.get("ReaBmsInDtlVO_ReaBmsInDtl_LotNo");
				if(reaBarCodeVO.CompGoodsLinkID == compGoodsLinkID && (!lotNo || reaBarCodeVO.LotNo == lotNo)) {
					record = rec;
					return false;
				}
			});
			//货品存在明细列表中,但条码不存在的明细中
			if(record)
				me.onScanCodeUrlAfterOfBoxAndExistDtl(record, reaBarCodeVO, barCode);
			else
				me.onScanCodeUrlAfterOfBoxAndNotExistDtl(reaBarCodeVO, barCode);
		}
		if(reaBarCodeVOList.length > 1)
			me.onChooseReaBarCodeVO(reaBarCodeVOList, callback);
		else
			callback(reaBarCodeVOList[0]);
	},
	/***
	 * @description 货品扫码调用服务处理后,条码类型为盒条码,货品存在明细列表中,但条码不存在入库明细的条码明细中
	 * @param {Object} record
	 * @param {Object} reaBarCodeVO
	 * @param {Object} barCode
	 */
	onScanCodeUrlAfterOfBoxAndExistDtl: function(record, reaBarCodeVO, barCode) {
		var me = this;
		var curReaGoodsScanCodeList = record.get("ReaBmsInDtlVO_CurReaBmsInDtlLinkListStr");
		if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);
		var indexOf = -1;
		if(curReaGoodsScanCodeList) {
			Ext.Array.each(curReaGoodsScanCodeList, function(model, index) {
				//一维盒条码或二维盒条码
				if(model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
					indexOf = index;
					return false;
				}
			});
		}
		if(indexOf >= 0) return;

		//当前扫码值不存在该货品的记录行里
		if(!curReaGoodsScanCodeList) curReaGoodsScanCodeList = [];

		reaBarCodeVO["BDocID"] = me.PK; //入库单Id
		reaBarCodeVO["BDocNo"] = ""; //入库单号
		reaBarCodeVO["BDtlID"] = record.get('ReaBmsInDtlVO_ReaBmsInDtl_Id');
		var operationVO = me.getBarcodeOperationVO(reaBarCodeVO);
		curReaGoodsScanCodeList.push(operationVO);

		var GoodsQty = record.get('ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		GoodsQty += 1;

		if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.encode(curReaGoodsScanCodeList);
		record.set('ReaBmsInDtlVO_CurReaBmsInDtlLinkListStr', curReaGoodsScanCodeList);
		record.set('ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty', GoodsQty);
		record.commit();

		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
		me.onShowDtlInfo(record);
	},
	/***
	 * @description 货品扫码调用服务处理后,条码类型为盒条码,封装条码操作记录基本信息
	 * @param {Object} reaBarCodeVO 货品条码信息
	 */
	getBarcodeOperationVO: function(reaBarCodeVO) {
		var me = this;
		var docNo = reaBarCodeVO.BDocNo;
		if(!docNo) docNo = "";

		var docID = reaBarCodeVO.BDocID;
		if(!docID) docID = -1;

		var dtlID = reaBarCodeVO.BDtlID;
		if(!dtlID) dtlID = -1;

		var sysPackSerial = reaBarCodeVO.SysPackSerial;
		if(!sysPackSerial) sysPackSerial = "";
		var operationVO = {
			"Id": -1,
			"OperTypeID": "5", //库存初始化
			"BDocNo": docNo,
			"BDocID": docID,
			"BDtlID": dtlID,
			"SysPackSerial": sysPackSerial,
			"OtherPackSerial": reaBarCodeVO.OtherPackSerial,
			"UsePackSerial": reaBarCodeVO.UsePackSerial,
			"LotNo": reaBarCodeVO.LotNo
		};
		if(reaBarCodeVO.GoodsSort) {
			operationVO.GoodsSort = reaBarCodeVO.GoodsSort;
		}
		return operationVO;
	},
	/***
	 * @description 货品扫码调用服务处理后,条码类型为盒条码,货品及条码都不存在入库明细列表中
	 * @param {Object} barCodeInfo
	 * @param {Object} barCode
	 */
	onScanCodeUrlAfterOfBoxAndNotExistDtl: function(reaBarCodeVO, barCode) {
		var me = this;
		var Price = reaBarCodeVO.Price;
		if(Price) Price = parseFloat(Price);
		else Price = 0;

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var storageID = buttonsToolbar.getComponent('StorageID').getValue();
		var storageName = buttonsToolbar.getComponent('StorageName').getValue();

		var curReaGoodsScanCodeList = [];
		reaBarCodeVO["BDocID"] = me.PK; //入库总单Id
		var operationVO = me.getBarcodeOperationVO(reaBarCodeVO);
		curReaGoodsScanCodeList.push(operationVO);

		if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.encode(curReaGoodsScanCodeList);
		if(reaBarCodeVO.InvalidDate) reaBarCodeVO.InvalidDate = JcallShell.Date.toString(reaBarCodeVO.InvalidDate, true);
		if(reaBarCodeVO.RegistDate) reaBarCodeVO.RegistDate = JcallShell.Date.toString(reaBarCodeVO.RegistDate, true);
		if(reaBarCodeVO.RegistNoInvalidDate) reaBarCodeVO.RegistNoInvalidDate = JcallShell.Date.toString(reaBarCodeVO.RegistNoInvalidDate, true);

		var addRecord = {
			'ReaBmsInDtlVO_ReaBmsInDtl_Id': "-1",
			"ReaBmsInDtlVO_CurReaBmsInDtlLinkListStr": curReaGoodsScanCodeList,
			"ReaBmsInDtlVO_ReaBmsInDtl_BarCodeType": reaBarCodeVO.BarCodeType,
			'ReaBmsInDtlVO_ReaBmsInDtl_GoodsUnit': reaBarCodeVO.UnitName,
			'ReaBmsInDtlVO_ReaBmsInDtl_GoodsCName': reaBarCodeVO.CName,

			'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_EName': reaBarCodeVO.EName,
			'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_SName': reaBarCodeVO.SName,
			'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_Id': reaBarCodeVO.ReaGoodsID,
			'ReaBmsInDtlVO_ReaBmsInDtl_CompGoodsLinkID': reaBarCodeVO.CompGoodsLinkID,
			'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoods_UnitMemo': reaBarCodeVO.UnitMemo,

			'ReaBmsInDtlVO_ReaBmsInDtl_ProdDate': null,
			'ReaBmsInDtlVO_ReaBmsInDtl_Price': Price,
			'ReaBmsInDtlVO_ReaBmsInDtl_SumTotal': Price,
			'ReaBmsInDtlVO_ReaBmsInDtl_GoodsQty': 1,
			'ReaBmsInDtlVO_ReaBmsInDtl_BiddingNo': reaBarCodeVO.BiddingNo,

			'ReaBmsInDtlVO_ReaBmsInDtl_LotNo': reaBarCodeVO.LotNo, //批号
			'ReaBmsInDtlVO_ReaBmsInDtl_InvalidDate': reaBarCodeVO.InvalidDate,
			'ReaBmsInDtlVO_ReaBmsInDtl_ApproveDocNo': reaBarCodeVO.ApproveDocNo,
			'ReaBmsInDtlVO_ReaBmsInDtl_RegisterInvalidDate': reaBarCodeVO.RegisterInvalidDate,
			'ReaBmsInDtlVO_ReaBmsInDtl_RegisterNo': reaBarCodeVO.RegisterNo,

			'ReaBmsInDtlVO_ReaBmsInDtl_ReaCompanyID': me.ReaCompanyID, //当前选择的供应商
			'ReaBmsInDtlVO_ReaBmsInDtl_ReaServerCompCode': me.ReaServerCompCode,
			'ReaBmsInDtlVO_ReaBmsInDtl_CompanyName': me.CompanyName, //当前选择的供应商
			"ReaBmsInDtlVO_ReaBmsInDtl_ReaCompCode": me.ReaCompCode,
			'ReaBmsInDtlVO_ReaBmsInDtl_ReaGoodsNo': reaBarCodeVO.ReaGoodsNo,
			'ReaBmsInDtlVO_ReaBmsInDtl_ProdGoodsNo': reaBarCodeVO.ProdGoodsNo,
			'ReaBmsInDtlVO_ReaBmsInDtl_CenOrgGoodsNo': reaBarCodeVO.CenOrgGoodsNo,
			'ReaBmsInDtlVO_ReaBmsInDtl_GoodsNo': reaBarCodeVO.GoodsNo
		};
		if(reaBarCodeVO.GoodsSort) {
			addRecord["ReaBmsInDtlVO_ReaBmsInDtl_GoodsSort"] = reaBarCodeVO.GoodsSort;
		}
		if(storageID) {
			addRecord["ReaBmsInDtlVO_ReaBmsInDtl_StorageID"] = storageID;
			addRecord["ReaBmsInDtlVO_ReaBmsInDtl_StorageName"] = storageName;
		}
		//默认冷链信息
		addRecord=me.addNewOfColdInfo(addRecord);
		me.store.add(addRecord);
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
		me.getSelectionModel().select(me.store.getCount() - 1);
		var records = me.getSelectionModel().getSelection();
		me.onShowDtlInfo(records[0]);
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
						reaBarCodeVO = JcallShell.JSON.decode(record.data.ReaBarCodeVO);
					p.close();
					if(callback) callback(reaBarCodeVO);
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.confirm.choose.reabarcode.CheckGrid', config);
		win.show();
		win.loadData(reaBarCodeVOList);
	},
	/**获取是否需要入库确认后条码打印*/
	getIsPrint: function() {
		var me = this;
		var cbIsPrint = me.getComponent("buttonsToolbar").getComponent("cbIsPrint");
		return cbIsPrint.getValue();
	},
	//默认的冷链信息
	addNewOfColdInfo: function (addRecord) {
		var me = this;
		//厂家出库温度
		addRecord["ReaBmsInDtlVO_ReaBmsInDtl_FactoryOutTemperature"] = "5℃";
		//到货温度
		addRecord["ReaBmsInDtlVO_ReaBmsInDtl_ArrivalTemperature"] = "5℃";
		//外观验收
		addRecord["ReaBmsInDtlVO_ReaBmsInDtl_AppearanceAcceptance"] = "完好";
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