/**
 * 登录界面
 * @author jing
 * @version 2018-09-17
 */
Ext.define('Shell.class.opd.userSign.app.App', {
    extend:'Ext.form.Panel',
	
	title:'登录',
	width:480,
	height:280,
	formtype: 'add',
    Account:'',
	/**登录服务地址*/
	loginUrl: '/ServiceWCF/DictionaryService.svc/EmpDeptLinksUserLogin',
	
	bodyPadding:'15px 100px',
	/**布局方式*/
	layout: 'absolute',
    /**光标定位延时*/
    focusTimes:200,
	
	afterRender:function(){
		var me = this ;
		me.addEvents('login');
		me.callParent(arguments);
		me.initListeners();
	},
	initComponent:function(){
		var me = this;
		me.items = me.applyItems();
		
		me.icon = Shell.util.Path.uiPath + '/images/login/logo-16.png';
		me.title = "登录";
		var LogoSrc = Shell.util.Path.uiPath + '/images/login/LoginTop.jpg';
		me.dockedItems = [{
		    xtype: 'toolbar',
		    dock: 'top',
		    height: 156,
		    border: false,
		    padding: 0,
		    items: [{
		        xtype: 'panel',
		        border: false,
		        width: 492, height: 156,
		        html: '<img style="width:492px;height:156px;" src="' + LogoSrc + '">'
		    }]
		}];
		me.dockedItems.push(me.createButtonToolbarItems());
		me.callParent(arguments);
	},
	applyItems: function () {
	    var me = this;
	    var items = [{
	        x: 20, y: 20,
            xtype:'textfield',
			fieldLabel:' 账 号 ',
			emptyText:'请输入账号',
			itemId: 'Account',
			width: 200,
	        height:20,
			labelWidth:50,
			labelAlign:'right',
			allowBlank: false,
			value:me.Account,
			name:'Account'
	    }, {
	        xtype: 'textfield', x: 220, y: 20,
			fieldLabel:' 密 码 ',
			emptyText:'请输入密码',
			itemId: 'Pwd',
			width: 200,
			height: 20,
			labelWidth: 50,
			labelAlign:'right',
			allowBlank:false,
			name:'Pwd',
			inputType:'password'
		}];
		
		return items;
	},
	/**创建挂靠功能栏*/
	createButtonToolbarItems:function(){
		var me = this,
			items = [{
			    xtype: 'label',
			    itemId: 'Msg',
			    style: 'margin:0 10px;color:red;font-weight:bold;'
			}, '->', {
                xtype:'button',
			    text: '登录',
			    tooltip: '登录系统',
			    iconCls: 'button-login',
			    handler: function () {
			        me.onLoginClick();
			    }
			}];
			
		return Ext.create('Ext.toolbar.Toolbar', {
		    dock: 'bottom',
		    itemId: 'buttonsToolbar',
		    items: items
		});
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
		var url = Shell.util.Path.rootPath + me.loginUrl +
			'?Account=' + Account +
			'&pwd=' + Pwd;
			
		me.onMsgChange('登录中...');
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
		    url: url,
		    async: false,
		    method: 'get',
		    success: function (response, options) {
		        rs = Ext.JSON.decode(response.responseText);
		        if (rs.success) {
		            me.fireEvent('login', me);
		        } else {
		            me.onMsgChange('登录失败！');
		            me.onAccountFocus(me.focusTimes);
		        }
		    }
		});
	},
	onAccountFocus:function(times){
		var me = this;
		Shell.util.Action.delay(function () { me.getComponent('Account').focus(); }, null, times);
	},
	onMsgChange:function(value){
		var me = this,
			Msg = me.getComponent('buttonsToolbar').getComponent('Msg');
			
		Msg.setText(value || '');
	}
});
	