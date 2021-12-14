Ext.define("Shell.class.setting.siteQuery.empDeptLinks.panel",{
	extend:'Shell.ux.panel.Panel',
	layout:'border',
	initComponent:function(){
		var me =this;
		me.items=me.createItem();
		me.callParent(arguments);
	},
	createItem:function(){
		var me = this;
    	me.leftGrid = Ext.create("Shell.class.setting.siteQuery.empDeptLinks.userGrid",{
    		region:'center',
    		listeners: {
                itemclick: function (m, record, item, index) {
                	me.rightGrid.deptGridRecord=record.data;
                	me.rightGrid.selectUrl= "/ServiceWCF/DictionaryService.svc/GetEmpDeptLinks?Where= UserNo ="+record.data.UserNo;
                	me.rightGrid.onSearch();
                }
            }
    	});
    	me.rightGrid = Ext.create("Shell.class.setting.siteQuery.empDeptLinks.deptGrid",{
    		region:'east',
    		width:'50%',
    		deptGridRecord:''
    	});
    	return [me.leftGrid,me.rightGrid];
    }     
});
