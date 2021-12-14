/**
 * 帮助系统发布应用
 * @author longfc
 * @version 2016-09-22
 */
Ext.define('Shell.class.qms.file.help.release.App', {
	extend: 'Shell.class.qms.file.basic.App',
	title: '帮助信息发布',

	/**文件的操作记录类型*/
	fFileOperationType: 5,

	/**基本应用的文档确认(通过/同意)操作按钮是否显示*/
	HiddenAgreeButton: false,
	/**基本应用的文档确认(通过/同意)操作按钮显示名称*/
	AgreeButtonText: "发布",
	/**基本应用的文档确认(直接发布)操作按钮的功能类型*/
	AgreeOperationType: 5,

	/**基本应用的文档确认(不通过/不同意)操作按钮是否显示*/
	HiddenDisagreeButton: true,
	/**基本应用的文档确认(不通过/不同意)操作按钮显示名称*/
	DisagreeButtonText: "撤消禁用",
	
	/**基本应用的文档确认(不通过/不同意)操作按钮的功能类型*/
	DisagreeOperationType: 1,
	/**提交并发布的操作按钮是否显示*/
	HiddenPublishButton: false,
	/**隐藏阅读人信息*/
	HiddenFFileReadingLog: true,

	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, true, true, true, true],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [false, false, false, true],
	hasNextExecutor: false,
	basicGrid: 'Shell.class.qms.file.help.release.Grid',
	basicFormApp: 'Shell.class.qms.file.help.release.Form',
	addTabPanelApp: 'Shell.class.qms.file.help.release.AddTabPanel',
	FTYPE: '5',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.listenersGrid();
	},
	initComponent: function() {
		var me = this;
		me.FTYPE = '5';
		me.callParent(arguments);
	},
	/*列表的事件监听**/
	listenersGrid: function() {
		var me = this;
		var Grid = me.getComponent('Grid');
		Grid.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				var id = record.get('FFile_Id');
				Grid.formtype = "edit";
				Grid.setAppOperationType();
				var status = record.get('FFile_Status');
				me.openFFileForm(record, status, "edit");
			},
			onAddClick: function() {
				Grid.formtype = "add";
				me.formtype = "add";
				if(Grid.BDictTreeId == "0") {
					JShell.Msg.alert("不能选择根节点");
				} else if(me.FTYPE == "" || me.FTYPE.length < 1) {
					JShell.Msg.alert("当前应用的FTYPE为空,不能操作");
				} else if(Grid.BDictTreeId == null || Grid.BDictTreeId == "") {
					JShell.Msg.alert("没有获取树信息");
				} else {
					me.openFFileForm(null, 1, "add");
				}
			},
			onShowClick: function() {
				var records = Grid.getSelectionModel().getSelection();
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
			}
		});
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
		me.delText = (isUseValue == true ? "启用帮助文档" : "禁用帮助文档");
		if(tempArr && tempArr.length < 1) {
			JShell.Msg.alert("请选择符合【" + me.delText + "】条件的帮助文档操作!", null, 800);
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
						var msg = (isUseValue == true ? "启用帮助文档成功" : "禁用帮助文档成功");
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
	}
});