/**
 * 输血过程记录:临床处理结果描述
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.clinicalresultsdesc.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '临床处理结果描述',
	height: 90,
	/**内容周围距离*/
	bodyPadding: '2px 2px 0px 0px',
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 65,
		labelAlign: 'right'
	},
	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemByContentTypeID?isPlanish=true&contentTypeId=6',

	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**启用表单状态初始化*/
	openFormType: true,
	//输血过程记录主单ID
	PK: null,
	formtype: "add",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '临床处理结果记录项ID',
			itemId: 'BloodTransItem_BloodTransRecordTypeItem_Id',
			name: 'BloodTransItem_BloodTransRecordTypeItem_Id',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '临床处理结果记录项名称',
			name: 'BloodTransItem_BloodTransRecordTypeItem_CName',
			itemId: 'BloodTransItem_BloodTransRecordTypeItem_CName',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '输血记录项所属分类字典ID',
			name: 'BloodTransItem_BloodTransRecordType_Id',
			itemId: 'BloodTransItem_BloodTransRecordType_Id',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '描述',
			height: 65,
			name: 'BloodTransItem_TransItemResult',
			itemId: 'BloodTransItem_TransItemResult',
			xtype: 'textarea'
		}, {
			fieldLabel: '主键ID',
			name: 'BloodTransItem_Id',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '输血记录主单ID',
			name: 'BloodTransItem_BloodTransForm_Id',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '内容分类ID',
			name: 'BloodTransItem_ContentTypeID',
			xtype: 'textfield',
			value: "6", //临床处理结果描述
			hidden: true
		}, {
			fieldLabel: '是否显示',
			name: 'BloodTransItem_Visible',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '显示次序',
			name: 'BloodTransItem_DispOrder',
			xtype: "numberfield",
			hidden: true
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var transItemResult = values.BloodTransItem_TransItemResult;
		if (transItemResult) {
			transItemResult = transItemResult.replace(/\\/g, '&#92').replace(/[\r\n]/g, '<br />');
		}
		var dispOrder = 0;
		if (values.BloodTransItem_DispOrder) {
			dispOrder = values.BloodTransItem_DispOrder;
		}
		var entity = {
			"Visible": 1,
			"ContentTypeID": 6, //values.BloodTransItem_ContentTypeID,
			"DispOrder": dispOrder,
			"TransItemResult": transItemResult
		};
		var transItemId = values.BloodTransItem_Id;
		if (transItemId) {
			entity.Id = transItemId;
		} else {
			entity.Id = -2;
		}
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		var transId = values.BloodTransItem_BloodTransForm_Id;
		if (transId) {
			entity.BloodTransForm = {
				Id: transId,
				DataTimeStamp: dataTimeStamp
			}
		}
		var recordTypeId = values.BloodTransItem_BloodTransRecordType_Id;
		if (recordTypeId) {
			entity.BloodTransRecordType = {
				Id: recordTypeId,
				DataTimeStamp: dataTimeStamp
			}
		}
		var typeItemId = values.BloodTransItem_BloodTransRecordTypeItem_Id;
		if (typeItemId) {
			entity.BloodTransRecordTypeItem = {
				Id: typeItemId,
				CName: values.BloodTransItem_BloodTransRecordTypeItem_CName,
				DataTimeStamp: dataTimeStamp
			}
		}
		return {
			entity: entity
		};
	},
	/**@overwrite isAdd*/
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		//me.load(-1);
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		if (me.formtype == "add" && data) {
			data["BloodTransItem_Id"] = "";
		}
		if (data.BloodTransItem_TransItemResult) {
			var reg = new RegExp("<br />", "g");
			data.BloodTransItem_TransItemResult = data.BloodTransItem_TransItemResult.replace(reg, "\r\n");
		}
		return data;
	}
});
