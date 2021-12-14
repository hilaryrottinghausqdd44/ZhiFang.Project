Ext.define("Shell.class.weixin.dict.lab.BLabDoctor.App",{
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
			itemclick:function(m,record){
				me.Form.getComponent('BLabDoctor_LabDoctorNo').setReadOnly(true);
				me.Form.isEdit(record.get("BLabDoctor_Id"));
			},
			addclick:function(){
				me.Form.getComponent('BLabDoctor_LabDoctorNo').setReadOnly(false);
				me.Form.isAdd();
				var v = me.Grid.getComponent('buttonsToolbar2').getComponent('ClienteleId').value;
				me.Form.getComponent('BLabDoctor_LabCode').setValue(v);
			},
			//查询之后再form表单显示的内容
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.Form.isEdit(record.get(me.Grid.PKField));
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
		
		me.Grid=Ext.create("Shell.class.weixin.dict.lab.BLabDoctor.Grid",{
			region:'center',
			hreder:false,
		});
		
		me.Form=Ext.create("Shell.class.weixin.dict.lab.BLabDoctor.Form",{
			region:'east',
			spilt:true,
			hreder:false,
			collapsible:true,
		});
		
		return [me.Grid,me.Form];
	},
});
