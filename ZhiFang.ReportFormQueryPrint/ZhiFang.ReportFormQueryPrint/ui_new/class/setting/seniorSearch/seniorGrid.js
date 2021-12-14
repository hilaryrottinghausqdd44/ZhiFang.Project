Ext.define("Shell.class.setting.seniorSearch.seniorGrid", {
    extend: 'Shell.ux.panel.AppPanel',
    width: 1100,
    height: 600,
    gridStore:'',
    layout: 'border',
    AddUrl: '/ServiceWCF/DictionaryService.svc/AddSeniorSetting',
    PublicUrl: "/ServiceWCF/DictionaryService.svc/UpdatePublicSetting",
    selectUrl:"/ServiceWCF/DictionaryService.svc/GetSeniorPublicSetting",
    appType:'',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    afterRender:function(){
		var me = this;
		me.callParent(arguments);		
		me.getParaValue = me.getParaValue();
	},
    createDockedItems: function () {
            var me = this;
            var tooblar = Ext.create('Ext.toolbar.Toolbar', {
            	itemId:'toolb',
                width: 100,
                items: [{
                    xtype: 'button', text: '启用',
                    iconCls: 'button-save',
                    listeners: {
                        click: function () {
                        	var ParaValue = true; 
		                   	var ParaNo = "isSeniorSetting";
		                   	var SName = me.appType;
		                   	var Name = "查询打印页面配置";
		                   	var ParaType = "config";
		                   	var ParaDesc = "bool";
		                   	var models = [];
		                   	var state = 1;
		                   	models.push({
		                   		"ParaValue": ParaValue,
		                   		"ParaNo" : ParaNo,
		                   		"SName" : SName,
		                   		"Name" : Name,
		                   		"ParaType" : ParaType,
		                   		"ParaDesc" : ParaDesc		                   		
		                   	});
		                   	me.ChangeParameter(models,state);
                            
                        }
                    }
                },{
                    xtype: 'button', text: '禁用',
                    iconCls: 'button-save',
                    listeners: {
                        click: function () {
                        	var ParaValue = false; 
		                   	var ParaNo = "isSeniorSetting";
		                   	var SName = me.appType;
		                   	var Name = "查询打印页面配置";
		                   	var ParaType = "config";
		                   	var ParaDesc = "bool";
		                   	var models = [];
		                   	var state = 2;
		                   	models.push({
		                   		"ParaValue": ParaValue,
		                   		"ParaNo" : ParaNo,
		                   		"SName" : SName,
		                   		"Name" : Name,
		                   		"ParaType" : ParaType,
		                   		"ParaDesc" : ParaDesc		                   		
		                   	});
		                   	me.ChangeParameter(models,state);
                            
                        }
                    }
                },
                {
                    xtype: 'button', text: '保存',
                    itemId:'save',
                    iconCls: 'button-save',
                    listeners: {
                        click: function () {
                            var rs = me.InsertColumnsTempale();
                            if(rs.success){
                            	Shell.util.Msg.showInfo("保存成功");
                            	var addColumns = me.getComponent("addColumns");
                            	addColumns.load();
                            }else{
                            	Shell.util.Msg.showError("保存失败");
                            }
                            
                        }
                    }
                }
                ]
            });
            return [tooblar];
    },
    createItems: function () {
        var me = this;
        me.columnsList = Ext.create("Shell.class.setting.seniorSearch.addList", {
            region: 'west',
            itemId: 'columnsList',
            title:'待选列',
            appType:me.appType,
            width:'30%',
            listeners: {
                itemclick: function (m, record, item, index) {
                    var addColumns = me.getComponent("addColumns");
                    var store = addColumns.getStore();
                    var records = store.data.items;
                    var flag = true;
                    for(var i=0;i<records.length;i++){
                    	if (record.data.SelectName == records[i].data.SelectName) {
	                       flag = false;
                           return false;
                    	}
                    }
                    if (flag) {
                        var newData = record.data;
                        newData["ShowName"] = record.data.CName;
                        newData["ShowOrderNo"] = 0;
                        newData["IsShow"] = "是";
                        store.loadData([newData], true);
                    } else {
                        Shell.util.Msg.showError("已配置此列");
                    }
                }
            }
        });
        me.columnsAdd = Ext.create("Shell.class.setting.seniorSearch.addColumns", {
            region: 'center',
            itemId:'addColumns',
            title: '已选列',
            appType:me.appType,
            width: 600
            
        });
        return [me.columnsList,me.columnsAdd];
    },
    InsertColumnsTempale: function () {
        var me = this;
        var selectTempale = [];
        var addColumns = me.getComponent("addColumns");
        var store = addColumns.getStore();
        
        store.each(function (record) {
            selectTempale.push({
                "JsCode": record.get("JsCode"),
                "Type": record.get("Type"),
                "SelectName": record.get("SelectName"),
                "Xtype": record.get("Xtype"),
                "Mark": record.get("Mark"),
                "Listeners": record.get("Listeners"),
                "SID": record.get("SID"),
                "CName": record.get("CName"),
                "IsShow": record.get("IsShow") == "是" ? true :(record.get("IsShow") == "否" ? false : record.get("IsShow")),
                "ShowName": record.get("ShowName"),
                "TextWidth": record.get("TextWidth"),
                "OrderMode": record.get("OrderMode"),
                "OrderFlag": record.get("OrderFlag"),
                "ShowOrderNo": record.get("ShowOrderNo"),
                "AppType": me.appType,
                "Width": record.get("Width"),
                "Site": record.get("Site"),
                "OrderNo": record.get("OrderNo"),
                "SearchType": 2,
                "STID":record.get("STID") == null ? 0:(record.get("STID") =="" ? 0:record.get("STID"))
            });
            console.log(record.get("STID"));
        });
        var rs = "";
        //console.log(Ext.encode({ "selectTempale": arr }));
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.AddUrl,
            async: false,
            method: 'POST',
            params: Ext.JSON.encode({selectTempale: selectTempale}),
            success: function (response, options) {
                  rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    },
    ChangeParameter:function(models,state){
    	var me = this;
    	Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.PublicUrl,
            async: false,
            method: 'POST',
            params: Ext.JSON.encode({models: models}),
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
                if(rs.success){
                	if(state == 1){
                		me.getComponent("toolb").getComponent("save").enable();
                		me.getComponent("addColumns").enable();
                		me.getComponent("columnsList").enable();
                	}else{
                		me.getComponent("toolb").getComponent("save").disable();
                		me.getComponent("addColumns").disable();
                		me.getComponent("columnsList").disable();
                	}
                	
                }else{
                	Shell.util.Msg.showError("修改失败");
                }
            }
        });
    },
    getParaValue:function(){
    	var me = this;
    	me.selectUrl +="?SName='"+me.appType+"'&ParaNo='isSeniorSetting'";
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.selectUrl,
            async: false,
            method: 'get',
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
                obresponse = JSON.parse(rs.ResultDataValue);
                if(obresponse[0] != null && obresponse[0] != ""){
                	if(obresponse[0].ParaValue == 'true'){
                		me.getComponent("toolb").getComponent("save").enable();
                		me.getComponent("addColumns").enable();
                		me.getComponent("columnsList").enable();
                	}else{
                		me.getComponent("toolb").getComponent("save").disable();
                		me.getComponent("addColumns").disable();
                		me.getComponent("columnsList").disable();
                	}
                }
            }
        }); 
    }
});