/**
 * 血制品分类表单
 * @author longfc
 * @version 2020-04-08
 */
Ext.define('Shell.class.sysbase.bloodclass.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '血制品分类信息',
	width: 240,
	height: 400,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddBloodClass',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodClassByField',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 60,
		labelAlign: 'right'
	},
	/**机构ID*/
	LabId: 0,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var BloodClass_CName = me.getComponent('BloodClass_CName');
		BloodClass_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodClass_PinYinZiTou: value,
								BloodClass_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodClass_PinYinZiTou: "",
						BloodClass_ShortCode: ""
					});
				}
			}
		});
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;

		var items = [{
				fieldLabel: '分类编码',
				name: 'BloodClass_Id',
				itemId: 'BloodClass_Id',
				xtype: 'numberfield',
				//xtype: 'numberfield',
				emptyText: '数字编码',
				locked: true,
				readOnly: true
			},
			{
				fieldLabel: '名称',
				name: 'BloodClass_CName',
				itemId: 'BloodClass_CName',
				emptyText: '必填项',
				allowBlank: false
			},
			{
				fieldLabel: '简称',
				name: 'BloodClass_SName'
			},
			{
				fieldLabel: '拼音字头',
				name: 'BloodClass_PinYinZiTou'
			},
			{
				fieldLabel: '快捷码',
				name: 'BloodClass_ShortCode'
			},
			{
				boxLabel: '是否使用',
				name: 'BloodClass_Visible',
				xtype: 'checkbox',
				checked: true
			}, {
				fieldLabel: '显示次序',
				name: 'BloodClass_DispOrder',
				xtype: 'numberfield',
				value: 0,
				allowBlank: false
			}
		];

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodClass_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodClass_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodClass_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			CName: values.BloodClass_CName,
			PinYinZiTou: values.BloodClass_PinYinZiTou,
			SName: values.BloodClass_SName,
			ShortCode: values.BloodClass_ShortCode,
			DispOrder: values.BloodClass_DispOrder,
			Visible: values.BloodClass_Visible ? true : false
		};
		if (values.BloodClass_Id) entity.Id = values.BloodClass_Id;
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

		entity.entity.Id = values.BloodClass_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return data;
	}
});
