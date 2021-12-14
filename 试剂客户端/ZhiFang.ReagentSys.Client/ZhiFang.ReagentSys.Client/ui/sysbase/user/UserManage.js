/**
 * 员工维护应用
 * @author Jcall
 * @version 2015-07-07
 */
Ext.define('Shell.sysbase.user.UserManage',{
	extend:'Ext.panel.Panel',
	
	title:'员工维护',
	layout:{type:'border'},
	bodyPadding:1,
	width:1200,
	height:600,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var OrgTree = me.getComponent('OrgTree'),
			UserList = me.getComponent('UserList'),
			InfoTab = me.getComponent('InfoTab');
			
		OrgTree.on({
			itemclick:function(model,record){
				Shell.util.Action.delay(function(){
					var id = record.get('tid');
					UserList.loadByOrgId(id);
				});
			}
		});
		UserList.on({
			select:function(view,record){
				Shell.util.Action.delay(function(){
					var id = record.get('HREmployee_Id');
					InfoTab.isEdit(id,{
						Id:record.get('HREmployee_HRDept_Id'),
						Name:record.get('HREmployee_HRDept_CName')
					});
				});
			}
		});
		InfoTab.on({
			save:function(){
				UserList.load(null,true);
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.items = me.createItems() || [];
		
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			SYS_USER_ORG_ID = Shell.util.SysInfo.getSYS_USER_ORG_ID(),
			SYS_USER_ORG_NAME = Shell.util.SysInfo.getSYS_USER_ORG_NAME(),
			items = [];
			
		items.push(Ext.create('Shell.sysbase.org.OrgTree',{
			itemId:'OrgTree',region:'west',canEidt:false,
			title:'部门树',header:false,width:200,
			rootOrgId:SYS_USER_ORG_ID || 0,
			rootOrgName:SYS_USER_ORG_NAME || '所有部门',
			split:true,collapsible:true,collapsed:false
		}));
		
		items.push(Ext.create('Shell.sysbase.user.UserList',{
			itemId:'UserList',region:'center',
			title:'用户列表',header:false
		}));
		
		items.push(Ext.create('Shell.sysbase.user.InfoTab',{
			itemId:'InfoTab',region:'east',
			title:'用户信息',header:false,width:440,
	        split:true,collapsible:true,collapsed:false,
	        rootOrgId:SYS_USER_ORG_ID || 0,
			rootOrgName:SYS_USER_ORG_NAME || '所有部门'
		}));
		
		return items;
	}
});