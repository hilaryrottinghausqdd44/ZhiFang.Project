Ext.define("Shell.class.weixin.dict.core.Doctor.App",{
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
				me.Form.getComponent('Doctor_Id').setReadOnly(true);
				me.Form.isEdit(record.get(me.Grid.PKField));
			},
			//查询之后再form表单显示的内容
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.Form.isEdit(record.get(me.Grid.PKField));
				},null,500);
			},
			addclick:function(){
				me.Form.getComponent('Doctor_Id').setReadOnly(false);
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
		me.Grid=Ext.create("Shell.class.weixin.dict.core.Doctor.Grid",{
			region:'center',
			header:false,
			itemId:'grid',
		});
		me.Form=Ext.create("Shell.class.weixin.dict.core.Doctor.Form",{
			region:'east',
			itemId:'form',
			split:true,
			collapsible: true
		})
		return [me.Grid,me.Form]; 
	}
});
