/**
 * 文档信息维护应用
 * @author longfc
 * @version 2016-08-04
 */
Ext.define('Shell.class.qms.file.manage.ManageApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '文档维护',
	checkOne: false,

	FTYPE: '',
	IDS: '',
	interactionType: "show",
	HiddenDisagreeOfGrid: false,
	DisagreeOfGridText: "撤消禁用",
	defaultWhere: '',
	hasReset: false,
	hasRefresh: false,
	hasShow: false,
	basicGrid: 'Shell.class.qms.file.manage.Grid',
	/**是否显示物理删除按钮*/
	HASDEL: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Grid = me.getComponent('Grid');
		Grid.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				Grid.openShowTabPanel(record);
			},
			onShowClick: function() {
				var me = this;
				var records = me.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				Grid.openShowTabPanel(records[0]);
			},
			onButtonLockClick: function(grid) {
				me.EditFFileIsUse(false);
			},
			onDisagreeSaveClick: function(grid) {
				me.EditFFileIsUse(true);
			},
			ondoTopClick: function(grid) {
				me.UpdateFFileIsTopByIds(true);
			},
			ondoNoTopClick: function(grid) {
				me.UpdateFFileIsTopByIds(false);
			}
		});
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
		me.FTYPE = me.FTYPE || '';
		me.title = me.title || "文档详情";
		me.basicGrid = me.basicGrid || 'Shell.class.qms.file.manage.Grid';
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "( (ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "')  or (ffile.EndTime>='" + dt + "') )";

		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**
	 * 文档撤消文档状态操作
	 * @param {Object} statusValue 判断文档状态是否符合更新条件值
	 * @param {Object} updateValue 文档状态更新值
	 * @param {Object} fFileOperationType 文档操作类型值
	 */
	EditFFileIsUse: function(isUseValue) {
		var me = this;
		var Grid = me.getComponent('Grid');
		var records = Grid.getSelectionModel().getSelection();
		if(records && records.length < 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var strIds = "",
			isUseStr = (isUseValue == true ? "1" : "0");
		var tempArr = [];
		for(var i = 0; i < records.length; i++) {
			var isUse = records[i].get("FFile_IsUse");
			if(isUse.toString() != isUseStr) {
				strIds = strIds + records[i].get("FFile_Id") + ",";
				tempArr.push(records[i]);
			}
		}
		me.delText = (isUseValue == true ? "启用文档" : "禁用文档");
		if(tempArr && tempArr.length < 1) {
			JShell.Msg.alert("请选择符合【" + me.delText + "】条件的文档操作!", null, 800);
			return;
		}
		if(strIds.length > 1)
			Ext.MessageBox.show({
				title: me.delText + '操作确认消息',
				msg: "请确认是否" + me.delText + "操作",
				width: 300,
				buttons: Ext.MessageBox.OKCANCEL,
				fn: function(btn) {
					if(btn == 'ok') {
						strIds = strIds.substring(0, strIds.length - 1);
						var url = (Grid.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + Grid.delUrl;
						fFileOperationType = (isUseValue == true ? "14" : "13");
						var entity = {
							"strIds": strIds,
							"isUse": isUseValue,
							"fFileOperationType": fFileOperationType
						};

						params = Ext.JSON.encode(entity);
						var msg = (isUseValue == true ? "启用文档成功" : "禁用文档成功");
						setTimeout(function() {
							JShell.Server.post(url, params, function(data) {
								me.hideMask(); //隐藏遮罩层
								if(data.success) {
									Ext.Array.each(tempArr, function(record, index, arr) {
										record.set("FFile_IsUse", isUseStr);
										record.set(me.DelField, true);
										record.set('ErrorInfo', msg);
										record.commit();
									});
								} else {
									Ext.Array.each(tempArr, function(record, index, arr) {
										record.set(me.DelField, false);
										record.set('ErrorInfo', data.msg);
										record.commit();
									});
								}
								me.hideMask(); //隐藏遮罩层
								if(data.success) {
									JShell.Msg.alert(me.delText + '成功', null, 800);
								} else {
									JShell.Msg.error('存在操作失败信息，具体错误内容请查看数据行的失败提示！');
								}
							});
						}, 100);
					}
				}
			});
	},
	/**
	 * 置顶/撤消置顶文档信息
	 * @param {Object} statusValue 判断文档状态是否符合更新条件值
	 * @param {Object} updateValue 文档状态更新值
	 * @param {Object} fFileOperationType 文档操作类型值
	 */
	UpdateFFileIsTopByIds: function(isTopValue) {
		var me = this;
		var Grid = me.getComponent('Grid');
		var records = Grid.getSelectionModel().getSelection();
		if(records && records.length < 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var strIds = "",
			isTopStr = (isTopValue == true ? "1" : "0");
		var tempArr = [];
		for(var i = 0; i < records.length; i++) {
			var isTop = records[i].get("FFile_IsTop");
			if(isTop.toString() != isTopStr) {
				strIds = strIds + records[i].get("FFile_Id") + ",";
				tempArr.push(records[i]);
			}
		}
		me.delText = (isTopValue == true ? "置顶" : "撤消置顶");
		if(tempArr && tempArr.length < 1) {
			JShell.Msg.alert("请选择符合【" + me.delText + "】条件的文档操作!", null, 800);
			return;
		}
		if(strIds.length > 1)
			Ext.MessageBox.show({
				title: me.delText + '操作确认消息',
				msg: "请确认是否" + me.delText + "操作",
				width: 300,
				buttons: Ext.MessageBox.OKCANCEL,
				fn: function(btn) {
					if(btn == 'ok') {
						strIds = strIds.substring(0, strIds.length - 1);
						var url = (Grid.editIsTopUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + Grid.editIsTopUrl;
						fFileOperationType = (isTopValue == true ? "16" : "17");
						var entity = {
							"strIds": strIds,
							"isTop": isTopValue,
							"fFileOperationType": fFileOperationType
						};

						params = Ext.JSON.encode(entity);
						var msg = (isTopValue == true ? "置顶成功" : "撤消置顶成功");

						setTimeout(function() {
							JShell.Server.post(url, params, function(data) {
								me.hideMask(); //隐藏遮罩层
								if(data.success) {
									Ext.Array.each(tempArr, function(record, index, arr) {
										record.set("FFile_IsTop", isTopStr);
										record.set(me.DelField, true);
										record.set('ErrorInfo', msg);
										record.commit();
									});
								} else {
									Ext.Array.each(tempArr, function(record, index, arr) {
										record.set(me.DelField, false);
										record.set('ErrorInfo', data.msg);
										record.commit();
									});
								}
								me.hideMask(); //隐藏遮罩层
								if(data.success) {
									JShell.Msg.alert(me.delText + '成功', null, 800);
								} else {
									JShell.Msg.error('存在操作失败信息，具体错误内容请查看数据行的失败提示！');
								}
								Grid.onSearch();
							});
						}, 100);
					}
				}
			});
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
			title: me.title || "文档查看",
			FTYPE: me.FTYPE,
			IDS: me.IDS,
			/**是否显示物理删除按钮*/
			hasDel: me.HASDEL,
			fFileOperationType: me.fFileOperationType,
			interactionType: 'show',
			DisagreeOfGridText: me.DisagreeOfGridText,
			HiddenDisagreeOfGrid: me.HiddenDisagreeOfGrid,
			defaultWhere: me.defaultWhere,
			hasRefresh: me.hasRefresh,
			hasShow: me.hasShow,
			itemId: 'Grid'
		});
		return [tree, Grid];
	}

});