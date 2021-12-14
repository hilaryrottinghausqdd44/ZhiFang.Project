Ext.define("Shell.ReportPrint1.class.setting", {
    extend: 'Ext.panel.Panel',
    title: '参数设置',
    seachstore:'',
    initComponent: function() {
        var me = this;
        me.items = [];
        var bar = new Ext.create("Shell.ReportPrint1.class.bar", {
            seachstore: me.seachstore
        });
        me.items.push(bar);
        me.dockedItems = [{
            xtype: 'button', dock: 'bottom', text: '保存设置', iconCls: 'button-accept',
            listeners: {
                click: function (m) {
                    var gridstore = bar.items.items[1].getComponent("grd").store;
                    me.fireEvent("setting", me, gridstore);
                }
            }
        }];
        me.callParent(arguments);
    }
});
Ext.define("Shell.ReportPrint1.class.bar", {
    extend: 'Shell.ux.search.SearchToolbar',
    title: '参数设置',
    seachstore:'',
    initComponent: function () {
        var me = this;
        var store = Ext.create('Ext.data.Store', {
            storeId:'simpsonsStore',
            fields: ['Cname', 'Ename'],
            data: [{ "Cname": "卡号", "Ename": "ZDY3" }, { "Cname": "样本号", "Ename": "SAMPLENO" }, { "Cname": "条码号", "Ename": "SerialNo" }, { "Cname": "病历号", "Ename": "PATNO" }],
            
        });
        me.items = [
            [{
                type: 'other',
                xtype: "button",
                text: '添加查询条件',
                listeners:{
                    click: function() {
                        store.add({"text":"中文名称","value":"英文名称"});
                    }
                }
            }
            ]
            ,[{
                type:'other',
                xtype: 'grid',
                itemId:'grd',
                width: 320,
                plugins: [
		            Ext.create('Ext.grid.plugin.CellEditing', {
		                clicksToEdit: 1
		        })
                ],
                columns:[
                    {
                        dataIndex: 'text',
                        text: "中文名称",
                        width:150,
                        sortable: false,
                        draggable: false,
                        editor:{  
                            allowBlank:false  
                        },
                        hideable: false
                    }, {
                        dataIndex: 'value',
                        text: '英文名称',
                        editor:{  
                            allowBlank:false  
                        } ,
                        width: 150,
                        sortable: false,
                        draggable: false,
                        hideable: false
                    }
                ],
                store: me.seachstore
            }
            ]
        ];
        me.callParent(arguments);
    }
});