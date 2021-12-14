/**
 * 打印查询
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.TestReportPrint.class.PrintSearch', {
	extend:'Shell.ux.search.SearchToolbar',
	requires: ["Shell.ux.form.field.CheckTrigger"],
    /**报告时间字段*/
	DateField: 'CHECKDATE',
	help: true,
    appType:'',
	/**帮助按钮处理*/
	onHelpClick:function(){
		var url = Shell.util.Path.uiPath + "/ReportPrint/help.html";
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
	        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetSelectTemplateByAppType?AppType=' + me.appType,
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
	        type: 'gear', tooltip: '<b>XP-IE8安装程序下载</b>',
	        handler: function () {
	            var XP_IE8_Url = 'https://www.baidu.com/link?url=Qg0alUX13qHTpy1T5muv0Kbp7ph5LZLkJvsyEHxBw5-' +
	                'QqnkwXtyG6Nz5ea6hy9a309F7QU3cb50F3baihdHsU96hcC0ksNHZRrbSeHxWZ9y&wd=ie8%20xp%2032%E4%' +
                    'BD%8D%20%E5%AE%8C%E6%95%B4&issp=1&f=3&ie=utf-8&tn=baiduhome_pg&oq=ie8%20xp&inputT=6896&rsp=0';
	            window.open(XP_IE8_Url);
	        }
	    }, { //Adobe_XI安装程序
	        type: 'gear', tooltip: '<b>Adobe-XI安装程序下载</b>',
	        handler: function () {
	            var Adobe_XI_Url = 'https://www.baidu.com/link?url=T-sjPv6zRnHJ_yfkhga5tRo7kerlwQ5WINhLwjozIzEWid32n_' +
                    'kRCglNUceyfU8WKpS_F91kLE2i7aeJoPdkuUhPtBhUmXPEGF1DA46qW83&wd=Adobe%20Reader&issp=1&f=8&ie=utf-8' +
                    '&tn=baiduhome_pg&oq=ie8%20xp&inputT=608';
	            window.open(Adobe_XI_Url);
	        }
	    }];
	    var selectItems = [];
	    var list = me.getSelectSetting();
	    var count = 0;
	    var arryItem = [];
	    for (var i = 0; i < list.length; i++) {
	        var items = list[i].JsCode.split("searchAndNext");
	        for (var index in items) {

	            console.log(items[index]);
	            var assembly = Ext.JSON.decode(items[index]);
	            if (list[i].Width != null && list[i].Width!="") {
	                assembly.width = list[i].Width;
	            }
	            if (list[i].TextWidth != null && list[i].TextWidth != "") {
	                assembly.labelWidth = list[i].TextWidth;
	            }
	            arryItem.push(assembly);
	        }
	        count++;
	        if (count == 6 || i == 5) {
	            count = 0;
	            selectItems.push(arryItem);
	            arryItem = new Array();
	        }    
	    }
	    selectItems.push(arryItem);
	    

	    me.items = [
//            [
		    //{text:"本日",tooltip:"查询本日数据",where:"datediff(day,RECEIVEDATE,getdate())=0"},
		    //{text:"三日内",tooltip:"查询三日内数据",where:"datediff(day,RECEIVEDATE,getDate())<=3"},
		    //{text:"本周",tooltip:"查询本周数据",where:"datediff(day,RECEIVEDATE,getdate())<=datepart(dw,getdate())"},
		    //{ text: "本周", tooltip: "查询本周数据", vType:1,where: "datediff(week,RECEIVEDATE,getdate())=0" },
		  //  { text: "本月", tooltip: "查询本月数据", vType:2,where: "datediff(month,RECEIVEDATE,getdate())=0" },
		    //{text:"本年",tooltip:"查询本年数据",where:"datediff(year,RECEIVEDATE,getdate())=0"},
            //{
            //    type: 'other', xtype: 'combobox',
            //    style: me.itemStyle,
            //    name: 'defaultPageSize',
            //    itemId: 'defaultPageSize',
            //    width: 130,
            //    displayField: 'text', valueField: 'value',
            //    store: Ext.create('Ext.data.Store', {
            //        fields: ['text', 'value'],
            //        data: [
            //            { text: '审核（报告）时间', value: 'CHECKDATE' },
            //            { text: '核收日期', value: 'RECEIVEDATE' },
            //            { text: '采样日期', value: 'COLLECTDATE' },
            //            { text: '签收日期', value: 'INCEPTDATE' },
            //            { text: '检测（上机）日期', value: 'TESTDATE' },
            //            { text: '录入（操作）日期', value: 'OPERDATE' }
            //        ]
            //    }),
            //    value: [me.DateField],
            //    listeners: {
            //        change: function (m, v) {
            //            var selectDate = me.items.items[1].getComponent('selectdate');
            //            selectDate.name = v;
            //            me.DateField = v;
            //        }
            //    }
            //},
		  //  { type: 'search', xtype: 'uxdatearea', itemId: 'selectdate', name: me.DateField, labelWidth: 0, width: 190 },
             //{ type: 'search', xtype: 'textfield', mark: 'in', itemId: "DeptNo", name: 'DeptNo', width: 130, hidden: true },
            //{
            //    fieldLabel: '',
            //    xtype: 'uxCheckTrigger',
            //    emptyText: '科室过滤',
            //    zIndex:1,
            //    width: 150,
            //    labelSeparator: '',
            //    labelWidth: 55,
            //    labelAlign: 'right',
            //    itemId: 'ClienteleName',
            //    className: 'Shell.TestReportPrint.class.dept.CheckGrid',
            //    listeners: {
            //        check: function (p, record) {
            //            var item = "";
            //            var clientName = "";
            //            if (record == null) {
            //                item = me.getItem("DeptNo");
            //                clientName = me.getItem("ClienteleName");
            //                item.setValue("");
            //                clientName.setValue("");
            //                return;
            //            }
            //            item = me.getItem("DeptNo");
            //            clientName = me.getItem("ClienteleName");
            //            item.setValue("(" + record.get("DeptNo") + ")");
            //            clientName.setValue(record.get("CName"));
            //            p.close();
            //        }
            //    }
            //},
            //{
            //    type: 'other', xtype: 'checkbox', itemId: 'checkSickType', boxLabel: '门诊', width: 50,
            //    listeners:{
            //        change: function (m, v) {
            //            var sick = me.getItem("mgSickType");
            //            if (v) {
            //                if (sick.getValue() == "" || sick.getValue == null) {
            //                    sick.setValue("('门诊')");
            //                } else {
            //                    sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) + ",'门诊')");
            //                }
            //                sick.type = 'search';
            //            } else {
            //                var index = sick.getValue().indexOf("门诊");
            //                if (index == 2) {
            //                    sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6));
            //                } else {
            //                    sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3));
            //                }
            //            }
            //            if (sick.getValue() == "" || sick.getValue() == null || sick.getValue().length < 6) {
            //                sick.type = 'other';
            //                sick.setValue("");
            //            }
            //        }
	        //    }
            //},
            //{ type: 'search', xtype: 'textfield', itemId: 'mgSickType', mark: 'in', name: 'SickTypeName', hidden: true },
            //{
            //    type: 'other', xtype: 'checkbox', itemId: 'checkSickType1', boxLabel: '住院', width: 50,
            //    listeners: {
            //        change: function (m, v) {
            //            var sick = me.getItem("mgSickType");
            //            if (v) {
            //                if (sick.getValue() == "" || sick.getValue == null) {
            //                    sick.setValue("('住院')");
            //                } else {
            //                    sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) + ",'住院')");
            //                }
            //                sick.type = 'search';
            //            } else {
            //                var index = sick.getValue().indexOf("住院");
            //                if (index == 2) {
            //                    sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6));
            //                } else {
            //                    sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3));
            //                }
  //            }
            //            if (sick.getValue() == "" || sick.getValue() == null || sick.getValue().length < 6) {
            //                sick.type = 'other';
            //            }
            //        }
            //    }
            //},
            //{
            //    type: 'other', xtype: 'checkbox', itemId: 'checkSickType2', boxLabel: '体检', width: 50,
            //    listeners: {
            //        change: function (m, v) {
            //            var sick = me.getItem("mgSickType");
            //            if (v) {
            //                if (sick.getValue() == "" || sick.getValue == null) {
            //                    sick.setValue("('体检')");
            //                } else {
            //                    sick.setValue(sick.getValue().substring(0, sick.getValue().length - 1) + ",'体检')");
            //                }
            //                sick.type = 'search';
            //            } else {
            //                var index = sick.getValue().indexOf("体检");
            //                if (index == 2) {
            //                    sick.setValue(sick.getValue().substring(0, 1) + sick.getValue().substring(6));
            //                } else {
            //                    sick.setValue(sick.getValue().substring(0, index - 2) + sick.getValue().substring(index + 3));
            //                }
            //            }
            //            if (sick.getValue() == "" || sick.getValue() == null || sick.getValue().length < 6) {
            //                sick.type = 'other';
            //            }
            //        }
            //    }
            //},     
	    //]
        //, [
		    //{ type: 'search', xtype: 'textfield', mark: '=', name: 'CNAME', fieldLabel: '姓名', labelWidth: 35, width: 110 },
		    //{ type: 'search', xtype: 'textfield', mark: '=', name: 'SAMPLENO', fieldLabel: '样本号', labelWidth: 50, width: 150 },
		    //{ type: 'search', xtype: 'textfield', mark: '=', name: 'PATNO', fieldLabel: '病历号', labelWidth: 50, width: 150 },
		    //{ type: 'search', xtype: 'textfield', mark: '=', name: 'ZDY3', fieldLabel: '卡号', labelWidth: 30, width: 130 },
            //{ type: 'search', xtype: 'textfield', mark: '=', name: 'Bed', fieldLabel: '床号', labelWidth: 30, width: 130 },
		    //{ type: 'search', xtype: 'textfield', mark: '=', name: 'SECTIONNO', fieldLabel: '小组号', labelWidth: 50, width: 130 }
        //]
	    ];
	    
	    for (var i = 0; i < selectItems.length; i++) {
	        if (i ==0) {
	            selectItems[i].push({ type: 'searchbut', tooltip: "查询数据(不包含分组按钮条件)" });
	        }
	        me.items.push(selectItems[i]);
	    }
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