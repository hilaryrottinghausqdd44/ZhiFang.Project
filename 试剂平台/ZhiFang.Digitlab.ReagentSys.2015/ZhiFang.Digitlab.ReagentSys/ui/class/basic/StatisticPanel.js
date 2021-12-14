/**
 * 统计基础面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.basic.StatisticPanel',{
    extend:'Ext.panel.Panel',
    title:'统计基础面板',
    
    bodyPadding:'1 0',
    overflowY:'auto',
    //autoScroll:true,
    layout:{type:'table',columns:2},
    defaults:{height:300,margin:1},
    
    /**功能栏位置*/
    toolbarDock:'top',
    /**模块设置*/
    modelConfig:{},
    
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	me.on({
    		resize:function(){me.onViewResize();}
    	});
    },
    initComponent:function(){
    	var me = this,
    		modelConfig = me.modelConfig;
    	
    	if(modelConfig.height) me.defaults.height = modelConfig.height;
    	if(modelConfig.margin) me.defaults.margin = modelConfig.margin;
    	if(modelConfig.columns) me.layout.columns = modelConfig.columns;
    	
    	me.items = me.items || [];
    	me.dockedItems = me.dockedItems || [];
    	me.callParent(arguments);
    },
    onViewResize:function(){
    	var me = this,
    		items = me.items.items,
			itemLen = items.length,
			width = me.getWidth(),
			height = me.getHeight(),
			colspanCount = 0;
			
		var toolbar = me.getDockedItems('toolbar[dock="' + me.toolbarDock + '"]')[0];
		var toolbarHeight = toolbar.getHeight();
		
		height -= toolbarHeight;
			
		for(var i=0;i<itemLen;i++){
    		colspanCount += (items[i].colspan || 1);
    	}
		
		var num = Math.ceil(colspanCount / me.layout.columns);
		if(num * me.defaults.height > height) width -= 18;
		if(me.isFloating()) width -= 2;
		eWidth = width / me.layout.columns - me.defaults.margin * 2,
		eWidth = Math.round(eWidth * 10) / 10;
		
    	for(var i=0;i<itemLen;i++){
    		var colspan = items[i].colspan || 1;
    		var w = eWidth * colspan + me.defaults.margin * 2 * (colspan - 1);
    		w = Math.round(w * 10) / 10;
    		items[i].setWidth(w);
    	}
    }
});