/**
 * 医生页面配置
 * @author ghx
 * @version 2020-02-07
 */
Ext.define("Shell.class.setting.app.focusPrint", {
    extend: 'Ext.tab.Panel',
    tabPosition:'left',
    cls:'lisTabPanelPostition',//ie字体显示不全问题
    //layout: {
    //    type: 'vbox',
    //    pack: 'center',
    //    align:'center'
    //},
    //autoScroll: true,
    appType: '',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },

    createItems:function () {
        var me = this;
        me.selectGrid = Ext.create("Shell.class.setting.focusPrint.select.Grid", {
           // style: 'margin-top:5px;margin-left:200px;margin-bottom:10px',
            style: 'margin-left:3px',
            width: 1000,
            height: 500,
            appType: me.appType,
            title: '设置查询报告单条件'
        });
        return [ me.selectGrid];
    }
});