/**
 * 实体编码选择列表
 * @author longfc
 * @version 2017-06-21
 */
Ext.define('Shell.class.sysbase.rowfilter.preconditions.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '实体编码选择列表',
	width: 270,
	height: 480,

	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACPreconditionsByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'RBACPreconditions_EntityCode',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '所属实体编码',
			isLike: true,
			fields: ['rbacpreconditions.EntityCName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '所属实体编码',
			dataIndex: 'RBACPreconditions_EntityCName',
			flex: 1,
			minWidth: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '实体编码',
			dataIndex: 'RBACPreconditions_EntityCode',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		if(!data || data.list.length == 0) return data;
		var newList = [],
			tempArr = [];
		var list = data.list;
		for(var i = 0; i < list.length; i++) {
			var entityCode = list[i]['RBACPreconditions_EntityCode']
			if(tempArr.indexOf(entityCode) == -1) {
				tempArr.push(entityCode);
				newList.push(list[i]);
			}
		}
		data.list = newList;
		return data;
	}
});