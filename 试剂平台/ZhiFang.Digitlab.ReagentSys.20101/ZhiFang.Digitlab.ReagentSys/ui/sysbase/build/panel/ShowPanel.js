/**
 * 效果显示面板
 * @author Jcall
 * @version 2014-08-21
 */
Ext.define('Shell.sysbase.build.panel.ShowPanel',{
	extend:'Shell.ux.panel.Panel',
	
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	/**替换内容*/
	replaceContent:function(app){
		var me = this,
			type = Ext.typeOf(app);
		
		if(type != 'object'){
			me.showWarning('Shell.sysbase.build.panel.ShowPanel的replaceContent方法接收的参数必须是一个应用对象!');
			return;
		}
		
		me.removeAll();
		me.add(app);
	}
});