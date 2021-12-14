/**
 * View
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.interface.Viewport', {
	extend: 'Ext.container.Viewport',
	layout: 'fit',
	id: 'SystemViewport',

	/**登录服务地址*/
	loginUrl: '/RBACService.svc/RBAC_BA_Login',

	/**首页内容*/
	SYS_MAIN_INFO: {
		text: '首页',
		tid: 'SYS_MAIN',
		iconCls: 'main-home-img-16',
		url: '#Shell.class.sysbase.main.Home',
		closable: false
	},
	/**小图标根目录*/
	MODULE_ICON_PATH_16: JShell.System.Path.getModuleIconPathBySize(16),

	/**当前账户名*/
	ACCOUNTNAME: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.FunctionTree.on({
			itemclick: function(v, record) {
				var leaf = record.get('leaf');
				if(leaf) {
					me.ContentTab.insertTab(record.data);
				}
			}
		});

		window.VIEWPORT = me;
		//JS文件加载完毕时处理
		JShell.System.afertJSLoading();
		//自动登录
		me.onBeginLogin();
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
			defaultLoad: false,
			collapseMode: 'mini',
			hidden: true,
			listeners: {
				configclick: function(p) {
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

	createDockedItems: function() {
		var me = this;
		var dockedItems = [{
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'TopToolbar',
			items: me.createTopToolbar()
		}, {
			xtype: 'toolbar',
			dock: 'bottom',
			itemId: 'BottomToolbar',
			items: me.createBottomToolbar()
		}];

		return dockedItems;
	},
	/**创建顶部功能栏*/
	createTopToolbar: function() {
		var me = this;

		var items = [{
			xtype: 'image',
			imgCls: 'main-logo-16 hand',
			style: 'margin:5px 2px 5px 5px;',
			listeners: {
				click: {
					element: 'el',
					fn: function(e, t) {
						if(JcallShell.System.LOGO_TO_PAGE) {
							window.open(JcallShell.System.LOGO_TO_PAGE);
						}
					}
				}
			}
		}, {
			xtype: 'label',
			text: JShell.System.Name,
			style: 'color:#04408c;font-weight:bold;font-size:16px;'
		}, {
			xtype: 'toolbar',
			border: false,
			margin: '0 0 5px 10px',
			itemId: 'DailyModuleToolbar',
			items: []
		}, '->', {
			xtype: 'button',
			itemId: 'UserInfo',
			textAlign: 'left',
			iconCls: 'main-user-16',
			hidden: true
		}];

		return items;
	},
	/**创建版本信息栏*/
	createBottomToolbar: function() {
		var me = this;

		var items = [{
			xtype: 'label',
			itemId: 'SysTime',
			style: 'color:rgb(4,64,140);fontWeight:bold;margin:3px 2px',
			text: '' //系统时间：2015-01-01 10:12:14 星期一
		}, '->', {
			xtype: 'label',
			itemId: 'Vesion',
			style: 'color:rgb(4,64,140);fontWeight:bold;margin:3px 2px',
			text: '版本：' + JcallShell.System.JS_VERSION
		}];

		return items;
	},
	getItemCom: function() {
		return this.items.items[0];
	},
	/**设置新系统时间*/
	setSysTime: function(value) {
		var me = this;
		var SysTime = me.getItemCom().getComponent('BottomToolbar').getComponent('SysTime');

		var v = '系统时间：' + (value || '无');
		SysTime.setText(v);
	},
	/**设置版本号*/
	setVersion: function(value) {
		var me = this;
		var Vesion = me.getItemCom().getComponent('BottomToolbar').getComponent('Vesion');

		var v = '版本：' + (value || '无');
		Vesion.setText(v);
	},
	/**登录成功后处理*/
	afterLogin: function() {
		var me = this,
			tree = me.getItemCom().getComponent('FunctionTree');

		//初始化用户信息
		me.initUserInfo();

		me.ContentTab.onCloseAll(true);

		if(JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) == JShell.System.ADMINNAME) {
			tree.getComponent('topToolbar').getComponent('module').show();
		} else {
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
	initSystemInfo: function() {
		var me = this;

		JcallShell.System.Date.init(function() {
			me.onSystimesChange();
		});
	},
	onSystimesChange: function() {
		var me = this;
		var SysTime = JcallShell.Date.toString(JShell.System.Date.getDate(), false, false, true);
		me.setSysTime(SysTime);
		setTimeout(function() {
			me.onSystimesChange();
		}, 1000);
	},
	onFunctionTreeConfigClick: function(p) {
		var me = this;
		me.ContentTab.insertTab({
			tid: 'SYS_MODULE',
			icon: JShell.System.Path.MODULE_ICON_ROOT_16 + '/configuration.PNG',
			text: '模块管理',
			url: '#Shell.class.sysbase.module.App'
		});
	},
	/**初始化常用功能*/
	initDailyModuleToolbar: function() {
		var me = this;

		//		var url = JShell.System.Path.UI + '/config/DailyModule_' + JShell.System.CODE + '.json?t=' + new Date().getTime();
		//		JShell.Server.get(url,function(data){
		//			if(data.success){
		//				me.changeDailyModuleToolbar(data.value.list);
		//			}
		//		});
	},
	/**常用功能变化*/
	changeDailyModuleToolbar: function(data) {
		var me = this,
			TopToolbar = me.getItemCom().getComponent('TopToolbar'),
			DailyModuleToolbar = TopToolbar.getComponent('DailyModuleToolbar'),
			list = data || [],
			items = [];

		DailyModuleToolbar.removeAll();

		list.unshift(me.SYS_MAIN_INFO); //首页功能
		var len = list.length;

		for(var i = 0; i < len; i++) {
			if(list[i].icon) list[i].icon = me.MODULE_ICON_PATH_16 + '/' + list[i].icon;
			items.push({
				text: '<b>' + list[i].text + '</b>',
				icon: list[i].icon,
				iconCls: list[i].iconCls,
				classInfo: list[i],
				handler: function() {
					me.ContentTab.insertTab(this.classInfo);
				}
			}, '-');
		}
		if(items.length > 0) {
			items.unshift('-');
			DailyModuleToolbar.add(items);
		}
	},

	/**错误信息显示*/
	onShowErrorPanel: function(msg) {
		var me = this;

		var html =
			'<div style="text-align:center;padding:20px;">' +
			'<div style="font-size:20px;color:blak;font-weight:bold;padding:20px;">' +
			'提示信息' +
			'</div>' +
			'<div style="color:red;font-weight:bold;padding:20px;">' +
			msg +
			'</div>' +
			'</div>'

		JShell.Win.open('Ext.panel.Panel', {
			title: '提示信息',
			header: false,
			maximizable: false, //是否带最大化功能
			closable: false, //关闭功能
			draggable: false, //移动功能
			resizable: false, //可变大小功能
			width: 400,
			height: 200,
			html: html
		}).show();
	},
	/**显示登录信息面板*/
	onShowLoginMsgPanel: function(msg) {
		var me = this;

		var html =
			'<div style="text-align:center;padding:20px;">' +
			'<div style="font-size:20px;color:blak;font-weight:bold;padding:20px;">' +
			'提示信息' +
			'</div>' +
			'<div style="color:green;font-weight:bold;padding:20px;">' +
			'登录中...' +
			'</div>' +
			'</div>'

		me.LoginMsgPanel = JShell.Win.open('Ext.panel.Panel', {
			title: '提示信息',
			header: false,
			maximizable: false, //是否带最大化功能
			closable: false, //关闭功能
			draggable: false, //移动功能
			resizable: false, //可变大小功能
			width: 400,
			height: 200,
			html: html
		}).show();
	},
	/**关闭登录信息面板*/
	onCloseLoginMsgPanel: function() {
		this.LoginMsgPanel.close();
	},

	/**自动登录*/
	onBeginLogin: function() {
		var me = this,
			params = JShell.Page.getParams(true);

		if(!params.ACCOUNT || !params.PASSWORD) {
			me.onShowErrorPanel('请传递账号、密码参数：ACCOUNT、PASSWORD');
		} else {
			me.onLogin(params.ACCOUNT, params.PASSWORD, function(data) {
				me.afterLogin();
			});
		}
	},
	onLogin: function(account, password, callback) {
		var me = this;

		if(!JShell.System.ADMIN_CAN_LOGIN && account == JShell.System.ADMINNAME) {
			me.onShowErrorPanel('此账号不能登录！');
			return;
		}

		var url = JShell.System.Path.ROOT + me.loginUrl +
			'?strUserAccount=' + account +
			'&strPassWord=' + password;

		me.onShowLoginMsgPanel(); //显示登录信息面板

		var AllCookie = JShell.System.Cookie.getAllCookie();
		//清理所有cookie
		JShell.System.Cookie.clearCookie();

		JShell.Server.get(url, function(data) {
			me.onCloseLoginMsgPanel(); //关闭登录信息面板
			if(data == 'true') {
				if(!JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID)) {
					JShell.System.Cookie.set({
						name: JShell.System.Cookie.map.DEPTCODE,
						value: ''
					});
				}
				if(Ext.typeOf(JShell.System.onAfterLogin) == 'function') {
					JShell.System.onAfterLogin();
				}
				callback();
			} else {
				for(var i = 0; i < AllCookie.length; i++) {
					JShell.System.Cookie.set(AllCookie[i][0], AllCookie[i][1]);
				}

				me.onShowErrorPanel('登录失败！</br>【账号：' + account + '】【密码：' + password + '】');
			}
		}, true, null, true);
	},
	/**初始化用户信息*/
	initUserInfo: function(value) {
		var me = this;
		var UserInfo = me.getItemCom().getComponent('TopToolbar').getComponent('UserInfo');

		var ACCOUNTNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) || '无';
		var DEPTNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || '无';
		var USERNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME) || '无';

		var text = '当前用户：' + ACCOUNTNAME + '【' + USERNAME + '】';
		UserInfo.setText(text);
		var tooltip = '用户账号：' + ACCOUNTNAME + '</br>用户名称：' + USERNAME + '</br>所属机构：' + DEPTNAME;
		UserInfo.setTooltip(tooltip);

		UserInfo.show();
	}
});