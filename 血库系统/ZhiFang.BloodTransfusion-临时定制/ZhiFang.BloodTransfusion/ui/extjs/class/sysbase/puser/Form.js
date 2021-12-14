/**
 * 人员信息维护
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.puser.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '人员信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchPUserById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddPUser',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdatePUserByField',

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
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '帐号',
			name: 'PUser_ShortCode',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '名称',
			name: 'PUser_CName',
			itemId: 'PUser_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '身份类型',
			name: 'PUser_Usertype',
			itemId: 'PUser_Usertype',
			xtype: 'uxSimpleComboBox',
			value: '检验技师',
			//emptyText: '必填项',
			//allowBlank: false,
			hasStyle: true,
			data: [
				['检验技师', '检验技师', 'color:green;'],
				['医生', '医生', 'color:black;'],
				['护士', '护士', 'color:black;'],
				['护工', '护工', 'color:black;']
			]
		}, {
			fieldLabel: '绑定医生',
			name: 'PUser_Doctor_CName',
			itemId: 'PUser_Doctor_CName',
			emptyText: '绑定医生',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.doctor.CheckGrid',
			editable: true,
			classConfig: {
				title: '绑定医生选择'
			},
			listeners: {
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						if (!field.getValue()) {
							var info = "请输入工号!";
							JShell.Msg.alert(info, null, 2000);
							return;
						}
						me.onGetDoctor(field);
					}
				},
				beforetriggerclick: function(p) {
					var cname = me.getComponent('PUser_CName');
					if (cname) {
						p.changeClassConfig({
							searchInfoVal: cname.getValue()
						});
					}
					return true;
				},
				check: function(p, record) {
					var data = record ? record.data : '';
					me.onDoctorCheck(p, data);
				}
			}
		}, {
			fieldLabel: '绑定医生Id',
			name: 'PUser_Doctor_Id',
			itemId: 'PUser_Doctor_Id',
			xtype: 'textfield',
			hidden: true,
		}, {
			fieldLabel: '次序',
			name: 'PUser_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: 'Code1',
			name: 'PUser_Code1',
		}, {
			fieldLabel: 'Code2',
			name: 'PUser_Code2',
		}, {
			fieldLabel: 'Code3',
			name: 'PUser_Code3',
		}, {
			fieldLabel: 'Code4',
			name: 'PUser_Code4',
		}, {
			fieldLabel: 'Code5',
			name: 'PUser_Code5',
		},{
			boxLabel: '是否使用',
			name: 'PUser_Visible',
			xtype: 'checkbox',
			checked: true
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
			ShortCode: values.PUser_ShortCode,
			CName: values.PUser_CName,
			Usertype: values.PUser_Usertype,
			Code1: values.PUser_Code1,
			Code2: values.PUser_Code2,
			Code3: values.PUser_Code3,
			Code4: values.PUser_Code4,
			Code5: values.PUser_Code5,
			DispOrder: values.PUser_DispOrder,
			Visible: values.PUser_Visible ? 1 : 0
		};
		if (values.PUser_Id) entity.Id = values.PUser_Id;
		if (values.PUser_Doctor_Id) {
			entity.Doctor = {
				Id: values.PUser_Doctor_Id,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 1]
			};
		} else {
			//entity.Doctor=null;
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
		fieldsArr.push("Doctor_Id");
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
	onGetDoctor: function(field) {
		var me = this;
		var fieldVal = field.getValue();
		if (fieldVal) {
			var fields = "Doctor_Id,Doctor_CName,Doctor_Code1,Doctor_Code2,Doctor_Code3";
			var where = "doctor.Code1='" + fieldVal + "'";
			var url = JShell.System.Path.ROOT +
				"/BloodTransfusionManageService.svc/BT_UDTO_SearchDoctorByHQL?isPlanish=true";
			url += '&where=' + where + "&fields=" + fields;
			JShell.Server.get(url, function(data) {
				if (data.success && data.value) {
					var list = data.value.list;
					if (list.length == 1) {
						me.onDoctorCheck(null, list[0]);
					} else if (list.length > 1) {
						JShell.Msg.error('工号为:' + fieldVal + ',获取医生信息存在多条记录！');
					} else {
						JShell.Msg.error('工号为:' + fieldVal + ',获取医生信息失败！');
					}
				} else {
					JShell.Msg.error('工号为:' + fieldVal + ',获取医生信息失败！');
				}
			});
		}
	},
	/**@desc 弹出人员选择器选择确认后处理*/
	onDoctorCheck: function(p, data) {
		var me = this;
		var ManagerID = null,
			ManagerName = null;
		ManagerID = me.getComponent('PUser_Doctor_Id');
		ManagerName = me.getComponent('PUser_Doctor_CName');
		if (ManagerName) ManagerName.setValue(data ? data['Doctor_CName'] : '');
		if (ManagerID) ManagerID.setValue(data ? data['Doctor_Id'] : '');
		if (p) p.close();
	}
});
