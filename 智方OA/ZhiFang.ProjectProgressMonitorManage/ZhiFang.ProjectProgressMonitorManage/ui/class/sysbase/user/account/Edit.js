/**
 * 账户维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.user.account.Edit', {
	extend: 'Shell.ux.form.Panel',
	title: '账户维护',
	width: 280,
	height: 220,
	formtype:'edit',
	selectUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACUserById?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/RBACService.svc/RBAC_UDTO_UpdateRBACUserByField',
	/**自动生成账号服务地址*/
	createAccountUrl:'/RBACService.svc/RBAC_RJ_AutoCreateUserAccount',
	/**密码重置服务地址*/
	pwdResetUrl:'/RBACService.svc/RBAC_RJ_ResetAccountPassword',
	
	/**显示成功信息*/
	showSuccessInfo:false,
//	/**是否启用保存按钮*/
//	hasSave: true,
//	/**是否重置按钮*/
//	hasReset:true,
//	/**是否启用取消按钮*/
//	hasCancel: true,
	
	/**内容周围距离*/
	bodyPadding: 10,
	/**布局方式*/
	layout: 'anchor',
	/** 每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 80,
		width:220,
		labelAlign: 'right'
	},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		me.buttonToolbarItems = [{
			text:'密码重置',
			iconCls:'button-lock',
			itemId:'PwdReset',
			tooltip:'<b>随机生成密码</b>',
			handler:function(){me.onPwdReset();}
		},'->','save','reset','cancel'];
		
		me.items = me.createFormItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createFormItems: function() {
		var me = this,
			items = [];
			
		items.push({
			name:'RBACUser_Account',
			fieldLabel:'员工账号',emptyText:'用户账号,不能重复',allowBlank:false
		},{
			name:'RBACUser_AccBeginTime',
			xtype:'datefield',fieldLabel:'账号开始时间',format:'Y-m-d'
		},{
			name:'RBACUser_AccEndTime',
			xtype:'datefield',fieldLabel:'账号截止时间',format:'Y-m-d'
		},{
			xtype:'checkbox',name:'RBACUser_EnMPwd',
			boxLabel:'允许修改密码',width:110
		},{
			xtype:'checkbox',name:'RBACUser_AccLock',
			boxLabel:'账号锁定',width:110
		},{
			fieldLabel:'主键ID',name:'RBACUser_Id',hidden:true
		});

		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		//data.RBACUser_EnMPwd = "true" ? true : false;
		//data.RBACUser_AccLock = "true" ? true : false;
		data.RBACUser_AccBeginTime = JShell.Date.getDate(data.RBACUser_AccBeginTime);
		data.RBACUser_AccEndTime = JShell.Date.getDate(data.RBACUser_AccEndTime);
		return data;
	},
	/**@overwrite 取消按钮点击处理方法*/
	onCancelClick: function() {
		this.close();
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues();
		
		var entity = {
			entity: {
				Id:values.RBACUser_Id,
				Account:values.RBACUser_Account,
				EnMPwd:values.RBACUser_EnMPwd ? true : false,
				AccLock:values.RBACUser_AccLock ? true : false
			}
		};
		if(values.RBACUser_AccBeginTime){
			entity.entity.AccBeginTime = JShell.Date.toServerDate(values.RBACUser_AccBeginTime);
		}
		if(values.RBACUser_AccEndTime){
			entity.entity.AccEndTime = JShell.Date.toServerDate(values.RBACUser_AccEndTime);
		}
		
		entity.fields = 'Id,Account,EnMPwd,AccLock,AccBeginTime,AccEndTime';
		
		return entity;
	},
	/**自动生成账号*/
	onCreateAccount:function(){
		var me = this,
			values = me.getForm().getValues(),
			url = JShell.System.Path.getRootUrl(me.createAccountUrl);
			
		url += "?id=" + me.HREmployeeId + "&strUserAccoun=" + values.Account;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				if(data.value.UserAccount.indexOf('数据库中不存在员工') == -1){
					me.getForm().setValues({Account:data.value.UserAccount});
				}else{
					JShell.Msg.error(data.value.UserAccount);
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**更改标题*/
	changeTitle:function(){},
	/**密码重置*/
	onPwdReset:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.pwdResetUrl);
		
		url += '?id=' + me.PK;
		
		
		JShell.Msg.confirm({
			msg:'确定重新生成密码吗？'
		},function(but){
			if(but != 'ok') return;
			
			JShell.Server.get(url,function(data){
				if(data.success){
					JShell.Msg.alert('您的新密码：' + data.value.AccountPassword);
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	}
});