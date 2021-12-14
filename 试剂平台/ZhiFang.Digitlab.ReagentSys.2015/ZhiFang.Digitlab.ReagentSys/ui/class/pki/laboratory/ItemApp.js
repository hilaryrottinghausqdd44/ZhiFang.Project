/**
 * 送检单位项目维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.laboratory.ItemApp',{
    extend:'Ext.panel.Panel',
    title:'送检单位项目维护',
    
    layout:'border',
    bodyPadding:1,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var LaboratoryGrid = me.getComponent('LaboratoryGrid');
		var LaboratoryItemGrid = me.getComponent('LaboratoryItemGrid');
		LaboratoryGrid.on({
			select:function(rowModel,record){
				var id = record.get(LaboratoryGrid.PKField);
				JShell.Action.delay(function(){
					LaboratoryItemGrid.loadByLaboratoryId(id);
				});
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(Ext.create('Shell.class.pki.laboratory.Grid',{
			region:'west',
			split:true,collapsible:true,
			itemId:'LaboratoryGrid',
			header:false
		}));
		items.push(Ext.create('Shell.class.pki.laboratory.ItemGrid',{
			region:'center',
			itemId:'LaboratoryItemGrid',
			header:false
		}));
			
		return items;
	}
});