/**
 * 任务二审信息
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.task.twoaudit.EditPanel',{
    extend: 'Shell.class.wfm.task.basic.EditTabPanel',
    title:'任务二审信息',
    
    FormClassName:'Shell.class.wfm.task.twoaudit.Form',
    FormClassConfig:{
    	formtype:'edit'
    },
    /**任务ID*/
    TaskId:null,
    
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.Form.on({
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
	}
});