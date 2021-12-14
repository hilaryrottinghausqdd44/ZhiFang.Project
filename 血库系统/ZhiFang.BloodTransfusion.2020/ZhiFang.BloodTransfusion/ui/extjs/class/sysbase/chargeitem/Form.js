/**
 * 费用项目信息
 * @author longfc
 * @version 2020-07-07
 */
Ext.define('Shell.class.sysbase.chargeitem.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '费用项目信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodChargeItem',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeItemByField',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 75,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,

	/**费用项目类型ID*/
	DictTypeId: null,
	DictTypeCName: null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var BloodChargeItem_CName = me.getComponent('BloodChargeItem_CName');
		BloodChargeItem_CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if (newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BloodChargeItem_PinYinZiTou: value,
								BloodChargeItem_SName: value,
								BloodChargeItem_ShortCode: value
							});
						});
					}, null, 800);
				} else {
					me.getForm().setValues({
						BloodChargeItem_PinYinZiTou: "",
						BloodChargeItem_SName: "",
						BloodChargeItem_ShortCode: ""
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
			fieldLabel: '编号',
			name: 'BloodChargeItem_Id',
			itemId: 'BloodChargeItem_Id',
			//xtype: 'numberfield',
			type:"key",
			emptyText: '数字编码',
			allowBlank: false,
			locked: true,
			readOnly: true
		}, {
			fieldLabel: '名称',
			name: 'BloodChargeItem_CName',
			itemId: 'BloodChargeItem_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '所属分类',
			name: 'BloodChargeItem_BloodChargeItemType_CName',
			itemId: 'BloodChargeItem_BloodChargeItemType_CName',
			emptyText: '所属分类',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.chargeitemtype.CheckGrid',
			editable: false,
			classConfig: {
				title: '所属分类选择'
			},
			listeners: {
				check: function(p, record) {
					me.onBloodChargeItemTypeCheck(p, record);
				}
			}
		}, {
			fieldLabel: '所属分类Id',
			name: 'BloodChargeItem_BloodChargeItemType_Id',
			itemId: 'BloodChargeItem_BloodChargeItemType_Id',
			xtype: 'textfield',
			hidden: true,
		},{
			fieldLabel: '简称',
			name: 'BloodChargeItem_SName',
			emptyText: '简称',
			allowBlank: true
		}, {
			fieldLabel: '快捷码',
			name: 'BloodChargeItem_ShortCode',
			emptyText: '快捷码',
			allowBlank: true
		}, {
			fieldLabel: '拼音字头',
			name: 'BloodChargeItem_PinYinZiTou',
			emptyText: '拼音字头',
			allowBlank: true
		}, {
			fieldLabel: '系数',
			name: 'BloodChargeItem_Modulus',
			xtype: 'numberfield',
			value: 1
		}, {
			fieldLabel: '入价',
			name: 'BloodChargeItem_InPrice',
			xtype: 'numberfield',
			value: 0
		}, {
			fieldLabel: '出价',
			name: 'BloodChargeItem_OutPrice',
			xtype: 'numberfield',
			value: 0
		}, {
			fieldLabel: '次序',
			name: 'BloodChargeItem_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否组合',
			name: 'BloodChargeItem_IsGroup',
			xtype: 'checkbox',
			checked: true
		}, {
			boxLabel: '是否使用',
			name: 'BloodChargeItem_IsUse',
			xtype: 'checkbox',
			checked: true
		}, {
			fieldLabel: 'HIS费用单位',
			name: 'BloodChargeItem_HisChargeItemUnits',
			emptyText: 'HIS费用单位',
			allowBlank: true
		}, {
			fieldLabel: 'HIS费用备注',
			name: 'BloodChargeItem_HisPriceDemo',
			emptyText: 'HIS费用备注',
			allowBlank: true
		}, {
			fieldLabel: '费用规格',
			height: 85,
			name: 'BloodChargeItem_ChargeItemSpec',
			xtype: 'textarea'
		});

		return items;
	},
	/**@desc 弹出所属分类选择器选择确认后处理*/
	onBloodChargeItemTypeCheck: function(p, record) {
		var me = this;
		var Id = me.getComponent('BloodChargeItem_BloodChargeItemType_Id');
		var CName = me.getComponent('BloodChargeItem_BloodChargeItemType_CName');
		if (CName) CName.setValue(record ? record.get('BloodChargeItemType_CName') : '');
		if (Id) Id.setValue(record ? record.get('BloodChargeItemType_Id') : '');
		if (p) p.close();
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -2,
			CName: values.BloodChargeItem_CName,
			SName: values.BloodChargeItem_SName,
			ShortCode: values.BloodChargeItem_ShortCode,
			PinYinZiTou: values.BloodChargeItem_PinYinZiTou,
			Modulus: values.BloodChargeItem_Modulus,
			InPrice: values.BloodChargeItem_InPrice,
			OutPrice: values.BloodChargeItem_OutPrice,
			DispOrder: values.BloodChargeItem_DispOrder,
			
			HisOrderCode: values.BloodChargeItem_HisOrderCode,
			HisChargeItemUnits: values.BloodChargeItem_HisChargeItemUnits,
			HisPriceDemo: values.BloodChargeItem_HisPriceDemo,
			IsGroup: values.BloodChargeItem_IsGroup ? true : false,
			IsUse: values.BloodChargeItem_IsUse ? true : false,
			ChargeItemSpec: values.BloodChargeItem_ChargeItemSpec
		};
		if (values.BloodChargeItem_BloodChargeItemType_Id) {
			entity.BloodChargeItemType = {
				Id: values.BloodChargeItem_BloodChargeItemType_Id,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			};
		}
		if (values.BloodChargeItem_Id) entity.Id = values.BloodChargeItem_Id;
		return {
			entity: entity
		};
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodChargeItem_Id').setReadOnly(false);
		if(me.DictTypeId){
			me.getForm().setValues({
				"BloodChargeItem_BloodChargeItemType_Id":me.DictTypeId,
				"BloodChargeItem_BloodChargeItemType_CName":me.DictTypeCName
			});
		}
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodChargeItem_Id').setReadOnly(true);
	},
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodChargeItem_Id').setReadOnly(true);
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
		fieldsArr.push("BloodChargeItemType_Id");
		entity.fields = fieldsArr.join(',');
		//delete entity.entity.BloodChargeItemType;
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		entity.empID = empID;
		entity.empName = empName;
		entity.entity.Id = values.BloodChargeItem_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});
