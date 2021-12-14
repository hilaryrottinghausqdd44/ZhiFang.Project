/**
 * 供货管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.App', {
	extend: 'Ext.panel.Panel',
	title: '供货管理',

	layout:'border',
    bodyPadding:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.initListeners();
		
		var DocPanel = me.getComponent('DocPanel');
		var DocGrid = DocPanel.getComponent('DocGrid');
		var cenOrgId = JShell.REA.System.CENORG_ID;
		if(cenOrgId){
			DocGrid.defaultWhere = 'bmscensaledoc.Comp.Id=' + cenOrgId;
		}
		DocGrid.onSearch();
	},
	initComponent: function() {
		var me = this;
		
		me.items = me.createItems();

		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		items.push({
			region:'north',header:false,
			split:true,collapsible:true,
			itemId:'DocPanel',height:300,
			layout:'border',border:false,
			items:[Ext.create('Shell.class.rea.sale.DocGrid',{
				region:'center',itemId:'DocGrid',header:false
			}),Ext.create('Shell.class.rea.sale.DocForm',{
				region:'east',itemId:'DocForm',header:false,
				split:true,collapsible:true
			})]
		});
		items.push({
			region:'center',header:false,border:false,
			itemId:'DtlPanel',layout:'border',
			items:[Ext.create('Shell.class.rea.sale.DtlGrid',{
				region:'center',itemId:'DtlGrid',header:false
			}),Ext.create('Shell.class.rea.sale.DtlBarcodeForm',{
				region:'east',itemId:'DtlBarcodeForm',header:false,
				split:true,collapsible:true
			}),Ext.create('Shell.class.rea.sale.DtlBarcodeGrid',{
				region:'east',itemId:'DtlBarcodeGrid',header:false,
				split:true,collapsible:true
			})]
		});
		
		return items;
	},
	initListeners:function(){
		var me = this;
		var DocPanel = me.getComponent('DocPanel');
		var DocGrid = DocPanel.getComponent('DocGrid');
		var DocForm = DocPanel.getComponent('DocForm');
		
		var DtlPanel = me.getComponent('DtlPanel');
		var DtlGrid = DtlPanel.getComponent('DtlGrid');
		var DtlBarcodeForm = DtlPanel.getComponent('DtlBarcodeForm');
		var DtlBarcodeGrid = DtlPanel.getComponent('DtlBarcodeGrid');
		
		DocGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					var id = record.get(DocGrid.PKField);
					var IOFlag = record.get('BmsCenSaleDoc_IOFlag');
					DtlGrid.defaultWhere = 'bmscensaledtl.BmsCenSaleDoc.Id=' + id;
					
					if(IOFlag == '1'){
						DocForm.isShow(id);
					}else{
						DocForm.isEdit(id);
					}
					
					DtlBarcodeForm.clearData();
					DtlBarcodeGrid.clearData();
					DtlGrid.onSearch();
				},null,200);
			},
			addclick:function(p){
				DocForm.isAdd();
			},
			editclick:function(p,record){
				DocForm.isEdit(record.get(p.PKField));
			}
		});
		
		DocForm.on({
			save:function(){
				DocGrid.onSearch();
			}
		});
		
		DtlGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					DtlBarcodeForm.initData({
						Name:record.get('BmsCenSaleDtl_GoodsName'),
						Qty:record.get('BmsCenSaleDtl_GoodsQty'),
						Unit:record.get('BmsCenSaleDtl_GoodsUnit')
					});
					DtlBarcodeGrid.loadBySaleDtlId(record.get(DtlGrid.PKField));
				},null,200);
			}
		});
		DtlBarcodeGrid.store.on({
			load:function(store,records,successful){
				var number = (records || []).length;
				DtlBarcodeForm.changeBarcodeNumber(number);
			}
		});
		DtlBarcodeGrid.on({
			BarcodeNumberChanged:function(p,number){
				DtlBarcodeForm.changeBarcodeNumber(number);
			}
		});
	}
});