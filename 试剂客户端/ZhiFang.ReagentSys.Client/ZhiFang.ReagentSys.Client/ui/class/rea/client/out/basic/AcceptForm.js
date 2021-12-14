/**
 * 领用确认提示
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.out.basic.AcceptForm', {
	extend: 'Shell.ux.form.Panel',
	title: '出库确认操作',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 250,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '10px 20px 0px 10px',
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchTestEquipLabById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddTestEquipLab',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateTestEquipLabByField',
	/**获取用户密码数据服务路径*/
	selectLoginUrl: '/RBACService.svc/RBAC_BA_Login',
	/**获取账户服务路径*/
	selectUserUrl: '/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?isPlanish=true',
	formtype: 'add',
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 50,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	UserName: '',
	UserID: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			xtype: 'displayfield',
			fieldLabel: '确认人',
			itemId: 'UserName',
			name: 'UserName'
		}, {
			fieldLabel: '确认人ID',
			hidden: true,
			itemId: 'UserID',
			name: 'UserID'
		}, {
			fieldLabel: '密码',
			inputType: 'password',
			emptyText: '必填项',
			allowBlank: false,
			itemId: 'Password',
			name: 'Password'
		}, {
			hidden: true,
			fieldLabel: '账号',
			itemId: 'AccountName',
			name: 'AccountName'
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			UserName: values.UserName,
			UserID: values.UserID
		};

		return {
			entity: entity
		};
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;

		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;

	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if(me.hasSave) items.push('->','accept');
		items.push({
			text: '关闭',
			tooltip: '关闭',
			iconCls: 'button-del',
			handler: function() {
				me.close();
			}
		});
		if(items.length == 0) return null;

		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: me.buttonDock,
			itemId: 'buttonsToolbar',
			items: items,
			hidden: hidden
		});
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
		} else {
			me.getForm().setValues(me.lastData);
		}
		var UserName = me.getComponent('UserName');
		UserName.setValue(me.UserName);
		var UserID = me.getComponent('UserID');
		UserID.setValue(me.UserID);
		me.getAccountByUserId();
	},
	onAcceptClick: function() {
		var me = this;
		if(!me.getForm().isValid()) return;
		var values = me.getForm().getValues();
		me.showMask(me.loadingText); //显示遮罩层
		var Account = me.getComponent('AccountName');
		if(!Account.getValue()) {
			JShell.Msg.error('用户账号不能为空!');
			return;
		}
		me.Login(Account.getValue(), values.Password, function(data) {
			var success = Ext.JSON.decode(data);
			me.hideMask();
			var str = data + '';
			if(str == 'true') {
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error('确认人密码不正确,请重新输入!');
			}
		});
	},
	/**校验是否是领用人*/
	onCheckPassword: function() {
		var me = this;
	},
	/**根据用户Id,找到用户账号*/
	getAccountByUserId: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUserUrl;
		var values = me.getForm().getValues();
		if(!values.UserID) return;
		url += "&fields=RBACUser_Account&where=rbacuser.HREmployee.Id=" + values.UserID + "";
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data && data.value) {
					var list = data.value.list;
					var Account = list[0].RBACUser_Account;
					var UserName = me.getComponent('UserName');
					UserName.setValue(me.UserName + '(' + Account + ')');
					var AccountName = me.getComponent('AccountName');
					AccountName.setValue(Account);
				}
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**根据用户Id,找到用户账号*/
	Login: function(strUserAccount, UserName, callback) {
		var me = this;
		var pwdError = false;
		var url = JShell.System.Path.ROOT + me.selectLoginUrl;
		url += '?strUserAccount=' + strUserAccount + '&strPassWord=' + UserName;
		JShell.Server.get(url, function(text) {
			callback(text);
		}, false, null, true);
	}
});