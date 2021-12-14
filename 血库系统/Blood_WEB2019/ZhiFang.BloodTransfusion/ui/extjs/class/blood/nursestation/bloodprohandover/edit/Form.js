/**
 * 护士站--血袋接收
 * @author longfc
 * @version 2020-03-17
 */
Ext.define('Shell.class.blood.nursestation.bloodprohandover.edit.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.picker.DateTime',
		'Shell.ux.form.field.DateTime',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '血袋接收登记信息',
	width: 240,
	height: 320,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagHandoverVOById?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagHandoverVO',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 70,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	/**血袋接收登记ID*/
	PK: null,
	/**主键字段*/
	PKField: 'BloodBagHandoverVO_BloodBagHandover_Id',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '登记时间',
			name: 'BloodBagHandoverVO_BloodBagHandover_BagOperTime',
			emptyText: '必填项',
			xtype: 'datetimefield', //datefield
			format: 'Y-m-d H:i:s',
			allowBlank: false
		}, {
			fieldLabel: '血袋外观',
			name: 'BloodBagHandoverVO_BloodAppearance_BDict_CName',
			itemId: 'BloodBagHandoverVO_BloodAppearance_BDict_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				title: '血袋外观选择',
				/**默认数据条件*/
				defaultWhere: "bdict.BDictType.DictTypeCode='BloodAppearance'"
			},
			listeners: {
				check: function(p, record) {
					me.onBDictCheck(p, record, "BloodAppearance");
				}
			}
		}, {
			fieldLabel: '血袋外观字典ID',
			name: 'BloodBagHandoverVO_BloodAppearance_BDict_Id',
			itemId: 'BloodBagHandoverVO_BloodAppearance_BDict_Id',
			hidden: true
		}, {
			fieldLabel: '血袋外观记录ID',
			name: 'BloodBagHandoverVO_BloodAppearance_Id',
			hidden: true
		}, {
			fieldLabel: '血袋完整性',
			name: 'BloodBagHandoverVO_BloodIntegrity_BDict_CName',
			itemId: 'BloodBagHandoverVO_BloodIntegrity_BDict_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				title: '血袋完整性选择',
				/**默认数据条件*/
				defaultWhere: "bdict.BDictType.DictTypeCode='BloodIntegrity'"
			},
			listeners: {
				check: function(p, record) {
					me.onBDictCheck(p, record, "BloodIntegrity");
				}
			}
		}, {
			fieldLabel: '血袋完整性字典ID',
			name: 'BloodBagHandoverVO_BloodIntegrity_BDict_Id',
			itemId: 'BloodBagHandoverVO_BloodIntegrity_BDict_Id',
			hidden: true
		}, {
			fieldLabel: '血袋完整性记录ID',
			name: 'BloodBagHandoverVO_BloodIntegrity_Id',
			hidden: true
		}, {
			fieldLabel: '主键ID',
			name: 'BloodBagHandoverVO_BloodBagHandover_Id',
			hidden: true
		});

		return items;
	},
	/**@desc 弹出字典选择确认后处理*/
	onBDictCheck: function(p, record, type1) {
		var me = this;
		var ManagerID = null,
			ManagerName = null;
		if (type1 == "BloodAppearance") {
			ManagerID = me.getComponent('BloodBagHandoverVO_BloodAppearance_BDict_Id');
			ManagerName = me.getComponent('BloodBagHandoverVO_BloodAppearance_BDict_CName');
		} else if (type1 == "BloodIntegrity") {
			ManagerID = me.getComponent('BloodBagHandoverVO_BloodIntegrity_BDict_Id');
			ManagerName = me.getComponent('BloodBagHandoverVO_BloodIntegrity_BDict_CName');
		}

		if (ManagerName) ManagerName.setValue(record ? record.get('BDict_CName') : '');
		if (ManagerID) ManagerID.setValue(record ? record.get('BDict_Id') : '');
		p.close();
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var bagOperTime = values.BloodBagHandoverVO_BloodBagHandover_BagOperTime;
		if(bagOperTime)bagOperTime=JShell.Date.toServerDate(bagOperTime);
		
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		var entity = {
			BloodBagHandover: {
				Id: values.BloodBagHandoverVO_BloodBagHandover_Id,
				BagOperTime: bagOperTime
			},
			BloodAppearance: {
				Id: values.BloodBagHandoverVO_BloodAppearance_Id,
				BDict: {
					Id: values.BloodBagHandoverVO_BloodAppearance_BDict_Id
				}
			},
			BloodIntegrity: {
				Id: values.BloodBagHandoverVO_BloodIntegrity_Id,
				BDict: {
					Id: values.BloodBagHandoverVO_BloodIntegrity_BDict_Id
				}
			}
		};

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
		/* for (var i in fields) {
			var arr = fields[i].split('_');
			if (arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(','); */
		
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		
		var bagOperTime = data.BloodBagHandoverVO_BloodBagHandover_BagOperTime;
		if (bagOperTime) {
			data.BloodBagHandoverVO_BloodBagHandover_BagOperTime = JcallShell.Date.toString(bagOperTime.replace(/\//g, "-"));
		}
		return data;
	}
});
