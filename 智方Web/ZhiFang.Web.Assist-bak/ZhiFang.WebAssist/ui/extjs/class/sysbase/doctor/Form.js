/**
 * 医生信息维护
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.doctor.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '医生信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDoctorById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/WebAssistLisService.svc/WA_UDTO_AddDoctor',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateDoctorByField',

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
			fieldLabel: '医生编码',
			name: 'Doctor_Id',
			itemId: 'Doctor_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '名称',
			name: 'Doctor_CName',
			emptyText: '必填项',
			allowBlank: false
		},  {
			fieldLabel: 'Code1',
			name: 'Doctor_Code1',
		}, {
			fieldLabel: 'Code2',
			name: 'Doctor_Code2',
		}, {
			fieldLabel: 'Code3',
			name: 'Doctor_Code3',
		}, {
			fieldLabel: '次序',
			name: 'Doctor_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否使用',
			name: 'Doctor_Visible',
			xtype: 'checkbox',
			checked: true
		});

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('Doctor_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('Doctor_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('Doctor_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			CName: values.Doctor_CName,
			DispOrder: values.Doctor_DispOrder,
			Visible: values.Doctor_Visible ? 1 : 0,
			Code1: values.Doctor_Code1,
			Code2: values.Doctor_Code2,
			Code3: values.Doctor_Code3
		};
		if (values.Doctor_Id) entity.Id = values.Doctor_Id;
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

		entity.entity.Id = values.Doctor_Id;

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
