
/***
 * 角色管理--角色权限分配APP
 */
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/manage/class/rolemanager/jueselistQuery.js');
Ext.Loader.setPath('Ext.manage.rolemanager', getRootPath() + '/ui/manage/class/rolemanager/mokuaiyucaozuoCheck.js');
Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
Ext.Loader.setPath('Ext.manage.rolemanager', getRootPath() + '/ui/manage/class/rolemanager/jueseChecklist.js');
Ext.Loader.setPath('Ext.manage.rolemanager', getRootPath() + '/ui/manage/class/rolemanager/mokuaiTreeCheck.js');
Ext.ns('Ext.manage');
Ext.define('Ext.manage.rolemanager.jueseRightsApp', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.jueseRightsApp',
    header:false,
    layout: 'border',
    
    /***
     * 角色权限关系表查询服务
     * @type String
     */
    searchRoleRightServerUrl:''+getRootPath() +'/RBACService.svc/RBAC_UDTO_SearchRBACRoleRightByHQL',
    roleRightfields:'RBACRoleRight_RBACModuleOper_RBACModule_Id,RBACRoleRight_RBACModuleOper_RBACModule_DataTimeStamp,RBACRoleRight_RBACModuleOper_Id,RBACRoleRight_RBACModuleOper_DataTimeStamp,RBACRoleRight_RBACRole_Id,RBACRoleRight_RBACRole_DataTimeStamp,RBACRoleRight_Id,RBACRoleRight_DataTimeStamp',
    
    /***
     * 角色权限关系表新增保存服务
     * @type String
     */
    saveRoleRightServerUrl:''+getRootPath() +'/RBACService.svc/RBAC_UDTO_AddRBACRoleRight',
    /***
     * 角色权限关系表删除服务
     * @type String
     */
    deleteRoleRightServerUrl:''+getRootPath() +'/RBACService.svc/RBAC_UDTO_DelRBACRoleRight',
    
    /***
     * 模块角色关系表新增保存服务
     * @type String
     */
    saveServerUrl:''+getRootPath() +'/RBACService.svc/RBAC_UDTO_AddRBACRoleModule',
    
    /***
     * 模块角色关系表修改服务
     * @type String
     */
    editServerUrl:''+getRootPath() +'/RBACService.svc/RBAC_UDTO_UpdateRBACRoleModuleByField',
    /***
     * 模块角色关系表删除服务
     * @type String
     */
    deleteServerUrl:''+getRootPath() +'/RBACService.svc/RBAC_UDTO_DelRBACRoleModule',
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    createItems:function(){
       var me=this;;
       var items=[ 
        //角色列表
        {
        width:255,
        xtype:'jueselistQuery',
        header:true,
        itemId:'jueselistQuery',
        title:'角色',
        region:'west',
        split:true,
        collapsible:true,
        collapsed:false,
        layout: 'fit'
    },
     {
        //width:585,
        header:false,
        split:true,
        autoScroll :true,
        //模块树,角色模块操作列表,确定表单
        xtype:'panel',
        itemId:'center',
        region:'center',
        collapsible:false,
        collapsed:false,
        layout: 'fit',
        border:false,
        items: [
            {
            xtype: 'mokuaiyucaozuoCheck',
            border:false,
            autoScroll :true,
            split:true,
            //height:555,
            header:false,
            itemId:'mokuaiyucaozuoCheck'
            }]
        
    } ];
    return items;
    },
    /***
     * 获取左角色列表组件
     */
    getjueselistQuery:function(){
        var me=this;
        var com=me.getComponent('jueselistQuery');
        return com;
    },
    /***
     * 获取模块树,角色模块操作列表,确定表单区域
     */
    getmokuaiyucaozuoCheck:function(){
        var me=this;
        var com=me.getComponent('center').getComponent('mokuaiyucaozuoCheck');
        return com;
    },
    /***
     * 获取中间模块树
     */
    getmokuaiTreeCheck:function(){
        var me=this;
        var com=me.getmokuaiyucaozuoCheck().getComponent('mokuaiTreeCheck');
        return com;
    },
    /***
     * 获取右上模块操作选择列表
     */
    getjueseChecklist:function(){
        var me=this;
        var topCenter=me.getmokuaiyucaozuoCheck().getComponent('center');
        var com=topCenter.getComponent('jueseChecklist');
        return com;
    },

    /**
     * 初始化
     */
    initComponent:function(){
        var me = this;
        Ext.Loader.setPath('Ext.manage',getRootPath()+'/ui/manage/class');
        Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/manage/class/rolemanager/jueselistQuery.js');
        Ext.Loader.setPath('Ext.manage.rolemanager', getRootPath() + '/ui/manage/class/rolemanager/mokuaiyucaozuoCheck.js');
        
        me.items=me.createItems();
        me.callParent(arguments);
    },
    comNum:0,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        } 
        me.afterRenderOperate();
    },
    /***
     * 左角色列表行选中事件处理
     */
    roleListSelectClick:function() {
        var me = this;
        //获取左角色列表组件
        var getjueselistQuery=me.getjueselistQuery();
        //模块树
        var getmokuaiTreeCheck = me.getmokuaiTreeCheck();
        var records = getjueselistQuery.getSelectionModel().getSelection();
        var roleId='';//角色Id值
        var selectRecord=null;
        if (records && records.length > 0) {
            selectRecord = records[0];
            roleId=selectRecord.get('RBACRole_Id');//角色Id值
        }
        
        var arrChecked=me.getDataById(roleId,null,'role');
        getmokuaiTreeCheck.selectdChecked=arrChecked;
        //默认选中的模块树的数组数据,用以在保存时区分哪些节点是新增或删除
        getmokuaiTreeCheck.setTreeChecked(arrChecked);
    },
    /***
     * 模块树行选中事件处理
     */
    moduleTreeSelectClick:function() {
        var me=this;
        //获取左角色列表组件
        var jueselistQuery=me.getjueselistQuery();
        //模块树
        var getmokuaiTreeCheck = me.getmokuaiTreeCheck();
        //获取模块树,角色模块操作列表,确定表单区域
        var getmokuaiyucaozuoCheck = me.getmokuaiyucaozuoCheck();
        //获取右上模块操作选择列表
        var jueseChecklist = me.getjueseChecklist();
        var records = getmokuaiTreeCheck.getSelectionModel().getSelection();
        var moduleId='';//模块Id值
        var selectRecord=null;
        var checked=false;
        if(records && records.length > 0){
            selectRecord = records[0];
            moduleId=selectRecord.get('tid');//模块Id值 
            checked=selectRecord.get('checked');
        }else{//没有选中节点的处理
            if (records && records.length > 0) {
                var allChecked = getmokuaiTreeCheck.getAllChecked();
                getmokuaiTreeCheck.selectRecord=allChecked[0];
                selectRecord = allChecked[0];
                moduleId=selectRecord.get('tid');//模块Id值
                checked=selectRecord.get('checked');
            }
        }
        if(checked==true){
            //加载该模块当前的模块操作访问信息
            jueseChecklist.setDisabled(false);
            var w='rbacmoduleoper.RBACModule.Id='+moduleId;
            jueseChecklist.load(w);
            setTimeout(function(){
	            //模块操作选择列表数据还原
	            var records = jueselistQuery.getSelectionModel().getSelection();
	            var roleId='';//角色Id值
	            var selectRecord=null;
	            if (records && records.length > 0) {
	                selectRecord = records[0];
	                roleId=selectRecord.get('RBACRole_Id');//角色Id值
	            }
	            var arr=me.getRoleRightByModuleIdAndRoleId(moduleId,roleId,null);
	            jueseChecklist.setCheckedIds(arr);
	          
	            if(jueseChecklist.getStore().getCount()>0){
	                jueseChecklist.getSelectionModel().select(0);
	            }

            },500);
            
        }else if(checked==false){
            jueseChecklist.getStore().removeAll();
            //jueseChecklist.setDisabled(true);
        }
    },
    afterRenderOperate:function() {
        var me = this;
        //获取左角色列表组件
        var getjueselistQuery=me.getjueselistQuery();
        //模块树
        var getmokuaiTreeCheck = me.getmokuaiTreeCheck();
        //获取模块树,角色模块操作列表,确定表单区域
        var getmokuaiyucaozuoCheck = me.getmokuaiyucaozuoCheck();
        //获取右上模块操作选择列表
        var jueseChecklist = me.getjueseChecklist();
        
        //左角色选择列表
        getjueselistQuery.on({
            //获取还原模块树的已选中项的后台数据
	        select:function(view, record) {
                me.roleListSelectClick();
                jueseChecklist.getStore().removeAll();
            }
	    });
        
        //模块树事件
        getmokuaiTreeCheck.on({
            //选择后更新还原模块操作角色列表数据
            select:function(view,record,index, eOpts) {
                getmokuaiTreeCheck.selectRecord=record;
                me.moduleTreeSelectClick();
            },
            checkchange:function(node,checked,eOpts ){
		        //获取右上模块操作选择列表
		        var jueseChecklist = me.getjueseChecklist();
		        if(checked==true){
		            //加载该模块当前的模块操作访问信息
                    var records = getmokuaiTreeCheck.getSelectionModel().getSelection();
                    if(records && records.length > 0){
			            var selectRecord = records[0];
			            var moduleId=""+selectRecord.get('tid');//模块Id值 
                        var nodeId=""+node.data.tid;
			            if(moduleId==nodeId){
                            me.moduleTreeSelectClick();
                        }
			        }
		        }else if(checked==false){
		            //jueseChecklist.setDisabled(true);
                    if(jueseChecklist.getStore().getCount()>0){
                        jueseChecklist.getStore().removeAll();
                    }
		        }
            }
        });
        
        //确定选择保存事件
        getmokuaiyucaozuoCheck.on({
            okClick:function(com,eOpts){
                //模块树原来选中的节点
                var oldChecked=getmokuaiTreeCheck.selectdChecked;
                //模块树本次操作所有的选中节点
                var arrAllChecked = [];
                arrAllChecked = getmokuaiTreeCheck.getAllChecked();
                //模块树当前选中的节点
                //var selectRecord=getmokuaiTreeCheck.selectRecord;

                //右上角色选择列表选中的行记录
                var listChecks=jueseChecklist.getAllChecked();
                //右上角色选择列表所有的改变行记录
                var listChanged=jueseChecklist.getAllChangedRecords();
                //模块角色新增,删除处理
                me.saveContents(oldChecked,arrAllChecked);
                if(listChanged&&listChanged.length>0){
	                //角色权限新增,删除处理
	                me.saveRoleRight(listChanged);
                    jueseChecklist.store.each(function(record) {
                        record.commit();
                    });
                }
                setTimeout(function(){
                    //模块树现在选中的节点重新赋值
	                var getjueselistQuery=me.getjueselistQuery();
	                var records = getjueselistQuery.getSelectionModel().getSelection();
	                var roleId='';//角色Id值
	                var selectRecord=null;
	                if (records && records.length > 0) {
	                    selectRecord = records[0];
	                    roleId=selectRecord.get('RBACRole_Id');//角色Id值
	                }
	                //默认选中的模块树的数组数据,用以在保存时区分哪些节点是新增或删除
	                var arrChecked=me.getDataById(roleId,null,'role');
	                getmokuaiTreeCheck.selectdChecked=arrChecked;
                },1500);
                
            }
        });
        
        
    },
    /**
     * 角色权限新增,删除处理
     * @private
     */
    saveRoleRight:function(listChanged){
        var me=this;
        //获取左角色列表组件
        var jueselistQuery=me.getjueselistQuery();
        var getmokuaiTreeCheck = me.getmokuaiTreeCheck();
        //获取右上模块操作选择列表
        var jueseChecklist = me.getjueseChecklist();
        var records = jueselistQuery.getSelectionModel().getSelection();
        var selectRoleId='';//角色Id值
        var selectRecord=null;
        if (records && records.length > 0) {
            selectRecord = records[0];
            selectRoleId=selectRecord.get('RBACRole_Id');//角色Id值
        }
        var roleDataTimeStamp=''+selectRecord.get('RBACRole_DataTimeStamp');
        var roleDataTimeStampArr=[];
        if (roleDataTimeStamp && roleDataTimeStamp != undefined) {
            roleDataTimeStampArr = roleDataTimeStamp.split(',');
        }else {
            Ext.Msg.alert('提示', '不能保存数据,角色数据对象的时间戳值获取不到');
            return;
        }
        var RBACRole={Id:selectRoleId,DataTimeStamp:roleDataTimeStampArr};

        //模块操作勾选的节点数据处理
        var result=false;
        Ext.Array.each(listChanged, function(newModel) {
             var checked=false;
             var newmoduleOperId=''+newModel['RBACModuleOper_Id'];
             checked=newModel['checkBoxColumn'];//自定义复选框列
             
             //新增角色权限关系
            if(checked==true){
                var moduleOperDataTimeStamp=""+newModel['RBACModuleOper_DataTimeStamp'];
                var moduleOperDataTimeStampArr=[];
                if (moduleOperDataTimeStamp =='undefined'||moduleOperDataTimeStamp=="") {
                    Ext.Msg.alert('提示', '不能保存数据,模块操作数据对象的时间戳值获取不到');
                    return;
                }else {
                    moduleOperDataTimeStampArr = moduleOperDataTimeStamp.split(',');
                }
                var RBACModuleOper={Id:newmoduleOperId,DataTimeStamp:moduleOperDataTimeStampArr};
                var newAdd= {
                        Id:-1,
                        LabId:1,
                        RBACModuleOper:RBACModuleOper,
                        RBACRole:RBACRole
                    };
                var obj={'entity':newAdd};
                //util-POST方式与后台交互
                var params = Ext.JSON.encode(obj);
                //POST方式与后台交互
		        var defaultPostHeader='application/json';
		        var async=false;
                postToServer(me.saveRoleRightServerUrl,params,null,defaultPostHeader,async);
                 
            }else if(checked==false){//删除角色权限关系
                var arr=me.getRoleRightByModuleOperIdAndRoleId(newmoduleOperId,selectRoleId,null);
                Ext.Array.each(arr, function(jsonData) {
	                var roleRightId=''+jsonData['roleRightId'];
	                if(roleRightId!=''&&roleRightId!='undefined'&&roleRightId!=undefined){
	                   me.deleteServer(roleRightId,null,'roleRight');
	                }
                });
            }
        });
 
    },
    /**
     * 左角色及模块树的确定选择钮事件处理
     * oldChecked:模块树原来选中的节点数据封装
     * arrAllChecked:模块树选中的节点
     * selectRecord:模块树当前选中的节点
     * listChecks:角色选择列表选中的行记录
     * listChanged:角色选择列表所有改变的行记录数据处理(勾选时为新增),不勾选的为删除
     * @private
     */
    saveContents:function(oldChecked,arrAllChecked){
        var me=this;
        //获取左角色列表组件
        var getjueselistQuery=me.getjueselistQuery();
        var getmokuaiTreeCheck = me.getmokuaiTreeCheck();
        var records = getjueselistQuery.getSelectionModel().getSelection();
        var selectRoleId='';//角色Id值
        var selectRecord=null;
        if (records && records.length > 0) {
            selectRecord = records[0];
            selectRoleId=selectRecord.get('RBACRole_Id');//角色Id值
        }
        var roleDataTimeStamp=''+selectRecord.get('RBACRole_DataTimeStamp');
        var roleDataTimeStampArr=[];
        if (roleDataTimeStamp==""|| roleDataTimeStamp=="undefined") {
            Ext.Msg.alert('提示', '不能保存数据,角色数据对象的时间戳值获取不到');
            return;
        }else {
            roleDataTimeStampArr = roleDataTimeStamp.split(',');
        }
        var RBACRole={Id:selectRoleId,DataTimeStamp:roleDataTimeStampArr};
        
        //模块树勾选的节点数据处理
        var result=false;
        //模块树选中的所有节点
        Ext.Array.each(arrAllChecked, function(newModel) {
             var result=false;
             var newModuleId=''+newModel.get('tid');
             //模块树原来选中的所有节点
             Ext.Array.each(oldChecked, function(oldModel) {
                var oldModuleId=''+oldModel['moduleId'];
                if(newModuleId==oldModuleId){//比较模块id,
                    result=true;//不作删除处理
                }
             });
             
             //模块树:新增角色模块访问权限关系
	        if(result==false&&newModuleId!="0"){
	            var moduleDataTimeStamp=""+newModel.get('DataTimeStamp');
	            var moduleDataTimeStampArr=[];
                
	            if (moduleDataTimeStamp=="undefined"||moduleDataTimeStamp=="") {
	                Ext.Msg.alert('提示', '不能保存数据,模块树数据对象的时间戳值获取不到');
                    return;
	            }else {
                    moduleDataTimeStampArr = moduleDataTimeStamp.split(',');
	                
	            }
	            var RBACModule={Id:newModuleId,DataTimeStamp:moduleDataTimeStampArr};
	            
	            var newAdd= {
	                    Id:-1,
	                    LabId:1,
	                    RBACModule:RBACModule,
	                    RBACRole:RBACRole
	                };
                var obj={'entity':newAdd};
                //util-POST方式与后台交互
                var params = Ext.JSON.encode(obj);
                //POST方式与后台交互
		        var defaultPostHeader='application/json';
		        var async=false;
                postToServer(me.saveServerUrl,params,null,defaultPostHeader,async);
	             
	        }
        });
        //模块树勾选的节点数据处理结束
    
        //模块树原来选中的所有节点
        Ext.Array.each(oldChecked, function(oldModel) {
             var result=false;
             var oldModuleId=''+oldModel['moduleId'];
             var checked=true;
            Ext.Array.each(arrAllChecked, function(newModel) {
                var newModuleId=''+newModel.get('tid');
                //checked=newModel.get('checked');
                if(newModuleId==oldModuleId){//比较模块id,
                    result=true;//原来选中的节点已经存在所有选中的节点中时,不作处理
                }
             });
             
             //原来的勾选的模块节点不存在所有选中的节点并处于不勾选时,作模块角色关系的删除处理
             if(result==false){
                var arr=me.getRBACRoleModuleByModuleIdAndRoleId(oldModuleId,selectRoleId,null);
                Ext.Array.each(arr, function(jsonData) {
                    var RBACRoleModuleId=''+jsonData['RBACRoleModuleId'];
	                if(RBACRoleModuleId!=''&&RBACRoleModuleId!='undefined'&&RBACRoleModuleId!=undefined){
	                      me.deleteServer(RBACRoleModuleId,null,'roleModule');
	                }
                });
                
                //是否需要删除该模块节点的角色权限关系
                var arr=me.getRoleRightByModuleIdAndRoleId(oldModuleId,selectRoleId,null);
                Ext.Array.each(arr, function(jsonData) {
                    var roleRightId=''+jsonData['roleRightId'];
                    if(roleRightId!=''&&roleRightId!='undefined'&&roleRightId!=undefined){
                       me.deleteServer(roleRightId,null,'roleRight');
                    }
                });
                
             }
        });
        
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
    /***
     * 查询角色权限关系表数据--获取角色权限关系表主键ID值
     * @param {} moduleId 模块id
     * @param {} roleId 权限id
     * @param {} callback
     * @return {}
     */
    getRoleRightByModuleOperIdAndRoleId:function(moduleOperId,roleId,callback){        
        var me = this;
        var arrData=[];
        var myUrl =me.searchRoleRightServerUrl+'?isPlanish=true&fields='+"RBACRoleRight_Id,RBACRoleRight_RBACModuleOper_Id,RBACRoleRight_RBACRole_Id,RBACRoleRight_RBACModuleOper_RBACModule_Id";
        var w="&where=rbacroleright.RBACModuleOper.Id=" + moduleOperId+' and rbacroleright.RBACRole.Id='+roleId;
        w=me.encodeString(w);
        myUrl=myUrl+w+'&page=1&start=0&limit=10000';
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
        async:false,//非异步
        url:myUrl,
        method:'GET',
        timeout:3000,
        success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                arrData=[];
                if(result.ResultDataValue && result.ResultDataValue != ''){
                    result.ResultDataValue = result.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                    var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                    for (var i = 0; i < ResultDataValue.count; i++) {
	                    //角色权限关系数据对象
	                    var roleRightId = ResultDataValue.list[i]['RBACRoleRight_Id'];
	                    var jsonData = {
	                        'roleRightId':roleRightId
	                    };
                    }
                    arrData.push(jsonData);
                    if(Ext.typeOf(callback) === 'function'){
                        callback(arrData);
                    }
                }
            }
        },
        failure : function(response,options){ 
            arrData=[];
            Ext.Msg.alert('提示','获取角色权限关系数据信息请求失败！');
        }
     });
      return arrData; 
    },
    /***
     * 查询角色权限关系表数据--还原右上模块操作列表勾选
     * @param {} moduleId 模块id
     * @param {} roleId 权限id
     * @param {} callback
     * @return {}
     */
    getRoleRightByModuleIdAndRoleId:function(moduleId,roleId,callback){        
        var me = this;
       
        var arrData=[];
        var myUrl =me.searchRoleRightServerUrl+'?isPlanish=true&fields='+""+me.roleRightfields+"";
        var w="&where=rbacroleright.RBACModuleOper.RBACModule.Id=" + moduleId+' and rbacroleright.RBACRole.Id='+roleId;
        w=me.encodeString(w);
        myUrl=myUrl+w+'&page=1&start=0&limit=10000';
 
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
        async:false,//非异步
        url:myUrl,
        method:'GET',
        timeout:6000,
        success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                arrData=[];
                if(result.ResultDataValue && result.ResultDataValue != ''){
                    result.ResultDataValue = result.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                    var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                    for (var i = 0; i < ResultDataValue.count; i++) {
                        //模块操作数据对象
                        var moduleOperId = ''+ResultDataValue.list[i]['RBACRoleRight_RBACModuleOper_Id'];
                        var moduleOperDataTimeStamp = ResultDataValue.list[i]['RBACRoleRight_RBACModuleOper_DataTimeStamp'];
                        
                        //角色子数据对象
                        var roleId = ResultDataValue.list[i]['RBACRoleRight_RBACRole_Id'];
                        var roleDataTimeStamp = ResultDataValue.list[i]['RBACRoleRight_RBACRole_DataTimeStamp'];
                        //模块子数据对象
                        var moduleId = ResultDataValue.list[i]['RBACRoleRight_RBACModuleOper_RBACModule_Id'];
                        var moduleDataTimeStamp = ResultDataValue.list[i]['RBACRoleRight_RBACModuleOper_RBACModule_DataTimeStamp'];
                        
                        //角色权限关系数据对象
                        var roleRightId = ResultDataValue.list[i]['RBACRoleRight_Id'];
                        var roleRightDataTimeStamp = ResultDataValue.list[i]['RBACRoleRight_DataTimeStamp'];
                        jsonData = {
                            'roleRightId':roleRightId,//
                            'roleRightDataTimeStamp':''+roleRightDataTimeStamp,
                            'RBACModuleOper_Id':moduleOperId,//
                            'moduleOperDataTimeStamp':''+moduleOperDataTimeStamp,
                            'moduleId':moduleId,//
                            'moduleDataTimeStamp':''+moduleDataTimeStamp,
                            'roleId':roleId,//
                            'roleDataTimeStamp':''+roleDataTimeStamp
                        };
                        arrData.push(jsonData);
                    }
                    if(Ext.typeOf(callback) === 'function'){
                        callback(arrData);
                    }
                }
            }
        },
        failure : function(response,options){ 
            arrData=[];
            Ext.Msg.alert('提示','获取角色权限关系数据信息请求失败！');
        }
     });
      return arrData; 
    },
    /***
     * 查询角色模块关系表数据--获取角色模块关系表主键ID值
     * @param {} id
     * @param {} callback
     * @return {}
     */
    getRBACRoleModuleByModuleIdAndRoleId:function(moduleId,roleId,callback){        
        var me = this;
        var arrData=[];
        var fields='RBACRoleModule_Id,RBACRoleModule_DataTimeStamp,RBACRoleModule_RBACModule_ParentID,RBACRoleModule_RBACModule_Id,RBACRoleModule_RBACModule_DataTimeStamp,RBACRoleModule_RBACRole_Id,RBACRoleModule_RBACRole_DataTimeStamp'
        var myUrl = getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL' +'?isPlanish=true&fields='+fields;
        var w="&where=rbacrolemodule.RBACModule.Id="+moduleId+' and rbacrolemodule.RBACRole.Id='+roleId;
        w=me.encodeString(w);
        myUrl=myUrl+w;
        
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
        async:false,//非异步
        url:myUrl,
        method:'GET',
        timeout:2000,
        success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                arrData=[];
                if(result.ResultDataValue && result.ResultDataValue != ''){
                    result.ResultDataValue = result.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                    var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                    for (var i = 0; i < ResultDataValue.count; i++) {
	                    //模块角色关系数据对象
	                    var RBACRoleModuleId = ''+ResultDataValue.list[i]['RBACRoleModule_Id'];
	                    var RBACRoleModuleDataTimeStamp = ResultDataValue.list[i]['RBACRoleModule_DataTimeStamp'];
	                    
	                    //模块角色关系数据对象的角色子数据对象
	                    var roleId = ResultDataValue.list[i]['RBACRoleModule_RBACRole_Id'];
	                    var roleDataTimeStamp = ResultDataValue.list[i]['RBACRoleModule_RBACRole_DataTimeStamp'];
	                    //模块角色关系数据对象的模块子数据对象
	                    var moduleId = ResultDataValue.list[i]['RBACRoleModule_RBACModule_Id'];
	                    var moduleDataTimeStamp = ResultDataValue.list[i]['RBACRoleModule_RBACModule_DataTimeStamp'];
	                    
	                    var jsonData = {
	                        'RBACRoleModuleId':RBACRoleModuleId,//模块角色关系数据对象Id
	                        'RBACRoleModuleDataTimeStamp':''+RBACRoleModuleDataTimeStamp,
	                        'RBACRole_Id':roleId,//角色对象Id
	                        'roleDataTimeStamp':''+roleDataTimeStamp,
	                        'moduleId':moduleId,//模块数据对象Id
	                        'moduleDataTimeStamp':''+moduleDataTimeStamp
	                    };
                        arrData.push(jsonData);
                    }
                    if(Ext.typeOf(callback) === 'function'){
                        callback(arrData);
                    }
                }
            }
        },
        failure : function(response,options){ 
            arrData=[];
            Ext.Msg.alert('提示','获取数据信息请求失败！');
        }
     });
      return arrData; 
    },
    /***
     * id为角色Id值或者模块Id值
     * 当id为角色Id值时查询匹配模块角色关系表里的的角色数据集合(左角色列表及中间模块树)
     * 当id为模块Id值时查询匹配模块角色关系表里的的模块数据集合(中间模块树及右上角色选择列表)
     * @param {} id
     * @param {} callback
     * @return {}
     */
    getDataById:function(id,callback,type){        
        var me = this;
        var arrData=[];
        var fields='RBACRoleModule_Id,RBACRoleModule_DataTimeStamp,RBACRoleModule_RBACModule_ParentID,RBACRoleModule_RBACModule_Id,RBACRoleModule_RBACModule_DataTimeStamp,RBACRoleModule_RBACRole_Id,RBACRoleModule_RBACRole_DataTimeStamp'
        var myUrl = getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL' +'?isPlanish=true&fields='+fields;
        var w='';
        if(type=='role'){
            w='rbacrolemodule.RBACRole.Id='+id;
        }else if(type=='module'){
            w='rbacrolemodule.RBACModule.Id='+id;
        }
        w=me.encodeString(w);
        myUrl=myUrl+'&where='+w;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
        async:false,//非异步
        url:myUrl,
        method:'GET',
        timeout:2000,
        success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                arrData=[];
                if(result.ResultDataValue && result.ResultDataValue != ''){
                    result.ResultDataValue = result.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                    var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                    for (var i = 0; i < ResultDataValue.count; i++) {
                        //模块角色关系数据对象
                        var RBACRoleModuleId = ResultDataValue.list[i]['RBACRoleModule_Id'];
                        var RBACRoleModuleDataTimeStamp = ResultDataValue.list[i]['RBACRoleModule_DataTimeStamp'];
                        
                        //模块角色关系数据对象的角色子数据对象
                        var roleId = ResultDataValue.list[i]['RBACRoleModule_RBACRole_Id'];
                        var roleDataTimeStamp = ResultDataValue.list[i]['RBACRoleModule_RBACRole_DataTimeStamp'];
                        //模块角色关系数据对象的模块子数据对象
                        var moduleId = ResultDataValue.list[i]['RBACRoleModule_RBACModule_Id'];
                        var moduleDataTimeStamp = ResultDataValue.list[i]['RBACRoleModule_RBACModule_DataTimeStamp'];
                        
                        var treeData = {
                            'RBACRoleModuleId':RBACRoleModuleId,//模块角色关系数据对象Id
                            'RBACRoleModuleDataTimeStamp':''+RBACRoleModuleDataTimeStamp,
                            'RBACRole_Id':roleId,//角色对象Id
                            'roleDataTimeStamp':''+roleDataTimeStamp,
                            'moduleId':moduleId,//模块数据对象Id
                            'moduleDataTimeStamp':''+moduleDataTimeStamp
                        };
                        arrData.push(treeData);
                        
                    }
                    if(Ext.typeOf(callback) === 'function'){
                        callback(arrData);
                    }
                }
            }
        },
        failure : function(response,options){ 
            arrData=[];
            Ext.Msg.alert('提示','获取数据信息请求失败！');
        }
     });
      return arrData; 
    }, 
    /**
     * 从数据库中删除记录
     * @private
     * @param {} id
     */
    deleteServer:function(Id,callback,type){
        var me = this;
        var url ='';
        
        if(Id==""||Id=='undefined'||Id==undefined){
            return false;
        }else{
	        if(type=='roleModule'){
	           url = me.deleteServerUrl + "?Id="+Id;//删除角色模块关系
	        }else if(type=='roleRight'){
	           url = me.deleteRoleRightServerUrl + "?Id="+Id;//删除角色权限关系
	        }
	        Ext.Ajax.request({
	            async:false,//非异步
	            url:url,
	            method:'GET',
	            timeout:2000,
	            success:function(response,opts){
	                var result = Ext.JSON.decode(response.responseText);
	                if(result.success){
	                   if(Ext.typeOf(callback) === 'function'){
	                        callback(response.responseText);
	                    }
	                }else{
	                    Ext.Msg.alert("提示","删除失败！错误信息【<b style='color:red'>'"+ result.ErrorInfo +"</b>】");
	                }
	            },
	            failure:function(response,options){ 
	                Ext.Msg.alert("提示","删除服务出错！");
	            }
	        });
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
    initLink:function(panel) {
        var me = this;
        var appInfos = me.getAppInfos();
        var length = appInfos.length;
        me.comNum++;
        if (me.comNum == length) {
            if (me.panelType == 'Ext.tab.Panel') {
                var f = function() {
                    me.setActiveTab(me.defaultactive);
                    me.un('tabchange', f);
                };
                me.on('tabchange', f);
            }
            if (Ext.typeOf(me.callback) == 'function') {
                me.callback(me);
            }
        }
    }
});