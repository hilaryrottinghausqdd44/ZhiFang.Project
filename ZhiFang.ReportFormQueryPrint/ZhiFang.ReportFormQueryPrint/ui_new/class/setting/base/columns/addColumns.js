Ext.define("Shell.class.setting.base.columns.addColumns", {
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
                dataIndex: 'CName',
                text: '列名',
                width: 60,
                sortable: false,
                editor: false
            }, {
                dataIndex: 'ShowName',
                editor: true,
                text: '显示名称',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'ColumnName',
                editor: false,
                text: '字段名',
                width: 120,
                sortable: false
            }, {
                dataIndex: 'Width',
                text: '宽度',
                editor: //true,
	                new Ext.form.field.Number({
	                	editable: true
	                }),
                width: 60,
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
                editor: //true,
	                new Ext.form.field.Number({
	                	editable: true
	                }),
                sortable: false
            }, {
                dataIndex: 'OrderFlag',
                text: '是否排序',
                width: 60,
                editor: //true,
	                new Ext.form.field.ComboBox({
	                	editable: false,
					    store:  new Ext.data.Store({
					    	fields: ['abbr', 'name'],
						    data : [
						        {"abbr":"true", "name":"是"},
						        {"abbr":"false", "name":"否"}
						    ]
					    }),
					    queryMode: 'local',
					    displayField: 'name',
					    valueField: 'abbr',
	                }),
                renderer: function (v) {
	                if(v == "true"){
	                	v = "是";
	                }else if(v == "false"){
	                	v = "否";
	                }
	                return v;
	            },
                sortable: false
            }, {
                dataIndex: 'OrderDesc',
                text: '排序顺序',
                width: 60,
                editor: //true,
	                new Ext.form.field.Number({
	                	editable: true
	                }),
                sortable: false
            }, {
                dataIndex: 'OrderMode',
                text: '排序方式',
                width: 60,
                editor: //true,
	                new Ext.form.field.ComboBox({
	                	editable: false,
					    store:  new Ext.data.Store({
					    	fields: ['abbr', 'name'],
						    data : [
						        {"abbr":"Desc", "name":"倒序"},
						        {"abbr":"Asc", "name":"正序"}
						    ]
					    }),
					    queryMode: 'local',
					    displayField: 'name',
					    valueField: 'abbr',
	                }),
                renderer: function (v) {
	                if(v == "Desc"){
	                	v = "倒序";
	                }else if(v == "Asc"){
	                	v = "正序";
	                }
	                return v;
	            },
                sortable: false
            }, {
                dataIndex: 'IsShow',
                text: '是否显示',
                width: 60,
                editor: //true,
	                new Ext.form.field.ComboBox({
	                	editable: false,
					    store:  new Ext.data.Store({
					    	fields: ['abbr', 'name'],
						    data : [
						        {"abbr":"true", "name":"是"},
						        {"abbr":"false", "name":"否"}
						    ]
					    }),
					    queryMode: 'local',
					    displayField: 'name',
					    valueField: 'abbr',
	                }),
                renderer: function (v) {
	                if(v == "true"){
	                	v = "是";
	                }else if(v == "false"){
	                	v = "否";
	                }
	                return v;
	            },
                sortable: false
            },/* {
                dataIndex: 'Render',
                text: '自定义方法',
                width: 120,
                editor: true,
                sortable: false,
                editor: false
            },*/ {
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
            fields: ["OrderFlag", "OrderDesc", "OrderMode"/*, "Render"*/, "ColumnName", "ColID", "CName", "ShowName", "Width", "Site", "OrderNo", "IsShow"],
            data: []
        });
        return store;
    }
});