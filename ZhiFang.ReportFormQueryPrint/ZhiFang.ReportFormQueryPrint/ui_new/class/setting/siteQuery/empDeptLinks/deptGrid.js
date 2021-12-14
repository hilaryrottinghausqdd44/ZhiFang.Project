Ext.define("Shell.class.setting.siteQuery.empDeptLinks.deptGrid", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: false,
    appType: '',
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetEmpDeptLinks',
    deleteUrl: '/ServiceWCF/DictionaryService.svc/DeleteEmpDeptLinks',
    multiSelect: true,
    pagingtoolbar: 'number',
    selType: 'checkboxmodel',
    /**默认每页数量*/
    defaultPageSize: 20,
    deptGridRecord:'',
    initComponent: function () {
        var me = this;
        me.columns = [
             {
                dataIndex: 'UserNo',
                text: '用户编号',
                width: 120,
                sortable: false
            },{
                dataIndex: 'UserCName',
                editor: true,
                text: '用户姓名',
                width: 120,
                sortable: false
            }, {
                dataIndex: 'ShortCode',
                editor: true,
                text: '账号',
                width: 120,
                sortable: false
            }, {
                dataIndex: 'DeptCName',
                editor: true,
                text: '科室名称',
                width: 120,
                sortable: false
            },{
                dataIndex: 'EDLID',
                text: 'ID',
                width: 120,
                sortable: false,
                hidden:true
            }
        ];
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    createDockedItems: function () {
        var me = this;
        var tooblar = Ext.create('Ext.toolbar.Toolbar', {
            width: 100,
            items: [
            {
                xtype: 'button', text: '添加',
                iconCls: 'button-add',
                
                listeners: {
                    click: function () {
                        Shell.util.Win.open("Shell.class.setting.siteQuery.empDeptLinks.addPanel", {
                            appType: me.appType,
                            gridStore:me.store,
                            formType:"add",
							width:600,
							height:400,
							deptGridRecord:me.deptGridRecord,
                            listeners: {
                                save: function (m, str) {
                                    if (str.success) {
                                    	Ext.MessageBox.alert("成功提示","添加成功");
                                        m.close();
                                        me.load();
                                    } else {
                                        Shell.util.Msg.showError("添加失败");
	                                    console.log(str.ErrorInfo);
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
                                }else{
                                	Shell.util.Msg.showError("删除失败");
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
        var EDLIDarry = [];
        for (var i = 0; i < records.length; i++) {
            EDLIDarry.push(records[i].data);
        }
        var me = this;
        var rs = "";
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.deleteUrl,
            async: false,
            method: 'POST',
            params: Ext.encode({ "entity": EDLIDarry }),
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    }
});