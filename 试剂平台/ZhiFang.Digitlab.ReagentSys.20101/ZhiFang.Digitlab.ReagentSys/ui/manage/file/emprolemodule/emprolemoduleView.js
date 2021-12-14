Ext.onReady(function() {
    Ext.QuickTips.init();
    Ext.Loader.setConfig({
        enabled:true
    });
    Ext.Loader.setPath("Ext.manage.emprolemodule", getRootPath() + "/ui/manage/class/emprolemodule");
    var panel = Ext.create("Ext.manage.emprolemodule.emprolemoduleApp");
    Ext.create("Ext.container.Viewport", {
        layout:"fit",
        items:[ panel ]
    });
});