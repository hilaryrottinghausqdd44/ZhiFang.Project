/**
 * 产品选择
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.admin.cenorg.CenOrgGrid', {
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	extend: 'Shell.ux.grid.Panel',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaCenOrg_OrgType',
		direction: 'ASC'
	},{
		property: 'ReaCenOrg_OrgNo',
		direction: 'ASC'
	}],
	/**机构类型*/
	OrgType: [
		['', '全部'],
		['0', '供货方'],
		['1', '订货方']
	],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reacenorg.Visible=1';
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			fieldLabel: '类型',
			width: 110,
			labelWidth: 40,
			name: 'ReaCenOrgCenOrgType',
			itemId: 'ReaCenOrgCenOrgType',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			data: me.OrgType
		}];

		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '中文名/英文名/机构编码',
			isLike: true,
			itemId: 'Search',
			fields: ['reacenorg.CName', 'reacenorg.EName','reacenorg.OrgNo']
		};
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});

		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaCenOrg_OrgType',
			text: '机构类型',
			width: 60,
			renderer: function(value, meta, record, rowIndex, colIndex) {
				var v = "";
				if(value == "0") {
					v = "供货方";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "订货方";
					meta.style = "color:orange;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaCenOrg_CName',
			text: '机构名称',
			width: 160,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_OrgNo',
			text: '机构编码',
			width: 80,
			type: 'int',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_CenterOrgNo',
			text: '平台机构编码',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_EName',
			text: '英文名',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_Contact',
			text: '联系人',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			OrgType = buttonsToolbar.getComponent('ReaCenOrgCenOrgType');
		OrgType.on({
			change: function() {
				me.onSearch();
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			OrgType = buttonsToolbar.getComponent('ReaCenOrgCenOrgType'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];

		if(OrgType && OrgType.getValue()) {
			params.push('reacenorg.OrgType=' + OrgType.getValue());
		}
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		}
		return me.callParent(arguments);
	}
});