/***
 *  角色管理--角色权限分配:
 *  模块树,角色模块操作列表,确定表单
 */
 
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
Ext.Loader.setPath('Ext.manage.rolemanager', getRootPath() + '/ui/manage/class/rolemanager/jueseChecklist.js');
Ext.Loader.setPath('Ext.manage.rolemanager', getRootPath() + '/ui/manage/class/rolemanager/mokuaiTreeCheck.js');
Ext.ns('Ext.manage');
Ext.define('Ext.manage.rolemanager.mokuaiyucaozuoCheck', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.mokuaiyucaozuoCheck',
    title:'模块与操作选择',
    layout: 'border',
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    comNum:0,
    /**
     * 初始化
     */
    initComponent:function(){
        var me = this;
        me.addEvents('okClick');
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.manage',getRootPath()+'/ui/manage/class');
        
		Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
        Ext.Loader.setPath('Ext.manage.rolemanager', getRootPath() + '/ui/manage/class/rolemanager/mokuaiTreeCheck.js');
		Ext.Loader.setPath('Ext.manage.rolemanager', getRootPath() + '/ui/manage/class/rolemanager/jueseChecklist.js');
        me.items=me.createItems();
        me.callParent(arguments);
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        } 
    },
    createItems:function() {
        var me = this;
        var items=[ {
        width:300,
        xtype:'mokuaiTreeCheck',
        header:true,
        itemId:'mokuaiTreeCheck',
        title:'模块树',
        region:'west',
        split:true,
        collapsible:true,
        collapsed:false,
        border:false,
        layout: 'fit'
    },{
        header:false,
        itemId:'center',
        region:'center',
        layout: 'fit',
        dockedItems:[{
                xtype:'toolbar',
                dock:'bottom',//
                itemId:'dockedItemsbuttons', 
                items:[
                {
                    xtype:'button',itemId:'btnOK',iconCls:'build-button-save',
                    width:100,text:'确定选择',name:'btnOK',
                    listeners: {
                        click: {
                            element: 'el',
                            fn: function(){ 
                                me.fireEvent('okClick');
                            }
                        }   
                    }
                    
                }
               ]
            }],
        items:[
        {
        header:true,
        layout: 'fit',
        region:'center',
        xtype:'jueseChecklist',
        itemId:'jueseChecklist',
        title:'模块操作选择列表',
        split:true,
        autoScroll :true,
        collapsible:true,
        collapsed:false,
        border:false
        }
    ]  
    } ];
    return items;
    },
    getAppInfoFromServer:function(id, callback) {
        var me = this;
        var url = me.getAppInfoServerUrl + '?isPlanish=true&id=' + id;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            url:url,
            method:'GET',
            timeout:2e3,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var appInfo = '';
                    if (result.ResultDataValue && result.ResultDataValue != '') {
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if (Ext.typeOf(callback) == 'function') {
                        var obj = {
                            success:false,
                            ErrorInfo:'没有获取到应用组件信息!'
                        };
                        if (appInfo != '') {
                            obj = {
                                success:true,
                                appInfo:appInfo
                            };
                        }
                        callback(obj);
                    }
                } else {
                    if (Ext.typeOf(callback) == 'function') {
                        var obj = {
                            success:false,
                            ErrorInfo:'获取应用组件信息失败！错误信息' + result.ErrorInfo + '</b>'
                        };
                        callback(obj);
                    }
                }
            },
            failure:function(response, options) {
                if (Ext.typeOf(callback) == 'function') {
                    var obj = {
                        success:false,
                        ErrorInfo:'获取应用组件信息请求失败！'
                    };
                    callback(obj);
                }
            }
        });
    }
});