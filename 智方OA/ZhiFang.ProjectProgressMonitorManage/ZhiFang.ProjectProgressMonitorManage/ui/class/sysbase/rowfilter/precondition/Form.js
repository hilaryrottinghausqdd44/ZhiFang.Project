/***
 * 预置条件
 * @author longfc
 * @version 2017-06-14
 */
Ext.define('Shell.class.sysbase.rowfilter.precondition.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '预置条件',
	width: 320,
	bodyPadding: 5,
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 95,
		width: 160,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	/**新增服务地址*/
	addUrl: '/RBACService.svc/RBAC_UDTO_AddRBACRowFilterAndRBACRoleRightByPreconditionsId',
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACRowFilterById?isPlanish=true',
	editUrl: '/RBACService.svc/RBAC_UDTO_UpdateRBACRowFilterAndRBACRoleRightByFieldAndPreconditionsId',
	//预置条件选中行记录
	preconditionSelect: null,
	moduleOperId: null,
	PK: null,
	//宏命令本地缓存数据
	localMacroData: [],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			xtype: 'textfield',
			name: 'RBACRowFilter_CName',
			itemId: 'RBACRowFilter_CName',
			fieldLabel: '行过滤条件名称',
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			xtype: 'textfield',
			name: 'RBACRowFilter_RBACPreconditions_CName',
			itemId: 'RBACRowFilter_RBACPreconditions_CName',
			fieldLabel: '所属预置条件',
			locked: true,
			emptyText: '必填项',
			allowBlank: false,
			readOnly: true,
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			xtype: 'combobox',
			name: 'RBACRowFilter_StandCode',
			itemId: 'RBACRowFilter_StandCode',
			fieldLabel: '值类型',
			mode: 'local',
			editable: false,
			typeAhead: true,
			forceSelection: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			emptyText: '必填项',
			allowBlank: false,
			//readOnly: true,
			//locked: true,
			store: new Ext.data.Store({
				fields: ['value', 'text'],
				autoLoad: true,
				data: me.columnTypeList
			}),
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					me.valueTypeChange(com, newValue, oldValue, e, eOpts);
				}
			}
		}, {
			xtype: 'combobox',
			name: 'RBACPreconditions_SName',
			itemId: 'RBACPreconditions_SName',
			fieldLabel: '关系',
			colspan: 2,
			width: me.defaults.width * 2,
			mode: 'local',
			editable: false,
			typeAhead: true,
			forceSelection: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			autoSelect: true,
			store: new Ext.data.Store({
				fields: ['value', 'text'],
				autoLoad: true,
				data: me.stringTypeList,
				listeners: {
					load: function(store, records, successful, eOpts) {}
				}
			}),
			listeners: {
				//				select: function(com, records, eOpts) {
				//					me.operationTypeSelect(com, eOpts);
				//				},
				change: function(com, newValue, oldValue, e, eOpts) {
					me.operationTypeSelect(com, eOpts);
					me.setRowFilterCondition();
				}
			}
		}, {
			xtype: 'checkboxfield',
			itemId: 'RBACRowFilter_IsUse',
			name: 'RBACRowFilter_IsUse',
			fieldLabel: '',
			boxLabel: '是否使用',
			checked: true,
			inputValue: true,
			uncheckedValue: false,
			style: {
				marginLeft: '95px'
			}
		});
		//日期型结果录入值
		items.push({
			xtype: 'datefield',
			name: 'datefieldOne',
			itemId: 'datefieldOne',
			fieldLabel: '日期值一',
			value: '',
			format: 'Y-m-d',
			editable: true,
			hidden: true,
			validator: function(value) {
				if(value) {
					var datefieldOne = me.getComponent("datefieldOne").getValue();
					var datefieldTwo = me.getComponent("datefieldTwo").getValue();
					if(datefieldTwo) {
						var EndDateValue = JcallShell.Date.toString(datefieldTwo, true);
						var StartDateValue = JcallShell.Date.toString(datefieldOne, true);
						if(StartDateValue > EndDateValue) {
							value = "";
							return '大于日期值二';
						} else {
							return true;
						}
					} else {
						return true;
					}
				} else {
					return true;
				}
			}
		}, {
			xtype: 'datefield',
			name: 'datefieldTwo',
			itemId: 'datefieldTwo',
			fieldLabel: '日期值二',
			value: '',
			format: 'Y-m-d',
			editable: true,
			hidden: true,
			validator: function(value) {
				if(value) {
					var datefieldOne = me.getComponent("datefieldOne").getValue();
					var datefieldTwo = me.getComponent("datefieldTwo").getValue();
					if(datefieldOne) {
						var EndDateValue = JcallShell.Date.toString(datefieldTwo, true);
						var StartDateValue = JcallShell.Date.toString(datefieldOne, true);
						if(EndDateValue < StartDateValue) {
							value = "";
							return '小于日期值一';
						} else {
							return true;
						}
					} else {
						return true;
					}
				} else {
					return true;
				}
			}
		});
		//布尔勾选
		items.push({
			xtype: 'checkboxfield',
			itemId: 'booleanOne',
			name: 'booleanOne',
			hidden: true,
			fieldLabel: '布尔值',
			boxLabel: '',
			inputValue: 'true',
			uncheckedValue: 'false',
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					me.setRowFilterCondition();
				}
			}
		});
		//数值型结果录入值
		items.push({
			xtype: 'numberfield',
			hidden: true,
			name: 'numberfieldOne',
			itemId: 'numberfieldOne',
			decimalPrecision: 5,
			value: '',
			fieldLabel: '数值结果一',
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					me.setRowFilterCondition();
				}
			}
		}, {
			xtype: 'numberfield',
			hidden: true,
			value: '',
			decimalPrecision: 5,
			name: 'numberfieldTwo',
			itemId: 'numberfieldTwo',
			fieldLabel: '数值结果二',
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					me.setRowFilterCondition();
				}
			}
		});
		//字符型结果录入值
		items.push({
			height: 50,
			xtype: 'textarea',
			hidden: false,
			name: 'stringOne',
			itemId: 'stringOne',
			value: '',
			fieldLabel: '结果值一',
			emptyText: '多项录入用逗号(,)分割',
			validator: function(value) {
				if(value && value.indexOf('"') > -1) {
					return '不能包含非法字符！如双引号';
				} else {
					return true;
				}
			},
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					if(newValue && newValue.indexOf('"') > -1) {
						JcallShell.Msg.alert('不能包含非法字符！如双引号', null, 1000);
					} else {
						me.setRowFilterCondition();
					}
				}
			}
		}, {
			xtype: 'textfield',
			hidden: true,
			name: 'stringTwo',
			itemId: 'stringTwo',
			value: '',
			fieldLabel: '结果值二',
			validator: function(value) {
				if(value && value.indexOf('"') > -1) {
					return '不能包含非法字符！如双引号';
				} else {
					return true;
				}
			},
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					if(newValue && newValue.indexOf('"') > -1) {
						JcallShell.Msg.alert('不能包含非法字符！如双引号', null, 1000);
					} else {
						me.setRowFilterCondition();
					}
				}
			}
		});
		//宏下列框
		items.push({
			xtype: 'combobox',
			name: 'cbomacrosOne',
			itemId: 'cbomacrosOne',
			fieldLabel: '宏命令',
			hidden: true,
			mode: 'local',
			editable: true,
			typeAhead: true,
			forceSelection: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			value: '',
			store: new Ext.data.Store({
				fields: ['value', 'text'],
				autoLoad: true,
				data: []
			}),
			validator: function(value) {
				if(value && value.indexOf('"') > -1) {
					return '不能包含非法字符！如双引号';
				} else {
					return true;
				}
			},
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					if(newValue && newValue.indexOf('"') > -1) {
						JcallShell.Msg.alert('不能包含非法字符！如双引号', null, 1000);
					} else {
						me.setRowFilterCondition();
					}
				}
			}
		}, {
			xtype: 'combobox',
			name: 'cbomacrosTwo',
			itemId: 'cbomacrosTwo',
			fieldLabel: '宏命令二',
			hidden: true,
			mode: 'local',
			editable: true,
			typeAhead: true,
			forceSelection: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			value: '',
			store: new Ext.data.Store({
				fields: ['value', 'text'],
				autoLoad: true,
				data: []
			}),
			validator: function(value) {
				if(value && value.indexOf('"') > -1) {
					return '不能包含非法字符！如双引号';
				} else {
					return true;
				}
			},
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					if(newValue && newValue.indexOf('"') > -1) {
						JcallShell.Msg.alert('不能包含非法字符！如双引号', null, 1000);
					} else {
						me.setRowFilterCondition();
					}
				}
			}
		});

		items.push({
			fieldLabel: '行数据条件',
			emptyText: '必填项',
			allowBlank: false,
			name: 'RBACRowFilter_RowFilterCondition',
			itemId: 'RBACRowFilter_RowFilterCondition',
			height: 150,
			xtype: 'textarea',
			validator: function(value) {
				if(value && value.indexOf('"') > -1) {
					return '不能包含非法字符！如双引号';
				} else {
					return true;
				}
			},
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					if(newValue && newValue.indexOf('"') > -1) {
						JcallShell.Msg.alert('不能包含非法字符！如双引号', null, 1000);
					}
				}
			}
		});

		items.push({
			xtype: 'textfield',
			fieldLabel: '实体编码',
			name: 'RBACRowFilter_EntityCode',
			itemId: 'RBACRowFilter_EntityCode',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '实体编码',
			name: 'RBACRowFilter_EntityCName',
			itemId: 'RBACRowFilter_EntityCName',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '预置条件代码',
			name: 'RBACRowFilter_RBACPreconditions_EName',
			itemId: 'RBACRowFilter_RBACPreconditions_EName',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '主键ID',
			name: 'RBACRowFilter_Id',
			value: me.PK,
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '所属预置条件主键ID',
			name: 'RBACRowFilter_RBACPreconditions_Id',
			itemId: 'RBACRowFilter_RBACPreconditions_Id',
			hidden: true
		});
		return items;
	},
	//值类型改变后
	valueTypeChange: function(com, newValue, oldValue, e, eOpts) {
		var me = this;
		//日期
		var datefieldOne = me.getComponent('datefieldOne');
		var datefieldTwo = me.getComponent('datefieldTwo');
		//布尔勾选
		var booleanOne = me.getComponent('booleanOne');
		//数值型
		var numberfieldOne = me.getComponent('numberfieldOne');
		var numberfieldTwo = me.getComponent('numberfieldTwo');
		//字符型
		var stringOne = me.getComponent('stringOne');
		var stringTwo = me.getComponent('stringTwo');
		//宏命令
		var cbomacrosOne = me.getComponent('cbomacrosOne');
		var cbomacrosTwo = me.getComponent('cbomacrosTwo');

		datefieldOne.setVisible(false);
		datefieldTwo.setVisible(false);
		numberfieldOne.setVisible(false);
		numberfieldTwo.setVisible(false);

		stringOne.setVisible(false);
		stringTwo.setVisible(false);
		booleanOne.setVisible(false);
		booleanOne.setVisible(false);
		cbomacrosOne.setVisible(false);
		cbomacrosTwo.setVisible(false);

		stringOne.setValue("");
		stringTwo.setValue("");

		datefieldOne.setValue("");
		datefieldTwo.setValue("");

		numberfieldOne.setValue("");
		numberfieldTwo.setValue("");
		cbomacrosOne.setValue("");

		var oTypeCom = me.getComponent('RBACPreconditions_SName');
		switch(newValue) {
			case "date": //日期型
				oTypeCom.store.loadData(me.dateTypeList);
				oTypeCom.setValue("=");
				datefieldOne.setVisible(true);
				break;
			case "boolean": //布尔勾选
				oTypeCom.store.loadData(me.booleanTypeList);
				booleanOne.setVisible(true);
				break;
			case "string": //字符串
				oTypeCom.store.loadData(me.stringTypeList);
				stringOne.setVisible(true);
				oTypeCom.setValue("=");
				break;
			case "number": //数值型
				oTypeCom.store.loadData(me.numberTypeList);
				oTypeCom.setValue("=");
				numberfieldOne.setVisible(true);
				break;
			case "macros": //宏命令macrosOperationType
				oTypeCom.store.loadData(me.macrosOperationType);
				oTypeCom.setValue("=");
				cbomacrosOne.setVisible(true);
				me.getMacroCommandData();
				cbomacrosOne.store.loadData(me.localMacroData);
				cbomacrosTwo.store.loadData(me.localMacroData);
				oTypeCom.setValue("=");
				break;
			default:
				break;
		}
	},
	//关系选择后
	operationTypeSelect: function(com, eOpts) {
		var me = this;
		//值类型
		var valueType = me.getComponent("RBACRowFilter_StandCode");
		//日期
		var datefieldOne = me.getComponent('datefieldOne');
		var datefieldTwo = me.getComponent('datefieldTwo');
		//布尔勾选
		var booleanOne = me.getComponent('booleanOne');
		//数值型
		var numberfieldOne = me.getComponent('numberfieldOne');
		var numberfieldTwo = me.getComponent('numberfieldTwo');
		//字符型
		var stringOne = me.getComponent('stringOne');
		var stringTwo = me.getComponent('stringTwo');
		//宏命令
		var cbomacrosOne = me.getComponent('cbomacrosOne');
		var cbomacrosTwo = me.getComponent('cbomacrosTwo');

		datefieldOne.setVisible(false);
		datefieldTwo.setVisible(false);
		numberfieldOne.setVisible(false);
		numberfieldTwo.setVisible(false);

		stringOne.setVisible(false);
		stringTwo.setVisible(false);
		booleanOne.setVisible(false);
		cbomacrosOne.setVisible(false);
		cbomacrosTwo.setVisible(false);

		var newValue = valueType.getValue();
		var otype = com.getValue();
		switch(newValue) {
			case "date": //日期型
				datefieldOne.setVisible(true);
				if(otype == '>= and <=') { //区间(包含边界)(输入2项)
					datefieldTwo.setVisible(true);
				} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
					datefieldTwo.setVisible(true);
				} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
					datefieldTwo.setVisible(true);
				} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
					datefieldTwo.setVisible(true);
				}
				break;
			case "boolean": //布尔勾选
				booleanOne.setVisible(true);
				break;
			case "string": //字符串
				stringOne.setVisible(true);
				if(otype == 'like A%B') { //字符之间(A*B) (输入2项)
					stringTwo.setVisible(true);
				}
				break;
			case "number": //数值型
				numberfieldOne.setVisible(true);
				if(otype == '>= and <=') { //区间(包含边界)(输入2项)
					numberfieldTwo.setVisible(true);
				} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
					numberfieldTwo.setVisible(true);
				} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
					numberfieldTwo.setVisible(true);
				} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
					numberfieldTwo.setVisible(true);
				}
				break;
			case "macros": //宏命令macrosOperationType
				cbomacrosOne.setVisible(true);
				if(otype == '>= and <=') { //区间(包含边界)(输入2项)
					cbomacrosTwo.setVisible(true);
				} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
					cbomacrosTwo.setVisible(true);
				} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
					cbomacrosTwo.setVisible(true);
				} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
					cbomacrosTwo.setVisible(true);
				}
				break;
			default:
				break;
		}
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var me = this;
		var fields = ['RBACRowFilter_EntityCode', 'RBACRowFilter_EntityCName', 'RBACRowFilter_StandCode', 'RBACRowFilter_CName', 'RBACRowFilter_EName', 'RBACPreconditions_SName', 'RBACRowFilter_IsUse', 'RBACRowFilter_Id', 'RBACRowFilter_RowFilterCondition', 'RBACRowFilter_RowFilterConstruction', 'RBACRowFilter_RBACPreconditions_CName', 'RBACRowFilter_RBACPreconditions_Id', 'RBACRowFilter_RBACPreconditions_EName'];
		return fields;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var isUse = data.RBACRowFilter_IsUse;
		data.RBACRowFilter_IsUse = (isUse == true || isUse == "true" || isUse == "1" ? true : false);
		//还原
		if(data.RBACRowFilter_RowFilterConstruction) {
			var comment = JShell.JSON.decode(data.RBACRowFilter_RowFilterConstruction);
			if(comment.datefieldOne) comment.datefieldOne = JShell.Date.getDate(comment.datefieldOne);
			if(comment.datefieldTwo) comment.datefieldTwo = JShell.Date.getDate(comment.datefieldTwo);

			if(comment.booleanOne) comment.booleanOne = comment.booleanOne == "true" ? true : false;;
			data = Ext.Object.merge(data, comment);
		}
		return data;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent("RBACRowFilter_IsUse").setVisible(false);
		me.setDefaultValue();
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent("RBACRowFilter_IsUse").setVisible(true);
		me.setDefaultValue();
	},
	setDefaultValue: function() {
		var me = this;
		if(me.preconditionSelect) {
			var cNameValue = me.preconditionSelect.get("RBACPreconditions_CName");
			var valueTypeValue = me.preconditionSelect.get("RBACPreconditions_ValueType");
			var objValue = {
				"RBACRowFilter_RBACPreconditions_Id": me.preconditionSelect.get("RBACPreconditions_Id"),
				"RBACRowFilter_RBACPreconditions_CName": cNameValue,
				"RBACRowFilter_StandCode": valueTypeValue,
				"RBACRowFilter_RBACPreconditions_EName": me.preconditionSelect.get("RBACPreconditions_EName"),
				"RBACRowFilter_EntityCName": me.preconditionSelect.get("RBACPreconditions_EntityCName"),
				"RBACRowFilter_EntityCode": me.preconditionSelect.get("RBACPreconditions_EntityCode")
			};
			if(me.formtype == "add") {
				objValue["RBACRowFilter_CName"] = cNameValue;
			}
			me.getForm().setValues(objValue);
		}
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		if(values.RBACRowFilter_CName) {
			values.RBACRowFilter_CName = values.RBACRowFilter_CName.replace(/"/g, '');
		}
		var entity = {
			CName: values.RBACRowFilter_CName,
			EName: values.RBACRowFilter_RBACPreconditions_EName,
			StandCode: values.RBACRowFilter_StandCode,
			SName: values.RBACPreconditions_SName,
			EntityCName: values.RBACRowFilter_EntityCName,
			EntityCode: values.RBACRowFilter_EntityCode,
			IsUse: values.RBACRowFilter_IsUse,
			IsPreconditions: true
		};

		//还原用	
		if(values.stringOne) {
			values.stringOne = values.stringOne.replace(/"/g, '');
			values.stringOne = values.stringOne.replace(/\\/g, '');
			values.stringOne = values.stringOne.replace(/[\r\n]/g, '');
		}

		if(values.stringTwo) {
			values.stringTwo = values.stringTwo.replace(/"/g, '');
			values.stringTwo = values.stringTwo.replace(/\\/g, '');
			values.stringTwo = values.stringTwo.replace(/[\r\n]/g, '');
		}
		if(values.numberfieldOne) {
			values.numberfieldOne = values.numberfieldOne.replace(/"/g, '');
		}
		if(values.numberfieldTwo) {
			values.numberfieldTwo = values.numberfieldTwo.replace(/"/g, '');
		}
		if(values.datefieldOne) {
			values.datefieldOne = values.datefieldOne.replace(/"/g, '');
		}
		if(values.datefieldTwo) {
			values.datefieldTwo = values.datefieldTwo.replace(/"/g, '');
		}
		var comment = {
			stringOne: values.stringOne,
			stringTwo: values.stringTwo,
			datefieldOne: values.datefieldOne,
			datefieldTwo: values.datefieldTwo,
			numberfieldOne: values.numberfieldOne,
			numberfieldTwo: values.numberfieldTwo,
			booleanOne: values.booleanOne,
			cbomacrosOne: values.cbomacrosOne,
			cbomacrosTwo: values.cbomacrosTwo
		};

		if(values.RBACRowFilter_RowFilterCondition) {
			//非法字符的过滤处理
			entity.RowFilterCondition = values.RBACRowFilter_RowFilterCondition.replace(/"/g, '');
			entity.RowFilterCondition = entity.RowFilterCondition.replace(/\\/g, '');
			entity.RowFilterCondition = entity.RowFilterCondition.replace(/[\r\n]/g, '');
		}
		entity.RowFilterConstruction = JShell.JSON.encode(comment);

		if(values.RBACRowFilter_RBACPreconditions_Id) {
			entity.RBACPreconditions = {
				Id: values.RBACRowFilter_RBACPreconditions_Id
			};
			if(me.formtype == "add") {
				var strDataTimeStamp = "1,2,3,4,5,6,7,8";
				entity.RBACPreconditions.DataTimeStamp = strDataTimeStamp.split(',');
			}
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

		for(var i in fields) {
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',') + ",IsPreconditions,RBACPreconditions_Id"; //RBACPreconditions_Id
		entity.entity.Id = values.RBACRowFilter_Id;
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if(!me.getForm().isValid()) return;
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) return;
		params.moduleOperId = me.moduleOperId;
		me.fireEvent('beforesave', me, params);
	},
	saveing: function(params) {
		var me = this;
		me.showMask(me.saveText); //显示遮罩层
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		var id = params.entity.Id;
		params = JShell.JSON.encode(params);

		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object') {
					id = id.id;
				}
				if(me.isReturnData) {
					me.fireEvent('save', me, me.returnData(id));
				} else {
					me.fireEvent('save', me, id);
				}
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				me.fireEvent('saveerror', me);
				JShell.Msg.error(data.msg);
			}
		});
	},
	//设置行数据条件结果值
	setRowFilterCondition: function() {
		var me = this;
		JShell.Action.delay(function() {
			//预置条件项代码
			var eName = me.getComponent('RBACRowFilter_RBACPreconditions_EName').getValue();
			//数据类型
			var valueType = me.getComponent('RBACRowFilter_StandCode').getValue();
			//关系
			var operationValue = me.getComponent('RBACPreconditions_SName').getValue();
			var rowFilterStr = "";
			switch(valueType) {
				case "date": //日期型
					rowFilterStr = me.getRowFilterStrOfDate(eName, operationValue);
					break;
				case "boolean": //布尔勾选
					//布尔勾选
					var booleanOne = me.getComponent('booleanOne').getValue();
					rowFilterStr = eName + operationValue + booleanOne;
					break;
				case "string": //字符串
					rowFilterStr = me.getRowFilterStrOfString(eName, operationValue);
					break;
				case "number": //数值型
					rowFilterStr = me.getRowFilterStrOfNumber(eName, operationValue);
					break;
				case "macros": //宏命令
					rowFilterStr = me.getRowFilterStrOfMacros(eName, operationValue);
					break;
				default:
					break;
			}
			//行数据条件
			//var rowFilterCondition = me.getComponent('RBACRowFilter_RowFilterCondition');
			//rowFilterCondition.setValue(rowFilterStr);
			me.getForm().setValues({
				'RBACRowFilter_RowFilterCondition': rowFilterStr
			});
		}, null, 800);
	},

	//字符串关系运算关系
	stringTypeList: [{
			'value': '=',
			'text': '等于'
		},
		{
			'value': '<>',
			'text': '不等于'
		},
		{
			'value': 'like A%',
			'text': '开始于(A*)'
		},
		{
			'value': 'like %A',
			'text': '结束于(*A)'
		},
		{
			'value': 'like A%B',
			'text': '字符之间(A*B) (输入2项)'
		},
		{
			'value': '= or = or =',
			'text': '等于其中一项(输入多项)'
		},
		{
			'value': 'not (= or = or =)',
			'text': '不等于任何一项(输入多项)'
		},
		{
			'value': 'like %A% or like %B%',
			'text': '包含(可输入多个)'
		},
		{
			'value': 'not (like %A% or like %B%)',
			'text': '不包含(可输入多个)'
		}
	],

	//日期时间关系运算关系
	dateTypeList: [{
			'value': '=',
			'text': '等于'
		},
		{
			'value': '<>',
			'text': '不等于'
		},
		{
			'value': '>',
			'text': '大于'
		},
		{
			'value': '>=',
			'text': '大于等于'
		},
		{
			'value': '<',
			'text': '小于'
		},
		{
			'value': '<=',
			'text': '小于等于'
		},
		{
			'value': '>= and <=',
			'text': '区间(包含边界)(输入2项)'
		},
		{
			'value': '<= or >=',
			'text': '区间外(包含边界)(输入2项)'
		},
		{
			'value': '> and <',
			'text': '区间(不包含边界)(输入2项)'
		},
		{
			'value': '< or >',
			'text': '区间外(不包含边界)(输入2项)'
		}
	],
	//值类型
	columnTypeList: [{
			'value': 'date',
			'text': '日期型'
		},
		{
			'value': 'boolean',
			'text': '布尔勾选'
		},
		{
			'value': 'number',
			'text': '数值型'
		},
		{
			'value': 'string',
			'text': '字符串'
		},
		{
			'value': 'macros',
			'text': '宏命令'
		}
	],
	//布尔勾选关系运算关系
	booleanTypeList: [{
		'value': '=',
		'text': '等于'
	}],
	//number关系运算关系
	numberTypeList: [{
			'value': '=',
			'text': '等于'
		},
		{
			'value': '<>',
			'text': '不等于'
		},
		{
			'value': '>',
			'text': '大于'
		},
		{
			'value': '>=',
			'text': '大于等于'
		},
		{
			'value': '<',
			'text': '小于'
		},
		{
			'value': '<=',
			'text': '小于等于'
		},
		{
			'value': '>= and <=',
			'text': '区间(包含边界)(输入2项)'
		},
		{
			'value': '<= or >=',
			'text': '区间外(包含边界)(输入2项)'
		},
		{
			'value': '> and <',
			'text': '区间(不包含边界)(输入2项)'
		},
		{
			'value': '< or >',
			'text': '区间外(不包含边界)(输入2项)'
		}
	],
	//宏的区间关系通过新增两行的关系建立
	macrosOperationType: [{
			'value': '=',
			'text': '等于'
		},
		{
			'value': '>',
			'text': '大于'
		},
		{
			'value': '<>',
			'text': '不等于'
		},
		{
			'value': '>=',
			'text': '大于等于'
		},
		{
			'value': '<',
			'text': '小于'
		},
		{
			'value': '<=',
			'text': '小于等于'
		},
		{
			'value': '>= and <=',
			'text': '区间(包含边界)(输入2项)'
		},
		{
			'value': '<= or >=',
			'text': '区间外(包含边界)(输入2项)'
		},
		{
			'value': '> and <',
			'text': '区间(不包含边界)(输入2项)'
		},
		{
			'value': '< or >',
			'text': '区间外(不包含边界)(输入2项)'
		}
	],
	getRowFilterStrOfString: function(eName, operationValue) {
		var me = this;
		var rowFilterStr = "";
		//字符型
		var oneValue = me.getComponent('stringOne').getValue();
		var twoValue = me.getComponent('stringTwo').getValue();
		if(oneValue) {
			oneValue = oneValue.replace(/\\/g, '');
			oneValue = oneValue.replace(/[\r\n]/g, '');
		}
		if(twoValue) {
			twoValue = twoValue.replace(/\\/g, '');
			twoValue = twoValue.replace(/[\r\n]/g, '');
		}
		var tempArr = [];
		if(oneValue) tempArr = oneValue.split(",");

		switch(operationValue) {
			case 'like A%': //开始于(A*)
				if(oneValue)
					rowFilterStr = eName + " like '" + oneValue + "%'";
				break;
			case "like %A": //结束于(*A)
				if(oneValue)
					rowFilterStr = eName + " like '%" + oneValue + "'";
				break;
			case "like A%B": //字符之间(A*B) (输入2项)
				if(oneValue && twoValue)
					rowFilterStr = eName + " like '" + oneValue + "%" + twoValue + "'";
				break;
			case "= or = or =": //等于其中一项(输入多项)	
				if(tempArr.length > 0) {
					for(var i = 0; i < tempArr.length; i++) {
						rowFilterStr += (eName + "='" + tempArr[i] + "'");
						if(i < tempArr.length - 1) rowFilterStr += " or ";
					}
				}
				break;
			case "not (= or = or =)": //不等于任何一项(输入多项)
				if(tempArr.length > 0) {
					for(var i = 0; i < tempArr.length; i++) {
						rowFilterStr += (eName + "='" + tempArr[i] + "'");
						if(i < tempArr.length - 1) rowFilterStr += " or ";
					}
					if(rowFilterStr) rowFilterStr = "not (" + rowFilterStr + ")";
				}
				break;
			case "like %A% or like %B%": //包含(可输入多个)
				if(tempArr.length > 0) {
					for(var i = 0; i < tempArr.length; i++) {
						rowFilterStr += (eName + " like '%" + tempArr[i] + "%'");
						if(i < tempArr.length - 1) rowFilterStr += " or ";
					}
					if(rowFilterStr) rowFilterStr = "(" + rowFilterStr + ")";
				}
				break;
			case "not (like %A% or like %B%)": //不包含(可输入多个)
				if(tempArr.length > 0) {
					for(var i = 0; i < tempArr.length; i++) {
						rowFilterStr += (eName + " like '%" + tempArr[i] + "%'");
						if(i < tempArr.length - 1) rowFilterStr += " or ";
					}
					if(rowFilterStr) rowFilterStr = " not (" + rowFilterStr + ")";
				}
				break;
			default: //等于,不等于
				if(oneValue)
					rowFilterStr = eName + operationValue + "'" + oneValue + "'";
				break;
		}
		return rowFilterStr;
	},
	getRowFilterStrOfDate: function(eName, operationValue) {
		var me = this;
		var rowFilterStr = "";
		//日期
		var oneValue = me.getComponent('datefieldOne').getValue();
		var twoValue = me.getComponent('datefieldTwo').getValue();
		if(!oneValue) return "";
		switch(operationValue) {
			case '>= and <=': //区间(包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " >='" + oneValue + "' and " + eName + "<='" + twoValue + "'";
				break;
			case "<= or >=": //区间外(包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " <='" + oneValue + "' or " + eName + ">='" + twoValue + "'";
				break;
			case "> and <": //区间(不包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " >'" + oneValue + "' and " + eName + "<'" + twoValue + "'";
				break;
			case "< or >": //区间外(不包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " <'" + oneValue + "' or " + eName + ">='" + twoValue + "'";
				break;
			default: //等于,不等于,
				rowFilterStr = eName + operationValue + "'" + oneValue + "'";
				break;
		}
		return rowFilterStr;
	},
	getRowFilterStrOfNumber: function(eName, operationValue) {
		var me = this;
		var rowFilterStr = "";
		//数值
		var oneValue = me.getComponent('numberfieldOne').getValue();
		var twoValue = me.getComponent('numberfieldTwo').getValue();
		if(!oneValue) return "";
		switch(operationValue) {
			case '>= and <=': //区间(包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " >='" + oneValue + "' and " + eName + "<='" + twoValue + "'";
				break;
			case "<= or >=": //区间外(包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " <='" + oneValue + "' or " + eName + ">='" + twoValue + "'";
				break;
			case "> and <": //区间(不包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " >'" + oneValue + "' and " + eName + "<'" + twoValue + "'";
				break;
			case "< or >": //区间外(不包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " <'" + oneValue + "' or " + eName + ">='" + twoValue + "'";
				break;
			default: //等于,不等于,
				rowFilterStr = eName + operationValue + "'" + oneValue + "'";
				break;
		}
		return rowFilterStr;
	},
	getRowFilterStrOfMacros: function(eName, operationValue) {
		var me = this;
		var rowFilterStr = "";
		//宏命令
		var oneValue = me.getComponent('cbomacrosOne').getValue();
		var twoValue = me.getComponent('cbomacrosTwo').getValue();
		if(!oneValue) return "";
		switch(operationValue) {
			case '>= and <=': //区间(包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " >='" + oneValue + "' and " + eName + "<='" + twoValue + "'";
				break;
			case "<= or >=": //区间外(包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " <='" + oneValue + "' or " + eName + ">='" + twoValue + "'";
				break;
			case "> and <": //区间(不包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " >'" + oneValue + "' and " + eName + "<'" + twoValue + "'";
				break;
			case "< or >": //区间外(不包含边界)(输入2项)
				if(twoValue)
					rowFilterStr = eName + " <'" + oneValue + "' or " + eName + ">='" + twoValue + "'";
				break;
			default: //等于,不等于
				rowFilterStr = eName + operationValue + "'" + oneValue + "'";
				break;
		}
		return rowFilterStr;
	},
	//获取宏命令数据集
	getMacroCommandData: function() {
		var me = this;
		if(me.localMacroData.length > 0) return me.localMacroData;

		var w = '';
		var myUrl = JShell.System.Path.ROOT + '/ConstructionService.svc/CS_UDTO_SearchBTDMacroCommandByHQL' + '?isPlanish=true&fields=' + 'BTDMacroCommand_Id,BTDMacroCommand_CName,BTDMacroCommand_EName,BTDMacroCommand_MacroCode,BTDMacroCommand_TypeCode,BTDMacroCommand_TypeName';
		me.localMacroData = [];
		Ext.Ajax.request({
			async: false,
			timeout: 6000,
			url: myUrl,
			method: 'GET',
			success: function(response, opts) {
				var data = JShell.JSON.decode(response.responseText);
				if(data.success) {
					var count = 0;
					var list = [];
					if(data.ResultDataValue && data.ResultDataValue != '') {
						var ResultDataValue = JShell.JSON.decode(data.ResultDataValue);
						count = ResultDataValue.count;
						list = ResultDataValue.list;
					} else {
						list = [];
						count = 0;
					}
					for(var i = 0; i < list.length; i++) {
						var macroCode = list[i]['BTDMacroCommand_MacroCode'];
						var cName = list[i]['BTDMacroCommand_CName'];
						var typeCode = list[i]['BTDMacroCommand_TypeCode'];
						if(typeCode != 'IntDynamic') {
							if(macroCode == "$AddDay:+0,P1$" || macroCode == "$AddMonth:+0,P1$") { //过滤为日期追加天数或月数

							} else {
								//过滤数字类型
								var obj = {
									text: cName,
									value: macroCode
								};
								me.localMacroData.push(obj);
							}
						}
					}
				} else {
					JcallShell.Msg.alert('获取信息失败！', null, 1000);
				}
			}
		});
		return me.localMacroData;
	}
});