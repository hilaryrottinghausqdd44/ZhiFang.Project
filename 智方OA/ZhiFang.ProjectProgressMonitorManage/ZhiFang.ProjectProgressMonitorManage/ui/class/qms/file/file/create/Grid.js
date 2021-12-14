/**
 * 文档发布列表
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.file.create.Grid', {
	extend: 'Shell.class.qms.file.basic.Grid',
	title: '文档发布',
	width: 1200,
	height: 800,

	hasReset: true,
	/**是否显示新增按钮*/
	hasAdd: true,
	hasShow: false,
	checkOne: false,
	/**是否显示内容页签*/
	hasContent: false,
	/**是否显示文档详情页签*/
	hasFFileOperation: true,
	/**文档状态值*/
	fFileStatus: 1,
	/**列表的默认查询条件--是否只查询当前登录者的数据*/
	isSearchUSERID: true,
	/*列表查询栏是否带内容类型复选框**/
	hasCheckBDictTree: true,
	/**文档状态选择项的默认值*/
	defaultStatusValue: "1",
	initComponent: function() {
		var me = this;
		me.AgreeOfGridText = "确认提交";
		me.HiddenAgreeOfGrid = true;
		me.DisagreeOfGridText = "撤消提交";
		me.HiddenDisagreeOfGrid = false;
		me.defaultWhere = 'ffile.IsUse=1';
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});