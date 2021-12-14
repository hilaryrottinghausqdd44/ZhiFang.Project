/**
 * 新闻管理应用
 * @author longfc
 * @version 2016-09-22
 */
Ext.define('Shell.class.qms.file.news.manage.App', {
	extend: 'Shell.class.qms.file.manage.ManageApp',
	title: '新闻管理',

	hasReset: true,
	checkOne: false,
	IDS: '',
	interactionType: "show",
	HiddenDisagreeOfGrid: false,
	DisagreeOfGridText: "撤消禁用",
	defaultWhere: '',
	/**文档状态选择项的默认值*/
	defaultStatusValue: "",
	basicGrid: 'Shell.class.qms.file.news.manage.Grid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},
	initComponent: function() {
		var me = this;
		me.FTYPE = '2';
		me.addTabPanelApp ='Shell.class.qms.file.news.basic.AddTabPanel';
		me.title = me.title || "文档详情";
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "( (ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "')  or (ffile.EndTime>='" + dt + "') )";
		me.items = me.createItems();
		me.callParent(arguments);
	}
});