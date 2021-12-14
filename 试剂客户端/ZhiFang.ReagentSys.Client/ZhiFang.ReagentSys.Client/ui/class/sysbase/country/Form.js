/**
 * 国家信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.country.Form',{
	extend:'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'国家信息',
    width:570,
	height:320,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBCountryById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/SingleTableService.svc/ST_UDTO_AddBCountry',
    /**修改服务地址*/
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBCountryByField', 
    
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:60,
        labelAlign:'right'
    },
    /**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**启用表单状态初始化*/
	openFormType:true,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//拼音字头监听
		me.initPinYinZiTouListeners();
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(
			{fieldLabel:'国家名称',name:'BCountry_Name',itemId:'BCountry_Name',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'国家简称',name:'BCountry_SName',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'快捷码',name:'BCountry_Shortcode',itemId:'BCountry_Shortcode',
				emptyText:'必填项',allowBlank:false},	
			{fieldLabel:'拼音字头',name:'BCountry_PinYinZiTou',itemId:'BCountry_PinYinZiTou',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'备注',height:85,
				name:'BCountry_Comment',xtype:'textarea'},
			{boxLabel:'是否使用',name:'BCountry_IsUse',
				xtype:'checkbox',checked:true},
			{fieldLabel:'主键ID',name:'BCountry_Id',hidden:true}
		);
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.BCountry_Name,
			SName:values.BCountry_SName,
			Shortcode:values.BCountry_Shortcode,
			PinYinZiTou:values.BCountry_PinYinZiTou,
			IsUse:values.BCountry_IsUse ? true : false,
			Memo:values.BCountry_Memo
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
		
		entity.entity.Id = values.BCountry_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**拼音字头监听*/
	initPinYinZiTouListeners:function(){
		var me = this,
			BCountry_Name = me.getComponent('BCountry_Name');
			
		BCountry_Name.on({
			change:function(field,newValue,oldValue,eOpts){
				me.changePinYinZiTou(newValue);
			}
		});
	},
	changePinYinZiTou:function(data){
		var me = this,
			BCountry_PinYinZiTou = me.getComponent('BCountry_PinYinZiTou'),
			BCountry_Shortcode = me.getComponent('BCountry_Shortcode');
			
		if(data != ""){
			JShell.Action.delay(function(){
				JShell.System.getPinYinZiTou(data,function(value){
					me.getForm().setValues({
						BCountry_PinYinZiTou:value,
						BCountry_Shortcode:value
					});
				});
			},null,200);
		}else{
			me.getForm().setValues({
				BCountry_PinYinZiTou:"",
				BCountry_Shortcode:""
			});
		}
	}
});