/**
 * 客户端验收验货单明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.manualinput.add.DtlGrid', {
	extend: 'Shell.class.rea.client.confirm.add.DtlGrid',
	
	title: '验货单明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlConfirmVOOfConfirmTypeByHQL?isPlanish=true&confirmType=manualinput',
	
	OTYPE: "manualinput",
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.manualinput.add.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "验货单明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('changeSumTotal','onAddReaGoodsClick');
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
			xtype: 'actioncolumn',
			text: '复制',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			hidden: me.hiddenCopy,
			items: [{
				getClass: function(v, meta, record) {
					meta.tdAttr = 'data-qtip="<b>拆分复制当前货品</b>"';
					//盒条码在当前验收时不能再拆分
					if(record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType") == "1")
						return '';
					else
						return 'button-add hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var dataOne = {};
					dataOne = Ext.apply(dataOne, rec.data);
					me.onCopyRecord(dataOne);
				}
			}]
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsNo',
			sortable: false,
			text: '货品平台编码',
			hidden: true,
			defaultRenderer: true
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
		},{
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdOrgName',
			text: '厂家名称',
			//hidden: true,
			width: 60,
			defaultRenderer: true
		},  {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsUnit',
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo',
			text: '<b style="color:blue;">货品批号</b>',
			width: 80,
			editor: {
				allowBlank: false,
				emptyText: '双击选择批号',
				listeners: {
					render: function(field, eOpts) {
						field.getEl().on('dblclick', function(p, el, e) {
							me.IsShowDtlInfo = false;
							me.onChooseLotNo();
						});
					},
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						me.onSetLotNo(records[0], newValue);
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var IsNeedPerformanceTest = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_IsNeedPerformanceTest');
				if(value && IsNeedPerformanceTest == 'true') {
					var IsLotNoExist = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_IsLotNoExist');
					//                  meta.style = 'background-color:white;color:#ffffff;';
					if(IsLotNoExist == '0') meta.style = 'background-color:red;color:#ffffff;';
				}
				return value;
			}
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate',
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price',
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty',
			text: '购进数',
			width: 60,
			type: 'float',
			align: 'right'
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal',
			sortable: false,
			text: '金额',
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount',
			style: 'font-weight:bold;color:#fff;background:#5cb85c;',
			text: '接收数',
			width: 65,
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount',
			text: '拒收数',
			style: 'font-weight:bold;color:#fff;background:#c9302c;',
			width: 65,
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptMemo',
			sortable: false,
			text: '<b style="color:red;">异常信息</b>',
			width: 80,
			hidden: false,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdGoodsNo',
			text: '<b style="color:blue;">厂商编号</b>',
			width: 80,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate',
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BiddingNo',
			text: '<b style="color:blue;">招标号</b>',
			width: 80,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_TaxRate',
			text: '<b style="color:blue;">税率</b>',
			align: 'right',
			width: 60,
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
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ApproveDocNo',
			sortable: false,
			text: '批准文号',
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
			text: '货品序号',
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_StorageType',
			sortable: false,
			text: '储藏条件',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_IsLotNoExist',
			sortable: false,
			text: '批号是否已存在,用于批号列颜色标识',
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_IsNeedPerformanceTest',
			sortable: false,
			text: '性能验证',
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}];
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
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		},{
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ArrivalTemperature',
			text: '<b style="color:blue;">到货温度</b>',
			width: 80,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		},{
			dataIndex: 'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AppearanceAcceptance',
			text: '<b style="color:blue;">外观验收</b>',
			width: 80,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		});
		return columns;
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
			name: "btnAdd",
			itemId: "btnAdd",
			text: '货品选择',
			tooltip: '货品选择',
			handler: function() {
				me.onAddReaGoods();
			}
		});
		items.push('-', {
			xtype: 'radiogroup',
			fieldLabel: '扫码操作',
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
			width: 320,
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
								JShell.Msg.alert("验收扫码的条码值不能为空!");
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
	onAddReaGoods: function() {
		var me = this;
		if(me.ReaCompID) {
			//机构类型为供应商
			var defaultWhere = " reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.OrgType=0 and reagoodsorglink.CenOrg.Id=" + me.ReaCompID;
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
						me.fireEvent('onAddReaGoodsClick', me, records);
						if(records && records.length > 0) me.onReaGoodAccept(records);
						p.close();
					}
				}
			}).show();
		} else {
			JShell.Msg.alert("请选择供货商后再操作", null, 1000);
		}
	},
	/**@description 货品选择后添加到验收货品明细列表里*/
	onReaGoodAccept: function(records) {
		var me = this;
		for(var i = 0; i < records.length; i++) {
			var record = records[i];
			var price = record.get("ReaGoodsOrgLink_Price");
			if(!price) price = 0;
			price = parseFloat(price);
			//手工验收选择货品时,LabcGoodsLinkID与CompGoodsLinkID是一样的
			var addRecord = {
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id': "-1",
				"ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList": "",
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr": "",
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType": record.get("ReaGoodsOrgLink_ReaGoods_BarCodeMgr"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName': record.get("ReaGoodsOrgLink_ReaGoods_CName"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdOrgName': record.get("ReaGoodsOrgLink_ReaGoods_ProdOrgName"),

				'ReaSaleDtlConfirmVO_ReaGoodsEName': record.get("ReaGoodsOrgLink_ReaGoods_EName"),
				'ReaSaleDtlConfirmVO_ReaGoodsSName': record.get("ReaGoodsOrgLink_ReaGoods_SName"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID': record.get("ReaGoodsOrgLink_ReaGoods_Id"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CompGoodsLinkID': record.get("ReaGoodsOrgLink_Id"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LabcGoodsLinkID': record.get("ReaGoodsOrgLink_Id"),

				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsUnit': record.get("ReaGoodsOrgLink_ReaGoods_UnitName"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_UnitMemo': record.get("ReaGoodsOrgLink_ReaGoods_UnitMemo"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate': record.get("ReaGoodsOrgLink_ReaGoods_RegistDate"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate': record.get("ReaGoodsOrgLink_ReaGoods_InvalidDate"),
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price": price,

				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BiddingNo': record.get("ReaGoodsOrgLink_BiddingNo"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo': "", //批号
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ApproveDocNo': record.get("ReaGoodsOrgLink_ReaGoods_ApproveDocNo"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterInvalidDate': record.get("ReaGoodsOrgLink_ReaGoods_RegistNoInvalidDate"),
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotSerial": "",
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotQRCode": "",
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SysLotSerial": "",

				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterNo': record.get("ReaGoodsOrgLink_ReaGoods_RegistNo"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo': record.get("ReaGoodsOrgLink_ReaGoods_ReaGoodsNo"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdGoodsNo': record.get("ReaGoodsOrgLink_ReaGoods_ProdGoodsNo"),
				"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CenOrgGoodsNo": record.get("ReaGoodsOrgLink_CenOrgGoodsNo"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsNo': record.get("ReaGoodsOrgLink_ReaGoods_GoodsNo"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSort': record.get("ReaGoodsOrgLink_ReaGoods_GoodsSort"),
				'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_IsNeedPerformanceTest': record.get("ReaGoodsOrgLink_ReaGoods_IsNeedPerformanceTest")
			};
			/**
			 * 2018-12-21
			 * 如果验货品扫码的模式为” 严格模式”,接收数和拒收数都默认为0;
			 * 如果验货品扫码的模式为” 混合模式”,接收数默认为1,拒收数为0;
			 */
			if(me.CodeScanningMode == "mixing") {
				addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount"] = 1;
				addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount"] = 0;
				addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty"] = 1;
				addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal"] = price;
			} else {
				addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount"] = 0;
				addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty"] = 0;
				addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount"] = 0;
				addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal"] = 0;
			}
			//默认的冷链信息
			addRecord=me.addNewOfColdInfo(addRecord);

			me.store.add(addRecord);
		}
		//联动取总额
		var total = me.getSumTotal();
		me.fireEvent('changeSumTotal', total);
	},
	/**@description 接收数量值改变后联动*/
	onAcceptCountChanged: function(record) {
		var me = this;
		var AcceptCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount');
		var Price = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price');
		if(AcceptCount)
			AcceptCount = parseFloat(AcceptCount);
		else AcceptCount = 0;
		if(RefuseCount)
			RefuseCount = parseFloat(RefuseCount);
		else RefuseCount = 0;

		var GoodsQty = AcceptCount + RefuseCount;
		var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		SumTotal = SumTotal ? SumTotal : 0;

		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal', SumTotal);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.commit();

		//联动取总额
		var total = me.getSumTotal();
		me.fireEvent('changeSumTotal', total);
	},
	/**@description 拒收数量值改变后联动*/
	onRefuseCountChanged: function(record) {
		var me = this;
		var AcceptCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount');
		var Price = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price');
		if(AcceptCount)
			AcceptCount = parseFloat(AcceptCount);
		else AcceptCount = 0;
		if(RefuseCount)
			RefuseCount = parseFloat(RefuseCount);
		else RefuseCount = 0;
		var GoodsQty = AcceptCount + RefuseCount;
		var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		SumTotal = SumTotal ? SumTotal : 0;
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal', SumTotal);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.commit();
	},
	/***
	 * @description 货品扫码时货品存在明细列表中,条码类型为批条码处理
	 * @param {Object} record
	 */
	onScanCodeBatchBarCodeExist: function(record) {
		var me = this;
		var AcceptCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount');
		var GoodsQty = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
		if(RefuseCount) RefuseCount = parseFloat(RefuseCount);
		//扫码方式的值
		var scanCode = me.getScanCodeValue();
		switch(scanCode) {
			case 2: //接收扫码处理
				AcceptCount = AcceptCount + 1;
				break;
			case 3: //拒收扫码处理
				RefuseCount = RefuseCount + 1;
				break;
			default:
				break;
		}
		GoodsQty = AcceptCount + AcceptCount;
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		record.commit();
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();

		me.onShowDtlInfo(record);
	},
	/***
	 * @description 货品扫码时条码类型为盒条码,货品及条码都存在验收明细列表的处理*
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
		if(indexOf < 0) {
			JShell.Msg.alert("没有找到该货品条码信息!", null, 2000);
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}

		//扫码方式的值
		var scanCode = me.getScanCodeValue();
		//如果当前扫码方式与条码明细的接收方式一样,直接提示并返回
		if(parseInt(curReaGoodsScanCodeList[indexOf].ReceiveFlag) == scanCode) {
			var info = "条码为:" + barCode + "已被" + (scanCode == 2 ? "接收" : "拒收") + ",请不要重复扫码!";
			JShell.Msg.error(info);
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			me.onShowDtlInfo(record);
			return;
		}

		var GoodsQty = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty');
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);

		if(curReaGoodsScanCodeList)
			curReaGoodsScanCodeList[indexOf].ReceiveFlag = scanCode;

		var AcceptCount = 0,
			RefuseCount = 0;
		Ext.Array.each(curReaGoodsScanCodeList, function(curModel, index1) {
			if(parseInt(curModel["ReceiveFlag"]) == 2) AcceptCount = AcceptCount + 1;
			else if(parseInt(curModel["ReceiveFlag"]) == 3) RefuseCount = RefuseCount + 1;
		});

		if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
		if(RefuseCount) RefuseCount = parseFloat(RefuseCount);
		GoodsQty = AcceptCount + RefuseCount;

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
	 * @description 货品扫码,条码不存在验收明细列表,调用服务处理
	 * @param {Object} barCode
	 */
	onScanCodeUrl: function(barCode) {
		var me = this;
		if(!me.ReaCompID) {
			JShell.Msg.error("获取供应商信息失败!请选择供应商后再扫码!");
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}
		var barCode2=JShell.String.encode(barCode);
		var url = (me.scanCodeUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.scanCodeUrl;
		var params = "?reaCompID=" + me.ReaCompID + "&serialNo=" + barCode2 + "&scanCodeType=manualinput";

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
			if(record) {
				me.onScanCodeUrlAfterOfBoxAndExistDtl(record, reaBarCodeVO, barCode);
			} else {
				me.onScanCodeUrlAfterOfBoxAndNotExistDtl(reaBarCodeVO, barCode);
				var records = me.store.data.items;
				me.onSetRecIsNeedPerformanceTest(records, true);
			}
		}
		//弹出货品选择列表
		if(reaBarCodeVOList.length > 1) {
			me.onChooseReaBarCodeVO(reaBarCodeVOList, callback);
			var records = me.store.data.items;
			me.onSetRecIsNeedPerformanceTest(records, true);
		} else {
			callback(reaBarCodeVOList[0]);
		}
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
		//扫码方式的值
		var scanCode = me.getScanCodeValue();
		reaBarCodeVO["BDocID"] = me.PK; //验收单Id
		reaBarCodeVO["BDocNo"] = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SaleDocConfirmNo'); //验收单号
		reaBarCodeVO["BDtlID"] = record.get('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id');
		var operationVO = me.getBarcodeOperationVO(reaBarCodeVO, scanCode);
		curReaGoodsScanCodeList.push(operationVO);

		//重新计算接收数及拒收数
		var AcceptCount = 0,
			RefuseCount = 0;
		Ext.Array.each(curReaGoodsScanCodeList, function(curModel, index1) {
			if(parseInt(curModel["ReceiveFlag"]) == 2) AcceptCount = AcceptCount + 1;
			else if(parseInt(curModel["ReceiveFlag"]) == 3) RefuseCount = RefuseCount + 1;
		});
		if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
		if(RefuseCount) RefuseCount = parseFloat(RefuseCount);
		var GoodsQty = AcceptCount + RefuseCount;

		if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.encode(curReaGoodsScanCodeList);
		record.set('ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList', curReaGoodsScanCodeList);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount', RefuseCount);

		var lotNo = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo");
		if(!lotNo) {
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo', reaBarCodeVO.LotNo);
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ApproveDocNo', reaBarCodeVO.ApproveDocNo);
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterInvalidDate', reaBarCodeVO.RegistNoInvalidDate);
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterNo', reaBarCodeVO.RegisterNo);
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate', reaBarCodeVO.InvalidDate);
			record.set('ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BiddingNo', reaBarCodeVO.BiddingNo);
		}
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
	onScanCodeUrlAfterOfBoxAndNotExistDtl: function(reaBarCodeVO, barCode) {
		var me = this;
		var Price = reaBarCodeVO.Price;
		if(Price) Price = parseFloat(Price);
		else Price = 0;

		var SumTotal = 0,
			AcceptCount = 0,
			RefuseCount = 0,
			GoodsQty = 0;
		//扫码方式的值
		var scanCode = me.getScanCodeValue();
		//接收扫码处理
		if(scanCode == 2)
			AcceptCount = 1;
		else
			RefuseCount = 1;

		GoodsQty = AcceptCount + RefuseCount;
		SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		var curReaGoodsScanCodeList = [];
		reaBarCodeVO["BDocID"] = me.PK; //验收单Id
		var operationVO = me.getBarcodeOperationVO(reaBarCodeVO, scanCode);
		curReaGoodsScanCodeList.push(operationVO);
		if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.encode(curReaGoodsScanCodeList);
		if(reaBarCodeVO.InvalidDate) reaBarCodeVO.InvalidDate = JcallShell.Date.toString(reaBarCodeVO.InvalidDate, true);

		if(reaBarCodeVO.RegistDate) reaBarCodeVO.RegistDate = JcallShell.Date.toString(reaBarCodeVO.RegistDate, true);
		if(reaBarCodeVO.RegistNoInvalidDate) reaBarCodeVO.RegistNoInvalidDate = JcallShell.Date.toString(reaBarCodeVO.RegistNoInvalidDate, true);
		//手工验收选择货品时,LabcGoodsLinkID与CompGoodsLinkID是一样的
		var addRecord = {
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Id': "-1",
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LabcGoodsLinkID': reaBarCodeVO.CompGoodsLinkID,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CompGoodsLinkID': reaBarCodeVO.CompGoodsLinkID,
			"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType": reaBarCodeVO.BarCodeType,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName': reaBarCodeVO.CName,

			'ReaSaleDtlConfirmVO_ReaGoodsEName': reaBarCodeVO.EName,
			'ReaSaleDtlConfirmVO_ReaGoodsSName': reaBarCodeVO.SName,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID': reaBarCodeVO.ReaGoodsID,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsUnit': reaBarCodeVO.UnitName,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_UnitMemo': reaBarCodeVO.UnitMemo,

			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate': "", // reaBarCodeVO.RegistDate,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ApproveDocNo': reaBarCodeVO.ApproveDocNo,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterInvalidDate': reaBarCodeVO.RegistNoInvalidDate,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RegisterNo': reaBarCodeVO.RegisterNo,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate': reaBarCodeVO.InvalidDate, //有效期

			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BiddingNo': reaBarCodeVO.BiddingNo,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo': reaBarCodeVO.LotNo, //批号
			"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price": Price,
			"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal": SumTotal,
			"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount": AcceptCount,

			"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount": RefuseCount,
			"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty": GoodsQty,
			"ReaSaleDtlConfirmVO_CurReaGoodsScanCodeList": curReaGoodsScanCodeList,
			"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotSerial": "",
			"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotQRCode": "",
			"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SysLotSerial": "",

			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo': reaBarCodeVO.ReaGoodsNo,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdGoodsNo': reaBarCodeVO.ProdGoodsNo,
			"ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_CenOrgGoodsNo": reaBarCodeVO.CenOrgGoodsNo,
			'ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsNo': reaBarCodeVO.GoodsNo
		};
		if(reaBarCodeVO.GoodsSort) {
			addRecord["ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsSort"] = reaBarCodeVO.GoodsSort;
		}
		addRecord=me.addNewOfColdInfo(addRecord);
		me.store.add(addRecord);
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
		me.getSelectionModel().select(me.store.getCount() - 1);
		var records = me.getSelectionModel().getSelection();
		me.onShowDtlInfo(records[0]);
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
			var GoodsQty = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_GoodsQty");
			var Price = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_Price");
			var AcceptCount = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_AcceptCount");
			var RefuseCount = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_RefuseCount");
			var SumTotal = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_SumTotal");
			var BarCodeType = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_BarCodeType");
			var ReaGoodsName = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName");
			var InvalidDate = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate");
			var ReaGoodsNo = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo");
			if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
			else GoodsQty = 0;
			if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
			else AcceptCount = 0;
			if(RefuseCount) RefuseCount = parseFloat(RefuseCount);
			else RefuseCount = 0;

			if(!GoodsQty || GoodsQty <= 0) {
				info = "货品为" + ReaGoodsName + ",购进数不能为空或小于等于0!";
				return false;
			}
			if(AcceptCount <= 0 && RefuseCount <= 0) {
				info = "货品为" + ReaGoodsName + ",验收数量和拒收数量小于或等于零，不能验收！";
				return false;
			}
			if(AcceptCount > GoodsQty) {
				info = "货品为" + ReaGoodsName + ",验收数量大于购进数，不能验收！";
				return false;
			}
			if(RefuseCount > GoodsQty) {
				info = "货品为" + ReaGoodsName + ",拒收数量大于购进数，不能验收！";
				return false;
			}
			if((AcceptCount + RefuseCount) > GoodsQty) {
				info = "货品为" + ReaGoodsName + ",验收和拒收数量大于总量，不能验收！";
				return false;
			}
			if(!ReaGoodsNo) {
				info = "货品为" + ReaGoodsName + ",待验收货品的货品编码为空!";
				return false;
			}
			if(!LotNo) {
				info = "货品为" + ReaGoodsName + ",待验收货品的货品批号为空!";
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
			}
		});
		if(info) {
			result.isValid = false;
			result.info = info;
		}
		return result;
	},
	/**@description 刷新按钮点击处理方法*/
	onRefreshClick: function() {
		var me = this;
		if(!me.ReaCompID && (!me.PK || me.PK | me.PK == null)) {
			me.store.removeAll();
			var error = me.errorFormat.replace(/{msg}/, "请选择供货商后再操作！");
			me.getView().update(error);
		} else if(me.ReaCompID && (!me.PK || me.PK | me.PK == null)) {
			me.store.removeAll();
		} else if(me.PK) {
			me.onSearch();
		}
	},
	/**@description 选择货品批号*/
	onChooseLotNo: function() {
		var me = this;
		var selected = me.getSelectionModel().getSelection();
		if(!selected || selected.length <= 0) return;
		var record = selected[0];
		var lotNo = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo");
		var reaGoodsID = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsID");
		var reaGoodsNo = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsNo");
		var reaGoodsName = record.get("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ReaGoodsName");
		var maxWidth = 860; //document.body.clientWidth * 0.68;
		var height = document.body.clientHeight * 0.88;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			GoodsID: reaGoodsID,
			ReaGoodsNo: reaGoodsNo,
			GoodsCName: reaGoodsName,
			CurLotNo: lotNo,
			listeners: {
				accept: function(p, rec) {
					me.IsShowDtlInfo = true;
					if(rec) {
						record.set("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_LotNo", rec.get("ReaGoodsLot_LotNo"));
						record.set("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_ProdDate", rec.get("ReaGoodsLot_ProdDate"));
						record.set("ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_InvalidDate", rec.get("ReaGoodsLot_InvalidDate"));
						me.onSetLotNo(record, rec.get("ReaGoodsLot_LotNo"));
						record.commit();
					}
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.goodslot.CheckGrid', config);
		win.show();
	},
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		me.onSetRecIsNeedPerformanceTest(records);
	}
});