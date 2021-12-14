/**
 * 程序发布应用
 * @author longfc
 * @version 2016-09-28
 */
Ext.define('Shell.class.oa.pgm.program.release.common.App', {
	extend: 'Shell.class.oa.pgm.basic.App',
	title: '程序发布',

	hasReset: true,
	/**是否显示新增按钮*/
	hasAdd: true,
	hasCheckBDictTree: false,
	hasShow: false,
	/**文档状态选择项的默认值*/
	defaultStatusValue: 0,
	basicGrid: 'Shell.class.oa.pgm.basic.Grid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.listenersGrid();
	},
	initComponent: function() {
		var me = this;
		me.basicGrid = me.basicGrid || "Shell.class.oa.pgm.basic.Grid";
		me.title = me.title || "程序发布";
		me.callParent(arguments);
	}
});