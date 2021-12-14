/**
 * 使用月用量统计月用量统计
 * @author longfc
 * @version 2018-09-10
 */
Ext.define('Shell.class.rea.client.statistics.monthusage.DocGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	//extend: 'Shell.class.rea.client.SearchGrid',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.YearAndMonthComboBox',
		'Shell.ux.form.field.DateArea'
	],
	title: '月用量统计',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaMonthUsageStatisticsDocByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaManageService.svc/RS_UDTO_DelReaMonthUsageStatisticsDocAndDtlByDocId',
	/**默认加载*/
	defaultLoad: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaMonthUsageStatisticsDoc_DataAddTime',
		direction: 'DESC'
	}],
	/**是否有收缩面板按钮*/
	hasCollapse: false,
	/**月结最小年份*/
	minYearValue: 2018,
	/**月结最大年份*/
	maxYearValue: 2018,
	/**月结最小选择项*/
	roundMinValue: null,
	/**月结最大选择项*/
	roundMaxValue: null,

	/**PDF报表模板*/
	pdfFrx: null,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 14,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",
	/**统计类型*/
	TypeIDKey: "ReaMonthUsageStatisticsDocType",
	/**周期类型*/
	RoundTypeKey: "ReaMonthUsageStatisticsDocRoundType",
	/**用户UI配置Key*/
	userUIKey: 'statistics.monthusage.DocGrid',
	/**用户UI配置Name*/
	userUIName: "月用量统计列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//me.initSearchDate(-30);
	},
	initComponent: function() {
		var me = this;

		JShell.REA.StatusList.getStatusList(me.TypeIDKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.RoundTypeKey, false, true, null);

		var Sysdate = JcallShell.System.Date.getDate();
		me.maxYearValue = Sysdate.getFullYear();
		me.roundMaxValue = Ext.util.Format.date(Sysdate, "Y-m");
		me.roundMinValue = me.minYearValue + "-01";

		//查询框信息
		me.searchInfo = {
			emptyText: '统计周期/领用部门',
			itemId: 'Search',
			//flex: 1,
			width: "81%",
			isLike: true,
			fields: ['reamonthusagestatisticsdoc.Round', 'reamonthusagestatisticsdoc.DeptName']
		};
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaMonthUsageStatisticsDoc_TypeID',
			text: '统计类型',
			width: 75,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.TypeIDKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.TypeIDKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.TypeIDKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.TypeIDKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.TypeIDKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.TypeIDKey].FColor[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDoc_RoundTypeId',
			text: '周期类型',
			width: 75,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.RoundTypeKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.RoundTypeKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.RoundTypeKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.RoundTypeKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.RoundTypeKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.RoundTypeKey].FColor[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDoc_DeptName',
			text: '领用部门',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDoc_Round',
			text: '统计周期',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDoc_StartDate',
			text: '起始日期',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDoc_EndDate',
			text: '结束日期',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDoc_Visible',
			text: '启用',
			width: 50,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDoc_DocNo',
			text: '月用量单号',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaMonthUsageStatisticsDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		items.push(me.createButtonToolbarItems3());
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());

		items.push(me.createRoundTypeToolbarItems());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createButtonsToolbarSearch());
		items.push(me.createPrintButtonToolbarItems());
		return items;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];

		return items;
	},
	/**创建功能 按钮栏*/
	createRoundTypeToolbarItems: function() {
		var me = this;
		var items = [];
		var typeIDList = JShell.REA.StatusList.Status["ReaMonthUsageStatisticsDocType"];
		if(typeIDList) {
			typeIDList = typeIDList.List;
		} else {
			typeIDList = [];
		}
		var roundTypeList = JShell.REA.StatusList.Status["ReaMonthUsageStatisticsDocRoundType"];
		if(roundTypeList) {
			roundTypeList = roundTypeList.List;
		} else {
			roundTypeList = [];
		}
		//统计类型
		items.push({
			width: "32%",
			labelWidth: 0,
			emptyText: '统计类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'TypeID',
			hasStyle: true,
			data: typeIDList,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		//周期类型
		items.push({
			width: "32%",
			labelWidth: 0,
			emptyText: '周期类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'RoundTypeId',
			hasStyle: true,
			data: roundTypeList,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			width: "32%",
			labelWidth: 0,
			emptyText: '统计周期',
			xtype: 'uxYearAndMonthComboBox',
			itemId: 'Round',
			minYearValue: me.minYearValue,
			maxYearValue: me.maxYearValue,
			minValue: me.roundMinValue,
			maxValue: me.roundMaxValue,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'roundTypeToolbar',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createDateAreaToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'uxdatearea',
			itemId: 'date',
			labelWidth: 55,
			labelAlign: 'right',
			fieldLabel: '日期范围',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'dateareaToolbar',
			items: items
		});
	},
	/**查询输入栏*/
	createButtonsToolbarSearch: function() {
		var me = this;
		var items = [];
		items.push('refresh');
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbarSearch',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems3: function() {
		var me = this;
		var items = [];

		items.push({
			width: 80,
			iconCls: 'button-add',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '新增统计',
			tooltip: '<b>新增月用量统计</b>',
			handler: function() {
				me.onAddClick();
			}
		});
		items.push('del');
		items.push('->', {
			iconCls: 'button-right',
			tooltip: '<b>收缩面板</b>',
			handler: function() {
				me.collapse();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar3',
			items: items
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var where = [];
		var roundTypeToolbar = me.getComponent('roundTypeToolbar'),
			typeID = roundTypeToolbar.getComponent('TypeID'),
			rundTypeId = roundTypeToolbar.getComponent('RoundTypeId'),
			round = roundTypeToolbar.getComponent('Round');

		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');

		var buttonsToolbarSearch = me.getComponent('buttonsToolbarSearch'),
			search = buttonsToolbarSearch.getComponent('Search');

		if(typeID) {
			var value = typeID.getValue();
			if(value)
				where.push("reamonthusagestatisticsdoc.TypeID=" + value);
		}
		if(rundTypeId) {
			var value = rundTypeId.getValue();
			if(value)
				where.push("reamonthusagestatisticsdoc.RoundTypeId=" + value + "");
		}
		if(round) {
			var value = round.getValue();
			if(value)
				where.push("reamonthusagestatisticsdoc.Round='" + value + "'");
		}
		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					where.push("reamonthusagestatisticsdoc.StartDate>='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push("reamonthusagestatisticsdoc.EndDate<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}

		if(search) {
			var value = search.getValue();
			if(value) {
				var searchHql = me.getSearchWhere(value);
				if(searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		return where.join(" and ");
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	/**@description 取消月用量*/
	onAddSave: function(round) {
		var me = this;
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_AddReaMonthUsageStatisticsDoc");
		var params = {
			"round": round
		};
		params = JShell.JSON.encode(params);
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				//me.fireEvent('onAddClick', me);
				me.onSearch();
			} else {
				JShell.Msg.error('新增月用量统计出错！' + data.msg);
			}
		});
	},
	/**@description 取消月用量*/
	onCancel: function(id) {
		var me = this;
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/ST_UDTO_UpdateCancelReaMonthUsageStatisticsDocById?id=" + id);
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				me.onSearch();
			} else {
				JShell.Msg.error('取消月用量统计出错！' + data.msg);
			}
		});
	},
	/**@description 打印月用量统计*/
	onPrintClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		if(!id) {
			JShell.Msg.error("请先保存打印月用量统计记录后,再打印月用量统计!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/");
		url += '?operateType=1&id=' + id;
		window.open(url);
	},
	/**@description 验证日期类型是否选择*/
	validDateType: function() {
		var me = this;
		return true;
	},
	/**@description 设置日期范围值*/
	onSetDateArea: function(day) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day);
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateAreaValue) date.setValue(dateAreaValue);
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		//sdate=Ext.Date.format(sdate,"Y-m-d");
		//edate=Ext.Date.format(edate,"Y-m-d");
		var dateArea = {
			start: sdate,
			end: edate
		};
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateArea) date.setValue(dateArea);
	},
	/**创建功能按钮栏Items*/
	createPrintButtonToolbarItems: function() {
		var me = this,
			items = [];
		//items.push('refresh');
		items = me.createReportClassButtonItem(items);
		items.push("-", me.createTemplate(), {
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '预览',
			//hidden: true,
			tooltip: '预览PDF清单',
			handler: function() {
				me.onPrintClick();
			}
		});
		items.push('-', {
			text: '导出',
			tooltip: 'EXCEL导出',
			iconCls: 'file-excel',
			xtype: 'button',
			width: 60,
			name: 'EXCEL',
			itemId: 'EXCEL',
			handler: function() {
				me.onDownLoadExcel();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbarPrint',
			items: items
		});
	},
	/**模板分类选择项*/
	createReportClassButtonItem: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}
		items.push({
			xtype: 'uxSimpleComboBox',
			itemId: 'ReportClass',
			labelWidth: 0,
			width: 75,
			value: me.reaReportClass,
			data: [
				["", "请选择"],
				["Frx", "PDF预览"],
				["Excel", "Excel导出"]
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onReportClassCheck(newValue);
				}
			}
		});
		return items;
	},
	/**模板选择项*/
	createTemplate: function() {
		var me = this;
		return {
			fieldLabel: '',
			emptyText: '模板选择',
			labelWidth: 0,
			width: 140,
			name: 'cboTemplate',
			itemId: 'cboTemplate',
			xtype: 'uxCheckTrigger',
			classConfig: {
				width: 195,
				height: 460,
				checkOne: true,
				/**BReportType:7*/
				breportType: me.breportType,
				/**模板分类:Excel模板,Frx模板*/
				publicTemplateDir: me.publicTemplateDir
			},
			className: 'Shell.class.rea.client.template.CheckGrid',
			listeners: {
				check: function(p, record) {
					me.onTemplateCheck(p, record);
				}
			}
		};
	},
	/**@description 报告模板分类选择后*/
	onReportClassCheck: function(newValue) {
		var me = this;
		me.reaReportClass = newValue;
		me.pdfFrx = "";
		if(me.reaReportClass == "Frx") {
			me.publicTemplateDir = "Frx模板";
		} else if(me.reaReportClass == "Excel") {
			me.publicTemplateDir = "Excel模板";
		}
		var buttonsToolbar = me.getComponent("buttonsToolbarPrint");
		var cbo = buttonsToolbar.getComponent("cboTemplate");
		cbo.setValue("");
		cbo.classConfig["publicTemplateDir"] = me.publicTemplateDir;
		var picker = cbo.getPicker();
		if(picker) {
			picker["publicTemplateDir"] = me.publicTemplateDir;
			cbo.getPicker().load();
		}
	},
	onTemplateCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbarPrint");
		var cbo = buttonsToolbar.getComponent("cboTemplate");
		var cname = "";
		if(record) {
			me.pdfFrx = record.get("FileName");
			cname = record.get("CName");
		} else {
			me.pdfFrx = "";
		}
		if(cbo) {
			cbo.setValue(cname);
		}
		p.close();
	},
	/**@description 选择某一试剂耗材订单,预览PDF清单*/
	onPrintClick: function() {
		var me = this,
			operateType = '1';
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		if(!id) {
			JShell.Msg.error("请先选择月用量统计单后再操作!");
			return;
		}
		if(!me.reaReportClass || me.reaReportClass != "Frx") {
			JShell.Msg.error("请先选择Frx模板后再操作!");
			return;
		}
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择Frx模板后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchBusinessReportOfPdfById");
		var params = [];
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("operateType=" + operateType);
		params.push("id=" + id);
		params.push("breportType=" + me.breportType);
		params.push("frx=" + JShell.String.encode(me.pdfFrx));
		url += "?" + params.join("&");
		window.open(url);
	},
	/**选择某一试剂耗材订单,导出EXCEL*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		if(!id) {
			JShell.Msg.error("请先选择月用量统计单后再操作!");
			return;
		}
		if(!me.reaReportClass || me.reaReportClass != "Excel") {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchBusinessReportOfExcelById");
		var params = [];
		params.push("operateType=" + operateType);
		params.push("id=" + id);
		params.push("breportType=" + me.breportType);
		if(me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	}
});