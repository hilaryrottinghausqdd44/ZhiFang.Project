/**
 * 员工选择
 * @author
 * @version 2015-07-02
 */
Ext.define('Shell.class.qms.equip.templet.user.check.CheckApp',{
    extend:'Shell.ux.panel.AppPanel',
    title:'员工选择',
    width:600,
    height:400,
    PKCheckField:'HREmployee_Id',
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
        me.Grid.store.on({
			refresh : function(s, eOpts) {
				me.fireEvent('load',me.Grid);
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

		me.Tree = Ext.create('Shell.class.sysbase.org.Tree', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			collapseMode:'mini',
			itemId: 'Tree',
			width:200
		});
		me.Grid = Ext.create('Shell.class.qms.equip.templet.user.check.CheckGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			checkOne:false,
			/**默认加载*/
			defaultLoad:false
		});
		return [me.Tree,me.Grid];
	}
});