/**
 * 附件管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.attachment.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'附件管理',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
//		var Grid = me.getComponent('Grid');
//		var Form = me.getComponent('Form');
//		Grid.on({
//			itemclick:function(v, record) {
//				JShell.Action.delay(function(){
//					var id = record.get(Grid.PKField);
//					Form.isEdit(id);
//				},null,500);
//			},
//			select:function(RowModel, record){
//				JShell.Action.delay(function(){
//					var id = record.get(Grid.PKField);
//					Form.isEdit(id);
//				},null,500);
//			}
//		});
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.Tree = Ext.create('Shell.class.sysbase.attachment.Grid', {
			region: 'west',
			width: 200,
			header: false,
			itemId: 'Grid',
			split: true,
			collapsible: true
		});
//		me.Grid = Ext.create('Shell.class.sysbase.attachment.Form', {
//			region: 'center',
//			header: false,
//			itemId: 'Form'
//		});
		
		return [me.Grid];
	}
});
	