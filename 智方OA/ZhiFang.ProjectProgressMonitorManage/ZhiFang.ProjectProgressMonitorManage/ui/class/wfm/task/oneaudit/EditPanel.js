/**
 * 任务一审信息
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.task.oneaudit.EditPanel',{
    extend: 'Shell.class.wfm.task.basic.EditTabPanel',
    title:'任务一审信息',
    
    FormClassName:'Shell.class.wfm.task.oneaudit.Form',
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