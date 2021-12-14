/**
 * 文档发布表单
 * @author longfc
 * @version 2016-06-23
 * @version 2017-11-29 添加对发布阅读人,localStorage的支持,发布阅读人通过localStorage缓存进行还原登录者最近一次设置的
 */
Ext.define('Shell.class.qms.file.file.release.ReleaseForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.class.qms.file.copyuser.userComboBox'
	],
	bodyPadding: '5px 5px 5px 5px',
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 200,
		labelAlign: 'right'
	},
	formtype: "add",
	title: '发布信息',
	width: 790,
	height: 650,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**获取数据服务路径*/
	selectUrl: '/QMSService.svc/QMS_UDTO_SearchFFileById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/QMSService.svc/QMS_UDTO_AddFFileAndFFileCopyUser',
	/**修改服务地址*/
	editUrl: '/QMSService.svc/QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**文档操作记录类型值*/
	fFileOperationType: 5,
	/**文档状态值*/
	fFileStatus: 5,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	showSuccessInfo: false,
	height: 240,
	width: 440,

	hasReset: false,
	title: "发布文档",
	formtype: 'add',

	PK: '',
	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**编辑文档类型(如新闻/通知/文档/修订文档)*/
	FTYPE: '',
	/**是否启用localStorage*/
	openLocalStorage: true,
	/**localStorage的key值:fileRelease:+用户ID*/
	itemKey: "",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.selectUrl = me.selectUrl + "&isAddFFileReadingLog=0&isAddFFileOperation=0";
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.itemKey = "fileRelease:" + userId;
		if(!window.localStorage) me.openLocalStorage = false;
		if(!me.getlocalStorage()) me.setlocalStorage("");
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddFFileShowItems();
		items = items.concat(me.getAddFFileTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},
	/**创建文档可见组件*/
	createAddFFileShowItems: function() {
		var me = this;
		me.createFFile_IsTop('是否置顶');
		me.createFFile_IsDiscuss('发布后是否可评论');
		me.createDate();
		me.createFFileReadingUser("发布范围");
	},
	/**是否置顶*/
	createFFile_IsTop: function(fieldLabel) {
		var me = this;
		me.FFile_IsTop = {
			boxLabel: fieldLabel,
			name: 'FFile_IsTop',
			itemId: 'FFile_IsTop',
			xtype: 'checkbox',
			checked: true,
			style: {
				marginLeft: '20px'
			}
		};
	},
	/**发布后是否可评论*/
	createFFile_IsDiscuss: function(fieldLabel) {
		var me = this;
		me.FFile_IsDiscuss = {
			boxLabel: fieldLabel,
			name: 'FFile_IsDiscuss',
			itemId: 'FFile_IsDiscuss',
			xtype: 'checkbox',
			checked: true,
			style: {
				marginLeft: '20px'
			}
		};
	},
	/**发布阅读人*/
	createFFileReadingUser: function(fieldLabel) {
		var me = this;
		me.FFileReadingUser = {
			allowBlank: false,
			emptyText: '必填项',
			name: 'FFileReadingUser',
			itemId: 'FFileReadingUser',
			xtype: 'userComboBox',
			fieldLabel: fieldLabel,
			hasNull: false,
			objectEName: 'FFileReadingUser',
			selectUrl: '/QMSService.svc/QMS_UDTO_SearchFFileReadingUserByHQL?isPlanish=true',
			labelWidth: me.defaults.labelWidth,
			formtype: me.formtype,
			AppOperationType: me.AppOperationType,
			FTYPE: me.FTYPE,
			PK: me.FFileId,
			listeners: {
				changeResult: function(p, resultValue) {
					if(!resultValue) resultValue = {
						valueType: p.TypeArrData[0][0],
						list: [],
						IdStrValue: "",
						DisplayValue: ""
					};
					resultValue = Ext.JSON.encode(resultValue);
					me.setlocalStorage(resultValue);
				}
			}
		};
	},
	/**创建时间*/
	createDate: function() {
		var me = this;
		me.FFile_BeginTime = {
			fieldLabel: '生效日期',
			name: 'FFile_BeginTime',
			itemId: 'FFile_BeginTime',
			xtype: 'datefield',
			format: 'Y-m-d'
		};
		me.FFile_EndTime = {
			fieldLabel: '失效日期',
			name: 'FFile_EndTime',
			itemId: 'FFile_EndTime',
			xtype: 'datefield',
			format: 'Y-m-d'
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		//是否置顶
		me.FFile_IsTop.colspan = 2;
		me.FFile_IsTop.width = me.defaults.width * me.FFile_IsTop.colspan;
		items.push(me.FFile_IsTop);
		//发布后是否可评论
		me.FFile_IsDiscuss.colspan = 2;
		me.FFile_IsDiscuss.width = me.defaults.width * me.FFile_IsDiscuss.colspan;
		items.push(me.FFile_IsDiscuss);

		//有效开始时间
		me.FFile_BeginTime.colspan = 1;
		items.push(me.FFile_BeginTime);
		//有效截止时间
		me.FFile_EndTime.colspan = 1;
		items.push(me.FFile_EndTime);

		me.FFileReadingUser.colspan = 2;
		me.FFileReadingUser.width = me.defaults.width * me.FFileReadingUser.colspan;
		items.push(me.FFileReadingUser);
	
		return items;

	},

	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;

	},

	/**返回数据处理方法*/
	changeResult: function(data) {
		data.FFile_BeginTime = JShell.Date.getDate(data.FFile_BeginTime);
		data.FFile_EndTime = JShell.Date.getDate(data.FFile_EndTime);
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/***/
	isAdd: function() {
		var me = this;
		me.initFilterListeners();
		var resultValue = me.getlocalStorage();
		if(resultValue) resultValue = Ext.JSON.decode(resultValue);
		var copyUser = me.getComponent('FFileReadingUser');
		copyUser.setValue(resultValue);
	},
	onSaveClick: function() {
		var me = this;
		var isValid = me.getForm().isValid()
		if(!isValid) {
			JShell.Msg.alert("表单非空验证不通过!", null, 800);
			return;
		}
		var values = me.getForm().getValues();
		var params = {
			entity: {},
			ffileReadingUserType: -1,
			fFileReadingUserList: []
		};
		var entity = {
			IsTop: values.FFile_IsTop ? 1 : 0,
			IsDiscuss: values.FFile_IsDiscuss ? 1 : 0
		};
		var BeginTime = "",
			EndTime = "";
		if(values.FFile_BeginTime) {
			BeginTime = JShell.Date.toServerDate(values.FFile_BeginTime);
		}
		if(values.FFile_EndTime) {
			EndTime = JShell.Date.toServerDate(values.FFile_EndTime);
		}
		if(BeginTime != '') {
			entity.BeginTime = BeginTime;
		}
		if(EndTime != '') {
			entity.EndTime = EndTime;
		}
		//阅读人
		var copyUserValue = null;
		var copyUser = me.getComponent('FFileReadingUser');
		copyUserValue = copyUser.getValue();
		if(copyUserValue && copyUserValue != null) {
			var valueType = copyUserValue.valueType;
			var list = copyUserValue.list;
			if(valueType != null && valueType != "") {
				params.ffileReadingUserType = parseInt(valueType);
			}
			if(list != null && list != "") {
				params.fFileReadingUserList = list;
			}
		}
		params.entity = entity;
		me.fireEvent('save', me, params);
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
		return items;
	},
	setlocalStorage: function(value) {
		var me = this;
		if(me.openLocalStorage == true) {
			localStorage.setItem(me.itemKey, value);
		} else {
			Shell.util.Cookie.set(me.itemKey, value);
		}
	},
	getlocalStorage: function() {
		var me = this;
		if(me.openLocalStorage == true) {
			return localStorage.getItem(me.itemKey);
		} else {
			return Shell.util.Cookie.get(me.itemKey);
		}
	},
	removelocalStorage: function() {
		var me = this;
		if(me.openLocalStorage == true) {
			localStorage.removeItem(me.itemKey);
		} else {
			Shell.util.Cookie.delCookie(me.itemKey);
		}
	}
});