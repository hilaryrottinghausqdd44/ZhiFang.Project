/**
 * 输血检验项目
 * @author longfc
 * @version 2020-04-08
 */
Ext.define('Shell.class.sysbase.bloodbtestitem.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '输血检验项目信息',
	width: 240,
	height: 400,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBTestItemById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBTestItem',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBTestItemByField',
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
		var BloodBTestItem_CName = me.getComponent('BloodBTestItem_CName');
		BloodBTestItem_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodBTestItem_PinYinZiTou: value,
								BloodBTestItem_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodBTestItem_PinYinZiTou: "",
						BloodBTestItem_ShortCode: ""
					});
				}
			}
		});
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;

		var items = [{
				fieldLabel: '项目编码',
				name: 'BloodBTestItem_Id',
				itemId: 'BloodBTestItem_Id',
				//xtype: 'numberfield',
				type:"key",
				emptyText: '数字编码',
				locked: true,
				readOnly: true
			}, 
			{
				fieldLabel: '名称',
				name: 'BloodBTestItem_CName',
				itemId: 'BloodBTestItem_CName',
				emptyText: '必填项',
				allowBlank: false
			},
			{
				fieldLabel: '简称',
				name: 'BloodBTestItem_SName'
			},
			{
				fieldLabel: '拼音字头',
				name: 'BloodBTestItem_PinYinZiTou'
			},
			{
				fieldLabel: '快捷码',
				name: 'BloodBTestItem_ShortCode'
			},
			{
				fieldLabel: 'HisDispCode',
				name: 'BloodBTestItem_HisDispCode'
			},{
				fieldLabel: '显示次序',
				name: 'BloodBTestItem_DispOrder',
				xtype: 'numberfield',
				value: 0,
				allowBlank: false
			},
			{
				boxLabel: '是否使用',
				name: 'BloodBTestItem_IsUse',
				xtype: 'checkbox',
				checked: true
			},
			{
				boxLabel: '医嘱结果录入项',
				name: 'BloodBTestItem_IsResultItem',
				xtype: 'checkbox',
				checked: true
			},
			{
				boxLabel: '为输血前评估项',
				name: 'BloodBTestItem_IsPreTransEvalItem',
				xtype: 'checkbox',
				checked: true
			}
		];

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodBTestItem_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodBTestItem_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodBTestItem_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			CName: values.BloodBTestItem_CName,
			PinYinZiTou: values.BloodBTestItem_PinYinZiTou,
			SName: values.BloodBTestItem_SName,
			DispOrder: values.BloodBTestItem_DispOrder,
			ShortCode: values.BloodBTestItem_ShortCode,
			HisDispCode: values.BloodBTestItem_HisDispCode,
			IsUse: values.BloodBTestItem_IsUse ? true : false,
			IsResultItem: values.BloodBTestItem_IsResultItem ? true : false,
			IsPreTransEvalItem: values.BloodBTestItem_IsPreTransEvalItem ? true : false
		};
		if (values.BloodBTestItem_Id) entity.Id = values.BloodBTestItem_Id;
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
		entity.entity.Id = values.BloodBTestItem_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return data;
	}
});
