/**
 * @author guohx
 * @version 2020-04-8
*/
Ext.define('Shell.class.main.userSign.app.App', {
    extend:'Ext.form.Panel',
	
	title:'登录',
	width:480,
	height:280,
	formtype: 'add',
    Account:'',
    
	/**登录服务地址*/
	loginUrl: '/ServiceWCF/ReportFormService.svc/StaticUserLogin',
	
	bodyPadding:'15px 100px',
	/**布局方式*/
	layout: 'absolute',
    /**光标定位延时*/
    focusTimes:200,
    /**倒计时*/
	tackTime:5,
	staticTackTime:'',
	isstopsubmit:true,
	logincount:0,
	afterRender:function(){
		var me = this ;
		me.addEvents('login');
		me.callParent(arguments);
		me.initListeners();
		createCode();
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
	        x: 30, y: 20,
            xtype:'textfield',
			fieldLabel:'登录密码 ',
			emptyText:'请输入密码',
			itemId: 'Account',
			width: 180,
	        height:20,
			labelWidth:60,
			labelAlign:'right',
			allowBlank: false,
			value:me.Account,
			name:'Account'
	    },{
	        xtype: 'panel', x: 220, y: 20,
	        border: false,
	        width: 250, height: 60,
	        html:  '<div style="display:inline-block;">验证码 ：<input style="width:130px;" type="text" id="inputCode" /></div>'+
	        '<div style="display:inline-block;width:180px;" class="code" id="checkCode" onclick="createCode()" ></div>'
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
	},
	onLoginClick:function(){
		var me = this;
		 		
		if(!me.isstopsubmit){
			return;
		}
		if(!me.getForm().isValid()){
			me.onAccountFocus(me.focusTimes);
			return;
		}
		var returnvalue = validateCode();		
  		if(returnvalue == 1){
  			me.onMsgChange("请输入验证码！");
  			return;
  		}else if(returnvalue == 2){
  			me.onMsgChange("验证码输入有误！");
  			return;
  		}else{
  			me.onMsgChange("验证码正确！");
  		}
  		
		var values = me.getForm().getValues();
		
		me.onLogin(values.Account);
	},
	onLogin:function(Account){
		var me = this;
		var url = Shell.util.Path.rootPath + me.loginUrl +
			'?Account=' + Account 
			
		me.onMsgChange('登录中...');
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
		    url: url,
		    async: false,
		    method: 'get',
		    success: function (response, options) {
		        rs = Ext.JSON.decode(response.responseText);
		        Ext.JSON.decode();
		        if (rs.success) {
		            me.fireEvent('login', me);
		        } else {
		        	me.logincount=me.logincount+1;
		        	if(me.logincount == 5){
		        		me.isstopsubmit=false;
		        		me.staticTackTime =0;
						me.staticTackTime=me.staticTackTime+me.tackTime;
						me.task = {
				            run: function () {							   
				                me.onMsgChange("登录失败次数过多当前限制剩余时间:"+ me.staticTackTime + "秒");
				                if (me.staticTackTime == 0) {
				                    me.isstopsubmit=true;
				                    Ext.TaskManager.stop(me.task);
				                    me.logincount = 0;
				                    me.onMsgChange("");
				                }
				                me.staticTackTime--;
				            },
				            interval: 1000
				        };
		        		Ext.TaskManager.start(me.task);//启动计时器		
		        	}else{	
		        		me.onMsgChange('登录失败！');  		
		        	}		            
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
	