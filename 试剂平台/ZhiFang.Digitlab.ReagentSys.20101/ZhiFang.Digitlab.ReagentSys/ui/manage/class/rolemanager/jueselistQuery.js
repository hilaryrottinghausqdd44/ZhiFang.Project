
/***
 * 角色管理--角色权限分配
 * 左角色选择列表
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.rolemanager.jueselistQuery', {
    extend:'Ext.grid.Panel',
    alias:'widget.jueselistQuery',
    title:'角色列表',
    objectName:'RBACRole',
    defaultWhere:'',
    internalWhere:'',
    externalWhere:'',
    autoSelect:true,
    deleteIndex:-1,
    url:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?isPlanish=true&fields=RBACRole_DataTimeStamp,RBACRole_CName,RBACRole_Comment,RBACRole_UseCode,RBACRole_Id,RBACRole_IsUse,RBACRole_DispOrder,RBACRole_PinYinZiTou',
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
        
        me.store = Ext.create('Ext.data.Store', {
            fields:[ 'RBACRole_DataTimeStamp','RBACRole_CName', 'RBACRole_Comment', 'RBACRole_UseCode', 'RBACRole_Id', 'RBACRole_IsUse', 'RBACRole_DispOrder', 'RBACRole_PinYinZiTou' ],
            remoteSort:true,
            autoLoad:true,
            sorters:[ {
                property:'RBACRole_DispOrder',
                direction:'ASC'
            } ],
            pageSize:5000,
            proxy:{
                type:'ajax',
                url:me.url,
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
                    data.ResultDataValue = data.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.count = ResultDataValue.count;
                    data.list = ResultDataValue.list;
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
                where += "(rbacrole.CName like '%" + value + "%' or rbacrole.UseCode like '%" + value + "%' or rbacrole.Comment like '%" + value + "%');";
            }
            me.internalWhere = where;
            me.load(me.externalWhere);
        };
        me.columns = [ {
            text:'名称',
            dataIndex:'RBACRole_CName',
            width:100,
            sortable:true,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'代码',
            dataIndex:'RBACRole_UseCode',
            width:55,
            sortable:true,
            hidden:false,
            hideable:true,
            align:'left'
        },{
            text:'描述',
            dataIndex:'RBACRole_Comment',
            width:190,
            sortable:true,
            hidden:false,
            hideable:true,
            align:'left'
        },  {
            text:'主键ID',
            dataIndex:'RBACRole_Id',
            width:10,
            sortable:true,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'时间戳',
            dataIndex:'RBACRole_DataTimeStamp',
            width:10,
            sortable:true,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'使用',
            dataIndex:'RBACRole_IsUse',
            width:43,
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
            text:'显示次序',
            dataIndex:'RBACRole_DispOrder',
            width:64,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'拼音字头',
            dataIndex:'RBACRole_PinYinZiTou',
            width:80,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
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
                text:'',
                tooltip:'刷新数据',
                iconCls:'build-button-refresh',
                handler:function(but, e) {
                    var com = but.ownerCt.ownerCt;
                    com.store.load(com.externalWhere);
                }
            }, '->', {
                xtype:'textfield',
                itemId:'searchText',
                width:150,
                emptyText:'名称/代码/描述',
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
                tooltip:'按照名称/代码/描述进行查询',
                handler:function(button) {
                    me.search();
                }
            } ]
        } ];
        me.getAppInfoServerUrl = getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById';
        me.getAppInfoFromServer = function(id, callback) {
            var me = this;
            var url = me.getAppInfoServerUrl + '?isPlanish=true&id=' + id;
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,
                url:url,
                method:'GET',
                timeout:2000,
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
                        Ext.Msg.alert('提示', '获取应用组件信息失败！错误信息' + result.ErrorInfo + '</b>');
                    }
                },
                failure:function(response, options) {
                    Ext.Msg.alert('提示', '获取应用组件信息请求失败！');
                }
            });
        };
        me.fireEvent('saveClick');
        this.callParent(arguments);
    }
});