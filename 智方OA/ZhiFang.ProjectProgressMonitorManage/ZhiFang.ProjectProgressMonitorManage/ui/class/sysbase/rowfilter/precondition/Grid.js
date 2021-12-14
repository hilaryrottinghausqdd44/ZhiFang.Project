/***
 * 预置条件配置
 * @author longfc
 * @version 2017-06-14
 */
Ext.define('Shell.class.sysbase.rowfilter.precondition.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '预置条件',

	/**是否启用序号列*/
	hasRownumberer: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	hasAdd: false,
	hasEdit: false,
	/**是否启用查询框*/
	hasSearch: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	moduleId:null,
	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACPreconditionsByHQL?isPlanish=true',
	
	defaultOrderBy: [{
		property: 'RBACPreconditions_EntityCode',
		direction: 'ASC'
	},{
		property: 'RBACPreconditions_DispOrder',
		direction: 'ASC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere =me.defaultWhere||"";
		if(me.defaultWhere){
			me.defaultWhere+=" and rbacpreconditions.IsUse=1";
		}else{
			me.defaultWhere="rbacpreconditions.IsUse=1";
		}
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '预置条件名称',
			isLike: true,
			fields: ['rbacpreconditions.CName']
		};
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			text: '所属模块服务',
			dataIndex: 'RBACPreconditions_RBACModuleOper_CName',
			width: 200,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '所属实体对象',
			dataIndex: 'RBACPreconditions_EntityCName',
			width: 100,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		},  {
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
			sortable: false,
			hidden: true,
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
		},{
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
		},{
			text: '所属模块服务ID',
			dataIndex: 'RBACPreconditions_RBACModuleOper_Id',
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '使用',
			dataIndex: 'RBACPreconditions_IsUse',
			width: 40,
			hidden: true,
			align: 'center',
			isBool: true,
			type: 'bool'
		});
		return columns;
	}
});