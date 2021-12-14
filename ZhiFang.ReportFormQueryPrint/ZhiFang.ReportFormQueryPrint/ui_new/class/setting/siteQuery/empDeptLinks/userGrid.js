Ext.define("Shell.class.setting.siteQuery.empDeptLinks.userGrid", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    appType: '',
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetPUser',
    multiSelect: true,
    pagingtoolbar: 'number',
    /**默认每页数量*/
    defaultPageSize: 20,
    //selModel: new Ext.selection.CheckboxModel({ checkOnly: true }),
    initComponent: function () {
        var me = this;
        me.columns = [
             {
                dataIndex: 'UserNo',
                text: '用户编号',
                width: 100,
                sortable: false
            },{
                dataIndex: 'CName',
                editor: true,
                text: '用户姓名',
                width: 100,
                sortable: false
            }, {
                dataIndex: 'ShortCode',
                editor: true,
                text: '账号',
                width: 100,
                sortable: false
            }, {
                dataIndex: 'Password',
                editor: true,
                text: '密码',
                width: 100,
                sortable: false
            },{
                dataIndex: 'Role',
                text: '角色',
                editor: true,
                width: 100,
                sortable: false
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
            	xtype: 'textfield',
            	name: 'CName', 
            	fieldLabel: '姓名',
            	itemId:"QCNAME",
            	labelWidth: 35, 
            	width: 150,
            	value:''
            },
            {
                xtype: 'button', text: '查询',
                iconCls: 'button-search',
                listeners: {
                    click: function () {
                       //console.log(tooblar.getComponent("QCNAME").value);
                       var qcname= tooblar.getComponent("QCNAME").value;
                       if(qcname == '' || qcname == null){
                       		Shell.util.Msg.showWarning('请输入查询条件')
                       }else{
                       		var sql = "?Where=CName like '%"+qcname+"%'";
                       		me.selectUrl += sql;
                       		me.onSearch();
                       }
                    }
                }
            }
            /*, {
                xtype: 'button', text: '添加用户',
                iconCls: 'button-add',
                
                listeners: {
                    click: function () {
                        Shell.util.Win.open("Shell.class.setting.siteQuery.empDeptLinks.form", {
                            appType: me.appType,
                            gridStore:me.store,
                            formType:"add",
							width:400,
							height:250,
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
                xtype: 'button', text: '删除用户',
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
                }*/
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