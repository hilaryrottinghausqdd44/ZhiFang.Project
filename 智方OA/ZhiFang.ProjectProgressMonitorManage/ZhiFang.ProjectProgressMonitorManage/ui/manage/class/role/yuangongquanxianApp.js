//员工关系
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.zhifangux', getRootPath() + '/ui/zhifangux');
Ext.ns('Ext.manage');
Ext.define('Ext.manage.role.yuangongquanxianApp', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.yuangongquanxianApp',
    requires: ['Ext.zhifangux.CheckList'],
    title:'',
    width:956,
    height:297,
    externalWhere:'',
    objectName:'RBACRole',
    internalWhere:'',
    autoSelect:true,
    multiSelect:true,
    layout:{
        type:'border',
        regionWeights:{
            east:1
        }
    },
    itemData:[],
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    appInfos:[ {
        width:835,
        height:270,
        appId:'4753966499831169646',
        header:false,
        itemId:'bumenyuangongApp',
        title:'',
        region:'center',
        split:false,
        collapsible:false,
        collapsed:false,
        border:false,
        sequencenum:0,
        defaultactive:false
    }, {
        //x:804,
        width:480,
        height:270,
//        appId:'5485140513438077027',
        //appId:'4621382774765429957',
        appId:'5573109795529987270',
        //xtype:'jueseChecklist',
        header:true,
        itemId:'jueseCheckList',
        title:'角色选择',
        region:'east',
        split:true,
        collapsible:true,
        collapsed:false,
        border:true,
        //------------------------------------
	    //Jcall 2016-07-27 修改，用于默认排序字段
	    defaultOrderBy:[{property:'RBACRole_DispOrder',direction:'ASC'}],
	    //------------------------------------
        sequencenum:0,
//        margins: '5 0 0 5',
        defaultactive:false,
        layout: 'fit'
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
                    me.initLink();
                };
                appInfo.callback = callback2;
                var panel = Ext.create(cl, appInfo);
                me.add(panel);
              
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
    
    initLink:function() {
        var me = this;
        var appInfos = me.getAppInfos();
        var length = appInfos.length;
        me.comNum++;
        if (me.comNum == length) {
        	var jueseCheckList=me.getComponent('jueseCheckList');
            var bumenyuangongApp=me.getComponent('bumenyuangongApp').getComponent('yuangongListRead');
            var bumenstore=bumenyuangongApp.getStore();
            bumenstore.on({
            	load:function(s,records,successful){
	            	if (records){
		            	if(records.length==0){
			        		jueseCheckList.store.loadData([]);
			        	}
	            	}
                }
            });
          var empValues=[];
          var jueseCheckList=me.getComponent('jueseCheckList');  
          var bumenyuangongApp=me.getComponent('bumenyuangongApp').getComponent('yuangongListRead');
            bumenyuangongApp.on({
                select:function(sm, record, rowindex, o) {
                    if(jueseCheckList.getStore().getCount()==0){
                    	jueseCheckList.load();
//                        jueseCheckList.getSelectionModel().select(0);
                    }
                    var id = record.get(bumenyuangongApp.objectName + '_Id');
                    var callback = function(addData){
                        jueseCheckList.setCheckedIds(addData);
                    };
                  
                    //util-GET方式与后台交互
                    var arr=me.getDataById(id,callback);
                    jueseCheckList.setCheckedIds(arr);
                    if(jueseCheckList.getStore().getCount()>0){
                        jueseCheckList.getSelectionModel().select(0);
                    }
                    //取得选中行HREmployee_Id  用于批量添加  减少
                    var HREmployee_Id=record.data.HREmployee_Id;
                    empValues=[];
                    empValues.push(HREmployee_Id);
                }

            });
            var toolbar=jueseCheckList.getComponent('toolbar').getComponent('buttonstoolbar');
            //更新
            var update=toolbar.getComponent('refresh');
            update.on({
                click:function(){
                    jueseCheckList.load(me.externalWhere);
                }
            });
            //查询栏
            var searchText=toolbar.getComponent('searchText');
            searchText.on({
               specialkey: function(field,e){    
	                if (e.getKey()==Ext.EventObject.ENTER){  
	                	var newValue=searchText.getValue();
	                	 me.filterFn(newValue);
	                }  
	            }
            });
            //查询按钮
            var searchbtn=toolbar.getComponent('searchbtn');
            searchbtn.on({
                click:function(){
            	var newValue=searchText.getValue();
                    me.filterFn(newValue);
                }
            });
          //更新保存
            jueseCheckList.on({
                okClick:function(){
                    var store=jueseCheckList.getStore();
                    var  t='';
                    store.each(function(record) {
                        if(record.get(jueseCheckList.keyColumn)==false && record.dirty == true){
                           t=1;
                        }
                    }); 
                    //改变值(减少)
                    var data=jueseCheckList.getAllChanged();
                    var ChangeRoleId='';
                    var ChangeRoleValues=[];
                    Ext.each(data,function(item,index,allItems){
                         var  RBACRole_Id=item.RBACRole_Id;
                         ChangeRoleId= RBACRole_Id;
                         ChangeRoleValues.push(RBACRole_Id);
                    });
                    //选择值（增加)
                    var data=jueseCheckList.getAllChecked();
                    var RoleId='';
                    var RoleValues=[];
                    Ext.each(data,function(item,index,allItems){
                        var  RBACRole_Id=item.RBACRole_Id;
                        RoleId= RBACRole_Id;
                        RoleValues.push(RBACRole_Id);
                    });
                    var emp=Ext.encode(empValues);
                    emp = emp.replace(/\[/g,'');
                    emp = emp.replace(/\]/g,'');
                    emp = emp.replace(/'/g,'');
                    emp = emp.replace(/"/g,'');
                 
                    var RoleId=Ext.encode(RoleValues);
                    RoleId = RoleId.replace(/\[/g,'');
                    RoleId = RoleId.replace(/\]/g,'');
                    RoleId = RoleId.replace(/'/g,'');
                    RoleId = RoleId.replace(/"/g,'');
                    if(t==1){
                        var flag=1;
                        var RoleId=Ext.encode(ChangeRoleValues);
                        RoleId = RoleId.replace(/\[/g,'');
                        RoleId = RoleId.replace(/\]/g,'');
                        RoleId = RoleId.replace(/'/g,'');
                        RoleId = RoleId.replace(/"/g,'');
                        if(emp=='' || RoleId==''){
                            return;
                        }else{
                             me.Jurisdiction(emp,RoleId,flag); 
                             store.each(function(record) {
                                 if(record.dirty == true){
                                     record.commit();
                                 }
                             }); 
                        }
                    }else{
                        if(emp=='' || RoleId==''){
                             return;
                        }else{
                            var flag=0;
                            me.Jurisdiction(emp,RoleId,flag); 
                            store.each(function(record) {
                                record.commit();
                            }); 
                        }
                    }
                }
            });
            if (Ext.typeOf(me.callback) == 'function') {
                me.callback(me);
            }
        }
    },
    
    /**
     * 模糊查询过滤函数
     * @param {} value
     */
     filterFn: function (value) {
         var me = this, valtemp = value;
         var jueseCheckList=me.getComponent('jueseCheckList');
         var store = jueseCheckList.getStore(); //reload()
         if (!valtemp) {
             store.clearFilter();
             return;
         }
         valtemp = String(value).trim().split(" ");
         store.filterBy(function (record, id) {
             var data = record.data;
             var UseCode=record.data.RBACRole_UseCode;
             var CName=record.data.RBACRole_CName;
             var Comment=record.data.RBACRole_Comment;
             var dataarr={
                 RBACRole_UseCode:UseCode,
                 RBACRole_CName:CName,
                 RBACRole_Comment:Comment
             };
             for (var p in dataarr) {
                 var porp = String(dataarr[p]);
                 for (var i = 0; i < valtemp.length; i++) {
                     var macther = valtemp[i];
                     var macther2 = Ext.escapeRe(macther);
                     mathcer = new RegExp(macther2);
                     if (mathcer.test(porp)) {
                         return true;
                     } 
                 } 
             }
             return false;
         });
     },
    //获取返回的角色列表
    getDataById:function(id,callback){        
        var me = this;
        var arrData=[];
        var url = getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRoleByHREmpID' + '?id=' + id +
        '&'+'fields=RBACRole_CName,RBACRole_Id,RBACRole_Comment&isPlanish=true';
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
        async:false,//非异步
        url:url,
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
                        var RBACRoleId = ResultDataValue.list[i].RBACRole_Id;
                        var addData = {
                            'RBACRole_Id':RBACRoleId
                        };
                        arrData.push(addData);
                    }
                    if(Ext.typeOf(callback) === 'function'){
                        callback(arrData);
                    }
                }
            }
        },
        failure : function(response,options){ 
            arrData=[];
            Ext.Msg.alert('提示','获取应用信息请求失败！');
        }
     });
      return arrData; 
    },  

    //批量增加,减少
    Jurisdiction:function(empIdList,roleIdList,flag){
        var EmpIdList=getRootPath() + '/' +'RBACService.svc/RBAC_RJ_SetEmpRolesByEmpIdList';
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url:EmpIdList+ '?empIdList=' + empIdList +'&'+'roleIdList=' + roleIdList+'&'+'flag='+ flag,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
               Ext.Msg.alert('提示','批量提交成功!');
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','请求服务失败!');
            }
       });
    },
    getAppInfoFromServer:function(id, callback) {
        var me = this;
        var url = me.getAppInfoServerUrl + '?isPlanish=true&id=' + id;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            url:url,
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