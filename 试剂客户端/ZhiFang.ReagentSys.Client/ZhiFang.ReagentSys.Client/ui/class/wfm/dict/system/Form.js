/**
 * 字典信息-开发商功能
 * @author Jcall
 * @version 2016-08-25
 */
Ext.define('Shell.class.wfm.dict.system.Form',{
	extend:'Shell.class.wfm.dict.Form',
    
    title:'字典信息-开发商功能',
    
    /**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = me.callParent(arguments);
		
		items.unshift({
			fieldLabel:'开发商码',name:'FDict_DeveCode',
			emptyText:'必填项',allowBlank:false
		});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.callParent(arguments);
			
		entity.entity.DeveCode = values.FDict_DeveCode;
		
		return entity;
	}
});