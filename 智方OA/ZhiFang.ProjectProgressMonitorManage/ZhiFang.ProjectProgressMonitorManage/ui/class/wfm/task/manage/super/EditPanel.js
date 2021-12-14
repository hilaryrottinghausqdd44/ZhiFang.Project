/**
 * 任务详情修改
 * @author Jcall
 * @version 2018-08-03
 */
Ext.define('Shell.class.wfm.task.manage.super.EditPanel',{
    extend: 'Shell.class.wfm.task.basic.EditTabPanel',
    title:'任务详情管理',
    
    FormClassName:'Shell.class.wfm.task.manage.super.Form',
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
	}
});