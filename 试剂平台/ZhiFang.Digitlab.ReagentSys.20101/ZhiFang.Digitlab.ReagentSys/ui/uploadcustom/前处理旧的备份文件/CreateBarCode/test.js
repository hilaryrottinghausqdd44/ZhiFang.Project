/**
 * Created with JetBrains WebStorm.
 * User: 123
 * Date: 13-5-27
 * Time: 下午1:53
 * To change this template use File | Settings | File Templates.
 */
Ext.onReady(function () {
    Ext.QuickTips.init(); //初始化后就会激活提示功能
    Ext.Loader.setConfig({ enabled: true }); //允许动态加载


    //总体布局
    var viewPort = Ext.create("Ext.container.Viewport", {
        layout:'fit',
        items: [{
            xtype: 'createbarcode'
            /*  itemId: 'appSearch'*/
        }]
    });
});

