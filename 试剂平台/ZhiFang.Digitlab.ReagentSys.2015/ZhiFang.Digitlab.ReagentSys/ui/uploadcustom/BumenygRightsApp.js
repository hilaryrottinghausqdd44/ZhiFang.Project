Ext.define("BumenygRightsApp", {
    extend:"Ext.panel.Panel",
    panelType:"Ext.panel.Panel",
    alias:"widget.bumenygRightsApp",
    title:"部门员工权限",
    width:989,
    height:300,
    layout:{
        type:"border",
        regionWeights:{
            west:2,
            east:1
        }
    },
    getAppInfoServerUrl:getRootPath() + "/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById",
    appInfos:[ {
        x:296,
        width:386,
        height:273,
        appId:"4737011784260186118",
        header:true,
        itemId:"bumenyuangongTab",
        title:"部门员工选择",
        region:"center",
        split:false,
        collapsible:false,
        collapsed:false,
        border:true,
        sequencenum:0,
        defaultactive:false
    }, {
        x:687,
        width:300,
        height:273,
        appId:"4655054335881693957",
        header:true,
        itemId:"bumenyuangongjuese",
        title:"权限角色批量选择",
        region:"east",
        split:true,
        collapsible:true,
        collapsed:false,
        border:true,
        sequencenum:0,
        defaultactive:false
    }, {
        width:291,
        height:273,
        appId:"5057693520969860629",
        header:true,
        itemId:"quanxianbumenTree",
        title:"部门",
        region:"west",
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
            if (obj.success && obj.appInfo != "") {
                var ModuleOperCode = obj.appInfo.BTDAppComponents_ModuleOperCode;
                var ClassCode = obj.appInfo.BTDAppComponents_ClassCode;
                var cl = eval(ClassCode);
                var callback2 = function(panel) {
                    me.initLink(panel);
                };
                appInfo.callback = callback2;
                var panel = Ext.create(cl, appInfo);
                me.add(panel);
                if (me.panelType == "Ext.tab.Panel") {
                    if (appInfo.defaultactive) {
                        me.defaultactive = appInfo.itemId;
                    }
                    me.setActiveTab(panel);
                }
            } else {
                appInfo.html = obj.ErrorInfo;
                var panel = Ext.create("Ext.panel.Panel", appInfo);
                me.add(panel);
                if (me.panelType == "Ext.tab.Panel") {
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
            if (appInfos[i].title == "") {
                delete appInfos[i].title;
            } else if (appInfos[i].title == "_") {
                appInfos[i].title = "";
            }
        }
        return Ext.clone(appInfos);
    },
    //批量增加,减少
    Jurisdiction:function(empIdList,roleIdList,flag){
        var EmpIdList=getRootPath() + '/' +'RBACService.svc/RBAC_RJ_SetEmpRolesByEmpIdList';
  	    Ext.Ajax.defaultPostHeader = 'application/json';
  	    Ext.Ajax.request({
  	        url:EmpIdList+ '?empIdList=' + empIdList +'&'+'?roleIdList=' + roleIdList+'&'+'?flag='+ flag,
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
    initLink:function(panel) {
        var me = this;
        var appInfos = me.getAppInfos();
        var length = appInfos.length;
        me.comNum++;
        //联动代码必须放在这里边
        if (me.comNum == length) {
            if (me.panelType == "Ext.tab.Panel") {
                var f = function() {
                    me.setActiveTab(me.defaultactive);
                    me.un("tabchange", f);
                };
                me.on("tabchange", f);
            }
            var _quanxianbumenTree = me.getComponent("quanxianbumenTree");
            _quanxianbumenTree.on({
                select:function(view, record) {
                    var Id = record.get("Id");
                    var hql = "id=" + Id;
                    var _bumenyuangongTab_bumenyuangongCheck = me.getComponent("bumenyuangongTab").getComponent("bumenyuangongCheck");
                    _bumenyuangongTab_bumenyuangongCheck.load(hql);
                }
            });
            var _quanxianbumenTree = me.getComponent("quanxianbumenTree");
            _quanxianbumenTree.on({
                select:function(view, record) {
                    var Id = record.get("Id");
                    var hql = "hrdeptemp.HRDept.Id=" + Id;
                    var _bumenyuangongTab_bumenxgyuangongcheck = me.getComponent("bumenyuangongTab").getComponent("bumenxgyuangongcheck");
                    _bumenyuangongTab_bumenxgyuangongcheck.load(hql);
                }
            });
            
            var rec='';
            var recemp='';
            var recempxg='';
            //判断是那个页签
            var isList='';
            var btnSave = me.getComponent("bumenyuangongjuese").getComponent("juesepiliangForm").getComponent("btnSave");
            var btnDel = me.getComponent("bumenyuangongjuese").getComponent("juesepiliangForm").getComponent("btnDel");
            //角色列表
            var jueseChecksList = me.getComponent("bumenyuangongjuese").getComponent("jueseChecksList");
            //下属员工
            var bumenyuangongCheck = me.getComponent("bumenyuangongTab").getComponent("bumenyuangongCheck");
            //相关员工
            var bumenxgyuangongcheck = me.getComponent("bumenyuangongTab").getComponent("bumenxgyuangongcheck");
            var bumenyuangongTab = me.getComponent("bumenyuangongTab");
           //获取默认激活的页签
            var activeTabId=bumenyuangongTab.activeTab.itemId;
            bumenyuangongCheck.on({
                select:function(sm, record, rowindex, o) {
            	    var HREmployee_Id=record.data.HREmployee_Id;
            	    recemp = {HREmployee_Id:HREmployee_Id};
                },
                activate:function(){
                	me.setActive(true,bumenyuangongCheck);
                	isList='bumenyuangongCheck';
                }
            });
            bumenxgyuangongcheck.on({
                select:function(sm, record, rowindex, o) {
	            	var HRDeptEmp_HREmployee_Id=record.data.HRDeptEmp_HREmployee_Id;
	            	recempxg = {HRDeptEmp_HREmployee_Id:HRDeptEmp_HREmployee_Id};
                },
                activate:function(){
                	isList='bumenxgyuangongcheck';
                }
          });
          jueseChecksList.on({
              select:function(sm, record, rowindex, o) {
        	      var RBACRole_Id=record.data.RBACRole_Id;
		  		  rec = {RBACRole_Id:RBACRole_Id};
              }
          
          });
          btnSave.on({
        	  click:function() {
	        	  //获取角色id 并把id放到数组
	    	      var jueseList=[];
	    	      //获取部门下属员工id 并把id放到数组
	    	      var empIdList=[];
	    	      //获取相关员工id 并把id放到数组
	    	      var empList=[];
	    	      var flag=0;
	        	  var records = jueseChecksList.getSelectionModel().getSelection();
	    	      if (records.length > 0) {//当有选择的数据的时候   
	    	           for(var i = 0; i < records.length; i++) {
	    	               jueseList.push(rec);
	    	           }
	    	      }
	              var bumenyuangong = bumenyuangongCheck.getSelectionModel().getSelection();
	    	      if (bumenyuangong.length > 0) {//当有选择的数据的时候   
	    	           for(var i = 0; i < bumenyuangong.length; i++) {
	    	               empIdList.push(recemp);
	    	           }
	    	      }
	    	      var bumenxgyuangong = bumenxgyuangongcheck.getSelectionModel().getSelection();
	    	      if (bumenxgyuangong.length > 0) {//当有选择的数据的时候   
	    	           for (var i = 0; i < bumenxgyuangong.length; i++) {
	    	               empList.push(recempxg);
	    	           }
	    	      }
	    	      var emp=Ext.encode(empIdList);
	    	      var juese=Ext.encode(jueseList);
	    	      var empxg=Ext.encode(empList);
	    	      //部门相关员工
	    	      if(isList=='bumenxgyuangongcheck' || activeTabId=='bumenxgyuangongcheck' ){
	    	    	  me.Jurisdiction(empxg,juese,flag);
	    	    	  return;
	    	      }
	    	      
	    	      alert(juese);
	    	     //部门下属员工
	    	      if(isList=='bumenyuangongCheck'|| activeTabId=='bumenyuangongCheck'){
	    	    	  me.Jurisdiction(emp,juese,flag);
	    	    	  return;
	    	      }
	          }
          });
          btnDel.on({
        	  click:function() {
	        	  //获取角色id 并把id放到数组
	    	      var jueseList=[];
	    	      //获取部门下属员工id 并把id放到数组
	    	      var empIdList=[];
	    	      //获取相关员工id 并把id放到数组
	    	      var empList=[];
        	      var flag=1;
        	      var records = jueseChecksList.getSelectionModel().getSelection();
	    	      if (records.length > 0) {//当有选择的数据的时候   
	    	           for(var i = 0; i < records.length; i++) {
	    	               jueseList.push(rec);
	    	           }
	    	      }
	              var bumenyuangong = bumenyuangongCheck.getSelectionModel().getSelection();
	    	      if (bumenyuangong.length > 0) {//当有选择的数据的时候   
	    	           for(var i = 0; i < bumenyuangong.length; i++) {
	    	               empIdList.push(recemp);
	    	           }
	    	      }
	    	      var bumenxgyuangong = bumenxgyuangongcheck.getSelectionModel().getSelection();
	    	      if (bumenxgyuangong.length > 0) {//当有选择的数据的时候   
	    	           for (var i = 0; i < bumenxgyuangong.length; i++) {
	    	               empList.push(recempxg);
	    	           }
	    	      }
	    	      var emp=Ext.encode(empIdList);
	    	      var juese=Ext.encode(jueseList);
	    	      var empxg=Ext.encode(empList);
	    	      
	    	      alert(juese);
	         	  //部门相关员工
	    	      if(isList=='bumenxgyuangongcheck'|| activeTabId=='bumenxgyuangongcheck'){
	    	    	  me.Jurisdiction(empxg,juese,flag);
	    	    	  return;
	    	      }
	    	     //部门下属员工
	    	      if(isList=='bumenyuangongCheck'|| activeTabId=='bumenyuangongCheck'){
	    	    	  me.Jurisdiction(emp,juese,flag);
	    	    	  return;
	    	      }
	          }
          });
      
            if (Ext.typeOf(me.callback) == "function") {
                me.callback(me);
            }
        }
    },
    getAppInfoFromServer:function(id, callback) {
        var me = this;
        var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
        Ext.Ajax.defaultPostHeader = "application/json";
        Ext.Ajax.request({
            async:false,
            url:url,
            method:"GET",
            timeout:2e3,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var appInfo = "";
                    if (result.ResultDataValue && result.ResultDataValue != "") {
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if (Ext.typeOf(callback) == "function") {
                        var obj = {
                            success:false,
                            ErrorInfo:"没有获取到应用组件信息!"
                        };
                        if (appInfo != "") {
                            obj = {
                                success:true,
                                appInfo:appInfo
                            };
                        }
                        callback(obj);
                    }
                } else {
                    if (Ext.typeOf(callback) == "function") {
                        var obj = {
                            success:false,
                            ErrorInfo:'获取应用组件信息失败！错误信息【<b style="color:red">' + result.ErrorInfo + "</b>】"
                        };
                        callback(obj);
                    }
                }
            },
            failure:function(response, options) {
                if (Ext.typeOf(callback) == "function") {
                    var obj = {
                        success:false,
                        ErrorInfo:"获取应用组件信息请求失败！"
                    };
                    callback(obj);
                }
            }
        });
    }
});