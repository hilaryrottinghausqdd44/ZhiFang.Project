/**
 * 库存预警设置
 * @author xiehz
 * @version 2020-08-10
 */
Ext.define('Shell.class.sysbase.setqtyalertinfo.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'库存报警预警',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments); 
		me.AboGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.AboGrid.PKField);
					me.SetQtyAlertInfoGrid.aboNo = id;
					me.SetQtyAlertInfoGrid.onSearch();
				}, null, 500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function() {
					var id = record.get(me.AboGrid.PKField);
					me.SetQtyAlertInfoGrid.aboNo = id;
					me.SetQtyAlertInfoGrid.onSearch();
				}, null, 500);
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
		me.AboGrid = Ext.create('Shell.class.sysbase.setqtyalertinfo.AboGrid', {
			region: 'west',
			header: false,
			itemId: 'AboGrid'
		});
		me.SetQtyAlertInfoGrid = Ext.create('Shell.class.sysbase.setqtyalertinfo.SetQtyAlertInfoGrid', {
			region: 'center',
			header: false,
			itemId: 'SetQtyAlertInfoGrid'
		});		
		return [me.AboGrid, me.SetQtyAlertInfoGrid];
	}
})