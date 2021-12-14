/**
 * 任务信息查看
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.task.basic.ShowTabPanel',{
    extend: 'Ext.tab.Panel',
    title:'任务信息查看',
    
    width:790,
	height:490,
    
    autoScroll:false,
    
    /**任务ID*/
    TaskId:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		me.ContentPanel = Ext.create('Shell.class.wfm.task.basic.ContentPanel',{
			title:'任务内容',
			formtype:'show',
			hasLoadMask:false,//开启加载数据遮罩层
			TaskId:me.TaskId
		});
		
		me.Interaction = Ext.create('Shell.class.wfm.task.interaction.App',{
			title:'交流信息',
			FormPosition:'e',
			TaskId:me.TaskId
		});
		me.ShowChilrenGrid = Ext.create('Shell.class.wfm.task.basic.ShowChildrenGrid',{
			title:'子任务',
			ParentTaskId:me.TaskId
		});
		
		
		return [me.ContentPanel,me.Interaction,me.ShowChilrenGrid];
	}
});