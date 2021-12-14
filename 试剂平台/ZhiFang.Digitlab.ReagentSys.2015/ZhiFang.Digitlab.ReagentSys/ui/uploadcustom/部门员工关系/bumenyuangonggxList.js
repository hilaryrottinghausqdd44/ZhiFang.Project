//部门管理-相关员工列表
Ext.define('bumenyuangonggxList', {
    extend:'Ext.grid.Panel',
    alias:'widget.bumenyuangonggxList',
    title:'相关部门与员工列表',
    width:778,
    height:200,
    objectName:'HRDeptEmp',
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
        me.store.on({
            load:function(store, records, successful) {
                var autoSelect = me.autoSelect;
                if (successful && records.length > 0) {
                    if (me.deleteIndex && me.deleteIndex != '' && me.deleteIndex != -1) {
                        if (records.length - 1 > me.deleteIndex) {
                            me.getSelectionModel().select(me.deleteIndex);
                        } else {
                            me.getSelectionModel().select(records.length - 1);
                        }
                        me.deleteIndex = -1;
                    } else {
                        if (autoSelect) {
                            var num = 0;
                            if (autoSelect === true) {
                                num = 0;
                            } else {
                                if (autoSelect >= 0) {
                                    num = autoSelect % records.length;
                                } else {
                                    num = length - Math.abs(num) % length;
                                }
                            }
                            me.getSelectionModel().select(num);
                        }
                    }
                }
            }
        });
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    initComponent:function() {
        var me = this;
        Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
        me.url = getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL?isPlanish=true&fields=HRDeptEmp_Id,HRDeptEmp_HREmployee_CName,HRDeptEmp_HREmployee_HRDept_CName,HRDeptEmp_HREmployee_Id,HRDeptEmp_HREmployee_HRPosition_CName,HRDeptEmp_HREmployee_HRPosition_Id,HRDeptEmp_HREmployee_IsEnabled,HRDeptEmp_DataTimeStamp,HRDeptEmp_HREmployee_DataTimeStamp,HRDeptEmp_HREmployee_EName,HRDeptEmp_HREmployee_OfficeTel';
        me.store = Ext.create('Ext.data.Store', {
            fields:[ 'HRDeptEmp_Id', 'HRDeptEmp_HREmployee_CName', 'HRDeptEmp_HREmployee_HRDept_CName', 'HRDeptEmp_HREmployee_Id', 'HRDeptEmp_HREmployee_HRPosition_CName', 'HRDeptEmp_HREmployee_HRPosition_Id', 'HRDeptEmp_HREmployee_IsEnabled', 'HRDeptEmp_DataTimeStamp', 'HRDeptEmp_HREmployee_DataTimeStamp', 'HRDeptEmp_HREmployee_EName', 'HRDeptEmp_HREmployee_OfficeTel' ],
            remoteSort:true,
            autoLoad:false,
            sorters:[],
            pageSize:25,
            proxy:{
                type:'ajax',
                url:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL?isPlanish=true&fields=HRDeptEmp_Id,HRDeptEmp_HREmployee_CName,HRDeptEmp_HREmployee_HRDept_CName,HRDeptEmp_HREmployee_Id,HRDeptEmp_HREmployee_HRPosition_CName,HRDeptEmp_HREmployee_HRPosition_Id,HRDeptEmp_HREmployee_IsEnabled,HRDeptEmp_DataTimeStamp,HRDeptEmp_HREmployee_DataTimeStamp,HRDeptEmp_HREmployee_EName,HRDeptEmp_HREmployee_OfficeTel',
                reader:{
                    type:'json',
                    root:'list',
                    totalProperty:'count'
                },
                extractResponseData:function(response) {
                    var data = Ext.JSON.decode(response.responseText);
                    if (data.ResultDataValue && data.ResultDataValue != '') {
                    	data.ResultDataValue = data.ResultDataValue.replace(/[\r\n]+/g,'');
                        var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                        data.list = ResultDataValue.list;
                        data.count = ResultDataValue.count;
                    } else {
                        data.list = [];
                        data.count = 0;
                    }
                    response.responseText = Ext.JSON.encode(data);
                    return response;
                }
            }
        });
        me.load = function(where) {
            me.externalWhere = where;
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
            me.store.currentPage = 1;
            me.store.proxy.url = me.url + '&where=' + w;
            me.store.load();
        };
        me.search = function() {
            var toolbar = me.getComponent('buttonstoolbar');
            var value = toolbar.getComponent('searchText').getValue();
            var where = '';
            if (value && value != '') {
                where += '(hrdeptemp.HREmployee.CName like %27%25' + value + '%25%27 or hrdeptemp.HREmployee.HRDept.CName like %27%25' + value + '%25%27)';
            }
            me.internalWhere = where;
            me.load(me.externalWhere);
        };
        me.columns = [ {
            text:'主键ID',
            dataIndex:'HRDeptEmp_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'员工名称',
            dataIndex:'HRDeptEmp_HREmployee_CName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'直属部门名称',
            dataIndex:'HRDeptEmp_HREmployee_HRDept_CName',
            width:100,
            sortable:false,
            hidden:false,
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
            sortable:false,
            hidden:false,
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
            width:38,
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
            sortable:false,
            hidden:false,
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
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'办公电话',
            dataIndex:'HRDeptEmp_HREmployee_OfficeTel',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        } ];
        me.dockedItems = [ {
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
                   me.fireEvent('refreshClick');
                }
            }, {
                type:'add',
                itemId:'add',
                text:'新增',
                iconCls:'build-button-add',
                handler:function(but, e) {
                    var records = me.getSelectionModel().getSelection();
                    me.openFormWin(but.type, -1, null);
                }
            }, {
                type:'del',
                itemId:'del',
                text:'删除',
                iconCls:'build-button-delete',
                handler:function(but, e) {
                    me.fireEvent('delClick');
                }
            }, '->', {
                xtype:'textfield',
                itemId:'searchText',
                width:160,
                emptyText:'员工名称/直属部门名称',
                listeners:{
                    render:function(input) {
                        new Ext.KeyMap(input.getEl(), [ {
                            key:Ext.EventObject.ENTER,
                            fn:function() {
                                me.search();
                            }
                        } ]);
                    }
                }
            }, {
                xtype:'button',
                text:'查询',
                iconCls:'search-img-16 ',
                tooltip:'按照员工名称/直属部门名称进行查询',
                handler:function(button) {
                    me.search();
                }
            } ]
        } ];
        me.openFormWin = function(type, id, record) {
            var winId = '4715366595332862318';
            var callback = function(appInfo) {
                if (appInfo && appInfo != '') {
                    var ClassCode = appInfo.BTDAppComponents_ClassCode;
                    if (ClassCode && ClassCode != '') {
                        var panelParams = {
                            type:type,
                            dataId:id,
                            selectionRecord:record,
                            modal:true,
                            floating:true,
                            closable:true,
                            draggable:true
                        };
                        var Class = eval(ClassCode);
                        var panel = Ext.create(Class, panelParams).show();
                        panel.on({
                            saveClick:function() {
                                panel.close();
                                me.load();
                                me.fireEvent('saveClick');
                            }
                        });
                        if (type == 'add') {
                            me.fireEvent('afterOpenAddWin', panel);
                        } else if (type == 'edit') {
                            me.fireEvent('afterOpenEditWin', panel);
                        } else if (type == 'show') {
                            me.fireEvent('afterOpenShowWin', panel);
                        }
                    } else {
                        Ext.Msg.alert('提示', '没有类代码！');
                    }
                }
            };
            me.getAppInfoFromServer(winId, callback);
        };
        me.getAppInfoServerUrl = getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById';
        me.getAppInfoFromServer = function(id, callback) {
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
                        if (appInfo != '') {
                            if (Ext.typeOf(callback) == 'function') {
                                callback(appInfo);
                            }
                        } else {
                            Ext.Msg.alert('提示', '没有获取到应用组件信息！');
                        }
                    } else {
                        Ext.Msg.alert('提示', '获取应用组件信息失败！错误信息' + result.ErrorInfo  );
                    }
                },
                failure:function(response, options) {
                    Ext.Msg.alert('提示', '获取应用组件信息请求失败！');
                }
            });
        };
        me.fireEvent('saveClick');
        me.addEvents('addClick');
        me.addEvents('afterOpenAddWin');
        me.addEvents('delClick');
        me.addEvents('refreshClick');
        this.callParent(arguments);
    }
});