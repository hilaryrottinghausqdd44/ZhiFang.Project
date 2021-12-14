/**
 * 实验室订货单审核
 * @author Jcall
 * @version 2015-03-07
 */
Ext.define('Shell.class.rea.order.lab.audit.App', {
	extend: 'Ext.panel.Panel',
	title: '实验室订货单审核',

	layout:'border',
    bodyPadding:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.initListeners();
		
		var cenOrgId = JShell.REA.System.CENORG_ID;
		if(cenOrgId){
			me.DocGrid.defaultWhere = me.DocGrid.defaultWhere || "";
			if(me.DocGrid.defaultWhere){
				me.DocGrid.defaultWhere += ' and '; 
			}
			
			me.DocGrid.defaultWhere +=  
				'bmscenorderdoc.Status<>999 and ' +
				'bmscenorderdoc.Lab.Id=' + cenOrgId;
		}
		me.DocGrid.onSearch();
		
		setTimeout(function(){
			me.DocGrid.getView().focus();
		},1000);
	},
	initComponent: function() {
		var me = this;
		
		me.items = me.createItems();

		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
			
		me.DocGrid = Ext.create('Shell.class.rea.order.lab.audit.DocGrid',{
			region:'center',itemId:'DocGrid',header:false
		});
		me.DocForm = Ext.create('Shell.class.rea.order.basic.DocForm',{
			region:'east',itemId:'DocForm',header:false,
			split:true,collapsible:true,openFormType:true
		});
		me.DtlGrid = Ext.create('Shell.class.rea.order.basic.DtlGrid',{
			region:'center',itemId:'DtlGrid',header:false
		});
		me.DtlInfo = Ext.create('Shell.class.rea.order.basic.DtlInfo',{
			region:'east',itemId:'DtlInfo',header:false,
			split:true,collapsible:true
		})
		
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
	initListeners:function(){
		var me = this;
		
		me.DocGrid.on({
			addclick:function(p){
				me.DocForm.isAdd();
			},
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					me.onDocGridSelect(record);
				},null,200);
			},
			nodata:function(){
				me.DocForm.clearData();
				me.DtlGrid.clearData();
				me.DtlInfo.clearData();
			}
		});
		
		me.DocForm.on({
			save:function(){
				me.DocGrid.onSearch();
			}
		});
		
		me.DtlGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					me.DtlInfo.initData({
						Name:record.get('BmsCenOrderDtl_GoodsName'),
						Qty:record.get('BmsCenOrderDtl_GoodsQty'),
						Unit:record.get('BmsCenOrderDtl_GoodsUnit'),
						UnitMemo:record.get('BmsCenOrderDtl_UnitMemo'),
						Price:record.get('BmsCenOrderDtl_Price')
					});
				},null,200);
			},
			nodata:function(){
				me.DtlInfo.clearData();
			}
		});
	},
	/**主单选中触发*/
	onDocGridSelect:function(record){
		var me = this;
		
		var id = record.get(me.DocGrid.PKField);
		var Status = record.get('BmsCenOrderDoc_Status') + '';
		
		//清空明细数据
		me.DtlInfo.clearData();
		
		if(Status == '0'){//临时
			//显示主单内容
			me.DocForm.isEdit(id);
			//加载明细数据
			me.DtlGrid.onSearchByDocInfo({
				OrderDocID:id,
				OrderDocNo:record.get('BmsCenOrderDoc_SaleDocNo'),
				CompId:record.get('BmsCenOrderDoc_Comp_Id'),
				CenOrgId:record.get('BmsCenOrderDoc_Lab_Id')
			});//可编辑
		}else{
			//显示主单内容
			me.DocForm.isShow(id);
			//加载明细数据
			me.DtlGrid.onSearchOnlyRead(id);//只查看
		}
	}
});