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
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodstyleById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BloodTransfusionManageService.svc/BT_UDTO_AddBloodstyle',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodstyleByField',

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
	BloodClassId: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Bloodstyle_CName = me.getComponent('Bloodstyle_CName');
		Bloodstyle_CName.on({
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
			fieldLabel: '所属分类编码',
			name: 'Bloodstyle_BloodClass_Id',
			itemId: 'Bloodstyle_BloodClass_Id',
			locked: true,
			readOnly: true,
			hidden:true
		},{
			fieldLabel: '所属分类',
			name: 'Bloodstyle_BloodClass_CName',
			itemId: 'Bloodstyle_BloodClass_CName',
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '编码',
			name: 'Bloodstyle_Id',
			itemId: 'Bloodstyle_Id',
			//xtype: 'numberfield',
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '名称',
			name: 'Bloodstyle_CName',
			itemId: 'Bloodstyle_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '简称',
			name: 'Bloodstyle_SName'
		}, {
			fieldLabel: '拼音字头',
			name: 'Bloodstyle_PinYinZiTou'
		}, {
			fieldLabel: '快捷码',
			name: 'Bloodstyle_ShortCode'
		}, {
			fieldLabel: '单位',
			name: 'Bloodstyle_BloodUnit_CName',
			itemId: 'Bloodstyle_BloodUnit_CName',
			emptyText: '单位',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.bloodunit.CheckGrid',
			editable: false,
			classConfig: {
				title: '单位选择'
			},
			listeners: {
				check: function(p, record) {
					var data = record ? record : '';
					me.onBloodUnitCheck(p, data);
				}
			}
		}, {
			fieldLabel: '单位Id',
			name: 'Bloodstyle_BloodUnit_Id',
			itemId: 'Bloodstyle_BloodUnit_Id',
			xtype: 'textfield',
			hidden: true,
		}, {
			fieldLabel: '次序',
			name: 'Bloodstyle_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否使用',
			name: 'Bloodstyle_Visible',
			xtype: 'checkbox',
			checked: true
		});

		return items;
	},
	/**@desc 弹出单位选择器选择确认后处理*/
	onBloodUnitCheck: function(p, data) {
		var me = this;
		var ManagerID = null,
			ManagerName = null;
		ManagerID = me.getComponent('PUser_Doctor_Id');
		ManagerName = me.getComponent('PUser_Doctor_CName');
		if (ManagerName) ManagerName.setValue(data ? data['Doctor_CName'] : '');
		if (ManagerID) ManagerID.setValue(data ? data['Doctor_Id'] : '');
		if (p) p.close();
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			CName: values.Bloodstyle_CName,
			PinYinZiTou: values.BloodClass_PinYinZiTou,
			SName: values.BloodClass_SName,
			ShortCode: values.BloodClass_ShortCode,
			DispOrder: values.Bloodstyle_DispOrder,
			Visible: values.Bloodstyle_Visible ? 1 : 0,
			Memo: values.Bloodstyle_Memo,
			BloodClass: {
				Id: me.BloodClassId,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			}
		};
		if (values.Bloodstyle_BloodUnit_Id) {
			entity.BloodUnit = {
				Id: values.Bloodstyle_BloodUnit_Id,
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
		entity.fields = fieldsArr.join(',');
		delete entity.entity.BloodClass;
		
		entity.entity.Id = values.Bloodstyle_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});
