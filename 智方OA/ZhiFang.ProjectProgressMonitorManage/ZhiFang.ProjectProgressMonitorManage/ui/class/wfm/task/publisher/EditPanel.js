/**
 * 任务分配信息
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.task.publisher.EditPanel',{
    extend: 'Shell.class.wfm.task.basic.EditTabPanel',
    title:'任务分配信息',
    
    FormClassName:'Shell.class.wfm.task.publisher.Form',
    FormClassConfig:{
    	formtype:'edit'
    },
    /**任务ID*/
    TaskId:null,
	/**任务名称*/
	TaskName:null,
    
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.Form.on({
			load:function(p,data){
				//分配者存在
				if(data.value.PTask_PublisherID){
					//新增子任务列表
					me.onAddChildrenGrid();
				}
			},
			accept:function(p){
				//新增子任务列表
				me.onAddChildrenGrid();
			},
			aftersave:function(p,id,checkedBackButton){
				me.fireEvent('save',me,id,checkedBackButton);
			}
		});
		me.Attachment.on({
			selectedfilerender:function(p){
				me.Attachment.save();
			},
			beforesave:function(p){
				me.showMask(me.saveText);
			},
			save:function(p){
				me.hideMask();
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('save');
		me.callParent(arguments);
	},
	/**新增子任务列表*/
	onAddChildrenGrid:function(){
		var me = this;
		
		me.ChildrenGrid = Ext.create('Shell.class.wfm.task.publisher.children.Grid',{
			title:'子任务',
			ParentTaskId:me.TaskId,//父任务ID
			ParentTaskName:me.TaskName//父任务名称
		});
		
		me.add(me.ChildrenGrid);
	}
});