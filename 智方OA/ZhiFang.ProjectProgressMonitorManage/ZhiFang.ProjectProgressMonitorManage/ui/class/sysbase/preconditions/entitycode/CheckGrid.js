/**
 * 实体编码选择列表
 * @author longfc
 * @version 2017-06-21
 */
Ext.define('Shell.class.sysbase.preconditions.entitycode.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '实体编码选择列表',
	width: 270,
	height: 480,

	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchBaseEntityAttribute',
	defaultOrderBy: [{
		property: 'CName',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,
	EntityName:'',
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 100,
	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '属性显示名称',
			dataIndex: 'CName',
			flex: 1,
			minWidth: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '属性值类型',
			dataIndex: 'ValueType',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '属性名称',
			dataIndex: 'InteractionField',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'EntityName=' + me.EntityName;

		return url;
	},
});