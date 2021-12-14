/***
 * 模块服务信息
 * @author longfc
 * @version 2017-05-02
 */
Ext.define('Shell.class.sysbase.rowfilter.datafilters.ModuleOperGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '模块服务信息',

	defaultWhere: '',
	internalWhere: '',
	externalWhere: '',
	selectIndex: 0,
	autoSelect: true,
	deleteIndex: -1,
	autoScroll: true,
	sortableColumns: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,

	/**是否启用查询框*/
	hasSearch: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true',
	editUrl: JShell.System.Path.ROOT + '/' + 'RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField',
	defaultOrderBy: [{
		property: 'RBACModuleOper_UseRowFilter',
		direction: 'DESC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || "";
		if(me.defaultWhere) {
			me.defaultWhere += " and rbacmoduleoper.IsUse=1";
		} else {
			me.defaultWhere = "rbacmoduleoper.IsUse=1";
		}
		me.addEvents('udateUseRowFilter');
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '模块操作名称',
			isLike: true,
			fields: ['rbacmoduleoper.CName']
		};
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			text: '模块服务',
			dataIndex: 'RBACModuleOper_CName',
			flex: 1,
			minWidth: 180,
			sortable: false,
			hideable: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var useRowFilter = "" + record.get('RBACModuleOper_UseRowFilter');
				if(useRowFilter == "false" || useRowFilter == "0")
					meta.style = 'background:red;color:#FFF;';
				return value;
			}
		}, {
			text: '默认数据过滤条件',
			dataIndex: 'RBACModuleOper_RBACRowFilter_CName',
			width: 105,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '默认数据过滤条件ID',
			dataIndex: 'RBACModuleOper_RBACRowFilter_Id',
			width: 20,
			sortable: false,
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}, {
			text: '数据对象',
			dataIndex: 'RBACModuleOper_RowFilterBase',
			width: 80,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '数据对象中文名称',
			dataIndex: 'RBACModuleOper_RowFilterBaseCName',
			width: 80,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '模块操作主键ID',
			dataIndex: 'RBACModuleOper_Id',
			isKey: true,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '模块名称',
			dataIndex: 'RBACModuleOper_RBACModule_CName',
			width: 80,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '模块主键ID',
			dataIndex: 'RBACModuleOper_RBACModule_Id',
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '是否采用数据过滤条件',
			dataIndex: 'RBACModuleOper_UseRowFilter',
			width: 80,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, me.createUseRowFilter()];
		return columns;
	},
	/*创建行数据条件启用或禁用列**/
	createUseRowFilter: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '启/禁',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if(record.get('RBACModuleOper_UseRowFilter') == "true") {
						meta.tdAttr = 'data-qtip="<b>禁用行数据条件</b>"';
						return 'button-edit hand';
					} else {
						meta.tdAttr = 'data-qtip="<b>启用行数据条件</b>"';
						return 'button-edit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var useRowFilter = "" + rec.get('RBACModuleOper_UseRowFilter');
					var msg = useRowFilter == "true" ? "是否禁用该行数据条件" : "是否启用行数据条件";
					var newUseRowFilter = useRowFilter == "true" ? false : true;
					Ext.MessageBox.show({
						title: '操作确认消息',
						msg: msg,
						width: 300,
						icon: Ext.MessageBox.QUESTION,
						buttons: Ext.MessageBox.OKCANCEL,
						fn: function(btn) {
							if(btn == 'ok') {
								me.editUseRowFilter(rec, newUseRowFilter);
							}
						}
					});
				}
			}]
		};
	},
	editUseRowFilter: function(record, newValue) {
		var me = this;

		var editfields = 'Id,UseRowFilter';
		var msgInfo = "";
		if(newValue == true) {
			msgInfo = "行数据条件禁用";
		} else {
			msgInfo = "行数据条件启用";
		}
		var moduleOperId = record.get('RBACModuleOper_Id');
		var obj = {
			'Id': moduleOperId,
			'UseRowFilter': '' + newValue
		};
		var obj2 = {
			'entity': obj,
			'fields': editfields
		};
		var params = JShell.JSON.encode(obj2);

		JShell.Server.post(me.editUrl, params, function(data) {
			if(data.success) {
				record.set('RBACModuleOper_UseRowFilter', '' + newValue);
				record.commit();
				me.fireEvent('udateUseRowFilter', me, record, newValue);
				JShell.Msg.alert(msgInfo + "成功", null, 1000);
			} else {
				var msg = data.msg;
				msgInfo = msgInfo + '失败';
				JShell.Msg.error(msgInfo);
			}
		});
	}
});