/**
 * 管理员的节假日设置(可以编辑当前月及之前的节假日信息)
 * 节假日设置 SettingType：1、工作日改节假日(星期一到星期五)，2节假日改工作日
 * (1)如果是星期一到星期五,只新增保存工作日改节假日状态的单元格,删除已经保存到数据库的节假日改工作日状态的单元格
 * (2)如果是星期六,星期日,只新增保存节假日改工作日状态的单元格,删除已经保存到数据库的工作日改节假日状态的单元格
 * @author longfc
 * @version 2016-10-31
 */
Ext.define('Shell.class.oa.at.attendance.holidaysetting.ManageGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '节假日设置',
	width: 360,
	height: 500,

	selectUrl: '/WeiXinAppService.svc/ST_UDTO_SearchATHolidaySettingByYearAndMonth',
	/**删除数据服务路径*/
	delUrl: '/WeiXinAppService.svc/ST_UDTO_DelATHolidaySettingByIdStr',
	/**新增数据服务路径*/
	addUrl: '/WeiXinAppService.svc/ST_UDTO_AddATHolidaySetting',
	/**修改服务地址*/
	editUrl: '/WeiXinAppService.svc/ST_UDTO_UpdateATHolidaySettingByField',
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	hasRownumberer: false,
	/**是否启用新增按钮*/
	hasAdd: false,
	hasEidt: false,
	/**默认每页数量*/
	defaultPageSize: 60,
	/**是否启用刷新按钮*/
	hasRefresh: false,

	columnLines: true,
	isLoaded: false,
	//selType: 'rowmodel', //选格模式
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'Month',
		direction: 'ASC'
	}, {
		property: 'DateCode',
		direction: 'ASC'
	}], //
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,

	hasPagingtoolbar: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	columnLines: true,

	remoteSort: false,
	/*固定节假日**/
	DefaultHolidays: [
		'1-1',
		'5-1',
		'10-1'
	],
	DefaultBackGround: [
		'#F5DEB3' //周六,周日背景颜色
	],
	/*当前选择的年月是否可以设置节假日*/
	isSetHoliday: true,

	//-----------------------------农历换算二-----------------------------
	madd: new Array(0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334),
	HsString: '甲乙丙丁戊己庚辛壬癸',
	EbString: '子丑寅卯辰巳午未申酉戌亥',
	NumString: "一二三四五六七八九十",
	MonString: "正二三四五六七八九十冬腊",

	CalendarData: new Array(0xA4B, 0x5164B, 0x6A5, 0x6D4, 0x415B5, 0x2B6, 0x957, 0x2092F, 0x497, 0x60C96, 0xD4A, 0xEA5, 0x50DA9, 0x5AD, 0x2B6, 0x3126E, 0x92E, 0x7192D, 0xC95, 0xD4A, 0x61B4A, 0xB55, 0x56A, 0x4155B, 0x25D, 0x92D, 0x2192B, 0xA95, 0x71695, 0x6CA, 0xB55, 0x50AB5, 0x4DA, 0xA5B, 0x30A57, 0x52B, 0x8152A, 0xE95, 0x6AA, 0x615AA, 0xAB5, 0x4B6, 0x414AE, 0xA57, 0x526, 0x31D26, 0xD95, 0x70B55, 0x56A, 0x96D, 0x5095D, 0x4AD, 0xA4D, 0x41A4D, 0xD25, 0x81AA5, 0xB54, 0xB6A, 0x612DA, 0x95B, 0x49B, 0x41497, 0xA4B, 0xA164B, 0x6A5, 0x6D4, 0x615B4, 0xAB6, 0x957, 0x5092F, 0x497, 0x64B, 0x30D4A, 0xEA5, 0x80D65, 0x5AC, 0xAB6, 0x5126D, 0x92E, 0xC96, 0x41A95, 0xD4A, 0xDA5, 0x20B55, 0x56A, 0x7155B, 0x25D, 0x92D, 0x5192B, 0xA95, 0xB4A, 0x416AA, 0xAD5, 0x90AB5, 0x4BA, 0xA5B, 0x60A57, 0x52B, 0xA93, 0x40E95),
	TheDate: null,
	solarYear: null,
	solarMonth: null,
	solarDay: null,
	GetBit: function(m, n) {
		var me = this;
		return(m >> n) & 1;
	},
	e2c: function() {
		var me = this;
		me.TheDate = (arguments.length != 3) ? new Date() : new Date(arguments[0], arguments[1], arguments[2]);
		var total, m, n, k;
		var isEnd = false;
		var tmp = me.TheDate.getFullYear();

		total = (tmp - 1921) * 365 + Math.floor((tmp - 1921) / 4) + me.madd[me.TheDate.getMonth()] + me.TheDate.getDate() - 38;
		if(me.TheDate.getYear() % 4 == 0 && me.TheDate.getMonth() > 1) {
			total++;
		}
		for(m = 0;; m++) {
			k = (me.CalendarData[m] < 0xfff) ? 11 : 12;
			for(n = k; n >= 0; n--) {
				if(total <= 29 + me.GetBit(me.CalendarData[m], n)) {
					isEnd = true;
					break;
				}
				total = total - 29 - me.GetBit(me.CalendarData[m], n);
			}
			if(isEnd)
				break;
		}
		me.solarYear = 1921 + m;
		me.solarMonth = k - n + 1;
		me.solarDay = total;

		if(k == 12) {
			if(me.solarMonth == Math.floor(me.CalendarData[m] / 0x10000) + 1) {
				me.solarMonth = 1 - me.solarMonth;
			}
			if(me.solarMonth > Math.floor(me.CalendarData[m] / 0x10000) + 1) {
				me.solarMonth--;
			}
		}
	},
	GetcDateString: function() {
		var me = this;
		var tmp = "";
		//		tmp += me.HsString.charAt((me.solarYear - 4) % 10);
		//		tmp += me.EbString.charAt((me.solarYear - 4) % 12);
		//		tmp += "年 ";
		if(me.solarMonth < 1) {
			tmp += "(闰)";
			tmp += me.MonString.charAt(-me.solarMonth - 1);
		} else {
			tmp += me.MonString.charAt(me.solarMonth - 1);
		}
		tmp += "月";
		tmp += (me.solarDay < 11) ? "初" : ((me.solarDay < 20) ? "十" : ((me.solarDay < 30) ? "廿" : "三十"));
		if(me.solarDay % 10 != 0 || me.solarDay == 10) {
			tmp += me.NumString.charAt((me.solarDay - 1) % 10);
		}
		return tmp;
	},
	/*
	 *农历换算(只能换算到2020年,先不用)
	 **/
	GetLunarDay: function(solarYear, solarMonth, solarDay) {
		var me = this;
		if(solarYear < 1921) {
			return "";
		} else {
			solarMonth = (parseInt(solarMonth) > 0) ? (solarMonth - 1) : 11;
			me.e2c(solarYear, solarMonth, solarDay);
			return me.GetcDateString();
		}
	},
	//-----------------------------农历换算-----------------------------

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		//if(buttonsToolbar)
			//buttonsToolbar.getComponent('batchSave').setVisible(me.isSetHoliday);
	},

	initComponent: function() {
		var me = this;
		JShell.Function.onCheckboxClick = function(ele, e, index, dataIndex) {
			//满足可编辑条件时才处理
			if(me.isSetHoliday) {
				var record = me.store.getAt(index);
				var value = record.get(dataIndex);
				record.set(dataIndex, !value);
			} else {
				JShell.Msg.alert("当前选择的年月不能进行编辑操作!", null, 2000);
			}
		};
		JShell.Function.openEditFormClick = function(ele, e) {
			var Id = ele.getAttribute("data");
			//ele.readonly = !me.isSetHoliday;
			if(me.isSetHoliday) {
				if(Id == "" || Id == null) {
					JShell.Msg.alert("请点击保存后再编辑!", null, 1000);
				} else {
					me.openEditForm(Id);
				}
				if(e.stopPropagation) {
					e.stopPropagation();
				} else {
					e.cancelBubble = true;
				}
			} else {
				JShell.Msg.alert("当前选择的年月不能进行编辑操作!", null, 1000);
			}
		};
		//初始化送检时间
		me.initDate();
		//初始化功能按钮栏内容
		me.initButtonToolbarItems();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			editing: me.isSetHoliday,
			pluginId: 'myPluginCell',
			clicksToEdit: 1
		});

		//创建数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**打开编辑表单*/
	openEditForm: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight - 10;

		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO:'1',
			zindex: 10,
			zIndex: 10,
			resizable: false,
			title: "节假日设置",
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		if(id && id != null && id != "") {
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.oa.at.attendance.holidaysetting.Form', config).show();
	},
	getYearComboBoxData: function() {
		var me = this;
		me.YearValue = new Date().getFullYear();
		me.minYearValue = me.YearValue - 5;
		me.maxYearValue = me.YearValue + 5;
		var data = [];
		for(var i = me.minYearValue; i <= me.maxYearValue; i++) {
			data.push([i, i + '年']);
		}
		return data;
	},
	/**初始化功能按钮栏内容*/
	initButtonToolbarItems: function() {
		var me = this;
		me.buttonToolbarItems = [];

		if(me.hasRefresh) {
			me.buttonToolbarItems.push('refresh');
		}

		me.buttonToolbarItems.push('-', {
			width: 95,
			margin: '0 0 0 20px',
			xtype: 'uxSimpleComboBox',
			itemId: 'Year',
			minValue: me.minYearValue,
			//maxValue: me.maxYearValue,
			value: me.YearValue,
			data: me.getYearComboBoxData(),
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
			margin: '0 20px 0 5px',
			xtype: 'button',
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				me.GridSearch();
			}
		}, '-', {
			width: 60,
			iconCls: 'button-save',
			margin: '0 0 0 10px',
			xtype: 'button',
			itemId: "batchSave",
			text: '保存',
			tooltip: '<b>批量操作节假日的设置信息</b>',
			handler: function() {
				me.onSaveClick();
			}
		}, "->");
	},
	
	GridSearch: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			year = buttonsToolbar.getComponent('Year').getValue(),
			month = buttonsToolbar.getComponent('Month').getValue();
		if(year != "" && month != "") {
			//me.getView().setDisabled(true);
			me.verificationIsEdit(year, month);
			me.onSearch();
		} else {
			JShell.Msg.alert("请选择年和月份后再操作!", null, 500);
		}
		//if(buttonsToolbar)
			//buttonsToolbar.getComponent('batchSave').setVisible(me.isSetHoliday);
	},
	/*判断选择的年和月份是否可以设置节假日**/
	verificationIsEdit: function(year, month) {
		var me = this;
		me.isSetHoliday = true;
		var serverTime = JcallShell.System.Date.getDate();
		var serverYear = serverTime.getFullYear();
		var serverMonth = serverTime.getMonth() + 1;

//		//选择的年份小于服务器的年份值
//		if(year < serverYear) {
//			me.isSetHoliday = false;
//		}
//		//选择的年份等于服务器的年份值,但选择的月份大于等于服务器的月份值
//		if(year == serverYear && month <= serverMonth) {
//			me.isSetHoliday = false;
//		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		var Year = buttonsToolbar.getComponent('Year').getValue(),
			Month = buttonsToolbar.getComponent('Month').getValue();
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		if(Year != "" && Month != "") {
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'year=' + Year;
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'month=' + Month;
		}
		return url;
	},

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			text: '星期一',
			dataIndex: 'Monday',
			hidden: true,
			align: 'center',
			sortable: false,
			menuDisabled: true
		}, {
			text: '星期一',
			dataIndex: 'SetMonday',
			width: 125,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: true,
			type: 'boolean',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.changeValue(value, meta, record, rowIndex, colIndex, store, view, 'Monday');
			}
		}, {
			text: '星期二',
			dataIndex: 'Tuesday',
			hidden: true,
			align: 'center',
			sortable: false,
			menuDisabled: true
		}, {
			text: '星期二',
			dataIndex: 'SetTuesday',
			width: 125,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: true,
			type: 'boolean',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.changeValue(value, meta, record, rowIndex, colIndex, store, view, 'Tuesday');
			}
		}, {
			text: '星期三',
			dataIndex: 'Wednesday',
			hidden: true,
			align: 'center',
			sortable: false,
			menuDisabled: true
		}, {
			text: '星期三',
			dataIndex: 'SetWednesday',
			width: 125,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: true,
			type: 'boolean',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.changeValue(value, meta, record, rowIndex, colIndex, store, view, 'Wednesday');
			}
		}, {
			text: '星期四',
			dataIndex: 'Thursday',
			hidden: true,
			align: 'center',
			sortable: false,
			menuDisabled: true
		}, {
			text: '星期四',
			dataIndex: 'SetThursday',
			width: 125,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: true,
			type: 'boolean',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.changeValue(value, meta, record, rowIndex, colIndex, store, view, 'Thursday');
			}
		}, {
			text: '星期五',
			dataIndex: 'Friday',
			hidden: true,
			align: 'center',
			sortable: false,
			menuDisabled: true
		}, {
			text: '星期五',
			dataIndex: 'SetFriday',
			sortable: false,
			align: 'center',
			width: 125,
			menuDisabled: true,
			stopSelection: true,
			type: 'boolean',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.changeValue(value, meta, record, rowIndex, colIndex, store, view, 'Friday');
			}
		}, {
			text: '星期六',
			dataIndex: 'Saturday',
			hidden: true,
			align: 'center',
			sortable: false,
			menuDisabled: true
		}, {
			text: '星期六',
			dataIndex: 'SetSaturday',
			width: 125,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: true,
			type: 'boolean',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.changeValue(value, meta, record, rowIndex, colIndex, store, view, 'Saturday');
			}
		}, {
			text: '星期日',
			dataIndex: 'Sunday',
			hidden: true,
			align: 'center',
			sortable: false,
			menuDisabled: true
		}, {
			text: '星期日',
			dataIndex: 'SetSunday',
			width: 125,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: true,
			type: 'boolean',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.changeValue(value, meta, record, rowIndex, colIndex, store, view, 'Sunday');
			}
		});
		return columns;
	},

	/**初始化数据*/
	initDate: function() {
		var me = this;
		//时间处理
		var date = JShell.Date.getNextDate(new Date(), 0);
		var year = date.getFullYear();
		var month = date.getMonth() + 1;
		me.YearValue = date.getFullYear(); //年
		me.MonthValue = date.getMonth() + 1; //月
		me.minYearValue = me.YearValue - 5;
		me.maxYearValue = me.YearValue + 5;
		me.verificationIsEdit(me.YearValue, me.MonthValue);
	},
	changeValue: function(value, meta, record, rowIndex, colIndex, store, view, weekDay) {
		var me = this;
		var showValue = "",
			monthDay = "", //第几天
			qtipStrValue = "", //提示信息
			Day = "";
		//获取星期几的隐藏obj值"Set"+weekDay
		var weekDayInfo = record.get(weekDay);
		if(weekDayInfo && weekDayInfo != "" && weekDayInfo != null) {
			weekDayInfo = Ext.JSON.decode(weekDayInfo);
		}

		//该月和第几天(如:01)
		Day = weekDayInfo.Day;
		if(Day == undefined) {
			Day = "";
		}
		//SettingType：1、(星期一到星期五)工作日改节假日，2节假日改工作日
		var isChecked = value;
		//背景图片处理
		var picpath = "";
		var result = "";
		//工作日的类样式
		var workdayClass = "btn btn-primary";
		//节假日的类样式
		var holidayClass = "btn btn-success";
		//如果勾选上,是工作日
		var checkClass = isChecked ? holidayClass : workdayClass;
		var checkText = isChecked ? "节假日" : "工作日";

		if(Day != "") {
			var background = "";
			var lunarDay = weekDayInfo.LunarDay;
			monthDay = "<a style='font-weight:bold;'>" + Day + "日</a>";
			//农历先不用,算法换算有问题
			//			if(lunarDay != "") {
			//				monthDay = monthDay + "<br/><span style='font-weight:bold;color:#A9A9A9'>" + lunarDay + "</span>";
			//			}
			checkText = isChecked ? "节假日" : "工作日";
			checkClass = isChecked ? holidayClass : workdayClass;

			meta.style = "width:100%;height=60px;background-repeat:no-repeat; background-position:2px 2px;" + background; //background-image:url(" + picpath + ");
			var id = "";
			if(weekDayInfo && weekDayInfo.Id != null) {
				id = weekDayInfo.Id;
			}
			result = "<div class='hand' data='" + id + "'style='background-position: 50% -2px;width:100%;padding:2px 2px;text-overflow: ellipsis;white-space: nowrap;white-space:normal;line-height:18px;word-break:break-all; word-wrap:break-word;' onclick='JShell.Function.openEditFormClick(this,event)'>" + monthDay + "</div>";

			result = result + '<div  onclick="JShell.Function.onCheckboxClick(this,event,' + rowIndex + ',' + '\'Set' + weekDay + '\')"' + 'class="' + checkClass + '"' + 'style="background-position: 50% -2px;vertical-align:middle;background-repeat:no-repeat;">' + checkText + '</div>';

		}
		return result;
	},
	createNullRow: function() {
		var me = this;
		var row = {
			//节假日实体信息
			Id: '',
			Year: '',
			Month: '',
			DateCode: '',
			SettingType: -1,
			HolidayName: '',
			Name: '',
			SName: '',
			IsUse: true,
			WeekInfo: '',
			Day: '',
			//列信息
			Monday: "",
			Tuesday: "",
			Wednesday: "",
			Thursday: "",
			Friday: "",
			Saturday: "",
			Sunday: "",
			SetSunday: true,
			SetMonday: false,
			SetTuesday: false,
			SetWednesday: false,
			SetThursday: false,
			SetFriday: false,
			SetSaturday: true
		};
		return row;
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
		var row = me.createNullRow();

		buttonsToolbar = me.getComponent('buttonsToolbar');
		var year = buttonsToolbar.getComponent('Year').getValue();
		Ext.Array.each(data.value, function(obj, index) {
			DateCode = obj.DateCode;
			//DateCode=DateCode.replace("/","-");
			DateCode = Ext.util.Format.date(DateCode, 'Y-m-d');
			var indexOf = tempArrATEventDateCode.indexOf(Ext.util.Format.date(DateCode, 'Y-m-d'));
			var weekDay = me.changeweekDay(obj.DateCode);

			row = me.createNullRow();

			row.Id = obj.Id;
			row.Year = obj.Year;
			row.Month = obj.Month;
			row.SettingType = obj.SettingType;
			row.DateCode = Ext.util.Format.date(DateCode, 'Y-m-d');

			row.HolidayName = obj.HolidayName;
			row.Name = obj.Name;
			row.SName = obj.SName;
			row.Shortcode = obj.Shortcode;
			row.PinYinZiTou = obj.PinYinZiTou;
			row.IsUse = obj.IsUse;

			if(indexOf == -1) { //不存在list中
				tempArrATEventDateCode.push(Ext.util.Format.date(DateCode, 'Y-m-d'));
				list.push(row);
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
	 * 某一月的考勤工作日空数据集
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
			var row = me.createNullRow();
			if(day > countDays || day > 31) {
				break;
			}
			for(var j = colIndex; j < 7; j++) {
				day = day + 1;
				if(day > countDays || day > 31) {
					break;
				}
				var DateCode = year + "-" + month + "-" + day;
				var weekDay = me.changeweekDay(DateCode);
				var LunarDay = me.GetLunarDay(year, month, day);
				var obj = {
					Id: "",
					Year: year,
					Month: month,
					SettingType: -1,
					HolidayName: "",
					Name: "",
					SName: "",
					IsUse: true,
					WeekDay: weekDay, //星期几				
					MonthDay: month + "-" + day, //月和日
					DateCode: DateCode, //日期
					Day: day, //第几天
					LunarDay: LunarDay //农历日期
				};
				row[weekDay] = Ext.JSON.encode(obj);
				//固定节假日处理
				Ext.Array.each(me.DefaultHolidays, function(obj, index) {
					if((month + "-" + day) == obj) {
						row["Set" + weekDay] = true;
					}
				});
				if(j == 6) {
					colIndex = 0;
					break;
				}
				colIndex = colIndex = 1;
			}

			nullRecords.push(row);
		}
		return nullRecords;
	},
	/**
	 * 考勤工作日数据集处理
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
		var reg = new RegExp("null", "g");
		//数据库的结果集
		for(var i = 0; i < list.length; i++) {
			//数据库的节假日记录行
			var record = list[i];
			var weekDay = me.changeweekDay(record.DateCode);
			//找到空数据集里的匹配行和列,用DateCode与空记录行的DateCode匹配比较
			for(var rowIndex = 0; rowIndex < nullRecords.length; rowIndex++) {
				var tempObj = null;
				var weekDayInfo = nullRecords[rowIndex][weekDay];
				if(weekDayInfo != "") {
					tempObj = Ext.JSON.decode(weekDayInfo);
				}
				var tempday1 = -1,
					tempday2 = 0;
				//取空数据集的星期几列的日期的第几天值
				if(tempObj != null) {
					var tempArr2 = tempObj.DateCode.toString().split("-");
					tempday2 = tempArr2[tempArr2.length - 1];
					if(tempday2.length == 1)
						tempday2 = "0" + tempday2;
				}
				record.DateCode = record.DateCode.replace(/\//g, "-");

				var tempArr1 = record.DateCode.toString().split("-");
				if(tempArr1 != null) {
					tempday1 = tempArr1[tempArr1.length - 1];
					if(tempday1.length == 1)
						tempday1 = "0" + tempday1;
				}

				//两个日期的第几天相同
				if(tempday2 == tempday1) {
					record.Day = tempObj.Day;
					record.LunarDay = tempObj.LunarDay;

					var objStr = Ext.JSON.encode(record);
					objStr = objStr.replace(reg, "''");
					//需要封装到星期几列的信息
					nullRecords[rowIndex][weekDay] = objStr;
					//设置星期几的列值,星期六星期天特殊处理(星期六星期天改工作日)
					//SettingType：1、工作日改节假日，2节假日改工作日
					switch(weekDay) {
						case "Sunday":
							var SettingType = record.SettingType;
							nullRecords[rowIndex]["Set" + weekDay] = (SettingType == 1 ? true : false);
						case "Saturday":
							nullRecords[rowIndex]["Set" + weekDay] = (SettingType == 1 ? true : false);
							break;
						default:
							//星期一到星期五,工作日改节假日
							nullRecords[rowIndex]["Set" + weekDay] = true;
							break;
					}
					break;
				}
			}
		}
		result.list = nullRecords;
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
	/*封装实体信息信息*/
	getOneEntity: function() {
		var me = this;
		var entity = {
			Id: '-1',
			Year: '',
			Month: '',
			DateCode: '',
			SettingType: -1,
			HolidayName: '',
			Name: '',
			SName: '',
			IsUse: true
		};
		return entity;
	},
	/*保存按钮处理*/
	onSaveClick: function() {
		var me = this,
			isChecked = false,
			delIdStr = "",
			addArr = [],
			delArr = [];
		/*每一行里的星期一到星期天的列信息及与该行的设置星期一到设置星期天的列信息合并,
		 *找出哪些是新增的节假日/工作日信息和哪些是需要删除的节假日/工作日信息
		 */
		me.store.each(function(record) {
			//每一行里的所有列
			for(var i = 0; i < me.columns.length; i++) {
				var columnName = me.columns[i].dataIndex;
				var obj = null; //每一个星期几的封装信息(包含Id)
				var columnValue = record.get(columnName);
				switch(columnName) {
					case "Monday":
						if(columnValue != null && columnValue != "")
							obj = Ext.JSON.decode(columnValue);
						break;
					case "Tuesday":
						if(columnValue != null && columnValue != "")
							obj = Ext.JSON.decode(columnValue);
						break;
					case "Wednesday":
						if(columnValue != null && columnValue != "")
							obj = Ext.JSON.decode(columnValue);
						break;
					case "Thursday":
						if(columnValue != null && columnValue != "")
							obj = Ext.JSON.decode(columnValue);
						break;
					case "Friday":
						if(columnValue != null && columnValue != "")
							obj = Ext.JSON.decode(columnValue);
						break;
					case "Saturday":
						if(columnValue != null && columnValue != "")
							obj = Ext.JSON.decode(columnValue);
						break;
					case "Sunday":
						if(columnValue != null && columnValue != "")
							obj = Ext.JSON.decode(columnValue);
						break;
					default:
						break;
				}

				if(obj != null) {
					var id = obj.Id;
					isChecked = record.get("Set" + columnName);
					var entity = null;
					if((id == null || id == "")) {
						entity = me.getOneEntity();
						entity.Year = obj.Year;
						entity.Month = obj.Month;
						if(obj.DateCode != "") {
							entity.DateCode = JShell.Date.toServerDate(obj.DateCode);
						}
					}

					switch(columnName) {
						case "Saturday":
							if(isChecked == false && (id == null || id == "")) {
								entity.SettingType = 2;
								addArr.push(entity);
							} else if(id != null && id != "") {
								//需要删除的星期六工作日设置
								if(isChecked == true) {
									delArr.push(id);
								}
							}
							break;
						case "Sunday":
							if(isChecked == false && (id == null || id == "")) {
								entity.SettingType = 2;
								addArr.push(entity);
							} else if(id != null && id != "") {
								//需要删除的星期日工作日设置
								if(isChecked == true) {
									delArr.push(id);
								}
							}
							break;
						default:
							//星期一到星期五,isChecked为true才是新增
							if(isChecked == true && (id == null || id == "")) {
								entity.SettingType = 1;
								addArr.push(entity);
							} else if(id != null && id != "") {
								//需要删除的节假日设置
								if(isChecked == false) {
									delArr.push(id);
								}
							}
							break;
					}

				}
			}
		});

		me.showAddInfo = "";
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = 0;

		//新增或修改完是否显示提示信息
		var isSearch = true;
		//新增的考勤信息处理
		if(addArr.length > 0) {
			if(delArr.length > 0) {
				isSearch = false;
			}
			me.showMask(me.saveText); //显示遮罩层
			me.saveCount = 0;
			me.saveErrorCount = 0;
			me.saveLength = addArr.length;
			Ext.Array.each(addArr, function(entity, index) {
				me.addSave(entity, index, isSearch);
			});
			me.hideMask(); //隐藏遮罩层
		}

		//删除的考勤信息处理
		if(delArr.length > 0) {
			isSearch = true;
			me.showMask(me.saveText); //显示遮罩层
			me.saveCount = 0;
			me.saveErrorCount = 0;
			me.saveLength = 1;
			delIdStr = delArr.toString();
			if(delIdStr != "" && delIdStr.length > 0)
				me.delByIdStr(1, delIdStr, isSearch);
		}
	},
	/*保存单个修改信息*/
	editSave: function(entity, index, isSearch) {
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = Ext.JSON.encode({
			entity: entity,
			fields: "Id,SettingType"
		});
		me.postSave(url, params, index, isSearch);
	},
	/*保存单个新增信息*/
	addSave: function(entity, index, isSearch) {
		var me = this;
		var url = JShell.System.Path.getUrl(me.addUrl);
		var params = Ext.JSON.encode({
			entity: entity
		});
		me.postSave(url, params, index, isSearch);
	},
	/*删除已经设置好的节假日信息*/
	delByIdStr: function(index, idStr, isSearch) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'idStr=' + idStr;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if(data.success) {
					if(isSearch == true) {
						me.onSearch();
						JShell.Msg.alert("保存操作处理成功", null, 1000);
					}
				} else {
					JShell.Msg.error("保存操作(删除处理)失败!<br />" + data.msg);
				}
				me.hideMask(); //隐藏遮罩层
			});
		}, 100 * index);
	},
	/*提交新增或者修改信息操作*/
	postSave: function(url, params, index, isSearch) {
		var me = this;
		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				if(data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						if(isSearch == true) {
							me.onSearch();
							JShell.Msg.alert("保存操作成功", null, 1000);
						}
					}
				}
			});
		}, 100 * (index + 1));
	}
});