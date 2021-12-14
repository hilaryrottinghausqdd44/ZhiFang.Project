Ext.define("Shell.class.weixin.dict.core.ClientEleArea.App",{
	extend:'Shell.ux.panel.AppPanel',
	
	initComponent:function(){
		var me =this;
		me.items=me.createPitems();
		me.callParent(arguments);
	},
	
	afterRender:function(){
		var me =this;
		me.callParent(arguments);
		
		me.Grid.on({
			itemclick:function(v,record){
				me.Form.getComponent('ClientEleArea_Id').setReadOnly(true);
				me.Form.isEdit(record.get(me.Form.PKField));
				var id= record.get("ClientEleArea_clienteleId");
				var name =record.get("ClientEleArea_clienteleName");
				me.Form.setClienteleId(id);
				me.Form.getComponent('ClientEleArea_clienteleName').setValue(name);
				
			},
			select:function(RowModel, record){
				me.Form.isEdit(record.get(me.Grid.PKField));
				var id= record.get("ClientEleArea_clienteleId");
				var name =record.get("ClientEleArea_clienteleName");
				me.Form.setClienteleId(id);
				me.Form.getComponent('ClientEleArea_clienteleName').setValue(name);
				
			},
			addclick:function(){
				me.Form.getComponent('ClientEleArea_Id').setReadOnly(false);
				me.Form.isAdd();
			},
		});
		
		me.Form.on({
				save:function(p,id){
					me.Grid.onSearch(id);
				}
		});
	},
	
	createPitems:function(){
		var me =this;      
		me.Grid=Ext.create("Shell.class.weixin.dict.core.ClientEleArea.Grid",{
			region:'center',
			header:false,
			itemId:'grid',
		});
		me.Form=Ext.create("Shell.class.weixin.dict.core.ClientEleArea.Form",{
			region:'east',
			itemId:'form',
			width:280,
			split:true,
			collapsible: true
		})
		return [me.Grid,me.Form];
	},

});
