/**
 * 查询条件面板
 * @author Jcall
 * @version 2018-09-03
 * 代码新包迁移
 * @author Jing
 * @version 2018-09-20
 * @author Guohx
 * @version 2020-01-08
 */
Ext.define('Shell.class.historyAndBackups.backupsBasic.QueryPanel',{
	extend:'Shell.ux.search.SearchToolbar',
	requires: ["Shell.ux.form.field.CheckTrigger"],
    /**报告时间字段*/
	DateField: 'CHECKDATE',
	help: true,
    appType:'',
    /*是否区分大小写*/
    isCaseSensitive:false,
	/**帮助按钮处理*/
	onHelpClick:function(){
		var url = Shell.util.Path.uiPath + "/app/help/index.html";
		Shell.util.Win.openUrl(url,{
			title:'使用说明'
		});
	},
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);		
		var field = me.getFieldsByName(me.DateField);
		if (field) {

		    if (!field.getValue()) {
		        var date = new Date();
		        field.setValue({ start: date, end: date });
		    }
		}

	},
	getSelectSetting:function () {
	    var me = this;
	    var columns = [];

	    Ext.Ajax.defaultPOSTHander = "application/json";
	    Ext.Ajax.request({
	        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetSelectTemplateByAppType?AppType=' + encodeURI(me.appType),
	        async: false,
	        method: 'get',
	        success: function (response, options) {
	            var reponse = Ext.JSON.decode(response.responseText);
	            if (reponse.success) {
	                columns = Ext.JSON.decode(reponse.ResultDataValue); 
	                
	            }
	        }
	    });
	    return columns;
	},
    //获得查询组件
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
	initComponent: function () {
	    var me = this;
	    
	    me.toolButtons = [{//XP_IE8安装程序
//	        type: 'gear', tooltip: '<b>XP-IE8安装程序下载</b>',
//	        handler: function () {
//				var XP_IE8_Url = Shell.util.Path.rootPath + '/web_src/adobe/Adobe Reader XI_11.0.0.379.exe';
//	            window.open(XP_IE8_Url);
//	        }
//	    }, { //Adobe_XI安装程序
	        type: 'gear', tooltip: '<b>Adobe-XI安装程序下载</b>',
	        handler: function () {
				var Adobe_XI_Url = Shell.util.Path.rootPath + '/web_src/adobe/Adobe Reader XI_11.0.0.379.exe';
	            window.open(Adobe_XI_Url);
	        }
	    }];
	    var selectItems = [];
	    var list = me.getSelectSetting();
	    var count = 0;
	    var arryItem = [];
	    var seniorItem = [];
	    for (var i = 0; i < list.length; i++) {
	        var items = list[i].JsCode.split("searchAndNext");
	        var searchType = list[i].SearchType;
	        for (var index in items) {
	            var assembly = Ext.JSON.decode(items[index]);
	            if(searchType==2){
	            	seniorItem.push(assembly);
	            }else{
		            if (count == 0) {
		                assembly.margin = '1 1 1 0';
		                assembly.labelAlign = 'left';
		            } else {
	                    assembly.margin = '1 1 1 4';
	                }
		            if (list[i].Width != null && list[i].Width!="") {
		                assembly.width = list[i].Width;
		            }
		            if (list[i].TextWidth != null && list[i].TextWidth != "") {
		                assembly.labelWidth = list[i].TextWidth;
		            }
		            arryItem.push(assembly);
	            }
	        }
	        count++;
	        if (count == 6 || i == 5) {
	            count = 0;
	            selectItems.push(arryItem);
	            arryItem = new Array();
	        }    
	    }
	    selectItems.push(arryItem);
	    

	    me.items = [];
	     
	    for (var i = 0; i < selectItems.length; i++) {
	        if (i == 0) {
	            selectItems[i].push({ type: 'searchbut', tooltip: "查询数据(不包含分组按钮条件)" });
	        }
	        me.items.push(selectItems[i]);
	    }
	
	            
	    /*var ParaValue = me.getSeniorButton();
	    if(ParaValue == 'true'){
	    	me.items.push({
		    	xtype:'button',
		    	text:'高级查询',
		    	iconCls:'button-searcha',
		    	type:'other',
	            handler: function () {
	            	//me.AdvancedQuery(seniorItem);
	            	me.win = Shell.util.Win.open('Shell.class.basic.AdvancedQuery', {  
	            		seniorItem:me.seniorItem
	            	});
        			me.win.show();
	            }
	    	});
	    }*/
	    me.callParent(arguments);
	},
	/*getSeniorButton:function(){
		var me = this;
		var obq ='';
		var selectUrl="/ServiceWCF/DictionaryService.svc/GetSeniorPublicSetting";
    	selectUrl +="?SName='"+encodeURI(me.appType)+"'&ParaNo='isSeniorSetting'";
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + selectUrl,
            async: false,
            method: 'get',
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
                obresponse = JSON.parse(rs.ResultDataValue);
                obq = obresponse[0].ParaValue;   
            }
        });
        return obq;
	},*/

	/**
	 * 适配输入框
	 * @private
	 * @param {} config
	 * @return {}
	 */
	applyTextfield:function(config){
		var me = this;
		return Ext.applyIf(config,{
			xtype:'textfield',
			margin:'1 1 1 4',
			labelAlign:'right',
			enableKeyEvents:true,
			listeners:{
	            keyup:function(field,e){
                	if(e.getKey() == Ext.EventObject.ESC){
                		field.setValue('');
                		me.onSearch();
                	}else if(e.getKey() == Ext.EventObject.ENTER){
                		me.onSearch();
                	}
                }
	        }
		});
	},
	/**分组查询处理*/
	onGroupSearch:function(but){
		var me = this,
			dateField = but.ownerCt.ownerCt.getItem("selectdate"),
			now = new Date(),
			strat = "",
			end = now;
		if(but.vType == 1){//本周
			var days = now.getDay() - 1;
			days = days < 0 ? 6 : days;
			start = Shell.util.Date.getNextDate(now,0 - days)
		}else if(but.vType == 2){//本月
			start = new Date();
			start.setDate(1);
		}
		dateField.setValue({start:start,end:end});
		me.onSearch(but);
	}
	/**高级查询*/
	/*AdvancedQuery:function(seniorItem){
		var me = this;
		me.win = Shell.util.Win.open('Shell.ux.panel.Panel', {   
		    title: '高级查询',
		    id: 'myPanel',
         	name: 'myPanel',
            autoScroll: true,
         	width: 450,
	        height: 300,
	        layout: {
		        type: 'table',
		        columns: 2
		    },
		    defaults: {
		        bodyStyle: 'padding:20px'
		    },

		    items: seniorItem,
		    buttons:[
		    	{
		    		text:'确定',
		    		handler:function(){
		    			var where = " 1=1 ";
		    			
		    			var districtName = me.win.getComponent("districtName");
		    			var SickTypeName = me.win.getComponent("SickTypeName");
		    			var defaultPageSize = me.win.getComponent("defaultPageSize");
                        var selectdate = me.win.getComponent("selectdate");
                        var chargeName = me.win.getComponent("chargeName");
                        var operator = me.win.getComponent("operator");
                        var checker = me.win.getComponent("checker");
                        var zdy7 = me.win.getComponent("zdy7");
		    			if(districtName != undefined){
		    				if(districtName.getValue()){
		    					where +=' and  DistrictName="'+districtName.getValue()+'"';
		    				}
		    			}
		    			if(SickTypeName!= undefined){
		    				if(SickTypeName.getValue()){
		    					where += ' and SickTypeName="'+SickTypeName.getValue()+'"';
		    				}
		    			}
		    			if(defaultPageSize!= undefined && selectdate!= undefined){
		    				if(defaultPageSize.getValue() && selectdate.getValue()){
			    				var startTime = Ext.Date.format(selectdate.getValue().start,'Y-m-d H:i:s');
								var endTime = Ext.Date.format(selectdate.getValue().end,'Y-m-d H:i:s');
								if(startTime && endTime){
									where += " and "+defaultPageSize.getValue()+">='"+startTime+"' and "+defaultPageSize.getValue()+" <'"+endTime+"'";
								}else if(startTime && !endTime){
									where += " and "+defaultPageSize.getValue()+">='"+startTime+"'";
								}else if(!startTime && endTime){
									where += " and "+defaultPageSize.getValue()+"<'"+endTime+"'";
								}
		    				}
						}
		    			if(chargeName!= undefined){
		    				if(chargeName.getValue()){
		    					where += ' and ChargeName="'+chargeName.getValue()+'"';
		    				}
		    			}
		    			if(operator!= undefined){
		    				if(operator.getValue()){
		    					where += ' and Operator="'+operator.getValue()+'"';
		    				}
		    				
		    			}
		    			if(checker!= undefined){
		    				if(checker.getValue()){
		    					where += ' and Checker="'+checker.getValue()+'"';
		    				}
		    			}
		    			if(zdy7!= undefined){
		    				if(zdy7.getValue()){
		    					where += ' and ZDY7 like  "%'+zdy7.getValue()+'%"';
		    				}
		    			}
		    			if(districtName || SickTypeName || selectdate || chargeName || operator || checker || zdy7){
		    				me.fireEvent('AdvancedQuery',where);
		    			}else{
                            Shell.util.Msg.showInfo("请输入查询条件！");
		    			}
		    			
		    		}
		    		
		    	},
		    	{
		    		text:'清除',
		    		handler:function(){
		    			for(j = 0; j < seniorItem.length; j++) {
   							me.win.getComponent(seniorItem[j].itemId).setValue("");
						} 
                        
		    		}
		    	},
		    	{
		    		text:'关闭',
		    		handler:function(){
                        me.win.close();
		    		}
		    	}
		    ]
        });
        me.win.show();
    }*/
});