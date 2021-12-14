/**
 * 就诊类型字典
 * @author guozhaojing
 * @version 2018-03-27
 */
Ext.define('Shell.class.weixin.dict.core.SickType.App',{
	extend:'Shell.ux.panel.AppPanel',
	title:'就诊类型字典',
	
	initComponent:function(){
		var me =this;
		me.items=me.createItems();
		me.callParent(arguments);
	},
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick:function(v, record){
				JShell.Action.delay(function(){
				me.Form.getComponent('SickType_Id').setReadOnly(true);
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
				me.Form.getComponent('SickType_Id').setReadOnly(false);
				console.log(me.Form.getComponent('SickType_Id'));
				me.Form.isAdd();
			},
			editclick:function(p,record){
				me.Form.getComponent('SickType_Id').setReadOnly(true);
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
		var me = this;
		me.Grid=Ext.create('Shell.class.weixin.dict.core.SickType.Grid',{
			region: 'center',
			header: false,
			itemId: 'Grid'	
		});
		me.Form=Ext.create('Shell.class.weixin.dict.core.SickType.Form',{
			region: 'east',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true
		});
		return [me.Grid,me.Form];
	},
})
