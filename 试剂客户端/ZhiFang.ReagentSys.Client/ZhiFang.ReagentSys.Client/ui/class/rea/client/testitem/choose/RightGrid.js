/**
 * 检验项目选择
 * 右列表不调用服务获取后台数据
 * @author longfc
 * @version 2019-02-14
 */
Ext.define('Shell.class.rea.client.testitem.choose.RightGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '当前选择项目列表',
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
		property: 'ReaTestItem_LisCode',
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
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**用户UI配置Key*/
	userUIKey: 'testitem.RightGrid',
	/**用户UI配置Name*/
	userUIName: "当前选择项目列表",

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
			dataIndex: 'ReaTestItem_CName',
			text: '项目名称',
			sortable: true,
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_EName',
			text: '英文名称',
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_Price',
			text: '价格',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_LisCode',
			text: 'Lis代码',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_ShortCode',
			text: '代码',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_ZX1',
			text: 'ZX1',
			width: 100,
			editor: {},
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_ZX2',
			editor: {},
			text: 'ZX2',
			sortable: true,
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_ZX3',
			editor: {},
			text: 'ZX3',
			sortable: true,
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
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
			handler: function() {
				var records = [];
				me.store.each(function(rec) {
					records.push(rec);
				});
				me.onRemoveClick(records);
			}
		});
		items.push('-', {
			iconCls: 'button-check',
			text: '确定选择',
			tooltip: '确定当前选择并退出',
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