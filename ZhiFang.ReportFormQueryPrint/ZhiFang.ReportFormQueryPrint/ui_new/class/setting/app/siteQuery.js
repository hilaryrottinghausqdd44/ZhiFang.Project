/**
 * 护士配置
 * @author jing
 * @version 2018-09-28
 */
Ext.define("Shell.class.setting.app.siteQuery", {
    extend: 'Ext.tab.Panel',
    tabPosition: 'left',
    cls:'lisTabPanelPostition',//ie字体显示不全问题
    //layout: {
    //    type: 'vbox',
    //    pack: 'center',
    //    align: 'center'
    //},
    //autoScroll: true,
    bodyStyle: 'overflow-x:hidden;overflow-y:scroll',
    appType:'',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },

    createItems:function () {
        var me = this;
        me.grid = Ext.create("Shell.class.setting.siteQuery.columns.grid", {
           // style: 'margin-top:5px;margin-left:200px;margin-bottom:10px',
            style: 'margin-left:3px',
            width: 1000,
            height:500,
            appType:me.appType,
            title: '显示报告单列设置'
        });
        me.selectGrid = Ext.create("Shell.class.setting.siteQuery.select.grid", {
           // style: 'margin-top:5px;margin-left:200px;margin-bottom:10px',
            style: 'margin-left:3px',
            width: 1000,
            height: 500,
            appType: me.appType,
            title: '设置查询报告单条件'
        });
        me.empDeptLinksGrid = Ext.create("Shell.class.setting.siteQuery.empDeptLinks.panel", {
           // style: 'margin-top:5px;margin-left:200px;margin-bottom:10px',
            style: 'margin-left:3px',
            width: 1000,
            height: 500,
            appType: me.appType,
            title: '用户对应科室设置'
        });
        me.publicSetting = Ext.create("Shell.class.setting.siteQuery.public.panel", {
          // style: 'margin-top:5px;margin-left:200px;margin-bottom:10px',
            style: 'margin-left:3px',
            width: 1000,
            height: 500,
            colspan: 2,
            appType: me.appType,
            title: '全局设置'
        });
        return [me.grid, me.selectGrid,me.empDeptLinksGrid ,me.publicSetting];
    }
});