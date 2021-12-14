/**
 * 
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.testitem.equipitemgoodlink.EquipGoodsApp', {
	extend: 'Ext.panel.Panel',
	title: '仪器与仪器试剂',
	width: 700,
	height: 480,
	autoScroll: false,
    layout: "anchor",
	/**内容周围距离*/
	bodyPadding:'1px',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.EquipReagentGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('ReaEquipReagentLink_Id');
					var GoodsID = record.get('ReaEquipReagentLink_GoodsID');
					var TestEquipID= record.get('ReaEquipReagentLink_TestEquipID');
					var TestCount = record.get('ReaEquipReagentLink_TestCount');
                    var obj ={
                    	GoodsID:GoodsID,
                    	TestCount:TestCount
                    }
                    me.LinkGrid.GoodsObj=obj;
					me.LinkGrid.GoodsID=GoodsID;
					me.LinkGrid.LinkID=id;
					me.LinkGrid.TestEquipID=TestEquipID;
					me.LinkGrid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
				    var id = record.get('ReaEquipReagentLink_Id');
					var GoodsID = record.get('ReaEquipReagentLink_GoodsID');
					var TestEquipID= record.get('ReaEquipReagentLink_TestEquipID');
					var TestCount = record.get('ReaEquipReagentLink_TestCount');
                    var obj ={
                    	GoodsID:GoodsID,
                    	TestCount:TestCount
                    }
                    me.LinkGrid.GoodsObj=obj;
					me.LinkGrid.GoodsID=GoodsID;
					me.LinkGrid.LinkID=id;
					me.LinkGrid.TestEquipID=TestEquipID;
					me.LinkGrid.onSearch();
				},null,500);
			},
			nodata:function(p){
				me.LinkGrid.clearData();
			}
		});
	},
	/**仪器试剂加载*/
	loadGoodsData:function(id){
		var me = this;
		me.EquipReagentGrid.EquipID=id;
		me.EquipReagentGrid.onSearch();
	},
	clearData:function(){
		var me = this;
		me.EquipReagentGrid.clearData();
		me.LinkGrid.clearData();
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
		me.EquipReagentGrid = Ext.create('Shell.class.rea.client.testitem.equipitemgoodlink.EquipReagentGrid', {
			region: 'north',
			title:'',
			anchor:"100% 45%",
			header: false,
			itemId: 'EquipReagentGrid'	   
		});

	    me.LinkGrid = Ext.create('Shell.class.rea.client.testitem.equipitemgoodlink.LinkGrid', {
			region: 'center',
			header: false, margin:'0.2px 0px 0px 0px',
			anchor:"100% 55%",
			itemId: 'Grid'
		});
		return [me.EquipReagentGrid,me.LinkGrid];
	}
});