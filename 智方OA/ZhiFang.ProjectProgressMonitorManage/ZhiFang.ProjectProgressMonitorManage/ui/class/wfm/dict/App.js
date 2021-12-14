/**
 * 字典管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.dict.App',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'字典管理',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.TypeGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.TypeGrid.PKField);
					var DataTimeStamp = record.get('PDictType_DataTimeStamp');
					me.Form.DictTypeId = id;
					me.Form.DictTypeDataTimeStamp = DataTimeStamp;
					me.Grid.DictTypeId = id;
					me.Grid.DictTypeDataTimeStamp = DataTimeStamp;
					me.Grid.defaultWhere = 'pdict.PDictType.Id=' + id;
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.TypeGrid.PKField);
					var DataTimeStamp = record.get('PDictType_DataTimeStamp');
					me.Form.DictTypeId = id;
					me.Form.DictTypeDataTimeStamp = DataTimeStamp;
					me.Grid.DictTypeId = id;
					me.Grid.DictTypeDataTimeStamp = DataTimeStamp;
					me.Grid.defaultWhere = 'pdict.PDictType.Id=' + id;
					me.Grid.onSearch();
				},null,500);
			},
			nodata:function(p){
				me.Grid.clearData();
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
		
		me.TypeGrid = Ext.create('Shell.class.wfm.dict.type.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'TypeGrid'
		});
		me.Grid = Ext.create('Shell.class.wfm.dict.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.wfm.dict.Form', {
			region: 'east',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			width: 200
		});
		
		return [me.TypeGrid,me.Grid,me.Form];
	}
});