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
			DocGrid.CenOrgId = cenOrgId;
			DocGrid.defaultWhere = DocGrid.defaultWhere || "";
			if(DocGrid.defaultWhere){
				DocGrid.defaultWhere += ' and '
			}
			DocGrid.defaultWhere += 'bmscensaledoc.Status=0 and bmscensaledoc.Comp.Id=' + cenOrgId;
		}
		
		if(JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME) == JShell.System.ADMINNAME){
			DocGrid.showDelButton();
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
			}),Ext.create('Shell.class.rea.sale.DtlInfo',{
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
					var Status = record.get('BmsCenSaleDoc_Status') + '';
					DtlGrid.CompId = record.get('BmsCenSaleDoc_Comp_Id');
					DtlGrid.CenOrgId = record.get('BmsCenSaleDoc_Lab_Id');
					
					DtlGrid.SaleDocID = record.get('BmsCenSaleDoc_Id'),
					DtlGrid.SaleDocNo = record.get('BmsCenSaleDoc_SaleDocNo'),
					
					DtlGrid.defaultWhere = 'bmscensaledtl.BmsCenSaleDoc.Id=' + id;
					
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
			itemclick:function(view,record){
				JShell.Action.delay(function(){
					var id = record.get(DocGrid.PKField);
					var Status = record.get('BmsCenSaleDoc_Status') + '';
					DtlGrid.CompId = record.get('BmsCenSaleDoc_Comp_Id');
					DtlGrid.CenOrgId = record.get('BmsCenSaleDoc_Lab_Id');
					
					DtlGrid.SaleDocID = record.get('BmsCenSaleDoc_Id'),
					DtlGrid.SaleDocNo = record.get('BmsCenSaleDoc_SaleDocNo'),
					
					DtlGrid.defaultWhere = 'bmscensaledtl.BmsCenSaleDoc.Id=' + id;
					
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
			nodata:function(){
				DocForm.clearData();
				DtlGrid.clearData();
				DtlInfo.clearData();
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
						CName:record.get('BmsCenSaleDtl_GoodsName'),
						EName:record.get('BmsCenSaleDtl_Goods_EName'),
						Unit:record.get('BmsCenSaleDtl_GoodsUnit'),
						UnitMemo:record.get('BmsCenSaleDtl_UnitMemo'),
						LotNo:record.get('BmsCenSaleDtl_LotNo'),
						InvalidDate:JShell.Date.toString(record.get('BmsCenSaleDtl_InvalidDate'),true),
						Count:record.get('BmsCenSaleDtl_GoodsQty'),
						Price:record.get('BmsCenSaleDtl_Price')
					});
				},null,200);
			},
			itemclick:function(view,record){
				JShell.Action.delay(function(){
					DtlInfo.initData({
						CName:record.get('BmsCenSaleDtl_GoodsName'),
						EName:record.get('BmsCenSaleDtl_Goods_EName'),
						Unit:record.get('BmsCenSaleDtl_GoodsUnit'),
						UnitMemo:record.get('BmsCenSaleDtl_UnitMemo'),
						LotNo:record.get('BmsCenSaleDtl_LotNo'),
						InvalidDate:JShell.Date.toString(record.get('BmsCenSaleDtl_InvalidDate'),true),
						Count:record.get('BmsCenSaleDtl_GoodsQty'),
						Price:record.get('BmsCenSaleDtl_Price'),
						MixSerial:record.get('BmsCenSaleDtl_MixSerial')
					});
				},null,200);
			},
			nodata:function(){
				DtlInfo.clearData();
			}
		});
	}
});