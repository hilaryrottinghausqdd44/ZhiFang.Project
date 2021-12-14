/**
 * 下级机构维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorgcondition.ChildrenApp', {
	extend: 'Ext.panel.Panel',
	title: '下级机构维护',

	layout:'border',
    bodyPadding:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		var CenOrgSimpleGrid = me.getComponent('CenOrgSimpleGrid');
		var ChildrenGrid = me.getComponent('ChildrenGrid');
		CenOrgSimpleGrid.on({
			select:function(rowModel,record){
				var id = record.get(CenOrgSimpleGrid.PKField);
				JShell.Action.delay(function(){
					ChildrenGrid.CenOrgId = id;
					ChildrenGrid.onSearch();
				});
			}
		});
		
		CenOrgSimpleGrid.store.on({
			load:function(store,records,successful){
				var number = (records || []).length;
				if(number == 0){
					ChildrenGrid.clearData();
				}
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
		
		items.push(Ext.create('Shell.class.rea.cenorg.SimpleGrid',{
			region:'west',
			split:true,collapsible:true,
			itemId:'CenOrgSimpleGrid',
			header:false
		}));
		items.push(Ext.create('Shell.class.rea.cenorgcondition.ChildrenGrid',{
			region:'center',
			itemId:'ChildrenGrid',
			header:false
		}));
		
		return items;
	}
});