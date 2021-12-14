/**
 * 普通用户文档信息查看应用
 * @author longfc
 * @version 2016-06-30
 */
Ext.define('Shell.class.qms.file.basic.ShowApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '文档详情',
	checkOne: true,
	/**查询条件是否带上登录帐号id*/
	isSearchUSERID: false,
	hasReset: false,
	/**文件的操作记录类型*/
	fFileOperationType: 6,
	defaultStatusValue: "5",
	/**获取树的最大层级数*/
	LEVEL: "",
	treeShortcodeWhere: '',
	/**是否显示根节点*/
	SHOWROOT: false,
	FTYPE: '',
	IDS: '',
	interactionType: "show",
	DisagreeOfGridText: "撤消禁用",
	defaultWhere: '',
	basicGrid: '',
	/**PDF预览0下载按钮显示，1不显示*/
	DOWNLOAD:'',
	/**PDF预览0打印按钮显示，1不显示*/
	PRINT:'',
	/**1 使用内置pdf预览,0 不使用内置浏览器，不支持控制pdf下载，打印按钮，*/
    BUILTIN:'1',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		var Grid = me.getComponent('Grid');
		
		var Tree = me.getComponent('Tree');
		Tree.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					Grid.BDictTreeId = id;
					Grid.BDictTreeCName = record.get('text');
					if(id.length > 0 && id != "0") {
						Grid.revertSearchData();
						Grid.load();
					}
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					Grid.BDictTreeId = id;
					Grid.BDictTreeCName = record.get('text');
					if(id.length > 0 && id != "0") {
						Grid.revertSearchData();
						Grid.load();
					}
				}, null, 500);
			}
		});
	},

	initComponent: function() {
		var me = this;
		me.FTYPE=me.FTYPE||'';
		me.title = me.title || "文档详情";
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "(ffile.IsUse=1) and ( (ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "')  or (ffile.EndTime>='" + dt + "'))";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var tree = Ext.create('Shell.class.sysbase.dicttree.Tree', {
			region: 'west',
			width: 230,
			header: false,
			itemId: 'Tree',
			split: true,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			treeShortcodeWhere: me.treeShortcodeWhere,
			collapsible: true,
			rootVisible: (me.SHOWROOT === true || me.SHOWROOT === "true") ? true : false
		});
		var Grid = Ext.create(me.basicGrid, {
			region: 'center',
			header: false,
			checkOne: me.checkOne,
			title: me.title || "阅读文档",
			FTYPE: me.FTYPE,
			IDS: me.IDS,
			/**PDF预览0下载按钮显示，1不显示*/
			DOWNLOAD:me.DOWNLOAD,
			/**PDF预览0打印按钮显示，1不显示*/
			PRINT:me.PRINT,
			/**1 使用内置pdf预览,0 不使用内置浏览器，不支持控制pdf下载，打印按钮，*/
            BUILTIN:me.BUILTIN,
			defaultWhere: me.defaultWhere,
			itemId: 'Grid'
		});
		return [tree, Grid];
	}
});