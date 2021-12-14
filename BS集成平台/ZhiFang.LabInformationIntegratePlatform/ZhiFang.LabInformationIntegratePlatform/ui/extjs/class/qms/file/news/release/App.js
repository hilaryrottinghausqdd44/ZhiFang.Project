/**
 * 新闻发布应用
 * @author longfc
 * @version 2016-09-22
 */
Ext.define('Shell.class.qms.file.news.release.App', {
	extend: 'Shell.class.qms.file.basic.App',
	title: '新闻编辑',

	/**文件的操作记录类型*/
	fFileOperationType: 5,

	/**基本应用的文档确认(通过/同意)操作按钮是否显示*/
	HiddenAgreeButton: false,
	/**基本应用的文档确认(通过/同意)操作按钮显示名称*/
	AgreeButtonText: "发布",
	/**基本应用的文档确认(直接发布)操作按钮的功能类型*/
	AgreeOperationType: 5,

	/**基本应用的文档确认(不通过/不同意)操作按钮是否显示*/
	HiddenDisagreeButton: false,
	/**基本应用的文档确认(不通过/不同意)操作按钮显示名称*/
	DisagreeButtonText: "暂存",
	/**基本应用的文档确认(不通过/不同意)操作按钮的功能类型*/
	DisagreeOperationType: 1,
	/**提交并发布的操作按钮是否显示*/
	HiddenPublishButton: false,
	/**隐藏阅读人信息*/
	HiddenFFileReadingLog: true,
	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, true, true, true, true],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [false, false, false, true],
	hasNextExecutor: false,

	basicGrid: 'Shell.class.qms.file.news.release.Grid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.listenersGrid();
	},
	initComponent: function() {
		var me = this;
		me.FTYPE = '2';
		me.addTabPanelApp = 'Shell.class.qms.file.news.basic.AddTabPanel';
		
		me.title = me.title || "新闻编辑";
		me.callParent(arguments);
	}
});