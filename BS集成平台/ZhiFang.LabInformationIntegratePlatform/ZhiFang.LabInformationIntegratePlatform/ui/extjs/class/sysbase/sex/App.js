/**
 * 性别维护
 * @author Jcall
 * @version 2018-09-14
 */
Ext.define('Shell.class.sysbase.sex.App', {
	extend:'Ext.panel.Panel',
	title:'性别维护',
	bodyPadding:1,
	layout:'border',
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.Grid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.Form.isEdit(record.get(me.Grid.PKField));
				},null,200);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.Form.isEdit(record.get(me.Grid.PKField));
				},null,200);
			},
			addclick:function(){
				me.Form.isAdd();
			},
			nodata:function(){
				me.Form.clearData();
			}
		});
		me.Form.on({
		    save: function (p, id) {
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
		
		me.Grid = Ext.create('Shell.class.sysbase.sex.Grid', {
			region:'center',
			header:false,
			split:true,
			collapsible:true
		});
		me.Form = Ext.create('Shell.class.sysbase.sex.Form', {
			region:'east',
			header:false,
			split:true,
			collapsible:true
		});
			
		return [me.Grid,me.Form];
	}
});