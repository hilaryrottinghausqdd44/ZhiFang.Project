Ext.define("Shell.class.weixin.dict.core.SampleType.App",{
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
				JShell.Action.delay(function(){
					me.Form.getComponent('SampleType_Id').setReadOnly(true);
				if(record.data.SampleType_Id !=null && record.data.SampleType_Id.length>0){
					me.Form.isEdit(record.get(me.Form.PKField));
				}	
				},null,500);
			},
			//查询之后再form表单显示的内容
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.Form.isEdit(record.get(me.Grid.PKField));
				},null,500);
			},
			addclick:function(){
				JShell.Action.delay(function(){
					me.Form.getComponent('SampleType_Id').setReadOnly(false);
					me.Form.isAdd();	
				},null,500);
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
		me.Grid=Ext.create("Shell.class.weixin.dict.core.SampleType.Grid",{
			region:'center',
			header:false,
			itemId:'grid',
		});
		me.Form=Ext.create("Shell.class.weixin.dict.core.SampleType.Form",{
			region:'east',
			itemId:'form',
			split:true,
			collapsible: true
		})
		return [me.Grid,me.Form]; 
	}
});
