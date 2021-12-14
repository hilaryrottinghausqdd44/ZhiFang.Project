Ext.onReady(function(){ 
    Ext.QuickTips.init();//初始化后就会激活提示功能
    Ext.Loader.setConfig({enabled: true});//允许动态加载
    Ext.Loader.setPath('Ext.zhifangux', '../zhifangux');
Ext.ns('Ext.zhifangux');

var test=

Ext.define('Ext.manage.module.RBACModuleOper', {
    extend:'Ext.grid.Panel',
    alias:'widget.rbacmoduleoper',
    title:'模块操作编辑列表',
    /**
     * 外面传入的应用组件ID
     * 在openAppComponentsTreeWin里有测试数据
     */
    appId:-1,
    /***
     * 接收外部的传入的模块Id值
     * @type String
     */
    moduleId:'',
    /***
     * 接收外部的传入的模块时间
     * @type String
     */
    moduleDataTimeStamp:'',
    /**
     * 需要过滤的数据字段
     * @type 
     */
    filterFields:['DataTimeStamp','DataAddTime','LabID','dataStatus'],
    /***
     * 是否打开应用操作树
     * @type Boolean
     */
    isOpenAppComponentsTree:true,
    /***
     * 应用操作树返回的选中节点数组
     * @type 
     */
    arrnodesChecked:'',
    /***
     * 查询应用操作列表的HQLwhere串
     * @type 
     */
    btdAppComponentsHQL:[],
    width:800,
    height:200,
    objectName:'RBACModuleOper',
    defaultWhere:'',
    fields:
    [ 
    'RBACModuleOper_UseCode', 'RBACModuleOper_CName', 
    'RBACModuleOper_Comment', 'RBACModuleOper_IsUse',
    'RBACModuleOper_DispOrder', 'RBACModuleOper_DefaultChecked',
    'RBACModuleOper_RBACModule_CName', 'RBACModuleOper_RBACModule_IsUse',
    'RBACModuleOper_RBACModule_DispOrder', 'RBACModuleOper_RBACModule_Id',
    'RBACModuleOper_RBACModule_DataTimeStamp', 'RBACModuleOper_Id',
    'RBACModuleOper_DataTimeStamp', 'RBACModuleOper_LabID' ,
    'RBACModuleOper_BTDAppComponentsOperate_Id','RBACModuleOper_BTDAppComponentsOperate_DataTimeStamp'
    ],
  
    /***
     * 编辑字段
     */
    editfields:'RBACModuleOper_Id,RBACModuleOper_CName,RBACModuleOper_IsUse,RBACModuleOper_Comment,RBACModuleOper_DefaultChecked,RBACModuleOper_BTDAppComponentsOperate_Id',
    saveServerUrl:getRootPath() + '/'+'RBACService.svc/RBAC_UDTO_AddRBACModuleOper',
    editServerUrl:getRootPath() + '/'+'RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField',
    getServerUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true',
    /***
     * 查询应用操作组件的URL
     */
    getBTDAppComponentsOperateUrl:getRootPath() +'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsOperateByHQL?isPlanish=true',                
    internalWhere:'',
    externalWhere:'',
    autoSelect:true,
    deleteIndex:-1,
    autoScroll:true,
    sortableColumns:false,
    /**
     * 该字段是否需要过滤
     * @private
     * @param {} field
     * @return {Boolean}
     */
    isFilterField:function(field){
        var me = this;
        var filterFields = me.filterFields || [];
        for(var i in filterFields){
            if(filterFields[i] == field.split('_').slice(-1)){
                return true;
            }
        }
        return false;
    },

    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    /**
     * 弹出应用模块树页面
     * @private
     * @param {} type
     * @param {} id
     */
    openAppComponentsTreeWin:function(type,obj){
        var me = this;
        var title = '';
        var Id = -1;
         if(type == "show"){
            title = "应用操作树选择";
            Id = -1;
        }
        
        //测试语句
        me.appId='5022608030844759775';
        
        var appComponentsTree = Ext.create('Ext.zhifangux.appComponentsTree',{
            modal:true,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            draggable:true,//可移动
            
            isWindow:true,//窗口打开
            
            getAppListServerUrl:me.getAppListServerUrl,
            updateFileServerUrl:me.updateFileServerUrl,
            
            title:title,
            type:type,
            Id:Id,
            appId:me.appId,//外面传入的应用组件Id值
            arrChecked:[],//
            treeDataConfig:0,//为过滤没有操作的应用,其他值时不过滤
            externalApp:me//外部调用的应用组件,如表单或者列表
        }).show();
        
        //外部传入参数默认勾选子节点
        //appComponentsTree.defaultChecked(arrChecked);
        appComponentsTree.on({
            cancelClick:function(){
                appComponentsTree.close();
            },
            okClick:function(){
                me.arrnodesChecked=appComponentsTree.getnodesChecked();
                appComponentsTree.close();
                //拼成查询应用操作组件信息的HQL的where串,以方便获取应用操作组件Id及应用操作组件时间戳
                me.btdAppComponentsHQL='';
                if(me.arrnodesChecked.length>0){
                    //列表中显示被勾选中的对象
                    me.btdAppComponentsHQL='';
                    Ext.Array.each(me.arrnodesChecked,function(record){
                      me.btdAppComponentsHQL=me.btdAppComponentsHQL+"'"+record.tid+"'"+',';
                    });
                    if(me.btdAppComponentsHQL.length>1){
	                    me.btdAppComponentsHQL=me.btdAppComponentsHQL.substring(0,me.btdAppComponentsHQL.length-1);
	                    me.btdAppComponentsHQL='&where= btdappcomponentsoperate.Id in ('+me.btdAppComponentsHQL+')';
                        var arr=me.getBTDAppComponentsDatas();
                        me.setRecordByArray(arr);
                    }
                 }
            }
        });

    },
    /**
     * 保存,修改按钮事件处理
     * @private
     */
    saveContents:function(){
        var me = this;
        var store = me.store;
        
        var addArr = [];//需要新增的数据
        var editArr = [];//需要修改的数据
        store.each(function(record){
            var dirty = record.dirty;//是否是脏数据
            if(dirty){
                var id = record.get('RBACModuleOper_Id');
                if(id == ''){//新增的数据
                    addArr.push(record);
                }else{//修改的数据
                    editArr.push(record);
                }
            }
        });
        //需要新增的数据
        for(var i in addArr){
            addArr[i].dataStatus = ''//状态置空
            var entity = me.getEntityByRecord(addArr[i]);
            entity.Id = -1;
            entity.LabId = 0;
            var obj = {entity:entity};
            var callback = function(responseText){
                var result = Ext.JSON.decode(responseText);
                var record = addArr[i];
                if(result.success){
                    var data = Ext.JSON.decode(result.ResultDataValue);
                    record.set('RBACModuleOper_Id',data.id);
                    record.set('dataStatus','<b style="color:green">新增成功</b>');
                    record.commit();
                }else{
                    record.set('dataStatus','<b style="color:red">新增失败</b>');
                }
            };
            var params = Ext.JSON.encode(obj);
            //util-POST方式与后台交互
            postToServer(me.saveServerUrl,params,callback);
        }
        //需要修改的数据
        for(var i in editArr){
            editArr[i].dataStatus = ''//状态置空
            var entity = me.getEntityByRecord(editArr[i]);
            var obj = {
                entity:entity,
                fields:me.editfields
            };
            var callback = function(responseText){
                var result = Ext.JSON.decode(responseText);
                var record = editArr[i];
                if(result.success){
                    record.set('dataStatus','<b style="color:green">修改成功</b>');
                    record.commit();
                }else{
                    record.set('dataStatus','<b style="color:red">新增失败</b>');
                    record.commit();
                }
            };
            var params = Ext.JSON.encode(obj);
            //util-POST方式与后台交互
            postToServer(me.editServerUrl,params,callback);
        }
    },
    /**
     * 根据record获取需要的数据对象
     * @private
     * @param {} record
     * @return {}
     */
    getEntityByRecord:function(record){
        var me = this;
        var obj = {};
        var data = record.data;
        for(i in data){
            var isFilterField = me.isFilterField(i);
            if(!isFilterField){//非过滤字段
                obj[i.split('_').slice(-1)] = data[i];
            }
        }
        return obj;
    },
    /***
     * 获取应用操作组件Id及应用组件Id时间戳
     */
    getBTDAppComponentsDatas:function() {
        var me=this;
    
        if(me.btdAppComponentsHQL==""||me.btdAppComponentsHQL==null){
            Ext.Msg.alert('提示','请先选择应用操作！');
            return null;
        }
        if(me.getBTDAppComponentsOperateUrl==""||me.getBTDAppComponentsOperateUrl==null){
            Ext.Msg.alert('提示','没有配置获取应用操作数据服务地址或者配置失败！');
            return null;
        }
        var localData=[];
        Ext.Ajax.request({
            async:false,//非异步
            url:me.getBTDAppComponentsOperateUrl+me.btdAppComponentsHQL+'',
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                var ResultDataValue = {count:0,list:[]};
                if(result['ResultDataValue'] && result['ResultDataValue'] != ""){
                    ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
                }
	            var count = ResultDataValue['count'];
	            for (var i = 0; i <count; i++) { 
	                var BTDAppComponentsOperateId=ResultDataValue.list[i]['BTDAppComponentsOperate_Id'];
	                var DataTimeStamp=ResultDataValue.list[i]['BTDAppComponentsOperate_DataTimeStamp'];
                    //var moduleOperCode=ResultDataValue.list[i]['ModuleOperCode'];//内部编码
	                //往前台操作列表新增数据
                    var obj={ 
                           'RBACModuleOper_Id':'',
                           'RBACModuleOper_CName':'',
                           'RBACModuleOper_IsUse':true,
                           'RBACModuleOper_Comment':'',
                           'RBACModuleOper_BTDAppComponentsOperate_Id':"'"+BTDAppComponentsOperateId+"'",
                           'RBACModuleOper_BTDAppComponentsOperate_DataTimeStamp':DataTimeStamp,
                           'RBACModuleOper_RBACModule_Id':me.moduleId,
                           'RBACModuleOper_RBACModule_DataTimeStamp':me.moduleDataTimeStamp
	                    };
                    localData.push(obj);
	                }

                }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息请求失败！');
            }
        });
        return localData;
    },
    /**
     * 给记录列表赋值
     * @private
     * @param {} array
     */
    setRecordByArray:function(array){
        var me = this;
        Ext.Array.each(array,function(obj){
            var rec = ('Ext.data.Model',obj);
            me.addRecordByRecord(rec);//添加组件记录
        });
    },
    /**
     * 新添加模块操作记录
     * @private
     * @param {} record
     */
    addRecordByRecord:function(record){
        var me = this;
        var list = me;//列属性列表
        var store = list.store;
        store.add(record);
    },
    initComponent:function() {
        var me = this;
        Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
        var w = '';
        //外部传入的模块Id
        if(me.moduleId!=''){
            w += '&where '+me.moduleId + '';
        }
        me.store = Ext.create('Ext.data.Store', {
            fields:me.fields,
            remoteSort:true,
            autoLoad:false,
            sorters:[],
            pageSize:10,
            proxy:{
                type:'ajax',
                url:me.getServerUrl+'&fields='+me.fields + '&where=' + w,
                reader:{
                    type:'json',
                    root:'list',
                    totalProperty:'count'
                },
                extractResponseData:function(response) {
                    var data = Ext.JSON.decode(response.responseText);
                    if (data.ResultDataValue && data.ResultDataValue != '') {
                        data.ResultDataValue = data.ResultDataValue.replace(/[rn]+/g, '<br/>');
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
            //外部传入的模块Id
            if(me.moduleId!=''){
                w += me.moduleId + ' and ';
            }
            w = w.slice(-5) == ' and ' ? w.slice(0, -5) :w;
            me.store.currentPage = 1;
            me.store.proxy.url = me.getServerUrl+'&fields='+me.fields + '&where=' + w;
            me.store.load();
        };
        me.columns = [ {
            text:'模块操作代码',
            dataIndex:'RBACModuleOper_UseCode',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'模块操作名称',
            dataIndex:'RBACModuleOper_CName',
            width:222,
            sortable:false,
            hidden:false,
            hideable:true,
            editor:{
                allowBlank:true
            },
            align:'left'
        }, {
            text:'模块操作描述',
            dataIndex:'RBACModuleOper_Comment',
            width:171,
            sortable:false,
            hidden:false,
            hideable:true,
            editor:{
                allowBlank:true
            },
            align:'left'
        }, 
        {text:'是否使用',dataIndex:'RBACModuleOper_IsUse',width:69,align:'left',
            sortable:false,
            hidden:false,
            hideable:true,
            xtype:'checkcolumn',
            editor:{
                xtype:'checkbox',
                cls:'x-grid-checkheader-editor'
            },listeners:{
                checkchange:function(com,rowIndex,checked, eOpts ){
                }
             }
         },
        {
            text:'显示次序',
            dataIndex:'RBACModuleOper_DispOrder',
            width:84,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left',
            xtype:'numbercolumn',
            format:'第0行',
            editor:{
                xtype:'numberfield',
                allowBlank:false,
                minValue:1,
                maxValue:999
            }
        }, {
            text:'默认状态',
            dataIndex:'RBACModuleOper_DefaultChecked',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块名称',
            dataIndex:'RBACModuleOper_RBACModule_CName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        },{
            text:'模块主键ID',
            dataIndex:'RBACModuleOper_RBACModule_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块时间戳',
            dataIndex:'RBACModuleOper_RBACModule_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块操作主键ID',
            dataIndex:'RBACModuleOper_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块操作时间戳',
            dataIndex:'RBACModuleOper_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块操作实验室ID',
            dataIndex:'RBACModuleOper_LabID',
            width:119,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'应用操作ID',
            dataIndex:'RBACModuleOper_BTDAppComponentsOperate_Id',
            width:119,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        } , {
            text:'应用操作时间戳',
            dataIndex:'RBACModuleOper_BTDAppComponentsOperate_DataTimeStamp',
            width:119,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }  ];
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
            }, {
                type:'add',
                itemId:'add',
                text:'新增',
                iconCls:'build-button-add',
                handler:function(but, e) {
                    me.fireEvent('addClick');
                    if(me.isOpenAppComponentsTree==true){
                        me.openAppComponentsTreeWin();
                    }
                }
            }, {
                type:'del',
                itemId:'del',
                text:'删除',
                iconCls:'build-button-delete',
                handler:function(but, e) {
                    me.fireEvent('delClick');
                    var list = but.ownerCt.ownerCt;
                    var records = list.getSelectionModel().getSelection();
                    if (records.length > 0) {
                        Ext.Msg.confirm('提示', '确定要删除吗？', function(button) {
                            if (button == 'yes') {
                                var records = me.getSelectionModel().getSelection();
                                for (var i in records) {
                                    var id = records[i].get('RBACModuleOper_RBACModule_Id');
                                    var callback = function() {
                                        var rowIndex = me.store.find('RBACModuleOper_RBACModule_Id', id);
                                        me.deleteIndex = rowIndex;
                                        me.load();
                                    };
                                    me.deleteInfo(id, callback);
                                }
                            }
                        });
                    } else {
                        Ext.Msg.alert('提示', '请选择数据进行操作！');
                    }
                }
            }, {
                xtype:'button',
                text:'修改保存',
                iconCls:'build-button-save',
                margin:'0 0 0 2',
                itemId:'save-button',
                name:'save-button',
                listeners:{
                    click:function(but, e, eOpts) {
                        
                        var editer_id = me.objectName + '_Id';
                        var editerFields = '';
                        var arrFields = [ 'RBACModuleOper_CName', 'RBACModuleOper_Comment', 'RBACModuleOper_RBACModule_IsUse' ];
                        if (editer_id === '' || editer_id === null) {
                            Ext.Msg.alert('提示', '请选择交互字段进行操作！');
                            return;
                        } else {
                            arrFields.push(editer_id);
                        }
                        var strCount = me.store.getModifiedRecords();
                        Ext.Msg.confirm('警告', '确定要修改保存吗？', function(button) {
                            if (button == 'yes') {
                                for (var i = 0; i < strCount.length; i++) {
                                    for (var j = 0; j < arrFields.length; j++) {
                                        editerFields = editerFields + arrFields[j] + ":'" + strCount[i].get(arrFields[j]) + "',";
                                    }
                                    editerFields = editerFields.substring(0, editerFields.length - 1);
                                    editerFields = '{' + editerFields + '}';
                                    var editerJSON = Ext.JSON.decode(editerFields);
                                    //me.saveToTable(editerJSON);
                                    editerFields = '';
                                }
                                me.store.load();
                                me.fireEvent('savechangesClick');
                            } else {
                                me.store.load();
                            }
                        });
                    }
                }
            } ]
        } ];
       
        var rowEditing = Ext.create('Ext.grid.plugin.CellEditing', {
            clicksToEdit:1
        });
        me.plugins = [ rowEditing ];
        me.deleteInfo = function(id, callback) {
            var url = getRootPath() + '/RBACService.svc/RBAC_UDTO_DelRBACModuleOper?id=' + id;
            Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
            Ext.Ajax.request({
                async:false,
                url:url,
                method:'GET',
                timeout:2000,
                success:function(response, opts) {
                    var result = Ext.JSON.decode(response.responseText);
                    if (result.success) {
                        if (Ext.typeOf(callback) == 'function') {
                            callback();
                        }
                    } else {
                        Ext.Msg.alert('提示', '删除信息失败！错误信息' + result.ErrorInfo + '</b>');
                    }
                },
                failure:function(response, options) {
                    Ext.Msg.alert('提示', '删除信息请求失败！');
                }
            });
        };
        me.openFormWin = function(type, id, record) {};
        me.fireEvent('saveClick');
        me.addEvents('addClick');
        me.addEvents('afterOpenAddWin');
        me.addEvents('delClick');
        me.addEvents('savechangesClick');
        this.callParent(arguments);
    }
});

//总体布局
    Ext.create('Ext.container.Viewport',{
        items:test
    });
});