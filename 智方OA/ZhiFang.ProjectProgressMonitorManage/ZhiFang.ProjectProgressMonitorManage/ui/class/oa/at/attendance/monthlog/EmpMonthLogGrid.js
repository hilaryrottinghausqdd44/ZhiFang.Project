/**
 * 员工月考勤查看
 * @author longfc
 * @version 2016-07-27
 */
Ext.define('Shell.class.oa.at.attendance.monthlog.EmpMonthLogGrid', {
	extend: 'Shell.class.oa.at.attendance.basic.Grid',
	title: '员工月考勤信息',

	/**获取数据服务路径*/
	selectUrl: "/WeiXinAppService.svc/GetEmpMonthLog",
	downLoadExcelUrl: "/WeiXinAppService.svc/SC_UDTO_DownLoadExportExcelOfAllMonthLogCount",
	/**查询月份*/
	MONTTCODE: JShell.Date.getDate(new Date()).getMonth() + 1,
	/**登录员工*/
	EMPID: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1,
	//	EMPID: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**默认加载数据*/
	defaultLoad: true,
	/**查询栏包含部门选项*/
	hasHRDept: false,
	/**查询栏包含周选项*/
	hasWeeked: false,
	/**部门列显示*/
	hiddenDept: false,
	/**姓名列显示*/
	hiddenCName: false,
	/**默认排序字段*/
	defaultOrderBy: [],
	/*数据范围部门：depth，公司：all*/
	datarangetype: "",
	/*是否有导出功能*/
	hasownLoadExcel: false,
	/**初始化功能按钮栏内容*/
	initButtonToolbarItems: function() {
		var me = this;
		me.defaultButtonToolbarItems();
		//需要添加导出统计处理
		if(me.hasownLoadExcel == true) {
			me.buttonToolbarItems.push("-");
			me.buttonToolbarItems.push({
				xtype: "numberfield",
				itemId: "wagesDays",
				name: "wagesDays",
				labelWidth: 45,
				width: 115,
				minValue: 0,
				maxValue: 31,
				value: 21,
				fieldLabel: "工资日",
				tooltip: '<b>每月的工资日总天数</b>',
				style: {
					marginLeft: '10px'
				}
			}, {
				xtype: "numberfield",
				itemId: "punchCount",
				name: "punchCount",
				width: 145,
				minValue: 1,
				value: 1,
				maxValue: 6,
				labelWidth: 80,
				tooltip: '<b>每天签到的打卡次数</b>',
				fieldLabel: "每天打卡次数"
			}, {
				width: 55,
				iconCls: 'file-excel hand',
				margin: '0 0 0 10px',
				xtype: 'button',
				text: '导出',
				tooltip: '<b>查询导出公司所有员工的考勤统计报表</b>',
				handler: function() {
					me.onDownLoadExcel();
				}
			}, "->");
		}
	},
	/**导出EXCEL文件*/
	onDownLoadExcel: function() {
		var me = this,
			monthCode = "",
			wagesDays = 0,
			punch = 0,
			operateType = '0'; //直接下载(0),直接打开(1)，默认0
		buttonsToolbar = me.getComponent('buttonsToolbar');
		var year = buttonsToolbar.getComponent('Year').getValue();
		var month = buttonsToolbar.getComponent('Month').getValue();
		var wagesDays = buttonsToolbar.getComponent('wagesDays').getValue();
		var punchCount = buttonsToolbar.getComponent('punchCount').getValue();
		var showInfo = "";
		var isExec = true;
		if(year == "" || year == null) {
			showInfo = showInfo + "年份选择不能为空!<br />";
			isExec = false;
		}
		if(isExec == true && (month == "" || month == null)) {
			showInfo = showInfo + "月份选择不能为空!<br />";
			isExec = false;
		}
		if(isExec == true && (wagesDays == "" || wagesDays == null)) {
			showInfo = showInfo + "工资日不能为空!<br />";
			isExec = false;
		}
		if(isExec == true && (punchCount == "" || punchCount == null)) {
			showInfo = showInfo + "打卡次数不能为空!<br />";
			isExec = false;
		}

		if(year != "" && year != null && month != "" && month != null) {
			monthCode = year + "-" + month;
		}
		if(isExec) {
			var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;
			url += "?operateType=" + operateType + "&monthCode=" + monthCode + "&wagesDays=" + wagesDays + "&punch=" + punchCount;
			window.open(url);
		} else {
			JShell.Msg.alert(showInfo, null, 2000);
		}
	},
	getDates: function(startDate, stopDate) {
		var dateArray = [];
		var currentDate = startDate;
		currentDate = JcallShell.Date.getDate(currentDate);
		stopDate = JcallShell.Date.getDate(stopDate);
		while(currentDate <= stopDate) {
			dateArray.push(Ext.util.Format.date(currentDate, 'Y-m-d'));
			currentDate = currentDate.addDays(1);
		}
		return dateArray;
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
		model.Overtime = [];
		buttonsToolbar = me.getComponent('buttonsToolbar');
		var year = buttonsToolbar.getComponent('Year').getValue();
		var month = buttonsToolbar.getComponent('Month').getValue();
		var tempDate = year + "-" + month + "-01";
		var countDays = JcallShell.Date.getCountDays(tempDate);
		for(var key in data.value) {
			tempArr = data.value[key];

			type = Ext.typeOf(tempArr);
			switch(key) {
				case 'SignList': //签到签退
					if(type == "array") {
						//考勤设置信息
						var setting = data.value["ATEmpAttendanceEventParaSettings"];
						if(setting == undefined)
							setting = "";
						if(setting && setting != null && setting != "") {
							if(setting.SignInTime != null && setting.SignInTime != "")
								setting.SignInTime = Ext.util.Format.date(setting.SignInTime, 'H:i');
							if(setting.SignOutTime != null && setting.SignOutTime != "")
								setting.SignOutTime = Ext.util.Format.date(setting.SignOutTime, 'H:i');
						}
						Ext.Array.forEach(tempArr, function(obj, index, array) {
							ATEventDateCode = obj.ATEventDateCode;
							ATEventDateCode = Ext.util.Format.date(ATEventDateCode, 'Y-m-d');
							var indexOf = tempArrATEventDateCode.indexOf(Ext.util.Format.date(ATEventDateCode, 'Y-m-d'));
							var weekDay = me.changeweekDay(obj.ATEventDateCode);
							model = me.createNullModel();
							model.Overtime = [];
							var sign = obj;
							if(sign == null) {
								sign = {};
							}
							sign.WeekDay = weekDay;
							model.EmpId = obj.EmpId;
							model.EmpName = obj.EmpName;
							model.ATEventDateCode = Ext.util.Format.date(ATEventDateCode, 'Y-m-d');
							model.ATEmpAttendanceEventParaSettings = setting;
							model.Sign = sign;
							if(indexOf == -1) { //不存在list中
								tempArrATEventDateCode.push(Ext.util.Format.date(ATEventDateCode, 'Y-m-d'));
								list.push(model);
							} else { //已存在list中
								Ext.Array.forEach(list, function(tempObj, index, array) {
									if(tempObj.ATEventDateCode == ATEventDateCode) {
										tempObj.Sign = sign;
										list[tempObj] = tempObj;
									}
								});
							}

						});
					}
					break;
				case 'LeaveList': //请假
					if(type == "array") {

						Ext.Array.forEach(tempArr, function(obj, index, array) {
							var StartDateTime = obj.StartDateTime.replace(/\//g, "-");
							var EndDateTime = obj.EndDateTime.replace(/\//g, "-");

							var days = me.DateDiff(StartDateTime, EndDateTime);
							if(days > 0) {
								days = (days > countDays ? countDays : days);
								//请假天数循环处理	
								for(var i = 0; i < days; i++) {
									ATEventDateCode = JcallShell.Date.getNextDate(StartDateTime, i).toString();

									if(JcallShell.Date.isValid(ATEventDateCode) == true) {
										ATEventDateCode = Ext.util.Format.date(ATEventDateCode, 'Y-m-d');
										var indexOf = tempArrATEventDateCode.indexOf(ATEventDateCode);
										var weekDay = me.changeweekDay(JcallShell.Date.getNextDate(StartDateTime, i).toString());
										model = me.createNullModel();
										model.Overtime = [];
										model.EmpId = obj.EmpId;
										model.EmpName = obj.EmpName;
										var leave = obj;
										if(leave == null) {
											leave = {
												ApproveStatusID: "",
												ATEventSubTypeName: "",
												WeekDay: weekDay
											};
										}
										leave.WeekDay = weekDay;
										model.ATEventDateCode = ATEventDateCode;
										model.Leave = leave; //Ext.JSON.encode(leave).toString();
										if(indexOf == -1) { //不存在
											tempArrATEventDateCode.push(ATEventDateCode);
											list.push(model);
										} else {
											Ext.Array.forEach(list, function(tempObj, index, array) {
												if(tempObj.ATEventDateCode == ATEventDateCode) {
													tempObj.Leave = leave;
													list[tempObj] = tempObj;
												}
											});
										}

									}
									var isMonthLastDay = JcallShell.Date.isMonthLastDay(ATEventDateCode);
									if(isMonthLastDay)
										break;
								}
							}
						});
					}
					break;
				case 'EgressList': //外出
					if(type == "array") {
						Ext.Array.forEach(tempArr, function(obj, index, array) {
							var StartDateTime = obj.StartDateTime.replace(/\//g, "-");
							var EndDateTime = obj.EndDateTime.replace(/\//g, "-");
							if(StartDateTime.indexOf(year) == -1)
								StartDateTime = year + "-" + StartDateTime;
							if(EndDateTime.indexOf(year) == -1)
								EndDateTime = year + "-" + EndDateTime;

							var days = me.DateDiff(StartDateTime, EndDateTime);
							if(days < 1)
								days = 1;
							if(days > 0) {
								days = (days > countDays ? countDays : days);
								//外出天数循环处理	
								for(var i = 0; i < days; i++) {
									ATEventDateCode = JcallShell.Date.getNextDate(StartDateTime, i).toString();
									if(JcallShell.Date.isValid(ATEventDateCode) == true) {
										ATEventDateCode = Ext.util.Format.date(ATEventDateCode, 'Y-m-d');
										var indexOf = tempArrATEventDateCode.indexOf(ATEventDateCode);
										var weekDay = me.changeweekDay(JcallShell.Date.getNextDate(StartDateTime, i).toString());
										model = me.createNullModel();
										model.Overtime = [];
										model.EmpId = obj.EmpId;
										model.EmpName = obj.EmpName;
										var egress = obj;
										if(egress == null) {
											egress = {
												ApproveStatusID: obj.ApproveStatusID,
												ATEventSubTypeName: obj.ATEventSubTypeName,
												ATEventSubTypeName: obj.ATEventSubTypeName,
												WeekDay: weekDay
											};
										}
										egress.weekDay = weekDay;
										model.ATEventDateCode = ATEventDateCode;
										model.Egress = egress;

										if(indexOf == -1) { //不存在
											tempArrATEventDateCode.push(ATEventDateCode);
											list.push(model);
										} else {
											Ext.Array.forEach(list, function(tempObj, index, array) {
												if(tempObj.ATEventDateCode == ATEventDateCode) {
													tempObj.Egress = egress;
													list[tempObj] = tempObj;
												}
											});
										}
										var isMonthLastDay = JcallShell.Date.isMonthLastDay(ATEventDateCode);
										if(isMonthLastDay)
											break;
									}
								}
							}
						});
					}
					break;
				case 'TripList': //出差
					if(type == "array") {
						Ext.Array.forEach(tempArr, function(obj, index, array) {
							var StartDateTime = obj.StartDateTime.replace(/\//g, "-");
							var EndDateTime = obj.EndDateTime.replace(/\//g, "-");
							var days = me.DateDiff(StartDateTime, EndDateTime);
							if(days > 0) {
								days = (days > countDays ? countDays : days);
								//出差天数循环处理	
								for(var i = 0; i < days; i++) {
									ATEventDateCode = JcallShell.Date.getNextDate(StartDateTime, i).toString();
									if(JcallShell.Date.isValid(ATEventDateCode) == true) {
										ATEventDateCode = Ext.util.Format.date(ATEventDateCode, 'Y-m-d');
										var indexOf = tempArrATEventDateCode.indexOf(ATEventDateCode);
										var weekDay = me.changeweekDay(JcallShell.Date.getNextDate(StartDateTime, i).toString());
										model = me.createNullModel();
										model.Overtime = [];
										model.EmpId = obj.EmpId;
										model.EmpName = obj.EmpName;
										var trip = obj;
										if(trip == null) {
											trip = {
												ApproveStatusID: obj.ApproveStatusID,
												ATEventSubTypeName: obj.ATEventSubTypeName,
												WeekDay: weekDay
											};
										}
										trip.WeekDay = weekDay;
										model.ATEventDateCode = ATEventDateCode;
										model.Trip = trip;
										if(indexOf == -1) { //不存在
											tempArrATEventDateCode.push(ATEventDateCode);
											list.push(model);
										} else {
											Ext.Array.forEach(list, function(tempObj, index, array) {
												if(tempObj.ATEventDateCode == ATEventDateCode) {
													tempObj.Trip = trip;
													list[tempObj] = tempObj;
												}
											});
										}
										var isMonthLastDay = JcallShell.Date.isMonthLastDay(ATEventDateCode);
										if(isMonthLastDay)
											break;
									}
								}
							}

						});
					}
					break;
				case 'OvertimeList': //加班
					if(type == "array") {
						var overtimeList = [];
						Ext.Array.forEach(tempArr, function(obj, index, array) {
							var StartDateTime = year + "-" + obj.StartDateTime.replace(/\//g, "-");
							var EndDateTime = year + "-" + obj.EndDateTime.replace(/\//g, "-");
							var days = me.DateDiff(StartDateTime, EndDateTime);

							if(days > 0) {
								days = (days > countDays ? countDays : days);
								//加班天数循环处理	
								for(var i = 0; i < days; i++) {
									ATEventDateCode = JcallShell.Date.getNextDate(StartDateTime, i).toString();
									if(JcallShell.Date.isValid(ATEventDateCode) == true) {
										ATEventDateCode = Ext.util.Format.date(ATEventDateCode, 'Y-m-d');
										var indexOf = tempArrATEventDateCode.indexOf(ATEventDateCode);
										var weekDay = me.changeweekDay(JcallShell.Date.getNextDate(StartDateTime, i).toString());
										model = me.createNullModel();
										model.Overtime = [];
										model.EmpId = obj.EmpId;
										model.EmpName = obj.EmpName;
										var overtime = obj;
										if(overtime == null) {
											overtime = {
												ApproveStatusID: obj.ApproveStatusID,
												ATEventSubTypeName: obj.ATEventSubTypeName,
												WeekDay: weekDay,
												EvenLengthUnit: ''
											};
										}
										overtime.WeekDay = weekDay;
										model.ATEventDateCode = ATEventDateCode;

										if(indexOf == -1) { //不存在
											tempArrATEventDateCode.push(ATEventDateCode);
											model.Overtime.push(overtime);
											list.push(model);
										} else {
											Ext.Array.forEach(list, function(tempObj, index, array) {
												if(tempObj.ATEventDateCode == ATEventDateCode) {
													tempObj.Overtime.push(overtime);
													list[tempObj] = tempObj;

												}
											});
										}
										var isMonthLastDay = JcallShell.Date.isMonthLastDay(ATEventDateCode);
										if(isMonthLastDay)
											break;
									}
								}
							}

						});
					}
					break;
				default:
					model.DeptName = info;
					break;
			}

		}
		result.list = list;
		var nullRecords = me.createNullRecords();
		//作第三次转换处理
		result = me.createRecords(result, nullRecords);
		return result;
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
	 * 计算天数差的函数，通用  
	 * */
	DateDiff: function(sDate1, sDate2) {
		var aDate, oDate1, oDate2, iDays = 0;
		if(sDate1 && sDate1.toString().length > 10) {
			sDate1 = sDate1.substring(0, 10);
		}
		if(sDate2 && sDate2.toString().length > 10) {
			sDate2 = sDate2.substring(0, 10);
		}
		if(JcallShell.Date.isValid(sDate1) == true && JcallShell.Date.isValid(sDate2) == true) {
			var myDate = new Date(Date.parse(sDate1.replace(/-/g, "/")));
			var myDate2 = new Date(Date.parse(sDate2.replace(/-/g, "/")));
			var datetime = myDate2.getTime() - myDate.getTime();
			iDays = Math.floor(datetime) / (24 * 3600 * 1000); //把相差的毫秒数转换为天数  
			if(iDays > -1) {
				iDays = iDays + 1;
			}
		}
		return iDays;
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		buttonsToolbar = me.getComponent('buttonsToolbar');
		var year = buttonsToolbar.getComponent('Year').getValue();
		var month = buttonsToolbar.getComponent('Month').getValue();
		if(year != "" && year != null && month != "" && month != null) {
			me.MONTTCODE = year + "-" + month;
		}
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//员工Id
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'EmpId=' + me.EMPID;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'MonthCode=' + me.MONTTCODE;
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
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";

		if(where) {
			url += '&where=' + JShell.String.encode(where);
		}

		return url;
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
		var addDays = 0;
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
			model.Overtime = [];
			if(day > countDays) {
				break;
			}
			for(var j = colIndex; j < 7; j++) {
				day = day + 1;
				if(day > countDays) {
					break;
				}
				var ATEventDateCode = year + "-" + month + "-" + day;
				var weekDay = me.changeweekDay(ATEventDateCode);
				var obj = {
					Sign: "",
					Overtime: [],
					Egress: "",
					Trip: "",
					Leave: "",
					Day: day,
					WeekDay: weekDay, //星期几
					MonthDay: month + "-" + day, //月和日
					ATEventDateCode: ATEventDateCode //日期
				};
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
		//var ATEventDateCode = list[0].ATEventDateCode;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var year = buttonsToolbar.getComponent('Year').getValue();
		var month = buttonsToolbar.getComponent('Month').getValue();
		var ATEventDateCode = year + "-" + month + "-01";
		var firstWeekDay = me.changeweekDay(ATEventDateCode);
		var countDays = JcallShell.Date.getCountDays(ATEventDateCode);
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
				day = day + 1;
				if(day > countDays) {
					break;
				}
				weekDay = me.changeweekDay(record.ATEventDateCode);
				var obj = {
					Sign: record.Sign,
					Overtime: record.Overtime,
					Egress: record.Egress,
					Trip: record.Trip,
					Leave: record.Leave,
					Day: day, //第几天
					WeekDay: weekDay, //星期几
					MonthDay: month + "-" + day, //月和日
					ATEventDateCode: record.ATEventDateCode, //日期
					ATEmpAttendanceEventParaSettings: record.ATEmpAttendanceEventParaSettings //员工考勤设置信息
				};
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
	},
	/*
	 * 
	 * @param {Object} value
	 * @param {Object} meta
	 * @param {Object} record
	 * @param {Object} rowIndex
	 * @param {Object} colIndex
	 * @param {Object} store
	 * @param {Object} view
	 * @param {Object} weekDay 星期几
	 */
	changeValue: function(value, meta, record, rowIndex, colIndex, store, view, weekDay) {
		var me = this;
		var showValue = "",
			qtipValue = "", //提示信息
			DateCode = "",
			SignInType = "",
			SignInTime = "",
			SignOutTime = "",
			overtimeStr = "",
			egressStr = "",
			tripStr = "",
			leaveStr = "",
			absentStr = ""; //旷工
		//var aStr = "<a style='color:green'; data-qtip='";
		var isShowAbsent = false; //是否显示旷工

		var weekDayInfo = record.get(weekDay);
		if(weekDayInfo && weekDayInfo != "" && weekDayInfo != null) {
			weekDayInfo = Ext.JSON.decode(weekDayInfo);
		} else {
			weekDayInfo = {
				Sign: "",
				Overtime: [],
				Egress: "",
				Trip: "",
				Leave: "",
				Day: '',
				WeekDay: "",
				MonthDay: '',
				ATEventDateCode: ""
			};
		}
		//该月和第几天(如:2016-07-01)
		ATEventDateCode = weekDayInfo.ATEventDateCode;

		//签到签退
		var sign = weekDayInfo.Sign;
		if(sign != null && sign != "" && weekDayInfo.WeekDay == weekDay) {
			//考勤设置信息处理
			var setSignInTime = "",
				setSignOutTime = "",
				setATEventPostion = "",
				setATEventPostionName = "",
				setATEventPostionRange = "";
			if(weekDayInfo.ATEmpAttendanceEventParaSettings && weekDayInfo.ATEmpAttendanceEventParaSettings) {
				var setting = weekDayInfo.ATEmpAttendanceEventParaSettings;
				if(setting == undefined)
					setting = "";
				if(setting != "" && setting != null) {
					setSignInTime = setting.SignInTime;
					setSignOutTime = setting.SignOutTime;
					setATEventPostion = setting.ATEventPostion;
					setATEventPostionName = setting.ATEventPostionName;
					setATEventPostionRange = setting.ATEventPostionRange;
					if(setATEventPostionRange != "" && setATEventPostionRange != null)
						setATEventPostionRange = setATEventPostionRange + "米";
				}
			}
			if(sign.SignInTime) {
				//签到显示信息
				SignInTime = ((sign.SignInType != "" && sign.SignInType != null) ? ("<span style='color:red'>" + sign.SignInType + sign.SignInTime + "</span>") : ("签到" + sign.SignInTime));
				//签到提示信息
				qtipValue = '设置签到时间:' + setSignInTime + "<br />";
				qtipValue = qtipValue + "实际签到时间:" + sign.SignInTime + "<br />";
				qtipValue = qtipValue + "登记事件:" + sign.SignInType + "<br />";

				qtipValue = qtipValue + '设置考勤地点坐标:' + setATEventPostion + "<br />";
				qtipValue = qtipValue + "实际签到地点坐标:" + sign.SigninATEventLogPostion + "<br />";
				qtipValue = qtipValue + '设置签到考勤地点:' + setATEventPostionName + "<br />";
				qtipValue = qtipValue + "实际签到考勤地点:" + sign.SigninATEventLogPostionName + "<br />";
				qtipValue = qtipValue + '设置考勤地点范围:' + setATEventPostionRange + "<br />";
				qtipValue = qtipValue + "说明:" + sign.SignInMemo + "<br />";

				var color = ((sign.SignInType != "" && sign.SignInType != null) ? me.AttendanceColor.Red : me.AttendanceColor.SignIn);
				SignInTime = me.getAStyle(color) + qtipValue + "' >" + SignInTime + "</a>";
			}
			if(sign.SignOutTime) {
				//签退显示信息
				SignOutTime = ((sign.SignOutType != "" && sign.SignOutType != null) ? ("<span style='color:" + me.AttendanceColor.Red + "'>" + sign.SignOutType + sign.SignOutTime + "</span>") : ("签退" + sign.SignOutTime));
				//签退提示信息
				qtipValue = '设置签退时间:' + setSignOutTime + '<br />';
				qtipValue = qtipValue + "实际签退时间:" + sign.SignOutTime + "<br />";

				qtipValue = qtipValue + "登记事件:" + sign.SignOutType + "<br />";

				qtipValue = qtipValue + "设置考勤地点坐标:" + setATEventPostion + "<br />";
				qtipValue = qtipValue + "实际签退地点坐标:" + sign.SignoutATEventLogPostion + "<br />";
				qtipValue = qtipValue + "设置签退考勤地点:" + setATEventPostionName + "<br />";
				qtipValue = qtipValue + "实际签退考勤地点:" + sign.SignoutATEventLogPostionName + "<br />";
				qtipValue = qtipValue + "设置考勤地点范围:" + setATEventPostionRange + "<br />";
				qtipValue = qtipValue + "说明:" + sign.SignOutMemo + "<br />";
				var color = ((sign.SignOutType != "" && sign.SignOutType != null) ? me.AttendanceColor.Red : me.AttendanceColor.SignOut);
				SignOutTime = me.getAStyle(color) + qtipValue + "' >" + SignOutTime + "</a>";
			}
			//工作日旷工处理
			if(sign.IsWorkDay == true) {
				if(SignInTime == "" && SignOutTime == "") {
					isShowAbsent = true;
					absentStr = "<a style='color:" + me.AttendanceColor.Red + "'; data-qtip='旷工' >旷工</a>";
				}
			}
		}

		//加班处理
		var overtimeList = weekDayInfo.Overtime;

		if(overtimeList != null && overtimeList != "" && weekDayInfo.WeekDay == weekDay) {
			qtipValue = "";
			for(var k = 0; k < overtimeList.length; k++) {
				var overtime = overtimeList[k];

				var tempVvertimeStr = "<span style='color:" + me.AttendanceColor.Overt + "'>" + overtime.ATEventSubTypeName + "</span>";
				switch(overtime.ApproveStatusID) {
					case 1:
						tempVvertimeStr += "<span style='color:" + me.AttendanceColor.Overt + "'>(已获批)</span>";
						break;
					case 2:
						tempVvertimeStr += "<span style='color:" + me.AttendanceColor.Red + "'>(退回)</span>";
						break;
					default:
						tempVvertimeStr += "<span style='color:" + me.AttendanceColor.Overt + "'>(未获批)</span>";
						break;
				}
				//加班提示信息
				qtipValue = "开始时间:" + overtime.StartDateTime + "<br />";
				qtipValue = qtipValue + "结束时间:" + overtime.EndDateTime + "<br />";
				qtipValue = qtipValue + "时长:" + overtime.EvenLength.toString() + overtime.EvenLengthUnit + "<br />";
				qtipValue = qtipValue + "说明:" + overtime.Memo + "<br />";
				if(overtimeList.length > 1) {
					overtimeStr = overtimeStr + me.getAStyle(me.AttendanceColor.Overt) + qtipValue + "' >" + tempVvertimeStr + "</a>";
					if(k < overtimeList.length-1)
						overtimeStr=overtimeStr + "<br />"
				} else {
					overtimeStr = me.getAStyle(me.AttendanceColor.Overt) + qtipValue + "' >" + tempVvertimeStr + "</a>";
				}
			}
		}

		//外出
		var egress = weekDayInfo.Egress;
		if(egress != null && egress != "" && weekDayInfo.WeekDay == weekDay) {
			egressStr = "<span style='color:green'>" + egress.ATEventSubTypeName + "</span>";
			switch(egress.ApproveStatusID) {
				case 1:
					egressStr += "<span style='color:" + me.AttendanceColor.Egress + "'>(已获批)</span>";
					break;
				case 2:
					egressStr += "<span style='color:" + me.AttendanceColor.Red + "'>(退回)</span>";
					break;
				default:
					egressStr += "<span style='color:" + me.AttendanceColor.Egress + "'>(未获批)</span>";
					break;
			}
			qtipValue = "申请时间:" + egress.DataAddTime + "<br />";
			qtipValue = qtipValue + "开始时间:" + egress.StartDateTime + "<br />";
			qtipValue = qtipValue + "结束时间:" + egress.EndDateTime + "<br />";
			qtipValue = qtipValue + "说明:" + egress.Memo + "<br />";
			egressStr = me.getAStyle(me.AttendanceColor.Egress) + qtipValue + "' >" + egressStr + "</a>";
		}
		//出差
		var trip = weekDayInfo.Trip;
		if(trip != null && trip != "" && weekDayInfo.WeekDay == weekDay) {
			tripStr = "<span style='color:" + me.AttendanceColor.Trip + "'>" + trip.ATEventSubTypeName + "</span>";
			switch(trip.ApproveStatusID) {
				case 1:
					tripStr += "<span style='color:" + me.AttendanceColor.Trip + "'>(已获批)</span>";
					break;
				case 2:
					tripStr += "<span style='color:" + me.AttendanceColor.Red + "'>(退回)</span>";
					break;
				default:
					tripStr += "<span style='color:" + me.AttendanceColor.Trip + "'>(未获批)</span>";
					break;
			}
			qtipValue = "申请时间:" + trip.DataAddTime + "<br />";
			qtipValue = qtipValue + "开始时间:" + trip.StartDateTime + "<br />";
			qtipValue = qtipValue + "结束时间:" + trip.EndDateTime + "<br />";
			qtipValue = qtipValue + "说明:" + trip.Memo + "<br />";
			tripStr = me.getAStyle(me.AttendanceColor.Trip) + qtipValue + "' >" + tripStr + "</a>";

		}
		//请假
		var leave = weekDayInfo.Leave;
		if(leave != null && leave != "" && weekDayInfo.WeekDay == weekDay) {
			leaveStr = "<span style='color:" + me.AttendanceColor.Leave + "'>" + leave.ATEventSubTypeName + "</span>";
			switch(leave.ApproveStatusID) {
				case 1:
					leaveStr += "<span style='color:" + me.AttendanceColor.Leave + "'>(已获批)</span>";
					break;
				case 2:
					leaveStr += "<span style='color:" + me.AttendanceColor.Red + "'>(退回)</span>";
					break;
				default:
					leaveStr += "<span style='color:" + me.AttendanceColor.Leave + "'>(未获批)</span>";
					break;
			}
			qtipValue = "申请时间:" + leave.DataAddTime + "<br />";
			qtipValue = qtipValue + "请假类别:" + leave.ATEventSubTypeName + "<br />";
			qtipValue = qtipValue + "开始时间:" + leave.StartDateTime + "<br />";
			qtipValue = qtipValue + "结束时间:" + leave.EndDateTime + "<br />";
			qtipValue = qtipValue + "说明:" + leave.Memo + "<br />";
			leaveStr = me.getAStyle(me.AttendanceColor.Leave) + qtipValue + "' >" + leaveStr + "</a>";
		}
		//第几天
		var monthDay = "";
		if(weekDayInfo.MonthDay != "") {
			monthDay = "<a>" + weekDayInfo.MonthDay + "</a>";
		}
		if(SignInTime != "") {
			isShowAbsent = false;
			showValue = showValue + SignInTime + "<br />";
		}
		if(SignOutTime != "") {
			isShowAbsent = false;
			showValue = showValue + SignOutTime + "<br />";
		}

		if(overtimeStr != "") {
			isShowAbsent = false;
			showValue = showValue + overtimeStr + "<br />";
		}
		if(egressStr != "") {
			isShowAbsent = false;
			showValue = showValue + egressStr + "<br />";
		}
		if(tripStr != "") {
			isShowAbsent = false;
			showValue = showValue + tripStr + "<br />";
		}
		if(leaveStr != "") {
			isShowAbsent = false;
			showValue = showValue + leaveStr + "<br />";
		}
		//旷工信息处理:ATEventDateCode必须是大于今天之前的日期才是旷工
		if(isShowAbsent) {
			var date = new Date();
			var year = date.getFullYear();
			var month = date.getMonth() + 1;
			var day = date.getDate();
			var crrDate = new Date(year, month, day);

			var arrTemp = ATEventDateCode.split('-');
			var oDate2 = new Date(arrTemp[0], arrTemp[1], arrTemp[2]);
			if(oDate2.getTime() <= crrDate.getTime()) {
				showValue = showValue + absentStr + "<br />";
			}
		}

		var result = "<table style='padding:2px 2px;vertical-align:top;width:100%;border-bottom-style:solid; border-bottom-width:0px; border-color:darkgrey;font-weight:bold;' border='0'>" +
			"<tr style='vertical-align:top;margin:2px;width:100%;'>" +
			"<td colspan='4' style='vertical-align:top;margin:2px;width:100%;'>" +
			monthDay +
			"</td>" +
			"</tr>" +
			"<tr style='vertical-align:top;margin:2px;width:100%;'>" +
			"<td colspan='4'>" +
			showValue +
			"</td>" +
			"</tr>" +
			"</table>";
		//if(result) meta.tdAttr = 'data-qtip="<b>' + qtipValue + '</b>"';

		return result;
	}
});