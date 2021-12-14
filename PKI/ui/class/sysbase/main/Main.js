/**
 * 首页
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.Main',{
    extend:'Ext.panel.Panel',
    title:'首页',
    /**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
//		me.html = 
//			'<html><body><iframe src="http://demo.zhifang.com.cn" ' +
//				'height="100%" width="100%" frameborder="0" ' +
//				'style="overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
//				'height:100%;width:100%;position:absolute;' +
//				'top:0px;left:0px;right:0px;bottom:0px">' +
//			'</iframe></body></html>';
		me.html = 
			'<html><body><iframe ' +
				'height="100%" width="100%" frameborder="0" ' +
				'style="background:#FFFFF0;overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
				'height:100%;width:100%;position:absolute;' +
				'top:0px;left:0px;right:0px;bottom:0px">' +
			'</iframe></body></html>';
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [];
		
		return items;
	}
});
	