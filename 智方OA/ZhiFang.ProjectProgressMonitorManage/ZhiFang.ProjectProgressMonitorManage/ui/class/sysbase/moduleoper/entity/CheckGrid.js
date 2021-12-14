/**
 * 实体对象选择列表
 * @author longfc
 * @version 2017-05-19
 */
Ext.define('Shell.class.sysbase.moduleoper.entity.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '实体对象选择',
	width: 480,
	height: 560,

	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_GetEntityList',
	//defaultOrderBy:[{property:'FDict_DispOrder',direction:'ASC'}],
	/**是否单选*/
	checkOne: true,

	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 245,
			emptyText: '名称',
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
			text: '中文名称',
			dataIndex: 'CName',
			width: 120,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '对象名称',
			dataIndex: 'ClassName',
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
	},
	/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = 'CName',
			where = "";

		if(isLike) {
			where = fields + " like " + value + "";
		} else {
			where = fields + "=" + value + "";
		}
		return where;
	}
});