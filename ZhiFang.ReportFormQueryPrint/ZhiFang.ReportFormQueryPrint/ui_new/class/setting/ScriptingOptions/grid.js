Ext.define("Shell.class.setting.ScriptingOptions.grid", {
	extend: 'Shell.ux.grid.Panel',
	/**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    /**获取列表数据服务*/
    selectUrl: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/GetDBVersion',
    //deleteUrl: '/ServiceWCF/DictionaryService.svc/deleteColumnsTempale',
    multiSelect: true,
    appType: '',
    pagingtoolbar: 'number',
    selType: 'checkboxmodel',
    /**默认每页数量*/
    defaultPageSize: 50,
    //初始化方法
    initComponent: function () {
        var me = this;
    	//获取数据
    	me.store = me.createStore();
    	//表头
        me.columns = [
            {
                dataIndex: 'ProcedureVersion',
                text: '程序集版本',
                width: 200,
                sortable: false//排序
            }
        ];
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    createDockedItems: function () {
        var me = this;
        var tooblar = Ext.create('Ext.toolbar.Toolbar', {
            width: 300,
            itemId:'vtooblar',
            items: [{
	            	//添加功能
	                xtype: 'button', text: '一键升级',
	                iconCls: 'button-add',
	                listeners: {
	                    click: function () {
	                        Ext.Ajax.defaultPostHeader = 'application/json';
					        Ext.Ajax.request({
					            url: Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/DBupdate",
					            async: false,
					            method: 'get',
					            success: function (response, options) {
					                rs = Ext.JSON.decode(response.responseText);
					            }
					        });
					        if(rs.success == true){
	                     		Shell.util.Msg.showInfo("升级成功！");
		                    }else{
		                     	Shell.util.Msg.showError("升级失败！");
		                    }
	                    }
	                }
	            },  '-',{
		            text: '',
		            itemId:'toobtext'
		        }
            ]
        });
        //分页栏
        var pagingtoolbar = me.createPagingToolbar();
        return [pagingtoolbar,tooblar];
    },
    
    
    //获取数据
    createStore: function () {
        var me = this;
        var config = {
            fields: ['ProcedureVersion'],
            pageSize:50,
            proxy: {
                type: 'ajax',
                url: me.selectUrl,
                reader: { type: 'json', totalProperty:'count',root: 'rows' },
                extractResponseData: function(response){
                	var rs = Ext.JSON.decode(response.responseText);
                	if(rs.success){
                		var ResultDataValue = Ext.JSON.decode(rs.ResultDataValue);
                		me.getComponent('vtooblar').getComponent('toobtext').text='当前数据库版本:'+ ResultDataValue[0].DBVersion;
                	}else{
                		Shell.util.Msg.showError("查询错误，请检查程序日志！");
                	}
                    return ResultDataValue;
                }
            },
            autoLoad: true
        };

        return Ext.create('Ext.data.Store', config);
    }
    
   
   
    	
});
