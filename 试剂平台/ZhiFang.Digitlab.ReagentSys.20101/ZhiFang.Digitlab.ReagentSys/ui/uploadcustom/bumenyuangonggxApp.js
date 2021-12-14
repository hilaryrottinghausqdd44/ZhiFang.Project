/***
 * 部门管理---部门员工关系
 */
Ext.define('bumenyuangonggxApp', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.bumenyuangonggxApp',
    title:'部门员工关系-App',
    width:816,
    height:300,
    bmzshql:'',
    bmxgyghql:'',
    layout:{
        type:'border',
        regionWeights:{
            west:2
        }
    },
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    appInfos:[ {
        width:338,
        height:273,
        appId:'5725327496396971875',
        header:true,
        itemId:'bumenTreeQuery',
        title:'部门',
        region:'west',
        split:true,
        collapsible:true,
        collapsed:false,
        border:true,
        sequencenum:0,
        defaultactive:false
    }, {
        x:343,
        width:471,
        height:273,
        appId:'4668943050283255950',
        header:false,
        itemId:'bumenTab',
        title:'12',
        region:'center',
        split:false,
        collapsible:false,
        collapsed:false,
        border:true,
        sequencenum:0,
        defaultactive:false
    } ],
    comNum:0,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.createItems();
    },
    createItems:function() {
        var me = this;
        var appInfos = me.getAppInfos();
        for (var i in appInfos) {
            var id = appInfos[i].appId;
            var callback = me.getCallback(appInfos[i]);
            me.getAppInfoFromServer(id, callback);
        }
    },
    getCallback:function(appInfo) {
        var me = this;
        var callback = function(obj) {
            if (obj.success && obj.appInfo != '') {
                var ModuleOperCode = obj.appInfo.BTDAppComponents_ModuleOperCode;
                var ClassCode = obj.appInfo.BTDAppComponents_ClassCode;
                var cl = eval(ClassCode);
                var callback2 = function(panel) {
                    me.initLink(panel);
                };
                appInfo.callback = callback2;
                var panel = Ext.create(cl, appInfo);
                me.add(panel);
                if (me.panelType == 'Ext.tab.Panel') {
                    if (appInfo.defaultactive) {
                        me.defaultactive = appInfo.itemId;
                    }
                    me.setActiveTab(panel);
                }
            } else {
                appInfo.html = obj.ErrorInfo;
                var panel = Ext.create('Ext.panel.Panel', appInfo);
                me.add(panel);
                if (me.panelType == 'Ext.tab.Panel') {
                    if (appInfo.defaultactive) {
                        me.defaultactive = appInfo.itemId;
                    }
                    me.setActiveTab(panel);
                }
            }
        };
        return callback;
    },
    getAppInfos:function() {
        var me = this;
        var appInfos = me.appInfos;
        for (var i in appInfos) {
            if (appInfos[i].title == '') {
                delete appInfos[i].title;
            } else if (appInfos[i].title == '_') {
                appInfos[i].title = '';
            }
        }
        return Ext.clone(appInfos);
    },
    //获取当前节点下的所有的子节点(不包含当前节点)
    nodevalue:'',//
    findchildnode:function (node){
        var me=this;
        var childnodes = node.childNodes;
        var nd;
        for(var i=0;i<childnodes.length;i++){ 
            //从节点中取出子节点依次遍历
            nd = childnodes[i];
            me.nodevalue += nd.data.Id + ',';
            if(nd.hasChildNodes()){ 
        }
        //判断子节点下是否存在子节点
        me.findchildnode(nd); 
        //如果存在子节点 递归
        }
    },

    initLink:function(panel) {
        var me = this;
        if(Ext.typeOf(me.callback)=='function'){me.callback(me);}
        var appInfos = me.getAppInfos();
        var length = appInfos.length;
        me.comNum++;
        var bmTree = me.getComponent('bumenTreeQuery');
        var bmTab2=me.getComponent('bumenTab');
        var bmzsygList = bmTab2.getComponent('bumenyuangongList');
        var bmxgygList = bmTab2.getComponent('bumenyuangonggxList');
        if (me.comNum == length) {
        bmTree.on({
                select:function(view, record) {
                     //部门的下属部门tab:部门列表只呈现一下级
                    bmTab2.activeTab=0;
                    
                    var nodes=bmTree.getSelectionModel().getSelection();
                    me.nodevalue='';
                    me.findchildnode(nodes[0]);
                    //补上当前选择中的节点
                    me.nodevalue=me.nodevalue+nodes[0].data.Id;
                    var Id ='';
                    if(record!=null){
                        Id = record.get('Id');
                    }
                    if(Id==''||Id==null||Id==undefined){
                        Ext.Msg.alert('提示','请在部门树选择部门后再操作');
                    }
                     //部门直属员工列表tab
                    var bmzshql = 'hremployee.HRDept.Id=' + Id;
                    me.bmzshql=bmzshql;
                    bmzsygList.load(bmzshql);

                   //部门相关员工列表(部门的所有员工列表，包括子部门的员工)
                    //var bmxgyghql = 'hrdeptemp.HRDept.Id=' + Id;
                    var bmxgyghql = 'hrdeptemp.HRDept.Id in(' + me.nodevalue+')';
                    me.bmxgyghql=bmxgyghql;
                    bmxgygList.load(bmxgyghql);
                 
                }
            });
            //部门直属员工列表
         //给打开的员工选择应用(yuangongCheckApp)属性赋值
         bmzsygList.on({
            afterOpenAddWin:function(formPanel){
                //取部门Id
                var node2=bmTree.getValue();
                //外部打开当前表单时传入的应用组件,比如可能是列表或者树
                formPanel.appPanel=bmzsygList;
                formPanel.bmzshql=me.bmzshql;
                var records=bmTree.getSelectionModel().getSelection();
                var node=null;
                if(records&&records.length>0){
                    node=records[0];
                    var hRDeptId=node.data.Id;
                    var hRDeptCName=node.data.text;
                    var hRDeptDataTimeStamp=''+node.data.DataTimeStamp;
                    //给部门员工查询选择添加(yuangongCheckApp)的员工所属部门hRDeptId赋值,hRDeptDataTimeStamp时间戳
                    if(hRDeptId==''||hRDeptId==null||hRDeptId==undefined){
                       Ext.Msg.alert('提示',',获取不到部门树的部门Id值信息,请在部门树选择后再操作！');
                    }else{
                        formPanel.hRDeptId=hRDeptId;
                    }
                    if(hRDeptDataTimeStamp==''||hRDeptDataTimeStamp==null||hRDeptDataTimeStamp==undefined){
                       Ext.Msg.alert('提示',',获取不到部门树的时间戳信息,部门树需要配置时间戳列信息！');
                    }else{
                        formPanel.hRDeptDataTimeStamp=hRDeptDataTimeStamp;
                    }
                }else{
                    node=null;
                } 
            },
            delClick:function(){
                //清空选中的员工的直属部门信息
                var records = bmzsygList.getSelectionModel().getSelection();
                if(records.length > 0){ 
                    var url=''+getRootPath()+'/RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField';
                    var fields='HRDept_Id,Id';
                    Ext.Msg.confirm('提示','本操作只清空员工的直属部门信息,确定要执行操作?',function (button){ 
                        if(button == 'yes'){ 
                            var records = bmzsygList.getSelectionModel().getSelection(); 
                            for(var i in records){ 
                                var hREmployeeId =''+ records[i].get('HREmployee_Id'); 
                                var hRDeptId =0;//清空部门信息需要传哪个值
                                var callback = function(){ 
                                    
                                }; 
                                //员工的直属部门信息清空
                                var HRDept='{Id:'+hRDeptId+'}';
                                var  newAdd= '{'+
                                        'Id:'+hREmployeeId+''+
                                        ',HRDept:'+HRDept+
                                    '}';
                                var obj={'entity':Ext.decode(newAdd),'fields':fields};
                                var params = Ext.JSON.encode(obj);
								//util-POST方式与后台交互
								postToServer(url,params,callback);
                                if(i==records.length-1){
                                    //部门直属员工列表更新
                                    var records=bmTree.getSelectionModel().getSelection();
                                    if(records!=null){
                                        var Id ='';
                                        var record=records[0];
                                        if(record!=null){
                                            Id =''+record.get('Id');
                                            var bmxshql = 'hremployee.HRDept.Id=' + Id;
                                            bmzsygList.load(bmxshql);
                                        }
                                    }
                                }
                            }  
                        } 
                    }); 
                }else{
                  Ext.Msg.alert('提示','请选择数据进行操作！');
                }
            }
         });
         //部门相关员工列表
         //给打开的员工选择应用(yuangongCheckApp)属性赋值
         bmxgygList.on({
            afterOpenAddWin:function(formPanel){
                //外部打开当前表单时传入的应用组件,比如可能是列表或者树
                formPanel.appPanel=bmxgygList;
                formPanel.bmxgyghql=me.bmxgyghql;
                //取部门Id
                var node2=bmTree.getValue();
                var records=bmTree.getSelectionModel().getSelection();
                var node=null;
                if(records&&records.length>0){
                    node=records[0];
                    var hRDeptId=node.data.Id;
                    var hRDeptCName=node.data.text;
                    var hRDeptDataTimeStamp=''+node.data.DataTimeStamp;
                    //给部门员工查询选择添加(yuangongCheckApp)的员工所属部门hRDeptId赋值,hRDeptDataTimeStamp时间戳
                    if(hRDeptId==''||hRDeptId==null||hRDeptId==undefined){
                       Ext.Msg.alert('提示',',获取不到部门树的部门Id值信息,请在部门树选择后再操作！');
                    }else{
                        formPanel.hRDeptId=hRDeptId;
                    }
                    if(hRDeptDataTimeStamp==''||hRDeptDataTimeStamp==null||hRDeptDataTimeStamp==undefined){
                       Ext.Msg.alert('提示',',获取不到部门树的时间戳信息,部门树需要配置时间戳列信息！');
                    }else{
                        formPanel.hRDeptDataTimeStamp=hRDeptDataTimeStamp;
                    }
                }else{
                    node=null;
                }
            }
         });
        }
    },
    getAppInfoFromServer:function(id, callback) {
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
                    if (Ext.typeOf(callback) == 'function') {
                        var obj = {
                            success:false,
                            ErrorInfo:'没有获取到应用组件信息!'
                        };
                        if (appInfo != '') {
                            obj = {
                                success:true,
                                appInfo:appInfo
                            };
                        }
                        callback(obj);
                    }
                } else {
                    if (Ext.typeOf(callback) == 'function') {
                        var obj = {
                            success:false,
                            ErrorInfo:'获取应用组件信息失败！错误信息' + result.ErrorInfo + ''
                        };
                        callback(obj);
                    }
                }
            },
            failure:function(response, options) {
                if (Ext.typeOf(callback) == 'function') {
                    var obj = {
                        success:false,
                        ErrorInfo:'获取应用组件信息请求失败！'
                    };
                    callback(obj);
                }
            }
        });
    }
});