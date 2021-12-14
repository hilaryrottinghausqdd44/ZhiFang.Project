/**
 * 供应商供货
 * @author Jcall
 * @version 2017-02-17
 */
Ext.define('Shell.class.rea.sale.comp.check.App', {
	extend: 'Ext.panel.Panel',
	title: '供应商供货',

	layout:'border',
    bodyPadding:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//主单监听
		me.DocGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					me.onDocGridSelect(record);
				},null,200);
			},
			itemclick:function(view,record){
				JShell.Action.delay(function(){
					me.onDocGridSelect(record);
				},null,200);
			},
			nodata:function(){
				me.DocForm.clearData();
				me.DtlGrid.clearData();
				me.DtlInfo.clearData();
			},
			addclick:function(p){
				me.DocForm.isAdd();
			},
			editclick:function(p,record){
				var Status = record.get('BmsCenSaleDoc_Status') + '';
				if(Status == '0'){//临时
					me.DocForm.isEdit(record.get(me.DocGrid.PKField));
				}
			}
		});
		//明细监听
		me.DtlGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					me.onDtlGridSelect(record);
				},null,200);
			},
			itemclick:function(view,record){
				JShell.Action.delay(function(){
					me.onDtlGridSelect(record);
				},null,200);
			},
			nodata:function(){
				me.DtlInfo.clearData();
			}
		});
		
		//主单内容
		me.DocForm.on({
			save:function(){
				me.DocGrid.onSearch();
			}
		});
		
		setTimeout(function(){
			me.DocGrid.focus();
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
			
		//供货单状态：0=临时；2=已审核；1=已验收；
		//供货单的供应商=本机构 
		var cenOrgId = JShell.REA.System.CENORG_ID;
		var defaultWhere = 'bmscensaledoc.Comp.Id=' + cenOrgId;
			
		//主单列表
		me.DocGrid = Ext.create('Shell.class.rea.sale.comp.check.DocGrid',{
			region:'center',itemId:'DocGrid',header:false,defaultLoad:true,
			defaultWhere:defaultWhere
		});
		//主单内容
		me.DocForm = Ext.create('Shell.class.rea.sale.basic.DocForm',{
			region:'east',itemId:'DocForm',header:false,
			split:true,collapsible:true
		});
		//明细列表
		me.DtlGrid = Ext.create('Shell.class.rea.sale.comp.check.DtlGrid',{
			region:'center',itemId:'DtlGrid',header:false
		});
		//明细内容
		me.DtlInfo = Ext.create('Shell.class.rea.sale.basic.DtlInfo',{
			region:'east',itemId:'DtlInfo',header:false,
			split:true,collapsible:true
		});
		
		items.push({
			region:'north',header:false,
			split:true,collapsible:true,
			itemId:'DocPanel',height:300,
			layout:'border',border:false,
			items:[me.DocGrid,me.DocForm]
		});
		items.push({
			region:'center',header:false,border:false,
			itemId:'DtlPanel',layout:'border',
			items:[me.DtlGrid,me.DtlInfo]
		});
		
		return items;
	},
	
	/**主单选中触发*/
	onDocGridSelect:function(record){
		var me = this;
		
		var id = record.get(me.DocGrid.PKField);
		var Status = record.get('BmsCenSaleDoc_Status') + '';
		
		//显示主单内容
		//me.DocForm.isShow(id);
		//清空明细数据
		me.DtlInfo.clearData();
		
		if(Status == '0'){//临时
			//显示主单内容
			me.DocForm.isEdit(id);
			//加载明细数据
			me.DtlGrid.onSearchByDocInfo({
				SaleDocID:id,
				SaleDocNo:record.get('BmsCenSaleDoc_SaleDocNo'),
				CompId:record.get('BmsCenSaleDoc_Comp_Id'),
				CenOrgId:record.get('BmsCenSaleDoc_Lab_Id')
			});//可编辑
		}else{
			//显示主单内容
			me.DocForm.isShow(id);
			//加载明细数据
			me.DtlGrid.onSearchOnlyRead(id);//只查看
		}
	},
	/**明细选中触发*/
	onDtlGridSelect:function(record){
		var me = this;
		
		me.DtlInfo.initData({
			CName:record.get('BmsCenSaleDtl_GoodsName'),
			EName:record.get('BmsCenSaleDtl_Goods_EName'),
			Unit:record.get('BmsCenSaleDtl_GoodsUnit'),
			UnitMemo:record.get('BmsCenSaleDtl_UnitMemo'),
			LotNo:record.get('BmsCenSaleDtl_LotNo'),
			InvalidDate:JShell.Date.toString(record.get('BmsCenSaleDtl_InvalidDate'),true),
			Count:record.get('BmsCenSaleDtl_GoodsQty'),
			Price:record.get('BmsCenSaleDtl_Price')
		});
	}
});