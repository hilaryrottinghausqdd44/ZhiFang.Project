/**
 * 首页
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.home.Home',{
    extend:'Ext.panel.Panel',
    title:'首页',
    
    bodyPadding:1,
    layout:'fit',
    
    /**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [{
			xtype:'image',
			src:JShell.System.Path.UI + '/images/rea/home.png'
		}];
		
		return items;
	}
});