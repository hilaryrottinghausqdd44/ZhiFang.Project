/**
 * 输血过程记录:主单信息
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.register.TransDocForm', {
	extend: 'Shell.class.blood.nursestation.transrecord.transdoc.DocForm',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '输血过程记录信息',
	height: 220,
	/**内容周围距离*/
	bodyPadding: '5px 2px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 85,
		width: 195,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**启用表单状态初始化*/
	openFormType: true,
	formtype: "add",
	//输血过程记录主单ID
	PK: null,
	//当前选中发血血袋记录集合
	outDtlRrecords: [],
	//表单默认值
	defaultsVals: {
		"BloodTransForm_BloodBReqForm_Id": "",
		"BloodTransForm_BloodBOutForm_Id": "",
		"BloodTransForm_BloodBOutItem_Id": "",
		"BloodTransForm_Bloodstyle_Id": "",
		"BloodTransForm_Bloodstyle_CName": "",
		"BloodTransForm_BBagCode": "",
		"BloodTransForm_PCode": "",
		"BloodTransForm_BloodBOutItem_BOutCount": "",
		"BloodTransForm_BloodBOutItem_BloodBUnit_BUnitName": ""
	},

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
		var me = this;
		var items = me.callParent(arguments);
		items.push({
			fieldLabel: '申请单Id',
			name: 'BloodTransForm_BloodBReqForm_Id',
			xtype: 'textfield',
			hidden: true,
			IsnotField: true,
			allowBlank: true
		}, {
			fieldLabel: '发血单Id',
			name: 'BloodTransForm_BloodBOutForm_Id',
			xtype: 'textfield',
			hidden: true,
			IsnotField: true,
			allowBlank: true
		}, {
			fieldLabel: '发血明细单Id',
			name: 'BloodTransForm_BloodBOutItem_Id',
			xtype: 'textfield',
			hidden: true,
			IsnotField: true,
			allowBlank: true
		}, {
			fieldLabel: '血制品Id',
			name: 'BloodTransForm_Bloodstyle_Id',
			xtype: 'textfield',
			hidden: true,
			IsnotField: true,
			allowBlank: true
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate);
		var hasAdverseReactions = values.BloodTransForm_HasAdverseReactions;
		if (hasAdverseReactions == true || hasAdverseReactions == 1) {
			hasAdverseReactions = 1;
		} else {
			hasAdverseReactions = 0;
		}
		var entity = {
			Visible: 1,
			BeforeCheck1: values.BloodTransForm_BeforeCheck1,
			BeforeCheck2: values.BloodTransForm_BeforeCheck2,
			TransCheck1: values.BloodTransForm_TransCheck1,
			TransCheck2: values.BloodTransForm_TransCheck2,
			HasAdverseReactions: hasAdverseReactions
		};
		if (values.BloodTransForm_BeforeCheckID1) {
			entity.BeforeCheckID1 = values.BloodTransForm_BeforeCheckID1;
		}
		if (values.BloodTransForm_BeforeCheckID2) {
			entity.BeforeCheckID2 = values.BloodTransForm_BeforeCheckID2;
		}
		if (values.BloodTransForm_TransCheckID1) {
			entity.TransCheckID1 = values.BloodTransForm_TransCheckID1;
		}
		if (values.BloodTransForm_TransCheckID2) {
			entity.TransCheckID2 = values.BloodTransForm_TransCheckID2;
		}
		var transBeginTime = values.BloodTransForm_TransBeginTime;
		if (transBeginTime && JShell.Date.toServerDate(transBeginTime)) {
			entity.TransBeginTime = JShell.Date.toServerDate(transBeginTime);
		}
		var transEndTime = values.BloodTransForm_TransEndTime;
		if (transEndTime && JShell.Date.toServerDate(transEndTime)) {
			entity.TransEndTime = JShell.Date.toServerDate(transEndTime);
		}
		var adverseReactionsTime = values.BloodTransForm_AdverseReactionsTime;
		if (adverseReactionsTime && JShell.Date.toServerDate(adverseReactionsTime)) {
			entity.AdverseReactionsTime = JShell.Date.toServerDate(adverseReactionsTime);
		}
		var adverseReactionsHP = values.BloodTransForm_AdverseReactionsHP;
		if (!adverseReactionsHP) {
			adverseReactionsHP = 0;
		}
		entity.AdverseReactionsHP = adverseReactionsHP;

		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		var reqId = values.BloodTransForm_BloodBReqForm_Id;
		if (reqId) {
			entity.BloodBReqForm = {
				Id: reqId,
				DataTimeStamp: dataTimeStamp
			}
		}
		var outDocId = values.BloodTransForm_BloodBOutForm_Id;
		if (outDocId) {
			entity.BloodBOutForm = {
				Id: outDocId,
				DataTimeStamp: dataTimeStamp
			}
		}
		//只有一个发血血袋时
		if (me.outDtlRrecords.length == 1) {
			entity.BBagCode = values.BloodTransForm_BBagCode;
			entity.PCode = values.BloodTransForm_PCode;
			var outDtlId = values.BloodTransForm_BloodBOutItem_Id;
			if (outDtlId) {
				entity.BloodBOutItem = {
					Id: outDtlId,
					DataTimeStamp: dataTimeStamp
				}
			}
			var bloodstyleId = values.BloodTransForm_Bloodstyle_Id;
			if (bloodstyleId) {
				entity.Bloodstyle = {
					Id: bloodstyleId,
					CName: values.BloodTransForm_Bloodstyle_CName,
					DataTimeStamp: dataTimeStamp
				}
			}
		}
		if(values.BloodTransForm_Id){
			entity.Id = values.BloodTransForm_Id;
		}else{
			entity.Id =-2;
		}
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
		entity.entity.Id = values.BloodTransForm_Id;
		return entity;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		//me.fireEvent('isAddAfter', me);
		me.setFormVals();
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		//me.fireEvent('isEditAfter', me);
		me.setFormVals();
	},
	setFormVals: function(values) {
		var me = this;
		if (values && values["BloodTransForm_BloodBReqForm_Id"]) {
			me.defaultsVals = values;
		}
		if (me.defaultsVals) me.getForm().setValues(me.defaultsVals);
	},
	getSaveInfo: function() {
		var me = this;
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		return params;
	}
});
