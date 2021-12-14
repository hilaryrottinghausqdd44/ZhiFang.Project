/**
 * 类型树
 * @author 
 * @version 2016-06-22
 */
Ext.define('Shell.class.sysbase.dicttree.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '类型树',
	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**获取树的最大层级数*/
	LEVEL: "",
	treeShortcodeWhere: '',
	/**是否显示根节点*/
	SHOWROOT:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Tree = me.getComponent('Tree');
		var Grid = me.getComponent('Grid');
		Tree.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					var name = record.get('text');
					Grid.loadByParentId(id, name);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					var name = record.get('text');
					Grid.loadByParentId(id, name);
				}, null, 500);
			}
		});
	},
	initComponent: function() {
		var me = this;
		if(me.IDS && me.IDS.toString().length > 0) {
			me.treeShortcodeWhere = "idListStr=" + me.IDS;
		} else {
			me.treeShortcodeWhere = me.treeShortcodeWhere || "";
		}
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Tree = Ext.create('Shell.class.sysbase.dicttree.Tree', {
			region: 'west',
			width: 250,
			header: false,
			itemId: 'Tree',
			split: true,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			treeShortcodeWhere: me.treeShortcodeWhere,
			collapsible: true,
			rootVisible:(me.SHOWROOT === true || me.SHOWROOT === "true")? true : false
		});
		me.Grid = Ext.create('Shell.class.sysbase.dicttree.Grid', {
			region: 'center',
			header: false,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			treeShortcodeWhere: me.treeShortcodeWhere,
			itemId: 'Grid'
		});
		return [me.Tree, me.Grid];
	}
});