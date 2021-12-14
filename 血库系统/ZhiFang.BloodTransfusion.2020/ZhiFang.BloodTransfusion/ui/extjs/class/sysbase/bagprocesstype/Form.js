/**
 * 加工类型
 * @author panjie
 * @version 2020-08-06
 */
Ext.define('Shell.class.sysbase.bagprocesstype.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '加工类型',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessTypeById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagProcessType',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcessTypeByField',

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
		var BloodBagProcessType_CName = me.getComponent('BloodBagProcessType_CName');
		BloodBagProcessType_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodBagProcessType_PinYinZiTou: value,
								BloodBagProcessType_SName: value,
								BloodBagProcessType_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodBagProcessType_PinYinZiTou: "",
						BloodBagProcessType_SName: "",
						BloodBagProcessType_ShortCode: ""
					});
				}
			}
		});
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '加工编号',
			name: 'BloodBagProcessType_Id',
			itemId: 'BloodBagProcessType_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		},
		{
			fieldLabel: '名称',
			name: 'BloodBagProcessType_CName',
			itemId: 'BloodBagProcessType_CName',
			emptyText: '必填项',
			allowBlank: true
		},{
			fieldLabel: '简称',
			name: 'BloodBagProcessType_SName',
			emptyText: '简称',
			allowBlank: true
		},{
			fieldLabel: '快捷码',
			name: 'BloodBagProcessType_ShortCode',
			emptyText: '快捷码',
			allowBlank: true
		},{
			fieldLabel: '拼音字头',
			name: 'BloodBagProcessType_PinYinZiTou',
			emptyText: '拼音字头',
			allowBlank: true
		},  {
			fieldLabel: '次序',
			name: 'BloodBagProcessType_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		},  {
			boxLabel: '是否使用',
			name: 'BloodBagProcessType_IsUse',
			xtype: 'checkbox',
			checked: true
		});

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodBagProcessType_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodBagProcessType_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodBagProcessType_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id:-2,
			//BCNo: values.BloodBagProcessType_BloodClass,
			//Memo: values.BDictType_Memo,
			CName: values.BloodBagProcessType_CName,
			BCCode: values.BloodBagProcessType_BCCode,
			PinYinZiTou: values.BloodBagProcessType_PinYinZiTou,
			SName: values.BloodBagProcessType_SName,
			ShortCode: values.BloodBagProcessType_ShortCode,
			DispOrder: values.BloodBagProcessType_DispOrder,
			//IsLargeUse: values.BloodBagProcessType_IsLargeUse? true : false,
			IsUse: values.BloodBagProcessType_IsUse? true : false
		};
		if (values.BloodBagProcessType_Id) entity.Id = values.BloodBagProcessType_Id;
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
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		entity.empID = empID;
		entity.empName = empName;
		//fieldsArr.push("BloodClass_Id");
		entity.entity.Id = values.BloodBagProcessType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});
