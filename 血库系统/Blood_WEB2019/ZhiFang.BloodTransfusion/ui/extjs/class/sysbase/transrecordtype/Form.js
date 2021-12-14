/**
 * 输血过程记录分类字典管理
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.transrecordtype.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '输血过程记录分类信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordTypeById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransRecordType',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransRecordTypeByField',

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
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '编码',
			name: 'BloodTransRecordType_Id',
			itemId: 'BloodTransRecordType_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		},{
			fieldLabel: '内容分类',
			name: 'BloodTransRecordType_ContentTypeID',
			itemId: 'BloodTransRecordType_ContentTypeID',
			xtype: 'uxSimpleComboBox',
			value: '1',
			emptyText: '必填项',
			allowBlank: false,
			hasStyle: true,
			data: [
				['1', '输血记录项', 'color:green;'],
				['2', '不良反应分类', 'color:orange;'],
				['3', '临床处理措施', 'color:black;'],
				['4', '不良反应选择项', 'color:orange;'],
				['5', '临床处理结果', 'color:black;'],
				['6', '临床处理结果描述', 'color:black;']
			]
		},{
			fieldLabel: '输血记录类型',
			name: 'BloodTransRecordType_TransTypeId',
			itemId: 'BloodTransRecordType_TransTypeId',
			xtype: 'uxSimpleComboBox',
			emptyText: '必填项',
			allowBlank: false,
			hasStyle: true,
			data: [
				['1', '输血结束前', 'color:green;'],
				['2', '输血结束', 'color:orange;']
			]
		},  {
			fieldLabel: '类型编码',
			name: 'BloodTransRecordType_TypeCode',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '名称',
			name: 'BloodTransRecordType_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '次序',
			name: 'BloodTransRecordType_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '备注',
			height: 85,
			name: 'BloodTransRecordType_Memo',
			xtype: 'textarea'
		}, {
			boxLabel: '是否使用',
			name: 'BloodTransRecordType_IsVisible',
			xtype: 'checkbox',
			inputValue:true,
			checked: true
		});

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodTransRecordType_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodTransRecordType_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodTransRecordType_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id:-2,
			TransTypeId:values.BloodTransRecordType_TransTypeId,
			ContentTypeID: values.BloodTransRecordType_ContentTypeID,
			TypeCode: values.BloodTransRecordType_TypeCode,
			CName: values.BloodTransRecordType_CName,
			DispOrder: values.BloodTransRecordType_DispOrder,
			IsVisible: values.BloodTransRecordType_IsVisible ? true : false,
			Memo: values.BloodTransRecordType_Memo
		};
		if (values.BloodTransRecordType_Id) entity.Id = values.BloodTransRecordType_Id;
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

		for(var i in fields) {
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');

		entity.entity.Id = values.BloodTransRecordType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});