/**
 * 输血过程记录:临床处理结果
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.clinicalresults.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '临床处理结果',
	height: 36,
	/**内容周围距离*/
	bodyPadding: '5px 2px 0px 0px',
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 85,
		labelAlign: 'right'
	},
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemByContentTypeID?isPlanish=true&contentTypeId=5',

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
		//me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '临床处理结果',
			emptyText: '临床处理结果',
			xtype: 'uxCheckTrigger',
			name: 'BloodTransItem_BloodTransRecordTypeItem_CName',
			itemId: 'BloodTransItem_BloodTransRecordTypeItem_CName',
			className: 'Shell.class.sysbase.transrecordtypeitem.CheckGrid',
			classConfig: {
				title: '临床处理结果选择',
				/**默认数据条件*/
				defaultWhere: 'bloodtransrecordtypeitem.BloodTransRecordType.ContentTypeID=5',
			},
			listeners: {
				check: function(p, record) {
					me.onTypeItemCheck(p, record);
				}
			}
		}, {
			fieldLabel: '临床处理结果记录项ID',
			itemId: 'BloodTransItem_BloodTransRecordTypeItem_Id',
			name: 'BloodTransItem_BloodTransRecordTypeItem_Id',
			xtype: 'textfield',
			hidden: true
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
			fieldLabel: '输血记录项所属分类字典ID',
			name: 'BloodTransItem_BloodTransRecordType_Id',
			itemId: 'BloodTransItem_BloodTransRecordType_Id',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '内容分类ID',
			name: 'BloodTransItem_ContentTypeID',
			value: "5", //临床处理结果
			xtype: 'textfield',
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
	onTypeItemCheck: function(p, record, type1) {
		var me = this;
		var typeItemId = null,
			recordTypeId = null,
			typeItemName = null,
			dispOrder = null;
		recordTypeId = me.getComponent('BloodTransItem_BloodTransRecordType_Id');
		typeItemId = me.getComponent('BloodTransItem_BloodTransRecordTypeItem_Id');
		typeItemName = me.getComponent('BloodTransItem_BloodTransRecordTypeItem_CName');
		dispOrder = me.getComponent('BloodTransItem_DispOrder');
		if (recordTypeId) recordTypeId.setValue(record ? record.get('BloodTransRecordTypeItem_BloodTransRecordType_Id') :
			'');
		if (typeItemName) typeItemName.setValue(record ? record.get('BloodTransRecordTypeItem_CName') : '');
		if (typeItemId) typeItemId.setValue(record ? record.get('BloodTransRecordTypeItem_Id') : '');
		if (dispOrder) dispOrder.setValue(record ? record.get('BloodTransRecordTypeItem_DispOrder') : '');
		p.close();
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var dispOrder = 0;
		if (values.BloodTransItem_DispOrder) {
			dispOrder = values.BloodTransItem_DispOrder;
		}
		var entity = {
			"ContentTypeID": 5, // values.BloodTransItem_ContentTypeID,
			"DispOrder": dispOrder,
			"Visible": 1
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
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		if (me.formtype == "add") {
			data["BloodTransItem_Id"] = "";
			data["BloodTransItem_BloodTransRecordTypeItem_Id"] = "";
			data["BloodTransItem_BloodTransRecordTypeItem_CName"] = "";
		}
		return data;
	}
});
