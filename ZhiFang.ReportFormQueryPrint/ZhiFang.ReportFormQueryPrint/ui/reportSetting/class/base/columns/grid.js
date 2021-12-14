Ext.define("Shell.reportSetting.class.base.columns.grid", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    appType: '',
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetAllColumnsTemplate',
    deleteUrl: '/ServiceWCF/DictionaryService.svc/deleteColumnsTempale',
    multiSelect: true,
    pagingtoolbar: 'number',
    selType: 'checkboxmodel',
    /**默认每页数量*/
    defaultPageSize: 20,
    //selModel: new Ext.selection.CheckboxModel({ checkOnly: true }),
    initComponent: function () {
        var me = this;
        me.selectUrl+='?appType='+this.appType;
        me.columns = [
            {
                dataIndex: 'CName',
                text: '列名',
                width: 50,
                sortable: false
            }, {
                dataIndex: 'ShowName',
                editor: true,
                text: '显示名称',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'ColumnName',
                editor: true,
                text: '字段名',
                width: 120,
                sortable: false
            }, {
                dataIndex: 'Width',
                text: '宽度',
                editor: true,
                width: 40,
                sortable: false
            }, {
                dataIndex: 'Site',
                text: '站点',
                editor: true,
                width: 60,
                sortable: false
            }, {
                dataIndex: 'OrderNo',
                text: '显示顺序',
                width: 60,
                editor: true,
                sortable: false
            }, {
                dataIndex: 'OrderFlag',
                text: '是否排序',
                width: 60,
                editor: true,
                sortable: false
            }, {
                dataIndex: 'OrderDesc',
                text: '排序顺序',
                width: 60,
                editor: true,
                sortable: false
            }, {
                dataIndex: 'OrderMode',
                text: '排序方式',
                width: 60,
                editor: true,
                sortable: false
            }, {
                dataIndex: 'Render',
                text: '自定义方法',
                width: 80,
                editor: true,
                sortable: false
            }, {
                dataIndex: 'IsShow',
                text: '是否显示',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'CTID',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }
        ];
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    createDockedItems: function () {
        var me = this;
        var tooblar = Ext.create('Ext.toolbar.Toolbar', {
            width: 100,
            items: [{
                xtype: 'button', text: '添加显示列',
                iconCls: 'button-add',
                listeners: {
                    click: function () {
                        Shell.util.Win.open("Shell.reportSetting.class.base.columns.addPanel", {
                            appType: me.appType,
                            gridStore:me.store,
                            listeners: {
                                save: function (m, rs) {
                                    if (rs.success) {
                                        m.close();
                                        me.load();
                                    } else {
                                        Shell.util.Msg.showError(rs.ErrorInfo);
                                    }
                                }
                            }
                        });
                    }
                }
            }, {
                xtype: 'button', text: '删除',
                iconCls: 'button-del',
                listeners: {
                    click: function () {
                        Shell.util.Msg.confirmDel(function (v) {
                            if (v == 'ok') {
                                var record = me.getSelectionModel().getSelection();
                                console.log(record);
                                var rs = me.deleteTemplate(record);
                                if (rs.success) {
                                    me.load();
                                }
                            }
                        });
                    }
                }
            }
            ]
        });
        //分页栏
        var pagingtoolbar = me.createPagingToolbar();
        return [pagingtoolbar,tooblar];
    },
    deleteTemplate: function (records) {
        var CTIDarry = [];
        for (var i = 0; i < records.length; i++) {
            CTIDarry.push(records[i].data.CTID);
        }
        var me = this;
        var rs = "";
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.deleteUrl,
            async: false,
            method: 'POST',
            params: Ext.encode({ "CTIDList": CTIDarry }),
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    }
});