/**
 * 省份选择
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.country.province.CheckApp',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'省份选择',
    
    width:540,
    height:400,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.CountryGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.CountryGrid.PKField);
					me.Grid.defaultWhere = 'bprovince.BCountry.Id=' + id;
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.CountryGrid.PKField);
					me.Grid.defaultWhere = 'bprovince.BCountry.Id=' + id;
					me.Grid.onSearch();
				},null,500);
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
		me.Grid = Ext.create('Shell.class.sysbase.country.province.CheckGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			/**默认加载*/
			defaultLoad:false
		});
		
		return [me.CountryGrid,me.Grid];
	}
});