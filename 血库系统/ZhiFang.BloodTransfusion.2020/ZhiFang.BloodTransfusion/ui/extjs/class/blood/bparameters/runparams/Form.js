/**
 * 系统运行参数
 * @author longfc
 * @version 2018-03-08
 */
Ext.define("Shell.class.blood.bparameters.runparams.Form", {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: "系统参数",
	width: 380,

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,

	addUrl: "/ServerWCF/SingleTableService.svc/ST_UDTO_AddBParameter",
	editUrl: "/ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBParameterByField",
	selectUrl: "/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterById?isPlanish=true",

	bodyPadding: "10px",
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},

	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 160,
		labelAlign: 'right'
	},

	initComponent: function() {
		var me = this;
		var width = me.width / 2 - 15;
		me.defaults.width = (width < 160) ? 160 : width;
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			xtype: "textfield",
			itemId: "BParameter_Id",
			hidden: true,
			name: "BParameter_Id",
			fieldLabel: "主键ID"
		}, {
			fieldLabel: '辅助录入信息',
			name: 'BParameter_ItemEditInfo',
			xtype: 'textfield',
			hidden: true
		});
		//辅助录入信息
		items.push({
			fieldLabel: '<b style="color:blue;">显示类型</b>',
			name: 'BParameter_ItemXType',
			itemId: 'BParameter_ItemXType',
			xtype: 'uxSimpleComboBox',
			hasStyle: false,
			IsnotField: true,
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns,
			data: [
				['textfield', '文本录入框'],
				['numberfield', '数字录入框'],
				['radiogroup', '单选组框'],
				['uxSimpleComboBox', '下拉选择框']
			],
			listeners: {
				change: function(p, newValue, oldValue) {
					me.setItemDataSetVisible();
				}
			}
		}, {
			xtype: "textarea",
			itemId: "BParameter_ParaValue",
			name: "BParameter_ParaValue",
			minHeight: 60,
			height: 180,
			style: {
				marginBottom: '4px'
			},
			allowBlank: false,
			emptyText: '必填项 设置路径时建议设置物理路径:如E:\\\UploadFiles',
			fieldLabel: "参数值",
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns
		}, {
			fieldLabel: '<b style="color:blue;">默认值</b>',
			name: 'BParameter_ItemDefaultValue',
			itemId: 'BParameter_ItemDefaultValue',
			IsnotField: true,
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns
		}, {
			fieldLabel: '<b style="color:blue;">单位名称</b>',
			name: 'BParameter_ItemUnit',
			itemId: 'BParameter_ItemUnit',
			IsnotField: true,
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns
		}, {
			fieldLabel: '<b style="color:blue;">数据源</b>',
			height: 120,
			name: 'BParameter_ItemDataSet',
			itemId: 'BParameter_ItemDataSet',
			xtype: 'textarea',
			IsnotField: true,
			hidden: true,
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns
		}, {
			xtype: "textarea",
			itemId: "BParameter_ParaDesc",
			name: "BParameter_ParaDesc",
			style: {
				marginBottom: '2px'
			},
			minHeight: 20,
			height: 105,
			fieldLabel: "参数说明",
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns
		});
		return items;
	},
	setItemDataSetVisible: function() {
		var me = this;
		var newValue = me.getComponent('BParameter_ItemXType').getValue();
		var visible = false;
		if (newValue != "textfield" || newValue != "numberfield") visible = true;
		me.getComponent('BParameter_ItemDataSet').setVisible(visible);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.createAddShowItems();
		items = items.concat(me.getAddLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},
	/**创建可见组件*/
	createAddShowItems: function() {
		var me = this;
		me.BParameter_Name = {
			itemId: "BParameter_Name",
			name: "BParameter_Name",
			fieldLabel: "参数名称",
			maxLength: 100,
			readOnly: true,
			locked: true
		};
		me.BParameter_SName = {
			itemId: "BParameter_SName",
			name: "BParameter_SName",
			fieldLabel: "参数分组",
			readOnly: true,
			locked: true
		};
		me.BParameter_Shortcode = {
			itemId: "BParameter_Shortcode",
			name: "BParameter_Shortcode",
			fieldLabel: "快捷码"
		};
		me.BParameter_PinYinZiTou = {
			itemId: "BParameter_PinYinZiTou",
			name: "BParameter_PinYinZiTou",
			fieldLabel: "拼音字头"
		};
		me.BParameter_DispOrder = {
			xtype: "numberfield",
			itemId: "BParameter_DispOrder",
			name: "BParameter_DispOrder",
			fieldLabel: "显示序号"
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddLayoutItems: function() {
		var me = this,
			items = [];
		//名称
		me.BParameter_Name.colspan = 2;
		me.BParameter_Name.width = me.defaults.width * me.BParameter_Name.colspan;
		items.push(me.BParameter_Name);

		//简称
		me.BParameter_SName.colspan = 1;
		me.BParameter_SName.width = me.defaults.width * me.BParameter_SName.colspan;
		items.push(me.BParameter_SName);
		//快捷码
		me.BParameter_Shortcode.colspan = 1;
		me.BParameter_Shortcode.width = me.defaults.width * me.BParameter_Shortcode.colspan;
		items.push(me.BParameter_Shortcode);

		me.BParameter_PinYinZiTou.colspan = 1;
		me.BParameter_PinYinZiTou.width = me.defaults.width * me.BParameter_PinYinZiTou.colspan;
		items.push(me.BParameter_PinYinZiTou);

		me.BParameter_DispOrder.colspan = 1;
		me.BParameter_DispOrder.width = me.defaults.width * me.BParameter_DispOrder.colspan;
		items.push(me.BParameter_DispOrder);

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		if (!values.BParameter_ParaValue || values.BParameter_ParaValue == "") {
			JShell.Msg.error("参数值不能为空!");
		}
		var isValid = me.getForm().isValid();
		if (!isValid) return;

		var entity = {
			Name: values.BParameter_Name,
			SName: values.BParameter_SName,
			Shortcode: values.BParameter_Shortcode,
			PinYinZiTou: values.BParameter_PinYinZiTou
		};
		if (values.BParameter_DispOrder) {
			entity.DispOrder = values.BParameter_DispOrder;
		}
		//参数值
		if (values.BParameter_ParaValue) {
			entity.ParaValue = values.BParameter_ParaValue.replace(/\\/g, '&#92');
			entity.ParaValue = entity.ParaValue.replace(/[\r\n]/g, '<br />');
		}
		//概要
		if (values.BParameter_ParaDesc) {
			entity.ParaDesc = values.BParameter_ParaDesc.replace(/\\/g, '&#92');
			entity.ParaDesc = entity.ParaDesc.replace(/[\r\n]/g, '<br />');
		}
		//辅助录入信息
		var itemDataSet = values.BParameter_ItemDataSet;
		if (itemDataSet) itemDataSet = itemDataSet.replace(/"/g, "'");
		var itemEditInfo = {
			ItemXType: values.BParameter_ItemXType,
			ItemDefaultValue: values.BParameter_ItemDefaultValue,
			ItemUnit: values.BParameter_ItemUnit,
			ItemDataSet: itemDataSet
		};
		if (itemEditInfo) itemEditInfo = JcallShell.JSON.encode(itemEditInfo);
		entity.ItemEditInfo = itemEditInfo;
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

		for (var i in fields) {
			var arr = fields[i].split('_');
			if (arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		entity.entity.Id = values.BParameter_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var reg = new RegExp("<br />", "g");
		//data.BParameter_ParaValue = data.BParameter_ParaValue.replace(/\\/g, '&#92');
		data.BParameter_ParaValue = data.BParameter_ParaValue.toString().replace(/&#92/g, '\\');
		data.BParameter_ParaValue = data.BParameter_ParaValue.toString().replace(reg, "\r\n");
		data.BParameter_ParaDesc = data.BParameter_ParaDesc.toString().replace(reg, "\r\n");
		data.BParameter_ParaValue = data.BParameter_ParaValue.replace(reg, "\r\n");
		//辅助录入信息
		var itemEditInfo = {
			ItemXType: "",
			ItemDefaultValue: "",
			ItemUnit: "",
			ItemDataSet: ""
		};
		var itemEditInfo1 = data["BParameter_ItemEditInfo"];
		if (itemEditInfo1) itemEditInfo1 = JShell.JSON.decode(itemEditInfo1);
		if (itemEditInfo1) itemEditInfo = itemEditInfo1;
		data["BParameter_ItemXType"] = itemEditInfo.ItemXType;
		data["BParameter_ItemDefaultValue"] = itemEditInfo.ItemDefaultValue;
		data["BParameter_ItemUnit"] = itemEditInfo.ItemUnit;
		data["BParameter_ItemDataSet"] = itemEditInfo.ItemDataSet;

		return data;
	}
});
