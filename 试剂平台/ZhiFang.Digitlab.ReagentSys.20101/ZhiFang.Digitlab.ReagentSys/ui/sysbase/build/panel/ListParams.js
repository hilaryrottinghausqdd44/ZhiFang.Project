/**
 * 列表属性设置面板
 * @author Jcall
 * @version 2014-08-21
 */
Ext.define('Shell.sysbase.build.panel.ListParams',{
	extend:'Shell.sysbase.build.panel.ParamsPanel',
	
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		return items;
	}
});