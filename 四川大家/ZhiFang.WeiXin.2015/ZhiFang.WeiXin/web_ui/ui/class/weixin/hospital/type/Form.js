/**
 * 医院分类表单
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.hospital.type.Form',{
    extend:'Shell.ux.form.Panel',
    
    title:'医院分类信息',
    width:240,
	height:400,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalTypeById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBHospitalType',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBHospitalTypeByField', 
	
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
		
		var BHospitalType_Name = me.getComponent('BHospitalType_Name');
		BHospitalType_Name.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								BHospitalType_PinYinZiTou:value,
								BHospitalType_Shortcode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						BHospitalType_PinYinZiTou:"",
						BHospitalType_Shortcode:""
					});
				}
			}
		});
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
		
		var items = [
			{fieldLabel:'分类名称',name:'BHospitalType_Name',
				itemId:'BHospitalType_Name',emptyText:'必填项',allowBlank:false},
			{fieldLabel:'分类简称',name:'BHospitalType_SName'},
			{fieldLabel:'拼音字头',name:'BHospitalType_PinYinZiTou'},
			{fieldLabel:'快捷码',name:'BHospitalType_Shortcode'},
			{fieldLabel:'分类编码',name:'BHospitalType_Code'},
			{fieldLabel:'分类描述',height:85,labelAlign:'top',
				name:'BHospitalType_Comment',xtype:'textarea'
			},
			{boxLabel:'是否使用',name:'BHospitalType_IsUse',
				xtype:'checkbox',checked:true
			},
			{fieldLabel:'主键ID',name:'BHospitalType_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.BHospitalType_Name,
			SName:values.BHospitalType_SName,
			PinYinZiTou:values.BHospitalType_PinYinZiTou,
			Shortcode:values.BHospitalType_Shortcode,
			Code:values.BHospitalType_Code,
			IsUse:values.BHospitalType_IsUse ? true : false,
			Comment:values.BHospitalType_Comment
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
		
		entity.entity.Id = values.BHospitalType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	}
});