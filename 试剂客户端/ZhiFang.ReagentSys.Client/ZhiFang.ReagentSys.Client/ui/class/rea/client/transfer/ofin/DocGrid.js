/**
 * 入库移库--入库主单列表
 * @author longfc
 * @version 2019-03-28
 */
Ext.define('Shell.class.rea.client.transfer.ofin.DocGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '入库列表',
	/**入库移库,获取库存货品库存数大于0的入库主单信息(HQL)*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsInDocOfQtyGEZeroByJoinHql?isPlanish=true',
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsInDoc_DataAddTime',
		direction: 'DESC'
	}],
	/**默认时间为当天*/
	defaultAddDate: null,
	/**状态查询按钮选中值*/
	searchStatusValue: null,
	/**是否有收缩面板按钮*/
	hasCollapse: false,
	/**PDF报表模板*/
	pdfFrx: null,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 5,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",
	/**入库类型Key*/
	ReaBmsInDocInType: 'ReaBmsInDocInType',
	/**入库客户端入库总单状态Key*/
	ReaBmsInDocStatus: 'ReaBmsInDocStatus',
	/**用户UI配置Key*/
	userUIKey: 'transfer.ofin.DocGrid',
	/**用户UI配置Name*/
	userUIName: "入库列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		me.onSearch();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('checkclick');
		JShell.REA.StatusList.getStatusList(me.ReaBmsInDocInType, false, true, null);
		JShell.REA.StatusList.getStatusList(me.ReaBmsInDocStatus, false, true, null);
		//初始化入库时间
		me.initDate();
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
			dataIndex: 'ReaBmsInDoc_DataAddTime',
			text: '入库时间',
			align: 'center',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsInDoc_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}, {
			dataIndex: 'ReaBmsInDoc_Status',
			text: '单据状态',
			width: 60,
			renderer: function(value, meta) {
				var v = value;
				if (JShell.REA.StatusList.Status[me.ReaBmsInDocStatus].Enum != null)
					v = JShell.REA.StatusList.Status[me.ReaBmsInDocStatus].Enum[value];
				var bColor = "";
				if (JShell.REA.StatusList.Status[me.ReaBmsInDocStatus].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.ReaBmsInDocStatus].BGColor[value];
				var fColor = "";
				if (JShell.REA.StatusList.Status[me.ReaBmsInDocStatus].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.ReaBmsInDocStatus].FColor[value];
				var style = 'font-weight:bold;';
				if (bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if (fColor) {
					style = style + "color:" + fColor + ";";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDoc_InDocNo',
			text: '入库总单号',
			width: 130,
			defaultRenderer: true
		}];

		return columns;
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		me.defaultAddDate = Sysdate;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());

		items.push(me.createDataTypeToolbarItems());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createDefaultButtonToolbarItems());
		items.push(me.createPrintButtonToolbarItems());
		return items;
	},
	/**日期范围按钮*/
	createDataTypeToolbarItems: function() {
		var me = this;
		var items = [];

		items.push({
			xtype: 'button',
			text: '当天',
			tooltip: '按当天查',
			handler: function() {
				me.onSetDateArea(0);
			}
		}, {
			xtype: 'button',
			text: '本周',
			tooltip: '按本周',
			handler: function() {
				me.onSetDateArea(0, 'Week');
			}
		}, {
			xtype: 'button',
			text: '本月',
			tooltip: '本月',
			handler: function() {
				me.onSetDateArea(0, 'Month');
			}
		}, {
			xtype: 'button',
			text: '近30天',
			tooltip: '按近30天查',
			handler: function() {
				me.onSetDateArea(-30);
			}
		}, {
			xtype: 'button',
			text: '近60天',
			tooltip: '按近60天查',
			handler: function() {
				me.onSetDateArea(-60);
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar3',
			items: items
		});
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = [];
		//查询框信息
		me.searchInfo = {
			emptyText: '入库总单号/供应商',
			itemId: 'search',
			flex: 1,
			isLike: true,
			fields: ['reabmsindoc.InDocNo', 'reabmsindoc.CompanyName']
		};

		var InTypeList = JShell.REA.StatusList.Status[me.ReaBmsInDocInType].List;
		items.push({
			fieldLabel: '入库类型',
			name: 'InType',
			itemId: 'InType',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			labelWidth: 60,
			labelAlign: 'right',
			data: InTypeList,
			width: 160,
			value: '',
			listeners: {
				change: function() {
					me.onSearch();
				}
			}
		}, {
			fieldLabel: '',
			name: 'CompanyName',
			itemId: 'CompanyName',
			xtype: 'uxCheckTrigger',
			emptyText: '供应商',
			width: 160,
			labelWidth: 0,
			labelAlign: 'right',
			className: 'Shell.class.rea.client.reacenorg.CheckTree',
			classConfig: {
				title: '供应商选择',
				resizable: false,
				/**是否显示根节点*/
				rootVisible: false,
				/**机构类型*/
				OrgType: "0"
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(p, record);
				}
			}
		}, {
			fieldLabel: '供货方主键ID',
			hidden: true,
			xtype: 'uxCheckTrigger',
			name: 'CompanyID',
			itemId: 'CompanyID'
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = ['refresh', '-', ];
		buttonToolbarItems.push({
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '条码打印',
			tooltip: '条码打印',
			hidden: true,
			handler: function() {
				me.onPrintClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnDesign",
			text: '条码模板设计',
			tooltip: '条码模板设计',
			hidden: true,
			handler: function() {
				me.onDesignClick();
			}
		});
		buttonToolbarItems.push('->', {
			iconCls: 'button-right',
			tooltip: '<b>收缩面板</b>',
			handler: function() {
				me.collapse();
			}
		});

		return buttonToolbarItems;
	},
	/**创建功能 按钮栏*/
	createDateAreaToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'uxdatearea',
			itemId: 'date',
			labelWidth: 60,
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		var url = me.callParent(arguments);
		var dtlHql = me.getDtlHql();
		if (dtlHql) url += "&dtlHql=" + dtlHql;
		return url;
	},
	/**获取入库明细条件*/
	getDtlHql: function() {
		var me = this;
		var where = [];
		var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
		var CompanyID = buttonsToolbar2.getComponent('CompanyID');
		if (CompanyID.getValue()) {
			where.push('reabmsindtl.ReaCompanyID=' + CompanyID.getValue());
		}
		return where.join(" and ");
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		var buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			buttonsToolbar3 = me.getComponent('buttonsToolbar3'),
			dateareaToolbar = me.getComponent('dateareaToolbar');
		var search = buttonsToolbar2.getComponent('search');
		var date = dateareaToolbar.getComponent('date');
		var CompanyID = buttonsToolbar2.getComponent('CompanyID');
		var InType = buttonsToolbar2.getComponent('InType');
		var where = [];
		if (me.searchStatusValue != null && parseInt(me.searchStatusValue) > -1)
			where.push("reabmsindoc.Status=" + me.searchStatusValue);
		if (date) {
			var dateValue = date.getValue();
			if (dateValue) {
				if (dateValue.start) {
					where.push('reabmsindoc.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if (dateValue.end) {
					where.push('reabmsindoc.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) +
						"'");
				}
			}
		}
		if (InType.getValue()) {
			where.push('reabmsindoc.InType=' + InType.getValue());
		}
		if (search) {
			var value = search.getValue();
			if (value) {
				var searchHql = me.getSearchWhere(value);
				if (searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		return where.join(" and ");
	},
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get("ReaBmsInDoc_Id");
		var config = {
			title: '入库单操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: "ReaBmsInDocStatus"
		};
		var win = JShell.Win.open('Shell.class.rea.client.reacheckinoperation.Panel', config);
		win.show();
	},
	/**订货方选择*/
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var Id = buttonsToolbar.getComponent('CompanyID');
		var CName = buttonsToolbar.getComponent('CompanyName');
		if (record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			me.onSearch();
			return;
		}
		if (record.data) {
			CName.setValue(record.data ? record.data.text : '');
			Id.setValue(record.data ? record.data.tid : '');
			p.close();
			me.onSearch();
		}
	},
	onSetDateArea: function(day, type) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day, type);
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateAreaValue) date.setValue(dateAreaValue);
		//		me.onSearch();
	},
	initFilterListeners: function(dateAreaValue) {
		var me = this;
		if (!me.defaultAddDate) return;
		var dateAreaValue = {
			start: me.defaultAddDate,
			end: me.defaultAddDate
		}
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateAreaValue) date.setValue(dateAreaValue);

		date.on({
			change: function() {
				me.onSearch();
			}
		});
	},
	/**根据传入天数计算日期范围*/
	calcDateArea: function(day, type) {
		var me = this;
		if (!day) day = 0;
		if (!type) {
			var edate = JcallShell.System.Date.getDate();
			var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		} else {
			var Sysdate = JcallShell.System.Date.getDate();
			var ApplyDate = JcallShell.Date.toString(Sysdate, true);
			var nowDayOfWeek = Sysdate.getDay(); //今天本周的第几天
			var nowDay = Sysdate.getDate(); //当前日     
			var LastMonthValue = Sysdate.getMonth(); //上月 
			var nowYear = Sysdate.getYear(); //当前年   
			nowYear += (nowYear < 2000) ? 1900 : 0; // 
			//获得本周的开始日期
			var getWeekStartDate = new Date(nowYear, LastMonthValue, nowDay - nowDayOfWeek);
			//获得本周的结束日期
			var getWeekEndDate = new Date(nowYear, LastMonthValue, nowDay + (6 - nowDayOfWeek));
			//获得本月的开始日期
			var getMonthStartDate = new Date(nowYear, LastMonthValue, 1);
			//获得本月的结束日期
			var myDate = JcallShell.Date.toString(Sysdate, true);
			var dayCount = me.getCountDays(myDate); //该月天数
			var getMonthEndDate = new Date(nowYear, LastMonthValue, dayCount);

			if (type == 'Week') {
				var edate = getWeekEndDate;
				var sdate = getWeekStartDate;
			}
			if (type == 'Month') {
				var edate = getMonthEndDate;
				var sdate = getMonthStartDate;
			}
		}
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
	},
	/**返回当月的天数*/
	getCountDays: function(date) {
		var curDate = JcallShell.Date.getDate(date);
		/* 获取当前月份 */
		var curMonth = curDate.getMonth();
		curDate.setMonth(curMonth + 1);
		curDate.setDate(0);
		/* 返回当月的天数 */
		return curDate.getDate();
	},
	onPrintClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = records[0].get("ReaBmsInDoc_Status");
		if (status != "2") {
			var statusName = "";
			if (me.StatusEnum != null)
				statusName = me.StatusEnum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能执行条码打印!");
			return;
		}
		//确认入库
		me.onShowPrintPanel(records[0]);
	},
	onDesignClick: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;

		var config = {
			resizable: true,
			SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				close: function(p, eOpts) {

				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.printbarcode.design.App', config);
		win.show();
	},
	onShowPrintPanel: function(record) {
		var me = this;
		var id = record.get("ReaBmsInDoc_Id");
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var id = null;
		if (record) id = record.get(me.PKField);

		var config = {
			resizable: true,
			PK: id,
			//SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				beforeclose: function(p, eOpts) {
					var plugin = p.getPlugin(p.cellpluginId);
					if (plugin) {
						plugin.cancelEdit();
					}
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.printbarcode.indoc.Grid', config);
		win.show();
	},

	/**创建功能按钮栏Items*/
	createPrintButtonToolbarItems: function() {
		var me = this,
			items = [];
		items = me.createReportClassButtonItem(items);
		items = me.createTemplate(items);
		items.push({
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '预览',
			//hidden: true,
			tooltip: '预览PDF清单',
			handler: function() {
				me.onPrintPDFClick();
			}
		});
		items.push('-', {
			text: '导出',
			tooltip: 'EXCEL导出',
			iconCls: 'file-excel',
			xtype: 'button',
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
		if (!items) {
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
	createTemplate: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			emptyText: '模板选择',
			labelWidth: 0,
			width: 145,
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
		});
		return items;
	},
	/**@description 报告模板分类选择后*/
	onReportClassCheck: function(newValue) {
		var me = this;
		me.reaReportClass = newValue;
		me.pdfFrx = "";
		if (me.reaReportClass == "Frx") {
			me.publicTemplateDir = "Frx模板";
		} else if (me.reaReportClass == "Excel") {
			me.publicTemplateDir = "Excel模板";
		}
		var buttonsToolbar = me.getComponent("buttonsToolbarPrint");
		var cbo = buttonsToolbar.getComponent("cboTemplate");
		cbo.setValue("");
		cbo.classConfig["publicTemplateDir"] = me.publicTemplateDir;
		var picker = cbo.getPicker();
		if (picker) {
			picker["publicTemplateDir"] = me.publicTemplateDir;
			cbo.getPicker().load();
		}
	},
	onTemplateCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbarPrint");
		var cbo = buttonsToolbar.getComponent("cboTemplate");
		var cname = "";
		if (record) {
			me.pdfFrx = record.get("FileName");
			cname = record.get("CName");
		}
		if (cbo) {
			cbo.setValue(cname);
		}
		p.close();
	},
	/**@description 选择某一试剂耗材订单,预览PDF清单*/
	onPrintPDFClick: function() {
		var me = this,
			operateType = '1';
		var records = me.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		if (!id) {
			JShell.Msg.error("请先选择出库单后再操作!");
			return;
		}
		if (!me.reaReportClass || me.reaReportClass != "Frx") {
			JShell.Msg.error("请先选择Frx模板后再操作!");
			return;
		}
		if (!me.pdfFrx) {
			JShell.Msg.error("请先选择清单模板后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchBusinessReportOfPdfById");
		var params = [];
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("operateType=" + operateType);
		params.push("id=" + id);
		params.push("breportType=" + me.breportType);
		if (me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	},
	/**选择某一试剂耗材订单,导出EXCEL*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';
		var records = me.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		if (!id) {
			JShell.Msg.error("请先选择入库单后再操作!");
			return;
		}
		if (!me.reaReportClass || me.reaReportClass != "Excel") {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		if (!me.pdfFrx) {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchBusinessReportOfExcelById");
		var params = [];
		params.push("operateType=" + operateType);
		params.push("id=" + id);
		params.push("breportType=" + me.breportType);
		if (me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	}
});
