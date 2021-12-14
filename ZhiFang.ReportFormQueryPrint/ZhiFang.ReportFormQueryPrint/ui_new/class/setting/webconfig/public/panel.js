Ext.define("Shell.class.setting.webconfig.public.panel", {
    extend: 'Shell.class.setting.base.public.panel',
    //itemStyle: "margin-top:10px;margin-left:30px",
    autoScroll:true,
    bodyStyle:'overflow-x:hidden;',
    GetAllSetting: function () {
        var me = this;
        Shell.util.Msg.showInfo("修改配置时请停止使用程序防止出现故障！");
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/LoadWebConfig" ,
            async: false,
            method: 'get',
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
                if (rs.success) {
                    var items = Ext.JSON.decode(rs.ResultDataValue);
                    for (var i = 0; i < items.length; i++) {
                    	if(items[i].key.indexOf("ConnectionString")>=0){
                    		var  values =  items[i].value.split(';');//获取键对应的value值并根据;分割
                    		for (var a = 0 ; a<values.length;a++) {//将value拆分成单个配置项
                    			var sonitem = values[a].split('=');
                    			for(var b = 0;b<me.items.length;b++){
		                    		var parano =  me.items.items[b].getComponent(items[i].key+"_"+sonitem[0]);
			                        if (parano) {
			                            parano.setValue(sonitem[1]);
		                        	}	
		                    	}
                    		}		
                    	}else{
	                    	for(var a = 0;a<me.items.length;a++){
	                    		var parano =  me.items.items[a].getComponent(items[i].key);
		                        if (parano) {
		                            parano.setValue(items[i].value);
	                        	}	
	                    	}		
                		}
                    }
                }
            }
        });
    },
    getItem:function (itemIdName) {
		
	    var me = this;
	    var item = '';
	    for (var i = 0; i < me.items.items.length; i++) {
	        var flag = me.items.items[i].getComponent(itemIdName);
	        if (flag != null) {
	            item = flag;
	            break;
	        }
	    }
	    return item;
	},
    createItems: function () {
        var me = this;
        var items = [];
       items.push({
            xtype: 'fieldset',
            title: '现用库链接(必填项)',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            itemId:'ReportFormQueryPrintConnectionString',
            defaults: {
                width: 300
            },
            items: [
				{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'ReportFormQueryPrintConnectionString_data source',
		            itemId: 'ReportFormQueryPrintConnectionString_data source',
		            fieldLabel: '数据库地址',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库所在服务器的IP地址！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'ReportFormQueryPrintConnectionString_user id',
		            itemId: 'ReportFormQueryPrintConnectionString_user id',
		            fieldLabel: '数据库用户名',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库用户名称！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'ReportFormQueryPrintConnectionString_password',
		            itemId: 'ReportFormQueryPrintConnectionString_password',
		            fieldLabel: '数据库用户密码',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库用户密码！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'ReportFormQueryPrintConnectionString_initial catalog',
		            itemId: 'ReportFormQueryPrintConnectionString_initial catalog',
		            fieldLabel: '数据库名称',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库名称！'
		                    })
		                }
		            }
		        }
			]
       });
        items.push({
            xtype: 'fieldset',
            title: '历史库链接(非必填)',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            itemId:'HistoryConnectionString',
            defaults: {
                width: 300
            },
            items: [
				{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'HistoryConnectionString_data source',
		            itemId: 'HistoryConnectionString_data source',
		            fieldLabel: '数据库地址',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库所在服务器的IP地址！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'HistoryConnectionString_user id',
		            itemId: 'HistoryConnectionString_user id',
		            fieldLabel: '数据库用户名',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库用户名称！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'HistoryConnectionString_password',
		            itemId: 'HistoryConnectionString_password',
		            fieldLabel: '数据库用户密码',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库用户密码！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'HistoryConnectionString_initial catalog',
		            itemId: 'HistoryConnectionString_initial catalog',
		            fieldLabel: '数据库名称',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库名称！'
		                    })
		                }
		            }
		        }
			]
       });
		items.push({
            xtype: 'fieldset',
            title: '备份库链接(非必填)',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            itemId:'BackupsConnectionString',
            defaults: {
                width: 300
            },
            items: [
				{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'BackupsConnectionString_data source',
		            itemId: 'BackupsConnectionString_data source',
		            fieldLabel: '数据库地址',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库所在服务器的IP地址！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'BackupsConnectionString_user id',
		            itemId: 'BackupsConnectionString_user id',
		            fieldLabel: '数据库用户名',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库用户名称！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'BackupsConnectionString_password',
		            itemId: 'BackupsConnectionString_password',
		            fieldLabel: '数据库用户密码',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库用户密码！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'BackupsConnectionString_initial catalog',
		            itemId: 'BackupsConnectionString_initial catalog',
		            fieldLabel: '数据库名称',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库名称！'
		                    })
		                }
		            }
		        }
			]
       });
       items.push({
            xtype: 'fieldset',
            title: '数据库业务类型(必选)',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		            xtype: 'combobox',
		            style: me.itemStyle,
		            name: 'DBSourceType',
		            itemId: 'DBSourceType',
		            fieldLabel: '数据库类型',
		            displayField: 'text', valueField: 'value',
		            store: Ext.create('Ext.data.Store', {
		                fields: ['text', 'value'],
		                data: [
		                    { text: '6.6库', value: 'ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66' },
		                    { text: '报告库', value: 'ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter' }
		                ]
		            }),
		            
		        }
			]
       });
        
       items.push({
            xtype: 'fieldset',
            title: '打印次数同步',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		            xtype: 'combobox',
		            style: me.itemStyle,
		            name: 'IsLisAddPrintTime',
		            itemId: 'IsLisAddPrintTime',
		            fieldLabel: '向LIS同步',
		            displayField: 'text', valueField: 'value',
		            store: Ext.create('Ext.data.Store', {
		                fields: ['text', 'value'],
		                data: [
		                    { text: '同步', value: '1' },
		                    { text: '不同步', value: '0' }
		                ]
		            }),
		            
		        },{
		            xtype: 'combobox',
		            style: me.itemStyle,
		            name: 'IsMEGroupSampleFormAddPrintTime',
		            itemId: 'IsMEGroupSampleFormAddPrintTime',
		            fieldLabel: '向框架微生物同步',
		            displayField: 'text', valueField: 'value',
		            store: Ext.create('Ext.data.Store', {
		                fields: ['text', 'value'],
		                data: [
		                    { text: '同步', value: '1' },
		                    { text: '不同步', value: '0' }
		                ]
		            }),
		            
		        }
			]
       });
       items.push({
            xtype: 'fieldset',
            title: 'LIS库连接(非必填)',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            itemId:'LISConnectionString',
            defaults: {
                width: 300
            },
            items: [
				{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'LISConnectionString_data source',
		            itemId: 'LISConnectionString_data source',
		            fieldLabel: '数据库地址',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库所在服务器的IP地址！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'LISConnectionString_user id',
		            itemId: 'LISConnectionString_user id',
		            fieldLabel: '数据库用户名',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库用户名称！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'LISConnectionString_password',
		            itemId: 'LISConnectionString_password',
		            fieldLabel: '数据库用户密码',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库用户密码！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'LISConnectionString_initial catalog',
		            itemId: 'LISConnectionString_initial catalog',
		            fieldLabel: '数据库名称',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库名称！'
		                    })
		                }
		            }
		        }
			]
       });
       items.push({
            xtype: 'fieldset',
            title: 'LabStar库连接(非必填)',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            itemId:'LabStarConnectionString',
            defaults: {
                width: 300
            },
            items: [
				{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'LabStarConnectionString_data source',
		            itemId: 'LabStarConnectionString_data source',
		            fieldLabel: '数据库地址',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库所在服务器的IP地址！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'LabStarConnectionString_user id',
		            itemId: 'LabStarConnectionString_user id',
		            fieldLabel: '数据库用户名',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库用户名称！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'LabStarConnectionString_password',
		            itemId: 'LabStarConnectionString_password',
		            fieldLabel: '数据库用户密码',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库用户密码！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'LabStarConnectionString_initial catalog',
		            itemId: 'LabStarConnectionString_initial catalog',
		            fieldLabel: '数据库名称',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入数据库名称！'
		                    })
		                }
		            }
		        }
			]
       });
       items.push({
            xtype: 'fieldset',
            title: '页面配置登录密码',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'StaticUserNo',
		            itemId: 'StaticUserNo',
		            fieldLabel: '静态密码',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请输入密码！'
		                    })
		                }
		            }
		        }
			]
	   });
	   items.push(
		   {
			xtype: 'fieldset',
			title: '其他',
			style: me.itemStyle,
			width:400,
			defaultType: 'textfield',
			defaults: {
				width: 300
			},
			items: [
				{
					xtype: 'textfield',
					style: me.itemStyle,
					name: 'PointType',
					itemId: 'PointType',
					fieldLabel: '查询RFGraphData数据时要过滤的PointType类型',
					listeners: {
						render: function (field, p) {
							Ext.QuickTips.init();
							Ext.QuickTips.register({
								target: field.el,
								text: '请输入类型编号，多种类型用逗号隔开'
							})
						}
					}
				},
				{
		            xtype: 'combobox',
		            style: me.itemStyle,
		            name: 'IsUseFrxGeneratePDF',
		            itemId: 'IsUseFrxGeneratePDF',
		            fieldLabel: '是否使用frx模板生成PDF',
		            displayField: 'text', valueField: 'value',
		            store: Ext.create('Ext.data.Store', {
		                fields: ['text', 'value'],
		                data: [
		                    { text: '是', value: '1' },
		                    { text: '否', value: '0' }
		                ]
		            }),
		            
		        }
			]
	});
        return items;
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
	                     rs = me.savePublicSetting();
	                     if(rs.success == true){
	                     	Shell.util.Msg.showInfo("保存成功！");
	                     }else{
	                     	Shell.util.Msg.showError("保存失败！");
	                     }
                    }
                }
            }]
        });
        return [tooblar];
    },
    savePublicSetting:function () {
        var me = this;
        var list = [];
        var rs = null;
        //console.log(me.items);
        for (var i = 0; i < me.items.keys.length; i++) {
        	var itemid = me.items.items[i].itemId+"";
        	if(itemid.indexOf("ConnectionString") >= 0){
        		var hash = {};
        		var value = [];
        		for(var a = 0; a < me.items.items[i].items.keys.length; a++){
        			if (me.items.items[i].items.keys[a] == 'not') continue;
        			var kay = me.items.items[i].items.keys[a]+"";
		            var str = me.items.items[i].getComponent(me.items.items[i].items.keys[a]).getValue();
		            var valueStr = kay.split('_')[1]+"="+str;
		            value.push(valueStr);
		        }
        		hash["key"] = itemid;
	            hash["value"] = value.join(';');  
	            list.push(hash);
        	}else{
        		for(var a = 0; a < me.items.items[i].items.keys.length; a++){
	        		if (me.items.items[i].items.keys[a] == 'not') continue;
		            var hash = {};
		            var str = me.items.items[i].getComponent(me.items.items[i].items.keys[a]).getValue();
		            hash["key"] = me.items.items[i].items.keys[a];
		            hash["value"] = str;            
		            list.push(hash);
		        }
        	}
        }
        //console.log(list);
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/UpdateWebConfig",
            async: false,
            method: 'POST',
            params: Ext.encode({ "model": list }),
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    },
});