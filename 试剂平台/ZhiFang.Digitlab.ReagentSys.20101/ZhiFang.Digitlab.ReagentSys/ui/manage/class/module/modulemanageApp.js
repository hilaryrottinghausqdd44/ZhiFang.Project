/***
 * 模块管理
 */
Ext.Loader.setConfig({enabled: true});
Ext.Loader.setPath('Ext.zhifangux', getRootPath()+'/ui/zhifangux');
Ext.Loader.setPath('Ext.ux', getRootPath()+'/ui/extjs/ux');

Ext.Loader.setPath('Ext.manage.module.rightApp', getRootPath() + '/ui/manage/class/module/rightApp.js');

Ext.ns('Ext.manage');
Ext.define('Ext.manage.module.modulemanageApp', {
    extend:'Ext.panel.Panel',
    alias:'widget.modulemanageApp',
    requires:[ "Ext.ux.CheckColumn" ],
    header:false,
    /**
     * 外面传入的应用组件ID
     * 在openAppComponentsTreeWin里有测试数据
     */
    appId:-1,
    layout: 'border',
    moduleId:'',
    moduleCName:'',
    
    fields:
    [ 
    'RBACModuleOper_UseCode', 'RBACModuleOper_CName', 
    'RBACModuleOper_Comment', 'RBACModuleOper_IsUse',
    'RBACModuleOper_DispOrder','RBACModuleOper_DefaultChecked',
    'RBACModuleOper_RBACModule_CName', 'RBACModuleOper_RBACModule_IsUse',
    'RBACModuleOper_RBACModule_DispOrder', 'RBACModuleOper_RBACModule_Id',
    'RBACModuleOper_RBACModule_DataTimeStamp', 'RBACModuleOper_Id',
    'RBACModuleOper_DataTimeStamp', 'RBACModuleOper_LabID' ,
    'RBACModuleOper_BTDAppComponentsOperate_Id','RBACModuleOper_BTDAppComponentsOperate_DataTimeStamp'
    ],
    queryFields:
    'RBACModuleOper_UseCode, RBACModuleOper_CName, RBACModuleOper_Comment, RBACModuleOper_IsUse,' +
    'RBACModuleOper_DispOrder, RBACModuleOper_DefaultChecked,RBACModuleOper_RBACModule_CName,' +
    ' RBACModuleOper_RBACModule_IsUse,RBACModuleOper_RBACModule_DispOrder, RBACModuleOper_RBACModule_Id,' +
    'RBACModuleOper_RBACModule_DataTimeStamp, RBACModuleOper_Id,RBACModuleOper_DataTimeStamp, RBACModuleOper_LabID ,' +
    'RBACModuleOper_BTDAppComponentsOperate_Id,RBACModuleOper_BTDAppComponentsOperate_DataTimeStamp',
    
    getServerUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true',
     
    createItems:function(){
       var me=this;;
       var items=[{
        header:false,
        split:true,
        autoScroll :true,
        region:'west',
        xtype: 'modulegridtreeapp',
        isWindow:false,//不打开弹出窗体
        itemId:'modulegridtree',//
        width: 315,
        layout: 'fit'
    },{
        xtype:'rightApp',
        itemId:'rightApp',
        name:'rightApp',
        region:'center',
        header:false,
        split:true,
        layout: 'border',
        collapsible:true,
        collapsed:false
    }];
    return items;
    },

    /**
     * 初始化
     */
    initComponent:function(){
        var me = this;
        Ext.Loader.setPath('Ext.manage',getRootPath()+'/ui/manage/class/module');
        me.items=me.createItems();
        me.callParent(arguments);
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        } 
        me.initLink();
    },
    /***
     * 联动处理
     */
    initLink:function() {
        var me=this;
        var modulegridtre=me.getmodulegridtree();
        var moduleform = me.getmoduleform();
        var moduleOper = me.getmoduleOper();
        modulegridtre.isWindow=false;//不打开弹出窗体
        modulegridtre.on({
                load:function(treeStore, node,records,successful,eOpts){
                    //更新树数据的状态标志
                },
                addClick:function(grid, rowIndex, colIndex, item, e, record) {
	                    var node = null;
	                    //更新树数据的状态标志
	                    modulegridtre.isWindow=false;//不打开弹出窗体
	                    moduleform.type = 'add';
                        moduleform.isAdd();
	                    if (record && record!=null) {
                            //如果url为空,该节点为父容器,可以添加子节点,如果url不为空,不能添加子节点
		                    var url=''+record.get("Url");
		                    if(url!=""&&url.length>0){
		                        Ext.Msg.alert("提示","当前节点不能添加子节点");
		                        return ;
		                    }else{
	                            
		                        node = record;
		                        var ParentID='';
		                        var parentNode = null;
			                    if(node.data.Id.toString()=='0'){
			                        parentNode = node;//根节点
		                            ParentID='0';
			                    }else{
		                           parentNode = node;
		                           ParentID=''+parentNode.data.Id;
		                           if(ParentID==''||ParentID=='undefined'){
		                                ParentID='0';
		                           }
		                        }
		                        moduleform.Id=node.get('Id'),
		                        moduleform.ParentID=''+ParentID;
		                        moduleform.LevelNum=parseInt(node.get('LevelNum'))+1;
		                        moduleform.TreeCatalog=parseInt(node.get('TreeCatalog'))+1;
		                        moduleform.ParentName=parentNode.data.text;
		                        
		                        var parentModuleName=moduleform.getComponent('parentModuleName');
		                        if(parentModuleName){
			                       parentModuleName.setValue(parentNode.data.text);
		                        }
			                    var parentIDCom=moduleform.getComponent('ParentID');
		                        if(parentIDCom){
			                       parentIDCom.setValue(ParentID);
		                        }
		                        
		                        var moduleType=node.data.ModuleType;//模块类型
		                        if(moduleType=='1'){//非构建
		                            var moduleTypeCom=moduleform.getComponent('ModuleType');
		                            if(moduleTypeCom){
		
			                        }
		                        }else{
		                            var moduleTypeCom=moduleform.getComponent('ModuleType');
		                            if(moduleTypeCom){
		                            }
			                       
		                            var appId=me.getServerLists(node.get('Id'));
		                            if(appId==null||appId==''){
		                               moduleOper.appId="";
		                               moduleOper.moduleId='-1';
		                               moduleOper.load('');
		                                
		                            }else{
			                            if(appId==null||appId==''){
		                                    moduleOper.appId="";
		                                    moduleOper.moduleId='';
		                                    moduleOper.load('');
			                            }else{
			                                moduleOper.appId=appId;//外面传入的模块应用ID
			                                //moduleOper.appDataTimeStamp=node.get('RBACModule_BTDAppComponents_DataTimeStamp');//外面传入的模块应用
			                                moduleOper.moduleId=node.get('Id');
			                                moduleOper.moduleCName=node.data.text;
			                                moduleOper.moduleDataTimeStamp=''+node.data.DataTimeStamp;
			                                moduleOper.load('');
			                            }
		                            }
		                        }
                           }
	                    } else {
	                        node = null;
	                        moduleform.Id='-1',
	                        moduleform.ParentID='0';
	                        moduleform.tid='0';
	                        moduleform.LevelNum=1;
	                        moduleform.TreeCatalog=1;
	                        moduleform.ParentName='所有模块';
	                        var moduleTypeCom=moduleform.getComponent('ModuleType');
	                        if(moduleTypeCom){
	                           moduleTypeCom.setValue('0');
	                        }
	                        var parentModuleName=moduleform.getComponent('parentModuleName');
	                        if(parentModuleName){
	                            parentModuleName.setValue('所有模块');
	                        }
	                        var parentIDCom=moduleform.getComponent('ParentID');
	                        if(parentIDCom){
	                            parentIDCom.setValue('0');
	                        }
	                    }
	                    moduleOper.appId="";
	                    moduleOper.moduleId='-1';
	                    moduleOper.load('');
                    
                },
                editClick:function(grid, rowIndex, colIndex, item, e, record) {
                    var node = null;
                    if (record && record) {
                        node = record;
                        var ParentID='';
                        var parentNode = null;
                        if(node.data.Id.toString()=='0'){
                            parentNode = node;//根节点
                            ParentID='0';
                        }else if(node.data.ParentID.toString()=='0'){//当前选择节点父节点值为0(根节点)
                           parentNode = node.parentNode;
                           ParentID='0';
                        }else{
                           parentNode = node.parentNode;
                           ParentID=''+parentNode.data.Id;
                           if(ParentID==''||ParentID=='undefined'){
                                ParentID='0';
                            }
                        }
	                    modulegridtre.isWindow=false;//不打开弹出窗体
	                    moduleform.type='edit';
	                
	                    moduleform.Id=node.get('Id'),
	                    moduleform.ParentID=ParentID;
	                    moduleform.LevelNum=parseInt(node.get('LevelNum'))+1;
	                    moduleform.TreeCatalog=parseInt(node.get('TreeCatalog'))+1;
	                    moduleform.ParentName=parentNode.data.text;
	                    moduleform.isEdit(node.get('Id'));
	                    //上级节点处理
	                    var parentModuleName=moduleform.getComponent('parentModuleName');
	                    if(parentModuleName){
	                       parentModuleName.setValue(parentNode.data.text);
	                    }
	                    var parentIDCom=moduleform.getComponent('ParentID');
	                    if(parentIDCom){
	                       parentIDCom.setValue(ParentID);
	                    }
                        //模块应用Id
                        var moduleType=node.data.ModuleType;//模块类型
                        var moduleTypeCom=moduleform.getComponent('ModuleType');
                        if(moduleType=='1'){//非构建
                            moduleOper.appId="";
                            moduleOper.moduleId='-1';
                            moduleOper.load('');
                            //模块应用ID
                            var appId=me.getServerLists(node.get('Id'));
                            if(appId==null||appId==''){
                                moduleOper.appId="";
                                moduleOper.moduleId='-1';
                                moduleOper.load('');
                            }else{
                                moduleOper.appId=appId;//外面传入的模块应用ID
                                //moduleOper.appDataTimeStamp=node.get('RBACModule_BTDAppComponents_DataTimeStamp');//外面传入的模块应用
                                moduleOper.moduleId=node.get('Id');
                                moduleOper.moduleCName=node.data.text;
                                moduleOper.moduleDataTimeStamp=''+node.data.DataTimeStamp;
                                moduleOper.load('');
                            }
                        }
                    } else {
                        moduleOper.appId="";
                        moduleOper.moduleId='';
                        moduleOper.load('');
                        node = null;
                        Ext.Msg.alert("提示","请在模块树选择一个节点后再操作！");
                        return;
                    }
                    
                },
                showClick:function(grid, rowIndex, colIndex, item, e, record) {
                    modulegridtre.isWindow=false;//不打开弹出窗体
                    var node = null;
                    if (record && record!=null) {
                        node =record;
                        var parentNode = null;
                   
	                    if(node.data.Id.toString()=='0'){
	                        parentNode = node;//根节点
	                    }else{
	                       parentNode = node.parentNode;
	                    }
	                    moduleform.type='show';
	                    moduleform.Id=node.get('Id'),
	                    moduleform.ParentID=parentNode.data.Id;
	                    moduleform.LevelNum=parseInt(node.get('LevelNum'))+1;
	                    moduleform.TreeCatalog=parseInt(node.get('TreeCatalog'))+1;
	                    moduleform.ParentName=parentNode.data.text;
	                    moduleform.isShow(node.get('Id'));
	                    
	                    var parentModuleName=moduleform.getComponent('parentModuleName');
	                    if(parentModuleName){
	                       parentModuleName.setValue(parentNode.data.text);
	                    }
	                    var parentIDCom=moduleform.getComponent('ParentID');
	                    if(parentIDCom){
	                       parentIDCom.setValue(parentNode.data.Id);
	                    }
                        var moduleType=''+node.data.ModuleType;//模块类型
                        if(moduleType=='1'){//非构建
                            moduleOper.appId="";
                            moduleOper.moduleId='-1';
                            moduleOper.load('');
                        }else{
                            var appId=me.getServerLists(node.get('Id'));
                            if(appId==null||appId==''){
                                moduleOper.appId="";
                                moduleOper.moduleId='-1';
                                moduleOper.load('');
                                //Ext.Msg.alert('提示','没有获取到该模块的应用ID信息,请先维护好模块信息再往下操作');
                            }else{
                                moduleOper.appId=appId;//外面传入的模块应用ID
                                //moduleOper.appDataTimeStamp=node.get('RBACModule_BTDAppComponents_DataTimeStamp');//外面传入的模块应用
                                moduleOper.moduleId=node.get('Id');
                                moduleOper.moduleCName=node.data.text;
                                moduleOper.moduleDataTimeStamp=''+node.data.DataTimeStamp;
                                moduleOper.load('');
                            }
                        }
                    } else {
                        moduleOper.appId="";
                        moduleOper.moduleId='-1';
                        moduleOper.load('');
                        node = null;
                        Ext.Msg.alert("提示","请在模块树选择一个节点后再操作！");
                        return;
                    }
                    
                },
                select:function(view, record) {
                    var records = modulegridtre.getSelectionModel().getSelection();
                    var node = null;
                    if (records && records.length > 0) {
                        node = records[0];
                        var ParentID='';
                        var parentNode = null;
                        if(node.data.Id.toString()=='0'){
                            parentNode = node;//根节点
                            ParentID='0';
                        }else if(node.data.ParentID.toString()=='0'){//当前选择节点父节点值为0(根节点)
                           parentNode = node.parentNode;
                           ParentID='0';
                        }else{
                           parentNode = node.parentNode;
                           ParentID=''+parentNode.data.Id;
                           if(ParentID==''||ParentID=='undefined'){
                                ParentID='0';
                            }
                        }
	                       
	                    modulegridtre.isWindow=false;//不打开弹出窗体
	                    moduleform.type='edit';
	                    moduleform.Id=node.get('Id'),
	                    moduleform.ParentID=ParentID;
	                    moduleform.LevelNum=parseInt(node.get('LevelNum'))+1;
	                    moduleform.TreeCatalog=parseInt(node.get('TreeCatalog'))+1;
	                    moduleform.ParentName=parentNode.data.text;
	                    moduleform.isEdit(record.get('Id'));
	                    
	                    var parentModuleName=moduleform.getComponent('parentModuleName');
	                    if(parentModuleName){
	                       parentModuleName.setValue(parentNode.data.text);
	                    }
	                    var parentIDCom=moduleform.getComponent('ParentID');
	                    if(parentIDCom){
	                       parentIDCom.setValue(ParentID);
	                    }
                        var moduleType=''+node.data.ModuleType;//模块类型
                        if(moduleType=='1'){//非构建
                            moduleOper.appId="";
                            moduleOper.moduleId='-1';
                            moduleOper.load('');
                        }else{
                            var appId=me.getServerLists(node.get('Id'));
                            if(appId==null||appId==''){
                                moduleOper.appId="";
                                moduleOper.moduleId='-1';
                                moduleOper.load('');
                                //Ext.Msg.alert('提示','没有获取到该模块的应用ID信息,请先维护好模块信息再往下操作');
                            }else{
			                    moduleOper.appId=appId;//外面传入的模块应用ID
		                        //moduleOper.appDataTimeStamp=node.get('RBACModule_BTDAppComponents_DataTimeStamp');//外面传入的模块应用
			                    moduleOper.moduleId=node.get('Id');
			                    moduleOper.moduleCName=node.data.text;
			                    moduleOper.moduleDataTimeStamp=''+node.data.DataTimeStamp;
			                    moduleOper.load('');
                            }
                        }
                    } else {
                        moduleOper.appId="";
                        moduleOper.moduleId='-1';
                        moduleOper.load('');
                        node = null;
                        Ext.Msg.alert("提示","请在模块树选择一个节点后再操作！");
                        return;
                    }
                    
                }
        });
        moduleform.on({
            saveClick:function(){
	            if(moduleform.isSuccess==true){
	               modulegridtre.load();
	            }
            },
            saveAsClick:function(){
                if(moduleform.isSuccess==true){
                   modulegridtre.load();
                }
            }
        });
    },
    /***
     * 获取数据集
     */
    getServerLists:function(moduleId){
        var me=this;
        var arrLists=[];
        var appId="";
        var myUrl=getRootPath()+"/RBACService.svc/RBAC_UDTO_SearchRBACModuleByHQL?isPlanish=true&fields=RBACModule_BTDAppComponents_CName,RBACModule_BTDAppComponents_Id,RBACModule_Id,RBACModule_BTDAppComponents_DataTimeStamp&page=1&start=0&limit=100";
        
        myUrl=myUrl+"&where=rbacmodule.Id='"+moduleId+"'";
        //查询数据过滤条件行记录
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            method:'GET',
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    var ResultDataValue = {count:0,list:[]};
                    if(result['ResultDataValue'] && result['ResultDataValue'] != ""){
                        ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
                        
                        if(ResultDataValue.list.length>0){
                            appId=ResultDataValue.list[0]["RBACModule_BTDAppComponents_Id"];
                        }else{
                            appId="";
                        }
                    }
                }else{
                    appId="";
                }
            },
            failure : function(response,options){
                 appId="";
            }
        });
        return appId;
    },
    /***
     * 获取左模块树
     */
    getmodulegridtree:function() {
        var me=this;
        var modulegridtree = me.getComponent('modulegridtree');
        return  modulegridtree;
    },
    /***
     * 获取表单
     */
    getmoduleform:function() {
        var me=this;
        var right = me.getComponent('rightApp');
        var moduleform = right.getComponent('moduleformapp');
        return moduleform;
    },
    /***
     * 获取模块操作列表
     */
    getmoduleOper:function() {
        var me=this;
        var right = me.getComponent('rightApp');
        var moduleoper = right.getComponent('moduleoperapp');
        return moduleoper;
    }
});