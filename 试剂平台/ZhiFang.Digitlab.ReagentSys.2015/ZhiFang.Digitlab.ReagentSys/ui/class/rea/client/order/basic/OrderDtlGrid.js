/**
 * 订货明细列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.basic.OrderDtlGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '订货明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlByHQL?isPlanish=true',

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
	hasDel: true,
	/**新增明细或删除明细按钮的启用状态*/
	buttonsDisabled: true,
	/**当前选择的供应商Id*/
	ReaCompID: null,
	ReaCompCName:null,
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**是否多选行*/
	checkOne: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			update: function(store, record) {
				me.onPriceOrGoodsQtyChanged(record);
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
		me.columns = me.createGridColumns();
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
			dataIndex: 'BmsCenOrderDtl_ReaGoodsName',
			text: '产品名称',
			sortable: true,
			width: 160,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDtl_GoodsQty',
			text: '<b style="color:blue;">申请数量</b>',
			width: 80,
			type: 'int',
			align: 'right',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			}
		}, {
			dataIndex: 'BmsCenOrderDtl_Price',
			sortable: true,
			text: '<b style="color:blue;">单价</b>',
			width: 80,
			type: 'float',
			align: 'right',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				decimalPrecision: 3,
				allowBlank: false
			}
		}, {
			dataIndex: 'BmsCenOrderDtl_SumTotal',
			sortable: true,
			text: '总计金额',
			align: 'right',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDtl_GoodsUnit',
			sortable: true,
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDtl_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDtl_ProdGoodsNo',
			text: '产品编号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_BiddingNo',
			text: '招标号',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BmsCenOrderDtl_ReaGoodsID',
			sortable: false,
			text: '产品Id',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDtl_OrderGoodsID',
			sortable: false,
			text: '货品机构关系ID',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}];

		return columns;
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
			emptyText: '货品中文名',
			fields: ['bmscenorderdtl.GoodsName']
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
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		JShell.Action.delay(function() {
			me.setButtonsDisabled(me.buttonsDisabled);
		}, null, 1000);
	},
	onPriceOrGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('BmsCenOrderDtl_Price');
		var GoodsQty = record.get('BmsCenOrderDtl_GoodsQty');

		var SumTotal = Price * GoodsQty;
		var TotalPrice = SumTotal ? SumTotal.toFixed(2) : 0;
		record.set('BmsCenOrderDtl_SumTotal', TotalPrice);
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
		me.setBtnDisabled("cboGoodstemplate", disabled);
	}
});