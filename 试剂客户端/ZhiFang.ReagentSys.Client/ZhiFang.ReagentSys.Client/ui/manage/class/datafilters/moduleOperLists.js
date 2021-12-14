/***
 * 中间区域模块操作列表
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.moduleOperLists', {
    extend:'Ext.grid.Panel',
    alias:'widget.moduleOperLists',
    title:'',
    objectName:'RBACModuleOper',
    defaultWhere:'',
    internalWhere:'',
    externalWhere:'',
    selectIndex:0,
    autoSelect:true,
    deleteIndex:-1,
    autoScroll:true,
    sortableColumns:false,
    selectServerUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true&fields=RBACModuleOper_RowFilterBase,RBACModuleOper_RBACRowFilter_CName,RBACModuleOper_RBACRowFilter_Id,RBACModuleOper_UseRowFilter,RBACModuleOper_BTDAppComponentsOperate_RowFilterBase,RBACModuleOper_CName,RBACModuleOper_Id,RBACModuleOper_DataTimeStamp,RBACModuleOper_RBACModule_CName,RBACModuleOper_RBACModule_Id,RBACModuleOper_RBACModule_DataTimeStamp,RBACModuleOper_BTDAppComponentsOperate_RowFilterBase',
    
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.store.on({
            load:function(store,records,successful){
                if(successful&&records.length>0){
                    me.getSelectionModel().select(me.selectIndex);
                }
            }
        });
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
        w=encodeString(w);
        var url =me.selectServerUrl;
        me.store.proxy.url = url + '&where=' + w;
        me.store.load();
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
        w=encodeString(w);
        var myUrl= me.selectServerUrl + '&where=' + w;
        var store=Ext.create('Ext.data.Store', {
            fields:['RBACModuleOper_RowFilterBase','RBACModuleOper_RBACRowFilter_CName','RBACModuleOper_RBACRowFilter_Id','RBACModuleOper_UseRowFilter', 'RBACModuleOper_BTDAppComponentsOperate_RowFilterBase','RBACModuleOper_CName', 'RBACModuleOper_Id', 'RBACModuleOper_DataTimeStamp', 'RBACModuleOper_RBACModule_CName', 'RBACModuleOper_RBACModule_Id', 'RBACModuleOper_RBACModule_DataTimeStamp' ],
            remoteSort:true,
            autoLoad:false,
            sorters:[],
            pageSize:1000,
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
    setCount:function(count) {
        var me = this;
        var com = me.getComponent('toolbar-bottom').getComponent('count');
        var str = '共' + count + '条';
        com.setText(str, false);
     },
   
    /**
     * 模糊查询过滤函数
     * @param {} value
     */
     filterFn: function (value) {
         var me = this, valtemp = value;
         var store = me.getStore();
         if (!valtemp) {
             store.clearFilter();
             return;
         }
         valtemp = String(value).trim().split(' ');
         store.filterBy(function (record, id) {
             var data = record.data;
             for (var p in data) {
                 var porp = String(data[p]);
                 for (var i = 0; i < valtemp.length; i++) {
                     var macther = valtemp[i];
                     var macther2 = '^' + Ext.escapeRe(macther);
                     mathcer = new RegExp(macther2);
                     if (mathcer.test(porp)) {
                         return true;
                     } 
                 } 
             }
             return false;
         });
     },
    initComponent:function() {
        var me = this;
        me.columns = [ {
            text:'模块操作名称',
            dataIndex:'RBACModuleOper_CName',
            width:200,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        },{
            text:'默认数据过滤条件',
            dataIndex:'RBACModuleOper_RBACRowFilter_CName',
            width:85,
            sortable:false,
            hidden:true,
            hideable:false,
            align:'left'
        },{
            text:'默认数据过滤条件ID',
            dataIndex:'RBACModuleOper_RBACRowFilter_Id',
            width:20,
            sortable:false,
            hidden:true,
            hideable:false,
            align:'left'
        },{
            text:'数据对象',
            dataIndex:'RBACModuleOper_RowFilterBase',
            width:80,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'行过滤依据对象',
            dataIndex:'RBACModuleOper_BTDAppComponentsOperate_RowFilterBase',
            width:80,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块操作主键ID',
            dataIndex:'RBACModuleOper_Id',
            width:10,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块操作时间戳',
            dataIndex:'RBACModuleOper_DataTimeStamp',
            width:10,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块名称',
            dataIndex:'RBACModuleOper_RBACModule_CName',
            width:80,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块主键ID',
            dataIndex:'RBACModuleOper_RBACModule_Id',
            width:10,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块时间戳',
            dataIndex:'RBACModuleOper_RBACModule_DataTimeStamp',
            width:10,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'是否采用数据过滤条件',
            dataIndex:'RBACModuleOper_UseRowFilter',
            width:80,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        } ];
        
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
            items:[ '->', {
                xtype:'textfield',
                itemId:'searchText',
                width:150,
                emptyText:'',
                listeners:{
                    change:function(com, newValue, oldValue, eOpts ){
                        me.filterFn(newValue);
                    },
                    render : function(input) {
	                    new Ext.KeyMap(input.getEl(), [{
	                        key : Ext.EventObject.ENTER,
	                        fn : function() {
                                var newValue=input.getValue();
	                            me.filterFn(newValue);
	                        }
	                    }]);
	                }
                }
            }]
        } ];
        me.fireEvent('saveClick');
        me.store = me.createStore('');
        this.callParent(arguments);
    }
});