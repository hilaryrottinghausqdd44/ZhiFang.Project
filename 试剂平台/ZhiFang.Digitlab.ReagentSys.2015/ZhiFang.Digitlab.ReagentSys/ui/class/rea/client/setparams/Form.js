/**
 * 扫码参数设置
 * @author longfc
 * @version 2018-01-17
 */
Ext.define('Shell.class.rea.client.setparams.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '扫码参数设置',

	width: 560,
	height: 480,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenBarCodeFormatById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/',
	/**修改服务*/
	editUrl: '/ReaSysManageService.svc/',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 15px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 1 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 165,
		width: 325,
		columns: 1,
		vertical: true,
		allowBlank: false,
		colspan: 1,
		labelAlign: 'right',
		border: 1,
		style: {
			borderStyle: 'solid'
		}
	},
	formtype: 'add',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.width = document.body.clientWidth * 0.62;
		me.defaults.width = me.width / me.layout.columns - 10;
		//if(me.defaults.width < 295) me.defaults.width = 295;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			xtype: 'radiogroup',
			fieldLabel: '手工验收扫码模式',
			items: [{
					boxLabel: '严格扫码',
					name: 'ConfirmManualInput',
					inputValue: false
				},
				{
					boxLabel: '混合扫码',
					name: 'ConfirmManualInput',
					inputValue: true,
					checked: true
				}
			]
		});

		items.push({
			xtype: 'radiogroup',
			fieldLabel: '订单验收扫码模式',
			items: [{
					boxLabel: '严格扫码',
					name: 'ConfirmReaOrder',
					inputValue: false
				},
				{
					boxLabel: '混合扫码',
					name: 'ConfirmReaOrder',
					inputValue: true,
					checked: true
				}
			]
		});

		items.push({
			xtype: 'radiogroup',
			fieldLabel: '供货验收扫码模式',
			items: [{
					boxLabel: '严格扫码',
					name: 'ConfirmReaSale',
					inputValue: false
				},
				{
					boxLabel: '混合扫码',
					name: 'ConfirmReaSale',
					inputValue: true,
					checked: true
				}
			]
		});
		items.push({
			xtype: 'radiogroup',
			fieldLabel: '验收入库扫码模式',
			items: [{
					boxLabel: '严格扫码',
					name: 'ConfirmStorage',
					inputValue: false
				},
				{
					boxLabel: '混合扫码',
					name: 'ConfirmStorage',
					inputValue: true,
					checked: true
				}
			]
		});
		items.push({
			xtype: 'radiogroup',
			fieldLabel: '出库扫码模式',
			items: [{
					boxLabel: '严格扫码',
					name: 'StorageOut',
					inputValue: false
				},
				{
					boxLabel: '混合扫码',
					name: 'StorageOut',
					inputValue: true,
					checked: true
				}
			]
		});
		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		if(!PlatformOrgNo) PlatformOrgNo = me.PlatformOrgNo;
		var entity = {
			"Id": -1,
			"CName": values.ReaCenBarCodeFormat_CName,
			"Memo": memo
		};
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = ['Id'];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaCenBarCodeFormat_Id;
		return entity;
	}
});