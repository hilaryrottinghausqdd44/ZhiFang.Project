/**
 * 选择树形结构
 * @author 
 * @version 2016-06-22
 */
Ext.define('Shell.class.sysbase.dicttree.CheckTree', {
	extend: 'Shell.class.sysbase.dicttree.Tree',

	title: '类型树信息',
	rootVisible: true,
	/**默认加载数据*/
	defaultLoad: true,
	/**是否单选*/
	checkOne:true,
	isShowchecked: false,
	treeShortcodeWhere: '',
	/**根节点*/
	root: {
		text: '类型树',
		iconCls: 'main-package-16',
		id: 0,
		tid: 0,
		leaf: false,
		expanded: false
	},
	initComponent: function() {
		var me = this;
		me.FTYPE = me.FTYPE || "";
		me.IDS = me.IDS || "";
		me.LEVEL = me.LEVEL || "";
		me.ROLEHREMPLOYEECNAME = me.ROLEHREMPLOYEECNAME || "";
		me.addEvents('accept');
		me.topToolbar = me.topToolbar || ['-', '->', {
			xtype: 'button',
			iconCls: 'button-accept',
			text: '确定',
			tooltip: '确定',
			handler: function() {
				me.onAcceptClick();
			}
		}];
		me.callParent(arguments);
	},
	/**确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();

		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('accept', me, records[0]);
	}
});