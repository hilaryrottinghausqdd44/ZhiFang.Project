/**
 * 输血过程记录项字典管理
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.transrecordtypeitem.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '字典信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordTypeItemById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransRecordTypeItem',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransRecordTypeItemByField',

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
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '记录项编码',
			name: 'BloodTransRecordTypeItem_Id',
			itemId: 'BloodTransRecordTypeItem_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '内容分类',
			name: 'BloodTransRecordTypeItem_BloodTransRecordType_ContentTypeID',
			itemId: 'BloodTransRecordTypeItem_BloodTransRecordType_ContentTypeID',
			xtype: 'uxSimpleComboBox',
			locked: true,
			readOnly: true,
			hasStyle: true,
			data: [
				['1', '输血记录项', 'color:green;'],
				['2', '不良反应分类', 'color:orange;'],
				['3', '临床处理措施', 'color:black;'],
				['4', '不良反应选择项', 'color:orange;'],
				['5', '临床处理结果', 'color:black;'],
				['6', '临床处理结果描述', 'color:black;']
			]
		}, {
			fieldLabel: '编码',
			name: 'BloodTransRecordTypeItem_TransItemCode',
			xtype: 'textfield'
		}, {
			fieldLabel: '显示名称',
			name: 'BloodTransRecordTypeItem_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '简称',
			name: 'BloodTransRecordTypeItem_SName'
		}, {
			fieldLabel: '显示次序',
			name: 'BloodTransRecordTypeItem_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否使用',
			name: 'BloodTransRecordTypeItem_IsVisible',
			xtype: 'checkbox',
			inputValue: true,
			checked: true
		}, {
			fieldLabel: '辅助录入信息',
			name: 'BloodTransRecordTypeItem_TransItemEditInfo',
			xtype: 'textfield',
			hidden: true
		});
		//输血过程记录项
		items.push({
			fieldLabel: '<b style="color:blue;">显示类型</b>',
			name: 'BloodTransRecordTypeItem_ItemXType',
			itemId: 'BloodTransRecordTypeItem_ItemXType',
			xtype: 'uxSimpleComboBox',
			hasStyle: false,
			hidden: true,
			IsnotField: true,
			data: [
				['textfield', '文本录入框'],
				['numberfield', '数字录入框'],
				['radiogroup', '单选组框'],
				['uxSimpleComboBox', '下拉选择框']
			],
			listeners: {
				change: function(p, newValue, oldValue) {
					me.setItemDataSetVisible();
				}
			}
		}, {
			fieldLabel: '<b style="color:blue;">默认值</b>',
			name: 'BloodTransRecordTypeItem_ItemDefaultValue',
			itemId: 'BloodTransRecordTypeItem_ItemDefaultValue',
			IsnotField: true,
			hidden: true
		}, {
			fieldLabel: '<b style="color:blue;">单位名称</b>',
			name: 'BloodTransRecordTypeItem_ItemUnit',
			itemId: 'BloodTransRecordTypeItem_ItemUnit',
			IsnotField: true,
			hidden: true
		}, {
			fieldLabel: '<b style="color:blue;">数据源</b>',
			height: 120,
			name: 'BloodTransRecordTypeItem_ItemDataSet',
			itemId: 'BloodTransRecordTypeItem_ItemDataSet',
			xtype: 'textarea',
			IsnotField: true,
			hidden: true
		});
		return items;
	},
	isAdd: function() {
		var me = this;
		me.setItemVisible();
		me.callParent(arguments);
		me.getComponent('BloodTransRecordTypeItem_BloodTransRecordType_ContentTypeID').setValue(me.ContentTypeID);
		if (me.ContentTypeID == "1") me.getComponent('BloodTransRecordTypeItem_ItemXType').setValue("textfield");
		me.getComponent('BloodTransRecordTypeItem_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.setItemVisible();
		me.callParent(arguments);
		me.setItemDataSetVisible();
		me.getComponent('BloodTransRecordTypeItem_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.setItemVisible();
		me.callParent(arguments);
		me.setItemDataSetVisible();
		me.getComponent('BloodTransRecordTypeItem_Id').setReadOnly(true);
	},
	//输血过程记录项信息
	setItemVisible: function() {
		var me = this;
		var visible = false;
		var contentTypeID = "" + me.ContentTypeID;
		if (contentTypeID == "1") {
			visible = true;
		}
		me.getComponent('BloodTransRecordTypeItem_ItemXType').setVisible(visible);
		me.getComponent('BloodTransRecordTypeItem_ItemDefaultValue').setVisible(visible);
		me.getComponent('BloodTransRecordTypeItem_ItemUnit').setVisible(visible);
		me.getComponent('BloodTransRecordTypeItem_ItemDataSet').setVisible(visible);
	},
	setItemDataSetVisible: function() {
		var me = this;
		var newValue = me.getComponent('BloodTransRecordTypeItem_ItemXType').getValue();
		var visible = false;
		var contentTypeID = "" + me.ContentTypeID;
		if (contentTypeID == "1" && (newValue != "textfield" || newValue != "numberfield")) visible = true;
		me.getComponent('BloodTransRecordTypeItem_ItemDataSet').setVisible(visible);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			TransItemCode: values.BloodTransRecordTypeItem_TransItemCode,
			CName: values.BloodTransRecordTypeItem_CName,
			SName: values.BloodTransRecordTypeItem_SName,
			DispOrder: values.BloodTransRecordTypeItem_DispOrder,
			IsVisible: values.BloodTransRecordTypeItem_IsVisible ? true : false,
			BloodTransRecordType: {
				Id: me.DictTypeId,
				ContentTypeID: values.BloodTransRecordTypeItem_BloodTransRecordType_ContentTypeID,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			}
		};
		//封闭辅助录入信息
		var contentTypeID = "" + me.ContentTypeID;
		if (contentTypeID == "1") {
			var itemDataSet = values.BloodTransRecordTypeItem_ItemDataSet;
			itemDataSet = itemDataSet.replace(/"/g, "'");
			var transItemEditInfo = {
				ItemXType: values.BloodTransRecordTypeItem_ItemXType,
				ItemDefaultValue: values.BloodTransRecordTypeItem_ItemDefaultValue,
				ItemUnit: values.BloodTransRecordTypeItem_ItemUnit,
				ItemDataSet: itemDataSet
			};
			entity.TransItemEditInfo = JcallShell.JSON.encode(transItemEditInfo);
		}
		if (values.BloodTransRecordTypeItem_Id) entity.Id = values.BloodTransRecordTypeItem_Id;
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
		delete entity.entity.BloodTransRecordType;
		entity.entity.Id = values.BloodTransRecordTypeItem_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		//辅助录入信息
		var transItemEditInfo = {
			ItemXType: "",
			ItemDefaultValue: "",
			ItemUnit: "",
			ItemDataSet: ""
		};
		var transItemEditInfo1 = data["BloodTransRecordTypeItem_TransItemEditInfo"];
		if (transItemEditInfo1) transItemEditInfo1 = JShell.JSON.decode(transItemEditInfo1);
		if (transItemEditInfo1) transItemEditInfo = transItemEditInfo1;
		data["BloodTransRecordTypeItem_ItemXType"] = transItemEditInfo.ItemXType;
		data["BloodTransRecordTypeItem_ItemDefaultValue"] = transItemEditInfo.ItemDefaultValue;
		data["BloodTransRecordTypeItem_ItemUnit"] = transItemEditInfo.ItemUnit;
		data["BloodTransRecordTypeItem_ItemDataSet"] = transItemEditInfo.ItemDataSet;
		return data;
	}
});
