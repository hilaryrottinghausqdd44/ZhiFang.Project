/**
 * 结转报表
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.monthly.basic.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.DtlGrid',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.TextSearchTrigger'
	],
	title: '结转报表明细信息',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchQtyMonthBalanceDtlListById?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**用户UI配置Key*/
	userUIKey: 'monthly.basic.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "库存结转报表明细列表",
	features: [{
		ftype: 'summary'
	}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.createFullscreenItems();
		items.push('-', 'refresh');
		//查询框信息
		me.searchInfo = null;

		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_GoodsName',
			text: '货品',
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_LotNo',
			text: '货品批号',
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_GoodsUnit',
			text: '单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_UnitMemo',
			text: '规格',
			width: 90,
			defaultRenderer: true
		}, {
			text: '初始库存数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_PreMonthQty',
			width: 80,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '入库总数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_InQty',
			width: 80,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		},{
			text: '库存初始化数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_AvailabilityQty',
			width: 80,
			hidden:true,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		},{
			text: '验货入库数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_ComfirmInQty',
			width: 80,
			hidden:true,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '移库入库数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_TransferInQty',
			width: 80,
			hidden:true,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		},{
			text: '盘盈入库数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_SurplusInQty',
			width: 80,
			hidden:true,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '退库入库数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_OutOfInQty',
			width: 80,
			hidden:true,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '移库出库数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_TransferOutQty',
			width: 80,
			hidden:true,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '仪器使用数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_EquipQty',
			width: 80,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '退供应商数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_ReturnQty',
			width: 80,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '调帐出库数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_AdjustmentOutQty',
			width: 80,
			hidden:true,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '库存报损数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_LossQty',
			width: 80,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		},{
			text: '盘亏出库数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_DiskLossQty',
			width: 80,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '剩余库存数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_MonthQty',
			width: 85,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '计算库存数',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_CalcGoodsQty',
			width: 85,
			renderer: function(value, meta) {
				var v = value || '0';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				}
				return v;
			}
		}, {
			text: '库存金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_MonthQtyPrice',
			width: 140,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			text: '初始金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_PreMonthQtyPrice',
			width: 140,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			text: '入库总金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_InQtyPrice',
			width: 100,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			text: '库存初始化金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_AvailabilityPrice',
			width: 100,
			hidden: true,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		},{
			text: '验货入库金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_ComfirmInPrice',
			width: 100,
			hidden: true,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		},{
			text: '移库入库金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_TransferInPrice',
			width: 100,
			hidden: true,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		},{
			text: '退库入库金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_OutOfInPrice',
			width: 100,
			hidden: true,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		},{
			text: '盘盈入库金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_SurplusInPrice',
			width: 100,
			hidden: true,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		},{
			text: '移库出库金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_TransferOutPrice',
			width: 90,
			hidden: true,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			text: '仪器使用金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_EquipPrice',
			width: 100,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			text: '退供应商金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_ReturnPrice',
			width: 80,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			text: '报损数金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_LossQtyPrice',
			width: 80,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			text: '盘亏出库金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_DiskLossQtyPrice',
			width: 80,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			text: '调帐出库金额',
			dataIndex: 'ReaBmsQtyMonthBalanceDtl_AdjustmentOutQtyPrice',
			width: 90,
			hidden: true,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}];

		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if(!me.PK) return;

		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		if(me.PK) url = url + "&id=" + me.PK;

		return url;
	}
});