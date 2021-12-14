Ext.define("Shell.class.siteQuery.addEmpDept.unselect", {
    extend: 'Ext.grid.Panel',
    autoSelect: false,
    //multiSelect: true,
    //selType: 'checkboxmodel',
    celledit:function () {
        return Ext.create('Ext.grid.plugin.CellEditing', {
            clickToEdit: 1
        });
    },
    initComponent: function () {
        var me = this;
        me.plugins = [me.celledit()];
        me.store = me.createStore();
        me.columns = [{
            xtype: 'actioncolumn', align: 'center', text: '操作', width: 35,
            items: [
                {
                    tooltip: '删除',
                    iconCls: 'button-del',
                    handler: function (view, row, col, item, e) {
                        var record = view.getStore().getAt(row);
                        Shell.util.Msg.confirmDel(function (but) {
                            if (but != "ok") return;
                            me.store.remove(record);
                            me.fireEvent("rebockColumns", me, record);
                        });
                    }
                }
            ]
        }, 
             {
                dataIndex: 'DeptNo',
                text: '科室ID',
                width: 60,
                sortable: false
            },{
                dataIndex: 'CName',
                text: '科室名称',
                width: 100,
                sortable: false
            }
        ];
        me.callParent(arguments);
    },
    createStore: function () {
        var store = new Ext.data.Store({
            autoLoad: true,
            fields: ["DeptNo", "CName"],
            data: []
        });
        return store;
    }
});