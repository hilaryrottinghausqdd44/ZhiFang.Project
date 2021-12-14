/**
 * 部门员工的某一统计月的所有工作日志
 * @author longfc
 * @version 2016-10-07
 */
Ext.define('Shell.class.oa.worklog.show.EmpGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '工作日志信息',
	width: 360,
	height: 500,

	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogByEmpId',

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	hasRownumberer: false,
	/**是否启用新增按钮*/
	hasAdd: false,
	hasEidt: false,
	columnLines: true,
	isLoaded: false,

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

	hasPagingtoolbar: false,
	/**周默认值*/
	defaultDateTypeValue: '1',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	columnLines: true,

	/**员工Id*/
	EMPID: "",
	/*是否查询全部**/
	IsIncludeSubDept: false,
	remoteSort: false,

	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: me.getStoreFields(),
			pageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
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
		worklogtypeChoose.push();

		if(me.hasRefresh) {
			me.buttonToolbarItems.push('refresh');
		}
		me.buttonToolbarItems.push("->", '-', {
			width: 95,
			xtype: 'uxYearComboBox',
			itemId: 'Year',
			minValue: 2016,
			value: me.YearValue,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != null && newValue != "") {
						me.GridSearch();
					}
				}
			}
		}, {
			width: 80,
			xtype: 'uxMonthComboBox',
			itemId: 'Month',
			value: me.MonthValue,
			margin: '0 2px 0 10px',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != null && newValue != "") {
						me.GridSearch();
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
				me.GridSearch();
			}
		});
	},
	GridSearch: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			Year = buttonsToolbar.getComponent('Year'),
			Month = buttonsToolbar.getComponent('Month');
		if(me.EMPID != "" && Year != "" && Month != "") {
			me.onSearch();
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl;
		var whereParams = me.getSearchWhereParams();
		url += (url.indexOf('?') == -1 ? '?' : '&');
		if(whereParams) {
			url += whereParams;
		}
		me.isLoaded = true;
		return url;
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
			width: 100,
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
			text: '星期一',
			dataIndex: 'Monday',
			width: 110,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Monday');
			}
		}, {
			text: '星期二',
			dataIndex: 'Tuesday',
			width: 110,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Tuesday');
			}
		}, {
			text: '星期三',
			dataIndex: 'Wednesday',
			width: 110,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Wednesday');
			}
		}, {
			text: '星期四',
			dataIndex: 'Thursday',
			width: 110,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Thursday');
			}
		}, {
			text: '星期五',
			dataIndex: 'Friday',
			sortable: false,
			align: 'center',
			width: 110,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Friday');
			}
		}, {
			text: '星期六',
			dataIndex: 'Saturday',
			width: 110,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Saturday');
			}
		}, {
			text: '星期日',
			dataIndex: 'Sunday',
			width: 110,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Sunday');
			}
		}, {
			text: '日期',
			dataIndex: 'DateCode',
			width: 110,
			hidden: true,
			sortable: false,
			menuDisabled: true
		});
		return columns;
	},
	/**获取带查询条件*/
	getSearchWhereParams: function() {
		var me = this,
			whereParams = "",
			StartDate = "",
			EndDate = "",
			params = [],
			buttonsToolbar = me.getComponent('buttonsToolbar');

		//按员工查询
		if(me.EMPID != "") {
			params.push("&empid=" + me.EMPID);
		}
		params.push("&isincludesubdept=" + me.IsIncludeSubDept);
		var Year = buttonsToolbar.getComponent('Year').getValue(),
			Month = buttonsToolbar.getComponent('Month').getValue();

		if(Year != "" && Month != "") {
			Month = Year + "-" + Month;
			params.push("&monthday=" + Month);
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

		me.BeginDate = JShell.Date.getMonthFirstDate(year, month);
		me.EndDate = JShell.Date.getMonthLastDate(year, month);

		me.YearValue = date.getFullYear(); //年
		me.MonthValue = date.getMonth() + 1; //月
	},
	getMetaValue: function(value, meta, record, rowIndex, colIndex, store, view, weekDay) {
		var me = this;
		switch(weekDay) {
			case "Sunday":
				value = me.changeValue(value, meta, record, rowIndex, colIndex, store, view, weekDay);
				break;
			case "Monday":
				value = me.changeValue(value, meta, record, rowIndex, colIndex, store, view, weekDay);
				break;
			case "Tuesday":
				value = me.changeValue(value, meta, record, rowIndex, colIndex, store, view, weekDay);
				break;
			case "Wednesday":
				value = me.changeValue(value, meta, record, rowIndex, colIndex, store, view, weekDay);
				break;
			case "Thursday":
				value = me.changeValue(value, meta, record, rowIndex, colIndex, store, view, weekDay);
				break;
			case "Friday":
				value = me.changeValue(value, meta, record, rowIndex, colIndex, store, view, weekDay);
				break;
			case "Saturday":
				value = me.changeValue(value, meta, record, rowIndex, colIndex, store, view, weekDay);
				break;
			default:
				break;
		}
		return value;
	},
	changeValue: function(value, meta, record, rowIndex, colIndex, store, view, weekDay) {
		var me = this;
		var showValue = "",
			monthDay = "", //第几天
			qtipStrValue = "", //提示信息
			DateCode = "";
		var weekDayInfo = record.get(weekDay);
		if(weekDayInfo && weekDayInfo != "" && weekDayInfo != null) {
			weekDayInfo = Ext.JSON.decode(weekDayInfo);
		}
		//工作日的类样式
		var workdayClass = "btn btn-sm btn-primary";
		//节假日的类样式
		var holidayClass = "btn btn-sm btn-success";

		//该月和第几天(如:2016-07-01)
		DateCode = weekDayInfo.DateCode;
		if(weekDayInfo.WeekDay == weekDay) {
			var isWorkDay = weekDayInfo.IsWorkDay;
			var checkClass = isWorkDay ? workdayClass : holidayClass;
			var checkText = isWorkDay ? "工作日" : "节假日";
			if(DateCode == undefined) {
				DateCode = "";
			}
			var qtipStrValue = "";
			monthDay = "<a style='font-weight:bold'>" + DateCode + "</div>";
			if(weekDayInfo.Id == "" || weekDayInfo.Id == null) {
				monthDay = "<a style='font-weight:bold'>" + DateCode + "</div>";
				qtipStrValue = "无日报总结!";
				if(checkText == "工作日") {
					//checkText = checkText + "(无)";
					checkClass = "btn btn-sm btn-warning";
				}
			} else {
				monthDay = "<a style='font-weight:bold'>" + DateCode + "</div>";
				var dataAddTime = weekDayInfo.DataAddTime;
				if(dataAddTime == undefined) {
					dataAddTime = "";
				}
				var qtipToDayContent = weekDayInfo.ToDayContent;
				if(qtipToDayContent != null) {
					qtipToDayContent = qtipToDayContent.replace(/\\r\\n/g, "<br />");
					qtipToDayContent = qtipToDayContent.replace(/\\n/g, "<br />");
				}
				if(qtipToDayContent == undefined) {
					qtipToDayContent = "";
				}
				var qtipCopyForEmpNameList = weekDayInfo.CopyForEmpNameList;
				if(qtipCopyForEmpNameList == undefined) {
					qtipCopyForEmpNameList = "";
				}
				var picpath = '../ui/images/user/user.png';
				var copyForEmpName = "<span style='width:100%;color:green;font-weight:bold'>抄送:@" + qtipCopyForEmpNameList + "</span><br />";
				qtipStrValue = "<table style='width:460px;border-bottom-style:solid; border-bottom-width:0px; border-color:darkgrey' border='0'>" +

					"<tr style='margin:2px;height:26px'>" +
					"<td rowspan='1' width='50' align='center' valign='middle'>" +
					"<img width='50' src='" + picpath + "'/></td>" +

					"<td width='105' valign='middle' colspan='3'>" +
					"<div style='float :left;margin-left:5px;font-weight:bold'>填报时间：" + dataAddTime + "</div>" +
					"</td>" +
					"</tr>" +
					"<tr style='margin:2px;width:100%;'>" +
					"<td colspan='2'>" +
					copyForEmpName +
					"</td>" +
					"</tr>" +
					"</table>";
				qtipStrValue = qtipStrValue + "<p border=0 style='vertical-align:top;'>" + "日报信息:" + qtipToDayContent + "</p>";
			}
			if(checkText != undefined && checkText != null)
				showValue = '<div class="' + checkClass + '"' + 'style="background-position: 50% -2px;vertical-align:middle;background-repeat:no-repeat;">' + checkText + '</div>'

		}
		var result = "<table style='padding:2px 2px;vertical-align:top;width:100%;border-bottom-style:solid; border-bottom-width:0px; border-color:darkgrey;font-weight:bold;height:36px;' border='0'>" +
			"<tr style='vertical-align:top;margin:2px;height:16px;width:100%;'>" +
			"<td style='text-align:center;vertical-align:top;margin:2px;width:100%;'>" +
			monthDay +
			"</td>" +
			"</tr>" +
			"<tr style='vertical-align:top;margin:2px;height:20px;width:100%;'>" +
			"<td style='text-align:center'>" +
			showValue +
			"</td>" +
			"</tr>" +
			"</table>";
		meta.style = 'width:100%;padding:2px 4px;text-overflow: ellipsis;white-space: nowrap;white-space:normal;line-height:20px;word-break:break-all; word-wrap:break-word;';
		//meta.css = "background:#93A9C1;";

		if(qtipStrValue) meta.tdAttr = 'data-qtip="<b>' + qtipStrValue + '</b>"';

		return result;
	},
	createNullModel: function() {
		var me = this;
		var model = {
			"Id": "",
			"EmpName": "",
			"EmpId": "",
			"DateCode": "",
			"Monday": "",
			"Tuesday": "",
			"Wednesday": "",
			"Thursday": "",
			"Friday": "",
			"Saturday": "",
			"Sunday": "",
			"ToDayContent": '',
			"NextDayContent": '',
			"DataAddTime": '',
			"WeekInfo": '',
			"CopyForEmpNameList": ''
		};
		return model;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		var result = {},
			list = [];
		result.success = data.success;
		result.msg = data.msg;
		//存在天数的数组
		var tempArrATEventDateCode = [];
		var ATEventDateCode = "",
			info = null,
			type = null,
			tempArr = [];
		var model = me.createNullModel();

		buttonsToolbar = me.getComponent('buttonsToolbar');
		var year = buttonsToolbar.getComponent('Year').getValue();
		Ext.Array.each(data.value, function(obj, index) {
			DateCode = obj.DateCode;
			DateCode = Ext.util.Format.date(DateCode, 'Y-m-d');
			var indexOf = tempArrATEventDateCode.indexOf(Ext.util.Format.date(DateCode, 'Y-m-d'));
			var weekDay = me.changeweekDay(obj.DateCode);

			model = me.createNullModel();
			model.Id = obj.Id;
			model.DataAddTime = obj.DataAddTime;
			model.WeekInfo = obj.WeekInfo;

			model.EmpId = obj.EmpId;
			model.EmpName = obj.EmpName;
			model.DateCode = Ext.util.Format.date(DateCode, 'Y-m-d');
			model.ToDayContent = obj.ToDayContent;
			model.CopyForEmpNameList = obj.CopyForEmpNameList;

			if(indexOf == -1) { //不存在list中
				tempArrATEventDateCode.push(Ext.util.Format.date(DateCode, 'Y-m-d'));
				list.push(model);
			} else { //已存在list中
				Ext.Array.forEach(list, function(tempObj, index, array) {
					if(tempObj.DateCode == DateCode) {
						list[tempObj] = tempObj;
					}
				});
			}

		});
		result.list = list;
		var nullRecords = me.createNullRecords();
		//作第三次转换处理
		result = me.createRecords(result, nullRecords);
		return result;
	},
	/**
	 * 员工个人月考勤空数据集
	 * */
	createNullRecords: function() {
		var me = this;
		nullRecords = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var year = buttonsToolbar.getComponent('Year').getValue();
		var month = buttonsToolbar.getComponent('Month').getValue();
		var firdDay = year + "-" + month + "-01";
		var firstWeekDay = me.changeweekDay(firdDay);

		var addRowIndexWeekDays = [];
		var countDays = JcallShell.Date.getCountDays(firdDay);
		if(countDays == null || countDays == "") {
			countDays = 30;
		}
		switch(firstWeekDay) {
			case "Monday":
				addDays = 0;
				break;
			case "Tuesday":
				addDays = 1;
				addRowIndexWeekDays = ["Monday"];
				break;
			case "Wednesday":
				addDays = 2;
				addRowIndexWeekDays = ["Monday", "Tuesday"];
				break;
			case "Thursday":
				addDays = 3;
				addRowIndexWeekDays = ["Monday", "Tuesday", "Wednesday"];
				break;
			case "Friday":
				addDays = 4;
				addRowIndexWeekDays = ["Monday", "Tuesday", "Wednesday", "Thursday"];
				break;
			case "Saturday":
				addDays = 5;
				addRowIndexWeekDays = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];
				break;
			case "Sunday":
				addDays = 6;
				addRowIndexWeekDays = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
				break;
			default:
				break;
		}
		var rowIndex = 0,
			colIndex = addDays,
			day = 0;
		for(var i = 0; i < 7; i++) {
			var model = me.createNullModel();
			if(day > countDays) {
				break;
			}
			for(var j = colIndex; j < 7; j++) {
				day = day + 1;
				if(day > countDays) {
					break;
				}
				var DateCode = year + "-" + month + "-" + day;
				var weekDay = me.changeweekDay(DateCode);
				var obj = {
					Id: "",
					EmpName: "",
					EmpId: "",
					Day: day,
					WeekDay: weekDay, //星期几
					MonthDay: month + "-" + day, //月和日
					DateCode: DateCode, //日期
					DataAddTime: "",
					ToDayContent: "",
					NextDayContent: "",
					CopyForEmpNameList: ""
				};
				switch(weekDay) {
					case "Saturday":
						obj.IsWorkDay = false;
						break;
					case "Sunday":
						obj.IsWorkDay = false;
						break;
					default:
						obj.IsWorkDay = true;
						break;
				}
				model[weekDay] = Ext.JSON.encode(obj);
				if(j == 6) {
					colIndex = 0;
					break;
				}
				colIndex = colIndex = 1;
			}
			nullRecords.push(model);
		}
		return nullRecords;
	},
	/**@overwrite查询某一天属于星期几*/
	changeweekDay: function(dateStr) {
		var me = this;
		var weekDay = ["星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
		var myDate = new Date(Date.parse(dateStr.replace(/-/g, "/")));
		var day = weekDay[myDate.getDay()];
		switch(day) {
			case "星期天":
				day = "Sunday";
				break;
			case "星期一":
				day = "Monday";
				break;
			case "星期二":
				day = "Tuesday";
				break;
			case "星期三":
				day = "Wednesday";
				break;
			case "星期四":
				day = "Thursday";
				break;
			case "星期五":
				day = "Friday";
				break;
			case "星期六":
				day = "Saturday";
				break;
			default:
				break;
		}
		return day;
	},
	/**
	 * 员工个人月考勤
	 * */
	createRecords: function(data, nullRecords) {
		var me = this;
		var result = {},
			list = data.list;
		result.success = data.success;
		result.msg = data.msg;
		if(list == null || list.length < 1) {
			result.list = nullRecords;
			return result;
		}
		var weekDay = ""; //星期几
		var addDays = 0, //当月需要补的天数
			day = 0; //第几天
		var rowIndex = 0, //第几行
			colIndex = 0; //第几列
		var addRowIndexWeekDays = [];
		//算出该月的第一天是星期几,第一天除不是星期一之外,都需要补空格
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var year = buttonsToolbar.getComponent('Year').getValue();
		var month = buttonsToolbar.getComponent('Month').getValue();
		var DateCode = year + "-" + month + "-01";
		var firstWeekDay = me.changeweekDay(DateCode);

		var countDays = JcallShell.Date.getCountDays(DateCode);
		if(countDays == null || countDays == "") {
			countDays = 30;
		}
		switch(firstWeekDay) {
			case "Monday":
				addDays = 0;
				break;
			case "Tuesday":
				addDays = 1;
				addRowIndexWeekDays = ["Monday"];
				break;
			case "Wednesday":
				addDays = 2;
				addRowIndexWeekDays = ["Monday", "Tuesday"];
				break;
			case "Thursday":
				addDays = 3;
				addRowIndexWeekDays = ["Monday", "Tuesday", "Wednesday"];
				break;
			case "Friday":
				addDays = 4;
				addRowIndexWeekDays = ["Monday", "Tuesday", "Wednesday", "Thursday"];
				break;
			case "Saturday":
				addDays = 5;
				addRowIndexWeekDays = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];
				break;
			case "Sunday":
				addDays = 6;
				addRowIndexWeekDays = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
				break;
			default:
				break;
		}
		colIndex = addDays;
		var reg = new RegExp("null", "g");
		for(var i = 0; i < list.length; i++) {
			var record = list[i];
			if(record && record != undefined) {
				if(rowIndex == nullRecords.length) {
					break;
				}
				if(day > countDays) {
					break;
				}
				day = day + 1;

				weekDay = me.changeweekDay(record.DateCode);
				var obj = {
					Id: record.Id,
					Day: day, //第几天
					WeekDay: weekDay, //星期几
					MonthDay: month + "-" + day, //月和日
					DateCode: record.DateCode, //日期
					DataAddTime: record.DataAddTime,
					ToDayContent: record.ToDayContent,
					CopyForEmpNameList: record.CopyForEmpNameList
				};
				if(record.IsWorkDay) {
					obj.IsWorkDay = record.IsWorkDay;
				} else {
					switch(weekDay) {
						case "Saturday":
							obj.IsWorkDay = false;
							break;
						case "Sunday":
							obj.IsWorkDay = false;
							break;
						default:
							obj.IsWorkDay = true;
							break;
					}
				}
				if(colIndex == 7) {
					colIndex = 0;
					rowIndex = rowIndex + 1;
				}
				colIndex = colIndex + 1;
				var row = nullRecords[rowIndex];
				var rowValue = Ext.JSON.encode(obj);
				rowValue = rowValue.replace(reg, "''");
				row[weekDay] = rowValue;
				nullRecords[rowIndex] = row;
			}
		}
		result.list = nullRecords;
		return result;
	}
});