/**
 * 业务接口关系配置信息
 * @author longfc	
 * @version 2018-11-19
 */
Ext.define('Shell.class.rea.client.businessinterfacelink.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '业务接口关系配置信息',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	width: 620,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '15px 20px 0px 0px',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterfaceLinkById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBusinessInterfaceLink',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBusinessInterfaceLinkByField',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 115,
		width: 325,
		labelAlign: 'right'
	},

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	PK: null,
	/**业务类型Key*/
	BusinessType: "ReaBusinessType",
	//业务接口ID
	BusinessID: null,
	//业务接口名称
	BusinessCName: null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.BusinessType, false, false, null);
		//me.defaults.width = me.width/me.layout.columns - 10;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '所属接口',
			name: 'ReaBusinessInterfaceLink_BusinessCName',
			itemId: 'ReaBusinessInterfaceLink_BusinessCName',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '业务接口ID',
			name: 'ReaBusinessInterfaceLink_BusinessId',
			itemId: 'ReaBusinessInterfaceLink_BusinessId',
			readOnly: true,
			locked: true,
			hidden: true
		}, {
			fieldLabel: '业务类型',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBusinessInterfaceLink_BusinessType',
			itemId: 'ReaBusinessInterfaceLink_BusinessType',
			hasStyle: true,
			allowBlank: false,
			colspan: 1,
			data: JShell.REA.StatusList.Status[me.BusinessType].List,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '供应商',
			name: 'ReaBusinessInterfaceLink_CompanyName',
			itemId: 'ReaBusinessInterfaceLink_CompanyName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.reacenorg.basic.CheckGrid',
			classConfig: {
				title: '供应商选择',
				checkOne: true,
				defaultWhere: 'reacenorg.OrgType=0',
				width: 300
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(record);
					p.close();
				}
			},
			colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel: '供应商ID',
			name: 'ReaBusinessInterfaceLink_CompID',
			itemId: 'ReaBusinessInterfaceLink_CompID',
			readOnly: true,
			locked: true,
			hidden: true
		},  {
			fieldLabel: '供应商机构码',
			name: 'ReaBusinessInterfaceLink_ReaCompCode',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaBusinessInterfaceLink_ReaCompCode',
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '供应商平台机构码',
			name: 'ReaBusinessInterfaceLink_ReaServerCompCode',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaBusinessInterfaceLink_ReaServerCompCode',
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '启用',
			name: 'ReaBusinessInterfaceLink_Visible',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '显示次序',
			name: 'ReaBusinessInterfaceLink_DispOrder',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'numberfield',
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '专项1',
			name: 'ReaBusinessInterfaceLink_ZX1',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '专项2',
			name: 'ReaBusinessInterfaceLink_ZX2',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '专项3',
			name: 'ReaBusinessInterfaceLink_ZX3',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '主键ID',
			name: 'ReaBusinessInterfaceLink_Id',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		return items;
	},
	/**供应商选择*/
	onCompAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('ReaBusinessInterfaceLink_CompID');
		var ComName = me.getComponent('ReaBusinessInterfaceLink_CompanyName');
		ComId.setValue(record ? record.get('ReaCenOrg_Id') : '');
		ComName.setValue(record ? record.get('ReaCenOrg_CName') : '');
		
		var ReaCompCode = me.getComponent('ReaBusinessInterfaceLink_ReaCompCode');
		var ReaServerCompCode = me.getComponent('ReaBusinessInterfaceLink_ReaServerCompCode');
		ReaCompCode.setValue(record ? record.get('ReaCenOrg_OrgNo') : '');
		ReaServerCompCode.setValue(record ? record.get('ReaCenOrg_PlatformOrgNo') : '');
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			BusinessType: values.ReaBusinessInterfaceLink_BusinessType,
			BusinessCName: values.ReaBusinessInterfaceLink_BusinessCName,
			CompanyName: values.ReaBusinessInterfaceLink_CompanyName,
			ReaServerCompCode: values.ReaBusinessInterfaceLink_ReaServerCompCode,
			ReaCompCode: values.ReaBusinessInterfaceLink_ReaCompCode,
			ZX1: values.ReaBusinessInterfaceLink_ZX1,
			ZX2: values.ReaBusinessInterfaceLink_ZX2,
			ZX3: values.ReaBusinessInterfaceLink_ZX3,
			Visible: values.ReaBusinessInterfaceLink_Visible ? 1 : 0
		};
		if(values.ReaBusinessInterfaceLink_DispOrder) {
			entity.DispOrder = values.ReaBusinessInterfaceLink_DispOrder;
		}
		if(values.ReaBusinessInterfaceLink_CompID) {
			entity.CompID = values.ReaBusinessInterfaceLink_CompID;
		}
		if(values.ReaBusinessInterfaceLink_BusinessId) {
			entity.BusinessId = values.ReaBusinessInterfaceLink_BusinessId;
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
			'BusinessType', 'Id', 'BusinessId', 'BusinessCName',
			'ReaServerCompCode', 'CompID','CompanyName', 'Visible',
			'ReaCompCode', 'ZX1', 'ZX2', 'ZX3',
			'DispOrder'
		];
		entity.fields = fields.join(',');
		if(values.ReaBusinessInterfaceLink_Id != '') {
			entity.entity.Id = values.ReaBusinessInterfaceLink_Id;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.ReaBusinessInterfaceLink_Visible = data.ReaBusinessInterfaceLink_Visible == '1' ? true : false;
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
	/**新增业务接口*/
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		if(me.BusinessID){
			var objValue={
				"ReaBusinessInterfaceLink_BusinessId":me.BusinessID,
				"ReaBusinessInterfaceLink_BusinessCName":me.BusinessCName
			};
			me.getForm().setValues(objValue);
		}
	}
});