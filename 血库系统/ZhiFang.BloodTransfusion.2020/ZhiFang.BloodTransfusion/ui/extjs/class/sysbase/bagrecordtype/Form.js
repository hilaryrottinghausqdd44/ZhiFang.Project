/**
 * 血袋记录类型
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.bagrecordtype.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '血袋记录类型',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordTypeById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagRecordType',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagRecordTypeByField',

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
		var BloodBagRecordType_CName = me.getComponent('BloodBagRecordType_CName');
		BloodBagRecordType_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodBagRecordType_PinYinZiTou: value,
								BloodBagRecordType_SName: value,
								BloodBagRecordType_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodBagRecordType_PinYinZiTou: "",
						BloodBagRecordType_SName: "",
						BloodBagRecordType_ShortCode: ""
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
			fieldLabel: '编号',
			name: 'BloodBagRecordType_Id',
			itemId: 'BloodBagRecordType_Id',
			type:"key",
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		},{
			fieldLabel: '内容分类',
			name: 'BloodBagRecordType_ContentTypeID',
			itemId: 'BloodBagRecordType_ContentTypeID',
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
				['6', '临床处理结果描述', 'color:black;'],
				
				['7', '入库核对', 'color:orange;'],
				['8', '库存检查', 'color:black;'],
				['9', '配血登记', 'color:orange;'],
				['10', '出库领用', 'color:black;'],
				['11', '血袋交接', 'color:black;'],
				['12', '血袋回收', 'color:black;']
			]
		}, {
			fieldLabel: '输血记录类型',
			name: 'BloodBagRecordType_TransTypeId',
			itemId: 'BloodBagRecordType_TransTypeId',
			xtype: 'uxSimpleComboBox',
			emptyText: '必填项',
			allowBlank: false,
			hasStyle: true,
			data: [
				['1', '输血结束前', 'color:green;'],
				['2', '输血结束', 'color:orange;']
			]
		}, {
			fieldLabel: '类型编码',
			name: 'BloodBagRecordType_TypeCode',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '名称',
			name: 'BloodBagRecordType_CName',
			itemId: 'BloodBagRecordType_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '简称',
			name: 'BloodBagRecordType_SName',
			emptyText: '简称',
			allowBlank: true
		},{
			fieldLabel: '快捷码',
			name: 'BloodBagRecordType_ShortCode',
			emptyText: '快捷码',
			allowBlank: true
		},{
			fieldLabel: '拼音字头',
			name: 'BloodBagRecordType_PinYinZiTou',
			emptyText: '拼音字头',
			allowBlank: true
		},{
			fieldLabel: '次序',
			name: 'BloodBagRecordType_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '备注',
			height: 85,
			name: 'BloodBagRecordType_Memo',
			xtype: 'textarea'
		}, {
			boxLabel: '是否使用',
			name: 'BloodBagRecordType_IsUse',
			xtype: 'checkbox',
			inputValue:true,
			checked: true
		});

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodBagRecordType_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodBagRecordType_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodBagRecordType_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id:-2,
			ContentTypeID: values.BloodBagRecordType_ContentTypeID,
			TransTypeId: values.BloodBagRecordType_TransTypeId,
			TypeCode: values.BloodBagRecordType_TypeCode,
			CName: values.BloodBagRecordType_CName,
			SName: values.BloodBagRecordType_SName,
			ShortCode: values.BloodBagRecordType_ShortCode,
			PinYinZiTou: values.BloodBagRecordType_PinYinZiTou,
			DispOrder: values.BloodBagRecordType_DispOrder,
			IsUse: values.BloodBagRecordType_IsUse ? true : false,
			Memo: values.BloodBagRecordType_Memo
		};
		if (values.BloodBagRecordType_Id) entity.Id = values.BloodBagRecordType_Id;
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
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		entity.empID = empID;
		entity.empName = empName;
		entity.entity.Id = values.BloodBagRecordType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});