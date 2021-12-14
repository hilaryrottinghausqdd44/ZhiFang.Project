/**
 * 血制品维护
 * @author longfc
 * @version 2020-04-10
 */
Ext.define('Shell.class.sysbase.bloodstyle.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '血制品信息',
	width: 260,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodStyleById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodStyle',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodStyleByField',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 85,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,

	/**字典类型ID*/
	BloodClassId: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var BloodStyle_CName = me.getComponent('BloodStyle_CName');
		BloodStyle_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodClass_PinYinZiTou: value,
								BloodClass_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodClass_PinYinZiTou: "",
						BloodClass_ShortCode: ""
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
			fieldLabel: '所属分类编号',
			name: 'BloodStyle_BloodClass_Id',
			itemId: 'BloodStyle_BloodClass_Id',
			locked: true,
			readOnly: true,
			hidden: true
		}, {
			fieldLabel: '所属分类',
			name: 'BloodStyle_BloodClass_CName',
			itemId: 'BloodStyle_BloodClass_CName',
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '编号',
			name: 'BloodStyle_Id',
			itemId: 'BloodStyle_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '名称',
			name: 'BloodStyle_CName',
			itemId: 'BloodStyle_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '简称',
			name: 'BloodStyle_SName'
		}, {
			fieldLabel: '拼音字头',
			name: 'BloodStyle_PinYinZiTou'
		}, {
			fieldLabel: '快捷码',
			name: 'BloodStyle_ShortCode'
		},{
			fieldLabel: '单位',
			name: 'BloodStyle_BloodUnit_CName',
			itemId: 'BloodStyle_BloodUnit_CName',
			emptyText: '单位',
			xtype: 'uxCheckTrigger',
			editable: false,
			className: 'Shell.class.sysbase.bloodunit.CheckGrid',
			classConfig: {
				title: '单位选择'
			},
			listeners: {
				check: function(p, record) {
					me.onBloodUnitCheck(p, record);
				}
			}
		}, {
			fieldLabel: '单位Id',
			name: 'BloodStyle_BloodUnit_Id',
			itemId: 'BloodStyle_BloodUnit_Id',
			xtype: 'textfield',
			hidden: true,
		}, {
			fieldLabel: '贮存时长(天)',
			name: 'BloodStyle_StoreDays',
			xtype: 'numberfield',
			value: 0
		},{
			fieldLabel: '贮存温度单位',
			name: 'BloodStyle_BloodStoreCond_CName',
			itemId: 'BloodStyle_BloodStoreCond_CName',
			emptyText: '单位',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			editable: false,
			classConfig: {
				title: '贮存温度单位选择',
				defaultWhere:"bdict.BDictType.DictTypeCode='BloodStoreCond'",
			},
			listeners: {
				check: function(p, record) {
					me.onBloodStoreCondCheck(p, record);
				}
			}
		}, {
			fieldLabel: '单位Id',
			name: 'BloodStyle_BloodStoreCond_Id',
			itemId: 'BloodStyle_BloodStoreCond_Id',
			xtype: 'textfield',
			hidden: true,
		},{
			fieldLabel: '次序',
			name: 'BloodStyle_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否做交叉配血',
			name: 'BloodStyle_IsMakeBlood',
			xtype: 'checkbox',
			checked: true
		}, {
			boxLabel: '是否使用',
			name: 'BloodStyle_IsUse',
			xtype: 'checkbox',
			checked: true
		});

		return items;
	},
	/**@desc 弹出单位选择器选择确认后处理*/
	onBloodUnitCheck: function(p, record) {
		var me = this;
		var Id = null,
			CName = null;
		Id = me.getComponent('BloodStyle_BloodUnit_Id');
		CName = me.getComponent('BloodStyle_BloodUnit_CName');
		if (CName) CName.setValue(record ? record.get('BloodUnit_CName') : '');
		if (Id) Id.setValue(record ? record.get('BloodUnit_Id') : '');
		if (p) p.close();
	},
	/**@desc 弹出贮存温度单位选择器选择确认后处理*/
	onBloodStoreCondCheck: function(p, record) {
		var me = this;
		var Id = null,
			CName = null;
		Id = me.getComponent('BloodStyle_BloodStoreCond_Id');
		CName = me.getComponent('BloodStyle_BloodStoreCond_CName');
		if (CName) CName.setValue(record ? record.get('BDict_CName') : '');
		if (Id) Id.setValue(record ? record.get('BDict_Id') : '');
		if (p) p.close();
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			CName: values.BloodStyle_CName,
			PinYinZiTou: values.BloodClass_PinYinZiTou,
			SName: values.BloodClass_SName,
			DispOrder: values.BloodStyle_DispOrder,
			StoreDays: values.BloodStyle_StoreDays,
			IsMakeBlood: values.BloodStyle_IsMakeBlood ? 1 : 0,
			IsUse: values.BloodStyle_IsUse ? 1 : 0,
			Memo: values.BloodStyle_Memo,
			BloodClass: {
				Id: me.BloodClassId,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			}
		};
		if (values.BloodStyle_BloodUnit_Id) {
			entity.BloodUnit = {
				Id: values.BloodStyle_BloodUnit_Id,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 1]
			};
		}
		if (values.BloodStyle_BloodStoreCond_Id) {
			entity.BloodStoreCond = {
				Id: values.BloodStyle_BloodStoreCond_Id,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 1]
			};
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
		//fieldsArr.push("BloodClassId");
		fieldsArr.push("BloodUnit_Id");
		fieldsArr.push("BloodStoreCond_Id");
		entity.fields = fieldsArr.join(',');
		delete entity.entity.BloodClass;
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		entity.empID = empID;
		entity.empName = empName;
		entity.entity.Id = values.BloodStyle_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});
