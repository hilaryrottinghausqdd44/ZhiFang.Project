/**
 * 资质证件查看
 * @author longfc
 * @version 2017-07-14
 */
Ext.define('Shell.class.rea.cenorg.qualification.search.App', {
	extend: 'Ext.panel.Panel',
	title: '资质证件查看',

	layout:'border',
    bodyPadding:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.CenOrgGrid.on({
			select:function(rowModel,record){
				var id = record.get('CenOrgCondition_cenorg1_Id');
				//供应商=供应商 and （实验室ID=本机构ID or 实验室ID=null）
				JShell.Action.delay(function(){
					me.QualificationGrid.defaultWhere = 'goodsqualification.Visible=1 and goodsqualification.Comp.Id=' + id + 
						' and (goodsqualification.CenOrg.Id=' + JShell.REA.System.CENORG_ID+" or goodsqualification.CenOrg.Id is null)";
					me.QualificationGrid.onSearch();
				});
			},
			nodata:function(){
				me.QualificationGrid.clearData();
			}
		});
		
		
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
		
		me.CenOrgGrid = Ext.create('Shell.class.rea.cenorg.condition.parent.SimpleGrid',{
			region:'west',
			split:true,collapsible:true,
			itemId:'CenOrgGrid',
			header:false,
			collapseMode:'mini',
			defaultWhere:'cenorgcondition.cenorg2.Id=' + JShell.REA.System.CENORG_ID
		});
		me.QualificationGrid = Ext.create('Shell.class.rea.cenorg.qualification.search.Grid',{
			region:'center',
			itemId:'QualificationGrid',
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
		
		return [me.CenOrgGrid,me.QualificationGrid];
	}
});