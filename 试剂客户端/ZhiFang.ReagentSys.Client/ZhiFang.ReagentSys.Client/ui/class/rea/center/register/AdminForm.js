/**
 * 管理员信息
 * @author liangyl
 * @version 2018-05-14
 */
Ext.define('Shell.class.rea.center.register.AdminForm', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.BoolComboBox'
    ],
	
	title: '管理员信息',
	width:240,
    height:300,
	
	/**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchCenOrgById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddCenOrg',
    /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateCenOrgByField',
	/**自动生成账号服务地址*/
	createAccountUrl:'/RBACService.svc/RBAC_RJ_AutoCreateUserAccount',
	/**校验用户账号是否已存在*/
	checkAccountUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACUserListByHQL?isPlanish=true',
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**内容周围距离*/
	bodyPadding:'10px 10px 0 10px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 240,
		labelAlign: 'right'
	},
	buttonDock:'top',
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'add',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.buttonToolbarItems =[{
			xtype: 'label',text: '管理员信息',
			margin: '0 0 10 10',style: "font-weight:bold;color:blue;",
			itemId: 'adminInfo',name: 'adminInfo'
		}];
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		items.push({fieldLabel:'主键ID',name:'CenOrg_Id',hidden:true,type:'key'});
		//姓名
		items.push({
			fieldLabel:'姓名',name:'HREmployee_CName',
			emptyText:'必填项',//allowBlank:false,
			colspan: 1,width: me.defaults.width *1
		},{
			fieldLabel:'手机号',name:'HREmployee_MobileTel',
			regex: /^1[34578]\d{9}$/,
			regexText:'不是手机号码'
		});
		//登录账号
		items.push({
			fieldLabel:'登录账号',name:'HREmployee_Account',
			emptyText:'为空时取机构编号',name:'HREmployee_Account',//allowBlank:false,
			colspan: 1,width: me.defaults.width *1
		});
		//初始密码
		items.push({
			fieldLabel:'初始密码',name:'HREmployee_Password',
			emptyText:'必填项',allowBlank:false,
			colspan: 1,width: me.defaults.width *1,
			inputType:'password'
		});
		return items;
	},
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName:values.HREmployee_CName,
            MobileTel:values.HREmployee_MobileTel,
			IsUse:1,
			IsEnabled:1,
			Account:values.HREmployee_Account,
			PWD:values.HREmployee_Password,
			PwdExprd:true,
			EnMPwd:true
		};
		return entity;
	},
	getAccounInfo:function(){
		var me =this,
			values = me.getForm().getValues();
		var entity = {
			CName:values.HREmployee_CName,
			Account:values.HREmployee_Account,
			PWD:values.HREmployee_Password,
			PwdExprd:true,
			EnMPwd:true
		};
		var Sysdate = JcallShell.System.Date.getDate();
		if(Sysdate){
			entity.AccBeginTime=JShell.Date.toServerDate(Sysdate);
		}
		return entity;
	},
	initFilterListeners:function(){
		var me =this;
	   
	},
	/**校验账号是否已存在*/
	isAccountCheck: function(val,callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.checkAccountUrl;
		//去掉特殊字符 &和？
		var valStr=JcallShell.String.encode(val);      
		url += "&fields=RBACUser_Id&where=rbacuser.Account='"+valStr+"'";
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	}
});