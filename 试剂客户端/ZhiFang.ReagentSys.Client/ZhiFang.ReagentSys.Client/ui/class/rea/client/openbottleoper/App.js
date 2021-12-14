/**
 * 开瓶管理-开瓶管理
 * @author longfc	
 * @version 2021-01-25
 */
Ext.define('Shell.class.rea.client.openbottleoper.App',{
    extend:'Shell.ux.panel.AppPanel',
	
    title:'开瓶管理',
    
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
		
		me.Grid = Ext.create('Shell.class.rea.client.openbottleoper.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.rea.client.openbottleoper.Form', {
			region: 'east',
			header: true,
			itemId: 'Form',
			split: true,
			/**获取数据服务路径*/
			selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaOpenBottleOperDocById?isPlanish=true',
			collapsible: true
		});
		return [me.Grid,me.Form];
	}
});
	