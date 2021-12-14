/**
 * 实验室订货单查看
 * @author Jcall
 * @version 2015-07-15
 */
Ext.define('Shell.class.rea.order.lab.show.App', {
	extend: 'Ext.panel.Panel',
	title: '实验室订货单查看',

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
				'bmscenorderdoc.Status<>0 and bmscenorderdoc.Status<>999 and ' +
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
			
		me.DocGrid = Ext.create('Shell.class.rea.order.basic.DocGrid',{
			region:'center',itemId:'DocGrid',header:false,
			hasDel:false,isShow:true,
			multiSelect:false,selType:'rowmodel'
		});
		me.DocForm = Ext.create('Shell.class.rea.order.lab.show.DocForm',{
			region:'east',itemId:'DocForm',header:false,
			split:true,collapsible:true,openFormType:true
		});
		me.DtlGrid = Ext.create('Shell.class.rea.order.basic.DtlGridMemo',{
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
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					var id = record.get(me.DocGrid.PKField);
					me.DocForm.isShow(id);
					me.DtlInfo.clearData();
					me.DtlGrid.onSearchByOrderDocId(id);
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
	}
});