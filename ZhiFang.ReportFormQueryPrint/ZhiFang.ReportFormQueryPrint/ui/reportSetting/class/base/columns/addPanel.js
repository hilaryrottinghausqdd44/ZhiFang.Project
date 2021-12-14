Ext.define("Shell.reportSetting.class.base.columns.addPanel", {
    extend: 'Shell.ux.panel.AppPanel',
    width: 800,
    height: 600,
    layout: 'border',
    gridStore:'',
    appType:'',
    AddUrl: '/ServiceWCF/DictionaryService.svc/AddColumnsTempale',
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
        me.columnsList = Ext.create("Shell.reportSetting.class.base.columns.addList", {
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
                        if (record.data.ColumnName == records.get("ColumnName")) {
                            flag = false;
                            return false;
                        }
                    });
                    if (flag) {
                        var newData = record.data;
                        newData["ShowName"] = record.data.CName;
                        newData["OrderFlag"] = 0;
                        newData["OrderDesc"] = 0;
                        newData["IsShow"] = 1;
                        newData["OrderNo"] = 0;
                        store.loadData([newData], true);
                        me.getComponent("columnsList").store.removeAt(index);
                    } else {
                        Shell.util.Msg.showError("已配置此列");
                    }
                }
            }
        });
        me.columnsAdd = Ext.create("Shell.reportSetting.class.base.columns.addColumns", {
            region: 'east',
            itemId:'addColumns',
            title: '设置列表',
            width: 500
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
                "ColID": record.get("ColID"),
                "IsShow": record.get("IsShow"),
                "Render": record.get("Render"),
                "CName": record.get("CName"),
                "ShowName": record.get("ShowName"),
                "ColumnName": record.get("ColumnName"),
                "Width": record.get("Width"),
                "Site": record.get("Site"),
                "OrderMode": record.get("OrderMode"),
                "OrderFlag": record.get("OrderFlag"),
                "OrderDesc": record.get("OrderDesc"),
                "AppType": me.appType,
                "OrderNo": record.get("OrderNo")
            });
        });
        var rs = "";
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.AddUrl,
            async: false,
            method: 'POST',
            params: Ext.encode({ "columnsTemplate": arr }),
            success: function (response, options) {
                 rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    }
});