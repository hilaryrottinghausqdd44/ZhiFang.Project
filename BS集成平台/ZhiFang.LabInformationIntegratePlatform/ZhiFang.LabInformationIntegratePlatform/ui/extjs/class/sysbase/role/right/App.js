/**
 * 角色操作设置
 * @author Jcall
 * @version 2019-12-02
 */
Ext.define('Shell.class.sysbase.role.right.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'角色操作设置',
    
    //获取角色模块服务
    getRoleModuleListUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL',
    
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
	//创建内容
	createItems:function(){
		var me = this;
		//角色列表
		me.Grid = Ext.create('Shell.class.sysbase.role.SimpleGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		//角色权限操作勾选树
		me.Tree = Ext.create('Shell.class.sysbase.role.right.CheckTree', {
			region: 'east',
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true
		});
		
		return [me.Grid,me.Tree];
	},
	//选一行处理
	selectOneRow:function(record){
		var me = this;
		JShell.Action.delay(function(){
			me.Tree.onDufaltCheck(record.get(me.Grid.PKField));
		},null,300);
	}
});