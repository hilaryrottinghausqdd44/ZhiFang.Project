/**
 * 送检单位合同和合同明细
 */
Ext.define('Shell.class.pki.contractprice.AppTab',{
    extend:'Ext.tab.Panel',
    title:'送检单位合同和合同明细',
    layout:'border',
    bodyPadding:1,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		items.push(Ext.create('Shell.class.pki.dealercontract.Grid',{
			region:'center',
			itemId:'ContractPriceGrid',
			AddType:'AddDunitContractPrice',
			/**是否带功能按钮*/
			hasButtons:true,
			title:'合同维护',
			/**是否带修改价格功能*/
			canEditPrice:false,
			header:false
		}));
		items.push(Ext.create('Shell.class.pki.dunitdealer.DUnitDealerRelationGrid',{
			region:'center',
			split:true,collapsible:true,
			itemId:'DUnitDealerRelationGrid',
			SearchToolbarType: "SearchToolbar",
//			LaboratoryId:'',
			title:'合同明细',
			header:false
		}));
		return items;
	}
});