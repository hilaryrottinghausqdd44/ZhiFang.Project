/**
 * View
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.interface.Viewport', {
	extend: 'Ext.container.Viewport',
	layout: 'fit',
	id:'SystemViewport',
	
	/**登录服务地址*/
    loginUrl:'/RBACService.svc/RBAC_BA_Login',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		window.VIEWPORT = me;
		//JS文件加载完毕时处理
		JShell.System.afertJSLoading();
		//自动登录
		me.onBeginLogin();
	},
	
	initComponent: function() {
		var me = this;
		me.addEvents('login');
		me.items = [];
		me.callParent(arguments);
	},
	
	/**错误信息显示*/
	onShowErrorPanel:function(msg){
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
		
		JShell.Win.open('Ext.panel.Panel',{
			title:'提示信息',
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
	/**显示登录信息面板*/
	onShowLoginMsgPanel:function(msg){
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
		
		me.LoginMsgPanel = JShell.Win.open('Ext.panel.Panel',{
			title:'提示信息',
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
	/**关闭登录信息面板*/
	onCloseLoginMsgPanel:function(){
		this.LoginMsgPanel.close();
	},
	
	/**自动登录*/
	onBeginLogin:function(){
		var me = this,
			params = JShell.Page.getParams(true);
			
		if(!params.ACCOUNT || !params.PASSWORD){
			me.onShowErrorPanel('请传递账号、密码参数：ACCOUNT、PASSWORD');
		}else{
			me.onLogin(params.ACCOUNT,params.PASSWORD,function(data){
				me.fireEvent('login',me);
			});
		}
	},
	onLogin:function(account,password,callback){
		var me = this;
		
		if(!JShell.System.ADMIN_CAN_LOGIN && account == JShell.System.ADMINNAME){
			me.onShowErrorPanel('此账号不能登录！');
			return;
		}
		
		var url = JShell.System.Path.ROOT + me.loginUrl + 
			'?strUserAccount=' + account + '&strPassWord=' + password;
			
		me.onShowLoginMsgPanel();//显示登录信息面板
		
		JShell.Server.get(url,function(data){
			me.onCloseLoginMsgPanel();//关闭登录信息面板
			if(data == 'true'){
				if(!JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID)){
					JShell.System.Cookie.set({
						name:JShell.System.Cookie.map.DEPTCODE,
						value:''
					});
				}
				if(Ext.typeOf(JShell.System.onAfterLogin) == 'function'){
					JShell.System.onAfterLogin(callback);
				}
			}else{
				me.onShowErrorPanel('登录失败！</br>【账号：' + account + '】【密码：' + password + '】');
			}
		},true,null,true);
	}
});