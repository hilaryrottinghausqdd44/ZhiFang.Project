/**
 * 业务接口配置
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.businessinterface.Form', {
	extend: 'Shell.ux.form.Panel',	
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	
	title: '业务接口配置',
	width: 250,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '15px 20px 0px 0px',
	formtype: "edit",
	autoScroll: false,
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterfaceById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBusinessInterface',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBusinessInterfaceByField',
	
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 1 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 115,
		width: 255,
		labelAlign: 'right'
	},

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	PK: null,
	/**接口类型Key*/
	InterfaceType: "ReaBusinessInterfaceType",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.InterfaceType, false, false, null);
		me.defaults.width=me.width-10;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '接口名称',
			name: 'ReaBusinessInterface_CName',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width *1
		},{
			fieldLabel: '调用URL入口',
			name: 'ReaBusinessInterface_URL',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '接口类型',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBusinessInterface_InterfaceType',
			itemId: 'ReaBusinessInterface_InterfaceType',
			hasStyle: true,
			allowBlank: false,
			colspan: 1,
			data: JShell.REA.StatusList.Status[me.InterfaceType].List,
			width: me.defaults.width * 1
		},  {
			fieldLabel: '启用',
			name: 'ReaBusinessInterface_Visible',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '显示次序',
			name: 'ReaBusinessInterface_DispOrder',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'numberfield',
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel: 'AppKey',
			name: 'ReaBusinessInterface_AppKey',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: 'AppPassword',
			name: 'ReaBusinessInterface_AppPassword',
			colspan: 1,
			width: me.defaults.width * 1
		},  {
			fieldLabel: '实验室平台机构码',
			name: 'ReaBusinessInterface_ReaServerLabcCode',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaBusinessInterface_ReaServerLabcCode',
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '专项1',
			name: 'ReaBusinessInterface_ZX1',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '专项2',
			name: 'ReaBusinessInterface_ZX2',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '专项3',
			name: 'ReaBusinessInterface_ZX3',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '主键ID',
			name: 'ReaBusinessInterface_Id',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		return items;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			InterfaceType: values.ReaBusinessInterface_InterfaceType,
			CName: values.ReaBusinessInterface_CName,
			URL: values.ReaBusinessInterface_URL,
			AppKey: values.ReaBusinessInterface_AppKey,
			AppPassword: values.ReaBusinessInterface_AppPassword,
			ReaServerLabcCode: values.ReaBusinessInterface_ReaServerLabcCode,
			ZX1: values.ReaBusinessInterface_ZX1,
			ZX2: values.ReaBusinessInterface_ZX2,
			ZX3: values.ReaBusinessInterface_ZX3,
			Visible: values.ReaBusinessInterface_Visible ? 1 : 0
		};
		if(values.ReaBusinessInterface_DispOrder) {
			entity.DispOrder = values.ReaBusinessInterface_DispOrder;
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = [
			'InterfaceType', 'Id', 'CName', 'URL',
			'AppKey', 'AppPassword', 'Visible',
			'ReaServerLabcCode', 'ZX1', 'ZX2', 'ZX3',
			'DispOrder'
		];
		entity.fields = fields.join(',');
		if(values.ReaBusinessInterface_Id != '') {
			entity.entity.Id = values.ReaBusinessInterface_Id;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.ReaBusinessInterface_Visible = data.ReaBusinessInterface_Visible == '1' ? true : false;
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;

	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	/**@overwrite 重置按钮点击处理方法
	 * */
	onResetClick: function() {
		var me = this;
		me.callParent(arguments);
		var UseCode = me.getComponent('ReaBusinessInterface_ReaServerLabcCode');
		var labOrgNo = JShell.REA.System.CENORG_CODE;
		UseCode.setValue(labOrgNo);
	}
});