/***
 * 选择列表和按钮
 */

Ext.ns('Ext.manage');
Ext.define('Ext.manage.departmentstaff.xuanzexuangongApp', {
    extend: 'Ext.panel.Panel',
    panelType: 'Ext.panel.Panel',
    alias: 'widget.xuanzexuangongApp',
    title: '',
    layout: 'border',
    comNum: 0,
    afterRender: function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    /**
	   * 初始化
	   */
    initComponent: function() {
        var me = this;
        me.addEvents('btnOKClick');
        me.addEvents('closeClick');
        me.items = me.createItems();
        me.callParent(arguments);
    },

    createItems: function() {
        var me = this;
        Ext.Loader.setConfig({
            enabled: true
        });
        Ext.Loader.setPath("Ext.manage.departmentstaff.xuanzeyuangongList", getRootPath() + "/ui/manage/class/departmentstaff/xuanzeyuangongList.js");
        //选择列表
        var xuanzeyuangongList = Ext.create("Ext.manage.departmentstaff.xuanzeyuangongList", {
            itemId: 'xuanzeyuangongList',
            header: false,
            title:'选择员工',
            region: 'center'
        });
        //确认选择
        var btnForm = Ext.create("Ext.form.Panel", {
            itemId: 'btnForm',
            header: false,
            title:'按钮',
            height:32,
            border:true,
            region: 'south',
            items : [{
	            xtype: 'button',
	            text: '确认',
	            itemId: 'btnOK',
	            x: 208,
	            y: 3,
	            width: 80,
	            height: 22,
	            handler:function(but, e) {
                    me.fireEvent('btnOKClick');
                }
	        },
	        {
	            xtype: 'button',
	            text: '关闭',
	            itemId: 'btnClose',
	            x: 250,
	            y: 3,
	            width: 80,
	            height: 22,
	            handler:function(but, e) {
                    me.fireEvent('closeClick');
                }
	        }]
        });
        var appInfos = [xuanzeyuangongList,btnForm];
        return appInfos;
    }

});