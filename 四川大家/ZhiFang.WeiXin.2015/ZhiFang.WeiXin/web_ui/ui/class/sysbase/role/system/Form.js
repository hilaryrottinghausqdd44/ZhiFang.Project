/**
 * 角色表单-开发商功能
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.role.system.Form',{
    extend:'Shell.class.sysbase.role.Form',
    title:'角色信息-开发商功能',
    
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = me.callParent(arguments);
		
		items.unshift({
			fieldLabel:'开发商码',name:'RBACRole_DeveCode',
			emptyText:'必填项',allowBlank:false
		});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.callParent(arguments);
			
		entity.entity.DeveCode = values.RBACRole_DeveCode;
		
		return entity;
	}
});