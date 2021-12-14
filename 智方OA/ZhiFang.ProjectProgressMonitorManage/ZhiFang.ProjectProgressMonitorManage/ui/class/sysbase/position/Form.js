/**
 * 职务维护
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.position.Form', {
	extend:'Shell.ux.form.Panel',
		requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title:'职务维护',
	width:400,
	height:300,
	bodyPadding:'20px 10px',
	layout:'anchor',
	defaults:{
		anchor:'100%',
		labelWidth:80,
		labelAlign:'right'
	},
	autoScroll:true,
	
	//获取数据服务路径
    selectUrl:'/RBACService.svc/RBAC_UDTO_SearchHRPositionById?isPlanish=true',
	//新增服务地址
    addUrl:'/RBACService.svc/RBAC_UDTO_AddHRPosition',
    //修改服务地址
    editUrl:'/RBACService.svc/RBAC_UDTO_UpdateHRPositionByField',
	
	//是否启用保存按钮
	hasSave:true,
	//是否重置按钮
	hasReset:true,
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		//表单监听
		me.initFromListeners();
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [
			{fieldLabel:'名称',name:'HRPosition_CName',itemId:'HRPosition_CName',
			emptyText:'必填项',allowBlank:false},
			{fieldLabel:'简称',name:'HRPosition_SName'},
			{fieldLabel:'英文名称',name:'HRPosition_EName'},
			{fieldLabel:'快捷码',name:'HRPosition_Shortcode'},
			{fieldLabel:'代码',name:'HRPosition_UseCode'},
//			{fieldLabel:'开发商代码',name:'HRPosition_UseCode'},
			{fieldLabel:'备注',name:'HRPosition_Comment',xtype:'textarea'},
			{fieldLabel:'使用',name:'HRPosition_IsUse',xtype:'checkbox',checked:true},
			{fieldLabel:'主键ID',name:'HRPosition_Id',hidden:true}
		];
			
		return items;
	},
	//@overwrite 获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName:values.HRPosition_CName,
			SName:values.HRPosition_SName,
			EName:values.HRPosition_EName,
			Shortcode:values.HRPosition_Shortcode,
			UseCode:values.HRPosition_UseCode,
			Comment:values.HRPosition_Comment,
			IsUse:values.HRPosition_IsUse ? 1 : 0
		};
		return {entity:entity};
	},
	//@overwrite 获取修改的数据
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		
		for(var i in fields){
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		
		entity.entity.Id = values.HRPosition_Id;
		return entity;
	},
	/**初始化表单监听*/
	initFromListeners: function() {
		var me = this;

		//拼音字头
		var CName = me.getComponent('HRPosition_CName');
		CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if(newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								HRPosition_Shortcode: value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						HRPosition_Shortcode: ""
					});
				}
			}
		});
		
		
	}
});