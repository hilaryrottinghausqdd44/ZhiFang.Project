Ext.define("Shell.reportSetting.class.base.columns.addColumns", {
    extend: 'Ext.grid.Panel',
    autoSelect: false,
    celledit:function () {
        return Ext.create('Ext.grid.plugin.CellEditing', {
            clickToEdit: 1
        });
    },
    initComponent: function () {
        var me = this;
        me.plugins = [me.celledit()];
        me.store = me.createStore();
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
                dataIndex: 'IsShow',
                text: '是否显示',
                width: 60,
                editor: true,
                sortable: false
            }
            , {
                dataIndex: 'Render',
                text: '自定义方法',
                width: 80,
                editor: true,
                sortable: false
            }, {
                dataIndex: 'ColID',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }
        ];
        me.callParent(arguments);
    },
    createStore: function () {
        var store = new Ext.data.Store({
            autoLoad: true,
            fields: ["OrderFlag", "OrderDesc", "OrderMode", "Render", "ColumnName", "ColID", "CName", "ShowName", "Width", "Site", "OrderNo", "IsShow"],
            data: []
        });
        return store;
    }
});