/**
 * 用户订单基本列表
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.userorder.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.TextSearchTrigger'
	],

	title: '用户订单',
	width: 1200,
	height: 800,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderFormNoPlanishByHQL',
	/**修改服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserOrderFormByField',
	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/ServerWCF/SystemCommonService.svc/GetClassDicList',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'DataAddTime',
		direction: 'DESC'
	}, {
		property: 'Status',
		direction: 'ASC'
	}],
	/**删除标志字段*/
	DelField: 'delState',
	multiSelect: true,
	defaultWhere: '',
	hasAdd: false,
	hasShow: true,
	hasEdit: false,

	hasRefresh: true,
	hasDel: false,
	/*退款申请*/
	hasRefund: true,
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
	StatusList: null,
	StatusEnum: null,
	StatusFColorEnum: null,
	StatusBGColorEnum: null,
	/**是否显示撤回操作列*/
	hiddenRetract: false,
	/*用户订单状态选择不显示暂存*/
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

	/*人员选择是否移除申请人**/
	isRemoveApplyManID: false,
	DateTypeList: [
		["", "不过滤"],
		["PayTime", "缴费时间"],
		["RefundApplyTime", "申请时间"],
		["RefundOneReviewStartTime", "处理开始时间"],
		["RefundOneReviewFinishTime", "处理完成时间"],
		["RefundTwoReviewStartTime", "审批开始时间"],
		["RefundTwoReviewFinishTime", "审批完成时间"],
		["RefundThreeReviewStartTime", "发放开始时间"],
		["RefundThreeReviewFinishTime", "发放完成时间"]
	],
	SearchTypeList: [
		["", "不过滤"],
		["RefundApplyManID", "申请人"],
		["RefundOneReviewManID", "退款处理人"],
		["RefundTwoReviewManID", "退款审批人"],
		["RefundThreeReviewManID", "退款发放人"]
	],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
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
	onRefundClick: function() {

	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {

	},
	initComponent: function() {
		var me = this;
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
			emptyText: '医生姓名/用户姓名/申请人',
			isLike: true,
			itemId: 'search',
			fields: ['DoctorName', 'UserName', 'RefundApplyManName']
		};
		var items = me.buttonToolbarItems || [];
		if(me.hasRefresh) items.unshift('refresh');
		if(me.hasAdd) items.push('add');
		if(me.hasRefund) items.push({
			xtype: 'button',
			itemId: 'btnRefund',
			iconCls: 'button-edit hand',
			text: "退款申请",
			tooltip: '退款申请',
			handler: function() {
				me.onRefundClick();
			}
		});

		if(me.hasEdit) {
			items.push('edit', '-');
		} else {
			items.push('-');
		}
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
		return items;
	},
	/**初始化查询栏内容*/
	createDefaultButtonToolbarItems: function() {
		var me = this,
			items = [];
		items.push("-");
		items.push({
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: me.DateTypeList,
			value: me.defaultDateTypeValue,
			width: 170,
			labelWidth: 60,
			fieldLabel: '日期选择',
			emptyText: '日期选择',
			tooltip: '日期选择',
			itemId: 'DateType',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			width: 95,
			labelWidth: 1,
			labelAlign: 'right',
			//fieldLabel: '',
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			width: 95,
			labelWidth: 1,
			labelAlign: 'right',
			//fieldLabel: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push("-");
		var tempTypeList = [];
		tempTypeList = me.removeSomeSearchTypeList();

		items.push({
			xtype: 'uxSimpleComboBox',
			width: 100,
			labelWidth: 0,
			fieldLabel: '',
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
			width: 260,
			labelWidth: 65,
			fieldLabel: '选择人员',
			emptyText: '选择人员',
			name: 'UserName',
			itemId: 'UserName',
			xtype: 'uxCheckTrigger',
			value: me.defaultUserName,
			className: 'Shell.class.sysbase.user.CheckApp',
			classConfig: {
				title: '选择人员',
				height: 420,
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
		var toolbarSearch = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'toolbarSearch',
			items: items
		};
		return toolbarSearch;
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
	/**获取用户订单状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "UserOrderFormStatus",
				"classnamespace": "ZhiFang.WeiXin.Entity"
			}]
		};
		return params;
	},
	/**获取用户订单状态信息*/
	getStatusListData: function(callback) {
		var me = this,
			params = {},
			url = JShell.System.Path.getRootUrl(me.classdicSelectUrl);

		params = Ext.encode(me.getParams());
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].UserOrderFormStatus.length > 0) {
						me.StatusList = [];
						me.StatusEnum = {};
						me.StatusFColorEnum = {};
						me.StatusBGColorEnum = {};
						var tempArr = [];
						me.StatusList.push(["", '全部', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].UserOrderFormStatus, function(obj, index) {
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
		var qtipValue = "";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>退费原因:</b>" + record.get("RefundReason") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>退款处理说明:</b>" + record.get("RefundOneReviewReason") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>退款审批意见:</b>" + record.get("RefundTwoReviewReason") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>退款发放说明:</b>" + record.get("RefundThreeReviewReason") + "</p>";

		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>银行转账单号:</b>" + record.get("BankTransFormCode") + "</p>";
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
			text: '申请人ID',
			dataIndex: 'RefundApplyManID',
			hidden: true,
			hideable: false
		}, {
			text: '主键ID',
			dataIndex: 'Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '用户姓名',
			dataIndex: 'UserName',
			width: 85,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '医生姓名',
			dataIndex: 'DoctorName',
			hidden: false,
			width: 85,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
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
		});

		//查看操作列
		//columns.push(me.createShowCcolumn());
		//操作记录查看列
		//columns.push(me.createOperation());
		columns.push({
			text: '实际金额',
			dataIndex: 'Price',
			hidden: false,
			hidden: true,
			width: 80,
			hideable: false
		}, {
			text: '退费金额',
			dataIndex: 'RefundPrice',
			hidden: false,
			hidden: true,
			width: 80,
			hideable: false
		}, {
			text: '折扣率',
			dataIndex: 'Discount',
			hidden: false,
			hidden: true,
			width: 80,
			hideable: false
		}, {
			text: '折扣价格',
			dataIndex: 'DiscountPrice',
			hidden: false,
			hidden: true,
			width: 80,
			hideable: false
		}, {
			text: '缴费时间',
			dataIndex: 'PayTime',
			width: 130,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '退费单编号',
			dataIndex: 'MRefundFormCode',
			hidden: false,
			width: 80,
			hidden: true,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '用户订单编号',
			dataIndex: 'UOFCode',
			hidden: false,
			width: 80,
			hidden: true,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '消费单编号',
			dataIndex: 'OSUserConsumerFormCode',
			hidden: false,
			width: 80,
			hidden: true,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '消费码',
			dataIndex: 'PayCode',
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
			dataIndex: 'RefundApplyManName',
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
			dataIndex: 'RefundApplyTime',
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
			text: '退款处理开始时间',
			dataIndex: 'RefundOneReviewStartTime',
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
			text: '退款处理完成时间',
			dataIndex: 'RefundOneReviewFinishTime',
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
			text: '退款处理说明',
			dataIndex: 'RefundOneReviewReason',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '退款审批人',
			dataIndex: 'RefundTwoReviewManName',
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
			text: '退款审批开始时间',
			dataIndex: 'RefundTwoReviewStartTime',
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
			text: '退款审批完成时间',
			dataIndex: 'RefundTwoReviewFinishTime',
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
			text: '退款审批说明',
			dataIndex: 'RefundTwoReviewReason',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '退款发放人',
			dataIndex: 'RefundThreeReviewManName',
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
			text: '退款发放开始时间',
			dataIndex: 'RefundThreeReviewStartTime',
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
			text: '退款发放完成时间',
			dataIndex: 'RefundThreeReviewFinishTime',
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
			text: '退款发放说明',
			dataIndex: 'RefundThreeReviewReason',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '退费原因',
			dataIndex: 'RefundReason',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '银行转账单号',
			dataIndex: 'BankTransFormCode',
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
	/**打开用户订单单操作记录列表*/
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
		var win = JShell.Win.open('Shell.class.sysbase.scoperation.SCOperation', config).show();
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			toolbarSearch = me.getComponent('toolbarSearch');
		var selectStatus = "",
			UserID = "",
			UserSearchType = "",
			DateType = "",
			BeginDate = "",
			EndDate = "",
			UserID = "",
			search = null;

		if(buttonsToolbar) {
			selectStatus = buttonsToolbar.getComponent('selectStatus').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
		}
		if(toolbarSearch) {

			UserSearchType = toolbarSearch.getComponent('UserSearchType').getValue();
			UserID = toolbarSearch.getComponent('UserID').getValue();
			DateType = toolbarSearch.getComponent('DateType').getValue();
			BeginDate = toolbarSearch.getComponent('BeginDate').getValue();
			EndDate = toolbarSearch.getComponent('EndDate').getValue();
		}
		var params = [];

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
				params.push("" + DateType + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate), true) + "'");
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
		var RefundApplyManID = "";
		var id = "";
		if(record != null) {
			id = record.get('Id');
			RefundApplyManID = record.get('RefundApplyManID');
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
			title: "用户订单信息",
			formtype: 'edit',
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			RefundApplyManID: RefundApplyManID,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		JShell.Win.open('Shell.class.weixin.ordersys.userorder.show.ShowTabPanel', config).show();
	}
});