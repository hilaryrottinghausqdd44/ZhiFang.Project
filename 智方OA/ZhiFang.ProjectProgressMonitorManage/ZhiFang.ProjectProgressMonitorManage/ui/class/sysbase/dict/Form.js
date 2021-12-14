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
	selectUrl: '/BaseService.svc/ST_UDTO_SearchFDictById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BaseService.svc/ST_UDTO_AddFDict',
	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateFDictByField',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 30,
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
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '名称',
			name: 'FDict_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '编码',
			name: 'FDict_Shortcode',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '次序',
			name: 'FDict_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '备注',
			height: 85,
			name: 'FDict_Memo',
			xtype: 'textarea'
		}, {
			boxLabel: '是否使用',
			name: 'FDict_IsUse',
			xtype: 'checkbox',
			checked: true
		}, {
			fieldLabel: '主键ID',
			name: 'FDict_Id',
			hidden: true
		});

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			CName: values.FDict_CName,
			Shortcode: values.FDict_Shortcode,
			DispOrder: values.FDict_DispOrder,
			IsUse: values.FDict_IsUse ? true : false,
			Memo: values.FDict_Memo,
			FDictType: {
				Id: me.DictTypeId
			}
		};

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
		delete entity.entity.FDictType;

		entity.entity.Id = values.FDict_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});