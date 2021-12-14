Ext.define("Shell.reportSetting.class.doctor.public.panel", {
    extend: 'Shell.reportSetting.class.base.public.panel',

    createItems: function () {
        var me = this;
        var items = [];
        items.push({
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'defaultWhere',
            itemId: 'defaultWhere',
            fieldLabel: '默认查询条件'
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
                        text: '字符全部大写可接收多个逗号隔开'
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
                        text: '字符全部大写'
                    })
                }
            }
        });

        items.push({
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'defaultDates',
            itemId: 'defaultDates',
            fieldLabel: '默认查询天数'
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'DateField',
            itemId: 'DateField',
            labelWidth: 110,
            fieldLabel: '默认查询时间字段',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: '通过地址调用时的默认字段并不是查询框中的字段，字符全部大写'
                    })
                }
            }
        }, {
            xtype: 'combobox',
            style: me.itemStyle,
            name: 'defaultPageSize',
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
            fieldLabel: '最大打印次数'
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'mergePageCount',
            itemId: 'mergePageCount',
            fieldLabel: '双A5合并数量'
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'ForcedPagingField',
            itemId: 'ForcedPagingField',
            fieldLabel: '强制分页字段'
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
            boxLabel: '默认勾选'
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
            xtype: 'checkbox',
            name: 'hasPdfPrinter',
            itemId: 'hasPdfPrinter',
            boxLabel: '是否需要选择打印机 需要降低IE浏览器安全性'
        }, {
            xtype: 'textfield',
            name: 'pdfPrinterList',
            itemId: 'pdfPrinterList',
            fieldLabel: 'PDF打印机数组',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: '可接收多个逗号隔开'
                    })
                }
            }
        }, {
            xtype: 'checkbox',
            name: 'isListHidden',
            itemId: 'isListHidden',
            boxLabel: '当报告列表数量<=1时隐藏报告列表，直接显示报告内容'
        });

        return items;
    }
});