/**
 * 节假日设置
 * @author longfc
 * @version 2016-11-04
 */
Ext.define("Shell.class.oa.at.attendance.holidaysetting.Form", {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	width: 320,
	height: 380,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	title: "节假日设置",
	isSuccessMsg: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	objectName: "ATHolidaySetting",
	selectUrl: '/WeiXinAppService.svc/ST_UDTO_SearchATHolidaySettingById?isPlanish=true',

	/**新增数据服务路径*/
	addUrl: '/WeiXinAppService.svc/ST_UDTO_AddATHolidaySetting',
	/**修改服务地址*/
	editUrl: '/WeiXinAppService.svc/ST_UDTO_UpdateATHolidaySettingByField',
	/**启用表单状态初始化*/
	openFormType: true,
	formtype: 'edit',
	bodyPadding: '5px 5px 5px 5px',
	layout: {
		type: 'table',
		columns: 1 //每行有几列
	},
	PDictId: '',
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 280,
		labelAlign: 'right'
	},
	/**内容自动显示*/
	autoScroll: true,
	initComponent: function() {
		var me = this;
		me.width = me.width;
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
			itemId: "ATHolidaySetting_Id",
			hidden: true,
			name: "ATHolidaySetting_Id",
			fieldLabel: "主键ID"
		});

		return items;
	},

	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		//me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddShowItems();
		items = items.concat(me.getAddFFileTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},
	/**创建可见组件*/
	createAddShowItems: function() {
		var me = this;
		me.ATHolidaySetting_Name = {
			itemId: "ATHolidaySetting_Name",
			name: "ATHolidaySetting_Name",
			fieldLabel: "名称"
		};
		me.ATHolidaySetting_HolidayName = {
			xtype: "combobox",
			hasButton: false,
			model: "local",
			editable: true,
			displayField: "text",
			valueField: "value",
			value: "",
			store: new Ext.data.SimpleStore({
				fields: ["value", "text"],
				data: [
					["周六", "周六"],
					["周日", "周日"],
					["元旦", "元旦"],
					["除夕", "除夕"],
					["春节", "春节"],
					["清明节", "清明节"],
					["劳动节", "劳动节"],
					["中秋节", "中秋节"],
					["国庆节", "国庆节"]
				]
			}),
			itemId: "ATHolidaySetting_HolidayName",
			name: "ATHolidaySetting_HolidayName",
			fieldLabel: "节假日"
		};
		me.ATHolidaySetting_SName = {
			itemId: "ATHolidaySetting_SName",
			name: "ATHolidaySetting_SName",
			fieldLabel: "简称"
		};
		me.ATHolidaySetting_Shortcode = {
			itemId: "ATHolidaySetting_Shortcode",
			name: "ATHolidaySetting_Shortcode",
			fieldLabel: "快捷码"
		};
		me.ATHolidaySetting_DispOrder = {
			xtype: "numberfield",
			itemId: "ATHolidaySetting_DispOrder",
			name: "ATHolidaySetting_DispOrder",
			fieldLabel: "显示序号",
		};

		me.ATHolidaySetting_Comment = {
			xtype: "textarea",
			itemId: "ATHolidaySetting_Comment",
			name: "ATHolidaySetting_Comment",
			minHeight: 60,
			height: 120,
			style: {
				marginBottom: '4px'
			},
			fieldLabel: "概要",
		};

		me.ATHolidaySetting_IsUse = {
			xtype: "checkbox",
			boxLabel: "",
			inputValue: "true",
			uncheckedValue: "false",
			checked: true,
			itemId: "ATHolidaySetting_IsUse",
			name: "ATHolidaySetting_IsUse",
			fieldLabel: "是否使用"
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		//名称
		me.ATHolidaySetting_Name.colspan = 1;
		me.ATHolidaySetting_Name.width = me.defaults.width * me.ATHolidaySetting_Name.colspan;
		items.push(me.ATHolidaySetting_Name);
		me.ATHolidaySetting_HolidayName.colspan = 1;
		me.ATHolidaySetting_HolidayName.width = me.defaults.width * me.ATHolidaySetting_HolidayName.colspan;
		items.push(me.ATHolidaySetting_HolidayName);
		//简称
		me.ATHolidaySetting_SName.colspan = 1;
		me.ATHolidaySetting_SName.width = me.defaults.width * me.ATHolidaySetting_SName.colspan;
		items.push(me.ATHolidaySetting_SName);
		//快捷码
		me.ATHolidaySetting_Shortcode.colspan = 1;
		me.ATHolidaySetting_Shortcode.width = me.defaults.width * me.ATHolidaySetting_Shortcode.colspan;
		items.push(me.ATHolidaySetting_Shortcode);

		me.ATHolidaySetting_DispOrder.colspan = 1;
		me.ATHolidaySetting_DispOrder.width = me.defaults.width * me.ATHolidaySetting_DispOrder.colspan;
		items.push(me.ATHolidaySetting_DispOrder);

		me.ATHolidaySetting_IsUse.colspan = 1;
		items.push(me.ATHolidaySetting_IsUse);

		me.ATHolidaySetting_Comment.colspan = 1;
		me.ATHolidaySetting_Comment.width = me.defaults.width * me.ATHolidaySetting_Comment.colspan;
		items.push(me.ATHolidaySetting_Comment);

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		//		if(!values.ATHolidaySetting_Comment || values.ATHolidaySetting_Comment == "") {
		//			JShell.Msg.error("参数值不能为空!");
		//		}
		var isValid = me.getForm().isValid();
		if(!isValid) return;
		var entity = {
			Name: values.ATHolidaySetting_Name,
			//EName: values.ATHolidaySetting_EName,
			SName: values.ATHolidaySetting_SName,
			Shortcode: values.ATHolidaySetting_Shortcode,
			HolidayName: values.ATHolidaySetting_HolidayName,
			IsUse: values.ATHolidaySetting_IsUse.toString() == "true" ? true : false
		};
		if(values.ATHolidaySetting_DispOrder) {
			entity.DispOrder = values.ATHolidaySetting_DispOrder;
		}

		//概要
		if(values.ATHolidaySetting_Comment) {
			entity.Comment = values.ATHolidaySetting_Comment.replace(/\\/g, '&#92');
			entity.Comment = entity.Comment.replace(/[\r\n]/g, '<br />');
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
		//fieldsArr.push('BParameter_Id');
		entity.fields = fieldsArr.join(',');

		entity.entity.Id = values.ATHolidaySetting_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var reg = new RegExp("<br />", "g");
		data.ATHolidaySetting_Comment = data.ATHolidaySetting_Comment.toString().replace(/&#92/g, '\\');
		data.ATHolidaySetting_Comment = data.ATHolidaySetting_Comment.toString().replace(reg, "\r\n");
		data.ATHolidaySetting_Comment = data.ATHolidaySetting_Comment.replace(reg, "\r\n");
		return data;
	}
});