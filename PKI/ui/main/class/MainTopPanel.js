/**
 * 应用栏
 */
Ext.ns('Ext.main');
Ext.define('Ext.main.MainTopPanel',{
	extend:'Ext.panel.Panel',
	alias:'widget.maintoppanel',
	//=================================================
	fiedls:"fields=RBACModule_Id,RBACModule_CName,RBACModule_Comment,RBACModule_Url,RBACModule_PicFile",
	/**
	 * 常用应用服务地址
	 * @type 
	 */
	moduleServerUrl:getRootPath()+'/RBACService.svc/RBAC_RJ_CheckEmpModuleRight',
	/**
	 * 模块图标对象
	 * @type 
	 */
	moduleObj:{
		id:'Id',//唯一键
		text:'CName',//名称
		tooltip:'Comment',//描述
		url:'Url',//应用路径
		img:'PicFile',//图片名称
		moduletype:'ModuleType'//构建模式
	},
	logoName:'logo.png',
	sysStyleUrl:getRootPath()+'/ui/extjs/resources/css/ext-all.css',
	/**
	 * 系统整体样式列表
	 * @type 
	 */
	sysStyleList:[
		{text:'默认',filename:'ext-all.css'},
		{text:'橙色',filename:'ext-all-orange.css'},
		{text:'灰色',filename:'ext-all-gray.css'},
		//{text:'海蓝',filename:'ext-neptune.css'},
		{text:'夜间',filename:'ext-all-access.css'}
	],
	//=================================================
	/**
	 * 渲染完后处理
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//加载logo图标
		if(me.logoName && me.logoName != ""){
			me.setLogo(me.logoName);
		}
		//加载常用应用模块列表
		me.loadModules();
	},
	/**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.layout = "border";
		
		me.bodyStyle = "background-color:#0C82B3;background-image:url(../images/main/TitleBackground.jpg);background-repeat:no-repeat;";
		
		me.items = me.createItems();
		me.addEvents('moduleclick');//注册点击常用模块事件
		me.addEvents('lockclick');//注册点击锁定用户事件
		me.addEvents('closeclick');//注册点击退出系统事件
		me.addEvents('changeSysStyle');//注册改变系统风格事件
		me.callParent(arguments);
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		//LOGO图标
		var logo = me.createLogo();
		//系统名称
		var sysName = me.createSysName();
		//常用模块功能
		var module = me.createModule();
		//功能按钮
		var buttons = me.createButtons();
		
		logo.region = "west";
		logo.width = 177;
		
		sysName.region = "west";
		sysName.width = 277;
		
		module.region = "center";
		module.itemId = "modulelist";
		
		buttons.region = "east";
		buttons.width = 200;
		
		var items = [logo,module,buttons,sysName];
		return items;
	},
	/**
	 * 创建logo图标
	 * @private
	 * @return {}
	 */
	createLogo:function(){
		var com = {
			xtype:'image',
			margin:'20 0 20 23',
			itemId:'logo',
			border:false
		}
		return com;
	},
	/**
	 * 创建系统名称
	 * @private
	 * @return {}
	 */
	createSysName:function(){
		var com = {
			xtype:'image',
			margin:'29 0 20 5',
			itemId:'sysName',
			src:'../images/main/SysName.png',
			border:false
		}
		return com;
	},
	/**
	 * 创建常用功能
	 * @private
	 * @return {}
	 */
	createModule:function(){
		var com = {
			xtype:'container',
			border:false,
			layout:'column'
		}
		return com;
	},
	/**
	 * 创建功能按钮
	 * @private
	 * @return {}
	 */
	createButtons:function(){
		var me = this;
		
		var com = {
			xtype:'container',
			layout:'column',
			itemId:'buttons',
			border:false,
			padding:'22 0 22 0',
			items:[{
				xtype:'image',
				cls:'main-replaceSkin-45',
				overCls:'main-replaceSkin2-45',
				listeners:{
					click:{
						element:'el',
						fn:function(e,t){
							me.openReplaceSkinWin(e,t);
						}
					},
					mouseover:{
						element:'el',
						fn:function(e,t){
							Ext.create('Ext.tip.ToolTip',{
							    target:t,
							    html:'更换皮肤'
							});
						}
					}
				}
			},{
				xtype:'image',
				cls:'main-lock-45',
				overCls:'main-lock2-45',
				listeners:{
					click:{
						element:'el',
						fn:function(e,t){
							//me.fireEvent('lockclick');
							me.openAccountWin(e,t);
						}
					},
					mouseover:{
						element:'el',
						fn:function(e,t){
							Ext.create('Ext.tip.ToolTip',{
							    target:t,
							    html:'账户设置'
							});
						}
					}
				}
			},{
				xtype:'image',
				cls:'main-config-45',
				overCls:'main-config2-45',
				listeners:{
					click:{
						element:'el',
						fn:function(e,t){
							//me.fireEvent('configclick');
							me.openConfigWin(e,t);
						}
					},
					mouseover:{
						element:'el',
						fn:function(e,t){
							Ext.create('Ext.tip.ToolTip',{
							    target:t,
							    html:'系统设置'
							});
						}
					}
				}
			},{
				xtype:'image',
				cls:'main-close-45',
				overCls:'main-close2-45',
				listeners:{
					click:{
						element:'el',
						fn:function(){
							me.fireEvent('closeclick');
						}
					},
					mouseover:{
						element:'el',
						fn:function(e,t){
							Ext.create('Ext.tip.ToolTip',{
							    target:t,
							    html:'关闭系统'
							});
						}
					}
				}
			}]
		};
		return com;
	},
	/**
	 * 打开皮肤配置
	 * @private
	 */
	openReplaceSkinWin:function(e,t){
		var me = this;
		var menu = me.sysStyleList;
		for(var i in menu){
			menu[i].listeners = {
				click:function(but) {
					//更换系统整体样式
	        		me.changeSysStyle(but.filename);
			    }
			};
		}
		var com = Ext.create('Ext.menu.Menu',{
			items:menu
		}).showAt(e.getXY());
	},
	/**打开账户设置*/
	openAccountWin:function(e,t){
		var me = this;
		var isAdmin = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) == 'admin';
		var menu = [
			{text:'锁定账户',iconCls:'lock-img-16',name:'lock'},
			{text:'切换账户',iconCls:'main-user-16',name:'change'}
		];
		
		if(!isAdmin){
			menu.push({text:'修改密码',iconCls:'button-edit',name:'edit'});
		}
		
		for(var i in menu){
			menu[i].listeners = {
				click:function(but) {
					//更换系统整体样式
	        		me.fireEvent('lockclick',but.name);
			    }
			};
		}
		var com = Ext.create('Ext.menu.Menu',{
			items:menu
		}).showAt(e.getXY());
	},
	/**
	 * 打开系统配置
	 * @param {Object} e
	 * @param {Object} t
	 */
	openConfigWin:function(e,t){
		
	},
	otherMenu:function(){
		var me = this;
	},
	/**
	 * 更换系统整体样式
	 * @private
	 * @param {} value
	 */
	changeSysStyle:function(value){
		var me = this;
		var url = getRootPath()+ '/ui/extjs/resources/css/' + value;
		me.sysStyleUrl = url;
		Ext.util.CSS.swapStyleSheet('theme',url);
		me.fireEvent('changeSysStyle',url);
	},
	//=====================渲染完后处理=======================
	/**
	 * 加载模块
	 * @private
	 */
	loadModules:function(){
		var me = this;
		var modulelist = me.getComponent('modulelist');
		var callback = function(list){
			for(var i in list){
				var module = list[i];
				var com = me.createModuleByInfo(module);
				modulelist.add(com);
			}
			//测试的模块列表
			var testList = [{
				Id:'5594060454194108775',
				CName:'模块管理',
				Comment:'这是模块管理功能模块',
				Url:'../../manage/file/module/modulemanage.html',
				PicFile:'2613a5e7-5490-4385-98bd-00d866e79714.PNG',
				ModuleType:1
			},{
				Id:'4932897323128375563',
				CName:'所有程序管理',
				Comment:'这是所有程序管理功能模块',
				Url:'../../build/file/appList.html',
				PicFile:'list.PNG',
				ModuleType:1
			},{
				Id:'4923283064268053794',
				CName:'定制程序管理',
				Comment:'这是定制程序管理功能模块',
				Url:'../../manage/file/customcode/customCodeList.html',
				PicFile:'list.PNG',
				ModuleType:1,
				disabled:true
			},{
				Id:'5726165943017426833',
				CName:'字典管理',
				Comment:'这是字典管理功能模块',
				Url:'../../manage/file/dictionary/dictionary.html',
				PicFile:'dictionary.PNG',
				ModuleType:1
			}];
			
			//非admin账户没有快捷功能模块
			if(getSystemInfo('UserAccount') != 'admin'){testList=[];}
			
			for(var i in testList){
				var module = me.createModuleByInfo(testList[i]);
				modulelist.add(module);
			}
		}
		var EmployeeID = getCookie('000200');
		if(EmployeeID && EmployeeID != ""){
			me.getModulesFromServer(callback);
		}else{
			callback([]);
		}
	},
	/**
	 * 根据模块信息创建模块图标
	 * @private
	 * @param {} info
	 */
	createModuleByInfo:function(info){
		var me = this;
		var moduleObj = me.moduleObj;
		
		var config = {
			margin:'12 0 12 0',
			text:info[me.moduleObj.text],
			src:getIconRootPathBySize(32)+'/'+info[me.moduleObj.img],
			url:info[me.moduleObj.url],
			comment:info[me.moduleObj.tooltip],
			moduleId:info[me.moduleObj.id],
			img:info[me.moduleObj.img],
			moduletype:info[me.moduleObj.moduletype],
			listeners:{
				click:{
					element:'el',
					fn:function(){
						if(com.cls != 'main-moduleBg2'){
							var app = {
				        		tid:com.moduleId,
				        		text:com.text,
				        		icon:getIconRootPathBySize(16)+'/'+com.img,
				        		url:com.url,
				        		moduletype:com.moduletype
				        	};
				        	me.fireEvent('moduleclick',app);
						}
					}
				}
			}
		};
		if(info.disabled){
			config.cls = 'main-moduleBg2';
		}else{
			config.overCls = 'main-moduleBg';
		}
		var com = Ext.create('Ext.main.ModuleImg',config);
		return com;
	},
	//=====================后台获取&存储=======================
	/**
	 * 从后台获取常用应用列表
	 * @private
	 * @param {} callback
	 */
	getModulesFromServer:function(callback){
		var me = this;
		var par = "isPlanish=true&page=1&start=0&limit=10&fields=RBACModule_Id,RBACModule_CName,RBACModule_Comment,RBACModule_Url,RBACModule_PicFile";
		var EmployeeID = getCookie('000200');
		if(EmployeeID && EmployeeID != ''){par += "&id=" + EmployeeID};
        var url = me.moduleServerUrl + "?" + par;
        var c = function(text){
        	var result = Ext.JSON.decode(text);
        	var list = [];
        	if(result.success){
        		if(result.ResultDataValue && result.ResultDataValue != ''){
            		var data = Ext.JSON.decode(result.ResultDataValue);
            		data && (list = data.List);
            	}
        	}else{
        		alertError("获取常用模块错误:" + result.ErrorInfo);
        	}
        	if(Ext.typeOf(callback) == "function"){callback(list);}
        };
        getToServer(url,c);
	},
	//=========================对外公开方法========================
	/**
	 * 设置LOGO
	 * @public
	 * @param {} logoName
	 */
	setLogo:function(logoName){
		var me = this;
		//var src = getLogoRootPath() + "/" + logoName;
		var src = "../images/main/" + logoName;
		//设置地步信息栏的用户信息
		var logo = me.getComponent('logo');
		if(logo){
			logo.setSrc(src);
		}
	},
	/**
	 * 获取系统风格文件路径
	 * @public
	 * @return {}
	 */
	getSysStyleUrl:function(){
		var me = this;
		var url = me.sysStyleUrl;
		return url;
	}
});