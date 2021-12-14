/**
 * 预置条件项的行过滤条件选择
 * @author longfc
 * @version 2017-08-22
 */
Ext.define('Shell.class.sysbase.rowfilter.precondition.copy.RowfilterGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '预置条件项的行过滤条件选择',
	width: 270,
	height: 480,

	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACRowFilterByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'RBACRowFilter_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,

	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'rbacrowfilter.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: "78%",
			emptyText: '行条件名称',
			isLike: true,
			fields: ['rbacrowfilter.CName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '行条件名称',
			dataIndex: 'RBACRowFilter_CName',
			flex: 1,
			minWidth: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'RBACRowFilter_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}];
		return columns;
	}
});