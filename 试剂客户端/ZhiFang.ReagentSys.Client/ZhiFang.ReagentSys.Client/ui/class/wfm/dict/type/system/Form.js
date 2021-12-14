/**
 * 字典类型信息-开发商功能
 * @author Jcall
 * @version 2016-08-25
 */
Ext.define('Shell.class.wfm.dict.type.system.Form',{
	extend:'Shell.class.wfm.dict.type.Form',
    
    title:'字典类型信息-开发商功能',
    
    /**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = me.callParent(arguments);
		
		items.unshift({
			fieldLabel:'开发商码',name:'FDictType_DeveCode',
			emptyText:'必填项',allowBlank:false
		});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.callParent(arguments);
			
		entity.entity.DeveCode = values.FDictType_DeveCode;
		
		return entity;
	}
});