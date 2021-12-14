/**
 * 首页-功能集合
 * @author Jcall
 * @version 2018-04-08
 */
Ext.define('Shell.class.sysbase.main.home.tab.App',{
    extend:'Ext.tab.Panel',
    title:'首页',
    margin:1,
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
		
		me.SystemInfo = {
			title:'系统介绍',
			xtype:'image',
			src:JShell.System.Path.UI + '/images/rea/home.png'
		};
		me.QModule = Ext.create('Shell.class.sysbase.main.home.tab.QModule',{
			title:'常用功能'
		});
		me.SystemHelp = {
			title:'帮助功能',
			xtype:'panel'
		};
		me.FlowX = {
			title:'X流程',
			xtype:'panel'
		};
		
		return [me.SystemInfo,me.QModule,me.SystemHelp,me.FlowX];
	}
});