Ext.define("Shell.class.weixin.blcloentcontrol.addPanel", {
    extend: 'Shell.ux.panel.AppPanel',
    width: 600,
    title:'科室配置',
    height: 400,
    layout: 'border',
    gridStore:'',
    appType:'',
    AddUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddBusinessLogicClientControl',
    clientGridRecord:'',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },

    createDockedItems: function () {
            var me = this;
            var tooblar = Ext.create('Ext.toolbar.Toolbar', {
                width: 100,
                items: [{
                    xtype: 'button', text: '保存',
                    iconCls: 'button-save',
                    listeners: {
                        click: function () {
                            var rs = me.InsertColumnsTempale();
                            me.fireEvent("save",me,rs);
                        }
                    }
                }]
            });
            return [tooblar];
    },
    createItems: function () {
        var me = this;
        me.columnsList = Ext.create("Shell.class.weixin.blcloentcontrol.addList", {
            region: 'west',
            itemId: 'columnsList',
            title:'待选列',
			gridStore:me.gridStore,
            width: '50%',
            listeners: {
                itemclick: function (m, record, item, index) {
                    var addColumns = me.getComponent("addColumns");
                    var store = addColumns.getStore();
                    var gStore = me.gridStore;
                    var flag = true;
                    gStore.each(function (records) {
                        if (record.data.BusinessLogicClientControl_CLIENTELE_CNAME == records.get("BusinessLogicClientControl_CLIENTELE_CNAME")) {
                            flag = false;
                            return false;
                        }
                    });
                    if (flag) {
                        var newData = record.data;
                        store.loadData([newData], true);
                        me.getComponent("columnsList").store.removeAt(index);
                    } else {
                        Shell.util.Msg.showError("已配置此列");
                    }
                }
            }
        });
        me.columnsAdd = Ext.create("Shell.class.weixin.blcloentcontrol.addColumns", {
            region: 'center',
            itemId:'addColumns',
            title: '已选列',
            width: '50%' ,
            listeners: {
                rebockColumns: function (m, r) {
                    var addColumns = me.getComponent("columnsList");
                    var store = addColumns.getStore();
                    store.loadData([r], true);
                }
            }
        });
        return [me.columnsList,me.columnsAdd];
    },
    InsertColumnsTempale: function () {
        var me = this;
        var arr = [];
        var addColumns = me.getComponent("addColumns");
        var store = addColumns.getStore();
        store.each(function (record) {
            arr.push({
                "Account": me.clientGridRecord.RBACUser_Account,
                "CLIENTELE": {"Id":record.get("CLIENTELE_Id"),"DataTimeStamp":[0,0,0,0,0,0,0,0]},
                "Flag":1
            });
        });
		
		
		var len = arr.length,
		    successCount = 0,
		    failCount = 0,
		    rs = "";
		for(var i=0;i<len;i++){
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
			    url: JShell.System.Path.ROOT + me.AddUrl,
			    async: false,
			    method: 'POST',
			    params: Ext.encode({entity : arr[i] }),
			    success: function (response, options) {
					 var rdata = Ext.JSON.decode(response.responseText);
					if (rdata.success === true) {
						successCount++;
					} else {
						failCount++;
					}
					if (successCount + failCount == len) {
						rs="删除完毕！成功：" + successCount + "个，失败：" + failCount + "个";
					}			        
			    }
			});
		};		
        return rs;
    }
});