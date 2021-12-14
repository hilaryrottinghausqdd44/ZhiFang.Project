Ext.define("Shell.class.weixin.blcloentcontrol.addColumns", {
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
                        JShell.Msg.del(function (but) {
                            if (but != "ok") return;
                            me.store.remove(record);
                            me.fireEvent("rebockColumns", me, record);
                        });
                    }
                }
            ]
        }, 
             {
                dataIndex: 'CLIENTELE_Id',
                text: '实验室ID',
                width: 60,
                sortable: false
            },{
                dataIndex: 'CLIENTELE_CNAME',
                text: '实验室名称',
                width: 100,
                sortable: false
            }
        ];
        me.callParent(arguments);
    },
    createStore: function () {
        var store = new Ext.data.Store({
            autoLoad: true,
            fields: ["CLIENTELE_Id", "CLIENTELE_CNAME"],
            data: []
        });
        return store;
    }
});