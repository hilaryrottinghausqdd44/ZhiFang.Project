/**
 * 角色表单
 * @author longfc
 * @version 2020-04-08
 */
Ext.define('Shell.class.sysbase.role.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '角色信息',
	width: 240,
	height: 400,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACRole',
	/**修改服务地址*/
	editUrl: '/ServerWCF/RBACService.svc/RBAC_UDTO_UpdateRBACRoleByField',
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
		var RBACRole_CName = me.getComponent('RBACRole_CName');
		RBACRole_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								RBACRole_PinYinZiTou: value,
								RBACRole_Shortcode: value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						RBACRole_PinYinZiTou: "",
						RBACRole_Shortcode: ""
					});
				}
			}
		});
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;

		var items = [{
				fieldLabel: '角色编码',
				name: 'RBACRole_Id',
				itemId: 'RBACRole_Id',
				//xtype: 'numberfield',
				emptyText: '数字编码',
				allowBlank: false,
				locked: true,
				readOnly: true
			}, {
				fieldLabel: '显示次序',
				name: 'RBACRole_DispOrder',
				xtype: 'numberfield',
				value: 0,
				allowBlank: false
			},
			{
				fieldLabel: '角色名称',
				name: 'RBACRole_CName',
				itemId: 'RBACRole_CName',
				emptyText: '必填项',
				allowBlank: false
			},
			{
				fieldLabel: '分类',
				name: 'RBACRole_SName'
			},
			{
				fieldLabel: '英文名称',
				name: 'RBACRole_EName'
			},
			{
				fieldLabel: '拼音字头',
				name: 'RBACRole_PinYinZiTou'
			},
			{
				fieldLabel: '快捷码',
				name: 'RBACRole_Shortcode'
			},
			{
				fieldLabel: '系统代码',
				name: 'RBACRole_UseCode'
			},
			{
				fieldLabel: '标准代码',
				name: 'RBACRole_StandCode'
			},
			{
				fieldLabel: '开发商码',
				name: 'RBACRole_DeveCode',
				itemId: 'RBACRole_DeveCode'
			},
			{
				fieldLabel: '角色描述',
				height: 85,
				labelAlign: 'top',
				name: 'RBACRole_Comment',
				xtype: 'textarea'
			},
			{
				boxLabel: '是否使用',
				name: 'RBACRole_IsUse',
				xtype: 'checkbox',
				checked: true
			},
			{
				fieldLabel: '时间戳',
				name: 'RBACRole_DataTimeStamp',
				hidden: true
			},
			{
				fieldLabel: '机构ID',
				name: 'RBACRole_LabID',
				itemId: 'RBACRole_LabID',
				hidden: true,
				value: me.LabId
			}
		];

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('RBACRole_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('RBACRole_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('RBACRole_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			CName: values.RBACRole_CName,
			EName: values.RBACRole_EName,
			PinYinZiTou: values.RBACRole_PinYinZiTou,

			SName: values.RBACRole_SName,
			DispOrder: values.RBACRole_DispOrder,

			UseCode: values.RBACRole_UseCode,
			StandCode: values.RBACRole_StandCode,
			DeveCode: values.RBACRole_DeveCode,

			Shortcode: values.RBACRole_Shortcode,
			IsUse: values.RBACRole_IsUse ? true : false,

			Comment: values.RBACRole_Comment,

			LabID: values.RBACRole_LabID
		};
		if (values.RBACRole_Id) entity.Id = values.RBACRole_Id;
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

		entity.entity.Id = values.RBACRole_Id;
		entity.entity.DataTimeStamp = values.RBACRole_DataTimeStamp.split(',')
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		//开发商代码为sys_admin 时,开发商代码字段只读
		var DeveCode = data.RBACRole_DeveCode;
		if (DeveCode && DeveCode == 'sys_admin') {
			me.getComponent('RBACRole_DeveCode').setReadOnly(true);
		}
		return data;
	}
});
