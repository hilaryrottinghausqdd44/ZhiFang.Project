/** 【可选参数】
  * 公共属性
 * valueField 显示列配置
 * FunctionCode 设置功能编码引用
 * templeListUrl 模板列表url
 * deptUrl 科室列表url
 * alldeptUrl 全院列表url
 * templetid 我的页签id
 * deptid 科室页签id
 * alldeptid 全院页签id
 * loadstore 页签数据源加载开关  
 * Store  数据源开关
 * activeTab 默认选中页签
 * templethide 我的模板 显示隐藏
 * depthide 科室显示隐藏
 * alldepthide 全院显示隐藏
 * 公开方法
 * SetWin()   设置按钮根据功能应用id 弹出设置内容
 * List ()    返回现在列表选中行id的值
 * setDefaultCheck()  设置默认选中某一页签的方法
 * getValue()  获取选择中的页签的id值
 * 公开事件
 * onClick  每个每个页签的选择事件
 * onSelect 每个页签勾选的选择事件
 * onDelSelect 每个页签反勾选的选择事件
 * onSetClick 设置
**/
Ext.ns('Ext.zhifangux');

Ext.define('Ext.zhifangux.InspectingItem', {
    extend:'Ext.tab.Panel',
    alias:[ 'widget.inspectingitem' ],
    getAppInfoServerUrl:getRootPath() + '/ServerWCF/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    width:300,
    height:300,
    internalWhere:'',
    //列表的内部hql
    externalWhere:'',
    //列表前台显示的字段(内部使用)
    templetField:[ {
        text:'模板编码',
        dataIndex:'MEPTDefaultItemMode_Id',
        hidden:true
    }, {
        text:'模板名称',
        dataIndex:'MEPTDefaultItemMode_Name'
    } ],
    FunctionCode:'5437282047684741975',
    serverUrl:getRootPath() + '/MEPTService.svc/MEPT_UDTO_SearchModuleList',
    fieldStr:['MEPTDefaultItemMode_Id', 'MEPTDefaultItemMode_Name', 'MEPTDefaultItemMode_HREmployee_Id', 'MEPTDefaultItemMode_HRDept_Id'],
    id:'',
    templethide:false,
    depthide:false,
    alldepthide:false,
    templetid:'templetid',
    deptid:'deptid',
    alldeptid:'alldeptid',
    tabid:'',
    loadstore:false,
    Store:false,
    
    
    /**
     * 创建列表对象数据源 
     */
    listStore:function(where) {
        var me = this;
        var myUrl = me.getUrl(where);
        //数据代理
        var proxy = me.createProxy(myUrl);
        var obj = {
            pageSize:10000,
            fields:me.fieldStr,
            autoLoad:me.Store,
//            autoLoad:me.isload,
            proxy:proxy
        };
//        idProperty:me.primaryKey;
        var store = Ext.create('Ext.data.Store', obj);
        return store;
    },
    getUrl:function(where) {
        var me = this;
        var myUrl = '';
        //前台需要显示的字段
        var fields = me.fieldStr;
        if (me.serverUrl == '') {
            Ext.Msg.alert('提示', '没有配置列表的获取数据的服务！');
        }
        if (!fields) {
            fields = '';
        }
        myUrl = me.serverUrl + '?isPlanish=true&fields=' + fields;
        //服务地址
        me.externalWhere = where;
        var w = '';
        if (me.internalWhere) {
            w += me.internalWhere;
        }
        if (where && where != '') {
            if (w != '') {
                w += ' and ' + where;
            } else {
                w += where;
            }
        }
        myUrl = myUrl + '&where=' + w;
        return myUrl;
    },
    /**
 	 * 创建服务代理
 	 * @private
 	 * @param {} url
 	 * @return {}
 	 */
    createProxy:function(url) {
        var me=this;
        var proxy = {
            type:'ajax',
            url:url,
            reader:{
                type:'json',
                root:'list',
                totalProperty:'count'
            },
            extractResponseData:function(response) {
                var data = Ext.JSON.decode(response.responseText);
                me.counts=0;
                if (data.ResultDataValue && data.ResultDataValue != '') {
                	data.ResultDataValue = data.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                	var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.count = ResultDataValue.count;
                    me.counts=ResultDataValue.count;
                    data.list = ResultDataValue.list;
                } else {
                    data.count = 0;
                    me.counts=0;
                    data.list = [];
                }
                response.responseText = Ext.JSON.encode(data);
                return response;
            }
        };
        return proxy;
    },
    
    
    getValue:function() {
        var me = this;
        var tabid = me.tabid;
        return tabid;
    },
    setDefaultCheck:function(value) {
        var me = this;
        me.activeTab = value;
        return me.activeTab;
    },
    SetWin:function(id, hql) {
        var me = this;
        var maxHeight = document.body.clientHeight * .98;
        var maxWidth = document.body.clientWidth * .98;
        var callback = function(appInfo) {
            var ClassCode = appInfo['BTDAppComponents_ClassCode'];
            var cl = eval(ClassCode);
            var par = {
                maxWidth:maxWidth,
                autoScroll:true,
                modal:true,
                floating:true,
                closable:true,
                draggable:true,
                resizable:true,
                title:'选择模板'
            };
            var win = Ext.create(cl, par);
            if (win.height > maxHeight) {
                win.height = maxHeight;
            }
            win.show();
            var MB = win.getComponent('MRXMMBYY').getComponent('MRXMMB');
            MB.load(hql);
        };
        me.getInfoByIdFormServer(id, callback);
    },
    List:function() {
        var me = this;
        var list = me.id;
        return list;
    },
    addAppEvents:function() {
        var me = this;
        me.addEvents('onClick');
        me.addEvents('onSelect');
        me.addEvents('onDelSelect');
        me.addEvents('onSetClick');
    },
    initComponent:function() {
        var me = this;
        me.addAppEvents();
        me.setAppItems();
        me.listeners={
        	tabchange : function(tabPanel, newCard, oldCard){
	        	var com = tabPanel.activeTab.items.items[0];
	        	me.Store=true;
            }
        };
        this.callParent(arguments);
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.listeners={
	        activate:function(eOpts) {
        	    alert('sdasd');
            }
        };
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    setAppItems:function() {
        var me = this;
        var items = [];
        var templet = me.TempletList();
        if (templet) {
            if (me.templethide == false) {
                items.push(templet);
            }
        }
        var dept = me.DeptList();
        if (dept) {
            if (me.depthide == false) {
                items.push(dept);
            }
        }
        var alldept = me.AllDeptList();
        if (alldept) {
            if (me.alldepthide == false) {
                items.push(alldept);
            }
        }
        me.items = items;
    },
    TempletList:function() {
        var me = this;
        var templetItems = [];
        templetItems.push(me.TempletSet());
        var com = {
            xtype:'gridpanel',
            padding:'0,0,0,0',
            layout:'fit',
            border:false,
            title:'我的模板',
            id:me.templetid,
            selType:'checkboxmodel',
            simpleSelect:true,
            enableKeyNav:true,
            multiSelect:true,
            columns:me.templetField,
            dockedItems:[ {
                xtype:'toolbar',
                dock:'bottom',
                items:templetItems
            } ]
        };
        com.listeners = {
            select:function(sm, record, rowindex, o) {
                me.id = record.data.MEPTDefaultItemMode_Id;
                me.addEvents('onSelect');
            },
            deselect:function(sm, record, rowindex, o) {
                me.id = record.data.MEPTDefaultItemMode_Id;
                me.addEvents('onDelSelect');
            },
            activate:function(eOpts) {
            	me.Store=true;
            	me.tabid = me.templetid;
            	var temp = Ext.getCmp(me.templetid).store;
                if (me.loadstore == true) {
                	temp.reload();
                } else {
                    if (temp.getCount() == 0) {
                    	temp.reload();
                    } else {
                        return;
                    }
                }
            }
            
        };
        com.store=me.listStore('&idType=1&id=4');
        return com;
    },
    TempletSet:function() {
        var me = this;
        var com = {
            xtype:'button',
            iconCls:'icon-setup',
            text:'设置',
            handler:function(o) {
                var hql = 'ModeType=1';
                me.SetWin(me.FunctionCode, hql);
                me.fireEvent('onSetClick');
            }
        };
        return com;
    },
    getInfoByIdFormServer:function(id, callback) {
        var me = this;
        var myUrl = me.getAppInfoServerUrl + '?isPlanish=true&id=' + id;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            url:myUrl,
            method:'GET',
            timeout:2e3,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var appInfo = '';
                    if (result.ResultDataValue && result.ResultDataValue != '') {
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if (Ext.typeOf(callback) == 'function') {
                        if (appInfo == '') {
                            Ext.Msg.alert('提示', '没有获取到应用组件信息！');
                        } else {
                            callback(appInfo);
                        }
                    }
                } else {
                    Ext.Msg.alert('提示', '获取应用信息失败！');
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert('提示', '获取应用信息请求失败！');
            }
        });
    },
    DeptList:function() {
        var me = this;
        var DeptSetItems = [];
        DeptSetItems.push(me.DeptSet());
        var com = {
            xtype:'gridpanel',
            padding:'0,0,0,0',
            layout:'fit',
            border:false,
            title:'科室',
            id:me.deptid,
            selType:'checkboxmodel',
            simpleSelect:true,
            enableKeyNav:true,
            multiSelect:true,
            columns:me.templetField,
            dockedItems:[ {
                xtype:'toolbar',
                dock:'bottom',
                items:DeptSetItems
            } ]
        };
        com.listeners = {
            select:function(sm, record, rowindex, o) {
                me.id = record.data.MEPTDefaultItemMode_Id;
                me.addEvents('onSelect');
            },
            deselect:function(sm, record, rowindex, o) {
                me.id = record.data.MEPTDefaultItemMode_Id;
                me.addEvents('onDelSelect');
            },
            activate:function(tab) {
                me.tabid = me.deptid;
                var dep = Ext.getCmp(me.deptid).store;
                if (me.loadstore == true) {
                	dep.reload();
                } else {
                    if (dep.getCount() == 0) {
                    	dep.reload();
                    } else {
                        return;
                    }
                }
            }
        };
        com.store=me.listStore('&idType=2&id=2');
        return com;
    },
    DeptSet:function() {
        var me = this;
        var com = {
            xtype:'button',
            iconCls:'icon-setup',
            text:'设置',
            handler:function(o) {
                var hql = 'ModeType=2';
                me.SetWin(me.FunctionCode, hql);
                me.fireEvent('onSetClick');
            }
        };
        return com;
    },
    AllDeptList:function() {
        var me = this;
        var allSetItems = [];
        allSetItems.push(me.AllDeptSet());
        var com = {
            xtype:'gridpanel',
            padding:'0,0,0,0',
            layout:'fit',
            selType:'checkboxmodel',
            simpleSelect:true,
            enableKeyNav:true,
            multiSelect:true,
            border:false,
            title:'全院',
            id:me.alldeptid,
            columns:me.templetField,
            dockedItems:[ {
                xtype:'toolbar',
                dock:'bottom',
                items:allSetItems
            } ]
        };
        com.listeners = {
            select:function(sm, record, rowindex, o) {
                me.id = record.data.MEPTDefaultItemMode_Id;
                me.addEvents('onSelect');
            },
            deselect:function(sm, record, rowindex, o) {
                me.id = record.data.MEPTDefaultItemMode_Id;
                me.addEvents('onDelSelect');
            },
            activate:function(tab) {
                me.tabid = me.alldeptid;
                var alldept = Ext.getCmp(me.alldeptid).store;
                if (me.loadstore == true) {
                    alldept.reload();
                } else {
                    if (alldept.getCount() == 0) {
                        alldept.reload();
                    } else {
                        return;
                    }
                }
            }
        };
        com.store=me.listStore('&idType=3&id=1');
        return com;
    },
    AllDeptSet:function() {
        var me = this;
        var com = {
            xtype:'button',
            iconCls:'icon-setup',
            text:'设置',
            handler:function(o) {
                me.SetWin(me.FunctionCode, '');
                me.fireEvent('onSetClick');
            }
        };
        return com;
    }
});