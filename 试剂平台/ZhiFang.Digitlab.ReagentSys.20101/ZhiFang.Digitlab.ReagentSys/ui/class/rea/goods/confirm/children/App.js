/**
 * 下级机构试剂确认
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.goods.confirm.children.App', {
	extend: 'Ext.panel.Panel',
	title: '下级机构试剂确认',

	layout:'border',
    bodyPadding:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.CenOrg.on({
			select:function(rowModel,record){
				var CompId = record.get('CenOrgCondition_cenorg1_Id');
				me.Grid.defaultWhere = 'goods.Comp.Id=' + CompId+ ' and goods.CenOrg.Id=' + JShell.REA.System.CENORG_ID;
				JShell.Action.delay(function(){
					me.Grid.onSearch();
				});
			},
			nodata:function(){
				me.Grid.clearData();
			}
		});
		
		me.CenOrg.defaultWhere = 'cenorgcondition.cenorg2.Id=' + JShell.REA.System.CENORG_ID;
		me.CenOrg.onSearch();
	},
	initComponent: function() {
		var me = this;
		
		me.items = me.createItems();

		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		me.CenOrg = Ext.create('Shell.class.rea.cenorg.condition.parent.SimpleGrid',{
			region:'west',
			split:true,collapsible:true,
			itemId:'CenOrg',
			header:false,
			collapseMode:'mini'
		});
		
		me.Grid = Ext.create('Shell.class.rea.goods.confirm.children.Grid',{
			region:'center',
			itemId:'Grid',
			header:false,
			listeners:{
				toMaxClick:function(){
					me.CenOrg.collapse();
				},
				toMinClick:function(){
					me.CenOrg.expand();
				}
			}
		});
		
		items.push(me.CenOrg,me.Grid,me.Form);
		
		return items;
	}
});