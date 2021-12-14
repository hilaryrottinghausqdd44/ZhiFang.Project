/**
 * 选择模块/操作树
 * @author Jcall
 * @version 2019-12-02
 */
Ext.define('Shell.class.sysbase.role.right.CheckTree',{
    extend:'Shell.ux.tree.CheckPanel',
	
	title:'模块/操作',
	width:300,
	height:500,
	
	//获取数据服务路径
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACModuleToListTree?fields=RBACModule_DataTimeStamp',
	//获取模块操作列表服务
	getModuleOperListUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL',
	//获取角色功能权限列表
	getRoleModuleListUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL',
	//获取角色操作权限列表
	getRoleOperListUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleRightByHQL',
	
	//新增角色模块服务
    addRoleModuleUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACRoleModule',
    //删除角色模块服务
    delRoleModuleUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_DelRBACRoleModule',
    //新增角色操作服务
    addRoleOperUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACRoleRight',
    //删除角色操作服务
    delRoleOperUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_DelRBACRoleRight',
	
	//默认加载数据
	defaultLoad:true,
	//根节点
	root:{
		text:'所有模块',
		iconCls:'main-package-16',
		id:0,
		tid:0,
		leaf:false,
		expanded:false
	},
	
	//模块操作字段
	MODULE_OPER_FIELDS:['Id','CName','RBACModule_Id'],
	//模块操作列表
	MODULE_OPER_LIST:[],
	//角色模块权限字段
	ROLE_MODULE_FIELDS:['Id','RBACRole_Id','RBACModule_Id','RBACModule_CName'],
	//角色模块权限列表
	ROLE_MODULE_LIST:[],
	//角色操作权限字段
	ROLE_OPER_FIELDS:['Id','RBACRole_Id','RBACModuleOper_Id','RBACModuleOper_CName'],
	//角色操作权限列表
	ROLE_OPER_LIST:[],
	
	//角色ID
	roleId:null,
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.store.on({
			load:function(){
				//禁用所有的操作功能
				me.disableControl();
				//存在角色ID
				if(me.roleId){
					//默认勾选权限数据
					me.onDufaltCheck(me.roleId,function(){
						//启用所有的操作功能
						me.enableControl();
					});
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.addEvents('save');
		
		me.topToolbar = me.topToolbar || ['-','->',{
			iconCls:'button-right',
			tooltip:'<b>收缩面板</b>',
			handler:function(){me.collapse();}
		}];
		
		me.callParent(arguments);
	},
	//操作按钮栏
	createDockedItems:function(){
		var me = this;
		var dockedItems = me.callParent(arguments);
		
		dockedItems[0].items = dockedItems[0].items.concat(me.topToolbar);
		
		dockedItems.push(Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'bottomToolbar',
			items: ['->',{
				text:'保存',iconCls:'button-save',handler:function(){me.onSaveClick();}
			}]
		}));
		
		return dockedItems;
	},
	//数据更改
	changeData:function(data){
		var me = this;
		me._lastData = data;
    	var changeNode = function(node){
    		//图片地址处理
    		if(node['icon'] && node['icon'] != ''){
    			node['icon'] = JShell.System.Path.getModuleIconPathBySize(16) + "/" + node['icon'];
    		}
    		
    		//模块操作
    		for(var i in me.MODULE_OPER_LIST){
    			//模块操作挂在该模块下面
    			if(me.MODULE_OPER_LIST[i].RBACModule.Id == node.tid){
    				node.leaf = false;
    				node[me.defaultRootProperty].push({
    					isOper:true,
    					leaf:true,
    					icon:'oper.PNG',
    					tid:me.MODULE_OPER_LIST[i].Id,
    					text:me.MODULE_OPER_LIST[i].CName
    				});
    			}
    		}
    		
    		var children = node[me.defaultRootProperty];
    		if(children){
    			changeChildren(children);
    		}
    	};
    	
    	var changeChildren = function(children){
    		Ext.Array.each(children,changeNode);
    	};
    	
    	var children = data[me.defaultRootProperty];
    	changeChildren(children);
    	
    	return data;
	},
	//获取数据字段
	getStoreFields:function(){
		var me = this;
		
		var fields = [
			{name:'isOper',type:'bool'},//是否是操作
			{name:'checked',type:'bool'},
			{name:'text',type:'auto'},//默认的现实字段
			{name:'expanded',type:'auto'},//是否默认展开
			{name:'leaf',type:'auto'},//是否叶子节点
			{name:'icon',type:'auto'},//图标
			{name:'url',type:'auto'},//地址
			{name:'tid',type:'auto'}//默认ID号
		];
			
		return fields;
	},
	
	//加载模块操作列表
	loadModuleOperList:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.getModuleOperListUrl + '?fields=RBACModuleOper_' + me.MODULE_OPER_FIELDS.join(',RBACModuleOper_');
			
		JShell.Server.get(url,function(data){
			if(data.success){
				me.MODULE_OPER_LIST = (data.value || {}).list || [];
				callback();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	//加载角色模块权限列表
	loadRoleModuleList:function(roleId,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.getRoleModuleListUrl + '?fields=RBACRoleModule_' + me.ROLE_MODULE_FIELDS.join(',RBACRoleModule_');
		
		url += '&where=rbacrolemodule.RBACRole.Id=' + roleId;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				me.ROLE_MODULE_LIST = (data.value || {}).list || [];
				callback();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
		
	},
	//加载角色操作权限列表
	loadRoleOperList:function(roleId,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.getRoleOperListUrl + '?fields=RBACRoleRight_' + me.ROLE_OPER_FIELDS.join(',RBACRoleRight_');
		
		url += '&where=rbacroleright.RBACRole.Id=' + roleId;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				me.ROLE_OPER_LIST = (data.value || {}).list || [];
				callback();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	
	//点击刷新按钮
	onRefreshClick: function(){
		var me = this;
		//加载模块操作列表
		me.loadModuleOperList(function(){
			me.canLoad = true;
			me.store.load();
		});
	},
	//默认勾选权限数据
	onDufaltCheck:function(roleId,callback){
		var me = this;
		me.roleId = roleId;
		//禁用所有的操作功能
		me.disableControl();
		//加载角色模块权限列表
		me.loadRoleModuleList(roleId,function(){
			//加载角色操作权限列表
			me.loadRoleOperList(roleId,function(){
				//勾选结点
				me.onCheckNodes();
				//启用所有的操作功能
				me.enableControl();
				
				if(JShell.typeOf(callback) == 'function'){
					callback();
				}
			});
		});
	},
	//勾选结点
	onCheckNodes:function(){
		var me = this,
			ids = [];
			
		for(var i in me.ROLE_MODULE_LIST){
			ids.push(me.ROLE_MODULE_LIST[i].RBACModule.Id);
		}
		for(var i in me.ROLE_OPER_LIST){
			ids.push(me.ROLE_OPER_LIST[i].RBACModuleOper.Id);
		}
		
		me.changeChecked(ids);
	},
	//更改勾选
	changeChecked:function(ids){
		var me = this;
			len = ids.length;
		
		if(!me._lastData) return;
		
		me.disableControl();//禁用所有的操作功能
		
		var changeNode = function(node){
    		node['checked'] = false;//还原为不选中
    		node['expanded'] = false;//默认收起
    		
    		for(var i=0;i<len;i++){
    			if(node['tid'] == ids[i]){
    				node['checked'] = true;//选中
    				break;
    			}
    		}
    		
    		var children = node[me.defaultRootProperty];
    		if(children){
    			changeChildren(children);
    		}
    	};
    	
    	var changeChildren = function(children){
    		Ext.Array.each(children,changeNode);
    	};
    	
    	var root = me.root;
    	root.expanded = true;//默认展开
    	root.Tree = me._lastData.Tree;
    	
    	changeChildren(root.Tree);
    	me.setRootNode(root);
    	
    	//默认展开第一层首个勾选结点
    	var firstChecked = null;
    	var fChildren = me.getRootNode().childNodes;
    	for(var i in fChildren){
    		if(fChildren[i].data.checked){
    			firstChecked = fChildren[i];
    			break;
    		}
    	}
    	if(firstChecked){
    		me.expandNode(firstChecked);
    	}
    	
    	me.enableControl();//启用所有的操作功能
	},
	
	//保存
	onSaveClick:function(){
		var me = this,
			nodes = me.getChecked();
			
		//更改新增和删除模块/操作数据
		me.onChangeAddAndRemoveModules(nodes);
		
		//新增&删除角色模块/操作
		if(me.AddModules.length > 0 || me.RemoveModules.length > 0 || me.AddOpers.length > 0 || me.RemoveOpers.length > 0){
			me.doAddAndRomoveRoleModuleOrOper();
		}
	},
	//更改新增和删除模块/操作数据
	onChangeAddAndRemoveModules:function(nodes){
		var me = this,
			roleModuleList = Ext.clone(me.ROLE_MODULE_LIST),
			roleOperList = Ext.clone(me.ROLE_OPER_LIST);
			
		me.AddModules = [];
		me.RemoveModules = [];
		me.AddOpers = [];
		me.RemoveOpers = [];
		me.ErrorInfo = [];
		
		for(var i in nodes){
			var id = nodes[i].data.tid;
			if(!id) continue;
			
			var isOper = nodes[i].data.isOper;
			if(isOper){
				//操作ID在原始角色操作数组中是否存在
				var inOpers = false;
				for(var j in roleOperList){
					//操作ID存在，去掉该操作，不做增删操作
					if(roleOperList[j] && roleOperList[j].RBACModuleOper.Id == id){
						roleOperList[j] = null;
						inOpers = true;
						break;
					}
				}
				//操作ID不存在，新增
				if(!inOpers){
					me.AddOpers.push({
						Id:id,
						CName:nodes[i].data.text
					});
				}
			}else{
				//模块ID在原始角色模块数组中是否存在
				var inModules = false;
				for(var j in roleModuleList){
					//模块ID存在，去掉该模块，不做增删操作
					if(roleModuleList[j] && roleModuleList[j].RBACModule.Id == id){
						roleModuleList[j] = null;
						inModules = true;
						break;
					}
				}
				//模块ID不存在，新增
				if(!inModules){
					me.AddModules.push({
						Id:id,
						CName:nodes[i].data.text
					});
				}
			}
		}
		
		//剩下的都是需要删除的模块
		for(var i in roleModuleList){
			if(roleModuleList[i]){
				me.RemoveModules.push(roleModuleList[i]);
			}
		}
		//剩下的都是需要删除的操作
		for(var i in roleOperList){
			if(roleOperList[i]){
				me.RemoveOpers.push(roleOperList[i]);
			}
		}
	},
	
	//新增删除操作
	doAddAndRomoveRoleModuleOrOper:function(){
		var me = this;
			
		me.ErrorInfo = [];
		//禁用所有的操作功能
		me.disableControl();
		me.doAddRoleModule(0);
	},
	//新增角色模块
	doAddRoleModule:function(index){
		var me = this,
			AddModules = me.AddModules,
			len = AddModules.length;
			
		if(index >= len){
			me.doRomoveRoleModule(0);
		}else{
			var url = JShell.System.Path.ROOT + me.addRoleModuleUrl;
			var entity = {
				IsUse:1,
				RBACRole:{
					Id:me.roleId,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				},
				RBACModule:{
					Id:AddModules[index].Id,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				}
			};
			
			JShell.Server.post(url,Ext.encode({entity:entity}),function(data){
				if(!data.success){
					me.ErrorInfo.push(AddModules[index].CName + ' 新增错误');
				}
				me.doAddRoleModule(++index);
			});
		}
	},
	//删除角色模块
	doRomoveRoleModule:function(index){
		var me = this,
			RemoveModules = me.RemoveModules,
			len = RemoveModules.length;
			
		if(index >= len){
			me.doAddRoleOper(0);
		}else{
			var url = JShell.System.Path.ROOT + me.delRoleModuleUrl + '?id=' + RemoveModules[index].Id;
				
			JShell.Server.get(url,function(data){
				if(!data.success){
					me.ErrorInfo.push(RemoveModules[index].CName + ' 删除错误');
				}
				me.doRomoveRoleModule(++index);
			});
		}
	},
	//新增角色操作
	doAddRoleOper:function(index){
		var me = this,
			AddOpers = me.AddOpers,
			len = AddOpers.length;
			
		if(index >= len){
			me.doRomoveRoleOper(0);
		}else{
			var url = JShell.System.Path.ROOT + me.addRoleOperUrl;
			var entity = {
				IsUse:1,
				RBACRole:{
					Id:me.roleId,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				},
				RBACModuleOper:{
					Id:AddOpers[index].Id,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				}
			};
			
			JShell.Server.post(url,Ext.encode({entity:entity}),function(data){
				if(!data.success){
					me.ErrorInfo.push(AddOpers[index].CName + ' 新增错误');
				}
				me.doAddRoleOper(++index);
			});
		}
	},
	//删除角色操作
	doRomoveRoleOper:function(index){
		var me = this,
			RemoveOpers = me.RemoveOpers,
			len = RemoveOpers.length;
			
		if(index >= len){
			me.afterAddAndRomoveRoleModule();
		}else{
			var url = JShell.System.Path.ROOT + me.delRoleOperUrl + '?id=' + RemoveOpers[index].Id;
				
			JShell.Server.get(url,function(data){
				if(!data.success){
					me.ErrorInfo.push(RemoveOpers[index].CName + ' 删除错误');
				}
				me.doRomoveRoleOper(++index);
			});
		}
	},
	//保存完毕处理
	afterAddAndRomoveRoleModule:function(){
		var me = this;
		
		if(me.ErrorInfo.length > 0){
			JShell.Msg.error(me.ErrorInfo.join('</br>'));
		}else{
			//加载角色模块权限列表
			me.loadRoleModuleList(me.roleId,function(){
				//加载角色操作权限列表
				me.loadRoleOperList(me.roleId,function(){
					me.enableControl();//启用所有的操作功能
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
				});
			});
		}
		
		me.AddModules = [];
		me.RemoveModules = [];
		me.AddOpers = [];
		me.RemoveOpers = [];
		me.ErrorInfo = [];
	}
});