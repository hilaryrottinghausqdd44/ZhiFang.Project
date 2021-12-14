/**
 * 销售客户管理
 * @author Jcall
 * @version 2016-11-10
 */
Ext.define('Shell.class.wfm.client.user.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'销售客户管理',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.UserGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
				    me.Panel.Grid.SalesManID = record.get('RBACEmpRoles_HREmployee_Id');
					me.Panel.Grid.SalesManName = record.get('RBACEmpRoles_HREmployee_CName');

					var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
					var SalesManID=record.get('RBACEmpRoles_HREmployee_Id');
				    var IsAdmin =me.getRolesById(record.get('RBACEmpRoles_HREmployee_Id'));
					
					//管理员
					if(IsAdmin || SalesManID != userId ){
					    me.Panel.SplitGrid.SalesManID = record.get('RBACEmpRoles_HREmployee_Id');
					    me.Panel.SplitGrid.SalesManName = record.get('RBACEmpRoles_HREmployee_CName');
						me.Panel.SplitGrid.show();
						me.Panel.SplitGrid.onSearch();
						me.Panel.SplitGrid.ShowRolesText(IsAdmin);
					}else{
						me.Panel.SplitGrid.hide();
					}	
					
					me.Panel.Grid.ShowText(record.get('RBACEmpRoles_HREmployee_CName'),SalesManID,IsAdmin);
				   	me.Panel.Grid.IsAdmin=IsAdmin;

				    me.Panel.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
				    me.Panel.Grid.SalesManID = record.get('RBACEmpRoles_HREmployee_Id');
					me.Panel.Grid.SalesManName = record.get('RBACEmpRoles_HREmployee_CName');

					var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
					var SalesManID=record.get('RBACEmpRoles_HREmployee_Id');
				    var IsAdmin =me.getRolesById(record.get('RBACEmpRoles_HREmployee_Id'));
						
					//管理员
					if(IsAdmin || SalesManID != userId ){
					    me.Panel.SplitGrid.SalesManID = record.get('RBACEmpRoles_HREmployee_Id');
					    me.Panel.SplitGrid.SalesManName = record.get('RBACEmpRoles_HREmployee_CName');
						me.Panel.SplitGrid.show();
						me.Panel.SplitGrid.onSearch();
						me.Panel.SplitGrid.ShowRolesText(IsAdmin);
					}else{
						me.Panel.SplitGrid.hide();
					}	
					me.Panel.Grid.ShowText(record.get('RBACEmpRoles_HREmployee_CName'),SalesManID,IsAdmin);
				    me.Panel.Grid.IsAdmin=IsAdmin;
				    me.Panel.Grid.onSearch();
				},null,500);
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
		
		//销售人员列表
		me.UserGrid = Ext.create('Shell.class.sysbase.user.role.SimpleGrid', {
			region: 'west',
			header: false,
			itemId: 'UserGrid',
			split: true,
			collapsible: true,
			autoSelect: true,//默认选中数据
			ROLE_IDS:JShell.WFM.GUID.Role.Saler.GUID
		});
		//客户列表
		me.Panel = Ext.create('Shell.class.wfm.client.user.Panel', {
			region: 'center',
			header: false,
			itemId: 'Panel'
		});
		return [me.UserGrid,me.Panel];
	},
	/**判断登录者是否是管理员*/
	getRolesById:function(id){
		var me=this;
		var isAdmin=false;
		var arr=[];
		me.Panel.SplitGrid.getRBACEmpRoles(id,function(data){
			arr=[];
	    	if(data.value.list){
	    		for(var i=0;i<data.value.list.length;i++){
	    			var RBACRoleId=data.value.list[i].RBACEmpRoles_RBACRole_Id;
	    			if(RBACRoleId=="5153823459927439789"){//总经理
	    				arr.push(RBACRoleId);
	    			}
	    			if(RBACRoleId=="5579125521166282740"){//副总经理	
	    				arr.push(RBACRoleId);
	    			}
	    			if(RBACRoleId=="5027619732489863317"){//商务经理
	    				arr.push(RBACRoleId);
	    			}
	    		}
	    	}
	    });
	    if(arr.length>0){
	    	isAdmin=true;
	    	arr=[];
	    }
		return isAdmin;
	}

});
	