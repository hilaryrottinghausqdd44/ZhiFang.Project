/**
 * 特推项目产品
 * @author liangyl	
 * @version 2016-12-28
 */
Ext.define('Shell.class.weixin.item.product.apply.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'特推项目产品',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.CheckGrid.on({
			itemclick:function(v, record){
				JShell.Action.delay(function(){
					var AreaId=record.get(me.CheckGrid.PKField);
					me.Grid.AreaID=AreaId;
					var ClientNo=record.get("ClientEleArea_ClientNo");
                    me.Grid.ClientNo=ClientNo;
					var AreaName=record.get('ClientEleArea_AreaCName');
					me.Grid.AreaName=AreaName;
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
                    var AreaId=record.get(me.CheckGrid.PKField);
                    var ClientNo=record.get("ClientEleArea_ClientNo");
                    me.Grid.ClientNo=ClientNo;
					me.Grid.AreaID=AreaId;
					var AreaName=record.get('ClientEleArea_AreaCName');
					me.Grid.AreaName=AreaName;
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
		me.CheckGrid = Ext.create('Shell.class.weixin.item.product.apply.AreaGrid', {
			region: 'west',
			width:300,
			header: false,
			itemId: 'CheckGrid',
			split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.weixin.item.product.apply.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.CheckGrid,me.Grid];
	}
});