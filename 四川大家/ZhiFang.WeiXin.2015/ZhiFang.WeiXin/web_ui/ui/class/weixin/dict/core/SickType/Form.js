/**
 * 就诊类型字典表
 * @author guozhaojing
 * @version 2018-03-28
 */
Ext.define('Shell.class.weixin.dict.core.SickType.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    title:'就诊类型字典',
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
        labelWidth:80,
        labelAlign:'right'
    },
  
    
    createItems:function(){
		var me = this;
		
		var items = [
			{fieldLabel:'编码',name:'SickType_Id',readOnly:true,itemId:'SickType_Id',locked:true},
			{fieldLabel:'中文名称',name:'SickType_CName',itemId:'SickType_CName'},
			{fieldLabel:'简码',name:'SickType_ShortCode'},
			{fieldLabel:'排列次序',name:'SickType_DispOrder'},
			//{fieldLabel:'His系统对应编码', labelWidth:100,name:'SickType_HisOrderCode'},
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Id:values.SickType_Id,
			CName:values.SickType_CName,
			ShortCode:values.SickType_ShortCode,
			DispOrder:values.SickType_DispOrder,
			//HisOrderCode:values.SickType_HisOrderCode,
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
	
  });