/**
 * 
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.testitem.equipitemgoodlink.AddPanel', {
	extend: 'Ext.panel.Panel',
	title: '仪器与仪器项目',
	width: 700,
	height: 480,
	autoScroll: false,
    layout: "anchor",
	/**内容周围距离*/
	bodyPadding:'1px',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.EquipItemGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('ReaTestEquipItem_Id');
					var TestItemID = record.get('ReaTestEquipItem_TestItemID');
					var TestEquipID = record.get('ReaTestEquipItem_TestEquipID');
					me.Grid.TestItemID=TestItemID;
					me.Grid.TestEquipID=TestEquipID;
					me.Grid.TestEquipItemID=id;
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
				    var id = record.get('ReaTestEquipItem_Id');
					var TestItemID = record.get('ReaTestEquipItem_TestItemID');
					var TestEquipID = record.get('ReaTestEquipItem_TestEquipID');
					me.Grid.TestItemID=TestItemID;
					me.Grid.TestEquipID=TestEquipID;
					me.Grid.TestEquipItemID=id;
					me.Grid.onSearch();
				},null,500);
			},
			nodata:function(p){
				me.Grid.clearData();
			}
		});
	},
	/**仪器项目加载*/
	loadEquipData:function(id){
		var me = this;
		me.EquipItemGrid.EquipID=id;
		me.EquipItemGrid.onSearch();
	},
	clearData:function(){
		var me = this;
		me.EquipItemGrid.clearData();
		me.Grid.clearData();
	},
	initComponent: function() {
		var me = this;
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.EquipItemGrid = Ext.create('Shell.class.rea.client.testitem.equipitemgoodlink.EquipItemGrid', {
			region: 'north',
			title:'',
			anchor:"100% 45%",
			header: false,
			itemId: 'EquipItemGrid'	   
		});
	    me.Grid = Ext.create('Shell.class.rea.client.testitem.equipitemgoodlink.Grid', {
			region: 'center',
			header: false, margin:'0.2px 0px 0px 0px',
			anchor:"100% 55%",
			itemId: 'Grid'
		});
		return [me.EquipItemGrid,me.Grid];
	}
});