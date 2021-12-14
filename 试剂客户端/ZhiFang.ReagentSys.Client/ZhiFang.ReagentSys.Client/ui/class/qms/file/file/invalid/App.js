/**
 * 文档作废
 * @author longfc
 * @version 2017-06-22
 */
Ext.define('Shell.class.qms.file.file.invalid.App', {
	extend: 'Shell.class.qms.file.basic.App',
	title: '文档作废',
	
	FTYPE: '1',	
	hiddenRadiogroupChoose: [false, false, true, true, false],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [true, false, false, false],
	
	basicGrid: 'Shell.class.qms.file.file.invalid.Grid',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Grid = me.getComponent('Grid');

		Grid.on({
			onShowClick: function() {
				var records = Grid.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				Grid.openShowTabPanel(records[0]);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.FTYPE = '1';
		/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
		me.IDS = me.IDS || "";
		/**抄送人,阅读人的按人员选择时的角色姓名传入*/
		me.ROLEHREMPLOYEECNAME = me.ROLEHREMPLOYEECNAME || "";
		/**编辑文档类型(如新闻/通知/文档/修订文档)*/
		me.FTYPE = me.FTYPE || "";
		me.callParent(arguments);
	}
});