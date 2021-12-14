/**
 * 客户端验收:实验室订单导入
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.choose.order.OrderDtlCheck', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	
	title: '订货明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaOrderDtlOfConfirmVOByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**是否多选行*/
	checkOne: false,
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.choose.order.OrderDtlCheck',
	/**用户UI配置Name*/
	userUIName: "订单验收订单明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('setDocConfirmEntity', 'onCheck');
		me.buttonToolbarItems = me.createButtonToolbarItems();
		if(!me.checkOne) me.setCheckboxModel();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
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
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ProdGoodsNo',
			sortable: false,
			text: '厂商货品编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_CenOrgGoodsNo',
			sortable: false,
			text: '供应商货品编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_GoodsNo',
			sortable: false,
			text: '货品平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoodsName',
			text: '货品名称',
			width: 275,
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
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoods_SName',
			text: '简称',
			width: 60,
			hidden: true,
			align: 'center'
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoods_EName',
			text: '英文名称',
			width: 60,
			hidden: true,
			align: 'center'
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_GoodsQty',
			text: '订单数',
			width: 60,
			type: 'float',
			align: 'center'
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ConfirmCount',
			text: '可验收',
			width: 60,
			hidden: true,
			type: 'float',
			align: 'center'
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReceivedCount',
			style: 'font-weight:bold;color:#fff;background:#5cb85c;',
			text: '已接收',
			width: 60,
			type: 'float',
			align: 'center'
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_RejectedCount',
			text: '已拒收',
			style: 'font-weight:bold;color:#fff;background:#c9302c;',
			width: 60,
			type: 'float',
			align: 'center'
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_Price',
			hidden: true,
			text: '单价',
			width: 60,
			type: 'float',
			align: 'center'
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_SumTotal',
			hidden: true,
			text: '总计金额',
			align: 'center',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_GoodsUnit',
			sortable: true,
			text: '包装单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_UnitMemo',
			text: '包装规格',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_BiddingNo',
			hidden: true,
			text: '招标号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoodsID',
			sortable: false,
			text: '货品Id',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_CompGoodsLinkID',
			sortable: false,
			text: '供货商货品机构关系ID',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoods_ApproveDocNo',
			sortable: false,
			text: '批准文号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoods_RegistNo',
			sortable: false,
			text: '注册号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoods_RegistDate',
			hidden: true,
			text: '注册日期',
			align: 'center',
			hidden: true,
			type: 'date',
			isDate: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoods_RegistNoInvalidDate',
			hidden: true,
			text: '注册证有效期',
			align: 'center',
			hidden: true,
			type: 'date',
			isDate: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_AcceptFlag',
			text: '已验收标志',
			hidden: true,
			align: 'center'
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_OrderDocID',
			sortable: false,
			text: '订单总单Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_OrderDocNo',
			sortable: false,
			text: '订单总单号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr',
			sortable: false,
			text: '验货明细条码关系集合Str',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoods_GoodsSort',
			text: '货品序号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_LotNo',
			text: '批号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ProdDate',
			text: '批号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_InvalidDate',
			text: '失效日期',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_RegisterNo',
			text: '注册证号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_StorageType',
			text: '温度保存条件',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaCompID',
			text: '供货商Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_CompanyName',
			text: '供货商',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaServerCompCode',
			text: '供货商平台机构码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaCompCode',
			text: '供货商机构码',
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaOrderDtlOfConfirmVO_ReaGoods_IsNeedPerformanceTest',
			sortable: false,
			text: '注册号',
			hidden: true,
			defaultRenderer: true
		} ];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		items.push('refresh');
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnChooseOrder",
			disabled: (!me.PK ? false : true), //订单已存在
			text: '订单选择',
			tooltip: '订单选择',
			handler: function() {
				me.onChooseOrder();
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '确认选择',
			tooltip: '确认选择',
			handler: function() {
				me.onCheck();
			}
		});

		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品中文名',
			fields: ['reabmscenorderdtl.ReaGoodsName']
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
		if(!me.PK) return false;
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	onChooseOrder: function() {
		var me = this;
		me.showChooseOrder();
	},
	onCheck: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var tempArr = [];
		Ext.Array.each(records, function(record) {
			tempArr.push(record.data);
		});
		me.fireEvent("onCheck", me, tempArr);
	},
	/**订单导入*/
	showChooseOrder: function() {
		var me = this;
		//var maxWidth = document.body.clientWidth * 0.82;
		var height = document.body.clientHeight * 0.72;
		var config = {
			resizable: true,
			SUB_WIN_NO: '1', //内部窗口编号
			width: 860,
			height: height,
			listeners: {
				accept: function(p, record) {
					me.fireEvent("setDocConfirmEntity", record);
					if(record) {
						me.PK = record.get("ReaBmsCenOrderDoc_Id");
						me.loadDataByOrderId();
					} else {
						me.PK = null;
						me.formtype = "show";
						me.defaultWhere = "";
						me.Status = null;
						me.store.removeAll();
						me.fireEvent('nodata', me);
					}
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.confirm.choose.order.OrderCheck', config);
		win.show();
	},
	loadDataByOrderId: function(id) {
		var me = this;
		if(id) me.PK = id;
		var defaultWhere = "";
		if(me.PK) defaultWhere = "reabmscenorderdtl.OrderDocID=" + me.PK;
		me.defaultWhere = defaultWhere;
		me.onSearch();
	},
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		me.setBtnDisabled("btnChooseOrder", true);
	},
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	}
});