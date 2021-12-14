/**
 * 系统参数表单
 * @author longfc
 * @version 2016-10-11
 */
Ext.define("Shell.class.sysbase.bparameter.Form", {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: "系统参数",
	width: 380,

	addUrl: "/SingleTableService.svc/ST_UDTO_AddBParameter",
	editUrl: "/SingleTableService.svc/ST_UDTO_UpdateBParameterByField",
	selectUrl: "/SingleTableService.svc/ST_UDTO_SearchBParameterById?isPlanish=true",

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
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,

	SYSParaNoList: [],
	/**申请单状态枚举*/
	SYSParaNoEnum: {},
	/**申请单状态背景颜色枚举*/
	SYSParaNoBGColorEnum: {},
	SYSParaNoFColorEnum: {},
	SYSParaNoBGColorEnum: {},

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
		me.getSYSParaNoData();
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
			maxLength: 100
		};
		me.BParameter_SName = {
			itemId: "BParameter_SName",
			name: "BParameter_SName",
			fieldLabel: "参数分组"
		};
		me.BParameter_Shortcode = {
			itemId: "BParameter_Shortcode",
			name: "BParameter_Shortcode",
			fieldLabel: "快捷码"
		};
		me.BParameter_ParaType = {
			itemId: "BParameter_ParaType",
			name: "BParameter_ParaType",
			fieldLabel: "参数类型",
			xtype: "combobox",
			hasButton: false,
			model: "local",
			editable: false,
			displayField: "text",
			valueField: "value",
			value: "CONFIG",
			store: new Ext.data.SimpleStore({
				fields: ["value", "text"],
				data: [
					["CONFIG", "可配置参数"],
					["SYS", "全系统"]
				]
			})
		};
		me.BParameter_DispOrder = {
			xtype: "numberfield",
			itemId: "BParameter_DispOrder",
			name: "BParameter_DispOrder",
			fieldLabel: "显示序号",
		};
		me.BParameter_ParaNo = {
			xtype: 'uxSimpleComboBox',
			data: me.SYSParaNoList,
			itemId: "BParameter_ParaNo",
			name: "BParameter_ParaNo",
			fieldLabel: "参数编码"
		};
		me.BParameter_IsUse = {
			xtype: "checkbox",
			boxLabel: "",
			inputValue: "true",
			uncheckedValue: "false",
			checked: true,
			itemId: "BParameter_IsUse",
			name: "BParameter_IsUse",
			fieldLabel: "是否使用"
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

		me.BParameter_ParaType.colspan = 1;
		me.BParameter_ParaType.width = me.defaults.width * me.BParameter_ParaType.colspan;
		items.push(me.BParameter_ParaType);
		me.BParameter_DispOrder.colspan = 1;
		me.BParameter_DispOrder.width = me.defaults.width * me.BParameter_DispOrder.colspan;
		items.push(me.BParameter_DispOrder);

		me.BParameter_ParaNo.colspan = 2;
		me.BParameter_ParaNo.width = me.defaults.width * me.BParameter_ParaNo.colspan;
		items.push(me.BParameter_ParaNo);

		me.BParameter_IsUse.colspan = 2;
		me.BParameter_IsUse.width = me.defaults.width * me.BParameter_IsUse.colspan;
		items.push(me.BParameter_IsUse);

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
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		var entity = {
			Id:-2,
			Name: values.BParameter_Name,
			//EName: values.BParameter_EName,
			SName: values.BParameter_SName,
			Shortcode: values.BParameter_Shortcode,
			ParaType: values.BParameter_ParaType,
			ParaNo: values.BParameter_ParaNo,
			IsUse: values.BParameter_IsUse.toString() == "true" ? true : false
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

		for(var i in fields) {
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		fieldsArr.push('BDict_Id');
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
	},
	/**获取申请总单状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "SYSParaNo",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		};
		return params;
	},
	/**获取系统参数编码信息*/
	getSYSParaNoData: function(callback) {
		var me = this;
		if(me.SYSParaNoList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode(me.getParams());
		me.SYSParaNoList = [];
		me.SYSParaNoEnum = {};
		me.SYSParaNoFColorEnum = {};
		me.SYSParaNoBGColorEnum = {};
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].SYSParaNo.length > 0) {
						//me.SYSParaNoList.push(["", '全部', 'font-weight:bold;color:black;text-align:center;']);
						Ext.Array.each(data.value[0].SYSParaNo, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								me.SYSParaNoFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor); //background-
								me.SYSParaNoBGColorEnum[obj.Id] = obj.BGColor;
							}
							me.SYSParaNoEnum[obj.Id] = obj.Name;
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.SYSParaNoList.push(tempArr);
						});
					}
				}
			}
		}, false);
	}
});