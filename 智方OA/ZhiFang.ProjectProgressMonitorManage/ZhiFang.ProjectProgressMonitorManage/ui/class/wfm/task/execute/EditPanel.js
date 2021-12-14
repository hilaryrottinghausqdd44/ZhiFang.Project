/**
 * 任务执行信息
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.task.execute.EditPanel',{
    extend: 'Shell.class.wfm.task.basic.EditTabPanel',
    title:'任务执行信息',
    
    height:490,
    
    FormClassName:'Shell.class.wfm.task.execute.Form',
    FormClassConfig:{
    	formtype:'edit'
    },
    /**任务ID*/
    TaskId:null,
    
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.Form.on({
			aftersave:function(p,id){
				me.fireEvent('save',me,id);
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
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		var items = me.callParent(arguments);
		
		me.ShowChilrenGrid = Ext.create('Shell.class.wfm.task.basic.ShowChildrenGrid',{
			title:'子任务',
			ParentTaskId:me.TaskId
		});
		
		items.push(me.ShowChilrenGrid);
		
		return items;
	}
});