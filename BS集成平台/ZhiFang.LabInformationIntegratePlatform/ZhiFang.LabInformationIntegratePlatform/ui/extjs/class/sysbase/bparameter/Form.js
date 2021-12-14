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

	addUrl: "/ServerWCF/SingleTableService.svc/ST_UDTO_AddBParameterByParaNo",
	editUrl: "/ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBParameterByParaNoAndField",
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
		});

		return items;
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
		me.BParameter_ParaValue = {
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
		};
		me.BParameter_ParaDesc = {
			xtype: "textarea",
			itemId: "BParameter_ParaDesc",
			name: "BParameter_ParaDesc",
			style: {
				marginBottom: '2px'
			},
			minHeight: 20,
			height: 105,
			fieldLabel: "参数说明",
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

		me.BParameter_ParaValue.colspan = 2;
		me.BParameter_ParaValue.width = me.defaults.width * me.BParameter_ParaValue.colspan;
		items.push(me.BParameter_ParaValue);

		me.BParameter_ParaDesc.colspan = 2;
		me.BParameter_ParaDesc.width = me.defaults.width * me.BParameter_ParaDesc.colspan;
		items.push(me.BParameter_ParaDesc);

		me.BParameter_IsUse.colspan = 2;
		me.BParameter_IsUse.width = me.defaults.width * me.BParameter_IsUse.colspan;
		items.push(me.BParameter_IsUse);
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		if(!values.BParameter_ParaValue || values.BParameter_ParaValue == "") {
			JShell.Msg.error("参数值不能为空!");
		}
		var isValid = me.getForm().isValid();
		if(!isValid) return;
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		var entity = {
			Name: values.BParameter_Name,
			//EName: values.BParameter_EName,
			SName: values.BParameter_SName,
			Shortcode: values.BParameter_Shortcode,
			ParaType: values.BParameter_ParaType,
			ParaNo: values.BParameter_ParaNo,
			IsUse: values.BParameter_IsUse.toString() == "true" ? true : false
		};
		if(values.BParameter_DispOrder) {
			entity.DispOrder = values.BParameter_DispOrder;
		}

		//参数值
		if(values.BParameter_ParaValue) {
			entity.ParaValue = values.BParameter_ParaValue.replace(/\\/g, '&#92');
			entity.ParaValue = entity.ParaValue.replace(/[\r\n]/g, '<br />');
		}
		//概要
		if(values.BParameter_ParaDesc) {
			entity.ParaDesc = values.BParameter_ParaDesc.replace(/\\/g, '&#92');
			entity.ParaDesc = entity.ParaDesc.replace(/[\r\n]/g, '<br />');
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
		return data;
	},
	/**获取申请总单状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "SYSParaNo",
				"classnamespace": "ZhiFang.Entity.Gene"
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