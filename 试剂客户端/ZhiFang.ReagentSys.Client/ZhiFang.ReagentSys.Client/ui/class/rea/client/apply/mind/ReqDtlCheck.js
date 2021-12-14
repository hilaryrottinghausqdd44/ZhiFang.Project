/**
 * @description 部门采购申请录入申请明细列表
 * @author liuyj
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.mind.ReqDtlCheck', {
	extend: 'Shell.class.rea.client.apply.basic.ReqDtlCheck',

	title: '已选货品明细',
	width: 445,
	height: 500,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 150,
	
	/**用户UI配置Key*/
	userUIKey: 'apply.basic.ReqDtlCheck',
	/**用户UI配置Name*/
	userUIName: "已选货品明细列表",
	
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.onRemoveDt([record]);
			}
		});
	},
	initComponent: function() {
		var me = this;
		/* me.CurDeptId = me.CurDeptId || "";
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.addEvents('onCheck');
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems(); */
		//数据列
		me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsReqDtl_ReaGoodsNo',
			text: '货品编码',
			width: 65,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_GoodsID',
			text: '货品名Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsReqDtl_DispOrder',
			text: '显示次序',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_GoodsCName',
			text: '货品名称',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_ReaCenOrg_Id',
			text: '供应商Id',
			hidden: true
		}];
	
		columns.push({
			dataIndex: 'ReaBmsReqDtl_MonthlyUsage',
			text: '理论月用量',
			width: 75,
			sortable: false,
			defaultRenderer: true
		}, me.createCurrentQtyColumn());
		columns.push(me.createReaCenOrgColumn());
		columns.push({
			dataIndex: 'ReaBmsReqDtl_AvgUsedQty',
			text: '<b style="color:blue;">平均使用量</b>',
			width: 70,
			sortable: false
		},{
			dataIndex: 'ReaBmsReqDtl_SuggestPurchaseQty',
			text: '<b style="color:blue;">建议采购量</b>',
			width: 70,
			sortable: false
		});
		columns.push(me.createReqGoodsQtyColumn());
		columns.push({
			dataIndex: 'ReaBmsReqDtl_Price',
			text: '供货商价格',
			hidden: true,
			width: 75,
			sortable: false,
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
			dataIndex: 'ReaBmsReqDtl_SumTotal',
			text: '合计金额',
			hidden: true,
			width: 75,
			sortable: false,
			type: 'float',
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		});
		columns.push(me.createGoodsQtyColumn(), {
			dataIndex: 'ReaBmsReqDtl_ExpectedStock',
			text: '预期库存量',
			hidden: true,
			width: 75,
			sortable: false,
			defaultRenderer: true
		});
		columns.push({
			dataIndex: 'ReaBmsReqDtl_GoodsUnitID',
			text: '包装单位Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_GoodsUnit',
			text: '包装单位',
			width: 65,
			//hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_UnitMemo',
			text: '单位描述',
			width: 65,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_ProdID',
			text: '厂家ID',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_ProdOrgName',
			text: '生产厂家',
			width: 75,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			width: 65,
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_Memo',
			sortable: false,
			text: '<b style="color:blue;">备注</b>',
			width: 60,
			hidden: false,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		}, {
			//自定义
			dataIndex: 'SuitableType',
			text: '适用机型',
			width: 65,
			sortable: false,
			defaultRenderer: true
		});
		return columns;
	}
});