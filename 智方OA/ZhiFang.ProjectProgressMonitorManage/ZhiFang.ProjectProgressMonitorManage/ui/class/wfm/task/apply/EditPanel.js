/**
 * 任务申请信息
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.task.apply.EditPanel', {
	extend: 'Shell.class.wfm.task.basic.EditTabPanel',
	title: '任务申请信息',

	width: 670,
	height: 400,

	FormClassName: 'Shell.class.wfm.task.apply.Form',
	FormClassConfig: {
		formtype: 'edit'
	},
	/**任务ID*/
	TaskId: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.Form.on({
			aftersave: function(p, id) {
				me.fireEvent('save', me, id);
			}
		});
		me.Attachment.on({
			selectedfilerender: function(p) {
				me.Attachment.save();
			},
			//			beforesave:function(p){
			//				me.showMask(me.saveText);
			//			},
			/*附件上传进度条提示处理
			 * @author longfc
			 * @version 2015-10-08
			 */
			save: function(p) {
				//me.hideMask();
				if(me.Attachment.progressMsg!="")
				JShell.Msg.alert(me.Attachment.progressMsg);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.callParent(arguments);
	}
});