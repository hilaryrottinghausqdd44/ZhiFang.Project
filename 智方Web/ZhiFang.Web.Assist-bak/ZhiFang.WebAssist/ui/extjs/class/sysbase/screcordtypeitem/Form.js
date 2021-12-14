/**
 * 记录项字典
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordtypeitem.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.ColorField'
	],
	title: '记录项字典信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeItemById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCRecordTypeItem',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordTypeItemByField',

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

	/**字典类型ID*/
	DictTypeId: null,
	/**字典类型的内容分类*/
	ContentTypeID: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var CName = me.getComponent('SCRecordTypeItem_CName');
		CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								SCRecordTypeItem_PinYinZiTou: value,
								SCRecordTypeItem_ShortCode: value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						SCRecordTypeItem_PinYinZiTou: "",
						SCRecordTypeItem_ShortCode: ""
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
			fieldLabel: '记录项编码',
			name: 'SCRecordTypeItem_Id',
			itemId: 'SCRecordTypeItem_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '对照编码',
			name: 'SCRecordTypeItem_ItemCode',
			xtype: 'textfield'
		}, {
			fieldLabel: '显示名称',
			xtype: 'textfield',
			name: 'SCRecordTypeItem_CName',
			itemId: 'SCRecordTypeItem_CName',
			emptyText: '必填项',
			allowBlank: false
		},{
			fieldLabel: '背景颜色',
			name: 'SCRecordTypeItem_BGColor',
			itemId: 'SCRecordTypeItem_BGColor',
			xtype: 'colorfield'
		},  {
			fieldLabel: '简称',
			xtype: 'textfield',
			name: 'SCRecordTypeItem_SName'
		}, {
			fieldLabel: '快捷码',
			xtype: 'textfield',
			name: 'SCRecordTypeItem_ShortCode'
		}, {
			fieldLabel: '拼音字头',
			xtype: 'textfield',
			name: 'SCRecordTypeItem_PinYinZiTou'
		}, {
			fieldLabel: '显示次序',
			name: 'SCRecordTypeItem_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否使用',
			name: 'SCRecordTypeItem_IsUse',
			xtype: 'checkbox',
			inputValue: true,
			checked: true
		}, {
			fieldLabel: '辅助录入信息',
			name: 'SCRecordTypeItem_ItemEditInfo',
			xtype: 'textfield',
			hidden: true
		});
		//输血过程记录项
		items.push({
			fieldLabel: '<b style="color:blue;">显示类型</b>',
			name: 'SCRecordTypeItem_ItemXType',
			itemId: 'SCRecordTypeItem_ItemXType',
			xtype: 'uxSimpleComboBox',
			value: 'editComboBox',
			hasStyle: false,
			data: [
				['textfield', '文本录入框'],
				['numberfield', '数字录入框'],
				['radiogroup', '单选组框'],
				['uxSimpleComboBox', '下拉选择框'],
				['editComboBox', '下拉编辑框'],
				['datetimefield', '日期时间框'],
				['datefield', '日期选择框'],
				['timefield', '时间选择框']
			],
			listeners: {
				change: function(p, newValue, oldValue) {
					me.setItemDataSetVisible();
				}
			}
		}, {
			fieldLabel: '<b style="color:blue;">默认值</b>',
			xtype: 'textfield',
			name: 'SCRecordTypeItem_DefaultValue',
			itemId: 'SCRecordTypeItem_DefaultValue'
		}, {
			fieldLabel: '<b style="color:blue;">单位名称</b>',
			xtype: 'textfield',
			name: 'SCRecordTypeItem_ItemUnit',
			itemId: 'SCRecordTypeItem_ItemUnit'
		}, {
			fieldLabel: '<b style="color:blue;">描述</b>',
			height: 120,
			name: 'SCRecordTypeItem_Description',
			itemId: 'SCRecordTypeItem_Description',
			xtype: 'textarea'
		});
		return items;
	},
	isAdd: function() {
		var me = this;
		me.setItemVisible();
		me.callParent(arguments);
		me.getComponent('SCRecordTypeItem_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.setItemVisible();
		me.callParent(arguments);
		me.setItemDataSetVisible();
		me.getComponent('SCRecordTypeItem_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.setItemVisible();
		me.callParent(arguments);
		me.setItemDataSetVisible();
		me.getComponent('SCRecordTypeItem_Id').setReadOnly(true);
	},
	//输血过程记录项信息
	setItemVisible: function() {
		var me = this;
	},
	setItemDataSetVisible: function() {
		var me = this;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			ItemCode: values.SCRecordTypeItem_ItemCode,
			CName: values.SCRecordTypeItem_CName,
			BGColor: values.SCRecordTypeItem_BGColor,
			SName: values.SCRecordTypeItem_SName,
			ShortCode: values.SCRecordTypeItem_ShortCode,
			PinYinZiTou: values.SCRecordTypeItem_PinYinZiTou,
			DispOrder: values.SCRecordTypeItem_DispOrder,
			IsUse: values.SCRecordTypeItem_IsUse ? true : false,
			ItemXType: values.SCRecordTypeItem_ItemXType,
			ItemUnit: values.SCRecordTypeItem_ItemUnit,
			DefaultValue: values.SCRecordTypeItem_DefaultValue,
			Description: values.SCRecordTypeItem_Description
		};
		//封闭辅助录入信息
		var ItemEditInfo = {};
		entity.ItemEditInfo = JcallShell.JSON.encode(ItemEditInfo);
		if (values.SCRecordTypeItem_Id) entity.Id = values.SCRecordTypeItem_Id;
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
		delete entity.entity.SCRecordType;
		entity.entity.Id = values.SCRecordTypeItem_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		//辅助录入信息
		/* 		var transItemEditInfo = {
					ItemXType: "",
					ItemUnit: "",
					ItemDataSet: ""
				}; */
		//		var transItemEditInfo1 = data["SCRecordTypeItem_ItemEditInfo"];
		//		if(transItemEditInfo1) transItemEditInfo1 = JShell.JSON.decode(transItemEditInfo1);
		//		if(transItemEditInfo1) transItemEditInfo = transItemEditInfo1;
		//		data["SCRecordTypeItem_ItemXType"] = transItemEditInfo.ItemXType;
		//		data["SCRecordTypeItem_ItemUnit"] = transItemEditInfo.ItemUnit;
		return data;
	}
});
