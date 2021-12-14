/**
 * 字典管理-开发商功能
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.dict.system.App',{
    extend:'Shell.ux.model.GridFormApp',
    
    title:'字典管理-开发商功能',
    GridClassName:'Shell.class.wfm.dict.system.Grid',
    FormClassName:'Shell.class.wfm.dict.system.Form'
});

/**
 * 字典管理-开发商功能
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.dict.system.App',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'字典管理-开发商功能',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.TypeGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.TypeGrid.PKField);
					var DataTimeStamp = record.get('BDictType_DataTimeStamp');
					me.Form.DictTypeId = id;
					me.Grid.DictTypeId = id;
					me.Grid.defaultWhere = 'pdict.BDictType.Id=' + id;
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.TypeGrid.PKField);
					var DataTimeStamp = record.get('BDictType_DataTimeStamp');
					me.Form.DictTypeId = id;
					me.Grid.DictTypeId = id;
					me.Grid.defaultWhere = 'pdict.BDictType.Id=' + id;
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
					me.Grid.onSelectData();
					var DeveCode = record.get('BDict_DeveCode');
					if(DeveCode){
						me.Form.isShow(record.get(me.Grid.PKField));
					}else{
						me.Form.isEdit(record.get(me.Grid.PKField));
					}
				},null,200);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.Grid.onSelectData();
					var DeveCode = record.get('BDict_DeveCode');
					if(DeveCode){
						me.Form.isShow(record.get(me.Grid.PKField));
					}else{
						me.Form.isEdit(record.get(me.Grid.PKField));
					}
				},null,200);
			},
			addclick:function(){
				me.Form.isAdd();
			},
			editclick:function(p,record){
				me.Form.isEdit(record.get(me.Grid.PKField));
			},
			nodata:function(p){
				me.Form.clearData();
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
		me.Grid = Ext.create('Shell.class.wfm.dict.system.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.wfm.dict.system.Form', {
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