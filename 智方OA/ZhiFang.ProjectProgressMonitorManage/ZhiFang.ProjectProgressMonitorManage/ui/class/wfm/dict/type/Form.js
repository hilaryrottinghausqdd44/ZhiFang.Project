/**
 * 字典类型信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.dict.type.Form',{
	extend:'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'字典类型信息',
    width:240,
	height:320,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictTypeById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPDictType',
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePDictTypeByField', 
    
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:30,
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
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(
			{fieldLabel:'编码',name:'PDictType_DictTypeCode',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'名称',name:'PDictType_CName',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'次序',name:'PDictType_DispOrder',
				xtype:'numberfield',value:0,
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'备注',height:85,
				name:'PDictType_Memo',xtype:'textarea'},
			{boxLabel:'是否使用',name:'PDictType_IsUse',
				xtype:'checkbox',checked:true},
			{fieldLabel:'主键ID',name:'PDictType_Id',hidden:true}
		);
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			DictTypeCode:values.PDictType_DictTypeCode,
			CName:values.PDictType_CName,
			DispOrder:values.PDictType_DispOrder,
			IsUse:values.PDictType_IsUse ? true : false,
			Memo:values.PDictType_Memo
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
		
		entity.entity.Id = values.PDictType_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	}
});