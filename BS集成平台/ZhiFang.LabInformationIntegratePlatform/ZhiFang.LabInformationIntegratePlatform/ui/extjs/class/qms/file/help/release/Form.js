/**
 * 帮助系统发布表单
 * @author longfc
 * @version 2016-11-22
 */
Ext.define('Shell.class.qms.file.help.release.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.class.qms.file.copyuser.userComboBox'
	],
	bodyPadding: '5px 5px 5px 5px',
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 95,
		width: 210,
		labelAlign: 'right'
	},
	formtype: "add",
	title: '帮助信息',
	width: 790,
	height: 520,
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
	fFileOperationType: 5,
	/**文档状态值*/
	fFileStatus: 5,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**列表行的树类型Id*/
	BDictTreeId: "",
	/**列表行的树类型*/
	BDictTreeCName: "",
	autoScroll: true,
	/**帮助文档所属模块Id*/
	PK: '',

	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**获取树的最大层级数*/
	LEVEL: "",
	/**编辑文档类型(如新闻/通知/文档/修订文档)*/
	FTYPE: '5',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();

	},
	initComponent: function() {
		var me = this;
		var width = me.width / 4 - 10;
		me.defaults.width = (width < 210) ? 210 : width;
		me.selectUrl = me.selectUrl + "&isAddFFileReadingLog=0&isAddFFileOperation=0";
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddNewsShowItems();
		items = items.concat(me.getAddFFileTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},

	/**创建可见组件*/
	createAddNewsShowItems: function() {
		var me = this;
		me.createFFile_Title("文档标题");
		//me.createFFile_Keyword("模块选择");
		me.createFFile_No("文档编码");
		//me.createFFile_Summary('模块子序号');
		me.createFFileMemo('文档备注');
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

	/***模块名称*/
	createFFile_Keyword: function(fieldLabel) {
		var me = this;
		me.FFile_Keyword= {
			fieldLabel: fieldLabel,
			emptyText: '文档所属的模块   模块编号格式"H"+模块ID-+"子序号"',
			allowBlank: true,
			name: 'FFile_Keyword',
			itemId: 'FFile_Keyword',
			IsnotField: false,
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			multiSelect: false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.sysbase.module.CheckTree', {
					isShowchecked: false,
					rootVisible: false,
					defaultLoad: true,
					listeners: {
						accept: function(p, record) {
							var No = me.getComponent('FFile_No');
							var VersionNo = me.getComponent('FFile_Keyword');
							var Title = me.getComponent('FFile_Title');

							No.setValue(record ? "H-" + record.get('tid') : ''); //"H" + 
							VersionNo.setValue(record ? record.get('text') : '');
							if(Title.getValue() == "") {
								Title.setValue(record ? record.get('text') : '');
							}
							p.close();
						}
					}
				}).show();
			}
		};
	},
	/**创建所属模块编号*/
	createFFile_No: function(fieldLabel) {
		var me = this;
		me.FFile_No = {
			fieldLabel: fieldLabel,
			emptyText: '必填项  输入格式"H-"+"模块Id"+"子序号" 如(H-100666601-1)',
			allowBlank: false,
			name: 'FFile_No',
			itemId: 'FFile_No',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue != null && newValue != undefined) {
						newValue = newValue.toString();
						if(newValue.length > 0 && (newValue.indexOf("H") == -1 || newValue.indexOf("H") > 0)) {
							newValue = "H-" + newValue;
							com.setValue(newValue);
						}
					}
				}
			}
		};
	},
	/**模块子序号*/
	createFFile_Summary: function(fieldLabel) {
		var me = this;
		me.FFile_Summary = {
			fieldLabel: fieldLabel,
			//xtype: 'numberfield',
			itemId: 'FFile_Summary',
			name: 'FFile_Summary'
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
	/**所属模块子序号*/
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
			height = me.height * 0.72;

		me.FFile_Memo = {
			fieldLabel: fieldLabel,
			name: 'FFile_Memo',
			minHeight: minHeight,
			height: height,
			maxLength: 500,
			maxLengthText: fieldLabel + "最多只能输入500字",
			style: {
				marginBottom: '2px'
			},
			xtype: 'textarea'
		};
	},

	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		//标题
		me.FFile_Title.colspan = 4;
		me.FFile_Title.width = me.defaults.width * me.FFile_Title.colspan;
		items.push(me.FFile_Title);

		//模块编号
		me.FFile_No.colspan = 4;
		me.FFile_No.width = me.defaults.width * me.FFile_No.colspan;
		items.push(me.FFile_No);

		//备注
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
	/**返回数据处理方法*/
	changeResult: function(data) {
		//data.FFile_BeginTime = JShell.Date.getDate(data.FFile_BeginTime);
		//data.FFile_EndTime = JShell.Date.getDate(data.FFile_EndTime);
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

	/**选择上级机构*/
	onBDictTreeAccept: function(record) {
		var me = this,
			ParentID = me.getComponent('FFile_BDictTree_Id'),
			ParentName = me.getComponent('FFile_BDictTree_CName');
		ParentID.setValue(record.get('tid'));
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
		var BDictTreeId = {
			fieldLabel: '内容类型ID',
			hidden: true,
			itemId: 'FFile_BDictTree_Id',
			name: 'FFile_BDictTree_Id'
		};
		if(me.formtype == "add") BDictTreeId.value = me.BDictTreeId;
		items.push(BDictTreeId);
		
		var USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;

		var FFile_DrafterId = {
			fieldLabel: '起草人ID',
			hidden: true,
			itemId: 'FFile_DrafterId',
			name: 'FFile_DrafterId'
		};
		if(me.formtype == "add") {
			if(USERID && USERID != null) {
				FFile_DrafterId.value = USERID;
			}
		}
		items.push(FFile_DrafterId);
		
		var FFile_BDictTree_CName = {
			fieldLabel: '内容类型',
			readOnly: true,
			locked: true,
			emptyText: '必填项',
			itemId: 'FFile_BDictTree_CName',
			name: 'FFile_BDictTree_CName',
			IsnotField: true,
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			hidden: true,
			multiSelect: false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.sysbase.dicttree.CheckTree', {
					isShowchecked: false,
					rootVisible: true,
					defaultLoad: false,
					listeners: {
						accept: function(p, record) {
							me.onBDictTreeAccept(record);
							p.close();
						}
					}
				}).show();
			}
		};
		if(me.formtype == "add") FFile_BDictTree_CName.value = me.BDictTreeCName;
		items.push(FFile_BDictTree_CName);
		return items;
	}
});