/**
 * View
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.interface.one.Viewport', {
	extend: 'Ext.container.Viewport',
	layout: 'fit',
	id:'SystemViewport',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		window.VIEWPORT = me;
		//自动登录
		setTimeout(function(){
			me.fireEvent('login',me);
		},100);
		
	},
	
	initComponent: function() {
		var me = this;
		me.addEvents('login');
		me.items = [];
		me.callParent(arguments);
	}
});