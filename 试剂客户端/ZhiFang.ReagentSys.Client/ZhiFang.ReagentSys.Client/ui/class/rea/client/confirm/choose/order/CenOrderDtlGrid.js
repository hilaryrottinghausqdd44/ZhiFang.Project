/**
 * 订单明细
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.confirm.choose.order.CenOrderDtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '订单明细列表',
	width: 800,
	height: 500,
	
	
	/**查询数据*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaOrderDtlOfConfirmVOByHQL?isPlanish=true',

	/**默认加载数据*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**默认每页数量*/
	defaultPageSize: 50,
	defaultDisableControl: false,
	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**主单ID*/
	OrderDocID: null,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.choose.order.CenOrderDtlGrid',
	/**用户UI配置Name*/
	userUIName: "订单验收订单明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},

	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			dataIndex: 'ReaOrderDtlOfConfirmVO_BarCodeType',
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
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoodsNo',
			sortable: false,
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoodsName',
			text: '货品名称',
			width: 145,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaOrderDtlOfConfirmVO_BarCodeType");
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
			dataIndex: 'ReaBmsCenOrderDtl_GoodsQty',
			text: '订货总数',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReceivedRejectedCount',
			text: '已验收',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReceivedCount',
			style: 'font-weight:bold;color:#fff;background:#5cb85c;',
			text: '已接收',
			width: 60,
			hidden: true,
			type: 'float',
			align: 'center'
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_RejectedCount',
			text: '已拒收',
			hidden: true,
			style: 'font-weight:bold;color:#fff;background:#c9302c;',
			width: 60,
			type: 'float',
			align: 'center'
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ConfirmCount',
			text: '可验收',
			width: 60,
			type: 'float',
			align: 'center'
		});
		return columns;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this,
			result = {},
			list = [],
			arr = [];
		for(var i = 0; i < data.list.length; i++) {
			var ItemPrice = 0;
			var ReceivedCount = data.list[i].ReaOrderDtlOfConfirmVO_ReceivedCount;
			var RejectedCount = data.list[i].ReaOrderDtlOfConfirmVO_RejectedCount;
			if(!ReceivedCount) ReceivedCount = 0;
			if(!RejectedCount) RejectedCount = 0;
			var count = Number(ReceivedCount) + Number(RejectedCount);
			if(!count) count = 0;
			var obj1 = {
				ReaOrderDtlOfConfirmVO_ReceivedRejectedCount: count
			};
			var obj2 = Ext.Object.merge(data.list[i], obj1);
			arr.push(obj2);
		}
		result.list = arr;
		return result;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		if(!me.OrderDocID) {
			var error = me.errorFormat.replace(/{msg}/, "没有选择待验收的订货单!");
			me.getView().update(error);
			return false;
		}
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
		//me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,
			params = [];
		me.internalWhere = '';
		if(me.OrderDocID) {
			params.push('reabmscenorderdtl.OrderDocID=' + me.OrderDocID);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere ="("+ me.getSearchWhere(search)+")";
			}
		}
		return me.callParent(arguments);
	}
});