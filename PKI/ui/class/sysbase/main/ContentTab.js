/**
 * 内容区域
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.ContentTab',{
    extend:'Ext.tab.Panel',
    
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
    	me.on({
    		tabchange:function(tabPanel,newCard,oldCard,eOpts){
    			//取得COOKIE失效时间
				var expires = JShell.System.Cookie.getDefaultCookieExpires();
				var moduleId = newCard.itemId.split('_').slice(-1);
				//清空历史打开记录
				Ext.util.Cookies.set(JShell.System.Cookie.map.MODULEID,moduleId,expires);
			}
    	});
    },
    
    /**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.plugins = Ext.create('Ext.ux.TabCloseMenu',{
			closeTabText:'关闭当前',
			closeOthersTabsText:'关闭其他',
			closeAllTabsText:'关闭所有'
		});
		
		me.items = [Ext.create('Shell.class.sysbase.main.Main',{
			itemId:'main'
//		}),Ext.create('Shell.class.sysbase.module.DefaultModule',{
//			itemId:'defaultmodule'
		})];
		
		me.callParent(arguments);
	},
    insertTab:function(config,hasToActiveTab){
    	if(!config) return;
    	
    	var me = this,
    		itemId = me.getId() + '_' + config.tid,
    		url = config.url,
    		className = config.className,
    		panel = me.getComponent(itemId);
    		
    	if(!panel){
    		panel = me.createPanelConfig(config);
            
    		if(className){
    			panel = Ext.create(className,panel);
    		}else if(url){
    			if(url.slice(0,1) == '#'){
    				var urlArr = url.split('?');
    				if(urlArr[1]){
    					var params = urlArr[1].split('&');
	    				for(var i=0;i<params.length;i++){
	    					var nameArr = params[i].split('=');
	    					panel[nameArr[0].toLocaleUpperCase()] = nameArr[1];
	    				}
    				}
            		panel = Ext.create(urlArr[0].slice(1),panel);
            	}else{
            		panel = me.createUrlPanel(panel,url,config.tid);
            	}
    		}
    		panel = me.add(panel);
    	}
    	me.setActiveTab(panel);
    },
    createPanelConfig:function(config){
    	var me = this,
    		itemId = me.getId() + '_' + config.tid,
    		text = config.text,
    		icon = config.icon,
    		iconCls = config.iconCls;
    	
    	var con = {
            title:text,
            closable:true
        };
        
        if(itemId){
        	con.itemId = itemId;
        }
        
        if(icon){
        	con.icon = icon;
        }else{
        	if(iconCls){
        		con.iconCls = iconCls;
        	}
        }
        
        return con;
    },
    createUrlPanel:function(panel,url,id){
    	var me = this;
    	var itemId = panel.itemId;
    	
//  	if(url.slice(0,1) == '/'){
//  		url = JShell.System.Path.UI + url;
//  	}
    	
    	if(url.slice(0,3) == '../'){
    		url = JShell.System.Path.UI + '/' + url.replace(/\.\.\//g,'');
    		url += (url.indexOf('?') != -1 ? '&' : '?') + 'moduleId=' + id;
    	}else if(url.indexOf('getAppHtmlPath()@@') != -1){
    		url = JShell.System.Path.UI + '/app/app.html' + url.split('@@')[1];
    		url += (url.indexOf('?') != -1 ? '&' : '?') + '&moduleId=' + id;
    	}
    		
    	panel.listeners={
			afterlayout:function(con,layout){
				if(!con.hasLayout){
					con.hasLayout= true;
					var html = 
						'<html><body><iframe id="' + itemId + '" src="' + url + 
							'" height="100%" width="100%" frameborder="0" ' +
							'style="overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
							'height:100%;width:100%;position:absolute;' +
							'top:0px;left:0px;right:0px;bottom:0px">' +
						'</iframe></body></html>';
					con.update(html);
				}
			}
		};
		
		return panel;
    },
    onCloseAll:function(){
    	this.plugins[0].onCloseAll();
    }
});
	