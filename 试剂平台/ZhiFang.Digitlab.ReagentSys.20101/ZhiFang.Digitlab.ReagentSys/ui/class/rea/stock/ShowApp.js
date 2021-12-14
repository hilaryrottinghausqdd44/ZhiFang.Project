/**
 * 库存管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.stock.ShowApp', {
	extend: 'Ext.panel.Panel',
	title: '库存管理',

	layout:'border',
    bodyPadding:1,
    
    /**默认数据条件*/
	defaultWhere:'',
	/**机构类型*/
    ORGTYPE:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//联动监听
		me.initListeners();
	},
	initComponent: function() {
		var me = this;
		
		if(me.ORGTYPE){
			var type = me.ORGTYPE.toLocaleUpperCase();
			if(type == 'COMP'){
				me.ShowGridConfig = {
					showComp:false,
					defaultCompValue:{Id:JShell.REA.System.CENORG_ID}
				};
			}else if(type == 'PROD'){
				me.ShowGridConfig = {
					showProd:false,
					defaultProdValue:{Id:JShell.REA.System.CENORG_ID}
				};
			}else if(type == 'LAB'){
				me.ShowGridConfig = {
					showLab:false,
					defaultLabValue:{Id:JShell.REA.System.CENORG_ID}
				};
			}
		}
		
		me.items = me.createItems();

		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		items.push(
			Ext.create('Shell.class.rea.stock.ShowGrid',Ext.apply({
				region:'center',itemId:'ShowGrid',header:false
			},me.ShowGridConfig)),
			Ext.create('Shell.class.rea.stock.ShowInfo',{
				region:'east',itemId:'ShowInfo',header:false,
				split:true,collapsible:true
			})
		);
		
		return items;
	},
	initListeners:function(){
		var me = this;
		var ShowGrid = me.getComponent('ShowGrid');
		var ShowInfo = me.getComponent('ShowInfo');
		
		ShowGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					ShowInfo.initData({
						GoodsName:record.get('CenQtyDtl_GoodsName'),
						ProdGoodsNo:record.get('CenQtyDtl_ProdGoodsNo'),
						UnitMemo:record.get('CenQtyDtl_UnitMemo'),
						LotNo:record.get('CenQtyDtl_LotNo'),
						InvalidDate:JShell.Date.toString(record.get('CenQtyDtl_InvalidDate'),true),
						GoodsQty:record.get('CenQtyDtl_GoodsQty'),
						ProdOrgName:record.get('CenQtyDtl_ProdOrgName'),
						CompanyName:record.get('CenQtyDtl_CompanyName'),
						LabName:record.get('CenQtyDtl_LabName'),
						GoodsUnit:record.get('CenQtyDtl_GoodsUnit')
					});
				},null,200);
			}
		});
	}
});