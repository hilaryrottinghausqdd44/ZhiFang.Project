//部门直属员工
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.zhifangux', getRootPath() + '/ui/zhifangux');
Ext.define('HREmployeelist', {
    extend:'Ext.zhifangux.CheckList',
    alias:['widget.HREmployeelist'],
    requires: ['Ext.zhifangux.CheckList'],
    layout:'fit',
    width:600,
    title:'',
    externalWhere:'',
    internalWhere:'',
    border:true,
    isload:false,
    isbutton:false,
    autoSelect:true,
    autoScroll:true,
    multiSelect:true,
    primaryKey:'HREmployee_Id',
//    serverUrl:getRootPath()+ '/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID',
    serverUrl:getRootPath()+ '/RBACService.svc/RBAC_UDTO_GetHREmployeeNoRBACRoleIDByHRDeptID',

    sortableColumns:true,
    valueField:[{
        text:'员工名称',
        dataIndex:'HREmployee_CName',
        width:72,
        sortable:true,
        hidden:false,
        hideable:true,
        align:'left'
    }, {
        text:'直属部门',
        dataIndex:'HREmployee_HRDept_CName',
        width:100,
        sortable:true,
        hidden:false,
        hideable:true,
        align:'left'
    }, {
        text:'职位',
        dataIndex:'HREmployee_HRPosition_CName',
        width:54,
        sortable:true,
        hidden:false,
        hideable:true,
        align:'left'
    },{
        text:'在职',
        dataIndex:'HREmployee_IsEnabled',
        width:46,
        xtype:'booleancolumn',
        trueText:'是',
        falseText:'否',
        defaultRenderer:function(value) {
            if (value === undefined) {
                return this.undefinedText;
            }
            if (!value || value === 'false' || value === '0' || value === 0) {
                return this.falseText;
            }
            return this.trueText;
        },
        sortable:true,
        hidden:false,
        hideable:true,
        align:'left'
    },{
        text:'英文名称',
        dataIndex:'HREmployee_EName',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'left'
    }, {
        text:'主键ID',
        dataIndex:'HREmployee_Id',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    }, {
        text:'显示次序',
        dataIndex:'HREmployee_DispOrder',
        width:66,
        sortable:true,
        hidden:false,
        hideable:true,
        align:'left'
    } ],
    internalDockedItem :[ {
        xtype:'toolbar',
        itemId:'buttonstoolbar',
        dock:'top',
        items:[ {
            itemId:'btn',
            text:'所选员工拥有角色',
            xtype:'button',
            iconCls:'build-button-save'
        }, {
            itemId:'refresh',
            text:'更新',
            iconCls:'build-button-refresh'
        }, '->', {
            xtype:'textfield',
            itemId:'searchText',
            width:160,
            emptyText:'员工名称/直属部门/职位'
        }, {
            xtype:'button',
            text:'查询',
            itemId:'searchbtn',
            iconCls:'search-img-16 ',
            tooltip:'员工名称/直属部门/职位'
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