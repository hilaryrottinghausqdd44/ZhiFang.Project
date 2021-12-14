Ext.define("Shell.class.setting.base.select.addPanel", {
    extend: 'Shell.ux.panel.AppPanel',
    width: 1100,
    height: 600,
    gridStore:'',
    layout: 'border',
    AddUrl: '/ServiceWCF/DictionaryService.svc/AddSelectTempale',
    appType:'',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    createDockedItems: function () {
            var me = this;
            var tooblar = Ext.create('Ext.toolbar.Toolbar', {
                width: 100,
                items: [{
                    xtype: 'button', text: '保存',
                    iconCls: 'button-save',
                    listeners: {
                        click: function () {
                            var rs = me.InsertColumnsTempale();
                            me.fireEvent("save",me,rs);
                        }
                    }
                }]
            });
            return [tooblar];
    },
    createItems: function () {
        var me = this;
        me.columnsList = Ext.create("Shell.class.setting.base.select.addList", {
            region: 'west',
            itemId: 'columnsList',
            title:'待选列',
            width:'30%',
            listeners: {
                itemclick: function (m, record, item, index) {
                    var addColumns = me.getComponent("addColumns");
                    var store = addColumns.getStore();
                    var gStore = me.gridStore;
                    var flag = true;
                    gStore.each(function (records) {
                        if (record.data.SelectName == records.get("SelectName")) {
                            flag = false;
                            return false;
                        }
                    });
                    if (flag) {
                        var newData = record.data;
                        newData["ShowName"] = record.data.CName;
                        newData["ShowOrderNo"] = 0;
                        newData["IsShow"] = "是";
                        store.loadData([newData], true);
                        me.getComponent("columnsList").store.removeAt(index);
                    } else {
                        Shell.util.Msg.showError("已配置此列");
                    }
                    
                }
            }
        });
        me.columnsAdd = Ext.create("Shell.class.setting.base.select.addColumns", {
            region: 'center',
            itemId:'addColumns',
            title: '已选列',
            listeners: {
                rebockColumns: function (m, r) {
                    var addColumns = me.getComponent("columnsList");
                    var store = addColumns.getStore();
                    store.loadData([r], true);
                }
            },
            width: 600
        });
        return [me.columnsList,me.columnsAdd];
    },
    InsertColumnsTempale: function () {
        var me = this;
        var arr = [];
        var addColumns = me.getComponent("addColumns");
        var store = addColumns.getStore();
        store.each(function (record) {
            arr.push({
                /*"JsCode": record.get("JsCode"),*/
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
                "OrderNo": record.get("OrderNo")                
            });
        });
        var rs = "";
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.AddUrl,
            async: false,
            method: 'POST',
            params: Ext.encode({ "selectTempale": arr }),
            success: function (response, options) {
                 rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    }
});