/*
 * 新闻发布
 * @author longfc
 * @version 2017-01-03
 */
Ext.define('Shell.class.qms.file.news.basic.AddTabPanel', {
	extend: 'Shell.class.qms.file.basic.AddTabPanel',
	
	title: '新闻发布',
	//border: false,
	closable: true,
	hasNextExecutor: false,
	/**新增新闻服务地址*/
	addUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_AddFFileByFormData',
	/**修改服务地址*/
	editUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileByFieldAndFormData',
	basicFormApp: 'Shell.class.qms.file.news.basic.Form',
	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, true, true, true, true],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [false, false, false, true],
	hasNextExecutor: false,
	initComponent: function() {
		var me = this;
		me.FTYPE = "2";
		
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.doNextExecutorListeners();
		//基本信息处理
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
				me.fireEvent('save', me, e);
				if(UploadForm.progressMsg != "")
					JShell.Msg.alert(UploadForm.progressMsg);
			}
		});
		//页签切换处理
		me.ontabchange();
		me.activeTab = 0;
	},
	createItems: function() {
		var me = this;
		var tempFormtype = me.formtype;
		if(me.FTYPE == "4") {
			tempFormtype = me.formtype == "add" ? "edit" : me.formtype;
		}
		var weiXinTabHidden=JcallShell.System.WeiXinTabHidden;
		if(weiXinTabHidden==undefined)weiXinTabHidden=true;
		/**
		 * 微信页签处理
		 * @author JCall
		 * @version 2017-01-12
		 */
		me.WeiXinContentForm = Ext.create("Shell.class.qms.file.news.basic.WeiXin", {
			itemId: 'WeiXinContentForm',
			formtype: tempFormtype,
			border: false,
			height: me.height,
			title: '微信内容',
			PK: me.PK,
			hidden:weiXinTabHidden
		});
		
		return [me.basicForm, me.ContentForm, me.WeiXinContentForm, me.UploadForm, me.OperationForm, me.ReadlogForm];
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var Form = me.getComponent('basicForm');
		var values = Form.getForm().getValues();

		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		var IsUse = 1;
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
		//文档树类型
		if(entity.BDictTree == null || entity.BDictTree == undefined) {
			entity.BDictTree = {
				Id: me.BDictTreeId,
				DataTimeStamp: strDataTimeStamp.split(',')
			};
		}
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
				//新闻发布的指向OA的src和href不替换(有新闻发布的图片是从OA服务器选取的)
				//FFile_Content = FFile_Content.replace(new RegExp(oaSrc, "g"), 'src="..');
				//FFile_Content = FFile_Content.replace(new RegExp(oaHref, "g"), 'href="..');
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
		//------------------------------------------
		/**
		 * 微信页签处理
		 * @author JCall
		 * @version 2017-01-12
		 */
		var WeiXinForm = me.getComponent('WeiXinContentForm');
		var WeiXinData = WeiXinForm.getData();
		//是否同步到微信服务器
		entity.IsSyncWeiXin = WeiXinData.IsSyncWeiXin;
		//微信内容
		entity.WeiXinContent = WeiXinData.WeiXinContent.replace(/\\/g, '&#92');
		entity.WeiXinContent = entity.WeiXinContent.replace(/[\r\n]/g, '<br/>');
		//新闻缩略图描述
		entity.ThumbnailsMemo = WeiXinData.ThumbnailsMemo.replace(/\\/g, '&#92');
		entity.ThumbnailsMemo = entity.ThumbnailsMemo.replace(/[\r\n]/g, '<br/>');

		//新闻缩略图上传保存路径
		if(WeiXinData.ThumbnailsPath) {
			entity.ThumbnailsPath = WeiXinData.ThumbnailsPath;
		}
		//微信Title
		entity.WeiXinTitle = WeiXinData.WeiXinTitle || entity.Title;
		//微信MEDIA_ID
		if(WeiXinData.Mediaid) {
			entity.Mediaid = WeiXinData.Mediaid;
		}
		//微信Url
		if(WeiXinData.WeiXinUrl) {
			entity.WeiXinUrl = WeiXinData.WeiXinUrl;
		}
		//微信缩略图Thumbmediaid
		if(WeiXinData.Thumbmediaid) {
			entity.Thumbmediaid = WeiXinData.Thumbmediaid;
		}
		//微信Author
		if(WeiXinData.WeiXinAuthor) {
			entity.WeiXinAuthor = WeiXinData.WeiXinAuthor;
		}
		//微信Digest
		if(WeiXinData.WeiXinDigest) {
			entity.WeiXinDigest = WeiXinData.WeiXinDigest;
		}
		//微信Content_source_url
		if(WeiXinData.WeiXinContentsourceurl) {
			entity.WeiXinContentsourceurl = WeiXinData.WeiXinContentsourceurl;
		}
		//------------------------------------------
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
		//发布信息处理
		fields.push('PublisherId', 'PublisherName', 'BeginTime', 'EndTime', 'IsTop', 'IsDiscuss');

		//------------------------------------------
		/**
		 * 微信页签处理
		 * @author JCall
		 * @version 2017-01-12
		 */
		var WeiXinForm = me.getComponent('WeiXinContentForm');
		//是否加载过数据，如果已经加载过，则需要更新这些字段
		if(WeiXinForm.hasLoaded) {
			var WeiXinFields = [
				'IsSyncWeiXin', 'WeiXinContent', 'ThumbnailsMemo', 'ThumbnailsPath',
				'WeiXinTitle', 'Mediaid', 'WeiXinUrl', 'Thumbmediaid', 'WeiXinAuthor',
				'WeiXinDigest', 'WeiXinContentsourceurl'
			];
			fields = fields.concat(WeiXinFields);
		}
		//------------------------------------------

		entity.fields = fields.join(',');
		entity.entity.Id = values.FFile_Id;
		return entity;
	},
	/**确定提交按钮点击处理方法*/
	onAgreeSaveClick: function() {
		var me = this;
		var isExec = true,
			itemId = "",
			msg = "";
		var form = me.getComponent('basicForm');
		me.fFileStatus = 5;
		me.fFileOperationType = 5;
		me.ffileOperationMemo = "新闻发布";
		if(isExec) {
			msg = "请确认是否执行" + me.ffileOperationMemo + "操作?";
			Ext.MessageBox.show({
				title: '操作确认消息',
				msg: msg,
				width: 300,
				buttons: Ext.MessageBox.OKCANCEL,
				fn: function(btn) {
					if(btn == 'ok') {
						me.openFFileForm(me.FFileId);
						//me.saveffile(null);
					}
				},
				icon: Ext.MessageBox.QUESTION
			});
		}
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
		var url = Form.formtype == 'add' ? me.addUrl : me.editUrl;
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
			//2020新增 将区域转换为AreaId
			if (paramsRelease.fFileReadingUserList.length>0 && paramsRelease.fFileReadingUserList[0]["BHospitalArea"]) {
				params.fFileReadingUserList = [{ "AreaId": paramsRelease.fFileReadingUserList[0]["BHospitalArea"]["Id"] }];
			}
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
		var items = [{
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
		}];

		//------------------------------------------
		/**
		 * 微信页签处理
		 * @author JCall
		 * @version 2017-01-12
		 */
		var filenew = {
			xtype: 'filefield',
			fieldLabel: '缩略图文件',
			name: 'file',
			itemId: 'file'
		};
		if(Form.formtype == 'add') {
			var WeiXinForm = me.getComponent('WeiXinContentForm');
			var ImageFile = WeiXinForm.getComponent('InsidePanel').getComponent("Summary").getComponent("ImageFile");

			if(ImageFile && ImageFile.fileInputEl && ImageFile.fileInputEl.dom && ImageFile.fileInputEl.dom.files) {
				if(ImageFile.fileInputEl.dom.files.length > 0) {
					filenew = Ext.Object.merge(ImageFile, filenew);
				}
			} else {
				if(ImageFile && ImageFile.value != "" && ImageFile.value != undefined) {
					filenew = Ext.Object.merge(ImageFile, filenew);
				}
			}
		}
		items.push(filenew);
		//------------------------------------------

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
			items: items,
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
							JShell.Msg.error('服务错误！');
						}
					});
				}
			}
		});
		me.getComponent("buttonsToolbar").add(panel);
	}
});