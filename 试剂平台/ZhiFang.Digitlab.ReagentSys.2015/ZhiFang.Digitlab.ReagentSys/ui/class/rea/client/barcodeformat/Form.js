/**
 * 供货方条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '条码规则信息',

	width: 420,
	height: 320,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenBarCodeFormatById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaCenBarCodeFormat',
	/**修改服务*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaCenBarCodeFormatByField',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 185,
		labelAlign: 'right'
	},
	/**当前的供货方平台机构编码*/
	PlatformOrgNo: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaults.width = me.width / me.layout.columns - 10;
		if(me.defaults.width < 185) me.defaults.width = 185;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '规则名称',
			name: 'ReaCenBarCodeFormat_CName',
			itemId: 'ReaCenBarCodeFormat_CName',
			allowBlank: false,
			colspan: 2,
			width: me.defaults.width * 2
		});

		items.push({
			fieldLabel: '规则前缀',
			name: 'ReaCenBarCodeFormat_SName',
			itemId: 'ReaCenBarCodeFormat_SName',
			allowBlank: false,
			colspan: 2,
			width: me.defaults.width * 2
		});
		items.push({
			fieldLabel: '分割符',
			name: 'ReaCenBarCodeFormat_ShortCode',
			itemId: 'ReaCenBarCodeFormat_ShortCode',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1
		});

		items.push({
			fieldLabel: '分隔符数',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_SplitCount',
			itemId: 'ReaCenBarCodeFormat_SplitCount',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//启用
		items.push({
			fieldLabel: '启用',
			name: 'ReaCenBarCodeFormat_IsUse',
			itemId: 'ReaCenBarCodeFormat_IsUse',
			xtype: 'uxBoolComboBox',
			//value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '显示次序',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_DispOrder',
			itemId: 'ReaCenBarCodeFormat_DispOrder',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//样例
		items.push({
			xtype: 'textarea',
			fieldLabel: '样例',
			name: 'ReaCenBarCodeFormat_BarCodeFormatExample',
			itemId: 'ReaCenBarCodeFormat_BarCodeFormatExample',
			allowBlank: false,
			colspan: 2,
			width: me.defaults.width * 2,
			height: 50
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaCenBarCodeFormat_Memo',
			itemId: 'ReaCenBarCodeFormat_Memo',
			colspan: 2,
			width: me.defaults.width * 2,
			height: 50
		});

		items.push({
			fieldLabel: '主键ID',
			name: 'ReaCenBarCodeFormat_Id',
			hidden: true,
			type: 'key'
		}, {
			fieldLabel: '供货方平台编码',
			hidden: true,
			name: 'BmsCenSaleDocConfirm_PlatformOrgNo',
			itemId: 'BmsCenSaleDocConfirm_PlatformOrgNo'
		});
		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BmsCenSaleDocConfirm_PlatformOrgNo').setValue(me.PlatformOrgNo);
		me.getComponent('ReaCenBarCodeFormat_IsUse').setValue(true);
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.ReaCenBarCodeFormat_IsUse = data.ReaCenBarCodeFormat_IsUse == '1' ? true : false;
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var barCodeFormatExample = values.ReaCenBarCodeFormat_BarCodeFormatExample.replace(/\\/g, '&#92');
		barCodeFormatExample = barCodeFormatExample.replace(/[\r\n]/g, '');

		var memo = values.ReaCenBarCodeFormat_Memo.replace(/\\/g, '&#92');
		memo = memo.replace(/[\r\n]/g, '');
		var isUse = values.ReaCenBarCodeFormat_IsUse;
		if(isUse == "1"||isUse == 1 || isUse == true) isUse = 1;
		else isUse = 0;
		var PlatformOrgNo=values.BmsCenSaleDocConfirm_PlatformOrgNo;
		if(!PlatformOrgNo)PlatformOrgNo=me.PlatformOrgNo;
		var entity = {
			"Id": -1,
			"CName": values.ReaCenBarCodeFormat_CName,
			"SName": values.ReaCenBarCodeFormat_SName,
			"ShortCode": values.ReaCenBarCodeFormat_ShortCode,

			"SplitCount": values.ReaCenBarCodeFormat_SplitCount,
			"DispOrder": values.ReaCenBarCodeFormat_DispOrder,
			"PlatformOrgNo": PlatformOrgNo,
			"IsUse": isUse,
			"BarCodeFormatExample": barCodeFormatExample,
			"Memo": memo
		};
		if(values.ReaCenBarCodeFormat_Id) entity.Id = values.ReaCenBarCodeFormat_Id;
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
			'Id', 'CName', 'SName', 'ShortCode', 'IsUse', 'BarCodeFormatExample','PlatformOrgNo', 'Memo'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaCenBarCodeFormat_Id;
		return entity;
	}
});