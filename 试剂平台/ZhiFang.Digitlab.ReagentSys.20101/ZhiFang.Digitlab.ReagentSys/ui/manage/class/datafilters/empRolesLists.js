/***
 * 右区域:选择角色节点查看按钮后弹出拥有角色员工
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.empRolesLists', {
    extend:'Ext.grid.Panel',
    alias:'widget.empRolesLists',
    title:'该角色下的员工列表信息',
    width:517,
    height:400,
    objectName:'RBACEmpRoles',
    selectServerUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true',
    selectFields:'RBACEmpRoles_HREmployee_BSex_Name,RBACEmpRoles_RBACRole_CName,RBACEmpRoles_RBACRole_DataTimeStamp,RBACEmpRoles_RBACRole_Id,RBACEmpRoles_HREmployee_CName,RBACEmpRoles_HREmployee_HRDept_CName,RBACEmpRoles_HREmployee_HRPosition_CName,RBACEmpRoles_HREmployee_IsUse,RBACEmpRoles_HREmployee_HRDept_DataTimeStamp,RBACEmpRoles_HREmployee_Id,RBACEmpRoles_Id,RBACEmpRoles_DataTimeStamp',
    defaultWhere:'',
    internalWhere:'',
    externalWhere:'',
    autoSelect:true,
    deleteIndex:-1,
    autoScroll:true,
    sortableColumns:false,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    load:function(where) {
        var me=this;
        if (where !== true) {
            me.externalWhere = where;
        }
        var w = '';
        if (me.externalWhere && me.externalWhere != '') {
            if (me.externalWhere.slice(-1) == '^') {
                w += me.externalWhere;
            } else {
                w += me.externalWhere + ' and ';
            }
        }
        if (me.defaultWhere && me.defaultWhere != '') {
            w += me.defaultWhere + ' and ';
        }
        if (me.internalWhere && me.internalWhere != '') {
            w += me.internalWhere + ' and ';
        }
        w = w.slice(-5) == ' and ' ? w.slice(0, -5) :w;
        w=me.encodeString(w);
        me.store.currentPage = 1;
        var url =me.selectServerUrl+'&fields='+me.selectFields;
        me.store.proxy.url = url + '&where=' + w;
        me.store.load();
    },
    /**
     * 字符串转码
     * @param {} value
     * @return {}
     */
    encodeString:function(value){
        var v = value || "";
        v = encodeURI(v);
        return v;
    },
    createStore:function(where) {
        var me=this;
        if (where !== true) {
            me.externalWhere = where;
        }
        var w = '';
        if (me.externalWhere && me.externalWhere != '') {
            if (me.externalWhere.slice(-1) == '^') {
                w += me.externalWhere;
            } else {
                w += me.externalWhere + ' and ';
            }
        }
        if (me.defaultWhere && me.defaultWhere != '') {
            w += me.defaultWhere + ' and ';
        }
        if (me.internalWhere && me.internalWhere != '') {
            w += me.internalWhere + ' and ';
        }
        w = w.slice(-5) == ' and ' ? w.slice(0, -5) :w;
        w=me.encodeString(w);
        var url =me.selectServerUrl+'&fields='+me.selectFields;
        var myUrl= url + '&where=' + w;
        var store=Ext.create('Ext.data.Store', {
            fields:[ 'RBACEmpRoles_HREmployee_BSex_Name','RBACEmpRoles_RBACRole_CName','RBACEmpRoles_RBACRole_DataTimeStamp','RBACEmpRoles_RBACRole_Id','RBACEmpRoles_HREmployee_CName', 'RBACEmpRoles_HREmployee_HRDept_CName', 'RBACEmpRoles_HREmployee_HRPosition_CName', 'RBACEmpRoles_HREmployee_IsUse', 'RBACEmpRoles_HREmployee_HRDept_DataTimeStamp', 'RBACEmpRoles_HREmployee_Id', 'RBACEmpRoles_Id', 'RBACEmpRoles_DataTimeStamp' ],
            remoteSort:true,
            autoLoad:false,
            sorters:[],
            pageSize:2000,
            proxy:{
                type:'ajax',
                url:myUrl,
                reader:{
                    type:'json',
                    root:'list',
                    totalProperty:'count'
                },
                extractResponseData:function(response) {
                    var data = Ext.JSON.decode(response.responseText);
                    if (!data.success) {
                        Ext.Msg.alert('提示', '错误信息:' + data.ErrorInfo);
                    }
                    if (data.ResultDataValue && data.ResultDataValue != '') {
                        //data.ResultDataValue = data.ResultDataValue.replace(/[\\r\\n]+/g, '<br/>');
                        var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                        data.list = ResultDataValue.list;
                        data.count = ResultDataValue.count;
                    } else {
                        data.list = [];
                        data.count = 0;
                    }
                    response.responseText = Ext.JSON.encode(data);
                    me.setCount(data.count);
                    return response;
                }
            },
            listenres:{
                load:function(s, records, successful, eOpts) {
                    if (!successful) {
                        Ext.Msg.alert('提示', '获取数据服务错误！');
                    }
                }
            }
        });
        return store;
    },
    initComponent:function() {
        var me = this;

        me.store = me.createStore();
        me.columns = [ {
            xtype:'rownumberer',
            text:'序号',
            width:35,
            align:'center'
        }, {
            text:'员工名称',
            dataIndex:'RBACEmpRoles_HREmployee_CName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'直属部门名称',
            dataIndex:'RBACEmpRoles_HREmployee_HRDept_CName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'职位',
            dataIndex:'RBACEmpRoles_HREmployee_HRPosition_CName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'在职',
            dataIndex:'RBACEmpRoles_HREmployee_IsUse',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'RBACEmpRoles_HREmployee_HRDept_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'员工主键ID',
            dataIndex:'RBACEmpRoles_HREmployee_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'RBACEmpRoles_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'RBACEmpRoles_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'角色主键ID',
            dataIndex:'RBACEmpRoles_RBACRole_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'角色名称',
            dataIndex:'RBACEmpRoles_RBACRole_CName',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'性别',
            dataIndex:'RBACEmpRoles_HREmployee_BSex_Name',
            width:50,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        },{
            text:'时间戳',
            dataIndex:'RBACEmpRoles_RBACRole_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }];
        me.setCount = function(count) {
            var me = this;
            var com = me.getComponent('toolbar-bottom').getComponent('count');
            var str = '共' + count + '条';
            com.setText(str, false);
        };
        me.dockedItems = [ {
            xtype:'toolbar',
            dock:'bottom',
            itemId:'toolbar-bottom',
            items:[ {
                xtype:'label',
                itemId:'count',
                text:'共0条'
            } ]
        }];
        me.fireEvent('saveClick');
        this.callParent(arguments);
    }
});