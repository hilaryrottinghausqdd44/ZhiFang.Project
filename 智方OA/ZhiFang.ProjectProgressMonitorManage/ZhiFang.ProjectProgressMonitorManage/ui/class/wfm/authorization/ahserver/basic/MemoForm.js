/**
 * 服务器授权备注表单
 * @author longfc	
 * @version 2020-08-21
 */
Ext.define('Shell.class.wfm.authorization.ahserver.basic.MemoForm', {
	extend: 'Shell.ux.form.Panel',
	
	title: '授权备注',
	width: 480,
	height: 450,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerLicenceById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddAHServerLicence',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerLicenceByField',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 65,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**启用表单状态初始化*/
	openFormType: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;
		var items = [{
			fieldLabel: '服务器授权编号',
			name: 'AHServerLicence_Id',
			itemId: 'AHServerLicence_Id',
			type:"key",
			hidden:true,
			locked: true,
			readOnly: true
		},{
			fieldLabel: '备注',
			height: me.height - 80,
			name: 'AHServerLicence_Comment',
			itemId: 'AHServerLicence_Comment',
			xtype: 'textarea'
		}];

		return items;
	},
	/**更改标题*/
	changeTitle: function() {},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id:-2,
			Comment: values.AHServerLicence_Comment
		};
		if (values.AHServerLicence_Id) entity.Id = values.AHServerLicence_Id;
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
		entity.entity.Id = values.AHServerLicence_Id;
		return entity;
	}
});