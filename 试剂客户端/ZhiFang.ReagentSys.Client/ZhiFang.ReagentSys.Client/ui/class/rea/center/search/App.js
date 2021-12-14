/**
 * 授权查询
 * @author liangyl	
 * @version 2018-05-08
 */
Ext.define('Shell.class.rea.center.search.App', {
	extend: 'Ext.panel.Panel',
	title: '授权查询',

	layout:'border',
    bodyPadding:1,
	border:false,
     /**获取角色模块服务*/
    selectUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL',
    /**选中的角色ID*/
	SelectRole:{},
	 /**原始模块数组*/
    ResourceModules:[],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
        me.RoleGrid.on({
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
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];

		me.RoleGrid = Ext.create('Shell.class.rea.center.register.RoleGrid', {
		    region:'center',
			itemId:'RoleGrid',
			header:false
//			split: true,
//			collapsible: true,
//			collapseMode:'mini'	
		});

		me.Tree = Ext.create('Shell.class.rea.center.register.ModuleCheckTree', {
			region:'east',
			itemId:'Tree',
			header:false,
			width:380,
			split: true,
			collapsible: true,
			collapseMode:'mini'	
		});
		return [me.RoleGrid,me.Tree];
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
				Id:record.get(me.RoleGrid.PKField),
				CName:record.get('RBACRole_CName')
			};
			me.Tree.enableControl();//启用所有的操作功能
			me.changeRoleModule(record.get(me.RoleGrid.PKField));
		},null,300);
	},
	
	
	onSaveRoleModule2:function(){
		var me =this;
		var nodes=me.Tree.getChecked(),
		    nodesLen = nodes.length,
		    list=[];
		 //选择的角色
	    var records = me.RoleGrid.getSelectionModel().getSelection();
	    if (records.length == 0) {
			JShell.Msg.error('请选择角色');
			return;
		}
	    var SelectRoleID=records[0].get('RBACEmpRoles_RBACRole_Id');
		for(var i =0 ;i<nodesLen;i++){
			var id = nodes[i].data.tid;
			if(!id) continue;
			var  obj = {
				IsUse:true,
				RBACModule:{
					Id:id,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				},
				RBACRole:{
					Id:SelectRoleID,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				}
			};
			list.push(obj);
		}
		return  list;
	}
});