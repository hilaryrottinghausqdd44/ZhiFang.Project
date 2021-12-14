/**
 * 客户端验收验货单明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.manualinput.add.DtlGrid', {
	extend: 'Shell.class.rea.client.confirm.add.DtlGrid',
	title: '验货单明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchBmsCenSaleDtlConfirmVOOfConfirmByHQL?isPlanish=true',
	OTYPE: "manualinput",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
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
		//数据列
		me.columns = me.createGridColumns();
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
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdGoodsNo',
			text: '<b style="color:blue;">产品编号</b>',
			width: 80,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
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
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo',
			text: '<b style="color:blue;">产品批号</b>',
			width: 80,
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
			width: 80,
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
			width: 65,
			type: 'float',
			align: 'center',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty',
			text: '购进数',
			width: 60,
			type: 'float',
			align: 'center'
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal',
			sortable: false,
			text: '金额',
			align: 'center',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount',
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
						me.comSetReadOnlyOfBarCodeMgr(field, e);
					}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount',
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
						me.comSetReadOnlyOfBarCodeMgr(field, e);
					}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptMemo',
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
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdDate',
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
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BiddingNo',
			text: '<b style="color:blue;">招标号</b>',
			width: 80,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_TaxRate',
			text: '<b style="color:blue;">税率</b>',
			align: 'right',
			width: 60,
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
		},{
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
	onAddReaGoods: function() {
		var me = this;
		if(me.ReaCompID) {
			var sysdate = JcallShell.System.Date.getDate();
			var curTime = JcallShell.Date.toString(sysdate,true);
			 
			var defaultWhere = " reagoodsorglink.Visible=1 and ((reagoodsorglink.BeginTime<='" + curTime + "' and reagoodsorglink.EndTime is null ) or (reagoodsorglink.BeginTime<='" + curTime + "' and reagoodsorglink.EndTime>='" + curTime + "')) and reagoodsorglink.CenOrg.OrgType=0 and reagoodsorglink.CenOrg.Id=" + me.ReaCompID;
			JShell.Win.open('Shell.class.rea.client.confirm.manualinput.GoodsOrgLinkCheck', {
				resizable: false,
				defaultWhere: defaultWhere,
				/**是否单选*/
				checkOne: false,
				listeners: {
					accept: function(p, records) {
						if(records && records.length > 0) me.onReaGoodAccept(records);
						p.close();
					}
				}
			}).show();
		} else {
			JShell.Msg.alert("请选择供货方后再操作", null, 1000);
		}
	},
	onReaGoodAccept: function(records) {
		var me = this;
		for(var i = 0; i < records.length; i++) {
			var record = records[i];
			var addRecord = {
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id': "-1",
				"BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList": "",
				"BmsCenSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr": "",
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr": record.get("ReaGoodsOrgLink_ReaGoods_BarCodeMgr"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName': record.get("ReaGoodsOrgLink_ReaGoods_CName"),
				'BmsCenSaleDtlConfirmVO_ReaGoodsEName': record.get("ReaGoodsOrgLink_ReaGoods_EName"),
				'BmsCenSaleDtlConfirmVO_ReaGoodsSName': record.get("ReaGoodsOrgLink_ReaGoods_SName"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsID': record.get("ReaGoodsOrgLink_ReaGoods_Id"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderGoodsID': record.get("ReaGoodsOrgLink_Id"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsNo': record.get("ReaGoodsOrgLink_ReaGoods_GoodsNo"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsUnit': record.get("ReaGoodsOrgLink_ReaGoods_UnitName"),

				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_UnitMemo': record.get("ReaGoodsOrgLink_ReaGoods_UnitMemo"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdDate': record.get("ReaGoodsOrgLink_ReaGoods_RegistDate"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InvalidDate': record.get("ReaGoodsOrgLink_ReaGoods_InvalidDate"),
				"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price": record.get("ReaGoodsOrgLink_Price"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BiddingNo': record.get("ReaGoodsOrgLink_BiddingNo"),

				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo': "", //批号
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdGoodsNo': record.get("ReaGoodsOrgLink_ReaGoods_ProdGoodsNo"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ApproveDocNo': record.get("ReaGoodsOrgLink_ReaGoods_ApproveDocNo"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RegisterInvalidDate': record.get("ReaGoodsOrgLink_ReaGoods_RegistNoInvalidDate"),
				'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RegisterNo': record.get("ReaGoodsOrgLink_ReaGoods_RegistNo")
			};
			me.store.add(addRecord);
		}
	},
	/**@description 接收数量值改变后联动*/
	onAcceptCountChanged: function(record) {
		var me = this;
		var AcceptCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount');
		var Price = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price');
		if(AcceptCount)
			AcceptCount = parseFloat(AcceptCount);
		else AcceptCount = 0;
		if(RefuseCount)
			RefuseCount = parseFloat(RefuseCount);
		else RefuseCount = 0;

		var GoodsQty = AcceptCount + RefuseCount;
		var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		SumTotal = SumTotal ? SumTotal.toFixed(2) : 0;

		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal', SumTotal);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.commit();
	},
	/**@description 拒收数量值改变后联动*/
	onRefuseCountChanged: function(record) {
		var me = this;
		var AcceptCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount');
		var Price = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price');
		if(AcceptCount)
			AcceptCount = parseFloat(AcceptCount);
		else AcceptCount = 0;
		if(RefuseCount)
			RefuseCount = parseFloat(RefuseCount);
		else RefuseCount = 0;
		var GoodsQty = AcceptCount + RefuseCount;
		var SumTotal = parseFloat(Price) * parseFloat(AcceptCount);
		SumTotal = SumTotal ? SumTotal.toFixed(2) : 0;
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal', SumTotal);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.commit();
	},
	/***
	 * @description 货品扫码时货品存在明细列表中,条码类型为批条码处理
	 * @param {Object} record
	 */
	onScanCodeBatchBarCodeExist: function(record) {
		var me = this;
		var AcceptCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount');
		var RefuseCount = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount');
		var GoodsQty = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty');
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
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
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
			var info = "条码为:" + barCode + "已被" + (scanCode == 2 ? "接收" : "拒收") + ",请不要重复扫码!";
			JShell.Msg.error(info);
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			me.onShowDtlInfo(record);
			return;
		}

		var GoodsQty = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty');
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
	 * @description 货品扫码,条码不存在验收明细列表,调用服务处理
	 * @param {Object} barCode
	 */
	onScanCodeUrl: function(barCode) {
		var me = this;
		if(!me.ReaCompID) {
			JShell.Msg.error("货品扫码--获取供应商信息失败!");
			me.gettxtScanCode().setValue("");
			me.gettxtScanCode().focus();
			return;
		}

		var url = (me.scanCodeUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.scanCodeUrl;
		var params = "?reaCompID=" + me.ReaCompID + "&serialNo=" + barCode + "&scanCodeType=manualinput";
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
		//扫码方式的值
		var scanCode = me.getScanCodeValue();
		reaBarCodeVO["BDocID"] = me.PK; //验收单Id
		reaBarCodeVO["BDocNo"] = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SaleDocConfirmNo'); //验收单号
		reaBarCodeVO["BDtlID"] = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id');
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
		record.set('BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList', curReaGoodsScanCodeList);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty', GoodsQty);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount', AcceptCount);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount', RefuseCount);
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
		var Price = barCodeInfo.Price;
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

		if(barCodeInfo.InvalidDate) barCodeInfo.InvalidDate = JcallShell.Date.toString(barCodeInfo.InvalidDate, true);
		if(barCodeInfo.RegistDate) barCodeInfo.RegistDate = JcallShell.Date.toString(barCodeInfo.RegistDate, true);
		if(barCodeInfo.RegistNoInvalidDate) barCodeInfo.RegistNoInvalidDate = JcallShell.Date.toString(barCodeInfo.RegistNoInvalidDate, true);
		var addRecord = {
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id': "-1",
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderGoodsID': barCodeInfo.ReaGoodsOrgLinkID,
			"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr": barCodeInfo.BarCodeMgr,
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName': barCodeInfo.CName,
			'BmsCenSaleDtlConfirmVO_ReaGoodsEName': barCodeInfo.EName,
			'BmsCenSaleDtlConfirmVO_ReaGoodsSName': barCodeInfo.SName,
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsID': barCodeInfo.ReaGoodsID,
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsNo': barCodeInfo.GoodsNo,
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsUnit': barCodeInfo.UnitName,
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_UnitMemo': barCodeInfo.UnitMemo,
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdDate': barCodeInfo.RegistDate,
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ApproveDocNo': barCodeInfo.ApproveDocNo,
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RegisterInvalidDate': barCodeInfo.RegistNoInvalidDate,
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RegisterNo': barCodeInfo.RegisterNo,

			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InvalidDate': barCodeInfo.InvalidDate, //有效期			
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BiddingNo': barCodeInfo.BiddingNo,
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo': barCodeInfo.LotNo, //批号
			'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdGoodsNo': barCodeInfo.ProdGoodsNo,

			"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price": Price,
			"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal": SumTotal,
			"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount": AcceptCount,
			"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount": RefuseCount,
			"BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty": GoodsQty,
			"BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList": curReaGoodsScanCodeList
		};
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

				var GoodsQty = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty");
				var Price = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price");
				var AcceptCount = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount");
				var RefuseCount = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_RefuseCount");
				var SumTotal = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal");
				var BarCodeMgr = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
				var ReaGoodsName = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName");

				if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
				else GoodsQty = 0;
				if(AcceptCount) AcceptCount = parseFloat(AcceptCount);
				else AcceptCount = 0;
				if(RefuseCount) RefuseCount = parseFloat(RefuseCount);
				else RefuseCount = 0;

				if(!GoodsQty || GoodsQty <= 0) {
					info = "货品为" + ReaGoodsName + ",购进数不能为空或小于等于0!";
					return false;
				} else if(AcceptCount <= 0 && RefuseCount <= 0) {
					info = "货品为" + ReaGoodsName + ",验收数量和拒收数量小于或等于零，不能验收！";
					return false;
				} else if(AcceptCount > GoodsQty) {
					info = "货品为" + ReaGoodsName + ",验收数量大于购进数，不能验收！";
					return false;
				} else if(RefuseCount > GoodsQty) {
					info = "货品为" + ReaGoodsName + ",拒收数量大于购进数，不能验收！";
					return false;
				} else if((AcceptCount + RefuseCount) > GoodsQty) {
					info = "货品为" + ReaGoodsName + ",验收和拒收数量大于总量，不能验收！";
					return false;
				} else if(!LotNo) {
					info = "货品为" + ReaGoodsName + ",待验收货品的产品批号为空!";
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
				}
			});

		if(info) {
			isValid = false;
			JShell.Msg.error(info);
		}
		return isValid;
	}
});