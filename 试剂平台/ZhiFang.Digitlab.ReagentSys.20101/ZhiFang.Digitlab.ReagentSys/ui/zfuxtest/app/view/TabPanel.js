Ext.define('AM.view.TabPanel',{ 
    extend: 'Ext.tab.Panel',
    alias : 'widget.tabpanel',
    initComponent : function(){ 
        Ext.apply(this,{ 
            id: 'content-panel', 
            defaults: { 
            	autoScroll:true, 
                bodyPadding: 2
            }, 
            activeTab: 0, 
            border: true, 
           	plain: true,
            items: [{ 
            	id: 'HomePage', 
                title: '首页', 
                //iconCls:'home',
                html: "<html><body><iframe src='Page/main.html' style='height:100%;width:100%' frameborder='no'></iframe></body></html>",
                layout: 'fit' 
            }] 
        }); 
        this.callParent(arguments); 
    } 
}) 