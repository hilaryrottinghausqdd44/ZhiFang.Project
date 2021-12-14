/**
 * 实验室试剂-采购统计
 * @author Jcall
 * @version 2017-07-21
 */
Ext.define('Shell.class.rea.sale.lab.statis.App', {
	extend: 'Ext.panel.Panel',
	title: '实验室试剂-采购统计',

	layout:'border',
    bodyPadding:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//主单监听
		me.GridLeft.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					me.onGridLeftSelect(record);
				},null,200);
			},
			itemclick:function(view,record){
				JShell.Action.delay(function(){
					me.onGridLeftSelect(record);
				},null,200);
			},
			nodata:function(){
				me.GridRight.clearData();
			}
		});
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
		me.GridLeft = Ext.create('Shell.class.rea.sale.lab.statis.GridLeft',{
			region:'west',itemId:'GridLeft',header:false,defaultLoad:true,
			split:true,collapsible:true,collapseMode:'mini'
		});
		//明细列表
		me.GridRight = Ext.create('Shell.class.rea.sale.lab.statis.GridRight',{
			region:'center',itemId:'GridRight',header:false,
			listeners:{
				toMaxClick:function(){
					me.GridLeft.collapse();
				},
				toMinClick:function(){
					me.GridLeft.expand();
				}
			}
		});
		
		return [me.GridLeft,me.GridRight];
	},
	
	/**主单选中触发*/
	onGridLeftSelect:function(record){
		var me = this,
			topToolabr1 = me.GridLeft.getComponent('topToolabr1'),
			topToolabr2 = me.GridLeft.getComponent('topToolabr2'),
			date = topToolabr1.getComponent('date').getValue(),
			CompID = topToolabr2.getComponent('CompID').getValue(),
			GoodsID = record.get(me.GridLeft.PKField);
		
		var Start = date ? date.start : null;
		var End = date ? date.end : null;
		
		me.GridRight.onSearchByParams(Start,End,GoodsID,CompID);
	}
});