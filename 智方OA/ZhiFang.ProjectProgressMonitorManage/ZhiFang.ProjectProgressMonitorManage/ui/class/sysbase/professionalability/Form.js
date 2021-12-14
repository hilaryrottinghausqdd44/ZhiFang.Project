/**
 * 专业级别维护
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.professionalability.Form', {
	extend:'Shell.ux.form.Panel',
		requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title:'专业级别维护',
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
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBProfessionalAbilityById?isPlanish=true',
	//新增服务地址
    addUrl:'/SingleTableService.svc/ST_UDTO_AddBProfessionalAbility',
    //修改服务地址
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBProfessionalAbilityByField',
	
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
			{fieldLabel:'名称',name:'BProfessionalAbility_Name',itemId:'BProfessionalAbility_Name',
			emptyText:'必填项',allowBlank:false},
			{fieldLabel:'简称',name:'BProfessionalAbility_SName'},
			{fieldLabel:'快捷码',name:'BProfessionalAbility_Shortcode'},
			{fieldLabel:'拼音字头',name:'BProfessionalAbility_PinYinZiTou'},
			{fieldLabel:'备注',name:'BProfessionalAbility_Comment',xtype:'textarea'},
			{fieldLabel:'使用',name:'BProfessionalAbility_IsUse',xtype:'checkbox',checked:true},
			{fieldLabel:'主键ID',name:'BProfessionalAbility_Id',hidden:true}
		];
			
		return items;
	},
	//@overwrite 获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Name:values.BProfessionalAbility_Name,
			SName:values.BProfessionalAbility_SName,
			PinYinZiTou:values.BProfessionalAbility_PinYinZiTou,
			Shortcode:values.BProfessionalAbility_Shortcode,
			UseCode:values.BProfessionalAbility_UseCode,
			Comment:values.BProfessionalAbility_Comment,
			IsUse:values.BProfessionalAbility_IsUse ? 1 : 0
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
		
		entity.entity.Id = values.BProfessionalAbility_Id;
		return entity;
	},
	/**初始化表单监听*/
	initFromListeners: function() {
		var me = this;

		//拼音字头
		var CName = me.getComponent('BProfessionalAbility_Name');
		CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if(newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BProfessionalAbility_Shortcode: value,
								BProfessionalAbility_PinYinZiTou:value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						BProfessionalAbility_Shortcode: "",
						BProfessionalAbility_PinYinZiTou:""
					});
				}
			}
		});
	}
});