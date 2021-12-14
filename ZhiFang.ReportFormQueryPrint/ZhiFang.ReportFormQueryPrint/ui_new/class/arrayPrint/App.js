/**
 * 代码新包迁移
 * @author Jing
 * @version 2018-09-20
*/
Ext.Loader.setConfig({
    enabled: true,
    paths: { 'Shell': Shell.util.Path.uiPath }
});
var panel = null;
Ext.onReady(function () {
    Ext.QuickTips.init();//初始化后就会激活提示功能
    Ext.useShims = true;//防止PDF挡住Exj原始组件内容
    Shell.util.Win.begin();//屏蔽快捷键
    panel = Ext.create('Shell.class.arrayPrint.PdfWins',  Ext.apply({
        itemId: 'PdfWin',
        header: false,
        height: 800,
        width: 800
    }));

    //总体布局
    var viewport = Ext.create('Ext.container.Viewport', {
        layout: 'fit',
        padding: 1,
        items: [panel]
    });
});