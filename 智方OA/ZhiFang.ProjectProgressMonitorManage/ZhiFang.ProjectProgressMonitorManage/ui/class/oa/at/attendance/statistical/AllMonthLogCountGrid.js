/**
 * 公司月考勤统计查看
 * @author longfc
 * @version 2016-10-26
 */
Ext.define('Shell.class.oa.at.attendance.statistical.AllMonthLogCountGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '公司月考勤统计信息',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	features: [{
		ftype: 'summary'
	}],
	/**获取数据服务路径*/
	selectUrl: "/WeiXinAppService.svc/GetAllMonthLogCountList",
	downLoadExcelUrl: "/WeiXinAppService.svc/SC_UDTO_DownLoadExportExcelOfAllMonthLogCount",
	/**查询月份*/
	MONTTCODE: JShell.Date.getDate(new Date()).getMonth() + 1,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	defaultPageSize: 2000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: false,
	remoteSort: false,
	qtipValue: "事假,病假,年假,出差天数是按统计月的工作日统计;<br >在统计婚假、丧假、产假、护理假是按统计月的自然日统计;<br >出差天数按统计月的工作日出差天数统计;出差存休是按统计月的实际出差天数统计.",
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'DeptName',
		direction: 'ASC'
	}, {
		property: 'EmpName',
		direction: 'ASC'
	}],
	hasRefresh: true,
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
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];

		columns.push({
			text: '部门',
			dataIndex: 'DeptName',
			width: 70,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '编号',
			dataIndex: 'EmpNo',
			width: 80,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '姓名',
			dataIndex: 'EmpName',
			width: 75,
			//flex:1,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true,
			summaryRenderer: function(value) {
				return '<strong>合计</strong>';
			}
		}, {
			text: '全勤',
			dataIndex: 'IsFullAttendance',
			width: 40,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			//			summaryRenderer: function(value) {
			//				return '<strong>合计</strong>';
			//			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				if(value && value == "否") {
					meta.style = 'font-weight:bold;color:red';
				}
				return value;
			}
		});
		columns.push({
			header: '签到(天)',
			align: 'center',
			dataIndex: 'SignInDays',
			width: 55,
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, "0") + '</strong>';
			}
		}, {
			header: '迟到(次)',
			align: 'center',
			dataIndex: 'LateCount',
			width: 55,
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, "0");
				if(value && parseInt(value) > 0) {
					meta.style = 'font-weight:bold;color:red';
				}
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, "0") + '</strong>';
			}
		}, {
			header: '签退(天)',
			align: 'center',
			dataIndex: 'SignOutDays',
			width: 55,
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, "0") + '</strong>';
			}
		}, {
			header: '早退(次)',
			align: 'center',
			dataIndex: 'LeaveEarlyCount',
			width: 55,
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, "0");
				if(value && parseInt(value) > 0) {
					meta.style = 'font-weight:bold;color:red';
				}
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, "0") + '</strong>';
			}
		},{
			header: '补签打卡',
			align: 'center',
			dataIndex: 'FillCardsDays',
			width: 65,
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, "0");
//				if(value && parseInt(value) > 0) {
//					meta.style = 'font-weight:bold;color:red';
//				}
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, "0") + '</strong>';
			}
		}, {
			header: '事假',
			align: 'center',
			dataIndex: 'JobLeaveDays',
			width: 65,
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '旷工',
			dataIndex: 'AbsenteeismDays',
			width: 65,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			summaryType: 'sum',
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				if(value && parseInt(value) > 0) {
					meta.style = 'font-weight:bold;color:red';
				}
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '入职缺勤',
			dataIndex: 'EntryAbsenceDays',
			width: 55,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				if(value && parseInt(value) > 0) {
					meta.style = 'font-weight:bold;color:red';
				}
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '离职缺勤',
			dataIndex: 'LeavingAbsenceDays',
			width: 55,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				if(value && parseInt(value) > 0) {
					meta.style = 'font-weight:bold;color:red';
				}
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '病假',
			dataIndex: 'SickLeaveDays',
			sortable: false,
			width: 55,
			align: 'center',
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '婚假',
			dataIndex: 'MarriageLeaveDays',
			width: 55,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '产假',
			dataIndex: 'MaternityLeaveDays',
			width: 55,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '护理假',
			dataIndex: 'CareLeaveDays',
			width: 50,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '丧假',
			dataIndex: 'BereavementLeaveDays',
			width: 55,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '调休',
			dataIndex: 'AdjustTheBreakDays',
			width: 55,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '年假',
			dataIndex: 'AnnualLeaveDays',
			width: 55,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '外出',
			dataIndex: 'EgressDays',
			width: 60,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '出差',
			dataIndex: 'TripDays',
			width: 60,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '加班',
			dataIndex: 'OvertimeDays',
			width: 60,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '出差存休',
			dataIndex: 'TravelHoliday',
			width: 60,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '未打卡(次)',
			dataIndex: 'NotPunch',
			width: 65,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, "0");
				if(value && parseInt(value) > 0) {
					meta.style = 'font-weight:bold;color:red';
				}
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, "0") + '</strong>';
			}
		}, {
			text: '缺勤',
			dataIndex: 'DaysOfAbsence',
			width: 70,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				if(value && parseInt(value) > 0) {
					meta.style = 'font-weight:bold;color:red';
				}
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '工资日',
			dataIndex: 'WagesDays',
			width: 65,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, "0") + '</strong>';
			}
		}, {
			text: '公司日',
			dataIndex: 'CompanyDays',
			width: 75,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			type: 'numbercolumn',
			xtype: 'numbercolumn',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				var infoEmp = "部门:" + record.get("DeptName") + "&nbsp;&nbsp;姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + me.qtipValue + '"';
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		});
		return columns;
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var me = this,
			columns = me._resouce_columns || [],
			length = columns.length,
			fields = [];
		var isString = false;
		for(var i = 0; i < length; i++) {
			if(columns[i].dataIndex) {
				var obj = isString ? columns[i].dataIndex : {
					name: columns[i].dataIndex,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var date = JShell.Date.getNextDate(new Date(), 0);
		var year = date.getFullYear();
		var month = date.getMonth() + 1;
		me.defaultYearValue = year;
		me.defaultMonthValue = month;
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
						me.getWorkDaysOfOneMonth();
						setTimeout(function() {
							me.onSearch();
						}, 500);
					}
				}
			}
		});
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
			tooltip: '<b>每月的工资日总天数</b>'

		}, {
			xtype: "numberfield",
			itemId: "punchCount",
			name: "punchCount",
			width: 155,
			minValue: 1,
			value: 1,
			maxValue: 6,
			labelWidth: 110,
			tooltip: '<b>每天签到的打卡次数</b>',
			fieldLabel: "每天签到打卡次数",
			style: {
				marginLeft: '10px'
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
	},

	/**初始化功能按钮栏内容*/
	initButtonToolbarItems: function() {
		var me = this;
		me.defaultButtonToolbarItems();
	},
	getWorkDaysOfOneMonth: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			year = buttonsToolbar.getComponent('Year').getValue(),
			month = buttonsToolbar.getComponent('Month').getValue();
		var wagesDays = buttonsToolbar.getComponent('wagesDays');
		if(year != "" && month != "") {
			var url = JShell.System.Path.ROOT + "/WeiXinAppService.svc/ST_UDTO_GetWorkDaysOfOneMonth";
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'year=' + year + "&month=" + month;
			JShell.Server.get(url, function(data) {
				if(data.success) {
					var workDays = data.value.workDays;
					if(workDays != "" && workDays != null)
						wagesDays.setValue(workDays);
				} else {

				}

			});
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
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this,
			monthCode = "",
			wagesDays = 0,
			punch = 0;
		me.store.removeAll(); //清空数据
		buttonsToolbar = me.getComponent('buttonsToolbar');
		var year = buttonsToolbar.getComponent('Year').getValue();
		var month = buttonsToolbar.getComponent('Month').getValue();
		var wagesDays = buttonsToolbar.getComponent('wagesDays').getValue();
		var punchCount = buttonsToolbar.getComponent('punchCount').getValue();
		var showInfo = "";
		var isExec = true;
		if(year == "") {
			showInfo = showInfo + "年份选择不能为空!<br />";
			isExec = false;
		}
		if(isExec == true && month == "") {
			showInfo = showInfo + "月份选择不能为空!<br />";
			isExec = false;
		}
		if(isExec == true && wagesDays == "") {
			showInfo = showInfo + "工资日不能为空!<br />";
			isExec = false;
		}
		if(isExec == true && punchCount == "") {
			showInfo = showInfo + "打卡次数不能为空!<br />";
			isExec = false;
		}
		if(year != "" && month != "") {
			monthCode = year + "-" + month;
		}
		if(isExec == true) {
			me.disableControl(); //禁用 所有的操作功能

			if(!me.defaultLoad) return false;
			me.getView().update();
			var url = JShell.System.Path.ROOT + me.selectUrl;
			url += "?monthCode=" + monthCode + "&wagesDays=" + wagesDays + "&punch=" + punchCount;
			me.store.proxy.url = url; //查询条件
		} else {
			JShell.Msg.alert(showInfo, null, 2000);
		}
	}
});