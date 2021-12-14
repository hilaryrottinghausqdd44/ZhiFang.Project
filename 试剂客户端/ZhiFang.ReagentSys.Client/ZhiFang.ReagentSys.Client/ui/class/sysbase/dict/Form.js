/**
 * 字典信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.dict.Form',{
	extend:'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'字典信息',
    width:240,
	height:320,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBDictById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/SingleTableService.svc/ST_UDTO_AddBDict',
    /**修改服务地址*/
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBDictByField', 
    
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
	
	/**字典类型ID*/
	DictTypeId:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(
			{fieldLabel:'名称',name:'BDict_CName',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'次序',name:'BDict_DispOrder',
				xtype:'numberfield',value:0,
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'备注',height:85,
				name:'BDict_Memo',xtype:'textarea'},
			{boxLabel:'是否使用',name:'BDict_IsUse',
				xtype:'checkbox',checked:true},
			{fieldLabel:'主键ID',name:'BDict_Id',hidden:true}
		);
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.BDict_CName,
			DispOrder:values.BDict_DispOrder,
			IsUse:values.BDict_IsUse ? true : false,
			Memo:values.BDict_Memo,
			BDictType:{Id:me.DictTypeId,DataTimeStamp:[0,0,0,0,0,0,0,0]}
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
		delete entity.entity.BDictType;
		
		entity.entity.Id = values.BDict_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	}
});