/**
 * 出入库历史查询
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.stock.HistoryApp', {
	extend: 'Ext.panel.Panel',
	title: '出入库历史查询',
	
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
			Ext.create('Shell.class.rea.stock.HistoryGrid',Ext.apply({
				region:'center',itemId:'HistoryGrid',header:false
			},me.ShowGridConfig)),
			Ext.create('Shell.class.rea.stock.HistoryInfo',{
				region:'east',itemId:'HistoryInfo',header:false,
				split:true,collapsible:true
			})
		);
		
		return items;
	},
	initListeners:function(){
		var me = this;
		var HistoryGrid = me.getComponent('HistoryGrid');
		var HistoryInfo = me.getComponent('HistoryInfo');
		
		HistoryGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					HistoryInfo.initData({
						TestEquipName:record.get('CenQtyDtlTempHistory_TestEquipName'),
						GoodsName:record.get('CenQtyDtlTempHistory_GoodsName'),
						ProdGoodsNo:record.get('CenQtyDtlTempHistory_ProdGoodsNo'),
						UnitMemo:record.get('CenQtyDtlTempHistory_UnitMemo'),
						LotNo:record.get('CenQtyDtlTempHistory_LotNo'),
						InvalidDate:JShell.Date.toString(record.get('CenQtyDtlTempHistory_InvalidDate'),true),
						GoodsQty:record.get('CenQtyDtlTempHistory_GoodsQty'),
						ProdOrgName:record.get('CenQtyDtlTempHistory_ProdOrgName'),
						CompanyName:record.get('CenQtyDtlTempHistory_CompanyName'),
						LabName:record.get('CenQtyDtlTempHistory_LabName'),
						GoodsUnit:record.get('CenQtyDtlTempHistory_GoodsUnit')
					});
				},null,200);
			}
		});
	}
});