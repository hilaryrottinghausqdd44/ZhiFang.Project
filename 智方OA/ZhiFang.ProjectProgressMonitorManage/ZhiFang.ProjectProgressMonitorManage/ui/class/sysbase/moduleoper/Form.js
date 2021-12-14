/***
 * 模块服务管理
 * @author longfc
 * @version 2017-05-17
 */
Ext.define('Shell.class.sysbase.moduleoper.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '模块服务信息',
	width: 320,
	bodyPadding: 5,

	/**新增服务地址*/
	addUrl: '/RBACService.svc/RBAC_UDTO_AddRBACModuleOper',
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperById?isPlanish=true',
	editUrl: '/RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField',

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
	/**模块ID*/
	moduleId: null,
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
			fieldLabel: '模块服务名称',
			name: 'RBACModuleOper_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '模块服务URL',
			name: 'RBACModuleOper_ServiceURLEName',
			emptyText: '必填项',
			allowBlank: false,
			height: 60,
			xtype: 'textarea'
		}, {
			fieldLabel: '模块操作代码',
			name: 'RBACModuleOper_UseCode'
		}, {
			fieldLabel: '数据对象',
			name: 'RBACModuleOper_RowFilterBaseCName',
			itemId: 'RBACModuleOper_RowFilterBaseCName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.moduleoper.entity.CheckGrid',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '显示次序',
			name: 'RBACModuleOper_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '数据对象代码',
			name: 'RBACModuleOper_RowFilterBase',
			itemId: 'RBACModuleOper_RowFilterBase',
			hidden: true
		}, {
			fieldLabel: "描述",
			name: 'RBACModuleOper_Comment',
			minHeight: 100,
			height: 160,
			maxLength: 500,
			maxLengthText: "摘要最多只能输入500字",
			style: {
				marginBottom: '2px'
			},
			xtype: 'textarea'
		}, {
			boxLabel: '采用行过滤条件',
			name: 'RBACModuleOper_UseRowFilter',
			xtype: 'checkbox',
			inputValue: true,
			checked: true,
			style: {
				marginLeft: '95px'
			}
		}, {
			boxLabel: '是否使用',
			name: 'RBACModuleOper_IsUse',
			itemId: 'RBACModuleOper_IsUse',
			xtype: 'checkbox',
			inputValue: true,
			checked: true,
			hidden: true,
			style: {
				marginLeft: '95px'
			}
		}, {
			fieldLabel: '主键ID',
			name: 'RBACModuleOper_Id',
			hidden: true
		}, {
			fieldLabel: '主键ID',
			name: 'RBACModuleOper_RBACModule_Id',
			value: me.moduleId,
			hidden: true
		});

		return items;
	},
	/**初始化监听*/
	initListeners: function() {
		var me = this;
		var RowFilterBase = me.getComponent('RBACModuleOper_RowFilterBase');
		var RowFilterBaseCName = me.getComponent('RBACModuleOper_RowFilterBaseCName');

		//数据对象
		if(RowFilterBaseCName) {
			RowFilterBaseCName.on({
				check: function(p, record) {
					RowFilterBase.setValue(record ? record.get('ClassName') : '');
					RowFilterBaseCName.setValue(record ? record.get('CName') : '');
					p.close();
				}
			});
		}
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName: values.RBACModuleOper_CName,
			RowFilterBase: values.RBACModuleOper_RowFilterBase,
			RowFilterBaseCName: values.RBACModuleOper_RowFilterBaseCName,
			UseCode: values.RBACModuleOper_UseCode,
			DispOrder: values.RBACModuleOper_DispOrder,
			UseRowFilter: values.RBACModuleOper_UseRowFilter ? true : false,
			IsUse: values.RBACModuleOper_IsUse ? true : false
		};
		if(values.RBACModuleOper_ServiceURLEName) {
			entity.ServiceURLEName = values.RBACModuleOper_ServiceURLEName.replace(/\\/g, '');
			entity.ServiceURLEName = entity.ServiceURLEName.replace(/[\r\n]/g, '');
		}
		if(values.RBACModuleOper_Comment) {
			entity.Comment = values.RBACModuleOper_Comment.replace(/\\/g, '&#92');
			entity.Comment = entity.Comment.replace(/[\r\n]/g, '<br />');
		} else {
			entity.Comment = "";
		}
		if(values.RBACModule_Id) {
			me.moduleId = values.RBACModule_Id;
		}
		entity.RBACModule = {
			Id: me.moduleId
		};
		if(me.formtype == "add") {
			var strDataTimeStamp = "1,2,3,4,5,6,7,8";
			entity.RBACModule.DataTimeStamp = strDataTimeStamp.split(',');
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
		entity.fields = fieldsArr.join(',');

		entity.entity.Id = values.RBACModuleOper_Id;
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var reg = new RegExp("<br />", "g");
		data.RBACModuleOper_Comment = data.RBACModuleOper_Comment.replace(reg, "\r\n");
		return data;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent("RBACModuleOper_IsUse").setVisible(false);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent("RBACModuleOper_IsUse").setVisible(true);
	}
});