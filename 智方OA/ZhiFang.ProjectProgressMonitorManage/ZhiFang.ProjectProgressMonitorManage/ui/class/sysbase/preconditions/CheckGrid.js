/**
 * 预置条件项选择列表
 * @author longfc
 * @version 2017-08-22
 */
Ext.define('Shell.class.sysbase.preconditions.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '预置条件项选择列表',
	width: 270,
	height: 480,

	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACPreconditionsByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'RBACPreconditions_DispOrder',
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
		me.defaultWhere += 'rbacpreconditions.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '预置条件名称',
			isLike: true,
			fields: ['rbacpreconditions.CName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '所属模块服务',
			dataIndex: 'RBACPreconditions_RBACModuleOper_CName',
			width: 200,
			sortable: false,
			
			hideable: true,
			defaultRenderer: true
		}, {
			text: '所属实体编码',
			dataIndex: 'RBACPreconditions_EntityCName',
			width: 100,
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '预置条件',
			dataIndex: 'RBACPreconditions_CName',
			flex: 1,
			minWidth: 200,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '预置条件代码',
			dataIndex: 'RBACPreconditions_EName',
			width: 160,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '值类型',
			dataIndex: 'RBACPreconditions_ValueType',
			width: 100,
			hidden: true,
			sortable: false,
			hideable: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				switch(value) {
					case "date":
						value = "日期型";
						break;
					case "boolean":
						value = "布尔勾选";
						break;
					case "number":
						value = "数值型";
						break;
					case "string":
						value = "字符串";
						break;
					default:
						break;
				}
				return value;
			}
		}, {
			text: '实体编码',
			dataIndex: 'RBACPreconditions_EntityCode',
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'RBACPreconditions_DispOrder',
			width: 50,
			hidden: true,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}, {
			text: '主键ID',
			dataIndex: 'RBACPreconditions_Id',
			isKey: true,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '所属模块服务ID',
			dataIndex: 'RBACPreconditions_RBACModuleOper_Id',
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}];

		return columns;
	}
});