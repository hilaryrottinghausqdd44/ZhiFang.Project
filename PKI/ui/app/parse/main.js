Ext.onReady(function(){
	//显示错误信息
	function showError(value){
		var error = document.getElementById('error');
		error.innerHTML = "<b style='color:red;'>" + value + "</b>";
	}
	//账户登录
	function login(c){
		//用户名
		strUserAccount = getQueryString("strUserAccount");
		//密码
		strPassWord = getQueryString("strPassWord");
		
		if(!strUserAccount || !strPassWord){
			showError("账号或密码错误！");
			return;
		}
		
		//登录服务地址
		var url = getRootPath()+'/RBACService.svc/RBAC_BA_Login' + 
				"?strUserAccount=" + strUserAccount + "&strPassWord=" + strPassWord;
		
		getToServer(url,function(text){
			var bo = (text + '') == 'true' ? true : false;
			bo ? c() : showError("账号或密码错误！");
		},false);
	};
	
	login(showPanel);
	
	function showPanel(){
		Ext.QuickTips.init();//初始化后就会激活提示功能
		Ext.Loader.setConfig({enabled: true});//允许动态加载
		Ext.Loader.setPath('Ext.main','../../main/class');
		
		/**模块id前缀*/
    	var modulePrefix = 'module-';
		
		var maintreepanel = Ext.create('Ext.main.MainTreePanel',{
			region:'west',itemId:'maintreepanel',width:220,
			collapsible:true,split:true,header:{height:30}
		});
		var maintabpanel = Ext.create('Ext.main.MainTabPanel',{
			region:'center',itemId:'maintabpanel',id:'maintabpanel',
			tabBar:{height:30,defaults:{height:27,minWidth:100}}
		});
		
		//总体布局
		var viewport = Ext.create('Ext.container.Viewport',{
			layout:'border',
			padding:1,
			items:[maintreepanel,maintabpanel]
		});
		
		//左树联动事件处理
		maintreepanel.on({
			itemclick:function(com,record){
				maintreepanelItemclick(record);
			},
			beforeload:function(store,operation,eOpts){
				maintreepanel.oldModuleId = getCookie('000500');
				var expires = getCookieDate();//cookie失效时间
				Ext.util.Cookies.set('000500',cookie['100001'],expires);//默认模块ID
			},
			load:function(store,operation,eOpts){
				var expires = getCookieDate();//cookie失效时间
				Ext.util.Cookies.set('000500',maintreepanel.oldModuleId,expires);//默认模块ID
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
		
		function maintreepanelItemclick(record){
			//数据对象
			var obj = {
				url:record.get('url'),
				tid:record.get('tid'),
				text:record.get('text'),
				icon:record.get('icon'),
				isComponent:record.get('ModuleType') == 0 ? true :false
			};
			
			//添加应用
			setApp(obj,true);
		}
		/**
		 * 根据数据对象添加应用
		 * @private
		 * @param {} obj{url,tid,text,icon,isComponent}
		 * @param {} hasToActiveTab
		 */
		
		function setApp(obj,hasToActiveTab){
			var appId = "";
			
			//构建的访问路径处理
			var url = obj.url;
			if(url && url != ""){
				var arr = url.split("@@");
				if(arr.length == 2){
					url = eval(arr[0])+arr[1];
				}
			}
			
			var arr = url.split('?id=');
			if(arr.length == 2){
				appId = arr[1];
			}
			if(url && url != ""){
				//请求的地址带上模块ID
				url += (url.indexOf('?') != -1 ? "&" : "?");
				url += "moduleId=" + obj.tid;
			}
			
			maintabpanel.setApp({
				itemId:modulePrefix + obj.tid,
				text:obj.text,
				icon:obj.icon,
				url:url,
				isComponent:obj.isComponent,
				classCode:''
			},hasToActiveTab);
		}
	}
});