/**
 * @description 申请明细及订单明细模板
 * @author longfc
 * @version 2018-01-16
 */
Ext.define('Shell.class.rea.client.goodstemplate.ApplyForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '新增模板',
	width: 405,
	height: 205,
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaChooseGoodsTemplateById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaChooseGoodsTemplate',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaChooseGoodsTemplateByField',

	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 1 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 185,
		labelAlign: 'right'
	},
	hasSave: true,
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,

	/**模板类型*/
	TemplateType: null,
	/**模板内容*/
	ContextJson: null,

	/**所属机构Id*/
	OrgID: null,
	/**所属机构*/
	OrgName: null,

	/**所属部门Id*/
	DeptID: null,
	/**所属部门*/
	DeptName: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.width = me.width || 405;
		me.defaults.width = parseInt((me.width - 25) / me.layout.columns);
		if(me.defaults.width < 185) me.defaults.width = 185;
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '主键ID',
			name: 'ReaChooseGoodsTemplate_Id',
			hidden: true,
			type: 'key'
		});
		//模板类型
		items.push({
			fieldLabel: '模板类型',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			name: 'ReaChooseGoodsTemplate_SName',
			itemId: 'ReaChooseGoodsTemplate_SName',
			data: [
				["ReaReqDtl", "申请明细"],
				["ReaOrderDtl", "订单明细"]
			],
			value: me.TemplateType,
			colspan: 1,
			readOnly: true,
			locked: true,
			hidden: true,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '模板名称',
			name: 'ReaChooseGoodsTemplate_CName',
			itemId: 'ReaChooseGoodsTemplate_CName',
			colspan: 1,
			width: me.defaults.width * 1,
			emptyText: '必填项',
			allowBlank: false
		});
		items.push({
			fieldLabel: '模板简码',
			name: 'ReaChooseGoodsTemplate_ShortCode',
			itemId: 'ReaChooseGoodsTemplate_ShortCode',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaChooseGoodsTemplate_Memo',
			height: 40,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '所属机构Id',
			name: 'ReaChooseGoodsTemplate_OrgID',
			value: me.OrgID,
			hidden: true
		}, {
			fieldLabel: '所属机构',
			name: 'ReaChooseGoodsTemplate_OrgName',
			value: me.OrgName,
			hidden: true,
			readOnly: true,
			locked: true
		});

		items.push({
			fieldLabel: '所属部门Id',
			name: 'ReaChooseGoodsTemplate_DeptID',
			value: me.DeptID,
			hidden: true
		}, {
			fieldLabel: '所属部门',
			name: 'ReaChooseGoodsTemplate_DeptName',
			value: me.DeptName,
			hidden: true,
			readOnly: true,
			locked: true
		});
		items.push({
			fieldLabel: '模板内容',
			name: 'ReaChooseGoodsTemplate_ContextJson',
			itemId: 'ReaChooseGoodsTemplate_ContextJson',
			value: me.ContextJson,
			hidden: true,
			readOnly: true,
			locked: true
		});
		return items;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		if(me.ContextJson) me.ContextJson = me.ContextJson.replace(/"/g, "'");
		var objValue = {
			"ReaChooseGoodsTemplate_SName": me.TemplateType,
			"ReaChooseGoodsTemplate_OrgID": me.OrgID,
			"ReaChooseGoodsTemplate_OrgName": me.OrgName,
			"ReaChooseGoodsTemplate_DeptID": me.DeptID,
			"ReaChooseGoodsTemplate_DeptName": me.DeptName,
			"ReaChooseGoodsTemplate_ContextJson": me.ContextJson
		};
		me.lastData = objValue;
		me.getForm().setValues(objValue);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var values = me.getForm().getValues();
		if(!me.getForm().isValid()) return;

		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) {
			JShell.Msg.error("封装提交信息出错!");
			return;
		}
		var id = params.entity.Id;
		params = JcallShell.JSON.encode(params);
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				id = me.formtype == 'add' ? data.value : id;
				id += '';
				me.fireEvent('save', me, id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var contextJson = values.ReaChooseGoodsTemplate_ContextJson;
		var memo = values.ReaChooseGoodsTemplate_Memo;
		if(contextJson) contextJson = contextJson.replace(/"/g, '');
		if(memo) memo = memo.replace(/"/g, '');
		var entity = {
			"CName": values.ReaChooseGoodsTemplate_CName,
			"ShortCode": values.ReaChooseGoodsTemplate_ShortCode,
			"SName": values.ReaChooseGoodsTemplate_SName,
			"ContextJson": contextJson,
			"Memo": memo
		};
		//模板类型
		switch(values.ReaChooseGoodsTemplate_SName) {
			case "ReaReqDtl":
				if(values.ReaChooseGoodsTemplate_DeptID) entity.DeptID = values.ReaChooseGoodsTemplate_DeptID;
				entity.DeptName = values.ReaChooseGoodsTemplate_DeptName;
				break;
			default:
				if(values.ReaChooseGoodsTemplate_OrgID) entity.OrgID = values.ReaChooseGoodsTemplate_OrgID;
				entity.OrgName = values.ReaChooseGoodsTemplate_OrgName;
				break;
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
		entity.fields = 'Id,DeptID,DeptName,OrgID,OrgName,CName,ShortCode,ContextJson,SName,Memo';
		entity.entity.Id = values.ReaChooseGoodsTemplate_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return data;
	}
});