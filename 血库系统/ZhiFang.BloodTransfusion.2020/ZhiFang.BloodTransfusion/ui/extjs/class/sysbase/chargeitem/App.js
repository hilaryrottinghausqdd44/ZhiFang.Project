/**
 * 费用项目管理
 * @author longfc
 * @version 2020-07-07
 */
Ext.define('Shell.class.sysbase.chargeitem.App',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'费用项目管理',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.TypeGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.loadData(record);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.loadData(record);
				},null,500);
			},
			nodata:function(p){
				me.Grid.clearData();
				me.Form.getForm().reset();
				me.Form.disableControl();
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
				me.Form.getForm().reset();
				me.Form.disableControl();
			}
		});
		me.Form.on({
			save:function(p,id){
				me.Grid.onSearch();
			}
		});
		
		me.TypeGrid.onSearch();
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.TypeGrid = Ext.create('Shell.class.sysbase.chargeitemtype.SimpleGrid', {
			region: 'west',
			header: false,
			width: 280,
			split: true,
			collapsible: true,
			itemId: 'TypeGrid'
		});
		me.Grid = Ext.create('Shell.class.sysbase.chargeitem.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.chargeitem.Form', {
			region: 'east',
			header: true,
			itemId: 'Form',
			split: true,
			collapsible: false,
			width: 280
		});
		
		return [me.TypeGrid,me.Grid,me.Form];
	},
	loadData:function(record){
		var me = this;
		var id = record.get(me.TypeGrid.PKField);
		me.Form.getForm().reset();
		me.Form.DictTypeId = id;
		me.Form.DictTypeCName ="";
		me.Grid.defaultWhere ="";
		if(id){
			me.Form.DictTypeCName = record.get("BloodChargeItemType_CName");
			me.Grid.defaultWhere = 'bloodchargeitem.BloodChargeItemType.Id=' + id;
		}
		me.Grid.onSearch();
	}
});