/**
 * 借款单基本列表
 * @author longfc
 * @version 2016-11-09
 */
Ext.define('Shell.class.oa.ploanbill.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.TextSearchTrigger'
	],

	title: '借款单',
	width: 1200,
	height: 800,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPLoanBillNoPlanishByHQL',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePLoanBillByField',
	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/SystemCommonService.svc/GetClassDicList',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'DataAddTime',
		direction: 'DESC'
	}, {
		property: 'Status',
		direction: 'ASC'
	}, {
		property: 'ComponeID',
		direction: 'ASC'
	}, {
		property: 'DeptID',
		direction: 'ASC'
	}, {
		property: 'ApplyManID',
		direction: 'ASC'
	}],
	/**删除标志字段*/
	DelField: 'delState',
	multiSelect: true,
	defaultWhere: '',
	hasAdd: false,
	hasShow: true,
	hasEdit: false,
	hasPrint: false,
	hasRefresh: true,
	hasDel: false,
	checkOne: true,
	autoHeight: false,

	/**是否隐藏工具栏查询条件*/
	hiddenbuttonsToolbar: false,
	isSearchChildNode: false,
	autoScroll: true,
	/**默认每页数量*/
	defaultPageSize: 20,
	/**默认加载数据*/
	defaultLoad: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否显示被禁用的数据*/
	isShowDel: false,
	hiddenIsUse: true,
	StatusList: null,
	StatusEnum: null,
	StatusFColorEnum: null,
	StatusBGColorEnum: null,
	/**是否显示撤回操作列*/
	hiddenRetract: true,
	/*借款状态选择不显示暂存*/
	removeApply: true,
	/**默认员工赋值*/
	hasDefaultUser: true,
	/**默认员工ID*/
	defaultUserID: null,
	/**默认员工名称*/
	defaultUserName: null,
	defaultUserType: '',
	/**状态选择项的默认值*/
	defaultStatusValue: "",
	/*日期范围类型默认值**/
	defaultDateTypeValue: '',
	/*是否显示启/禁用列**/
	isShowIsUseColumn: false,
	/*人员选择是否移除申请人**/
	isRemoveApplyManID: false,
	DateTypeList: [
		["", "不过滤"],
		["ApplyDate", "申请时间"],
		["ReviewDate", "一审时间"],
		["TwoReviewDate", "核对时间"],
		["ThreeReviewDate", "特殊审批时间"],
		["FourReviewDate", "借款复核时间"],
		["PayDate", "出纳检查打款时间"],
		["ReceiveDate", "领款时间"]
	],

	SearchTypeList: [
		["", "不过滤"],
		["ApplyManID", "申请人"],
		["ReviewManID", "一审人"],
		["TwoReviewManID", "核对人"],
		["ThreeReviewManID", "特殊审批人"],
		["FourReviewManID", "借款复核人"],
		["PayManID", "出纳检查打款人"],
		["ReceiveManID", "领款人"]
	],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(me.hiddenbuttonsToolbar) {
			buttonsToolbar.setVisible(false);
		}
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.onItemDblClick(grid, record, item, index, e, eOpts);
			}
		});
		Ext.QuickTips.init();
		Ext.override(Ext.ToolTip, {
			maxWidth: 680
		});
	},
	onPrintClick: function() {

	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {

	},
	initComponent: function() {
		var me = this;
		me.initDate();
		if(!me.checkOne) {
			me.multiSelect = true;
			me.selType = 'checkboxmodel';
		}

		if(me.hasDefaultUser) {
			//默认员工ID
			me.defaultUserID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			//默认员工名称
			me.defaultUserName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		}
		me.getStatusListData();
		//创建数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//初始化默认条件
		me.initDefaultWhere();
		me.addEvents('onAddClick', me);
		me.addEvents('onEditClick', me);
		me.addEvents('onShowTabPanelClick', me);
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];

		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempStatus = me.StatusList;
		return tempStatus;
	},
	/**人员查询选择项过滤*/
	removeSomeSearchTypeList: function() {
		var me = this;
		var tempList = me.SearchTypeList;
		return tempList;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '借款类型/领款方式/申请人',
			isLike: true,
			itemId: 'search',
			fields: ['LoanBillTypeName', 'ReceiveTypeName', 'ApplyMan']
		};
		var items = me.buttonToolbarItems || [];
		if(me.hasRefresh) items.unshift('refresh');
		if(me.hasAdd) items.push('add');
		if(me.hasEdit) {
			items.push('edit', '-');
		} else {
			items.push('-');
		}
		items.push({
			boxLabel: '显示禁用',
			itemId: 'checkIsUse',
			checked: me.isShowDel,
			value: me.isShowDel,
			hidden: me.hiddenIsUse,
			inputValue: false,
			xtype: 'checkbox',
			style: {
				marginRight: '4px'
			},
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue == true) {
						me.isShowDel = true;
					} else {
						me.isShowDel = false;
					}
					me.onSearch();
				}
			}
		});
		var tempStatus = me.StatusList;
		tempStatus = me.removeSomeStatusList();
		items.push({
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: tempStatus,
			value: me.defaultStatusValue,
			width: 160,
			labelWidth: 65,
			fieldLabel: '状态选择',
			tooltip: '状态选择',
			emptyText: '状态选择',
			itemId: 'selectStatus',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		if(me.hasPrint) {
			items.push('-', {
				xtype: 'button',
				itemId: 'btnPrint',
				iconCls: 'button-print hand',
				text: "预览及打印",
				tooltip: '预览及打印借款单',
				handler: function() {
					me.onPrintClick();
				}
			});
		}
		return items;
	},
	/**初始化查询栏内容*/
	createDefaultButtonToolbarItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '借款类型Id',
			hidden: true,
			xtype: 'textfield',
			itemId: 'LoanBillTypeID',
			name: 'LoanBillTypeID'
		}, {
			width: 165,
			labelWidth: 65,
			fieldLabel: '借款类型',
			emptyText: '借款类型',
			name: 'LoanBillTypeName',
			itemId: 'LoanBillTypeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.pdict.CheckGrid',
			classConfig: {
				title: '借款类型选择',
				height: 320,
				defaultLoad: true,
				defaultWhere: "pdict.PDictType.Id=4641331093809308015"
			},
			listeners: {
				check: function(p, record) {
					var toolbarSearch = me.getComponent('toolbarSearch');
					var CName = toolbarSearch.getComponent('LoanBillTypeName');
					var Id = toolbarSearch.getComponent('LoanBillTypeID');
					CName.setValue(record ? record.get('PDict_CName') : '');
					Id.setValue(record ? record.get('PDict_Id') : '');

					p.close();
					me.onSearch();
				}
			}
		});

		items.push({
			fieldLabel: '借款内容类型Id',
			hidden: true,
			xtype: 'textfield',
			itemId: 'LoanBillContentTypeID',
			name: 'LoanBillContentTypeID'
		}, {
			width: 170,
			labelWidth: 65,
			fieldLabel: '内容类型',
			emptyText: '借款内容类型',
			name: 'LoanBillContentTypeName',
			itemId: 'LoanBillContentTypeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.pdict.CheckGrid',
			classConfig: {
				title: '借款内容类型选择',
				height: 380,
				defaultLoad: true,
				defaultWhere: "pdict.PDictType.Id=4808477805327937637"
			},
			listeners: {
				check: function(p, record) {
					var toolbarSearch = me.getComponent('toolbarSearch');
					var CName = toolbarSearch.getComponent('LoanBillContentTypeName');
					var Id = toolbarSearch.getComponent('LoanBillContentTypeID');
					CName.setValue(record ? record.get('PDict_CName') : '');
					Id.setValue(record ? record.get('PDict_Id') : '');
					p.close();
					me.onSearch();
				}
			}
		});
		var tempTypeList = [];
		tempTypeList = me.removeSomeSearchTypeList();
		items.push({
			xtype: 'uxSimpleComboBox',
			width: 165,
			labelWidth: 65,
			fieldLabel: '选择人员',
			itemId: 'UserSearchType',
			tooltip: '按自定义选择项搜索',
			data: tempTypeList,
			/**默认员工类型*/
			value: me.defaultUserType,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != "-1") {
						var toolbarSearch = me.getComponent('toolbarSearch'),
							UserID = toolbarSearch.getComponent('UserID').getValue();
						if(UserID && UserID != "")
							me.onSearch();
					}
				}
			}
		}, {
			fieldLabel: '选择人员Id',
			hidden: true,
			xtype: 'textfield',
			itemId: 'UserID',
			value: me.defaultUserID,
			name: 'UserID'
		}, {
			width: 115,
			labelWidth: 0,
			fieldLabel: '',
			emptyText: '选择人员',
			name: 'UserName',
			itemId: 'UserName',
			xtype: 'uxCheckTrigger',
			value: me.defaultUserName,
			className: 'Shell.class.sysbase.user.CheckApp',
			classConfig: {
				title: '选择人员',
				height: 380,
				defaultLoad: true
			},
			listeners: {
				check: function(p, record) {
					var toolbarSearch = me.getComponent('toolbarSearch');
					var CName = toolbarSearch.getComponent('UserName');
					var Id = toolbarSearch.getComponent('UserID');
					CName.setValue(record ? record.get('HREmployee_CName') : '');
					Id.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				},
				change: function() {
					me.onSearch();
				}
			}
		});
		items.push("-");
		items.push({
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: me.DateTypeList,
			value: me.defaultDateTypeValue,
			width: 175,
			labelWidth: 65,
			fieldLabel: '日期选择',
			emptyText: '日期选择',
			tooltip: '日期选择',
			itemId: 'DateType',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue) me.onSearch();
				}
			}
		}, {
			width: 95,
			labelWidth: 1,
			labelAlign: 'right',
			value: me.defaultBeginDateDate,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue) me.onSearch();
				}
			}
		}, {
			width: 95,
			labelWidth: 1,
			labelAlign: 'right',
			value: me.defaultEndDateDate,
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			width: 60,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				me.onSearch();
			}
		});
		items.push("-");
		var tempTypeList = [];
		tempTypeList = me.removeSomeSearchTypeList();

		var toolbarSearch = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'toolbarSearch',
			items: items
		};
		return toolbarSearch;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		var toolbarSearch = me.getComponent('toolbarSearch');
		if(toolbarSearch) {
			var BeginDate = toolbarSearch.getComponent('BeginDate').getValue();
			var EndDate = toolbarSearch.getComponent('EndDate').getValue();
			var StartDateValue = JcallShell.Date.toString(BeginDate, true);
			var EndDateValue = JcallShell.Date.toString(EndDate, true);
			if(StartDateValue > EndDateValue) {
				JShell.Msg.error('结束日期不能小于开始日期!');
				return;
			}
		}
		me.load(null, true, autoSelect);
	},

	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		var defaultDate = defaultDate = JcallShell.Date.getNextDate(Sysdate, -7);
		me.defaultBeginDateDate = JcallShell.Date.toString(defaultDate, true);
		me.defaultEndDateDate = JcallShell.Date.toString(Sysdate, true);
	},
	/**对外公开,由外面传入列信息*/
	createNewColumns: function() {
		var me = this;
		//创建数据列
		var tempColumns = [];
		return tempColumns;
	},

	createGridColumns: function() {
		var me = this;
		//创建数据列
		var columns = [];
		columns = me.createNewColumns().length > 0 ? me.createNewColumns() : me.createDefaultColumns();
		return columns;
	},

	/**获取借款状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "PLoanBillStatus",
				"classnamespace": "ZhiFang.Entity.ProjectProgressMonitorManage"
			}]
		};
		return params;
	},
	/**获取借款状态信息*/
	getStatusListData: function(callback) {
		var me = this,
			params = {},
			url = JShell.System.Path.getRootUrl(me.classdicSelectUrl);

		params = Ext.encode(me.getParams());
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].PLoanBillStatus.length > 0) {
						me.StatusList = [];
						me.StatusEnum = {};
						me.StatusFColorEnum = {};
						me.StatusBGColorEnum = {};
						var tempArr = [];
						me.StatusList.push(["", '全部', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].PLoanBillStatus, function(obj, index) {
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
							me.StatusList.push(tempArr);

						});
					}
				}
			}
		}, false);
	},
	showQtipValue: function(meta, record) {
		var me = this;
		var qtipMemoValue = record.get("LoanBillMemo");
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>借款说明:</b>" + qtipMemoValue + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>审核意见:</b>" + record.get("ReviewInfo") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>商务核对意见:</b>" + record.get("TwoReviewInfo") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>特殊审批意见:</b>" + record.get("ThreeReviewInfo") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>账务复核意见:</b>" + record.get("FourReviewInfo") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>出纳打款意见:</b>" + record.get("ReceiveManInfo") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>领款人银行备注说明:</b>" + record.get("ReceiveBankInfo") + "</p>";
		if(qtipValue) {
			meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		}
		return meta;
	},
	getMetaStyle: function(index) {
		var me = this;
		var bColor = "";
		if(me.StatusBGColorEnum != null)
			bColor = me.StatusBGColorEnum[index];
		var fColor = "";
		if(me.StatusFColorEnum != null)
			fColor = me.StatusFColorEnum[index];

		var style = 'font-weight:bold;';
		if(bColor) {
			style = style + "background-color:" + bColor + ";";
		}
		if(fColor) {
			style = style + "color:" + fColor + ";";
		}
		return style;
	},
	/**创建数据列*/
	createDefaultColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			text: '主键ID',
			dataIndex: 'Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '特殊审批标记',
			dataIndex: 'IsSpecially',
			hidden: true,
			hideable: false
		}, {
			text: '申请人ID',
			dataIndex: 'ApplyManID',
			hidden: true,
			hideable: false
		}, {
			text: '所属公司',
			dataIndex: 'ComponeName',
			hidden: false,
			width: 185,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '合同',
			dataIndex: 'ContractName',
			hidden: true,
			width: 60,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '借款类型',
			dataIndex: 'LoanBillTypeName',
			width: 85,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '借款金额',
			dataIndex: 'LoanBillAmount',
			hidden: false,
			hidden: true,
			width: 120,
			hideable: false
		}, {
			text: '内容类型',
			dataIndex: 'LoanBillContentTypeName',
			hidden: false,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '所属部门',
			dataIndex: 'DeptName',
			hidden: false,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, me.createRetract(), {
			text: '状态',
			dataIndex: 'Status',
			width: 70,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta) {
				var v = value;
				if(me.StatusEnum != null)
					v = me.StatusEnum[value];
				var bColor = "";
				if(me.StatusBGColorEnum != null)
					bColor = me.StatusBGColorEnum[value];
				var fColor = "";
				if(me.StatusFColorEnum != null)
					fColor = me.StatusFColorEnum[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			text: '填写时间',
			dataIndex: 'DataAddTime',
			width: 130,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		});

		if(me.isShowIsUseColumn) {
			columns.push({
				text: '启用',
				dataIndex: 'IsUse',
				width: 40,
				hideable: false,
				renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
					if(value.toString() == "true" || value.toString() == "1") {
						value = "是";
					} else {
						value = "否";
						meta.style = 'font-weight:bold;color:red';
					}
					return value;
				}
			});
			columns.push(me.createIsUse());
		}
		//查看操作列
		columns.push(me.createShowCcolumn());
		//交流列
		columns.push(me.createInteraction());
		//操作记录查看列
		columns.push(me.createOperation());

		columns.push({
			text: '借款单号',
			dataIndex: 'LoanBillNo',
			hidden: false,
			width: 80,
			hidden: true,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '申请人',
			dataIndex: 'ApplyMan',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = me.getMetaStyle(2);
				}
				return value;
			}
		}, {
			text: '申请时间',
			dataIndex: 'ApplyDate',
			width: 130,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = me.getMetaStyle(2);
				}
				return value;
			}
		}, {
			text: '借款说明',
			dataIndex: 'LoanBillMemo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '上级领导',
			dataIndex: 'ReviewMan',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = (status == "4" ? me.getMetaStyle(status) : me.getMetaStyle(3));
				}
				return value;
			}
		}, {
			text: '审核时间',
			dataIndex: 'ReviewDate',
			width: 130,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = (status == "4" ? me.getMetaStyle(status) : me.getMetaStyle(3));
				}
				return value;
			}
		}, {
			text: '审核意见',
			dataIndex: 'ReviewInfo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '行政助理',
			dataIndex: 'TwoReviewMan',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = (status == "6" ? me.getMetaStyle(status) : me.getMetaStyle(5));
				}
				return value;
			}
		}, {
			text: '行政核对时间',
			dataIndex: 'TwoReviewDate',
			width: 130,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = (status == "6" ? me.getMetaStyle(status) : me.getMetaStyle(5));
				}
				return value;
			}
		}, {
			text: '行政核对意见',
			dataIndex: 'TwoReviewInfo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '总经理',
			dataIndex: 'ThreeReviewMan',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = (status == "8" ? me.getMetaStyle(status) : me.getMetaStyle(7));
				}
				return value;
			}
		}, {
			text: '特殊审批时间',
			dataIndex: 'ThreeReviewDate',
			width: 130,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = (status == "8" ? me.getMetaStyle(status) : me.getMetaStyle(7));
				}
				return value;
			}
		}, {
			text: '特殊审批意见',
			dataIndex: 'ThreeReviewInfo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '账务人员',
			dataIndex: 'FourReviewMan',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = (status == "10" ? me.getMetaStyle(status) : me.getMetaStyle(9));
				}
				return value;
			}
		}, {
			text: '账务复核时间',
			dataIndex: 'FourReviewDate',
			width: 130,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					var status = record.get("Status").toString();
					meta.style = (status == "10" ? me.getMetaStyle(status) : me.getMetaStyle(9));
				}
				return value;
			}
		}, {
			text: '账务复核意见',
			dataIndex: 'FourReviewInfo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '出纳打款人',
			dataIndex: 'PayManName',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					meta.style = me.getMetaStyle(11);
				}
				return value;
			}
		}, {
			text: '出纳打款时间',
			dataIndex: 'PayDate',
			width: 130,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					meta.style = me.getMetaStyle(11);
				}
				return value;
			}
		}, {
			text: '出纳打款意见',
			dataIndex: 'PayDateInfo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '领款人',
			dataIndex: 'ReceiveManName',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					meta.style = me.getMetaStyle(12);
				}
				return value;
			}
		}, {
			text: '领款时间',
			dataIndex: 'ReceiveDate',
			width: 130,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				if(value != "") {
					meta.style = me.getMetaStyle(12);
				}
				return value;
			}
		}, {
			text: '领款确认意见',
			dataIndex: 'ReceiveManInfo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '领款方式',
			dataIndex: 'ReceiveTypeName',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '概要说明',
			dataIndex: 'LoanBillMemo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '领款人银行备注说明',
			dataIndex: 'ReceiveBankInfo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		});
		return columns;
	},
	/*创建交流列**/
	createShowCcolumn: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '查看',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openShowTabPanel(rec);
				}
			}]
		};
	},
	/*打开查看应用*/
	showPGMProgramById: function() {
		var me = this;

	},
	/*创建借款单撤回列**/
	createRetract: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '撤回',
			align: 'center',
			width: 40,
			hidden: me.hiddenRetract,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var reviewDate = record.get('ReviewDate');
					var status = record.get('Status').toString();
					if((reviewDate == null || reviewDate == "") && status == "2") {
						meta.tdAttr = 'data-qtip="<b>撤回</b>"';
						//meta.style = 'background-color:green;';
						return 'button-edit hand';
					} else {
						//meta.tdAttr = 'data-qtip="<b></b>"';
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onRetractClick(rec);
				}
			}]
		};
	},
	/**@overwrite 撤回按钮点击处理方法*/
	onRetractClick: function(rec) {
		var me = this;
		var id = rec.get("Id");
		var entity = {
			Id: id,
			Status: 1,
			OperationMemo: "申请撤回"
		};

		var fields = ['Id', 'Status'];
		var params = {
			entity: entity,
			fields: fields.join(',')
		};
		var params = Ext.JSON.encode(params);
		me.showMask("数据提交保存中...");
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				rec.set("Status", "1");
				JShell.Msg.alert("申请撤回操作成功", null, 800);
			} else {
				JShell.Msg.error("申请撤回操作失败!<br />" + data.msg);
			}
		});
	},
	/*创建启用或禁用列**/
	createIsUse: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '启/禁',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var reviewDate = record.get('ReviewDate');
					var status = record.get('Status').toString();
					var isShow = true;
					switch(status) {
						case "1": //
							isShow = true;
							break;
							//						case "2": //申请状态,当部门审核人没有进行审核时可以进行撤回操作
							//							if(reviewDate == null || reviewDate == "") {
							//								isShow = true;
							//							} else {
							//								isShow = false;
							//							}
							break;
						case "4": //一审退回
							isShow = true;
							break;
						default:
							isShow = false;
							break;
					}
					if(isShow == true) {
						if(record.get('IsUse') == "true") {
							meta.tdAttr = 'data-qtip="<b>禁用</b>"';
							//meta.style = 'background-color:green;';
							return 'button-edit hand';
						} else {
							meta.tdAttr = 'data-qtip="<b>启用</b>"';
							//meta.style = 'background-color:red;';
							return 'button-edit hand';
						}
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isUse = rec.get('IsUse');
					var msg = isUse.toString() == "true" ? "是否禁用该借款单" : "是否启用借款单";
					var newIsUse = isUse.toString() == "true" ? 0 : 1;
					Ext.MessageBox.show({
						title: '操作确认消息',
						msg: msg,
						width: 300,
						icon: Ext.MessageBox.QUESTION,
						buttons: Ext.MessageBox.OKCANCEL,
						fn: function(btn) {
							if(btn == 'ok') {
								me.UpdateIsUseByStrIds(rec, newIsUse);
							}
						}
					});
				}
			}]
		};
	},
	/*程序启用或禁用操作**/
	UpdateIsUseByStrIds: function(rec, newIsUse) {
		var me = this;
		var id = rec.get(me.PKField);
		var status = rec.get('Status').toString();
		var isUse = rec.get('IsUse');

		url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var msgInfo = "";
		if(isUse.toLowerCase().toString() == "true" || isUse.toString() == "1") {
			msgInfo = "借款单禁用";
		} else {
			msgInfo = "借款单启用";
		}
		var entity = {
			Id: id,
			Status: rec.get('Status'),
			IsUse: newIsUse,
			OperationMemo: msgInfo
		};
		var params = {
			entity: entity,
			fields: "Id,Status,IsUse"
		};
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				rec.set("IsUse", newIsUse);
				//me.getView().refresh();
				rec.commit();
				JShell.Msg.alert(msgInfo + "成功", null, 1000);
			} else {
				var msg = data.msg;
				msgInfo = msgInfo + '失败';
				JShell.Msg.error(msgInfo + "<br />" + data.msg);
			}
		});
	},
	/*创建操作记录列**/
	createOperation: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '操作记录',
			align: 'center',
			width: 55,
			hidden: false,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openOperationGrid(rec);
				}
			}]
		};
	},
	/**打开操作记录列表*/
	openOperationGrid: function(rec) {
		var me = this;
		var id = rec.get(me.PKField);
		var config = {
			showSuccessInfo: false,
			resizable: false,
			hasButtontoolbar: false,
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			PK: id
		};
		var win = JShell.Win.open('Shell.class.oa.sc.operation.Grid', config).show();
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
					me.showInteractionById(rec);
				}
			}]
		};
	},

	/**根据ID查看交流*/
	showInteractionById: function(record) {
		var me = this;
		var id = record.get('Id');
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.sysbase.scinteraction.App', {
			PK: id,
			height: height,
			width: maxWidth
		}).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			toolbarSearch = me.getComponent('toolbarSearch');
		var selectStatus = "",
			LoanBillTypeID = "",
			LoanBillContentTypeID = "",
			UserID = "",
			UserSearchType = "",
			DateType = "",
			BeginDate = "",
			EndDate = "",
			search = null;

		if(buttonsToolbar) {
			selectStatus = buttonsToolbar.getComponent('selectStatus').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
		}
		if(toolbarSearch) {
			LoanBillTypeID = toolbarSearch.getComponent('LoanBillTypeID').getValue();
			LoanBillContentTypeID = toolbarSearch.getComponent('LoanBillContentTypeID').getValue();

			UserSearchType = toolbarSearch.getComponent('UserSearchType').getValue();
			UserID = toolbarSearch.getComponent('UserID').getValue();
			DateType = toolbarSearch.getComponent('DateType').getValue();
			BeginDate = toolbarSearch.getComponent('BeginDate').getValue();
			EndDate = toolbarSearch.getComponent('EndDate').getValue();
		}
		var params = [];

		//借款类型
		if(LoanBillTypeID) {
			params.push("LoanBillTypeID=" + LoanBillTypeID + "");
		}
		if(LoanBillContentTypeID) {
			params.push("LoanBillContentTypeID=" + LoanBillContentTypeID + "");
		}
		if(selectStatus) {
			params.push("Status=" + selectStatus + "");
		}

		if(DateType == "") {
			DateType = "DataAddTime";
		}

		//时间
		if(DateType) {
			if(BeginDate) {
				params.push("" + DateType + ">='" + JShell.Date.toString(BeginDate, true) + "'");
			}
			if(EndDate) {
				params.push("" + DateType + "<'" + JShell.Date.toString(EndDate, true) + " 23:59:59'");
			}
		}

		//自定义员工选择搜索
		if(UserSearchType && UserSearchType != "-1") {
			if(UserID && UserID != "") {
				params.push(UserSearchType + "=" + UserID + "");
			}
		}
		//是否显示被禁用的数据,如果不显示
		if(me.isShowDel == false) {
			params.push("IsUse=1");
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
			params.push(me.defaultWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			params.push(me.externalWhere);
		}
		return me.callParent(arguments);
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},

	/**初始化默认条件*/
	initDefaultWhere: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') ';
		}

	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onEditClick', me, records[0]);
	},
	onShowClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onShowClick', me, records[0]);
	},
	onDelClick: function() {
		var me = this;
		me.fireEvent('onDelClick', me);
	},
	/**查看应用*/
	openShowTabPanel: function(record) {
		var me = this;

		var me = this;
		var ApplyManID = "";
		var id = "";
		if(record != null) {
			id = record.get('Id');
			ApplyManID = record.get('ApplyManID');
		}
		var maxWidth = document.body.clientWidth * 0.84;
		var minWidth = (maxWidth < 1147 ? 1147 : maxWidth);
		var height = document.body.clientHeight * 0.88;
		height = (height < 560 ? 560 : height);
		var config = {
			showSuccessInfo: false,
			PK: id,
			hasOperation: true,
			height: height,
			minWidth: minWidth,
			width: maxWidth,
			zindex: 10,
			zIndex: 10,
			resizable: false,
			title: "借款单信息",
			formtype: 'edit',
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			ApplyManID: ApplyManID,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		JShell.Win.open('Shell.class.oa.ploanbill.show.ShowTabPanel', config).show();

	}
});