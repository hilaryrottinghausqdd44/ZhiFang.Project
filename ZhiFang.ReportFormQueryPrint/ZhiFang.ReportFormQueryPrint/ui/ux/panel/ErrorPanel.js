/**
 * 错误面板类
 * @author Jcall
 * @version 2014-08-18
 */
Ext.define('Shell.ux.panel.ErrorPanel',{
	extend:'Shell.ux.panel.Panel',
	alias:'widget.uxerrorpanel',
	
	/**重写初始化面板属性*/
	initComponent:function(){
		var me = this;
		
		me.html = me.html || "<b style='color:red;'>错误页面</b>";
		
		me.callParent(arguments);
	}
});