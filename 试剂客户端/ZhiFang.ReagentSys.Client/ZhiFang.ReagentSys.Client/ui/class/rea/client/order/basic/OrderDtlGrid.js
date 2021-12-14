/**
 * 订货明细列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.basic.OrderDtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '订货明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsCenOrderDtlByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	hasDel: true,
	/**新增明细或删除明细按钮的启用状态*/
	buttonsDisabled: true,
	/**当前选择的供应商Id*/
	ReaCompID: null,
	ReaCompCName: null,
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**是否多选行*/
	checkOne: false,
	/**隐藏货品平台编码列*/
	hiddenGoodsNo: true,
	/*品牌*/
	ProdOrg: 'ProdOrg',
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 200,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			update: function(store, record) {
				if(record.dirty) {
					var changedObj = record.getChanges();
					for(var modified in changedObj) {
						if(modified == "ReaBmsCenOrderDtl_Price" || modified == "ReaBmsCenOrderDtl_GoodsQty") {
							me.onPriceOrGoodsQtyChanged(record);
						} else if(modified == "ReaBmsCenOrderDtl_ReqGoodsQty") {
							me.onReqGoodsQtyChanged(record);
						}
					}
				}
			}
		});
		me.on({
			nodata: function(p) {
				me.enableControl(true);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onDelAfter');
		if(!me.checkOne) me.setCheckboxModel();
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		//me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
		//只能点击复选框才能选中
		me.selModel = new Ext.selection.CheckboxModel({
			checkOnly: true
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsCenOrderDtl_GoodSName',
			text: '简称',
			width: 90,
			defaultRenderer: true,
			doSort: function(state) {
				//自定义排序字段
				me.store.sort({
					property: "ReaGoods_SName",
					direction: state
				});
			}
		},{
			dataIndex: 'ReaBmsCenOrderDtl_GoodEName',
			text: '英文名称',
			width: 90,
			defaultRenderer: true,
			doSort: function(state) {
				//自定义排序字段
				me.store.sort({
					property: "ReaGoods_EName",
					direction: state
				});
			}
		},{
			dataIndex: 'ReaBmsCenOrderDtl_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ReaGoodsName',
			text: '货品名称',
			sortable: true,
			width: 160,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsCenOrderDtl_BarCodeType");
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
			dataIndex: 'ReaBmsCenOrderDtl_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_CenOrgGoodsNo',
			text: '供货商货品编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_GoodsNo',
			text: '平台货品编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_MonthlyUsage',
			text: '理论月用量',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_CurrentQty',
			text: '库存数',
			width: 75,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ArrivalTime',
			text: '<b style="color:blue;">到货时间</b>',
			width: 85,
			sortable: false,
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d',
				listeners: {
					focus: function(com, e, eOpts) {
						me.comSetReadOnly(com, e);
					}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ReqGoodsQty',
			text: '<b style="color:blue;">申请数</b>',
			width: 70,
			type: 'float',
			//align: 'right',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false,
				listeners: {
					focus: function(com, e, eOpts) {
						me.onSetReqGoodsQtyReadOnly(com, e);
					}
				}
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_GoodsQty',
			text: '<b style="color:blue;">实批数</b>',
			width: 70,
			type: 'float',
			//align: 'right',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false,
				listeners: {
					focus: function(com, e, eOpts) {
						me.onSetGoodsQtyReadOnly(com, e);
					}
				}
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ExpectedStock',
			text: '预期库存量',
			hidden: true,
			width: 85,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_Price',
			sortable: true,
			text: '<b style="color:blue;">单价</b>',
			width: 70,
			type: 'float',
			//align: 'right',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				decimalPrecision: 3,
				allowBlank: false,
				listeners: {
					focus: function(com, e, eOpts) {
						me.comSetReadOnly(com, e);
					}
				}
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
			dataIndex: 'ReaBmsCenOrderDtl_SumTotal',
			sortable: true,
			text: '总价',
			//align: 'right',
			width: 70,
			type: 'float',
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ProdOrgName',
			sortable: false,
			text: '<b style="color:blue;">品牌</b>',
			width: 90,
			editor: {
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.sysbase.dict.CheckGrid',
				classConfig: {
					title: '品牌选择',
					defaultWhere: "bdict.BDictType.DictTypeCode='" + this.ProdOrg + "'"
				},
				listeners: {
					focus: function(com, e, eOpts) {
						me.comSetReadOnly(com, e);
					},
					check: function(p, record) {
						var records = me.getSelectionModel().getSelection();
						if(records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						records[0].set('ReaBmsCenOrderDtl_ProdOrgName', record ? record.get('BDict_CName') : '');
						me.getView().refresh();
						p.close();
					}
				}
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_GoodsUnit',
			sortable: true,
			text: '单位',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}];

		columns.push({
			dataIndex: 'ReaBmsCenOrderDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ReaGoodsID',
			sortable: false,
			text: '货品Id',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_CompGoodsLinkID',
			sortable: false,
			text: '供货商货品机构关系ID',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_LabcGoodsLinkID',
			sortable: false,
			text: '订货方货品机构关系ID',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_BarCodeType',
			sortable: false,
			text: '货品条码类型',
			hidden: true,
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
			dataIndex: 'ReaBmsCenOrderDtl_IsPrintBarCode',
			sortable: false,
			text: '是否打印条码',
			hidden: true,
			renderer: function(value, meta, record) {
				var v = "";
				if(value == "0") {
					v = "否";
					meta.style = "color:orange;";
				} else if(value == "1") {
					v = "是";
					meta.style = "color:green;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_GoodsSort',
			text: '货品序号',
			width: 90,
			hidden: true,
			defaultRenderer: true
		});
		return columns;
	},
	comSetReadOnly: function(com, e) {
		var me = this;
		var isReadOnly = false;
		if(me.formtype === "show") {
			isReadOnly = true;
		} else {
			isReadOnly = false;
		}
		com.setReadOnly(isReadOnly);
	},
	/**申请数量列是否可编辑处理*/
	onSetReqGoodsQtyReadOnly: function(com, e) {
		var me = this;
		var isReadOnly = false;
		if(me.formtype === "show") {
			isReadOnly = true;
		} else {
			//采购申请或采购申请并审核
			if(me.OTYPE == "entry" || me.OTYPE == "applyandreview") {
				isReadOnly = false;
			} else {
				isReadOnly = true;
			}
		}
		com.setReadOnly(isReadOnly);
	},
	/**审批数量列是否可编辑处理*/
	onSetGoodsQtyReadOnly: function(com, e) {
		var me = this;
		var isReadOnly = false;
		if(me.formtype === "show") {
			isReadOnly = true;
		} else {
			//申请审核或采购申请并审核
			if(me.OTYPE == "check" || me.OTYPE == "applyandreview") {
				isReadOnly = false;
			} else {
				isReadOnly = true;
			}
		}
		com.setReadOnly(isReadOnly);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品中文名/货品平台编码',
			fields: ['reabmscenorderdtl.ReaGoodsName', 'reabmscenorderdtl.GoodsNo']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();

		me.store.removeAll();
		if(!me.PK && me.formtype == "add") return false;

		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		JShell.Action.delay(function() {
			me.setButtonsDisabled(me.buttonsDisabled);
		}, null, 2000);
	},
	onPriceOrGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsCenOrderDtl_Price');
		var GoodsQty = record.get('ReaBmsCenOrderDtl_GoodsQty');

		if(!GoodsQty) GoodsQty = 0;
		if(!Price) Price = 0;
		GoodsQty = parseFloat(GoodsQty);
		Price = parseFloat(Price);
		var SumTotal = Price * GoodsQty;
		var TotalPrice = SumTotal ? SumTotal : 0;
		record.set('ReaBmsCenOrderDtl_SumTotal', TotalPrice);

		var CurrentQty = record.get('ReaBmsCenOrderDtl_CurrentQty');
		if(!CurrentQty) CurrentQty = 0;
		CurrentQty = parseFloat(CurrentQty);
		var ExpectedStock = parseFloat(GoodsQty) + parseFloat(CurrentQty);
		//预期库存量=当次采购数+现有库存量
		record.set('ReaBmsCenOrderDtl_ExpectedStock', ExpectedStock);
		record.commit();
	},
	onReqGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsCenOrderDtl_Price');
		var ReqGoodsQty = record.get('ReaBmsCenOrderDtl_ReqGoodsQty');
		//var GoodsQty = record.get('ReaBmsCenOrderDtl_GoodsQty');
		if(!ReqGoodsQty) ReqGoodsQty = 0;
		if(!Price) Price = 0;
		ReqGoodsQty = parseFloat(ReqGoodsQty);
		Price = parseFloat(Price);
		var SumTotal = Price * ReqGoodsQty;
		var TotalPrice = SumTotal ? SumTotal : 0;
		record.set('ReaBmsCenOrderDtl_GoodsQty', ReqGoodsQty);
		record.set('ReaBmsCenOrderDtl_SumTotal', TotalPrice);
		record.commit();
	},
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	},
	/**按钮的启用或或禁用*/
	setButtonsDisabled: function(disabled) {
		var me = this;
		me.setBtnDisabled("btnAdd", disabled);
		me.setBtnDisabled("btnDel", disabled);
		me.setBtnDisabled("btnSave", disabled);
		//lyj申请状态模板依旧可以选择的bug修改
		me.setBtnDisabled("cboGoodstemplate", disabled);
	}
});