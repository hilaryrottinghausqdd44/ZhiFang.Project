Ext.define("Shell.class.setting.seniorSearch.addColumns", {
    extend: 'Shell.ux.grid.Panel',
     /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetSeniorSetting',
    delectUrl: '/ServiceWCF/DictionaryService.svc/deleteSelectTempale',
    defaultPageSize: 20,
    pagingtoolbar: 'number',
    appType:'',
    celledit:function () {
        return Ext.create('Ext.grid.plugin.CellEditing', {
            clickToEdit: 1
        });
    },
    initComponent: function () {
        var me = this;
        me.selectUrl += '?appType=' + encodeURI(me.appType);
        me.plugins = [me.celledit()];
        me.columns = [
       		{
                xtype: 'actioncolumn', align: 'center', text: '操作', width: 35,
                items: [
                    {
                        tooltip: '删除',
                        iconCls: 'button-del',
                        handler: function (view, row, col, item, e) {
                            var record = view.getStore().getAt(row);
                            Shell.util.Msg.confirmDel(function (but) {
                                if (but != "ok") return;
                                //me.store.remove(record);
                                var STID = record.data.STID;
                                Ext.Ajax.defaultPostHeader = 'application/json';
						        Ext.Ajax.request({
						            url: Shell.util.Path.rootPath + me.delectUrl,
						            async: false,
						            method: 'POST',
						            params: Ext.JSON.encode({STIDList: [STID]}),
						            success: function (response, options) {
						                   rs = Ext.JSON.decode(response.responseText);
						                    if(rs.success){
				                            	Shell.util.Msg.showInfo("删除成功");
				                            	me.load();
				                            }else{
				                            	Shell.util.Msg.showError("删除失败");
				                            }
						            }
						        });
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
                dataIndex: 'SelectName',
                text: '字段名',
                width: 120,
                sortable: false,
                editor: false
            }, {
                dataIndex: 'TextWidth',
                text: '文字宽度',
                editor: //true,
	                new Ext.form.field.Number({
	                	editable: true
	                }),
                width: 60,
                sortable: false
            }, {
                dataIndex: 'Width',
                text: '总体宽度',
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
                dataIndex: 'ShowOrderNo',
                text: '显示顺序',
                width: 60,
                editor: //true,
	                new Ext.form.field.Number({
	                	editable: true
	                }),
                sortable: false
            }, {
                dataIndex: 'IsShow',
                text: '是否显示',
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
                width: 60,
                sortable: false
            }, {
                dataIndex: 'SID',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }, {
                dataIndex: 'Xtype',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }, {
                dataIndex: 'Mark',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }, {
                dataIndex: 'Listeners',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }, {
                dataIndex: 'Type',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }, {
                dataIndex: 'JsCode',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            },{
                dataIndex: 'STID',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }
        ];
        me.callParent(arguments);
    }
});