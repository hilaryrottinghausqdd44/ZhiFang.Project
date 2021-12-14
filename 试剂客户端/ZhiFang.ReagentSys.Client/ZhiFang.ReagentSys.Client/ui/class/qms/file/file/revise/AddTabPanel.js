/**
 * 修订文档及编辑应用
 * @author 
 * @version 2016-07-05
 */
Ext.define('Shell.class.qms.file.file.revise.AddTabPanel', {
	extend: 'Shell.class.qms.file.file.basic.AddTabPanel',
	header: true,
	activeTab: 0,
	title: '修订文档',
	//border: false,
	/**文档操作记录类型*/
	fFileOperationType: 1,
	/**文档状态值*/
	fFileStatus: 1,
	FTYPE: '',
	OriginalFileID: '',
	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, false, false, false, false],
	/*附件是否保存**/
	uploadFormSave: false,
	contentFormTitle: '修订内容',
	uploadTitle: '修订文档附件信息',
	basicFormApp: 'Shell.class.qms.file.file.revise.Form',
	initComponent: function() {
		var me = this;
		me.FTYPE = me.FTYPE || JcallShell.QMS.Enum.FType.修订文档应用;
		me.callParent(arguments);
	},
	createDafultItems: function() {
		var me = this;
		me.callParent(arguments);
		//修订文档应用,需要清空原文档附件
		if(me.formtype == "add") {
			me.UploadForm.AppOperationType = JcallShell.QMS.Enum.AppOperationType.新增修订文档;
			me.UploadForm.FFileId = "";
			me.UploadForm.fkObjectId = "";
			me.UploadForm.fileUploader.fkObjectId = "";
			me.UploadForm.fileItemGird.fkObjectId = "";
			me.UploadForm.defaultLoad = false;

			me.OperationForm.PK = "";
			me.ReadlogForm.FFileId = "";
			me.ReadlogForm.PK = "";
		}

	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.doNextExecutorListeners();
		var Form = me.getComponent('basicForm');
		var UploadForm = me.getComponent('UploadForm');

		Form.on({
			save: function() {
				if(me.uploadFormSave == false) {
					me.UploadForm.save();
					me.uploadFormSave = true;
				} else {
					me.hideMask(); //隐藏遮罩层
					me.close();
				}
			},
			load: function(from, data) {
				JShell.Action.delay(function() {
					me.loadFFileCopyUser();
				}, null, 100);
			}
		});
		UploadForm.on({
			//附件上所有操作处理完
			save: function(win, e) {
				me.fireEvent('save', me, e);
				if(UploadForm.progressMsg != "")
					JShell.Msg.alert(UploadForm.progressMsg);
			}
		});
		//页签切换处理
		me.ontabchange();
		me.activeTab = 0;
		JShell.Action.delay(function() {
			me.loadbasicForm();
			me.loadContentForm(true);
			me.loaduploadForm();
			me.uploadIsLoad = false;
		}, null, 500);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var Form = me.getComponent('basicForm');
		var values = Form.getForm().getValues();
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		var IsUse = 1; //隐藏,全是在用 values.FFile_IsUse ? 1 : 0;

		var entity = {
			Title: values.FFile_Title, //标题
			No: values.FFile_No,
			Status: me.fFileStatus,
			Keyword: values.FFile_Keyword,
			Summary: values.FFile_Summary,
			Source: values.FFile_Source,
			VersionNo: values.FFile_VersionNo,
			ReviseNo: values.FFile_ReviseNo,
			ReviseReason: values.FFile_ReviseReason,
			OriginalFileID: me.OriginalFileID, //修订文档的原始文档Id
			IsUse: IsUse
		};
		switch(me.FTYPE) {
			case JcallShell.QMS.Enum.FType.修订文档应用: //文档
				entity.Type = JcallShell.QMS.Enum.FType.文档应用;
				break;
			default:
				entity.Type = me.FTYPE;
				break;
		}
		if(values.FFile_Pagination) {
			entity.Pagination = values.FFile_Pagination;
		}
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		//创建人
		if(userName) {
			entity.CreatorName = userName;
			entity.DrafterCName = userName;
		}
		if(userId) {
			entity.Creator = {
				Id: userId
			};
			entity.Creator.DataTimeStamp = strDataTimeStamp.split(',');
			//修订人
			entity.Revisor = {
				Id: userId
			};
			entity.Revisor.DataTimeStamp = strDataTimeStamp.split(',');
			//起草人
			entity.DrafterId = userId;
		}
		//如果文档不是暂存状态
		if(me.fFileStatus != 1) {
			//下一执行者处理
			var NextExecutorId = null,
				NextExecutorName = "";
			var comName = me.getComponent("buttonsToolbar").getComponent('NextExecutorName');
			var comId = me.getComponent("buttonsToolbar").getComponent('NextExecutorId');
			if(comId) {
				NextExecutorId = comId.getValue();
			}
			if(comName) {
				NextExecutorName = comName.getValue();
			}
			//文档状态如果不是发布
			if(me.fFileStatus != 5) {
				if(NextExecutorId == "") {
					JShell.Msg.error("请选择执行者信息");
					return;
				}
			}

			var radioItem = me.getComponent("buttonsToolbar").getComponent('radiogroupChoose');
			//起草时间,审核,审批时间,发布时间处理
			var dateTime = new Date();
			var dt = Ext.Date.format(dateTime, 'Y-m-d h:m:s');
			dt = JShell.Date.toServerDate(dt);
			//起草人
			entity.DrafterId = userId;
			entity.DrafterCName = userName;
			if(dt != null) {
				entity.DrafterDateTime = dt;
			}

			if(radioItem) {
				var checked = radioItem.getValue();
				//var choose=checked.choose;
				switch(checked.choose) {
					case 1: //只起草
						entity.CheckerId = NextExecutorId;
						entity.CheckerName = NextExecutorName;
						if(dt != null) {
							entity.CheckerDateTime = dt;
						}
						break;
					case 3: //自动审核
						//审核人
						entity.CheckerId = userId;
						entity.CheckerName = userName;

						entity.ApprovalId = NextExecutorId;
						entity.ApprovalName = NextExecutorName;
						if(dt != null) {
							entity.CheckerDateTime = dt;
						}
						break;
					case 4: //审批
						//审核人
						entity.CheckerId = userId;
						entity.CheckerName = userName;
						//审批人
						entity.ApprovalId = userId;
						entity.ApprovalName = userName;

						//发布人
						entity.PublisherId = NextExecutorId;
						entity.PublisherName = NextExecutorName;
						if(dt != null) {
							entity.CheckerDateTime = dt;
							entity.ApprovalDateTime = dt;
						}
						break;
					case 5: //发布
						//审核人
						entity.CheckerId = userId;
						entity.CheckerName = userName;
						//审批人
						entity.ApprovalId = userId;
						entity.ApprovalName = userName;

						//发布人
						entity.PublisherId = userId;
						entity.PublisherName = userName;
						if(dt != null) {
							entity.CheckerDateTime = dt;
							entity.ApprovalDateTime = dt;
							entity.PublisherDateTime = dt;
						}
						break;
					default:
						me.ffileOperationMemo = "";
						break;
				}
			}
		}
		//文档树类型
		if(values.FFile_BDictTree_Id) {
			entity.BDictTree = {
				Id: values.FFile_BDictTree_Id,
				DataTimeStamp: strDataTimeStamp.split(',')
			};
		}

		//文档内容
		var contentvalues = me.ContentForm.getForm().getValues();
		var FFile_Content = contentvalues.FFile_Content;
		if(FFile_Content && FFile_Content != 'undefined') {
			entity.Content = contentvalues.FFile_Content.replace(/\\/g, '&#92');
		}

		//文档概要
		if(values.FFile_Memo) {
			entity.Memo = values.FFile_Memo.replace(/\\/g, '&#92');
			entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		}
		//抄送人

		return {
			entity: entity,
			ffileOperationMemo: me.ffileOperationMemo
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this;
		var Form = me.getComponent('basicForm');
		var values = Form.getForm().getValues();
		entity = me.getAddParams();

		var fields = [];
		var fields = ['Id', 'Title', 'No', 'VersionNo', 'Pagination', 'Source',
			'Keyword', 'Summary', 'Memo', 'Type', 'Status', 'BDictTree_Id'
		];
		//如果文档内容已加载,需要编辑保存
		if(me.contentIsLoad) {
			fields.push('Content');
		}
		//起草人,审核人等信息处理
		fields.push('DrafterDateTime', 'CheckerDateTime', 'ApprovalDateTime', 'PublisherDateTime', 'DrafterCName', 'DrafterId', 'CheckerName', 'CheckerId', 'ApprovalName', 'ApprovalId', 'PublisherName', 'PublisherId');

		//发布信息处理
		fields.push('BeginTime', 'EndTime', 'IsTop', 'IsDiscuss');

		//修订信息
		fields.push('ReviseNo', 'ReviseReason', 'Revisor_Id', 'ReviseTime');

		entity.fields = fields.join(',');
		entity.entity.Id = values.FFile_Id;
		//修订新增
		if(me.formtype=="add")entity.entity.Id = "-1";
		
		return entity;
	},
	/*文档新增及编辑保存*/
	saveffile: function(paramsRelease) {
		var me = this;
		var Form = me.getComponent('basicForm');
		var values = Form.getForm().getValues();
		var isValid = Form.getForm().isValid();
		if(!values.FFile_Title || values.FFile_Title == "") {
			JShell.Msg.error("标题不能为空!");
			return;
		}
		if(!isValid) return;
		var url = me.formtype == 'add' ? Form.addUrl : Form.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if(!params) return;
		if(me.formtype == "edit" && me.PK == "") return;

		params.fFileOperationType = me.fFileOperationType;
		params.entity.Status = me.fFileStatus;
		//抄送对象类型, 默认没有选择
		params.ffileCopyUserType = -1;
		params.fFileCopyUserList = [];

		params.ffileReadingUserType = -1;
		params.fFileReadingUserList = [];

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
		//抄送人
		var copyUserValue = null;
		var copyUser = me.getComponent('basicForm').getComponent('FFileCopyUser');
		copyUserValue = copyUser.getValue();
		if(copyUserValue && copyUserValue != null) {
			var valueType = copyUserValue.valueType;
			var list = copyUserValue.list;
			if(valueType != null && valueType != "") {
				params.ffileCopyUserType = parseInt(valueType);
			}
			if(list != null && list != "") {
				params.fFileCopyUserList = list;
			}
		}

		var Content = params.entity.Content;
		params.entity.Content = "";
		if(params.entity.Creator && params.entity.Creator.DataTimeStamp) {
			params.entity.Creator.DataTimeStamp = Ext.JSON.encode(params.entity.Creator.DataTimeStamp);
		}
		if(params.entity.Revisor && params.entity.Revisor.DataTimeStamp) {
			params.entity.Revisor.DataTimeStamp = Ext.JSON.encode(params.entity.Revisor.DataTimeStamp);
		}
		if(params.entity.BDictTree && params.entity.BDictTree.DataTimeStamp) {
			params.entity.BDictTree.DataTimeStamp = Ext.JSON.encode(params.entity.BDictTree.DataTimeStamp);
		}

		var panel = Ext.create('Ext.form.Panel', {
			title: '需要保存的数据',
			bodyPadding: 10,
			layout: 'anchor',
			hidden: true,
			/**每个组件的默认属性*/
			defaults: {
				anchor: '100%',
				labelWidth: 80,
				labelAlign: 'right',
				hidden: true
			},
			html: '<div style="text-align:center;margin:20px;">数据保存中...</div>',
			items: [{
				xtype: 'textfield',
				fieldLabel: 'fFileContent',
				name: 'fFileContent',
				value: Content
			}, {
				xtype: 'textarea',
				fieldLabel: 'fFileEntity',
				name: 'fFileEntity',
				value: Ext.JSON.encode(params.entity)
			}, {
				xtype: 'textfield',
				fieldLabel: 'fields',
				name: 'fields',
				value: params.fields
			}, {
				xtype: 'textfield',
				fieldLabel: 'fFileCopyUserList',
				name: 'fFileCopyUserList',
				value: Ext.JSON.encode(params.fFileCopyUserList)
			}, {
				xtype: 'textfield',
				fieldLabel: 'ffileCopyUserType',
				name: 'ffileCopyUserType',
				value: params.ffileCopyUserType
			}, {
				xtype: 'textfield',
				fieldLabel: 'fFileReadingUserList',
				name: 'fFileReadingUserList',
				value: Ext.JSON.encode(params.fFileReadingUserList)
			}, {
				xtype: 'textfield',
				fieldLabel: 'ffileReadingUserType',
				name: 'ffileReadingUserType',
				value: params.ffileReadingUserType
			}, {
				xtype: 'textfield',
				fieldLabel: 'fFileOperationType',
				name: 'fFileOperationType',
				value: params.fFileOperationType
			}, {
				xtype: 'textfield',
				fieldLabel: 'ffileOperationMemo',
				name: 'ffileOperationMemo',
				value: params.ffileOperationMemo
			}, {
				xtype: 'filefield',
				fieldLabel: 'file',
				name: 'file'
			}],
			listeners: {
				afterrender: function() {
					panel.getForm().submit({
						url: url,
						success: function(form, action) {
							var data = action.result;
							if(data.success) {
								if(me.formtype == "add") {
									data.value = Ext.JSON.decode(data.ResultDataValue);

									me.PK = data.value.id;
									me.FFileId = data.value.id;
									me.UploadForm.FFileId = me.PK;
									me.UploadForm.fkObjectId = me.PK;
									me.UploadForm.fileUploader.fkObjectId = me.PK;
									me.UploadForm.fileItemGird.fkObjectId = me.PK;
									
									me.UploadForm.setValues({
										fkObjectId: data.value.id
									});
								}
								Form.fireEvent('save');

								if(Form.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, Form.hideTimes);
							} else {
								var msg = data.ErrorInfo;
								var index = msg.indexOf('UNIQUE KEY');
								if(index != -1) {
									msg = '有重复';
								}
								JShell.Msg.error(msg);
							}
						},
						failure: function(form, action) {
							JShell.Msg.error('服务错误！');
						}
					});
				}
			}
		});
		me.getComponent("buttonsToolbar").add(panel);
	}
});