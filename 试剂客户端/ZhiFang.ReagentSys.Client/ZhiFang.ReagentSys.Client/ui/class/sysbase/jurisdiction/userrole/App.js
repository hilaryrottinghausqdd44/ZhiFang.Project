/**
 * 用户角色维护
 * @author Jcall
 * @version 2017-01-18
 */
Ext.define('Shell.class.sysbase.jurisdiction.userrole.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'用户角色维护',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Tree.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
					me.Grid.loadByDeptId(id);
				},null,200);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('tid');
					me.Grid.loadByDeptId(id);
				},null,200);
			}
		});
		
		me.Grid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.Grid.PKField);
					me.RoleCheckGrid.loadLinkByUserId(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.Grid.PKField);
					me.RoleCheckGrid.loadLinkByUserId(id);
				},null,500);
			},
			nodata:function(){
				me.RoleCheckGrid.clearData();
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
		
		me.Tree = Ext.create('Shell.class.sysbase.org.Tree', {
			region: 'west',
			width: 200,
			header: false,
			itemId: 'Tree',
			rootVisible:false,//是否显示根节点
			split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.sysbase.jurisdiction.userrole.UserSimpleGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.RoleCheckGrid = Ext.create('Shell.class.sysbase.jurisdiction.userrole.RoleCheckGrid', {
			region: 'east',
			header: false,
			itemId: 'RoleCheckGrid',
			split: true,
			collapsible: true
		});
		
		return [me.Tree,me.Grid,me.RoleCheckGrid];
	}
});