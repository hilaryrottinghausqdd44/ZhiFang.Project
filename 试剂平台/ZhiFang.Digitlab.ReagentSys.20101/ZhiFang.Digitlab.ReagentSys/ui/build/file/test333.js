Ext.onReady(function() {
    Ext.QuickTips.init();
    Ext.Loader.setConfig({
        enabled:true
    });
 
    Ext.define("RLSalesTracking", {
        extend:"Ext.panel.Panel",
        panelType:"Ext.panel.Panel",
        alias:"widget.RLSalesTracking",
        title:"销售跟踪管理",
        width:1100,
        height:450,
        autoScroll:true,
        layout:{
            type:"border",
            regionWeights:{
                north:4,
                south:3,
                west:2,
                east:1
            }
        },
        appInfos:[ {
            appId:"5513892948321543982",
            itemId:"gzxxbqddz",
            header:false,
            title:"",
            border:true,
            region:"north",
            split:false,
            collapsible:false,
            collapsed:false,
            width:770,
            height:26
        }, {
            appId:"4821316204111928455",
            itemId:"RLSalesCList",
            header:false,
            title:"",
            border:true,
            region:"center",
            split:false,
            collapsible:false,
            collapsed:false,
            width:415,
            height:345
        }, {
            appId:"5660591269462598328",
            itemId:"RLSalesTrackingList",
            header:false,
            title:"",
            border:true,
            region:"east",
            split:true,
            collapsible:true,
            collapsed:false,
            width:350,
            height:345
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
                getAppInfo(id, callback, "BTDAppComponents_ClassCode", false);
            }
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
        getCallback:function(appInfo) {
            var me = this;
            var callback = function(obj) {
                var panel = null;
                var callback2 = function(panel) {
                    me.initLink(panel);
                };
                var list = Ext.clone(me.moduleOperList);
                if (list) {
                    var appModuleOperList = [];
                    for (var i = 0; i < list.length; i++) {
                        var arr = list[i]["RBACModuleOper_UseCode"].split(".");
                        if (arr.length > 2 && arr[1] == appInfo.itemId) {
                            list[i]["RBACModuleOper_UseCode"] = arr.slice(1).join(".");
                            appModuleOperList.push(list[i]);
                        }
                    }
                    var list2 = [];
                    for (var i = 0; i < appModuleOperList.length; i++) {
                        var arr = appModuleOperList[i]["RBACModuleOper_UseCode"].split(".");
                        if (arr.length == 2) {
                            appInfo[arr[1]] = appModuleOperList[i]["RBACModuleOper_Id"];
                        } else if (arr.length > 2) {
                            list2.push(appModuleOperList[i]);
                        }
                    }
                    if (list2.length > 0) {
                        appInfo.moduleOperList = list2;
                    }
                }
                appInfo.callback = callback2;
                if (obj.success && obj.appInfo != "") {
                    var ClassCode = obj.appInfo.ClassCode;
                    var cl = eval(ClassCode);
                    panel = Ext.create(cl, appInfo);
                } else {
                    appInfo.html = obj.ErrorInfo;
                    panel = Ext.create("Ext.panel.Panel", appInfo);
                }
                me.add(panel);
                if (me.panelType == "Ext.tab.Panel") {
                    if (appInfo.defaultactive) {
                        me.defaultactive = appInfo.itemId;
                    }
                    me.setActiveTab(panel);
                }
            };
            return callback;
        },
        initLink:function(panel) {
            var me = this;
            var appInfos = me.getAppInfos();
            var length = appInfos.length;
            me.comNum++;
            if (me.comNum == length) {
                if (me.panelType == "Ext.tab.Panel") {
                    var f = function() {
                        me.setActiveTab(me.defaultactive);
                        me.un("tabchange", f);
                    };
                    me.on("tabchange", f);
                }
                var _RLSalesCList = me.getComponent("RLSalesCList");
                _RLSalesCList.on({
                    select:function(view, record) {
                        var HITACHIBusinessProject_Id = record.get("HITACHIBusinessProject_Id");
                        var hql = "hitachibusinessprojecttrace.HITACHIBusinessProject.Id=" + HITACHIBusinessProject_Id;
                        var _RLSalesTrackingList = me.getComponent("RLSalesTrackingList");
                        _RLSalesTrackingList.load(hql);
                    }
                });
                var RLSalesTrackingList = me.getComponent("RLSalesTrackingList");
                var RLSalesCList = me.getComponent("RLSalesCList");
                RLSalesCList.on({
                    toolbarDefault1Click:function() {
                        var recorddatas = RLSalesCList.getSelectionModel().getSelection();
                        if (recorddatas.length > 0) {
                            Ext.Msg.confirm("提示", "确定要将选中的信息登记状态修改为：跟踪信息不全？", function(button) {
                                if (button == "yes") {
                                    if (recorddatas.length > 0) {
                                        var hqlWhere = "%27跟踪信息不全%25%27";
                                        var selectURL = getRootPath() + "/HITACHIService.svc/ST_UDTO_SearchBBusinessStatusByHQL?isPlanish=true&fields=BBusinessStatus_Name,BBusinessStatus_Id,BBusinessStatus_DataTimeStamp&page=1&start=0&limit=10000";
                                        var selectField = "&where=bbusinessstatus.Name like " + hqlWhere;
                                        selectURL = selectURL + selectField;
                                        Ext.Ajax.defaultPostHeader = "application/json";
                                        Ext.Ajax.request({
                                            url:selectURL,
                                            method:"GET",
                                            success:function(response, opts) {
                                                var result = Ext.JSON.decode(response.responseText);
                                                if (result.success) {
                                                    if (result.ResultDataValue && result.ResultDataValue != "") {
                                                        var r = Ext.JSON.decode(result.ResultDataValue);
                                                        var bbsDataTimeStamp = "" + r.list[0]["BBusinessStatus_DataTimeStamp"];
                                                        var bbStatusId = r.list[0]["BBusinessStatus_Id"];
                                                        var url = getRootPath() + "/HITACHIService.svc/ST_UDTO_UpdateHITACHIBusinessProjectByField";
                                                        var list = me.getComponent("RLSalesCList");
                                                        var records = list.getSelectionModel().getSelection();
                                                        var objArr = [];
                                                        if (bbsDataTimeStamp && bbsDataTimeStamp != undefined) {
                                                            objArr = bbsDataTimeStamp.split(",");
                                                        }
                                                        var BBusinessStatus = {
                                                            Id:bbStatusId,
                                                            DataTimeStamp:objArr
                                                        };
                                                        var Id = "";
                                                        var dataTimeStamp = "";
                                                        var dataTimeStampArr = [];
                                                        for (var i = 0; i < records.length; i++) {
                                                            var record = records[i];
                                                            Id = record.get("HITACHIBusinessProject_Id");
                                                            dataTimeStamp = record.get("HITACHIBusinessProject_DataTimeStamp");
                                                            if (dataTimeStamp && dataTimeStamp != undefined) {
                                                                dataTimeStampArr = dataTimeStamp.split(",");
                                                            }
                                                            var newAdd = {
                                                                Id:Id,
                                                                DataTimeStamp:dataTimeStampArr,
                                                                BBusinessStatus:BBusinessStatus
                                                            };
                                                            var obj = {
                                                                entity:newAdd,
                                                                fields:"DataTimeStamp,Id,BBusinessStatus_Id,BBusinessStatus_DataTimeStamp"
                                                            };
                                                            var params = Ext.JSON.encode(obj);
                                                            var callbackUpate = function(responseText) {
                                                                var result = Ext.JSON.decode(responseText);
                                                                if (result.success) {
                                                                    Ext.Msg.alert("提示", "更改完成");
                                                                } else {
                                                                    Ext.Msg.alert("提示", "更改失败");
                                                                }
                                                            };
                                                            var defaultPostHeader = "application/json";
                                                            postToServer(url, params, callbackUpate, defaultPostHeader);
                                                        }
                                                        RLSalesCList.load();
                                                    }
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                });
                RLSalesTrackingList.on({
                    afterOpenEditWin:function(win) {
                        win.on({
                            beforeSave:function() {
                                var HITACHIClientName = win.getComponent("HITACHIBusinessProjectTrace_HITACHIBusinessProject_HITACHIClientName");
                                HITACHIClientName.setValue("");
                            },
                            saveClick:function() {
                                var list1 = me.getComponent("RLSalesCList");
                                var records = list1.getSelectionModel().getSelection();
                                if (records.length == 1) {
                                    var record = records[0];
                                    var id = record.get(list1.objectName + "_Id");
                                    list1.autoSelect = id;
                                    list1.load(true);
                                }
                            }
                        });
                    },
                    afterOpenAddWin:function(win) {
                        var records = RLSalesCList.getSelectionModel().getSelection();
                        if (records.length == 1) {
                            var key = "HITACHIBusinessProjectTrace_HITACHIBusinessProject_HITACHIClientName";
                            var value = records[0].get("HITACHIBusinessProject_HITACHIClientName");
                            win.setValueByItemId(key, value);
                            var key = "HITACHIBusinessProjectTrace_HITACHIBusinessProject_Id";
                            var value = records[0].get("HITACHIBusinessProject_Id");
                            var value1 = records[0].get("HITACHIBusinessProjectTrace_HITACHIBusinessProject_Id");
                            win.setValueByItemId(key, value);
                            var key = "HITACHIBusinessProjectTrace_HITACHIBusinessProject_DataTimeStamp";
                            var value = records[0].get("HITACHIBusinessProject_DataTimeStamp");
                            win.setValueByItemId(key, value);
                        }
                        win.on({
                            saveClick:function() {
                                var list1 = me.getComponent("RLSalesCList");
                                var records = list1.getSelectionModel().getSelection();
                                if (records.length == 1) {
                                    var record = records[0];
                                    var id = record.get(list1.objectName + "_Id");
                                    list1.autoSelect = id;
                                    list1.load(true);
                                }
                            }
                        });
                    }
                });
                var gzxxbqddz = me.getComponent("gzxxbqddz");
              //跟踪全部单子
              var allbtn= me.getComponent("gzxxbqddz").getComponent("addClick");
              allbtn.on({
	                click:function(){
	                	var RLSalesCList = me.getComponent("RLSalesCList");

	                	var RLSalesTrackingList = me.getComponent("RLSalesTrackingList");

	                	var hqlwherevalue = gzxxbqddz.getValue();
        
	                	var fields = "HITACHIBusinessProject_BHITACHIClient_BProvince_Name,HITACHIBusinessProject_CongressName,HITACHIBusinessProject_UpdateTime,HITACHIBusinessProject_HITACHIClientName,HITACHIBusinessProject_AskEquipHITACI_Name,HITACHIBusinessProject_AuditTime,HITACHIBusinessProject_ContendComp,HITACHIBusinessProject_BBusinessStatus_DataTimeStamp,HITACHIBusinessProject_ContendEquip,HITACHIBusinessProject_Id,HITACHIBusinessProject_BudgetBuyTimeCount,HITACHIBusinessProject_DataTimeStamp,HITACHIBusinessProject_ExpirationDate,HITACHIBusinessProject_IsFiling,HITACHIBusinessProject_BBusinessStatus_Name,HITACHIBusinessProject_WorkStatus,HITACHIBusinessProject_BBusinessStatus_Id,HITACHIBusinessProject_Purpose";

	                	RLSalesCList.url = getRootPath() + "/HITACHIService.svc/ST_UDTO_SearchHITACHIBusinessProjectByHQL?isPlanish=true&&page=1&limit=5000&searchkey=&fields=" + fields+"&where=(hitachibusinessproject.BBusinessStatus.Name like '登记有效')";
	                	var callback = function(data) {
	                	    var result = Ext.JSON.decode(data);
	                	    if (result.success) {
	                	        if (result.ResultDataValue && result.ResultDataValue != "") {
	                	            var appInfo = Ext.JSON.decode(result.ResultDataValue);
	                	            var userid = "";
	                	            if (appInfo.list && appInfo.list != "" && appInfo.list != undefined) {
	                	                RLSalesCList.loadData(appInfo.list);
	                	                RLSalesTrackingList.loadData([]);
	                	            }
	                	        } else {
	                	            RLSalesTrackingList.loadData([]);
	                	            RLSalesCList.loadData([]);
	                	        }
	                	        Ext.util.Cookies.set("000660", "");
	                	    }
	                	};

	                	var v = RLSalesCList.loaddata || "";

	                	Ext.util.Cookies.set("000660", v);
	                	
	                	
	                  
	                	getToServer(RLSalesCList.url, callback);
	              	}
	            });
              //跟踪不全单子
              var notallBtn= me.getComponent("gzxxbqddz").getComponent("gzxxbqddz");
              notallBtn.on({
	                click:function(){
                      var hqlwherevalue = gzxxbqddz.getValue();
                      var fields = "HITACHIBusinessProject_CongressName,HITACHIBusinessProject_HITACHIClientName,HITACHIBusinessProject_UpdateTime,HITACHIBusinessProject_AuditTime,HITACHIBusinessProject_BBusinessStatus_Name,HITACHIBusinessProject_BBusinessStatus_Id,HITACHIBusinessProject_BBusinessStatus_DataTimeStamp,HITACHIBusinessProject_Id,HITACHIBusinessProject_DataTimeStamp";
                      RLSalesCList.url = getRootPath() + "/HITACHIService.svc/ST_UDTO_SearchHITACHIBusinessProjectTraceUncompleteByHQL?isPlanish=true&&page=1&limit=1000&searchkey=&fields=" + fields;
                      var RLSalesCList = me.getComponent("RLSalesCList");
                      var RLSalesTrackingList = me.getComponent("RLSalesTrackingList");
                      var callback = function(data) {
                          var result = Ext.JSON.decode(data);
                          if (result.success) {
                              if (result.ResultDataValue && result.ResultDataValue != "") {
                                  var appInfo = Ext.JSON.decode(result.ResultDataValue);
                                  var userid = "";
                                  if (appInfo.list && appInfo.list != "" && appInfo.list != undefined) {
                                     RLSalesCList.loadData(appInfo.list);
                                     RLSalesTrackingList.loadData([]);
                                 }
                              } else {
                                  RLSalesTrackingList.loadData([]);
                                  RLSalesCList.loadData([]);
                             }
                             Ext.util.Cookies.set("000660", "");
                        }
                    };
//                 alert(   RLSalesCList.getInternalWhere());
                	
                	
                    var v = RLSalesCList.loaddata || "";
                    Ext.util.Cookies.set("000660", v);
                    getToServer(RLSalesCList.url, callback);
                    
//                    RLSalesCList.setCount(count);
	              	}
	            });

                if (Ext.typeOf(me.callback) == "function") {
                    me.callback(me);
                }
            }
        }
    });

    
    var viewport = Ext.create("Ext.Viewport", {
        //layout:'fit',
        width:400,
        height:300,
        autoScroll:true,
        items:[ {
            xtype:"RLSalesTracking",
            //类代码别名称
            itemId:"test1"
        } ]
    });
});