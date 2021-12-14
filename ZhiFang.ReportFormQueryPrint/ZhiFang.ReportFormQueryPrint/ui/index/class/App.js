Ext.define("Shell.index.class.App", {
    extend: 'Shell.ux.panel.Panel',
    layout:'border',
    initComponent: function () {
        var me = this;
        
        me.items = me.createItems();
        me.callParent(arguments);
    },
    createItems: function () {
        var me = this;
        me.tree = Ext.create("Ext.tree.Panel", {
            region: 'west',
            title: '列表',
            split: true, collapsible: true,
            width: 200,
            store:  Ext.create('Ext.data.TreeStore', {
                root: {
                    expanded: true,
                    children: [
                        { text: "医生页面", leaf: true, url: Shell.util.Path.rootPath + '/ui/doctorReportPrint.html' },
                        { text: "护士页面", leaf: true, url: Shell.util.Path.rootPath + '/ui/nurseReportPrint.html' },
                        { text: "自助页面", leaf: true, url: Shell.util.Path.rootPath + '/ui/SelfHelpPrint.html' },
                        { text: "检验之星", leaf: true, url: Shell.util.Path.rootPath + '/ui/reportprint.html' },
                        { text: "门诊页面", leaf: true, url: Shell.util.Path.rootPath + '/ui/departmentReportPrint.html' },
                        { text: "报告测试", leaf: true, url: Shell.util.Path.rootPath + '/ui/tiaoshi.html' },
                        { text: "删除临时文件", leaf: true, url: Shell.util.Path.rootPath + '/ui/deleteReportPrint.html' },
                        { text: "SVC服务", leaf: true, url: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc' },
                        { text: "WCF服务", leaf: true, url: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormWebService.asmx' }
                    ]
                }
            }),
            rootVisible: false,
            listeners: {
                itemclick: function (v, r, item) {
                    var n = r.raw.url;
                    me.ifrme.update('<iframe scrolling="auto" frameborder="0" width="100%" height="100%" src="' + n + '"></iframe>');
                    me.ifrme.getComponent("url").setValue(n);
                }
            }
        });
        me.ifrme = Ext.create("Shell.ux.panel.Panel", {
            region: 'center',
            toolbars : [
            {
                itemId:'url',
                xtype: 'textfield',
                fieldLabel: 'URL',
                style:'margin-left:20px',
                labelWidth: 30,
                listeners: {
                    specialkey: function (field, e) {
                        if (e.getKey() == Ext.EventObject.ENTER) {
                            console.log(me.ifrme.getComponent("url").getValue());
                            me.ifrme.update('<iframe scrolling="auto" frameborder="0" width="100%" height="100%" src="' + me.ifrme.getComponent("url").getValue() + '"></iframe>');
                        }
                    }
                }
            }
            ],
            html:''
        });
        return [me.tree, me.ifrme];
    }
});