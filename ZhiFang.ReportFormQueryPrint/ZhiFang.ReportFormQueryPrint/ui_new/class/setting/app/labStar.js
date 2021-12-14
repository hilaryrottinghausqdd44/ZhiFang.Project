/**
 * 技师站调用页面配置
 * @author guohx	
 * @version 2020-04-20
 */
Ext.define("Shell.class.setting.app.labStar", {
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
        me.grid = Ext.create("Shell.class.setting.labStar.columns.Grid", {
            style: 'margin-left:3px',
            width:1000,
            height:500,
            appType:me.appType,
            title: '显示报告单列设置'
        });
        return [me.grid];
    }
});