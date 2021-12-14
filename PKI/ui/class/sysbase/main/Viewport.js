/**
 * View
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.Viewport', {
	extend: 'Ext.container.Viewport',
	layout: 'fit',
	id:'SystemViewport',
	
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
		me.initInfo();
		me.showLoginWin();
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
			items:me.createTopToolbar()
		},{
			xtype:'toolbar',
			dock:'bottom',
			itemId:'BottomToolbar',
			items:me.createBottomToolbar()
		}];
		
		return dockedItems;
	},
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
		}, '->'];
		
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

		return items;
	},
	/**
	 * @description 创建版本信息栏
	 */
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
			text:'版本：1.0.0.1'
		}];
		
		return items;
	},
	getItemCom:function(){
		return this.items.items[0];
	},
	setUserInfo:function(value){
		var me = this;
		var UserInfo = me.getItemCom().getComponent('TopToolbar').getComponent('UserInfo');
		
		var v = '当前用户：' + (value || '无');
		UserInfo.setText(v);
	},
	setSysTime:function(value){
		var me = this;
		var SysTime = me.getItemCom().getComponent('BottomToolbar').getComponent('SysTime');
		
		var v = '系统时间：' + (value || '无');
		SysTime.setText(v);
	},
	setVersion:function(value){
		var me = this;
		var Vesion = me.getItemCom().getComponent('BottomToolbar').getComponent('Vesion');
		
		var v = '版本：' + (value || '无');
		Vesion.setText(v);
	},
	showLoginWin:function(isLocked,account){
		var me = this;
		JShell.Win.open('Shell.class.sysbase.main.Login',{
			formtype:'add',
			resizable: false,
			closable:!isLocked,
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
	afterLogin:function(){
		var me = this,
			Login = me.getItemCom().getComponent('TopToolbar').getComponent('Login'),
			UserInfo = me.getItemCom().getComponent('TopToolbar').getComponent('UserInfo'),
			tree = me.getItemCom().getComponent('FunctionTree');
			
		var ACCOUNTNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) || '无';
		var DEPTNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || '无';
		
		me.setUserInfo(ACCOUNTNAME + '【' + DEPTNAME + '】');
		Login.hide();
		UserInfo.show();
		
		me.ContentTab.onCloseAll();
		
		if(JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) == JShell.System.ADMINNAME){
			tree.getComponent('topToolbar').getComponent('module').show();
		}else{
			tree.getComponent('topToolbar').getComponent('module').hide();
		}
		
		tree.show();
		tree.load();
	},
	initInfo:function(){
		var me = this;
		
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
		JShell.Win.open('Shell.class.sysbase.user.AccountPwd', {
			resizable: false,
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
		setTimeout(function(){me.onSystimesChange();},1000);
	},
	onFunctionTreeConfigClick:function(p){
		var me = this;
		me.ContentTab.insertTab({
			tid:'SYS_MODULE',
			icon:JShell.System.Path.MODULE_ICON_ROOT_16 + '/configuration.PNG',
			text:'模块管理',
			url:'#Shell.class.sysbase.module.App'
		});
	}
});