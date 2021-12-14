/**
 * 所有页面配置
 * @author jing
 * @version 2018-09-17
 */
Ext.define('Shell.class.setting.app.App', {
    extend: 'Ext.tab.Panel',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },

    createItems:function () {
        var me = this;
        me.doctor = Ext.create("Shell.class.setting.app.Doctor", {
            title: '医生页面设置',
            appType: "doctor"
        });
        me.Lis = Ext.create("Shell.class.setting.app.Lis", {
            title: '技师站页面设置',
            appType: "lis"
        });
        me.Nurse = Ext.create("Shell.class.setting.app.Nurse", {
            title: '护士页面设置',
            appType: "nurse"
        });
        me.Odp = Ext.create("Shell.class.setting.app.Odp", {
            title: '查询台页面设置',
            appType: "odp"
        });
        me.Selfhelp = Ext.create("Shell.class.setting.app.Selfhelp", {
            title: '自助打印页面设置',
            appType: "selfhelp",
            bodyStyle: 'overflow-x:hidden; overflow-y:hidden',
            autoScroll:false
        });
         me.siteQuery = Ext.create("Shell.class.setting.app.siteQuery", {
            title: '站点查询设置',
            appType: "siteQuery"
        });
        me.historyAndBackups = Ext.create("Shell.class.setting.app.historyAndBackups", {
            title: '分库查询设置',
            appType: "historyAndBackups"
        });
        me.focusPrint = Ext.create("Shell.class.setting.app.focusPrint", {
            title: '集中打印设置',
            appType: "focusPrint"
        });
        me.CheckReportRequest = Ext.create("Shell.class.setting.app.CheckReportRequest", {
            title: '检验前后查询设置',
            appType: "CheckReportRequest"
        });
        me.labStar = Ext.create("Shell.class.setting.app.labStar", {
            title: 'LabStar调用设置',
            appType: "labStar"
        });
        me.xmlConfig = Ext.create("Shell.class.setting.xmlConfig.grid", {
            title: '模板配置信息',
            width: 1000,
            height: 500
        });
        me.printSetting = Ext.create("Shell.class.setting.PrintSetting.class.App", {
            title: '小组打印模板设置'
        });
        me.clearPrintCount = Ext.create("Shell.class.setting.clearPrintCount.App", {
            title: '清除打印次数'
        });
        me.webconfig = Ext.create("Shell.class.setting.app.webconfig", {
            title: '程序基础配置',
            appType: "webconfig",
            bodyStyle: 'overflow-x:hidden; overflow-y:hidden',
            autoScroll:false
        });
        me.ScriptingOptions = Ext.create("Shell.class.setting.ScriptingOptions.grid", {
            title: '程序升级脚本'
        });
        return [me.doctor, me.Nurse, me.Odp, me.Lis, me.Selfhelp,me.siteQuery,me.historyAndBackups,me.focusPrint,me.CheckReportRequest,me.labStar, me.xmlConfig,me.printSetting,me.clearPrintCount,me.webconfig,me.ScriptingOptions];
    }
});