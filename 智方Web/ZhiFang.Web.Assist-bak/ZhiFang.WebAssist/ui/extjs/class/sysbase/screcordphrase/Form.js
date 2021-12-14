/**
 * 记录项短语
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordphrase.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '记录项短语信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordPhraseById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCRecordPhrase',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordPhraseByField',

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
	
	/**记录项字典ID*/
	BObjectId:"",
	/**短语类型业务表Id*/
	TypeObjectId:"",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var CName = me.getComponent('SCRecordPhrase_CName');
		CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								SCRecordPhrase_PinYinZiTou: value,
								SCRecordPhrase_ShortCode: value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						SCRecordPhrase_PinYinZiTou: "",
						SCRecordPhrase_ShortCode: ""
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
			name: 'SCRecordPhrase_Id',
			itemId: 'SCRecordPhrase_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		},{
			fieldLabel: '名称',
			name: 'SCRecordPhrase_CName',
			itemId: 'SCRecordPhrase_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '简称',
			name: 'SCRecordPhrase_SName',
			emptyText: ''
		}, {
			fieldLabel: '快捷码',
			name: 'SCRecordPhrase_ShortCode',
			emptyText: ''
		},{
			fieldLabel: '拼音字头',
			name: 'SCRecordPhrase_PinYinZiTou',
			emptyText: ''
		},{
			fieldLabel: '次序',
			name: 'SCRecordPhrase_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '备注',
			height: 85,
			name: 'SCRecordPhrase_Memo',
			xtype: 'textarea'
		}, {
			boxLabel: '是否使用',
			name: 'SCRecordPhrase_IsUse',
			xtype: 'checkbox',
			inputValue:true,
			checked: true
		});

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('SCRecordPhrase_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('SCRecordPhrase_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('SCRecordPhrase_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id:-2,
			CName: values.SCRecordPhrase_CName,
			SName: values.SCRecordPhrase_SName,
			ShortCode: values.SCRecordPhrase_ShortCode,
			PinYinZiTou: values.SCRecordPhrase_PinYinZiTou,
			DispOrder: values.SCRecordPhrase_DispOrder,
			IsUse: values.SCRecordPhrase_IsUse ? true : false,
			Memo: values.SCRecordPhrase_Memo
		};
		if (values.SCRecordPhrase_Id) entity.Id = values.SCRecordPhrase_Id;
		
		//如短语类型为按科室时，存储的是科室的Id
		if (me.TypeObjectId) entity.TypeObjectId = me.TypeObjectId;
		
		//记录项字典ID，存储记录项类型字典表Id或记录项字典表Id
		if (me.BObjectId) entity.BObjectId = vme.BObjectId;
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

		entity.entity.Id = values.SCRecordPhrase_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});