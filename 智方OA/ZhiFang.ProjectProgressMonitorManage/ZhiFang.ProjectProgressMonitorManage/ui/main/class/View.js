/**
 * 控制台
 */
Ext.ns('Ext.main');
Ext.define('Ext.main.View',{
	extend:'Ext.container.Viewport',
	alias:'widget.view',
	//===================可配置参数====================
	loginPanel:null,
	mainPanel:null,
	/**
	 * 渲染完后处理
	 * @private
	 */
	afterRender:function(){  
		var me = this;
		me.callParent(arguments);
		//禁止使用F5刷新
		var keyMap=new Ext.KeyMap(Ext.getBody(),{  
			key:Ext.EventObject.F5,
			fn:function(){},
			scope:this,
			defaultEventAction:"stopEvent"
		});
		keyMap.enable();
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initComponent:function(){
		var me = this;
		//初始化参数
		me.initParams();
		//初始化视图内容
		me.initView();
		me.callParent(arguments);
	},
	/**
	 * 初始化参数
	 * @private
	 */
	initParams:function(){
		var me = this;
		me.layout = "fit";
		var expires = getCookieDate();//cookie失效时间
		Ext.util.Cookies.set('000500',cookie['100001'],expires);//默认模块ID
	},
	/**
	 * 初始化视图内容
	 * @private
	 */
	initView:function(){
		var me = this;
		me.items = me.createItems();
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		Ext.util.Cookies.set('100002',"false");
		var login = me.createLogin();
		me.loginPanel = login;
		
		var items = [login];
		return items;
	},
	createLogin:function(){
		var me = this;
		var login = Ext.create('Ext.main.Login',{
			itemId:'login',title:'',hasCancel:false,border:false,bodyStyle:'background:#1992ca;',
			listeners:{
				login:function(){
					if(!me.isTheSameUser()){
						var expires = getCookieDate();//cookie失效时间
						Ext.util.Cookies.set('100004',"",expires);//清空历史打开记录
						me.setOldUserIdToCookie();//记录历史用户
					}
					
					//锁定标记
					var isLocked = getCookie('100002');
					if(isLocked == "true"){
						me.mainPanel.setUserName(me.getCookieUserName());
						me.mainPanel.loadTree();
						me.showMain();
					}else{
						me.getComponent('login').hide();
						var main = me.createMain();
						main.setUserName(me.getCookieUserName());
						me.mainPanel = main;
						me.add(main);
					}
				},
				cancel:function(){
					me.showMain();
				}
			}
		});
		
		var panel = Ext.create('Ext.panel.Panel',{
			itemId:'login',
			autoScroll:true,
			border:false,
			layout:{
				type:'vbox',
				pack:'center'
			},
			bodyStyle:'background:#1992ca;',
			//html:'<img src="../image/login/bgImg.jpg"/>',
			items:[{
				xtype:'panel',
				itemId:'login',
				width:'100%',
				border:false,
				bodyStyle:'background:#1992ca;',
				layout:{
					type:'hbox',
					align:'middle',
					pack:'center'
				},
				items:login
			}]
		});
		return panel;
	},
	/**
	 * 创建首页面板
	 * @private
	 * @return {}
	 */
	createMain:function(){
		var me = this;
		var main = Ext.create('Ext.main.Main',{
			itemId:'main',border:false,
			listeners:{
				//点击锁定用户按钮
				lockclick:function(name){
					me.openLockWin2(name);
				},
				//点击退出系统按钮
				closeclick:function(){
					//做退出相应处理
					me.closeMainWin();
				}
			}
		});
		return main;
	},
	/**
	 * 弹出锁定窗口
	 * @private
	 */
	openLockWin2:function(name){
		var me = this;
		
		switch(name){
			case 'lock' : 
				me.showLockLoginWin();
				break;
			case 'change' : 
				me.showLockLoginWin(true);
				break;
			case 'edit' : 
				JShell.Win.open('Shell.class.sysbase.user.AccountPwd', {
					resizable: false,
					listeners:{
						save:function(p){
							p.close();
							me.showLockLoginWin();
						}
					}
				}).show();
				break;
		}
	},
	/**显示账户锁定/切换页面*/
	showLockLoginWin:function(isChange){
		var me = this;
		
		var login = me.getLoginWin();
		login.showOk();
		
		if(!isChange){
			Ext.util.Cookies.set('100002',"true");
			login.hideCancel();
		}else{
			login.showCancel();
		}
		
		login.clearPassWord();//清空密码栏
	    me.showLogin();
	},
	/**
	 * 弹出锁定窗口
	 * @private
	 */
	openLockWin:function(){
		var me = this;
		var isAdmin = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) == 'admin';
		var lockWin = Ext.create('Ext.main.LockPanel',{
			modal:true,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			draggable:true,//可移动
			showPwd:!isAdmin,
			userAccount:getCookie('000301'),//当前用户
			listeners:{
				//锁定用户
				lockclick:function(){
					Ext.util.Cookies.set('100002',"true");
					var login = me.getLoginWin();
					login.showOk();
					login.hideCancel();
					login.clearPassWord();//清空密码栏
				    lockWin.hide();
				    me.showLogin();
				},
				//切换用户
				changeclick:function(){
					var login = me.getLoginWin();
					login.showOk();
					login.showCancel();
					login.clearPassWord();//清空密码栏
					lockWin.hide();
					me.showLogin();
				},
				afterrender:function(com){
					com.getComponent('change').focus(false,200);
				}
			}
		});
		lockWin.show();
	},
	/**
	 * 显示登录面板
	 * @private
	 */
	showLogin:function(){
		var me = this;
		me.mainPanel && me.mainPanel.hide();
		me.loginPanel && me.loginPanel.show();
	},
	/**
	 * 显示首页面板
	 * @private
	 */
	showMain:function(){
		var me = this;
		me.loginPanel && me.loginPanel.hide();
		me.mainPanel && me.mainPanel.show();
	},
	/**
	 * 获取登录界面
	 * @private
	 * @return {}
	 */
	getLoginWin:function(){
		var me = this;
		var login = me.getComponent('login').getComponent('login').getComponent('login');
		return login;
	},
	//=========================公共方法========================
	/**
	 * 获取cookie中的用户名称
	 * @private
	 * @return {}
	 */
	getCookieUserName:function(){
		//var EmployeeName = getSystemInfo('EmployeeName');//员工姓名
		//return EmployeeName;
		
		var SYS_USER_NAME = Shell.util.SysInfo.getSYS_USER_NAME();
		var SYS_USER_ORG_NAME = Shell.util.SysInfo.getSYS_USER_ORG_NAME() || "没有部门";
		
		return SYS_USER_NAME + " ( " + SYS_USER_ORG_NAME + " ) ";
	},
	/**
	 * 判断当前用书是否是最后一个历史用户
	 * @private
	 * @return {}
	 */
	isTheSameUser:function(){
		var bo = false;
		var nowUserAccount = getCookie('000301');//当前账户名
		var oldUserAccount = getCookie('100003');//最后一个历史账户名
		if(nowUserAccount && oldUserAccount && nowUserAccount != "" && nowUserAccount == oldUserAccount){//当前用户就是最后一个历史用户
			bo = true;
		}
		return bo;
	},
	/**
	 * 记录历史用户
	 * @private
	 */
	setOldUserIdToCookie:function(){
		//把当前用户最为最后一个历史用户保存到永久cookie中
	    var nowUserAccount = getCookie('000301');//当前账户名
	    var expires = getCookieDate();//cookie失效时间
		Ext.util.Cookies.set('100003',nowUserAccount,expires);
	},
	closeMainWin:function(){
		//清理cookie中的部分数据
		//alert("清理cookie中的部分数据!");
		//通知服务器已离线
		//alert("通知服务器已离线");
		//关闭浏览器
		ExitClose();
		window.close();
	}
});