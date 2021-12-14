/**
 * 输血过程记录:病人体征信息
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.register.TransDtlForm', {
	extend: 'Shell.class.blood.nursestation.transrecord.transdtl.DtlForm',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '病人体征信息',
	/**内容周围距离*/
	bodyPadding: '5px 2px 0px 0px',
	height: 375,
	width: 725,
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 185,
		labelAlign: 'right'
	},
	formtype: "add",
	//输血过程记录主单ID
	PK: null,
	//fieldset的高度计算值
	fheight: 275,
	//fieldset的高度是否需要计算
	hasCalcFHeight: true,
	/**病人体征信息记录项集合信息*/
	transRecordTypeItemList: [],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.isAdd();
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('save');
		me.callParent(arguments);
	},
	getSaveInfo: function() {
		var me = this;
		var values = me.getForm().getValues();
		var entityList = [];
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];

		for (var i = 0; i < me.itemObjList.length; i++) {
			var itemObj = me.itemObjList[i];
			//没有输血记录项字典Id,直接忽略
			if (!itemObj.itemId) continue;

			var id = null; //输血过程记录明细Id
			if (itemObj.BloodTransItem && itemObj.BloodTransItem.Id) {
				id = itemObj.BloodTransItem.Id;
			}
			if (!id && itemObj.Id) {
				id = itemObj.Id;
			}
			//更新保存时,没有输血记录项Id,直接忽略
			if (me.formtype == "edit" && !id) continue;

			//输血记录项字典信息
			var recordTypeItem = itemObj.BloodTransRecordTypeItem;
			var transRecordTypeItem = {
				"Id": itemObj.itemId,
				"CName": itemObj.CName,
				"DataTimeStamp": dataTimeStamp
			};
			var dispOrder = 0;
			if (recordTypeItem && recordTypeItem.DispOrder) {
				dispOrder = recordTypeItem.DispOrder;
			}
			if (!itemObj.ContentTypeID) itemObj.ContentTypeID = 1;
			var itemValue = values[itemObj.itemId];
			if (!itemValue) itemValue = "";
			var entity = {
				"Visible": 1,
				"DispOrder": dispOrder,
				"ContentTypeID": itemObj.ContentTypeID,
				"TransItemResult": itemValue,
				"BloodTransRecordTypeItem": transRecordTypeItem,
			};
			if (id) {
				entity.Id = id;
			} else {
				entity.Id = -2;
			}
			//数字类型值处理
			if (itemValue && itemObj.xtype == 'numberfield') {
				entity.NumberItemResult = parseFloat(itemValue);
			}

			//输血过程记录分类字典信息
			if (itemObj.BloodTransRecordType) {
				entity.BloodTransRecordType = {
					"Id": itemObj.BloodTransRecordType.Id,
					"DataTimeStamp": dataTimeStamp
				};
			}
			//输血过程记录主单信息
			var transFormId = "";
			if (itemObj.BloodTransForm) {
				transFormId = itemObj.BloodTransForm.Id;
			}
			if (!transFormId && me.PK) {
				transFormId = me.PK;
			}
			if (transFormId) {
				entity.BloodTransForm = {
					"Id": transFormId,
					"DataTimeStamp": dataTimeStamp
				};
			}
			entityList.push(entity);
		}
		return entityList;
	}
});
