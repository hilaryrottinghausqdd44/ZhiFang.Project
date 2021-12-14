/**
 * 子任务分配信息
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.task.publisher.children.EditPanel',{
    extend: 'Shell.class.wfm.task.basic.EditTabPanel',
    title:'子任务分配信息',
    
    FormClassConfig:{
    	formtype:'edit'
    },
    /**任务ID*/
    TaskId:null,
    
    /**是否是申请任务*/
    isApplyTask:false,
    
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
		
		if(me.isApplyTask){
			me.FormClassName = 'Shell.class.wfm.task.publisher.children.ApplyForm';
		}else{
			me.FormClassName = 'Shell.class.wfm.task.publisher.children.PublisherForm';
		}
		
		me.callParent(arguments);
	}
});