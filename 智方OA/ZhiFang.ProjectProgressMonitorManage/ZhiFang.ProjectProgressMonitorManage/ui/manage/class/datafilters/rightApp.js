
/***
 * 右区域数据过滤条件
 * 对外公开属性
 * ismodulePaste:是否显示开始粘贴按钮操作,false:显示,true:隐藏
 * isShowCopy:是否隐藏复制按钮,false:显示,true:隐藏
 * isShowPaste:是否隐藏粘贴按钮,false:显示,true:隐藏
 * isShowPredefinedAttributes:是否隐藏预定义属性按钮,false:显示,true:隐藏
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.rightApp', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.rightApp',
    title:'',
    moduleRecord:'',
    moduleId:'',
    moduleOperIndex:'',
    isUseRowFilter:false,
    width:680,
    height:460,
    layout: 'border',
    border:true,
    /**默认加载数据时启用遮罩层*/
    hasLoadMask:true,
    objectCName:"",
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
     * 是否开始粘贴按钮操作
     * false:未开始,
     * true:开始
     * @type Boolean
     */
    ismodulePaste:false,
    /***
     * 是否隐藏复制按钮
     * @type Boolean
     */
    isShowCopy:true,
    /***
     * 是否隐藏粘贴按钮
     * false:显示,
     * true:隐藏
     * @type Boolean
     */
    isShowPaste:true,
     /***
     * 是否隐藏预定义属性按钮
     * @type Boolean
     */
    isShowPredefinedAttributes:false,
    /**
     * 是否隐藏取消按钮
     * @type Boolean
     */
    isShowCancel:false,
    
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
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
    deleteRoleRightUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_DelRBACRoleRight',
    /***
     * 删除行过滤条件
     * @type 
     */
    deleteRBACRowFilterUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_DelRBACRowFilter',
    /***
     * 复制角色权限
     * sourceModuleOperID:源模块操作ID 
     * targetModuleOperID:目标模块操作ID
     * @type 
     */
    copyRoleRightByModuleOperIDUrl:getRootPath()+'/RBACService.svc/RBAC_RJ_CopyRoleRightByModuleOperID',
    comNum:0,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if(me.moduleId && me.moduleId != ""){me.load(me.moduleId);}
        
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
        me.initLink();
    },
    /***
     * 对外分开,加载表单及模块操作列表信息
     * @param {}moduleId
     */
    load:function(moduleId) {
        var me=this;
        if(moduleId!=""){
            //var moduleId=moduleId;
            var hqlWhere='rbacmoduleoper.RBACModule.Id='+moduleId;
            me.moduleId=moduleId;
            var moLists=me.getmoduleOperLists();
            if(moLists){
                moLists.selectIndex=0;
                moLists.load(hqlWhere);
            }
            var moList=me.getmoduleOperLists();
            var moRecords = moList.getSelectionModel().getSelection();
            var moRecord = null;
            //行过滤条件角色树
	        var rdfTree=me.getroleDataFiltersTree();
	        //2.中间区域为模块操作列表连动数据过滤条件角色树
	        
	        //3.表单连动代码
	        var form=me.getdatafiltersForm();
            if (moRecords && moRecords.length > 0) {
                moRecord = moRecords[0];
                form.enableControl();
                rdfTree.setDisabled(false);
            } else {
                form.disableControl();
                rdfTree.setDisabled(true);
                moRecord = null;
            }
            
            if(moRecord!=null){
                //是否开启数据权限
                var form=me.getdatafiltersForm();
                var useRowFilter=''+moRecord.get('RBACModuleOper_UseRowFilter');
                if (useRowFilter&& useRowFilter=='true') {// 
                   if(form){
                        form.setisUseRowFilter(true);
                        me.isUseRowFilter=true;
                   }
                }else{
                    if(form){
                        form.setisUseRowFilter(false);
                        me.isUseRowFilter=false;
                    }
                }
            }
            
        }
    },
    /**
     * 打开预定义可选属性设置页面
     * @private
     * @param {} appType
     * @param {} id
     */
    openAppWin:function(){
        var me = this;
        //1.左区域为模块树连动中间模块操作列表
        var moduleOper=me.getmoduleOperLists();
        var moRecords = moduleOper.getSelectionModel().getSelection();
        if (moRecords && moRecords.length > 0) {
            me.objectName=moRecords[0].get('RBACModuleOper_RowFilterBase');//数据对象
            if(me.objectName==null||me.objectName==""){
                me.objectName=moRecords[0].get('RBACModuleOper_BTDAppComponentsOperate_RowFilterBase');//行过滤依据对象
                 }
            } else {
            me.objectName="";
        }
       
        if(me.objectName&&me.objectName!=null&&me.objectName!=""){
            var title ='';
            var panel = 'Ext.manage.datafilters.predefinedAttributesTree';
            var roleLists=[];
            var moduleOperId='';
            var maxWidth = document.body.clientWidth*0.46;
            var maxHeight = document.body.clientHeight*0.96;
            var win = Ext.create(panel,{
                title:title,
                width:maxWidth,
                height:maxHeight,
                maxWidth:maxWidth,
                maxHeight:maxHeight,
                moduleOperId:me.moduleOperId,
                /***
                 * 下拉列表框树的所需的数据对象名称
                 * @type String
                 */
                objectName:me.objectName,
                /***
                 * 下拉列表框树的所需的数据对象中文名
                 * @type String
                 */
                objectCName:me.objectCName,
                layout:'border',
                modal:true,//模态
                resizable:false,//可变大小
                floating:true,//漂浮
                closable:true,//有关闭按钮
                draggable:true//可移动
            }).show();
            //保存监听
            win.on({
                //更新模块操作列表后事件,text为响应信息,arrTreeJson为已经选择的树格式数据
                okClick:function(but,e,text,arrTreeJson){
                   if(text!=null){
                        var result = Ext.JSON.decode(text);
                        if (result.success) {
                            win.close();
                            //Ext.Msg.alert('提示','预定义可选属性设置成功');
                        } else {
                            alertError(result.ErrorInfo);
                        }
                   }
                },
                cancelClick:function(){
                    win.close();
                },
                close:function(){
                    //win.close();
                }
            });
        }else{
            Ext.Msg.alert('提示','获取不到模块操作的数据对象');
        }
    },
    initLink:function() {
        var me = this;
        //1.左区域为模块树连动中间模块操作列表
        var moduleOper=me.getmoduleOperLists();
        //行过滤条件角色树
        var rdfTree=me.getroleDataFiltersTree();
        //2.中间区域为模块操作列表连动数据过滤条件角色树
        
        //3.表单连动代码
        var form=me.getdatafiltersForm();
        var btnisShowCopy=form.getComponent('btnisShowCopy');//复制按钮
        var btnisShowPaste=form.getComponent('btnisShowPaste');//粘贴按钮
        var btnCancel=form.getComponent('btnCancel');//取消按钮
        if(btnCancel){
            btnCancel.on({
                click:function(com, e, eOpts ){
                    //清空粘贴信息
                    me.operateInfo["objectCName"]='';
                    me.operateInfo["moduleCopyCName"]='';
                    me.operateInfo["modulePasteCName"]='';
                    me.operateInfo["moduleCopy"]={Id:'',CName:''};
                    me.operateInfo["modulePaste"]={Id:'',CName:''};
                    me.ismodulePaste=false;
                    btnisShowPaste.setVisible(false);
                    btnCancel.setVisible(false);
                }
            });
        }
        //预定义可选属性
        var predefinedAttributes=form.getComponent('btnPredefinedAttributes');
        if(predefinedAttributes){
            predefinedAttributes.on({
                click:function(com, e, eOpts ){
                    Ext.Loader.setConfig({enabled:true});
                    Ext.Loader.setPath('Ext.manage.datafilters.predefinedAttributesTree', getRootPath() + '/ui/manage/class/datafilters/predefinedAttributesTree.js');    
                    me.openAppWin();
                }
            });
        }
        
        if(moduleOper){
            moduleOper.store.on({
	            load:function(store,records,successful){
	                if(successful&&records.length>0){
                        form.enableControl();
	                    rdfTree.setDisabled(false);
	                }else{
                        form.disableControl();
                        rdfTree.setDisabled(true);
                    }
	            }
	        });
            moduleOper.on({
                select:function( rowModel,record, index, eOpts ){
                    if(record){
                        me.moduleOperIndex=index;
                        moduleOper.selectIndex=index;
                        var id=record.get('RBACModuleOper_Id');
                        me.moduleOperId=id;
                        //是否开启数据权限
                        var form=me.getdatafiltersForm();
                        var useRowFilter=''+record.get('RBACModuleOper_UseRowFilter');
                        if (useRowFilter&& useRowFilter=='true') {// 
                           if(form){
                                form.setisUseRowFilter(true);
                                me.isUseRowFilter=true;
                           }
                        }else{
                            if(form){
                                form.setisUseRowFilter(false);
                                me.isUseRowFilter=false;
                            }
                        }
                        if(rdfTree){
                            rdfTree.load(id);
                        }
                        if(me.operateInfo&&me.operateInfo!=null&me.operateInfo!=''&&me.ismodulePaste==true){
	                        //粘贴时选择模块操作行
	                        var moduleCopy=me.operateInfo["moduleCopy"];
	                        var modulePaste=me.operateInfo["modulePaste"];
	                        if(moduleCopy&&moduleCopy!=""&&moduleCopy!=null){
	                            var moduleOperIdCopy=moduleCopy["Id"];
	                            var objectCNameCopy=me.operateInfo["objectCName"];
	                            
	                            var moduleOperId=record.get('RBACModuleOper_Id');
			                    var cName=record.get('RBACModuleOper_CName');
                                
			                    me.objectName=record.get('RBACModuleOper_RowFilterBase');//数据对象
			                    if(me.objectName==null||me.objectName==""){
			                        me.objectName=record.get('RBACModuleOper_BTDAppComponentsOperate_RowFilterBase');//行过滤依据对象
			                    }
                                //复制已经有信息
	                            if(objectCNameCopy.length>0&&objectCNameCopy==me.objectName){
                                    me.operateInfo["modulePaste"]={Id:moduleOperId,CName:cName};
                                    //粘贴的模块中文名称
                                    me.operateInfo["modulePasteCName"]=record.get('RBACModuleOper_RBACModule_CName');
	                            }else{
                                    //复制的模块中文名称
	                                me.operateInfo["moduleCopyCName"]=record.get('RBACModuleOper_RBACModule_CName'); 
                                }
	                        }
                        }  
                    }
                },
                itemclick:function(grid, record,  item,  index,  e,  eOpts ){
                    if(record){
                        me.moduleOperIndex=index;
                        var id=record.get('RBACModuleOper_Id');
                        if(me.moduleOperId!=id){
	                        me.moduleOperId=id;
	                        //是否开启数据权限
	                        var form=me.getdatafiltersForm();
	                        var useRowFilter=''+record.get('RBACModuleOper_UseRowFilter');
	                        if (useRowFilter&& useRowFilter=='true') {// 
	                           if(form){
	                                form.setisUseRowFilter(true);
	                                me.isUseRowFilter=true;
	                           }
	                        }else{
	                            if(form){
	                                form.setisUseRowFilter(false);
	                                me.isUseRowFilter=false;
	                            }
	                        }
	                        if(rdfTree){
	                            rdfTree.load(id);
	                        }
                            
                            if(me.operateInfo&&me.operateInfo!=null&me.operateInfo!=''&&me.ismodulePaste==true){
	                            //粘贴时选择模块操作行
	                            var moduleCopy=me.operateInfo["moduleCopy"];
	                            var modulePaste=me.operateInfo["modulePaste"];
	                            if(moduleCopy&&moduleCopy!=""&&moduleCopy!=null){
	                                var moduleOperIdCopy=moduleCopy["Id"];
	                                var objectCNameCopy=me.operateInfo["objectCName"];
	                                
	                                var moduleOperId=record.get('RBACModuleOper_Id');
	                                var cName=record.get('RBACModuleOper_CName');
	                                me.objectName=record.get('RBACModuleOper_RowFilterBase');//数据对象
	                                if(me.objectName==null||me.objectName==""){
	                                    me.objectName=record.get('RBACModuleOper_BTDAppComponentsOperate_RowFilterBase');//行过滤依据对象
	                                }
	                                if(objectCNameCopy.length>0&&objectCNameCopy==me.objectName){
	                                    me.operateInfo["modulePaste"]={Id:moduleOperId,CName:cName};
		                                //粘贴的模块中文名称
	                                    me.operateInfo["modulePasteCName"]=record.get('RBACModuleOper_RBACModule_CName');
	                                }else{
	                                    //复制的模块中文名称
	                                    me.operateInfo["moduleCopyCName"]=record.get('RBACModuleOper_RBACModule_CName'); 
	                                }
	                            }
	                        }
                        }
                    }
                }
            });
        }
        if(btnisShowCopy){
	        //复制按钮
	        btnisShowCopy.on({
	            click:function(com, e, eOpts ){
	                //1.左区域为模块树连动中间模块操作列表
	                var moList=me.getmoduleOperLists();
	                var moRecords = moList.getSelectionModel().getSelection();
	                var moRecord = null;
	                if (moRecords && moRecords.length > 0) {
	                    moRecord = moRecords[0];
	                    var moduleOperId=moRecord.get('RBACModuleOper_Id');
	                    var cName=moRecord.get('RBACModuleOper_CName');
	                    me.objectName=moRecord.get('RBACModuleOper_RowFilterBase');//数据对象
	                    
	                    if(me.operateInfo){
	                        if(me.objectName==null||me.objectName==""){
	                            me.objectName=record.get('RBACModuleOper_BTDAppComponentsOperate_RowFilterBase');//行过滤依据对象
	                        }
	                        if(me.objectName==null||me.objectName==""){
	                            me.operateInfo["moduleCopy"]={Id:'',CName:''};
	                            me.operateInfo["objectCName"]='';
                                me.operateInfo["modulePasteCName"]="";
                                me.operateInfo["moduleCopyCName"]="";
	                            me.ismodulePaste=false;
	                            btnisShowPaste.setVisible(false);
	                        }else{
	                            me.operateInfo["moduleCopy"]={Id:moduleOperId,CName:cName};
	                            me.operateInfo["objectCName"]=me.objectName;
                                me.operateInfo["moduleCopyCName"]=moRecord.get('RBACModuleOper_RBACModule_CName'); 
	                            me.ismodulePaste=true;
	                            btnisShowPaste.setVisible(true);
                                btnCancel.setVisible(false);
	                        }
	                        
		                }
	                }
	                
	            }
	        });
        }
        if(btnisShowPaste){
	        //粘贴按钮
	        btnisShowPaste.on({
	            click:function(com, e, eOpts ){
	                if(me.operateInfo&&me.operateInfo!=null&me.operateInfo!=''){
		                var moduleCopy=me.operateInfo["moduleCopy"];
		                var modulePaste=me.operateInfo["modulePaste"];
	                    if(moduleCopy==""){
	                        alert("请先操作复制按钮功能");
	                    }
		                if(modulePaste==""){
		                    alert("请选择需要粘贴的模块操作");
		                }else{
		                    if(moduleCopy!=""&&modulePaste!=undefined&&modulePaste==""){
		                        alert("请选择需要粘贴的模块操作");
		                    }else if(moduleCopy!=""&&modulePaste!=undefined&&modulePaste!=""){
		                        var moduleCopyId=me.operateInfo["moduleCopy"]["Id"];
		                        var modulePasteId=me.operateInfo["modulePaste"]["Id"];
	                            
		                        if(moduleCopyId==modulePasteId){
		                            alert("复制和粘贴的模块操作不能相同");
		                        }else if(moduleCopyId!=""&&modulePasteId!=""){
                                    var modulePasteCName=me.operateInfo["modulePasteCName"];
                                    var moduleCopyCName=me.operateInfo["moduleCopyCName"];
                                    
                                    var moduleOCopyCName=me.operateInfo["moduleCopy"]["CName"];
                                    var moduleOPasteCName=me.operateInfo["modulePaste"]["CName"];
                                    var msg="复制模块为【"+moduleCopyCName+"】"+"的模块操作【"+moduleOCopyCName+"】的所有行过滤条件<br/>"+
                                    "粘贴到模块为【"+modulePasteCName+"】"+"模块操作【"+moduleOPasteCName+"】"
                                    
                                    Ext.Msg.confirm('【复制】&&【粘贴】操作', "是否要继续"+msg+"操作?",
                                                function(button) {
                                                    if (button == 'yes') {
                                                        var c = function(text) {
						                                    me.fireEvent('okClick');
						                                    var result = Ext.JSON.decode(text);
						                                        if (result.success) {
						                                            Ext.Msg.show({
						                                                title: '【复制】&&【粘贴】功能操作',
						                                                msg: msg,
						                                                width: 380,
						                                                buttons: [],
						                                                icon: Ext.window.MessageBox.INFO
						                                            });
						                                             //清空粘贴信息
						                                            me.operateInfo["objectCName"]='';
						                                            me.operateInfo["moduleCopy"]={Id:'',CName:''};
						                                            me.operateInfo["modulePaste"]={Id:'',CName:''};
                                                                    me.operateInfo["modulePasteCName"]="";
                                                                    me.operateInfo["moduleCopyCName"]="";
                                                                    
						                                            me.ismodulePaste=false;
						                                            btnisShowPaste.setVisible(false);
						                                            btnCancel.setVisible(false);
						                                            rdfTree.load(modulePasteId);
						                                        } else {
						                                            btnisShowPaste.setVisible(false);
						                                            btnCancel.setVisible(false);
						                                            alertError(result.ErrorInfo);
						                                        }
						
						                                        if(me.hasLoadMask && me.mk){me.mk.hide();}//隐藏遮罩层
						                                    };
						                                    if(me.hasLoadMask){
						                                        me.mk = me.mk || new Ext.LoadMask(me.getEl(),{msg:'操作数据中...',removeMask:true});
						                                        me.mk.show();//显示遮罩层
						                                    }
						                                    var url=me.copyRoleRightByModuleOperIDUrl+"?sourceModuleOperID="+moduleCopyId+"&targetModuleOperID="+modulePasteId;
						                                    //sourceModuleOperID:源模块操作ID 
						                                    //targetModuleOperID:目标模块操作ID
						                                    getToServer(url,c, false,30000);
                                                    }
                                                });
	                            }else{
	                                alert("复制和粘贴的模块操作不能为空或相同");
	                            }
		                    }
		                }
	                }else{
	                    alert("请先操作复制按钮功能");
	                }
	            }
	        });
        }
        if(form){
        form.on({//是否开启数据权限
            changeUseRowFilter:function(com, newValue, oldValue, eOpts ){
                me.isUseRowFilter=newValue;
                var editServerUrl=getRootPath() + '/'+'RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField';
                var editfields='Id,UseRowFilter';
                var moList=me.getmoduleOperLists();
                var moRecords = moList.getSelectionModel().getSelection();
                var moRecord = null;
                if (moRecords && moRecords.length > 0) {
                    moRecord = moRecords[0];
                } else {
                    moRecord = null;
                }
                if(moRecord!=null){
                    var moduleOperId=moRecord.get('RBACModuleOper_Id');
                    var obj={
                    'Id':moduleOperId,
                    'UseRowFilter':''+newValue
                    };
                    var obj2={'entity':obj,'fields':editfields};
                    var params = Ext.JSON.encode(obj2);
                    var c = function(text){
                    var result = Ext.JSON.decode(text);
                        if(result.success){
                        }
                    }
                    //util-POST方式与后台交互
                    var defaultPostHeader='application/json';
                    var async=false;
                    postToServer(editServerUrl,params,c,defaultPostHeader,async);
                    moRecord.set('RBACModuleOper_UseRowFilter',''+newValue);
                    moRecord.commit();
                }
               
            }
        });
        
        var btnCom=form.getComponent('btnAddDatafilters');
        if(btnCom){
             //新增过滤条件按钮
            btnCom.on({
                click:function(com, e, eOpts ){
                    me.openAppEditWin('add',-1);
                }
            });
        }
        }
        
        //4.行过滤条件角色树:分配角色列显示处理
        if(rdfTree){
            rdfTree.store.on({
            load:function(treeStore, node, records, successful, eOpts) {
                    if(successful==false){
                        rdfTree.isLoaded=false;
                    }
              }
            });
            
            rdfTree.on({
                //查看某角色下的员工角色数据
                showClick:function(grid,rowIndex,colIndex,item,e,record){
                    if(record&&record!=null){
                       //objectType为RBACRowFilter是数据过滤条件节点
                        var objectType=''+record.get('objectType');
                        if(objectType=='RBACRole'){
                            var tid=record.get('tid');//角色
                            var roleCName=record.get('text');//角色名称
                            var hqlWhere='rbacemproles.RBACRole.Id='+tid;
                            me.openRoleListShowWin(hqlWhere,roleCName);
                        }else if(objectType=='RBACRowFilter'){
                            Ext.Msg.alert('提示','请选择角色查看');
                        }
                    }
                },
                
                editClick:function(grid,rowIndex,colIndex,item,e,record){
                    if(record&&record!=null){
                        //objectType为RBACRowFilter是数据过滤条件节点
                        var objectType=''+record.get('objectType');
                        if(objectType=='RBACRowFilter'){
                            var appId=record.get('tid');//
                            me.openAppEditWin('edit',appId);
                        }else if(objectType=='RBACRole'){
                            var item1=item;
                            var parentNode=record.parentNode;
                            if(parentNode&&parentNode!=undefined){
	                            var appId=parentNode.get('tid');//
	                            me.openAppEditWin('edit',appId);
                            }else{
                                Ext.Msg.alert('提示','请选择数据过滤条件');
                            }
                        }
                    }
                },
                deleteClick:function( grid,rowIndex,colIndex,item,e,record){
                    var moRecords = moduleOper.getSelectionModel().getSelection();
                    var moRecord = null;
                    if (moRecords && moRecords.length > 0) {
                        moRecord = moRecords[0];
                    } else {
                        moRecord = null;
                    }
                    
                    if(record&&record!=null){
                        var roleId=record.get('tid');//节点id值,角色id
                        var rowId=record.get('pid');//节点id值,角色的数据过滤行条件id
                        //objectType为RBACRowFilter是数据过滤条件节点
                        var objectType=''+record.get('objectType');
                        var moduleOperId=moRecord.get('RBACModuleOper_Id');
                        var obj={};
                        //角色节点
                        if(objectType=='RBACRole'){
                            obj={moduleOperId:moduleOperId,rowId:rowId,roleId:roleId};
                            //如果是角色节点,需要更新角色权限关系信息的数据过滤行条件id为空
                            var list=me.getRoleRightLists(obj,'edit');
                            Ext.Array.each(list, function(model) {
                                var id=model['RBACRoleRight_Id'];
                                var obj={
                                    Id:id
                                }
                                me.updateRoleRight(obj,'part');
                            });
                            //更新行过滤--角色树
                            if(rdfTree){
                                rdfTree.load(moduleOperId);
                            }
                            
                        }else if(objectType=='RBACRowFilter'){//父节点
                            rowId=record.get('tid');//节点id值,角色的数据过滤行条件id
                            var obj2={moduleOperId:moduleOperId,rowId:rowId,roleId:null};
                            //1.如果是父节点,需要更新模块操作的相关数据过滤行条件id为空,并直接删除角色权限关系信息
                            var list=me.getRoleRightLists(obj2,'delete');

                            //2.更新该行过滤条件在模块操作的默认行过滤条件为空
                            //模块操作数据对象
                            var RBACModuleOper={"Id":moduleOperId};
                            var editServerUrl=getRootPath() + '/'+'RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField';
                            var editfields='Id,RBACRowFilter';
                            
                            var obj2={'entity':RBACModuleOper,'fields':editfields};
                            var params = Ext.JSON.encode(obj2);
                            //util-POST方式与后台交互
                            var defaultPostHeader='application/json';
                            var async=false;
                            postToServer(editServerUrl,params,null,defaultPostHeader,async);
        
                            //3.删除完角色权限的行过滤条件的关系后
                            Ext.Msg.confirm('提示','确定要删除吗？',function(button){
                                if(button == 'yes'){
                                   Ext.Array.each(list, function(model) {
                                    id=''+model['RBACRoleRight_Id'];
                                    var roleId=''+model['RBACRoleRight_RBACRole_Id'];
                                    if(roleId==""){
                                        me.deleteRoleRight(id);
                                    }else{
		                                var obj={
		                                    Id:id
		                                }
		                                me.updateRoleRight(obj,'part');
                                    }
                                 });
                                //4.删除完角色权限的行过滤条件的关系后,删除行过滤条件数据
                                me.deleteRBACRowFilter(rowId);
                                //5.更新行过滤--角色树
                                if(rdfTree){
                                    rdfTree.load(moduleOperId);
                                } 
                               }
                            });
                        }
                    } 
                },
                itemdblclick:function(grid, record,  item,  index,  e,  eOpts ){
                    if(record&&record!=null){
                        var objectType=''+record.get('objectType');
                        //角色节点
                        if(objectType=='RBACRole'){
                            var tid=record.get('tid');//角色
                            var roleCName=record.get('text');//角色
                            var hqlWhere='rbacemproles.RBACRole.Id='+tid;
                            if(moduleOper){
                                me.openRoleListShowWin(hqlWhere,roleCName);
                            }
                        }else if(objectType=='RBACRowFilter'){
                            var appId=record.get('tid');//
                            me.openAppEditWin('edit',appId);
                        }
                    }
                }
            });
        }
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
                result=true;
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
              Ext.Msg.alert('提示','删除行过滤条件成功');
          },
          failure : function(response,options){
              Ext.Msg.alert('提示','删除行过滤条件失败');
          }
        });
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
    /**
     * 打开某角色下的员工信息列表应用窗口
     * @private
     * @param {} title
     * @param {} ClassCode
     * @param {} id
     */
    openRoleListShowWin:function(hqlWhere,roleCName){
        var me = this;
        var panel = 'Ext.manage.datafilters.empRolesLists';
        var maxHeight = document.body.clientHeight*0.98;
        var maxWidth = document.body.clientWidth*0.98;
        var title=""+roleCName+"的员工信息";
        var win = Ext.create(panel,{
            id:-1,
            title:title,
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
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.manage.datafilters.ComboBoxTree', getRootPath() + '/ui/manage/class/datafilters/ComboBoxTree.js');
        Ext.Loader.setPath('Ext.manage.datafilters.setForm', getRootPath() + '/ui/manage/class/datafilters/setForm.js');
        Ext.Loader.setPath('Ext.manage.datafilters.roleChooseLists', getRootPath() + '/ui/manage/class/datafilters/roleChooseLists.js');
        Ext.Loader.setPath('Ext.manage.datafilters.setDatafiltersApp', getRootPath() + '/ui/manage/class/datafilters/setDatafiltersApp.js');
        
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
        var mdtRecord=me.moduleRecord;
        if(mdtRecord&&mdtRecord!=null){
            setformTitle ='　　　　　　　所属模块:'+mdtRecord.get('text');
        }
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
            objectName=record.get('RBACModuleOper_BTDAppComponentsOperate_RowFilterBase');
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
                isShowPredefinedAttributes:me.isShowPredefinedAttributes,
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
                objectCName:objectCName,//objectName中文数据对象
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
                    var hqlWhere2='rbacmoduleoper.RBACModule.Id='+me.moduleId;
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
    /***
     * 获取右区域应用的模块操作列表
     */
    getmoduleOperLists:function(){
        var me=this;
        var com=me.getComponent('moduleOperLists');
        return com;
    },
    /***
     * 获取右区域应用的右上表单
     */
    getdatafiltersForm:function(){
        var me=this;
        var com=me.getComponent('right').getComponent('datafiltersForm');
        return com;
    },
    /***
     * 获取右区域应用的右区域的下数据过滤条件角色树
     */
    getroleDataFiltersTree:function(){
        var me=this;
        var com=me.getComponent('right').getComponent('roleDataFiltersTree');
        return com;
    },
    createItems:function() {
        var me = this;

        var moduleOperLists=Ext.create('Ext.manage.datafilters.moduleOperLists',{
            width:210,
            header:true,
            xtype:moduleOperLists,
            itemId:'moduleOperLists',
            name:'moduleOperLists',
            title:'模块操作列表',
            region:'west',
            split:true,
            collapsible:true,
            collapsed:false,
            border:false
        });
        var datafiltersForm=Ext.create('Ext.manage.datafilters.datafiltersForm',{
                itemId:'datafiltersForm',
                name:'datafiltersForm',
                header:false,
                height:40,
                operateInfo:me.operateInfo,
                region:'north',
                isShowPredefinedAttributes:me.isShowPredefinedAttributes,
                collapsible:false,
                collapsed:false,
                isShowPaste:me.isShowPaste,
                isShowCopy:me.isShowCopy,
                isShowCancel:me.isShowCancel,
                border:false
        });
        
        var roleDataFiltersTree=Ext.create('Ext.manage.datafilters.roleDataFiltersTree',{
                name:'roleDataFiltersTree',
                itemId:'roleDataFiltersTree',
                header:false,
                region:'center',
                border:false,
                autoScroll:true,
                layout:'fit'
        });
        var appInfos = [
            moduleOperLists,
            {
            //右数据过滤条件APP
            xtype:'panel',
            name:'right',
            itemId:'right',
            title:'数据过滤条件&角色树',
            region:'center',
            minHeight:420,
            autoScroll:true,
            items:[datafiltersForm,roleDataFiltersTree]  
        }]; 
        return appInfos;
    },
    /**
     * 初始化
     */
    initComponent:function(){
        var me = this;
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.manage.datafilters.ComboBoxTree', getRootPath() + '/ui/manage/class/datafilters/ComboBoxTree.js');
        Ext.Loader.setPath('Ext.manage.datafilters.empRolesLists', getRootPath() + '/ui/manage/class/datafilters/empRolesLists.js');
        Ext.Loader.setPath('Ext.manage.datafilters.moduleOperLists', getRootPath() + '/ui/manage/class/datafilters/moduleOperLists.js');
        Ext.Loader.setPath('Ext.manage.datafilters.datafiltersForm', getRootPath() + '/ui/manage/class/datafilters/datafiltersForm.js');
        Ext.Loader.setPath('Ext.manage.datafilters.roleDataFiltersTree', getRootPath() + '/ui/manage/class/datafilters/roleDataFiltersTree.js');
        Ext.Loader.setPath('Ext.manage.datafilters.setDatafiltersApp', getRootPath() + '/ui/manage/class/datafilters/setDatafiltersApp.js');
        
        me.items=me.createItems();
        me.callParent(arguments);
    },
    
     beforeRender:function() {
        var me = this;
        me.callParent(arguments);
    }
});