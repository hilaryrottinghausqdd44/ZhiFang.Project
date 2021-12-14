/**
 * 服务URL选择列表
 * @author longfc
 * @version 2017-05-19
 */
Ext.define('Shell.class.sysbase.moduleoper.service.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '服务URL选择',
	width: 480,
	height: 560,

	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_GetServiceUrlList',
	//defaultOrderBy:[{property:'FDict_DispOrder',direction:'ASC'}],
	/**是否单选*/
	checkOne: true,

	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 245,
			emptyText: '名称',
			hidden: true,
			isLike: true,
			fields: ['CName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '名称',
			dataIndex: 'CName',
			width: 120,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '服务URL',
			dataIndex: 'ServiceURL',
			flex: 1,
			minWidth: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '描述',
			dataIndex: 'Description',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}];

		return columns;
	}
});