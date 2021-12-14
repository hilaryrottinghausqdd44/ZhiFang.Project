Ext.define('Shell.SelfHelpPrint.class.PrintApp', {
    extend: 'Ext.panel.Panel',
    printPageType: 'A4', //打印类型  A4 , A5 , 双A5
    selectColumn: 'SerialNo', // 设置查询字段
    lastDay: 90, //查询多少天之前的记录
    printtimes:1, //限制打印次数
    //requires: 'Shell.AutoPrint.class.PrintList',
    layout: {
        type: 'absolute'
    },
    //倒计时关闭
    tackTime:5,
    
    //设置背景图片  
    bodyStyle: {
        background: 'url(http://localhost/ZhiFang.ReportFormQueryPrint/ui/SelfHelpPrint/images/sunset.jpg)',// no-repeat #00FFFF
        //padding: '10px'
    },
    initComponent:function(){
        var me =this;
        me.items = me.createitem();
        me.task = {
            run: function () {
                me.getComponent("close").setText("关闭(" + me.tackTime + ")");
                if (me.tackTime == 0) {
                    me.hideRepot();
                    Ext.TaskManager.stop(me.task);
                }
                me.tackTime--;
            },
            interval: 1000
        };
        me.callParent(arguments);
    },
    createitem:function(){
        var me = this;
        var item = [];
        me.print = Ext.create("Shell.ReportPrint.class.PrintPdfWinS", {
            width: 0,
            height: 0,
            showProgressInfoWin: false,
            listeners: {
                printEnd: function (m, ids) {
                    me.tackTime = 5;
                    me.showRepot();
                    Ext.TaskManager.start(me.task);//启动计时器
                    me.getComponent("reportview").setText("打印完毕");
                }
            }
        });
        item.push(me.print);
        var item1 = [{
            x: '50%',
            y: '30%',
            xtype: 'label',
            itemId: 'reportCont',
            text: '自助打印',
            style: {
                'font-size': '50px'
            }
        }, {
            x: '50%',
            y: '50%',
            xtype: 'field',
            itemId:'carid',
            fieldLabel: '卡号',
            width: 200,
            labelWidth: 30,
            height: 30,
            listeners: {
                specialkey: function (field, e) {
                    if (e.getKey() == Ext.EventObject.ENTER) {
                        me.showRepot();
                        var arr = me.getReport(field.getValue());
                        me.tackTime = 5;
                        var data = [];
                        for (var i = 0; i < arr.length; i++) {
                            var obj = {
                                ReportFormID: arr[i].ReportFormID,
                               // DATE: DATE,
                                SectionNo: '',
                                SectionType: 1,
                                CNAME: '',
                                SAMPLENO: '',
                                PageName: arr[i].PageName,//纸张类型,A4/A5
                                PageCount: arr[i].PageCount,//文件页量
                                url:"/"
                            };
                            data.push(obj);
                        }
                        var conf = {
                            A4Type: 1,
                            strPageName: me.printPageType,//双A5
                            isDoublePrint: null,
                            data: data
                        };
                        me.print.print(conf,false);

                        if (arr.length > 0) {
                            me.getComponent("close").hide();
                            me.getComponent("reportview").setText("共" + arr.length + "份报告，正在打印请稍等");
                            
                        } else {
                            Ext.TaskManager.start(me.task);//启动计时器
                            me.getComponent("reportview").setText("没有可打印的报告");
                        }
                    }
                }
            }
        }, {
            x: '62%',
            y: '50%',
            xtype: 'label',
            hidden:true,
            itemId: 'close',
            text: '关闭',
            listeners: {
                render : function() {//渲染后添加click事件
                    Ext.fly(this.el).on('click',
                      function (e, t) {
                          me.hideRepot();
                          Ext.TaskManager.stop(me.task);
                      });
                },
                scope : this
            }
        }, {
            x: '50%',
            y: '58%',
            xtype: 'label',
            itemId: 'reportview',
            hidden:true,
            text: '查询中。。。。。。。。',
            width: 200,
            height: 30
        }];

        item = Ext.Array.merge(item, item1);
        return item;
    },
    //查询报告
    getReport: function (reportValue) {
        var me = this;
        var uri = Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/SelectReport";
        var arr = [];
        var dateEnd = Shell.util.Date.toString(new Date()).split(" ")[0];
        var dateStart = Shell.util.Date.getNextDate(dateEnd, -me.lastDay);// 时间范围
        dateStart = Shell.util.Date.toString(dateStart);
       
        var where = "PrintTimes < " + me.printtimes + " and " + me.selectColumn + "='" + reportValue + "' and RECEIVEDATE>='" + dateStart + "' and RECEIVEDATE<='" + dateEnd + "'";
       
        var fields = 'ReportFormID,PageName,PageCount';
        Ext.Ajax.request({
            url: uri,
            async: false,
            method: 'GET',
            params: {
                fields:fields,
                where: where,
                page: 1,
                limit:100
            },
            success: function (response, options) {
                var rs = Ext.JSON.decode(response.responseText);
                if (rs.success) {
                    var v = Ext.JSON.decode(rs.ResultDataValue);
                    arr = v.rows;
                }
            }
        });
        return arr;
    },

    //隐藏显示
    showRepot:function(){
        var me = this;
        card = me.getComponent("carid");
        vie = me.getComponent("reportview");
        var close = me.getComponent("close");
        vie.show();
        card.hide();
        close.show();
    },
    hideRepot:function(){
        var me = this;
        var card = me.getComponent("carid");
        var vie = me.getComponent("reportview");
        var close = me.getComponent("close");
        vie.hide();
        card.show();
        close.hide();
    },
    
});