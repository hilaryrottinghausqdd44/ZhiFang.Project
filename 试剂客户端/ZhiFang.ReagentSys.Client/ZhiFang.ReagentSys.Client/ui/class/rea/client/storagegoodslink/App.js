/**
 * 库房试剂维护
 * @author longfc	
 * @version 2019-07-09
 */
Ext.define('Shell.class.rea.client.storagegoodslink.App', {
	extend: 'Ext.panel.Panel',
	
	title: '库房(货架)试剂维护',
	width: 700,
	height: 480,
	autoScroll: false,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding:'1px',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.StorageGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('ReaStorage_Id');
					me.LinkGrid.StorageID=id;
					me.LinkGrid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
				    var id = record.get('ReaStorage_Id');
					me.LinkGrid.StorageID=id;
					me.LinkGrid.onSearch();
				},null,500);
			},
			nodata:function(p){
				me.LinkGrid.clearData();
			}
		});
		
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
		me.StorageGrid = Ext.create('Shell.class.rea.client.storagegoodslink.StorageGrid', {
			region: 'west',
			title:'库房列表',
			width: 320,
			header: false,
			itemId: 'StorageGrid',
		    split: true,
			collapsible: true,
			collapseMode:'mini'
		});

		me.LinkGrid = Ext.create('Shell.class.rea.client.storagegoodslink.LinkGrid', {
			region: 'center',
			header: false,
			itemId: 'LinkGrid'
		});
		return [me.StorageGrid,me.LinkGrid];
	}
});