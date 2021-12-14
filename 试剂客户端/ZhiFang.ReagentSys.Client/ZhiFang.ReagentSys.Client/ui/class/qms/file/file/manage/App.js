/**
 * 文档信息维护应用
 * @author longfc
 * @version 2016-08-04
 */
Ext.define('Shell.class.qms.file.file.manage.App', {
	extend: 'Shell.class.qms.file.manage.ManageApp',
	title: '文档维护',
	checkOne: false,

	FTYPE: '1',
	IDS: '',
	interactionType: "show",
	HiddenDisagreeOfGrid: false,
	DisagreeOfGridText: "撤消禁用",
	defaultWhere: '',
	/**文档状态默认为发布*/
	defaultStatusValue:0,
	basicGrid: 'Shell.class.qms.file.file.manage.Grid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.title = me.title || "文档详情";
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "( (ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "')  or (ffile.EndTime>='" + dt + "') )";

		me.items = me.createItems();
		me.callParent(arguments);
	}
});