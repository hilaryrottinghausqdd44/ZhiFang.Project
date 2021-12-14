/**
 * 套餐项目选择
 * @author liangyl	
 * @version 2017-03-09
 */
Ext.define('Shell.class.weixin.report.item.labtestitem.CheckApp',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'套餐项目选择',
    
    width:600,
    height:400,
    
    /**是否单选*/
	checkOne:true,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.AreaGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('ClientEleArea_ClientNo');
					me.Grid.AreaID=id;
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('ClientEleArea_ClientNo');
					me.Grid.AreaID=id;
					me.Grid.onSearch();
				},null,500);
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
		
		me.AreaGrid = Ext.create('Shell.class.weixin.hospital.area.Grid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'AreaGrid',
			width:200
		});
		me.Grid = Ext.create('Shell.class.weixin.report.item.labtestitem.CheckGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			checkOne:me.checkOne,
			/**默认加载*/
			defaultLoad:false
		});
		
		return [me.AreaGrid,me.Grid];
	}
});