/**
 * 员工选择
 * @author longfc
 * @version 2020-03-04
 */
Ext.define('Shell.class.limp.sysbase.user.CheckApp',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'员工选择',
    
    width:600,
    height:400,
    
    /**是否单选*/
	checkOne:true,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Tree.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
					me.Grid.loadByDeptId(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('tid');
					me.Grid.loadByDeptId(id);
				},null,500);
			}
		});
		
		me.Grid.on({
			accept: function(p, record) {
				me.fireEvent('accept',me,record);
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.Tree = Ext.create('Shell.class.limp.sysbase.org.Tree', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'Tree',
			width:200
		});
		me.Grid = Ext.create('Shell.class.limp.sysbase.user.CheckGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			checkOne:me.checkOne,
			/**默认加载*/
			defaultLoad:false
		});
		
		return [me.Tree,me.Grid];
	}
});