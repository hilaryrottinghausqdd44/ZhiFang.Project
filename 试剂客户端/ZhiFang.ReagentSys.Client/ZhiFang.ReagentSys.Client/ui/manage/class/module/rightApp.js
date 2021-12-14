/***
 * 右区域
 */

Ext.ns('Ext.manage');
Ext.define('Ext.manage.module.rightApp', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.rightApp',
    /***
     * 模块树选中的行
     * @type String
     */
    moduleRecord:'',
    moduleId:'',
    layout: 'border',
    height:520,
    width:680,
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    comNum:0,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if(me.moduleId && me.moduleId != ""){me.load(me.moduleId);}
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    /***
     * 对外分开,加载表单及模块操作列表信息
     * AppComponentsId:模块的应用Id
     * AppComponentsDataTimeStamp:模块的应用时间戳
     * @param {} obj对象内容{Id:'',text:'',ParentID:'',ParentName:'',LevelNum:0,TreeCatalog:0,AppComponentsId:'',AppComponentsDataTimeStamp:'',DataTimeStamp:''}
     */
    load:function(obj) {
        var me=this;
        var type = Ext.typeOf(obj);
        if(type == 'string'){
            me.loadForModuleId(obj);
        }else if(type == 'object'){
            me.loadOne(obj);
        }
    },
    loadForModuleId:function(moduleId) {
        var me=this;
        var clists=[];
        var chqlWhere='rbacmodule.Id='+moduleId;
        var mycUrl=me.selectRBACModuleUrl+"&fields="+me.fieldsCModule;
        clists=me.getServerLists(mycUrl,chqlWhere);
        var plists=[];
        var obj={};
        if(clists&&clists.length>0){
            var obj2=clists[0];
            var pid=obj2["RBACModule_ParentID"];
            var id=obj2["RBACModule_Id"];
            var LevelNum=obj2["RBACModule_LevelNum"];
            var IsLeaf=obj2["RBACModule_IsLeaf"];
            var text=obj2["RBACModule_CName"];
            var TreeCatalog=obj2["RBACModule_TreeCatalog"];
            var DataTimeStamp=obj2["RBACModule_DataTimeStamp"];
            var AppComponentsId=obj2["RBACModule_BTDAppComponents_Id"];
            var AppComponentsDataTimeStamp=obj2["RBACModule_BTDAppComponents_DataTimeStamp"];
            obj={
	            Id:id,
                text:text,
	            ParentID:'',ParentName:'',
	            LevelNum:LevelNum,
	            TreeCatalog:TreeCatalog,
	            AppComponentsId:AppComponentsId,
	            AppComponentsDataTimeStamp:AppComponentsDataTimeStamp,
	            DataTimeStamp:DataTimeStamp
            };
            
            if(pid==""||pid=='0'){
                obj.ParentID='0';
                obj.ParentName='';
            }else{
                var phqlWhere='rbacmodule.Id='+moduleId;
                var mypUrl=me.selectRBACModuleUrl+"&fields="+me.fieldsPModule;
                plists=me.getServerLists(mypUrl,phqlWhere);
                if(plists&&plists.length>0){
                    var obj3=plists[0];
                    obj.ParentID=obj3["RBACModule_Id"];
                    obj.ParentName=obj3["RBACModule_CName"];;
                }else{
                    obj.ParentID='0';
                    obj.ParentName='';
                }
            }
        }
        if(obj["Id"]!=""){
            me.loadOne(obj);
        }
        
    },
    /***
     * 依模块Id查询模块信息
     * @type 
     */
    selectRBACModuleUrl:getRootPath() +"/RBACService.svc/RBAC_UDTO_SearchRBACModuleByHQL?isPlanish=true",
    /***
     * 模块子节点信息
     * @type String
     */
    fieldsCModule:"RBACModule_ParentID,RBACModule_LevelNum,RBACModule_IsLeaf,RBACModule_CName,RBACModule_Id,RBACModule_DataTimeStamp,RBACModule_TreeCatalog,RBACModule_BTDAppComponents_Id,RBACModule_BTDAppComponents_DataTimeStamp",
    /***
     * 模块父节点信息
     * @type String
     */
    fieldsPModule:"RBACModule_Id,RBACModule_CName",
    
    /***
     * 获取数据集
     * 1.依操作对象(样本单)ID查询样本状态类型表
     * 2.依操作对象ID(样本单)查询样本操作信息列表
     * 3.查询样本操作对象类型信息列表
     * 4.查询样本操作类型(录入,采样,接收,签收)
     * 5.查询样本状态类型信息列表
     */
    getServerLists:function(url,hqlWhere){
        var me=this;
        var arrLists=[];
        var myUrl="";
        if(hqlWhere&&hqlWhere!=null){
            myUrl=url+'&where='+hqlWhere;
        }else{
            myUrl=url;
        }
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
                    arrLists=ResultDataValue.list;
                }
                var count = ResultDataValue['count'];
                }else{
                    arrLists=[];
                }
            },
            failure : function(response,options){
                 arrLists=[];
            }
        });
        return arrLists;
    },
    
    /***
     * 内部,加载表单及模块操作列表信息
     * AppComponentsId:模块的应用Id
     * AppComponentsDataTimeStamp:模块的应用时间戳
     * @param {} obj对象内容{Id:'',text:'',ParentID:'',ParentName:'',LevelNum:0,TreeCatalog:0,AppComponentsId:'',AppComponentsDataTimeStamp:'',DataTimeStamp:''}
     */
    loadOne:function(obj) {
        var me=this;
        //var node = null;
        if (obj!="") {
            var moduleform = me.getmoduleform();
            var moduleOper = me.getmoduleOper();
            var ParentID='';
            var parentNode = null;
            if(obj['Id']=='0'){
                ParentID='0';
            }else if(obj['ParentID']=='0'){//当前选择节点父节点值为0(根节点)
               ParentID='0';
            }else{
               ParentID=obj['ParentID'];
               if(ParentID==''||ParentID=='undefined'){
                    ParentID='0';
                }
            }
            moduleform.type='edit';
            moduleform.Id=obj['Id'],
            moduleform.ParentID=ParentID;
            moduleform.LevelNum=parseInt(obj['LevelNum'])+1;
            moduleform.TreeCatalog=parseInt(obj['TreeCatalog'])+1;
            moduleform.ParentName=obj['ParentName'];
            moduleform.isEdit(obj['Id']);
            //上级节点处理
            var parentModuleName=moduleform.getComponent('parentModuleName');
            if(parentModuleName){
               parentModuleName.setValue(obj['ParentName']);
            }
            var parentIDCom=moduleform.getComponent('ParentID');
            if(parentIDCom){
               parentIDCom.setValue(ParentID);
            }
            //模块应用Id
            var moduleType=obj["ModuleType"];//模块类型
            var moduleTypeCom=moduleform.getComponent('ModuleType');
            if(moduleType=='1'){//非构建
                moduleOper.appId="";
                moduleOper.moduleId='-1';
                moduleOper.load('');
            }else{
                var appId=obj['AppComponentsId'];
                if(appId==null||appId==''){
                    moduleOper.appId="";
                    moduleOper.moduleId='-1';
                    moduleOper.load('');
                }else{
                    //moduleOper.setDisabled(false);
                    moduleOper.appId=appId;//外面传入的模块应用ID
                    moduleOper.appDataTimeStamp=obj['AppComponentsDataTimeStamp'];//外面传入的模块应用
                    moduleOper.moduleId=obj['Id'];
                    moduleOper.moduleCName=obj['text'];
                    moduleOper.moduleDataTimeStamp=''+obj['DataTimeStamp'];
                    moduleOper.load('');
                }
            }
        } else {
            moduleOper.appId="";
            moduleOper.moduleId='-1';
            moduleOper.load('');
            Ext.Msg.alert("提示","请在模块树选择一个节点后再操作！");
            return;
        }
    },
    /***
     * 
     */
    getmoduleform:function(){
        var me=this;
        var com=me.getComponent('moduleformapp');
        return com;
    },
    /***
     * 
     */
    getmoduleOper:function(){
        var me=this;
        var com=me.getComponent('moduleoperapp');
        return com;
    },
    createItems:function() {
        var me = this;
        var moduleformapp=Ext.create('Ext.manage.module.ModuleFormApp',{
            itemId:'moduleformapp',
            name:'moduleformapp',
            border:false,
            autoScroll :true,
            split:true,
            height:300,
            header:false,
            region:'north'
        });
        var moduleoperapp=Ext.create('Ext.manage.module.ModuleOperApp',{
            itemId:'moduleoperapp',
            split:true,
            border:false,
            //height:320,
            itemId:'moduleoperapp',
            name:'moduleoperapp',
            region:'center',
            header:false,
            autoScroll :true
        });
        var appInfos = [moduleformapp,moduleoperapp];   
      return appInfos;
    },
    /**
     * 初始化
     */
    initComponent:function(){
        var me = this;
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.zhifangux.DateField',getRootPath()+'/ui/zhifangux/DateField.js');
        Ext.Loader.setPath('Ext.manage.module.ModuleTree', getRootPath() + '/ui/manage/class/module/ModuleTree.js');
        Ext.Loader.setPath('Ext.build.AppListPanel', getRootPath() + '/ui/build/class/AppListPanel.js');
        
        Ext.Loader.setPath('Ext.manage.module.ModuleFormApp', getRootPath() + '/ui/manage/class/module/ModuleFormApp.js');
        Ext.Loader.setPath('Ext.manage.module.ModuleOperApp', getRootPath() + '/ui/manage/class/module/ModuleOperApp.js');
        me.items=me.createItems();
        me.callParent(arguments);
    },
    initLink:function() {
        var me = this;
        
    }
});