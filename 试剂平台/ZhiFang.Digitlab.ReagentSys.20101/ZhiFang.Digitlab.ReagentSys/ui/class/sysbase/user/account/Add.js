/**
 * 新增账户
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.user.account.Add', {
	extend: 'Shell.ux.form.Panel',
	title: '新增账户',
	width: 280,
	height: 240,
	formtype:'add',
	/**修改服务地址*/
	addUrl:'/RBACService.svc/RBAC_UDTO_AddRBACUser',
	/**自动生成账号服务地址*/
	createAccountUrl:'/RBACService.svc/RBAC_RJ_AutoCreateUserAccount',
	
	/**显示成功信息*/
	showSuccessInfo:false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用取消按钮*/
	hasCancel: true,
	/**默认密码*/
	defaultPwd:'123456',
	
	/**内容周围距离*/
	//bodyPadding: 10,
	/**布局方式*/
	//layout: 'anchor',
	/** 每个组件的默认属性*/
	defaults: {
		//anchor: '100%',
		labelWidth: 80,
		width:220,
		labelAlign: 'right'
	},
	/**用户ID*/
	HREmployeeId:null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		if(!me.HREmployeeId) return;
		
		var isDefaultPwd = me.getComponent('isDefaultPwd'),
			Pwd = me.getComponent('Pwd'),
			Pwd2 = me.getComponent('Pwd2');
			
		isDefaultPwd.on({
			change:function(field,newValue,oldValue){
				if(newValue){
					Pwd.hide();
					Pwd2.hide();
					Pwd.setValue(me.defaultPwd);
					Pwd2.setValue(me.defaultPwd);
				}else{
					Pwd.show();
					Pwd2.show();
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		if(me.HREmployeeId){
			me.items = me.createFormItems();
		}else{
			me.items = [];
			me.html = me.errorFormat.replace(/{msg}/g,'请传递HREmployeeId参数！');
		}

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createFormItems: function() {
		var me = this,
			items = [];
			
		items.push({
			x:10,y:10,width:198,labelWidth:80,
			fieldLabel:'员工账号',emptyText:'用户账号,不能重复',name:'Account',allowBlank:false
		},{
			x:208,y:10,width:22,
			xtype:'button',text:'',iconCls:'button-config',tooltip:'根据用户自动生成账号',
			handler:function(){me.onCreateAccount();}
		},{
			x:10,y:40,
			xtype:'datefield',fieldLabel:'账号开始时间',name:'AccBeginTime',format:'Y-m-d'
		},{
			x:10,y:70,
			xtype:'datefield',fieldLabel:'账号截止时间',name:'AccEndTime',format:'Y-m-d'
		},{
			x:10,y:100,xtype:'checkbox',boxLabel:'允许修改密码',name:'EnMPwd',checked:true
		},{
			x:10,y:130,xtype:'checkbox',boxLabel:'默认密码(' + me.defaultPwd + ')',
			itemId:'isDefaultPwd',checked:true
		},{
			x:10,y:160,fieldLabel:'密码设置',emptyText:'必填',itemId:'Pwd',name:'Pwd',
			allowBlank:false,hidden:true,value:me.defaultPwd
		},{
			x:10,y:190,fieldLabel:'密码验证',emptyText:'重新输入密码',itemId:'Pwd2',name:'Pwd2',
			allowBlank:false,hidden:true,value:me.defaultPwd
		});

		return items;
	},
	/**@overwrite 取消按钮点击处理方法*/
	onCancelClick: function() {
		this.close();
	},
	/**@overwrite 获取修改的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		//密码不能为空
		if (!values.Pwd) {
			JShell.Msg.error('密码不能为空!');
			return false;
		}
		//判断两次密码输入是否一致
		if (values.Pwd != values.Pwd2) {
			JShell.Msg.error('两次密码不一致!');
			return false;
		}
		
		var entity = {
			entity: {
				LabID:1,
				HREmployee:{Id:me.HREmployeeId},
				Account:values.Account,
				EnMPwd:values.EnMPwd ? true : false,
				PwdExprd:true,//密码永不过期
				PWD:values.Pwd
			}
		};
		if(values.AccBeginTime){
			entity.entity.AccBeginTime = JShell.Date.toServerDate(values.AccBeginTime);
		}
		if(values.AccEndTime){
			entity.entity.AccEndTime = JShell.Date.toServerDate(values.AccEndTime);
		}
		
		return entity;
	},
	/**自动生成账号*/
	onCreateAccount:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.createAccountUrl);
			
		url += "?id=5738387449976463848" + "&strUserAccoun=";
		
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
	isAdd:function(){
		var me = this;
		
		me.callParent(arguments);
		
		if(!me.HREmployeeId){
			me.disableControl();
		}else{
			me.getForm().setValues({AccBeginTime:new Date()});
		}
	},
	/**更改标题*/
	changeTitle:function(){}
});