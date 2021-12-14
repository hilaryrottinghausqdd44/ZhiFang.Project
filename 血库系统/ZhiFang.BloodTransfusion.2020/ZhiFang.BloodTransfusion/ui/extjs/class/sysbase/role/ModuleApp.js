/**
 * 角色模块设置
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.role.ModuleApp',{
    extend:'Shell.ux.panel.AppPanel',
    title:'角色模块设置',
    
    /**获取角色模块服务*/
    selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL',
    /**新增角色模块服务*/
    addUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACRoleModule',
    /**删除角色模块服务*/
    delUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_DelRBACRoleModule',
    
    /**选中的角色ID*/
	SelectRole:{},
    /**原始模块数组*/
    ResourceModules:[],
    /**需要新增模块数组*/
    AddModules:[],
    /**需要删除模块数组*/
    RemoveModules:[],
    /**错误信息*/
    ErrorInfo:[],
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
			itemclick:function(v, record) {
				me.selectOneRow(record);
			},
			select:function(RowModel, record){
				me.selectOneRow(record);
			},
			nodata:function(){
				me.Tree.disableControl();
			}
		});
		
		me.Tree.on({
			save:function(p,nodes){
				me.onSaveRoleModule(nodes);
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.Grid = Ext.create('Shell.class.sysbase.role.SimpleGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Tree = Ext.create('Shell.class.sysbase.role.ModuleCheckTree', {
			region: 'east',
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true
		});
		
		return [me.Grid,me.Tree];
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
	onSaveRoleModule:function(nodes){
		var me = this;
		
		//更改新增和删除模块数据
		me.onChangeAddAndRemoveModules(nodes);
		
		JShell.Msg.log('角色：' + me.SelectRole.CName);
		for(var i=0;i<me.AddModules.length;i++){
			JShell.Msg.log('新增模块：' + me.AddModules[i].CName);
		}
		for(var i=0;i<me.RemoveModules.length;i++){
			JShell.Msg.log('删除模块：' + me.RemoveModules[i].CName);
		}
		
		//新增&删除操作
		if(me.AddModules.length > 0 || me.RemoveModules.length > 0){
			me.doAddAndRomoveRoleModule();
		}
	},
	/**更改新增和删除模块数据*/
	onChangeAddAndRemoveModules:function(nodes){
		var me = this,
			nodesLen = nodes.length,
			ResourceModules = Ext.clone(me.ResourceModules),
			resourceLen = ResourceModules.length;
			
		me.AddModules = [];
		me.RemoveModules = [];
		me.ErrorInfo = [];
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		for(var i=0;i<nodesLen;i++){
			var id = nodes[i].data.tid;
			if(!id) continue;
			
			var inArr = false;//模块ID在原始模块数组中是否存在
			for(var j=0;j<resourceLen;j++){
				//模块ID存在，去掉该模块，不做增删操作
				if(ResourceModules[j] && ResourceModules[j].Id == id){
					ResourceModules[j] = null;
					inArr = true;
					break;
				}
			}
			//模块ID不存在，新增
			if(!inArr){
				me.AddModules.push({
					Id:id,
					CName:nodes[i].data.text,
					DataTimeStamp:strDataTimeStamp//nodes[i].data.DataTimeStamp
				});
			}
		}
		
		//剩下的都是需要删除的模块
		for(var i=0;i<resourceLen;i++){
			if(ResourceModules[i]){
				me.RemoveModules.push(ResourceModules[i]);
			}
		}
	},
	/**新增删除操作*/
	doAddAndRomoveRoleModule:function(){
		var me = this;
			
		me.ErrorInfo = [];
		me.doAddRoleModule(0);
	},
	/**新增角色模块*/
	doAddRoleModule:function(index){
		var me = this,
			AddModules = me.AddModules,
			len = AddModules.length;
			
		if(index >= len){
			me.doRomoveRoleModule(0);
		}else{
			var url = JShell.System.Path.ROOT + me.addUrl;
			var entity = {
				IsUse:true,
				RBACModule:{
					Id:AddModules[index].Id,
					DataTimeStamp:AddModules[index].DataTimeStamp.split(',')
				},
				RBACRole:{
					Id:me.SelectRole.Id,
					DataTimeStamp:me.SelectRole.DataTimeStamp.split(',')
				}
			};
			var params = Ext.encode({entity:entity});
			
			JShell.Server.post(url,params,function(data){
				if(!data.success){
					me.ErrorInfo.push(AddModules[index].CName + ' 新增错误');
				}
				me.doAddRoleModule(++index);
			});
		}
	},
	/**删除角色模块*/
	doRomoveRoleModule:function(index){
		var me = this,
			RemoveModules = me.RemoveModules,
			len = RemoveModules.length;
			
		if(index >= len){
			me.afterAddAndRomoveRoleModule();
		}else{
			var url = JShell.System.Path.ROOT + me.delUrl + '?id=' + RemoveModules[index].RoleModuleId;
				
			JShell.Server.get(url,function(data){
				if(!data.success){
					me.ErrorInfo.push(RemoveModules[index].CName + ' 删除错误');
				}
				me.doRomoveRoleModule(++index);
			});
		}
	},
	afterAddAndRomoveRoleModule:function(){
		var me = this;
		if(me.ErrorInfo.length > 0){
			JShell.Msg.error(me.ErrorInfo.join('</br>'));
		}else{
			JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
		}
		//var record = me.Grid.store.findRecord(me.Grid.PKField, me.SelectRole.Id);
		//me.selectOneRow(record);
		
		me.AddModules = [];
		me.RemoveModules = [];
		me.ErrorInfo = [];
	},
	/**选一行处理*/
	selectOneRow:function(record){
		var me = this;
		me.Tree.disableControl();//禁用所有的操作功能
		JShell.Action.delay(function(){
			me.SelectRole = {
				Id:record.get(me.Grid.PKField),
				CName:record.get('RBACRole_CName'),
				DataTimeStamp:record.get('RBACRole_DataTimeStamp')
			};
			me.Tree.enableControl();//启用所有的操作功能
			me.changeRoleModule(record.get(me.Grid.PKField));
		},null,300);
	}
});
	