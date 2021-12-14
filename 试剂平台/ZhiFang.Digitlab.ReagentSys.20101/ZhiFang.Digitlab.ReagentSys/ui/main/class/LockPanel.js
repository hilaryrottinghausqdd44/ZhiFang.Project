/**
 * 账户操作
 */
Ext.ns('Ext.main');
Ext.define('Ext.main.LockPanel',{
	extend:'Ext.form.Panel',
	alias:'widget.lockpanel',
	//===================可配置参数====================
	/**
	 * 标题
	 * @type String
	 */
	title:'账户设置',
	/**
	 * 面板宽度
	 * @type Number
	 */
	width:400,
	/**
	 * 面板高度
	 * @type Number
	 */
	height:200,
	/**
	 * 当前用户
	 * @type String
	 */
	userAccount:'',
	
	/**显示密码修改按钮*/
	showPwd:true,
	/**
     * 登录的后台服务地址
     * @type String
     */
    loginServerUrl:getRootPath()+'/RBACService.svc/RBAC_BA_Login',
	//=================================================
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	/**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.layout = "absolute";
		me.items = me.createItems();
        me.addEvents('lockclick');//注册锁定用户按钮监听
        me.addEvents('changclick');//注册切换用户按钮监听
        me.addEvents('pwdclick');//注册修改密码按钮监听
		me.callParent(arguments);
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		var items = [{
			xtype:'label',
			text:'当前账号：',
			width:60,
			x:100,
			y:30
		},{
			xtype:'label',
			text:me.userAccount,
			itemId:'strUserAccount',
			width:240,
			x:170,
			y:30
		},{
			xtype:'button',
			text:'切换账号',
			itemId:'change',
			width:90,
			x:100,
			y:100,
			handler:function(){
				me.fireEvent('changeclick');
			}
		},{
			xtype:'button',
			text:'锁定账号',
			itemId:'lock',
			width:90,
			x:210,
			y:100,
			handler:function(){
				me.fireEvent('lockclick');
			}
		}];
		
		if(me.showPwd){
			items.push({
				xtype:'button',
				text:'修改密码',
				itemId:'pwd',
				width:90,
				x:100,
				y:130,
				handler:function(){
					//me.fireEvent('pwdclick');
					me.openPwdWin();
				}
			});
		}
		
		return items;
	},
	/**
	 * 打开修改密码面板
	 */
	openPwdWin:function(){
		var me = this;
		JShell.Win.open('Shell.class.sysbase.user.AccountPwd', {
			resizable: false,
			listeners:{
				save:function(p){
					p.close();
					me.fireEvent('lockclick');
				}
			}
		}).show();
	},
	//=========================对外公开方法========================
	/**
	 * 设置当前用户账号
	 * @public
	 * @param {} str
	 */
	setUserAccount:function(str){
		var me = this;
		var strUserAccount = me.getComponent('strUserAccount');
		strUserAccount.setText(str);
	}
});