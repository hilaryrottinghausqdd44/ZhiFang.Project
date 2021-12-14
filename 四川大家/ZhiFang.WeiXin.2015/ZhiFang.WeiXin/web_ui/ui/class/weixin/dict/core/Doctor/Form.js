Ext.define("Shell.class.weixin.dict.core.Doctor.Form",{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
    ],
    title:'医生',
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
	PKField:'',
	/**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchDoctorById?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddDoctor',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateDoctorByField',
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
    			if(d.value.Doctor_Visible=='1'){
    				d.value.Doctor_Visible=true;
    			}else{
    				d.value.Doctor_Visible=false;
    			}
    			m.getForm().setValues(d.value);
    		}
    	})
    },
    
	createItems:function(){
		var me =this;
		var items=[
		{
			fieldLabel:'编号',
			name:'Doctor_Id',
			itemId:'Doctor_Id',
			readOnly:true,
			locked:true
		},{
			fieldLabel:'医生名称',
			name:'Doctor_CName',
			itemId:'Doctor_CName',
		},{
			fieldLabel:'输入代码',
			name:'Doctor_ShortCode',
			itemId:'Doctor_ShortCode',
		},{
			fieldLabel:'是否显示',
			name:'Doctor_Visible',
			itemId:'Doctor_Visible',
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
			Id:value.Doctor_Id,
			CName:value.Doctor_CName,
			ShortCode:value.Doctor_ShortCode,
			Visible:value.Doctor_Visible ? 1:0,
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
