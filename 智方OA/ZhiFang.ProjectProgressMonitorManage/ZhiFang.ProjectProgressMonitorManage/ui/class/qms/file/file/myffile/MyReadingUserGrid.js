/**
 * QMS当前登录者的阅读文件列表
 * @author longfc
 * @version 2016-06-28
 */
Ext.define('Shell.class.qms.file.file.myffile.MyReadingUserGrid', {
	extend: 'Shell.class.qms.file.show.Grid',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '我的阅读文件信息',
	hasRefresh: true,
	/**文件的操作记录类型*/
	fFileOperationType: 6,
	hasOperation: false,
	hasCheckBDictTree: true,
	remoteSort: false,
	/*推送操作列是否隐藏**/
	hiddenWeiXinMessagePush: true,
	defaultStatusValue: "5",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});