Ext.define("Shell.class.setting.base.columns.grid", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    appType: '',
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetAllColumnsTemplate',
    deleteUrl: '/ServiceWCF/DictionaryService.svc/deleteColumnsTempale',
    setDefualtUrl: '/ServiceWCF/DictionaryService.svc/SetColumnsDefaultSetting',
    multiSelect: true,
    pagingtoolbar: 'number',
    selType: 'checkboxmodel',
    /**默认每页数量*/
    defaultPageSize: 20,
    //selModel: new Ext.selection.CheckboxModel({ checkOnly: true }),
    initComponent: function () {
        var me = this;
        me.selectUrl+='?appType='+encodeURI(me.appType);
        me.columns = [
            {
                dataIndex: 'CName',
                text: '列名',
                width: 60,
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
                editor: true,
                sortable: false
            }, {
                dataIndex: 'OrderFlag',
                text: '是否排序',
                width: 60,
                editor: true,
                sortable: false,
                renderer: function (v) {
	                if(v == true){
	                	v = "是";
	                }else if(v == false){
	                	v = "否";
	                }
	                return v;
	            }
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
                sortable: false,
                renderer: function (v) {
	                if(v == "Desc"){
	                	v = "倒序";
	                }else if(v == "Asc"){
	                	v = "正序";
	                }
	                return v;
	            }
            }, {
                dataIndex: 'Render',
                text: '自定义方法',
                width: 120,
                editor: true,
                sortable: false
            }, {
                dataIndex: 'IsShow',
                text: '是否显示',
                width: 60,
                sortable: false,
                renderer: function (v) {
	                if(v == true){
	                	v = "是";
	                }else if(v == false){
	                	v = "否";
	                }
	                return v;
	            }
            }, {
                dataIndex: 'CTID',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }, {
                dataIndex: 'ColID',
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
                        Shell.util.Win.open("Shell.class.setting.base.columns.addPanel", {
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
                }, 
                {
	                xtype: 'button', text: '修改',
	                iconCls: 'button-edit',
	                listeners: {
	                    click: function () {
	                    	var store = me.getSelectionModel().getSelection();//被选中的一条数据
	                    	if(store.length == 1){//判断是否只选中一条数据
	                    		Shell.util.Win.open("Shell.class.setting.base.columns.edit", {
		                            appType: me.appType,
		                            editStore:store[0].data,//选中数据的字段
		                            listeners: {
		                                save: function (m, rs) {
		                                    if (rs.success) {
		                                        m.close();
		                                        me.load();
		                                        Shell.util.Msg.showInfo("修改成功！");
		                                    } else {
		                                        Shell.util.Msg.showError(rs.ErrorInfo);
		                                    }
		                                }
		                            }
		                        });	
	                    	}else if(store.length < 1){
	                    		Shell.util.Msg.showError("请选择修改数据");
	                    	}else{
	                    		Shell.util.Msg.showError("暂不支持多选修改，请选择一项数据修改");
	                    	}
	                    }
	                }
	            },
                {
                    xtype: 'button', text: '恢复默认',
                    iconCls: 'button-config',
                    listeners: {
                        click: function () {
                            Shell.util.Msg.showMsg({
                                title: '恢复默认',
                                msg: '恢复默认将会清空现在的设置，使用系统默认设置，确定要恢复吗？',
                                icon: Ext.Msg.WARNING,
                                buttons: Ext.Msg.OKCANCEL,
                                callback: function (v) {
                                    if (v == 'ok') {
                                        me.RestoreDefault();
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
    },
    RestoreDefault: function () {
        var me = this;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.setDefualtUrl +'?appType=' + me.appType,
            async: false,
            method: 'GET',
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
                if (rs.success) {
                    me.load();
                }
            }
        });
    }
});