/**
 * @description 部门采购申请录入申请明细列表
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.basic.ReqDtlCheck', {
	extend: 'Shell.class.rea.client.apply.basic.ApplyDtGrid',

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
		me.CurDeptId = me.CurDeptId || "";
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.addEvents('onCheck');
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
		var columns = [
		{
			dataIndex: 'ReaBmsReqDtl_GoodsSName',
			text: '简称',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_GoodsEName',
			text: '英文',
			width: 85,
			defaultRenderer: true
		},{
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
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			iconCls: 'button-refresh',
			text: '刷新',
			tooltip: '刷新操作',
			handler: function() {
				me.store.removeAll();
				me.onSearch();
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-del',
			text: '移除',
			tooltip: '删除操作',
			handler: function() {
				me.onRemoveDt(null);
			}
		});
		items.push('-', {
			iconCls: 'button-check',
			text: '确定选择',
			tooltip: '确定当前货品选择',
			handler: function() {
				me.onCheck();
			}
		});

		items.push('-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '同步库存',
			tooltip: '同步库存',
			hidden: true,
			handler: function() {
				me.onCurrentQtyClick();
			}
		});
		return items;
	},
	onCheck: function() {
		var me = this;
		me.fireEvent('onCheck', me);
	},
	/**@description 移除处理方法*/
	onRemoveDt: function(records) {
		var me = this;
		if(!records) records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = records.length;
		var showMask = false;
		for(var i in records) {
			var id = records[i].get(me.PKField);
			if(!id || id == "-1") {
				me.delCount++;
				me.store.remove(records[i]);
				if((me.delCount + me.delErrorCount) == me.delLength && me.delErrorCount == 0) {
					me.fireEvent('onDelAfter', me);
				}
			} else {
				if(showMask == false) {
					showMask = true;
					me.showMask(me.delText); //显示遮罩层
				}
				me.delOneById(records[i], i, id);
			}
		}
	}
});