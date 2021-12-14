/**
 * 血袋记录项字典
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.bagrecorditem.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '血袋记录项字典信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordItemById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagRecordItem',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagRecordItemByField',

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
		var BloodBagRecordItem_CName = me.getComponent('BloodBagRecordItem_CName');
		BloodBagRecordItem_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodBagRecordItem_PinYinZiTou: value,
								BloodBagRecordItem_SName: value,
								BloodBagRecordItem_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodBagRecordItem_PinYinZiTou: "",
						BloodBagRecordItem_SName: "",
						BloodBagRecordItem_ShortCode: ""
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
			fieldLabel: '记录项编号',
			name: 'BloodBagRecordItem_Id',
			itemId: 'BloodBagRecordItem_Id',
			//xtype: 'numberfield',
			type:"key",
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '内容分类',
			name: 'BloodBagRecordItem_BloodBagRecordType_ContentTypeID',
			itemId: 'BloodBagRecordItem_BloodBagRecordType_ContentTypeID',
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
				['6', '临床处理结果描述', 'color:black;'],
				
				['7', '入库核对', 'color:orange;'],
				['8', '库存检查', 'color:black;'],
				['9', '配血登记', 'color:orange;'],
				['10', '出库领用', 'color:black;'],
				['11', '血袋交接', 'color:black;'],
				['12', '血袋回收', 'color:black;']
			]
		}, {
			fieldLabel: '编码',
			name: 'BloodBagRecordItem_ItemCode',
			xtype: 'textfield'
		}, {
			fieldLabel: '显示名称',
			name: 'BloodBagRecordItem_CName',
			itemId: 'BloodBagRecordItem_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '简称',
			name: 'BloodBagRecordItem_SName'
		}, {
			fieldLabel: '快捷码',
			name: 'BloodBagRecordItem_ShortCode',
			emptyText: '快捷码',
			allowBlank: true
		}, {
			fieldLabel: '拼音字头',
			name: 'BloodBagRecordItem_PinYinZiTou',
			emptyText: '拼音字头',
			allowBlank: true
		}, {
			fieldLabel: '显示次序',
			name: 'BloodBagRecordItem_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否使用',
			name: 'BloodBagRecordItem_IsUse',
			xtype: 'checkbox',
			inputValue: true,
			checked: true
		}, {
			fieldLabel: '辅助录入信息',
			name: 'BloodBagRecordItem_ItemEditInfo',
			xtype: 'textfield',
			hidden: true
		});
		//输血过程记录项
		items.push({
			fieldLabel: '<b style="color:blue;">显示类型</b>',
			name: 'BloodBagRecordItem_ItemXType',
			itemId: 'BloodBagRecordItem_ItemXType',
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
			name: 'BloodBagRecordItem_ItemDefaultValue',
			itemId: 'BloodBagRecordItem_ItemDefaultValue',
			IsnotField: true,
			hidden: true
		}, {
			fieldLabel: '<b style="color:blue;">单位名称</b>',
			name: 'BloodBagRecordItem_ItemUnit',
			itemId: 'BloodBagRecordItem_ItemUnit',
			IsnotField: true,
			hidden: true
		}, {
			fieldLabel: '<b style="color:blue;">数据源</b>',
			height: 120,
			name: 'BloodBagRecordItem_ItemDataSet',
			itemId: 'BloodBagRecordItem_ItemDataSet',
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
		me.getComponent('BloodBagRecordItem_BloodBagRecordType_ContentTypeID').setValue(me.ContentTypeID);
		if (me.ContentTypeID == "1") me.getComponent('BloodBagRecordItem_ItemXType').setValue("textfield");
		me.getComponent('BloodBagRecordItem_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.setItemVisible();
		me.callParent(arguments);
		me.setItemDataSetVisible();
		me.getComponent('BloodBagRecordItem_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.setItemVisible();
		me.callParent(arguments);
		me.setItemDataSetVisible();
		me.getComponent('BloodBagRecordItem_Id').setReadOnly(true);
	},
	//输血过程记录项信息
	setItemVisible: function() {
		var me = this;
		var visible = false;
		var contentTypeID = "" + me.ContentTypeID;
		if (contentTypeID == "1") {
			visible = true;
		}
		me.getComponent('BloodBagRecordItem_ItemXType').setVisible(visible);
		me.getComponent('BloodBagRecordItem_ItemDefaultValue').setVisible(visible);
		me.getComponent('BloodBagRecordItem_ItemUnit').setVisible(visible);
		me.getComponent('BloodBagRecordItem_ItemDataSet').setVisible(visible);
	},
	setItemDataSetVisible: function() {
		var me = this;
		var newValue = me.getComponent('BloodBagRecordItem_ItemXType').getValue();
		var visible = false;
		var contentTypeID = "" + me.ContentTypeID;
		if (contentTypeID == "1" && (newValue != "textfield" || newValue != "numberfield")) visible = true;
		me.getComponent('BloodBagRecordItem_ItemDataSet').setVisible(visible);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			ItemCode: values.BloodBagRecordItem_ItemCode,
			CName: values.BloodBagRecordItem_CName,
			SName: values.BloodBagRecordItem_SName,
			ShortCode: values.BloodBagRecordItem_ShortCode,
			PinYinZiTou: values.BloodBagRecordItem_PinYinZiTou,
			DispOrder: values.BloodBagRecordItem_DispOrder,
			IsUse: values.BloodBagRecordItem_IsUse ? true : false,
			BloodBagRecordType: {
				Id: me.DictTypeId,
				ContentTypeID: values.BloodBagRecordItem_BloodBagRecordType_ContentTypeID,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			}
		};
		//封闭辅助录入信息
		var contentTypeID = "" + me.ContentTypeID;
		if (contentTypeID == "1") {
			var itemDataSet = values.BloodBagRecordItem_ItemDataSet;
			itemDataSet = itemDataSet.replace(/"/g, "'");
			var itemEditInfo = {
				ItemXType: values.BloodBagRecordItem_ItemXType,
				ItemDefaultValue: values.BloodBagRecordItem_ItemDefaultValue,
				ItemUnit: values.BloodBagRecordItem_ItemUnit,
				ItemDataSet: itemDataSet
			};
			entity.TransItemEditInfo = JcallShell.JSON.encode(itemEditInfo);
		}
		if (values.BloodBagRecordItem_Id) entity.Id = values.BloodBagRecordItem_Id;
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
		delete entity.entity.BloodBagRecordType;
		entity.entity.Id = values.BloodBagRecordItem_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		//辅助录入信息
		var itemEditInfo = {
			ItemXType: "",
			ItemDefaultValue: "",
			ItemUnit: "",
			ItemDataSet: ""
		};
		var itemEditInfo1 = data["BloodBagRecordItem_ItemEditInfo"];
		if (itemEditInfo1) itemEditInfo1 = JShell.JSON.decode(itemEditInfo1);
		if (itemEditInfo1) itemEditInfo = itemEditInfo1;
		data["BloodBagRecordItem_ItemXType"] = itemEditInfo.ItemXType;
		data["BloodBagRecordItem_ItemDefaultValue"] = itemEditInfo.ItemDefaultValue;
		data["BloodBagRecordItem_ItemUnit"] = itemEditInfo.ItemUnit;
		data["BloodBagRecordItem_ItemDataSet"] = itemEditInfo.ItemDataSet;
		return data;
	}
});
