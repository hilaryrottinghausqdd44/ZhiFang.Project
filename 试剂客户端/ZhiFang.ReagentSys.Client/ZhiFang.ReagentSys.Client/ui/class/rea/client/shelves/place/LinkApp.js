/**
 * 出库货架权限
 * @author liangyl
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.place.LinkApp', {
    extend:'Shell.ux.panel.AppPanel',
	title: '出库货架权限',
	width: 700,
	height: 480,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
//					me.fireEvent('select', record);
					me.onSelect(record);
	            },null,500);
	       },
	       select:function(RowModel, record){
				JShell.Action.delay(function(){
//					me.fireEvent('select', record);
					me.onSelect(record);
	            },null,500);
	        },
	        nodata:function(p){
				me.LinkGrid.clearData();
			}
		});
		me.LinkGrid.on({
			save:function(com){
				me.fireEvent('LinkGridSave', com);
			}
		});
		
	},
	
	initComponent: function() {
		var me = this;
		me.addEvents('LinkGridSave');
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		/***
		 * 2018-11-19
		 * 隐藏(去掉)货架权限人员维护
		 */
		me.LinkGrid = Ext.create('Shell.class.rea.client.shelves.place.LinkGrid', {
			region: 'south',
			header: false,
			split: false,
			collapsible: false,
			title:'货架权限人员',
			collapseMode:'mini',
			hidden:true,
			height:250,
			itemId: 'LinkGrid'
		});
        me.Grid = Ext.create('Shell.class.rea.client.shelves.place.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.LinkGrid,me.Grid];
	},
	//选中处理,选多行时取第一行
	onSelect:function(record){
		var me = this;
		var id=record.get(me.Grid.PKField);
		var name=record.get('ReaPlace_CName');
		if(id){
			me.LinkGrid.PlaceID=id;
			me.LinkGrid.PlaceName=name;
			me.LinkGrid.onSearch();
		}else{
			me.LinkGrid.clearData();
		}
	},
	onSearch:function(firstRaw){
		var me =this;
		var id=firstRaw.get('ReaStorage_Id');
		if(id){
			var Name = firstRaw.get('ReaStorage_CName');
			me.Grid.ReaStorageID=id;
			me.Grid.ReaStorageCName=Name;
			me.LinkGrid.StorageID=id;
		    me.LinkGrid.StorageName=Name;
			me.Grid.loadDataById(id);
		}else{
			me.Grid.clearData();
		}
	},
	clearData:function(){
		var me=this;
		me.Grid.clearData();
	}
});