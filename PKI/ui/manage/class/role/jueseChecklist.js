//角色列表
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
Ext.define('jueseChecklist', {
    extend:'Ext.zhifangux.CheckList',
    alias:['widget.jueseChecklist'],
    layout:'fit',
    width:600,
    title:'角色选择',
    externalWhere:'',
    internalWhere:'',
    border:true,
    autoSelect:true,
    autoScroll:true,
    multiSelect:true,
    primaryKey:'RBACRole_Id',
    serverUrl:getRootPath()+ '/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL',
    sortableColumns:true,
    valueField:[{
        text:'主键',
        dataIndex:'RBACRole_Id',
        width:181,
        hidden:true,
        align:'left'
    }, {
        text:'代码',
        dataIndex:'RBACRole_UseCode',
        width:87,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'left'
    } ,{
        text:'名称',
        dataIndex:'RBACRole_CName',
        width:126,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'left'
    },  {
        text:'描述',
        dataIndex:'RBACRole_Comment',
        width:181,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'left'
    },{
        text:'显示次序',
        dataIndex:'RBACRole_DispOrder',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    }],
    internalDockedItem :[ {
        xtype:'toolbar',
        itemId:'buttonstoolbar',
        dock:'top',
        items:[ {
            itemId:'refresh',
            text:'更新',
            iconCls:'build-button-refresh'
        }, '->', {
            xtype:'textfield',
            itemId:'searchText',
            width:160,
            emptyText:'代码/名称/描述'
        }, {
            xtype:'button',
            text:'查询',
            itemId:'searchbtn',
            iconCls:'search-img-16 ',
            tooltip:'代码/名称/描述'
        } ]
    } ],
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    }
});