/**
 * 平台客户新增页面
 * @author Jcall
 * @version 2016-08-27
 */
Ext.define('Shell.class.sysbase.serviceclient.create.TabPanel',{
    extend: 'Ext.tab.Panel',
    title:'平台客户新增页面',
    
    width:670,
	height:400,
	
	/**新增机构服务地址*/
    addOrgUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddHRDept',
    /**新增用户服务地址*/
    addEmpUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddHREmployee',
    /**新增账号服务地址*/
	addUserUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACUser',
	/**新增角色员工服务*/
    addEmpRoleUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACEmpRoles',
	/**新增角色模块服务*/
    addRoleModuleUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACRoleModule',
    /**获取数据服务路径*/
	selectModuleListUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL',
	/**拷贝角色服务地址*/
	copyRoleUrl:'',
	/**拷贝字典服务地址*/
	copyDictUrl:'',
	/**拷贝机构字典树服务地址*/
	copyLabDictTreeUrl:'',
	
    autoScroll:false,
    
    /**保存返回的信息*/
    ReasultInfo:{
    	LabID:null,//平台客户ID
    	OrgID:null,//机构ID
    	EmpID:null,//用户ID
    	UserID:null,//账户ID
    	RoleID:null,//角色ID
    	OrgAdminRoleID:null//机构管理员角色ID
    },
    
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//获取机构管理员角色ID
		me.getOrgAdminRoleId(10,0,function(){});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		
		me.dockedItems = [Ext.create('Shell.ux.toolbar.Button',{
			dock:'bottom',
			itemId:'buttonsToolbar',
			items:['->','save']
		})];
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		
		me.Form = Ext.create('Shell.class.sysbase.serviceclient.Form',{
			formtype:'add',
			hasLoadMask:false,//开启加载数据遮罩层
			title:'客户信息',
			hasSave:false,//是否启用保存按钮
			hasReset:false,//是否重置按钮
			changeTitle:function(){}
		});
		
		me.OtherInfoForm = Ext.create('Shell.class.sysbase.serviceclient.create.OtherInfoForm',{
			title:'其他信息'
		});
		
		return [me.Form,me.OtherInfoForm];
	},
	/**保存按钮处理*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.Form.getForm().isValid()){
			me.setActiveTab(me.Form);//定位到平台客户信息页签
			return;
		}
		
		if(!me.ReasultInfo.OrgAdminRoleID){
			me.getOrgAdminRoleId(10,0,function(){
				me.onBeginSave();//开始保存数据
			});
			return;
		}
		
		//开始保存数据
		me.onBeginSave();
	},
	
	/**获取机构管理员角色ID*/
	getOrgAdminRoleId:function(total,index,callback){
		if(index >= total){
			JShell.Msg.error('机构管理员角色获取失败！');
			return;
		}
		
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectModuleListUrl),
			RoleCode = JShell.System.ORG_ADMIN_ROLE_USECODE;
			
		url += "?where=rbacrole.UseCode='" + RoleCode + "'";
			
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				me.ReasultInfo.OrgAdminRoleID = list[0].Id;
				callback();
			}else{
				me.getOrgAdminRoleId(total,++index,callback);
			}
		});
	},
	
	/**打开进度面板*/
	onOpenProgressWin:function(){
		var me = this;
		
		me.ProgressWin = JShell.Win.open('Shell.class.sysbase.serviceclient.create.ProgressWin',{
			maximizable:false,//是否带最大化功能
			closable: false, //关闭功能
			resizable: false, //可变大小功能
			listeners:{
				close:function(){
					me.fireEvent('save',me);
				}
			}
		}).show();
	},
	
	/**开始保存数据*/
	onBeginSave:function(){
		var me = this;
		
		//打开进度面板
		me.onOpenProgressWin();
		me.ProgressWin.onBegin();
		
		//平台客户+机构+用户+账户+角色+角色用户关系+角色模块关系+字典+字典树
		
		//保存平台客户信息
		me.onSaveServiceClientInfo();
	},
	/**保存平台客户信息*/
	onSaveServiceClientInfo:function(callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.Form.addUrl),
			params = me.Form.getAddParams();
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				var id = data.value;
				id = Ext.typeOf(id) == 'object' ? id.id : id;
				
				me.ReasultInfo.LabID = id;//平台客户ID
				me.ProgressWin.onShowInfo('平台客户信息创建成功');
				setTimeout(function(){
					me.onSaveOrgInfo();//保存机构信息
				},20);
			}else{
				me.ProgressWin.onShowError('平台客户信息创建失败');
				callback();
			}
		});
	},
	/**保存机构信息*/
	onSaveOrgInfo:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addOrgUrl),
			values = me.Form.getValues();
			
		var params = {
			entity:{
				LabID:me.ReasultInfo.LabID,
				CName:values.SServiceClient_Name,
				PinYinZiTou:values.SServiceClient_PinYinZiTou,
				SName:values.SServiceClient_SName,
				Shortcode:values.SServiceClient_Shortcode,
				IsUse:true,
				Comment:values.SServiceClient_Comment,
				
				Tel:values.SServiceClient_PhoneNum,
				ZipCode:values.SServiceClient_MailNo,
				Address:values.SServiceClient_Address,
				Contact:values.SServiceClient_LinkMan
			}
		};
			
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				var id = data.value;
				id = Ext.typeOf(id) == 'object' ? id.id : id;
				me.ReasultInfo.OrgID = id;//机构ID
				
				me.ProgressWin.onShowInfo('机构信息创建成功');
				setTimeout(function(){
					me.onSaveEmpInfo(id);//保存用户信息
				},20);
			}else{
				me.ProgressWin.onShowError('机构信息创建失败');
			}
		});
	},
	/**保存用户信息*/
	onSaveEmpInfo:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addEmpUrl),
			values = me.Form.getValues();
			
		var params = {
			entity:{
				LabID:me.ReasultInfo.LabID,
				HRDept:{Id:me.ReasultInfo.OrgID,DataTimeStamp:[0,0,0,0,0,0,0,0]},
				NameL:values.SServiceClient_SName || values.SServiceClient_Name,
				NameF:'_机构管理员',
				CName:(values.SServiceClient_SName || values.SServiceClient_Name) + '_机构管理员',
				IsEnabled:1,
				IsUse:true
			}
		};
			
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				var id = data.value;
				id = Ext.typeOf(id) == 'object' ? id.id : id;
				me.ReasultInfo.EmpID = id;//用户ID
				
				me.ProgressWin.onShowInfo('用户信息创建成功');
				setTimeout(function(){
					me.onSaveUserInfo();//保存账户信息
				},20);
			}else{
				me.ProgressWin.onShowError('用户信息创建失败');
			}
		});
	},
	/**保存账户信息*/
	onSaveUserInfo:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addUserUrl),
			Account = me.OtherInfoForm.getAccount();
			
		var params = {
			entity:{
				LabID:me.ReasultInfo.LabID,
				HREmployee:{Id:me.ReasultInfo.EmpID,DataTimeStamp:[0,0,0,0,0,0,0,0]},
				Account:Account,
				EnMPwd:true,
				PwdExprd:true,//密码永不过期
				PWD:'123456'
			}
		};
			
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				var id = data.value;
				id = Ext.typeOf(id) == 'object' ? id.id : id;
				me.ReasultInfo.UserID = id;//账户ID
				
				me.ProgressWin.onShowInfo('账户信息创建成功');
				setTimeout(function(){
					me.onSaveUserInfo();//保存角色信息
				},20);
			}else{
				me.ProgressWin.onShowError('账户信息创建失败');
			}
		});
	},
	/**保存角色信息*/
	onSaveUserInfo:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.copyRoleUrl);
			
		var params = {
			LabID:me.ReasultInfo.LabID,
			ids:me.ReasultInfo.OrgAdminRoleID
		};
			
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				var id = data.value;
				id = Ext.typeOf(id) == 'object' ? id.id : id;
				me.ReasultInfo.RoleID = id;//角色ID
				
				me.ProgressWin.onShowInfo('角色信息创建成功');
				setTimeout(function(){
					me.onSaveEmpRoleInfo();//保存角色员工关系
				},20);
			}else{
				me.ProgressWin.onShowError('角色信息创建失败');
			}
		});
	},
	/**保存角色员工关系*/
	onSaveEmpRoleInfo:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addEmpRoleUrl);
			
		var params = {
			LabID:me.ReasultInfo.LabID,
			HREmployee:{Id:me.ReasultInfo.EmpID,DataTimeStamp:[0,0,0,0,0,0,0,0]},
			RBACRole:{Id:me.ReasultInfo.RoleID,DataTimeStamp:[0,0,0,0,0,0,0,0]},
			IsUse:true
		};
			
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.ProgressWin.onShowInfo('角色员工关系创建成功');
				setTimeout(function(){
					me.onSaveRoleModuleInfo();//保存角色模块关系
				},20);
			}else{
				me.ProgressWin.onShowError('角色员工关系创建失败');
			}
		});
	},
	/**保存角色模块关系*/
	onSaveRoleModuleInfo:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addRoleModuleUrl),
			ModuleIds = me.OtherInfoForm.getModuleIds();
			
		me.onSaveOneRoleModuleInfo(ModuleIds,0);
	},
	/**保存一个角色模块关系*/
	onSaveOneRoleModuleInfo:function(ModuleIds,index){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addRoleModuleUrl),
			len = ModuleIds.length;
			
		if(index >= len){
			me.ProgressWin.onShowInfo('角色模块关系创建成功');
			setTimeout(function(){
				me.onSaveDictInfo();//保存字典信息
			},20);
			return;
		}else{
			var params = {
				LabID:me.ReasultInfo.LabID,
				RBACModule:{Id:ModuleIds[index],DataTimeStamp:[0,0,0,0,0,0,0,0]},
				RBACRole:{Id:me.ReasultInfo.RoleID,DataTimeStamp:[0,0,0,0,0,0,0,0]},
				IsUse:true
			};
				
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				if(data.success){
					me.onSaveOneRoleModuleInfo(ModuleIds,++index);
				}else{
					me.ProgressWin.onShowError('角色模块关系创建失败');
				}
			});
		}
	},
	/**保存字典信息*/
	onSaveDictInfo:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.copyDictUrl),
			DictTypeIds = me.OtherInfoForm.getDictTypeIds();
			
		var params = {
			LabID:me.ReasultInfo.LabID,
			ids:DictTypeIds.join(',')
		};
			
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.ProgressWin.onShowInfo('字典信息创建成功');
				setTimeout(function(){
					me.onSaveLabDictTreeInfo();//保存机构字典树信息
				},20);
			}else{
				me.ProgressWin.onShowError('字典信息创建失败');
			}
		});
	},
	/**保存机构字典树信息*/
	onSaveLabDictTreeInfo:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.copyLabDictTreeUrl),
			FromLabID = me.OtherInfoForm.getFromLabID();
			
		var params = {
			FromLabID:FromLabID,//被拷贝的平台客户ID
			ToLabID:me.ReasultInfo.LabID//拷贝到该平台客户ID
		};
			
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.ProgressWin.onShowInfo('机构字典树信息创建成功');
				setTimeout(function(){
					me.ProgressWin.onEnd();
				},20);
			}else{
				me.ProgressWin.onShowError('机构字典树信息创建失败');
			}
		});
	}
});