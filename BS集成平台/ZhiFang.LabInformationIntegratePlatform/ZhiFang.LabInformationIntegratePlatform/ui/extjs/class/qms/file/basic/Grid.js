/**
 * QMS文档信息列表
 * @author longfc
 * @version 2016-06-22
 */
Ext.define('Shell.class.qms.file.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '文档信息列表',
	width: 1200,
	height: 800,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileByBDictTreeId?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField',

	/**删除文档信息(更新IsUse为false,文档状态为作废)*/
	delUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_DeleleFFileByIds',
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
	/**删除标志字段*/
	DelField: 'delState',
	multiSelect: true,
	defaultWhere: '',
	hasShow: true,
	hasSave: false,
	hasDel: false,
	/**文件的操作记录类型*/
	fFileOperationType: 1,

	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**获取树的最大层级数*/
	LEVEL: "",
	/**抄送人,阅读人的按人员选择时的角色姓名传入*/
	ROLEHREMPLOYEECNAME: "",
	/**编辑文档类型(如新闻/通知/文档/修订文档)*/
	FTYPE: '',
	/**列表的树类型Id*/
	BDictTreeId: "",
	BDictTreeCName: '',
	checkOne: true,
	fFileStatus: '1',

	/**查询条件是否带上登录帐号id*/
	isSearchUSERID: false,
	/**撤消提交/审核等按钮的显示值*/
	DisagreeOfGridText: "",
	/**提交/审核等按钮的显示值*/
	AgreeOfGridText: "",
	HiddenAgreeOfGrid: true,
	HiddenDisagreeOfGrid: true,
	
	/**应用操作分类*/
	AppOperationType: "",
	/**是否隐藏工具栏查询条件*/
	hiddenSearch: false,
	/**是否隐藏工具栏查询条件*/
	hiddenbuttonsToolbar: false,
	/*是否带内容类型复选**/
	hasCheckBDictTree: false,
	
	/**是否隐藏文档状态选择项*/
	hiddenFFileStatus: false,
	/**是否隐藏日期查询选择项*/
	hiddenDateSearch: false,
	hasRefresh: true,
	/**文档的交流类型:对查询应用(show)的交流应用获取交流记录做默认时间(交流的addtime大于等于发布时间)过滤，起草等(edit)不需要 未完成*/
	interactionType: "edit",
	/**我的阅读文档查询时是否查询传入节点的子孙节点*/
	isSearchChildNode: true,
	/*推送操作列是否隐藏**/
	hiddenWeiXinMessagePush: false,
	
	/**是否显示内容页签*/
	hasContent: true,
	/**是否显示文档详情页签*/
	hasFFileOperation: false,
	/**是否显示操作记录页签*/
	hasOperation: true,
	/**是否显示阅读记录页签*/
	hasReadingLog: true,
	FFileStatusList: null,
	StatusEnum: null,
	StatusFColorEnum: null,
	StatusBGColorEnum: null,
	
	FFileDateTypeList: null,
	/**默认查询条件是否需要Type*/
	hasSearchType: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**文档状态选择项的默认值*/
	defaultStatusValue: "1",
	/*文档日期范围类型默认值**/
	defaultFFileDateTypeValue: 'ffile.DrafterDateTime',
	    /**PDF预览0下载按钮显示，1不显示*/
	DOWNLOAD:'',
	/**PDF预览0打印按钮显示，1不显示*/
	PRINT:'',
	/**1 使用内置pdf预览,0 不使用内置浏览器，不支持控制pdf下载，打印按钮，*/
    BUILTIN:'1',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(me.hiddenbuttonsToolbar) {
			buttonsToolbar.setVisible(false);
		}
	},
	initComponent: function() {
		var me = this;
		me.getStatusListData(null);
		me.FTYPE = me.FTYPE || "";
		me.IDS = me.IDS || "";
		me.ROLEHREMPLOYEECNAME = me.ROLEHREMPLOYEECNAME || "";
		if(me.IDS && me.IDS.toString().length > 0) {
			me.treeShortcodeWhere = "idListStr=" + me.IDS;
		} else {
			me.treeShortcodeWhere = me.treeShortcodeWhere || "";
		}
		if(!me.checkOne) {
			me.multiSelect = true;
			me.selType = 'checkboxmodel';
		}
		me.createSearchInfo();
		//创建数据列
		me.columns = me.createGridColumns();
		//初始化功能按钮栏内容
		me.createSearchtoolbar();
		//初始化默认条件
		me.initDefaultWhere();
		me.addEvents('onAddClick', me);
		me.addEvents('onEditClick', me);
		me.addEvents('onDisagreeSaveClick', me);
		me.callParent(arguments);
	},
	/**获取文档状态信息*/
	getStatusListData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl('/ServerWCF/CommonService.svc/GetClassDic');
		url = url + "?classname=FFileStatus&classnamespace=ZhiFang.Entity.Common";
		JcallShell.Server.get(url, function(data) {
			if(data.success && data.value) {
				if(data.value.length > 0) {
					me.FFileStatusList = [];
					me.StatusEnum = {};
					me.StatusFColorEnum = {};
					me.StatusBGColorEnum = {};
					var tempArr = [];
					me.FFileStatusList.push(["", '全部', 'font-weight:bold;text-align:center;']);
					Ext.Array.each(data.value, function(obj, index) {
						var style = ['font-weight:bold;text-align:center;'];
						if(obj.FontColor) {
							//style.push('color:' + obj.FontColor);
							me.StatusFColorEnum[obj.Id] = obj.FontColor;
						}
						if(obj.BGColor) {
							style.push('color:' + obj.BGColor); //background-
							me.StatusBGColorEnum[obj.Id] = obj.BGColor;
						}
						me.StatusEnum[obj.Id] = obj.Name;
						tempArr = [obj.Id, obj.Name, style.join(';')];
						me.FFileStatusList.push(tempArr);

					});
				}
			}
		}, false);
	},
	/**对外公开,由外面传入列信息*/
	createNewColumns: function() {
		var me = this;
		//创建数据列
		var tempColumns = [];
		return tempColumns;
	},
	/**overwrite*/
	createSearchInfo: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 110,
			emptyText: '标题/编号/关键字',
			isLike: true,
			itemId: 'search',
			style: {
				marginRight: '10px'
			},
			fields: ['ffile.Title', 'ffile.No', 'ffile.Keyword']
		};
	},
	createGridColumns: function() {
		var me = this;
		//创建数据列
		var columns = [];
		columns = me.createNewColumns().length > 0 ? me.createNewColumns() : me.createDefaultColumns();
		return columns;
	},
	/**初始化查询栏内容*/
	createSearchtoolbar: function() {
		var me = this;
		var items = [];

		if(me.hasRefresh) {
			items.push('refresh');
		}
		if(me.hasAdd) items.push('add');
		if(me.hasShow) items.push('show');
		if(me.hasSave) items.push('save');
		
		//禁用按钮
		items = me.createButtonLockButton(items);
		//撤消提交/撤消禁用按钮
		items.push(me.createDisagreeSaveButton());
		//置顶按钮
		items = me.createdoTopButton(items);
		//撤销置顶按钮
		items = me.createdoNoTopButton(items);
		//生成帮助文档按钮
		items = me.createSaveHelpHtmlAndJsonButton(items);
		//if(me.hasDel) items.push("-",'del');
		items.push('->', "-");
		//是否带内容类型复选
		if(me.hasCheckBDictTree) {
			items.push({
				boxLabel: '本节点',
				itemId: 'checkBDictTreeId',
				checked: false,
				value: false,
				inputValue: false,
				xtype: 'checkbox',
				style: {
					marginRight: '8px'
				},
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(newValue == true) {
							me.isSearchChildNode = false;
						} else {
							me.isSearchChildNode = true;
						}
						me.onSearch();
					}
				}
			});
		}
		var dataList=me.FFileStatusList != null ? me.FFileStatusList : me.getStatusListData(null);
		if(!dataList){
			dataList=JcallShell.QMS.Enum.getList('FFileStatus', true, false, true, true, false);
		}
		dataList=me.removeDataList(dataList);
		items.push({
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data:dataList,
			value: me.defaultStatusValue,
			width: 95,
			labelWidth: 0,
			hidden: me.hiddenFFileStatus,
			fieldLabel: '',
			tooltip: '文档状态选择',
			itemId: 'selectStatus',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: me.FFileDateTypeList != null ? me.FFileDateTypeList : JcallShell.QMS.Enum.getList('FFileDateType', false, false, true, true, false),
			value: me.defaultFFileDateTypeValue,
			hidden: me.hiddenDateSearch,
			width: 85,
			labelWidth: 0,
			fieldLabel: '',//日期范围
			tooltip: '日期选择',
			itemId: 'FFileDateType',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			width: 95,
			labelWidth: 1,
			labelAlign: 'right',
			hidden: me.hiddenDateSearch,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue) me.onSearch();
				}
			}
		}, {
			width: 100,
			hidden: me.hiddenDateSearch,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue) me.onSearch();
				}
			}
		});
		items.push({
			type: 'search',
			info: me.searchInfo
		});
		me.buttonToolbarItems = items;
	},
	revertSearchData: function() {
		var me = this;
	},
	/**文档状态下拉选择框数据处理*/
	removeDataList: function(dataList) {
		var me = this;
		return dataList;
	},
	initDefaultColumn: function(columns) {
		var me = this;
		columns.push({
			text: '创建时间',
			dataIndex: 'FFile_DataAddTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		}, {
			text: '创建人Id',
			dataIndex: 'FFile_Creator_Id',
			hidden: true,
			hideable: false
		}, {
			text: '创建人',
			dataIndex: 'FFile_CreatorName',
			width: 120,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'FFile_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '树类型ID',
			dataIndex: 'FFile_BDictTree_Id',
			hidden: true,
			hideable: false
		}, {
			text: '是否允许评论',
			dataIndex: 'FFile_IsDiscuss',
			hidden: true,
			hideable: false
		}, {
			text: '原始文档GUID',
			dataIndex: 'FFile_OriginalFileID',
			hidden: true,
			hideable: false
		}, {
			text: '置顶',
			dataIndex: 'FFile_IsTop',
			hidden: true,
			hideable: false
		}, me.createreFFileTitle(), {
			text: '状态',
			dataIndex: 'FFile_Status',
			width: 65,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta) {
				var v = JcallShell.QMS.Enum.FFileStatus[value];
				meta.style = 'font-weight:bold;color:' + JShell.QMS.Enum.FFileOperationTypeColor[value];
				return v;
			}
		}, {
			text: '类型',
			dataIndex: 'FFile_BDictTree_CName',
			hidden: false,
			sortable: true,
			width: 100,
			hideable: false
		}, {
			text: '修订时间',dataIndex: 'FFile_ReviseTime',
			hidden: true,isDate: true,hasTime: true,
			sortable: true,width: 100,hideable: false
		});
		return columns;
	},

	/**创建数据列*/
	createDefaultColumns: function() {
		var me = this;
		var columns = [];
		columns = me.initDefaultColumn(columns);
		if(me.FTYPE == JcallShell.QMS.Enum.FType.新闻应用) {
			columns = me.createWeiXinMessagePush(columns);
		}
		//交流列
		columns.push(me.createInteraction());
		//操作记录查看列
		columns.push(me.createOperation());
		//阅读记录列
		columns.push(me.createreadinglog());
		columns.push(me.createreFFileNo(), me.createreVersionNo(), me.createreKeyword());
		me.createOtherColumn(columns);
		return columns;
	},
	/*创建文档标题列*/
	createreFFileTitle: function() {
		var me = this;
		return {
			text: '标题',
			dataIndex: 'FFile_Title',
			width: 160,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var IsTop = record.get("FFile_IsTop");
				if(IsTop == "true") {
					value = "<b style='color:red;'>【置顶】</b>" + value;
				}
				return value;
			}
		};
	},
	/*创建编号列*/
	createreFFileNo: function() {
		var me = this;
		return {
			text: '编号',
			dataIndex: 'FFile_No',
			hidden: false,
			width: 80,
			hideable: false
		};
	},
	/*创建版本列*/
	createreVersionNo: function() {
		var me = this;
		return {
			text: '版本',
			dataIndex: 'FFile_VersionNo',
			hidden: false,
			width: 90,
			hideable: false
		};
	},
	/*创建关键字列*/
	createreKeyword: function() {
		var me = this;
		return {
			text: '关键字',
			dataIndex: 'FFile_Keyword',
			hidden: false,
			width: 80,
			hideable: false
		};
	},
	/**微信推送操作列*/
	createWeiXinMessagePush: function(columns) {
		var me = this;
		columns.push({
			text: '开始日期',
			dataIndex: 'FFile_BeginTime',
			hidden: true
		}, {
			text: '结束日期',
			dataIndex: 'FFile_EndTime',
			hidden: true
		}, {
			xtype: 'actioncolumn',
			text: '推送',
			align: 'center',
			hidden: me.hiddenWeiXinMessagePush,
			width: 45,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				getClass: function(v, meta, record) {
					//是否符合微信消息推送的条件
					var canPush = me.CanDoWeiXinMessagePush(record);
					//已发布
					var isOver = record.get('FFile_Status') == 5;
					if(isOver && canPush) {
						meta.tdAttr = 'data-qtip="<b>新闻微信消息推送</b>"';
						return 'button-config hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					//根据新闻ID进行微信消息推送
					me.WeiXinMessagePush(id);
				}
			}]
		});
		return columns;
	},
	/*创建交流列*/
	createreadinglog: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '阅读',
			align: 'center',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.openFFileGrid(id, 'Shell.class.qms.file.readinglog.Grid', '');
				}
			}]
		}
	},
	/*创建启用列*/
	createreIsUse: function() {
		var me = this;
		return {
			text: '启用',
			dataIndex: 'FFile_IsUse',
			isBool: true,
			width: 40,
			type: 'bool',
			editor: {
				xtype: 'uxBoolComboBox'
			},
			sortable: false,
			menuDisabled: true
		};
	},
	/*创建发布人列*/
	createrePublisherName: function() {
		var me = this;
		return {
			text: '发布人',
			dataIndex: 'FFile_PublisherName',
			width: 80,
			sortable: false,
			menuDisabled: true
		};
	},
	/*创建发布时间列*/
	createrePublisherDateTime: function() {
		var me = this;
		return {
			text: '发布时间',
			dataIndex: 'FFile_PublisherDateTime',
			width: 130,
			sortable: true,
			isDate: true,
			hasTime: true,
			menuDisabled: true
		};
	},
	createOtherColumn: function(columns) {
		var me = this;
		if(columns == null) {
			columns = [];
		}
		columns.push({
			text: '起草人',
			dataIndex: 'FFile_DrafterCName',
			width: 80,
			sortable: false,
			menuDisabled: true
		}, {
			text: '审核人',
			dataIndex: 'FFile_CheckerName',
			width: 80,
			sortable: false,
			menuDisabled: true
		}, {
			text: '审批人',
			dataIndex: 'FFile_ApprovalName',
			width: 80,
			sortable: false,
			menuDisabled: true
		}, me.createrePublisherName(), {
			text: '起草时间',
			dataIndex: 'FFile_DrafterDateTime',
			width: 130,
			isDate: true,
			hasTime: true,
			sortable: false,
			menuDisabled: true
		}, {
			text: '审核时间',
			dataIndex: 'FFile_CheckerDateTime',
			width: 130,
			isDate: true,
			hasTime: true,
			sortable: false,
			menuDisabled: true
		}, {
			text: '审批时间',
			dataIndex: 'FFile_ApprovalDateTime',
			width: 130,
			sortable: false,
			isDate: true,
			hasTime: true,
			menuDisabled: true
		}, me.createrePublisherDateTime());
		return columns;
	},
	/*创建操作记录列**/
	createOperation: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hidden: false,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.openFFileGrid(id, 'Shell.class.qms.file.operation.Grid', '');
				}
			}]
		};
	},
	/*创建交流列**/
	createInteraction: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '交流',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-interact hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var isInteraction = rec.get("FFile_IsDiscuss");
					if(isInteraction) {
						me.showInteractionById(rec);
					} else {
						JShell.Msg.error('当前文档不允许交流');
					}
				}
			}]
		};
	},
	/**创建禁用内容按钮*/
	createButtonLockButton: function(items) {
		return items;
	},
	/**创建置顶按钮*/
	createdoTopButton: function(items) {
		return items;
	},
	/**创建撤消置顶按钮*/
	createdoNoTopButton: function(items) {
		return items;
	},
	/**生成帮助文档*/
	createSaveHelpHtmlAndJsonButton: function(items) {
		return items;
	},
	/**创建撤消提交/禁用按钮*/
	createDisagreeSaveButton: function() {
		var me = this;
		return {
			xtype: 'button',
			itemId: 'btnDisagree',
			iconCls: 'button-back',
			hidden: me.HiddenDisagreeOfGrid,
			text: me.DisagreeOfGridText,
			tooltip: me.DisagreeOfGridText,
			handler: function() {
				me.fireEvent('onDisagreeSaveClick', me);
			}
		};
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			selectStatus = buttonsToolbar.getComponent('selectStatus').getValue(),
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
			EndDate = buttonsToolbar.getComponent('EndDate').getValue(),
			search = buttonsToolbar.getComponent('search').getValue(),
			ffileDateType = buttonsToolbar.getComponent('FFileDateType').getValue();
		var checkBDictTreeId = buttonsToolbar.getComponent('checkBDictTreeId');
		var params = [];
		if(selectStatus) {
			params.push("ffile.Status=" + selectStatus + "");
		}
		if(ffileDateType == "") {
			ffileDateType = "ffile.DataAddTime";
		}
		if(BeginDate) {
			params.push(ffileDateType + ">='" + JShell.Date.toString(BeginDate, true) + "'");
		}
		if(EndDate) {
			params.push(ffileDateType + "<='" + JShell.Date.toString(EndDate, true) + "  23:59:59'");
		}
		var where = "",
			arr = [],
			url = JShell.System.Path.ROOT + me.selectUrl + "&isSearchChildNode=" + me.isSearchChildNode;

		if(me.BDictTreeId && me.BDictTreeId.toString().length > 0) {
			where = 'id=' + me.BDictTreeId + '^';
		} else if(me.IDS && me.IDS.toString().length > 0) {
			where = 'id=' + me.IDS + '^';
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}

		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		if(arr.length > 0) {
			where += '(' + arr.join(" and ") + ')';
		}
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'where=' + JcallShell.String.encode(where) + '&fields=' + me.getStoreFields(true).join(',');

		return url;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},

	/**初始化默认条件*/
	initDefaultWhere: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		//如果FTYPE是传入的是修订文档类型4,查询和保存时需要用1
		var ftype = (me.FTYPE == 4 ? 1 : me.FTYPE);

		if(me.hasSearchType == true && ftype != "" && ftype != null) {
			if(me.defaultWhere && me.defaultWhere.length > 0) {
				me.defaultWhere = me.defaultWhere + " and ffile.Type in(" + ftype + ")";
			} else {
				me.defaultWhere = "ffile.Type in(" + ftype + ")";
			}
		}
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') ';
		}

		if(me.isSearchUSERID) {
			var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
			//			var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
			if(userId && userId != null && userId != "" && userName != "admin") {
				if(me.defaultWhere && me.defaultWhere.length > 0) {
					me.defaultWhere += " and (ffile.Creator.Id=" + userId + ")";
				} else {
					me.defaultWhere = "(ffile.Creator.Id=" + userId + ")";
				}
			}
		}
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	onEditClick: function() {
		var me = this;
		me.fireEvent('onEditClick', me);
	},
	onShowClick: function() {
		var me = this;
		me.fireEvent('onShowClick', me);
	},
	onDelClick: function() {
		var me = this;
		me.fireEvent('onDelClick', me);
	},
	/**打开文档操作记录列表*/
	openFFileGrid: function(id, grid, OriginalFileID) {
		var me = this;
		var config = {
			showSuccessInfo: false,
			hasButtontoolbar: false,
			PK: id,
			OriginalFileID: OriginalFileID
		};
		var win = JShell.Win.open(grid, config).show();
	},
	/**为应用操作分类赋值*/
	setAppOperationType: function() {
		var me = this;
		switch(me.FTYPE) {
			case JcallShell.QMS.Enum.FType.文档应用:
				me.AppOperationType = (me.formtype == "add") ? JcallShell.QMS.Enum.AppOperationType.新增文档 : JcallShell.QMS.Enum.AppOperationType.编辑文档;
				break;
			case JcallShell.QMS.Enum.FType.新闻应用:
				me.AppOperationType = (me.formtype == "add") ? JcallShell.QMS.Enum.AppOperationType.新增新闻 : JcallShell.QMS.Enum.AppOperationType.编辑新闻;
				break;
			case JcallShell.QMS.Enum.FType.修订文档应用:
				me.AppOperationType = (me.formtype == "add") ? JcallShell.QMS.Enum.AppOperationType.新增修订文档 : JcallShell.QMS.Enum.AppOperationType.编辑修订文档;
				break;
			default:
				break;
		}
	},
	/**根据新闻ID进行微信消息推送
	 * @param {Object} id
	 * @author Jcall
	 * @version 2016-08-25
	 */
	WeiXinMessagePush: function(id) {
		var me = this;
		var url = JShell.System.Path.getRootUrl('/ServerWCF/CommonService.svc/QMS_FFileWeiXinMessagePushById?id=' + id);
		//根据新闻ID进行微信消息推送
		me.showMask('微信消息推送中...'); //显示遮罩层
		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				JShell.Msg.alert('新闻成功进行微信消息推送！');
			} else {
				JShell.Msg.error('微信消息推送失败！');
			}
		});
	},
	/**是否符合微信消息推送的条件
	 * @param {Object} record
	 * @author Jcall
	 * @version 2016-08-25
	 */
	CanDoWeiXinMessagePush: function(record) {
		var me = this,
			NowTimes = JShell.System.Date.getDate().getTime(),
			BeginTime = record.get('FFile_BeginTime'),
			EndTime = record.get('FFile_EndTime');
		//永久
		if(!BeginTime && !EndTime) return true;

		//存在开始日期
		if(BeginTime) {
			BeginTime = JShell.Date.getDate(JShell.Date.toString(BeginTime, true));
			if(NowTimes < BeginTime.getTime()) return false;
		}

		//存在开始日期
		if(EndTime) {
			EndTime = JShell.Date.getNextDate(JShell.Date.toString(EndTime, true));
			if(NowTimes >= EndTime.getTime()) return false;
		}

		return true;
	},

	/**根据ID查看文档交流*/
	showInteractionById: function(record) {
		var me = this;
		var id = record.get('FFile_Id');
		var publisherDateTime = record.get("FFile_PublisherDateTime");

		var isInteraction = true;
		//如果是查看应用,交流记录列表需要处理默认条件
		switch(me.interactionType) {
			case "show":
				var IsDiscuss = record.get("FFile_IsDiscuss");
				if(IsDiscuss.toLowerCase() == "false") {
					isInteraction = false;
				}
				break;
			default:
				break;
		}
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var creatorId = record.get("FFile_Creator_Id")
		var IsCreator = false;
		if(userId == creatorId) {
			IsCreator = true;
		}
		var maxWidth = document.body.clientWidth - 20;
		var height = document.body.clientHeight - 20;
		var defaultWhere = "";
		//如果是查看应用,交流记录列表需要处理默认条件
		switch(me.interactionType) {
			case "show":
				if(publisherDateTime && publisherDateTime != "") {
					defaultWhere = "ffileinteraction.DataAddTime>='" + publisherDateTime + "'";
				}
				break;
			default:
				break;
		}
		JShell.Win.open('Shell.class.qms.file.interaction.App', {
			FFileId: id,
			FileId: id,
			PK: id,
			height: height,
			width: maxWidth,
			FTYPE: me.FTYPE,
			IsCreator: (IsCreator != null ? IsCreator : false),
			/**交流记录列表的默认条件*/
			defaultWhere: defaultWhere,
			IsDiscuss: (IsDiscuss != null ? IsDiscuss : false),
			fFileOperationType: me.fFileOperationType
		}).show();
	},
	/**查看文档*/
	openShowTabPanel: function(record) {
		var me = this;
		var maxWidth = document.body.clientWidth - 20;
		var height = document.body.clientHeight - 20;
		var defaultWhere = "";
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
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var creatorId = record.get("FFile_Creator_Id")
		var IsCreator = false;
		if(userId == creatorId) {
			IsCreator = true;
		}
		//如果是查看应用,交流记录列表需要处理默认条件
		switch(me.interactionType) {
			case "show":
				if(publisherDateTime && publisherDateTime != "") {
					defaultWhere = "ffileinteraction.DataAddTime>='" + publisherDateTime + "'";
				}
				break;
			default:
				break;
		}
		//如果原始文档id不为空，显示修订记录页
		var ReviseTime = record.get('FFile_OriginalFileID'); 
		var hasRevise = ReviseTime ? true : false;
		
		JShell.Win.open('Shell.class.qms.file.show.ShowTabPanel', {
			FFileId: id,
			FileId: id,
			PK: id,
			height: height,
			width: maxWidth,
			FTYPE: me.FTYPE,
			/**是否显示内容页签*/
			hasContent: me.hasContent,
			/**是否显示文档详情页签*/
			hasFFileOperation: me.hasFFileOperation,
			/**是否显示操作记录页签*/
			hasOperation: me.hasOperation,
			/**是否显示阅读记录页签*/
			hasReadingLog: me.hasReadingLog,
			/**是否显示修订记录页签*/
			hasRevise:hasRevise,
			IsCreator: (IsCreator != null ? IsCreator : false),
			/**交流记录列表的默认条件*/
			defaultWhere: defaultWhere,
			IsDiscuss: (IsDiscuss != null ? IsDiscuss : false),
			fFileOperationType: me.fFileOperationType,
			 /**PDF预览0下载按钮显示，1不显示*/
			DOWNLOAD:me.DOWNLOAD,
			/**PDF预览0打印按钮显示，1不显示*/
			PRINT:me.PRINT,
			BUILTIN:me.BUILTIN,
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
				}
			}
		}).show();
	}
});