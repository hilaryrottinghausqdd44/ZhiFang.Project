/**
 * 仪器项目试剂历史管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.EquipItemGoodsApp', {
	extend: 'Ext.panel.Panel',
	title: '仪器项目试剂历史管理',

	layout:'border',
    bodyPadding:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		var EquipShowGrid = me.getComponent('EquipShowGrid');
		var EquipItemShowGrid = me.getComponent('EquipItemShowGrid');
		var EquipItemGoodsGrid = me.getComponent('EquipItemGoodsGrid');
		EquipShowGrid.on({
			select:function(rowModel,record){
				var id = record.get(EquipShowGrid.PKField);
				JShell.Action.delay(function(){
					EquipItemShowGrid.load();
				});
			}
		});
		EquipItemShowGrid.on({
			select:function(rowModel,record){
				var id = record.get(EquipItemShowGrid.PKField);
				JShell.Action.delay(function(){
					EquipItemGoodsGrid.load();
				});
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
		
		items.push(Ext.create('Shell.class.rea.equip.EquipShowGrid',{
			region:'west',itemId:'EquipShowGrid',width:200,
			split:true,collapsible:true,header:false
		}));
		items.push(Ext.create('Shell.class.rea.equip.EquipItemShowGrid',{
			region:'west',itemId:'EquipItemShowGrid',width:200,
			split:true,collapsible:true,header:false
		}));
		items.push(Ext.create('Shell.class.rea.equip.EquipItemGoodsGrid',{
			region:'center',itemId:'EquipItemGoodsGrid',header:false
		}));
		
		return items;
	}
});