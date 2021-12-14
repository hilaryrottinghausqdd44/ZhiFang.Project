/**
 * 周考勤列表
 * @author liangyl
 * @version 2016-07-27
 */
Ext.define('Shell.class.oa.at.attendance.week.Grid', {
	extend: 'Shell.class.oa.at.attendance.basic.Grid',
	title: '公司周考勤',
	defaultLoad: true,
	Type: 1,
	EndDate: '',
	hiddenDept: true,
	hiddenCName: true,
	DeptDateList: [],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		me.getView().on("refresh", function() {
			me.changDateText();
           
		});
		me.getStore().on('load', function() {
			me.mergeCells(me, [1]);
			var buttonsToolbar = me.getComponent('buttonsToolbar');
			var HRDeptCName = buttonsToolbar.getComponent('HRDeptCName');
			var depDate = JcallShell.Array.unique(me.DeptDateList);
			var depstore = new Ext.data.SimpleStore({
				fields: ["DeptId", "DeptName"],
				data: depDate
			});
			HRDeptCName.store = depstore;
			HRDeptCName.bindStore(HRDeptCName.store);
		});
	},
	StartDateAndEndDate: function() {
		var me = this,
			weekdate = {};
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		Year = buttonsToolbar.getComponent('Year').getValue(),
			Month = buttonsToolbar.getComponent('Month').getValue();
		if(me.hasWeeked) {
			var Week = buttonsToolbar.getComponent('Week').getValue();
			weekdate = JcallShell.Date.getWeekStartDateAndEndDate(Year, Month, Week);
		}
		return weekdate;
	},
	changDateText: function() {
		var me = this;
		var colIndex = 0;
		if(me.hiddenDept == true && me.hiddenCName == true) {
			colIndex = colIndex + 2;
		}
		if(me.hiddenCName == true && me.hiddenDept == false) {
			colIndex = colIndex + 0;
		}
		var EndDate = '',
			StartDate = '',
			DateCode = '1';
		var weekdate = me.StartDateAndEndDate();
		if(weekdate.StartDate != '' && weekdate.EndDate != '') {
			EndDate = weekdate.EndDate;
			StartDate = weekdate.StartDate;
		}
		var StartD =  JcallShell.Date.getDate(StartDate);
		var NextDate = '',
			weekText = '';
		var currDate = StartD;
		for(var i = 0; i <= 6; i++) {
			switch(i) {
				case 0:
					weekText = '星期一';
					break;
				case 1:
					weekText = '星期二';
					break;
				case 2:
					weekText = '星期三';
					break;
				case 3:
					weekText = '星期四';
					break;
				case 4:
					weekText = '星期五';
					break;
				case 5:
					weekText = '星期六';
					break;
				case 6:
					weekText = '星期日';
					break;
				default:
					break;
			}
			var weekDays =  JcallShell.Date.getDate(currDate).getDay();
			var currDate2 =  JcallShell.Date.getDate(StartD);
			currDate =  JcallShell.Date.getNextDate(currDate2, i);
			NextDate = Ext.util.Format.date(currDate, 'Y/m/d');
			me.columns[colIndex + i + 1].setText(NextDate + '<br/>' + weekText); //	
		}
	},
	//签到迟到早退
	SignList: function(dayData, WeekDay) {
		var me = this;
		var sign = {
			SignInTime: '',
			SignInType: '',
			SignOutTime: '',
			SignOutType: '',
			WeekDay: '',
			SignOutMemo: '',
			SignInMemo: '',
			ATEventDateCode: '',
			SignInId: '',
			SigninATEventLogPostion: '',
			SigninATEventLogPostionName: ''
		};
		if(dayData.SignList.SignInTime != null) {
			sign.SignInTime = dayData.SignList.SignInTime;
		}
		sign.SignOutTime = dayData.SignList.SignOutTime == null ? '' : dayData.SignList.SignOutTime;
		sign.SignOutType = dayData.SignList.SignOutType == null ? '' : dayData.SignList.SignOutType;
		sign.SignInType = dayData.SignList.SignInType == null ? '' : dayData.SignList.SignInType;
		sign.SignOutMemo = dayData.SignList.SignOutMemo == null ? '' : dayData.SignList.SignOutMemo;
		sign.SignInMemo = dayData.SignList.SignInMemo == null ? '' : dayData.SignList.SignInMemo;
		sign.WeekDay = WeekDay;
		sign.ATEventDateCode = dayData.SignList.ATEventDateCode == null ? '' : dayData.SignList.ATEventDateCode;

		sign.SigninATEventLogPostion = dayData.SignList.SigninATEventLogPostion == null ? '' : dayData.SignList.SigninATEventLogPostion;
		sign.SigninATEventLogPostionName = dayData.SignList.SigninATEventLogPostionName == null ? '' : dayData.SignList.SigninATEventLogPostionName;

		return sign;
	},
	createObj: function() {
		var me = this;
		var obj = {
			StartDateTime: '',
			EndDateTime: '',
			Memo: '',
			ATEventSubTypeName: '',
			DataAddTime: '',
			WeekDay: '',
			ApproveName: '',
			EvenLength: '',
			EvenLengthUnit: '',
			ATEventTypeName: '',
			ApproveStatusID: '',
			ApplyEmp: ''
		};
		return obj;
	},
	//加班
	OvertimeList: function(dayData, WeekDay) {
		var me = this;
		var overtime = me.createObj();
		var OvertimeArr = dayData.OvertimeList;
		var type = Ext.typeOf(OvertimeArr);

		if(type == 'array') {
			Ext.Array.forEach(OvertimeArr, function(obj, index, array) {
				overtime.StartDateTime = obj.StartDateTime;
				overtime.EndDateTime = obj.EndDateTime;
				overtime.Memo = obj.Memo;
				overtime.ATEventSubTypeName = obj.ATEventSubTypeName;
				overtime.ATEventTypeName = obj.ATEventTypeName;
				overtime.ApproveStatusID = obj.ApproveStatusID;
				overtime.ApproveName = obj.ApproveName;
				overtime.EvenLength = obj.EvenLength;
				overtime.DataAddTime = obj.DataAddTime;
				overtime.EvenLengthUnit = obj.EvenLengthUnit;
				overtime.WeekDay = WeekDay;
			});
		}
		return overtime;
	},
	//请假
	LeaveList: function(dayData, WeekDay) {
		var me = this;
		var leave = me.createObj();
		var LeaveArr = dayData.LeaveList;
		var type = Ext.typeOf(LeaveArr);
		if(type == 'array') {
			Ext.Array.forEach(LeaveArr, function(obj, index, array) {
				leave.StartDateTime = obj.StartDateTime;
				leave.EndDateTime = obj.EndDateTime;
				leave.Memo = obj.Memo;
				leave.ATEventSubTypeName = obj.ATEventSubTypeName;
				leave.ATEventTypeName = obj.ATEventTypeName;
				leave.ApproveStatusID = obj.ApproveStatusID;
				leave.ApproveName = obj.ApproveName;
				leave.WeekDay = WeekDay;
				leave.DataAddTime = obj.DataAddTime;
				leave.ApplyEmp = obj.ApplyEmp;
			});
		}
		return leave;
	},
	//外出
	EgressList: function(dayData, WeekDay) {
		var me = this;
		var egress = me.createObj();
		var EgressArr = dayData.EgressList;
		var type = Ext.typeOf(EgressArr);
		if(type == 'array') {
			Ext.Array.forEach(EgressArr, function(obj, index, array) {
				egress.StartDateTime = obj.StartDateTime;
				egress.EndDateTime = obj.EndDateTime;
				egress.Memo = obj.Memo;
				egress.ATEventSubTypeName = obj.ATEventSubTypeName;
				egress.ATEventTypeName = obj.ATEventTypeName;
				egress.ApproveStatusID = obj.ApproveStatusID;
				egress.ApproveName = obj.ApproveName;
				egress.WeekDay = WeekDay;
				egress.DataAddTime = obj.DataAddTime;
				egress.ApplyEmp = obj.ApplyEmp;
			});
		}
		return egress;
	},
	//出差
	TripList: function(dayData, WeekDay) {
		var me = this;
		var trip = me.createObj();

		var TripArr = dayData.TripList;
		var type = Ext.typeOf(TripArr);
		if(type == 'array') {
			Ext.Array.forEach(TripArr, function(obj, index, array) {
				trip.StartDateTime = obj.StartDateTime;
				trip.EndDateTime = obj.EndDateTime;
				trip.Memo = obj.Memo;
				trip.ATEventSubTypeName = obj.ATEventSubTypeName;
				trip.ATEventTypeName = obj.ATEventTypeName;
				trip.ApproveStatusID = obj.ApproveStatusID;
				trip.ApproveName = obj.ApproveName;
				trip.WeekDay = WeekDay;
				trip.DataAddTime = obj.DataAddTime;
				trip.ApplyEmp = obj.ApplyEmp;
			});
		}
		return trip;
	},

	dayresult: function(dayData, weekDay) {
		var me = this;
		var dateDay = '';
		var obj = {};
		var EndDate = '',
			StartDate = '',
			DateCode = '1';
		var weekdate = me.StartDateAndEndDate();
		if(weekdate.StartDate != '' && weekdate.EndDate != '') {
			EndDate = weekdate.EndDate;
			StartDate = weekdate.StartDate;
		}
		var StartD = JcallShell.Date.getDate(StartDate);
		var startDate = '',
			endDate = '';
		var week = '';
		var arr = [];
		var objDay = {
			date: '',
			weekDays: ''
		};
		var currDate = StartD;
		for(var i = 1; i <= 7; i++) {

			objDay = {
				date: '',
				weekDays: ''
			};
			objDay.date =JcallShell.Date.toString(currDate, true);
			var weekDays = JcallShell.Date.getDate(currDate).getDay();
			week = '';
			switch(weekDays) {
				case 0:
					week = 'Sunday';
					break;
				case 1:
					week = 'Monday';
					break;
				case 2:
					week = 'Tuesday';
					break;
				case 3:
					week = 'Wednesday';
					break;
				case 4:
					week = 'Thursday';
					break;
				case 5:
					week = 'Friday';
					break;
				case 6:
					week = 'Saturday';
					break;
				default:
					break;
			}

			objDay.weekDays = week;
			arr.push(objDay);
			var currDate2 =  JcallShell.Date.getDate(StartD);
			var d=JcallShell.Date.getDate(StartD);
			currDate =  JcallShell.Date.getNextDate(currDate2, i);
		}
		var result = Ext.isArray(arr); //为数组时才处理
		if(result) {
			Ext.Array.each(arr, function(model) {
				dateDay = model['date'];
				var mweekDay = model['weekDays'];
				if(mweekDay == weekDay) {
					var Sign = me.SignList(dayData, weekDay);
					var Overtime = me.OvertimeList(dayData, weekDay);
					var Trip = me.TripList(dayData, weekDay);
					var Egress = me.EgressList(dayData, weekDay);
					var Leave = me.LeaveList(dayData, weekDay);
					obj = {
						Sign: Sign,
						Overtime: Overtime,
						Trip: Trip,
						Egress: Egress,
						Leave: Leave,
						IsRender: false,
						DateCode: dateDay
					};
				}
			});
		}
		return obj;
	},

	changeResult: function(data) {
		var me = this;
		var list = [],
			result = {},
			Monday = "",
			Tuesday = "",
			Wednesday = "",
			Thursday = "",
			Friday = "",
			Saturday = "",
			StartDate = '',
			EndDate = '',
			Sunday = "";
		var info = data.value;
		var reg = new RegExp("null", "g");
		var value = Ext.JSON.decode(Ext.encode(info).replace(reg, "''"));

		for(var i = 0; i < value.length; i++) {
			var DeptName = value[i].DeptName;
			var DeptId = value[i].DeptId;
			var EmpName = value[i].EmpName;
			var EmpId = value[i].EmpId;
			var StartDate = value[i].StartDate;
			var EndDate = value[i].EndDate;
			var weekdate = me.StartDateAndEndDate();

			//考勤设置信息
			var setting = value[i].ATEmpAttendanceEventParaSettings;
			if(setting == undefined)
				setting = "";
			if(setting && setting != null && setting != "") {
				if(setting.SignInTime != null && setting.SignInTime != "")
					setting.SignInTime = Ext.util.Format.date(setting.SignInTime, 'H:i');
				if(setting.SignOutTime != null && setting.SignOutTime != "")
					setting.SignOutTime = Ext.util.Format.date(setting.SignOutTime, 'H:i');
			}

			if(weekdate.StartDate != '' && weekdate.EndDate != '') {
				//星期一
				if(value[i].Monday != '' && value[i].Monday != null) {
					var dayData = value[i].Monday;
					var mondayobj = me.dayresult(dayData, 'Monday');
					mondayobj.ATEmpAttendanceEventParaSettings = setting;
					Monday = Ext.JSON.encode(mondayobj).toString();
				}
				//星期二
				if(value[i].Tuesday != '' && value[i].Tuesday != null) {
					var dayData = value[i].Tuesday;
					var dateDay2 = '';
					var tuesdayobj = me.dayresult(dayData, 'Tuesday');
					tuesdayobj.ATEmpAttendanceEventParaSettings = setting;
					Tuesday = Ext.JSON.encode(tuesdayobj).toString();
				}
				//星期三
				if(value[i].Wednesday != '' && value[i].Wednesday != null) {
					var dayData = value[i].Wednesday;
					var wednesdayobj = me.dayresult(dayData, 'Wednesday');
					wednesdayobj.ATEmpAttendanceEventParaSettings = setting;
					Wednesday = Ext.JSON.encode(wednesdayobj).toString();
				}
				//星期四
				if(value[i].Thursday != '' && value[i].Thursday != null) {
					var dayData = value[i].Thursday;
					var thursdayobj = me.dayresult(dayData, 'Thursday');
					thursdayobj.ATEmpAttendanceEventParaSettings = setting;
					Thursday = Ext.JSON.encode(thursdayobj).toString();
				}
				//星期五
				if(value[i].Friday != '' && value[i].Friday != null) {
					var dayData = value[i].Friday;
					var fridayobj = me.dayresult(dayData, 'Friday');
					fridayobj.ATEmpAttendanceEventParaSettings = setting;
					Friday = Ext.JSON.encode(fridayobj).toString();
				}
				//星期六
				if(value[i].Saturday != '' && value[i].Saturday != null) {
					var dayData = value[i].Saturday;
					var saturdayobj = me.dayresult(dayData, 'Saturday');
					saturdayobj.ATEmpAttendanceEventParaSettings = setting;
					Saturday = Ext.JSON.encode(saturdayobj).toString();
				}
				//星期日
				if(value[i].Sunday != '' && value[i].Sunday != null) {
					var dayData = value[i].Sunday;
					var sundayobj = me.dayresult(dayData, 'Sunday');
					sundayobj.ATEmpAttendanceEventParaSettings = setting;
					Sunday = Ext.JSON.encode(sundayobj).toString();
				}
			}
			var obj = {
				DeptName: DeptName,
				DeptId: DeptId,
				StartDate: StartDate,
				EndDate: EndDate,
				EmpName: EmpName,
				EmpId: EmpId,
				Monday: Monday,
				Tuesday: Tuesday,
				Wednesday: Wednesday,
				Thursday: Thursday,
				Friday: Friday,
				Saturday: Saturday,
				Sunday: Sunday
			};
			list.push(obj);
		}
		result.list = list;
		return result;
	},

	getchangeValue: function(dayData, weekDay, meta, absent) {
		var me = this;
		var value = "",
			absentStr = '',
			SignInTime = '',
			SignOutTime = '',
			Attr = '',
			Overtime = '',
			Leave = '',
			Egress = '',
			DateCode = '',
			StartDate = '',
			EndDate = '',
			Trip = '';

		var isShowAbsent = false; //是否显示旷工
		if(dayData != '' && dayData != null) {
			DateCode = dayData.DateCode;
			StartDate = dayData.StartDate;
			EndDate = dayData.EndDate;
			//签到签退
			
			if(dayData.Sign){
				if(dayData.Sign != "" && dayData.Sign.WeekDay == weekDay) {
					//考勤设置信息处理
					var setSignInTime = "",
						setSignOutTime = "",
						setATEventPostion = "",
						setATEventPostionName = "",
						setATEventPostionRange = "";
					if(dayData.ATEmpAttendanceEventParaSettings && dayData.ATEmpAttendanceEventParaSettings) {
						var setting = dayData.ATEmpAttendanceEventParaSettings;
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
	
					if(dayData.Sign.SignInTime) {
						Attr = '设置签到时间:' + setSignInTime + "<br />";
						Attr = Attr + "<span>实际签到时间:" + dayData.Sign.SignInTime + "</span>" + "<br />";
						Attr = Attr + "<span>登记事件:" + "签到" + "</span>" + "<br />";
	
						Attr = Attr + '设置考勤地点坐标:' + setATEventPostion + "<br />";
						Attr = Attr + "<span>实际签到地点坐标:" + dayData.Sign.SigninATEventLogPostion + "</span>" + "<br />";
						Attr = Attr + '设置签到考勤地点:' + setATEventPostionName + "<br />";
						Attr = Attr + "<span>实际签到考勤地点:" + dayData.Sign.SigninATEventLogPostionName + "</span>" + "<br />";
						Attr = Attr + '设置考勤地点范围:' + setATEventPostionRange + "<br />";
	
						Attr = Attr + "<span>说明:" + dayData.Sign.SignInMemo + "</span>" + "<br />";
						SignInTime = (dayData.Sign.SignInType != "" ? ("<a style='color:red'; data-qtip='" + Attr + "'>" + '迟到' + dayData.Sign.SignInTime + "</a>") : ("<a style='color:black'; data-qtip='" + Attr + "'>" + "签到" + dayData.Sign.SignInTime + "</a>"));
	
					}
					if(dayData.Sign.SignOutTime) {
						Attr = '设置签退时间:' + setSignOutTime + '<br />';
						Attr = Attr + "实际签退时间:" + dayData.Sign.SignOutTime + "<br />";
	
						Attr = Attr + "<span>登记事件:" + "签退" + "</span>" + "<br />";
	
						Attr = Attr + "设置考勤地点坐标:" + setATEventPostion + "<br />";
						Attr = Attr + "<span>实际签退地点坐标:" + dayData.Sign.SigninATEventLogPostion + "</span>" + "<br />";
						Attr = Attr + "设置签退考勤地点:" + setATEventPostionName + "<br />";
						Attr = Attr + "<span>实际签退考勤地点:" + dayData.Sign.SigninATEventLogPostionName + "</span>" + "<br />";
						Attr = Attr + "设置考勤地点范围:" + setATEventPostionRange + "<br />";
						Attr = Attr + "<span>说明:" + dayData.Sign.SignOutMemo + "</span>" + "<br />";
	
						SignOutTime = (dayData.Sign.SignOutType != "" ? ("<a style='color:red'; data-qtip='" + Attr + "'>" + '早退' + dayData.Sign.SignOutTime + "</a>") : ("<a  style='color:black'; data-qtip='" + Attr + "'>" + "签退" + dayData.Sign.SignOutTime + "</a>"));
					}
					if(SignInTime == "" && SignOutTime == "") {
						isShowAbsent = true;
						var absentStrAttr = "<span>旷工" + "</span>" + "<br />";
						absentStr = "<a style='color:red'; data-qtip='" + absentStrAttr + "' >" + "旷工</a>";
					}
				}
			}
			//加班
			if(dayData.Overtime){
		
				if(dayData.Overtime != "" && dayData.Overtime.WeekDay == weekDay) {
					if(dayData.Overtime.ATEventTypeName != "") {
						OvertimeAttr = "<span>开始时间：" + dayData.Overtime.StartDateTime + "</span>" + "<br />" +
							"<span>结束时间：" + dayData.Overtime.EndDateTime + "</span>" + "<br />" +
							"<span>加班时长：" + dayData.Overtime.EvenLength + " " + dayData.Overtime.EvenLengthUnit + "</span>" + "<br />" +
							"<span>备注：" + dayData.Overtime.Memo + "</span>";
						Overtime = "<a style='color:green'; data-qtip='" + OvertimeAttr + "' >" + dayData.Overtime.ATEventTypeName + "</a>";
						switch(dayData.Overtime.ApproveStatusID) {
							case 1:
								Overtime += "<span style='color:green'>(已获批)</span>";
								break;
							case 2:
								Overtime += "<span style='color:red'>(退回)</span>";
								break;
							default:
								Overtime += "<span style='color:green'>(未获批)</span>";
								break;
						}
					}
	
				}		
			}
			//请假
			if(dayData.Leave){
				if(dayData.Leave != "" && dayData.Leave.WeekDay == weekDay) {
					if(dayData.Leave.ATEventTypeName != "") {
						LeaveAttr =
							"<span>申请时间：" + dayData.Leave.DataAddTime + "</span>" + "<br />" +
							"<span>请假类别：" + dayData.Leave.ATEventSubTypeName + "</span>" + "<br />" +
							"<span>开始时间：" + dayData.Leave.StartDateTime + "</span>" + "<br />" +
							"<span>结束时间：" + dayData.Leave.EndDateTime + "</span>" + "<br />" +
							"<span>备注：" + dayData.Leave.Memo + "</span>" + "<br />";
						//						"<span>说明：</span>";
						Leave = "<a style='color:green'; data-qtip='" + LeaveAttr + "' >" + dayData.Leave.ATEventSubTypeName + "</a>";
						switch(dayData.Leave.ApproveStatusID) {
							case 1:
								Leave += "<span style='color:green';data-qtip='" + LeaveAttr + "'>(已获批)</span>";
								break;
							case 2:
								Leave += "<span style='color:red';data-qtip='" + LeaveAttr + "'>(退回)</span>";
								break;
							default:
								Leave += "<span style='color:green';data-qtip='" + LeaveAttr + "'>(未获批)</span>";
								break;
						}
					}
				}
				
			}
			
			//外出
			if(dayData.Egress){
				if(dayData.Egress != "" && dayData.Egress.WeekDay == weekDay) {
					if(dayData.Egress.ATEventTypeName != "") {
						EgressAttr = "<span>申请时间：" + dayData.Egress.DataAddTime + "</span>" + "<br />" +
							"<span>起始时间：" + dayData.Egress.StartDateTime + "</span>" + "<br />" +
							"<span>结束时间：" + dayData.Egress.EndDateTime + "</span>" + "<br />" +
							"<span>备注：" + dayData.Egress.Memo + "</span>";
						//						"<span>说明：</span>";
						Egress = "<a style='color:green'; data-qtip='" + EgressAttr + "' >" + dayData.Egress.ATEventTypeName + "</a>";
						switch(dayData.Egress.ApproveStatusID) {
							case 1:
								Egress += "<span style='color:green'>(已获批)</span>";
								break;
							case 2:
								Egress += "<span style='color:red'>(退回)</span>";
								break;
							default:
								Egress += "<span style='color:green'>(未获批)</span>";
								break;
						}
					}
				}
			}
			
			//出差
			if(dayData.Trip){
				if(dayData.Trip != "" && dayData.Trip.WeekDay == weekDay) {
					if(dayData.Trip.ATEventTypeName != "") {
						TripAttr = "<span>申请时间：" + dayData.Trip.DataAddTime + "</span>" + "<br />" +
							"<span>起始时间：" + dayData.Trip.StartDateTime + "</span>" + "<br />" +
							"<span>结束时间：" + dayData.Trip.EndDateTime + "</span>" + "<br />" +
							"<span>备注：" + dayData.Trip.Memo + "</span>";
						//						"<span>说明：</span>";
						Egress = "<a style='color:green'; data-qtip='" + TripAttr + "' >" + dayData.Trip.ATEventTypeName + "</a>";
						switch(dayData.Trip.ApproveStatusID) {
							case 1:
								Trip += "<span style='color:green'>(已获批)</span>";
								break;
							case 2:
								Trip += "<span style='color:red'>(退回)</span>";
								break;
							default:
								Trip += "<span style='color:green'>(未获批)</span>";
								break;
						}
					}
				}
		
			}
			
		}
		if(SignInTime != "") {
			value = value + SignInTime + "<br />";
			isShowAbsent = false;
		}
		if(SignOutTime != "") {
			value = value + SignOutTime + "<br />";
			isShowAbsent = false;
		}
		if(Overtime != "") {
			value = value + Overtime + "<br />";
			isShowAbsent = false;
		}
		if(Leave != "") {
			value = value + Leave + "<br />";
			isShowAbsent = false;
		}
		if(Egress != "") {
			value = value + Egress + "<br />";
			isShowAbsent = false;
		}
		if(Trip != '') {
			value = value + Trip + "<br />";
			isShowAbsent = false;
		}
		if(isShowAbsent && absent == '0') {
			var date = new Date();
			var year = date.getFullYear();
			var month = date.getMonth() + 1;
			var day = date.getDate();
			var Hours = date.getHours(); //获取当前小时数(0-23)
			var Minute = date.getMinutes(); //获取当前分钟数(0-59)
			var Second = date.getSeconds(); //获取当前秒数(0-59)

			var crrDate = new Date(year, month, day);
			var oDate2 = new Date(DateCode);
			
			var Start = JcallShell.Date.toString(DateCode,true)+ " 00:00:00";
//			var Start = Ext.util.Format.date(DateCode + " 00:00:00");
			var Newd = year + "-" + month + "-" + day + " 23:59:59";
//			var Newd = year + "-" + month + "-" + day ;
            var begintime_ms = Date.parse(new Date(Start.replace(/-/g, "/"))); //begintime 为开始时间
 
            var endtime_ms = Date.parse(new Date(Newd.replace(/-/g, "/")));   // endtime 为结束时间
			if(endtime_ms >begintime_ms) {
				value = value + absentStr + "<br />";
			}
		}
		meta.style = 'padding:3px 6px;text-overflow: ellipsis;white-space: nowrap;white-space:normal;line-height:20px;word-break:break-all; word-wrap:break-word;font-weight:bold;';
		meta.css = "background:#93A9C1;";
		return value;
	},

	DateDiff: function(d1, d2) {
		start_at = new Date(d1.replace(/^(\d{4})(\d{2})(\d{2})$/, "$1/$2/$3"));
		end_at = new Date(d2.replace(/^(\d{4})(\d{2})(\d{2})$/, "$1/$2/$3"));
		if(start_at > end_at) {
			return false;
		}
		return true;
	},
	changeValue: function(value, meta, record, rowIndex, colIndex, store, view, weekDay) {
		var me = this;
		var value = "",
			Attr = ""; //旷工
		var absent = '0'; //1:旷工
		var MondayInfo = record.get('Monday');
		if(MondayInfo && MondayInfo != "" && MondayInfo != null) {
			var Monday = Ext.JSON.decode(MondayInfo);
			if(Monday != "") {
				absent = '0';
				value = me.getchangeValue(Monday, weekDay, meta, absent);
			}
		}
		var TuesdayInfo = record.get('Tuesday');
		var Tuesday = Ext.JSON.decode(TuesdayInfo);
		if(Tuesday != "") {
			absent = '0';
			value += me.getchangeValue(Tuesday, weekDay, meta, absent);
		}
		var WednesdayInfo = record.get('Wednesday');
		var Wednesday = Ext.JSON.decode(WednesdayInfo);
		if(Wednesday != "") {
			absent = '0';
			value += me.getchangeValue(Wednesday, weekDay, meta, absent);
		}
		var ThursdayInfo = record.get('Thursday');
		var Thursday = Ext.JSON.decode(ThursdayInfo);
		if(Thursday != "") {
			absent = '0';
			value += me.getchangeValue(Thursday, weekDay, meta, absent);
		}
		var FridayInfo = record.get('Friday');
		var Friday = Ext.JSON.decode(FridayInfo);
		if(Friday != "") {
			absent = '0';
			value += me.getchangeValue(Friday, weekDay, meta, absent);
		}
		var SaturdayInfo = record.get('Saturday');
		var Saturday = Ext.JSON.decode(SaturdayInfo);
		if(Saturday != "") {
			absent = '1';
			value += me.getchangeValue(Saturday, weekDay, meta, absent);
		}
		var SundayInfo = record.get('Sunday');
		var Sunday = Ext.JSON.decode(SundayInfo);
		if(Sunday != "") {
			absent = '1';
			value += me.getchangeValue(Sunday, weekDay, meta, absent);
		}
		return value;
	},
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
				async: false,
				reader: {
					type: 'json',
					totalProperty: 'count',
					root: 'list'
				},
				extractResponseData: function(response) {
					var data = JShell.Server.toJson(response.responseText);
					if(data.success) {
						var value = data.value;
						var valueData = {};
						var reg = new RegExp("null", "g");
						var weekMondayArr = [],
							dataArr = [];
						for(var i = 0; i < value.length; i++) {
							var f = value[i].Monday.SignList;
							var DeptName = value[i].DeptName;
							var DeptId = value[i].DeptId;
							var EmpName = value[i].EmpName;
							var EmpId = value[i].EmpId;
							var d = [DeptId, DeptName];
							me.DeptDateList.push(d);
							var obj = {
								DeptName: DeptName,
								DeptId: DeptId,
								EmpName: EmpName,
								EmpId: EmpId
							};
							dataArr.push(obj);
						}
						data.count = dataArr.length || 0;
						data.list = dataArr || [];
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
	mergeCells: function(grid, rows) {
		var gridEl = Ext.get(grid.getId() + "-body").first();
		var arrayTr = gridEl.select("tr");
		arrayTr.each(function(el) {
			if(el.getAttribute("class") == "x-grid-header-row") {
				arrayTr.removeElement(el)
			}
		})
		var merge = function(rowspanObj, removeObjs) {
			var trIndex = 1;
			arrayTr.each(function(tr) {
				var arrayTd = tr.select("td");
				arrayTd.each(function(td) {
					if(td.getAttribute("class").indexOf("x-grid-cell-special") != -1) {
						arrayTd.removeElement(td)
					}
				});
				if(trIndex == rowspanObj["tr"]) {
					var tdIndex = 1;
					arrayTd.each(function(td) {
						if(tdIndex == rowspanObj["td"]) {
							if(rowspanObj["rowspan"] != 1) {
								td.set({
									"rowspan": rowspanObj["rowspan"],
									"valign": "middle"
								});
							}
						}
						tdIndex++;
					});
				}
				Ext.each(removeObjs, function(obj) {
					var tdIndex = 1;
					if(trIndex == obj["tr"]) {
						arrayTd.each(function(td) {
							if(tdIndex == obj["td"]) {
								td.set({
									"style": "display:none"
								})
							}
							tdIndex++;
						})
					}
				})
				trIndex++;
			})
		}
		var rowspanObj = {};
		var removeObjs = [];
		Ext.each(rows, function(rowIndex) {
			var trIndex = 1;
			var rowspan = 1;
			var divHtml = null; //单元格内的数值
			var trCount = arrayTr.getCount();
			
			arrayTr.each(function(tr) {
				//准备td集合
				var arrayTd = tr.select("td");
				arrayTd.each(function(td) {
//						//移除序号,多选框等不进行合并的td
//						if(td.getAttribute("class").indexOf("x-grid-cell-special") != -1) {
//							arrayTd.removeElement(td)
//						}
					})
					//准备格式化每一列
				var tdIndex = 1;
				arrayTd.each(function(td) {
					if(tdIndex == rowIndex) {
						if(!divHtml) {
							divHtml = td.first().getHTML();
							rowspanObj = {
								tr: trIndex,
								td: tdIndex,
								rowspan: rowspan
							}
						} else {
							var cellText = td.first().getHTML();
							if(cellText == divHtml) {
								rowspanObj["rowspan"] = rowspanObj["rowspan"] + 1;
								removeObjs.push({
									tr: trIndex,
									td: tdIndex
								});
								if(trIndex == trCount) {
									merge(rowspanObj, removeObjs); //执行合并函数
								}
							} else {
								merge(rowspanObj, removeObjs); //执行合并函数
								divHtml = cellText;
								rowspanObj = {
									tr: trIndex,
									td: tdIndex,
									rowspan: rowspan
								}
								removeObjs = [];
							}
						}
					}
					tdIndex++;
				})
				trIndex++;
			})
		})
	},

	/**更改周组件的数据
	 * @author longfc
	 * @version 2016-10-31
	 */
	changeWeek: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Year = buttonsToolbar.getComponent('Year'),
			Month = buttonsToolbar.getComponent('Month'),
			Week = buttonsToolbar.getComponent('Week');
		//周列表
		me.DateTimeList = [];
		var weeks = JShell.Date.getMonthTotalWeekByYearMonth(Year.getValue(), Month.getValue());
		for(var i = 0; i < weeks; i++) {
			me.DateTimeList.push([(1 + i) + '', '第' + (i + 1) + '周']);
		}
		Week.store.loadData(me.DateTimeList);
		Week.setValue('1'); //默认第一周
	}
});