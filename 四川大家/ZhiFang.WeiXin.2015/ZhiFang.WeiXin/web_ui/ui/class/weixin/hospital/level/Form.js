/**
 * 医院等级表单
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.hospital.level.Form',{
    extend:'Shell.ux.form.Panel',
    
    title:'医院等级信息',
    width:240,
	height:400,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalLevelById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBHospitalLevel',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBHospitalLevelByField', 
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:60,
        labelAlign:'right'
    },
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//表单监听
		me.initFromListeners();
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
		
		var items = [
			{fieldLabel:'等级名称',name:'BHospitalLevel_Name',
				itemId:'BHospitalLevel_Name',emptyText:'必填项',allowBlank:false},
			{fieldLabel:'等级简称',name:'BHospitalLevel_SName'},
			{fieldLabel:'拼音字头',name:'BHospitalLevel_PinYinZiTou'},
			{fieldLabel:'快捷码',name:'BHospitalLevel_Shortcode'},
			{fieldLabel:'等级编码',name:'BHospitalLevel_Code'},
			{fieldLabel:'等级描述',height:85,labelAlign:'top',
				name:'BHospitalLevel_Comment',xtype:'textarea'
			},
			{boxLabel:'是否使用',name:'BHospitalLevel_IsUse',
				xtype:'checkbox',checked:true
			},
			{fieldLabel:'主键ID',name:'BHospitalLevel_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.BHospitalLevel_Name,
			SName:values.BHospitalLevel_SName,
			PinYinZiTou:values.BHospitalLevel_PinYinZiTou,
			Shortcode:values.BHospitalLevel_Shortcode,
			Code:values.BHospitalLevel_Code,
			IsUse:values.BHospitalLevel_IsUse ? true : false,
			Comment:values.BHospitalLevel_Comment
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
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
		
		entity.entity.Id = values.BHospitalLevel_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化表单监听*/
	initFromListeners:function(){
		var me = this;
		
		var BHospitalLevel_Name = me.getComponent('BHospitalLevel_Name');
		BHospitalLevel_Name.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								BHospitalLevel_PinYinZiTou:value,
								BHospitalLevel_Shortcode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						BHospitalLevel_PinYinZiTou:"",
						BHospitalLevel_Shortcode:""
					});
				}
			}
		});
	}
});