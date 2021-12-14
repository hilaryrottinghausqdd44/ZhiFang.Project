/**
 * 库存货品选择
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.add.choose.RightGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '当前已选择盘库货品明细',
	width: 530,
	height: 620,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	/**获取数据服务路径/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true*/
	selectUrl: '',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCheckDtl_StorageID',
		direction: 'ASC'
	}, {
		property: 'ReaBmsCheckDtl_PlaceID',
		direction: 'ASC'
	},  {
		property: 'ReaBmsCheckDtl_ReaGoodsNo',
		direction: 'ASC'
	},{
		property: 'ReaGoods_DispOrder',
		direction: 'ASC'
	},{
		property: 'ReaBmsCheckDtl_LotNo',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**是否调用服务从后台获取数据*/
	remoteLoad: false,
	/**查询框信息*/
	searchInfo: null,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**用户UI配置Key*/
	userUIKey: 'inventory.add.choose.RightGrid',
	/**用户UI配置Name*/
	userUIName: "已选盘库货品明细列表",
	
	//closable:true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.onRemoveClick([record]);
			}
		});
	},
	initComponent: function() {
		var me = this;

		me.addEvents('onAccept', 'onRemove', 'onRefresh');
		//自定义按钮功能栏
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
			dataIndex: 'ReaBmsCheckDtl_ReaGoodsNo',
			text: '货品编码',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsSort',
			text: '货品序号',
			hidden:true,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_DispOrder',
			text: '显示次序',
			hidden:true,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ProdGoodsNo',
			text: '厂商货品编码',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsNo',
			text: '货品平台编码',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_BarCodeType',
			text: '条码类型',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsName',
			text: '货品名称',
			width: 120,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsCheckDtl_BarCodeType");
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
			dataIndex: 'ReaBmsCheckDtl_GoodsSName',
			text: '简称',
			width: 90,
			defaultRenderer: true,
			sortable: false
		}, {
			dataIndex: 'ReaBmsCheckDtl_ProdOrgName',
			text: '品牌',
			width: 90,
			defaultRenderer: true,
			sortable: false
		},{
			dataIndex: 'ReaBmsCheckDtl_GoodsUnit',
			text: '包装单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_UnitMemo',
			text: '包装规格',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_StorageName',
			text: '所属库房',
			width: 85,
			//hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_PlaceName',
			text: '所属货架',
			width: 75,
			//hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_LotNo',
			text: '货品批号',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_SumTotal',
			sortable: false,
			text: '库存总计',
			align: 'right',
			type: 'float',
			width: 75,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsQty',
			text: '库存数',
			width: 75,
			type: 'float',
			align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_Price',
			text: '平均价格',
			width: 75,
			type: 'float',
			align: 'center',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_CheckQty',
			text: '<b style="color:blue;">实盘数</b>',
			width: 75,
			type: 'float',
			align: 'center',
			hidden: true,
			editor: {
				xtype: 'numberfield',
				listeners: {
					focus: function(field, e, eOpts) {
						field.setReadOnly(me.CheckQtyReadOnly);
					}
				}
			},
			renderer: function(value, meta, record) {
				var checkQty = parseFloat(value);
				var goodsQty = parseFloat(record.get("ReaBmsCheckDtl_GoodsQty"));

				if(checkQty < goodsQty) {
					var bColor = "";
					var fColor = "red";
					var style = 'font-weight:bold;';
					if(bColor)
						style = style + "background-color:" + bColor + ";";
					if(fColor)
						style = style + "color:" + fColor + ";";
					meta.tdAttr = 'data-qtip="<b>' + checkQty + '</b>"';
					meta.style = style;
				}
				return value;
			}
		}, {
			//xtype: 'checkcolumn',
			dataIndex: 'ReaBmsCheckDtl_IsException',
			text: '是否异常',
			width: 65,
			align: 'center',
			hidden: true,
			renderer: function(value, meta) {
				var v = value + '';
				if(v == '1') {
					meta.style = 'color:red';
					v = JShell.All.TRUE;
				} else if(v == '0') {
					meta.style = 'color:green';
					v = JShell.All.FALSE;
				} else {
					v == '';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCheckDtl_IsHandleException',
			text: '异常是否已处理',
			width: 100,
			align: 'center',
			hidden: true,
			renderer: function(value, meta) {
				var v = value + '';
				if(v == '1') {
					meta.style = 'color:green';
					v = JShell.All.TRUE;
				} else if(v == '0') {
					meta.style = 'color:red';
					v = JShell.All.FALSE;
				} else {
					v == '';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCheckDtl_Memo',
			sortable: false,
			text: '<b style="color:blue;">备注信息</b>',
			width: 120,
			//hidden: true,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_CompanyName',
			text: '所属供应商',
			hidden: true,
			width: 105,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_CheckDocID',
			text: '盘库单ID',
			hidden: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ReaCompanyID',
			sortable: false,
			text: '供应商ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ReaCompCode',
			sortable: false,
			text: '供应商机构码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ReaServerCompCode',
			sortable: false,
			text: '供应商机平台构码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_StorageID',
			sortable: false,
			text: '库房ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_PlaceID',
			sortable: false,
			text: '货架ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsID',
			sortable: false,
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_CompGoodsLinkID',
			sortable: false,
			text: '货品机构关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_LotSerial',
			text: '一维批号条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_LotQRCode',
			text: '试剂二维批条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_GoodsLotID',
			text: '批号ID',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ProdDate',
			text: '生产日期',
			hidden: true,
			isDate: true,
			hasTime: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_InvalidDate',
			text: '有效期',
			hidden: true,
			isDate: true,
			hasTime: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ZX1',
			text: 'ZX1',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ZX2',
			text: 'ZX2',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDtl_ZX3',
			text: 'ZX3',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = []; //'->'
		items.push({
			xtype: 'button',
			iconCls: 'button-del',
			text: '选择移除',
			hidden: true,
			tooltip: '移除列表选择的行',
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				me.onRemoveClick(records);
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-del',
			text: '全部移除',
			tooltip: '移除列表选择的行',
			hidden: false,
			handler: function() {
				var records = [];
				me.store.each(function(rec) {
					records.push(rec);
				});
				me.onRemoveClick(records);
			}
		});
		items.push({
			iconCls: 'button-check',
			text: '确定选择',
			tooltip: '确定当前选择并退出',
			hidden: true,
			handler: function() {
				me.onAcceptClick();
			}
		});

		return items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		//是否调用服务从后台获取数据
		if(!me.remoteLoad) return false;

		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		var records = [];
		me.store.each(function(rec) {
			records.push(rec);
		});
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.fireEvent('onAccept', me, records);
	},
	onRemoveClick: function(records) {
		var me = this;
		if(!records) records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onRemove', me, records);
	},
	onRefreshClick: function() {
		var me = this;
		me.fireEvent('onRefresh', me);
	}
});