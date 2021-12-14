/**
 * 内容区域
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.ContentTab',{
    extend:'Ext.tab.Panel',
    
    //获取系统列表服务
	GET_SYSTEM_LIST_URL:JcallShell.System.Path.ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL',
	//系统列表
	SYSTEM_LIST:null,
	//系统MAP
	SYSTEM_MAP:{},
    
    afterRender:function() {
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
    	me.getSystemList();
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
			closeAllTabsText:'关闭所有',
			onCloseAll : function(ignoreClosable){
		        this.doClose(false,ignoreClosable);
			},
		    doClose : function(excludeActive,ignoreClosable){
		        var items = [];
		
		        this.tabPanel.items.each(function(item){
		            if(item.closable || ignoreClosable === true){
		                if(!excludeActive || item != this.item){
		                    items.push(item);
		                }
		            }
		        }, this);
		
		        Ext.each(items, function(item){
		            this.tabPanel.remove(item);
		        }, this);
		    }
		});
		
//		me.items = [Ext.create('Shell.class.sysbase.main.Home',{
//			itemId:'main'
//		})];
		
		me.callParent(arguments);
	},
    insertTab:function(config,hasToActiveTab){
    	if(!config) return;
    	
    	var me = this;
    	if(me.SYSTEM_LIST){
    		me.toDoInsertTab(config,hasToActiveTab);
    	}else{
    		me.getSystemList(function(){
    			me.toDoInsertTab(config,hasToActiveTab);
    		});
    	}
    	
    },
    toDoInsertTab:function(config,hasToActiveTab){
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
    			var UrlInfo = me.getUrlInfo(url);
    			
    			if(UrlInfo.isExtjs){//本系统EXTJS页面
    				var urlArr = UrlInfo.url.split('?');
    				if(urlArr[1]){
    					var params = urlArr[1].split('&');
	    				for(var i=0;i<params.length;i++){
	    					var nameArr = params[i].split('=');
	    					panel[nameArr[0].toLocaleUpperCase()] = nameArr[1];
	    				}
    				}
            		panel = Ext.create(urlArr[0].slice(1),panel);
    			}else{//其他页面
    				panel = me.createUrlPanel(panel,UrlInfo.url,config.tid);
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
        
        if(config.closable == false){
        	con.closable = false;
        }
        
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
    	
    	if(url.slice(0,1) == '$'){
    		url = url.slice(1);
    		url = url.replace(/{account}/g,JShell.LocalStorage.get("account"))
    			.replace(/{password}/g,JShell.LocalStorage.get("password"));
    	}else if(url.slice(0,3) == '../'){
    		url = JShell.System.Path.UI + '/' + url.replace(/\.\.\//g,'');
    		url += (url.indexOf('?') != -1 ? '&' : '?') + 'moduleId=' + id;
    	}else if(url.indexOf('getAppHtmlPath()@@') != -1){
    		url = JShell.System.Path.UI + '/app/app.html' + url.split('@@')[1];
    		url += (url.indexOf('?') != -1 ? '&' : '?') + '&moduleId=' + id;
    	}
    	
    	//每一次请求URL都不会缓存
    	url += (url.indexOf('?') != -1 ? '&' : '?') + 'v=' + new Date().getTime() + '&isExtjs=true';
    	
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
    onCloseAll:function(ignoreClosable){
    	this.plugins[0].onCloseAll(ignoreClosable);
    },
    //获取系统列表
	getSystemList:function(callback){
		var me = this,
			fields = ['SystemCode','SystemName','SystemHost'],
			url = me.GET_SYSTEM_LIST_URL + '?fields=IntergrateSystemSet_' + fields.join(',IntergrateSystemSet_');
		
		me.body.mask('获取系统信息...');//显示遮罩层
		JShell.Server.get(url,function(data){
			me.body.unmask();//隐藏遮罩层
			if(data.success){
				me.SYSTEM_LIST = (data.value || {}).list || [];
				for(var i in me.SYSTEM_LIST){
					me.SYSTEM_MAP[me.SYSTEM_LIST[i].SystemCode] = me.SYSTEM_LIST[i];
				}
				if(Ext.typeOf(callback) == 'function'){
					callback();
				}
			}else{
				JShell.msg.error(data.msg);
			}
		});
	},
	//获取功能路径	
	getUrlInfo:function(url){
		var me = this,
			url = url || '',
			start = url.indexOf('{'),
			end = url.indexOf('}'),
			href = '',
			data = {
				otherSystem:false,
				isExtjs:false,
				url:url
			};
			
		if(url.slice(0,4) == 'http'){//互联网路径
			//不做处理
		}else if(start == -1 || end == -1){//本系统
			if(url.slice(0,1) == '#'){//ExtJS页面
				data.isExtjs = true;
	    	}else if(url.slice(0,1) == '/'){//本地相对路径
	    		data.url = JShell.System.Path.ROOT + url;
	    	}
		}else if(start != -1 && end != -1){//同域其他系统
			var systemCode = url.slice(start+1,end);
			var systemInfo = me.SYSTEM_MAP[systemCode];
			
			data.url = url.slice(end+1);
			
			if(!systemInfo){//系统编码没有配置
				data.isUrl = true;
				data.url = '/ui/layui/views/html/nosystem.html?code=' + systemCode;
			}else{
				if(data.url.slice(0,1) == '#'){//ExtJS页面
					data.otherSystem = true;
					
					url = url.replace(/\?/g,'&');
					data.url = JShell.System.Path.LOCAL + '/' + systemInfo.SystemHost + '/ui/extjs/interface/one/index.html?className=' + data.url.slice(1);
		    	}else if(data.url.slice(0,1) == '/'){//相对路径
		    		data.systemName = true;
		    		data.url = JShell.System.Path.LOCAL + '/' + systemInfo.SystemHost + data.url;
		    	}
			}
		}
		
		return data;
	}
});
	