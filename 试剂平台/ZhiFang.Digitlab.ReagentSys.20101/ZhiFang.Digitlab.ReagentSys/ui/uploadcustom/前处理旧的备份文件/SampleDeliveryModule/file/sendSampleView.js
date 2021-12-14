Ext.onReady(function(){ 
    Ext.QuickTips.init();//初始化后就会激活提示功能
    Ext.Loader.setConfig({enabled: true});//允许动态加载
    Ext.Loader.setPath('Ext.zhifangux.BasicTabPanel', getRootPath() + '/ui/zhifangux/BasicTabPanel.js');
    Ext.Loader.setPath('Ext.mept', getRootPath() +'ui/mept/SampleDeliveryModule/class');
    var panel = Ext.create('Ext.mept.SampleDeliveryModule.sendSampleApp');
    
    //总体布局
    Ext.create('Ext.container.Viewport',{
        layout:'fit',
        items:[panel]
    });
});