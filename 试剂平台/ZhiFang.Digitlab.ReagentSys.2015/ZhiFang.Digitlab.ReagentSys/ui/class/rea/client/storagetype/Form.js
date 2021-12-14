/**
 * 入库类型表单
 * @author liangyl
 * @version 2017-11-10
 */
Ext.define('Shell.class.rea.client.storagetype.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '入库类型信息',
	requires: [
	    'Shell.ux.form.field.CheckTrigger'
	],
	width: 240,
	height: 300,
	/**内容周围距离*/
	bodyPadding: '10px',
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBStorageTypeById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddBStorageType',
	 /**修改服务地址*/
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBStorageTypeByField',
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
			fieldLabel: '类型名称',name: 'BStorageType_Name',emptyText: '必填项',allowBlank: false
		}, {
			fieldLabel:'简称',name:'BStorageType_SName'
		},{
			fieldLabel: '快捷码',name: 'BStorageType_Shortcode',emptyText: '必填项',allowBlank: false
		}, {
			fieldLabel:'拼音字头',name:'BStorageType_PinYinZiTou'
		},{
			fieldLabel:'启用',name:'BStorageType_IsUse',
			xtype:'uxBoolComboBox',value:true,hasStyle:true
		},{
			height: 85,fieldLabel:'描述',emptyText: '描述',name: 'BStorageType_Comment',xtype: 'textarea'
		}, {
			fieldLabel: '主键ID',name: 'BStorageType_Id',hidden: true
		});
		return items;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Name: values.BStorageType_Name,
			Shortcode: values.BStorageType_Shortcode,
			SName: values.BStorageType_SName,
			PinYinZiTou: values.BStorageType_PinYinZiTou,
			Comment: values.BStorageType_Comment,
			IsUse:values.BStorageType_IsUse ? 1 : 0
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
			'Name', 'Id', 'Shortcode', 'SName',
			'PinYinZiTou','Comment','IsUse'
		];
		entity.fields = fields.join(',');
		if(values.BStorageType_Id != '') {
			entity.entity.Id = values.BStorageType_Id;
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