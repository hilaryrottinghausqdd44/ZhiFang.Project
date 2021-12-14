/**
 * 报表查询条件
 * @author liangyl	
 * @version 2017-01-23
 */
Ext.define('Shell.class.oa.at.attendance.statistical.empdetail.basic.SearchToolbar', {
	extend: 'Shell.ux.toolbar.Button',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	height: 95,
	/**布局方式*/
	layout: 'absolute',
	/**是否级联选中子节点*/
	isCheckTree: false,
	defaultStatusValue: '',
	/**默认申请时间*/
	defaultApplyDate: null,
	/**收缩高度*/
	toolbarHeight: 95,
	/**导出数据服务路径*/
	downLoadExcelUrl: '/WeiXinAppService.svc/SC_UDTO_DownLoadExportExcelOfATEmpAttendanceEventLogDetail',
	/**请假类型*/
	searchType: 10301,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},

	initComponent: function() {
		var me = this;
		me.addEvents('search');
		//初始化申请时间
		me.initDate();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		var ApplyDate = JcallShell.Date.toString(Sysdate, true);
		me.defaultApplyDate = ApplyDate;
	},
	/**@overwrite 获取列表布局组件内容*/
	getTableLayoutItems: function() {
		var me = this,
			items = [];
		items.push(me.EXCEL, me.ApplyDate, me.Today, me.Yesterday, me.Thisweek, me.Thismonth, me.Last7days, me.Onemonth, me.LastMonth, me.BeginDate, me.EndDate);
		//操作
		var buttons = me.createButtons();
		if(buttons) {
			items = items.concat(buttons);
		}
		return items;
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		//创建可见组件
		me.createShowItems();
		//创建隐形组件
		items = items.concat(me.createHideItems());
		//获取列表布局组件内容
		items = items.concat(me.getTableLayoutItems());
		return items;
	},
	/**创建可见组件*/
	createShowItems: function() {
		var me = this;
		//创建时间组件
		me.createDateItems();
		//创建字典选择组件
		me.createDictItems();
		//创建其他组件
		me.createOrderItems();
	},
	//创建字典选择组件
	createDictItems: function() {
		var me = this;
		var DEPTNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		//部门
		me.DeptName = {
			fieldLabel: '部门',
			emptyText: '部门',
			name: 'DeptName',
			itemId: 'DeptName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.oa.at.attendance.statistical.empdetail.basic.CheckTree',
			labelWidth: 35,
			width: 260,
			//value: DEPTNAME,
			classConfig: {
				title: '部门选择',
				checkOne: true
			}
		};
		var USERNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

		//员工
		me.UserName = {
			fieldLabel: '员工',
			emptyText: '员工',
			name: 'UserName',
			itemId: 'UserName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.sysbase.user.CheckApp',
			labelWidth: 35,
			width: 160,
			value: USERNAME,
			classConfig: {
				title: '员工选择',
				checkOne: false
			}
		};
		//职务
		me.JobsName = {
			fieldLabel: '职务',
			emptyText: '职务',
			name: 'JobsName',
			itemId: 'JobsName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.wfm.service.accept.CheckGrid',
			labelWidth: 35,
			width: 160,
			classConfig: {
				title: '职务选择',
				checkOne: true
			}
		};

		//审批状态
		me.ApproveStatusName = {
			fieldLabel: '审批状态',
			emptyText: '审批状态',
			name: 'ApproveStatusName',
			itemId: 'ApproveStatusName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.oa.at.attendance.statistical.empdetail.basic.ApproveStatusCheckGrid',
			labelWidth: 55,
			width: 260,
			classConfig: {
				title: '审批状态选择',
				checkOne: true
			}
		};
		//请假类型
		me.ATEmpApplyAllLog = {
			fieldLabel: '请假类型',
			emptyText: '请假类型',
			name: 'ATEmpApplyAllLog',
			itemId: 'ATEmpApplyAllLog',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.oa.at.attendance.statistical.empdetail.basic.LeaveTypeCheckGrid',
			labelWidth: 55,
			width: 260
		};

	},
	createOrderItems: function() {
		var me = this;
		//开始时间
		me.BeginDate2 = {
			width: 95,
			fieldLabel: '',
			labelWidth: 5,
			itemId: 'BeginDate2',
			name: 'BeginDate2',
			xtype: 'datefield',
			format: 'Y-m-d'
		};
		//结束时间
		me.EndDate2 = {
			width: 105,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate2',
			name: 'EndDate2',
			xtype: 'datefield',
			format: 'Y-m-d'
		};

		me.CheckGroup = {
			xtype: 'checkboxgroup',
			itemId: 'CheckGroup',
			name: 'CheckGroup',
			fieldLabel: '',
			columns: 2,
			vertical: true,
			items: [{
					boxLabel: '包含打卡且考勤正常的人',
					width: 160,
					name: 'rb',
					inputValue: '1',
					checked: true
				}, {
					boxLabel: '包含打卡但有异常的人 ',
					width: 145,
					name: 'rb',
					inputValue: '2',
					checked: true
				}
				//	            { boxLabel: '包含未打卡的人',width:115, name: 'rb', inputValue: '3', checked: true  },
				//	            { boxLabel: '包含已停职员工',width:110, name: 'rb', inputValue: '4' }
			]
		};
	},
	/**创建时间组件*/
	createDateItems: function() {
		var me = this;
		//导出
		me.EXCEL = {
			text: '导出',
			tooltip: 'EXCEL导出',
			iconCls: 'file-excel',
			xtype: 'button',
			width: 60,
			x: 5,
			y: 5,
			name: 'EXCEL',
			itemId: 'EXCEL',
			handler: function() {
				me.onDownLoadExcel();
			}
		};
		//申请时间
		me.ApplyDate = {
			x: 70,
			y: 5,
			name: 'ApplyDate',
			itemId: 'ApplyDate',
			xtype: 'displayfield',
			fieldLabel: '申请时间',
			labelWidth: 60
		};

		//今日
		me.Today = {
			text: '今日',
			tooltip: '今日',
			xtype: 'button',
			width: 45,
			x: 130,
			y: 5,
			name: 'Today',
			itemId: 'Today'
		};
		//昨日
		me.Yesterday = {
			text: '昨日',
			tooltip: '昨日',
			xtype: 'button',
			width: 45,
			x: 175,
			y: 5,
			name: 'Yesterday',
			itemId: 'Yesterday'
		};
		//本周
		me.Thisweek = {
			text: '本周',
			tooltip: '本周',
			xtype: 'button',
			width: 45,
			x: 220,
			y: 5,
			name: 'Thisweek',
			itemId: 'Thisweek'
		};
		//本月
		me.Thismonth = {
			text: '本月',
			tooltip: '本月',
			xtype: 'button',
			width: 45,
			x: 265,
			y: 5,
			name: 'Thismonth',
			itemId: 'Thismonth'
		};
		//上月
		me.LastMonth = {
			text: '上月',
			tooltip: '上月',
			xtype: 'button',
			width: 45,
			x: 310,
			y: 5,
			name: 'LastMonth',
			itemId: 'LastMonth'
		};
		//最近7天
		me.Last7days = {
			text: '最近7天',
			tooltip: '最近7天',
			xtype: 'button',
			width: 55,
			x: 365,
			y: 5,
			name: 'Last7days',
			itemId: 'Last7days'
		};

		//最近30天
		me.Onemonth = {
			text: '最近30天',
			tooltip: '最近30天',
			xtype: 'button',
			width: 62,
			x: 430,
			y: 5,
			name: 'Onemonth',
			itemId: 'Onemonth'
		};
		//开始时间
		me.BeginDate = {
			x: 530,
			y: 5,
			width: 95,
			fieldLabel: '',
			labelWidth: 5,
			itemId: 'BeginDate',
			name: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			emptyText: '必填项',
			allowBlank: false,
			value: me.defaultApplyDate
		};
		//结束时间
		me.EndDate = {
			x: 630,
			y: 5,
			width: 105,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			name: 'EndDate',
			xtype: 'datefield',
			value: me.defaultApplyDate,
			emptyText: '必填项',
			allowBlank: false,
			format: 'Y-m-d'
		};
	},
	createHideItems: function() {
		var me = this,
			items = [];
		var USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var DEPTID = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		items.push({
			fieldLabel: '部门Id',
			xtype: 'textfield',
			hidden: true,
			name: 'DeptID',
			//value: DEPTID,
			itemId: 'DeptID'
		});
		items.push({
			fieldLabel: '员工Id',
			xtype: 'textfield',
			value: USERID,
			hidden: true,
			name: 'UserID',
			itemId: 'UserID'
		});
		items.push({
			fieldLabel: '职务Id',
			xtype: 'textfield',
			hidden: true,
			name: 'JobsID',
			itemId: 'JobsID'
		});
		items.push({
			fieldLabel: '审批状态',
			xtype: 'textfield',
			hidden: true,
			name: 'ApproveStatusID',
			itemId: 'ApproveStatusID'
		});
		items.push({
			fieldLabel: '请假类型',
			xtype: 'textfield',
			hidden: true,
			name: 'ATEmpApplyAllLogID',
			itemId: 'ATEmpApplyAllLogID'
		});
		return items;
	},
	/**创建功能按钮*/
	createButtons: function() {
		var me = this,
			items = [];
		items.push({
			x: 750,
			y: 5,
			width: 60,
			iconCls: 'button-cancel',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '清空',
			tooltip: '<b>清空查询条件</b>',
			handler: function() {
				me.onClearSearch();
			}
		}, {
			x: 820,
			y: 5,
			width: 60,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				me.onFilterSearch();
			}
		}, {
			x: 890,
			y: 5,
			width: 22,
			iconCls: 'button-up',
			margin: '0 0 0 4px',
			xtype: 'button',
			tooltip: '<b>收缩</b>',
			itemId: 'up',
			handler: function() {
				this.ownerCt.getComponent('down').show();
				this.ownerCt.getComponent('up').hide();
				me.OrderHide(false);
				me.setHeight(30);
			}
		}, {
			x: 890,
			y: 5,
			width: 22,
			iconCls: 'button-down',
			margin: '0 0 0 4px',
			xtype: 'button',
			hidden: true,
			itemId: 'down',
			tooltip: '<b>展开</b>',
			handler: function() {
				this.ownerCt.getComponent('down').hide();
				this.ownerCt.getComponent('up').show();
				me.OrderHide(true);
				me.setHeight(me.toolbarHeight);
			}
		});
		return items;
	},
	/**隐藏其他组件*/
	OrderHide: function(bo) {
		var me = this;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//部门
		var DeptName = me.getComponent('DeptName'),
			DeptID = me.getComponent('DeptID');
		if(DeptName) {
			DeptName.on({
				check: function(p, record) {
					var onlyShowDept = p.getComponent('topToolbar').getComponent('onlyShowDept');
					me.isCheckTree = onlyShowDept.getValue();
					DeptName.setValue(record ? record.get('text') : '');
					DeptID.setValue(record ? record.get('tid') : '');
					p.close();
				}
			});
		}
		//审批状态
		var ApproveStatusName = me.getComponent('ApproveStatusName'),
			ApproveStatusID = me.getComponent('ApproveStatusID');
		if(ApproveStatusName) {
			ApproveStatusName.on({
				check: function(p, record) {
					if(ApproveStatusName) ApproveStatusName.setValue(record ? record.get('ATApproveStatus_Name') : '');
					if(ApproveStatusID) ApproveStatusID.setValue(record ? record.get('ATApproveStatus_Id') : '');
					p.close();
				}
			});
		}

		//员工
		var UserName = me.getComponent('UserName');
		var UserID = me.getComponent('UserID');
		if(UserName) {
			UserName.on({
				check: function(p, record) {
					var Id = '',
						Name = '';
					for(var i = 0; i < record.length; i++) {
						if(i > 0) {
							Id += ",";
							Name += ",";
						}
						Id += record[i] ? record[i].get('HREmployee_Id') : '';
						Name += record[i] ? record[i].get('HREmployee_CName') : '';
					}
					UserName.setValue(Name);
					UserID.setValue(Id);
					p.close();
				}
			});
		}

		//请假类型
		var ATEmpApplyAllLog = me.getComponent('ATEmpApplyAllLog'),
			ATEmpApplyAllLogID = me.getComponent('ATEmpApplyAllLogID');
		if(ATEmpApplyAllLog) {
			ATEmpApplyAllLog.on({
				check: function(p, record) {
					var Id = '',
						Name = '';
					if(record) {
						for(var i = 0; i < record.length; i++) {
							if(i > 0) {
								Id += ",";
								Name += ",";
							}
							Id += record[i] ? record[i].get('Id') : '';
							Name += record[i] ? record[i].get('Name') : '';
						}
					}
					ATEmpApplyAllLog.setValue(Name);
					ATEmpApplyAllLogID.setValue(Id);
					p.close();

				}
			});
		}

		//今日
		var Today = me.getComponent('Today'),
			//昨日
			Yesterday = me.getComponent('Yesterday'),
			//本周
			Thisweek = me.getComponent('Thisweek'),
			//本月
			Thismonth = me.getComponent('Thismonth'),
			//上月
			LastMonth = me.getComponent('LastMonth'),

			//最近7天
			Last7days = me.getComponent('Last7days'),
			//最近30天
			Onemonth = me.getComponent('Onemonth');
		//开始时间
		var BeginDate = me.getComponent('BeginDate');
		var EndDate = me.getComponent('EndDate');
		var Sysdate = JcallShell.System.Date.getDate();
		var ApplyDate = JcallShell.Date.toString(Sysdate, true);
		Today.on({
			click: function() {
				BeginDate.setValue(ApplyDate);
				EndDate.setValue(ApplyDate);
			}
		});
		Yesterday.on({
			click: function() {
				var Yesterdaydate = JShell.Date.getNextDate(Sysdate, -1);
				BeginDate.setValue(Yesterdaydate);
				EndDate.setValue(ApplyDate);
			}
		});
		var nowDayOfWeek = Sysdate.getDay(); //今天本周的第几天
		var nowDay = Sysdate.getDate(); //当前日     
		var LastMonthValue = Sysdate.getMonth(); //上月 
		var nowYear = Sysdate.getYear(); //当前年   
		nowYear += (nowYear < 2000) ? 1900 : 0; // 
		Thisweek.on({
			click: function() {
				//获得本周的开始日期
				var getWeekStartDate = new Date(nowYear, LastMonthValue, nowDay - nowDayOfWeek);
				var getWeekStartDate = me.formatDate(getWeekStartDate);
				//获得本周的结束日期
				var getWeekEndDate = new Date(nowYear, LastMonthValue, nowDay + (6 - nowDayOfWeek));
				var getWeekEndDate = me.formatDate(getWeekEndDate);
				BeginDate.setValue(getWeekStartDate);
				EndDate.setValue(getWeekEndDate);
			}
		});
		//当月
		Thismonth.on({
			click: function() {
				//获得本月的开始日期
				var getMonthStartDate = new Date(nowYear, LastMonthValue, 1);
				var getMonthStartDate = me.formatDate(getMonthStartDate);
				//获得本月的结束日期
				var myDate = JcallShell.Date.toString(Sysdate, true);
				var dayCount = JcallShell.Date.getCountDays(myDate); //该月天数
				var getMonthEndDate = new Date(nowYear, LastMonthValue, dayCount);
				var getMonthEndDate = me.formatDate(getMonthEndDate);
				BeginDate.setValue(getMonthStartDate);
				EndDate.setValue(getMonthEndDate);
			}
		});
		//上月
		LastMonth.on({
			click: function() {
				//获得本月的上一个月的日期
				var yearValue = Sysdate.getFullYear();
				var monthValue = Sysdate.getMonth();
				if(monthValue == 0) {
					monthValue = 12;
					yearValue = yearValue - 1;
				}
				if(monthValue < 10) {
					monthValue = "0" + monthValue;
				}
				var startDate = "" + yearValue + "-" + monthValue + "-" + "01";
				var getMonthStartDate = JcallShell.Date.getDate(startDate);
				var getMonthStartDate = me.formatDate(getMonthStartDate);
				//获得本月的结束日期
				var myDate = JcallShell.Date.toString(getMonthStartDate, true);
				var dayCount = JcallShell.Date.getCountDays(myDate); //该月天数

				var getMonthEndDate = JShell.Date.getNextDate(startDate, dayCount - 1);
				var getMonthEndDate = me.formatDate(getMonthEndDate);
				BeginDate.setValue(getMonthStartDate);
				EndDate.setValue(getMonthEndDate);
			}
		});
		Last7days.on({
			click: function() {
				var lastdays = JShell.Date.getNextDate(Sysdate, -7);
				BeginDate.setValue(lastdays);
				EndDate.setValue(ApplyDate);
			}
		});
		Onemonth.on({
			click: function() {
				var monthdays = JShell.Date.getNextDate(Sysdate, -30);
				BeginDate.setValue(monthdays);
				EndDate.setValue(ApplyDate);
			}
		});
	},
	//格式化日期：yyyy-MM-dd     
	formatDate: function formatDate(date) {
		var myyear = date.getFullYear();
		var mymonth = date.getMonth() + 1;
		var myweekday = date.getDate();

		if(mymonth < 10) {
			mymonth = "0" + mymonth;
		}
		if(myweekday < 10) {
			myweekday = "0" + myweekday;
		}
		return(myyear + "-" + mymonth + "-" + myweekday);
	},
	/**清空查询内容*/
	onClearSearch: function() {
		var me = this;
		var BeginDate = me.getComponent('BeginDate');
		var EndDate = me.getComponent('EndDate');
		var DeptID = me.getComponent('DeptID');
		var UserID = me.getComponent('UserID');
		var ApproveStatusID = me.getComponent('ApproveStatusID');
		var ATEmpApplyAllLogID = me.getComponent('ATEmpApplyAllLogID');
		var ATEmpApplyAllLog = me.getComponent('ATEmpApplyAllLog');
		var DeptName = me.getComponent('DeptName');
		var UserName = me.getComponent('UserName');
		var ApproveStatusName = me.getComponent('ApproveStatusName');
		me.isCheckTree = false;

		BeginDate.setValue(me.defaultApplyDate);
		EndDate.setValue(me.defaultApplyDate);
		DeptID.setValue(null);
		UserID.setValue(null);

		DeptName.setValue('');
		UserName.setValue('');

		if(ApproveStatusName) ApproveStatusName.setValue('');
		if(ApproveStatusID) ApproveStatusID.setValue(null);

		if(ATEmpApplyAllLogID) ATEmpApplyAllLogID.setValue(null);
		if(ATEmpApplyAllLog) ATEmpApplyAllLog.setValue('');
	},
	/**查询处理*/
	onFilterSearch: function() {
		var me = this;
		var params = me.getParams();
		me.fireEvent('search', me, params);
	},
	/**导出处理*/
	onDownLoadExcel: function() {
		var me = this;
		var params = me.getParams();
		me.DownLoadExcel(params);
	},

	/**导出EXCEL文件*/
	DownLoadExcel: function(pams) {
		var me = this,
			operateType = '0';
		var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;
		var params = [];
		params.push("operateType=" + operateType);
		params.push("searchType=" + me.searchType);
		params.push("attypeId=" + pams.ATEmpApplyAllLog);
		params.push("deptId=" + pams.DeptID);
		params.push("isGetSubDept=" + pams.isCheckTree);
		params.push("empId=" + pams.UserID);
		params.push("startDate=" + pams.applySDate);
		params.push("endDate=" + pams.applyEDate);
		params.push("approveStatusID=" + pams.ApproveStatusID);
		url += "?" + params.join("&");
		window.open(url);
	},
	/**获取参数*/
	getParams: function() {
		var me = this,
			DeptID = '',
			UserID = '',
			ApproveStatusID = -1,
			SDate = '',
			EDate = '',
			applySDate = '',
			applyEDate = '',
			ATEmpApplyAllLog = '',
			params = {};

		params.applySDate = applySDate;
		params.applyEDate = applyEDate;
		params.DeptID = DeptID;
		params.UserID = UserID;
		params.ApproveStatusID = ApproveStatusID;
		params.ATEmpApplyAllLog = ATEmpApplyAllLog;
		if(me.getComponent('BeginDate').getValue()) {
			params.applySDate = JcallShell.Date.toString(me.getComponent('BeginDate').getValue(), true);
		}
		if(me.getComponent('EndDate').getValue()) {
			params.applyEDate = JcallShell.Date.toString(me.getComponent('EndDate').getValue(), true);
		}
		if(me.getComponent('DeptID').getValue()) {
			params.DeptID = me.getComponent('DeptID').getValue();
		}
		if(me.getComponent('UserID').getValue()) {
			params.UserID = me.getComponent('UserID').getValue();
		}
		if(me.getComponent('ApproveStatusID').getValue()) {
			params.ApproveStatusID = me.getComponent('ApproveStatusID').getValue();
		}
		if(me.getComponent('ATEmpApplyAllLogID').getValue()) {
			params.ATEmpApplyAllLog = me.getComponent('ATEmpApplyAllLogID').getValue();
		}
		params.isCheckTree = me.isCheckTree;
		return params;
	}
});