/**
 * 机构选择列表
 * @author longfc
 * @version 2017-11-16
 */
Ext.define('Shell.class.rea.client.order.CenOrgCheck', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '机构选择列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 565,
	height: 420,
	/**是否单选*/
	checkOne: true,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaCenOrg_DispOrder',
		direction: 'ASC'
	}],
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reacenorg.Visible=1 and reacenorg.OrgType=0';
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '机构名称/机构编码/英文名',
			fields: ['reacenorg.CName', 'reacenorg.EName', 'reacenorg.OrgNo']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaCenOrg_CName',
			text: '机构名称',
			sortable: true,
			minWidth: 260,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_OrgNo',
			sortable: true,
			text: '机构编码',
			width: 90,
			type: 'int',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_CenterOrgNo',
			sortable: true,
			text: '平台机构编码',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenOrg_EName',
			sortable: true,
			text: '英文名',
			width: 80,
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		me.internalWhere = params.join(' and ');
		return me.callParent(arguments);
	}
});