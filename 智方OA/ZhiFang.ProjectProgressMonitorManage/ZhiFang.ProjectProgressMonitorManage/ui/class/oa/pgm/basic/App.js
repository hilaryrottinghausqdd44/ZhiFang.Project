/**
 * 程序基本应用
 * @author longfc
 * @version 2016-09-27
 */
Ext.define('Shell.class.oa.pgm.basic.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '程序信息',
	formtype: 'show',
	checkOne: true,
	/**列表的默认查询条件--是否只查询当前登录者的数据*/
	isSearchUSERID: false,
	defaultWhere: '',
	hasReset: false,
	hasAdd: false,
	treeShortcodeWhere: '',
	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	LEVEL: '0',
	hasCheckBDictTree: false,
	/**文档状态选择项的默认值*/
	defaultStatusValue: 0,
	/**是否隐藏程序附件大小*/
	hiddenPGMProgramSize: true,
	basicFormApp: "",
	basicGrid: '',
	/*仪器厂商品牌ID**/
	ETYPEID: '5724611581318422977',
	/*仪器分类**/
	EBRADID: '4777630349498328266',
	isLeaf:true,
	/*树节点的定制程序表单**/
	TreeNodes: {
		/*定制通讯树节点**/
		Customize: [
			'4648301227558053549', //单向定制
			'5342346356669779643', //双向定制
			'4661212243234641189', //流水线
			'5150849139652891931' //	第三方检测结果上传下
		],
		/*模板通讯树节点**/
		Template: [
			'4616559717672404089',//模板
			'5684872576807158459'//通讯
		]
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Tree = me.getComponent('Tree');
		var Grid = me.getComponent('Grid');

		Tree.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					me.isLeaf=record.get('leaf');
					Grid.PBDictTreeId = record.raw.pid;
					Grid.PBDictTreeCName = record.parentNode.get("text");
					Grid.SubBDictTreeId = id;
					Grid.SubBDictTreeCName = record.get('text');
					Grid.isLeaf=record.get('leaf');
					if(id.length > 0 && id != "0") {
						Grid.load();
					}
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					me.isLeaf=record.get('leaf');
					Grid.isLeaf=record.get('leaf');
					Grid.PBDictTreeId = record.raw.pid;
					Grid.PBDictTreeCName = record.parentNode.get("text");
					Grid.SubBDictTreeId = id;
					Grid.SubBDictTreeCName = record.get('text');
					
					if(id.length > 0 && id != "0") {
						Grid.load();
					}
				}, null, 500);
			}
		});

	},

	initComponent: function() {
		var me = this;
		/*仪器厂商品牌ID**/
		me.ETYPEID = me.ETYPEID || '5724611581318422977';
		/*仪器分类**/
		me.EBRADID = me.EBRADID || '4777630349498328266';
		me.basicFormApp = me.basicFormApp || "Shell.class.oa.pgm.basic.Form";
		me.basicGrid = me.basicGrid || "Shell.class.oa.pgm.basic.Grid";
		me.title = me.title || "程序发布";
		/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
		me.IDS = me.IDS || "";
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
			title: me.title || "程序操作",
			checkOne: me.checkOne,
			hasAdd: me.hasAdd,
			/**默认加载数据*/
			defaultLoad: me.defaultLoad,
			isSearchUSERID: me.isSearchUSERID,
			hasReset: me.hasReset,
			hiddenPGMProgramSize:me.hiddenPGMProgramSize,
			IDS: me.IDS,
			/*仪器厂商品牌ID**/
			ETYPEID: me.ETYPEID,
			/*仪器分类**/
			EBRADID: me.EBRADID,
			hasCheckBDictTree: me.hasCheckBDictTree,
			defaultStatusValue: me.defaultStatusValue,
			itemId: 'Grid'
		});
		return [tree, Grid];
	},
	/**打开新增或编辑文档表单*/
	openFFileForm: function(record) {
		var me = this;
		var Grid = me.getComponent('Grid');
		var id = "";
		if(record != null) {
			id = record.get('PGMProgram_Id');
		}
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight - 10;
		//先清除原基本表单的值信息
		me.basicFormApp = "";
		//依树节点处理显示程序基本表单
		var customize = me.TreeNodes.Customize;
		var template = me.TreeNodes.Template;
		var subNo="1";
		//当前节点是否存在定制通讯数组里
		if(customize.toString().indexOf(Grid.SubBDictTreeId.toString()) > -1) {
			me.basicFormApp = "Shell.class.oa.pgm.program.release.customize.Form";
			subNo="1";
		}
		if(me.basicFormApp == "") {
			//当前节点是否存在模板通讯数组里
			if(template.toString().indexOf(Grid.SubBDictTreeId.toString()) > -1) {
				me.basicFormApp = "Shell.class.oa.pgm.program.release.template.Form";
				subNo="2";
			}
		}
		//当前节点不存在定制通讯及模板通讯数组里,通用表单
		if(me.basicFormApp == "") {
			me.basicFormApp = "Shell.class.oa.pgm.program.release.common.Form";
			subNo="3";
		}	
		var config = {
			showSuccessInfo: false,
			height: height,
			width: maxWidth,
			zindex: 10,
			zIndex: 10,
			SUB_WIN_NO:subNo,
			resizable: false,
			hasReset: Grid.hasReset,
			title: Grid.title || "编辑程序",
			formtype: 'add',
			ETYPEID: me.ETYPEID,
			PBDictTreeId: Grid.PBDictTreeId,
			PBDictTreeCName: Grid.PBDictTreeCName,
			SubBDictTreeId: Grid.SubBDictTreeId,
			SubBDictTreeCName: Grid.SubBDictTreeCName,
			basicFormApp: me.basicFormApp,
			isLeaf:Grid.isLeaf,
			listeners: {
				save: function(win) {
					Grid.onSearch();
					win.close();
				}
			}
		};
		var form = 'Shell.class.oa.pgm.program.release.AddTabPanel';
		if(id && id != null && id != "") {
			config.formtype = 'edit';
			config.PK = id;
			title: me.title || "编辑程序";
		}
		JShell.Win.open(form, config).show();
	},
	/*程序列表的事件监听**/
	listenersGrid: function() {
		var me = this;
		var Grid = me.getComponent('Grid');
		Grid.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				var id = record.get('PGMProgram_Id');
				var status = record.get('PGMProgram_Status');
				switch(status) {
					case "1": //状态为
						Grid.formtype = "edit";
						me.openFFileForm(record);
						break;
					default:
						Grid.formtype = "show";
						me.openShowTabPanel(record);
						break;
				}
			},
			onAddClick: function() {
				Grid.formtype = "add";
				me.formtype = "add";
				if(Grid.SubBDictTreeId == "0") {
					JShell.Msg.alert("不能选择字典树根节点!",null,2000);
				} else if(Grid.SubBDictTreeId == null || Grid.SubBDictTreeId == "") {
					JShell.Msg.alert("没有获取字典树信息",null,2000);
				} else {
					me.openFFileForm(null);
				}
			},
			onEditClick: function(grid, record) {
				Grid.formtype = "edit";
				me.formtype = "edit";
				me.openFFileForm(record);
			},
			onShowClick: function(grid, record) {
				Grid.formtype = "show";
				me.formtype = "show";
				var records = Grid.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				me.openShowTabPanel(records[0]);
			},
			onShowTabPanelClick: function(grid, record, rowIndex, colIndex) {
				me.openShowTabPanel(record);
			}
		});
	},
	openShowTabPanel: function(record) {
		var me = this;
		var Grid = me.getComponent('Grid');
		var maxWidth = document.body.clientWidth * 0.82;
		var height = document.body.clientHeight - 10;
		var id = record.get('PGMProgram_Id');
		var config = {
			showSuccessInfo: false,
			height: height,
			width: maxWidth,
			resizable: false,
			title: "程序详细信息",
			formtype: 'show',
			PK: id,
			listeners: {
				close: function(win) {
					Grid.onSearch();
				}
			}
		};
		var form = 'Shell.class.oa.pgm.program.show.DetailedContent';
		var win = JShell.Win.open(form, config).show();
		//win.isShow(id);
	}
});