/**
 * 产品供货维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.cenorg.basic.CenOrgGrid', {
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	extend: 'Shell.ux.grid.Panel',
	
	width: 360,
	
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
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];

		//查询框信息
		me.searchInfo = {
			width: 205,
			emptyText: '机构名称/机构编码',
			isLike: true,
			itemId: 'Search',
			fields: ['reacenorg.CName','reacenorg.OrgNo']// 'reacenorg.EName',
		};
		items.push('refresh','-', {
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
			hidden:true,
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
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_OrgNo',
			text: '机构编码',
			width: 100,
			//type: 'int',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_PlatformOrgNo',
			text: '平台机构编码',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_EName',
			text: '英文名',
			width: 80,
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
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		}
		
		return me.callParent(arguments);
	}
});