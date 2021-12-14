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
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchPUserById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_AddPUser',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdatePUserByField',
	/**密码重置服务地址*/
	pwdResetUrl:'/ServerWCF/WebAssistManageService.svc/WA_RJ_ResetAccountPassword',
	
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
	initComponent:function(){
		var me = this;
		
		me.buttonToolbarItems = [{
			text:'密码重置',
			iconCls:'button-lock',
			itemId:'PwdReset',
			tooltip:'<b>随机生成密码</b>',
			handler:function(){me.onPwdReset();}
		},'->'];
		
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
			hidden:true,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '名称',
			name: 'PUser_CName',
			itemId: 'PUser_CName',
			emptyText: '必填项',
			allowBlank: false
		},{
			fieldLabel: '身份类型',
			name: 'PUser_Usertype',
			itemId: 'PUser_Usertype',
			xtype: 'uxSimpleComboBox',
			value: '护士',
			hasStyle: true,
			data: [
				['检验技师', '检验技师', 'color:green;'],
				['医生', '医生', 'color:black;'],
				['护士', '护士', 'color:black;'],
				['护工', '护工', 'color:black;']
			]
		}, {
			fieldLabel: '绑定科室',
			name: 'PUser_Department_CName',
			itemId: 'PUser_Department_CName',
			emptyText: '绑定科室',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.department.CheckGrid',
			editable: true,
			classConfig: {
				title: '绑定科室选择',
				width: 280,
				height:380
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
		}, {
			fieldLabel: '绑定医生',
			name: 'PUser_Doctor_CName',
			itemId: 'PUser_Doctor_CName',
			emptyText: '绑定医生',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.doctor.CheckGrid',
			editable: true,
			hidden:true,
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
			fieldLabel: '登录帐号',
			name: 'PUser_ShortCode',
			emptyText: '必填项',
			allowBlank: false
		},  {
			fieldLabel: '登录密码',
			name: 'PUser_Password',
			itemId: 'PUser_Password',
			IsnotField: true
		}, {
			fieldLabel: '确认密码',
			name: 'PUser_RPassword',
			itemId: 'PUser_RPassword',
			IsnotField: true
		}, {
			fieldLabel: '次序',
			name: 'PUser_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '对照码1',
			name: 'PUser_Code1',
		}, {
			fieldLabel: '对照码2',
			name: 'PUser_Code2',
		}, {
			fieldLabel: '对照码3',
			name: 'PUser_Code3',
		}, {
			fieldLabel: '对照码4',
			name: 'PUser_Code4',
		}, {
			fieldLabel: '对照码5',
			name: 'PUser_Code5',
		},{
			boxLabel: '是否使用',
			name: 'PUser_Visible',
			xtype: 'checkbox',
			checked: true
		});

		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar:function(){
		var me = this,
			items = me.buttonToolbarItems || [];
		
		if(me.hasSave) items.push('save');
		if(me.hasSaveas) items.push('saveas');
		if(me.hasReset) items.push('reset');
		if(me.hasCancel) items.push('cancel');
		//if(items.length > 0) items.unshift('->');
		if(items.length == 0) return null;
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		
		return Ext.create('Shell.ux.toolbar.Button',{
			dock:me.buttonDock,
			itemId:'buttonsToolbar',
			items:items,
			hidden:hidden
		});
	},
	setVisibles2:function(visible){
		var me = this;
		me.getComponent('buttonsToolbar').getComponent('PwdReset').setVisible(visible);
		me.getComponent('PUser_Password').setVisible(!visible);
		me.getComponent('PUser_RPassword').setVisible(!visible);
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.setVisibles2(false);
		me.getComponent('PUser_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.setVisibles2(true);
		me.getComponent('PUser_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.setVisibles2(true);
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
		
		if (values.PUser_Password) entity.Password = values.PUser_Password;
		
		if (values.PUser_Id) entity.Id = values.PUser_Id;
		if (values.PUser_Department_Id) {
			entity.Department = {
				Id: values.PUser_Department_Id,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 1]
			};
		} else {
			entity.Department=null;
		}
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
		fieldsArr.push("Department_Id");
		//fieldsArr.push("Doctor_Id");
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
				"/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchDoctorByHQL?isPlanish=true";
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
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if (!me.getForm().isValid()) return;
	
		values = me.getForm().getValues();
		if (me.formtype=="add"&&values.PUser_Password != values.PUser_RPassword) {
			JShell.Msg.error("两次输入的密码不相同!");
			return;
		}
		me.callParent(arguments);
	},
	/**密码重置*/
	onPwdReset:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.pwdResetUrl);
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		url += '?id=' + me.PK+"&empID="+empID+"&empName="+empName;
		
		JShell.Msg.confirm({
			msg:'确定重新生成密码吗？'
		},function(but){
			if(but != 'ok') return;
			
			JShell.Server.get(url,function(data){
				if(data.success){
					JShell.Msg.alert('您的新密码：' + data.value.AccountPassword);
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	}
});
