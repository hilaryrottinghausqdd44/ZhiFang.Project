//部门相关员工
Ext.ns('Ext.manage');
Ext.define('Ext.manage.departmentstaff.bumenxiangguanList', {
    extend:'Ext.zhifangux.GridPanel',
    alias:'widget.bumenxiangguanList',
    title:'部门相关员工',
    width:778,
    height:200,
    objectName:'HRDeptEmp',
    defaultLoad:false,
    defaultWhere:'',
    sortableColumns:false,
    initComponent:function() {
        var me = this;
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.zhifangux.GridPanel', getRootPath() + '/ui/zhifangux/GridPanel.js');
        me.url = getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL?isPlanish=true&fields=HRDeptEmp_Id,HRDeptEmp_HREmployee_CName,HRDeptEmp_HRDept_Id,HRDeptEmp_HREmployee_HRDept_CName,HRDeptEmp_HREmployee_Id,HRDeptEmp_HREmployee_HRPosition_CName,HRDeptEmp_HREmployee_HRPosition_Id,HRDeptEmp_HREmployee_IsEnabled,HRDeptEmp_DataTimeStamp,HRDeptEmp_HREmployee_DataTimeStamp,HRDeptEmp_HREmployee_EName,HRDeptEmp_HREmployee_OfficeTel';
        me.searchArray = [ 'hrdeptemp.HREmployee.CName', 'hrdeptemp.HREmployee.HRDept.CName' ];
        me.store = me.createStore({
            fields:[ 'HRDeptEmp_Id', 'HRDeptEmp_HREmployee_CName','HRDeptEmp_HRDept_Id','HRDeptEmp_HREmployee_HRDept_CName', 'HRDeptEmp_HREmployee_Id', 'HRDeptEmp_HREmployee_HRPosition_CName', 'HRDeptEmp_HREmployee_HRPosition_Id', 'HRDeptEmp_HREmployee_IsEnabled', 'HRDeptEmp_DataTimeStamp', 'HRDeptEmp_HREmployee_DataTimeStamp', 'HRDeptEmp_HREmployee_EName', 'HRDeptEmp_HREmployee_OfficeTel' ],
            url:'RBACService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL?isPlanish=true&fields=HRDeptEmp_Id,HRDeptEmp_HREmployee_CName,HRDeptEmp_HRDept_Id,HRDeptEmp_HREmployee_HRDept_CName,HRDeptEmp_HREmployee_Id,HRDeptEmp_HREmployee_HRPosition_CName,HRDeptEmp_HREmployee_HRPosition_Id,HRDeptEmp_HREmployee_IsEnabled,HRDeptEmp_DataTimeStamp,HRDeptEmp_HREmployee_DataTimeStamp,HRDeptEmp_HREmployee_EName,HRDeptEmp_HREmployee_OfficeTel',
            remoteSort:true,
            sorters:[],
            PageSize:25,
            hasCountToolbar:false,
            buffered:false,
            leadingBufferZone:null
        });
        me.defaultColumns = [ {
            text:'主键ID',
            dataIndex:'HRDeptEmp_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'员工名称',
            dataIndex:'HRDeptEmp_HREmployee_CName',
            width:100,
            sortable:true,
            hideable:true,
            align:'left'
        }, {
            text:'主键',
            dataIndex:'HRDeptEmp_HRDept_Id',
            width:100,
            sortable:false,
            hideable:true,
            hidden:true,
            align:'left'
        }, {
            text:'直属部门名称',
            dataIndex:'HRDeptEmp_HREmployee_HRDept_CName',
            width:100,
            sortable:true,
            hideable:true,
            align:'left'
        }, {
            text:'员工主键ID',
            dataIndex:'HRDeptEmp_HREmployee_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'职位名称',
            dataIndex:'HRDeptEmp_HREmployee_HRPosition_CName',
            width:100,
            sortable:true,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'HRDeptEmp_HREmployee_HRPosition_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'在职',
            dataIndex:'HRDeptEmp_HREmployee_IsEnabled',
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
            width:38,
            sortable:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'HRDeptEmp_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'HRDeptEmp_HREmployee_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'英文名称',
            dataIndex:'HRDeptEmp_HREmployee_EName',
            width:100,
            sortable:true,
            hideable:true,
            align:'left'
        }, {
            text:'办公电话',
            dataIndex:'HRDeptEmp_HREmployee_OfficeTel',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        } ];
        me.columns = me.createColumns();
        me.dockedItems = [ {
            itemId:'pagingtoolbar',
            xtype:'pagingtoolbar',
            store:me.store,
            dock:'bottom',
            displayInfo:true
        }, {
            xtype:'toolbar',
            itemId:'buttonstoolbar',
            dock:'top',
            items:[ {
                type:'refresh',
                itemId:'refresh',
                text:'更新',
                iconCls:'build-button-refresh',
                handler:function(but, e) {
                    var com = but.ownerCt.ownerCt;
                    com.load(true);
                }
            }, {
                type:'add',
                itemId:'add',
                text:'新增',
                iconCls:'build-button-add',
                handler:function(but, e) {
                    me.fireEvent('addClick');
                }
            }, {
                type:'del',
                itemId:'del',
                text:'删除',
                iconCls:'build-button-delete',
                handler:function(but, e) {
                    var records = me.getSelectionModel().getSelection();
                    if (records.length > 0) {
                        Ext.Msg.confirm('提示', '确定要删除吗？', function(button) {
                            var createFunction = function(id) {
                                var f = function() {
                                    var rowIndex = me.store.find('HRDeptEmp_Id', id);
                                    me.deleteIndex = rowIndex;
                                    me.load(true);
                                    me.fireEvent('delClick');
                                };
                                return f;
                            };
                            if (button == 'yes') {
                                for (var i in records) {
                                    var id = records[i].get('HRDeptEmp_Id');
                                    var callback = createFunction(id);
                                    me.deleteInfo(id, callback);
                                }
                            }
                        });
                    } else {
                        alertInfo('请选择数据进行操作！');
                    }
                }
            }, '->', {
                xtype:'textfield',
                itemId:'searchText',
                width:160,
                emptyText:'员工名称/直属部门名称'
            }, {
                xtype:'button',
                text:'查询',
                iconCls:'search-img-16 ',
                tooltip:'按照员工名称/直属部门名称进行查询',
                handler:function(button) {
            	    me.fireEvent('searchClick');
                }
            } ]
        } ];
        me.deleteInfo = function(id, callback) {
            var url = getRootPath() + '/RBACService.svc/RBAC_UDTO_DelHRDeptEmp?id=' + id;
            var c = function(text) {
                var result = Ext.JSON.decode(text);
                if (result.success) {
                    if (Ext.typeOf(callback) == 'function') {
                        callback();
                    }
                } else {
                    alertError(result.ErrorInfo);
                }
            };
            getToServer(url, c);
        };
        me.addEvents('addClick');
        me.addEvents('searchClick');
        me.addEvents('afterOpenAddWin');
        me.addEvents('delClick');
        this.callParent(arguments);
    }
});