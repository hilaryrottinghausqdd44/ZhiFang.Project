Ext.define('Shell.reportSetting.class.PrintApp', {
    extend: 'Ext.tab.Panel',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },

    createItems:function () {
        var me = this;
        me.doctor = Ext.create("Shell.reportSetting.class.Doctor", {
            title: '医生页面设置',
            appType: "医生"
        });
        me.Lis = Ext.create("Shell.reportSetting.class.Lis", {
            title: '检验之星页面设置',
            appType: "检验之星"
        });
        me.Nurse = Ext.create("Shell.reportSetting.class.Nurse", {
            title: '护士页面设置',
            appType: "护士"
        });
        me.Odp = Ext.create("Shell.reportSetting.class.Odp", {
            title: '查询台页面设置',
            appType: "查询台"
        });
        me.Selfhelp = Ext.create("Shell.reportSetting.class.Selfhelp", {
            title: '自助打印页面设置',
            appType: "自助打印"
        });
        me.xmlConfig = Ext.create("Shell.reportSetting.class.xmlConfig.grid", {
            title: '模板配置信息',
            width: 1000,
            height: 500
           // appType: "自助打印"
        });
        return [me.doctor, me.Nurse, me.Odp, me.Lis, me.Selfhelp, me.xmlConfig];
    }
});