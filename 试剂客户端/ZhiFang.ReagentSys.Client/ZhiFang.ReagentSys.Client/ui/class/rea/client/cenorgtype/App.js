/**
 * 机构类型管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.client.cenorgtype.App', {
	extend: 'Ext.panel.Panel',
	
	title: '机构类型管理',
	layout:'border',
    bodyPadding:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		var Grid = me.getComponent('Grid');
		var Form = me.getComponent('Form');
		Grid.on({
			select:function(rowModel,record){
				var id = record.get(Grid.PKField);
				JShell.Action.delay(function(){
					Form.isEdit(id);
				});
			},
			addclick:function(){
				Form.isAdd();
			},
			editclick:function(panel,id){
				Form.isEdit(id);
			}  
		});
		
		Grid.store.on({
			load:function(store,records,successful){
				var number = (records || []).length;
				if(number == 0){
					Form.isShow();
				}
			}
		});
		
		Form.on({
			save:function(){
				Grid.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		me.items = me.createItems();

		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		items.push(Ext.create('Shell.class.rea.client.cenorgtype.Grid',{
			region:'center',
			itemId:'Grid',
			header:false
		}));
		items.push(Ext.create('Shell.class.rea.client.cenorgtype.Form',{
			region:'east',
			split:true,collapsible:true,
			itemId:'Form',
			header:false
		}));
		
		return items;
	}
});