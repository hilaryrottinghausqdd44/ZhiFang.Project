/**
 * 控制台
 */
Ext.ns('Ext.main');
Ext.define('Ext.main.Main',{
	extend:'Ext.panel.Panel',
	alias:'widget.main',
	requires: ['Ext.main.MainTreePanel','Ext.main.MainTabPanel','Ext.main.MainTopPanel','Ext.main.MainBottomPanel'],
	//===================可配置参数====================
	/**
	 * 树标题
	 * @type String
	 */
	title:'',//'控制台',
	/**
	 * 左树面板宽度
	 * @type Number
	 */
	westWidth:220,
	/**
	 * top面板高度
	 * @type Number
	 */
	northHeight:89,
	/**
	 * bottom面板高度
	 * @type Number
	 */
	southHeight:25,
	/**
     * 获取应用信息的后台服务地址
     * @type String
     */
    getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    /**
     * 模块id前缀
     * @type String
     */
    modulePrefix:'module-',
	//=================================================
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//监听各个模块的事件
		me.initListeners();
		//根据cookie中记录的已打开模块显示模块
		//me.showOpenedModule();
	},
	/**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		Ext.Loader.setPath('Ext.main',getRootPath() + '/ui/main/class');
		me.layout = 'border';
		me.bodyStyle = "background-color:#0C82B3;";
		me.items = me.createItems();
		me.addEvents('moduleclick');//注册点击常用模块事件
		me.addEvents('lockclick');//注册点击锁定用户事件
		me.addEvents('closeclick');//注册点击退出系统事件
		me.callParent(arguments);
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		var items = [{
			region:'west',
			xtype:'maintreepanel',
			itemId:'maintreepanel',
			width:me.westWidth,
			collapsible:true,split:true,
			padding:'0 0 0 2',
			header:{height:30}
		},{
			region:'north',
			xtype:'maintoppanel',
			itemId:'maintoppanel',
			height:me.northHeight,
			border:false,
			padding:'0 2 0 2'
		},{
			region:'center',
			xtype:'maintabpanel',
			itemId:'maintabpanel',
			id:'maintabpanel',
			items:[{
				xtype:'panel',
				itemId:'maintabpanel-'+cookie['100001'],
				title:'首页',
				html:'<div style="margin-top:20px;text-align:center;font-weight:bold;">首页</div>'
			}],
			padding:'0 2 0 0',
			tabBar:{
	        	height:30,
	            defaults:{
	            	height:27,
	            	minWidth:100
	            }
	        }
		},{
			region:'south',
			itemId:'mainbottompanel',
			xtype:'mainbottompanel',
			height:me.southHeight,
			border:false
		}];
		return items;
	},
	/**
	 * 监听各个模块的事件
	 * @private
	 */
	initListeners:function(){
		var me = this;
		var maintoppanel = me.getComponent('maintoppanel');
		var maintreepanel = me.getComponent('maintreepanel');
		var maintabpanel = me.getComponent('maintabpanel');
		//左树联动事件处理
		maintreepanel.on({
			itemclick:function(com,record){
				me.maintreepanelItemclick(record);
			},
			beforeload:function(store,operation,eOpts){
				me.oldModuleId = getCookie('000500');
				var expires = getCookieDate();//cookie失效时间
				Ext.util.Cookies.set('000500',cookie['100001'],expires);//默认模块ID
			},
			load:function(store,operation,eOpts){
				var expires = getCookieDate();//cookie失效时间
				Ext.util.Cookies.set('000500',me.oldModuleId,expires);//默认模块ID
			}
		});
		//最大最小快捷键监听（F2）
		me.maxAndMinListeners();
		//
		maintoppanel.on({
			//点击常用模块按钮
			moduleclick:function(app){
				var obj = {
					tid:app.tid,
					text:app.text,
					icon:app.icon,
					url:app.url,
					isComponent:app.moduletype == 0 ? true :false
				};
				
				me.setApp(obj,true);
				me.fireEvent('moduleclick');
			},
			//点击锁定用户按钮
			lockclick:function(name){
				me.fireEvent('lockclick',name);
			},
			//点击退出系统按钮
			closeclick:function(){
				me.fireEvent('closeclick');
			},
			changeSysStyle:function(url){
				var items = maintabpanel.items.items;
				for(var i in items){
					var item = items[i];
					var iframe = document.getElementById(item.itemId);
					iframe && iframe.contentWindow.changeStyle(url);
				}
			}
		});
		//tab页切换的时候改变Cookie中模块ID值
		maintabpanel.on({
			tabchange:function(tabPanel,newCard,oldCard,eOpts){
				var expires = getCookieDate();//cookie失效时间
				var moduleId = newCard.itemId.split('-').slice(-1);
				Ext.util.Cookies.set('000500',moduleId,expires);//当前模块ID
			}
		});
	},
	/**
	 * 左树联动事件处理
	 * @private
	 * @param {} record
	 */
	maintreepanelItemclick:function(record){
		var me = this;
		var url = record.get('url');
		//数据对象
		var obj = {
			url:url,
			tid:record.get('tid'),
			text:record.get('text'),
			icon:record.get('icon'),
			isComponent:record.get('ModuleType') == 0 ? true :false
		};
		
		//添加应用
		me.setApp(obj,true);
	},
	/**
	 * 根据数据对象添加应用
	 * @private
	 * @param {} obj{url,tid,text,icon,isComponent}
	 * @param {} hasToActiveTab
	 */
	setApp:function(obj,hasToActiveTab){
		var me = this;
		var appId = "";
		
		//构建的访问路径处理
		var url = obj.url;
		if(url && url != ""){
			var arr = url.split("@@");
			if(arr.length == 2){
				url = eval(arr[0])+arr[1];
			}
		}
		
		//var url = obj.url;
		var arr = url.split('?id=');
		(arr.length == 2) && (appId = arr[1]);
		if(url && url != ""){
			//请求的地址带上模块ID
			url += (url.indexOf('?') != -1 ? "&" : "?");
			url += "moduleId=" + obj.tid;
			//整体样式
			var sysStyleUrl = me.getComponent('maintoppanel').getSysStyleUrl();
			url += "&sysStyleUrl=" + sysStyleUrl;
		}
		
		if(obj.tid != 0){
			var callback = function(ClassCode){
				var app = {
					itemId:me.modulePrefix + obj.tid,
					text:obj.text,
					icon:obj.icon,
					url:url,
					isComponent:obj.isComponent,
					classCode:ClassCode
				};
				var maintabpanel = me.getComponent('maintabpanel');
				maintabpanel.setApp(app,hasToActiveTab);
			}
//			if(obj.isComponent && appId != ""){
//				me.getAppInfo(appId,callback);
//			}else{
//				callback("");
//			}
			callback("");
		}
	},
	/**
	 * 最大最小快捷键监听（F2）
	 * @private
	 */
	maxAndMinListeners:function(){
		var me = this;
		//绑定一个新的快捷键
		var keyMap=new Ext.KeyMap(Ext.getBody(),{  
			key:Ext.EventObject.F2,
			//ctrl:true,
			fn:function(){me.changeToMaxOrMin();},
			scope:this,
			defaultEventAction: "stopEvent"
		});  
		keyMap.enable();
	},
	/**
	 * 处理最大或最小
	 * @private
	 */
	changeToMaxOrMin:function(){
		var me = this;
		var maintoppanel = me.getComponent('maintoppanel');
		var maintreepanel = me.getComponent('maintreepanel');
		
		if(me.isMax){
			maintoppanel.show();
			maintreepanel.show();
			me.isMax = false
		}else{
			maintoppanel.hide();
			maintreepanel.hide();
			me.isMax = true;
		}
	},
	/**
	 * 获取应用信息
	 * @private
	 * @param {} callback
	 */
	getAppInfo:function(id,callback){
		var me = this;
        
        if(id){
            var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,//非异步
                url:url,
                method:'GET',
                timeout:5000,
                success:function(response,opts){
                    var result = Ext.JSON.decode(response.responseText);
                    if(result.success){
                    	result.ResultDataValue =result.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                    	var ClassCode = "";
                    	if(result.ResultDataValue && result.ResultDataValue != ""){
                    		var appInfo = Ext.JSON.decode(result.ResultDataValue);
                        	ClassCode = appInfo['BTDAppComponents_ClassCode'] || "";
                    	}
                        if(Ext.typeOf(callback) == "function"){
                        	callback(ClassCode);//回调函数
                        }
                    }else{
                        alertError(result.errorInfo);
                    }
                },
                failure : function(response,options){ 
                    alertError('获取应用组件信息请求失败！');
                }
            });
        }else{
        	if(Ext.typeOf(callback) == "function"){
	            callback("");//回调函数
	        }
        }
	},
	/**
	 * 根据cookie中记录的已打开模块显示模块
	 * @private
	 */
	showOpenedModule:function(){
		var me = this;
		var maintreepanel = me.getComponent('maintreepanel');
		var maintabpanel = me.getComponent('maintabpanel');
		//打开cookie中记录的应用
		var openedModules = function(){
			var openedModuleIds = getCookie('100004');
			if(openedModuleIds && openedModuleIds != ""){
				var arr = openedModuleIds.split(",");
		    	for(var i in arr){
		    		var id = arr[i].split("-")[1];
		    		var node = maintreepanel.findNodeById(id);
		    		if(node){
		    			var data = node.data;
		    			//数据对象
						var obj = {
							url:data.url,
							tid:data.tid,
							text:data.text,
							icon:data.icon,
							isComponent:data.isComponent
						}
						//添加应用
						me.setApp(obj,false);
		    		}
		    	}
			}
		}
		//是否已获取到模块数数据
		var hasData = maintreepanel.hasData();
		var domain = function(){
			bo = maintreepanel.hasData();
			if(bo){
				openedModules();
			}else{
				setTimeout(domain,100);
			}
		}
		domain(hasData);
	},
	//=========================对外公开方法========================
	/**
	 * 设置用户名称
	 * @public
	 * @param {} userName
	 */
	setUserName:function(userName){
		var me = this;
		//设置地步信息栏的用户信息
		var mainbottompanel = me.getComponent('mainbottompanel');
		if(mainbottompanel){
			mainbottompanel.setUserInfo(userName);
		}
	},
	/**
	 * 设置LOGO
	 * @public
	 * @param {} logoName
	 */
	setLogo:function(logoName){
		var me = this;
		var maintoppanel = me.getComponent('maintoppanel');
		if(maintoppanel){
			maintoppanel.setLogo(logoName);
		}
	},
	/**
	 * 设置版权信息
	 * @public
	 * @param {} copyrightInfo
	 */
	setCopyrightInfo:function(copyrightInfo){
		var me = this;
		var mainbottompanel = me.getComponent('mainbottompanel');
		if(mainbottompanel){
			mainbottompanel.setCopyrightInfo(copyrightInfo);
		}
	},
	/**
	 * 更新左树
	 * @public
	 */
	loadTree:function(){
		var me = this;
		var tree = me.getComponent('maintreepanel');
		tree.load();
	}
});