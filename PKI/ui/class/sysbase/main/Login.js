/**
 * 登录界面
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.Login',{
    extend:'Shell.ux.form.Panel',
	
	title:'登录',
	width:240,
	height:140,
	formtype: 'add',
	
	/**登录服务地址*/
    loginUrl:'/RBACService.svc/RBAC_BA_Login',
	
	bodyPadding:'15px 10px',
	/**布局方式*/
	layout:'anchor',
    /** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:40,
        labelAlign:'right',
        allowBlank:false
    },
    /**光标定位延时*/
    focusTimes:200,
	
	afterRender:function(){
		var me = this ;
		me.addEvents('login');
		me.callParent(arguments);
		
		me.initListeners();
		
		if(me.account){
			me.getComponent('Account').setValue(me.account);
			JShell.Action.delay(function(){me.getComponent('Pwd').focus();},null,me.focusTimes);
		}else{
			me.onAccountFocus(me.focusTimes * 2);
		}
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var items = [{
			fieldLabel:'账号',
			emptyText:'请输入账号',
			itemId:'Account',
			name:'Account'
		},{
			fieldLabel:'密码',
			emptyText:'请输入密码',
			itemId:'Pwd',
			name:'Pwd',
			inputType:'password'
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
		var url = JShell.System.Path.ROOT + me.loginUrl + 
			'?strUserAccount=' + Account + 
			'&strPassWord=' + Pwd;
			
		me.onMsgChange();
		me.showMask('登录中...');//显示遮罩层
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩层
			if(data == 'true'){
				if(!JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID)){
					JShell.System.Cookie.set({
						name:JShell.System.Cookie.map.DEPTCODE,
						value:''
					});
				}
				if(JShell.typeOf(JShell.System.onAfterLogin) == 'function'){
					JShell.System.onAfterLogin();
				}
				me.fireEvent('login',me);
			}else{
				me.onMsgChange('登录失败！');
				me.onAccountFocus(me.focusTimes);
			}
		},true,null,true);
	},
	onAccountFocus:function(times){
		var me = this;
		JShell.Action.delay(function(){me.getComponent('Account').focus();},null,times);
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
	