/**
 * 科室信息列表
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.department.Grid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Ext.ux.CheckColumn'
	],

	title: '科室信息列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartmentByField',
	/**删除数据服务路径*/
	delUrl: '/BloodTransfusionManageService.svc/BT_UDTO_DelDepartment',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,

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
	//	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},
	/**上级机构ID*/
	ParentID: 0,
	/**上级机构名称*/
	ParentName: '',

	defaultOrderBy: [{
		property: 'Department_DispOrder',
		direction: 'ASC'
	}],

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
			width: 145,
			emptyText: '编码/名称/Code1/Code2/Code3',
			isLike: true,
			fields: ['department.Id', 'department.CName', 'department.Code1', 'department.Code2', 'department.Code3']
		};
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'GridEditing'
		});
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '部门编码',
			dataIndex: 'Department_Id',
			width: 95,
			isKey: true,
			hideable: false
		}, {
			text: '名称',
			dataIndex: 'Department_CName',
			width: 120,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'Code1',
			dataIndex: 'Department_Code1',
			width: 85,
			editor: {},
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'Code2',
			dataIndex: 'Department_Code2',
			width: 85,
			editor: {},
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'Code3',
			dataIndex: 'Department_Code3',
			width: 85,
			editor: {},
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'Code4',
			dataIndex: 'Department_Code4',
			width: 85,
			editor: {},
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'Code5',
			dataIndex: 'Department_Code5',
			width: 85,
			editor: {},
			menuDisabled: true,
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'Department_Visible',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '次序',
			dataIndex: 'Department_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 45,
			hideable: false,
			sortable: false,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];
		if (me.hasRefresh) items.push('refresh');
		if (me.hasAdd) items.push('add');
		if (me.hasEdit) items.push('edit');
		if (me.hasDel) items.push('del');
		if (me.hasShow) items.push('show');
		if (me.hasSave) items.push('save');
		items.push('-', {
			fieldLabel: '', //身份类型
			labelWidth: 0,
			width: 75,
			name: 'OfSyncField',
			itemId: 'OfSyncField',
			xtype: 'uxSimpleComboBox',
			value: 'Code1',
			hidden: true,
			hasStyle: true,
			data: [
				['Id', '按编码', 'color:black;'],
				['CName', '按名称', 'color:black;'],
				['Code1', '按Code1', 'color:black;'],
				['Code2', '按Code2', 'color:black;'],
				['Code3', '按Code3', 'color:black;']
			]
		}, {
			xtype: 'splitbutton',
			text: '操作',
			iconCls: 'button-down',
			menu: [ {
				iconCls: 'button-add',
				text: '选择下级科室',
				handler: function() {
					me.onChooseCDept();
				}
			}, '-',{
				iconCls: 'button-import',
				text: '按申请信息建立病区与科室关系',
				handler: function() {
					me.onSyncWarpAndDept();
				}
			}, '-', {
				iconCls: 'button-exp',
				text: '导入到集成平台',
				handler: function() {
					me.onSync("tolimp");
				}
			}, {
				iconCls: 'button-import',
				text: '从集成平台导入',
				hidden: true,
				handler: function() {
					me.onSync("oflimp");
				}
			}]
		});
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		//items.push('-', 'save');
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.ApplyGrid.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get("Department_Id");
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.BloodTransfusion', //类域
			className: 'UpdateOperationType', //类名
			title: '操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='Department'"
		};
		var win = JShell.Win.open('Shell.class.blood.scoperation.SCOperation', config);
		win.show();
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
			me.updateOne(i, id, rec);
		}
	},
	updateOne: function(index, id, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var IsUse = rec.get('Department_Visible');
		if (IsUse == true || IsUse == 1) {
			IsUse = 1;
		} else {
			IsUse = 0;
		}
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";

		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				Visible: IsUse,
				Visible: rec.get('Department_Visible'),
				Code1: rec.get('Department_Code1'),
				Code2: rec.get('Department_Code2'),
				Code3: rec.get('Department_Code3'),
				Code4: rec.get('Department_Code4'),
				Code5: rec.get('Department_Code5'),
				DispOrder: rec.get('Department_DispOrder')
			},
			empID: empID,
			empName: empName,
			fields: 'Id,Visible,Code1,Code2,Code3,Code4,Code5,DispOrder'
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
		//me.fireEvent('addclick', this);
		me.openOrgForm();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var id = records[0].get(me.PKField);
		me.openOrgForm(id);
	},
	/**根据上级机构ID加载数据*/
	loadByParentId: function(id, name) {
		var me = this;
		me.ParentID = id;
		me.ParentName = name;
		if (id != 0) {
			me.defaultWhere = ('department.Id=' + id + ' or department.ParentID=' + id);
		}
		me.onSearch();
	},
	/**打开表单*/
	openOrgForm: function(id) {
		var me = this;
		var config = {
			ParentID: me.ParentID, //上级机构ID
			ParentName: me.ParentName, //上级机构名称
			showSuccessInfo: false,
			resizable: false,
			formtype: 'add',
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.sysbase.department.Form', config).show();
	},
	//选择病区下的科室信息
	onChooseCDept: function() {
		var me = this;
		if (!me.ParentID) {
			JShell.Msg.error("请选择病区后再操作!");
			return;
		}
		var defaultWhere = " department.Visible=1 and (department.ParentID is null or department.ParentID = 0)";
		var arrIdStr = [],
			idStr = "";
		me.store.each(function(record) {
			var puserId = record.get("DepartmentUser_Id");
			if (puserId && Ext.Array.contains(puserId) == false) arrIdStr.push(puserId);
		});
		if (arrIdStr.length > 0) idStr = arrIdStr.join(",");
		if (idStr) defaultWhere = defaultWhere + " and department.Id not in (" + idStr + ")";

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
		JShell.Win.open('Shell.class.sysbase.department.choose.App', config).show();
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
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		for (var i = 0; i < len; i++) {
			me.editSaveOne(i, records[i],empID,empName, p);
		}
	},
	editSaveOne: function(index, record, empID,empName,p) {
		var me = this;
		var entity = {
			'Id': record.get("Department_Id"),
			'ParentID': me.ParentID
		};
		var params = JShell.JSON.encode({
			"entity": entity,
			"fields": "Id,ParentID",
			empID: empID,
			empName: empName
		});
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**
	 * @description 导入数据
	 * @param {Object} type1
	 */
	onSync: function(type1) {
		var me = this;
		var url = JShell.System.Path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SyncDeptListToLIMP";
		var params = Ext.JSON.encode({
			syncType: type1
		});
		setTimeout(function() {
			me.showMask("导入处理中,请耐心等待!"); //显示遮罩层
			JShell.Server.post(url, params, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {

				} else {

				}
			});
		}, 100 * 1);
	},
	//按申请信息建立病区与科室关系
	onSyncWarpAndDept: function(id) {
		var me = this;
		var url = JShell.System.Path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_AddWarpAndDept";
		setTimeout(function() {
			me.showMask("建立关系处理中,请耐心等待!"); //显示遮罩层
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {

				} else {

				}
			});
		}, 100 * 1);
	}
});
