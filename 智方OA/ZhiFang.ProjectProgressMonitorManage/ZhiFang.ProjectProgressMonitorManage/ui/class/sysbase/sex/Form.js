/**
 * 性别表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.sex.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'性别信息',
    width:240,
	height:400,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBSexById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/SingleTableService.svc/ST_UDTO_AddBSex',
    /**修改服务地址*/
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBSexByField', 
	
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
	/**机构ID*/
	LabId:0,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var BSex_Name = me.getComponent('BSex_Name');
		BSex_Name.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								BSex_PinYinZiTou:value,
								BSex_Shortcode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						BSex_PinYinZiTou:"",
						BSex_Shortcode:""
					});
				}
			}
		});
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
		
		var items = [
			{fieldLabel:'性别名称',name:'BSex_Name',
				itemId:'BSex_Name',emptyText:'必填项',allowBlank:false},
			{fieldLabel:'性别简称',name:'BSex_SName'},
			{fieldLabel:'拼音字头',name:'BSex_PinYinZiTou'},
			{fieldLabel:'快捷码',name:'BSex_Shortcode'},
			{fieldLabel:'性别描述',height:85,labelAlign:'top',
				name:'BSex_Comment',xtype:'textarea'
			},
			{boxLabel:'是否使用',name:'BSex_IsUse',
				xtype:'checkbox',checked:true
			},
			{fieldLabel:'主键ID',name:'BSex_Id',hidden:true},
			{fieldLabel:'机构ID',name:'BSex_LabID',itemId:'BSex_LabID',
				hidden:true,value:me.LabId}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.BSex_Name,
			SName:values.BSex_SName,
			PinYinZiTou:values.BSex_PinYinZiTou,
			Shortcode:values.BSex_Shortcode,
			IsUse:values.BSex_IsUse ? true : false,
			Comment:values.BSex_Comment,
			LabID:values.BSex_LabID
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
		
		entity.entity.Id = values.BSex_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	}
});