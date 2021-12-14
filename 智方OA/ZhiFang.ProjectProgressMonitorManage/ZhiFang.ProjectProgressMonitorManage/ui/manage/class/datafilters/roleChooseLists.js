/***
 * 分配角色按钮弹出角色选择列表
 */
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.roleChooseLists', {
    extend:'Ext.zhifangux.CheckList',
    alias:'widget.roleChooseLists',
    title:'角色选择列表',
    width:382,
    height:380,
    isload:false,//  加载数据开关
    isbutton:false,
    primaryKey:'RBACRole_Id',
    serverUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?isPlanish=true&fields=RBACRole_CName,RBACRole_Id,RBACRole_DataTimeStamp',
    objectName:'RBACRole',
    defaultWhere:'',
    internalWhere:'',
    externalWhere:'',
    autoSelect:true,
    deleteIndex:-1,
    autoScroll:true,
    //selType:'checkboxmodel',
    multiSelect:true,
    sortableColumns:false,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
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
        
        me.store.proxy.url = me.serverUrl + '&where=' + w;
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
        w=me.encodeString(w);
        var myUrl= me.serverUrl + '&where=' + w;
        var store=Ext.create('Ext.data.Store', {
            fields:[ 'RBACRole_CName', 'RBACRole_Id', 'RBACRole_DataTimeStamp' ],
            remoteSort:true,
            autoLoad:true,
            sorters:[],
            pageSize:10000,
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
        me.valueField = [ 
            {
            text:'名称',
            dataIndex:'RBACRole_CName',
            width:168,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'RBACRole_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'RBACRole_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        } ];
        me.internalDockedItem = [ 
            {
            xtype:'toolbar',
            itemId:'buttonstoolbar',
            dock:'top',
            items:[ '->', 
	            {text:'确定选择',xtype:'button',iconCls:'build-button-save',
	            width:80,height:22,
	            itemId:'save',handler:function(button){
	                    me.fireEvent('saveClick');
	                }
	            },{
                    xtype : 'textfield',
                    itemId : 'searchText',
                    width : 160,
                    emptyText : '名称',
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
                 } ]
        } ];
        me.fireEvent('saveClick');
        this.callParent(arguments);
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
         store.filterBy(function(record,id){
            for(var i in searchColumns){
                var text = record.get(searchColumns[i]);
                if(text.indexOf(newValue)!=-1)
                    return true;
            }
            return false;
        });
     },
    /**
     * 查询
     * @private
     */
    search:function(){
        this.load(true);
    }
});