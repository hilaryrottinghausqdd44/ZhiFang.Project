/**
 * 客户端验收验货单明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.reaorder.add.DtlGrid', {
	extend: 'Shell.class.rea.client.confirm.add.DtlGrid',
	title: '验货单明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchBmsCenSaleDtlConfirmVOOfOrderByHQL?isPlanish=true&confirmType=order',
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**验收主单ID*/
	PK: null,
	/**选择订单的所属供应商ID*/
	ReaCompID: null,
	/**选择好的订单ID*/
	DocOrderId: null,
	/**选择好的订单的待验收订单明细集合*/
	ReaOrderDtlVOList: [],
	OTYPE: "reaorder",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata: function(g) {
				me.setButtonsDisabled();
				me.addRecordsOfOrderDtl(me.ReaOrderDtlVOList);
			}
		});
		JShell.Action.delay(function() {
			me.setButtonsDisabled();
		}, null, 800);
		me.store.on({
			update: function(store, record) {
				if(record.dirty) {
					var changedObj = record.getChanges();
					for(var modified in changedObj) {
						if(modified == "BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount")
							me.onAcceptCountChanged(record);
						else if(modified == "BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount")
							me.onRefuseCountChanged(record);
						else if(modified == "BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price")
							me.onPriceChanged(record);
					}
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onSetConfirmInfo');
		//数据列
		me.columns = me.createGridColumns();
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
			name: "btnChoseOrder",
			itemId: "btnChoseOrder",
			text: '订单选择',
			tooltip: '订单选择',
			disabled: (!me.DocOrderId ? false : true),
			handler: function() {
				if(me.formtype == "edit") {
					JShell.Msg.alert('请将当前选择的订单验收完再选择!', null, 2000);
					return;
				}
				if(me.store.getCount() > 0) {
					JShell.Msg.confirm({
						title: '<div style="text-align:center;">订单货品选择提示</div>',
						msg: '是否清空当前选择的订单并重新选择?',
						closable: true,
					}, function(but, text) {
						if(but != "ok") return;
						me.DocOrderId = null;
						me.ReaCompID = null;
						me.ReaOrderDtlVOList = [];
						me.store.removeAll();
						me.showChooseOrder();
					});
				} else {
					me.showChooseOrder();
				}
			}
		});
		items.push({
			iconCls: 'button-add',
			name: "btnChoseReaOrderDtl",
			itemId: "btnChoseReaOrderDtl",
			text: '订单货品选择',
			tooltip: '订单货品选择',
			disabled: (!me.DocOrderId ? false : true),
			handler: function() {
				me.showChoseReaOrderDtl();
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
						me.onReaGoodsScanCode(field, e);
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
			xtype: 'actioncolumn',
			text: '复制',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				getClass: function(v, meta, record) {
					meta.tdAttr = 'data-qtip="<b>拆分复制当前货品</b>"';
					//盒条码在当前验收时不能再拆分
					if(record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr") == "1")
						return '';
					else
						return 'button-add hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var dataOne = rec.data;
					me.onCopyRecord(dataOne);
				}
			}]
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr',
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
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdGoodsNo',
			text: '<b style="color:blue;">产品编号</b>',
			width: 60,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName',
			text: '产品名称',
			width: 120,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
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
			dataIndex: 'BmsCenSaleDtlConfirmVO_ReaGoodsSName',
			text: '中文简称',
			hidden: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_ReaGoodsEName',
			text: '英文名',
			hidden: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsUnit',
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_UnitMemo',
			text: '规格',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_OrderGoodsQty',
			text: '订购数',
			width: 55,
			type: 'float',
			align: 'center',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_ReceivedCount',
			style: 'font-weight:bold;color:#fff;background:#5cb85c;',
			text: '已接收',
			width: 50,
			type: 'float',
			align: 'center',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_RejectedCount',
			text: '已拒收',
			style: 'font-weight:bold;color:#fff;background:#c9302c;',
			width: 50,
			type: 'float',
			align: 'center',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_ConfirmCount',
			text: '可验收',
			width: 50,
			type: 'float',
			sortable: false,
			align: 'center'
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo',
			text: '<b style="color:blue;">产品批号</b>',
			width: 75,
			editor: {
				allowBlank: false,
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
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InvalidDate',
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
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price',
			text: '<b style="color:blue;">单价</b>',
			width: 50,
			type: 'float',
			align: 'center',
			sortable: false,
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty',
			text: '购进数',
			width: 50,
			type: 'float',
			align: 'center',
			sortable: false
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal',
			sortable: false,
			text: '金额',
			align: 'center',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount',
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
						me.comSetReadOnlyOfBarCodeMgr(field, e);
					}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount',
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
						me.comSetReadOnlyOfBarCodeMgr(field, e);
					}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptMemo',
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
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdDate',
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
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BiddingNo',
			text: '<b style="color:blue;">招标号</b>',
			width: 60,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_TaxRate',
			text: '<b style="color:blue;">税率</b>',
			align: 'right',
			width: 50,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RegisterNo',
			sortable: false,
			text: '<b style="color:blue;">注册证编号</b>',
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RegisterInvalidDate',
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
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdGoodsNo',
			text: '<b style="color:blue;">厂商产品编号</b>',
			width: 80,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsSerial',
			sortable: false,
			text: '产品条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SysLotSerial',
			sortable: false,
			text: '系统内部批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsID',
			sortable: false,
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderGoodsID',
			sortable: false,
			text: '供应商与货品关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsNo',
			sortable: false,
			text: '货品平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ApproveDocNo',
			sortable: false,
			text: '批准文号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDoc_Id',
			sortable: false,
			text: '订单Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDtl_Id',
			sortable: false,
			text: '订单明细Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderDocNo',
			sortable: false,
			text: '订单总单号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SaleDocConfirmNo',
			sortable: false,
			text: '验收单号',
			hidden: true,
			defaultRenderer: true
		}];
		//货品扫码相关,验货已扫码记录集合只取同一条码的最后一次操作记录
		columns.push({
			dataIndex: 'BmsCenSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr',
			sortable: false,
			text: '验货已扫码记录集合',
			hidden: true,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		});
		columns.push({
			dataIndex: 'BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList',
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
		});
		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		//me.getView().update();
		//订单验收新增
		if(!me.PK) {
			me.setButtonsDisabled();
			me.addRecordsOfOrderDtl(me.ReaOrderDtlVOList);
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
		this.onSearch();
	},
	/**@description 按钮的启用或或禁用*/
	setButtonsDisabled: function() {
		var me = this;
		var bo = true;
		if(me.DocOrderId) bo = false;
		me.setBtnDisabled("btnChoseReaOrderDtl", bo);
		//me.setBtnDisabled("btnChoseOrder", !bo);
	},
	/**@description 订单导入*/
	showChooseOrder: function() {
		var me = this;
		//var maxWidth = document.body.clientWidth * 0.82;
		var height = document.body.clientHeight * 0.72;
		var config = {
			resizable: true,
			SUB_WIN_NO: '1',
			width: 860,
			height: height,
			listeners: {
				accept: function(p, record) {
					if(record) {
						var docOrderId = record.get("BmsCenOrderDoc_Id");
						var isConfirm = me.isConfirmOfDocOrderId(docOrderId);
						if(isConfirm == true) {
							me.onSetConfirmInfo(record);
							me.getReaOrderDtlVOList();
							me.setButtonsDisabled();
							p.close();
						}
					} else {
						me.DocOrderId = null;
						me.ReaOrderDtlVOList = [];
						me.setButtonsDisabled();
						p.close();
					}
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.confirm.choose.order.OrderCheck', config);
		win.show();
	},
	/**@description 判断订单是否可以新增验收或继续验收*/
	isConfirmOfDocOrderId: function(orderId) {
		var me = this;
		var isConfirm = true;
		var selectUrl = '/ReaSysManageService.svc/ST_UDTO_SearchOrderIsConfirmOfByOrderId';
		var url = (selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + selectUrl;
		url = url + "?orderId=" + orderId;
		JShell.Server.get(url, function(data) {
			isConfirm = data.success;
			if(data.success == false) {
				JShell.Msg.error("当前选择的订单不能验收:" + data.msg);
			}
		});
		return isConfirm;
	},
	/**订单货品选择*/
	showChoseReaOrderDtl: function() {
		var me = this;
		if(!me.DocOrderId) return;
		//var maxWidth = document.body.clientWidth * 0.82;
		var height = document.body.clientHeight * 0.72;

		var where = [],
			arrId = [];
		me.store.each(function(record) {
			//如果某一订单明细的货品为盒条码的,不能重复选择
			if(record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr") == "1") {
				var dtlId = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDtl_Id");
				arrId.push("bmscenorderdtl.Id!=" + dtlId);
			}
		});

		var config = {
			resizable: true,
			SUB_WIN_NO: '1',
			width: 820,
			height: height,
			PK: me.DocOrderId,
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
					if(list) me.addRecordsOfOrderDtl(list);
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.confirm.choose.order.OrderDtlGrid', config);
		win.show();
		if(arrId.length > 0) {
			var dtlIdStr = "(" + arrId.join(" and ") + ")";
			where.push(dtlIdStr);
		}
		where.push("bmscenorderdtl.BmsCenOrderDoc.Id=" + me.DocOrderId);
		win.defaultWhere = where.join(" and ");
		win.onSearch();
	},
	/**选择订单后,更新验收主单的基本信息*/
	onSetConfirmInfo: function(record) {
		var me = this;
		me.DocOrderId = record.get("BmsCenOrderDoc_Id");
		me.ReaCompID = record.get("BmsCenOrderDoc_ReaCompID");
		var objValue = {
			"BmsCenSaleDocConfirm_BmsCenOrderDoc_Id": record.get("BmsCenOrderDoc_Id"),
			"BmsCenSaleDocConfirm_OrderDocNo": record.get("BmsCenOrderDoc_OrderDocNo"), //订货总单号			
			"BmsCenSaleDocConfirm_ReaCompID": record.get("BmsCenOrderDoc_ReaCompID"),
			"BmsCenSaleDocConfirm_ReaCompName": record.get("BmsCenOrderDoc_ReaCompName")
		};
		me.fireEvent('onSetConfirmInfo', me, objValue);
	},
	/**选择订单后,获取选择的订单明细信息*/
	getReaOrderDtlVOList: function() {
		var me = this;
		var selectUrl = '/ReaSysManageService.svc/ST_UDTO_SearchReaOrderDtlVOByHQL?isPlanish=true&page=1&limit=1000';
		var url = (selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + selectUrl;
		var where = "bmscenorderdtl.BmsCenOrderDoc.Id=" + me.DocOrderId;
		url = url + "&where=" + where + "&fields=ReaOrderDtlVO_BarCodeMgr,ReaOrderDtlVO_ReaGoodsName,ReaOrderDtlVO_GoodsQty,ReaOrderDtlVO_ReceivedCount,ReaOrderDtlVO_RejectedCount,ReaOrderDtlVO_Price,ReaOrderDtlVO_SumTotal,ReaOrderDtlVO_GoodsUnit,ReaOrderDtlVO_UnitMemo,ReaOrderDtlVO_ReaGoods_ProdGoodsNo,ReaOrderDtlVO_BiddingNo,ReaOrderDtlVO_Id,ReaOrderDtlVO_ReaGoodsID,ReaOrderDtlVO_OrderGoodsID,ReaOrderDtlVO_LotSerial,ReaOrderDtlVO_GoodsNo,ReaOrderDtlVO_LotNo,ReaOrderDtlVO_ReaGoods_ApproveDocNo,ReaOrderDtlVO_ReaGoods_RegistNo,ReaOrderDtlVO_ReaGoods_RegistDate,ReaOrderDtlVO_ReaGoods_RegistNoInvalidDate,ReaOrderDtlVO_ConfirmCount,ReaOrderDtlVO_AcceptFlag,ReaOrderDtlVO_PackSerial,ReaOrderDtlVO_BmsCenOrderDoc_Id,ReaOrderDtlVO_OrderDocNo,ReaOrderDtlVO_ReaBmsCenSaleDtlConfirmLinkVOListStr,ReaOrderDtlVO_ReaGoods_EName,ReaOrderDtlVO_ReaGoods_SName";
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value)
					me.ReaOrderDtlVOList = data.value.list;
				if(data.value.list.length > 0)
					me.addRecordsOfOrderDtl(data.value.list);
				else {
					JShell.Msg.alert("获取订单明细信息为空!");
				}
			} else {
				JShell.Msg.alert("获取订单明细信息出错:" + data.msg);
			}
		});
	},
	/**默认将选择订单的订单明细添加到待验收明细列表*/
	addRecordsOfOrderDtl: function(list) {
		var me = this;
		if(!list) list = [];
		for(var i = 0; i < list.length; i++) {
			var objDtl = list[i];
			var entity = {
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id": -1,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDoc_Id": objDtl.ReaOrderDtlVO_BmsCenOrderDoc_Id,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDtl_Id": objDtl.ReaOrderDtlVO_Id,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderDocNo": objDtl.ReaOrderDtlVO_OrderDocNo,

				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr": objDtl.ReaOrderDtlVO_BarCodeMgr,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName": objDtl.ReaOrderDtlVO_ReaGoodsName,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsID": objDtl.ReaOrderDtlVO_ReaGoodsID,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderGoodsID": objDtl.ReaOrderDtlVO_OrderGoodsID,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsNo": objDtl.ReaOrderDtlVO_GoodsNo,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsUnit": objDtl.ReaOrderDtlVO_GoodsUnit,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_UnitMemo": objDtl.ReaOrderDtlVO_UnitMemo,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BiddingNo": objDtl.ReaOrderDtlVO_BiddingNo,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo": objDtl.ReaOrderDtlVO_LotNo,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdGoodsNo": objDtl.ReaOrderDtlVO_ReaGoods_ProdGoodsNo,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ApproveDocNo": objDtl.ReaOrderDtlVO_ReaGoods_ApproveDocNo,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RegisterNo": objDtl.ReaOrderDtlVO_ReaGoods_RegistNo,
				"BmsCenSaleDtlConfirmVO_ReaGoodsEName": objDtl.ReaOrderDtlVO_ReaGoods_EName,
				"BmsCenSaleDtlConfirmVO_ReaGoodsSName": objDtl.ReaOrderDtlVO_ReaGoods_SName,
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptMemo": objDtl.ReaOrderDtlVO_AcceptMemo,

				"BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList": objDtl.ReaOrderDtlVO_ReaBmsCenSaleDtlConfirmLinkVOListStr,
				"BmsCenSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr": objDtl.ReaOrderDtlVO_ReaBmsCenSaleDtlConfirmLinkVOListStr
			};
			
			if(objDtl.ReaOrderDtlVO_InvalidDate) entity.BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InvalidDate = JShell.Date.toString(objDtl.ReaOrderDtlVO_InvalidDate, true);
			if(objDtl.ReaOrderDtlVO_ReaGoods_RegistNoInvalidDate) entity.BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RegisterInvalidDate = JShell.Date.toString(objDtl.ReaOrderDtlVO_ReaGoods_RegistNoInvalidDate, true);

			if(objDtl.ReaOrderDtlVO_Price) entity.BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price = objDtl.ReaOrderDtlVO_Price;
			//订单购进数
			if(objDtl.ReaOrderDtlVO_GoodsQty) entity.BmsCenSaleDtlConfirmVO_OrderGoodsQty = objDtl.ReaOrderDtlVO_GoodsQty;
			//已接收数
			if(objDtl.ReaOrderDtlVO_ReceivedCount) entity.BmsCenSaleDtlConfirmVO_ReceivedCount = objDtl.ReaOrderDtlVO_ReceivedCount;
			//已拒收数
			if(objDtl.ReaOrderDtlVO_RejectedCount) entity.BmsCenSaleDtlConfirmVO_RejectedCount = objDtl.ReaOrderDtlVO_RejectedCount;
			//可验收数
			if(objDtl.ReaOrderDtlVO_ConfirmCount) entity.BmsCenSaleDtlConfirmVO_ConfirmCount = objDtl.ReaOrderDtlVO_ConfirmCount;
			me.store.add(entity);
		}
	},
	/**@description 接收数量值改变后联动*/
	onAcceptCountChanged: function(record) {
		var me = this;
		var ConfirmCount = record.get('BmsCenSaleDtlConfirmVO_ConfirmCount');
		var AcceptCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount');
		var Price = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price');
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
			record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		}
		var GoodsQty = AcceptCount + RefuseCount;
		//当前验收数大于可验收数
		if(GoodsQty > ConfirmCount) {
			GoodsQty = ConfirmCount;
			RefuseCount = GoodsQty - AcceptCount;
			record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
			record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		}

		var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		SumTotal = SumTotal ? SumTotal.toFixed(2) : 0;

		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal', SumTotal);
		record.commit();
	},
	/**@description 拒收数量值改变后联动*/
	onRefuseCountChanged: function(record) {
		var me = this;
		var ConfirmCount = record.get('BmsCenSaleDtlConfirmVO_ConfirmCount');
		var AcceptCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount');
		var Price = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price');

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
			record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		}
		var GoodsQty = AcceptCount + RefuseCount;
		//当前验收数大于可验收数
		if(GoodsQty > ConfirmCount) {
			GoodsQty = ConfirmCount;
			AcceptCount = GoodsQty - RefuseCount;
			record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
			record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		}

		var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		SumTotal = SumTotal ? SumTotal.toFixed(2) : 0;

		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal', SumTotal);
		record.commit();
	},
	/***
	 * @description 货品扫码时货品存在,条码类型为批条码处理
	 * @param {Object} record
	 */
	onScanCodeBatchBarCodeExist: function(record) {
		var me = this;
		var AcceptCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount');
		var GoodsQty = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty');
		//订单明细的可验收数
		var ConfirmCount = record.get('BmsCenSaleDtlConfirmVO_ConfirmCount');

		if(ConfirmCount) ConfirmCount = parseFloat(ConfirmCount);
		if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
		if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
		if(RefuseCount) RefuseCount = parseFloat(RefuseCount);

		//扫码方式的值
		var scanCode = me.getScanCodeValue();
		switch(scanCode) {
			case 1: //接收扫码处理
				AcceptCount = AcceptCount + 1;
				if(AcceptCount >= ConfirmCount) {
					AcceptCount = ConfirmCount;
					RefuseCount = 0;
				}
				break;
			case 2: //拒收扫码处理
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
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
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
				//使用盒条码或系统内部盒条码
				if(model["UsePackSerial"] == barCode || model["SysPackSerial"] == barCode) {
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
			var info = "条码为:" + barCode + "已被" + ("" + scanCode == "1" ? "接收" : "拒收") + ",请不要重复扫码!";
			JShell.Msg.error(info);
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			me.onShowDtlInfo(record);
			return;
		}

		var GoodsQty = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty');
		var ConfirmCount = record.get('BmsCenSaleDtlConfirmVO_ConfirmCount');

		if(ConfirmCount) ConfirmCount = parseFloat(ConfirmCount);
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

		switch(parseInt(scanCode)) {
			case 1: //接收扫码处理
				if(AcceptCount >= ConfirmCount) {
					AcceptCount = ConfirmCount;
					RefuseCount = 0;
				}
				break;
			case 2: //拒收扫码处理
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

		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.encode(curReaGoodsScanCodeList);
		record.set('BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList', curReaGoodsScanCodeList);

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
		if(!me.ReaCompID || !me.DocOrderId) {
			JShell.Msg.error("货品扫码--获取供应商信息失败!");
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}

		var url = (me.scanCodeUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.scanCodeUrl;
		var params = "?reaCompID=" + me.ReaCompID + "&serialNo=" + barCode + "&scanCodeType=reaorder&orderId=" + me.DocOrderId;
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
		if(reaBarCodeVOList.length <= 0) return;

		var callback = function(reaBarCodeVO) {
			if(!reaBarCodeVO) return;
			//先判断该条码的货品是否存在于验收明细列表中
			var record = null;
			me.store.each(function(rec) {
				var orderGoodsID = rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderGoodsID");
				if(reaBarCodeVO.ReaGoodsOrgLinkID == orderGoodsID) {
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
		var curReaGoodsScanCodeList = record.get("BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList");
		if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);
		var indexOf = -1;
		if(curReaGoodsScanCodeList) {
			Ext.Array.each(curReaGoodsScanCodeList, function(model, index) {
				//使用盒条码或系统内部盒条码
				if(model["UsePackSerial"] == barCode || model["SysPackSerial"] == barCode) {
					indexOf = index;
					return false;
				}
			});
		}
		if(indexOf >= 0) return;

		//当前扫码值不存在该货品的记录行里
		if(!curReaGoodsScanCodeList) curReaGoodsScanCodeList = [];
		var ConfirmCount = record.get('BmsCenSaleDtlConfirmVO_ConfirmCount');
		if(ConfirmCount) ConfirmCount = parseFloat(ConfirmCount);
		//如果当前扫码数已大于或等于可验收数
		if(curReaGoodsScanCodeList.length >= ConfirmCount) {
			var info = "条码为:" + barCode + ",货品名称为:" + record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName") + ",当前扫码数(" + curReaGoodsScanCodeList.length + ")已大于或等于可验收数(" + ConfirmCount + ")!";
			JShell.Msg.error(info);
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}

		//扫码方式的值
		var scanCode = me.getScanCodeValue();
		switch(scanCode) {
			case 1: //接收扫码处理
				break;
			case 2: //拒收扫码处理
				break;
			default:
				break;
		}
		reaBarCodeVO["BDocID"] = me.PK; //验收单Id
		reaBarCodeVO["BDocNo"] = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SaleDocConfirmNo'); //验收单号
		reaBarCodeVO["BDtlID"] = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id');
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
		record.set('BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList', curReaGoodsScanCodeList);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
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
		var info = "条码为:" + barCode + ",货品名称为:" + barCodeInfo.CName + ",不存在当前订单的验收明细中!";
		JShell.Msg.error(info);
		me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
	},
	/***
	 * @description 保存前验证
	 */
	validatorSave: function() {
		var me = this;
		var isValid = true;
		var info = "";
		if(me.store.getCount() <= 0)
			if(!LotNo) {
				info = "待验收货品明细为空!";
				isValid = false;
			}
		if(isValid == true)
			me.store.each(function(record) {
				var LotNo = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo");
				var ConfirmCount = record.get("BmsCenSaleDtlConfirmVO_ConfirmCount");
				var GoodsQty = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty");
				var Price = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price");
				var AcceptCount = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount");
				var RefuseCount = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount");
				var BarCodeMgr = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
				var ReaGoodsName = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName");
				var orderId = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDoc_Id");
				var orderDtId = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDtl_Id");

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
				} else if(!GoodsQty || GoodsQty <= 0) {
					info = "货品为" + ReaGoodsName + ",购进数不能为空或小于等于0!";
					return false;
				} else if(GoodsQty > ConfirmCount) {
					info = "货品为" + ReaGoodsName + ",购进数大于可验收数，不能保存！";
					return false;
				} else if(AcceptCount <= 0 && RefuseCount <= 0) {
					info = "货品为" + ReaGoodsName + ",验收数和拒收数小于或等于零，不能保存！";
					return false;
				} else if(AcceptCount > GoodsQty) {
					info = "货品为" + ReaGoodsName + ",验收数量大于购进数，不能保存！";
					return false;
				} else if(RefuseCount > GoodsQty) {
					info = "货品为" + ReaGoodsName + ",拒收数量大于购进数，不能保存！";
					return false;
				} else if((AcceptCount + RefuseCount) > GoodsQty) {
					info = "货品为" + ReaGoodsName + ",验收和拒收数量大于总量，不能保存！";
					return false;
				} else if(!LotNo) {
					info = "货品为" + ReaGoodsName + ",待验收货品的产品批号为空!";
					return false;
				} else if(!orderId) {
					info = "货品为" + ReaGoodsName + ",所属订单主单信息为空！";
					return false;
				} else if(!orderDtId) {
					info = "货品为" + ReaGoodsName + ",所属订单明细信息为空！";
					return false;
				} else if(BarCodeMgr == "1") {
					var curReaGoodsScanCodeList = [],
						dtlAcceptCountList = [],
						dtlRefuseCountList = [];
					curReaGoodsScanCodeList = record.get("BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList");
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
					} else if(dtlRefuseCountList.length > 0 && RefuseCount < dtlRefuseCountList.length) {
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
				} else if(BarCodeMgr != "1") {
					//同一订单明细的验证
					var dtlId = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDtl_Id");
					var SumGoodsQty = 0,
						SumAcceptCount = 0,
						SumRefuseCount = 0;
					me.store.queryBy(function(record2) {
						if(record2.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDtl_Id') == 'dtlId') {
							var GoodsQty2 = record2.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty");
							var AcceptCount2 = record2.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount");
							var RefuseCount2 = record2.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount");

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
						info = "货品为" + ReaGoodsName + ",同一订单明细的本次购进总数(" + SumGoodsQty + "大于可验收数(" + ConfirmCount + ")!";
						return false;
					}
				}
			});

		if(info) {
			isValid = false;
			JShell.Msg.error(info);
		}
		return isValid;
	},
	/**@description 获取单个的修改封装信息*/
	getSaveOneInfo: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		var entity = me.callParent(arguments);
		entity.OrderDocNo = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderDocNo");

		var orderId = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDoc_Id");
		var orderDtId = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BmsCenOrderDtl_Id");
		var strDataTimeStamp2 = "1,2,3,4,5,6,7,8";
		if(orderId) {
			entity.BmsCenOrderDoc = {
				Id: orderId,
				DataTimeStamp: strDataTimeStamp2.split(',')
			};
		}
		if(orderDtId) {
			entity.BmsCenOrderDtl = {
				Id: orderDtId,
				DataTimeStamp: strDataTimeStamp2.split(',')
			};
		}
		return entity;
	}
});