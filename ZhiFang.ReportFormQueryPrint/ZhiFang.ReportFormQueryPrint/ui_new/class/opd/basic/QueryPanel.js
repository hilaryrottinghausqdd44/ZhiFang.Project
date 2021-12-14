/**
 * 查询条件面板
 * @author Jcall
 * @version 2018-09-03
 * 代码新包迁移
 * @author Jing
 * @version 2018-09-20
 */
Ext.define('Shell.class.opd.basic.QueryPanel',{
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
				//把shouName赋值给对应的显示位置
				if (assembly.fieldLabel && assembly.fieldLabel != '') {
					assembly.fieldLabel = list[i].ShowName;
				}
				if (assembly.emptyText && assembly.emptyText != '') {
					assembly.emptyText = list[i].ShowName;
				}
				if (assembly.text && assembly.text != '') {
					assembly.text = list[i].ShowName;
				}
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
	    
	    me.items.push({
	    	xtype:'button',
	    	text:'高级查询',
	    	iconCls:'button-searcha',
	    	type:'other',
	        handler: function () {
	        	me.win = Shell.util.Win.open('Shell.class.opd.basic.AdvancedQuery', {  
	        		seniorItem:me.seniorItem,
	        		listeners:{
	        			save:function(where){
	        				me.fireEvent('load',where);
	        				me.win.close();
	        			}
	        		}
	        		
	        	});
				me.win.show();
	        }
		});
	    me.callParent(arguments);
	},
	

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
	
});