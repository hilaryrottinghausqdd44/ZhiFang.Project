/**
 * 任务信息修改
 * @author Jcall
 * @version 2018-08-03
 */
Ext.define('Shell.class.wfm.task.manage.super.EditApp',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'任务信息修改',
    
    width:2000,
    height:800,
    
    //任务ID
	TaskId:null,
	
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Form = Ext.create('Shell.class.wfm.task.manage.super.EditPanel', {
			region: 'west',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			collapseMode:'mini',
			TaskId:me.TaskId
		});
		me.Grid = Ext.create('Shell.class.wfm.task.manage.super.OperLogGrid', {
			region: 'center',
			header: false,
			itemId:'Grid',
			TaskId:me.TaskId
		});
		
		return [me.Form,me.Grid];
	}
});