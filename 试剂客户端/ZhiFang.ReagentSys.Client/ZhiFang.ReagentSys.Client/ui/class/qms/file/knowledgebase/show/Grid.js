/**
 * 普通用户知识库查看
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.knowledgebase.show.Grid', {
	extend: 'Shell.class.qms.file.show.Grid',
	title: '知识库查看',
	hasRefresh: true,
	/**文件的操作记录类型*/
	fFileOperationType: 6,
	hasOperation: false,
	hasCheckBDictTree: true,
	remoteSort: false,
	/*推送操作列是否隐藏**/
	hiddenWeiXinMessagePush: true,
	defaultStatusValue: "5",
	hiddenFFileStatus:true,
	/**是否显示文档详情页签*/
	hasFFileOperation: false,
	FFileDateTypeList:[
		["ffile.DrafterDateTime",'起草时间'], 
		["ffile.PublisherDateTime", '发布时间']
	],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});