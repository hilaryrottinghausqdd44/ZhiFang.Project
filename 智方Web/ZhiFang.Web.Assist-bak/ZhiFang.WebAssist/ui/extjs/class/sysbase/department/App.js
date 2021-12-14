/**
 * 科室信息维护
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.department.App',{
    extend: 'Shell.ux.panel.AppPanel',
    
    /**标题*/
    title: '科室信息维护',
    width: 1280,
    height: 800,
    
    afterRender: function() {
    	var me = this;
    	me.callParent(arguments);
		me.Tree.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
					var name = record.get('text');
					me.Grid.loadByParentId(id,name);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('tid');
					var name = record.get('text');
					me.Grid.loadByParentId(id,name);
				},null,500);
			}
		});
    	me.Grid.on({
    		itemclick: function(v, record) {
    			//me.loadForm(record);
    		},
    		select: function(RowModel, record) {
    			//me.loadForm(record);
    		},
    		addclick: function(p) {
				/* me.Form.ParentID = me.Grid.ParentID ;
				me.Form.ParentName = me.Grid.ParentName ;
    			me.Form.isAdd(); */
    		},
			nodata: function(p) {
    			//me.Form.isShow();
    		}
    	});
    	/* me.Form.on({
    		save: function(p, id) {
    			me.Grid.onSearch();
    		}
    	}); */
    
    	//me.Grid.onSearch();
    },
    loadForm: function(record) {
    	var me = this;
    	JShell.Action.delay(function() {
    		var id = record.get(me.Grid.PKField);
    		me.Form.ParentID = me.Grid.ParentID ;
    		me.Form.ParentName = me.Grid.ParentName ;
    		me.Form.isEdit(id);
    	}, null, 500);
    },
    initComponent: function() {
    	var me = this;
    	me.items = me.items || me.createItems();
    	me.callParent(arguments);
    },
    createItems: function() {
    	var me = this;
		me.Tree = Ext.create('Shell.class.sysbase.department.Tree', {
			region: 'west',
			width: 280,
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true
		});
    	me.Grid = Ext.create('Shell.class.sysbase.department.Grid', {
    		region: 'center',
    		header: false,
    		itemId: 'Grid'
    	});
    	me.Form = Ext.create('Shell.class.sysbase.department.Form', {
    		region: 'east',
    		header: true,
    		itemId: 'Form',
    		split: true,
    		collapsible: false,
    		width: 280
    	});
    
    	return [me.Tree,me.Grid];//, me.Form
    }
});