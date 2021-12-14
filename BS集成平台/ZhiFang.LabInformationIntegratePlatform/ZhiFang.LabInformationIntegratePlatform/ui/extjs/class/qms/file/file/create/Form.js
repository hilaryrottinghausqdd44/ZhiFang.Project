/**
 * 新增文档表单
 * @author longfc
 * @version 2016-06-23
 */
Ext.define('Shell.class.qms.file.file.create.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.class.qms.file.copyuser.userComboBox',
		'Shell.ux.form.field.UEditor'
	],
	bodyPadding: '5px 5px 5px 5px',
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 85,
		width: 220,
		labelAlign: 'right'
	},
	formtype: "add",
	title: '文档信息',
	width: 790,
	height: 550,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_AddFFileByFormData',
	/**修改服务地址*/
	editUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileByFieldAndFormData',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**带附件按钮*/
	hasAttachButton: true,
	/**文档操作记录类型值*/
	fFileOperationType: 1,
	/**文档状态值*/
	fFileStatus: 1,

	/**列表行的树类型Id*/
	BDictTreeId: "",
	/**列表行的树类型*/
	BDictTreeCName: "",
	autoScroll: true,
	PK: '',
	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**编辑文档类型(如新闻/通知/文档/修订文档)*/
	FTYPE: '',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();

	},
	initComponent: function() {
		var me = this;
		var width = me.width / 4 - 10;
		me.defaults.width = (width < 220) ? 220 : width;
		me.selectUrl = me.selectUrl + "&isAddFFileReadingLog=0&isAddFFileOperation=0";
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		switch(me.FTYPE) {
			case JcallShell.QMS.Enum.FType.文档应用: //文档应用
				me.createAddFFileShowItems();
				break;
			case JcallShell.QMS.Enum.FType.新闻应用: //新闻应用
				me.createAddNewsShowItems();
				break;
			default:
				me.createAddFFileShowItems();
				break;
		}
		items = items.concat(me.getAddFFileTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},
	/**创建文档可见组件*/
	createAddFFileShowItems: function() {
		var me = this;
		me.createFFile_Title("文档标题");
		me.createFFile_No('编号');
		me.createFFile_VersionNo("版本号");
		//me.createDate();
		me.createFFFile_Pagination('页码');
		me.createFFile_Source('文档来源');
		me.createFFile_IsUse('是否使用');

		me.createFFileBDictTreeCName("内容类型");
		me.createFFileKeyword('关键字');

		me.createFFileSummary('文摘');
		me.createFFileMemo('概要');
		me.createFFileCopyUser("参与讨论人员");
	},

	/**创建新闻可见组件*/
	createAddNewsShowItems: function() {
		var me = this;
		me.createFFile_Title("新闻标题");
		me.createFFile_No('编号');
		me.createFFile_VersionNo("版本号");
		//me.createDate();
		me.createFFFile_Pagination('页码');
		me.createFFile_Source('新闻来源');
		me.createFFile_IsUse('是否使用');

		me.createFFileBDictTreeCName("内容类型");
		me.createFFileKeyword('关键字');

		me.createFFileSummary('文摘');
		me.createFFileMemo('新闻概要');
		me.createFFileCopyUser("参与讨论人员");
	},

	/***标题*/
	createFFile_Title: function(fieldLabel) {
		var me = this;
		me.FFile_Title = {
			fieldLabel: fieldLabel,
			itemId: 'FFile_Title',
			name: 'FFile_Title',
			allowBlank: false,
			emptyText: '必填项'
		};
	},
	/***文档编号*/
	createFFile_No: function(fieldLabel) {
		var me = this;
		me.FFile_No = {
			fieldLabel: fieldLabel,
			itemId: 'FFile_No',
			name: 'FFile_No'
		};

	},
	/***版本号*/
	createFFile_VersionNo: function(fieldLabel) {
		var me = this;
		me.FFile_VersionNo = {
			fieldLabel: fieldLabel,
			itemId: 'FFile_VersionNo',
			name: 'FFile_VersionNo'
		};
	},

	/**文档页码*/
	createFFFile_Pagination: function(fieldLabel) {
		var me = this;
		me.FFile_Pagination = {
			fieldLabel: fieldLabel,
			xtype: 'numberfield',
			itemId: 'FFile_Pagination',
			name: 'FFile_Pagination'
		};
	},
	/**文档来源*/
	createFFile_Source: function(fieldLabel) {
		var me = this;
		me.FFile_Source = {
			fieldLabel: fieldLabel,
			name: 'FFile_Source',
			itemId: 'FFile_Source',
			minHeight: 20,
			emptyText: ''
		};
	},
	createFFile_IsUse: function(fieldLabel) {
		var me = this;
		me.FFile_IsUse = {
			boxLabel: fieldLabel,
			name: 'FFile_IsUse',
			itemId: 'FFile_IsUse',
			xtype: 'checkbox',
			hidden: true,
			checked: true,
			style: {
				marginLeft: '50px'
			}
		};
	},
	/**关键字*/
	createFFileKeyword: function(fieldLabel) {
		var me = this;
		me.FFile_Keyword = {
			fieldLabel: fieldLabel,
			name: 'FFile_Keyword',
			itemId: 'FFile_Keyword',
			emptyText: '逗号分割'
		};
	},
	createFFileBDictTreeCName: function(fieldLabel) {
		var me = this;
		var treeShortcodeWhere = "";
		if(me.IDS && me.IDS.toString().length > 0) {
			treeShortcodeWhere = "idListStr=" + me.IDS;
		}
		me.FFile_BDictTree_CName = {
			fieldLabel: fieldLabel,
			allowBlank: false,
			emptyText: '必填项',
			itemId: 'FFile_BDictTree_CName',
			name: 'FFile_BDictTree_CName',
			IsnotField: true,
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			multiSelect: false,
			value: me.BDictTreeCName,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.sysbase.dicttree.CheckTree', {
					treeShortcodeWhere: me.treeShortcodeWhere,
					isShowchecked: false,
					rootVisible: false,
					defaultLoad: true,
					IDS: me.IDS,
					/**获取树的最大层级数*/
					LEVEL: me.LEVEL,
					value: me.BDictTreeCName,
					listeners: {
						accept: function(win, record) {
							if(record == null) {
								JShell.Msg.alert("没有选择到类型", null, 1000);
								return;
							} else {
								me.onBDictTreeAccept(record);
							}
							win.close();
						}
					}
				}).show();
			}
		};
	},
	/**文摘*/
	createFFileSummary: function(fieldLabel) {
		var me = this;
		me.FFile_Summary = {
			fieldLabel: fieldLabel,
			itemId: 'FFile_Summary',
			name: 'FFile_Summary',
			emptyText: ''
		};
	},
	/**摘要*/
	createFFileMemo: function(fieldLabel) {
		var me = this;
		var minHeight = me.height * 0.53,
			height = me.height * 0.53;

		switch(me.FTYPE) {
			case JcallShell.QMS.Enum.FType.文档应用: //文档应用
				if(me.AppOperationType == JcallShell.QMS.Enum.AppOperationType.新增修订文档 || me.AppOperationType == JcallShell.QMS.Enum.AppOperationType.编辑修订文档) {
					minHeight = me.height * 0.46;
					height = me.height * 0.46;
				}
				break;
			default:
				break;
		}
		me.FFile_Memo ={
			fieldLabel: fieldLabel,
			labelAlign: 'top',
			name: 'FFile_Memo',
			itemId: 'FFile_Memo',
			xtype: 'ueditor',
			style: {
				marginBottom: '2px'
			},
			width:30,
			height:height-60,
			autoScroll: true,
			minHeight: minHeight,
			itemId:'FFile_Memo',
			border: false
		};
//		me.FFile_Memo = {
//			fieldLabel: fieldLabel,
//			name: 'FFile_Memo',
//			minHeight: minHeight,
//			height: height,
////			maxLength: 500,
////			maxLengthText: "摘要最多只能输入500字",
//			style: {
//				marginBottom: '2px'
//			},
//			xtype: 'textarea'
//		};
	},
	/**创建时间*/
	createDate: function() {
		var me = this;
		me.FFile_BeginTime = {
			fieldLabel: '开始日期',
			name: 'FFile_BeginTime',
			itemId: 'FFile_BeginTime',
			xtype: 'datefield',
			format: 'Y-m-d'
		};
		me.FFile_EndTime = {
			fieldLabel: '发布日期',
			name: 'FFile_EndTime',
			itemId: 'FFile_EndTime',
			xtype: 'datefield',
			format: 'Y-m-d'
		};
	},
	/**抄送人*/
	createFFileCopyUser: function(fieldLabel) {
		var me = this;
		me.FFileCopyUser = {
			boxLabel: fieldLabel,
			name: 'FFileCopyUser',
			itemId: 'FFileCopyUser',
			xtype: 'userComboBox',
			fieldLabel: fieldLabel,
			defaultLoad: false,
			RoleHREmployeeCName: JcallShell.QMS.Enum.getEmployeeRoleName("r1;r2;r3;r4;r5"),
			objectEName: 'FFileCopyUser',
			/**获取数据服务路径*/
			selectUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileCopyUserByHQL?isPlanish=true',
			labelWidth: me.defaults.labelWidth,
			formtype: me.formtype,
			AppOperationType: me.AppOperationType,
			FTYPE: me.FTYPE,
			PK: me.FFileId

		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		//文档标题
		me.FFile_Title.colspan = 4;
		me.FFile_Title.width = me.defaults.width * me.FFile_Title.colspan;
		items.push(me.FFile_Title);
		//文档编号
		me.FFile_No.colspan = 1;
		me.FFile_No.width = me.defaults.width * me.FFile_No.colspan;
		items.push(me.FFile_No);
		//版本号
		me.FFile_VersionNo.colspan = 1;
		me.FFile_VersionNo.width = me.defaults.width * me.FFile_VersionNo.colspan;
		items.push(me.FFile_VersionNo);
		//页码
		me.FFile_Pagination.colspan = 1;
		items.push(me.FFile_Pagination);

		//文档来源
		me.FFile_Source.colspan = 1;
		me.FFile_Source.width = me.defaults.width * me.FFile_Source.colspan;
		items.push(me.FFile_Source);

		me.FFile_BDictTree_CName.colspan = 1;
		me.FFile_BDictTree_CName.width = me.defaults.width * me.FFile_BDictTree_CName.colspan;
		items.push(me.FFile_BDictTree_CName);
		//关键字
		me.FFile_Keyword.colspan = 3;
		me.FFile_Keyword.width = me.defaults.width * me.FFile_Keyword.colspan;
		items.push(me.FFile_Keyword);

		//文摘
		me.FFile_Summary.colspan = 4;
		me.FFile_Summary.width = me.defaults.width * me.FFile_Summary.colspan;
		items.push(me.FFile_Summary);
		//抄送人
		me.FFileCopyUser.colspan = 4;
		me.FFileCopyUser.width = me.defaults.width * me.FFileCopyUser.colspan;
		items.push(me.FFileCopyUser);

		//文档备注
		me.FFile_Memo.colspan = 4;
		me.FFile_Memo.width = me.defaults.width * me.FFile_Memo.colspan;
		items.push(me.FFile_Memo);
		return items;
	},

	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//字典监听
		var dictList = ['ContentType'];
		for(var i = 0; i < dictList.length; i++) {
			me.doDictListeners(dictList[i]);
		}
		//员工监听
		var dictList = [];
		for(var i = 0; i < dictList.length; i++) {
			me.doUserListeners(dictList[i]);
		}
		me.doDrafterCNameListeners("Drafter");
	},
	/**字典监听*/
	doDictListeners: function(name) {
		var me = this;
		var CName = me.getComponent('FFile_' + name + '_CName');
		var Id = me.getComponent('FFile_' + name + '_Id');
		var DataTimeStamp = me.getComponent('FFile_' + name + '_DataTimeStamp');

		if(!CName) return;

		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('PDict_CName') : '');
				Id.setValue(record ? record.get('PDict_Id') : '');
				DataTimeStamp.setValue(record ? record.get('PDict_DataTimeStamp') : '');
				p.close();
			}
		});
	},
	/**员工监听*/
	doUserListeners: function(name) {
		var me = this;
		var CName = me.getComponent('FFile_' + name + '_CName');
		var Id = me.getComponent('FFile_' + name + '_Id');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('RBACEmpRoles_HREmployee_CName') : '');
				Id.setValue(record ? record.get('RBACEmpRoles_HREmployee_Id') : '');
				p.close();
			}
		});
	},
	/**起草人监听*/
	doDrafterCNameListeners: function(name) {
		var me = this;
		var CName = me.getComponent('FFile_' + name + 'CName');
		var Id = me.getComponent('FFile_' + name + 'Id');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('RBACEmpRoles_HREmployee_CName') : '');
				Id.setValue(record ? record.get('RBACEmpRoles_HREmployee_Id') : '');
				p.close();
			}
		});
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		data.FFile_BeginTime = JShell.Date.getDate(data.FFile_BeginTime);
		data.FFile_EndTime = JShell.Date.getDate(data.FFile_EndTime);
		var reg = new RegExp("<br />", "g");
		data.FFile_Memo = data.FFile_Memo.replace(reg, "\r\n");
		//		reg = new RegExp("&#92", "g");
		//		data.FFile_Memo = data.FFile_Memo.replace(reg, '\\');
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**选择内容类型*/
	onBDictTreeAccept: function(record) {
		var me = this,
			ParentID = me.getComponent('FFile_BDictTree_Id'),
			ParentName = me.getComponent('FFile_BDictTree_CName');
		ParentID.setValue(record.get('tid') || '');
		ParentName.setValue(record.get('text') || '');
	},
	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '主键ID',
			hidden: true,
			name: 'FFile_Id'
		});
		var USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;

		me.FFile_DrafterId = {
			fieldLabel: '起草人ID',
			hidden: true,
			itemId: 'FFile_DrafterId',
			name: 'FFile_DrafterId'
		};
		if(me.formtype == "add") {
			if(USERID && USERID != null) {
				me.FFile_DrafterId.value = USERID;
			}
		}
		items.push(me.FFile_DrafterId);

		me.FFile_BDictTree_Id = {
			fieldLabel: '内容类型ID',
			hidden: true,
			itemId: 'FFile_BDictTree_Id',
			name: 'FFile_BDictTree_Id'
		};
		if(me.formtype == "add") {
			me.FFile_BDictTree_Id.value = me.BDictTreeId;
		}
		items.push(me.FFile_BDictTree_Id);
		return items;
	}
});