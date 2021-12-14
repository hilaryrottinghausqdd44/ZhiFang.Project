/**
 * 登录界面(LIS 6.6 PUser)
 * @author longfc
 * @version 2020-01-05
 */
Ext.define('Shell.class.sysbase.main.login.Panel66', {
	extend: 'Shell.ux.form.Panel',

	title: '登录',
	width: 492,
	height: 260,
	formtype: 'add',

	/**登录服务地址*/
	loginUrl: "/ServerWCF/WebAssistManageService.svc/BT_SYS_LoginOfPUser",

	/** 每个组件的默认属性*/
	defaults: {
		width: 200,
		labelWidth: 50,
		labelAlign: 'right',
		allowBlank: false
	},
	/**内容自动显示*/
	autoScroll: false,
	/**光标定位延时*/
	focusTimes: 200,

	/**是否锁定账号*/
	isLocked: false,
	/**默认账号*/
	account: null,
	/**是否包含注册账号按钮*/
	hasReg: true,
	/**是否包含同步账号按钮*/
	hasLisSyncHis: true,

	afterRender: function() {
		var me = this;
		me.addEvents('login');
		me.callParent(arguments);

		me.initListeners();

		me.on({
			show: function() {
				if (me.account) {
					setTimeout(function() {
						me.getComponent('Pwd').focus();
					}, me.focusTimes);
				} else {
					me.onAccountFocus(me.focusTimes);
				}
			}
		});

		//JS文件加载完毕时处理
		JShell.System.afertJSLoading();
	},
	initComponent: function() {
		var me = this;
		me.icon = JShell.System.Path.UI + '/css/images/system/logo-16.png';
		me.title = JShell.System.Name + '-' + me.title;
		me.items = me.createItems();

		var LogoSrc = JShell.System.Path.ROOT + "/ui" + JShell.System.LoginTopImage + '?v=' + JShell.System.JS_VERSION;
		me.dockedItems = [{
			xtype: 'toolbar',
			dock: 'top',
			height: 156,
			border: false,
			padding: 0,
			items: [{
				xtype: 'panel',
				border: false,
				width: 492,
				height: 156,
				html: '<img style="width:492px;height:156px;" src="' + LogoSrc + '">'
			}]
		}];
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var items = [{
			x: 20,
			y: 15,
			fieldLabel: ' 账 号 ',
			emptyText: '请输入账号',
			itemId: 'Account',
			name: 'Account',
			value: me.account || '',
			readOnly: me.isLocked,
			locked: me.isLocked
		}, {
			x: 220,
			y: 15,
			fieldLabel: ' 密 码 ',
			emptyText: '请输入密码',
			itemId: 'Pwd',
			name: 'Pwd',
			inputType: 'password',
			value: ''
		}];

		return items;
	},
	/**创建挂靠功能栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		
		items.push({
			xtype: 'label',
			itemId: 'Msg',
			style: 'margin:0 10px;color:red;font-weight:bold;'
		}, '->', 'login');

		if (me.closable) {
			items.push('cancel');
		}
		if (me.hasReg) {
			items.push({
				text: '注册',
				tooltip: '注册帐号',
				iconCls: 'button-add',
				style: 'margin-left:120px',
				handler: function() {
					me.onRegClick();
				}
			});
		}
		if (me.hasLisSyncHis) {
			items.push({
				text: '同步',
				tooltip: '同步账号',
				iconCls: 'button-save',
				//style: 'margin-right:120px',
				handler: function() {
					me.onLisSyncHiClick();
				}
			});
		}
		return items;
	},
	initListeners: function() {
		var me = this;
		var Account = me.getComponent('Account');
		var Pwd = me.getComponent('Pwd');

		new Ext.KeyMap(Account.getEl(), [{
			key: Ext.EventObject.ENTER,
			fn: function() {
				me.onLoginClick();
			}
		}]);
		new Ext.KeyMap(Pwd.getEl(), [{
			key: Ext.EventObject.ENTER,
			fn: function() {
				me.onLoginClick();
			}
		}]);
	},
	/**
	 * @description 注册帐号
	 */
	onRegClick: function() {
		var me = this;
		
		JShell.Win.open('Shell.class.sysbase.puser.reg.App', {
			resizable: true,
			width: 240,
			height: 320,
			listeners: {
				close: function(p) {

				}
			}
		}).show();
	},
	/**
	 * @description 同步账号
	 */
	onLisSyncHiClick: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + "/ServerWCF/WebAssistManageService.svc/WA_UDTO_SaveLisSyncHisDataInfo";
		me.showMask('同步账号中...'); //显示遮罩层
		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			var success = data.success;
			if (success == undefined || success == null) success = data;
			if (success == true || success == 'true') {

			} else {

			}
		}, true, null, false);
	},
	onLoginClick: function() {
		var me = this;

		if (!me.getForm().isValid()) {
			me.onAccountFocus(me.focusTimes);
			return;
		}

		var values = me.getForm().getValues();
		me.onLogin(values.Account, values.Pwd);
	},
	onLogin: function(Account, Pwd) {
		var me = this;

		if (!JShell.System.ADMIN_CAN_LOGIN && Account == JShell.System.ADMINNAME) {
			JShell.Msg.error('此账号不能登录！');
			return;
		}

		var url = JShell.System.Path.ROOT + me.loginUrl +
			'?strUserAccount=' + Account +
			'&strPassWord=' + Pwd + "&t=" + new Date().getTime();

		me.onMsgChange();
		me.showMask('登录中...'); //显示遮罩层

		var AllCookie = JShell.System.Cookie.getAllCookie();
		//清理所有cookie
		JShell.System.Cookie.clearCookie();

		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			var success = data.success;
			if (success == undefined || success == null) success = data;
			if (success == true || success == 'true') {
				JShell.LocalStorage.set("account", Account);
				JShell.LocalStorage.set("password", Pwd);

				if (!JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID)) {
					JShell.System.Cookie.set({
						name: JShell.System.Cookie.map.DEPTCODE,
						value: ''
					});
				}
				if (Ext.typeOf(JShell.System.onAfterLogin) == 'function') {
					JShell.System.onAfterLogin();
				}
				me.fireEvent('login', me);
			} else {
				for (var i = 0; i < AllCookie.length; i++) {
					JShell.System.Cookie.set(AllCookie[i][0], AllCookie[i][1]);
				}

				me.onMsgChange('登录失败！');
				me.onAccountFocus(me.focusTimes);
			}
		}, true, null, false);
	},
	onAccountFocus: function(times) {
		var me = this;
		setTimeout(function() {
			me.getComponent('Account').focus();
		}, times);
	},
	onMsgChange: function(value) {
		var me = this,
			Msg = me.getComponent('buttonsToolbar').getComponent('Msg');

		Msg.setText(value || '');
	},
	/**更改标题*/
	changeTitle: function() {

	}
});
