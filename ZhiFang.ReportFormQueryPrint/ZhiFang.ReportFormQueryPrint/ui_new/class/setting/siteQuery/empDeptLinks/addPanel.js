Ext.define("Shell.class.setting.siteQuery.empDeptLinks.addPanel", {
    extend: 'Shell.ux.panel.AppPanel',
    width: 1100,
    title:'科室配置',
    height: 600,
    layout: 'border',
    gridStore:'',
    appType:'',
    AddUrl: '/ServiceWCF/DictionaryService.svc/AddEmpDeptLinks',
    deptGridRecord:'',
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
        me.columnsList = Ext.create("Shell.class.setting.siteQuery.empDeptLinks.addList", {
            region: 'west',
            itemId: 'columnsList',
            title:'待选列',
            width: '50%',
            listeners: {
                itemclick: function (m, record, item, index) {
                    var addColumns = me.getComponent("addColumns");
                    var store = addColumns.getStore();
                    var gStore = me.gridStore;
                    var flag = true;
                    gStore.each(function (records) {
                        if (record.data.CName == records.get("DeptCName")) {
                            flag = false;
                            return false;
                        }
                    });
                    if (flag) {
                        var newData = record.data;
                        store.loadData([newData], true);
                        me.getComponent("columnsList").store.removeAt(index);
                    } else {
                        Shell.util.Msg.showError("已配置此列");
                    }
                }
            }
        });
        me.columnsAdd = Ext.create("Shell.class.setting.siteQuery.empDeptLinks.addColumns", {
            region: 'center',
            itemId:'addColumns',
            title: '已选列',
            width: '50%' ,
            listeners: {
                rebockColumns: function (m, r) {
                    var addColumns = me.getComponent("columnsList");
                    var store = addColumns.getStore();
                    store.loadData([r], true);
                }
            }
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
                "EDLID": 0,
                "UserNo": me.deptGridRecord.UserNo,
                "DeptNo": record.get("DeptNo"),
                "UserCName": me.deptGridRecord.CName,
                "ShortCode": me.deptGridRecord.ShortCode,
                "DeptCName": record.get("CName")
            });
        });
        var rs = "";
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.AddUrl,
            async: false,
            method: 'POST',
            params: Ext.encode({entity : arr }),
            success: function (response, options) {
                 rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    }
});