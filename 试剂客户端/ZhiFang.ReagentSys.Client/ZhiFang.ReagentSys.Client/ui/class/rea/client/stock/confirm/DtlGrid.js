/**
 * 验货单明细列表
 * @author liangyl
 * @version 2017-12-07
 */
Ext.define('Shell.class.rea.client.stock.confirm.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '验货单明细列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaDtlConfirmVOOfStoreInByHQL?isPlanish=true&isNoPrefEntity=true',
	/**新增客户端入库及入库明细*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsInDocAndDtl',
	/**扫码服务*/
	scanCodeUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo',
	/**获取库房数据服务路径*/
	selectStorageUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?isPlanish=true',
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
	/**验收单ID*/
	BmsCenSaleDocConfirmID: null,
	/**浮动窗体对象*/
	WinInfo: null,
	/**浮动窗体是否已打开*/
	IsLoadWinInfo: false,
	/**浮动货品窗体对象*/
	WinDtlPanel: null,
	/**浮动窗体是否已打开*/
	IsLoadWinDtlPanel: false,
	/**默认选中*/
	autoSelect: false,
	/**供应商ID*/
	ReaCompID: null,
	/**是否严格模式，严格1,非严格模式’0*/
	CodeScanningMode: '0',
	/**默认每页数量*/
	defaultPageSize: 500,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否显示货品信息(双击批号单元格选择时隐藏)*/
	IsShowDtlInfo: true,
	/**货品明细弹出消息框消失时间*/
	hideTimes: 5000,
	/**当前选择的库房*/
	StorageArr: [],
	/**当前选择的货架*/
	PlaceArr: [],
	/**用户UI配置Key*/
	userUIKey: 'stock.confirm.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "验收入库明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//隐藏货架工具栏
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		buttonsToolbar.hide();
		//验收ID不为空时加载
		if(me.BmsCenSaleDocConfirmID) {
			me.onSearch();
		}
		me.on('select', function(rowModel, record, index, e) {
			if(me.IsShowDtlInfo == true)
				me.onShowDtlInfo(record);
		});

	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.addEvents('onStoreInClick', 'changeSumTotal', 'onScanCodeShowDtl');
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
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.store.remove(rec);
				}
			}]
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType',
			text: '条码类型',
			hidden: true,
			sortable: false,
			width: 60,
			renderer: function(value, meta) {
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName',
			text: '货品名称',
			sortable: false,
			width: 150,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType");
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
		},{
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdOrgName',
			text: '厂家名称',
			sortable: false,
			width: 80,
			//hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_StorageCName',
			width: 100,
			text: '<b style="color:blue;">库房</b>',
			editor: new Ext.form.field.ComboBox({
				mode: 'local',
				editable: false,
				displayField: 'ReaStorage_CName',
				valueField: 'ReaStorage_CName',
				listClass: 'x-combo-list-small',
				store: new Ext.data.Store({
					autoLoad: true,
					fields: ['ReaStorage_Id', 'ReaStorage_CName'],
					data: []
				}),
				listeners: {
					render: function(field, eOpts) {
						field.getEl().on('click', function(p, el, e) {
							var record = field.ownerCt.editingPlugin.context.record;
							var linkList = [];
							var linkListStr = record.get("ReaSaleDtlConfirmVO_ReaStorageList");
							if(linkListStr) {
								linkList = Ext.JSON.decode(linkListStr);
								if(linkList.list) linkList = linkList.list;
							}
							if(linkList) field.store.loadData(linkList);
						});
					},
					select: function(field, records, eOpts) {
						var record = field.ownerCt.editingPlugin.context.record;
						record.set('ReaSaleDtlConfirmVO_StorageCName', records[0].get("ReaStorage_CName"));
						record.set('ReaSaleDtlConfirmVO_StorageID', records[0].get("ReaStorage_Id"));
						//record.commit();
						me.getView().refresh();
					}
				}
			}),
			renderer: function(value, meta, record) {
				//待入库货品的库房存在多个时处理
				var linkListStr = record.get("ReaSaleDtlConfirmVO_ReaStorageList");
				if(linkListStr) {
					var linkList = Ext.JSON.decode(linkListStr);
					if(linkList.list) linkList = linkList.list;
					if(linkList.length > 1) value = '<b style="padding:1px;color:red; border:solid 0px red">*</b>' + value;
				}
				return value;
			}
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_StorageID',
			text: '库房ID',
			sortable: false,
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaStorageList',
			text: '库房试剂信息',
			sortable: false,
			hidden: true,
			width: 100,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_PlaceCName',
			text: '货架',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_PlaceID',
			text: '货架ID',
			sortable: false,
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount',
			text: '验收数量',
			width: 65,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InCount',
			text: '已入库数',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'GoodsQtyCount',
			text: '可入库数',
			sortable: false,
			hidden: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty',
			text: '<b style="color:blue;">本次入库数</b>',
			sortable: false,
			width: 80,
			defaultRenderer: true,
			editor: {
				xtype: 'numberfield',
				allowDecimals: true,
				listeners: {
					focus: function(field, e, eOpts) {
						me.comSetReadOnlyOfBarCodeType(field, e);
					},
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						me.setSumTotal(newValue, records[0]);
					}
				}
			}
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo',
			text: '<b style="color:blue;">货品批号</b>',
			width: 80,
			editor: {
				xtype: 'textareafield',
				allowBlank: false,
				listeners: {
					render: function(field, eOpts) {
						field.getEl().on('dblclick', function(p, el, e) {
							me.onChooseLotNo();
						});
					}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate',
			text: '有效期至',
			width: 80,
			type: 'date',
			sortable: false,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price',
			sortable: false,
			text: '单价',
			width: 70,
			type: 'float',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TotalPrice',
			sortable: false,
			text: '总计金额',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsUnit',
			sortable: false,
			text: '包装单位',
			hidden: false,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_UnitMemo',
			sortable: false,
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TaxRate',
			sortable: false,
			hidden: true,
			text: '税率',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate',
			sortable: false,
			hidden: true,
			text: '生产日期',
			align: 'center',
			width: 90,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BiddingNo',
			sortable: false,
			text: '招标号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ApproveDocNo',
			sortable: false,
			text: 'ApproveDocNo',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterInvalidDate',
			sortable: false,
			type: 'date',
			sortable: false,
			isDate: true,
			text: '注册证有效期',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterNo',
			sortable: false,
			text: '注册证号',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id',
			sortable: false,
			text: '验收单明细id',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSerial',
			sortable: false,
			text: '货品条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_PackSerial',
			sortable: false,
			text: '包装单位条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_MixSerial',
			sortable: false,
			text: '混合条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaCompID',
			sortable: false,
			text: '供应商ID',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaCompanyName',
			sortable: false,
			hidden: true,
			text: '供应商',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr',
			sortable: false,
			hidden: true,
			text: '验货明细条码关系',
			width: 100,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlLinkVOListStr',
			sortable: false,
			text: '供货明细条码关系集合Str',
			hidden: true,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsInDtlLinkListStr',
			sortable: false,
			hidden: true,
			text: '已入库扫码记录集合',
			width: 100,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList',
			sortable: false,
			hidden: true,
			text: '当次扫码记录集合',
			width: 100,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType',
			sortable: false,
			hidden: true,
			text: '是否是盒条码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID',
			text: '货品ID',
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty',
			text: '本次入库数量',
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CompGoodsLinkID',
			sortable: false,
			text: '货品机构关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDocConfirmNo',
			sortable: false,
			text: '验收单号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo',
			sortable: false,
			text: '货品编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaServerCompCode',
			text: '供应商机平台构码',
			sortable: false,
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsNo',
			text: '不能货品编码',
			sortable: false,
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: false,
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'AddTag',
			text: '新增标记',
			sortable: false,
			width: 80,
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotQRCode',
			text: '二维批条码',
			sortable: false,
			width: 80,
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaCompCode',
			text: '供货方编码',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSort',
			text: '货品序号',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_FactoryOutTemperature',
			text: '厂家出库温度',
			editor: {
				readOnly: false
			},
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ArrivalTemperature',
			text: '到货温度',
			width: 80,
			editor: {
				readOnly: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AppearanceAcceptance',
			text: '外观验收',
			width: 80,
			editor: {
				readOnly: false
			},
			defaultRenderer: true
		}];

		return columns;
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
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: []
		};
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];

		buttonToolbarItems.push('refresh', '-', {
			name: 'txtScanCode',
			itemId: 'txtScanCode',
			emptyText: '条码号扫码',
			labelSeparator: '',
			labelWidth: 0,
			width: 135,
			hidden: false,
			labelAlign: 'right',
			xtype: 'textfield',
			enableKeyEvents: true,
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						if(!field.getValue()) {
							var info = "请输入条码号!";
							JShell.Msg.alert(info, null, 2000);
							return;
						}
						me.onScanCode(field, e);
					}
				}
			}
		}, '-', {
			fieldLabel: '库房',
			name: 'StorageID',
			itemId: 'StorageID',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			labelWidth: 35,
			width: 165,
			labelAlign: 'right',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.changeLoadPlace(newValue, com.getRawValue());
				}
			}
		}, {
			text:'<font color="red"  size="3px" >入库</font>',
			tooltip: '入库',
			iconCls: 'button-save',
			handler: function() {
				me.fireEvent('onStoreInClick', me);
			}
		}, '-', {
			xtype: 'checkboxfield',
			boxLabel: '是否显示浮动窗',
			checked: true,
			margin: '0 0 0 5',
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
		return buttonToolbarItems;
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		//改变默认条件
		me.defaultWhere = 'reabmscensaledtlconfirm.Status in(1,2) and reabmscensaledtlconfirm.AcceptCount>0';

		me.internalWhere = '';
		//验收单ID
		if(me.BmsCenSaleDocConfirmID) {
			me.internalWhere = "reabmscensaledtlconfirm.SaleDocConfirmID='" + me.BmsCenSaleDocConfirmID + "'";
		}
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getLoadUrlFields(true);

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
	/**创建数据字段*/
	getLoadUrlFields: function(isString) {
		var me = this;
		var fields = me.getStoreFields(true).join(',');
		fields = fields.replace("GoodsQtyCount,", "");
		fields = fields.replace("AddTag,", "");
		//解决请求URL查询字符串长度过长
		fields = fields.replace(/ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_/g, "DtlFieldsVO_");
		return fields;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);

		//统计总计金额
		if(Number(me.CodeScanningMode) != 1) {
			me.setRecordTotal();
		}
		if(records && records.length > 0) {
			me.getSelectionModel().selectAll();
		}
		var StorageData = [];
		me.getStorageInfo(function(data) {
			if(data && data.value) {
				StorageData = data.value.list;
			}
		});
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var storageName = buttonsToolbar.getComponent('StorageID');
		storageName.loadData(me.getStatusData(StorageData));
		if(StorageData.length > 0) storageName.setValue(StorageData[0].ReaStorage_Id);
	},
	//混合模式下，合计金额和统计金额赋值
	setRecordTotal: function() {
		var me = this;
		var records = me.store.data.items,
			len = records.length;
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var GoodsQty = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty');
			me.setSumTotal(GoodsQty, rec);
		}
		me.getSumTotal();
	},
	/**
	 * @description 货品为盒条码时的入库数量输入框的处理
	 * @description 货品为批条码时,在"严格模式"下,也不强制必须货品扫码
	 * */
	comSetReadOnlyOfBarCodeType: function(field, e) {
		var me = this;
		var record = field.ownerCt.editingPlugin.context.record;
		var barCodeMgr = "" + record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType");
		//如果扫码模式为严格模式,批条码及盒条码需要扫码&&barCodeMgr=="1"
		if(me.CodeScanningMode == "1" && barCodeMgr == "1") {
			field.setReadOnly(true);
			//return;
		} else {
			field.setReadOnly(false);
		}
	},
	/***
	 *对选择行只设置库房
	 * */
	onlyStorage: function(storageID, storageCName) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) return;

		var len = records.length;
		for(var i = 0; i < len; i++) {
			//在更新入库货品的指定库房时,需要判断指定的库房是否存在于入库货品的库房试剂关系里,不存在时不更新该入库货品的库房
			var linkList = [];
			var isHash = false;
			var linkListStr = records[i].get("ReaSaleDtlConfirmVO_ReaStorageList");
			if(linkListStr) {
				linkList = Ext.JSON.decode(linkListStr);
				if(linkList.list) linkList = linkList.list;
			}
			for(var j = 0; j < linkList.length; j++) {
				if(linkList[j]["ReaStorage_Id"] == storageID) {
					isHash = true;
					break;
				}
			}
			if(isHash != true) continue;

			records[i].set('ReaSaleDtlConfirmVO_StorageCName', storageCName);
			records[i].set('ReaSaleDtlConfirmVO_StorageID', storageID);
			records[i].set('ReaSaleDtlConfirmVO_PlaceID', '');
			records[i].set('ReaSaleDtlConfirmVO_PlaceCName', '');
			records[i].set('AddTag', '1');
		}
		me.getSelectionModel().deselectAll();
	},
	/**选中库房加载货架*/
	changeLoadPlace: function(StorageID, StorageCName) {
		var me = this;
		me.hideMask();
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		buttonsToolbar.removeAll();
		if(!StorageID) {
			me.NOPlaceTip(buttonsToolbar);
			me.onlyStorage(StorageID, StorageCName);
			buttonsToolbar.hide();
		} else {
			var arr = [];
			me.getPlaceById(StorageID, function(data) {
				if(data && data.value) {
					if(data.value.list.length == 0) {
						me.NOPlaceTip(buttonsToolbar);
					}
					for(var i = 0; i < data.value.list.length; i++) {
						var PlaceCName = data.value.list[i].ReaPlace_CName;
						var PlaceID = data.value.list[i].ReaPlace_Id;
						var btn = {
							xtype: 'button',
							itemId: 'btn' + i,
							text: PlaceCName,
							tooltip: PlaceCName,
							enableToggle: false,
							StorageCName: StorageCName,
							StorageID: StorageID,
							PlaceID: PlaceID,
							PlaceCName: PlaceCName
						};
						buttonsToolbar.add(btn, '-');
					}
					if(data.value.list.length == 1) { //只有一行默认选中库房货架
						var com = {
							StorageCName: StorageCName,
							StorageID: StorageID,
							PlaceID: PlaceID,
							PlaceCName: PlaceCName
						};
						me.setRecStoragePlace(com);
					}
				} else {
					me.NOPlaceTip(buttonsToolbar);
					me.onlyStorage(StorageID, StorageCName);
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
						}
					});
				}
			}
		}
	},
	/**
	 *不选中的按钮清空选中状态     */
	cleartogglebuttonsToolbar: function(buttonsToolbar, com) {
		for(var i = 0; i < buttonsToolbar.items.length; i++) {
			if(buttonsToolbar.items.items[i].itemId) {
				if(com.itemId != buttonsToolbar.items.items[i].itemId) {
					buttonsToolbar.items.items[i].toggle(false);
				}
			}
		}
	},
	/**勾选货品赋值*/
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
		me.StorageArr = [];
		me.PlaceArr = [];
		me.StorageArr.push({
			StorageID: storageID,
			StorageCName: storageCName
		});
		me.PlaceArr.push({
			PlaceID: placeID,
			PlaceCName: placeCName
		});

		for(var i = 0; i < len; i++) {
			//在设置货架时,需要判断当前货架的所属库房是否等于入库货品指定的库房
			var linkList = [];
			var isHash = false;
			var linkListStr = records[i].get("ReaSaleDtlConfirmVO_ReaStorageList");
			if(linkListStr) {
				linkList = Ext.JSON.decode(linkListStr);
				if(linkList.list) linkList = linkList.list;
			}
			for(var j = 0; j < linkList.length; j++) {
				if(linkList[j]["ReaStorage_Id"] == storageID) {
					isHash = true;
					break;
				}
			}
			if(isHash != true) continue;

			records[i].set('ReaSaleDtlConfirmVO_StorageCName', storageCName);
			records[i].set('ReaSaleDtlConfirmVO_StorageID', storageID);
			records[i].set('ReaSaleDtlConfirmVO_PlaceID', placeID);
			records[i].set('ReaSaleDtlConfirmVO_PlaceCName', placeCName);
			records[i].set('AddTag', '1');
		}
		me.getSelectionModel().deselectAll();
	},
	/**没有货架提示*/
	NOPlaceTip: function(buttonsToolbar) {
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
	/**禁用所有的操作功能*/
	disableControl: function() {},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this,
			result = {},
			list = [],
			arr = [];

		for(var i = 0; i < data.list.length; i++) {
			//添加可入库数量
			var AcceptCount = data.list[i].ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount;
			var InCount = data.list[i].ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InCount;
			if(!InCount) InCount = 0;
			if(!AcceptCount) AcceptCount = 0;
			var GoodsQtyCount = Number(AcceptCount) - Number(InCount);

			var obj1 = {
				GoodsQtyCount: GoodsQtyCount,
				ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TotalPrice: '0'
			};
			//混合模式本次入库数量默认等于可入库数量
			if(Number(me.CodeScanningMode) != 1) {
				obj1.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty = GoodsQtyCount;
			} else
				obj1.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty = 0;
			var obj2 = Ext.Object.merge(data.list[i], obj1);
			arr.push(obj2);
		}
		result.list = arr;
		return data;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		if(!me.BmsCenSaleDocConfirmID) {
			var msg = me.msgFormat.replace(/{msg}/, '验收单Id为空!');
			me.getView().update(msg);
			return false;
		}
		me.load(null, true, autoSelect);
	},
	/**货品扫码*/
	onScanCode: function(field, e) {
		var me = this;
		var barCode = field.getValue();
		var indexOf = -1; //条码所在验收明细列表的行索引
		var curRecord = null; //条码所在的行记录
		var dtlConfirmLinkList = null; //当前条码为盒条码时的条码明细关系
		var LinkListList = null; //当前已入库的盒条码
		var num = 0;
		var isScanCode = true; //是否需要调用服务
		me.store.each(function(rec) {
			indexOf = indexOf + 1;
			var barCodeMgr = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType");
			var LotSerial = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotSerial");
			var LotQRCode = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotQRCode");
			switch(barCodeMgr) {
				case "0": //批条码
					if(LotSerial == barCode) {
						curRecord = rec;
						num = indexOf;
						return false;
					}
					break;
				case "1":
					//根据当次扫码记录集合，判断是否已扫码
					var scanCodeList = [];
					var ScanCodeListStr = rec.get("ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList");
					if(ScanCodeListStr) scanCodeList = Ext.JSON.decode(ScanCodeListStr);
					var indexOfSerial = 0;
					if(scanCodeList) {
						Ext.Array.each(scanCodeList, function(model) {
							//一维盒条码或二维盒条码
							if(model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
								indexOfSerial = 1;
								return false;
							}
						});
					}
					//当次扫码记录集合已存在数据，校验是否已扫码
					if(indexOfSerial == 1) {
						isScanCode = false;
						var info = "条码为:" + barCode + "已扫码,请不要重复扫码!";
						JShell.Msg.alert(info, null, 2000);
						return;
					}
					//已入库盒条码明细
					var LinkListStr = rec.get('ReaSaleDtlConfirmVO_ReaBmsInDtlLinkListStr');
					if(LinkListStr) LinkListList = Ext.JSON.decode(LinkListStr);
					var fag = false
					//如果当前扫码的条码已入库,直接提示并返回	
					fag = me.getLinkListListSerial(LinkListList, barCode);
					if(fag) {
						isScanCode = false;
						return;
					}
					//从验货明细条码关系里找
					var dtlConfirmLinkStr = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr");
					if(dtlConfirmLinkStr) dtlConfirmLinkList = Ext.JSON.decode(dtlConfirmLinkStr);
					var serialNo = "";
					if(dtlConfirmLinkList) {
						Ext.Array.each(dtlConfirmLinkList, function(model) {
							if(model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
								if(model["OperTypeID"] == '3') {
									var info = "该条码已被拒收,不能入库!";
									JShell.Msg.alert(info, null, 2000);
								}
								if(num != indexOf) {
									//选中行号为num的数据行
									if(indexOf >= 0) {
										me.getSelectionModel().select(indexOf);
										rec = me.getStore().getAt(indexOf);
										curRecord = rec;
									}
								}
								num = indexOf;
								return false;
							}
						});
					}
					var isExec = true;
					//从供货明细条码关系里找
					var reaBmsCenSaleDtlLinkVOList = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlLinkVOListStr");
					if(reaBmsCenSaleDtlLinkVOList) reaBmsCenSaleDtlLinkVOList = JcallShell.JSON.decode(reaBmsCenSaleDtlLinkVOList);
					Ext.Array.each(reaBmsCenSaleDtlLinkVOList, function(model) {
						if(model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
							//记录当次扫码操作
							var obj = {
								SysPackSerial: !model.SysPackSerial ? '' : model.SysPackSerial,
								OtherPackSerial: !model.OtherPackSerial ? '' : model.OtherPackSerial,
								UsePackSerial: !model.UsePackSerial ? '' : model.UsePackSerial,
								UsePackQRCode: !model.UsePackQRCode ? '' : model.UsePackQRCode
							};
							me.getCurReaGoodsScanCode(obj, rec, barCode);
							isExec = false;
							return false;
						}
					});
					if(serialNo == barCode && isExec) {
						curRecord = rec;
						num = indexOf;
						return false;
					}
					break;
				default:
					break;
			}
		});
		if(curRecord) {
			//从验货明细条码关系里找
			var dtlConfirmLinkStr = curRecord.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr");
			if(dtlConfirmLinkStr) dtlConfirmLinkList = Ext.JSON.decode(dtlConfirmLinkStr);
			//已入库盒条码明细
			var LinkListStr = curRecord.get('ReaSaleDtlConfirmVO_ReaBmsInDtlLinkListStr');
			if(LinkListStr) LinkListList = Ext.JSON.decode(LinkListStr);
			indexOf = -1;
			var barCodeMgr = curRecord.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType");
			switch(barCodeMgr) {
				case "0": //批条码
					me.onBatchBarCode(curRecord, num);
					break;
				case "1": //盒条码
					me.onBoxBarCode(barCode, curRecord, dtlConfirmLinkList, num, LinkListList);
					break;
				default:
					me.onBatchBarCode(curRecord, num);
					break;
			}
		}
		if(isScanCode && !curRecord) {
			me.onScanCodeInfo(barCode);
		}
	},
	/***
	 * @description 货品扫码时货品存在,条码类型为批条码处理
	 * @param {Object} record
	 */
	onBatchBarCode: function(record, num) {
		var me = this;
		//本次入库数量<可入库数量 ，可入库数量+1
		//本次入库数量
		var GoodsQty = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty');
		var GoodsQtyCount = record.get('GoodsQtyCount');
		if(!GoodsQty) GoodsQty = 0;
		if(!GoodsQtyCount) GoodsQtyCount = 0;

		if(Number(GoodsQty) < Number(GoodsQtyCount)) {
			if(GoodsQty) GoodsQty = Number(GoodsQty);
			GoodsQty = GoodsQty + 1;
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty', GoodsQty);
			me.setSumTotal(GoodsQty, record);
		}
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
		me.getSelectionModel().select(num);
		me.onShowDtlInfo(record);
	},
	getLinkListListSerial: function(LinkListList, barCode) {
		var me = this;
		var isExect = false;
		//如果当前扫码的条码已入库,直接提示并返回	
		Ext.Array.each(LinkListList, function(model, index) {
			if(model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
				me.gettxtScanCode().setValue("");
				me.gettxtScanCode().focus();
				isExect = true;
				var info = "条码为:" + barCode + "已入库,请不要重复扫码!";
				JShell.Msg.alert(info, null, 2000);
				return false;
			}
		});
		return isExect;
	},
	/***
	 * 根据本次入库数量计算总计金额
	 */
	setSumTotal: function(GoodsQty, record) {
		var me = this;
		var Price = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price');
		if(!Price) Price = 0;
		var SumTotal = Number(GoodsQty) * Number(Price);
		record.set('AddTag', '1');
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TotalPrice', SumTotal);
		me.fireEvent('changeSumTotal', record, SumTotal);
	},
	/***
	 * 获取本次入库明细的总计金额
	 */
	getSumTotal: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var SumTotal = 0;
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var total = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TotalPrice');
			if(!total) total = 0;
			SumTotal += Number(total);
		}
		return SumTotal;
	},
	/***
	 * @description 货品扫码时货品存在,条码类型为盒条码处理*
	 * @param {Object} record 条码所在的行记录
	 * @param {Object} dtlConfirmLinkList 当前条码为盒条码时的条码明细关系
	 */
	onBoxBarCode: function(barCode, record, dtlConfirmLinkList, num, LinkListList) {
		var me = this;
		var indexOf = -1;
		var fag = false;
		if(dtlConfirmLinkList) {
			var serialNo = "";
			Ext.Array.each(dtlConfirmLinkList, function(model, index) {
				if(model["SysPackSerial"]) {
					serialNo = model["SysPackSerial"];
				}
				if(model["UsePackSerial"]) {
					serialNo = model["UsePackSerial"];
				}
				if(model["UsePackQRCode"]) {
					serialNo = model["UsePackQRCode"];
				}
				if(serialNo == barCode) {
					indexOf = index;
					var obj = {
						SysPackSerial: !model["SysPackSerial"] ? '' : model["SysPackSerial"],
						OtherPackSerial: !model["OtherPackSerial"] ? '' : model["OtherPackSerial"],
						UsePackSerial: model["UsePackSerial"],
						UsePackQRCode: model["UsePackQRCode"]
					};
					me.getCurReaGoodsScanCode(obj, record, barCode);
					return false;
				}
			});
		}
		if(indexOf < 0) {
			JShell.Msg.alert("没有找到该条码信息!", null, 2000);
			me.getSelectionModel().select(num);
			return;
		}
		if(indexOf > -1) {
			//本次入库数量<可入库数量 ，可入库数量+1
			//本次入库数量
			var GoodsQty = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty');
			var GoodsQtyCount = record.get('GoodsQtyCount');
			if(!GoodsQty) GoodsQty = 0;
			if(!GoodsQtyCount) GoodsQtyCount = 0;

			if(Number(GoodsQty) < Number(GoodsQtyCount)) {
				if(GoodsQty) GoodsQty = Number(GoodsQty);
				GoodsQty = GoodsQty + 1;
				record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty', GoodsQty);
				me.setSumTotal(GoodsQty, record);
			}
			var dtlConfirmLinkStr = "";
			if(dtlConfirmLinkList) dtlConfirmLinkStr = Ext.JSON.encode(dtlConfirmLinkList);
			//			me.setSumTotal(GoodsQty,record);
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			me.getSelectionModel().select(num);
			me.onShowDtlInfo(record);
		}
	},
	/**
	 * 调用服务扫码
	 */
	onScanCodeInfo: function(barCode) {
		var me = this;
		me.getSelectionModel().deselectAll();
		//调用服务
		me.onScanCodeUrl(barCode);
	},
	//获取当次扫码信息
	getCurReaGoodsScanCode: function(obj, record, barCode) {
		var me = this;
		var CodeArr = [];
		CodeArr = record.get('ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList') ? record.get('ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList') : [];
		if(CodeArr.length > 1) {
			CodeArr = Ext.JSON.decode(CodeArr);
			for(var i = 0; i < CodeArr.length; i++) {
				if(CodeArr[i].UsePackSerial != barCode || CodeArr[i].UsePackQRCode != barCode) {
					CodeArr.push(obj);
				}
			}
		} else {
			CodeArr.push(obj);
		}
		var Arr = Ext.encode(CodeArr);
		record.set('ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList', Arr);
	},
	/**
	 * 校验本次入库数量不能大于可入库数量
	 */
	GoodsQtyCheck: function(curRecord) {
		var me = this;
		var isExect = true;
		//本次入库数量
		var DefalutGoodsQty = curRecord.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty');
		//可入库数量
		var GoodsQtyCount = curRecord.get('GoodsQtyCount');
		//货品名称 
		var ReaGoodsName = curRecord.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName');
		//货品批号
		var LotNo = curRecord.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo');

		if(DefalutGoodsQty) DefalutGoodsQty = Number(DefalutGoodsQty);
		if(GoodsQtyCount) GoodsQtyCount = Number(GoodsQtyCount);
		if(DefalutGoodsQty > GoodsQtyCount || DefalutGoodsQty == GoodsQtyCount) {
			isExect = false;
			curRecord.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty', GoodsQtyCount);
			JShell.Msg.alert('货品名称：【' + ReaGoodsName + '】,批号:【' + LotNo + '】的本次入库数量不能大于可入库数量', null, 2000);
		}
		return isExect;
	},
	/**
	 * @description 货品扫码,条码不存在验收明细中,调用服务处理
	 * @param {Object} barCode
	 */
	onScanCodeUrl: function(barCode) {
		var me = this;
		var url = (me.scanCodeUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.scanCodeUrl;
		var barCode2=JShell.String.encode(barCode);
		var params = "?reaCompID=" + me.ReaCompID + "&serialNo=" + barCode2 + "&docConfirmID=" + me.BmsCenSaleDocConfirmID;
		
		url = url + params;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value) {
					var info = data.value;
					if(info.BoolFlag == false) {
						JShell.Msg.error(info.ErrorInfo);
					} else {
						me.onScanCodeUrlAfter(info, barCode);
					}
				} else {
					JShell.Msg.error("货品扫码调用条码规则解码失败!" + data.msg);
				}
			} else {
				JShell.Msg.error("获取条码信息出错:" + data.msg);
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
		if(reaBarCodeVOList.length <= 0) return;

		var callback = function(reaBarCodeVO) {
			if(!reaBarCodeVO) return;
			//先判断该条码的货品是否存在于验收明细列表中
			var record = null;
			me.store.each(function(rec) {
				var compGoodsLinkID = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CompGoodsLinkID");
				if(reaBarCodeVO.CompGoodsLinkID == compGoodsLinkID) {
					record = rec;
					return false;
				}
			});
			//货品存在验收明细中,但条码不存在验收的条码明细中
			if(record) {
				var isConduct = true;
				//从供货明细条码关系里找
				var reaBmsCenSaleDtlLinkVOList = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlLinkVOListStr");
				if(reaBmsCenSaleDtlLinkVOList) reaBmsCenSaleDtlLinkVOList = JcallShell.JSON.decode(reaBmsCenSaleDtlLinkVOList);
				Ext.Array.each(reaBmsCenSaleDtlLinkVOList, function(model) {
					//使用盒条码或系统内部盒条码
					//					if(model["UsePackSerial"] == barCode || model["SysPackSerial"] == barCode) {
					if(model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
						//记录当次扫码操作
						var obj = {
							SysPackSerial: !model.SysPackSerial ? '' : model.SysPackSerial,
							OtherPackSerial: !model.OtherPackSerial ? '' : model.OtherPackSerial,
							UsePackSerial: model.UsePackSerial,
							UsePackQRCode: model.UsePackQRCode
						};
						me.getCurReaGoodsScanCode(obj, record, barCode);
						isConduct = false;
						return false;
					}
				});
				if(!isConduct) return;
				me.onScanCodeUrlAfterOfBoxAndExistDtl(record, reaBarCodeVO, barCode);
			} else {
				JShell.Msg.error('没有找到该条码信息');
				return false;
			}
		}
		callback(reaBarCodeVOList[0]);
	},
	/***
	 * @description 货品扫码调用服务处理后,条码类型为盒条码,货品存在验收明细中,但条码不存在验收的条码明细中
	 * @param {Object} record
	 * @param {Object} reaBarCodeVO
	 * @param {Object} barCode
	 */
	onScanCodeUrlAfterOfBoxAndExistDtl: function(record, reaBarCodeVO, barCode) {
		var me = this;
		var curReaGoodsScanCodeList = [];
		var curReaGoodsScanCodeStr = record.get("ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList");
		if(curReaGoodsScanCodeStr) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeStr);

		var indexOf = -1;
		if(curReaGoodsScanCodeList.length > 0) {
			//记录当次扫码操作
			var obj = {
				SysPackSerial: !reaBarCodeVO.SysPackSerial ? '' : reaBarCodeVO.SysPackSerial,
				OtherPackSerial: !reaBarCodeVO.OtherPackSerial ? '' : reaBarCodeVO.OtherPackSerial,
				UsePackSerial: reaBarCodeVO.UsePackSerial,
				UsePackQRCode: reaBarCodeVO.UsePackQRCode
			};
			curReaGoodsScanCodeList.push(obj);
			var Arr = Ext.encode(curReaGoodsScanCodeList);
			record.set('ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList', Arr);
		} else {
			//记录当次扫码操作
			var obj = {
				SysPackSerial: !reaBarCodeVO.SysPackSerial ? '' : reaBarCodeVO.SysPackSerial,
				OtherPackSerial: !reaBarCodeVO.OtherPackSerial ? '' : reaBarCodeVO.OtherPackSerial,
				UsePackSerial: reaBarCodeVO.UsePackSerial,
				UsePackQRCode: reaBarCodeVO.UsePackQRCode
			};
			me.getCurReaGoodsScanCode(obj, record, barCode);
		}

		if(indexOf >= 0) return;
		//本次入库数量<可入库数量 ，可入库数量+1
		//本次入库数量
		var GoodsQty = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty');
		var GoodsQtyCount = record.get('GoodsQtyCount');
		if(!GoodsQty) GoodsQty = 0;
		if(!GoodsQtyCount) GoodsQtyCount = 0;

		if(Number(GoodsQty) < Number(GoodsQtyCount)) {
			if(GoodsQty) GoodsQty = Number(GoodsQty);
			GoodsQty = GoodsQty + 1;
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty', GoodsQty);
			me.setSumTotal(GoodsQty, record);
		}
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
		var num = me.store.indexOf(record);
		if(num > -1) me.getSelectionModel().select(num);
		me.onShowDtlInfo(record);
	},
	/**@description 货品扫码输入框*/
	gettxtScanCode: function() {
		var me = this;
		var txtScanCode = me.getComponent("buttonsToolbar").getComponent("txtScanCode");
		return txtScanCode;
	},
	/**验货明细入库校验*/
	isVerification: function() {
		var me = this,
			records = me.store.data.items,
			isExect = true,
			len = records.length;
		if(len == 0) return;
		var isAddTag = false;
		var msg = '';
		if(len == 0) {
			msg = '入库明细,不能为空';
			isExect = false;
		}
		//验证
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var AddTag = rec.get('AddTag');
			if(AddTag == '1') {
				isAddTag = true;
				var DefalutGoodsQty = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty');
				var GoodsQtyCount = rec.get('GoodsQtyCount');
				var BarCodeMgr = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType');
				var StorageID = rec.get('ReaSaleDtlConfirmVO_StorageID');
				var ReaGoodsName = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName');
				var ScanCodeList = rec.get('ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList');
				var LotNo = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo');
				if(!DefalutGoodsQty) DefalutGoodsQty = 0;
				if(!GoodsQtyCount) GoodsQtyCount = 0;

				var scanCodeList = [];
				var ScanCodeListStr = rec.get("ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList");
				if(ScanCodeListStr) scanCodeList = Ext.JSON.decode(ScanCodeListStr);
				var scanlen = scanCodeList.length;

				//库房不能为空
				if(!StorageID) {
					msg += '入库明细,货品名称:【' + ReaGoodsName + '】的库房不能为空<br>';
					isExect = false;
				}
				//批号不能为空
				if(!LotNo) {
					msg += '入库明细,货品名称:【' + ReaGoodsName + '】的货品批号不能为空<br>';
					isExect = false;
				}
				//本次入库数量+已入库数量不能大于验收数量
				if(Number(DefalutGoodsQty) > Number(GoodsQtyCount)) {
					msg += '入库明细,货品名称:【' + ReaGoodsName + '】的本次入库数量不能大于可入库数量<br>';
					isExect = false;
				}
				//本次入库数量0
				if(Number(DefalutGoodsQty) == 0) {
					msg += '入库明细,货品名称:【' + ReaGoodsName + '】的本次入库数量不能为0<br>';
					isExect = false;
				}
				//盒条码入库明细条码号不能为空(严格模式)
				if(BarCodeMgr == '1' && Number(me.CodeScanningMode) == 1 && !ScanCodeList) {
					msg += '入库明细,货品名称:【' + ReaGoodsName + '】的盒条码号还未扫码<br>';
					isExect = false;
				}
				if(scanlen > 0) {
					//本次扫码记录数不能大于可入库数
					if(DefalutGoodsQty < scanlen) {
						msg += '入库明细,货品名称:【' + ReaGoodsName + '】的本次扫码入库数大于可入库数量<br>';
						isExect = false;
					}
				}
			}
		}
		if(!isAddTag) {
			msg += '没有变更，不需要保存!';
			isExect = false;
		}
		if(!isExect) {
			JShell.Msg.error(msg);
		}
		return isExect;
	},
	/**取到验货单明细Id*/
	getDtlConfirmID: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var strId = '';
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var AddTag = rec.get('AddTag');
			if(AddTag == '1') {
				var BmsCenSaleDtlConfirmId = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id');
				if(i > 0) {
					strId += ",";
				}
				strId += BmsCenSaleDtlConfirmId;
			}
		}
		return strId;
	},
	/**验收明细关系条码*/
	getDtlConfirmLinkList: function(rec) {
		var me = this;
		var arr = [],
			dtlConfirmLinkList = [];
		var dtlConfirmLinkStr = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr");
		if(dtlConfirmLinkStr) dtlConfirmLinkList = Ext.JSON.decode(dtlConfirmLinkStr);
		if(dtlConfirmLinkList) {
			var serialNo = "";
			Ext.Array.each(dtlConfirmLinkList, function(model, index) {
				var obj = {};
				if(model["OperTypeID"] == '2') {
					if(model["SysPackSerial"]) {
						serialNo = model["SysPackSerial"];
					}
					if(model["UsePackSerial"]) {
						serialNo = model["UsePackSerial"];
					}
					if(model["UsePackQRCode"]) {
						serialNo = model["UsePackQRCode"];
					}
					obj.SysPackSerial = serialNo;
					obj.OtherPackSerial = serialNo;
					obj.UsePackSerial = serialNo;
					obj.UsePackQRCode = serialNo;
				}
				if(obj.SysPackSerial) arr.push(obj);
			});
		}
		if(arr.length > 0) arr = Ext.encode(arr);
		return arr;
	},
	/**获取入库明细信息*/
	getReaBmsInDtl: function(bmsindoc) {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		//入库明细
		var SaleDtlConfirmArr = [];
		var ReaBmsInDtlLink = [];
		var dtAddList = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var AddTag = rec.get('AddTag');
			if(AddTag == '1') {
				var StorageCName = rec.get('ReaSaleDtlConfirmVO_StorageCName');
				var StorageID = rec.get('ReaSaleDtlConfirmVO_StorageID');
				var PlaceCName = rec.get('ReaSaleDtlConfirmVO_PlaceCName');
				var PlaceID = rec.get('ReaSaleDtlConfirmVO_PlaceID');
				var GoodsQty = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_DefalutGoodsQty');
				var Price = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price');
				var GoodsUnit = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsUnit');
				var SumTotal = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TotalPrice');
				if(!SumTotal) SumTotal = 0;
				var LotNo = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo');
				var TaxRate = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TaxRate');
				if(!TaxRate) TaxRate = 0;
				TaxRate = Number(TaxRate);
				var GoodsName = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName');
				var GoodsID = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID');
				var BarCodeMgr = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType');
				var BmsCenSaleDtlConfirmId = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id');
				var LotSerial = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotSerial');
				var ReaCompanyName = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaCompanyName');
				var ReaCompID = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaCompID');
				var ReaServerCompCode = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaServerCompCode');
				var GoodsNo = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsNo');
				var CompGoodsLinkID = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CompGoodsLinkID');
				//有效期	           
				var InvalidDate = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate');
				//生产日期           
				var ProdDate = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate');
				var BiddingNo = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BiddingNo');
				var ApproveDocNo = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ApproveDocNo');
				var RegisterNo = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterNo');
				var RegisterInvalidDate = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterInvalidDate');
				var BarCodeType = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType');
				var ProdGoodsNo = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdGoodsNo');
				var CenOrgGoodsNo = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CenOrgGoodsNo');
				var ReaGoodsNo = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo');
				var LotQRCode = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotQRCode');
				var ReaCompCode = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaCompCode');
				var GoodsSort = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSort');
				var obj = {
					Id: -1,
					InDocNo: bmsindoc.InDocNo,
					SaleDtlConfirmID: BmsCenSaleDtlConfirmId,
					GoodsCName: GoodsName,
					GoodsUnit: GoodsUnit,
					GoodsQty: GoodsQty,
					Price: Price,
					SumTotal: Number(SumTotal),
					TaxRate: TaxRate,
					StorageID: StorageID,
					StorageName: StorageCName,
					ReaCompanyID: ReaCompID,
					CompanyName: ReaCompanyName,
					ReaServerCompCode: ReaServerCompCode,
					LotNo: LotNo,
					GoodsNo: GoodsNo,
					ProdGoodsNo: ProdGoodsNo,
					CompGoodsLinkID: CompGoodsLinkID,
					LotSerial: LotSerial,
					BiddingNo: BiddingNo,
					ApproveDocNo: ApproveDocNo,
					RegisterNo: RegisterNo,
					CenOrgGoodsNo: CenOrgGoodsNo,
					ReaGoodsNo: ReaGoodsNo,
					LotQRCode: LotQRCode,
					ReaCompCode: ReaCompCode,
					//冷链信息
					FactoryOutTemperature: rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_FactoryOutTemperature"),
					ArrivalTemperature: rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ArrivalTemperature"),
					AppearanceAcceptance: rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AppearanceAcceptance")
				}
				if(InvalidDate) {
					obj.InvalidDate = JShell.Date.toServerDate(InvalidDate);
				}
				if(ProdDate) {
					obj.ProdDate = JShell.Date.toServerDate(ProdDate);
				}
				if(RegisterInvalidDate) {
					obj.RegisterInvalidDate = JShell.Date.toServerDate(RegisterInvalidDate);
				}
				if(GoodsSort) {
					obj.GoodsSort = Number(GoodsSort);
				}
				if(BarCodeType) {
					obj.BarCodeType = Number(BarCodeType);
				}
				//货架
				if(PlaceID) {
					obj.PlaceID = PlaceID;
					obj.PlaceName = PlaceCName;
				}
				//货品ID
				obj.ReaGoods = {
					Id: GoodsID,
					DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
				};
				var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
				var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
				if(userId) {
					obj.CreaterID = userId;
					obj.CreaterName = userName;
				}
				SaleDtlConfirmArr.push(obj);
				ReaBmsInDtlLink = [];
				//验收接收数量
				var AcceptCount = rec.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount');
				//扫码明细
				var ScanCodeList = rec.get('ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList');
				//混合模式
				if(Number(me.CodeScanningMode) != 1) {
					if((Number(GoodsQty) != Number(AcceptCount)) && ScanCodeList.length > 0) {
						if(ScanCodeList.length > 0) ReaBmsInDtlLink = Ext.JSON.decode(ScanCodeList);
					} else {
						var DtlConfirmLinkList = me.getDtlConfirmLinkList(rec);
						if(DtlConfirmLinkList.length > 0) ReaBmsInDtlLink = Ext.JSON.decode(DtlConfirmLinkList);
					}
				} else {
					if(ScanCodeList.length > 0) {
						ReaBmsInDtlLink = Ext.JSON.decode(ScanCodeList);
					}
				}
				var entity = {
					BarCodeMgr: BarCodeMgr
				};
				if(SaleDtlConfirmArr.length > 0) {
					entity.ReaBmsInDtl = obj;
				}
				entity.ReaBmsInDtlLinkList = ReaBmsInDtlLink;
				dtAddList.push(entity);
			}
		}
		return dtAddList;
	},
	/**@description 选择货品批号*/
	onChooseLotNo: function() {
		var me = this;
		var selected = me.getSelectionModel().getSelection();
		if(!selected || selected.length <= 0) return;
		var record = selected[0];
		var LotNo = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo");
		var ReaGoodsID = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID");
		var ReaGoodsName = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName");
		var ReaGoodsNo = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo");
		var maxWidth = document.body.clientWidth * 0.58;
		var height = document.body.clientHeight * 0.68;
		
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			GoodsID: ReaGoodsID,
			ReaGoodsNo:ReaGoodsNo,
			GoodsCName: ReaGoodsName,
			CurLotNo: LotNo,
			listeners: {
				accept: function(p, rec) {
					if(rec) {
						record.set("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo", rec.get("ReaGoodsLot_LotNo"));
						record.set("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate", rec.get("ReaGoodsLot_ProdDate"));
						record.set("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate", rec.get("ReaGoodsLot_InvalidDate"));
						record.commit();
					}
					var num = me.store.indexOf(record);
					if(num > -1) me.getSelectionModel().select(num);
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.stock.confirm.LotCheckGrid', config);
		win.show();
	},
	//获取总单总计金额
	getAllSumTotal: function() {
		var me = this,
			records = me.store.data.items;
		var count = 0,
			len = records.length;
		for(var i = 0; i < len; i++) {
			var SumTotal = records[i].get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TotalPrice');
			if(!SumTotal) SumTotal = 0;
			count += Number(SumTotal);
		}
		return count;
	},
	/**货品扫码显示货品浮动窗体信息*/
	onShowDtlInfo: function(rec, SumTotal) {
		var me = this;
		var info = {
			"CName": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName") : "",
			"EName": rec ? rec.get("ReaSaleDtlConfirmVO_ReaGoodsEName") : "",
			"SName": rec ? rec.get("ReaSaleDtlConfirmVO_ReaGoodsSName") : "",
			"Unit": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsUnit") : "",
			"UnitMemo": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_UnitMemo") : "",
			"LotNo": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo") : "",
			"InvalidDate": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate") : "",
			"AcceptCount": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount") : "",
			"InCount": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InCount") : "",
			"Price": rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price") : ""
		};
		if(SumTotal) {
			info.SumTotal = SumTotal;
		} else {
			info.SumTotal = rec ? rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TotalPrice") : "";
		}
		//		return info;
		//重置消息框的消失隐藏时间
		me.hideTimes = 5000;
		me.fireEvent('onScanCodeShowDtl', me, info);
	},
	/**@description 货品扫码时是否显示浮动窗值*/
	getIShowDtlInfoValue: function() {
		var me = this;
		var iShowDtlInfo = me.getComponent("buttonsToolbar").getComponent("cboIShowDtlInfo");
		return iShowDtlInfo.getValue();
	},
	//获取是否需要入库确认后条码打印
	getIsPrint: function() {
		var me = this;
		var cbIsPrint = me.getComponent("buttonsToolbar").getComponent("cbIsPrint");
		return cbIsPrint.getValue();
	},
	/**获取库房信息*/
	getStorageInfo: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectStorageUrl;
		url += '&fields=ReaStorage_CName,ReaStorage_Id';
		url += '&where=reastorage.Visible=1';
		url += "&sort=[{property:'ReaStorage_IsMainStorage',direction:'DESC'},{property:'ReaStorage_DispOrder',direction:'ASC'}]";
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**获取库房列表*/
	getStatusData: function(list) {
		var me = this,
			data = [];
		for(var i in list) {
			var obj = list[i];
			data.push([obj.ReaStorage_Id, obj.ReaStorage_CName]);
		}
		return data;
	}
});