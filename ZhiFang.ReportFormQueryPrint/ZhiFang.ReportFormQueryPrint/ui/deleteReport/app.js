Ext.Loader.setConfig({
    enabled: true,
    paths: { 'Shell': Shell.util.Path.uiPath }
});
var panel = null;
Ext.onReady(function () {
    Ext.QuickTips.init();//初始化后就会激活提示功能
    Ext.useShims = true;//防止PDF挡住下拉框
    Shell.util.Win.begin();//屏蔽快捷键

    //根据参数决定是否显示log信息
    var params = Shell.util.Path.getRequestParams(true);
    for (var i in params) {
        if (i.toLowerCase() === "SHOWLOG" && params[i] === "true") {
            Shell.util.Config.showLog = true;
        } else if (i.toLowerCase() === "SHOWLOGWIN" && params[i] === "true") {
            Shell.util.Config.showLogWin = true;
        }
    }

    panel = Ext.create('Shell.deleteReport.class.PrintApp', {
        header: false
    });

    //总体布局
    var viewport = Ext.create('Ext.container.Viewport', {
        layout: 'fit',
        padding: 2,
        items: [panel]
    });
});