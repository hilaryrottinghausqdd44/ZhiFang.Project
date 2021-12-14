/**
 * 货位货架维护
 * @author liangyl
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.App', {
    extend:'Shell.ux.panel.AppPanel',
	title: '库房货架维护',
	width: 700,
	height: 480,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.storageGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.storageGrid.PKField);
					if(id){
						var Name = record.get('ReaStorage_CName');
						me.placeGrid.ReaStorageID=id;
						me.placeGrid.ReaStorageCName=Name;
						me.placeGrid.loadDataById(id);
					}else{
						me.placeGrid.clearData();
					}
					
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
				    var id = record.get(me.storageGrid.PKField);
					if(id){
						var Name = record.get('ReaStorage_CName');
						me.placeGrid.ReaStorageID=id;
						me.placeGrid.ReaStorageCName=Name;
						me.placeGrid.loadDataById(id);
					}else{
						me.placeGrid.clearData();
					}
				},null,500);
			},
			nodata:function(p){
				me.placeGrid.clearData();
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
		me.storageGrid = Ext.create('Shell.class.rea.client.shelves.storage.Grid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			width:580,
			defaultWhere:'reastorage.Visible=1',
			itemId: 'storageGrid'
		});
        me.placeGrid = Ext.create('Shell.class.rea.client.shelves.place.Grid', {
			region: 'center',
			header: false,
			itemId: 'placeGrid'
		});
		return [me.storageGrid,me.placeGrid];
	}
});