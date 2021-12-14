Ext.define("Shell.class.weixin.dict.lab.BLabDoctor.Form",{
	extend:"Shell.ux.form.Panel",
	requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
    ],
    title:'医生',
	width:240,
	height:400,
	bodyPadding:10,
	/**主键字段*/
	PKField:'BLabDoctor_Id',
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBLabDoctorById?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBLabDoctor',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBLabDoctorByField',
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
    			d.value.BLabDoctor_Visible=d.value.BLabDoctor_Visible=='1' ? true:false;
    			d.value.BLabDoctor_UseFlag=d.value.BLabDoctor_UseFlag=='1' ? true:false;
    			m.getForm().setValues(d.value);
    		}
    	})
    },
    
    createItems:function(){
		var me = this;
		
		var items = [
		{
			fieldLabel:'编码',
			name:'BLabDoctor_LabDoctorNo',
			itemId:'BLabDoctor_LabDoctorNo',
			readOnly:true,
			locked:true
		},{
			fieldLabel:'实验室编码',
			name:'BLabDoctor_LabCode',
			itemId:'BLabDoctor_LabCode',
			readOnly:true,
			locked:true
		},
		{
			fieldLabel:'医生名称',
			name:'BLabDoctor_CName',
			itemId:'BLabDoctor_CName',
		},{
			fieldLabel:'简码',
			name:'BLabDoctor_ShortCode',
			itemId:'BLabDoctor_ShortCode',
		},{
			fieldLabel:'是否显示',
			name:'BLabDoctor_Visible',
			itemId:'BLabDoctor_Visible',
			xtype: 'uxBoolComboBox',
			value: true
		},{
			fieldLabel:'排列次序',
			name:'BLabDoctor_DispOrder',
			itemId:'BLabDoctor_DispOrder',
		},{
			fieldLabel:'是否启用',
			name:'BLabDoctor_UseFlag',
			itemId:'BLabDoctor_UseFlag',
			xtype: 'uxBoolComboBox',
			value: true
		},{
			fieldLabel:'标准编码',
			name:"BLabDoctor_StandCode",
			itemId:'BLabDoctor_StandCode',
		},{
			fieldLabel:'智方标准编码',
			name:"BLabDoctor_ZFStandCode",
			itemId:'BLabDoctor_ZFStandCode',
		},{
			fieldLabel:'主键ID',
			name:'BLabDoctor_Id',
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
			LabCode:values.BLabDoctor_LabCode,
			LabDoctorNo:values.BLabDoctor_LabDoctorNo,
			CName:values.BLabDoctor_CName,
			ShortCode:values.BLabDoctor_ShortCode,
			Visible:values.BLabDoctor_Visible ? 1:0,
			StandCode:values.BLabDoctor_StandCode,
			ZFStandCode:values.BLabDoctor_ZFStandCode,
			UseFlag:values.BLabDoctor_UseFlag ? 1:0,
			DispOrder:values.BLabDoctor_DispOrder,
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
		entity.entity.Id = values.BLabDoctor_Id;
		return entity;
	},
});
