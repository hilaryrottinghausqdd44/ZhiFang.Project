/**
 * 文档审核
 * @author liangyl		
 * @version 2016-07-12
 */
Ext.define('Shell.class.qms.file.file.examine.Grid', {
	extend: 'Shell.class.qms.file.basic.Grid',
	title: '文档审核',
	width: 1200,
	height: 800,
	/**文档状态默认为发布*/
	defaultStatusValue: "2",
	/**文件的操作记录类型*/
	fFileOperationType: 3,
	DisagreeStatus: 3,
	/**是否隐藏文档状态选择项*/
	hiddenFFileStatus: false,
	PKField: 'FFile_Id',
	hasDel: false,
	IsChangeStatus: false,
	hasShow: false,
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'FFile_Status',
		direction: 'ASC'
	}, {
		property: 'FFile_DataAddTime',
		direction: 'ASC'
	}, {
		property: 'FFile_Title',
		direction: 'ASC'
	}],
	checkOne: false,
	DisagreeOfGridText: '',
	HiddenDisagreeOfGrid: '',
	agreefFileOperationType: 3,
	/**默认选中数据*/
	autoSelect: true,
	/**类型，文档，新闻，通知*/
	FTYPE: '1',
	/**文档操作记录类型(同意)*/
	fFileOperationType: '',
	/**文档状态（同意）*/
	fFileStatus: '',
	/**文档操作记录备注(同意)*/
	ffileOperationMemo: 3,
	AgreeOperationType: 3,
	/**文档操作记录备注(不同意)*/
	disffileMemo: '',
	/**不同意/通过按钮隐藏*/
	HiddenDisagreeButton: false,
	/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
	hiddenRadiogroupChoose: [false, false, false, false],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [true, false, false],
	/**功能按钮是的boxLabel显示*/
	boxLabelRadiogroupChoose: ['只起草', '仅审核', '仅批准', '发布'],
	hasNextExecutor: true,
	MemoPrefix: '',
	AgreeButtonText: '通过',
	DisagreeButtonText: '不通过',
	/**默认加载数据*/
	defaultLoad: true,
	/**文档状态默认为发布*/
	defaultStatusValue: "2",
	FTYPE: '',
	IDS: '',
	defaultWhereChecker: '',
	defaultWherefile: 'ffile.ApprovalId=',
	/**是否显示内容页签*/
	hasContent: false,
	/**是否显示文档详情页签*/
	hasFFileOperation: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = '(ffile.Status!=1)';
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		me.defaultWhereChecker = " and (ffile.IsUse=1) and (" + me.defaultWherefile + userId + ")";
		if(userId && userId != null && userId != "" && userName != "admin") {
			me.defaultWhere += me.defaultWhereChecker;
		}
		me.callParent(arguments);
	},
	/**打开查看表单*/
	openShowTabPanel: function(record, title, appForm) {
		var me = this;
		if(appForm == null) {
			appForm = 'Shell.class.qms.file.show.ShowTabPanel';
		}
		if(maxWidth == null || height == null) {
			var maxWidth = document.body.clientWidth - 380;
			var height = document.body.clientHeight - 60;
		}
		var id = record.get('FFile_Id');
		var publisherDateTime = record.get("FFile_PublisherDateTime");
		var IsDiscuss = true;
		//如果是查看应用,交流记录列表需要处理默认条件
		switch(me.interactionType) {
			case "show":
				IsDiscuss = record.get("FFile_IsDiscuss");
				if(IsDiscuss.toLowerCase() == "false") {
					IsDiscuss = false;
				}
				break;
			default:
				break;
		}
		var config = {
			height: height,
			width: maxWidth,
			FTYPE: me.FTYPE,
			title: title,
			IsChangeStatus: me.IsChangeStatus,
			/**文档操作记录类型(同意)*/
			fFileOperationType: me.fFileOperationType,
			/**文档状态（同意）*/
			fFileStatus: me.fFileOperationType,
			/**文档操作记录备注(同意)*/
			ffileOperationMemo: me.ffileOperationMemo,
			AgreeOperationType: me.AgreeOperationType,
			DisagreeStatus: me.DisagreeStatus,
			/**文档操作记录类型(不同意)*/
			agreefFileOperationType: me.agreefFileOperationType,
			/**文档操作记录备注(不同意)*/
			disffileMemo: me.disffileMemo,
			/**不同意/通过按钮隐藏*/
			HiddenDisagreeButton: me.HiddenDisagreeButton,
			/**功能按钮是否隐藏:组件是否隐藏,只起草,自动审核,自动批准,自动发布*/
			hiddenRadiogroupChoose: me.hiddenRadiogroupChoose,
			/**功能按钮默认选中*/
			checkedRadiogroupChoose: me.checkedRadiogroupChoose,
			boxLabelRadiogroupChoose: me.boxLabelRadiogroupChoose,
			MemoPrefix: me.MemoPrefix,
			hasNextExecutor: me.hasNextExecutor,
			IsDiscuss: (IsDiscuss != null ? IsDiscuss : false),
			AgreeButtonText: me.AgreeButtonText,
			DisagreeButtonText: me.DisagreeButtonText,
			/**是否显示内容页签*/
			hasContent: false,
			/**是否显示文档详情页签*/
			hasFFileOperation: true,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				},
				onAgreeBtnSaveClick: function(win) {
					me.fireEvent('onAgreeBtnSaveClick', win);
				},
				onDisAgreeBtnSaveClick: function(win) {
					me.fireEvent('onDisAgreeBtnSaveClick', win);
				},
				onAgreeSaveClick: function(win) {
					me.fireEvent('onAgreeSaveClick', win);
				}
			}
		};
		if(id && id != null) {
			config.PK = id;
			config.FFileId = id;
			config.FileId = id;
		}
		JShell.Win.open(appForm, config).show();
	},
	/***
	 * 文档的审核通过/不通过;同意/不同意
	 * @param {Object} fFileStatus 文档状态
	 * @param {Object} id
	 * @param {Object} fFileOperationType
	 */
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
			ffileOperationMemo: me.DisagreeOfGridText,
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