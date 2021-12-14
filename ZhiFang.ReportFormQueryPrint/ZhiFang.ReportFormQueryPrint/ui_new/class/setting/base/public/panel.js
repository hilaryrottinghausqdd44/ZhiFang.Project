Ext.define("Shell.class.setting.base.public.panel", {
    extend: 'Shell.ux.panel.AppPanel',
    itemStyle: "margin-top:10px;margin-left:30px",
    AddUrl: "/ServiceWCF/DictionaryService.svc/UpdatePublicSetting",
    selectURL: '/ServiceWCF/DictionaryService.svc/GetAllPublicSetting',
    setDefualtUrl: '/ServiceWCF/DictionaryService.svc/SetPublicDefaultSetting',
    appType: "",
    data:"",
    layout: {
        type: 'table',
        columns: 3,
        tableAttrs: {
            cellpadding: 1,
            cellspacing: 1,
            width: '100%',
            style: 'margin-top:40px',
            align: 'right'
            
        }
    },
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    afterRender:function () {
        var me = this;
        me.callParent(arguments);
        me.GetAllSetting();
    },
	GetAllSetting: function () {
        var me = this;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.selectURL + "?pageType="+encodeURI(me.appType),
            async: false,
            method: 'get',
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
                if (rs.success) {
                    var items = Ext.JSON.decode(rs.ResultDataValue).list;
                    me.data = items;
                    for (var i = 0; i < items.length; i++) {
                        var parano = me.getComponent(items[i].ParaNo);
                        
                        if (parano) {

                            parano.setValue(items[i].ParaValue);
                            //if (items[i].ParaNo == "HistoryDefaultCollapsed") {
                            //    parano.getComponent("rb1").checked = false;
                            //    parano.getComponent("rb2").checked = true;

                            //}
                        }
                        
                    }
                }
            }
        });
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
            }
           /* , {
                    xtype: 'button', text: '恢复默认',
                    iconCls: 'button-config',
                    listeners: {
                        click: function () {
                            Shell.util.Msg.showMsg({
                                title: '恢复默认',
                                msg: '恢复默认将会清空现在的设置，使用系统默认设置，确定要恢复吗？',
                                icon: Ext.Msg.WARNING,
                                buttons: Ext.Msg.OKCANCEL,
                                callback: function (v) {
                                    if (v == 'ok') {
                                        me.RestoreDefault();
                                    }
                                }
                            });
                        }
                    }
                }*/
            ]
        });
        return [tooblar];
    },
    getReponseData:function (name) {
        var me = this;
        var col = {};
        for (var i = 0; i < me.data.length; i++) {
            if (me.data[i].ParaNo == name) {
                col = me.data[i];
            }
        }
        return col;
    },

    savePublicSetting:function () {
        var me = this;
        var list = [];
        var rs = null;
        for (var i = 0; i < me.items.keys.length; i++) {
            if (me.items.keys[i] == 'not') continue;
            var hash = {};
            var str = me.getComponent(me.items.keys[i]).getValue();
            //if (str == 'true') {
            //    str = 'true';
            //}
            //if (str == 'false' && str !="") {
            //    str = 'false';
            //}
            if (me.items.keys[i] == 'defaultCheckedPage') {
                str = str.defaultCheckedPage;
            }
            var records = me.getReponseData(me.items.keys[i]);

            hash["ParaValue"] = str;
            //for (var o in obj) {
            //    if (o == 'getValue' && typeof (obj[o]) == 'function') {
            //        hash["value"] = obj[o]();
            //    }
            //}
            hash["ParaNo"] = me.items.keys[i];
            hash["SName"] = me.appType;
            hash["Name"] = "查询打印页面配置";
            hash["ParaType"] = "config";
            hash["ParaDesc"] = records.ParaDesc;
            list.push(hash);
        }
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.AddUrl,
            async: false,
            method: 'POST',
            params: Ext.encode({ "models": list }),
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    },

    initobject:function(){
        //var me = this;
        //var obj = new Object;
        //obj.defaultWhere = me.getComponent("defaultWhere");
        //obj.requestParamsArr = me.getComponent("requestParamsArr");
        //obj.hisRequestParamsArr = me.getComponent("hisRequestParamsArr");
        //obj.defaultDates = me.getComponent("defaultDates");
        //obj.defaultPageSize = me.getComponent("defaultPageSize");
        //obj.hasPrint =me.getComponent("hasPrint");
        //obj.A4Type =me.getComponent("A4Type");
        //obj.printType = me.getComponent("printType");
        //obj.maxPrintTimes = me.getComponent("maxPrintTimes");
        //obj.mergePageCount = me.getComponent("mergePageCount");
        //obj.ForcedPagingField = me.getComponent("ForcedPagingField");
        //obj.openAddPrintTimes = me.getComponent("openAddPrintTimes");
        //obj.checkUnprint = me.getComponent("checkUnprint");
        //obj.checkFilter = me.getComponent("checkFilter");
        //obj.headCollapsed = me.getComponent("headCollapsed");
        //obj.autoSelect = me.getComponent("autoSelect");
        //obj.CheckOnly =me.getComponent("CheckOnly"); 
        //obj.hasReportPage = me.getComponent("hasReportPage");
        //obj.hasResultPage =me.getComponent("hasResultPage");
        //obj.defaultCheckedPage =me.getComponent("defaultCheckedPage");
        //obj.hasPdfPrinter =me.getComponent("hasPdfPrinter");
        //obj.pdfPrinterList = me.getComponent("pdfPrinterList");
        //obj.isListHidden = me.getComponent("isListHidden");
        //return obj;
    },
    createItems: function () {
        var me = this;
        var items = [];
        
        items.push({
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'defaultWhere',
            itemId: 'defaultWhere',
            fieldLabel: '默认查询条件',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: '格式：PatNo="1001" ,值用双引号包含！'
                    })
                }
            }
        },{
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'defaultDates',
            itemId: 'defaultDates',
            fieldLabel: '默认查询天数',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "请填入数值！"
                    })
                }
            }
        }, {
            xtype: 'combobox',
            style: me.itemStyle,
            name: 'defaultPageSize',
            itemId: 'defaultPageSize',
            editable:false,
            displayField: 'text', valueField: 'value',
            store: Ext.create('Ext.data.Store', {
                fields: ['text', 'value'],
                data: [
                    { text: '10', value: '10' },
                    { text: '20', value: '20' },
                    { text: '50', value: '50' },
                    { text: '100', value: '100' },
                    { text: '200', value: '200' },
                    { text: '300', value: '300' },
                    { text: '400', value: '400' },
                    { text: '500', value: '500' }
                ]
            }),
            fieldLabel: '分页显示数量'
        });

        items.push({ colspan: 3, html: '<hr>', border: 0, itemId: 'not' });

        items.push({
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'hasPrint',
            itemId: 'hasPrint',
            boxLabel: '是否开启打印'
        }, {
            xtype: 'combobox',
            fieldLabel: 'A4纸张类型',
            name: 'A4Type',
            itemId: 'A4Type',
            editable:false,
            style: me.itemStyle,
            displayField: 'text', valueField: 'value',
            store: Ext.create('Ext.data.Store', {
                fields: ['text', 'value'],
                data: [
                    { text: 'A4', value: '1' },
                    { text: '16开', value: '2' }
                ]
            })
        },
        {
            xtype: 'combobox',
            style: me.itemStyle,
            name: 'printType',
            itemId: 'printType',
            editable:false,
            displayField: 'text', valueField: 'value',
            store: Ext.create('Ext.data.Store', {
                fields: ['text', 'value'],
                data: [
                    { text: 'A4', value: 'A4' },
                    { text: 'A5', value: 'A5' },
                    { text: '双A5', value: '双A5' }
                ]
            }),
            fieldLabel: '默认打印类型'
        });

        items.push({
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'maxPrintTimes',
            itemId: 'maxPrintTimes',
            fieldLabel: '最大打印次数',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "请填入数值！"
                    })
                }
            }
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'mergePageCount',
            itemId: 'mergePageCount',
            fieldLabel: '双A5合并数量',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "请填入数值！"
                    })
                }
            }
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'ForcedPagingField',
            itemId: 'ForcedPagingField',
            fieldLabel: '强制分页字段',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "字段名，只可填一个，例：PatNo ！"
                    })
                }
            }
        });

        items.push(
         {
             xtype: 'checkbox',
             style: me.itemStyle,
             name: 'openAddPrintTimes',
             itemId: 'openAddPrintTimes',
             boxLabel: '打印次数累加'
         }, {
             xtype: 'checkbox',
             name: 'checkUnprint',
             itemId: 'checkUnprint',
             style: me.itemStyle,
             boxLabel: '默认勾选未打印框'
         }, {
             xtype: 'checkbox',
             name: 'checkFilter',
             itemId: 'checkFilter',
             style: me.itemStyle,
             boxLabel: '默认勾选过滤框'
         });

        items.push({
            xtype: 'checkbox',
            name: 'headCollapsed',
            itemId: 'headCollapsed',
            style: me.itemStyle,
            boxLabel: '默认收起查询框'
        }, {
            xtype: 'checkbox',
            name: 'autoSelect',
            itemId: 'autoSelect',
            style: me.itemStyle,
            boxLabel: '默认勾选第一条数据'
        }, {
            xtype: 'checkbox',
            name: 'CheckOnly',
            itemId: 'CheckOnly',
            style: me.itemStyle,
            boxLabel: '点击复选框才能选中行'
        });

        items.push({
            xtype: 'checkbox',
            name: 'hasReportPage',
            itemId: 'hasReportPage',
            style: me.itemStyle,
            boxLabel: '显示报告页签'
        }, {
            xtype: 'checkbox',
            name: 'hasResultPage',
            itemId: 'hasResultPage',
            style: me.itemStyle,
            boxLabel: '显示结果页签'
        }, {
            xtype: 'uxradiogroup', defaultSelect: 1,
            fieldLabel: '默认勾选的页签',
            style: me.itemStyle,
            name: 'defaultCheckedPage',
            itemId: 'defaultCheckedPage',
            data: [{ text: '报告', value: 1 }, { text: '结果', value: 2 }]
        });
        items.push({
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'printCountSetting',
            itemId: 'printCountSetting',
            fieldLabel: '最大打印份数',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "请填入数值！"
                    })
                }
            }
        },{
            xtype: 'textfield',
            name: 'pdfPrinterList',
            itemId: 'pdfPrinterList',
            fieldLabel: 'PDF打印机数组',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: '打印机名称，可接收多个逗号隔开，不填为默认打印机'
                    })
                }
            }
        });
        items.push({
            xtype: 'checkbox',
            name: 'hasPdfPrinter',
            itemId: 'hasPdfPrinter',
            boxLabel: '是否需要选择打印机 需要降低IE浏览器安全性'
        },{
            xtype: 'checkbox',
            name: 'isListHidden',
            itemId: 'isListHidden',
            boxLabel: '当报告列表数量<=1时隐藏报告列表，直接显示报告内容',
            style:{            
            marginLeft: '30px'
        	}
        },{
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'isCaseSensitive',
            itemId: 'isCaseSensitive',
            boxLabel: '是否区分大小写'
            }, {
                xtype: 'textfield',
                style: me.itemStyle,
                name: 'listWidth',
                itemId: 'listWidth',
                fieldLabel: 'List列表宽度',
                listeners: {
                    render: function (field, p) {
                        Ext.QuickTips.init();
                        Ext.QuickTips.register({
                            target: field.el,
                            text: "请填入数值！"
                        })
                    }
                }
            });
        items.push({
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'isviewportHeader',
            itemId: 'isviewportHeader',
            boxLabel: '是否显示页面外边框'
        }/*,{
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'IsLabSignature',
            itemId: 'IsLabSignature',
            boxLabel: '是否显示电子章'
        }*//*,{
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'IsbTempReport',
            itemId: 'IsbTempReport',
            boxLabel: '是否显示部分审核报告'
        }*/);
        items.push(
        	{
        		xtype:'checkbox',
        		style:me.itemStyle,
        		name:'IsQueryRequest',
        		itemId:'IsQueryRequest',
        		boxLabel:'是否查询Request表'
        	},{
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'MaxDownLoadNum',
            itemId: 'MaxDownLoadNum',
            fieldLabel: '最大下载份数',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "请填入数值！"
                    })
                }
            }
        }
        );
        items.push(
            {
                xtype: 'combobox',
                style: me.itemStyle,
                name: 'HistoryCompareDateField',
                itemId: 'HistoryCompareDateField',
                editable: false,
                labelWidth: 110,
                fieldLabel: '历史对比默认查询时间字段',
                displayField: 'text', valueField: 'value',
                store: Ext.create('Ext.data.Store', {
                    fields: ['text', 'value'],
                    data: [
                        { text: '审核（报告）时间', value: 'CHECKDATE' },
                        { text: '核收日期', value: 'RECEIVEDATE' },
                        { text: '采样日期', value: 'COLLECTDATE' },
                        { text: '签收日期', value: 'INCEPTDATE' },
                        { text: '检测（上机）日期', value: 'TESTDATE' },
                        { text: '录入（操作）日期', value: 'OPERDATE' }
                    ]
                })
            },
            {
                xtype: 'textfield',
                style: me.itemStyle,
                name: 'HistoryCompareDefaultDates',
                itemId: 'HistoryCompareDefaultDates',
                fieldLabel: '历史对比默认查询天数',
                listeners: {
                    render: function (field, p) {
                        Ext.QuickTips.init();
                        Ext.QuickTips.register({
                            target: field.el,
                            text: "请填入数值！"
                        })
                    }
                }
            },
            //{
            //    xtype: 'uxradiogroup', defaultSelect: 1,
            //    fieldLabel: '历史对比框默认状态',
            //    style: me.itemStyle,
            //    name: 'HistoryDefaultCollapsed',
            //    itemId: 'HistoryDefaultCollapsed',
            //    data: [{ text: '弹出', value: 1 }, { text: '收起', value: 2 }]
            //},
            {
                xtype: 'checkbox',
                style: me.itemStyle,
                name: 'HistoryDefaultCollapsed',
                itemId: 'HistoryDefaultCollapsed',
                boxLabel: '历史对比框默认弹出'
            }
        );
        items.push(
            {
                xtype: 'textfield',
                style: me.itemStyle,
                name: 'sortFields',
                itemId: 'sortFields',
                fieldLabel: '预览结果数据的排序字段',
                listeners: {
                    render: function (field, p) {
                        Ext.QuickTips.init();
                        Ext.QuickTips.register({
                            target: field.el,
                            text: "文本格式：字段名1,排序方式;字段名2,排序方式   例如：disporder,ASC;ReceiveDate,DESC  ASC为升序，DESC为降序"
                        })
                    }
                }
            },
            {
                xtype: 'textfield',
                style: me.itemStyle,
                name: 'queryDateRange',
                itemId: 'queryDateRange',
                fieldLabel: '查询条件日期范围(天数)',
                listeners: {
                    render: function (field, p) {
                        Ext.QuickTips.init();
                        Ext.QuickTips.register({
                            target: field.el,
                            text: "请填入数值"
                        })
                    }
                } 
            },
            {
                xtype: 'checkbox',
                style: me.itemStyle,
                name: 'NewWindowLoadIframeToPrint',
                itemId: 'NewWindowLoadIframeToPrint',
                boxLabel: '点击打印，是否新窗口加载iframe预览打印'
            }

        );
        items.push(
            
            {
                xtype: 'checkbox',
                style: me.itemStyle,
                name: 'IsUseClodopPrint',
                itemId: 'IsUseClodopPrint',
                boxLabel: '是否使用CLodop方式打印'
            }
        );
        return items;
    },
    RestoreDefault: function () {
        var me = this;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.setDefualtUrl + '?appType=' + me.appType,
            async: false,
            method: 'GET',
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
                if (rs.success) {
                    me.GetAllSetting();
                }
            }
        });
    }
});