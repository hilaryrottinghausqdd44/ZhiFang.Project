Ext.define("Shell.class.setting.ScriptingOptions.update.app", {
    extend: 'Shell.ux.panel.Panel',
    
    layout:'border',
    bodyPadding:'10px 10px 0px 10px',
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.dbGrid.on({
            itemclick: function (v, record) {
                if (record.data.Describe == "报告库") {
                    me.scriptGrid.selectUrl=Shell.util.Path.rootPath+'/ServiceWCF/ReportFormService.svc/GetClassDic?classname=ReportDB&classnamespace=ZhiFang.ReportFormQueryPrint.Model';
                    console.log(me.scriptGrid);
                    me.scriptGrid.onSearch(null);
                } else {
                   me.scriptGrid.selectUrl='/ServiceWCF/ReportFormService.svc/GetClassDic?classname=DigitlabDB&classnamespace=ZhiFang.ReportFormQueryPrint.Model';
                    me.scriptGrid.onSearch();
                }
            },
            select: function (v, record) {
                if (record.data.Describe == "报告库") {
                    me.scriptGrid.selectUrl='/ServiceWCF/ReportFormService.svc/GetClassDic?classname=ReportDB&classnamespace=ZhiFang.ReportFormQueryPrint.Model';
                    me.scriptGrid.load();
                } else {
                   me.scriptGrid.selectUrl='/ServiceWCF/ReportFormService.svc/GetClassDic?classname=DigitlabDB&classnamespace=ZhiFang.ReportFormQueryPrint.Model';
                    me.scriptGrid.load();
                }
            }
        });
	},
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },
    createItems: function () {
        var me = this;
		me.dbGrid = Ext.create("Shell.class.setting.ScriptingOptions.update.dbGrid", {
            region: 'west',
            header: false,
            title: '数据库类型',
            itemId: 'dbGrid',
            width:280,
            collapsible: true,
            floatable: true,
            split: true,
	        minHeight: 80
        });
        me.scriptGrid = Ext.create("Shell.class.setting.ScriptingOptions.update.scriptGrid",{
			region:'center',
			header:false,
			title: '数据库脚本',
			itemId:'grid',
			collapsible: true,
            floatable: true,
            split: true,
	        minHeight: 80
		});
		
        return [me.scriptGrid];
    }
});