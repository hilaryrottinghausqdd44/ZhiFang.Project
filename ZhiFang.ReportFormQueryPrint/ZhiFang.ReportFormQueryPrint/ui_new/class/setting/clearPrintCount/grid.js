Ext.define('Shell.class.setting.clearPrintCount.grid',{
	extend: 'Shell.ux.panel.Grid',
	requires: ["Shell.ux.form.field.CheckTrigger","Shell.ux.form.field.DateArea"],
	/**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    /**获取列表数据服务*/
  	selectUrl:'/ServiceWCF/ReportFormService.svc/SelectReport?fields=PatNo,ClientPrint,PRINTTIMES,CNAME,SampleNo,CHECKDATE,CHECKTIME,RECEIVEDATE,SectionType,FormNo,ReportFormID,Serialno',
 	deleteCUrl:Shell.util.Path.rootPath +'/ServiceWCF/ReportFormService.svc/deleteClientPrint',
 	deletePUrl:Shell.util.Path.rootPath +'/ServiceWCF/ReportFormService.svc/deletePrintTimes',
    multiSelect: true,
    /**数据集属性*/
	storeConfig:{},
    pagingtoolbar: 'number',
    selType: 'checkboxmodel',
    /**默认每页数量*/
    defaultPageSize: 50,
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	me.enableControl();
    },
	initComponent: function() {
		var me = this;
		var date = new Date();
		currentTime=Shell.util.Date.toString(date).split(' ')[0];
		me.selectUrl += "&where=(RECEIVEDATE="+currentTime+")";
		//数据列
		me.columns = me.createGridColumns();
		me.dockedItems = me.createDockedItems();
		
	    //创建数据集属性
		me.createGridStore();
		me.callParent(arguments);
	},
	
	createGridColumns:function(){
		var me = this;

		var columns = [{
			text:'ID',dataIndex:'ReportFormID',width:120,
			sortable:false,menuDisabled:true,
			hidden:true
			
		},{
			text:'ID',dataIndex:'FormNo',width:120,
			sortable:false,menuDisabled:true,
			hidden:true
			
		},{
			text:'姓名',dataIndex:'CNAME',minWidth:130,itemId:'CNAME',
			flex:1,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'病历号',dataIndex:'PatNo',minWidth:130,itemId:'PatNo',
			flex:1,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'条码号',dataIndex:'Serialno',minWidth:130,itemId:'Serialno',
			flex:1,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'临床打印次数',dataIndex:'PRINTTIMES',width:130,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'自助打印次数',dataIndex:'clientprint',width:130,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}, {
	            xtype: 'actioncolumn', align: 'center', text: '清除自助打印次数', width: 100,
	            items: [
	                {
	                    tooltip: '删除',
	                    iconCls: 'button-del',
	                    handler: function (view, row, col, item, e) {
	                        var record = view.getStore().getAt(row);
	                        Shell.util.Msg.confirmDel(function (but) {
	                            if (but != "ok") return;
	                            var data ={formno: [record.data.FormNo]};
								Ext.Ajax.defaultPostHeader = 'application/json';
							    Ext.Ajax.request({
							        url: me.deleteCUrl,
							        async: false,
							        method: 'post',
							        params:Ext.JSON.encode(data),
							        success: function (response, options) {
							            rs = Ext.JSON.decode(response.responseText); 
							            if(rs.success){
							            	me.load();
							            }else{
							            	Shell.util.Msg.showInfo("删除失败!");
							            }
							        }
				            	});
	                        });
	                    }
	                }
	            ]
        	}, {
	            xtype: 'actioncolumn', align: 'center', text: '清除临床打印次数', width: 100,
	            items: [
	                {
	                    tooltip: '删除',
	                    iconCls: 'button-del',
	                    handler: function (view, row, col, item, e) {
	                        var record = view.getStore().getAt(row);
	                        Shell.util.Msg.confirmDel(function (but) {
	                            if (but != "ok") return;
	                            var data ={formno: [record.data.FormNo]};
								Ext.Ajax.defaultPostHeader = 'application/json';
							    Ext.Ajax.request({
							        url: me.deletePUrl,
							        async: false,
							        method: 'post',
							        params:Ext.JSON.encode(data),
							        success: function (response, options) {
							            rs = Ext.JSON.decode(response.responseText); 
							            if(rs.success){
							            	me.load();
							            }else{
							            	Shell.util.Msg.showInfo("删除失败!");
							            }
							        }
				            	});
	                        });
	                    }
	                }
	            ]
        	}
		];
		
		return columns;
	},	
	createDockedItems: function () {
        var me = this;
        var tooblar = Ext.create('Ext.toolbar.Toolbar', {
            width: 100,
            itemId:'cdi',
            items: [
            { fieldLabel:'姓名',labelWidth: 35, xtype: 'textfield', itemId:"CName", name: 'CName', width: 130 },
            { type: 'search', xtype: 'textfield', mark: 'in',itemId:"SECTIONNO", name: 'SECTIONNO', width: 130,hidden:true },
            {
                xtype: 'uxCheckTrigger',
                emptyText: '小组过滤',
                width: 200,
                labelSeparator: '',
                labelWidth: 55,
                labelAlign: 'right',
                itemId: 'secname',
                className: 'Shell.class.setting.clearPrintCount.section',
                listeners: {
                    check: function (p, record) {
                      
                    if (record == null) {
                    me.getComponent("cdi").getComponent("SECTIONNO").setValue("");
                    me.getComponent("cdi").getComponent("secname").setValue("");
                    return;
	                }
	               	me.getComponent("cdi").getComponent("SECTIONNO").setValue(record.get("SectionNo"));
	                me.getComponent("cdi").getComponent("secname").setValue(record.get("CName"));
	                p.close();
                    }
                }
            },{
				xtype: 'combobox',
                name: 'defaultPageSize',
                itemId: 'defaultPageSize',
                width: 130,
                displayField: 'text', 
				valueField: 'value',
				store:Ext.create('Ext.data.Store', {
	                    fields: ['text', 'value'],
	                    data: [
                            { text: '审核（报告）时间', value: 'CHECKDATE' },
                            { text: '核收日期', value: 'RECEIVEDATE' },
                            { text: '采样日期', value: 'COLLECTDATE' },
                            { text: '签收日期', value: 'INCEPTDATE' },
                            { text: '检测（上机）日期', value: 'TESTDATE' },
                            { text: '录入（操作）日期', value: 'OPERDATE' }
	                    ]
	                }),
                value: ['CHECKDATE'],
                listeners: {
                    change: function (m, v) {
                    	var selectDate=me.getComponent("cdi").getComponent("selectdate");
                        selectDate.name = v;
                        this.ownerCt.ownerCt.DateField = v;
                    }
                }
            },
		    { type: 'search', xtype: 'uxdatearea', itemId: 'selectdate', name: 'CHECKDATE', labelWidth: 0, width: 190 },
		    { type: 'search', xtype: 'textfield',itemId:'SERIALNO',  name: 'SERIALNO', fieldLabel: '条码号', labelWidth: 50, width: 150 },
		    { type: 'search', xtype: 'textfield', itemId:'PATNO', name: 'PATNO', fieldLabel: '病历号', labelWidth: 50, width: 150 },
		    {
            	//添加功能
                xtype: 'button', text: '查询',
                iconCls: 'button-search',
                listeners: {
                    click: function () {
                    	var cdi= me.getComponent("cdi");
                        var CName=cdi.getComponent("CName").value;
                        var SECTIONNO=cdi.getComponent("SECTIONNO").value;
                        var SERIALNO=cdi.getComponent("SERIALNO").value;
                        var PATNO=cdi.getComponent("PATNO").value;
                        
                        var selectdate=cdi.getComponent("selectdate").getValue();
                        var selectdatename=cdi.getComponent("selectdate").name;
                        me.selectUrl=me.selectUrl.split("&")[0];
                        var where = "&where=( ";
                        if(selectdate){
                        	
                        	var start = Shell.util.Date.toString(selectdate.start).split(' ')[0];
                            var end = Shell.util.Date.toString(selectdate.end).split(' ')[0];
                        	where += selectdatename+" >= '"+start+"' and "+selectdatename +" <= '" + end +"' ";
                        	if(CName){
                        		where += " and CName like '%"+CName+"%' ";
                        	}
                        	if(SECTIONNO){
                        		where += " and SECTIONNO = '"+SECTIONNO+"' ";
                        	}
                        	if(SERIALNO){
                        		where += " and SERIALNO = '"+SERIALNO+"' ";
                        	}
                        	if(PATNO){
                        		where += " and PATNO = '"+PATNO+"' ";
                        	}
                        	where += " )";
                        	me.selectUrl += where;
                        	me.onSearch();
                        }else{
                        	Shell.util.Msg.showInfo("请选择时间，时间为必选项!");
                        }
                    }
                }
            }]
        });
        //分页栏
        var pagingtoolbar = me.createPagingToolbar();
        return [pagingtoolbar,tooblar];
   },
   createGridStore: function () {
	    var me = this;
	    //数据集属性
	    me.storeConfig = me.storeConfig || {};
	    me.storeConfig.proxy = me.storeConfig.proxy || {
	        type: 'ajax',
	        url: '',
	        reader: { type: 'json', totalProperty: 'total', root: 'rows' },
	        extractResponseData: function (response) {
	            var result = Ext.JSON.decode(response.responseText);
	            if (result.success) {
	                var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
	                result.total = ResultDataValue.total;
	                result.rows = ResultDataValue.rows;
	            } else {
	                result.total = 0;
	                result.rows = [];
	                Shell.util.Msg.showError(result.ErrorInfo);
	            }
	            response.responseText = Ext.JSON.encode(result);
	            return response;
	        }
	    };
	}
	
});