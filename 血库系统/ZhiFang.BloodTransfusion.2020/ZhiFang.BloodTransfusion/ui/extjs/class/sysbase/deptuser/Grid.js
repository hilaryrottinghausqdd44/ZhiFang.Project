/**
 * 科室人员维护
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.deptuser.Grid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: ['Ext.ux.CheckColumn'],

	title: '科室人员信息',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentUserByHQL?isPlanish=true',
	/**新增数据服务路径*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddDepartmentUser',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartmentUserByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelDepartmentUser',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'DepartmentUser_PUser_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'transrecord.out.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "发血血袋信息",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '人员编码/人员名称/员Code1',
			isLike: true,
			fields: ['departmentuser.PUser.Id', 'departmentuser.PUser.CName', 'departmentuser.PUser.Code1']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '科室人员编码',
			dataIndex: 'DepartmentUser_Id',
			width: 100,
			hidden: true,
			isKey: true,
			hideable: false
		}, {
			text: '身份类型',
			dataIndex: 'DepartmentUser_PUser_Usertype',
			width: 65,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '人员编码',
			dataIndex: 'DepartmentUser_PUser_Id',
			width: 95,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '人员名称',
			dataIndex: 'DepartmentUser_PUser_CName',
			width: 95,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '人员Code1',
			dataIndex: 'DepartmentUser_PUser_Code1',
			width: 95,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'DepartmentUser_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '次序',
			dataIndex: 'DepartmentUser_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: 'btnCheck',
			text: '人员选择',
			tooltip: '人员选择',
			handler: function() {
				me.onCheckPUserClick();
			}
		});
		items.push('-',  'del');//'edit', 'save',
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	onCheckPUserClick: function() {
		var me = this;

		var defaultWhere = " puser.Visible=1 ";
		var arrIdStr = [],
			idStr = "";
		me.store.each(function(record) {
			var puserId = record.get("DepartmentUser_PUser_Id");
			if (puserId && Ext.Array.contains(puserId) == false) arrIdStr.push(puserId);
		});
		if (arrIdStr.length > 0) idStr = arrIdStr.join(",");
		if (idStr) defaultWhere = defaultWhere + " and puser.Id not in (" + idStr + ")";

		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			/**是否单选*/
			checkOne: false,
			canEdit: true,
			leftDefaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onAccept(p, records);
				}
			}
		};
		JShell.Win.open('Shell.class.sysbase.puser.choose.App', config).show();
	},
	onAccept: function(p, records) {
		var me = this;
		if (!records) {
			p.close();
			return;
		}
		var len = records.length;
		if (len == 0) {
			p.close();
			return;
		}
		if (!me.DepartmentId) {
			JShell.Msg.error("科室信息为空!");
			p.close();
			return;
		}
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for (var i = 0; i < len; i++) {
			me.addSaveOne(i, records[i], p);
		}
	},
	addSaveOne: function(index, record, p) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addUrl);
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		var entity = {
			'Id': -2,
			'IsUse': 1,
			'PUser': {
				"Id": record.get("PUser_Id"),
				"DataTimeStamp": strDataTimeStamp.split(',')
			},
			'Department': {
				"Id": me.DepartmentId,
				"DataTimeStamp": strDataTimeStamp.split(',')
			}
		};
		var dispOrder = record.get("PUser_DispOrder");
		if (!dispOrder) dispOrder = 0;
		entity.DispOrder = dispOrder;

		var params = JShell.JSON.encode({
			"entity": entity
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) {
						p.close();
						me.onSearch();
					} else {
						JShell.Msg.error(me.saveErrorCount + '条数据发生错误!');
					}
				}
			});
		}, 100 * index);
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
			var IsUse = rec.get('DepartmentUser_IsUse');
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
		this.fireEvent('addclick', this);
	}
});
