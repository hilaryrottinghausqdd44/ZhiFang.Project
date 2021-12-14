/**
 * 打印查询
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint1.class.PrintSearch',{
	extend:'Shell.ux.search.SearchToolbar',
	requires: ["Shell.ux.form.field.CheckTrigger"],
    /**报告时间字段*/
	DateField: 'RECEIVEDATE',
	help: true,
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
		if(!field.getValue()){
			var date = new Date();
			field.setValue({ start: date, end: date });
		}
		field.width =field.width-50;
		field.items.items[0].width = 90;

		me.items.items[1].getComponent("cbxitem").on({
		    change: function (m, v) {
		        var date = field.getValue();
		        var newstart = Shell.util.Date.getNextDate(date.end, v);
		        field.setValue({ start: newstart, end: date.end });
		    }
		});

		me.items.items[3].getComponent("cbxhao").on({
		    change: function (m, v) {
		        me.items.items[3].getComponent("hao").name = v;
		    }
            //,
		    //expand:function(combo, store,index){ 
		    //    Ext.getCmp("factory_code").getStore().load();
		    //} 
		});

		me.items.items[3].getComponent("checkname").on({
		    change: function (m, v) {
		        if (v) {
		            me.getFieldsByName("CNAME").mark = "like";
		        } else {
		            me.getFieldsByName("CNAME").mark = "=";
		        }
		    }
		});

		me.items.items[3].getComponent("checkdate").on({
		    change: function (m, v) {
		        
		        if (v) {
		            me.items.items[1].getComponent(me.DateField).name = "CheckDate";
		        } else {
		            me.items.items[1].getComponent(me.DateField).name = me.DateField;
		        }
		    }
		});
		
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

	    var seachstore = new Ext.data.SimpleStore({
	        autoLoad: true,
	        fields:['text','value'],
	        data: [["卡号", "ZDY3"], ["样本号", "SAMPLENO"], ["条码号", "SerialNo"], ["病历号", "PATNO"]]
	    });

	    me.items = [[
            { type: 'search', xtype: 'textfield', mark: 'in', itemId: "DeptNo", name: 'DeptNo', width: 130,hidden:true },
            { type: 'search', xtype: 'textfield', mark: 'in',itemId:"SECTIONNO", name: 'SECTIONNO', width: 130,hidden:true },
            { text: "本周", tooltip: "查询本周数据", vType:1,where: "datediff(week,RECEIVEDATE,getdate())=0" },
		    { text: "本月", tooltip: "查询本月数据", vType:2,where: "datediff(month,RECEIVEDATE,getdate())=0" },
		    {
		        type: 'other', xtype: 'combo', itemId: 'cbxitem',
		        store: new Ext.data.SimpleStore({
		            fields:['text','value'],
		            data: [["今天", "0"], ["3天", "-3"], ["7天", "-7"], ["15天", "-15"], ["1个月", "-30"], ["3个月", "-90"], ["6个月", "-180"], ["1年", "-365"]]
		        }), fieldLabel: '时间', labelWidth: 35, width: 110, value: ["今天", "0"], displayField: 'text',
		        valueField: 'value'
		    },
		    {
		        type: 'search', xtype: 'uxdatearea', itemId: me.DateField, name: me.DateField
		    },
		    { type: 'searchbut', tooltip: "查询数据(不包含分组按钮条件)" },
            {
                type: 'other', xtype: 'button', text: "清空条件", tooltip: "清空查询条件", margin: 2,
                iconCls: 'button-del',
                listeners: {
                    click: function () {
                        me.clearSearch();
                    }
                }
            },
            //{
            //    type: 'other', xtype: 'button',itemId:'seting', text: "参数设置", tooltip: "参数设置", margin: 2,
            //    iconCls: 'button-config',
            //    listeners: {
            //        click: function () {
            //            Shell.util.Win.open("Shell.ReportPrint1.class.setting", {
            //                seachstore:seachstore,
            //                listeners: {
            //                    setting: function (m,store) {
            //                        seachstore = store;
            //                        //me.items.items[2].getComponent("cbxhao").store = array;
                                   
            //                        me.items.items[2].getComponent("cbxhao").store.each(function (record) {
            //                            console.log(record.get("text"));
            //                        });
            //                        m.close();
            //                    }
            //                }
            //            });
            //        }
            //    }
            //},
	    ], [{
	        type: 'other',
	        fieldLabel: '',
	        xtype: 'uxCheckTrigger',
	        emptyText: '科室过滤',
	        width: 150,
	        labelSeparator: '',
	        labelWidth: 55,
	        labelAlign: 'right',
	        itemId: 'ClienteleName',
	        className: 'Shell.ReportPrint1.class.dept',
	        listeners: {
	            check: function (p, record) {
	                var dno = "";
	                if (record == null) {
	                    me.items.items[1].getComponent("DeptNo").setValue("");
	                    me.items.items[2].getComponent("ClienteleName").setValue("");
	                    return;
	                }
	                me.items.items[1].getComponent("DeptNo").setValue("(" + record.get("DeptNo") + ")");
	                me.items.items[2].getComponent("ClienteleName").setValue(record.get("CName"));
	                p.close();
	            }
	        }
	    },
            {
                type: 'other',
                fieldLabel: '',
                xtype: 'uxCheckTrigger',
                emptyText: '小组过滤',
                width: 200,
                labelSeparator: '',
                labelWidth: 55,
                labelAlign: 'right',
                itemId: 'secname',
                className: 'Shell.ReportPrint1.class.section',
                listeners: {
                    check: function (p, record) {
                        if (record == null) {
                            me.items.items[1].getComponent("SECTIONNO").setValue("");
                            me.items.items[2].getComponent("secname").setValue("");
                            me.items.items[2].getComponent("secname").setClassConfig({ sRecd: "" });
                            return;
                        }
                        var dno = "";
                        var name = "";
                        for (var i = 0; i < record.length; i++) {
                            dno += record[i].get("SectionNo") + ",";
                            name += record[i].get("CName") + ",";
                        }
                        dno = dno.substring(0, dno.length - 1);
                        name = name.substring(0, name.length - 1);
                        me.items.items[1].getComponent("SECTIONNO").setValue("(" + dno + ")");
                        me.items.items[2].getComponent("secname").setValue(name);
                        me.items.items[2].getComponent("secname").setClassConfig({ sRecd: record });
                        p.close();
                    }
                }
            }
	    ],[
		    { type: 'search', xtype: 'textfield', mark: '=', name: 'CNAME', fieldLabel: '姓名', labelWidth: 35, width: 110 },
		    {
		        type: 'other', xtype: 'combo',
		        store: seachstore, itemId: "cbxhao", mark: '=', width: 60, value: ["ZDY3"], displayField: 'text',
		        valueField: 'value'
		    },
            { type: 'search', xtype: 'textfield', mark: '=', itemId: "hao", name: 'ZDY3', width: 130 },
            { type: 'other', xtype: 'checkbox', itemId: 'checkname', boxLabel: '名字模糊查询', width: 100 },
            { type: 'other', xtype: 'checkbox', itemId: 'seachsavewhere', boxLabel: '查询后保留条件', width: 110, checked: true },
            { type: 'other', xtype: 'checkbox', itemId: 'checkdate', boxLabel: '审核时间', width: 90 }
	    ]];

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
			dateField = but.ownerCt.getComponent(me.DateField),
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
		if (!me.items.items[3].getComponent("seachsavewhere").getValue()) {
		    me.clearSearch();
		}
	},

	clearSearch: function () {
	    var me = this;
	    var field = me.getFieldsByName(me.DateField);
	    me.getFieldsByName("DeptNo").setValue("");
	    me.items.items[1].getComponent("cbxitem").setValue(["0"]);
	    field.setValue({ start: new Date(), end: new Date() });
	    me.getFieldsByName("CNAME").setValue("");
	    me.items.items[3].getComponent("cbxhao").setValue(["ZDY3"]);
	    me.items.items[3].getComponent("hao").name = "ZDY3";
	    me.items.items[3].getComponent("hao").setValue("");
	    me.getFieldsByName("SECTIONNO").setValue("");
	    me.items.items[2].getComponent("ClienteleName").setValue("");
	    me.items.items[2].getComponent("secname").setValue("");
	}
});