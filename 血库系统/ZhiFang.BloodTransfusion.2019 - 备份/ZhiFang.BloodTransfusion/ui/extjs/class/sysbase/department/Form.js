/**
 * 科室信息
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.department.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '科室信息',
	width: 280,
	height: 380,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddDepartment',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartmentByField',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 65,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	/**上级机构ID*/
	ParentID: 0,
	/**上级机构名称*/
	ParentName: '',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '部门编码',
			name: 'Department_Id',
			itemId: 'Department_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '名称',
			name: 'Department_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '上级机构',
			emptyText: '必填项',
			allowBlank: false,
			itemId: 'Department_ParentName',
			name: 'Department_ParentName',
			IsnotField: true,
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			value: me.ParentName,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.sysbase.department.CheckTree', {
					resizable: false,
					selectId: me.ParentID, //默认选中节点ID
					hideNodeId: me.PK, //默认隐藏节点ID
					listeners: {
						accept: function(p, record) {
							me.onParentModuleAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '上级机构主键ID',
			hidden: true,
			value: me.ParentID,
			name: 'Department_ParentID',
			itemId: 'Department_ParentID'
		}, {
			fieldLabel: 'Code1',
			name: 'Department_Code1',
		}, {
			fieldLabel: 'Code2',
			name: 'Department_Code2',
		}, {
			fieldLabel: 'Code3',
			name: 'Department_Code3',
		}, {
			fieldLabel: 'Code4',
			name: 'Department_Code4',
		}, {
			fieldLabel: 'Code5',
			name: 'Department_Code5',
		}, {
			fieldLabel: '次序',
			name: 'Department_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否使用',
			name: 'Department_Visible',
			xtype: 'checkbox',
			checked: true
		});

		return items;
	},
	/**选择上级机构*/
	onParentModuleAccept: function(record) {
		var me = this,
			ParentID = me.getComponent('Department_ParentID'),
			ParentName = me.getComponent('Department_ParentName');
		ParentID.setValue(record.get('tid'));
		ParentName.setValue(record.get('text') || '');
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('Department_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('Department_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('Department_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			CName: values.Department_CName,
			DispOrder: values.Department_DispOrder,
			Visible: values.Department_Visible ? 1 : 0,
			Code1: values.Department_Code1,
			Code2: values.Department_Code2,
			Code3: values.Department_Code3,
			Code4: values.Department_Code4,
			Code5: values.Department_Code5
		};
		var parentID = values.Department_ParentID;
		if (!parentID) {
			parentID = 0;
		}
		entity.ParentID = parentID;
		if (values.Department_Id) entity.Id = values.Department_Id;
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];

		for (var i in fields) {
			var arr = fields[i].split('_');
			if (arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');

		entity.entity.Id = values.Department_Id;
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		entity.empID = empID;
		entity.empName = empName;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});
