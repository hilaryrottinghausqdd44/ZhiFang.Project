/**
 * 输血过程记录:发血记录主单表单信息
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.out.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '输血相应指标未完成;需要输入相应原因终止输血',
	width: 380,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutFormById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBOutForm',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBOutCourseCompletionByEndBloodOper',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 95,
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
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '发血单号',
			name: 'BloodBOutForm_Id',
			itemId: 'BloodBOutForm_Id',
			locked: true,
			readOnly: true,
			allowBlank: false
		}, {
			fieldLabel: '终止输血操作人Id',
			name: 'BloodBOutForm_EndBloodOperId',
			itemId: 'BloodBOutForm_EndBloodOperId',
			locked: true,
			readOnly: true,
			hidden: true
		}, {
			fieldLabel: '终止输血操作人',
			name: 'BloodBOutForm_EndBloodOperName',
			itemId: 'BloodBOutForm_EndBloodOperName',
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '终止输血原因Id',
			name: 'BloodBOutForm_BDEndBReason_Id',
			itemId: 'BloodBOutForm_BDEndBReason_Id',
			xtype: 'textfield',
			hidden: true,
		}, {
			fieldLabel: '终止输血原因',
			name: 'BloodBOutForm_BDEndBReason_CName',
			itemId: 'BloodBOutForm_BDEndBReason_CName',
			xtype: 'uxCheckTrigger',
			editable: false,
			allowBlank: false,
			emptyText: '请选择终止输血原因',
			blankText:"请选择终止输血原因后再操作!",
			className: 'Shell.class.sysbase.dict.CheckGrid',
			validator: function(val, valid) {
				return me.validityBDEndBReasonCName();
			},
			classConfig: {
				title: '终止输血原因选择',
				defaultWhere: "bdict.BDictType.DictTypeCode='BDEndBReason'",
			},
			listeners: {
				check: function(p, record) {
					me.onBDEndBReasonCheck(p, record);
				}
			}
		}, {
			fieldLabel: '终止输血备注',
			height: 85,
			name: 'BloodBOutForm_EndBloodReason',
			xtype: 'textarea'
		});

		return items;
	},
	/**@desc 终止输血原因验证*/
	validityBDEndBReasonCName: function() {
		var me = this;
		var endBReasonCName = me.getComponent('BloodBOutForm_BDEndBReason_CName');
		if (!endBReasonCName) {
			return "请选择终止输血原因后再操作！";
		} else {
			return true;
		}
	},
	/**@desc 弹出单位选择器选择确认后处理*/
	onBDEndBReasonCheck: function(p, record) {
		var me = this;
		var Id = null,
			CName = null;
		Id = me.getComponent('BloodBOutForm_BDEndBReason_Id');
		CName = me.getComponent('BloodBOutForm_BDEndBReason_CName');
		if (CName) CName.setValue(record ? record.get('BDict_CName') : '');
		if (Id) Id.setValue(record ? record.get('BDict_Id') : '');
		if (p) p.close();
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: values.BloodBOutForm_Id,
			EndBloodOperName: values.BloodBOutForm_EndBloodOperName,
			EndBloodReason: values.BloodBOutForm_EndBloodReason
		};
		if (values.BloodBOutForm_BDEndBReason_Id) {
			entity.BDEndBReason = {
				Id: values.BloodBOutForm_BDEndBReason_Id,				
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 1]
			};
		}
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		if (values.BloodBOutForm_EndBloodOperId) {
			entity.EndBloodOperId = values.BloodBOutForm_EndBloodOperId;
		} else {
			entity.EndBloodOperId = empID;
		}
		if(!entity.EndBloodOperName)entity.EndBloodOperName=empName;
		
		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate);
		if (curDateTime && JShell.Date.toServerDate(curDateTime)) {
			entity.EndBloodOperTime = JShell.Date.toServerDate(curDateTime);
		}

		return {
			entity: entity
		};
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		me.getForm().setValues({
			"BloodBOutForm_EndBloodOperId": empID,
			"BloodBOutForm_EndBloodOperName": empName
		});
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams();
			entity.entity.Id = values.BloodBOutForm_Id;
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";	
		var params = {
			"entity": entity.entity,
			"updateValue": "3",
			"empID": empID,
			"empName": empName
		};
		return params;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		if(data.BloodBOutForm_EndBloodOperId){
			data.BloodBOutForm_EndBloodOperId=empID;
			data.BloodBOutForm_EndBloodOperName=empName;
		}
		return data;
	}
});
