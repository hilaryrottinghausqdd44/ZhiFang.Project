/**
 * 部门维护应用
 * @author Jcall
 * @version 2015-07-07
 */
Ext.define('Shell.sysbase.org.OrgManage',{
	extend:'Ext.panel.Panel',
	
	/**按钮栏位置top/bottom*/
	toolbarDock:'top',
	
	title:'部门维护',
	layout:{type:'border'},
	bodyPadding:1,
	width:1200,
	height:600,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var OrgTree = me.getComponent('OrgTree'),
			OrgForm = me.getComponent('OrgForm');
			
		OrgTree.on({
			addClick:function(tree,record){
				console.log('新增:' + record.get('text'));
				OrgForm.isAdd({
					Id:record.get('tid'),
					Name:record.get('text')
				});
			},
			itemclick:function(model,record){
				Shell.util.Action.delay(function(){
					var id = record.get('tid');
					var text = record.get('text');
					console.log('选中【' + text + '】[' + id + ']');
					
					if(id == OrgTree.rootOrgId){
						OrgForm.isShow(id,{
							Id:record.get('pid'),
							Name:'没有上级部门'
						});
					}else{
						OrgForm.isEdit(id,{
							Id:record.parentNode.get('tid'),
							Name:record.parentNode.get('text')
						});
					}
				});
			}
		});
		OrgForm.on({
			save:function(){
				OrgTree.load();
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
			itemId:'OrgTree',region:'center',
			title:'部门树',header:false,
			rootOrgId:SYS_USER_ORG_ID || 0,
			rootOrgName:SYS_USER_ORG_NAME || '所有部门',
			toolbarDock:me.toolbarDock
		}));
		
		items.push(Ext.create('Shell.sysbase.org.OrgForm',{
			itemId:'OrgForm',region:'east',
			title:'部门信息',header:false,width:440,
	        split:true,collapsible:true,collapsed:false,
	        rootOrgId:SYS_USER_ORG_ID || 0,
			rootOrgName:SYS_USER_ORG_NAME || '所有部门',
	        toolbarDock:me.toolbarDock
		}));
		
		return items;
	}
});