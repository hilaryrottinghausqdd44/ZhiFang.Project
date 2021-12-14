/**
 * 性别表单
 * @author Jcall
 * @version 2018-09-14
 */
Ext.define('Shell.class.sysbase.sex.Form', {
	extend:'Shell.ux.form.Panel',
	title:'性别信息',
	width:250,
	height:300,
	bodyPadding:10,
	layout:'anchor',
	defaults:{
		anchor:'100%',
		labelWidth:60,
		labelAlign:'right'
	},
	autoScroll:true,
	
	//获取数据服务路径
    selectUrl:'/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBSexById?isPlanish=true',
	//新增服务地址
    addUrl:'/ServerWCF/SingleTableService.svc/ST_UDTO_AddBSex',
    //修改服务地址
    editUrl:'/ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBSexByField',
	
	//是否启用保存按钮
	hasSave:true,
	//是否重置按钮
	hasReset:true,
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		var Name = me.getComponent('BSex_Name');
		Name.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								BSex_PinYinZiTou:value,
								BSex_Shortcode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						BSex_PinYinZiTou:"",
						BSex_Shortcode:""
					});
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [
			{fieldLabel:'性别名称',name:'BSex_Name',itemId:'BSex_Name'},
			{fieldLabel:'简称',name:'BSex_SName'},
			{fieldLabel:'快捷码',name:'BSex_Shortcode'},
			{fieldLabel:'拼音字头',name:'BSex_PinYinZiTou'},
			{fieldLabel:'备注',name:'BSex_Comment',xtype:'textarea'},
			{fieldLabel:'使用',name:'BSex_IsUse',xtype:'checkbox',checked:true},
			{fieldLabel:'主键ID',name:'BSex_Id',hidden:true}
		];
		return items;
	},
	//@overwrite 获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.BSex_Name,
			SName:values.BSex_SName,
			Shortcode:values.BSex_Shortcode,
			PinYinZiTou:values.BSex_PinYinZiTou,
			Comment:values.BSex_Comment,
			IsUse:values.BSex_IsUse ? true : false
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
		
		entity.entity.Id = values.BSex_Id;
		return entity;
	}
});