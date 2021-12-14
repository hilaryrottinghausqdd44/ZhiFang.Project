//员工角色分配应用
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.zhifangux', getRootPath() + '/ui/zhifangux');
Ext.ns('Ext.manage');
Ext.define('Ext.manage.role.jueseyuangongApp', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.jueseyuangongApp',
    title:'',
    requires: ['Ext.zhifangux.CheckList'],
    autoSelect:true,
    width:877,
    height:345,
    layout:{
        type:'border',
        regionWeights:{
            north:4,
            west:5
        }
    },
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    appInfos:[ {
        x:338,
        width:500,
        height:188,
        appId:'5395982393934851784',
        header:true,
        itemId:'JueseyuangongList',
        title:'拥有角色员工',
        region:'north',
        split:false,
        collapsible:false,
        collapsed:false,
        border:true,
        sequencenum:0,
        defaultactive:false
    }, {
        x:338,
        y:193,
        width:537,
        height:125,
        appId:'5283989966041544971',
        header:true,
        itemId:'bumenyuangongCheck',
        title:'选择员工添加角色',
        region:'center',
        split:false,
        collapsible:false,
        collapsed:false,
        border:false,
        sequencenum:0,
        defaultactive:false
    }, {
        width:300,
        height:318,
        appId:'5021416507794180276',
        header:true,
        itemId:'jueselistQuery',
        title:'角色列表',
        region:'west',
        split:true,
        collapsible:true,
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
            var RoleValues='';
            var bumenyuangong='';
            var  treeId='';
            var node='';
            var _jueselistQuery = me.getComponent('jueselistQuery');
            _jueselistQuery.on({
                select:function(view, record) {
                    var RBACRole_Id = record.get('RBACRole_Id');
                    var hql = 'RBACRole.Id=' + RBACRole_Id;
                    var _JueseyuangongList = me.getComponent('JueseyuangongList');
                    _JueseyuangongList.load(hql);
                    
                    var RBACRole_Id=record.data.RBACRole_Id;
                    RoleValues=RBACRole_Id;
                   _bumenyuangongCheck_bumenTreeQuery.getSelectionModel().select(0);
//                    var hql = 'HREmployee.Id=' + treeId;
                    bumenyuangongList.obj='&longHRDeptID='+treeId+'&longRBACRoleID='+RoleValues;
                    var obj= bumenyuangongList.obj;
                    var _bumenyuangongCheck_bumenyuangongCheck = me.getComponent('bumenyuangongCheck').getComponent('bumenyuangongCheck');
                    _bumenyuangongCheck_bumenyuangongCheck.loadobj(obj);
                    //取得选中行HREmployee_Id
                    var HREmployee_Id=record.data.Id;
                    bumenyuangong=HREmployee_Id;
                }
            });
            var _bumenyuangongCheck_bumenTreeQuery = me.getComponent('bumenyuangongCheck').getComponent('bumenTreeQuery');
//            alert(_bumenyuangongCheck_bumenTreeQuery);
            _bumenyuangongCheck_bumenTreeQuery.on({
                select:function(view, record) {
                    var bumenyuangongList = me.getComponent('bumenyuangongCheck').getComponent('bumenyuangongCheck');
                    var Id = record.get('Id');
                    treeId=Id;
//                    var hql = 'HREmployee.Id=' + Id;
                    bumenyuangongList.obj='&longHRDeptID='+Id+'&longRBACRoleID='+RoleValues;
                    var obj= bumenyuangongList.obj;
                    bumenyuangongList.loadobj(obj,'');
                    
                    //取得选中行HREmployee_Id
                    var HREmployee_Id=record.data.Id;
                    bumenyuangong=HREmployee_Id;
                }
            });
            //角色列表
            var JueseyuangongList = me.getComponent('JueseyuangongList');
            var bumenyuangongList = me.getComponent('bumenyuangongCheck').getComponent('bumenyuangongCheck');
            var  bumenyuangongListstore=bumenyuangongList.getStore();
            var bumenTreeQuery = me.getComponent('bumenyuangongCheck').getComponent('bumenTreeQuery');
            JueseyuangongList.on({
            	delClick:function(){
                   var records = JueseyuangongList.getSelectionModel().getSelection();
                   if (records.length > 0) {
                       var records = JueseyuangongList.getSelectionModel().getSelection();
                       for (var i in records) {
                           var id = records[i].get("RBACEmpRoles_Id");
                           var callback = function() {
                               var rowIndex = JueseyuangongList.store.find("RBACEmpRoles_Id", id);
                               JueseyuangongList.deleteIndex = rowIndex;
                               JueseyuangongList.load(true);
                               
                        	   var Listhql = 'HREmployee.Id=' + bumenyuangong;
                        	  
                        	   bumenyuangongList.getStore().load(Listhql);
 
//                        	   if(id!=''){
//                        		   autoSelect=true;
//	   	                        	bumenyuangongList.autoSelect=id;
//	   	   			                //新增、修改后保存更新部门员工列表
////	   	                        	bumenyuangongList.load(true);
//	   	                        	bumenyuangongList.getSelectionModel().select(0);
//
//	   	   			            } 
                           };
                           me.deleteInfo(id, callback);
                       }
                  } else {
                      Ext.Msg.alert("提示", "请选择数据进行操作！");
                  }
	            }
            });
            bumenyuangongList.on({
            	itemdblclick:function(view, record) {
                     var HREmployee_Id = record.get('HREmployee_Id');
                     var flag=0;
                     var JueseyuangongListstore=JueseyuangongList.getStore();

     				 var callback = function() {
                         var store=bumenyuangongList.getStore();
                         var rowIndex =store.find("HREmployee_Id", HREmployee_Id);
                         bumenyuangongList.deleteIndex = rowIndex;
                         var hql = 'HREmployee.Id=' + bumenyuangong;
                         var store=bumenyuangongList.getStore();
                         store.load(hql);

                         var hqlRole = 'RBACRole.Id=' + RoleValues;
                         JueseyuangongListstore.load(hqlRole);
                         JueseyuangongListstore.on({
	                    	 load:function(s,records,successful){
                        	     if(records.length>0){
	                    	         JueseyuangongList.getSelectionModel().select(records.length-1);
                        	     }
		                     }
	                     });
                     };
                     me.Jurisdiction(HREmployee_Id,RoleValues,flag,callback);
                }
            });
            var toolbar=bumenyuangongList.getComponent('toolbar').getComponent('buttonstoolbar');
            //更新
            var update=toolbar.getComponent('refresh');
            update.on({
                click:function(){
            	    bumenyuangongList.obj='&longHRDeptID='+treeId+'&longRBACRoleID='+RoleValues;
                    var obj= bumenyuangongList.obj;
//                    alert(node+'node');
                    bumenyuangongList.loadobj(obj,'');
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

            //所选员工拥有角色
            var btnjuse=toolbar.getComponent('btn');
            btnjuse.on({
            	click:function(){
	            	//选择值（增加)
                    var store=bumenyuangongList.getStore();
	                var data=bumenyuangongList.getAllChecked();
	                var HREmpValues=[];
	                Ext.each(data,function(item,index,allItems){
	                    var  HREmployee_Id=item.HREmployee_Id;
	                    HREmpValues.push(HREmployee_Id);
	                });
	                var emp=Ext.encode(HREmpValues);
                    emp = emp.replace(/\[/g,'');
                    emp = emp.replace(/\]/g,'');
                    emp = emp.replace(/'/g,'');
                    emp = emp.replace(/"/g,'');
                    var  flag=0;
                    var JueseyuangongListStore=JueseyuangongList.getStore();
	                var callback = function() {
	                     var rowIndex =store.find("HREmployee_Id", bumenyuangong);
	                     bumenyuangongList.deleteIndex = rowIndex;
	                     var hql = 'HREmployee.Id=' + bumenyuangong;
	                     var bumenyuangongListStore=bumenyuangongList.getStore();
	                     bumenyuangongListStore.load(hql);
	                     var Rolehql = 'RBACRole.Id=' + RoleValues;
	                     JueseyuangongListStore.load(Rolehql);
	                     JueseyuangongListStore.on({
	                    	 load:function(s,records,successful){
	                    	     if(records.length>0){
	                    	         JueseyuangongList.getSelectionModel().select(records.length-1);
	                    	     }
		                     }
	                     });
	                 };
	                me.Jurisdiction(emp,RoleValues,flag,callback); 
                }
            });

            if (Ext.typeOf(me.callback) == 'function') {
                me.callback(me);
            }
        }
    },

    //批量增加,减少
    Jurisdiction:function(empIdList,roleIdList,flag,callback){
        var EmpIdList=getRootPath() + '/' +'RBACService.svc/RBAC_RJ_SetEmpRolesByEmpIdList';
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url:EmpIdList+ '?empIdList=' + empIdList +'&'+'roleIdList=' + roleIdList+'&'+'flag='+ flag,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
        	    var result = Ext.JSON.decode(response.responseText);
        	        callback();
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','请求服务失败!');
            }
       });
    },
    //删除
    deleteInfo:function(id, callback) {
        var url = getRootPath() + "/RBACService.svc/RBAC_UDTO_DelRBACEmpRoles?id=" + id;
        Ext.Ajax.defaultPostHeader = "application/x-www-form-urlencoded";
        Ext.Ajax.request({
            async:false,
            url:url,
            method:"GET",
            timeout:2e3,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    if (Ext.typeOf(callback) == "function") {
                        callback();
                    }
                } else {
                    Ext.Msg.alert("提示", '删除信息失败！错误信息【<b style="color:red">' + result.ErrorInfo + "</b>】");
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert("提示", "删除信息请求失败！");
            }
        });
    },
    /**
     * 模糊查询过滤函数
     * @param {} value
     */
     filterFn: function (value) {
         var me = this, valtemp = value;
         var bumenyuangongList = me.getComponent('bumenyuangongCheck').getComponent('bumenyuangongCheck');
         var  store=bumenyuangongList.getStore();
         if (!valtemp) {
             store.clearFilter();
             return;
         }
         valtemp = String(value).trim().split(" ");
         store.filterBy(function (record, id) {
        	  var data = record.data;
              var HRDeptCName=record.data.HREmployee_HRDept_CName;
              var CName=record.data.HREmployee_CName;
              var Position=record.data.HREmployee_HRPosition_CName;
              var dataarr={
            	  HREmployee_HRDept_CName:HRDeptCName,
                  HREmployee_CName:CName,
                  HREmployee_HRPosition_CName:Position
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