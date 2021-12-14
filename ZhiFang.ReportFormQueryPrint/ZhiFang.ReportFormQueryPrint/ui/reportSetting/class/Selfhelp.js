/**
 * 查询台配置
 * @author jing
 * @version 2018-09-28
 */
Ext.define("Shell.reportSetting.class.Selfhelp", {
    extend: 'Shell.ux.panel.AppPanel',
    layout: {
        type:'table',
        columns:1,
    },
    bodyStyle: 'overflow-x:hidden;overflow-y:scroll',
    appType:'',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },

    createItems:function () {
        var me = this;
        //me.grid = Ext.create("Shell.reportSetting.class.selfhelp.columns.grid", {
        //    style: 'margin-top:5px;margin-left:200px;margin-bottom:10px',
        //    width:1000,
        //    height:500,
        //    appType:me.appType,
        //    title: '显示报告单列设置'
        //});
        //me.selectGrid = Ext.create("Shell.reportSetting.class.selfhelp.select.grid", {
        //    style: 'margin-top:5px;margin-left:200px;margin-bottom:10px',
        //    width: 1000,
        //    height: 500,
        //    appType: me.appType,
        //    title: '设置查询报告单条件'
        //});
        me.publicSetting = Ext.create("Shell.reportSetting.class.selfhelp.public.panel", {
            style: 'margin-top:5px;margin-left:200px;margin-bottom:10px',
            width: 1000,
            height: 500,
            colspan: 2,
            appType: me.appType,
            title: '全局设置'
        });
        return [me.publicSetting];
    }
});