/**
 * 新增
 * @author longfc
 * @version 2016-09-22
 */
Ext.define('Shell.class.qms.file.basic.AddTabPanel', {
	extend: 'Ext.tab.Panel',
	requires: [
		'Shell.class.qms.file.basic.toolbarButtons'
	],
	header: true,
	activeTab: 0,
	title: '文档',
	//border: false,
	closable: true,
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
	/**是否隐藏文档阅读对象组件*/
	HiddenFFileReadingLog: true,

	/**查看文档时是否需要添加文档阅读记录信息:1需要,0:不需要*/
	isAddFFileReadingLog: 0,
	/**查看文档时是否需要添加文档操作记录信息:1需要,0:不需要*/
	isAddFFileOperation: 0,
	/**文档操作记录类型*/
	fFileOperationType: 1,
	/**文档状态值*/
	fFileStatus: 1,
	/**原始文档的GUID值*/
	OriginalFileID: "",
	PK: '',
	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**获取树的最大层级数*/
	LEVEL: "",
	/**文档ID*/
	FFileId: '',
	formtype: "",
	/**列表行的树类型Id*/
	BDictTreeId: "",
	BDictTreeCName: '',

	/**显示操作记录页签	,false不显示*/
	hasOperation: false,
	/**显示阅读记录页签	,false不显示*/
	hasReadingLog: false,
	/**显示附件页签	,false不显示*/
	hasUploadForm:true,
	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, true, false, false, false],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [true, false, false, false],
	//文档内容页页签是否已加载过数据
	contentIsLoad: false,
	//附件页页签是否已加载过数据
	uploadIsLoad: false,
	/**文档操作记录备注信息*/
	ffileOperationMemo: "",
	/**应用操作分类*/
	AppOperationType: "",
	FTYPE: "",
	ISCLEAR : false,
	formLoaded: false,
	isLoadbasicForm: false,
	hasNextExecutor: true,
	basicFormApp: 'Shell.class.qms.file.file.create.Form',
	initComponent: function() {
		var me = this;
		if(!me.FFileId) me.FFileId = me.PK;
		me.FTYPE = me.FTYPE || "";
		me.ISCLEAR = me.ISCLEAR || false;
		me.bodyPadding = 1;
		me.basicFormApp = me.basicFormApp || 'Shell.class.qms.file.file.create.Form';
		me.setTitles();
		me.dockedItems = me.createDockedItems();
		me.createDafultItems();
		me.items = me.createItems();
		me.callParent(arguments);
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
				AgreeButtonText: me.AgreeButtonText,
				DisagreeButtonText: me.DisagreeButtonText,
				HiddenAgreeButton: me.HiddenAgreeButton,
				HiddenDisagreeButton: me.HiddenDisagreeButton,
				hiddenRadiogroupChoose: me.hiddenRadiogroupChoose,
				/**功能按钮默认选中*/
				checkedRadiogroupChoose: me.checkedRadiogroupChoose,
				RoleHREmployeeCName: me.RoleHREmployeeCName,
				MemoPrefix: '起草',
				hasNextExecutor: me.hasNextExecutor,
				listeners: {
					onCloseClick: function() {
						me.close();
					},
					onradiogroupchange: function(rdgroup, checked, returnDatas) {
						if(checked && checked.choose != undefined) {
							me.fFileOperationType = checked.choose != null ? checked.choose : 1;
							me.fFileStatus = checked.choose != null ? checked.choose : 1;
							me.AgreeOperationType = checked.choose != null ? checked.choose : 1;
							me.ffileOperationMemo = returnDatas.ffileOperationMemo;
						}
						if(checked.choose == 1)
							me.DisagreeOperationType = 1;
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
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.title = me.title || "";
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
	createDafultItems: function() {
		var me = this;
		var tempFormtype = me.formtype;
		var uploadPK = me.FFileId;
		var uploadDefaultLoad = me.formtype == "edit" ? true : false;
		//修订
		if(me.FTYPE == "4") {
			tempFormtype = me.formtype == "add" ? "edit" : me.formtype;
			uploadDefaultLoad = true;
		}
		me.basicForm = Ext.create(me.basicFormApp, {
			itemId: 'basicForm',
			formtype: tempFormtype,
			hasSave: false,
			hasReset: me.hasReset,
			border: false,
			title: me.basicFormTitle || '基本信息',
			AppOperationType: me.AppOperationType,
			height: me.height,
			width: me.width,
			FTYPE: me.FTYPE,
			PK: me.PK,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			ISCLEAR:me.ISCLEAR,
			BDictTreeId: me.BDictTreeId,
			BDictTreeCName: me.BDictTreeCName,
			FFileId: me.FFileId
		});
		me.ContentForm = Ext.create('Shell.class.qms.file.basic.ContentForm', {
			title: me.contentFormTitle || '文档内容',
			header: false,
			height: me.height,
			width: me.width,
			itemId: 'ContentForm',
			border: false,
			formtype: tempFormtype,
			FTYPE: me.FTYPE,
			FFileId: me.FFileId
		});
		me.UploadForm = Ext.create('Shell.class.qms.file.attachment.Upload', {
			region: 'center',
			header: false,
			title: me.uploadTitle || '附件信息',
			itemId: 'UploadForm',
			border: false,
			defaultLoad: uploadDefaultLoad,
			formtype: tempFormtype,
			FFileId: uploadPK,
			FTYPE: me.FTYPE,
			hidden: !me.hasUploadForm,
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
			defaultPageSize: 500,
			hidden: !me.hasReadingLog,
			itemId: 'ReadlogForm',
			PK: me.FFileId,
			FFileId: me.FFileId,
			border: false,
			isShowForm: false
		});
	},
	createItems: function() {
		var me = this;
		return [me.basicForm, me.ContentForm, me.UploadForm, me.OperationForm, me.ReadlogForm];
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
					case 'WeiXinContentForm':
						me.loadWeiXinContentForm();
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
	/**加载抄送人信息*/
	loadFFileCopyUser: function() {
		var me = this;
		//抄送人信息获取处理
		var userComboBox = me.getComponent('basicForm').getComponent('FFileCopyUser');
		if(userComboBox && userComboBox != undefined) {
			userComboBox.formtype = 'edit';
			if(userComboBox.PK != "") {
				userComboBox.loadDataById(userComboBox.PK);
			}
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
		var id = me.FFileId;
		me.UploadForm.FFileId = id;
		me.UploadForm.fkObjectId = id;
		//防止切换时加载二次
		if(me.UploadForm.defaultLoad == true) me.uploadIsLoad = true;
		//是否刷新
		if(isReset && isReset == true) me.uploadIsLoad == false;
		if(me.uploadIsLoad == false) {
			me.uploadIsLoad = true;
			me.UploadForm.load();
		}
		//修订新增
		if(me.FTYPE == "4" && me.formtype == "add") {
			//me.UploadForm.FFileId = "";
			//me.UploadForm.fkObjectId = "";
		}
	},
	/**加载微信内容信息*/
	loadWeiXinContentForm: function() {
		var me = this;
		me.WeiXinContentForm.load(me.FFileId);
	},
	loadDafultData: function() {
		var me = this;
		var basicForm = me.getComponent('basicForm');
		var comtab = me.getActiveTab(me.items.items[0]);
		me.uploadIsLoad = false;
		me.contentIsLoad = false;

		me.basicForm.isEdit(me.FFileId);
		JShell.Action.delay(function() {
			me.loadContentForm(true);
		}, null, 200);
		me.loaduploadForm(true);
		me.loadWeiXinContentForm();
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
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this;
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
	}
});