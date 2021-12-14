/**
 * 就诊类型维护
 * @author GHX
 * @version 2021-01-29
 */
Ext.define('Shell.class.weixin.sickType.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    
    title:'就诊类型信息',
    width:240,
	height:400,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchSickTypeById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddSickType',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateSickTypeByField', 
	
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
			{fieldLabel:'就诊类型名称',emptyText:'必填项',allowBlank:false,
				name:'SickType_CName',itemId:'SickType_CName'},
			{fieldLabel:'快捷码',name:'SickType_ShortCode'},
			{fieldLabel:'排序',name:'SickType_DispOrder'},
			//{fieldLabel:'HIS编码',name:'SickType_HisOrderCode'},
			{fieldLabel:'主键ID',name:'SickType_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.SickType_CName,
			ShortCode:values.SickType_ShortCode,
			DispOrder:values.SickType_DispOrder,
			HisOrderCode:values.SickType_HisOrderCode
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
		
		entity.entity.Id = values.SickType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化表单监听*/
	initFromListeners:function(){
		var me = this;
		
		var SickType_CName = me.getComponent('SickType_CName');
		SickType_CName.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								SickType_ShortCode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						SickType_ShortCode:""
					});
				}
			}
		});
	}
});