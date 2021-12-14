/**
 * 知识库管理应用
 * @author longfc
 * @version 2016-09-22
 */
Ext.define('Shell.class.qms.file.knowledgebase.manage.App', {
	extend: 'Shell.class.qms.file.manage.ManageApp',
	title: '知识库管理',

	hasReset: true,
	checkOne: false,
	FTYPE: '2',
	IDS: '',
	interactionType: "show",
	HiddenDisagreeOfGrid: false,
	DisagreeOfGridText: "撤消禁用",
	defaultWhere: '',
	basicGrid: 'Shell.class.qms.file.knowledgebase.manage.Grid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.FTYPE='3';
		me.title = me.title || "知识库详情";
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "( (ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "')  or (ffile.EndTime>='" + dt + "') )";

		me.items = me.createItems();
		me.callParent(arguments);
	}
	});