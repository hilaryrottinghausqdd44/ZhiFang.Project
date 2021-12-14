/**
 * 程序选择列表
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.authorization.program.Tree', {
	extend: 'Shell.class.sysbase.dicttree.Tree',
	title: '程序选择列表',
	width: 270,
	height: 300,
	rootVisible: false,
	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "5323720114866215336",
	/**获取树的最大层级数*/
	LEVEL: "",
	treeShortcodeWhere: '',
	hideNodeId:'5684872576807158459'
});