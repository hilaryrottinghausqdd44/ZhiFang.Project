/**
 * 子任务列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.basic.ShowChildrenGrid',{
    extend: 'Shell.class.wfm.task.basic.Grid',
    
    title:'子任务列表',
    
    /**带功能按钮栏*/
	hasButtontoolbar:false,
    /**父任务ID*/
	ParentTaskId:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//初始化检索监听
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				me.openShowForm(id);
			}
		});
	},
	
	initComponent:function(){
		var me = this;
		me.defaultWhere = 'ptask.PTaskID=' + (me.ParentTaskId || '-1');
		me.callParent(arguments);
	},
});