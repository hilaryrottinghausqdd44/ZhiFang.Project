/**
 * 角色列表
 * @author longfc
 * @version 2020-04-03
 */
Ext.define('Shell.class.sysbase.jurisdiction.userrole.RoleCheckGrid', {
	extend: 'Shell.class.sysbase.role.Grid',

	title: '角色列表 ',
	width: 370,
	height: 500,

	/**员工ID*/
	UserId: null,

	/**序号列宽度*/
	rowNumbererWidth: 40,
	/**复选框*/
	multiSelect: false,
	selType: 'rowmodel',

	/**新增关系服务*/
	addLinkUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACEmpRoles',
	/**删除关系服务*/
	delLinkUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_DelRBACEmpRoles',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用修改按钮*/
	//hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: false,
	/**是否启用修改按钮*/
	hasEdit: false,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'RBACRole_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.on({
			load: function() {
				if (me.UserId) {
					me.loadLinkByUserId(me.UserId);
				}
			}
		});
	},
	initComponent: function() {
		var me = this;

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '关系主键ID',
			dataIndex: 'LinkId',
			hidden: true,
			hideable: false
		}, {
			xtype: 'checkcolumn',
			text: '勾选',
			dataIndex: 'IsLinked',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '角色名称',
			dataIndex: 'RBACRole_CName',
			width: 150,
			renderer: function(value, meta, record) {
				var DeveCode = record.get('RBACRole_DeveCode');
				var v = value;
				if (DeveCode) {
					v += ' 【系统角色】';
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '角色编码',
			dataIndex: 'RBACRole_UseCode',
			width: 80,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '使用',
			dataIndex: 'RBACRole_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			isBool: true,
			type: 'boolean'
		}, {
			text: '主键ID',
			dataIndex: 'RBACRole_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}];

		return columns;
	},

	/**保存勾选数据*/
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
			var IsLinked = rec.get('IsLinked');
			var LinkId = rec.get('LinkId');
			if (LinkId && !IsLinked) {
				me.delLinkById(rec, LinkId);
			} else {
				me.addLink(rec, id);
			}
		}
	},
	/**添加一条关系*/
	addLink: function(rec, id) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.addLinkUrl;
		var entity = {
			IsUse: true,
			PUser: {
				Id: me.UserId,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			},
			RBACRole: {
				Id: id,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			}
		};
		JShell.Server.post(url, Ext.JSON.encode({
			entity: entity
		}), function(data) {
			if (data.success) {
				me.saveCount++;
				rec.commit();
			} else {
				me.saveErrorCount++;
			}
			me.onSaveEnd();
		});
	},
	/**删除一条关系*/
	delLinkById: function(rec, id) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.delLinkUrl + '?id=' + id;
		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.saveCount++;
				rec.commit();
			} else {
				me.saveErrorCount++;
			}
			me.onSaveEnd();
		});
	},
	onSaveEnd: function() {
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			me.hideMask(); //隐藏遮罩层
			if (me.saveErrorCount == 0) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
			} else {
				JShell.Msg.error('存在失败信息，请重新保存！');
			}
		}
	},
	/**根据用户ID获取关系数据*/
	loadLinkByUserId: function(id) {
		var me = this;
		me.UserId = id;
		me.showMask(me.loadingText); //显示遮罩层
		//获取关系数据
		me.getLinkData(function() {
			me.changeRoleLink(function() {
				me.hideMask(); //隐藏遮罩层
			});
		});
	},
	/**获取关系数据*/
	getLinkData: function(callback) {
		var me = this;

		var url = JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL';
		url += '?fields=RBACEmpRoles_Id,RBACEmpRoles_RBACRole_Id&isPlanish=true';
		url += '&where=rbacemproles.PUser.Id=' + me.UserId

		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.LinkData = data.value.list || [];
				callback();
			} else {
				me.LinkData = [];
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**勾选关系赋值*/
	changeRoleLink: function(callback) {
		var me = this;
		me.setIsLinked();

		if (!me.LinkData || me.LinkData.length <= 0) {
			return callback();
		}
		var list = me.LinkData;
		for (var i in list) {
			var rec = me.store.findRecord(me.PKField, list[i].RBACEmpRoles_RBACRole_Id);
			rec.set({
				LinkId: list[i].RBACEmpRoles_Id + '',
				IsLinked: true
			});
			rec.commit();
		}
		callback();
	},
	setIsLinked: function() {
		var me = this;
		var records = me.store.data.items,
			len = records.length;
		for (var i = 0; i < len; i++) {
			records[i].set({
				LinkId: '',
				IsLinked: false
			});
			records[i].commit();
		}
	},
	/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.setIsLinked();
		me.disableControl(); //禁用 所有的操作功能
	}
});
