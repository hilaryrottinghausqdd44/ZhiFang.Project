/**
 * 血液单位
 * @author longfc
 * @version 2020-04-08
 */
Ext.define('Shell.class.sysbase.bloodunit.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '角色信息',
	width: 240,
	height: 400,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnitById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddBloodUnit',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUnitByField',
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
		var BloodUnit_CName = me.getComponent('BloodUnit_CName');
		BloodUnit_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodUnit_PinYinZiTou: value,
								BloodUnit_Shortcode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodUnit_PinYinZiTou: "",
						BloodUnit_Shortcode: ""
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
				name: 'BloodUnit_Id',
				itemId: 'BloodUnit_Id',
				//xtype: 'numberfield',
				emptyText: '数字编码',
				allowBlank: false,
				locked: true,
				readOnly: true
			},
			{
				fieldLabel: '名称',
				name: 'BloodUnit_CName',
				itemId: 'BloodUnit_CName',
				emptyText: '必填项',
				allowBlank: false
			},
			{
				fieldLabel: '简称',
				name: 'BloodUnit_SName'
			},
			{
				fieldLabel: '拼音字头',
				name: 'BloodUnit_PinYinZiTou'
			},
			{
				fieldLabel: '快捷码',
				name: 'BloodUnit_Shortcode'
			},
			{
				boxLabel: '是否使用',
				name: 'BloodUnit_Visible',
				xtype: 'checkbox',
				checked: true
			}, {
				fieldLabel: '显示次序',
				name: 'BloodUnit_DispOrder',
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
		me.getComponent('BloodUnit_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodUnit_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodUnit_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			CName: values.BloodUnit_CName,
			EName: values.BloodUnit_EName,
			PinYinZiTou: values.BloodUnit_PinYinZiTou,

			SName: values.BloodUnit_SName,
			DispOrder: values.BloodUnit_DispOrder,

			UseCode: values.BloodUnit_UseCode,
			StandCode: values.BloodUnit_StandCode,
			DeveCode: values.BloodUnit_DeveCode,

			Shortcode: values.BloodUnit_Shortcode,
			Visible: values.BloodUnit_Visible ? true : false
		};
		if (values.BloodUnit_Id) entity.Id = values.BloodUnit_Id;
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

		entity.entity.Id = values.BloodUnit_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return data;
	}
});
