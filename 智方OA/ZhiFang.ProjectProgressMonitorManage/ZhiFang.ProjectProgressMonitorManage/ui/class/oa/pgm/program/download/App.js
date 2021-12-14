/**
 * 程序下载应用
 * @author longfc
 * @version 2016-09-28
 */
Ext.define('Shell.class.oa.pgm.program.download.App', {
	extend: 'Shell.class.oa.pgm.basic.App',
	title: '程序下载',

	hasReset: true,
	hasCheckBDictTree: true,
	hasShow: false,
	/**文档状态选择项的默认值*/
	defaultStatusValue: "3",
	basicGrid: 'Shell.class.oa.pgm.program.download.Grid',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.listenersGrid();
	},
	initComponent: function() {
		var me = this;
		me.IDS = me.IDS || "";
		/*仪器厂商品牌ID**/
		me.ETYPEID = me.ETYPEID || '5724611581318422977';
		/*仪器分类**/
		me.EBRADID = me.EBRADID || '4777630349498328266';
		me.basicGrid = me.basicGrid || "Shell.class.oa.pgm.program.download.Grid";
		me.title = me.title || "程序下载";
		me.callParent(arguments);
	}
});