/**
 * 微信消费采样
 * @author GHX
 * @version 2021-01-05
*/
Ext.Loader.setConfig({
    enabled: true,
    paths: { 'Shell': JShell.System.Path.UI }
});
var panel = null;
Ext.onReady(function () {
    Ext.QuickTips.init();//初始化后就会激活提示功能
    Ext.useShims = true;//防止PDF挡住Exj原始组件内容
    //屏蔽右键菜单
    Ext.getDoc().on("contextmenu",function(e){e.stopEvent();});
	
    panel = Ext.create('Shell.class.weixin.sampling.basic.App', {
        title:'',header: false
    });
    //总体布局
    var viewport = Ext.create('Ext.container.Viewport', {
        layout: 'fit',
        padding: 1,
        //header: false,
        items: [panel]
    });
});
