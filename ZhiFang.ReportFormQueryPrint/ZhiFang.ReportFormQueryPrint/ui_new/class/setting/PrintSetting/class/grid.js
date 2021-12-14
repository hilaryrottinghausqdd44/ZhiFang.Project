Ext.define('Shell.class.setting.PrintSetting.class.grid',{
	extend: 'Shell.ux.panel.Grid',
	requires: ["Shell.ux.form.field.CheckTrigger"],
	/**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    /**获取列表数据服务*/
  	selectUrl:'/ServiceWCF/DictionaryService.svc/GetSectionPrintList',
 
    multiSelect: true,
    
    pagingtoolbar: 'number',
    selType: 'checkboxmodel',
    /**默认每页数量*/
    defaultPageSize: 50,
    /*afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	me.enableControl();
    },*/
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	
	createGridColumns:function(){
		var me = this;

		var columns = [{
			text:'所属区域',dataIndex:'SPID',width:120,
			sortable:false,menuDisabled:true,
			hidden:true,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var v = value;
				if(me.AreaEnum != null){
					v = me.AreaEnum[v];
				}
				return v;
			}
		},{
			text:'小组号',dataIndex:'SectionNo',minWidth:150,itemId:'SectionNo',
			flex:1,sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			text:'id',dataIndex:'SPID',minWidth:150,itemId:'SPID',
			flex:1,sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			text:'小组名称',dataIndex:'CName',minWidth:150,itemId:'CName',
			flex:1,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'打印格式',dataIndex:'PrintFormat',minWidth:150,
			flex:1,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'模板名称',dataIndex:'PrintProgram',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'默认打印机',dataIndex:'DefPrinter',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'特殊项目号',dataIndex:'TestItemNo',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目最小数量',dataIndex:'ItemCountMin',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目最大数量',dataIndex:'ItemCountMax',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'就诊类型',dataIndex:'sicktypeno',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'打印排序',dataIndex:'PrintOrder',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'微生物属性',dataIndex:'microattribute',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'是否外送单',dataIndex:'IsRFGraphdataPDf',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true,
			renderer:function(value,meta,recore){
				if(value == true){
					return "是";
				}else{
					return "否";
					
				}
			}
			
		}];
		
		return columns;
	},
	createDockedItems: function () {
        var me = this;
        var tooblar = Ext.create('Ext.toolbar.Toolbar', {
            width: 100,
            itemId:'cdi',
            items: [
            { type: 'search', xtype: 'textfield', mark: 'in',itemId:"SECTIONNO", name: 'SECTIONNO', width: 130,hidden:true },
            {
               
                xtype: 'uxCheckTrigger',
                emptyText: '小组过滤',
                width: 200,
                labelSeparator: '',
                labelWidth: 55,
                labelAlign: 'right',
                itemId: 'secname',
                className: 'Shell.class.setting.PrintSetting.class.section',
                listeners: {
                    check: function (p, record) {
                      
                    if (record == null) {
                    me.getComponent("cdi").getComponent("SECTIONNO").setValue("");
                    me.getComponent("cdi").getComponent("secname").setValue("");
                    return;
	                }
	               	me.getComponent("cdi").getComponent("SECTIONNO").setValue(record.get("SectionNo"));
	                me.getComponent("cdi").getComponent("secname").setValue(record.get("CName"));
	                me.selectUrl = me.selectUrl.split('?')[0];
	                me.selectUrl+='?SectionNo='+record.get("SectionNo");
	               	me.load();
	                p.close();
                    }
                }
            },
            {
            	//添加功能
                xtype: 'button', text: '添加',
                iconCls: 'button-add',
                listeners: {
                    click: function () {
                        Shell.util.Win.open("Shell.class.setting.PrintSetting.class.form", {
                        	title:"添加",
                        	width:400,
                        	formType:'add',
                        	height:400,
                        	getStore:me.store.data,
                            appType:me.appType,
                            listeners: {
                                save: function (m, str) {
                                    if (str.success) {
                                    	Ext.MessageBox.alert("成功提示","添加成功");
                                        m.close();
                                        me.load();
                                    } else {
                                        Shell.util.Msg.showError("添加失败");
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
                    		Shell.util.Win.open("Shell.class.setting.PrintSetting.class.form", {
	                        	title:"修改",
	                        	width:400,
	                        	formType:'update',
	                        	height:400,
	                        	record:records,//选中项
	                        	getStore:me.store.data,//所有数据
	                            appType:me.appType,
	                            listeners: {
	                                save: function (m, str) {
	                                    if (str.success) {
	                                    	Ext.MessageBox.alert("成功提示","修改成功");
	                                        m.close();
	                                        me.load();
	                                    } else {
	                                        Shell.util.Msg.showError("修改失败");
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
            delLength.push(records[i].data.SPID);
            
        }
        if(delLength.length>0){//是否选择数据
        	var me = this;
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				method: 'POST',
				async: false,
			    url: Shell.util.Path.rootPath +'/ServiceWCF/DictionaryService.svc/DeleteSectionPrint',
			    params:Ext.JSON.encode({SPID:delLength}),
				success: function(response){
				    str = Ext.JSON.decode(response.responseText);
				},
				error : function(response){
					str = Ext.JSON.decode(response.responseText);
				}
			});
	        return str;
        }else{
        	str = {ErrorInfo:"未选择数据"};
        	return str;
        }
    }
    
});