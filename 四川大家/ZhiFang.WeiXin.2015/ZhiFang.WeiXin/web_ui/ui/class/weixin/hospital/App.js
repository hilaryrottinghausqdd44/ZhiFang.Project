/**
 * 医院维护
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.hospital.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'医院维护',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.AreaGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.Grid.AreaID=record.get('ClientEleArea_Id');
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.Grid.AreaID=record.get('ClientEleArea_Id');
					me.Grid.onSearch();
				},null,500);
			},
			nodata:function(p){
				me.Grid.clearData();
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
		
		me.Grid = Ext.create('Shell.class.weixin.hospital.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.AreaGrid = Ext.create('Shell.class.weixin.hospital.area.Grid', {
			region: 'west',
			width:300,
			header: false,
			itemId: 'AreaGrid',
			split: true,
			collapsible: true
		});
		
		return [me.Grid,me.AreaGrid];
	}
});