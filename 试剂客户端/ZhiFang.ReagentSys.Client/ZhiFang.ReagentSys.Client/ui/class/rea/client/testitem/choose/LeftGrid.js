/**
 * 检验项目选择
 * 右列表不调用服务获取后台数据
 * @author longfc
 * @version 2019-02-14
 */
Ext.define('Shell.class.rea.client.testitem.choose.LeftGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '待选检验项目列表',
	width: 530,
	height: 620,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**是否带确认按钮*/
	hasAcceptButton: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestItemByHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaTestItem_LisCode',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'testitem.LeftGrid',
	/**用户UI配置Name*/
	userUIName: "待选检验项目列表",

	initComponent: function() {
		var me = this;
		me.addEvents('onBeforeSearch');
		//查询框信息
		me.searchInfo = {
			width: 180,
			isLike: true,
			itemId: 'Search',
			emptyText: '项目名称/英文名称/Lis代码/代码',
			fields: ['reatestitem.CName', 'reatestitem.EName', 'reatestitem.LisCode', 'reatestitem.ShortCode']
		};
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
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
		//me.buttonToolbarItems.unshift();
		me.buttonToolbarItems.push('->', {
			iconCls: 'button-check',
			text: '全部选择',
			tooltip: '将当前页全部选择',
			handler: function() {
				me.onAcceptClick();
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this,
			where = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('Search');
		if(search) {
			var value = search.getValue();
			var searchHql = me.getSearchWhere(value);
			if(searchHql) {
				searchHql = "(" + searchHql + ")";
				where.push(searchHql);
			}
		}
		me.internalWhere = where.join(" and ");
	},
	/**获取外部传入的外部查询条件*/
	setExternalWhere: function(externalWhere) {
		var me = this;
		me.externalWhere = externalWhere;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		me.fireEvent('onBeforeSearch', me);
		//this.load(null, true, autoSelect);
		return me.callParent(arguments);
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
	}
});