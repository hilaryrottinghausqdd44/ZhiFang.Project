/**
 * 字典信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.dict.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '字典信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_AddBDict',
	/**修改服务地址*/
	editUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBDictByField',

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

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var BDict_CName = me.getComponent('BDict_CName');
		BDict_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BDict_PinYinZiTou: value,
								BDict_SName: value,
								BDict_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BDict_PinYinZiTou: "",
						BDict_SName: "",
						BDict_ShortCode: ""
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
			fieldLabel: '字典编号',
			name: 'BDict_Id',
			itemId: 'BDict_Id',
			//xtype: 'numberfield',
			type:"key",
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '名称',
			name: 'BDict_CName',
			itemId: 'BDict_CName',
			emptyText: '必填项',
			allowBlank: false
		},{
			fieldLabel: '简称',
			name: 'BDict_SName',
			emptyText: '简称',
			allowBlank: true
		},{
			fieldLabel: '快捷码',
			name: 'BDict_ShortCode',
			emptyText: '快捷码',
			allowBlank: true
		},{
			fieldLabel: '拼音字头',
			name: 'BDict_PinYinZiTou',
			emptyText: '拼音字头',
			allowBlank: true
		}, {
			fieldLabel: '标准代码',
			name: 'BDict_StandCode',
			emptyText: '标准代码',
			allowBlank: true
		}, {
			fieldLabel: '业务类型编码',
			name: 'BDict_UseCode',
			emptyText: '字典类型编码',
			allowBlank: true
		},{
			fieldLabel: '开发商代码',
			name: 'BDict_DeveCode',
			emptyText: '开发商代码',
			allowBlank: true
		}, {
			fieldLabel: '次序',
			name: 'BDict_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '备注',
			height: 85,
			name: 'BDict_Memo',
			xtype: 'textarea'
		}, {
			boxLabel: '是否使用',
			name: 'BDict_IsUse',
			xtype: 'checkbox',
			checked: true
		});
	
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
	
		var entity = {
			Id: -2,
			CName: values.BDict_CName,
			SName: values.BDict_SName,
			ShortCode: values.BDict_ShortCode,
			PinYinZiTou: values.BDict_PinYinZiTou,
			UseCode: values.BDict_UseCode,
			StandCode: values.BDict_StandCode,
			DeveCode: values.BDict_DeveCode,
			DispOrder: values.BDict_DispOrder,
			IsUse: values.BDict_IsUse ? true : false,
			Memo: values.BDict_Memo,
			BDictType: {
				Id: me.DictTypeId,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			}
		};
		if (values.BDict_Id) entity.Id = values.BDict_Id;
		return {
			entity: entity
		};
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BDict_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BDict_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BDict_Id').setReadOnly(true);
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
		delete entity.entity.BDictType;

		entity.entity.Id = values.BDict_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});
