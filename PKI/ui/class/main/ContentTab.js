/**
 * 内容区域
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.main.ContentTab',{
    extend:'Ext.tab.Panel',
    
    insertTab:function(config){
    	if(!config) return;
    	
    	var me = this,
    		itemId = config.itemId,
    		panel = me.getComponent(itemId);
    		
    	if(!panel){
    		var className = config.className;
    		if(className){
    			panel = Ext.create(className,config);
    		}
    		panel = me.add(panel);
    	}
    	me.setActiveTab(panel);
    }
});
	