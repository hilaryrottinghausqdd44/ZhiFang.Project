/**
 * 应用栏
 */
Ext.ns('Ext.main');
Ext.define('Ext.main.MainTabPanel',{
	extend:'Ext.tab.Panel',
	alias:'widget.maintabpanel',
	
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
		me.callParent(arguments);
	},
	/**
     * 添加TAB
     * @public
     * @param {} app{itemId,text,url,icon,iconCls,isComponent,classCode}
     * @param {} hasToActiveTab
     */
    setApp:function(app,hasToActiveTab){
    	var itemId = "maintabpanel-" + app.itemId,
    		text = app.text,
    		isComponent = app.isComponent,//是否以组件的方式追加
    		classCode = app.classCode,//类代码
    		url = app.url,
    		icon = app.icon,
    		iconCls = app.iconCls;
    		
    	var me = this;
    	
    	var panel = me.getComponent(itemId);
    	
        if(!panel){//不存在
            panel = {
                title:text,
                closable:true
            };
            
            if(itemId){
            	panel.itemId = itemId;
            }
            
            if(icon){
            	panel.icon = icon;
            }else{
            	if(iconCls){
            		panel.iconCls = iconCls;
            	}
            }
            //关闭叶签监听
            panel.listeners = {
            	close:function(){
            		me.removeModuleIdFormCookie(app.itemId);
            	}
            };
            
            if(isComponent && classCode != ""){//是否以组件的方式追加
            	var cl = eval(classCode);
            	panel = Ext.create(cl,panel);
            }else{//以iframe方式内嵌
            	if(url){
	            	if(url.slice(0,1) == '#'){
	            		panel = Ext.create(url.split('?')[0].slice(1),panel);
	            	}else{
	            		panel.listeners.afterlayout = function(con,layout){
							if(!con.hasLayout){
								con.hasLayout= true;
								var html = "<html><body><iframe id="+itemId+" src='"+url+"' height='100%' width='100%' frameborder='0' " +
										"style='overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;position:absolute;" +
										"top:0px;left:0px;right:0px;bottom:0px'></iframe></body></html>";
								con.update(html);
							}
						};
	            	}
            	}else{
	            	panel.html = '<div style="font-weight:bold;color:red;text-align:center;margin-top:20px;">没有绑定链接！</div>';
	            }
            }
            
            //设置面板四周的间距
            //panel.padding = 2;
            //添加应用面板
            var p = me.add(panel);
            if(hasToActiveTab){
            	me.setActiveTab(p);
            }
            //将已打开的模块id记录下来存进cookie
            me.setModuleIdToCookie(app.itemId);
        }else{ 
        	if(hasToActiveTab){
        		me.setActiveTab(panel);
        	}
        }
    },
    /**
     * 将已打开的模块id记录下来存进cookie
     * @private
     * @param {} id
     */
    setModuleIdToCookie:function(id){
    	var openedModuleIds = getCookie('100004');
		if(openedModuleIds && openedModuleIds != ""){
    		var arr = openedModuleIds.split(",");
    		var bo = true;
    		for(var i in arr){
    			if(arr[i] == id){
    				bo = false;
    				break;
    			}
    		}
    		if(bo){
    			openedModuleIds = openedModuleIds + "," + id;
    		}
    	}else{
    		openedModuleIds = id;
    	}
    	//cookie失效时间
    	var expires = getCookieDate();
    	Ext.util.Cookies.set('100004',openedModuleIds,expires);
    },
    /**
     * 从cookie中获取已打开的模块id数组
     * @private
     * @return {}
     */
    getModuleIdsFormCookie:function(){
    	var openedModuleIds = getCookie('100004');
    	var arr = openedModuleIds.split(",");
    	return arr;
    },
    /**
     * 从cookie中根据id删除已打开模块记录
     * @private
     * @param {} id
     */
    removeModuleIdFormCookie:function(id){
    	var openedModuleIds = getCookie('100004');
    	if(openedModuleIds){
    		var arr = openedModuleIds.split(",");
	    	for(var i in arr){
	    		if(arr[i] == id){
	    			arr.splice(i,1);//删除这个节点id
	    			break;
	    		}
	    	}
	    	openedModuleIds = arr.join(",");
	    	Ext.util.Cookies.set('100004',openedModuleIds);
    	}
    }
});