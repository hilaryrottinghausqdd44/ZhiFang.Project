Ext.define("Shell.class.setting.base.columns.addPanel", {
    extend: 'Shell.ux.panel.AppPanel',
    width: 1100,
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
        me.columnsList = Ext.create("Shell.class.setting.base.columns.addList", {
            region: 'west',
            itemId: 'columnsList',
            title:'待选列',
            width: '30%',
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
                        newData["OrderFlag"] = "否";
                        newData["OrderDesc"] = 0;
                        newData["IsShow"] = "是";
                        newData["OrderNo"] = 0;
                        store.loadData([newData], true);
                        me.getComponent("columnsList").store.removeAt(index);
                    } else {
                        Shell.util.Msg.showError("已配置此列");
                    }
                }
            }
        });
        me.columnsAdd = Ext.create("Shell.class.setting.base.columns.addColumns", {
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
                "IsShow": record.get("IsShow") == "是" ? true :(record.get("IsShow") == "否" ? false : record.get("IsShow")),
                //"Render": record.get("Render"),
                "CName": record.get("CName"),
                "ShowName": record.get("ShowName"),
                "ColumnName": record.get("ColumnName"),
                "Width": record.get("Width"),
                "Site": record.get("Site"),
                "OrderMode": record.get("OrderMode")  == "正序" ? Asc :(record.get("OrderMode") == "倒序" ? Desc : record.get("OrderMode")),
                "OrderFlag": record.get("OrderFlag") == "是" ? true :(record.get("OrderFlag") == "否" ? false : record.get("OrderFlag")),
                "OrderDesc": record.get("OrderDesc"),
                "AppType": me.appType,
                "OrderNo": record.get("OrderNo"),
                "IsUse":true
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