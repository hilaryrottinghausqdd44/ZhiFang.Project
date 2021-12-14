Ext.define("Shell.class.setting.clearPrintCount.App", {
    extend: 'Shell.ux.panel.Panel',
    
    layout:'border',
    //bodyPadding:'10px 10px 0px 10px',
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },
    createItems: function () {
        var me = this;
        
		me.grid = Ext.create('Shell.class.setting.clearPrintCount.grid',{
			region: 'center',	       
	        title: '清除打印次数',
	        collapsible: true,
            floatable: true,
            split: true,
	        minHeight: 80,
	        itemId: 'Grid'
		});
		
        return [me.grid];
    }
});