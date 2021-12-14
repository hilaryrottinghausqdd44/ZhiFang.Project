/**
 * 绑定科室
 * @author longfc
 * @version 2020-12-25
 */
Ext.define('Shell.class.sysbase.puser.reg.BindDept', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '绑定科室',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchPUserById?isPlanish=true',
	/**新增服务地址*/
	//addUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_AddPUserOfReg',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdatePUserByBindDept',
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 65,
		labelAlign: 'right'
	},
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype: 'edit',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '用户编码',
			name: 'PUser_Id',
			itemId: 'PUser_Id',
			hidden: true,
			emptyText: '数字编码',
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '姓名',
			name: 'PUser_CName',
			itemId: 'PUser_CName',
			emptyText: '必填项',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '绑定科室',
			name: 'PUser_Department_CName',
			itemId: 'PUser_Department_CName',
			allowBlank: false,
			emptyText: '绑定科室',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.department.CheckGrid',
			editable: true,
			classConfig: {
				title: '绑定科室选择',
				width: 280,
				height: 380
			},
			listeners: {
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						if (!field.getValue()) {
							var info = "请输入编号!";
							JShell.Msg.alert(info, null, 2000);
							return;
						}
						me.onGetDept(field);
					}
				},
				beforetriggerclick: function(p) {
					var cname = me.getComponent('PUser_Department_CName');
					if (cname) {
						p.changeClassConfig({
							searchInfoVal: cname.getValue()
						});
					}
					return true;
				},
				check: function(p, record) {
					var data = record ? record.data : '';
					me.onDepartmentCheck(p, data);
				}
			}
		}, {
			fieldLabel: '绑定科室Id',
			name: 'PUser_Department_Id',
			itemId: 'PUser_Department_Id',
			xtype: 'textfield',
			hidden: true,
		});

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('PUser_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('PUser_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('PUser_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			CName: values.PUser_CName
		};
		if (values.PUser_Id) entity.Id = values.PUser_Id;
		if (values.PUser_Department_Id) {
			entity.Department = {
				Id: values.PUser_Department_Id,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 1]
			};
		} else {
			entity.Department = null;
		}

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
		fieldsArr.push("Department_Id");
		entity.fields = fieldsArr.join(',');
		entity.entity.Id = values.PUser_Id;

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
	},
	/**
	 * @description 
	 * @param {Object} field
	 */
	onGetDept: function(field) {
		var me = this;
		var fieldVal = field.getValue();
		if (fieldVal) {
			var fields = "Department_Id,Department_CName,Department_Code1,Department_Code2,Department_Code3";
			var where = "department.Id='" + fieldVal + "'";
			var url = JShell.System.Path.ROOT +
				"/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchDepartmentByHQL?isPlanish=true";
			url += '&where=' + where + "&fields=" + fields;
			JShell.Server.get(url, function(data) {
				if (data.success && data.value) {
					var list = data.value.list;
					if (list.length == 1) {
						me.onDepCheck(null, list[0]);
					} else if (list.length > 1) {
						JShell.Msg.error('编号为:' + fieldVal + ',获取科室信息存在多条记录！');
					} else {
						JShell.Msg.error('编号为:' + fieldVal + ',获取科室信息失败！');
					}
				} else {
					JShell.Msg.error('编号为:' + fieldVal + ',获取科室信息失败！');
				}
			});
		}
	},
	/**@desc 弹出人员选择器选择确认后处理*/
	onDepartmentCheck: function(p, data) {
		var me = this;
		var ManagerID = null,
			ManagerName = null;
		ManagerID = me.getComponent('PUser_Department_Id');
		ManagerName = me.getComponent('PUser_Department_CName');
		if (ManagerName) ManagerName.setValue(data ? data['Department_CName'] : '');
		if (ManagerID) ManagerID.setValue(data ? data['Department_Id'] : '');
		if (p) p.close();
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this,
			type = me.formtype;

		if (type == 'add') {
			me.setTitle(JShell.All.ADD + me.defaultTitle);
		} else if (type == 'edit') {
			me.setTitle(JShell.All.EDIT + me.defaultTitle);
		} else if (type == 'show') {
			me.setTitle(JShell.All.SHOW + me.defaultTitle);
		} else {
			me.setTitle(me.defaultTitle);
		}
	},
});
