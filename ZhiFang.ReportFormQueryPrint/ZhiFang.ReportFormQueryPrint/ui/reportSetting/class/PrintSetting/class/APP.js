Ext.define("Shell.reportSetting.class.PrintSetting.class.App", {
    extend: 'Shell.ux.panel.Panel',
    
    layout:'border',
    bodyPadding:'10px 10px 0px 10px',
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		/*me.groupGrid.on({
			itemclick:function(v,r){
				me.grid.CName=r.get("CName");
				me.grid.SectionNo=r.get("SectionNo");
				me.grid.load();
			}
		}),
		me.grid.on({
			itemclick:function(v,f){
				me.grid.spid=r.get("SPID");
				me.grid.load();
			}
		})*/
	},
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },
    createItems: function () {
        var me = this;
        
		me.grid = Ext.create('Shell.reportSetting.class.PrintSetting.class.grid',{
			region: 'center',	       
	        title: '模板信息',
	        collapsible: true,
            floatable: true,
            split: true,
	        minHeight: 80,
	        itemId: 'Grid'
		});
		
        return [me.grid];
    }
});