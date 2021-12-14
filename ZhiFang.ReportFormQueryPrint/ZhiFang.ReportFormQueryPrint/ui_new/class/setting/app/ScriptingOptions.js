/**
 * 查询台配置
 * @author jing
 * @version 2018-09-28
 */
Ext.define("Shell.class.setting.app.ScriptingOptions", {
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
        me.update = Ext.create("Shell.class.setting.ScriptingOptions.update.app", {
           // style: 'margin-top:5px;margin-left:200px;margin-bottom:10px',
            style: 'margin-left:3px',
            width:1000,
            height:500,
            title: '脚本升级'
        });
        return [ me.update];
    }
});