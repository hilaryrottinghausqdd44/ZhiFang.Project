Ext.define("Shell.reportSetting.class.base.select.addPanel", {
    extend: 'Shell.ux.panel.AppPanel',
    width: 900,
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
        me.columnsList = Ext.create("Shell.reportSetting.class.base.select.addList", {
            region: 'west',
            itemId: 'columnsList',
            title:'选择列表',
            width: 300,
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
                        newData["IsShow"] = 1;
                        store.loadData([newData], true);
                        me.getComponent("columnsList").store.removeAt(index);
                    } else {
                        Shell.util.Msg.showError("已配置此列");
                    }
                    
                }
            }
        });
        me.columnsAdd = Ext.create("Shell.reportSetting.class.base.select.addColumns", {
            region: 'east',
            itemId:'addColumns',
            title: '设置列表',
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
                "JsCode": record.get("JsCode"),
                "Type": record.get("Type"),
                "SelectName": record.get("SelectName"),
                "Xtype": record.get("Xtype"),
                "Mark": record.get("Mark"),
                "Listeners": record.get("Listeners"),
                "SID": record.get("SID"),
                "CName": record.get("CName"),
                "IsShow": record.get("IsShow"),
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