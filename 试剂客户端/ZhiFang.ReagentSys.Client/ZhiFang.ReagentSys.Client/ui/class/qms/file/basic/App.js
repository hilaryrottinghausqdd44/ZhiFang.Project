/**
 * 文档基本应用
 * @author longfc
 * @version 2016-06-22
 */
Ext.define('Shell.class.qms.file.basic.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '文档信息',
	formtype: 'show',

	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**获取树的最大层级数*/
	LEVEL: "",
	/**抄送人,阅读人的按人员选择时的角色姓名传入*/
	ROLEHREMPLOYEECNAME: "",
	/**编辑文档类型(如新闻/通知/文档/修订文档)*/
	FTYPE: '',

	/**文件的操作记录类型*/
	fFileOperationType: 1,

	/**基本应用的文档确认(通过/同意)操作按钮是否显示*/
	HiddenAgreeButton: true,
	/**基本应用的文档确认(通过/同意)操作按钮显示名称*/
	AgreeButtonText: "",
	/**基本应用的文档确认(通过/同意)操作按钮的功能类型*/
	AgreeOperationType: -1,
	/**基本应用的文档确认(不通过/不同意)操作按钮是否显示*/
	HiddenDisagreeButton: true,
	/**基本应用的文档确认(不通过/不同意)操作按钮显示名称*/
	DisagreeButtonText: "",
	/**基本应用的文档确认(不通过/不同意)操作按钮的功能类型*/
	DisagreeOperationType: -1,

	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, false, false, false, false],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [true, false, false, false],
	/**新增或编辑文档时是否包含下一执行者下拉选择框*/
	hasNextExecutor: true,
	/**各应用的文档列表类名*/
	basicGrid: '',
	/**各应用的新增或编辑的基本信息表单*/
	basicFormApp: 'Shell.class.qms.file.file.create.Form',
	addTabPanelApp: 'Shell.class.qms.file.file.create.AddTabPanel',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Tree = me.getComponent('Tree');
		me.Grid.setAppOperationType();
		Tree.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					me.Grid.BDictTreeId = id;
					me.Grid.BDictTreeCName = record.get('text');
					if(id.length > 0 && id != "0") {
						me.Grid.revertSearchData();
						me.Grid.load();
					}
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('tid');
					me.Grid.BDictTreeId = id;
					me.Grid.BDictTreeCName = record.get('text');
					if(id.length > 0 && id != "0") {
						me.Grid.revertSearchData();
						me.Grid.load();
					}
				}, null, 500);
			}
		});

	},

	initComponent: function() {
		var me = this;
		me.basicFormApp = me.basicFormApp || 'Shell.class.qms.file.file.create.Form';
		me.addTabPanelApp = me.addTabPanelApp || 'Shell.class.qms.file.file.create.AddTabPanel';
		me.title = me.title || "文档操作";
		/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
		me.IDS = me.IDS || "";
		/**抄送人,阅读人的按人员选择时的角色姓名传入*/
		me.ROLEHREMPLOYEECNAME = me.ROLEHREMPLOYEECNAME || "";
		/**编辑文档类型(如新闻/通知/文档/修订文档)*/
		me.FTYPE = me.FTYPE || "";

		me.basicGrid = me.basicGrid || 'Shell.class.qms.file.basic.Grid';
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var tree = Ext.create('Shell.class.sysbase.dicttree.Tree', {
			region: 'west',
			width: 250,
			header: false,
			itemId: 'Tree',
			split: true,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			collapsible: true,
			rootVisible: (me.SHOWROOT === true || me.SHOWROOT === "true") ? true : false
		});
		me.Grid = Ext.create(me.basicGrid, {
			region: 'center',
			header: false,
			title: me.title || "文档操作",
			/**文件的操作记录类型*/
			fFileOperationType: me.fFileOperationType,
//			HiddenAgreeButton: me.HiddenAgreeButton,
//			AgreeButtonText: me.AgreeButtonText,
//			AgreeOperationType: me.AgreeOperationType,
//			HiddenDisagreeButton: me.HiddenDisagreeButton,
//			DisagreeButtonText: me.DisagreeButtonText,
//			DisagreeOperationType: me.DisagreeOperationType,
			
			isAddFFileOperation: me.isAddFFileOperation,
			isShowInvalid: me.isShowInvalid,
			/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			ROLEHREMPLOYEECNAME: me.ROLEHREMPLOYEECNAME,
			FTYPE: me.FTYPE,
			formtype: me.formtype,
			defaultWhere: "ffile.IsUse=1",
			itemId: 'Grid'
		});

		return [tree, me.Grid];
	},
	/**打开新增或编辑文档表单*/
	openFFileForm: function(record, fFileStatus, formtype) {
		var me = this;
		var id = "",
			OriginalFileID = "",
			BDictTreeId = me.Grid.BDictTreeId,
			BDictTreeCName = me.Grid.BDictTreeCName;
		if(record) {
			id = record.get('FFile_Id');
			BDictTreeId = record.get('FFile_BDictTree_Id');
			BDictTreeCName = record.get('FFile_BDictTree_CName');
			OriginalFileID = record.get('FFile_OriginalFileID');
			if(!OriginalFileID) {
				OriginalFileID = id;
			}
		}
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight - 30;
		var subNo = "";
		switch(me.FTYPE.toString()) {
			case "1": //文档
				break;
			case "2": //新闻
				break;
			case "3": //知识库
				break;
			case "4": //修订文档
				break;
			case "5": //帮助系统
				break;
			default:
				break;
		}
		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: me.FTYPE.toString() + "1",
			height: height,
			width: maxWidth,
			hasReset: me.Grid.hasReset,
			title: me.Grid.title || "编辑文档",
			formtype: formtype || 'add',
			BDictTreeId: BDictTreeId,
			BDictTreeCName: BDictTreeCName,
			fFileOperationType: me.fFileOperationType,
			fFileStatus: fFileStatus,
			HiddenAgreeButton: me.HiddenAgreeButton,
			AgreeButtonText: me.AgreeButtonText,
			AgreeOperationType: me.AgreeOperationType,
			HiddenDisagreeButton: me.HiddenDisagreeButton,
			DisagreeButtonText: me.DisagreeButtonText,
			DisagreeOperationType: me.DisagreeOperationType,
			FTYPE: me.FTYPE,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			AppOperationType: me.AppOperationType,
			hiddenRadiogroupChoose: me.hiddenRadiogroupChoose,
			/**功能按钮默认选中*/
			checkedRadiogroupChoose: me.checkedRadiogroupChoose,
			hasNextExecutor: me.hasNextExecutor,
			isAddFFileReadingLog: 0,
			isAddFFileOperation: 1,
			basicFormApp: me.basicFormApp,
			listeners: {
				save: function(win, e) {
					if(e.success) {
						me.Grid.onSearch();
						win.close();
					}
				}
			}
		};
		if(id && id != null && id != "") {
			config.formtype = formtype || 'edit';
			config.PK = id;
			config.FFileId = id;
			title: me.title || "编辑文档";
			config.OriginalFileID = OriginalFileID;
		}
		JShell.Win.open(me.addTabPanelApp, config).show(); //JShell.Win
	},
	/**
	 * 文档撤消文档状态操作
	 * @param {Object} statusValue 判断文档状态是否符合更新条件值
	 * @param {Object} updateValue 文档状态更新值
	 * @param {Object} fFileOperationType 文档操作类型值
	 */
	CancelFFileStatus: function(statusValue, updateValue, fFileOperationType) {
		var me = this;
	},
	/*文档列表的事件监听**/
	listenersGrid: function() {
		var me = this;
		me.Grid.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				var id = record.get('FFile_Id');
				var status = record.get('FFile_Status');
				switch(status) {
					case "1": //状态为暂存
					case "8": //撤消提交
				    case "9": //撤消审核
					case "15": //打回起草人
						if(me.FTYPE == "" || me.FTYPE.length < 1) {
							JShell.Msg.alert("当前应用的FTYPE为空,不能操作");
						} else {
							me.Grid.formtype = "edit";
							me.Grid.setAppOperationType();
							me.openFFileForm(record, 1, "edit");
						}
						break;
					default:
						if(me.FTYPE == "" || me.FTYPE.length < 1) {
							JShell.Msg.alert("当前应用的FTYPE为空,不能操作");
						} else {
							me.Grid.formtype = "show";
							me.Grid.openShowTabPanel(record);
						}
						break;
				}
			},
			onAddClick: function() {
				me.Grid.formtype = "add";
				me.formtype = "add";
				if(me.Grid.BDictTreeId == "0") {
					JShell.Msg.alert("不能选择根节点");
				} else if(me.FTYPE == "" || me.FTYPE.length < 1) {
					JShell.Msg.alert("当前应用的FTYPE为空,不能操作");
				} else if(!me.Grid.BDictTreeId) {
					JShell.Msg.alert("没有获取树信息");
				} else {
					me.openFFileForm(null, 1, "add");
				}
			},
			onShowClick: function() {
				var records = me.Grid.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				me.Grid.openShowTabPanel(records[0]);
			},
			//撤消提交操作
			onDisagreeSaveClick: function(grid) {
				me.CancelFFileStatus(2, 1, 8);
			}
		});
	}
});