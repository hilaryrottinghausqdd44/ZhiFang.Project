/***
 * 右区域数据过滤条件
 * 对外公开属性
 * isShowCopy:是否隐藏复制按钮,false:显示,true:隐藏
 * isShowPaste:是否隐藏粘贴按钮,false:显示,true:隐藏
 * isShowPredefinedAttributes:是否隐藏预定义属性按钮,false:显示,true:隐藏
 * @author longfc
 * @version 2017-05-02
 */
Ext.define('Shell.class.sysbase.rowfilter.datafilters.RowFilterTree', {
	extend: 'Shell.class.sysbase.rowfilter.basic.RowFilterTree',
	title: '数据过滤条件角色树',

	width: 680,
	height: 460,

	isFiltration: true,
	isFunction: false,
	isbuttonBar: false,

	AddPanel: 'Shell.class.sysbase.rowfilter.datafilters.AddPanel',
	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_RJ_SearchRBACRowFilterTreeByModuleOperID',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				if(record && record != null) {
					var objectType = '' + record.get('objectType');
					//角色节点
					if(objectType == 'RBACRole') {
						var tid = record.get('tid');
						var roleCName = record.get('text');
						var hqlWhere = 'rbacemproles.RBACRole.Id=' + tid;
						me.openRoleListShowWin(hqlWhere, roleCName);
					} else if(objectType == 'RBACRowFilter') {
						me.openAppEditWin('edit', record);
					}
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push({
			xtype: 'button',
			iconCls: 'button-edit',
			text: '维护预定义可选属性',
			itemId: 'btnPredefinedAttributes',
			name: 'btnPredefinedAttributes',
			//cls: "btn btn-default btn-sm active",
			style: {
				marginRight: '10px'
			},
			listeners: {
				click: function(com, e, eOpts) {
					me.openPredefinedAttributesWin();
				}
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-add',
			text: '新增过滤条件',
			itemId: 'btnAddDatafilters',
			name: 'btnAddDatafilters',
			//cls: "btn btn-default btn-sm active",
			style: {
				marginLeft: '10px'
			},
			listeners: {
				click: function(com, e, eOpts) {
					me.openAppEditWin('add', null);
				}
			}
		}, {
			xtype: 'button',
			text: '复制',
			itemId: 'btnCopy'
			//cls: "btn btn-default btn-sm active"
		}, {
			xtype: 'button',
			iconCls: 'button-saveas',
			text: '粘贴',
			itemId: 'btnPaste',
			hidden: true
			//cls: "btn btn-default btn-sm active"
		}, {
			xtype: 'button',
			iconCls: 'button-cancel',
			text: '取消',
			itemId: 'btnCancel',
			hidden: true
			//cls: "btn btn-default btn-sm active"
		});
		return {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'topToolbar2',
			items: items
		};
	},
	//打开预定义可选属性设置页面
	openPredefinedAttributesWin: function() {
		var me = this;
		if(me.objectName) {
			var roleLists = [];
			var maxWidth = 360; // document.body.clientWidth * 0.42;
			var maxHeight = document.body.clientHeight * 0.96;
			var config = {
				width: maxWidth,
				height: maxHeight,
				maxWidth: maxWidth,
				maxHeight: maxHeight,
				moduleOperId: me.moduleOperId,
				objectName: me.objectName,
				objectCName: me.objectCName,
				layout: 'border',
				resizable: false, //可变大小
				closable: true, //有关闭按钮
				draggable: true,
				listeners: {
					onsave: function(p, data) {
						if(data.success) {
							win.close();
						} else {
							JShell.Msg.error("预定义可选属性保存操作失败!<br />" + data.msg);
						}
					},
					oncancel: function() {
						win.close();
					}
				}
			};
			var win = JShell.Win.open('Shell.class.sysbase.rowfilter.datafilters.tree.PredefinedAttributesTree', config).show();
		} else {
			JShell.Msg.alert('获取不到模块操作的数据对象', null, 1000);
		}
	},
	loadData: function(id, moduleOperSelect) {
		var me = this;
		if(moduleOperSelect) me.moduleOperSelect = moduleOperSelect;
		me.load(id);
	},
	initListeners: function() {
		var me = this;
		var topToolbar2 = me.getComponent('topToolbar2');
		//复制按钮
		var btnCopy = topToolbar2.getComponent('btnCopy');
		//粘贴按钮
		var btnPaste = topToolbar2.getComponent('btnPaste');
		var btnCancel = topToolbar2.getComponent('btnCancel');
		//清空粘贴信息
		btnCancel.on({
			click: function(com, e, eOpts) {
				me.clearOperateInfo();
				me.ismodulePaste = false;
				btnPaste.setVisible(false);
				btnCancel.setVisible(false);
			}
		});
		//复制按钮
		btnCopy.on({
			click: function(com, e, eOpts) {
				me.copyRBACRowFilter(btnCopy, btnPaste, btnCancel);
			}
		});
		//粘贴按钮
		btnPaste.on({
			click: function(com, e, eOpts) {
				me.pasteRBACRowFilter(btnCopy, btnPaste, btnCancel);
			}
		});

		me.on({
			saveClick: function(treepanel, selectId) {
				me.selectId = selectId;
			}
		});
	},
	copyRBACRowFilter: function(btnCopy, btnPaste, btnCancel) {
		var me = this;
		var moRecord = me.moduleOperSelect;
		if(moRecord) {
			var cName = moRecord.get('RBACModuleOper_CName');
			if(me.operateInfo) {
				if(!me.objectName) {
					me.operateInfo["moduleCopy"] = {
						Id: '',
						CName: ''
					};
					me.operateInfo["objectCName"] = '';
					me.operateInfo["modulePasteCName"] = "";
					me.operateInfo["moduleCopyCName"] = "";
					me.ismodulePaste = false;
					btnPaste.setVisible(false);
				} else {
					me.operateInfo["moduleCopy"] = {
						Id: me.moduleOperId,
						CName: cName
					};
					me.operateInfo["objectCName"] = me.objectName;
					me.operateInfo["moduleCopyCName"] = moRecord.get('RBACModuleOper_RBACModule_CName');
					me.ismodulePaste = true;
					btnPaste.setVisible(true);
					btnCancel.setVisible(false);
				}
			}
		}
	},
	pasteRBACRowFilter: function(btnCopy, btnPaste, btnCancel) {
		var me = this;
		if(me.operateInfo) {
			var moduleCopy = me.operateInfo["moduleCopy"];
			var modulePaste = me.operateInfo["modulePaste"];
			if(moduleCopy == "") {
				JShell.Msg.alert("请先操作复制按钮功能", null, 1000);
			}
			if(modulePaste == "") {
				JShell.Msg.alert("请选择需要粘贴的模块操作", null, 1000);
			} else {
				if(moduleCopy != "" && modulePaste == "") {
					JShell.Msg.alert("请选择需要粘贴的模块操作", null, 1000);
				} else if(moduleCopy != "" && modulePaste != "") {
					me.copyAndPasteSave(btnCopy, btnPaste, btnCancel);
				}
			}
		} else {
			JShell.Msg.alert("请先操作复制按钮功能", null, 1000);
		}
	},
	clearTreeData: function() {
		var me = this;
		me.preconditionsId = null;
		me.preconditionsSelect = null;
		me.objectName = null;
		me.objectCName = null;
		var root = me.load("");
	},
	clearOperateInfo: function() {
		var me = this;
		//清空粘贴信息
		me.operateInfo["objectCName"] = '';
		me.operateInfo["moduleCopy"] = {
			Id: '',
			CName: ''
		};
		me.operateInfo["modulePaste"] = {
			Id: '',
			CName: ''
		};
	},
	setButtonVisible: function(bo) {
		var me = this;
		var topToolbar2 = me.getComponent('topToolbar2');
		//复制按钮
		var btnCopy = topToolbar2.getComponent('btnCopy');
		//粘贴按钮
		var btnPaste = topToolbar2.getComponent('btnPaste');
		var btnCancel = topToolbar2.getComponent('btnCancel');
		btnPaste.setVisible(bo);
		btnCancel.setVisible(bo);
	},
	copyAndPasteSave: function(btnCopy, btnPaste, btnCancel) {
		var me = this;
		var moduleCopyId = me.operateInfo["moduleCopy"]["Id"];
		var modulePasteId = me.operateInfo["modulePaste"]["Id"];
		if(moduleCopyId == modulePasteId) {
			JShell.Msg.alert("复制和粘贴的模块操作不能相同");
			btnPaste.setVisible(false);
			return;
		}
		if(!moduleCopyId || !modulePasteId) {
			JShell.Msg.alert("复制和粘贴的模块操作不能为空");
			btnPaste.setVisible(false);
			return;
		}
		var modulePasteCName = me.operateInfo["modulePasteCName"];
		var moduleCopyCName = me.operateInfo["moduleCopyCName"];

		var moduleOCopyCName = me.operateInfo["moduleCopy"]["CName"];
		var moduleOPasteCName = me.operateInfo["modulePaste"]["CName"];
		var msg = "复制模块为【" + moduleCopyCName + "】" + "的模块操作【" + moduleOCopyCName + "】的所有行过滤条件<br/>" +
			"粘贴到模块为【" + modulePasteCName + "】" + "模块操作【" + moduleOPasteCName + "】"

		Ext.Msg.confirm('【复制】&&【粘贴】行数据条件操作', "是否要继续" + msg + "操作?",
			function(button) {
				if(button == 'yes') {
					if(me.hasLoadMask) {
						me.mk = me.mk || new Ext.LoadMask(me.getEl(), {
							msg: '操作数据中...',
							removeMask: true
						});
						me.mk.show(); //显示遮罩层
					}
					var url = me.copyUrl + "?sourceModuleOperID=" + moduleCopyId + "&targetModuleOperID=" + modulePasteId;
					//sourceModuleOperID:源模块操作ID ,targetModuleOperID:目标模块操作ID
					JShell.Server.get(url, function(data) {
						me.setButtonVisible(false);
						if(me.hasLoadMask && me.mk) {
							me.mk.hide();
						} //隐藏遮罩层
						if(data.success) {
							Ext.Msg.show({
								title: '【复制】&&【粘贴】功能操作',
								msg: +"操作成功!",
								width: 380,
								buttons: [],
								icon: Ext.window.MessageBox.INFO
							});
							me.clearOperateInfo();
							me.ismodulePaste = false;
							me.load(modulePasteId);
						} else {
							JShell.Msg.error(data.msg);
						}
					}, null, false);
				}
			});
	},
	//打开新增或编辑数据过滤条件设置应用
	openAppEditWin: function(appType, record) {
		var me = this;
		if(!me.moduleOperId) {
			JShell.Msg.alert('请先选中模块操作数据行', null, 1000);
			return;
		}
		if(!me.objectName) {
			JShell.Msg.alert('当前模块操作没有行过滤条件依据对象', null, 1000);
			return;
		}
		var id = -1;
		var title = '';
		var setformTitle = '';
		//模块操作选中行记录
		if(record) {
			title = "修改数据过滤条件>>";
			id = record.get('tid');
		} else {
			title = "新增数据过滤条件>>";
		}
		//行过滤条件是否模块操作的默认行过滤查询条件(模块操作的行过滤条件Id不为空时为默认)
		var isDefaultCondition = false;
		if(appType=="edit"&&me.moduleOperSelect.get('RBACModuleOper_RBACRowFilter_Id')) {
			isDefaultCondition = true;
		}
		var maxWidth = document.body.clientWidth * 0.92;
		var maxHeight = document.body.clientHeight * 0.96;
		var config = {
			title: title,
			SUB_WIN_NO: "1",
			setformTitle: setformTitle,
			isShowPredefinedAttributes: false,
			width: maxWidth,
			height: maxHeight,
			maxWidth: maxWidth,
			maxHeight: maxHeight,
			layout: 'border',
			PK: id, //数据过滤条件的行记录ID
			filtersTree: me, //行过滤条件角色树
			moduleId: me.moduleId,
			moduleOperId: me.moduleOperId, //模块操作id						
			objectName: me.objectName, //objectName数据对象
			objectCName: me.objectCName, //objectName中文数据对象
			isDefaultCondition: isDefaultCondition,
			moduleOperSelect: me.moduleOperSelect, //模块操作选中行记录
			appType: appType,
			resizable: true, //可变大小
			closable: true, //有关闭按钮
			draggable: true,
			listeners: {
				saveClick: function(win, rowId) {
					me.fireEvent('saveClick', me, rowId);
					me.load(me.moduleOperId);
					win.close();
				},
				comeBackClick: function(win) {
					win.close();
				}
			}
		};
		JShell.Win.open(me.AddPanel, config).show();
	}
});