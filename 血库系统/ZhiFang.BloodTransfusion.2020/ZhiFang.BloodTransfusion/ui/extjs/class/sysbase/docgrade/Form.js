/**
 * 医生审核等级
 * @author longfc
 * @version 2020-07-06
 */
Ext.define('Shell.class.sysbase.docgrade.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '字典类型信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodDocGradeById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodDocGrade',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodDocGradeByField',

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
		var BloodDocGrade_CName = me.getComponent('BloodDocGrade_CName');
		BloodDocGrade_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodDocGrade_PinYinZiTou: value,
								BloodDocGrade_SName: value,
								BloodDocGrade_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodDocGrade_PinYinZiTou: "",
						BloodDocGrade_SName: "",
						BloodDocGrade_ShortCode: ""
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
			fieldLabel: '类型编号',
			name: 'BloodDocGrade_Id',
			itemId: 'BloodDocGrade_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		},{
			fieldLabel: '类型编码',
			name: 'BloodDocGrade_DictTypeCode',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '名称',
			name: 'BloodDocGrade_CName',
			itemId: 'BloodDocGrade_CName',
			emptyText: '必填项',
			allowBlank: false
		},{
			fieldLabel: '简称',
			name: 'BloodDocGrade_SName',
			emptyText: '简称',
			allowBlank: true
		},{
			fieldLabel: '快捷码',
			name: 'BloodDocGrade_ShortCode',
			emptyText: '快捷码',
			allowBlank: true
		},{
			fieldLabel: '拼音字头',
			name: 'BloodDocGrade_PinYinZiTou',
			emptyText: '拼音字头',
			allowBlank: true
		}, {
			fieldLabel: '下限值',
			name: 'BloodDocGrade_LowLimit',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '上限值',
			name: 'BloodDocGrade_UpperLimit',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		},  {
			fieldLabel: '次序',
			name: 'BloodDocGrade_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		},{
			boxLabel: '是否使用',
			name: 'BloodDocGrade_IsUse',
			xtype: 'checkbox',
			checked: true
		});

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodDocGrade_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodDocGrade_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodDocGrade_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id:-2,
			DictTypeCode: values.BloodDocGrade_DictTypeCode,
			CName: values.BloodDocGrade_CName,
			SName: values.BloodDocGrade_SName,
			ShortCode: values.BloodDocGrade_ShortCode,
			PinYinZiTou: values.BloodDocGrade_PinYinZiTou,
			LowLimit: values.BloodDocGrade_LowLimit,
			UpperLimit: values.BloodDocGrade_UpperLimit,
			DispOrder: values.BloodDocGrade_DispOrder,
			IsUse: values.BloodDocGrade_IsUse ? true : false
		};
		if (values.BloodDocGrade_Id) entity.Id = values.BloodDocGrade_Id;
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

		entity.entity.Id = values.BloodDocGrade_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});
