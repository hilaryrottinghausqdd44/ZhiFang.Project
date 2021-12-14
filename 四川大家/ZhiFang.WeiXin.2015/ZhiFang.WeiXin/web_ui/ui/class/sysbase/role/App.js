/**
 * 角色设置
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.role.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'角色设置',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.Grid.onSelectData();
					var DeveCode = record.get('RBACRole_DeveCode');
					if(DeveCode){
						me.Form.isShow(record.get(me.Grid.PKField));
					}else{
						me.Form.isEdit(record.get(me.Grid.PKField));
					}
				},null,200);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.Grid.onSelectData();
					var DeveCode = record.get('RBACRole_DeveCode');
					if(DeveCode){
						me.Form.isShow(record.get(me.Grid.PKField));
					}else{
						me.Form.isEdit(record.get(me.Grid.PKField));
					}
				},null,200);
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
		
		me.Grid = Ext.create('Shell.class.sysbase.role.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.role.Form', {
			region: 'east',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true
		});
		
		return [me.Grid,me.Form];
	}
});