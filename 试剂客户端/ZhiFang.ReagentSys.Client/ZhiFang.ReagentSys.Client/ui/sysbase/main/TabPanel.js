/**
 * 内容显示TAB面板
 * @author Jcall
 * @version 2014-08-18
 */
Ext.define('Shell.sysbase.main.TabPanel',{
	extend:'Ext.tab.Panel',
	alias:'widget.maintabpanel',
	
	mixins:['Shell.ux.panel.Panel'],
	
	prefix:'maintabpanel-',
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//开启右键快捷菜单设置
		me.onContextMenu();
		//视图准备完毕
		me.on({
			boxready:function(){me.boxIsReady();},
			expand:function(p,d){
				if(me.isCollapsed){me.load(null,true);}
				me.isCollapsed = false;
			}
		});
	},
	/**
     * 添加TAB
     * @public
     * @param {} app{className,itemId,text,icon,iconCls,url}
     * @param {} hasToActiveTab 是否默认选中
     */
    setApp:function(app,hasToActiveTab){
    	var me = this,
    		errorClassName = 'Shell.ux.panel.ErrorPanel',
    		errorPanel = {
    			title:'错误页面',
    			iconCls:'button-error',
    			closable:true
    		};
    		panel;
    		
    	if(!app){//app不存在
    		panel = Ext.create(errorClassName,errorPanel);
    		if(hasToActiveTab){me.setActiveTab(panel);}
    		return;
    	}
    	
    	app.itemId = me.prefix + (app.itemId != null ? app.itemId : '');
    	
    	if(app.itemId == me.prefix){//没有内部编号
    		panel = Ext.create(errorClassName,errorPanel);
    		if(hasToActiveTab){me.setActiveTab(panel);}
    		return;
    	}
    	
    	var type = Ext.typeOf(app),
    		panel = me.getComponent(app.itemId);
    	
    	if(panel){//panel存在于tabpanel中
    		if(hasToActiveTab){me.setActiveTab(panel);}
    		return;
    	}
    	
		if(type == 'string'){
			panel = Ext.create(app);
		}else if(type == 'object'){
			if(app.xtype){
	    		panel = app;
	    	}else{
	    		if(app.className){
	    			panel = Ext.create(app.className,app);
	    		}else if(app.url){
	    			panel = Ext.create('Ext.panel.Panel',Ext.apply({
	    				html:"<html><body><iframe id=" + app.itemId + " src='" + app.url + "' height='100%' width='100%' frameborder='0' " +
							"style='overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;position:absolute;" +
							"top:0px;left:0px;right:0px;bottom:0px'></iframe></body></html>"
	    			},app));
	    		}else{
	    			panel = Ext.create(errorClassName,errorPanel);
	    		}
	    	}
		}else{
			panel = Ext.create(errorClassName,errorPanel);
		}
    	
        panel.padding = 1;//设置面板四周的间距
        var p = me.add(panel);//添加应用面板
        if(hasToActiveTab){me.setActiveTab(p);}
    }
});