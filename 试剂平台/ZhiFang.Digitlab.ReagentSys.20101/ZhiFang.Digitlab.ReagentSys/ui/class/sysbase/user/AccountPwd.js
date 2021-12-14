/**
 * 账户密码维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.user.AccountPwd', {
	extend: 'Shell.ux.form.Panel',
	title: '账户密码维护',
	width: 280,
	height: 160,
	/**修改服务地址*/
	editUrl: '/RBACService.svc/RBAC_UDTO_UpdateRBACUserByField',
	/**校验用户名密码服务*/
	loginServerUrl:'/RBACService.svc/RBAC_BA_Login?isValidate=true',
	
	/**显示成功信息*/
	showSuccessInfo:false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用取消按钮*/
	hasCancel: true,

	/**内容周围距离*/
	bodyPadding: 10,
	/**布局方式*/
	layout: 'anchor',
	/** 每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 70,
		labelAlign: 'right'
	},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		me.items = me.createItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		var items = [{
			fieldLabel: '当前密码',
			emptyText: '当前密码',
			allowBlank: false,
			itemId: 'pwd',
			name: 'pwd',
			inputType: 'password'
		}, {
			fieldLabel: '新密码',
			emptyText: '新密码',
			allowBlank: false,
			itemId: 'newPwd1',
			name: 'newPwd1',
			inputType: 'password'
		}, {
			fieldLabel: '确认新密码',
			emptyText: '确认新密码',
			allowBlank: false,
			itemId: 'newPwd2',
			name: 'newPwd2',
			inputType: 'password'
		}];

		return items;
	},
	/**@overwrite 取消按钮点击处理方法*/
	onCancelClick: function() {
		this.close();
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues();

		//判断两次新密码输入是否一致
		if (values.newPwd1 != values.newPwd2) {
			JShell.Msg.error('请确认新密码正确!');
			return false;
		}
		
		if(values.pwd == values.newPwd1){
			JShell.Msg.error('新密码与当前密码相同，不需要保存!');
			return false;
		}
		
		//判断当前密码是否正确
		var url = JShell.System.Path.ROOT + me.loginServerUrl + "&strUserAccount=" + 
			JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) +
			"&strPassWord=" + values.pwd;
			
		var pwdError = false;
		JShell.Server.get(url,function(text){
			var success = Ext.JSON.decode(text);
            if(!success){
            	pwdError = true;
            }
		},false,null,true);
		
		if(pwdError){
			JShell.Msg.error('当前密码错误，请重新输入！');
			return false;
		}
		
		if(!JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTID)){
			return false;
		}

		var entity = {
			entity: {
				Id: JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTID),
				PWD: values.newPwd1
			},
			fields: 'Id,PWD'
		};
		return entity;
	}
});