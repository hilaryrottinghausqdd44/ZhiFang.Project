/**
 * 供货方条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.CenOrgGrid', {
	extend: 'Shell.ux.grid.Panel',
	
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
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
		property: 'ReaCenOrg_PlatformOrgNo',
		direction: 'ASC'
	}],
	
	//供货方
	defaultWhere: 'reacenorg.Visible=1 and reacenorg.OrgType=0',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
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
			emptyText: '机构名称/平台机构编码',
			isLike: true,
			itemId: 'Search',
			fields: ['reacenorg.CName', 'reacenorg.PlatformOrgNo'] // 'reacenorg.EName',
		};
		items.push('refresh', '-', {
			type: 'search',
			info: me.searchInfo
		});

		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaCenOrg_CName',
			text: '机构名称',
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_OrgNo',
			text: '机构编码',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_PlatformOrgNo',
			text: '平台机构编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_EName',
			text: '英文名',
			width: 80,
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
	}
});