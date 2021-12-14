/**
 * View
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.main.Viewport',{
	extend:'Ext.container.Viewport',
	layout:'fit',
	
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	
    	me.FunctionTree.on({
    		itemclick:function(v,record){
    			var leaf = record.get('leaf');
    			if(leaf){
    				me.ContentTab.insertTab({
	    				title:record.get('text'),
	    				iconCls:record.get('iconCls'),
	    				url:record.get('url'),
	    				className:record.get('className'),
	    				itemId:record.get('tid'),
	    				closable:true
	    			});
    			}
    		}
    	});
    },
    
	initComponent:function(){
		var me = this;
		me.FunctionTree = Ext.create('Shell.class.main.FunctionTree',{
			region:'west',width:200,header:false,title:'功能树',
			itemId:'FunctionTree',split:true,collapsible:true
		});
		me.ContentTab = Ext.create('Shell.class.main.ContentTab',{
			region:'center',header:false,title:'功能区域',
			itemId:'ContentTab',split:true,collapsible:true
		});
		me.items = [{
			itemId:'view',
			bodyPadding:1,
			layout:'border',
			tbar:me.createToolbar(),
			items:[me.FunctionTree,me.ContentTab]
		}];
		
		me.callParent(arguments);
	},
	createToolbar:function(){
		var me = this;
		return [{
			xtype:'label',text:JShell.System.Name,
			style:'margin:5px 10px;color:#04408c;font-weight:bold;font-size:16px;'
		}];
	}
});