/**
 * 学历维护
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.educationlevel.App', {
	extend:'Ext.panel.Panel',
	bodyPadding:1,
	title:'学历维护',
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
		    	me.Grid.onSearch();
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
		
		me.Grid = Ext.create('Shell.class.sysbase.educationlevel.Grid', {
			region:'center',
			header:false
		});
		me.Form = Ext.create('Shell.class.sysbase.educationlevel.Form', {
			region:'east',
			header:false,
			split:true,
			collapsible:true,
			width:250
		});
			
		return [me.Grid,me.Form];
	}
});