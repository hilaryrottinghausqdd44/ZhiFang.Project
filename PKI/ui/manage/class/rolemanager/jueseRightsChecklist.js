//模块操作选择列表
Ext.Loader.setConfig({enabled:true});

Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
Ext.ns('Ext.manage');
Ext.define('Ext.manage.rolemanager.jueseRightsChecklist', {
    extend:'Ext.zhifangux.CheckList',
    alias:['widget.jueseRightsChecklist'],
    layout:'fit',
    title:'模块选择列表',
    externalWhere:'',
    internalWhere:'',
    fields:[ 'RBACModuleOper_CName', 'RBACModuleOper_Comment', 'RBACModuleOper_FilterCondition', 'RBACModuleOper_Id', 'RBACModuleOper_DispOrder' ],
    border:true,
    autoSelect:true,
    autoScroll:true,
    multiSelect:true,
    primaryKey:'RBACModuleOper_Id',
    serverUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true&fields=RBACModuleOper_CName,RBACModuleOper_Comment,RBACModuleOper_FilterCondition,RBACModuleOper_Id,RBACModuleOper_DispOrder',
    sortableColumns:true,
    valueField:[ {
            xtype:'rownumberer',
            text:'序号',
            width:55,
            align:'center'
        }, {
            text:'名称',
            dataIndex:'RBACModuleOper_CName',
            width:120,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'描述',
            dataIndex:'RBACModuleOper_Comment',
            width:140,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'过滤条件',
            dataIndex:'RBACModuleOper_FilterCondition',
            width:160,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'RBACModuleOper_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'显示次序',
            dataIndex:'RBACModuleOper_DispOrder',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        } ],
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