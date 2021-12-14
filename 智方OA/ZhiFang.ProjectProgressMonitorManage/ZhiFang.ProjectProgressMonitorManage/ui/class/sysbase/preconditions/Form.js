/***
 * 预置条件项
 * @author longfc
 * @version 2017-06-14
 */
Ext.define('Shell.class.sysbase.preconditions.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '预置条件',
	width: 320,
	bodyPadding: 5,

	/**新增服务地址*/
	addUrl: '/RBACService.svc/RBAC_UDTO_AddRBACPreconditions',
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACPreconditionsById?isPlanish=true',
	editUrl: '/RBACService.svc/RBAC_UDTO_UpdateRBACPreconditionsByField',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 100,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	/**模块服务ID*/
	moduleOpeId: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '所属模块服务',
			name: 'RBACPreconditions_RBACModuleOper_CName',
			itemId: 'RBACPreconditions_RBACModuleOper_CName',
			className: 'Shell.class.sysbase.moduleoper.CheckGrid',
			xtype: 'uxCheckTrigger',
			emptyText: '必填项',
			allowBlank: false,
			classConfig: {
				defaultWhere: 'rbacmoduleoper.Id=' + me.moduleOpeId
			}
		}, {
			xtype: 'uxCheckTrigger',
			//className: 'Shell.class.sysbase.preconditions.entitycode.CheckGrid',
			className: 'Shell.class.sysbase.moduleoper.entity.CheckGrid',
			emptyText: '必填项',
			allowBlank: false,
			fieldLabel: '所属实体对象',
			name: 'RBACPreconditions_EntityCName',
			itemId: 'RBACPreconditions_EntityCName'
		}, {
			fieldLabel: '实体编码',
			hidden: true,
			name: 'RBACPreconditions_EntityCode',
			itemId: 'RBACPreconditions_EntityCode'
		}, {
			fieldLabel: '预置条件名称',
			name: 'RBACPreconditions_CName',
			itemId: 'RBACPreconditions_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '预置条件代码',
			name: 'RBACPreconditions_EName',
			itemId: 'RBACPreconditions_EName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.preconditions.entitycode.CheckGrid',
			emptyText: '必填项',
			allowBlank: false
		}, {
			xtype: 'combobox',
			name: 'RBACPreconditions_ValueType',
			itemId: 'RBACPreconditions_ValueType',
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
			store: new Ext.data.Store({
				fields: ['value', 'text'],
				autoLoad: true,
				data: me.columnTypeList
			})
		}, {
			fieldLabel: '显示次序',
			name: 'RBACPreconditions_DispOrder',
			xtype: 'numberfield',
			value: 0
		}, {
			boxLabel: '是否使用',
			name: 'RBACPreconditions_IsUse',
			itemId: "RBACPreconditions_IsUse",
			xtype: 'checkbox',
			inputValue: true,
			checked: true,
			hidden: true,
			style: {
				marginLeft: '95px'
			}
		}, {
			fieldLabel: '主键ID',
			name: 'RBACPreconditions_Id',
			hidden: true
		}, {
			fieldLabel: '所属模块服务主键ID',
			name: 'RBACPreconditions_RBACModuleOper_Id',
			itemId: 'RBACPreconditions_RBACModuleOper_Id',
			hidden: true
		});
		return items;
	},
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
	/**初始化监听*/
	initListeners: function() {
		var me = this;
		var Id = me.getComponent('RBACPreconditions_RBACModuleOper_Id');
		var CName = me.getComponent('RBACPreconditions_RBACModuleOper_CName');

		var EntityCode = me.getComponent('RBACPreconditions_EntityCode');
		var EntityCName = me.getComponent('RBACPreconditions_EntityCName');

		var RBACPreconditionsEName = me.getComponent('RBACPreconditions_EName');
		var RBACPreconditionsCName = me.getComponent('RBACPreconditions_CName');
		if(CName) {
			CName.on({
				check: function(p, record) {
					Id.setValue(record ? record.get('RBACModuleOper_Id') : '');
					CName.setValue(record ? record.get('RBACModuleOper_CName') : '');
//					if(!RBACPreconditionsCName.getValue()) {
//						RBACPreconditionsCName.setValue(record ? record.get('RBACModuleOper_CName') : '');
//					}
					var ClassName=record ? record.get('RBACModuleOper_RowFilterBase') : '';
					EntityCode.setValue(ClassName);
					EntityCName.setValue(record ? record.get('RBACModuleOper_RowFilterBaseCName') : '');
					
					RBACPreconditionsEName.changeClassConfig({
						EntityName:ClassName
					});
					if(typeof(RBACPreconditionsEName.getPicker)=="function")RBACPreconditionsEName.getPicker().EntityName=ClassName;
					
					p.close();
				}
			});
		}
		//实体对象
		if(EntityCName) {
			EntityCName.on({
				check: function(p, record) {
					EntityCode.setValue(record ? record.get('ClassName') : '');
					EntityCName.setValue(record ? record.get('CName') : '');
					
					RBACPreconditionsEName.changeClassConfig({
						EntityName: record ? record.get('ClassName') : ''
					});
					if(typeof(RBACPreconditionsEName.getPicker)=="function")RBACPreconditionsEName.getPicker().EntityName=record ? record.get('ClassName') : '';
					p.close();
				}
			});
		}
		//实体对象属性选择
		if(RBACPreconditionsEName) {
			RBACPreconditionsEName.on({
				check: function(p, record) {
					var interactionField = record ? record.get('InteractionField') : '';
					RBACPreconditionsEName.setValue(interactionField);
					var ValueType = me.getComponent('RBACPreconditions_ValueType');
					ValueType.setValue(record ? me.getChangeValueType(record.get('ValueType'), interactionField) : '');
					if(!RBACPreconditionsCName.getValue()) {
						RBACPreconditionsCName.setValue(record ? record.get('CName') : '');
					}
					p.close();
				}
			});
		}
	},
	getChangeValueType: function(valueType, interactionField) {
		var me = this;
		if(valueType) valueType = valueType.toLowerCase();
		switch(valueType) {
			case "nullable`1":
			case "int64":
				if(interactionField.indexOf("Time") > -1)
					valueType = "date";
				else
					valueType = "macros";
			case "int32":
				valueType = "number";
				break;
			default:
				break;
		}
		return valueType;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName: values.RBACPreconditions_CName,
			EName: values.RBACPreconditions_EName,
			ValueType: values.RBACPreconditions_ValueType,
			EntityCName: values.RBACPreconditions_EntityCName,
			EntityCode: values.RBACPreconditions_EntityCode,
			DispOrder: values.RBACPreconditions_DispOrder,
			IsUse: values.RBACPreconditions_IsUse ? true : false
		};
		if(values.ExecHQL) {
			entity.ExecHQL = entity.ExecHQL.replace(/\\/g, '');
			entity.ExecHQL = entity.ExecHQL.replace(/[\r\n]/g, '');
		}
		if(values.RBACPreconditions_RBACModuleOper_Id) {
			entity.RBACModuleOper = {
				Id: values.RBACPreconditions_RBACModuleOper_Id
			};
			if(me.formtype == "add") {
				var strDataTimeStamp = "1,2,3,4,5,6,7,8";
				entity.RBACModuleOper.DataTimeStamp = strDataTimeStamp.split(',');
			}
		}
		if(!entity.ModuleID) {
			entity.ModuleID = me.moduleId;
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
		fieldsArr.push('RBACModuleOper_Id');
		entity.fields = fieldsArr.join(',');
		entity.entity.Id = values.RBACPreconditions_Id;
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var RBACPreconditionsEName = me.getComponent('RBACPreconditions_EName');
		RBACPreconditionsEName.changeClassConfig({
			EntityName: data ? data.RBACPreconditions_EntityCode : ''
		});
		return data;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent("RBACPreconditions_IsUse").setVisible(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent("RBACPreconditions_IsUse").setVisible(true);
	},
	changeClassConfig: function(moduleOpeId) {
		var me = this;
		if(moduleOpeId) me.moduleOpeId = moduleOpeId;
		var data = {
			defaultWhere: 'rbacmoduleoper.Id=' + moduleOpeId
		};
		var CName = me.getComponent("RBACPreconditions_RBACModuleOper_CName");
		CName.setClassConfig(data);
	}
});