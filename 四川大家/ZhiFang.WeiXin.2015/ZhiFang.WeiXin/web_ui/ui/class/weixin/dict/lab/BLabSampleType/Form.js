Ext.define("Shell.class.weixin.dict.lab.BLabSampleType.Form",{
	extend:"Shell.ux.form.Panel",
	requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
    ],
    title:'样本类型',
	width:240,
	height:400,
	bodyPadding:10,
	/**主键字段*/
	PKField:'BLabSampleType_Id',
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBLabSampleTypeById?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBLabSampleType',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBLabSampleTypeByField',
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
    			d.value.BLabSampleType_Visible=d.value.BLabSampleType_Visible=='1' ? true:false;
    			d.value.BLabSampleType_UseFlag=d.value.BLabSampleType_UseFlag=='1' ? true:false;
    			m.getForm().setValues(d.value);
    		}
    	})
    },
    
    createItems:function(){
		var me = this;
		
		var items = [
		{
			fieldLabel:'编码',
			name:'BLabSampleType_LabSampleTypeNo',
			itemId:'BLabSampleType_LabSampleTypeNo',
			readOnly:true,
			locked:true
		},{
			fieldLabel:'实验室编码',
			name:'BLabSampleType_LabCode',
			itemId:'BLabSampleType_LabCode',
			readOnly:true,
			locked:true
		},
		{
			fieldLabel:'样本类型名称',
			name:'BLabSampleType_CName',
			itemId:'BLabSampleType_CName',
		},{
			fieldLabel:'简码',
			name:'BLabSampleType_ShortCode',
			itemId:'BLabSampleType_ShortCode',
		},{
			fieldLabel:'是否显示',
			name:'BLabSampleType_Visible',
			itemId:'BLabSampleType_Visible',
			xtype: 'uxBoolComboBox',
			value: true
		},{
			fieldLabel:'排列次序',
			name:'BLabSampleType_DispOrder',
			itemId:'BLabSampleType_DispOrder',
		},{
			fieldLabel:'是否启用',
			name:'BLabSampleType_UseFlag',
			itemId:'BLabSampleType_UseFlag',
			xtype: 'uxBoolComboBox',
			value: true
		},{
			fieldLabel:'标准编码',
			name:"BLabSampleType_StandCode",
			itemId:'BLabSampleType_StandCode',
		},{
			fieldLabel:'智方标准编码',
			name:"BLabSampleType_ZFStandCode",
			itemId:'BLabSampleType_ZFStandCode',
		},{
			fieldLabel:'住院码',
			name:"BLabSampleType_Code1",
			itemId:'BLabSampleType_Code1',
		},{
			fieldLabel:'门准编码',
			name:"BLabSampleType_Code2",
			itemId:'BLabSampleType_Code2',
		},{
			fieldLabel:'体检编码',
			name:"BLabSampleType_Code3",
			itemId:'BLabSampleType_Code3',
		},{
			fieldLabel:'主键ID',
			name:'BLabSampleType_Id',
			hidden:true
		}
		];
		
		return items;
	},
	
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			LabCode:values.BLabSampleType_LabCode,
			LabSampleTypeNo:values.BLabSampleType_LabSampleTypeNo,
			CName:values.BLabSampleType_CName,
			ShortCode:values.BLabSampleType_ShortCode,
			Visible:values.BLabSampleType_Visible? 1:0,
			DispOrder:values.BLabSampleType_DispOrder,
			//HisOrderCode:values.BLabSampleType_HisOrderCode,
			StandCode:values.BLabSampleType_StandCode,
			ZFStandCode:values.BLabSampleType_ZFStandCode,
			Code1:values.BLabSampleType_Code1,
			Code2:values.BLabSampleType_Code2,
			Code3:values.BLabSampleType_Code3,
			UseFlag:values.BLabSampleType_UseFlag ? 1 : 0,
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
		entity.entity.Id = values.BLabSampleType_Id;
		return entity;
	},
});
