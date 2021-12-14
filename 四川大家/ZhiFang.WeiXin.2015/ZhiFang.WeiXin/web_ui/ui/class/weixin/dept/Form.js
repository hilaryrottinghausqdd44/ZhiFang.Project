/**
 * 医院科室表单
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.dept.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    
    title:'医院科室信息',
    width:240,
	height:400,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalDeptById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBHospitalDept',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBHospitalDeptByField', 
	
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
			{fieldLabel:'科室名称',emptyText:'必填项',allowBlank:false,
				name:'BHospitalDept_Name',itemId:'BHospitalDept_Name'},
			{fieldLabel:'科室简称',name:'BHospitalDept_SName'},
			{fieldLabel:'拼音字头',name:'BHospitalDept_PinYinZiTou'},
			{fieldLabel:'快捷码',name:'BHospitalDept_Shortcode'},
			{fieldLabel:'科室描述',height:85,labelAlign:'top',
				name:'BHospitalDept_Comment',xtype:'textarea'
			},
			{boxLabel:'是否使用',name:'BHospitalDept_IsUse',
				xtype:'checkbox',checked:true
			},
			{fieldLabel:'主键ID',name:'BHospitalDept_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.BHospitalDept_Name,
			SName:values.BHospitalDept_SName,
			PinYinZiTou:values.BHospitalDept_PinYinZiTou,
			Shortcode:values.BHospitalDept_Shortcode,
			Comment:values.BHospitalDept_Comment,
			IsUse:values.BHospitalDept_IsUse ? true : false
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
		
		entity.entity.Id = values.BHospitalDept_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化表单监听*/
	initFromListeners:function(){
		var me = this;
		
		var BHospitalDept_Name = me.getComponent('BHospitalDept_Name');
		BHospitalDept_Name.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								BHospitalDept_PinYinZiTou:value,
								BHospitalDept_Shortcode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						BHospitalDept_PinYinZiTou:"",
						BHospitalDept_Shortcode:""
					});
				}
			}
		});
	}
});