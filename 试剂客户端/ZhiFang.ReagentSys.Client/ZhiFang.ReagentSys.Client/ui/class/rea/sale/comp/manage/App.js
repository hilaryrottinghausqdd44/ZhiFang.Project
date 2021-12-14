/**
 * 供货单管理-供应商专用
 * @author Jcall
 * @version 2017-07-25
 */
Ext.define('Shell.class.rea.sale.comp.manage.App', {
	extend: 'Ext.panel.Panel',
	title: '供货单管理-供应商专用',

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
			nodata:function(){
				me.DocForm.clearData();
				me.DtlGrid.clearData();
				me.DtlInfo.clearData();
			},
			addclick:function(p){
				me.DocForm.isAdd();
			},
			toRightClick:function(){
				me.DocForm.collapse();
			},
			toLeftClick:function(){
				me.DocForm.expand();
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
			},
			toTopClick:function(){
				var DocPanel = me.getComponent('DocPanel');
				DocPanel.setHeight(30);
			},
			toDownClick:function(){
				var DocPanel = me.getComponent('DocPanel');
				DocPanel.setHeight(300);
			},
			toRightClick:function(){
				me.DtlInfo.collapse();
			},
			toLeftClick:function(){
				me.DtlInfo.expand();
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
			
		//主单列表
		me.DocGrid = Ext.create('Shell.class.rea.sale.comp.manage.DocGrid',{
			region:'center',itemId:'DocGrid',header:false,defaultLoad:true
		});
		//主单内容
		me.DocForm = Ext.create('Shell.class.rea.sale.basic.DocForm',{
			region:'east',itemId:'DocForm',header:false,
			split:true,collapsible:true,collapseMode:'mini'
		});
		//明细列表
		me.DtlGrid = Ext.create('Shell.class.rea.sale.comp.manage.DtlGrid',{
			region:'center',itemId:'DtlGrid',header:false
		});
		//明细内容
		me.DtlInfo = Ext.create('Shell.class.rea.sale.basic.DtlInfo',{
			region:'east',itemId:'DtlInfo',header:false,
			split:true,collapsible:true,collapseMode:'mini'
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
		var IsSplit = record.get('BmsCenSaleDoc_IsSplit') + '';
  		
		//清空明细数据
		me.DtlInfo.clearData();
		if(Status == '0'){//临时
			//显示主单内容
			me.DocForm.isEdit(id);
			//加载明细数据
			me.DtlGrid.onSearchByDocInfo({
				IsSplit:IsSplit,
				Status:record.get('BmsCenSaleDoc_Status'),
				SaleDocID:id,
				SaleDocNo:record.get('BmsCenSaleDoc_SaleDocNo'),
				CompId:record.get('BmsCenSaleDoc_Comp_Id'),
				CenOrgId:record.get('BmsCenSaleDoc_Lab_Id')
			});
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