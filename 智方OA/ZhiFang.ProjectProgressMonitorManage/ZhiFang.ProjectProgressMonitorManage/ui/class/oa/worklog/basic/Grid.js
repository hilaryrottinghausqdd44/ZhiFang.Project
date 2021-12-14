/**
 * 工作日志
 * @author longfc
 * @version 2016-08-01
 */
Ext.define('Shell.class.oa.worklog.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '工作日志',
	width: 535,
	height: 580,
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	layout: 'fit',
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType',
	/**默认排序字段*/
	defaultOrderBy: [],
	defaultPageSize: 100,
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: false,
	/**周列表*/
	DateTimeList: [
		['1', '第1周'],
		['2', '第2周'],
		['3', '第3周'],
		['4', '第4周'],
		['5', '第5周']
	],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	hasPagingtoolbar: true,
	/**周默认值*/
	WeekValue: '1',
	hasRownumberer: false,
	/**是否启用新增按钮*/
	hasAdd: true,
	hasEidt: true,
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

	/**日志外键名称
	 * @author Jcall
	 * @version 2016-08-19
	 */
	LogName: '',

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
		me.addEvents('onAddClick');
		me.addEvents('onEditClick');

		JShell.Function.showWorklogInteraction = function(ele, e) {
			var LogName = ele.getAttribute("LogName");
			var Id = ele.getAttribute("data");
			me.showInteractionById(LogName, Id);

			if(e.stopPropagation) {
				e.stopPropagation();
			} else {
				e.cancelBubble = true;
			}
		};

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

		me.buttonToolbarItems.push({
			width: 95,
			xtype: 'uxYearComboBox',
			itemId: 'Year',
			minValue: 2016, //最小年份，@author Jcall，@version 2016-08-18
			value: me.YearValue
		}, {
			width: 80,
			xtype: 'uxMonthComboBox',
			itemId: 'Month',
			value: me.MonthValue,
			margin: '0 2px 0 10px',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != null && newValue != "") {
						me.onSearch();
					}
				}
			}
		});
		me.buttonToolbarItems.push({
			width: 80,
			fieldLabel: '',
			xtype: 'uxSimpleComboBox',
			itemId: 'Week',
			hidden: !me.hasWeeked,
			hasStyle: true,
			value: me.WeekValue,
			data: me.DateTimeList,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(me.hasWeeked == true && newValue && newValue != null && newValue != "") {
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
		}, '-', '->');
		if(me.hasRefresh) {
			me.buttonToolbarItems.push('refresh');
		}
		if(me.hasAdd) {
			me.buttonToolbarItems.push('add');
		}
		if(me.hasEidt) {
			me.buttonToolbarItems.push('edit');
		}
	},
	/***
	 * 判断substr字符串在str中出现的次数  isIgnore是否忽略大小写!
	 * @param {Object} str
	 * @param {Object} substr
	 * @param {Object} isIgnore
	 */
	countSubstr: function(str, substr, isIgnore) {
		var count;
		var reg = "";
		if(isIgnore == true) {
			reg = "/" + substr + "/gi";
		} else {
			reg = "/" + substr + "/g";
		}
		reg = eval(reg);
		if(str.match(reg) == null) {
			count = 0;
		} else {
			count = str.match(reg).length;
		}
		return count;
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
			width: 10,
			isDate: true,
			hasTime: true,
			hidden: true,
			sortable: false,
			menuDisabled: true
		}, {
			text: '工作日志',
			dataIndex: 'ToDayContent',
			flex: 1,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return me.ToDayContentRenderer(value, meta, record, rowIndex, colIndex, store, view);
			}
		}, {
			text: '日期标识',
			dataIndex: 'DateCode',
			width: 10,
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
	ToDayContentRenderer: function(value, meta, record, rowIndex, colIndex, store, view) {
		var me = this;

		var arr = [];
		var toDayContent = "";
		if(value != null) {
			value = value.replace(/\\r\\n/g, "<br />");
			value = value.replace(/\\n/g, "<br />");

			toDayContent = value.toString();
			var re = new RegExp("<br />", "g");
			arr = value.match(re);
		}
		if(arr && arr.length > 1) {
			var index = toDayContent.indexOf("<br />");
			index = (index > 85 ? 85 : index);
			toDayContent = toDayContent.substring(0, index) + "......"; //(index > 85?"......":"");
		} else {
			toDayContent = toDayContent.substring(0, 85) + (toDayContent.length > 85 ? "......" : "");
		}

		toDayContent = toDayContent.replace(/\\n/g, "<br />");
		var qtipToDayContent = value;
		var qtipStrValue = "";
		var empName = record.get('EmpName');
		var dataAddTime = record.get('DataAddTime').replace(/\//g, "-");
		var image1 = record.get('Image1');

		var picpath = '../ui/images/user/user.png';
		var worklogtype = "";
		var dateTimeStr = "<span style='color:blue;vertical-align:top;'>填报时间:" + dataAddTime + "</span><br />";
		var copyForEmpName = "<span style='width:100%;color:green;font-weight:bold'>抄送:@" + record.get("CopyForEmpNameList") + "</span><br />";

		var strValue = "<table style='width:100%;border-bottom-style:solid; border-bottom-width:0px; border-color:darkgrey' border='0'>" +
			"<tr style='margin:2px;height:29px'>" +
			"<td rowspan='1' width='50' align='center' valign='middle'>" +
			"<img width='50' src='" + picpath + "'/></td>" +
			"<td width='105' valign='middle' colspan='3'>" +
			"<div style='margin-left:5px;font-weight:bold'>" + empName + "</div>" +
			"<div style='float :left;margin-left:5px;font-weight:bold'>日期：" + dataAddTime + "</div>" + worklogtype +
			"</td>" +

			//交流，@author Jcall，@version 2016-08-19
			"<td width='50' valign='middle' colspan='1'>" +
			"<div class='hand' data='" + record.get(me.PKField) + "' LogName='" + me.LogName +
			"' style='margin-left:5px;padding:5px 10px;background-color:#337ab7;'" +
			" onclick='JShell.Function.showWorklogInteraction(this,event)'>" +
			"<a style='color:#ffffff;'>交流</a>" +
			"</div>" +
			"</td>" +

			"</tr>" +
			//--------------------------------------------

			"<tr style='margin:2px;width:100%;'>" +
			"<td colspan='4'>" +
			"内容:" + toDayContent +
			"</td>" +
			"</tr>" +

			"<tr style='margin:2px;width:100%;'>" +
			"<td colspan='4'>" +
			dateTimeStr +
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

		meta.style = 'width:98%;padding:2px 4px;text-overflow: ellipsis;white-space: nowrap;white-space:normal;line-height:20px;word-break:break-all; word-wrap:break-word;';
		meta.css = "background:#93A9C1;";
		return strValue;
	},
	/**获取带查询条件*/
	getSearchWhereParams: function() {
		var me = this,
			where = "";
		return where;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		//		var reg = new RegExp("\\n", "g");
		//		if(data.value && data.value != null && data.value.length > 0) {
		//			Ext.Array.each(data.value, function(obj, index) {
		//				if(data.value[index] != null)
		//					data.value[index].ToDayContent = obj.ToDayContent.replace(reg, "<br />");
		//			});
		//		}
		return data;
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
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var date = JShell.Date.getNextDate(new Date(), 0);
		var year = date.getFullYear();
		var month = date.getMonth() + 1;
		me.YearValue = year;
		me.MonthValue = month;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick');
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		me.fireEvent('onEditClick');
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
	}
});