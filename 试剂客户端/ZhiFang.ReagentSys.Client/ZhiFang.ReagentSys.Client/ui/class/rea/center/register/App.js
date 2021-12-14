 /**
 * 机构管理
 * @author liangyl	
 * @version 2018-05-08
 */
Ext.define('Shell.class.rea.center.register.App', {
	extend: 'Ext.panel.Panel',
	
	title: '机构管理',
	layout:'border',
    bodyPadding:1,
	border:false,
	
	addUrl:'/ReaManageService.svc/ST_UDTO_AddCenOrgOfInitialize',
     /**获取角色模块服务*/
    selectUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL',
   
	/**选中的角色ID*/
	SelectRole:{},
	 /**原始模块数组*/
    ResourceModules:[],
     /**显示某个角色分类,根据角色表SName过滤*/
    ROLETYPE:'',   
    hideTimes:2000,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar=me.getComponent('buttonsToolbar'),
		    btnsave=buttonsToolbar.getComponent('btnsave');
        me.RoleApp.RoleGrid.on({
			itemclick:function(v, record) {
				me.selectOneRow(record);
			},
			select:function(RowModel, record){
				me.selectOneRow(record);
			},
			nodata:function(){
				me.Tree.disableControl();
				btnsave.setDisabled(true);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
        //创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
	    me.CenOrgApp = Ext.create('Shell.class.rea.center.register.CenOrgApp', {
            region:'center',
			itemId:'CenOrgApp',
			header:false
	    });
		me.RoleApp = Ext.create('Shell.class.rea.center.register.RoleApp', {
		    region:'east',
			itemId:'RoleApp',width:280,	
			header:false,
			split: true,
			ROLETYPE:me.ROLETYPE,
			collapsible: true,
			collapseMode:'mini'	
		});

		me.Tree = Ext.create('Shell.class.rea.center.register.ModuleCheckTree', {
			region:'east',
			itemId:'Tree',
			header:false,
			split: true,
			collapsible: true,
			collapseMode:'mini'	
		});
		return [me.CenOrgApp,me.RoleApp,me.Tree];
	},
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this;
		var dockedItems = {
			xtype:'uxButtontoolbar',
			dock:'bottom',height:30	,
			itemId:'buttonsToolbar',	
			items:['->',{
				text:'注册',
				iconCls:'button-save',
				itemId:'btnsave',
				tooltip:'注册',
				margin:'0px 20px 1px 1px',
				handler:function(){
                    me.onSaveClick();
				}
			}]
		};
		return dockedItems;
	},
		/**获取角色模块*/
	changeRoleModule:function(roleId){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl;
			
		var fields = [
			'RBACRoleModule_Id',
			'RBACRoleModule_RBACModule_Id',
			'RBACRoleModule_RBACModule_CName',
			'RBACRoleModule_RBACRole_Id'
		];
		url += '?isPlanish=true&fields=' + fields.join(',');
		url += '&where=rbacrolemodule.RBACRole.Id=' + roleId;
		JShell.Server.get(url,function(data){
			if(data.success){
				me.changeChecked(data.value);
			}else{
				me.ResourceModules = [];
				me.Tree.onCancelCheck();
				me.Tree.showError(data.msg);
			}
		});
	},
	changeChecked:function(data){
		var me = this,
			list = (data || {}).list || [],
			len = list.length,
			ids = [];
			
		me.ResourceModules = [];
		for(var i=0;i<len;i++){
			var id = list[i].RBACRoleModule_RBACModule_Id;
			if(id){
				me.ResourceModules.push({
					Id:id,
					CName:list[i].RBACRoleModule_RBACModule_CName,
					RoleModuleId:list[i].RBACRoleModule_Id
				});
				ids.push(id);
			}
		}
		
		me.Tree.changeChecked(ids.join(','));
	},
	/**选一行处理*/
	selectOneRow:function(record){
		var me = this;
		me.Tree.disableControl();//禁用所有的操作功能
		JShell.Action.delay(function(){
			me.SelectRole = {
				Id:record.get(me.RoleApp.RoleGrid.PKField),
				CName:record.get('RBACRole_CName')
			};
			me.Tree.enableControl();//启用所有的操作功能
			me.changeRoleModule(record.get(me.RoleApp.RoleGrid.PKField));
		},null,300);
	},
	onSaveClick:function(){
		var me =this;
		var url = ( me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
		//校验
		if(!me.CenOrgApp.Form.getForm().isValid()) return;
		if(!me.CenOrgApp.AdminForm.getForm().isValid()) return;
		var IsExect=true;
		IsExect=me.RoleApp.onCheckRole();
		if(!IsExect) return;
		var values =me.CenOrgApp.AdminForm.getForm().getValues();		
		var isAccountCheck=true;
		//校验登录账号
		me.CenOrgApp.AdminForm.isAccountCheck(values.HREmployee_Account,function(data){
			if(data && data.value){
				var list =data.value.list,
				len=list.length;
				if(len>0){
					isAccountCheck=false;
					
				}
			}
		});
		if(!isAccountCheck){
			JShell.Msg.alert('登录账号【'+values.HREmployee_Account+'】该账号已存在,请输入其他账号',null,2000);
			return;
		} 
		//获取机构表
		var CenOrg = me.CenOrgApp.getCenOrgInfo();
		//获取人员信息
		var EmpInfo = me.CenOrgApp.getEmpInfo();
        //获取智方角色
		var roleIdStrOfZf = me.RoleApp.getRoleId();
		 //角色和模块
		var RoleModule = me.onSaveRoleModule2();
		
		var LabID = JShell.System.Cookie.get(JShell.System.Cookie.map.LABID) || -1;

		var params = Ext.encode({cenOrg:CenOrg,user:EmpInfo,moduleIdStr:RoleModule,labId:LabID,roleIdStrOfZf:roleIdStrOfZf});
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.fireEvent('save',me);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		if(me.hasLoadMask){me.body.mask(text);}//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.hasLoadMask){me.body.unmask();}//隐藏遮罩层
	},
	onSaveRoleModule2:function(){
		var me =this;
		var nodes=me.Tree.getChecked(),
		    nodesLen = nodes.length,
		    list=[];
	    var ModuleIds='';
		for(var i =0 ;i<nodesLen;i++){
			var id = nodes[i].data.tid;
			if(!id) continue;
			ModuleIds+=','+id;
	    }
		ModuleIds=0==ModuleIds.indexOf(",")?ModuleIds.substr(1):ModuleIds;
		return  ModuleIds;
	}
});