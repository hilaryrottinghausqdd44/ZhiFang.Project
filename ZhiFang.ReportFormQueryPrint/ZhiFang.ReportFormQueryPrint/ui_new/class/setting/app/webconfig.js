/**
 * 查询台配置
 * @author jing
 * @version 2018-09-28
 */
Ext.define("Shell.class.setting.app.webconfig", {
    extend: 'Shell.ux.panel.AppPanel',
    //tabPosition: 'left',
    layout: {
        type: 'vbox',
        pack: 'center',
        align: 'center'
    },
    autoScroll: true,
    bodyStyle: 'overflow-x:hidden;overflow-y:scroll',
    appType:'',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },

    createItems:function () {
        var me = this;
        me.publicSetting = Ext.create("Shell.class.setting.webconfig.public.panel", {
            //style: 'margin-top:5px;margin-left:200px;margin-bottom:10px',
            //style: 'margin-left:3px',
            width: 1520,
            height: 725,
            colspan: 2,
            appType: me.appType,
            title: '程序基础配置'
        });
        return [me.publicSetting];
    }
});