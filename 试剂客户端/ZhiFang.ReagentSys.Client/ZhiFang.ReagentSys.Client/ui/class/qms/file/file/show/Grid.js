/**
 * 普通用户文档查看列表
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.file.show.Grid', {
	extend: 'Shell.class.qms.file.show.Grid',
	title: '新闻查看列表',
	hasRefresh: true,
	/**文件的操作记录类型*/
	fFileOperationType: 6,
	hasOperation: false,
	hasCheckBDictTree: true,
	remoteSort: false,
	/*推送操作列是否隐藏**/
	hiddenWeiXinMessagePush: true,
	hiddenFFileStatus:true,
	defaultStatusValue: "5",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});