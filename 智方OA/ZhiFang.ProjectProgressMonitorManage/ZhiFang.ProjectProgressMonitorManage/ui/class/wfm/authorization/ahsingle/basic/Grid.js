/**
 * 单站点授权
 * @author longfc
 * @version 2016-12-10
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '单站点授权基础列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHSingleLicenceByHQL',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHSingleLicenceByField',
	/**删除数据服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelAHSingleLicence',

	/**默认加载*/
	defaultLoad: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**状态默认值*/
	defaultStatusValue: '',
	/**状态默认值*/
	defaultTypeValue: '',
	/**隐藏授权类型*/
	hiddenLicenceTypeId: false,
	hasSearchoolbar: true,
	/**是否有日期范围*/
	hasDates: true,

	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ApplyDataTime',
		direction: 'DESC'
	}, {
		property: 'PClientID',
		direction: 'ASC'
	}, {
		property: 'ApplyID',
		direction: 'ASC'
	}],
	//是否开始加载授权类型字典信息
	isLoadLicenceType: false,
	//是否开始加载授权状态字典信息
	isLoadStatus: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		Ext.QuickTips.init();
		Ext.override(Ext.ToolTip, {
			maxWidth: 680
		});
		me.initListeners();
		me.loadLicenceTypeData();
		me.loadStatusData();
		me.onSearch();
	},
	initComponent: function() {
		var me = this;
		me.initDate();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);

		me.ininClassDict();
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var sysdate = JcallShell.System.Date.getDate();
		var defaultDate = defaultDate = JcallShell.Date.getNextDate(sysdate, 14);
		me.defaultBeginDateDate = JcallShell.Date.toString(sysdate, true);
		me.defaultEndDateDate = JcallShell.Date.toString(defaultDate, true);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];

		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		if (me.hasSearchoolbar) items.push(me.createSearchoolbar());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.push('refresh', '-', {
			width: 145,
			labelWidth: 65,
			labelAlign: 'right',
			hasStyle: true,
			hidden: me.hiddenLicenceTypeId,
			xtype: 'uxSimpleComboBox',
			itemId: 'LicenceTypeId',
			fieldLabel: '授权类型',
			value: me.defaultTypeValue
		});
		buttonToolbarItems.push({
			width: 145,
			labelWidth: 35,
			labelAlign: 'right',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'CheckStatus',
			fieldLabel: '状态',
			value: me.defaultStatusValue
		});

		//查询框信息
		me.searchInfo = {
			width: 200,
			itemId: 'search',
			emptyText: '用户名称/授权号(SQH)',
			isLike: true,
			fields: ['PClientName', 'SQH']
		};
		buttonToolbarItems.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**初始化查询栏内容*/
	createSearchoolbar: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '用户',
			labelWidth: 35,
			emptyText: '用户选择',
			name: 'PClientName',
			itemId: 'PClientName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.wfm.client.CheckGrid',
			width: 205,
			classConfig: {
				height: 450,
				title: '用户选择'
			}
		}, {
			fieldLabel: '程序',
			labelWidth: 35,
			emptyText: '程序选择',
			name: 'ProgramName',
			itemId: 'ProgramName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.wfm.authorization.program.CheckApp',
			width: 205,
			classConfig: {
				title: '程序选择'
			}
		}, {
			fieldLabel: '仪器',
			labelWidth: 35,
			emptyText: '仪器选择',
			name: 'EquipName',
			itemId: 'EquipName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.wfm.authorization.equip.CheckApp',
			width: 205,
			classConfig: {
				title: '仪器选择'
			}
		}, {
			fieldLabel: '用户',
			emptyText: '用户',
			name: 'PClientID',
			itemId: 'PClientID',
			hidden: true,
			xtype: 'textfield'
		}, {
			fieldLabel: '程序',
			emptyText: '程序',
			name: 'ProgramID',
			itemId: 'ProgramID',
			hidden: true,
			xtype: 'textfield'
		}, {
			fieldLabel: '仪器',
			emptyText: '仪器',
			name: 'EquipID',
			itemId: 'EquipID',
			hidden: true,
			xtype: 'textfield'
		});

		items.push({
			boxLabel: '按授权截止日期',
			name: 'hasLicenceDate',
			itemId: "hasLicenceDate",
			xtype: 'checkbox',
			inputValue: true,
			checked: true,
			listeners: {
				change: function(field, newValue, oldValue, eOpts) {
					me.onHasLicenceDate(newValue);
				}
			}
		}, {
			width: 95,
			fieldLabel: '',
			labelWidth: 0,
			labelAlign: 'right',
			value: me.defaultBeginDateDate,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			listeners: {
				change: function(field, newValue, oldValue, eOpts) {
					var isValid = field.isValid();
					if (isValid) me.onLoadData();
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
				change: function(field, newValue, oldValue, eOpts) {
					var isValid = field.isValid();
					if (isValid) me.onLoadData();
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

		var toolbarSearch = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'toolbarSearch',
			items: items
		};
		return toolbarSearch;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '用户名称',
			dataIndex: 'PClientName',
			width: 120,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '授权名称',
			dataIndex: 'LicenceClientName',
			width: 120,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '客户服务编号',
			dataIndex: 'LicenceCode',
			width: 100,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '授权程序',
			dataIndex: 'ProgramName',
			width: 120,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '授权仪器',
			dataIndex: 'EquipName',
			width: 120,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '流程状态',
			dataIndex: 'Status',
			width: 95,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				var v = value || '';
				if (v) {
					var info = JShell.System.ClassDict.getClassInfoById('LicenceStatus', v);
					if (info) {
						v = info.Name;
						meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
					}
				}
				return v;
			}
		}, {
			text: '授权类型',
			dataIndex: 'LicenceTypeId',
			width: 60,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				var v = value || '';
				if (v) {
					var info = JShell.System.ClassDict.getClassInfoById('LicenceType', v);
					if (info) {
						v = info.Name;
						meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
					}
				}
				return v;
			}
		}, {
			text: '有效期状态',
			dataIndex: 'LicenceStatusId',
			width: 85,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				var EndDate = record.get('EndDate');
				var sysdate = JShell.System.Date.getDate();
				var CalcDays = 0;
				if (EndDate && sysdate) CalcDays = me.getCalcDays(JShell.Date.toString(EndDate, true), JShell.Date.toString(
					sysdate, true));
				var BGColor = "",
					StatusName = "正常";
				if (!record.get('LicenceTypeId')) StatusName = "";
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
				if (info) {
					if ((record.get('LicenceTypeId') != info.Id) && record.get('LicenceTypeId')) {
						if (CalcDays < 0) {
							BGColor = "#DCDCDC";
							StatusName = "已过期";
						}
						if (CalcDays <= 30 && CalcDays > 0) {
							BGColor = "#FFA500";
							StatusName = "一个月内过期";
						}
						if (CalcDays <= 7 && CalcDays >= 0) {
							BGColor = "red";
							StatusName = "即将过期";
						}
					}
				}
				if (BGColor != "")
					meta.style = 'background-color:' + BGColor + ';color:#ffffff';
				return StatusName;
			}
		}, {
			text: '有效期状态名称',
			dataIndex: 'LicenceStatusName',
			width: 90,
			hidden: true,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}];
		//交流列
		columns.push(me.createInteraction());
		//查看操作列
		columns.push(me.createShowCcolumn());
		//操作记录查看列
		columns.push(me.createOperation());

		columns.push({
			text: '物理Mac地址',
			dataIndex: 'MacAddress',
			width: 145,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '授权号(SQH)',
			dataIndex: 'SQH',
			width: 80,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '授权Key',
			dataIndex: 'LicenceKey',
			width: 195,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '授权开始日期',
			dataIndex: 'StartDate',
			width: 85,
			sortable: false,
			isDate: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '授权截止日期',
			dataIndex: 'EndDate',
			width: 85,
			sortable: false, //isDate: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
				if (info) {
					if (record.get('LicenceTypeId') == info.Id) {
						value = '永久';
					} else {
						value = Ext.util.Format.date(value, 'Y-m-d');
					}
				}
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '计划收款时间',
			dataIndex: 'PlannReceiptDate',
			width: 85,
			sortable: false,
			isDate: true,
			hasTime: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '授权时间',
			dataIndex: 'GenDateTime',
			width: 135,
			sortable: false,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '申请人',
			dataIndex: 'ApplyName',
			width: 70,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '申请时间',
			dataIndex: 'ApplyDataTime',
			width: 135,
			sortable: false,
			defaultRenderer: true,
			isDate: true,
			hasTime: true
		}, {
			text: '审核人',
			dataIndex: 'OneAuditName',
			width: 70,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '审核时间',
			dataIndex: 'OneAuditDataTime',
			width: 135,
			sortable: false,
			defaultRenderer: true,
			isDate: true,
			hasTime: true
		}, {
			text: '审批人',
			dataIndex: 'TwoAuditName',
			width: 70,
			sortable: false,
			defaultRenderer: true
		}, {
			text: 'ID',
			dataIndex: 'Id',
			hidden: true,
			isKey: true,
			hideable: false
		}, {
			text: '客户ID',
			dataIndex: 'PClientID',
			hidden: true,
			hideable: false
		}, {
			text: '审批时间',
			dataIndex: 'TwoAuditDataTime',
			width: 135,
			sortable: false,
			defaultRenderer: true,
			isDate: true,
			hasTime: true
		});

		return columns;
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
					var id = rec.get(me.PKField);
					me.openShowForm(rec);
				}
			}]
		};
	},
	/**根据ID查看交流*/
	showInteractionById: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.sysbase.scinteraction.App', {
			PK: id,
			height: height,
			width: maxWidth
		}).show();
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
			PK: id
		};
		var win = JShell.Win.open('Shell.class.wfm.authorization.basic.SCOperation', config).show();
	},
	showQtipValue: function(value, meta, record, rowIndex, colIndex, store, view) {
		var me = this;
		var PClientName = record.get("PClientName");
		var ProgramName = record.get("ProgramName");
		var EquipName = record.get("EquipName");
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>用户名称:</b>" + PClientName + "</p>";
		if (ProgramName != "" && ProgramName != undefined && ProgramName != null)
			qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>程序名称:</b>" + ProgramName +
			"</p>";
		if (EquipName != "" && EquipName != undefined && EquipName != null)
			qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>仪器名称:</b>" + EquipName +
			"</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>授权号:</b>" + record.get(
			"SQH") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>申请时间:</b>" + record.get(
			"ApplyDataTime") + "</p>";

		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>授权时间:</b>" + record.get(
			"GenDateTime") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>授权Key:</b>" + record.get(
			"LicenceKey") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>物理Mac地址:</b>" + record.get(
			"MacAddress") + "</p>";

		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>审核人:</b>" + record.get(
			"OneAuditName") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>审核时间:</b>" + record.get(
			"OneAuditDataTime") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>审批人:</b>" + record.get(
			"TwoAuditName") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>审批时间:</b>" + record.get(
			"TwoAuditDataTime") + "</p>";

		if (qtipValue) {
			meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		}
		return meta;
	},
	/**截止日期是否勾选上*/
	onHasLicenceDate: function(newValue) {
		var me = this;
		var buttonsToolbar = me.getComponent('toolbarSearch');
		var beginDate = buttonsToolbar.getComponent('BeginDate');
		var endDate = buttonsToolbar.getComponent('EndDate');
		if (newValue == false) {
			beginDate.setValue("");
			endDate.setValue("");
		} else {
			beginDate.setValue(me.defaultBeginDateDate);
			endDate.setValue(me.defaultEndDateDate);
		}
		me.onLoadData();
	},
	/**延时加载数据*/
	onLoadData: function(autoSelect) {
		var me = this;
		JShell.Action.delay(function() {
			me.onSearch(autoSelect);
		}, null, 600);
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;

		var toolbarSearch = me.getComponent('toolbarSearch');
		if (toolbarSearch) {
			var hasLicenceDate = toolbarSearch.getComponent('hasLicenceDate');
			if (hasLicenceDate) hasLicenceDate = hasLicenceDate.getValue();
			if (hasLicenceDate == "1" || hasLicenceDate == "true" || hasLicenceDate == "on") hasLicenceDate = true;
			if (hasLicenceDate == true) {
				var BeginDate = toolbarSearch.getComponent('BeginDate');
				var EndDate = toolbarSearch.getComponent('EndDate');
				var StartDateValue = JcallShell.Date.toString(BeginDate.getValue(), true);
				var EndDateValue = JcallShell.Date.toString(EndDate.getValue(), true);
				if (StartDateValue > EndDateValue) {
					JShell.Msg.error('结束日期不能小于开始日期!');
					me.hideMask();
					return;
				}
				var dateFormat = /^(\d{4})-(\d{2})-(\d{2})$/;
				if (StartDateValue && !dateFormat.test(StartDateValue)) {
					me.hideMask();
					return;
				}
				if (EndDateValue && !dateFormat.test(EndDateValue)) {
					me.hideMask();
					return;
				}
			}
		}
		me.load(null, true, autoSelect);
	},
	/**初始化字典信息*/
	ininClassDict: function() {
		var me = this;
		if (!JShell.System.ClassDict.LicenceType && me.isLoadLicenceType == false) {
			me.isLoadLicenceType = true;
			JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceType', function() {
				me.initLicenceTypeData();
			});
		} else {
			me.initLicenceTypeData();
		}
		if (!JShell.System.ClassDict.LicenceStatus && me.isLoadStatus == false) {
			me.isLoadStatus = true;
			JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceStatus', function() {
				me.initCheckStatus();
			});
		} else {
			me.initCheckStatus();
		}
	},
	/**获取授权状态信息*/
	loadStatusData: function() {
		var me = this;
		if (!JShell.System.ClassDict.LicenceStatus && me.isLoadStatus == false) {
			me.isLoadStatus = true;
			JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceStatus', function() {
				me.initCheckStatus();
			});
		} else {
			JShell.Action.delay(function() {
				me.initCheckStatus();
			}, null, 600);
		}
	},
	/**初始化授权状态下拉框数据源*/
	initCheckStatus: function() {
		var me = this;
		var list1 = JShell.System.ClassDict.LicenceStatus;
		var millisecond = 1;
		if (!list1) {
			list1 = [];
			millisecond = 600;
		}
		JShell.Action.delay(function() {
			if (list1.length <= 0) list1 = JShell.System.ClassDict.LicenceStatus;
			var checkStatus = null;
			var buttonsToolbar = me.getComponent('buttonsToolbar');
			if (buttonsToolbar) checkStatus = buttonsToolbar.getComponent('CheckStatus');
			if (checkStatus) { // && checkStatus.store.data.items.length <=0
				checkStatus.loadData(me.getLicenceStatusData(list1));
			}
		}, null, millisecond);
	},
	/**获取授权状态列表*/
	getLicenceStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for (var i in StatusList) {
			var obj = StatusList[i];
			//			if(obj.Id != 1) {
			var style = ['font-weight:bold;text-align:center'];
			if (obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
			//			}
		}
		return data;
	},
	/**获取授权类型列表*/
	loadLicenceTypeData: function() {
		var me = this;
		if (!JShell.System.ClassDict.LicenceType && me.isLoadLicenceType == false) {
			me.isLoadLicenceType = true;
			JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceType', function() {
				me.initLicenceTypeData();
			});
		} else {
			me.initLicenceTypeData();
		}
	},
	/**获取授权类型列表*/
	initLicenceTypeData: function() {
		var me = this;
		var list1 = JShell.System.ClassDict.LicenceType;
		var millisecond = 1;
		if (!list1) {
			list1 = [];
			millisecond = 800;
		}
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (buttonsToolbar) {
			me.updateLicenceType(list1, me);
		} else {
			JShell.Action.delay(function() {
				me.updateLicenceType(list1, me);
			}, null, millisecond);
		}
	},
	updateLicenceType: function(list1, me) {
		if (!list1 || list1.length <= 0) list1 = JShell.System.ClassDict.LicenceType;
		var licenceType = null;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (buttonsToolbar) licenceType = buttonsToolbar.getComponent('LicenceTypeId');
		if (licenceType) { //&& licenceType.store.data.items.length <= 0
			var data1 = me.getLicenceTypeData(list1);
			licenceType.loadData(data1);
		}
	},
	/**获取授权类型列表*/
	getLicenceTypeData: function(list1) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for (var i in list1) {
			var obj = list1[i];
			var style = ['font-weight:bold;text-align:center'];
			if (obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			toolbarSearch = me.getComponent('toolbarSearch');
		var Status = null,
			LicenceTypeId = null,
			PClientID = null,
			ProgramID = null,
			EquipID = null,
			BeginDate = null,
			EndDate = null,
			params = [],
			search = null;
		me.internalWhere = '';
		if (buttonsToolbar) {
			LicenceTypeId = buttonsToolbar.getComponent('LicenceTypeId').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
			Status = buttonsToolbar.getComponent('CheckStatus').getValue();
		}
		if (toolbarSearch) {
			PClientID = toolbarSearch.getComponent('PClientID').getValue();
			ProgramID = toolbarSearch.getComponent('ProgramID').getValue();
			EquipID = toolbarSearch.getComponent('EquipID').getValue();
			if (me.hasDates == true) {
				BeginDate = toolbarSearch.getComponent('BeginDate').getValue();
				EndDate = toolbarSearch.getComponent('EndDate').getValue();
			}
		}
		switch (Status) {
			case '3': //授权中
				params.push("((LicenceKey is null and Status=4) or Status in(2,5,8))");
				break;
			case '6': //特批授权中
				params.push("((LicenceKey is null and Status=4) or Status=8)");
				break;
			case '9': //授权完成
				params.push("((LicenceKey!=null and Status=4) or Status=7)");
				break;
			default:
				if (Status) {
					params.push("Status=" + Status + "");
				}
				break;
		}

		//类型
		if (LicenceTypeId) {
			params.push("LicenceTypeId='" + LicenceTypeId + "'");
		}
		//用户
		if (PClientID) {
			params.push("PClientID='" + PClientID + "'");
		}
		//程序
		if (ProgramID) {
			params.push("ProgramID='" + ProgramID + "'");
		}
		//仪器
		if (EquipID) {
			params.push("EquipID='" + EquipID + "'");
		}
		if (me.hasDates == true) {
			var hasLicenceDate = toolbarSearch.getComponent('hasLicenceDate');
			if (hasLicenceDate) hasLicenceDate = hasLicenceDate.getValue();
			if (hasLicenceDate == "1" || hasLicenceDate == "true" || hasLicenceDate == "on") hasLicenceDate = true;

			if (hasLicenceDate == true) {
				if (JShell.Date.toString(BeginDate, true)) {
					params.push("EndDate" + ">='" + JShell.Date.toString(BeginDate, true) + "'");
				}
				if (JShell.Date.toString(EndDate, true)) {
					params.push("EndDate" + "<'" + JShell.Date.toString(EndDate, true) + " 23:59:59'");
				}
			}
		}
		if (params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if (me.getSearchOtherWhere()) {
			if (me.internalWhere) {
				me.internalWhere += ' and ' + me.getSearchOtherWhere();
			} else {
				me.internalWhere = me.getSearchOtherWhere();
			}
		}
		if (search) {
			if (me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}

		return me.callParent(arguments);
	},
	/**初始化监听*/
	initListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var toolbarSearch = me.getComponent('toolbarSearch');
		if (!buttonsToolbar) {
			return;
		}
		//客户
		var PClientName = toolbarSearch.getComponent('PClientName'),
			PClientID = toolbarSearch.getComponent('PClientID');

		PClientName.on({
			check: function(p, record) {
				PClientName.setValue(record ? record.get('PClient_Name') : '');
				PClientID.setValue(record ? record.get('PClient_Id') : '');
				me.onSearch();
				p.close();
			}
		});

		//程序
		var ProgramName = toolbarSearch.getComponent('ProgramName'),
			ProgramID = toolbarSearch.getComponent('ProgramID');

		ProgramName.on({
			check: function(p, record) {
				ProgramName.setValue(record ? record.get('PGMProgram_Title') : '');
				ProgramID.setValue(record ? record.get('PGMProgram_Id') : '');
				me.onSearch();
				p.close();
			}
		});
		//仪器
		var EquipName = toolbarSearch.getComponent('EquipName'),
			EquipID = toolbarSearch.getComponent('EquipID');

		EquipName.on({
			check: function(p, record) {
				EquipName.setValue(record ? record.get('BEquip_CName') : '');
				EquipID.setValue(record ? record.get('BEquip_Id') : '');
				me.onSearch();
				p.close();
			}
		});

		//状态
		var CheckStatus = buttonsToolbar.getComponent('CheckStatus');
		CheckStatus.on({
			change: function(com, newValue, oldValue, eOpts) {
				me.onSearch();
			}
		});
		//类型
		var LicenceTypeId = buttonsToolbar.getComponent('LicenceTypeId');
		LicenceTypeId.on({
			change: function(com, newValue, oldValue, eOpts) {
				me.onSearch();
			}
		});
	},
	/**查询信息*/
	openShowForm: function(record) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.76;
		var height = document.body.clientHeight - 8;
		var id = record.get(me.PKField);
		var PClientID = record.get("PClientID");
		if (PClientID == "" || PClientID == undefined)
			PClientID = null;
		JShell.Win.open('Shell.class.wfm.authorization.ahsingle.show.Panel', {
			SUB_WIN_NO: '101', //内部窗口编号
			//resizable:false,
			height: height,
			width: maxWidth,
			title: '授权信息',
			PClientID: PClientID,
			PK: id
		}).show();
	},
	/**修改*/
	openEditForm: function(record, editPanel) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.74;
		maxWidth = maxWidth >= 980 ? 980 : maxWidth;
		var height = document.body.clientHeight - 8;
		var id = record.get(me.PKField);
		var PClientID = record.get("PClientID");
		if (PClientID == "" || PClientID == undefined)
			PClientID = null;
		JShell.Win.open(editPanel, {
			SUB_WIN_NO: '101',
			height: height,
			width: maxWidth,
			PK: id,
			PClientID: PClientID,
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**获取其他查询框内容*/
	getSearchOtherWhere: function(value) {
		var me = this,
			where = '';
		return where;
	},
	/*
	 * 计算两个日期的间隔天数
	 * startDate 开始时间
	 * endDate 结束时间
	 */
	getCalcDays: function(startDate, endDate) {
		var aDate, starttime, endtime, iDays;
		aDate = startDate.split("-");
		starttime = new Date(aDate[1] + '/' + aDate[2] + '/' + aDate[0]); //转换为12/13/2008格式
		aDate = endDate.split("-");
		endtime = new Date(aDate[1] + '/' + aDate[2] + '/' + aDate[0]);
		var i = (starttime - endtime) / 1000 / 60 / 60 / 24;
		if (i < 0) i -= 1;
		else
			i += 1;
		iDays = i; //把相差的毫秒数转换为天数
		return iDays;
	}
});
