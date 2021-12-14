/**
 * 申请类型维护
 * @author longfc
 * @version 2020-04-08
 */
Ext.define('Shell.class.sysbase.bloodusetype.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '申请类型信息',
	width: 240,
	height: 400,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseTypeById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddBloodUseType',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUseTypeByField',
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
		var BloodUseType_CName = me.getComponent('BloodUseType_CName');
		BloodUseType_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodUseType_PinYinZiTou: value,
								BloodUseType_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodUseType_PinYinZiTou: "",
						BloodUseType_ShortCode: ""
					});
				}
			}
		});
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;

		var items = [{
				fieldLabel: '编码',
				name: 'BloodUseType_Id',
				itemId: 'BloodUseType_Id',
				//xtype: 'numberfield',
				emptyText: '数字编码',
				allowBlank: false,
				locked: true,
				readOnly: true
			},
			{
				fieldLabel: '名称',
				name: 'BloodUseType_CName',
				itemId: 'BloodUseType_CName',
				emptyText: '必填项',
				allowBlank: false
			},
			{
				fieldLabel: '简称',
				name: 'BloodUseType_SName'
			},{
				fieldLabel: '拼音字头',
				name: 'BloodUseType_PinYinZiTou'
			},
			{
				fieldLabel: '快捷码',
				name: 'BloodUseType_ShortCode'
			},
			{
				boxLabel: '是否使用',
				name: 'BloodUseType_Visible',
				xtype: 'checkbox',
				checked: true
			}, {
				fieldLabel: '显示次序',
				name: 'BloodUseType_DispOrder',
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
		me.getComponent('BloodUseType_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodUseType_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodUseType_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			CName: values.BloodUseType_CName,
			PinYinZiTou: values.BloodUseType_PinYinZiTou,
			ShortCode: values.BloodUseType_ShortCode,
			SName: values.BloodUseType_SName,
			DispOrder: values.BloodUseType_DispOrder,
			Shortcode: values.BloodUseType_Shortcode,
			Visible: values.BloodUseType_Visible ? true : false
		};
		if (values.BloodUseType_Id) entity.Id = values.BloodUseType_Id;
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

		entity.entity.Id = values.BloodUseType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return data;
	}
});
