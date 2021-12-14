Ext.define("Shell.reportSetting.class.base.select.grid", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetAllSelectTemplate',
    deleteUrl: '/ServiceWCF/DictionaryService.svc/deleteSelectTempale',
    multiSelect:true,
    selType: 'checkboxmodel',
    appType: '',
    /**默认每页数量*/
    defaultPageSize: 20,
    pagingtoolbar: 'number',
    //selModel: new Ext.selection.CheckboxModel({ checkOnly: true }),
    initComponent: function () {
        var me = this;
        me.selectUrl += '?appType=' + this.appType;
        me.columns = [
            {
                dataIndex: 'CName',
                text: '列名',
                width: 50,
                sortable: false
            }, {
                dataIndex: 'ShowName',
                text: '显示名称',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'SelectName',
                text: '字段名',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'TextWidth',
                text: '文字宽度',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'Width',
                text: '总体宽度',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'Site',
                text: '站点',
                editor: true,
                width: 60,
                sortable: false
            }, {
                dataIndex: 'ShowOrderNo',
                text: '显示顺序',
                width: 60,
                editor: true,
                sortable: false
            }, {
                dataIndex: 'IsShow',
                text: '是否显示',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'STID',
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
                xtype: 'button', text: '添加查询项',
                iconCls: 'button-add',
                listeners: {
                    click: function () {
                        Shell.util.Win.open("Shell.reportSetting.class.base.select.addPanel", {
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
        var pagingtoolbar = me.createPagingToolbar();
        return [pagingtoolbar, tooblar];
    },
    deleteTemplate: function (records) {
        var CTIDarry = [];
        for (var i = 0; i < records.length; i++) {
            CTIDarry.push(records[i].data.STID);
        }
        var me = this;
        var rs = "";
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.deleteUrl,
            async: false,
            method: 'POST',
            params: Ext.encode({ "STIDList": CTIDarry }),
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    }
});