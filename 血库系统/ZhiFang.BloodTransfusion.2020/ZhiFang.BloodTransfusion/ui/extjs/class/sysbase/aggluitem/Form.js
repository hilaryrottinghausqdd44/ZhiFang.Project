/**
 * 凝集规则明细字典信息
 * @author longfc
 * @version 2020-04-08
 */
Ext.define('Shell.class.sysbase.aggluitem.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '凝集规则明细字典信息',
	width: 240,
	height: 400,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodAggluItemById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodAggluItem',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodAggluItemByField',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 60,
		labelAlign: 'right'
	},
	/**机构ID*/
	LabId: 0,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var BloodAggluItem_CName = me.getComponent('BloodAggluItem_CName');
		BloodAggluItem_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodAggluItem_PinYinZiTou: value,
								BloodAggluItem_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodAggluItem_PinYinZiTou: "",
						BloodAggluItem_ShortCode: ""
					});
				}
			}
		});
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;

		var items = [{
				fieldLabel: '血液分类',
				name: 'Bloodstyle_BloodClass_CName',
				itemId: 'Bloodstyle_BloodClass_CName',
				emptyText: '血液分类',
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.sysbase.bloodclass.CheckGrid',
				editable: false,
				classConfig: {
					title: '血液分类选择'
				},
				listeners: {
					check: function(p, record) {
						var data = record ? record : '';
						me.onBloodClassCheck(p, data);
					}
				}
			}, {
				fieldLabel: '血液分类Id',
				name: 'Bloodstyle_BloodClass_Id',
				itemId: 'Bloodstyle_BloodClass_Id',
				xtype: 'textfield',
				hidden: true,
			},{
			fieldLabel: '凝集字典',
			name: 'Bloodstyle_BloodAgglu_CName',
			itemId: 'Bloodstyle_BloodAgglu_CName',
			emptyText: '凝集字典',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			editable: false,
			classConfig: {
				title: '凝集字典选择',
				defaultWhere:"bdict.BDictType.DictTypeCode='BloodAgglu'",
			},
			listeners: {
				check: function(p, record) {
					var data = record ? record : '';
					me.onBloodAggluCheck(p, data);
				}
			}
		}, {
			fieldLabel: '凝集字典Id',
			name: 'Bloodstyle_BloodAgglu_Id',
			itemId: 'Bloodstyle_BloodAgglu_Id',
			xtype: 'textfield',
			hidden: true,
		}, {
				fieldLabel: '编号',
				name: 'BloodAggluItem_Id',
				itemId: 'BloodAggluItem_Id',
				type:"key",
				//xtype: 'numberfield',
				emptyText: '数字编码',
				allowBlank: false,
				locked: true,
				readOnly: true
			},
			{
				fieldLabel: '凝集名称',
				name: 'BloodAggluItem_CName',
				itemId: 'BloodAggluItem_CName',
				emptyText: '必填项',
				allowBlank: false
			},
			{
				fieldLabel: '简称',
				name: 'BloodAggluItem_SName'
			},
			{
				fieldLabel: '快捷码',
				name: 'BloodAggluItem_ShortCode'
			},
			{
				fieldLabel: '拼音字头',
				name: 'BloodAggluItem_PinYinZiTou'
			},
			{
				fieldLabel: '可配RH小因子',
				name: 'BloodAggluItem_AggluItemName'
			},
			{
				fieldLabel: '优先级',
				name: 'BloodAggluItem_RhPriority',
				itemId: 'BloodAggluItem_RhPriority',
				xtype: 'uxSimpleComboBox',
				value: '1',
				hasStyle: true,
				data: [
					['1', '普通', 'color:green;'],
					['2', '紧急', 'color:black;'],
					['3', '特殊', 'color:black;']
				]
			},
			{
				boxLabel: '是否使用',
				name: 'BloodAggluItem_IsUse',
				xtype: 'checkbox',
				checked: true
			}, {
				fieldLabel: '显示次序',
				name: 'BloodAggluItem_DispOrder',
				xtype: 'numberfield',
				value: 0,
				allowBlank: false
			}
		];

		return items;
	},
	/**@desc 弹出血液分类选择器选择确认后处理*/
	onBloodClassCheck: function(p, data) {
		var me = this;
		var Id = null,
			CName = null;
		Id = me.getComponent('Bloodstyle_BloodClass_Id');
		CName = me.getComponent('Bloodstyle_BloodClass_CName');
		if (CName) CName.setValue(data ? data['BloodClass_CName'] : '');
		if (Id) Id.setValue(data ? data['BloodClass_Id'] : '');
		if (p) p.close();
	},
	/**@desc 弹出凝集字典选择器选择确认后处理*/
	onBloodAggluCheck: function(p, data) {
		var me = this;
		var Id = null,
			CName = null;
		Id = me.getComponent('Bloodstyle_BloodAgglu_Id');
		CName = me.getComponent('Bloodstyle_BloodAgglu_CName');
		if (CName) CName.setValue(data ? data['BDict_CName'] : '');
		if (Id) Id.setValue(data ? data['BDict_Id'] : '');
		if (p) p.close();
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodAggluItem_Id').setReadOnly(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodAggluItem_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodAggluItem_Id').setReadOnly(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			CName: values.BloodAggluItem_CName,
			PinYinZiTou: values.BloodAggluItem_PinYinZiTou,
			SName: values.BloodAggluItem_SName,
			ShortCode: values.BloodAggluItem_ShortCode,
			AggluItemName: values.BloodAggluItem_AggluItemName,
			RhPriority: values.BloodAggluItem_RhPriority,
			DispOrder: values.BloodAggluItem_DispOrder,
			IsUse: values.BloodAggluItem_IsUse ? true : false
		};
		if (values.BloodAggluItem_Id) entity.Id = values.BloodAggluItem_Id;
		if (values.BloodAggluItem_BloodClass_Id) {
			entity.BloodClass = {
				Id: values.BloodAggluItem_BloodClass_Id,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 1]
			};
		}
		if (values.BloodAggluItem_BloodAgglu_Id) {
			entity.BloodAgglu = {
				Id: values.BloodAggluItem_BloodAgglu_Id,
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
		entity.fields = fieldsArr.join(',');
		fieldsArr.push("BloodClass_Id");
		fieldsArr.push("BloodAgglu_Id");
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		entity.empID = empID;
		entity.empName = empName;
		entity.entity.Id = values.BloodAggluItem_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return data;
	}
});
