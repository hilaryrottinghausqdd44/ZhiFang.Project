/**
 * 凝集规则明细字典信息
 * @author longfc
 * @version 2020-07-06
 */
Ext.define('Shell.class.sysbase.aggluitem.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'凝集规则明细字典信息',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.Form.isEdit(record.get(me.Grid.PKField));
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.Form.isEdit(record.get(me.Grid.PKField));
				},null,500);
			},
			addclick:function(){
				me.Form.isAdd();
			},
			editclick:function(p,record){
				me.Form.isEdit(record.get(me.Grid.PKField));
			}
		});
		me.Form.on({
			save:function(p,id){
				me.Grid.onSearch(id);
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
		
		me.Grid = Ext.create('Shell.class.sysbase.aggluitem.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.aggluitem.Form', {
			region: 'east',
			header: true,
			itemId: 'Form',
			split: true,
			collapsible: false
		});
		
		return [me.Grid,me.Form];
	}
});
	