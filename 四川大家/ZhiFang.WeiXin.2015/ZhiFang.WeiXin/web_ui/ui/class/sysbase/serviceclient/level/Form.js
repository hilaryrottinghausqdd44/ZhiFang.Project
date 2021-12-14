/**
 * 平台客户级别表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.serviceclient.level.Form',{
    extend:'Shell.ux.form.Panel',
    
    title:'平台客户级别表单',
    width:240,
	height:400,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchSServiceClientlevelById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_AddSServiceClientlevel',
    /**修改服务地址*/
    editUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateSServiceClientlevelByField', 
	
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
		
		var SServiceClientlevel_Name = me.getComponent('SServiceClientlevel_Name');
		SServiceClientlevel_Name.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								SServiceClientlevel_PinYinZiTou:value,
								SServiceClientlevel_Shortcode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						SServiceClientlevel_PinYinZiTou:"",
						SServiceClientlevel_Shortcode:""
					});
				}
			}
		});
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
		
		var items = [
			{fieldLabel:'显示次序',name:'SServiceClientlevel_DispOrder',
				xtype:'numberfield',value:0,allowBlank:false},
			{fieldLabel:'级别名称',name:'SServiceClientlevel_Name',
				itemId:'SServiceClientlevel_Name',emptyText:'必填项',allowBlank:false},
			{fieldLabel:'级别简称',name:'SServiceClientlevel_SName'},
			{fieldLabel:'级别编码',name:'SServiceClientlevel_Code'},
			{fieldLabel:'拼音字头',name:'SServiceClientlevel_PinYinZiTou'},
			{fieldLabel:'快捷码',name:'SServiceClientlevel_Shortcode'},
			{fieldLabel:'描述',height:85,labelAlign:'top',
				name:'SServiceClientlevel_Comment',xtype:'textarea'
			},
			{boxLabel:'是否使用',name:'SServiceClientlevel_IsUse',
				xtype:'checkbox',checked:true
			},
			{fieldLabel:'主键ID',name:'SServiceClientlevel_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.SServiceClientlevel_Name,
			SName:values.SServiceClientlevel_SName,
			Code:values.SServiceClientlevel_Code,
			PinYinZiTou:values.SServiceClientlevel_PinYinZiTou,
			Shortcode:values.SServiceClientlevel_Shortcode,
			DispOrder:values.SServiceClientlevel_DispOrder,
			IsUse:values.SServiceClientlevel_IsUse ? true : false,
			Comment:values.SServiceClientlevel_Comment
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
		
		entity.entity.Id = values.SServiceClientlevel_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	}
});