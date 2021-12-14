Ext.define("Shell.class.setting.doctor.public.Panel", {
    extend: 'Shell.class.setting.base.public.panel',

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
                        text: '格式：PatNo="1001" ,值要用双引号包含！'
                    })
                }
            }
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            itemId: 'requestParamsArr',
            name: 'requestParamsArr',//设置查询条件之后考虑这里为设置查询条件中没有的条件
            fieldLabel: '其他接收参数',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: '字符全部大写可接收多个，使用逗号隔开！'
                    })
                }
            }
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            itemId: 'hisRequestParamsArr',
            name: 'hisRequestParamsArr',
            fieldLabel: 'nitem表申请单号',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: '字符全部大写！'
                    })
                }
            }
        });

        items.push({
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
            name: 'DateField',
            itemId: 'DateField',
            editable:false,
            labelWidth: 110,
            fieldLabel: '默认查询时间字段',
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
            }),
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "地址栏访问存在参数时，此设置才会被采用"
                    })
                }
            }
        }, {
            xtype: 'combobox',
            style: me.itemStyle,
            name: 'defaultPageSize',
            editable:false,
            itemId: 'defaultPageSize',
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
            value: ['50'],
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
        },{
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
        }*/,{
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'IsbTempReport',
            itemId: 'IsbTempReport',
            boxLabel: '是否显示部分审核报告'
        });
        items.push(
        	{
        		xtype:'checkbox',
        		style:me.itemStyle,
        		name:'IsQueryRequest',
        		itemId:'IsQueryRequest',
        		boxLabel:'是否查询Request表'
        	},{
        		xtype:'checkbox',
        		style:me.itemStyle,
        		name:'IsSampleState',
        		itemId:'IsSampleState',
        		boxLabel:'是否查看样本时间节点状态'
        }, {
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
        },{
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
        }

        );
        items.push(
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
            },
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
            }
        );
        //items.push({ colspan: 3, html: '<hr>', border: 0, itemId: 'not1' });
        items.push(
            {
                xtype: 'checkbox',
                style: me.itemStyle,
                name: 'NewWindowLoadIframeToPrint',
                itemId: 'NewWindowLoadIframeToPrint',
                boxLabel: '点击打印，是否新窗口加载iframe预览打印'
            },
            {
                xtype: 'checkbox',
                style: me.itemStyle,
                name: 'IsUseClodopPrint',
                itemId: 'IsUseClodopPrint',
                boxLabel: '是否使用CLodop方式打印'
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
        return items;
    }
});