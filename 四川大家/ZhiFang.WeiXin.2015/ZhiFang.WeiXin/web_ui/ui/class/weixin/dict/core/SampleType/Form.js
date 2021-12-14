Ext.define("Shell.class.weixin.dict.core.SampleType.Form",{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
    ],
    title:'样本类型',
	width:240,
	height:400,
	bodyPadding:10,
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'edit',
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**主键字段*/
	PKField:'SampleType_Id',
	/**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchSampleTypeById?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddSampleType',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateSampleTypeByField',
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
  		//	console.log(d.value.SampleType_Visible);
//  			console.log(m);
    			if(d.value.SampleType_Visible=='1'){
    				d.value.SampleType_Visible=true;
    			}else{
    				d.value.SampleType_Visible=false;
    			}
    			m.getForm().setValues(d.value);
    			//m.store=d;
    		}
    	})
    },
    
	createItems:function(){
		var me =this;
		var items=[
		{
			fieldLabel:'编号',
			name:'SampleType_Id',
			itemId:'SampleType_Id',
			readOnly:true,
			locked:true
		},{
			fieldLabel:'样本类型名称',
			name:'SampleType_CName',
			itemId:'SampleType_CName',
		},{
			fieldLabel:'输入代码',
			name:'SampleType_ShortCode',
			itemId:'SampleType_ShortCode',
		},{
			fieldLabel:'排列次序',
			name:'SampleType_DispOrder',
			itemId:'SampleType_DispOrder',
		},{
			fieldLabel:'是否显示',
			name:'SampleType_Visible',
			itemId:'SampleType_Visible',
			xtype: 'uxBoolComboBox',
		},
		];
		return items;
	},
	
	
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me =this;
		var value =me.getForm().getValues();
		var entity={
			Id:value.SampleType_Id,
			CName:value.SampleType_CName,
			ShortCode:value.SampleType_ShortCode,
			DispOrder:value.SampleType_DispOrder,
			Visible:value.SampleType_Visible ? 1:0,
		}
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		
		for(var i in fields){
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');	
		return entity;
	},
});
