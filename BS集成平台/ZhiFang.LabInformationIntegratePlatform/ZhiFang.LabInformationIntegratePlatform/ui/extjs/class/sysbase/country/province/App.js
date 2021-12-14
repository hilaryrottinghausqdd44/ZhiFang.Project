/**
 * 省份管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.country.province.App',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'省份管理',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.CountryGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.CountryGrid.PKField);
					me.Grid.defaultWhere = 'bprovince.BCountry.Id=' + id;
					me.Form.BCountry = {
						Name:record.get('BCountry_Name'),
						Id:record.get('BCountry_Id'),
						DataTimeStamp:record.get('BCountry_DataTimeStamp')
					};
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.CountryGrid.PKField);
					me.Grid.defaultWhere = 'bprovince.BCountry.Id=' + id;
					me.Form.BCountry = {
						Name:record.get('BCountry_Name'),
						Id:record.get('BCountry_Id'),
						DataTimeStamp:record.get('BCountry_DataTimeStamp')
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
		me.Grid = Ext.create('Shell.class.sysbase.country.province.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.country.province.Form', {
			region: 'east',
			width: 200,
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true
		});
		
		return [me.CountryGrid,me.Grid,me.Form];
	}
});