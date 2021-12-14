/**
 * 城市选择
 * @author Jcall
 * @version 2017-02-09
 */
Ext.define('Shell.class.sysbase.country.province.city.CheckApp',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'城市选择',
    
    width:720,
    height:400,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.CountryGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.CountryGrid.PKField);
					me.ProvinceGrid.defaultWhere = 'bprovince.BCountry.Id=' + id;
					me.ProvinceGrid.onSearch();
				},null,200);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.CountryGrid.PKField);
					me.ProvinceGrid.defaultWhere = 'bprovince.BCountry.Id=' + id;
					me.ProvinceGrid.onSearch();
				},null,200);
			},
			nodata:function(p){
				me.ProvinceGrid.clearData();
			}
		});
		me.ProvinceGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.ProvinceGrid.PKField);
					me.Grid.defaultWhere = 'bcity.BProvince.Id=' + id;
					me.Grid.onSearch();
				},null,200);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.ProvinceGrid.PKField);
					me.Grid.defaultWhere = 'bcity.BProvince.Id=' + id;
					me.Grid.onSearch();
				},null,200);
			},
			nodata:function(p){
				me.Grid.clearData();
			}
		});
		
		me.Grid.on({
			accept: function(p, record) {
				me.fireEvent('accept',me,record);
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.CountryGrid = Ext.create('Shell.class.sysbase.country.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'CountryGrid',
			defaultLoad:true
		});
		me.ProvinceGrid = Ext.create('Shell.class.sysbase.country.province.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'ProvinceGrid',
			defaultLoad:true
		});
		me.Grid = Ext.create('Shell.class.sysbase.country.province.city.CheckGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			/**默认加载*/
			defaultLoad:false
		});
		
		return [me.CountryGrid,me.ProvinceGrid,me.Grid];
	}
});