/**
 * 城市管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.country.province.city.App',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'城市管理',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.CountryGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.CountryGrid.PKField);
					me.ProvinceGrid.defaultWhere = 'bprovince.BCountry.Id=' + id;
					me.ProvinceGrid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.CountryGrid.PKField);
					me.ProvinceGrid.defaultWhere = 'bprovince.BCountry.Id=' + id;
					me.ProvinceGrid.onSearch();
				},null,500);
			},
			nodata:function(p){
				me.ProvinceGrid.clearData();
				me.Form.clearData();
			}
		});
		
		me.ProvinceGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.ProvinceGrid.PKField);
					me.Grid.defaultWhere = 'bcity.BProvince.Id=' + id;
					me.Form.BProvince = {
						Name:record.get('BProvince_Name'),
						Id:record.get('BProvince_Id'),
						DataTimeStamp:record.get('BProvince_DataTimeStamp')
					};
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.ProvinceGrid.PKField);
					me.Grid.defaultWhere = 'bcity.BProvince.Id=' + id;
					me.Form.BProvince = {
						Name:record.get('BProvince_Name'),
						Id:record.get('BProvince_Id'),
						DataTimeStamp:record.get('BProvince_DataTimeStamp')
					};
					me.Grid.onSearch();
				},null,500);
			},
			nodata:function(p){
				me.Grid.clearData();
				me.Form.clearData();
			}
		});
		
		me.Grid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				},null,500);
			},
			addclick:function(p){
				me.Form.isAdd();
			},
			nodata:function(p){
				me.Form.clearData();
			}
		});
		
		me.Form.on({
			save:function(){
				me.Grid.onSearch();
			}
		});
		
		me.ProvinceGrid.onSearch();
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.CountryGrid = Ext.create('Shell.class.sysbase.country.SimpleGrid', {
			region: 'west',
			header: false,
			itemId: 'CountryGrid',
			split: true,
			collapsible: true,
			defaultLoad:true
		});
		me.ProvinceGrid = Ext.create('Shell.class.sysbase.country.province.SimpleGrid', {
			region: 'west',
			header: false,
			itemId: 'ProvinceGrid',
			split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.sysbase.country.province.city.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.country.province.city.Form', {
			region: 'east',
			width: 200,
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true
		});
		
		return [me.CountryGrid,me.ProvinceGrid,me.Grid,me.Form];
	}
});