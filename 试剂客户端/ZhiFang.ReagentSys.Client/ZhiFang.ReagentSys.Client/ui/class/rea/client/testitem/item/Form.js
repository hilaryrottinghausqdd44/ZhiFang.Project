/**
 * 检验项目信息维护
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.testitem.item.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '项目信息维护',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	width: 250,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '15px 20px 0px 0px',
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestItemById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaTestItem',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaTestItemByField',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 220,
		labelAlign: 'right'
	},

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	PK: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '项目名称',
			name: 'ReaTestItem_CName',
			emptyText: '必填项',
			allowBlank: false,
			itemId: 'ReaTestItem_CName',
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			fieldLabel: '英文名称',
			name: 'ReaTestItem_EName',
			itemId: 'ReaTestItem_EName',
			width: me.defaults.width * 1
		}, {
			fieldLabel: '简称',
			name: 'ReaTestItem_SName',
			itemId: 'ReaTestItem_SName',
			width: me.defaults.width * 1
		}, {
			fieldLabel: '代码',
			name: 'ReaTestItem_ShortCode',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaTestItem_Shortcode'
		}, {
			fieldLabel: 'Lis编码',
			name: 'ReaTestItem_LisCode',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaTestItem_LisCode'
		},  {
			fieldLabel: '价格',
			name: 'ReaTestItem_Price',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'numberfield',
			value: 0,
			minValue: 0,
			colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel: '显示次序',
			name: 'ReaTestItem_DispOrder',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'numberfield',
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '启用',
			name: 'ReaTestItem_Visible',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '专项1',
			name: 'ReaTestItem_ZX1',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaTestItem_ZX1'
		}, {
			fieldLabel: '专项2',
			name: 'ReaTestItem_ZX2',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaTestItem_ZX2'
		}, {
			fieldLabel: '专项3',
			name: 'ReaTestItem_ZX3',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaTestItem_ZX3'
		}, {
			height: 40,
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaTestItem_Memo',
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			fieldLabel: '主键',
			name: 'ReaTestItem_Id',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaTestItem_Id',
			hidden: true
		});
		return items;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName: values.ReaTestItem_CName,
			EName: values.ReaTestItem_EName,
			SName: values.ReaTestItem_SName,
			Price: values.ReaTestItem_Price,
			ShortCode: values.ReaTestItem_ShortCode,
			DispOrder: values.ReaTestItem_DispOrder,
			Visible: values.ReaTestItem_Visible == 1 ? 1 : 0,
			LisCode: values.ReaTestItem_LisCode,
			ZX1: values.ReaTestItem_ZX1,
			ZX2: values.ReaTestItem_ZX2,
			ZX3: values.ReaTestItem_ZX3,
			Memo: values.ReaTestItem_Memo
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

		var fields = [
			'Id', 'CName', 'EName', 'Price',
			'ShortCode', 'DispOrder',
			'Visible', 'LisCode', 'ZX1',
			'ZX2', 'ZX3', 'Memo', 'SName'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaTestItem_Id;
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.ReaTestItem_Visible = data.ReaTestItem_Visible == '1' ? true : false;
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var CName = me.getComponent('ReaTestItem_CName');
		CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				newValue = me.changeCharCode(newValue);
				if(newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								ReaTestItem_ShortCode: value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						ReaTestItem_ShortCode: ''
					});
				}
			}
		});

	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		items.push(me.createbtnToolbar());
		return items;
	},
	/**创建功能按钮栏*/
	createbtnToolbar: function() {
		var me = this,
			items = [{
				xtype: 'label',
				text: '项目信息',
				margin: '0 0 0 10',
				style: "font-weight:bold;color:blue;"
			}];
		if(items.length == 0) return null;
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			height: 26,
			itemId: 'btnToolbar',
			items: items
		});
	},
	/**中文符号转换为英文符号*/
	changeCharCode: function(val) {
		var me = this;
		/*正则转换中文标点*/
		val = val.replace(/：/g, ':');
		val = val.replace(/。/g, '.');
		val = val.replace(/“/g, '"');
		val = val.replace(/”/g, '"');
		val = val.replace(/【/g, '[');
		val = val.replace(/】/g, ']');
		val = val.replace(/《/g, '<');
		val = val.replace(/》/g, '>');
		val = val.replace(/，/g, ',');
		val = val.replace(/？/g, '?');
		val = val.replace(/、/g, ',');
		val = val.replace(/；/g, ';');
		val = val.replace(/（/g, '(');
		val = val.replace(/）/g, ')');
		val = val.replace(/‘/g, "'");
		val = val.replace(/’/g, "'");
		val = val.replace(/『/g, "[");
		val = val.replace(/』/g, "]");
		val = val.replace(/「/g, "[");
		val = val.replace(/」/g, "]");
		val = val.replace(/﹃/g, "[");
		val = val.replace(/﹄/g, "]");
		val = val.replace(/〔/g, "{");
		val = val.replace(/〕/g, "}");
		val = val.replace(/—/g, "-");
		val = val.replace(/·/g, ".");
		/*正则转换全角为半角*/
		//字符串先转化成数组  
		val = val.split("");
		for(var i = 0; i < val.length; i++) {
			//全角空格处理  
			if(val[i].charCodeAt(0) === 12288) {
				val[i] = String.fromCharCode(32);
			}
			/*其他全角*/
			if(val[i].charCodeAt(0) > 0xFF00 && val[i].charCodeAt(0) < 0xFFEF) {
				val[i] = String.fromCharCode(val[i].charCodeAt(0) - 65248);
			}
		}
		//数组转换成字符串  
		val = val.join("");
		return val;
	}
});