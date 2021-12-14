/**
 * 模块设置
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.module.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'模块设置',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var Tree = me.getComponent('Tree');
		var Grid = me.getComponent('Grid');
		Tree.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
					var name = record.get('text');
					Grid.loadByParentId(id,name);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('tid');
					var name = record.get('text');
					Grid.loadByParentId(id,name);
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
		
		me.Tree = Ext.create('Shell.class.sysbase.module.Tree', {
			region: 'west',
			width: 200,
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.sysbase.module.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		
		return [me.Tree,me.Grid];
	}
});
	