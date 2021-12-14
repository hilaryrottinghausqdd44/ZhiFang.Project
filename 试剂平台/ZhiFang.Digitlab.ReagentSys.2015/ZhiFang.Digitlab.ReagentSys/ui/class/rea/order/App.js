/**
 * 订货管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.order.App', {
	extend: 'Ext.panel.Panel',
	title: '订货管理',

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
			DocGrid.CenOrgId = cenOrgId;
			me.DocGrid.defaultWhere = me.DocGrid.defaultWhere || "";
			if(me.DocGrid.defaultWhere){
				me.DocGrid.defaultWhere += ' and '; 
			}
			DocGrid.defaultWhere += 'bmscenorderdoc.Lab.Id=' + cenOrgId;
		}
		DocGrid.onSearch();
		
		setTimeout(function(){
			DocGrid.focus();
		},2000);
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
			items:[Ext.create('Shell.class.rea.order.DocGrid',{
				region:'center',itemId:'DocGrid',header:false
			}),Ext.create('Shell.class.rea.order.DocForm',{
				region:'east',itemId:'DocForm',header:false,
				split:true,collapsible:true
			})]
		});
		items.push({
			region:'center',header:false,border:false,
			itemId:'DtlPanel',layout:'border',
			items:[Ext.create('Shell.class.rea.order.DtlGrid',{
				region:'center',itemId:'DtlGrid',header:false
			}),Ext.create('Shell.class.rea.order.DtlInfo',{
				region:'east',itemId:'DtlInfo',header:false,
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
		var DtlInfo = DtlPanel.getComponent('DtlInfo');
		
		DocGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					var id = record.get(DocGrid.PKField);
					var Status = record.get('BmsCenOrderDoc_Status') + '';
					DtlGrid.CompId = record.get('BmsCenOrderDoc_Comp_Id');
					DtlGrid.CenOrgId = record.get('BmsCenOrderDoc_Lab_Id');
					
					DtlGrid.OrderDocID = record.get('BmsCenOrderDoc_Id'),
					DtlGrid.OrderDocNo = record.get('BmsCenOrderDoc_SaleDocNo'),
					
					DtlGrid.defaultWhere = 'bmscenorderdtl.BmsCenOrderDoc.Id=' + id;
					
					if(Status == '1'){//确定状态
						DocForm.isShow(id);
						DtlInfo.clearData();
						DtlGrid.onSearchOnlyRead();
					}else{
						DocForm.isEdit(id);
						DtlInfo.clearData();
						DtlGrid.onSearch();
					}
				},null,200);
			},
			addclick:function(p){
				DocForm.isAdd();
			},
			editclick:function(p,record){
				DocForm.isEdit(record.get(DocGrid.PKField));
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
					DtlInfo.initData({
						Name:record.get('BmsCenOrderDtl_GoodsName'),
						Qty:record.get('BmsCenOrderDtl_GoodsQty'),
						Unit:record.get('BmsCenOrderDtl_GoodsUnit'),
						LotNo:record.get('BmsCenOrderDtl_LotNo'),
						InvalidDate:JShell.Date.toString(record.get('BmsCenOrderDtl_InvalidDate'),true)
					});
				},null,200);
			}
		});
	}
});