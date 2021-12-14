/**
 * 客户端验收验货单明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.reasale.add.DtlGrid', {
	extend: 'Shell.class.rea.client.confirm.add.DtlGrid',
	
	title: '验货单明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlConfirmVOOfConfirmTypeByHQL?isPlanish=true&confirmType=reasale',

	OTYPE: "reasale",
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**验收主单ID*/
	PK: null,
	/**选择供货单的所属供应商ID*/
	ReaCompID: null,
	PlatformOrgNo: null,
	/**选择好的供货单ID*/
	SaleDocID: null,
	/**选择好的供货单中的订货单号ID*/
	OrderDocID: null,
	/**客户端数据库是否独立部署*/
	ReaDataBaseIsDeploy: null,
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.reasale.add.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "验货单明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata: function(g) {
				me.setButtonsDisabled();
				if(me.SaleDocID) {
					me.loadSaleDocAndDtlVOOfSaleDocID(false);
				}
			}
		});
		JShell.Action.delay(function() {
			me.setButtonsDisabled();
		}, null, 800);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onSetConfirmInfo', 'changeSumTotal');
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];
		items.push({
			iconCls: 'button-refresh',
			text: '重置',
			tooltip: '重置',
			handler: function() {
				me.onRefreshClick();
			}
		}, '-', {
			iconCls: 'button-add',
			name: "btnChoseSaleDoc",
			itemId: "btnChoseSaleDoc",
			text: '供货单选择',
			tooltip: '供货单选择',
			disabled: (!me.SaleDocID ? false : true),
			handler: function() {
				if(me.formtype == "edit") {
					JShell.Msg.alert('请将当前选择的供货单验收完再选择!');
					return;
				}
				if(me.store.getCount() > 0) {
					JShell.Msg.confirm({
						title: '<div style="text-align:center;">供货单货品选择提示</div>',
						msg: '是否清空当前选择的供货单并重新选择?',
						closable: true,
					}, function(but, text) {
						if(but != "ok") return;
						me.SaleDocID = null;
						//me.ReaCompID = null;
						me.store.removeAll();
						me.onChooseSale();
					});
				} else {
					if(!me.ReaCompID) {
						JShell.Msg.alert('请选择供货商后再操作!');
						return;
					}
					me.onChooseSale();
				}
			}
		});
		items.push({
			iconCls: 'button-add',
			name: "btnChoseSaleDtl",
			itemId: "btnChoseSaleDtl",
			text: '供货货品选择',
			tooltip: '供货货品选择',
			disabled: (!me.SaleDocID ? false : true),
			handler: function() {
				me.onShowChoseReaCenSaleDtl();
			}
		});
		items.push({
			iconCls: 'button-check',
			name: "btnAllConfirm",
			itemId: "btnAllConfirm",
			text: '整单验收',
			tooltip: '对当前选择的供货单的未验收货品进行验收',
			disabled: (!me.SaleDocID ? false : true),
			handler: function() {
				me.onAllConfirm();
			}
		});
		items.push('-', {
			xtype: 'radiogroup',
			fieldLabel: '扫码方式',
			itemId: "rboScanCode",
			width: 160,
			vertical: false,
			labelWidth: '65px',
			items: [{
					boxLabel: '接收',
					name: 'ScanCode',
					inputValue: 2,
					checked: true
				},
				{
					boxLabel: '<b style="color:red;">拒收</b>',
					name: 'ScanCode',
					inputValue: 3
				}
			]
		}, {
			xtype: 'textfield',
			width: 280,
			style: {
				marginLeft: "15px"
			},
			emptyText: '货品扫码',
			fieldLabel: '',
			name: 'txtScanCode',
			itemId: 'txtScanCode',
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						//防止扫码时,自动出现触发多个回车事件
						JShell.Action.delay(function() {
							if(field.getValue()) {
								me.onReaGoodsScanCode(field, e);
							} else {
								JShell.Msg.alert("条码值不能为空!");
								return;
							}
						}, null, 30);
					}
				}
			}
		}, {
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
		});

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
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
			menuDisabled: true,
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType',
			text: '条码类型',
			hidden: true,
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo',
			text: '货品编码',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CenOrgGoodsNo',
			sortable: false,
			text: '供应商货品编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName',
			text: '货品名称',
			width: 120,
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
				if(value.indexOf('"')>=0)value=value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaGoodsSName',
			text: '中文简称',
			hidden: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaGoodsEName',
			text: '英文名',
			hidden: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsUnit',
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_UnitMemo',
			text: '规格',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_DtlGoodsQty',
			text: '供货数',
			width: 55,
			type: 'float',
			align: 'center',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReceivedCount',
			style: 'font-weight:bold;color:#fff;background:#5cb85c;',
			text: '已接收',
			width: 50,
			type: 'float',
			align: 'center',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_RejectedCount',
			text: '已拒收',
			style: 'font-weight:bold;color:#fff;background:#c9302c;',
			width: 50,
			type: 'float',
			align: 'center',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ConfirmCount',
			text: '可验收',
			width: 50,
			type: 'float',
			sortable: false,
			align: 'center'
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo',
			text: '<b style="color:blue;">货品批号</b>',
			width: 75,
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate',
			text: '<b style="color:blue;">有效期至</b>',
			width: 75,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price',
			text: '<b style="color:blue;">单价</b>',
			width: 80,
			type: 'float',
			align: 'right',
			sortable: false,
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty',
			text: '购进数',
			width: 50,
			type: 'float',
			align: 'right',
			sortable: false
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal',
			sortable: false,
			text: '金额',
			align: 'right',
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount',
			style: 'font-weight:bold;color:#fff;background:#5cb85c;',
			text: '接收数',
			width: 50,
			type: 'float',
			align: 'center',
			sortable: false,
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount',
			text: '拒收数',
			style: 'font-weight:bold;color:#fff;background:#c9302c;',
			width: 50,
			type: 'float',
			align: 'center',
			sortable: false,
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
			dataIndex: 'ReaSaleDtlOfConfirmVO_OrderDocNo',
			text: '订货单号',
			width: 60,
			sortable:false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlOfConfirmVO_OrderDocID',
			text: '订货单号ID',
			width: 60,
			sortable:false,
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptMemo',
			sortable: false,
			text: '<b style="color:red;">异常信息</b>',
			width: 70,
			hidden: false,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate',
			text: '<b style="color:blue;">生产日期</b>',
			align: 'center',
			width: 75,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			}
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BiddingNo',
			text: '<b style="color:blue;">招标号</b>',
			width: 60,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TaxRate',
			text: '<b style="color:blue;">税率</b>',
			align: 'right',
			width: 50,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterNo',
			sortable: false,
			text: '<b style="color:blue;">注册证编号</b>',
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterInvalidDate',
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdGoodsNo',
			text: '<b style="color:blue;">厂商货品编码</b>',
			width: 80,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSerial',
			sortable: false,
			text: '货品条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SysLotSerial',
			sortable: false,
			text: '系统内部批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotQRCode',
			sortable: false,
			text: '二维批条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID',
			sortable: false,
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CompGoodsLinkID',
			sortable: false,
			text: '供货商货品机构关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LabcGoodsLinkID',
			sortable: false,
			text: '订货方货品机构关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsNo',
			sortable: false,
			text: '货品平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ApproveDocNo',
			sortable: false,
			text: '批准文号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDtlID',
			sortable: false,
			text: '供货明细单ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDtlConfirmNo',
			sortable: false,
			text: '供货验收明细单号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDocConfirmNo',
			sortable: false,
			text: '验收单号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSort',
			sortable: false,
			text: '货品序号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_StorageType',
			sortable: false,
			text: '储藏条件',
			hidden: true,
			defaultRenderer: true
		}];
		columns.push({
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlLinkVOListStr',
			sortable: false,
			text: '供货明细条码关系集合Str',
			hidden: true,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		});
		//货品扫码相关,验货已扫码记录集合只取同一条码的最后一次操作记录
		columns.push({
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr',
			sortable: false,
			text: '验货已扫码记录集合',
			hidden: true,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		});
		columns.push({
			dataIndex: 'ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList',
			sortable: false,
			text: '当次扫码记录集合',
			hidden: true,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		});
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
		},{
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_FactoryOutTemperature',
			text: '<b style="color:blue;">厂家出库温度</b>',
			width: 90,
			hidden:true,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		},{
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ArrivalTemperature',
			text: '<b style="color:blue;">到货温度</b>',
			width: 80,
			hidden:true,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		},{
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AppearanceAcceptance',
			text: '<b style="color:blue;">外观验收</b>',
			width: 80,
			hidden:true,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		});
		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		//me.getView().update();
		//供货验收新增
		if(!me.PK) {
			me.setButtonsDisabled();
			if(me.SaleDocID) {
				me.loadSaleDocAndDtlVOOfSaleDocID(false);
			}
			me.fireEvent('nodata', me);
			return false;
		}
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		me.setButtonsDisabled();
	},
	/**@description 刷新按钮点击处理方法*/
	onRefreshClick: function() {
		var me = this;
		if(!me.ReaCompID && !me.SaleDocID && !me.PK) {
			me.store.removeAll();
			var error = me.errorFormat.replace(/{msg}/, "请选择供货商或供货单后再操作！");
			me.getView().update(error);
		} else if(me.PK || me.SaleDocID) {
			me.onSearch();
		}
	},
	/**@description 按钮的启用或或禁用*/
	setButtonsDisabled: function() {
		var me = this;
		var bo = true;
		if(me.SaleDocID) bo = false;
		me.setBtnDisabled("btnChoseSaleDtl", bo);
		me.setBtnDisabled("btnAllConfirm", bo);
	},
	/**供货单选择*/
	onChooseSale: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			title: '供应商为【' + me.ReaCompCName + "】的供货单选择",
			resizable: true,
			SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			ReaServerCompCode: me.PlatformOrgNo,
			ReaCompID: me.ReaCompID,
			ReaCompCName: me.ReaCompCName,
			listeners: {
				accept: function(p, record) {
					if(!record) {
						return;
					}
					me.SaleDocID = record.get("ReaBmsCenSaleDoc_Id");
					me.OrderDocID=record.get("ReaBmsCenSaleDoc_OrderDocID");
					me.loadSaleDocAndDtlVOOfSaleDocID(false, function(data) {
						if(data.success == true) {
							me.onSetConfirmInfo(record);
							p.close();
						} else {
							me.SaleDocID = null;
						}
					});
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.confirm.choose.sale.CheckApp', config);
		win.show();
	},
	/**
	 * @description 判断选择供货单是否可以新增验收或继续验收
	 * @param {Object} isAllConfirm 是否整单接收
	 * @param {Object} callback
	 */
	loadSaleDocAndDtlVOOfSaleDocID: function(isAllConfirm, callback) {
		var me = this;
		if(!me.SaleDocID) {
			JShell.Msg.error("获取选择的供货信息为空!");
			return;
		}
		var selectUrl = '/ReaManageService.svc/ST_UDTO_SearchReaSaleIsConfirmOfBySaleDocID';
		var url = (selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + selectUrl;
		url = url + "?saleDocID=" + me.SaleDocID;
		if(me.PK) {
			url = url + "&confirmId=" + me.PK;
		}
		JShell.Server.get(url, function(data) {
			if(data.success == false) {
				JShell.Msg.error("当前选择的供货单不能验收:" + data.msg);
			} else {
				me.loadSaleDtlOfConfirmVOOfSaleDocID(isAllConfirm);
			}
			me.setButtonsDisabled();
			if(callback) callback(data);
		});
	},
	/**@description 供货货品选择*/
	onShowChoseReaCenSaleDtl: function() {
		var me = this;
		if(!me.SaleDocID) {
			JShell.Msg.error("获取选择的供货信息为空!");
			return;
		}
		//var maxWidth = document.body.clientWidth * 0.82;
		var height = document.body.clientHeight * 0.72;
		var where = [],
			arrId = [];
		me.store.each(function(record) {
			//如果某一订单明细的货品为盒条码的,不能重复选择
			if(record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType") == "1") {
				var saleDtlID = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDtlID");
				arrId.push("reabmscensaledtl.Id!=" + saleDtlID);
			}
		});

		var config = {
			resizable: true,
			SUB_WIN_NO: '1',
			width: 820,
			height: height,
			PK: me.SaleDocID,
			listeners: {
				//需要验证当前的购进数量
				validGoodsQty: function(form, addRecord) {
					if(!addRecord) return true;
				},
				onAddRecord: function(p, addRecord) {
					//需要验证当前的购进数量
					if(addRecord) me.store.add(addRecord);
					p.close();
				},
				onCheck: function(p, list) {
					if(list) me.addRecordsOfSaleDtl(list, false);
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.confirm.choose.sale.SaleDtlCheck', config);
		win.show();
		if(arrId.length > 0) {
			var dtlIdStr = "(" + arrId.join(" and ") + ")";
			where.push(dtlIdStr);
		}
		where.push("reabmscensaledtl.SaleDocID=" + me.SaleDocID);
		win.defaultWhere = where.join(" and ");
		win.onSearch();
	},
	/**@description 选择供货单后,更新验收主单的基本信息*/
	onSetConfirmInfo: function(record) {
		var me = this;
		me.SaleDocID = record.get("ReaBmsCenSaleDoc_Id");
		var objValue = {
			"ReaBmsCenSaleDocConfirm_SaleDocID": record.get("ReaBmsCenSaleDoc_Id"),
			"ReaBmsCenSaleDocConfirm_SaleDocNo": record.get("ReaBmsCenSaleDoc_SaleDocNo")
			//"ReaBmsCenSaleDocConfirm_LabcName": record.get("ReaBmsCenSaleDoc_LabcName")
		};
		me.fireEvent('onSetConfirmInfo', me, objValue);
	},
	/**@description 当前选择的供货单整单验收*/
	onAllConfirm: function() {
		var me = this;
		if(!me.SaleDocID) {
			JShell.Msg.alert("获取选择的供货信息为空!", null, 2000);
			return;
		}
		if(me.store.getCount() > 0) {
			JShell.Msg.confirm({
				title: '<div style="text-align:center;">整单验收提示</div>',
				msg: '<b>对当前选择的供货单的未验收货品进行验收操作!<br />在整单验收操作前,会清空当前验收明细列表的货品信息!<br />是否继续执行整单验收操作!</b>',
				closable: true,
				multiline: false
			}, function(but, text) {
				if(but != "ok") return;
				me.store.removeAll();
				me.loadSaleDocAndDtlVOOfSaleDocID(true, function(data) {
					//if(data.success == true) p.close();
				});
			});
		} else {
			me.loadSaleDocAndDtlVOOfSaleDocID(true);
		}
	},
	/***
	 * @description 选择供货单后,获取选择的供货明细信息
	 * @param {Object} isAllConfirm 是否整单接收
	 */
	loadSaleDtlOfConfirmVOOfSaleDocID: function(isAllConfirm) {
		var me = this;
		if(!me.SaleDocID) {
			JShell.Msg.alert("获取选择的供货信息为空!", null, 2000);
			return;
		}
		var selectUrl = '/ReaManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlOfConfirmVOListBySaleDocID?isPlanish=true&page=1&limit=1000&saleDocID=' + me.SaleDocID;
		var url = (selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + selectUrl;
		var where = "reabmscensaledtl.SaleDocID=" + me.SaleDocID;
		url = url + "&where=" + where + "&fields=ReaSaleDtlOfConfirmVO_OrderDocNo,ReaSaleDtlOfConfirmVO_OrderDocID,ReaSaleDtlOfConfirmVO_BarCodeType,ReaSaleDtlOfConfirmVO_ReaGoodsName,ReaSaleDtlOfConfirmVO_ReaGoods_SName,ReaSaleDtlOfConfirmVO_ReaGoods_EName,ReaSaleDtlOfConfirmVO_GoodsQty,ReaSaleDtlOfConfirmVO_ConfirmCount,ReaSaleDtlOfConfirmVO_ReceivedCount,ReaSaleDtlOfConfirmVO_RejectedCount,ReaSaleDtlOfConfirmVO_Price,ReaSaleDtlOfConfirmVO_SumTotal,ReaSaleDtlOfConfirmVO_GoodsUnit,ReaSaleDtlOfConfirmVO_UnitMemo,ReaSaleDtlOfConfirmVO_ProdGoodsNo,ReaSaleDtlOfConfirmVO_BiddingNo,ReaSaleDtlOfConfirmVO_Id,ReaSaleDtlOfConfirmVO_ReaGoodsID,ReaSaleDtlOfConfirmVO_CompGoodsLinkID,ReaSaleDtlOfConfirmVO_LabcGoodsLinkID,ReaSaleDtlOfConfirmVO_LotSerial,ReaSaleDtlOfConfirmVO_LotQRCode,ReaSaleDtlOfConfirmVO_SysLotSerial,ReaSaleDtlOfConfirmVO_ReaGoodsNo,ReaSaleDtlOfConfirmVO_ProdOrgNo,ReaSaleDtlOfConfirmVO_CenOrgGoodsNo,ReaSaleDtlOfConfirmVO_GoodsNo,ReaSaleDtlOfConfirmVO_LotNo,ReaSaleDtlOfConfirmVO_ApproveDocNo,ReaSaleDtlOfConfirmVO_RegisterNo,ReaSaleDtlOfConfirmVO_ReaGoods_RegistDate,ReaSaleDtlOfConfirmVO_InvalidDate,ReaSaleDtlOfConfirmVO_RegisterInvalidDate,ReaSaleDtlOfConfirmVO_AcceptFlag,ReaSaleDtlOfConfirmVO_SaleDocID,ReaSaleDtlOfConfirmVO_SaleDocNo,ReaSaleDtlOfConfirmVO_ReaBmsCenSaleDtlLinkVOListStr,ReaSaleDtlOfConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr,ReaSaleDtlOfConfirmVO_ReaServerCompCode,ReaSaleDtlOfConfirmVO_IsPrintBarCode,ReaSaleDtlOfConfirmVO_ReaCompID,ReaSaleDtlOfConfirmVO_ReaCompanyName,ReaSaleDtlOfConfirmVO_ReaGoods_GoodsSort,ReaSaleDtlOfConfirmVO_ProdDate";
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value.list.length > 0) {
						me.addRecordsOfSaleDtl(data.value.list, isAllConfirm);
					} else {
						JShell.Msg.alert("获取供货明细信息为空!");
					}
				}
			} else {
				JShell.Msg.alert("获取供货明细信息出错:" + data.msg);
			}
		});
	},
	/***
	 * @description 默认将选择供货单的供货明细添加到待验收明细列表
	 * @param {Object} list
	 * @param {Object} isAllConfirm 是否整单接收
	 */
	addRecordsOfSaleDtl: function(list, isAllConfirm) {
		var me = this;
		if(!list) list = [];
		for(var i = 0; i < list.length; i++) {
			var objDtl = list[i];
			var entity = {
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id": -1,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDtlID": objDtl.ReaSaleDtlOfConfirmVO_Id,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType": objDtl.ReaSaleDtlOfConfirmVO_BarCodeType,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName": objDtl.ReaSaleDtlOfConfirmVO_ReaGoodsName,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID": objDtl.ReaSaleDtlOfConfirmVO_ReaGoodsID,

				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CompGoodsLinkID": objDtl.ReaSaleDtlOfConfirmVO_CompGoodsLinkID,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LabcGoodsLinkID": objDtl.ReaSaleDtlOfConfirmVO_LabcGoodsLinkID,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsUnit": objDtl.ReaSaleDtlOfConfirmVO_GoodsUnit,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_UnitMemo": objDtl.ReaSaleDtlOfConfirmVO_UnitMemo,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BiddingNo": objDtl.ReaSaleDtlOfConfirmVO_BiddingNo,

				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo": objDtl.ReaSaleDtlOfConfirmVO_LotNo,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ApproveDocNo": objDtl.ReaSaleDtlOfConfirmVO_ApproveDocNo,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterNo": objDtl.ReaSaleDtlOfConfirmVO_RegisterNo,
				"ReaSaleDtlConfirmVO_ReaGoodsEName": objDtl.ReaSaleDtlOfConfirmVO_ReaGoods_EName,
				"ReaSaleDtlConfirmVO_ReaGoodsSName": objDtl.ReaSaleDtlOfConfirmVO_ReaGoods_SName,

				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptMemo": objDtl.ReaSaleDtlOfConfirmVO_AcceptMemo,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotSerial": objDtl.ReaSaleDtlOfConfirmVO_LotSerial,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotQRCode": objDtl.ReaSaleDtlOfConfirmVO_LotQRCode,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SysLotSerial": objDtl.ReaSaleDtlOfConfirmVO_SysLotSerial,
				"ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList": "", //当次扫码集合
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlLinkVOListStr": objDtl.ReaSaleDtlOfConfirmVO_ReaBmsCenSaleDtlLinkVOListStr, //供货明细条码关系集合Str
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr": objDtl.ReaSaleDtlOfConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr,

				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo': objDtl.ReaSaleDtlOfConfirmVO_ReaGoodsNo,
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdGoodsNo': objDtl.ReaSaleDtlOfConfirmVO_ProdGoodsNo,
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CenOrgGoodsNo": objDtl.ReaSaleDtlOfConfirmVO_CenOrgGoodsNo,
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsNo': objDtl.ReaSaleDtlOfConfirmVO_GoodsNo,
				'ReaSaleDtlOfConfirmVO_OrderDocNo':objDtl.ReaSaleDtlOfConfirmVO_OrderDocNo,
				'ReaSaleDtlOfConfirmVO_OrderDocID':me.OrderDocID
			};
			if(objDtl.ReaSaleDtlOfConfirmVO_ReaGoods_GoodsSort) {
				entity["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSort"] = objDtl.ReaSaleDtlOfConfirmVO_ReaGoods_GoodsSort;
			}

			var ProdDate = objDtl.ReaSaleDtlOfConfirmVO_ProdDate;
			if(ProdDate) entity.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate = JShell.Date.toString(ProdDate, true);

			var InvalidDate = objDtl.ReaSaleDtlOfConfirmVO_InvalidDate;
			if(InvalidDate) entity.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate = JShell.Date.toString(InvalidDate, true);

			var RegisterInvalidDate = objDtl.ReaSaleDtlOfConfirmVO_RegisterInvalidDate;
			if(RegisterInvalidDate) entity.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterInvalidDate = JShell.Date.toString(RegisterInvalidDate, true);

			if(objDtl.ReaSaleDtlOfConfirmVO_Price) entity.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price = objDtl.ReaSaleDtlOfConfirmVO_Price;
			//供货数
			if(objDtl.ReaSaleDtlOfConfirmVO_DtlGoodsQty) entity.ReaSaleDtlConfirmVO_DtlGoodsQty = objDtl.ReaSaleDtlOfConfirmVO_DtlGoodsQty;
			//供货数=原供货明细的供货数
			if(objDtl.ReaSaleDtlOfConfirmVO_GoodsQty) entity.ReaSaleDtlConfirmVO_DtlGoodsQty = objDtl.ReaSaleDtlOfConfirmVO_GoodsQty;
			//已接收数
			if(objDtl.ReaSaleDtlOfConfirmVO_ReceivedCount) entity.ReaSaleDtlConfirmVO_ReceivedCount = objDtl.ReaSaleDtlOfConfirmVO_ReceivedCount;
			//已拒收数
			if(objDtl.ReaSaleDtlOfConfirmVO_RejectedCount) entity.ReaSaleDtlConfirmVO_RejectedCount = objDtl.ReaSaleDtlOfConfirmVO_RejectedCount;
			//可验收数
			if(objDtl.ReaSaleDtlOfConfirmVO_ConfirmCount)
				entity.ReaSaleDtlConfirmVO_ConfirmCount = objDtl.ReaSaleDtlOfConfirmVO_ConfirmCount;
			else
				entity.ReaSaleDtlConfirmVO_ConfirmCount = 0;

			//如果是整单接收
			if(isAllConfirm == true) {
				var AcceptCount = entity.ReaSaleDtlConfirmVO_ConfirmCount;
				var Price = entity.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price;
				if(!AcceptCount) AcceptCount = 0;
				if(!Price) Price = 0;

				//接收数=可验收数
				entity.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount = AcceptCount;
				//购进数=接收数
				entity.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty = AcceptCount;

				var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
				SumTotal = SumTotal ? SumTotal : 0;
				entity.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal = SumTotal;
				//拒收数=0
				entity.ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount = 0;

			}
			entity=me.addNewOfColdInfo(entity);
			me.store.add(entity);
		}
	},
	/**@description 接收数量值改变后联动*/
	onAcceptCountChanged: function(record) {
		var me = this;
		var ConfirmCount = record.get('ReaSaleDtlConfirmVO_ConfirmCount');
		var AcceptCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount');
		var Price = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price');
		if(ConfirmCount)
			ConfirmCount = parseFloat(ConfirmCount);
		else ConfirmCount = 0;

		if(AcceptCount)
			AcceptCount = parseFloat(AcceptCount);
		else AcceptCount = 0;

		if(RefuseCount)
			RefuseCount = parseFloat(RefuseCount);
		else RefuseCount = 0;
		//接收数大于可验收数
		if(AcceptCount > ConfirmCount) {
			AcceptCount = ConfirmCount;
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		}
		var GoodsQty = AcceptCount + RefuseCount;
		//当前验收数大于可验收数
		if(GoodsQty > ConfirmCount) {
			GoodsQty = ConfirmCount;
			RefuseCount = GoodsQty - AcceptCount;
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		}

		var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		SumTotal = SumTotal ? SumTotal : 0;

		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal', SumTotal);
		record.commit();
		//联动取总额
		var total = me.getSumTotal();
		me.fireEvent('changeSumTotal', total);
	},
	/**@description 拒收数量值改变后联动*/
	onRefuseCountChanged: function(record) {
		var me = this;
		var ConfirmCount = record.get('ReaSaleDtlConfirmVO_ConfirmCount');
		var AcceptCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount');
		var Price = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price');

		if(ConfirmCount)
			ConfirmCount = parseFloat(ConfirmCount);
		else ConfirmCount = 0;

		if(AcceptCount)
			AcceptCount = parseFloat(AcceptCount);
		else AcceptCount = 0;

		if(RefuseCount)
			RefuseCount = parseFloat(RefuseCount);
		else RefuseCount = 0;

		//拒收数大于可验收数
		if(RefuseCount > ConfirmCount) {
			RefuseCount = ConfirmCount;
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		}
		var GoodsQty = AcceptCount + RefuseCount;
		//当前验收数大于可验收数
		if(GoodsQty > ConfirmCount) {
			GoodsQty = ConfirmCount;
			AcceptCount = GoodsQty - RefuseCount;
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		}

		var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		SumTotal = SumTotal ? SumTotal : 0;

		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal', SumTotal);
		record.commit();
	},
	/***
	 * @description 货品扫码时货品存在,条码类型为批条码处理
	 * @param {Object} record
	 */
	onScanCodeBatchBarCodeExist: function(record) {
		var me = this;
		var AcceptCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount');
		var GoodsQty = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty');
		//订单明细的可验收数
		var ConfirmCount = record.get('ReaSaleDtlConfirmVO_ConfirmCount');

		if(ConfirmCount) ConfirmCount = parseFloat(ConfirmCount);
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
		if(RefuseCount) RefuseCount = parseFloat(RefuseCount);

		//扫码方式的值
		var scanCode = me.getScanCodeValue();
		switch(scanCode) {
			case 2: //接收扫码处理
				AcceptCount = AcceptCount + 1;
				if(AcceptCount >= ConfirmCount) {
					AcceptCount = ConfirmCount;
					RefuseCount = 0;
				}
				break;
			case 3: //拒收扫码处理
				RefuseCount = RefuseCount + 1;
				if(RefuseCount >= ConfirmCount) {
					RefuseCount = ConfirmCount;
					AcceptCount = 0;
				}
				break;
			default:
				break;
		}

		GoodsQty = AcceptCount + RefuseCount;
		if(GoodsQty > ConfirmCount) GoodsQty = ConfirmCount;
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		record.commit();
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();

		me.onShowDtlInfo(record);
	},
	/***
	 * @description 货品扫码时条码类型为盒条码,货品及条码都存在验收明细里的处理
	 * @param {Object} record 条码所在的行记录
	 * @param {Object} curReaGoodsScanCodeList 当前条码为盒条码时的条码明细关系
	 */
	onScanCodeOfBoxBarCodeExist: function(barCode, record, curReaGoodsScanCodeList) {
		var me = this;
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
		var AcceptCount = 0,
			RefuseCount = 0;
		//扫码方式的值
		var scanCode = me.getScanCodeValue();
		if(indexOf < 0) {
			//供货明细条码关系集合Str
			var reaBmsCenSaleDtlLinkVOList = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlLinkVOListStr");
			if(reaBmsCenSaleDtlLinkVOList) reaBmsCenSaleDtlLinkVOList = JcallShell.JSON.decode(reaBmsCenSaleDtlLinkVOList);
			Ext.Array.each(reaBmsCenSaleDtlLinkVOList, function(model, index) {
				//一维盒条码或二维盒条码
				if(model["UsePackSerial"] == barCode || model["UsePackQRCode"] == barCode) {
					indexOf = index;
					var operationVO = {
						"Id": -1,
						"OperTypeID": scanCode,
						"ReceiveFlag": scanCode,
						"BDocNo": record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDocConfirmNo"),
						"BDocID": me.PK,
						"BDtlID": record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id"),
						"SysPackSerial": model.SysPackSerial,
						"OtherPackSerial": model.OtherPackSerial,
						"UsePackSerial": model.UsePackSerial,
						"UsePackQRCode": model.UsePackQRCode,
						"LotNo": model.LotNo
					};
					if(record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSort")) {
						operationVO.GoodsSort = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSort");
					}
					curReaGoodsScanCodeList.push(operationVO);
					return false;
				}
			});

		} else {
			//如果当前扫码方式与条码明细的接收方式一样,直接提示并返回
			if(parseInt(curReaGoodsScanCodeList[indexOf].ReceiveFlag) == scanCode) {
				var info = "条码为:" + barCode + "已被" + ("" + scanCode == "2" ? "接收" : "拒收") + ",请不要重复扫码!";
				JShell.Msg.error(info);
				me.gettxtScanCode().setValue("");
				me.gettxtScanCode().focus();
				me.onShowDtlInfo(record);
				return;
			}
			if(curReaGoodsScanCodeList)
				curReaGoodsScanCodeList[indexOf].ReceiveFlag = scanCode;

		}
		if(!curReaGoodsScanCodeList) curReaGoodsScanCodeList = [];
		Ext.Array.each(curReaGoodsScanCodeList, function(curModel, index1) {
			if(parseInt(curModel["ReceiveFlag"]) == 2) AcceptCount = AcceptCount + 1;
			else if(parseInt(curModel["ReceiveFlag"]) == 3) RefuseCount = RefuseCount + 1;
		});
		var GoodsQty = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty');
		var ConfirmCount = record.get('ReaSaleDtlConfirmVO_ConfirmCount');

		if(ConfirmCount) ConfirmCount = parseFloat(ConfirmCount);
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);

		if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
		if(RefuseCount) RefuseCount = parseFloat(RefuseCount);

		switch(parseInt(scanCode)) {
			case 2: //接收扫码处理
				if(AcceptCount >= ConfirmCount) {
					AcceptCount = ConfirmCount;
					RefuseCount = 0;
				}
				break;
			case 3: //拒收扫码处理
				if(RefuseCount >= ConfirmCount) {
					RefuseCount = ConfirmCount;
					AcceptCount = 0;
				}
				break;
			default:
				break;
		}
		GoodsQty = AcceptCount + RefuseCount;
		if(GoodsQty > ConfirmCount) GoodsQty = ConfirmCount;

		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.encode(curReaGoodsScanCodeList);
		record.set('ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList', curReaGoodsScanCodeList);

		record.commit();
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
		me.onShowDtlInfo(record);
	},
	/**
	 * @description 货品扫码,条码不存在验收明细中,调用服务处理
	 * @param {Object} barCode
	 */
	onScanCodeUrl: function(barCode) {
		var me = this;
		if(!me.ReaCompID) {
			JShell.Msg.error("请选择供应商后再扫码!");
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}
		if(!me.SaleDocID) {
			JShell.Msg.error("未提取供货单,请提取供货单后再扫码!");
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}
		var barCode2=JShell.String.encode(barCode);
		var url = (me.scanCodeUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.scanCodeUrl;
		var params = "?reaCompID=" + me.ReaCompID + "&serialNo=" + barCode2 + "&scanCodeType=reasale&bobjectID=" + me.SaleDocID;

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
	 * @description 货品扫码调用服务后,获取到条码信息后的处理
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
			//先判断该条码的货品是否存在于验收明细列表中
			var record = null;
			me.store.each(function(rec) {
				var compGoodsLinkID = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CompGoodsLinkID");
				var lotNo = rec.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo");
				if(reaBarCodeVO.CompGoodsLinkID == compGoodsLinkID && (!lotNo || reaBarCodeVO.LotNo == lotNo)) {
					record = rec;
					return false;
				}
			});
			//货品存在验收明细中,但条码不存在验收的条码明细中
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
	 * @description 货品扫码调用服务处理后,条码类型为盒条码,货品存在验收明细中,但条码不存在验收的条码明细中
	 * @param {Object} record
	 * @param {Object} reaBarCodeVO
	 * @param {Object} barCode
	 */
	onScanCodeUrlAfterOfBoxAndExistDtl: function(record, reaBarCodeVO, barCode) {
		var me = this;
		var curReaGoodsScanCodeList = record.get("ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList");
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
		var ConfirmCount = record.get('ReaSaleDtlConfirmVO_ConfirmCount');
		if(ConfirmCount) ConfirmCount = parseFloat(ConfirmCount);
		//如果当前扫码数已大于或等于可验收数
		if(curReaGoodsScanCodeList.length >= ConfirmCount) {
			var info = "条码为:" + barCode + ",货品名称为:" + record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName") + ",当前扫码数(" + curReaGoodsScanCodeList.length + ")已大于或等于可验收数(" + ConfirmCount + ")!";
			JShell.Msg.error(info);
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}

		//扫码方式的值
		var scanCode = me.getScanCodeValue();

		reaBarCodeVO["BDocID"] = me.PK; //验收单Id
		reaBarCodeVO["BDocNo"] = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDocConfirmNo'); //验收单号
		reaBarCodeVO["BDtlID"] = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id');
		var operationVO = me.getBarcodeOperationVO(reaBarCodeVO, scanCode);
		curReaGoodsScanCodeList.push(operationVO);
		//重新计算接收数及拒收数
		var AcceptCount = 0,
			RefuseCount = 0,
			GoodsQty = 0;
		Ext.Array.each(curReaGoodsScanCodeList, function(curModel, index1) {
			if(parseInt(curModel["ReceiveFlag"]) == 2) AcceptCount = AcceptCount + 1;
			else if(parseInt(curModel["ReceiveFlag"]) == 3) RefuseCount = RefuseCount + 1;
		});
		if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
		if(RefuseCount) RefuseCount = parseFloat(RefuseCount);
		var GoodsQty = AcceptCount + RefuseCount;
		if(AcceptCount > ConfirmCount) AcceptCount = ConfirmCount;
		if(RefuseCount > ConfirmCount) RefuseCount = ConfirmCount;
		GoodsQty = AcceptCount + RefuseCount;
		if(GoodsQty > ConfirmCount) GoodsQty = ConfirmCount;

		if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.encode(curReaGoodsScanCodeList);
		record.set('ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList', curReaGoodsScanCodeList);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.commit();
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
		me.onShowDtlInfo(record);
	},
	/***
	 * @description 货品扫码调用服务处理后,条码类型为盒条码,货品及条码都不存在验收明细中
	 * @param {Object} barCodeInfo
	 * @param {Object} barCode
	 */
	onScanCodeUrlAfterOfBoxAndNotExistDtl: function(barCodeInfo, barCode) {
		var me = this;
		var info = "条码为:" + barCode + ",货品名称为:" + barCodeInfo.CName + ",不存在当前供货单的验收明细中!";
		JShell.Msg.error(info);
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
	},
	/***
	 * @description 保存前验证
	 */
	validatorSave: function() {
		var me = this;
		var result = {
			"isValid": true,
			"info": ""
		};
		if(me.store.getCount() <= 0) {
			result.info = "待验收货品明细为空!";
			result.isValid = false;
			return result;
		}
		var info = "";
		me.store.each(function(record) {
			var LotNo = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo");
			var ConfirmCount = record.get("ReaSaleDtlConfirmVO_ConfirmCount");
			var GoodsQty = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty");
			var Price = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price");
			var AcceptCount = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount");
			var RefuseCount = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount");
			var BarCodeType = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType");
			var ReaGoodsName = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName");
			var saleDtlID = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDtlID");
			var InvalidDate = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate");

			if(ConfirmCount) ConfirmCount = parseFloat(ConfirmCount);
			else ConfirmCount = 0;

			if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
			else GoodsQty = 0;

			if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
			else AcceptCount = 0;

			if(RefuseCount) RefuseCount = parseFloat(RefuseCount);
			else RefuseCount = 0;

			//当前行记录的验证
			if(!ConfirmCount || ConfirmCount <= 0) {
				info = "货品为" + ReaGoodsName + ",可验收数为空或小于等于0!";
				return false;
			}
			if(!GoodsQty || GoodsQty <= 0) {
				info = "货品为" + ReaGoodsName + ",购进数不能为空或小于等于0!";
				return false;
			}
			if(GoodsQty > ConfirmCount) {
				info = "货品为" + ReaGoodsName + ",购进数大于可验收数，不能保存！";
				return false;
			}
			if(AcceptCount <= 0 && RefuseCount <= 0) {
				info = "货品为" + ReaGoodsName + ",验收数和拒收数小于或等于零，不能保存！";
				return false;
			}
			if(AcceptCount > GoodsQty) {
				info = "货品为" + ReaGoodsName + ",验收数量大于购进数，不能保存！";
				return false;
			}
			if(RefuseCount > GoodsQty) {
				info = "货品为" + ReaGoodsName + ",拒收数量大于购进数，不能保存！";
				return false;
			}
			if((AcceptCount + RefuseCount) > GoodsQty) {
				info = "货品为" + ReaGoodsName + ",验收和拒收数量大于总量，不能保存！";
				return false;
			}
			if(!LotNo) {
				info = "货品为" + ReaGoodsName + ",待验收货品的货品批号为空!";
				return false;
			}
			if(!saleDtlID) {
				info = "货品为" + ReaGoodsName + ",所属供货明细信息为空！";
				return false;
			}
			if(!InvalidDate) {
				info = "货品为" + ReaGoodsName + ",有效期至不能为空!";
				return false;
			}
			if(!Price) Price = 0;
			if(Price < 0) {
				info = "货品为" + ReaGoodsName + ",单价不能为空或不能小于等于0!";
				return false;
			}
			if(BarCodeType == "1") {
				var curReaGoodsScanCodeList = [],
					dtlAcceptCountList = [],
					dtlRefuseCountList = [];
				curReaGoodsScanCodeList = record.get("ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList");
				if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);
				if(curReaGoodsScanCodeList && curReaGoodsScanCodeList.length > 0) {
					Ext.Array.each(curReaGoodsScanCodeList, function(item, index, countriesItSelf) {
						if(item.ReceiveFlag == 2) dtlAcceptCountList.push(item);
						else dtlRefuseCountList.push(item);
					});
				}

				if(dtlAcceptCountList.length > 0 && AcceptCount < dtlAcceptCountList.length) {
					info = "货品为" + ReaGoodsName + ",接收数(" + AcceptCount + ")小于接收扫码数(" + dtlAcceptCountList.length + ")!";
					return false;
				}
				if(dtlRefuseCountList.length > 0 && RefuseCount < dtlRefuseCountList.length) {
					info = "货品为" + ReaGoodsName + ",拒收数(" + RefuseCount + ")小于拒收扫码数(" + dtlRefuseCountList.length + ")!";
					return false;
				}

				switch(me.CodeScanningMode) {
					case "strict":
						if(curReaGoodsScanCodeList.length <= 0) {
							info = "货品为" + ReaGoodsName + ",盒条码信息为空!";
							return false;
						}
						break;
					default:
						break;
				}
			} else if(BarCodeType != "1") {
				//同一供货明细的验证
				var dtlId = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDtlID");
				var SumGoodsQty = 0,
					SumAcceptCount = 0,
					SumRefuseCount = 0;
				me.store.queryBy(function(record2) {
					if(record2.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDtlID') == dtlId) {
						var GoodsQty2 = record2.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty");
						var AcceptCount2 = record2.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount");
						var RefuseCount2 = record2.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount");

						if(GoodsQty2) GoodsQty2 = parseFloat(GoodsQty2);
						else GoodsQty2 = 0;
						if(AcceptCount2) AcceptCount2 = parseFloat(AcceptCount2);
						else AcceptCount2 = 0;
						if(RefuseCount2) RefuseCount2 = parseFloat(RefuseCount2);
						else RefuseCount2 = 0;

						SumGoodsQty += GoodsQty2;
						SumAcceptCount += AcceptCount2;
						SumRefuseCount += RefuseCount2;
					}
				});
				if(SumGoodsQty > ConfirmCount) {
					info = "货品为" + ReaGoodsName + ",同一供货明细的本次购进总数(" + SumGoodsQty + "大于可验收数(" + ConfirmCount + ")!";
					return false;
				}
			}
		});

		if(info) {
			result.isValid = false;
			result.info = info;
		}
		return result;
	},
	/**@description 获取单个的修改封装信息*/
	getSaveOneInfo: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		var entity = me.callParent(arguments);
		var saleDtlID = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDtlID");

		if(saleDtlID) entity.SaleDtlID = saleDtlID;
		return entity;
	},
	getOrderDocNo:function(){
		var me = this;
		var OrderDocNo = "";
		me.store.each(function(record) {
			OrderDocNo = record.get("ReaSaleDtlOfConfirmVO_OrderDocNo");
		});
		return OrderDocNo;
	},
	getOrderDocID:function(){
		var me = this;
		var OrderDocID = "";
		me.store.each(function(record) {
			OrderDocID = record.get("ReaSaleDtlOfConfirmVO_OrderDocID");
		});
		return OrderDocID;
	}
});