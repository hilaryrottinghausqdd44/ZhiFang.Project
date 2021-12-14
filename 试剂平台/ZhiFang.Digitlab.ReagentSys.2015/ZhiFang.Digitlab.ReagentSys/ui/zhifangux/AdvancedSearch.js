//高级查询
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.AdvancedSearch', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.advancedsearch',

    width:600,
    height:580,
    layout:'absolute',
    frame:true,
    padding:0,
    bodyCls:'bg-white',
    DataServerUrl:'',//查询服务地址
    initComponent:function(){
        var me = this;
        
        //添加事件，别的地方就能对这个事件进行监听
        this.addEvents('appClick');
        this.addEvents('appMouseover');
        this.addEvents('appMouseout');
        
        me.listeners = {};
        
        this.callParent(arguments);
    },
    afterRender: function() {
        var me = this;
        //提示

        me.callParent(arguments);
    }
});