Ext.define("Shell.class.weixin.blcloentcontrol.clientGrid", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: false,
    appType: '',
    /**获取列表数据服务*/
    selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBusinessLogicClientControlByHQL?isPlanish=true',
    deleteUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelBusinessLogicClientControl',
    multiSelect: true,
    pagingtoolbar: 'number',
    selType: 'checkboxmodel',
    /**默认每页数量*/
    defaultPageSize: 25,
    clientGridRecord:'',
    initComponent: function () {
        var me = this;
        me.columns = [
             {
             	text:'实验室名称',dataIndex:'BusinessLogicClientControl_CLIENTELE_CNAME',width:250,
             	sortable:false
             } ,{
             	text:'是否使用',dataIndex:'BusinessLogicClientControl_Flag',width:100,
             	sortable:false,hidden:true
             },{
             	text:'人员ID',dataIndex:'BusinessLogicClientControl_Account',width:100,
             	sortable:false
             },{
             	text:'实验室ID',dataIndex:'BusinessLogicClientControl_CLIENTELE_Id',width:100,
             	sortable:false,hidden:true
             },{
             	text:'主键ID',dataIndex:'BusinessLogicClientControl_Id',isKey:true,hidden:true,hideable:false
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
                        JShell.Win.open("Shell.class.weixin.blcloentcontrol.addPanel", {
                            gridStore:me.store,
                            formType:"add",
							width:800,
							height:600,
							clientGridRecord:me.clientGridRecord,
                            listeners: {
                                save: function (m, str) {
									Ext.MessageBox.alert("成功提示",str);
									m.close();
									me.onSearch();
                                    /* if (str.success) {
                                    	Ext.MessageBox.alert("成功提示","添加成功");
                                        m.close();
                                    } else {
                                        JShell.Msg.error("添加失败");
	                                    console.log(str.ErrorInfo);
                                    } */
                                }
                            }
                        }).show();
                    }
                }
            }, {
                xtype: 'button', text: '删除',
                iconCls: 'button-del',
                listeners: {
                    click: function () {
                        JShell.Msg.del(function (v) {
                            if (v == 'ok') {
                                var record = me.getSelectionModel().getSelection();
                                var rs = me.deleteTemplate(record);
                                if (rs.success) {
                                   me.onSearch();
                                }else{
                                	JShell.Msg.error("删除失败");
                                }
                            }
                        });
                    }
                }
                }
            ]
        });
        //分页栏
        var pagingtoolbar = me.createPagingtoolbar();
        return [pagingtoolbar,tooblar];
    },
    deleteTemplate: function (records) {       
        var me = this;
        var rs = "";		
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: JShell.System.Path.ROOT + me.deleteUrl+"?id="+records[0].data.BusinessLogicClientControl_Id,
            async: false,
            method: 'Get',
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    }
});