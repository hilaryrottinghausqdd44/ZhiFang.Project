/**
 * 子任务列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.basic.ChildrenGrid',{
    extend: 'Shell.class.wfm.task.basic.Grid',
    
    title:'子任务列表',
	
	/**查询栏包含指派人选项*/
	hasPublisher:false,
	/**查询栏包含执行人选项*/
	hasExecutor:true,
	/**查询栏包含检查人选项*/
	hasChecker:true,
	
	/**父任务ID*/
	ParentTaskId:null,
	/**父任务名称*/
	ParentTaskName:null,
	/**是否只查看*/
	isShow:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				var isOver = JShell.WFM.DictCode.TaskStatus.E3.value == record.get('PTask_Status_Shortcode');
				if(!isOver){
					me.openFormEdit(id);
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.defaultWhere = "ptask.PTaskID=" + me.ParentTaskId;
		me.callParent(arguments);
	},
	createGridColumns:function(){
		var me = this;
		var columns = me.callParent(arguments);
		
		columns.pop();
		
		return columns;
	},
	/**初始化功能按钮栏内容*/
	initButtonToolbarItems:function(){
		var me = this;
		me.callParent(arguments);
		
		if(!me.isShow){
			me.buttonToolbarItems.splice(1,0,'add');
		}
	},
	/**新增任务*/
	onAddClick:function(){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.publisher.Form', {
			resizable: false,
			formtype:'add',
			/**父任务ID*/
			ParentTaskId:me.ParentTaskId,
			/**父任务名称*/
			ParentTaskName:me.ParentTaskName,
			listeners: {
				save: function(p, records) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	openFormEdit:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.publisher.FormApp', {
			//resizable: false,
			TaskId:id
		}).show();
	},
	/**初始化默认条件*/
	initDefaultWhere:function(){
		//
	}
});