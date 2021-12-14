/**
 * @description 订单查看
 * @author longfc
 * @version 2017-11-21
 */
Ext.define('Shell.class.rea.client.order.show.OrderDtlGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderDtlGrid',
	title: '订货明细列表',
	/**是否多选行*/
	checkOne: true,
	/**用户UI配置Key*/
	userUIKey: 'order.show.OrderDtlGrid',
	/**用户UI配置Name*/
	userUIName: "订单查看明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		if(!me.checkOne) me.setCheckboxModel();
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
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
			hidden: true,
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
			dataIndex: 'ReaBmsCenOrderDtl_MonthlyUsage',
			text: '月用量',
			width: 75,
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
			text: '到货时间',
			width: 85,
			sortable: false,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ReqGoodsQty',
			text: '申请数',
			width: 75,
			type: 'float',
			align: 'right',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_GoodsQty',
			text: '实批数',
			width: 75,
			type: 'float',
			align: 'right',
			defaultRenderer: true
		},  {
			dataIndex: 'ReaBmsCenOrderDtl_InStorageQty',
			text: '已入库数',
			width: 75,
			type: 'float',
			align: 'right',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_NotInStorageQty',
			text: '未入库数',
			width: 75,
			type: 'float',
			align: 'right',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_Price',
			sortable: true,
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
			dataIndex: 'ReaBmsCenOrderDtl_SumTotal',
			sortable: true,
			text: '总价',
			align: 'right',
			width: 80,
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
			text: '品牌',
			width: 90,
			defaultRenderer: true
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
		});
		return columns;
	},
	setGoodstemplateClassConfig: function() {
		var me = this;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		url = url + "&orderDocId=" + me.PK;
		return url;
	}
});