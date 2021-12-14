/**
 * 下级机构试剂维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.goods.ChildrenApp', {
	extend: 'Ext.panel.Panel',
	title: '下级机构试剂维护',

	layout:'border',
    bodyPadding:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.CenOrgGrid.on({
			select:function(rowModel,record){
				var id = record.get('CenOrgCondition_cenorg2_Id');
				var Name = record.get('CenOrgCondition_cenorg2_CName');
				JShell.Action.delay(function(){
					me.GoodsGrid.Lab = {Id:id,Name:Name};//机构信息
					me.GoodsGrid.CenOrgId = id;//机构ID
					me.GoodsGrid.CompId = JShell.REA.System.CENORG_ID;//供应商ID
					me.GoodsGrid.defaultWhere = 'goods.CenOrg.Id=' + id + 
						' and goods.Comp.Id=' + JShell.REA.System.CENORG_ID;
					me.GoodsGrid.onSearch();
				});
			},
			nodata:function(){
				me.GoodsGrid.clearData();
			}
		});
		
		me.CenOrgGrid.defaultWhere = 'cenorgcondition.cenorg1.Id=' + JShell.REA.System.CENORG_ID;
		me.CenOrgGrid.onSearch();
	},
	initComponent: function() {
		var me = this;
		
		me.items = me.createItems();

		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		me.CenOrgGrid = Ext.create('Shell.class.rea.cenorg.condition.children.SimpleGrid',{
			region:'west',
			split:true,collapsible:true,
			itemId:'CenOrgGrid',
			header:false,
			collapseMode:'mini'
		});
		me.GoodsGrid = Ext.create('Shell.class.rea.goods.ChildrenGrid',{
			region:'center',
			itemId:'GoodsGrid',
			autoSelect:false,
			header:false,
			listeners:{
				toMaxClick:function(){
					me.CenOrgGrid.collapse();
				},
				toMinClick:function(){
					me.CenOrgGrid.expand();
				}
			}
		});
		
		return [me.CenOrgGrid,me.GoodsGrid];
	}
});