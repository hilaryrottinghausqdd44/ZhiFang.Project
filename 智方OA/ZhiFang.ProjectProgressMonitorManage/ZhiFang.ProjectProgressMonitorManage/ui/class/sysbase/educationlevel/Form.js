/**
 * 学历维护
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.educationlevel.Form', {
	extend:'Shell.ux.form.Panel',
		requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title:'学历维护',
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
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBEducationLevelById?isPlanish=true',
	//新增服务地址
    addUrl:'/SingleTableService.svc/ST_UDTO_AddBEducationLevel',
    //修改服务地址
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBEducationLevelByField',
	
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
			{fieldLabel:'名称',name:'BEducationLevel_Name',itemId:'BEducationLevel_Name',
			emptyText:'必填项',allowBlank:false},
			{fieldLabel:'简称',name:'BEducationLevel_SName'},
			{fieldLabel:'快捷码',name:'BEducationLevel_Shortcode'},
			{fieldLabel:'拼音字头',name:'BEducationLevel_PinYinZiTou'},
			{fieldLabel:'备注',name:'BEducationLevel_Comment',xtype:'textarea'},
			{fieldLabel:'使用',name:'BEducationLevel_IsUse',xtype:'checkbox',checked:true},
			{fieldLabel:'主键ID',name:'BEducationLevel_Id',hidden:true}
		];
			
		return items;
	},
	//@overwrite 获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Name:values.BEducationLevel_Name,
			SName:values.BEducationLevel_SName,
			PinYinZiTou:values.BEducationLevel_PinYinZiTou,
			Shortcode:values.BEducationLevel_Shortcode,
			UseCode:values.BEducationLevel_UseCode,
			Comment:values.BEducationLevel_Comment,
			IsUse:values.BEducationLevel_IsUse ? 1 : 0
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
		
		entity.entity.Id = values.BEducationLevel_Id;
		return entity;
	},
	/**初始化表单监听*/
	initFromListeners: function() {
		var me = this;

		//拼音字头
		var CName = me.getComponent('BEducationLevel_Name');
		CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if(newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BEducationLevel_Shortcode: value,
								BEducationLevel_PinYinZiTou:value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						BEducationLevel_Shortcode: "",
						BEducationLevel_PinYinZiTou:""
					});
				}
			}
		});
	}
});