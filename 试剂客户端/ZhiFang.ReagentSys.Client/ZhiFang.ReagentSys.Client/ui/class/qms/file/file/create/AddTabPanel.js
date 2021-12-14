/*
 * 文档新增
 * @author longfc
 * @version 2016-09-22
 */
Ext.define('Shell.class.qms.file.file.create.AddTabPanel', {
	extend: 'Shell.class.qms.file.file.basic.AddTabPanel',
	header: true,
	activeTab: 0,
	title: '文档',
	//border: false,
	closable: true,
	hasNextExecutor: true,
	basicFormApp: 'Shell.class.qms.file.file.create.Form',
	initComponent: function() {
		var me = this;
		me.FTYPE = me.FTYPE || "";
		me.basicFormApp = me.basicFormApp || 'Shell.class.qms.file.file.create.Form';
		//me.setTitles();
		//me.dockedItems = me.createDockedItems();
		//me.createDafultItems();
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.doNextExecutorListeners();
		//文档基本信息处理
		var Form = me.getComponent('basicForm');
		var UploadForm = me.getComponent('UploadForm');

		Form.on({
			save: function() {
				me.UploadForm.save();
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
				if(e.success) {
					me.fireEvent('save', me, e);
				} else {
					if(UploadForm.progressMsg) JShell.Msg.error(UploadForm.progressMsg);
				}
			}
		});
		//页签切换处理
		me.ontabchange();
		me.activeTab = 0;
	},
	/**文档的审核通过/不通过;同意/不同意*/
	UpdateFFileStatus: function() {
		var me = this;
		var Form = me.getComponent('basicForm');
		if(Form.PK == undefined || Form.PK == "") {
			return false;
		}
		var url = Form.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var entity = {
			Status: me.fFileStatus,
			Id: Form.PK
		};
		var fields = 'Id,Status';
		var params = {
			entity: entity,
			//文档操作记录备注信息
			ffileOperationMemo: me.ffileOperationMemo,
			fFileOperationType: me.fFileOperationType,
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
		//Form.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			//Form.hideMask(); //隐藏遮罩层
			if(data.success) {
				Form.fireEvent('save', me);
				if(Form.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, Form.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		});
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
			IsUse: IsUse
		};
		entity.Type = parseInt(me.FTYPE);
		if(values.FFile_VersionNo) {
			entity.VersionNo = values.FFile_VersionNo;
		}
		if(values.FFile_Keyword) {
			entity.Keyword = values.FFile_Keyword;
		}
		if(values.FFile_Summary) {
			entity.Summary = values.FFile_Summary;
		}
		if(values.FFile_Source) {
			entity.Source = values.FFile_Source;
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
			if(Form.formtype == "add") {
				entity.Creator.DataTimeStamp = strDataTimeStamp.split(',');
			}
			//起草人
			entity.DrafterId = userId;
		}
		//如果文档不是暂存状态
		if(me.fFileStatus != 1) {
			//下一执行者处理
			var NextExecutorId = null,
				NextExecutorName = "";
			if(me.hasNextExecutor == true) {
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
			}
			var radioItem = me.getComponent("buttonsToolbar").getComponent('radiogroupChoose');
			//起草时间,审核,审批时间,发布时间处理
			var dateTime = new Date();
			var dt = Ext.Date.format(dateTime, 'Y-m-d h:m:s');
			dt = JShell.Date.toServerDate(dt);
			if(dt != null) {
				entity.DrafterDateTime = dt;
			}
			if(radioItem) {
				var checked = radioItem.getValue();
				//var choose=checked.choose;
				switch(checked.choose) {
					case 1: //只起草
						if(me.hasNextExecutor == true) {
							entity.CheckerId = NextExecutorId;
							entity.CheckerName = NextExecutorName;
						}
						break;
					case 3: //自动审核
						//审核人
						entity.CheckerId = userId;
						entity.CheckerName = userName;
						if(me.hasNextExecutor == true) {
							entity.ApprovalId = NextExecutorId;
							entity.ApprovalName = NextExecutorName;
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

						break;
					case 5: //发布
						//审核人
						entity.CheckerId = userId;
						entity.CheckerName = userName;
						//审批人
						entity.ApprovalId = userId;
						entity.ApprovalName = userName;
						//发布人
						if(me.hasNextExecutor == true) {
							entity.PublisherId = userId;
							entity.PublisherName = userName;
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
				Id: values.FFile_BDictTree_Id
			};
			if(Form.formtype == "add") {
				entity.BDictTree.DataTimeStamp = strDataTimeStamp.split(',');
			}
		}

		//文档内容
		var contentvalues = me.ContentForm.getForm().getValues();
		var FFile_Content = contentvalues.FFile_Content;
		if(FFile_Content && FFile_Content != 'undefined') {
			if(me.FTYPE.toString() == "5") {
				//在线编辑器里的所有src及href都替换为相对路径..

				var src = 'src="' + JShell.System.Path.ROOT;
				var href = 'href="' + JShell.System.Path.ROOT;

				var oaTestSrc = 'src="http://demo.zhifang.com.cn/Zhifang.OA.Test';
				var oaTestHref = 'href="http://demo.zhifang.com.cn/Zhifang.OA.Test';

				var oaSrc = 'src="http://demo.zhifang.com.cn/Zhifang.OA';
				var oaHref = 'href="http://demo.zhifang.com.cn/Zhifang.OA';
				//OA.Test
				FFile_Content = FFile_Content.replace(new RegExp(oaTestSrc, "g"), 'src="..');
				FFile_Content = FFile_Content.replace(new RegExp(oaTestHref, "g"), 'href="..');
				//OA
				FFile_Content = FFile_Content.replace(new RegExp(oaSrc, "g"), 'src="..');
				FFile_Content = FFile_Content.replace(new RegExp(oaHref, "g"), 'href="..');
				//当前服务器
				FFile_Content = FFile_Content.replace(new RegExp(src, "g"), 'src="..');
				FFile_Content = FFile_Content.replace(new RegExp(href, "g"), 'href="..');
			}
			entity.Content = FFile_Content.replace(/\\/g, '&#92');
		} else {
			entity.Content = "";
		}

		//文档概要
		if(values.FFile_Memo) {
			entity.Memo = values.FFile_Memo.replace(/\\/g, '&#92');
			entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		} else {
			entity.Memo = "";
		}
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
		//审核人等信息处理
		//fields.push('CheckerDateTime', 'ApprovalDateTime', 'CheckerName', 'CheckerId', 'ApprovalName', 'ApprovalId');

		//发布信息处理
		fields.push('PublisherId', 'PublisherName', 'BeginTime', 'EndTime', 'IsTop', 'IsDiscuss');

		entity.fields = fields.join(',');
		entity.entity.Id = values.FFile_Id;
		return entity;
	},
	/**不通过按钮点击处理方法*/
	onDisagreeSaveClick: function() {
		var me = this;
		switch(me.DisagreeOperationType) {
			case 1: //新增文档起草暂存操作类型
				me.fFileStatus = 1;
				me.fFileOperationType = 1;
				me.saveffile(null);
				break;
			case 2: //编辑文档起草暂存操作类型
				me.fFileStatus = 1;
				me.fFileOperationType = 2;
				me.saveffile(null);
				break;
			case 3: //审核不通过
				me.fFileStatus = 9;
				me.fFileOperationType = 9;
				if(me.formtype == "add") {
					me.saveffile(null);
				} else {
					me.UpdateFFileStatus();
				}
				break;
			case 4: //批准不通过
				me.fFileStatus = 10;
				me.fFileOperationType = 10;
				if(me.formtype == "add") {
					me.saveffile(null);
				} else {
					me.UpdateFFileStatus();
				}
				break;
			default:
				break;
		}
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.loadDafultData();
		}, null, 200);
	},
	/*文档新增及编辑保存*/
	saveffile: function(paramsRelease) {
		var me = this;
		var Form = me.getComponent('basicForm');
		var values = Form.getForm().getValues();
		if(!values.FFile_Title || values.FFile_Title == "") {
			JShell.Msg.alert("标题不能为空!", null, 1000);
			return;
		}
		var isValid = Form.getForm().isValid();
		if(!isValid) {
			JShell.Msg.alert("基本信息表单验证不通过!", null, 1000);
			return;
		}
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		if(!userId) {
			JShell.Msg.alert("没能获取到登录人信息!请登录后再操作!!", null, 1000);
			return;
		}
		var url = Form.formtype == 'add' ? Form.addUrl : Form.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = Form.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if(!params) return;
		if(Form.formtype == "edit" && Form.PK == "") return;

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
		if(copyUser && copyUser != undefined)
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
									if(data.value == null) {
										JShell.Msg.error("保存失败!");
										return false;
									} else {
										me.PK = data.value.id;
										me.UploadForm.FFileId = data.value.id;
										me.UploadForm.fkObjectId = data.value.id;
										Form.fireEvent('save');
									}
								} else {
									Form.fireEvent('save');
								}
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
							var msg=action.result.ErrorInfo;
							if(msg)JShell.Msg.error('服务错误:'+msg);
						}
					});
				}
			}
		});
		me.getComponent("buttonsToolbar").add(panel);
	}
});