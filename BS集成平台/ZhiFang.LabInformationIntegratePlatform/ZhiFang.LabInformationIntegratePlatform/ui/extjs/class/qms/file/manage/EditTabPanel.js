/*
 * 文档/新闻/知识库管理的编辑应用(不更新原文档的状态及相关的审核人等信息)
 * @author longfc
 * @version 2016-11-02
 */
Ext.define('Shell.class.qms.file.manage.EditTabPanel', {
	extend: 'Ext.tab.Panel',
	requires: [
		'Shell.class.qms.file.basic.toolbarButtons'
	],
	header: true,
	activeTab: 0,
	title: '编辑',
	border: false,
	closable: true,
	autoScroll: true,
	/**是否重置按钮*/
	hasReset: false,
	/**是否启用取消按钮*/
	hasCancel: false,
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**功能按钮栏位置*/
	buttonDock: 'bottom',

	/**列表行的树类型Id*/
	BDictTreeId: "",
	BDictTreeCName: '',
	/**基本应用的文档确认(通过/同意)操作按钮是否显示*/
	HiddenAgreeButton: false,
	/**基本应用的文档确认(通过/同意)操作按钮显示名称*/
	AgreeButtonText: "保存",
	/**基本应用的文档确认(通过/同意)操作按钮的功能类型*/
	AgreeOperationType: 19,

	/**是否隐藏文档阅读对象组件*/
	HiddenFFileReadingLog: false,

	/**查看文档时是否需要添加文档阅读记录信息:1需要,0:不需要*/
	isAddFFileReadingLog: 0,
	/**查看文档时是否需要添加文档操作记录信息:1需要,0:不需要*/
	isAddFFileOperation: 0,
	/**文档操作记录类型*/
	fFileOperationType: 19,
	/**文档状态值*/
	fFileStatus: 1,
	/**原始文档的GUID值*/
	OriginalFileID: "",
	PK: '',
	/**文档ID*/
	FFileId: '',
	formtype: "",
	/**显示操作记录页签	,false不显示*/
	hasOperation: true,
	/**显示阅读记录页签	,false不显示*/
	hasReadingLog: true,
	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, true, false, false, false],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [true, false, false, false],
	//文档内容页页签是否已加载过数据
	contentIsLoad: false,
	//附件页页签是否已加载过数据
	uploadIsLoad: false,
	/**文档操作记录备注信息*/
	ffileOperationMemo: "编辑更新",
	/**应用操作分类*/
	AppOperationType: "",
	FTYPE: "",
	formLoaded: false,
	isLoadbasicForm: false,
	basicFormApp: 'Shell.class.qms.file.file.create.Form',
	initComponent: function() {
		var me = this;
		me.addEvents('msgShow');
		me.FTYPE = me.FTYPE || JcallShell.QMS.Enum.FType.文档应用;
		me.bodyPadding = 1;
		me.title = me.title || "";
		me.setTitles();
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
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
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		switch(me.FTYPE) {
			case JcallShell.QMS.Enum.FType.文档应用: //文档
				me.basicFormTitle = "基本信息";
				me.contentFormTitle = "详细内容";
				me.uploadTitle = "附件信息";
				break;
			case JcallShell.QMS.Enum.FType.新闻应用: //新闻
				me.basicFormTitle = "新闻基本信息";
				me.contentFormTitle = "新闻内容";
				me.uploadTitle = "新闻附件信息";
				break;
			default:
				me.basicFormTitle = "基本信息";
				me.contentFormTitle = "详细内容";
				me.uploadTitle = "附件信息";
				break;
		}
	},

	createItems: function() {
		var me = this;
		var tempFormtype = me.formtype;
		if(me.FTYPE == "4") {
			tempFormtype = me.formtype == "add" ? "edit" : me.formtype;
		}
		me.basicForm = Ext.create(me.basicFormApp, {
			itemId: 'basicForm',
			formtype: tempFormtype,
			hasSave: false,
			hasReset: me.hasReset,
			border: false,
			title: me.basicFormTitle || '基本信息',
			BDictTreeId: me.BDictTreeId,
			BDictTreeCName: me.BDictTreeCName,
			AppOperationType: me.AppOperationType,
			FTYPE: me.FTYPE,
			height: me.height,
			width: me.width,
			PK: me.PK,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			FFileId: me.FFileId
		});
		me.ContentForm = Ext.create('Shell.class.qms.file.basic.ContentForm', {
			title: me.contentFormTitle || '详细内容',
			header: false,
			height: me.height,
			width: me.width,
			itemId: 'ContentForm',
			border: false,
			formtype: tempFormtype,
			FTYPE: me.FTYPE,
			AppOperationType: me.AppOperationType,
			FFileId: me.FFileId
		});
		me.UploadForm = Ext.create('Shell.class.qms.file.attachment.Upload', {
			region: 'center',
			header: false,
			title: me.uploadTitle || '附件信息',
			itemId: 'UploadForm',
			border: false,
			defaultLoad: tempFormtype == "add" ? false : true,
			formtype: tempFormtype,
			FFileId: me.FFileId,
			FTYPE: me.FTYPE,
			AppOperationType: me.AppOperationType
		});
		me.OperationForm = Ext.create('Shell.class.qms.file.operation.Grid', {
			title: '操作记录',
			header: false,
			hasButtontoolbar: false,
			hasPagingtoolbar: false,
			/**默认每页数量*/
			defaultPageSize: 500,
			hidden: !me.hasOperation,
			itemId: 'OperationForm',
			PK: me.FFileId,
			FFileId: me.FFileId,
			border: false,
			isShowForm: false
		});
		me.ReadlogForm = Ext.create('Shell.class.qms.file.readinglog.Grid', {
			title: '阅读记录',
			header: false,
			hasButtontoolbar: false,
			hasPagingtoolbar: false,
			/**默认每页数量*/
			defaultPageSize: 500,
			hidden: !me.hasReadingLog,
			itemId: 'ReadlogForm',
			PK: me.FFileId,
			FFileId: me.FFileId,
			border: false,
			isShowForm: false
		});
		return [me.basicForm, me.ContentForm, me.UploadForm, me.OperationForm, me.ReadlogForm];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			var buttontoolbar = me.createButtontoolbar();
			if(buttontoolbar) items.push(buttontoolbar);
		}
		return items;
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
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			//items = [];
			toolbar = Ext.create('Shell.class.qms.file.basic.toolbarButtons', {
				dock: me.buttonDock,
				itemId: 'buttonsToolbar',
				RoleHREmployeeCName: me.RoleHREmployeeCName,
				MemoPrefix: '起草',
				HiddenAgreeButton: me.HiddenAgreeButton,
				AgreeButtonText: me.AgreeButtonText,
				AppOperationType: me.AppOperationType,
				HiddenDisagreeButton: true,
				DisagreeButtonText: "暂存",
				hiddenRadiogroupChoose: [true, false, false, false, false],
				/**功能按钮默认选中*/
				checkedRadiogroupChoose: [true, false, false, false],
				hasNextExecutor: false,
				listeners: {
					onCloseClick: function() {
						me.close();
					},
					onradiogroupchange: function(rdgroup, checked, returnDatas) {},
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
	/**加载抄送人信息*/
	loadFFileCopyUser: function() {
		var me = this;
		//抄送人信息获取处理
		var userComboBox = me.getComponent('basicForm').getComponent('FFileCopyUser');
		userComboBox.formtype = 'edit';
		if(userComboBox.PK != "") {
			userComboBox.loadDataById(userComboBox.PK);
		}
	},
	loadbasicForm: function(isReset) {
		var me = this;
		//是否刷新
		if(isReset && isReset == true) me.isLoadbasicForm == false;
		if(me.isLoadbasicForm) {
			me.basicForm.load(me.FFileId);
			me.isLoadbasicForm = true;
		}
	},
	/**加载文档内容信息*/
	loadContentForm: function(isReset) {
		var me = this;
		//是否刷新
		if(isReset && isReset == true) me.contentIsLoad == false;
		if(me.contentIsLoad == false) {
			me.ContentForm.load(me.FFileId);
			me.contentIsLoad = true;
		}
	},
	/**加载文档附件信息*/
	loaduploadForm: function(isReset) {
		var me = this;
		me.UploadForm.FFileId = me.FFileId;
		me.UploadForm.fkObjectId = me.FFileId;
		//防止切换时加载二次
		if(me.UploadForm.defaultLoad == true) me.uploadIsLoad = true;
		//是否刷新
		if(isReset && isReset == true) me.uploadIsLoad == false;
		if(me.uploadIsLoad == false) {
			me.uploadIsLoad = true;
			me.UploadForm.load();
		}
	},
	loadDafultData: function() {
		var me = this;
		var basicForm = me.getComponent('basicForm');
		var comtab = me.getActiveTab(me.items.items[0]);
		var id = me.PK;
		me.uploadIsLoad = false;
		me.contentIsLoad = false;

		me.basicForm.isEdit(me.FFileId);
		JShell.Action.delay(function() {
			me.loadContentForm(true);
		}, null, 200);
		me.loaduploadForm(true);
	},
	/**页签切换事件处理*/
	ontabchange: function() {
		var me = this;
		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var oldItemId = null;
				if(oldCard != null) {
					oldItemId = oldCard.itemId
				}
				switch(newCard.itemId) {
					case 'ContentForm':
						me.loadContentForm();
						break;
					case 'UploadForm':
						me.loaduploadForm();
						break;
					case 'basicForm':
						me.loadbasicForm();
						break;
					default:
						break
				}
			}
		});
	},
	/**打开新增或编辑发布文档表单*/
	openFFileForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.qms.file.file.release.ReleaseForm', {
			title: '发布信息',
			listeners: {
				save: function(win, params) {
					me.saveffile(params);
					win.close();
				}
			}
		}).show();
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.loadDafultData();
		}, null, 200);
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},

	/**确定提交按钮点击处理方法*/
	onAgreeSaveClick: function() {
		var me = this;
		var isExec = true,
			itemId = "",
			msg = "";
		var form = me.getComponent('basicForm');

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
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var Form = me.getComponent('basicForm');

		var values = Form.getForm().getValues();
		var entity = {
			Title: values.FFile_Title, //标题
			No: values.FFile_No,
			Status: me.fFileStatus, //传回但不更新
			Keyword: values.FFile_Keyword,
			Summary: values.FFile_Summary,
			Source: values.FFile_Source,
			VersionNo: values.FFile_VersionNo
			//IsUse: IsUse
		};
		entity.Type = parseInt(me.FTYPE);
		if(values.FFile_Pagination) {
			entity.Pagination = values.FFile_Pagination;
		}

		//文档树类型
		if(values.FFile_BDictTree_Id) {
			entity.BDictTree = {
				Id: values.FFile_BDictTree_Id
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
			'Keyword', 'Summary', 'Memo', 'Type', 'BDictTree_Id'
		];
		//如果文档内容已加载,需要编辑保存
		if(me.contentIsLoad) {
			fields.push('Content');
		}
		entity.fields = fields.join(',');
		entity.entity.Id = values.FFile_Id;
		return entity;
	},

	/**不通过按钮点击处理方法*/
	onDisagreeSaveClick: function() {
		var me = this;
	},
	/**文档的审核通过/不通过;同意/不同意*/
	UpdateFFileStatus: function() {
		var me = this;
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
		//发布信息处理
		params.ffileReadingUserType = -1;
		params.fFileReadingUserList = [];

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
									}
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