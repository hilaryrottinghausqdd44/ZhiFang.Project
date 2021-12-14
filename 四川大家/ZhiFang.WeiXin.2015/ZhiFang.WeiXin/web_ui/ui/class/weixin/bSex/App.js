/**
 * 实验室性别维护
 * @author GHX
 * @version 2021-02-03
 */
Ext.define('Shell.class.weixin.bSex.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'实验室性别维护',
    
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
			},
			nodata:function(p){
				me.Form.clearData();
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
		
		me.Grid = Ext.create('Shell.class.weixin.bSex.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.weixin.bSex.Form', {
			region: 'east',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true
		});
		
		return [me.Grid,me.Form];
	}
});