/**
 * 实验室就诊类型字典
 * @author guozhaojing
 * @version 2018-03-28
 */
Ext.define("Shell.class.weixin.dict.lab.BLabSickType.App",{
	extend:'Shell.ux.panel.AppPanel',
	title:'实验室就诊类型',
	
	initComponent:function(){
		var me =this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	
	afterRender:function(){
		var me =this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick:function(v, record){
				JShell.Action.delay(function(){
					me.Form.getComponent('BLabSickType_LabSickTypeNo').setReadOnly(true);
					me.Form.isEdit(record.get(me.Grid.PKField));
				},null,500);
			},
			//查询之后再form表单显示的内容
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.Form.isEdit(record.get(me.Grid.PKField));
				},null,500);
			},
			addclick:function(){
				me.Form.getComponent('BLabSickType_LabSickTypeNo').setReadOnly(false);
				me.Form.isAdd();
				var v = me.Grid.getComponent('buttonsToolbar2').getComponent('ClienteleId').value;
				me.Form.getComponent('BLabSickType_LabCode').setValue(v);
				
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
					me.Grid.onSearch(id);
				}
			});
	},
	
	createItems:function(){
		var me =this;
		me.Grid=Ext.create("Shell.class.weixin.dict.lab.BLabSickType.Grid",{
			region:'center',
			hreder:false,
			itemId:'grid',
		});
		me.Form=Ext.create("Shell.class.weixin.dict.lab.BLabSickType.Form",{ 
			region:'east',
			itemId:'form',
			hreder:false,
			split:true,
			callapsible:true
		});
		return [me.Grid,me.Form];
	}
});
