/**
 * 周考勤列表
 * @author 
 * @version 2016-07-22
 */
Ext.define('Shell.class.oa.at.attendance.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '考勤列表',
	width: 360,
	height: 500,
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	/**获取数据服务路径*/
	selectUrl: '/WeiXinAppService.svc/GetATEmpListWeekLog',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'DeptName',
		direction: 'ASC'
	}],
	defaultPageSize: 2000,
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: false,
	/**周列表*/
	DateTimeList: [],
	/**考勤的颜色显示*/
	AttendanceColor: {
		SignIn: 'black', //正常签到
		SignOut: 'black', //正常签退
		Overt: 'green', //加班
		Leave: 'green', //请假
		Egress: 'green', //外出
		Trip: 'green', //出差
		Red: 'red' //签到迟到,签退早退及旷工,审核退回
	},
	hasPagingtoolbar: false,
	/**月默认值*/
	defaultMonthValue: '1',
	/**周默认值*/
	defaultWeekValue: '1',
	hasRownumberer: false,
	/**查询栏包含部门选项*/
	hasHRDept: true,
	/**查询栏包含周选项*/
	hasWeeked: true,
	/**部门列显示*/
	hiddenDept: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**姓名列显示*/
	hiddenCName: true,
	Type: 2,
	columnLines: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		Ext.QuickTips.init();
		Ext.override(Ext.ToolTip, {
			maxWidth: 680
		});
	},
	getAStyle: function(color) {
		return "<a style='color:" + color + "'; data-qtip='";
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
	defaultButtonToolbarItems: function() {
		var me = this;
		me.buttonToolbarItems = [];
		if(me.hasRefresh) {
			me.buttonToolbarItems.push('refresh', '-');
		}
		me.buttonToolbarItems.push({
			width: 80,
			xtype: 'uxYearComboBox',
			itemId: 'Year',
			minValue: 2016, 
			value: me.defaultYearValue
		}, {
			width: 70,
			xtype: 'uxMonthComboBox',
			itemId: 'Month',
			value: me.defaultMonthValue,
			margin: '0 2px 0 10px',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != null && newValue != "") {
						me.onSearch();
					}
				}
			}
		});
		if(me.hasWeeked) {
			me.buttonToolbarItems.push({
				width: 80,
				fieldLabel: '',
				xtype: 'uxSimpleComboBox',
				itemId: 'Week',
				hasStyle: true,
				value: me.defaultWeekValue,
				data: me.DateTimeList,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(newValue && newValue != null && newValue != "") {
							me.onSearch();
						}
					}
				}
			});
		}
		me.buttonToolbarItems.push({
			width: 200,
			labelWidth: 50,
			labelAlign: 'right',
			hidden: !me.hasHRDept,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'HRDeptCName',
			displayField: 'DeptName',
			fieldLabel: "部门",
			valueField: 'DeptId',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.filterFn(newValue);
					me.mergeCells(me, [1]);
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
	/**初始化功能按钮栏内容*/
	initButtonToolbarItems: function() {
		var me = this;
		me.defaultButtonToolbarItems();
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
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		if(me.hiddenDept) {
			columns.push({
				text: '部门',
				dataIndex: 'DeptName',
				width: 100,
				sortable: false,
				menuDisabled: true,
				defaultRenderer: true
			})
		}
		if(me.hiddenCName) {
			columns.push({
				text: '姓名',
				dataIndex: 'EmpName',
				width: 100,
				sortable: false,
				menuDisabled: true,
				defaultRenderer: true
			})
		}
		columns.push({
			text: '部门Id',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			dataIndex: 'DeptId',
			width: 85 //isDate:true
		}, {
			text: '星期一',
			dataIndex: 'Monday',
			width: 110,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Monday');
			}
		}, {
			text: '星期二',
			dataIndex: 'Tuesday',
			width: 110,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Tuesday');
			}
		}, {
			text: '星期三',
			dataIndex: 'Wednesday',
			width: 110,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Wednesday');
			}
		}, {
			text: '星期四',
			dataIndex: 'Thursday',
			width: 110,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Thursday');
			}
		}, {
			text: '星期五',
			dataIndex: 'Friday',
			sortable: false,
			width: 110,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Friday');
			}
		}, {
			text: '星期六',
			dataIndex: 'Saturday',
			width: 110,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Saturday');
			}
		}, {
			text: '星期日',
			dataIndex: 'Sunday',
			width: 110,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.getMetaValue(value, meta, record, rowIndex, colIndex, store, view, 'Sunday');
			}
		}, {
			text: '日期',
			dataIndex: 'ATEventDateCode',
			width: 110,
			hidden: true,
			sortable: false,
			menuDisabled: true
		});
		return columns;
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			buttonsToolbar = me.getComponent('buttonsToolbar');
		Year = buttonsToolbar.getComponent('Year').getValue(),
			Month = buttonsToolbar.getComponent('Month').getValue();

		var HRDeptCName = buttonsToolbar.getComponent('HRDeptCName');
		HRDeptCName.setValue('');

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		if(me.hasWeeked) {
			var Week = buttonsToolbar.getComponent('Week').getValue();
			var weekdate = JcallShell.Date.getWeekStartDateAndEndDate(Year, Month, Week);
			if(weekdate.StartDate != '' && weekdate.EndDate != '') {
				url += '?Type=' + me.Type + '&StartDate=' + weekdate.StartDate + '&EndDate=' + weekdate.EndDate;
			}
		}
		return url;
	},
	/**初始化时间*/
	initDate: function() {
		var me = this,
			date = JShell.System.Date.getDate(),
			weeks = JShell.Date.getMonthTotalWeekByDate(date);
		var year = date.getFullYear();
		var month = date.getMonth() + 1;
		me.defaultYearValue = year;
		me.defaultMonthValue = month;
		me.defaultWeekValue = JShell.Date.getMonthWeekByDate(date) + ''; //周
		//周列表
		me.DateTimeList = [];
		for(var i = 0; i < weeks; i++) {
			me.DateTimeList.push([(1 + i) + '', '第' + (i + 1) + '周']);
		}
	},
	filterFn: function(value) {
		var me = this,
			valtemp = value;
		var store = me.getStore();
		if(!valtemp) {
			store.clearFilter();
			return
		}
		valtemp = String(value).trim().split(" ");
		store.filterBy(function(record, id) {
			var data = record.data;
			var DeptId = record.data.DeptId;
			var DeptName = record.data.DeptName;

			var dataarr = {
				DeptId: DeptId,
				DeptName: DeptName
			};
			for(var p in dataarr) {
				var porp = Ext.util.Format.lowercase(String(dataarr[p]));
				for(var i = 0; i < valtemp.length; i++) {
					var macther = valtemp[i];
					var macther2 = Ext.escapeRe(macther);
					mathcer = new RegExp(macther2);
					if(mathcer.test(porp)) {
						return true
					}
				}
			}
			return false
		})
	},

	mergeCells: function(grid, rows) {},
	createNullModel: function() {
		var me = this;
		var model = {
			"DeptName": "",
			"DeptId": "",
			"EmpName": "",
			"EmpId": "",
			"ATEventDateCode": "",
			Monday: "",
			Tuesday: "",
			Wednesday: "",
			Thursday: "",
			Friday: "",
			Saturday: "",
			Sunday: "",
			Sign: '',
			Leave: '',
			Egress: '',
			Trip: '',
			Overtime: '',
			"ToDayContent": '',
			"NextDayContent": '',
			ATEmpAttendanceEventParaSettings:''
		};
		return model;
	},
	changeValue: function(value, meta, record, rowIndex, colIndex, store, view, weekDay) {
		var me = this;

		return value;
	}
});