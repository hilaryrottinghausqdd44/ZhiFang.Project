/**
 * 记录项类型字典
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordtype.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.ColorField'
	],

	title: '记录项类型字典信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCRecordType',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordTypeByField',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 60,
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
		var CName = me.getComponent('SCRecordType_CName');
		CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								SCRecordType_PinYinZiTou: value,
								SCRecordType_ShortCode: value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						SCRecordType_PinYinZiTou: "",
						SCRecordType_ShortCode: ""
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
			fieldLabel: '编码',
			name: 'SCRecordType_Id',
			itemId: 'SCRecordType_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '所属类别',
			name: 'SCRecordType_ContentTypeID',
			itemId: 'SCRecordType_ContentTypeID',
			xtype: 'uxSimpleComboBox',
			value: '10000',
			emptyText: '必填项',
			allowBlank: false,
			hasStyle: true,
			data: [
				['10000', '院感登记', 'color:green;'],
				['20000', '输血过程登记', 'color:orange;']
			]
		}, {
			fieldLabel: '编码',
			name: 'SCRecordType_TypeCode',
			emptyText: '必填项',
			allowBlank: false
		},  {
			fieldLabel: '项目对照码',
			name: 'SCRecordType_TestItemCode',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '样本类型对照码',
			name: 'SCRecordType_SampleTypeCode',
			emptyText: '必填项',
			allowBlank: false
		},{
			fieldLabel: '名称',
			name: 'SCRecordType_CName',
			itemId: 'SCRecordType_CName',
			xtype: 'textfield',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '背景颜色',
			name: 'SCRecordType_BGColor',
			itemId: 'SCRecordType_BGColor',
			xtype: 'colorfield'
		}, {
			fieldLabel: '快捷码',
			xtype: 'textfield',
			name: 'SCRecordType_ShortCode'
		}, {
			fieldLabel: '拼音字头',
			xtype: 'textfield',
			name: 'SCRecordType_PinYinZiTou'
		}, {
			fieldLabel: '次序',
			name: 'SCRecordType_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否使用',
			name: 'SCRecordType_IsUse',
			xtype: 'checkbox',
			inputValue: true,
			checked: true
		}, {
			fieldLabel: '<b style="color:blue;">描述</b>',
			height: 140,
			name: 'SCRecordType_Description',
			itemId: 'SCRecordType_Description',
			xtype: 'textarea'
		}, {
			fieldLabel: '备注',
			height: 85,
			name: 'SCRecordType_Memo',
			xtype: 'textarea'
		});

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('SCRecordType_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('SCRecordType_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('SCRecordType_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			ContentTypeID: values.SCRecordType_ContentTypeID,
			TypeCode: values.SCRecordType_TypeCode,
			TestItemCode: values.SCRecordType_TestItemCode,
			SampleTypeCode: values.SCRecordType_SampleTypeCode,
			CName: values.SCRecordType_CName,
			BGColor: values.SCRecordType_BGColor,
			ShortCode: values.SCRecordType_ShortCode,
			PinYinZiTou: values.SCRecordType_PinYinZiTou,
			DispOrder: values.SCRecordType_DispOrder,
			IsUse: values.SCRecordType_IsUse ? true : false
		};
		if (values.SCRecordType_Description) {
			entity.Description = Ext.String.escape(values.SCRecordType_Description);
		}
		entity.Memo = Ext.String.escape(values.SCRecordType_Memo);
		if (values.SCRecordType_Id) entity.Id = values.SCRecordType_Id;
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

		entity.entity.Id = values.SCRecordType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});
