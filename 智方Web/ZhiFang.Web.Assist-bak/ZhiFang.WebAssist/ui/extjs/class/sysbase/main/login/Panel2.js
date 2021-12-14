/**
 * 登录界面2
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.login.Panel2',{
    extend:'Shell.ux.form.Panel',
	
	title:'登录',
	width:492,
	height:260,
	formtype: 'add',
	
	/**登录服务地址*/
    loginUrl:'/RBACService.svc/RBAC_BA_Login',
    
    /** 每个组件的默认属性*/
    defaults:{
    	width:200,
        labelWidth:50,
        labelAlign:'right',
        allowBlank:false
    },
    /**内容自动显示*/
	autoScroll:false,
    /**光标定位延时*/
    focusTimes:200,
    
    /**是否锁定账号*/
	isLocked:false,
    /**默认账号*/
	account:null,
	
	afterRender:function(){
		var me = this ;
		me.addEvents('login');
		me.callParent(arguments);
		
		me.initListeners();
		
		me.on({
			show:function(){
				if(me.account){
					setTimeout(function(){me.getComponent('Pwd').focus();},me.focusTimes);
				}else{
					me.onAccountFocus(me.focusTimes);
				}
			}
		});
		
		//JS文件加载完毕时处理
		JShell.System.afertJSLoading();
	},
	initComponent:function(){
		var me = this;
		me.icon = JShell.System.Path.UI + '/css/images/system/logo-16.png';
		me.title = JShell.System.Name + '-' + me.title;
		me.items = me.createItems();
		
		var LogoSrc = JShell.System.Path.UI + JShell.System.LoginTopImage + '?v=' + JShell.System.JS_VERSION;
		me.dockedItems = [{
			xtype:'toolbar',
			dock:'top',
			height:156,
			border:false,
			padding:0,
			items:[{
				xtype:'panel',
				border:false,
				width:492,height:156,
				html:'<img style="width:492px;height:156px;" src="' + LogoSrc + '">'
			}]
		}];
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [{
			x:20,y:15,
			fieldLabel:' 账 号 ',
			emptyText:'请输入账号',
			itemId:'Account',
			name: 'Account',
		    value:me.account || '',
		    readOnly:me.isLocked,
		    locked:me.isLocked
		},{
			x:220,y:15,
			fieldLabel:' 密 码 ',
			emptyText:'请输入密码',
			itemId:'Pwd',
			name:'Pwd',
			inputType: 'password',
		    value:''
		}];
		
		return items;
	},
	/**创建挂靠功能栏*/
	createButtonToolbarItems:function(){
		var me = this,
			items = [{
				xtype:'label',
				itemId:'Msg',
				style: 'margin:0 10px;color:red;font-weight:bold;'
			},'->','login'];
			
		if(me.closable){
			items.push('cancel');
		}
		
		return items;
	},
	initListeners:function(){
		var me = this;
		var Account = me.getComponent('Account');
		var Pwd = me.getComponent('Pwd');
		
		new Ext.KeyMap(Account.getEl(),[{
	      	key:Ext.EventObject.ENTER,
	      	fn:function(){
	       		me.onLoginClick();
	      	}
     	}]);
     	new Ext.KeyMap(Pwd.getEl(),[{
	      	key:Ext.EventObject.ENTER,
	      	fn:function(){
	       		me.onLoginClick();
	      	}
     	}]);
	},
	onLoginClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()){
			me.onAccountFocus(me.focusTimes);
			return;
		}
		
		var values = me.getForm().getValues();
		
		me.onLogin(values.Account,values.Pwd);
	},
	onLogin:function(Account,Pwd){
		var me = this;
		
		if(!JShell.System.ADMIN_CAN_LOGIN && Account == JShell.System.ADMINNAME){
			JShell.Msg.error('此账号不能登录！');
			return;
		}
		
		var url = JShell.System.Path.ROOT + me.loginUrl + 
			'?strUserAccount=' + Account + 
			'&strPassWord=' + Pwd;
			
		me.onMsgChange();
		me.showMask('登录中...');//显示遮罩层
		
		var AllCookie = JShell.System.Cookie.getAllCookie();
		//清理所有cookie
		JShell.System.Cookie.clearCookie();
		
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩层
			if(data == 'true'){
				JShell.LocalStorage.set("account",Account);
				JShell.LocalStorage.set("password",Pwd);
				
				if(!JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID)){
					JShell.System.Cookie.set({
						name:JShell.System.Cookie.map.DEPTCODE,
						value:''
					});
				}
				if(Ext.typeOf(JShell.System.onAfterLogin) == 'function'){
					JShell.System.onAfterLogin();
				}
				me.fireEvent('login',me);
			}else{
				for(var i=0;i<AllCookie.length;i++){
					JShell.System.Cookie.set(AllCookie[i][0],AllCookie[i][1]);
				}
				
				me.onMsgChange('登录失败！');
				me.onAccountFocus(me.focusTimes);
			}
		},true,null,true);
	},
	onAccountFocus:function(times){
		var me = this;
		setTimeout(function(){me.getComponent('Account').focus();},times);
	},
	onMsgChange:function(value){
		var me = this,
			Msg = me.getComponent('buttonsToolbar').getComponent('Msg');
			
		Msg.setText(value || '');
	},
	/**更改标题*/
	changeTitle:function(){
		
	}
});
	