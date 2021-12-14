/**
 * 部门员工日报统计列表
 * @author longfc
 * @version 2016-10-07
 */
Ext.define('Shell.class.oa.worklog.show.DeptEmpGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '部门员工日报统计信息',
	width: 360,
	height: 500,

	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType',
	defaultPageSize: 100,

	/**周列表*/
	DateTimeList: [],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,

	/**周默认值*/
	WeekValue: '1',
	hasRownumberer: false,
	/**是否启用新增按钮*/
	hasAdd: false,
	hasEidt: false,
	/**查询栏包含周选项*/
	hasWeeked: false,
	/**是否启用刷新按钮*/
	hasRefresh: false,

	columnLines: true,
	isLoaded: false,
	/**查询对象*/
	objectEName: '',
	/**登录员工*/
	EMPID: "",
	worklogtype: '',
	sendtype: '',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'EmpName',
		direction: 'DESC'
	}, {
		property: 'DataAddTime',
		direction: 'ASC'
	}], //
	defaultPageSize: 200,
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: true,

	hasPagingtoolbar: true,
	/**周默认值*/
	defaultDateTypeValue: '1',
	hasRownumberer: false,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	columnLines: true,
	/**查询对象*/
	objectEName: 'WorkLogDay',
	worklogtype: 'WorkLogDay',
	/*部门Id*/
	DeptId: '',
	/**员工Id*/
	EMPID: "",
	sendtype: 'ALL',
	/*是否查询全部**/
	IsIncludeSubDept: false,
	remoteSort: false,
	/**日志外键名称
	 * @author Jcall
	 * @version 2016-08-19
	 */
	LogName: '',
	features: [{
		ftype: 'grouping',
		groupHeaderTpl: "{name} (共 {rows.length} 行{[values.rows.length > 1 ? '' :'']})" //<span style='color:blue;'><strong> </strong></span>
	}],
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: me.getStoreFields(),
			pageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
			groupField: 'DateCode',
			groupDir: 'ASC',
			applySort: function() {
				Ext.data.GroupingStore.superclass.applySort.call(this);
				if(!me.groupOnSort && !this.remoteGroup) {
					if(me.groupField != me.sortInfo.field ||
						me.groupDir != me.sortInfo.direction) {
						me.sortData(me.groupField, this.groupDir);
					}
				}
			},
			applyGrouping: function(alwaysFireChange) {
				if(me.groupField !== false) {
					me.groupBy(this.groupField, true, me.groupDir);
					return true;
				} else {
					if(alwaysFireChange === true) {
						me.fireEvent('datachanged', me);
					}
					return false;
				}
			},
			proxy: {
				type: 'ajax',
				url: '',
				reader: {
					type: 'json',
					totalProperty: 'count',
					root: 'list'
				},
				extractResponseData: function(response) {
					var data = JShell.Server.toJson(response.responseText);
					if(data.success) {
						var info = data.value;
						if(info) {
							var type = Ext.typeOf(info);
							if(type == 'object') {
								info = info;
							} else if(type == 'array') {
								info.list = info;
								info.count = info.list.length;
							} else {
								info = {};
							}

							data.count = info.count || 0;
							data.list = info.list || [];
						} else {
							data.count = 0;
							data.list = [];
						}
						data = me.changeResult(data);
						me.fireEvent('changeResult', me, data);
					} else {
						me.errorInfo = data.msg;
					}
					response.responseText = Ext.JSON.encode(data);

					return response;
				}
			},
			listeners: {
				beforeload: function() {
					return me.onBeforeLoad();
				},
				load: function(store, records, successful) {
					me.onAfterLoad(records, successful);
				}
			}
		});
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		Ext.QuickTips.init();
		Ext.override(Ext.ToolTip, {
			maxWidth: 680
		});
	},
	initComponent: function() {
		var me = this;
		//初始化送检时间
		me.initDate();
		//初始化功能按钮栏内容
		me.initButtonToolbarItems();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},

	/**初始化功能按钮栏内容*/
	initButtonToolbarItems: function() {
		var me = this;
		me.buttonToolbarItems = [];
		var worklogtypeChoose = [];
		worklogtypeChoose.push({
			boxLabel: '日总结',
			name: 'worklogtype',
			inputValue: 'WorkLogDay',
			checked: true
		}, {
			boxLabel: '周总结',
			name: 'worklogtype',
			inputValue: 'WorkLogWeek',
			checked: false
		}, {
			boxLabel: '月总结',
			name: 'worklogtype',
			inputValue: 'WorkLogMonth',
			checked: false
		});

		if(me.hasRefresh) {
			me.buttonToolbarItems.push('refresh');
		}
		me.buttonToolbarItems.push("->", '-', {
			xtype: 'radiogroup',
			itemId: 'worklogtypeChoose',
			columns: 3,
			width: 180,
			vertical: true,
			items: worklogtypeChoose,
			listeners: {
				change: {
					fn: function(rdgroup, checked) {
						me.dateTypeChoose(rdgroup, checked.worklogtype);
					}
				}
			}
		}, '-', {
			width: 155,
			labelWidth: 60,
			labelAlign: 'right',
			fieldLabel: '日期',
			itemId: 'BeginDate',
			value: me.BeginDate,
			xtype: 'datefield',
			format: 'Y-m-d'
		}, {
			width: 100,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			value: me.EndDate,
			xtype: 'datefield',
			format: 'Y-m-d'
		}, {
			width: 95,
			xtype: 'uxYearComboBox',
			itemId: 'Year',
			hidden: true,
			minValue: 2016,
			value: me.YearValue
		}, {
			width: 80,
			xtype: 'uxMonthComboBox',
			itemId: 'Month',
			hidden: true,
			value: me.MonthValue,
			margin: '0 2px 0 10px',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != null && newValue != "") {

					}
				}
			}
		}, {
			width: 80,
			fieldLabel: '',
			xtype: 'uxSimpleComboBox',
			itemId: 'Week',
			hasStyle: true,
			hidden: true,
			value: me.WeekValue,
			data: me.DateTimeList,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != null && newValue != "") {
						me.onSearch();
					}
				}
			}
		});
		me.buttonToolbarItems.push({
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
	},
	dateTypeChoose: function(rdgroup, checkedValue) {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			BeginDate = buttonsToolbar.getComponent('BeginDate'),
			EndDate = buttonsToolbar.getComponent('EndDate'),

			Year = buttonsToolbar.getComponent('Year'),
			Month = buttonsToolbar.getComponent('Month'),
			Week = buttonsToolbar.getComponent('Week');
		switch(checkedValue) {
			case "WorkLogDay":
				BeginDate.setVisible(true);
				EndDate.setVisible(true);
				Year.setVisible(false);
				Month.setVisible(false);
				Week.setVisible(false);
				me.objectEName = 'WorkLogDay';
				me.worklogtype = 'WorkLogDay';

				me.LogName = 'WorkDayLog';
				break;
			case "WorkLogWeek":
				BeginDate.setVisible(false);
				EndDate.setVisible(false);

				Year.setVisible(true);
				Month.setVisible(true);
				Week.setVisible(true);

				me.objectEName = 'PWorkWeekLog';
				me.worklogtype = 'WorkLogWeek';
				me.LogName = 'WorkWeekLog';
				break;
			case "WorkLogMonth":
				BeginDate.setVisible(false);
				EndDate.setVisible(false);

				Year.setVisible(true);
				Month.setVisible(true);
				Week.setVisible(false);

				me.objectEName = 'PWorkMonthLog';
				me.worklogtype = 'WorkLogMonth';
				me.LogName = 'WorkMonthLog';
				break;
			default:
				break;
		}
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl;
		var whereParams = me.getSearchWhereParams();
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields='; //+ me.getStoreFields(true).join(',')
		if(whereParams) {
			url += whereParams;
		}
		me.isLoaded = true;
		return url;
	},
	/**根据日志ID查看日志交流
	 * @author Jcall
	 * @version 2016-08-19
	 */
	showInteractionById: function(LogName, LogId) {
		var me = this;
		JShell.Win.open('Shell.class.oa.worklog.interaction.App', {
			//resizable: false,
			LogName: LogName, //日志外键名称
			LogId: LogId, //日志ID
		}).show();
	},
	ToDayContentRenderer: function(value, meta, record, rowIndex, colIndex, store, view) {
		var me = this;
		var toDayContent = "";
		if(value != null) {
			value = value.replace(/\\r\\n/g, "<br />");
			value = value.replace(/\\n/g, "<br />");

			toDayContent = value.toString();
		}
		toDayContent = toDayContent.replace(/\\n/g, "<br />");
		var qtipToDayContent = value;
		var qtipStrValue = "";
		var empName = record.get('EmpName');
		var dataAddTime = record.get('DataAddTime').replace(/\//g, "-");
		var image1 = record.get('Image1');
		var LikeCount = record.get('LikeCount');
		var picpath = '../ui/images/user/user.png';
		var worklogtype = "";

		var likeCountStr = "<span style='color:blue;vertical-align:top;'>点赞计数:" + LikeCount + "</span><br />";

		var copyForEmpName = "<span style='width:100%;color:green;font-weight:bold'>抄送:@" + record.get("CopyForEmpNameList") + "</span><br />";

		var strValue = "<table style='width:100%;border-bottom-style:solid; border-bottom-width:0px; border-color:darkgrey' border='0'>" +

			"<tr style='margin:2px;height:26px'>" +
			"<td rowspan='1' width='50' align='center' valign='middle'>" +
			"<img width='50' src='" + picpath + "'/></td>" +

			"<td width='105' valign='middle' colspan='3'>" +
			"<div style='margin-left:5px;font-weight:bold'>" + empName + "</div>" +
			"<div style='float :left;margin-left:5px;font-weight:bold'>填报时间：" + dataAddTime + "</div>" + worklogtype +
			"</td>" +

			//交流，@author Jcall，@version 2016-08-19
			"<td width='50' valign='middle' colspan='1'>" +
			"</td>" +

			"</tr>" +

			"<tr style='margin:2px;width:100%;'>" +
			"<td colspan='4'>" +
			copyForEmpName +
			"</td>" +
			"</tr>" +
			"</table>";

		qtipStrValue = qtipStrValue + "<p border=0 style='vertical-align:top;'>" + "内容:" + qtipToDayContent + "</p>";

		if(qtipStrValue) meta.tdAttr = 'data-qtip="<b>' + qtipStrValue + '</b>"';

		meta.style = 'width:100%;padding:2px 4px;text-overflow: ellipsis;white-space: nowrap;white-space:normal;line-height:20px;word-break:break-all; word-wrap:break-word;';
		meta.css = "background:#93A9C1;";
		return strValue;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];

		columns.push({
			text: '姓名',
			dataIndex: 'EmpName',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		});
		columns.push({
			text: '员工Id',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			dataIndex: 'EmpID',
			width: 15
		}, {
			text: 'Id',
			isKey: true,
			hidden: true,
			hideable: false,
			dataIndex: 'Id',
			width: 15
		}, {
			text: '填报时间',
			dataIndex: 'DataAddTime',
			width: 130,
			isDate: true,
			hasTime: true,
			hidden: true,
			sortable: false,
			menuDisabled: true
		}, {
			text: '工作日志',
			dataIndex: 'ToDayContent',
			flex: 1,
			//width: 100,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.ToDayContentRenderer(value, meta, record, rowIndex, colIndex, store, view);
			}
		}, {
			text: '日期',
			dataIndex: 'DateCode',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '是否提交',
			dataIndex: 'Status',
			width: 60,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '点赞计数',
			dataIndex: 'LikeCount',
			width: 60,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '抄送人',
			dataIndex: 'CopyForEmpNameList',
			width: 10,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '图片一',
			dataIndex: 'Image1',
			width: 10,
			sortable: false,
			menuDisabled: false,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '任务',
			dataIndex: 'PTaskID',
			width: 100,
			sortable: false,
			menuDisabled: false,
			hidden: true,
			defaultRenderer: true
		});
		return columns;
	},

	/**获取带查询条件*/
	getSearchWhereParams: function() {
		var me = this,
			where = "";
		return where;
	},
	/**获取带查询条件*/
	getSearchWhereParams: function() {
		var me = this,
			whereParams = "",
			StartDate = "",
			EndDate = "",
			params = [],
			buttonsToolbar = me.getComponent('buttonsToolbar');

		params.push("&sendtype=" + me.sendtype);
		params.push("&worklogtype=" + me.worklogtype);
		//按部门或按员工查询
		if(me.DeptId != "") {
			params.push("&deptid=" + me.DeptId);
		}
		if(me.DeptId == "" && me.EMPID != "") {
			params.push("&empid=" + me.EMPID);
		}
		params.push("&isincludesubdept=" + me.IsIncludeSubDept);
		switch(me.objectEName) {
			case "WorkLogDay": //按日总结查询
				BeginDate = buttonsToolbar.getComponent('BeginDate').getValue();
				EndDate = buttonsToolbar.getComponent('EndDate').getValue();
				if(BeginDate != null && EndDate != null) {
					params.push("&sd=" + JShell.Date.toString(BeginDate, true));
					params.push("&ed=" + JShell.Date.toString(EndDate, true));
				}
				break;
			case "PWorkWeekLog": //按周总结查询
				var YearValue = buttonsToolbar.getComponent('Year').getValue(),
					MonthValue = buttonsToolbar.getComponent('Month').getValue(),
					WeekValue = buttonsToolbar.getComponent('Week').getValue();

				var weekdate = JcallShell.Date.getWeekStartDateAndEndDate(YearValue, MonthValue, WeekValue);
				if(weekdate && weekdate.StartDate && weekdate.EndDate) {
					params.push("&sd=" + weekdate.StartDate + "");
					params.push("&ed=" + weekdate.EndDate + "");
				}
				break;
			case "PWorkMonthLog": //按月总结查询
				var Year = buttonsToolbar.getComponent('Year').getValue(),
					Month = buttonsToolbar.getComponent('Month').getValue();
				var BeginDate = Year + '-' + Month + '-01';
				EndDate = JcallShell.Date.getMonthLastDate(Year, Month);
				if(BeginDate && EndDate) {
					params.push("&sd=" + JShell.Date.toString(BeginDate, true) + "");
					params.push("&ed=" + JShell.Date.toString(JShell.Date.getNextDate(EndDate, 0), true) + "");
				}
				break;
			default:
				break;
		}

		if(params.length > 0) {
			whereParams += params.toString().replace(/,/g, '');
		}
		return whereParams;
	},
	/**初始化数据*/
	initDate: function() {
		var me = this;
		//时间处理
		var date = JShell.Date.getNextDate(new Date(), 0);

		var year = date.getFullYear();
		var month = date.getMonth() + 1;
		var weeks = JShell.Date.getMonthTotalWeekByDate(date);

		me.BeginDate = JShell.Date.getMonthFirstDate(year, month);
		me.EndDate = JShell.Date.getMonthLastDate(year, month);

		me.YearValue = date.getFullYear(); //年
		me.MonthValue = date.getMonth() + 1; //月
		me.WeekValue = JShell.Date.getMonthWeekByDate(date) + ''; //周

		//周列表
		me.DateTimeList = [];
		for(var i = 0; i < weeks; i++) {
			me.DateTimeList.push([(1 + i) + '', '第' + (i + 1) + '周']);
		}
	}
});