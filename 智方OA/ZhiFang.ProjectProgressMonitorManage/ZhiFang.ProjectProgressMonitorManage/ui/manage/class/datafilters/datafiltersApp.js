/***
 * 数据过滤条件APP
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.datafiltersApp', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.datafiltersApp',
    title:'数据过滤条件配置',
    header:false,
    border:false,
    //width:680,
    /**默认加载数据时启用遮罩层*/
    hasLoadMask:true,
    layout:{
        type:'border',
        regionWeights:{
            north:4,
            south:3,
            west:2,
            east:1
        }
    },
    /***
     * 模块操作的信息
     * moduleCopy:{Id:模块操作id,CName:模块操作名称}
     * modulePaste:{Id:'',CName:''},//粘贴模块操作
     * objectCName:选择中的模块操作对象名称
     * moduleCopyCName:"",//复制模块中文名称
     * modulePasteCName:""//粘贴模块中文名称
     * @type 
     */
    operateInfo:{
        moduleCopy:{Id:'',CName:''},//复制模块操作
        modulePaste:{Id:'',CName:''},//粘贴模块操作{Id:'',CName:''}
        objectCName:"",
        moduleCopyCName:"",//复制模块中文名称
        modulePasteCName:""//粘贴模块中文名称
    },
    /***
     * 是否隐藏预定义属性按钮
     * @type Boolean
     */
    isShowPredefinedAttributes:false,
    /***
     * 是否隐藏复制按钮
     * @type Boolean
     */
    isShowCopy:false,
    /***
     * 是否隐藏粘贴按钮
     * @type Boolean
     */
    isShowPaste:true,
    /***
     * 模块操作选中行的索引
     * @type Number
     */
    moduleOperIndex:0,
    /***
     * 模块操作选中行的ID
     * @type Number
     */
    moduleOperId:0,
    /***
     * 模块树选中节点的ID
     * @type Number
     */
    moduleTreeId:0,
    /***
     * 角色权限关系表数据查询服务
     * 查询角色id,角色名称,角色时间戳
     * 查询数据过滤条件id,数据过滤条件名称,数据过滤条件时间戳
     * @type String
     */
    getRoleRightServerUrl:getRootPath() +'/RBACService.svc/RBAC_UDTO_SearchRBACRoleRightByHQL?isPlanish=true',
    /***
     * 角色权限关系表数据查询服务的查询字段
     * @type 
     */
    fieldsRoleRight:'RBACRoleRight_Id,RBACRoleRight_DataTimeStamp,RBACRoleRight_RBACRole_Id,RBACRoleRight_RBACRole_DataTimeStamp,RBACRoleRight_RBACRowFilter_Id,RBACRoleRight_RBACRowFilter_DataTimeStamp,RBACRoleRight_RBACRole_CName,RBACRoleRight_RBACRowFilter_CName,RBACRoleRight_RBACModuleOper_Id,RBACRoleRight_RBACModuleOper_DataTimeStamp',
    
    /***
     * 角色权限更新服务
     * 更新行过滤条件关系
     * @type 
     */
    editRoleRightServerUrl:getRootPath() +'/RBACService.svc/RBAC_UDTO_UpdateRBACRoleRightByField',
    editfieldsRoleRight:'Id,RBACRowFilter',
    editfieldsAllRoleRight:'Id,RBACRowFilter,RBACRole,RBACModuleOper',
    /***
     * 是否开启数据权限
     * @type Boolean
     */
    isUseRowFilter:false,
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    deleteRoleRightUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_DelRBACRoleRight',
    /***
     * 删除行过滤条件
     * @type 
     */
    deleteRBACRowFilterUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_DelRBACRowFilter',
    afterRender:function() {
        var me = this;
        me.initLink();
        me.callParent(arguments);
    },
    /**
     * 初始化
     */
    initComponent:function(){
        var me = this;
        me.items=me.createItems();
        me.callParent(arguments);
    },
  
    initLink:function() {
        var me = this;
        //1.左区域为模块树连动中间模块操作列表
        var moduleTree=me.getmoduleTree();
        var rightApp=me.getrightApp();
        var moduleOper=me.getmoduleOperLists();
        //行过滤条件角色树
        var rdfTree=me.getroleDataFiltersTree();
        var form=me.getdatafiltersForm();
        //先禁用
        if(form){
            form.disableControl();
        }
        if(rdfTree){
            rdfTree.setDisabled(true);
        }
        if(moduleTree){
            moduleTree.on({
                select:function( rowModel,record, index, eOpts ){
                    if(record){
                        moduleTree.selectRecord=record;
                        rightApp.moduleRecord==record;
                        var moduleId=record.get("Id");
                        rightApp.moduleId==record.get("Id");;
                        rightApp.load(moduleId);
                    }
                    
                    
                    form.setisUseRowFilter(false);
                    if(rdfTree){
                        
	                    //清空
	                    rdfTree.load(0);
	                }
                }
            });
        }
    },
    /***
     * 获取角色权限数据集
     */
    getRoleRightLists:function(obj,type){
        var me=this;
        var roleRightLists=[];
        var hqlWhere='';
        if(type=='delete'){
            hqlWhere='rbacroleright.RBACModuleOper.Id='+obj.moduleOperId+ ' and rbacroleright.RBACRowFilter.Id='+obj.rowId;
        }else if(type=='edit'){
            hqlWhere='rbacroleright.RBACModuleOper.Id='+obj.moduleOperId+ ' and rbacroleright.RBACRowFilter.Id='+obj.rowId;
            hqlWhere=hqlWhere+(' and rbacroleright.RBACRole.Id='+obj.roleId);
        }
        var myUrl=me.getRoleRightServerUrl+'&fields='+me.fieldsRoleRight+'&where='+hqlWhere;
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
                    roleRightLists=ResultDataValue.list;
                }
                var count = ResultDataValue['count'];
	            }else{
	                roleRightLists=[];
	            }
	        },
	        failure : function(response,options){
	             roleRightLists=[];
	        }
	    });
        return roleRightLists;
    },

    /***
     * 更新角色权限数据对象
     */
    updateRoleRight:function(obj,type){
        var me=this;
        var editServerUrl=me.editRoleRightServerUrl;
        var editfields=me.editfieldsRoleRight;
        if(type=='part'){
            editfields=me.editfieldsRoleRight;
        }else if(type=='all'){
            editfields=me.editfieldsAllRoleRight;
        }
        var obj2={'entity':obj,'fields':editfields};
        var params = Ext.JSON.encode(obj2);
        var callback=function(responseText){   
	        var result = Ext.JSON.decode(responseText);
            var isSuccess=result.success;
         };
        //util-POST方式与后台交互
        var defaultPostHeader='application/json';
        var async=false;
        postToServer(editServerUrl,params,callback,defaultPostHeader,async);
    },
    /***
     * 删除角色权限数据对象
     */
    deleteRoleRight:function(id){
        var me=this;
        var result=true;
        var deleteServerUrl=me.deleteRoleRightUrl+'?id='+id;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
	        url:deleteServerUrl,
	        async:false,//非异步
	        method:'GET',
	        success:function(response,opts){
                var result = Ext.JSON.decode(response);
                var isSuccess=result.success;
	            result=isSuccess;
	        },
	        failure : function(response,options){
	            result=true;
	        }
        });
        return result;
    },
    /***
     * 删除行过滤条件数据对象
     */
    deleteRBACRowFilter:function(id){
        var me=this;
        var deleteServerUrl=me.deleteRBACRowFilterUrl+'?id='+id;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url:deleteServerUrl,
            async:false,//非异步
            method:'GET',
          success:function(response,opts){
              var result = Ext.JSON.decode(response);
              var isSuccess=result.success;
              if(isSuccess){
                 Ext.Msg.alert('提示','删除行过滤条件成功');
              }
          },
          failure : function(response,options){
              Ext.Msg.alert('提示','删除行过滤条件失败');
          }
        });
    },
    /**
     * 打开某角色下的员工信息列表应用窗口
     * @private
     * @param {} title
     * @param {} ClassCode
     * @param {} id
     */
    openAppShowWin:function(hqlWhere){
        var me = this;
        var panel = 'Ext.manage.datafilters.empRolesLists';
        var maxHeight = document.body.clientHeight*0.98;
        var maxWidth = document.body.clientWidth*0.98;
        var win = Ext.create(panel,{
            id:-1,
            internalWhere:'',
            externalWhere:hqlWhere,
            maxWidth:maxWidth,
            autoScroll:true,
            modal:true,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            resizable:true,//可变大小
            draggable:true//可移动
        });
        
        if(win.height > maxHeight){
            win.height = maxHeight;
        }
        //解决chrome浏览器的滚动条问题
        var callback = function(){
            win.hide();
            win.show();
        }
        win.show(null,callback);
        win.load(hqlWhere);
    },
    /**
     * 打开数据过滤条件设置页面
     * @private
     * @param {} appType
     * @param {} id
     */
    openAppEditWin:function(appType,id){
        var me = this;
        //应用类型信息
        var appTypeInfo ='';
        var title ='';
        var panel = 'Ext.manage.datafilters.setDatafiltersApp';
        var roleLists=[];
        var moduleOperId='';
        //模块操作选中行号
        var num=0;
        //模块操作的行过滤依据对象
        var objectName='';
        var objectCName='数据对象';
        var setformTitle='';
        var appId = -1;//
        //模块操作的默认数据过滤条件的Id
        var defaultRowFilterId='';
        var moduleTree=me.getmoduleTree();
        var mdtRecord=moduleTree.selectRecord;
        if(mdtRecord&&mdtRecord!=null){
            setformTitle ='　　　　　　　所属模块:'+mdtRecord.get('text');
        }
        var moduleTree=me.getmoduleTree();
        var moduleOper=me.getmoduleOperLists();
        var records=moduleOper.getSelectionModel().getSelection();
        //行过滤条件角色树
        var filtersTree=me.getroleDataFiltersTree();
        var record=null;
        if(records&&records.length>0){
            record=records[0];
            setformTitle =setformTitle+'>>所属模块操作:'+record.get('RBACModuleOper_CName');
            defaultRowFilterId=''+record.get('RBACModuleOper_RBACRowFilter_Id');
            moduleOperId=record.get('RBACModuleOper_Id');
            objectName=""+record.get('RBACModuleOper_RowFilterBase');
            if(objectName==""){
                objectName=record.get('RBACModuleOper_BTDAppComponentsOperate_RowFilterBase');
            }
            //objectName='BProvince';//测试数据对象
            if(objectName==""||objectName==null){
                Ext.Msg.alert('提示','当前模块操作没有行过滤条件依据对象');
                return ;
            }else{
            //模块操作选中行记录
	        if(id && id > 0){
	            title = "修改数据过滤条件>>";
	            //数据过滤条件行记录的Id
	            appId = id;
	        }else{
	            title = "新增数据过滤条件>>";
                appId =-1;
	        }
            var maxWidth = document.body.clientWidth*0.92;
            var maxHeight = document.body.clientHeight*0.96;
	        var win = Ext.create(panel,{
	            title:title,
                setformTitle:setformTitle,
	            width:maxWidth,
	            height:maxHeight,
                maxWidth:maxWidth,
                maxHeight:maxHeight,
                layout:'border',
                defaultRowFilterId:defaultRowFilterId,//模块操作的默认数据过滤条件的Id
                moduleOperId:moduleOperId,//模块操作id
	            appId:appId,//数据过滤条件的行记录ID
	            filtersTree:filtersTree,//行过滤条件角色树
	            moduleOperSelect:record,//模块操作选中行记录
	            objectName:objectName,//objectName数据对象
                objectCName:objectCName,
	            appType:appType,
	            modal:true,//模态
	            resizable:false,//可变大小
	            floating:true,//漂浮
	            closable:true,//有关闭按钮
	            draggable:true//可移动
	        }).show();
	        //保存监听
	        win.on({
                //更新模块操作列表后事件
                updateModuleOperClick:function(){
                    var moduleOper=me.getmoduleOperLists();
                    var hqlWhere2='rbacmoduleoper.RBACModule.Id='+me.moduleTreeId;
                    if(moduleOper){
                        moduleOper.load(hqlWhere2);
                    }
                },
	            comeBackClick:function(){
	                win.close();
	            },
                close:function(){
                    filtersTree.load(me.moduleOperId);
                }
	        });
            }
        }else{
            Ext.Msg.alert('提示','请先选中模块操作数据行');
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
                            ErrorInfo:'获取应用组件信息失败！错误信息' + result.ErrorInfo + '</b>'
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
    },
    createItems:function() {
        var me = this;
        var appInfos =[ {
	        //左模块树
	        width:220,
	        xtype:'moduleTree',
	        header:true,
	        itemId:'moduleTree',
	        name:'moduleTree',
	        title:'模块树',
	        region:'west',
	        split:true,
	        collapsible:true,
	        collapsed:false,
	        defaultactive:false
	    }, {
	        header:false,
	        xtype:'rightApp',
	        itemId:'rightApp',
	        name:'rightApp',
            moduleRecord:'',
	        region:'center',
	        split:true,
            isShowPaste:me.isShowPaste,
            isShowCopy:me.isShowCopy,
            isShowPredefinedAttributes:me.isShowPredefinedAttributes,
            operateInfo:me.operateInfo,
	        collapsible:true,
	        collapsed:false
	    }]; 
    return appInfos;
    },

    /***
     * 获取左模块树
     */
    getmoduleTree:function(){
        var me=this;
        var com=me.getComponent('moduleTree');
        return com;
    },
    /***
     * 获取右区域应用
     */
    getrightApp:function(){
        var me=this;
        var com=me.getComponent('rightApp');
        return com;
    },
    /***
     * 获取右区域应用的模块操作列表
     */
    getmoduleOperLists:function(){
        var me=this;
        var rightApp=me.getrightApp();
        var com=rightApp.getComponent('moduleOperLists');
        return com;
    },
    /***
     * 获取右区域应用的右区域
     */
    getrightAppForRight:function(){
        var me=this;
        var rightApp=me.getrightApp();
        var com=rightApp.getComponent('right');
        return com;
    },
    /***
     * 获取右区域应用的右上表单
     */
    getdatafiltersForm:function(){
        var me=this;
        var right=me.getrightAppForRight();
        var com=right.getComponent('datafiltersForm');
        return com;
    },
    /***
     * 获取右区域应用的右区域的下数据过滤条件角色树
     */
    getroleDataFiltersTree:function(){
        var me=this;
        var right=me.getrightAppForRight();
        var com=right.getComponent('roleDataFiltersTree');
        return com;
    }
});