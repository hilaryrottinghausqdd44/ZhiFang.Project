

Ext.define("Shell.class.setting.xmlConfig.grid", {
	extend: 'Shell.ux.grid.Panel',
	/**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    /**获取列表数据服务*/
    selectUrl: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/LoadConfig?fileName=ReportFromShowXslConfig.xml',
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
                dataIndex: 'ReportType',
                text: '小组类别',
                width: 200,
                sortable: false//排序
            }, {
                dataIndex: 'XSLName',
                editor: true,
                text: '模板名称',
                width: 200,
                sortable: false
            }, {
                dataIndex: 'PageName',
                text: '页面名称',
                editor: true,
                width: 200,
                sortable: false
            }, {
                dataIndex: 'Name',
                text: '样式名',
                editor: true,
                width: 200,
                sortable: false
            }
//          , {
//              dataIndex: 'OrderNo',
//              text: '显示顺序',
//              width: 150,
//              editor: true,
//              sortable: false
//          }
        ];
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    createDockedItems: function () {
        var me = this;
        var tooblar = Ext.create('Ext.toolbar.Toolbar', {
            width: 100,
            items: [{
            	//添加功能
                xtype: 'button', text: '添加',
                iconCls: 'button-add',
                listeners: {
                    click: function () {
                        Shell.util.Win.open("Shell.class.setting.xmlConfig.form", {
                        	title:"添加",
                        	width:400,
                        	formType:'add',
                        	height:200,
                        	getStore:me.store.data,
                        	toEng:me.toEnglish,
                            appType:me.appType,
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
            	//修改功能
                xtype: 'button', text: '修改',
                iconCls: 'button-edit',
                listeners: {
                    click: function () {
                    	var records = me.getSelectionModel().getSelection();
                    	var delLength = [];
				        var str;
				        for (var i = 0; i < records.length; i++) {
				            delLength.push(records[i].index);
				        }
                    	if(delLength.length != 1){
                    		Shell.util.Msg.showError("请正确选择修改项!");
                    	}else{
                    	    Shell.util.Win.open("Shell.class.setting.xmlConfig.form", {
	                        	title:"修改",
	                        	width:400,
	                        	formType:'update',
	                        	height:200,
	                        	record:records,//选中项
	                        	getStore:me.store.data,//所有数据
                        		toEng:me.toEnglish,
	                            appType:me.appType,
	                            listeners: {
	                                save: function (m, str) {
	                                    if (str.success) {
	                                    	Ext.MessageBox.alert("成功提示","修改成功");
	                                        m.close();
	                                        me.load();
	                                    } else {
	                                        Shell.util.Msg.showError("修改失败");
	                                        console.log(str.ErrorInfo);
	                                    }
	                                }
	                            }
	                        });
                    	}
                    }
                }
            },{
            	//删除功能
                xtype: 'button', text: '删除',
                iconCls: 'button-del',
                listeners: {
                    click: function () {
                        Shell.util.Msg.confirmDel(function (v) {
                        	var store = me.store.data;
                            if (v == 'ok') {
                                var record = me.getSelectionModel().getSelection();
                                var str = me.deleteTemplate(record,store);
                                if (str.success) {
                                	Ext.MessageBox.alert("成功提示","删除成功");
                                    me.load();
                                } else {
                                    Shell.util.Msg.showError(str.ErrorInfo);
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
    //删除方法
    deleteTemplate: function (records,store) {
        var delLength = [];
        var str;
        for (var i = 0; i < records.length; i++) {
            delLength.push(records[i].index);
        }
        
        if(delLength.length>0){//是否选择数据
        	var me = this;
			var list = [];
			store.each(function (record) {
			    list.push({
				    "ReportType": record.get("ReportType"),
			        "XSLName": record.get("XSLName"),
					"PageName": record.get("PageName"),
					"Name": record.get("Name")//,
					 //"lastName": record.get("lastName")
				});
			});
			//删除指定数据 得到剩余数据数组 list
			var len = list.length;
	        for(var i = len;i>=0;i--){
	        	for(var j = 0;j<delLength.length;j++){
	        		if(i == delLength[j]){
	        			list.splice(i,1);
	        		}
	        	}
	        }
	        list = me.toEnglish(list);
	        //拼接发送格式
	        var config = {
				"?xml": {"@version":"1.0","@encoding":"utf-8"},
				"DataSet": { "ReportFromShowXslConfig": list }
			};
			config = Ext.JSON.encode(config);
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				method: 'POST',
				async: false,
			    url: Shell.util.Path.rootPath +'/ServiceWCF/ReportFormService.svc/SaveConfig',
			    params:Ext.JSON.encode({
					"fileName": "ReportFromShowXslConfig.xml",
	            	"configStr": config
				}),
				success: function(response){
				    str = Ext.JSON.decode(response.responseText);
				}
			});
	        return str;
        }else{
        	str = {ErrorInfo:"未选择数据"};
        	return str;
        }
    },
    
    //获取数据
    createStore: function () {
        var me = this;
        var config = {
            fields: ['ReportType', 'XSLName', 'PageName', 'Name'],
            pageSize:50,
            proxy: {
                type: 'ajax',
                url: me.selectUrl,
                reader: { type: 'json', totalProperty:'count',root: 'rows' },
                extractResponseData: function(response){
                	var data = me.extractResponseData(response);
					me.toChinese(data);
                    return data;
                }
            },
            autoLoad: true
        };

        return Ext.create('Ext.data.Store', config);
    },
    /**数据转化*/
    extractResponseData:function(response){
    	//if()
        var me = this,
            result = Ext.JSON.decode(response.responseText);
		
        if (result.success && Ext.JSON.decode(result.ResultDataValue).DataSet != null) {
            var data = Ext.JSON.decode(result.ResultDataValue) || {};
            result.rows = data.DataSet.ReportFromShowXslConfig;
            result.ResultDataValue = null;
            me.xmlConfig = data["?xml"];
        } else {
            result.rows = [];
        }
        response.responseText = Ext.JSON.encode(result);

        //me.enableControl();//启用所有的操作功能

        return response;
    },
   /*显示时小组类别将数据库中英文字段改为汉字*/
   toChinese:function(data){
   		var result = Ext.JSON.decode(data.responseText);
   		if(result.rows.length>0){
   			for(var i =0 ;i<result.rows.length;i++){
	   			switch(result.rows[i].ReportType){
	   				case "Normal":
	   					result.rows[i].ReportType = "生化类";
	   					break;
	   				case "Micro":
	   					result.rows[i].ReportType = "微生物";
	   					break;
	   				case "NormalIncImage":
	   					result.rows[i].ReportType = "生化类(图)";
	   					break;
	   				case "MicroIncImage":
	   					result.rows[i].ReportType = "微生物(图)";
	   					break;
	   				case "CellMorphology":
	   					result.rows[i].ReportType = "细胞形态学";
	   					break;
	   				case "FishCheck":
	   					result.rows[i].ReportType = "Fish检测(图)";
	   					break;
	   				case "SensorCheck":
	   					result.rows[i].ReportType = "院感检测(图)";
	   					break;
	   				case "ChromosomeCheck":
	   					result.rows[i].ReportType = "染色体检测(图)";
	   					break;
	   				case "PathologyCheck":
	   					result.rows[i].ReportType = "病理检测(图)";
	   					break;
	   			}
	   		}	
   		}
   		data.responseText = Ext.JSON.encode(result);
		return data;
   },
   /*小组类别将汉字段改为英文保存到数据库*/
   toEnglish:function(list){
   		var result = list;
   		if(result.length>0){
   			for(var i =0 ;i<result.length;i++){
	   			switch(result[i].ReportType){
	   				case "生化类":
	   					result[i].ReportType = "Normal";
	   					break;
	   				case "微生物":
	   					result[i].ReportType = "Micro";
	   					break;
	   				case "生化类(图)":
	   					result[i].ReportType = "NormalIncImage";
	   					break;
	   				case "微生物(图)":
	   					result[i].ReportType = "MicroIncImage";
	   					break;
	   				case "细胞形态学":
	   					result[i].ReportType = "CellMorphology";
	   					break;
	   				case "Fish检测(图)":
	   					result[i].ReportType = "FishCheck";
	   					break;
	   				case "院感检测(图)":
	   					result[i].ReportType = "SensorCheck";
	   					break;
	   				case "染色体检测(图)":
	   					result[i].ReportType = "ChromosomeCheck";
	   					break;
	   				case "病理检测(图)":
	   					result[i].ReportType = "PathologyCheck";
	   					break;
	   			}
	   		}	
   		}
		list = result;
		return list;
   }
    	
});
