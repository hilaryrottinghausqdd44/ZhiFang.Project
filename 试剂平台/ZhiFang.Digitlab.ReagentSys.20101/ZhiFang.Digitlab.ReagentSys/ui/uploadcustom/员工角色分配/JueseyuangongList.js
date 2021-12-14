//拥有角色员工列表
Ext.define('JueseyuangongList', {
    extend:'Ext.grid.Panel',
    alias:'widget.JueseyuangongList',
    title:'拥有角色员工列表',
    width:792,
    height:200,
    objectName:'RBACEmpRoles',
    defaultWhere:'',
    internalWhere:'',
    externalWhere:'',
    autoSelect:true,
    deleteIndex:-1,
    autoScroll:true,
    sortableColumns:true,
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
                                if (autoSelect.length == 19) {
                                    var index = store.find(me.objectName + '_Id', autoSelect);
                                    if (index != -1) {
                                        num = index;
                                    }
                                    me.autoSelect = true;
                                } else {
                                    if (autoSelect >= 0) {
                                        num = autoSelect % records.length;
                                    } else {
                                        num = length - Math.abs(num) % length;
                                    }
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
        me.url = getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true&fields=RBACEmpRoles_HREmployee_CName,RBACEmpRoles_Id,RBACEmpRoles_HREmployee_EName,RBACEmpRoles_HREmployee_HRDept_CName,RBACEmpRoles_HREmployee_DispOrder,RBACEmpRoles_HREmployee_HRPosition_CName,RBACEmpRoles_RBACRole_Id,RBACEmpRoles_HREmployee_IsEnabled,RBACEmpRoles_RBACRole_CName';
        me.store = Ext.create('Ext.data.Store', {
            fields:[ 'RBACEmpRoles_HREmployee_CName', 'RBACEmpRoles_Id', 'RBACEmpRoles_HREmployee_EName', 'RBACEmpRoles_HREmployee_HRDept_CName', 'RBACEmpRoles_HREmployee_DispOrder', 'RBACEmpRoles_HREmployee_HRPosition_CName', 'RBACEmpRoles_RBACRole_Id', 'RBACEmpRoles_HREmployee_IsEnabled', 'RBACEmpRoles_RBACRole_CName' ],
            remoteSort:true,
            autoLoad:false,
            sorters:[],
            pageSize:1e4,
            proxy:{
                type:'ajax',
                url:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true&fields=RBACEmpRoles_HREmployee_CName,RBACEmpRoles_Id,RBACEmpRoles_HREmployee_EName,RBACEmpRoles_HREmployee_HRDept_CName,RBACEmpRoles_HREmployee_DispOrder,RBACEmpRoles_HREmployee_HRPosition_CName,RBACEmpRoles_RBACRole_Id,RBACEmpRoles_HREmployee_IsEnabled,RBACEmpRoles_RBACRole_CName',
                reader:{
                    type:'json',
                    root:'list',
                    totalProperty:'count'
                },
                extractResponseData:function(response) {
                    var data = Ext.JSON.decode(response.responseText);
                    if (data.ResultDataValue && data.ResultDataValue != '') {
                    	data.ResultDataValue = data.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                    	var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                        data.count = ResultDataValue.count;
                        data.list = ResultDataValue.list;
                    } else {
                        data.count = 0;
                        data.list = [];
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
        me.load = function(where) {
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
            me.store.currentPage = 1;
            me.store.proxy.url = me.url + '&where=' + w;
            me.store.load();
        };
        me.search = function() {
            var toolbar = me.getComponent('buttonstoolbar');
            var value = toolbar.getComponent('searchText').getValue();
            var where = '';
            if (value && value != '') {
                where += '(rbacemproles.HREmployee.CName like %27%25' + value + '%25%27 or rbacemproles.HREmployee.HRDept.CName like %27%25' + value + '%25%27 or rbacemproles.HREmployee.HRPosition.CName like %27%25' + value + '%25%27);';
            }
            me.internalWhere = where;
            me.load(me.externalWhere);
        };
        me.columns = [ {
            xtype:'rownumberer',
            text:'序号',
            width:35,
            align:'center'
        }, {
            text:'员工姓名',
            dataIndex:'RBACEmpRoles_HREmployee_CName',
            width:76,
            sortable:true,
            hidden:false,
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
            text:'英文名称',
            dataIndex:'RBACEmpRoles_HREmployee_EName',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'直属部门名称',
            dataIndex:'RBACEmpRoles_HREmployee_HRDept_CName',
            width:100,
            sortable:true,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'显示次序',
            dataIndex:'RBACEmpRoles_HREmployee_DispOrder',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'职位',
            dataIndex:'RBACEmpRoles_HREmployee_HRPosition_CName',
            width:79,
            sortable:true,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'RBACEmpRoles_RBACRole_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'在职',
            dataIndex:'RBACEmpRoles_HREmployee_IsEnabled',
            width:37,
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
            text:'名称',
            dataIndex:'RBACEmpRoles_RBACRole_CName',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            xtype:'actioncolumn',
            text:'操作',
            width:60,
            align:'center',
            sortable:false,
            hidden:false,
            hideable:false,
            items:[ {
                tooltip:'删除',
                iconCls:'build-button-delete hand',
                handler:function(grid, rowIndex, colIndex, item, e, record) {
            	    me.fireEvent("delClick");
//                    Ext.Msg.confirm('提示', '确定要删除吗？', function(button) {
//                        if (button == 'yes') {
//                            var id = record.get('RBACEmpRoles_Id');
//                            var callback = function() {
//                                me.deleteIndex = rowIndex;
//                                me.load(true);
//                            };
//                            me.deleteInfo(id, callback);
//                        }
//                    });
                }
            } ]
        } ];
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
                    com.store.load(com.externalWhere);
                }
            }, '->', {
                xtype:'textfield',
                itemId:'searchText',
                width:160,
                emptyText:'员工姓名/直属部门名称/职位',
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
                tooltip:'按照员工姓名/直属部门名称/职位进行查询',
                handler:function(button) {
                    me.search();
                }
            } ]
        } ];
        me.deleteInfo = function(id, callback) {
            var url = getRootPath() + '/RBACService.svc/RBAC_UDTO_DelRBACEmpRoles?id=' + id;
            Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
            Ext.Ajax.request({
                async:false,
                url:url,
                method:'GET',
                timeout:2e3,
                success:function(response, opts) {
                    var result = Ext.JSON.decode(response.responseText);
                    if (result.success) {
                        if (Ext.typeOf(callback) == 'function') {
                            callback();
                        }
                    } else {
                        Ext.Msg.alert('提示', '删除信息失败！错误信息' + result.ErrorInfo + '</b>】');
                    }
                },
                failure:function(response, options) {
                    Ext.Msg.alert('提示', '删除信息请求失败！');
                }
            });
        };
        me.openFormWin = function(type, id, record) {};
        me.fireEvent('saveClick');
        me.addEvents('delClick');
        this.callParent(arguments);
    }
});