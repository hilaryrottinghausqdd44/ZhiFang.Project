/**
 * 文档审核
 * @author liangyl	
 * @version 2016-07-3
 */
Ext.define('Shell.class.qms.file.file.examine.Form', {
	extend: 'Shell.class.qms.file.show.ShowTabPanel',
	header: true,
	activeTab: 0,
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.class.qms.file.basic.toolbarButtons'
	],
	IsDiscuss: 'false',
	title: '文档审核',
	border: false,
	/**修改服务地址*/
	editUrl: '/QMSService.svc/QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField',
	/**文档按钮text(同意)*/
	AgreeButtonText: '',
	/**文档操作记录类型(同意)*/
	fFileOperationType: '',
	/**文档状态（同意）*/
	fFileStatus: '',
	/**文档操作记录备注(同意)*/
	ffileOperationMemo: '',
	/**文档状态（同意)*/
	AgreeOperationType: '3',
	DisagreeButtonText: '',
	/**文档状态（不同意)*/
	DisagreeOperationType: 3,
	/**文档操作记录类型(不同意)*/
	agreefFileOperationType: '',
	/**文档操作记录备注(不同意)*/
	disffileMemo: '',
	DisagreeStatus: 3,
	/**不同意/通过按钮隐藏*/
	hiddenDisagreeButton: false,
	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, false, false, false],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [true, false, false],
	boxLabelRadiogroupChoose: ['仅审核', '自动批准'],
	/**创建者是登陆者*/
	IsCreator: false,
	/**文档Id*/
	FFileId: '',
	/**选择行的文档状态*/
	Status: '',
	/**存在下一执行者*/
	hasNextExecutor: true,
	/**下一执行者为空时提示信息*/
	NextExecutorMsg: '',
	MemoPrefix: '',
	IsChangeStatus: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.doNextExecutorListeners();
		me.on({
			save: function() {
				me.fireEvent('save', me);
				me.close();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = [];
		me.bodyPadding = 1;
		me.dockedItems = me.createDockedItems();

		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		var buttontoolbar = me.createButtontoolbar();
		if(buttontoolbar) items.push(buttontoolbar);
		return items;
	},

	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			//items = [];
			toolbar = Ext.create('Shell.class.qms.file.basic.toolbarButtons', {
				dock: 'bottom',
				itemId: 'buttonsToolbar',
				AgreeButtonText: me.AgreeButtonText,
				DisagreeButtonText: me.DisagreeButtonText,
				HiddenAgreeButton: me.HiddenAgreeButton,
				checkedRadiogroupChoose: me.checkedRadiogroupChoose,
				boxLabelRadiogroupChoose: me.boxLabelRadiogroupChoose,
				HiddenDisagreeButton: me.HiddenDisagreeButton,
				hiddenRadiogroupChoose: me.hiddenRadiogroupChoose,
				RoleHREmployeeCName: me.RoleHREmployeeCName,
				hasNextExecutor: me.hasNextExecutor,
				MemoPrefix: me.MemoPrefix,
				listeners: {
					onCloseClick: function() {
						me.close();
					},
					onradiogroupchange: function(rdgroup, checked, returnDatas) {
						var btnAgree = me.getComponent("buttonsToolbar").getComponent('btnAgree');
						var btnDisagree = me.getComponent("buttonsToolbar").getComponent('btnDisagree');
						var agreebtnText = '',
							disagreebtnText = '',
							fieldLabel;
						var radioItem = me.getComponent("buttonsToolbar").getComponent('radiogroupChoose');
						var NextExecutorName = me.getComponent("buttonsToolbar").getComponent('NextExecutorName');
						if(radioItem) {
							var checked2 = radioItem.getValue();
							var obj = {
								checkOne: true,
								RoleHREmployeeCName: JcallShell.QMS.Enum.getEmployeeRoleName("r2")
							};
							switch(checked2.choose) {
								case 3: //自动审核
									agreebtnText = '通过';
									disagreebtnText = '不通过';
									btnDisagree.setVisible(true);
									me.fFileStatus = 3;
									me.fFileOperationType = 3;
									me.NextExecutorMsg = "下一执行者【审批人】信息不能为空!";
									me.agreefFileOperationType = 9;
									fieldLabel = "审批人";
									if(me.AgreeOperationType == 3) {
										me.ffileOperationMemo = "直接自动审核";
									}
									obj.RoleHREmployeeCName = JcallShell.QMS.Enum.getEmployeeRoleName("r3");
									break;
								case 4:
									if(me.AgreeOperationType == 4) {
										me.ffileOperationMemo = "直接自动审批";
									} else {
										me.ffileOperationMemo = me.MemoPrefix + '并直接自动审批';
									}
									agreebtnText = '同意';
									disagreebtnText = '不同意';
									btnDisagree.setVisible(true);
									fieldLabel = "发布人";
									me.fFileStatus = 4;
									me.fFileOperationType = 4;
									me.NextExecutorMsg = "下一执行者【发布人】信息不能为空!";
									me.agreefFileOperationType = 10;
									obj.RoleHREmployeeCName = JcallShell.QMS.Enum.getEmployeeRoleName("r4");
									break;
								case 5:
									me.fFileStatus = 5;
									me.fFileOperationType = 5;
									agreebtnText = '发布';
									me.ffileOperationMemo = "自动发布";
									if(me.AgreeOperationType == 5) {
										me.ffileOperationMemo = "直自动发布";
									} else {
										me.ffileOperationMemo = me.MemoPrefix + '并自动发布';
									}
									if(btnDisagree) {
										btnDisagree.setVisible(false);
									}
									me.agreefFileOperationType = 15;
									break;
								default:
									break;
							}
							if(btnAgree) {
								btnAgree.setText(agreebtnText);
							}
							if(btnDisagree) {
								btnDisagree.setText(disagreebtnText);
							}
							if(NextExecutorName) {
								NextExecutorName.setFieldLabel(fieldLabel);
							}
							NextExecutorName.changeClassConfig(obj);
						}
					},
					onAgreeSaveClick: function() {
						me.onAgreeSaveClick();
					},
					onDisagreeSaveClick: function() {
						me.onDisagreeSaveClick();
					}
				}
			});
		return toolbar;
	},
	/**打开新增或编辑文档表单*/
	openFFileForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.qms.file.file.release.ReleaseForm', {
			PK: id,
			listeners: {
				save: function(win, params) {
					me.saveffile(params);
					win.close();
				},
				load: function(from, data) {
					JShell.Action.delay(function() {
						//抄送人信息获取处理
						var userComboBox = from.getComponent('FFileReadingUser');
						if(id != "") {
							from.formtype = 'edit';
							userComboBox.loadDataById(id);
						}
					}, null, 100);
				}
			}
		}).show();

	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var entity = {},
			msg = '',
			NextExecutorId, NextExecutorName;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var comId = me.getComponent("buttonsToolbar").getComponent('NextExecutorId');
		if(comId) {
			NextExecutorId = comId.getValue();
		}
		var comName = me.getComponent("buttonsToolbar").getComponent('NextExecutorName');
		if(comName) {
			NextExecutorName = comName.getValue();
		}

		var radioItem = me.getComponent("buttonsToolbar").getComponent('radiogroupChoose');
		if(radioItem) {
			var checked = radioItem.getValue();
			switch(checked.choose) {
				case 3: //审核
					entity.CheckerId = userId;
					entity.CheckerName = userName;
					entity.ApprovalId = NextExecutorId;
					entity.ApprovalName = NextExecutorName;
					me.ffileMemo = '审核';
					break;
				case 4: //审批
					entity.CheckerId = userId;
					entity.CheckerName = userName;
					//审批人
					entity.ApprovalId = userId;
					entity.ApprovalName = userName;
					//发布人
					entity.PublisherId = NextExecutorId;
					entity.PublisherName = NextExecutorName;
					me.ffileMemo = '审批';
					break;
				default:
					entity.CheckerId = userId;
					entity.CheckerName = userName;
					//审批人
					entity.ApprovalId = userId;
					entity.ApprovalName = userName;
					//发布人
					entity.PublisherId = userId;
					entity.PublisherName = userName;
					break;
			}
			return {
				entity: entity,
				ffileOperationMemo: me.ffileOperationMemo
			};
		}
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			entity = me.getAddParams();
		var fields = ['Id', 'Status'];
		var fFileOperationType = me.fFileOperationType;
		var Status = me.fFileStatus;
		//发布信息处理
		fields.push('BeginTime', 'EndTime', 'IsTop', 'IsDiscuss');
		
		//fields.push('CheckerId', 'CheckerName', 'ApprovalId', 'ApprovalName', 'PublisherId,PublisherName','CheckerDateTime', 'ApprovalDateTime', 'PublisherDateTime');
		if(entity) {
			entity.fields = fields.join(',');
			entity.entity.Id = me.FFileId;
			entity.fFileOperationType = fFileOperationType;
			entity.entity.Status = Status;
		}
		return entity;
	},
	/*文档新增及编辑保存*/
	saveffile: function(paramsRelease) {
		var me = this;
		var url = me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = me.getEditParams();
		if(!params) return;
		var msg = "请确认是否" + me.ffileMemo + "?";
		//抄送对象类型, 默认没有选择
		params.ffileCopyUserType = -1;
		params.fFileCopyUserList = [];
		params.ffileReadingUserType = -1;
		params.fFileReadingUserList = [];
		params.ffileOperationMemo = me.ffileOperationMemo;
		//发布信息处理
		if(paramsRelease != null) {
			params.ffileReadingUserType = paramsRelease.ffileReadingUserType;
			params.fFileReadingUserList = paramsRelease.fFileReadingUserList;
			if(paramsRelease.entity != null) {
				params.entity.IsTop = paramsRelease.entity.IsTop;
				params.entity.IsDiscuss = paramsRelease.entity.IsDiscuss;
				if(paramsRelease.entity.BeginTime) {
					params.entity.BeginTime = paramsRelease.entity.BeginTime;
				}
				if(paramsRelease.entity.EndTime) {
					params.entity.EndTime = paramsRelease.entity.EndTime;
				}
			}
		}
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.fireEvent('save', me);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		});
	},

	/**确定提交按钮点击处理方法*/
	onAgreeSaveClick: function() {
		var me = this;
		var isExec = true,
			itemId = "",
			msg = "";
		if(me.AgreeOperationType == 5 || me.fFileOperationType == 5) {
			me.fFileStatus = 5;
			me.fFileOperationType = 5;
			me.ffileOperationMemo = "自动发布";
			me.openFFileForm(me.FFileId);
			isExec = false;
		}
		//文档状态如果不是发布
		if(isExec && me.fFileStatus != 5 && me.fFileStatus != 7) {
			//下一执行者处理
			var NextExecutorId = "";
			var comId = me.getComponent("buttonsToolbar").getComponent('NextExecutorId');
			if(comId) {
				NextExecutorId = comId.getValue();
			}
			if(NextExecutorId == "") {
				JShell.Msg.error(me.NextExecutorMsg);
				isExec = false;
			}
		}
		if(isExec) {
			msg = "请确认是否" + me.ffileOperationMemo + "?";
			Ext.MessageBox.show({
				title: '操作确认消息',
				msg: msg,
				width: 300,
				buttons: Ext.MessageBox.OKCANCEL,
				fn: function(btn) {
					if(btn == 'ok') {
						me.saveffile(null);
					}
				},
				icon: Ext.MessageBox.QUESTION
			});
		}
	},
	/**不通过按钮点击处理方法*/
	onDisagreeSaveClick: function() {
		var me = this;
		var msg = "请确认是否" + me.disffileMemo + "?";
		Ext.MessageBox.show({
			title: '操作确认消息',
			msg: msg,
			width: 300,
			buttons: Ext.MessageBox.OKCANCEL,
			fn: function(btn) {
				if(btn == 'ok') {
					me.UpdateFFileStatus(me.DisagreeStatus, me.FFileId, me.agreefFileOperationType);
//					if(me.IsChangeStatus) {
//						me.UpdateFFileStatus(me.DisagreeStatus, me.FFileId, me.agreefFileOperationType);
//					} else {
//						me.AddFFileOperation(me.FFileId, me.agreefFileOperationType, me.disffileMemo);
//					}
				}
			},
			icon: Ext.MessageBox.QUESTION
		});
	},
	/**文档的审核通过/不通过;同意/不同意*/
	AddFFileOperation: function(id, type, ffileMemo) {
		var me = this;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var DictTypeDataTimeStamp = '1,2,3,4,5,6,7,8';
		var url = '/QMSService.svc/QMS_UDTO_AddFFileOperation';
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var entity = {
			Type: type,
			Memo: ffileMemo,
			IsUse: 1,
			CreatorID: userId,
			CreatorName: userName,
			DataAddTime: JShell.Date.toServerDate(new Date()),
			FFile: {
				Id: id,
				DataTimeStamp: DictTypeDataTimeStamp.split(",")
			}
		};
		var params = {
			entity: entity
		};
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.fireEvent('save', me);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		});
	},
	/**监听*/
	doNextExecutorListeners: function() {
		var me = this;
		var CName = me.getComponent("buttonsToolbar").getComponent('NextExecutorName');
		var Id = me.getComponent("buttonsToolbar").getComponent('NextExecutorId');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('RBACEmpRoles_HREmployee_CName') : '');
				Id.setValue(record ? record.get('RBACEmpRoles_HREmployee_Id') : '');
				p.close();
			}
		});
	},
	/**文档的审核通过/不通过;同意/不同意*/
	UpdateFFileStatus: function(fFileStatus, id, fFileOperationType) {
		var me = this;
		var url = me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var entity = {
			Status: fFileStatus,
			Id: id
		};
		var fields = 'Id,Status';
		var params = {
			entity: entity,
			//文档操作记录备注信息
			ffileOperationMemo: me.disffileMemo,
			fFileOperationType: fFileOperationType,
			ffileCopyUserType: -1,
			fFileCopyUserList: [],
			ffileReadingUserType: -1,
			fFileReadingUserList: [],
			fields: fields
		};
		//抄送人信息
		var ffileCopyUser = null;
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.fireEvent('save', me);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	}
});