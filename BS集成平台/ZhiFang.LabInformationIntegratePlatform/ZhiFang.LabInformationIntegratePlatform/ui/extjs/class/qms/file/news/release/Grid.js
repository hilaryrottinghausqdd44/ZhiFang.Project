/**
 * 新闻发布列表
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.news.release.Grid', {
	extend: 'Shell.class.qms.file.manage.Grid',
	title: '新闻发布',
	width: 1200,
	height: 800,
	
	hasReset: true,
	/**是否显示新增按钮*/
	hasAdd: true,
	checkOne: false,
	/**文档状态默认为发布*/
	defaultStatusValue: "",
	/**文档状态值*/
	fFileStatus: 5,
	/**列表的默认查询条件--是否只查询当前登录者的数据*/
	isSearchUSERID: true,
	/*列表查询栏是否带内容类型复选框**/
	hasCheckBDictTree: true,
	/*文档状态**/
	FFileStatusList: [
		["", JShell.All.ALL],
		["1", '暂时存储'],
		["5", '已发布']
	],
	FFileDateTypeList: [
		["ffile.DrafterDateTime", '起草时间'],
		["ffile.PublisherDateTime", '发布时间']
	],
	initComponent: function() {
		var me = this;
		me.DisagreeOfGridText = "撤消提交";
		me.AgreeOfGridText = "确认提交";
		me.HiddenAgreeOfGrid = true;		
		me.HiddenDisagreeOfGrid = true;
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "(ffile.IsUse=1) and ((ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "')  or (ffile.EndTime>='" + dt + "'))";
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建数据列*/
	createNewColumns: function() {
		var me = this;
		var columns = [];
		columns = me.initDefaultColumn(columns);
		columns.push(me.createreIsUse());
		columns.push(me.createrePublisherName());
		columns.push(me.createrePublisherDateTime());

		columns = me.createWeiXinMessagePush(columns);
		//修改发布信息列
		columns.push(me.createPublisher());
		//是否有交流列
		columns.push(me.createInteraction());
		//是否有操作记录查看列
		columns.push(me.createOperation());
		//阅读记录列
		columns.push(me.createreadinglog());
		//me.createOtherColumn(columns);
		return columns;
	}
});