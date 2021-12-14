/**
 * 实验室就诊类型字典
 * @author guozhaojing
 * @version 2018-03-28
 */
Ext.define("Shell.class.weixin.dict.lab.BLabSickType.Form",{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
    ],
    title:'实验室就诊类型字典',
    width:240,
	height:400,
	bodyPadding:10,
	
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBLabSickTypeById?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBLabSickType',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBLabSickTypeByField',
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
    
    afterRender:function(){
    	var me =this;
    	me.callParent(arguments);
    	me.on({
    		load:function(m,d){
    			d.value.BLabSickType_UseFlag=d.value.BLabSickType_UseFlag=='1'?true:false;
    			me.getForm().setValues(d.value);
    		}
    	});
    },
    
    createItems:function(){
		var me = this;
		
		var items = [
			{fieldLabel:'实验室编码',readOnly:true,locked:true,name:'BLabSickType_LabCode',itemId:'BLabSickType_LabCode'},
			{fieldLabel:'编码',readOnly:true,locked:true,name:'BLabSickType_LabSickTypeNo',itemId:'BLabSickType_LabSickTypeNo'},
			{fieldLabel:'名称',name:'BLabSickType_CName',itemId:'BLabSickType_CName'},
			{fieldLabel:'简码',name:'BLabSickType_ShortCode',itemId:'BLabSickType_ShortCode'},
			{fieldLabel:'排列次序',name:'BLabSickType_DispOrder',itemId:'BLabSickType_DispOrder'},
			//{fieldLabel:'His系统对应编码', labelWidth:100,name:'BLabSickType_HisOrderCode',itemId:'BLabSickType_HisOrderCode'},
			{fieldLabel:'标准编码',name:'BLabSickType_StandCode',itemId:'BLabSickType_StandCode'},
			{fieldLabel:'智方标准编码',name:'BLabSickType_ZFStandCode',itemId:'BLabSickType_ZFStandCode'},
			{fieldLabel:'是否启用',name:'BLabSickType_UseFlag',itemId:'BLabSickType_UseFlag',
				xtype: 'uxBoolComboBox',value: true
			},
			{fieldLabel:'主键ID',name:'BLabSickType_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			LabCode:values.BLabSickType_LabCode,
			LabSickTypeNo:values.BLabSickType_LabSickTypeNo,
			CName:values.BLabSickType_CName,
			ShortCode:values.BLabSickType_ShortCode,
			DispOrder:values.BLabSickType_DispOrder,
			//HisOrderCode:values.BLabSickType_HisOrderCode,
			StandCode:values.BLabSickType_StandCode,
			ZFStandCode:values.BLabSickType_ZFStandCode,
			UseFlag:values.BLabSickType_UseFlag ? 1 : 0,
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
		
		entity.entity.Id = values.BLabSickType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	}
})
