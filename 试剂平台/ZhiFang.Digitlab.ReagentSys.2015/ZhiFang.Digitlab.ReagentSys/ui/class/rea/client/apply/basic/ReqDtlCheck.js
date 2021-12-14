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
		//创建数据集
		me.store = me.createStore();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsReqDtl_GoodsID',
			text: '品名Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsReqDtl_OrderGoodsID',
			text: '货品机构关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_GoodsCName',
			text: '货品名(包装规格)',
			width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_ReaCenOrg_Id',
			text: '供应商Id',
			hidden: true
		}];
		columns.push(me.createReaCenOrgColumn());
		columns.push(me.createGoodsQtyColumn());
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
			dataIndex: 'GoodsOtherQty',
			text: '货品对应同系列的库存数量',
			width: 65,
			hidden: true,
			defaultRenderer: true
		});
		columns.push(me.createCurrentQtyColumn());
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
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品名',
			fields: ['reabmsreqdtl.GoodsCName']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		}, '-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '同步库存',
			tooltip: '同步库存',
			handler: function() {
				me.onCurrentQtyClick();
			}
		});
		return items;
	},
	onCheck:function() {
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
					me.fireEvent('onDeleted', me);
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