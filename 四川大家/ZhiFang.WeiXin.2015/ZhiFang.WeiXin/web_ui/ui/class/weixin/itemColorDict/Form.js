/**
 * 颜色维护
 * @author GHX
 * @version 2021-01-29
 */
Ext.define('Shell.class.weixin.itemColorDict.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    
    title:'颜色信息',
    width:240,
	height:400,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorDictById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddItemColorDict',
    /**修改服务地址*/
    editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateItemColorDictByField', 
	
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
			{fieldLabel:'颜色名称',emptyText:'必填项',allowBlank:false,
				name:'ItemColorDict_ColorName',itemId:'ItemColorDict_ColorName'},
			{fieldLabel:'快捷码',name:'ItemColorDict_ColorValue'},
			{fieldLabel:'主键ID',name:'ItemColorDict_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			ColorName:values.ItemColorDict_ColorName,
			ColorValue:values.ItemColorDict_ColorValue
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
		
		entity.entity.Id = values.ItemColorDict_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化表单监听*/
	initFromListeners:function(){
		var me = this;
		
		var ItemColorDict_ColorName = me.getComponent('ItemColorDict_ColorName');
		ItemColorDict_ColorName.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
					});
				}
			}
		});
	}
});