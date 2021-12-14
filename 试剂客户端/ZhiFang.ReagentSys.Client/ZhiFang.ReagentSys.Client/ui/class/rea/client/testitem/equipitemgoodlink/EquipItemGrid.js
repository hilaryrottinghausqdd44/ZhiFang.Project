/**
 * 仪器项目关系表
 * Rea_TestEquipItem_仪器项目关系表
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.testitem.equipitemgoodlink.EquipItemGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '仪器项目列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaTestEquipItemEntityListByJoinHql?isPlanish=true',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**默认加载数据*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 1000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**仪器ID*/
	EquipID: null,
	/**后台排序*/
	remoteSort: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.enableControl();
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 155,
			emptyText: 'Lis编码/项目名称/英文名称/简称',
			isLike: true,
			itemId: 'Search',
			fields: ['reatestitem.LisCode', 'reatestitem.CName', 'reatestitem.EName', 'reatestitem.SName']
		};
		me.buttonToolbarItems = [{
			xtype: 'label',
			text: '仪器项目',
			margin: '0 0 0 10',
			style: "font-weight:bold;color:blue;"
		}, '-', 'refresh', '->', {
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaTestEquipItem_TestItemID',
			text: '项目编号',
			hidden: true,
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_ReaTestItem_LisCode',
			text: 'Lis编码',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_ReaTestItem_CName',
			text: '项目名称',
			minWidth: 150,
			flex: 1,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_ReaTestItem_SName',
			text: '项目简称',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_ReaTestItem_EName',
			text: '英文名称',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_TestEquipID',
			text: '仪器Id',
			width: 150,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestEquipItem_Id',
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
			EquipID = null,
			params = [];
		me.internalWhere = '';
		if(me.EquipID) {
			params.push('reatestequipitem.TestEquipID=' + me.EquipID);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		var url = me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('Search');
		if(search && search.getValue()) {
			url = url + '&reatestitemHql=(' + me.getSearchWhere(search.getValue()) + ')';
		}
		return url;
	}
});