/**
 * View
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.Viewport', {
	extend: 'Ext.container.Viewport',
	layout: 'fit',
	id:'SystemViewport',
	
	/**是否默认开启全屏模式*/
	isLaunchFullscreen:false,
	
	/**首页内容*/
	SYS_MAIN_INFO:{
		text:'首页',
		tid:'SYS_MAIN',
		iconCls:'main-home-img-16',
		url:'#Shell.class.sysbase.main.Home',
		closable:false
	},
	/**小图标根目录*/
	MODULE_ICON_PATH_16:JShell.System.Path.getModuleIconPathBySize(16),
	
	/**当前账户名*/
	ACCOUNTNAME:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.FunctionTree.on({
			itemclick: function(v, record) {
				var leaf = record.get('leaf');
				if (leaf) {
					me.ContentTab.insertTab(record.data);
				}
			}
		});
		//功能区域的页签变化，同步到左侧功能树的选择
		me.ContentTab.on({
    		tabchange:function(tabPanel,newCard,oldCard,eOpts){
				var moduleId = newCard.itemId.split('_').slice(-1);
				if(moduleId == 'MAIN' || moduleId.length != 19){
					me.FunctionTree.getSelectionModel().deselectAll();
					return;
				}
				
				me.FunctionTree.selectNode(moduleId);
			}
    	});
		//me.initInfo();
		me.showLoginWin(true);
		
		window.VIEWPORT = me;
		
//		//是否默认开启全屏模式
//		if(me.isLaunchFullscreen){
//			me.launchFullscreen();
//		}else{
//			me.exitFullscreen();
//		}
	},

	initComponent: function() {
		var me = this;
		
		me.FunctionTree = Ext.create('Shell.class.sysbase.main.FunctionTree', {
			region: 'west',
			width: 200,
			header: false,
			title: '功能树',
			itemId: 'FunctionTree',
			split: true,
			collapsible: true,
			defaultLoad:false,
			collapseMode:'mini',
			hidden:true,
			listeners:{
				configclick:function(p){
					me.onFunctionTreeConfigClick(p);
				}
			}
		});
		me.ContentTab = Ext.create('Shell.class.sysbase.main.ContentTab', {
			region: 'center',
			header: false,
			title: '功能区域',
			itemId: 'ContentTab',
			split: true,
			collapsible: true
		});
		me.items = [{
			itemId: 'view',
			bodyPadding: 1,
			layout: 'border',
			dockedItems: me.createDockedItems(),
			items: [me.FunctionTree, me.ContentTab]
		}];

		me.callParent(arguments);
	},
	createDockedItems:function(){
		var me = this;
		var dockedItems = [{
			xtype:'toolbar',
			dock:'top',
			itemId:'TopToolbar',
			//padding:'5px 0',
			items:me.createTopToolbar()
		},{
			xtype:'toolbar',
			dock:'bottom',
			itemId:'BottomToolbar',
			items:me.createBottomToolbar()
		}];
		
		return dockedItems;
	},
	/**创建顶部功能栏*/
	createTopToolbar: function() {
		var me = this;

		var items = [{
			xtype:'image',
			imgCls:'main-logo-16 hand',
			style: 'margin:5px 2px 5px 5px;',
			listeners:{
				click:{
					element:'el',
					fn:function(e,t){
						if(JcallShell.System.LOGO_TO_PAGE){
							window.open(JcallShell.System.LOGO_TO_PAGE);
						}
					}
				}
			}
		},{
			xtype: 'label',
			text: JShell.System.Name,
			//style: 'margin:5px 10px;color:#04408c;font-weight:bold;font-size:16px;'
			style: 'color:#04408c;font-weight:bold;font-size:16px;'
		},{
			xtype:'toolbar',
			border:false,
			margin:'0 0 5px 10px',
			itemId:'DailyModuleToolbar',
			items:[]
		},'->'];
		
		//开启全屏、关闭全屏
		items.push({
			text:'开启全屏',
			itemId:'launchFullscreen',
			iconCls:'button-arrow-out',
			hidden:me.isLaunchFullscreen,
			handler:function(){
				me.launchFullscreen();
			}
		},{
			text:'关闭全屏',
			itemId:'exitFullscreen',
			iconCls:'button-arrow-in',
			hidden:!me.isLaunchFullscreen,
			handler:function(){
				me.exitFullscreen();
			}
		});
		
		items.push({
			text:'登录',
			itemId:'Login',
			iconCls:'button-login',
			handler:function(){
				me.showLoginWin();
			}
		});
		
		items.push({
			xtype:'splitbutton',
			itemId:'UserInfo',
            textAlign: 'left',
			iconCls:'main-user-16',
			hidden:true,
			handler:function(btn,e){
   				btn.overMenuTrigger = true;
   				btn.onClick(e);
			},
			menu:[{
				text: '锁定账户',
				iconCls:'main-lock-16',
				name:'lock',
				listeners:{
					click: function(but) {
						me.LockAccount();
					}
				}
			},{
				text: '切换账户',
				iconCls:'button-login',
				name:'change',
				listeners:{
					click: function(but) {
						me.onChangeAccount();
					}
				}
			},{
				text:'修改密码',
				iconCls:'button-edit',
				name:'edit',
				listeners:{
					click: function(but) {
						me.onEditPwd();
					}
				}
			}]
		});
		
		items.push({
			text:'帮助',
			itemId:'help',
			iconCls:'button-help',
			tooltip:'功能帮助说明',
			handler:function(){
				me.onHelpClick();
			}
		},{
			text:'交流',
			itemId:'interact',
			iconCls:'button-interact',
			tooltip:'功能交流',
			handler:function(){
				me.onInteractionClick();
			}
		});
		
		return items;
	},
	/**创建版本信息栏*/
	createBottomToolbar:function(){
		var me = this;

		var items = [{
			xtype:'label',
			itemId:'SysTime',
			style:'color:rgb(4,64,140);fontWeight:bold;margin:3px 2px',
			text:''//系统时间：2015-01-01 10:12:14 星期一
		}, '->',{
			xtype:'label',
			itemId:'Vesion',
			style:'color:rgb(4,64,140);fontWeight:bold;margin:3px 2px',
			text:'版本：' + JcallShell.System.JS_VERSION
		}];
		
		return items;
	},
	getItemCom:function(){
		return this.items.items[0];
	},
	/**初始化用户信息*/
	initUserInfo:function(value){
		var me = this;
		var UserInfo = me.getItemCom().getComponent('TopToolbar').getComponent('UserInfo');
		
		var ACCOUNTNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) || '无';
		var DEPTNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || '无';
		var USERNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME) || '无';
		
		var text = '当前用户：' + ACCOUNTNAME + '【' + USERNAME + '】';
		UserInfo.setText(text);
		var tooltip = '用户账号：' + ACCOUNTNAME + '</br>用户名称：' + USERNAME + '</br>所属机构：' + DEPTNAME;
		UserInfo.setTooltip(tooltip);
	},
	/**设置新系统时间*/
	setSysTime:function(value){
		var me = this;
		var SysTime = me.getItemCom().getComponent('BottomToolbar').getComponent('SysTime');
		
		var v = '系统时间：' + (value || '无');
		SysTime.setText(v);
	},
	/**设置版本号*/
	setVersion:function(value){
		var me = this;
		var Vesion = me.getItemCom().getComponent('BottomToolbar').getComponent('Vesion');
		
		var v = '版本：' + (value || '无');
		Vesion.setText(v);
	},
	/**显示登录面板*/
	showLoginWin:function(noCloseBtn,account){
		var me = this;
		JShell.Win.open('Shell.class.sysbase.main.Login',{
			formtype:'add',
			resizable: false,
			maximizable:false,//是否带最大化功能
			closable:!noCloseBtn,
			isLocked:account ? true : false,
			account:account,
			listeners:{
				login:function(p){
					p.close();
					if(account) return;
					me.afterLogin();
				}
			}
		}).show();
	},
	/**登录成功后处理*/
	afterLogin:function(){
		var me = this,
			Login = me.getItemCom().getComponent('TopToolbar').getComponent('Login'),
			UserInfo = me.getItemCom().getComponent('TopToolbar').getComponent('UserInfo'),
			tree = me.getItemCom().getComponent('FunctionTree');
		
		//初始化系统信息
		me.initSystemInfo();
		//初始化用户信息
		me.initUserInfo();
		
		Login.hide();
		UserInfo.show();
		
		me.ContentTab.onCloseAll(true);
		
		if(JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) == JShell.System.ADMINNAME){
			tree.getComponent('topToolbar').getComponent('module').show();
		}else{
			tree.getComponent('topToolbar').getComponent('module').hide();
		}
		
		tree.show();
		tree.load();
		
		//挂载首页
		me.ContentTab.insertTab(me.SYS_MAIN_INFO);
		//初始化常用功能
		me.initDailyModuleToolbar();
	},
	/**初始化系统信息*/
	initSystemInfo:function(){
		var me = this;
		
		//登录时的本地时间毫秒数
		var datetimes = new Date().getTime();
		JShell.System.LOGIN_DATE_TIMES = datetimes;
		JShell.System.Cookie.set({
			name:JShell.System.Cookie.map.LOGINDATETIMES,
			value:datetimes
		});
		//当前账户名
		me.ACCOUNTNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME);
		
		JcallShell.System.Date.init(function(){
			me.onSystimesChange();
		});
	},
	LockAccount:function(){
		var me = this;
		var account = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME);
		me.showLoginWin(true,account);
	},
	onChangeAccount:function(){
		this.showLoginWin();
	},
	onEditPwd:function(){
		var me = this;
		JShell.Win.open('Shell.class.sysbase.user.account.EditPwd', {
			resizable: false,
			maximizable:false,//是否带最大化功能
			listeners:{
				save:function(p){
					p.close();
					me.showLoginWin(true);
				}
			}
		}).show();
	},
	onSystimesChange:function(){
		var me = this;
		var SysTime = JcallShell.Date.toString(JShell.System.Date.getDate(),false,false,true);
		me.setSysTime(SysTime);
		
		var timeout = setTimeout(function(){
			var isValid = me.onAccountValid();
			if(!isValid){
				clearTimeout(timeout);
				me.onShowErrorPanel();//错误面板
			}else{
				me.onSystimesChange();
			}
		},1000);
	},
	/**账号有效性*/
	onAccountValid:function(){
		var me = this,
			account = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME),
			datetimes = JShell.System.Cookie.get(JShell.System.Cookie.map.LOGINDATETIMES);
		
		//在同一个浏览器中，同一个域内的相同账号可以再次打开，不同账号打开时，前面打开的页面将无效
		if(me.ACCOUNTNAME == account){
			isValid = true;
		}else{
			isValid = JShell.System.LOGIN_DATE_TIMES == datetimes;
		}
		
		return isValid;
	},
	onShowErrorPanel:function(){
		var me = this;
		
		var html = 
		'<div style="text-align:center;padding:20px;">' +
			'<div style="font-size:20px;color:blak;font-weight:bold;padding:20px;">' +
				'错误信息' +
			'</div>' +
			'<div style="color:red;font-weight:bold;padding:20px;">' +
				'其他账号已在本浏览器登录，请重新登录' +
			'</div>' +
		'</div>'
		
		JShell.Win.open('Ext.panel.Panel',{
			title:'错误提示',
			header:false,
			maximizable:false,//是否带最大化功能
			closable:false,//关闭功能
			draggable:false,//移动功能
			resizable:false,//可变大小功能
			width:400,
			height:200,
			html:html
		}).show();
	},
	onFunctionTreeConfigClick:function(p){
		var me = this;
		me.ContentTab.insertTab({
			tid:'SYS_MODULE',
			icon:JShell.System.Path.MODULE_ICON_ROOT_16 + '/configuration.PNG',
			text:'模块管理',
			url:'#Shell.class.sysbase.module.App'
		});
	},
	
	/**开启全屏模式*/
	launchFullscreen:function(){
		var me = this,
			TopToolbar = me.getItemCom().getComponent('TopToolbar'),
			launchFullscreen = TopToolbar.getComponent('launchFullscreen'),
			exitFullscreen = TopToolbar.getComponent('exitFullscreen');
		
		JShell.Win.frame.launchFullscreen();
		launchFullscreen.hide();
		exitFullscreen.show();
	},
	/**关闭全屏模式*/
	exitFullscreen:function(){
		var me = this,
			TopToolbar = me.getItemCom().getComponent('TopToolbar'),
			launchFullscreen = TopToolbar.getComponent('launchFullscreen'),
			exitFullscreen = TopToolbar.getComponent('exitFullscreen');
		
		JShell.Win.frame.exitFullscreen();
		exitFullscreen.hide();
		launchFullscreen.show();
	},
	/**初始化常用功能*/
	initDailyModuleToolbar:function(){
		var me = this;
			
		var url = JShell.System.Path.UI + '/config/DailyModule_' + JShell.System.CODE + '.json?t=' + new Date().getTime();
		JShell.Server.get(url,function(data){
			if(data.success){
				me.changeDailyModuleToolbar(data.value.list);
			}
		});
	},
	/**常用功能变化*/
	changeDailyModuleToolbar:function(data){
		var me = this,
			TopToolbar = me.getItemCom().getComponent('TopToolbar'),
			DailyModuleToolbar = TopToolbar.getComponent('DailyModuleToolbar'),
			list = data || [],
			items = [];
			
		DailyModuleToolbar.removeAll();
		
		list.unshift(me.SYS_MAIN_INFO);//首页功能
		var len = list.length;
		
		for(var i=0;i<len;i++){
			if(list[i].icon) list[i].icon = me.MODULE_ICON_PATH_16 + '/' + list[i].icon;
			items.push({
				text:'<b>' + list[i].text + '</b>',
				icon:list[i].icon,
				iconCls:list[i].iconCls,
				classInfo:list[i],
				handler:function(){
					me.ContentTab.insertTab(this.classInfo);
				}
			},'-');
		}
		if(items.length > 0){
			items.unshift('-');
			DailyModuleToolbar.add(items);
		}
	},
	
	/**帮助功能按钮点击处理*/
	onHelpClick:function(){
		var me = this,
			tabId = me.ContentTab.getId(),
			checkedModule = me.ContentTab.getActiveTab();
			
		if(!checkedModule){
			JShell.Msg.error('没有打开的功能！');
			return;
		}
		
		var checkedModuleId = checkedModule.itemId;
		var array = checkedModuleId.split(tabId + '_');
		
		if(array.length != 2){
			JShell.Msg.error('打开的功能编号错误！，编号：' + checkedModuleId);
			return;
		}
		
		var id = array[1];
		
		JShell.Win.open('Shell.class.qms.file.help.show.Panel',{
			//resizable: false,
			title:'帮助信息',
			ModuleId:id
		}).show();
	},
	/**功能交流*/
	onInteractionClick:function(){
		var me = this,
			tabId = me.ContentTab.getId(),
			checkedModule = me.ContentTab.getActiveTab();
			
		if(!checkedModule){
			JShell.Msg.error('没有打开的功能！');
			return;
		}
		
		var checkedModuleId = checkedModule.itemId;
		var array = checkedModuleId.split(tabId + '_');
		
		if(array.length != 2){
			JShell.Msg.alert('打开的功能编号错误！，编号：' + checkedModuleId);
			return;
		}
		
		var id = array[1] + '';
		if(id.length != 19){
			JShell.Msg.alert('打开的功能编号错误！功能GUID码：' + id);
			return;
		}
		
		JShell.Win.open('Shell.class.sysbase.scinteraction.AppExt',{
			//resizable: false,
			title:'功能交流',
			PK:id
		}).show();
	}
});