/**
 * 库房表
 * @author liangyl	
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.storage.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '库房信息',
	requires: [
	    'Shell.ux.form.field.CheckTrigger'
	],
	width: 240,
	height: 275,
	bodyPadding: 10,
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaStorageById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaStorage',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaStorageByField',
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 55,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '库房名称',name: 'ReaStorage_CName',emptyText: '必填项',allowBlank: false
		}, {
			fieldLabel: '代码',name: 'ReaStorage_ShortCode',emptyText: '必填项',allowBlank: false
		}, {
			fieldLabel:'专项1',name:'ReaStorage_ZX1',hidden:true
		},{
			fieldLabel:'专项2',name:'ReaStorage_ZX2',hidden:true
		},{
			fieldLabel:'专项3',name:'ReaStorage_ZX3',hidden:true
		},{
			fieldLabel:'启用',name:'ReaStorage_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:true
		},{
			fieldLabel:'显示次序',name:'ReaStorage_DispOrder',emptyText:'必填项',allowBlank:false,xtype:'numberfield',value:0
		},{
			height: 85,fieldLabel:'描述',emptyText: '描述',name: 'ReaStorage_Memo',xtype: 'textarea'
		}, {
			fieldLabel: '主键ID',name: 'ReaStorage_Id',hidden: true
		});
		return items;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName: values.ReaStorage_CName,
			ShortCode: values.ReaStorage_ShortCode,
			ZX1: values.ReaStorage_ZX1,
			ZX2: values.ReaStorage_ZX2,
			ZX3: values.ReaStorage_ZX3,
			DispOrder: values.ReaStorage_DispOrder,
			Memo: values.ReaStorage_Memo,
			Visible:values.ReaStorage_Visible ? 1 : 0
		};
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId){
			entity.CreaterID=userId;
		    entity.CreaterName = userName;
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
			'CName', 'Id', 'ShortCode', 'ZX1',
			'ZX2','ZX3','DispOrder','Memo','Visible'
		];
		entity.fields = fields.join(',');
		if(values.ReaStorage_Id != '') {
			entity.entity.Id = values.ReaStorage_Id;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	  /**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	}
});