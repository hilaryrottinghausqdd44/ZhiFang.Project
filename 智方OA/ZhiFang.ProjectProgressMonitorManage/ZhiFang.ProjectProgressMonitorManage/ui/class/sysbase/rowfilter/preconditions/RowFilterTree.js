/***
 * 右区域数据过滤条件
 * 对外公开属性
 * isShowCopy:是否隐藏复制按钮,false:显示,true:隐藏
 * isShowPaste:是否隐藏粘贴按钮,false:显示,true:隐藏
 * isShowPredefinedAttributes:是否隐藏预定义属性按钮,false:显示,true:隐藏
 * @author longfc
 * @version 2017-05-02
 */
Ext.define('Shell.class.sysbase.rowfilter.preconditions.RowFilterTree', {
	extend: 'Shell.class.sysbase.rowfilter.basic.RowFilterTree',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '数据过滤条件角色树',

	width: 680,
	height: 460,
	/**是否预置条件的行数据还是单表的行数据*/
	IsPreconditions: true,
	AddPanel: 'Shell.class.sysbase.rowfilter.preconditions.AddPanel',
	selectUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_RJ_SearchRBACRowFilterTreeByModuleOperID',
	//复制角色权限
	copyUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_RJ_CopyRoleRightByModuleOperID',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				if(record) {
					var objectType = '' + record.get('objectType');
					if(objectType == 'RBACRowFilter') {
						var valueStr = record.get("value");
						var EntityCode = "",
							EntityCName = "";
						if(valueStr) {
							var tempArr = valueStr.split("|");
							if(tempArr.length == 2) {
								EntityCode = tempArr[0];
								EntityCName = tempArr[1];
							}
							var topToolbar2 = me.getComponent('topToolbar2');
							topToolbar2.getComponent('EntityCode').setValue(EntityCode);
							topToolbar2.getComponent('EntityCName').setValue(EntityCName);
						}
					}
				}
			},
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
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.rowfilter.preconditions.CheckGrid',
			emptyText: '新增必选',
			allowBlank: false,
			fieldLabel: '所属实体编码',
			name: 'EntityCName',
			itemId: 'EntityCName',
			classConfig: {
				defaultWhere: 'rbacpreconditions.RBACModuleOper.Id=' + me.moduleOpeId
			},
			listeners: {
				check: function(p, record) {
					var topToolbar2 = me.getComponent('topToolbar2');
					var EntityCode = topToolbar2.getComponent('EntityCode');
					var EntityCName = topToolbar2.getComponent('EntityCName');
					EntityCode.setValue(record ? record.get('RBACPreconditions_EntityCode') : '');
					EntityCName.setValue(record ? record.get('RBACPreconditions_EntityCName') : '');
					p.close();
				}
			}
		}, {
			fieldLabel: '实体编码',
			hidden: true,
			xtype: 'textfield',
			name: 'EntityCode',
			itemId: 'EntityCode'
		}, {
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
		}, '-', {
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
	changeClassConfig: function(moduleOpeId) {
		var me = this;
		var topToolbar2 = me.getComponent('topToolbar2');
		var EntityCode = topToolbar2.getComponent('EntityCode');
		var EntityCName = topToolbar2.getComponent('EntityCName');
		if(moduleOpeId) me.moduleOpeId = moduleOpeId;
		var data = {
			defaultWhere: 'rbacpreconditions.RBACModuleOper.Id=' + moduleOpeId
		};
		EntityCName.setClassConfig(data);

		EntityCode.setValue("");
		EntityCName.setValue("");
	},
	loadData: function(moduleOpeId, moduleOperSelect) {
		var me = this;
		if(moduleOperSelect) me.moduleOperSelect = moduleOperSelect;
		me.changeClassConfig(moduleOpeId);
		me.load(moduleOpeId);
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
		me.moduleOperId = null;
		me.moduleOperSelect = null;
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
		var id = -1;
		if(!me.moduleOperId) {
			JShell.Msg.alert('请先选中模块操作数据行', null, 1000);
			return;
		}
		var title = '',
			EntityCode = "",
			EntityCName = "";
		//模块操作选中行记录
		if(record) {
			title = "修改数据过滤条件>>";
			id = record.get('tid');
			var valueStr = record.get("value");
			if(valueStr) {
				var tempArr = valueStr.split("|");
				if(tempArr.length == 2) {
					EntityCode = tempArr[0];
					EntityCName = tempArr[1];
				}
			}
		} else {
			title = "新增数据过滤条件>>";
			var topToolbar2 = me.getComponent('topToolbar2');
			EntityCode = topToolbar2.getComponent('EntityCode').getValue();
			EntityCName = topToolbar2.getComponent('EntityCName').getValue();
		}
		if(!EntityCode) {
			JShell.Msg.alert('请先选择所属实体分类后再操作!', null, 1000);
			return;
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
			width: maxWidth,
			height: maxHeight,
			maxWidth: maxWidth,
			maxHeight: maxHeight,
			layout: 'border',
			PK: id, //数据过滤条件的行记录ID
			moduleId: me.moduleId,
			moduleOperId: me.moduleOperId, //模块操作id						
			objectName: me.objectName, //objectName数据对象
			objectCName: me.objectCName, //objectName中文数据对象			
			EntityCode: EntityCode,
			EntityCName: EntityCName,
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