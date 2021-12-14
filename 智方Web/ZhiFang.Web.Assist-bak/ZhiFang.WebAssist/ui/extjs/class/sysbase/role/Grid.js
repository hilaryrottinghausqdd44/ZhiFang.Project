/**
 * 角色列表
 * @author longfc
 * @version 2020-04-08
 */
Ext.define('Shell.class.sysbase.role.Grid', {
	extend: 'Shell.class.assist.basic.GridPanel',
	requires: ['Ext.ux.CheckColumn'],

	title: '角色列表 ',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_UpdateRBACRoleByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_DelRBACRole ',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 100,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'RBACRole_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'role.Grid',
	/**用户UI配置Name*/
	userUIName: "角色列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.on({
			itemdblclick: function(view, record) {
				me.onEditClick();
			}
		});
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '角色名称/分类',
			isLike: true,
			fields: ['rbacrole.CName', 'rbacrole.SName']
		};

		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var me = this;
		var columns = [ {
			text: '角色编码',
			dataIndex: 'RBACRole_Id',
			isKey: true
		},{
			text: '角色名称',
			dataIndex: 'RBACRole_CName',
			width: 150,
			defaultRenderer: true
		}, {
			text: '系统预置类型',
			dataIndex: 'RBACRole_SySType',
			width: 100,
			renderer: function(value, meta, record) {
				var value2 = record.get("RBACRole_SySType");
				
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				var style="",value3="";
				if (value2 == "1") {
					style = style + "background-color:#C0FFC0;";
					value3="全系统预置";
				} else if (value2 == "2") {
					style = style + "background-color:#FFE0C0;";
					value3="临床医生预置";
				} else if (value2 == "3") {
					style = style + "background-color:#FFC0FF;";
					value3="临床护士预置";
				} else if (value2 == "4") {
					value3="检验技师预置";
					style = style + "background-color:#C0FFFF;";
				} else if (value2 == "5") {
					value3="护工角色预置";
					style = style + "background-color:#C0C000;";
				} else if (value2 == "6") {
					value3="感染科预置";
					style = style + "background-color:#C0FFFF;";
				} else if (value2 == "7") {
					value3="输血科预置";
					style = style + "background-color:#C0C000;";
				} else if (value2 == "8") {
					value3="统计和报表";
					style = style + "background-color:#C0C000;";
				} 
				meta.style = style;
				return value3;
			}
		}, {
			text: '分类',
			dataIndex: 'RBACRole_SName',
			width: 100,
			defaultRenderer: true
		},  {
			text: '系统代码',
			dataIndex: 'RBACRole_UseCode',
			width: 100,
			defaultRenderer: true
		}, {
			text: '标准代码',
			dataIndex: 'RBACRole_StandCode',
			width: 100,
			defaultRenderer: true
		}, {
			text: '开发商代码',
			dataIndex: 'RBACRole_DeveCode',
			width: 100,
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'RBACRole_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '显示次序',
			dataIndex: 'RBACRole_DispOrder',
			width: 60,
			defaultRenderer: true,
			type: 'int'
		}, {
			text: '机构ID',
			dataIndex: 'RBACRole_LabID',
			hidden: true
		}, {
			text: '时间戳',
			dataIndex: 'RBACRole_DataTimeStamp',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**
	 * @description 监测类型及样品列信息样式处理
	 * @param {Object} value2
	 */
	getCellStyle: function(value2) {
		//var style = 'font-weight:bold;';color:#1c8f36;
		var style = ''; //font-weight:bold;
		if (value2 == "1") {
			style = style + "background-color:#C0FFC0;";
		} else if (value2 == "2") {
			style = style + "background-color:#FFE0C0;";
		} else if (value2 == "3") {
			style = style + "background-color:#FFC0FF;";
		} else if (value2 == "4") {
			style = style + "background-color:#C0FFFF;";
		} else if (value2 == "5") {
			style = style + "background-color:#C0C0FF;";
		} 
		//style = style + "color:#ffffff;";
		return style;
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsUse = rec.get('RBACRole_IsUse');
			me.updateOneByIsUse(i, id, IsUse);
		}
	},
	updateOneByIsUse: function(index, id, IsUse) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				IsUse: IsUse
			},
			fields: 'Id,IsUse'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('addclick', me);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		me.fireEvent('editclick', me, records[0]);
	}
});
