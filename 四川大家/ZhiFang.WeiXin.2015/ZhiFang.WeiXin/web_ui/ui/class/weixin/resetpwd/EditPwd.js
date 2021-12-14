/**
 * 微信帐号密码重置--密码重置
 * @author longfc
 * @version 2017-12-19
 */
Ext.define('Shell.class.weixin.resetpwd.EditPwd', {
	extend: 'Shell.ux.form.Panel',
	title: '微信帐号密码重置',
	width: 280,
	height: 220,
	formtype:'edit',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountById?isPlanish=true',
	/**密码重置服务地址*/
	pwdResetUrl: '/ServerWCF/ZhiFangWeiXinService.svc/WXADS_WeiXinAccountPwdRest',
	/**显示成功信息*/
	showSuccessInfo:false,

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
	WeiXinAccount:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('pwdReset');
		me.buttonToolbarItems = ['->',{
			text:'密码重置',
			iconCls:'button-lock',
			itemId:'PwdReset',
			tooltip:'<b>随机生成密码</b>',
			handler:function(){me.onPwdReset();}
		},'cancel'];
		
		me.items = me.createFormItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createFormItems: function() {
		var me = this,
			items = [];
			
		items.push({
			name:'BWeiXinAccount_UserName',readOnly: true,locked: true,
			fieldLabel:'用户名称',emptyText:'用户账号,不能重复',allowBlank:false
		},{
			name:'BWeiXinAccount_WeiXinAccount',readOnly: true,locked: true,fieldLabel:'帐号',hidden:true
		},{
			name:'BWeiXinAccount_MobileCode',readOnly: true,locked: true,fieldLabel:'手机号码'
		},{
			name:'BWeiXinAccount_Email',readOnly: true,locked: true,fieldLabel:'Email'
		},{
			name:'BWeiXinAccount_DataAddTime',readOnly: true,locked: true,
			xtype:'datefield',fieldLabel:'注册时间',format:'Y-m-d'
		},{
			fieldLabel:'主键ID',name:'BWeiXinAccount_Id',hidden:true
		});
		
		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		var me=this;
		data.BWeiXinAccount_DataAddTime = JShell.Date.getDate(data.BWeiXinAccount_DataAddTime);
		me.WeiXinAccount = data.BWeiXinAccount_WeiXinAccount;
		return data;
	},
	/**@overwrite 取消按钮点击处理方法*/
	onCancelClick: function() {
		this.close();
	},
	/**更改标题*/
	changeTitle:function(){},
	/**密码重置*/
	onPwdReset:function(){
		var me = this,
		url = me.pwdResetUrl;
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		url += '?WeiXinAccount=' + me.WeiXinAccount;		
		JShell.Msg.confirm({
			msg:'确定重新生成密码吗？'
		},function(but){
			if(but != 'ok') return;			
			JShell.Server.get(url,function(data){
				if(data.success){
					me.fireEvent('pwdReset',me);
					JShell.Msg.alert('您的新密码：' + data.value);
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	}
});