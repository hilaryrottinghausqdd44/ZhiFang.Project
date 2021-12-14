/**
 * 角色用户维护
 * @author longfc
 * @version 2020-04-03
 */
Ext.define('Shell.class.sysbase.jurisdiction.roleuser.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'角色用户维护',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.RoleGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.RoleGrid.PKField);
					me.Grid.loadByRoleId(id);
				},null,200);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.RoleGrid.PKField);
					me.Grid.loadByRoleId(id);
				},null,200);
			},
			nodata:function(){
				me.Grid.clearData();
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
		
		me.RoleGrid = Ext.create('Shell.class.sysbase.role.SimpleGrid', {
			region: 'west',
			header: false,
			itemId: 'RoleGrid',
			split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.sysbase.jurisdiction.roleuser.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		
		return [me.RoleGrid,me.Grid];
	}
});