/**
 * 字典类型信息
 * @author longfc
 * @version 2020-07-10
 */
Ext.define('Shell.class.sysbase.dicttype.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '字典类型信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictTypeById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_AddBDictType',
	/**修改服务地址*/
	editUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBDictTypeByField',

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

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var BDictType_CName = me.getComponent('BDictType_CName');
		BDictType_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BDictType_PinYinZiTou: value,
								BDictType_SName: value,
								BDictType_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BDictType_PinYinZiTou: "",
						BDictType_SName: "",
						BDictType_ShortCode: ""
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
			fieldLabel: '类型编号',
			name: 'BDictType_Id',
			itemId: 'BDictType_Id',
			//xtype: 'numberfield',
			type:"key",
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		},{
			fieldLabel: '类型编码',
			name: 'BDictType_DictTypeCode',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '名称',
			name: 'BDictType_CName',
			itemId: 'BDictType_CName',
			emptyText: '必填项',
			allowBlank: false
		},{
			fieldLabel: '简称',
			name: 'BDictType_SName',
			emptyText: '简称',
			allowBlank: true
		},{
			fieldLabel: '快捷码',
			name: 'BDictType_ShortCode',
			emptyText: '快捷码',
			allowBlank: true
		},{
			fieldLabel: '拼音字头',
			name: 'BDictType_PinYinZiTou',
			emptyText: '拼音字头',
			allowBlank: true
		}, {
			fieldLabel: '业务编码',
			name: 'BDictType_UseCode',
			emptyText: '业务编码',
			allowBlank: true
		}, {
			fieldLabel: '标准代码',
			name: 'BDictType_StandCode',
			emptyText: '标准代码',
			allowBlank: true
		}, {
			fieldLabel: '开发商代码',
			name: 'BDictType_DeveCode',
			emptyText: '开发商代码',
			allowBlank: true
		}, {
			fieldLabel: '次序',
			name: 'BDictType_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '备注',
			height: 85,
			name: 'BDictType_Memo',
			xtype: 'textarea'
		}, {
			boxLabel: '是否使用',
			name: 'BDictType_IsUse',
			xtype: 'checkbox',
			checked: true
		});

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BDictType_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BDictType_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BDictType_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id:-2,
			DictTypeCode: values.BDictType_DictTypeCode,
			CName: values.BDictType_CName,
			SName: values.BDictType_SName,
			ShortCode: values.BDictType_ShortCode,
			PinYinZiTou: values.BDictType_PinYinZiTou,
			UseCode: values.BDictType_UseCode,
			StandCode: values.BDictType_StandCode,
			DeveCode: values.BDictType_DeveCode,
			DispOrder: values.BDictType_DispOrder,
			IsUse: values.BDictType_IsUse ? true : false,
			Memo: values.BDictType_Memo
		};
		if (values.BDictType_Id) entity.Id = values.BDictType_Id;
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
		
		entity.entity.Id = values.BDictType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});
