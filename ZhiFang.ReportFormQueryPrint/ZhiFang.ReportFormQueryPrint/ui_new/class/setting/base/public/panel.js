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
                xtype: 'button', text: '??????',
                iconCls: 'button-save',
                listeners: {
                    click: function () {
                         rs = me.savePublicSetting();
                         if(rs.success == true){
                         	Shell.util.Msg.showInfo("???????????????");
                         }else{
                         	Shell.util.Msg.showError("???????????????");
                         }
                    }
                }
            }
           /* , {
                    xtype: 'button', text: '????????????',
                    iconCls: 'button-config',
                    listeners: {
                        click: function () {
                            Shell.util.Msg.showMsg({
                                title: '????????????',
                                msg: '??????????????????????????????????????????????????????????????????????????????????????????',
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
            hash["Name"] = "????????????????????????";
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
            fieldLabel: '??????????????????',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: '?????????PatNo="1001" ,????????????????????????'
                    })
                }
            }
        },{
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'defaultDates',
            itemId: 'defaultDates',
            fieldLabel: '??????????????????',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "??????????????????"
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
            fieldLabel: '??????????????????'
        });

        items.push({ colspan: 3, html: '<hr>', border: 0, itemId: 'not' });

        items.push({
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'hasPrint',
            itemId: 'hasPrint',
            boxLabel: '??????????????????'
        }, {
            xtype: 'combobox',
            fieldLabel: 'A4????????????',
            name: 'A4Type',
            itemId: 'A4Type',
            editable:false,
            style: me.itemStyle,
            displayField: 'text', valueField: 'value',
            store: Ext.create('Ext.data.Store', {
                fields: ['text', 'value'],
                data: [
                    { text: 'A4', value: '1' },
                    { text: '16???', value: '2' }
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
                    { text: '???A5', value: '???A5' }
                ]
            }),
            fieldLabel: '??????????????????'
        });

        items.push({
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'maxPrintTimes',
            itemId: 'maxPrintTimes',
            fieldLabel: '??????????????????',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "??????????????????"
                    })
                }
            }
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'mergePageCount',
            itemId: 'mergePageCount',
            fieldLabel: '???A5????????????',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "??????????????????"
                    })
                }
            }
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'ForcedPagingField',
            itemId: 'ForcedPagingField',
            fieldLabel: '??????????????????',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "????????????????????????????????????PatNo ???"
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
             boxLabel: '??????????????????'
         }, {
             xtype: 'checkbox',
             name: 'checkUnprint',
             itemId: 'checkUnprint',
             style: me.itemStyle,
             boxLabel: '????????????????????????'
         }, {
             xtype: 'checkbox',
             name: 'checkFilter',
             itemId: 'checkFilter',
             style: me.itemStyle,
             boxLabel: '?????????????????????'
         });

        items.push({
            xtype: 'checkbox',
            name: 'headCollapsed',
            itemId: 'headCollapsed',
            style: me.itemStyle,
            boxLabel: '?????????????????????'
        }, {
            xtype: 'checkbox',
            name: 'autoSelect',
            itemId: 'autoSelect',
            style: me.itemStyle,
            boxLabel: '???????????????????????????'
        }, {
            xtype: 'checkbox',
            name: 'CheckOnly',
            itemId: 'CheckOnly',
            style: me.itemStyle,
            boxLabel: '??????????????????????????????'
        });

        items.push({
            xtype: 'checkbox',
            name: 'hasReportPage',
            itemId: 'hasReportPage',
            style: me.itemStyle,
            boxLabel: '??????????????????'
        }, {
            xtype: 'checkbox',
            name: 'hasResultPage',
            itemId: 'hasResultPage',
            style: me.itemStyle,
            boxLabel: '??????????????????'
        }, {
            xtype: 'uxradiogroup', defaultSelect: 1,
            fieldLabel: '?????????????????????',
            style: me.itemStyle,
            name: 'defaultCheckedPage',
            itemId: 'defaultCheckedPage',
            data: [{ text: '??????', value: 1 }, { text: '??????', value: 2 }]
        });
        items.push({
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'printCountSetting',
            itemId: 'printCountSetting',
            fieldLabel: '??????????????????',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "??????????????????"
                    })
                }
            }
        },{
            xtype: 'textfield',
            name: 'pdfPrinterList',
            itemId: 'pdfPrinterList',
            fieldLabel: 'PDF???????????????',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: '????????????????????????????????????????????????????????????????????????'
                    })
                }
            }
        });
        items.push({
            xtype: 'checkbox',
            name: 'hasPdfPrinter',
            itemId: 'hasPdfPrinter',
            boxLabel: '??????????????????????????? ????????????IE??????????????????'
        },{
            xtype: 'checkbox',
            name: 'isListHidden',
            itemId: 'isListHidden',
            boxLabel: '?????????????????????<=1????????????????????????????????????????????????',
            style:{            
            marginLeft: '30px'
        	}
        },{
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'isCaseSensitive',
            itemId: 'isCaseSensitive',
            boxLabel: '?????????????????????'
            }, {
                xtype: 'textfield',
                style: me.itemStyle,
                name: 'listWidth',
                itemId: 'listWidth',
                fieldLabel: 'List????????????',
                listeners: {
                    render: function (field, p) {
                        Ext.QuickTips.init();
                        Ext.QuickTips.register({
                            target: field.el,
                            text: "??????????????????"
                        })
                    }
                }
            });
        items.push({
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'isviewportHeader',
            itemId: 'isviewportHeader',
            boxLabel: '???????????????????????????'
        }/*,{
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'IsLabSignature',
            itemId: 'IsLabSignature',
            boxLabel: '?????????????????????'
        }*//*,{
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'IsbTempReport',
            itemId: 'IsbTempReport',
            boxLabel: '??????????????????????????????'
        }*/);
        items.push(
        	{
        		xtype:'checkbox',
        		style:me.itemStyle,
        		name:'IsQueryRequest',
        		itemId:'IsQueryRequest',
        		boxLabel:'????????????Request???'
        	},{
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'MaxDownLoadNum',
            itemId: 'MaxDownLoadNum',
            fieldLabel: '??????????????????',
            listeners: {
                render: function (field, p) {
                    Ext.QuickTips.init();
                    Ext.QuickTips.register({
                        target: field.el,
                        text: "??????????????????"
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
                fieldLabel: '????????????????????????????????????',
                displayField: 'text', valueField: 'value',
                store: Ext.create('Ext.data.Store', {
                    fields: ['text', 'value'],
                    data: [
                        { text: '????????????????????????', value: 'CHECKDATE' },
                        { text: '????????????', value: 'RECEIVEDATE' },
                        { text: '????????????', value: 'COLLECTDATE' },
                        { text: '????????????', value: 'INCEPTDATE' },
                        { text: '????????????????????????', value: 'TESTDATE' },
                        { text: '????????????????????????', value: 'OPERDATE' }
                    ]
                })
            },
            {
                xtype: 'textfield',
                style: me.itemStyle,
                name: 'HistoryCompareDefaultDates',
                itemId: 'HistoryCompareDefaultDates',
                fieldLabel: '??????????????????????????????',
                listeners: {
                    render: function (field, p) {
                        Ext.QuickTips.init();
                        Ext.QuickTips.register({
                            target: field.el,
                            text: "??????????????????"
                        })
                    }
                }
            },
            //{
            //    xtype: 'uxradiogroup', defaultSelect: 1,
            //    fieldLabel: '???????????????????????????',
            //    style: me.itemStyle,
            //    name: 'HistoryDefaultCollapsed',
            //    itemId: 'HistoryDefaultCollapsed',
            //    data: [{ text: '??????', value: 1 }, { text: '??????', value: 2 }]
            //},
            {
                xtype: 'checkbox',
                style: me.itemStyle,
                name: 'HistoryDefaultCollapsed',
                itemId: 'HistoryDefaultCollapsed',
                boxLabel: '???????????????????????????'
            }
        );
        items.push(
            {
                xtype: 'textfield',
                style: me.itemStyle,
                name: 'sortFields',
                itemId: 'sortFields',
                fieldLabel: '?????????????????????????????????',
                listeners: {
                    render: function (field, p) {
                        Ext.QuickTips.init();
                        Ext.QuickTips.register({
                            target: field.el,
                            text: "????????????????????????1,????????????;?????????2,????????????   ?????????disporder,ASC;ReceiveDate,DESC  ASC????????????DESC?????????"
                        })
                    }
                }
            },
            {
                xtype: 'textfield',
                style: me.itemStyle,
                name: 'queryDateRange',
                itemId: 'queryDateRange',
                fieldLabel: '????????????????????????(??????)',
                listeners: {
                    render: function (field, p) {
                        Ext.QuickTips.init();
                        Ext.QuickTips.register({
                            target: field.el,
                            text: "???????????????"
                        })
                    }
                } 
            },
            {
                xtype: 'checkbox',
                style: me.itemStyle,
                name: 'NewWindowLoadIframeToPrint',
                itemId: 'NewWindowLoadIframeToPrint',
                boxLabel: '????????????????????????????????????iframe????????????'
            }

        );
        items.push(
            
            {
                xtype: 'checkbox',
                style: me.itemStyle,
                name: 'IsUseClodopPrint',
                itemId: 'IsUseClodopPrint',
                boxLabel: '????????????CLodop????????????'
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