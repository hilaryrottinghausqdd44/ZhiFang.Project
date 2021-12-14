Ext.define("Shell.class.main.app.App", {
    extend: 'Shell.ux.panel.Panel',
    layout:'border',
    bodyPadding:1,
    //地址列表
    urls:[
        { text: "医生页面", leaf: true, url: Shell.util.Path.rootPath + '/ui/doctorReportPrint.html' },
        { text: "护士页面", leaf: true, url: Shell.util.Path.rootPath + '/ui/nurseReportPrint.html' },
        { text: "自助页面", leaf: true, url: Shell.util.Path.rootPath + '/ui/SelfHelpPrint.html' },
        { text: "检验之星", leaf: true, url: Shell.util.Path.rootPath + '/ui/reportprint.html' },
        { text: "门诊页面", leaf: true, url: Shell.util.Path.rootPath + '/ui/departmentReportPrint.html' },
        { text: "报告测试", leaf: true, url: Shell.util.Path.rootPath + '/ui/tiaoshi.html' },
        { text: "删除临时文件", leaf: true, url: Shell.util.Path.rootPath + '/ui/deleteReportPrint.html' }
    ],
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },
    createItems: function () {
        var me = this;
        me.tree = Ext.create("Ext.tree.Panel", {
            region: 'west',
            split: true,
			collapsible: true,
			//collapseMode:'mini',
			title: 'B/S打印控制台',
            width: 200,
            store:  Ext.create('Ext.data.TreeStore', {
                root: {
                    expanded: true,
                    children: me.urls
                }
            }),
            rootVisible: false,
            listeners: {
                itemclick: function (v, r, item) {
                    var n = r.raw.url;
                    me.ifrme.update('<iframe scrolling="auto" frameborder="0" width="100%" height="100%" src="' + n + '"></iframe>');
                    me.ifrme.getComponent("toolbar").getComponent("url").setValue(n);
                }
            }
        });
        me.ifrme = Ext.create("Ext.panel.Panel", {
            region:'center',
            dockedItems:[{
            	xtype:'toolbar',
            	itemId:'toolbar',
            	items:[{
	                itemId:'url',
	                xtype:'textfield',
	                fieldLabel:'入口地址',
	                width:'100%',
	                labelWidth: 60,
	                labelAlign:'right',
	                listeners: {
	                    specialkey: function (field, e) {
	                        if (e.getKey() == Ext.EventObject.ENTER) {
	                            me.ifrme.update('<iframe scrolling="auto" frameborder="0" width="100%" height="100%" src="' + me.ifrme.getComponent("url").getValue() + '"></iframe>');
	                        }
	                    }
	                }
	            }]
            }]
        });
        return [me.tree, me.ifrme];
    }
});