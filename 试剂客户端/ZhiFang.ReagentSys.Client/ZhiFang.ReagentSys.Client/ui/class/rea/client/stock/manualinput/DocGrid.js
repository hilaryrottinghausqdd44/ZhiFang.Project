/**
 * 客户端库存初始化(手工入库)
 * @author longfc
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.stock.manualinput.DocGrid', {
	extend: 'Shell.class.rea.client.SearchGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '手工入库',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocByHQL?isPlanish=true',
	/**确认入库服务路径*/
	confirmUrl: "/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocOfConfirmStock",

	/**默认加载*/
	defaultLoad: false,
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
		property: 'ReaBmsInDoc_DataAddTime',
		direction: 'DESC'
	}],

	/**状态查询按钮选中值*/
	searchStatusValue: null,
	/**客户端入库总单状态*/
	StatusKey: "ReaBmsInDocStatus",
	/**客户端入库类型*/
	InTypeKey: "ReaBmsInDocInType",

	/**扫码模式(严格模式:strict,混合模式：mixing)*/
	CodeScanningMode: "mixing",
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
	/**用户UI配置Key*/
	userUIKey: 'stock.manualinput.DocGrid',
	/**用户UI配置Name*/
	userUIName: "库存初始化列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(0);
	},
	initComponent: function() {
		var me = this;
		//默认只操作入库类型为库存初始化(手工入库)的数据
		me.defaultWhere = "reabmsindoc.InType=2";
		//查询框信息
		me.searchInfo = {
			emptyText: '入库总单号/送货人',
			itemId: 'Search',
			//flex: 1,
			width: "72%",
			isLike: true,
			fields: ['reabmsindoc.InDocNo', 'reabmsindoc.Carrier']
		};
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.InTypeKey, false, true, null);
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
			dataIndex: 'ReaBmsInDoc_DeptName',
			text: '所属部门',
			hidden: true,
			width: 85,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDoc_CreaterName',
			text: '登记人',
			hidden: true,
			width: 85,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDoc_DataAddTime',
			text: '入库日期',
			align: 'center',
			width: 95,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'ReaBmsInDoc_Status',
			text: '单据状态',
			width: 90,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.StatusKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.StatusKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.StatusKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.StatusKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.StatusKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.StatusKey].FColor[value];
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
			dataIndex: 'ReaBmsInDoc_StatusName',
			text: '单据状态',
			hidden: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDoc_InDocNo',
			text: '入库总单号',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDoc_Id',
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
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createStatusSearchButtonToolbar());
		items.push(me.createQuickSearchButtonToolbar());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createButtonsToolbarSearch());
		items.push(me.createPrintButtonToolbarItems());
		return items;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '新增',
			tooltip: '新增入库',
			handler: function() {
				me.onAddClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnEdit',
			iconCls: 'button-edit',
			text: "继续入库",
			tooltip: "对待继续入库继续验货入库",
			handler: function() {
				me.onInContinueClick();
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '确认入库',
			tooltip: '确认入库',
			handler: function() {
				me.onConfirmClick();
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '条码打印',
			tooltip: '条码打印',
			handler: function() {
				me.onPrintClick();
			}
		});
		items.push('->', {
			iconCls: 'button-right',
			tooltip: '<b>收缩面板</b>',
			handler: function() {
				me.collapse();
			}
		});
		return items;
	},
	/**查询输入栏*/
	createButtonsToolbarSearch: function() {
		var me = this;
		var items = ['refresh', '-', ];

		items.push({
			type: 'search',
			info: me.searchInfo
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbarSearch',
			items: items
		});
	},
	/**创建状态按钮查询栏*/
	createStatusSearchButtonToolbar: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			text: '全部',
			tooltip: '全部',
			itemId: "AllStatus",
			handler: function() {
				me.onStatusSearch(null);
			}
		}, {
			xtype: 'button',
			text: '待继续入库',
			tooltip: '查询状态为待继续入库',
			itemId: "Apply",
			enableToggle: false,
			handler: function() {
				me.onStatusSearch(1);
			}
		}, {
			xtype: 'button',
			text: '已入库 ',
			tooltip: '查询状态为入库的入库单',
			itemId: "Accept",
			enableToggle: false,
			handler: function() {
				me.onStatusSearch(2);
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'statusSearchButtonToolbar',
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		var buttonsToolbarSearch = me.getComponent('buttonsToolbarSearch'),
			dateareaToolbar = me.getComponent('dateareaToolbar');
		var search = buttonsToolbarSearch.getComponent('Search');

		var date = dateareaToolbar.getComponent('date');
		var where = [];
		if(me.searchStatusValue != null && parseInt(me.searchStatusValue) > -1)
			where.push("reabmsindoc.Status=" + me.searchStatusValue);

		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					where.push('reabmsindoc.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push('reabmsindoc.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
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
	onShowOperation: function(record) {
		var me = this;
		if(!record) {
			var records = me.getSelectionModel().getSelection();
			if(records.length != 1) {
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
	/**根据传入天数计算日期范围*/
	calcDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
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
	/**按验收状态快捷查询*/
	onStatusSearch: function(status) {
		var me = this;
		me.setStatusSearchToggle(status);
		me.searchStatusValue = status;
		me.onSearch();
	},
	/**按验收状态按钮状态点击后样式设置*/
	setStatusSearchToggle: function(status) {
		var me = this;
		var buttonsToolbar = me.getComponent('statusSearchButtonToolbar');
		var allStatus = buttonsToolbar.getComponent('AllStatus');
		var apply = buttonsToolbar.getComponent('Apply');
		var accept = buttonsToolbar.getComponent('Accept');

		switch(status) {
			case 1:
				allStatus.toggle(false);
				apply.toggle(true);
				accept.toggle(false);
				break;
			case 2:
				allStatus.toggle(false);
				apply.toggle(false);
				accept.toggle(true);
				break;
			default:
				allStatus.toggle(true);
				apply.toggle(false);
				accept.toggle(false);
				break;
		}
	},
	/**确认入库*/
	onConfirmClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = records[0].get("ReaBmsInDoc_Status");
		if(status != "1") {
			var StatusEnum = JShell.REA.StatusList.Status[me.StatusKey].Enum;
			var statusName = "";
			if(StatusEnum)
				statusName = StatusEnum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能执行确认入库!");
			return;
		}
		//确认入库
		me.onConfirmStock(records[0]);
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	/**@description 继续入库按钮*/
	onInContinueClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var status = records[0].get("ReaBmsInDoc_Status");
		if(status != "1") {
			var StatusEnum = JShell.REA.StatusList.Status[me.StatusKey].Enum;
			var statusName = "";
			if(StatusEnum)
				statusName = StatusEnum[status];
			JShell.Msg.alert("当前状态为【" + statusName + "】,不能执行继续入库操作!", null, 2000);
			return;
		}
		me.fireEvent('onInContinueClick', me, records[0]);
	},
	/**@description 确认入库*/
	onConfirmStock: function(record) {
		var me = this;

		var id = record.get("ReaBmsInDoc_Id");
		var url = JShell.System.Path.ROOT + me.confirmUrl + "?id=" + id + "&codeScanningMode=" + me.CodeScanningMode;

		JShell.Server.get(url, function(data) {
			if(data.success) {
				JShell.Msg.alert('确认入库成功', null, 1000);
				me.onSearch();
				me.onPrintClick();
			} else {
				JShell.Msg.error('确认入库失败！' + data.msg);
			}
		});
	},
	onPrintClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = records[0].get("ReaBmsInDoc_Status");
		if(status != "2") {
			var StatusEnum = JShell.REA.StatusList.Status[me.StatusKey].Enum;
			var statusName = "";
			if(StatusEnum)
				statusName = StatusEnum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能执行条码打印!");
			return;
		}
		//确认入库
		me.onShowPrintPanel(records[0]);
	},
	onShowPrintPanel: function(record) {
		var me = this;
		var id = record.get("ReaBmsInDoc_Id");
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var id = null;
		if(record) id = record.get(me.PKField);

		var config = {
			resizable: true,
			PK: id,
			//SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				beforeclose: function(p, eOpts) {
					var plugin = p.getPlugin(p.cellpluginId);
					if(plugin) {
						plugin.cancelEdit();
					}
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.printbarcode.indoc.Grid', config);
		win.show();
	},
	/**根据id*/
	onShowPrintPanelById: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		if(!id) return;
		var config = {
			resizable: true,
			PK: id,
			//SUB_WIN_NO: '1',
			width: maxWidth,
			height: height,
			listeners: {
				beforeclose: function(p, eOpts) {
					var plugin = p.getPlugin(p.cellpluginId);
					if(plugin) {
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
	createTemplate: function(items) {
		var me = this;
		if(!items) {
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
	onPrintPDFClick: function() {
		var me = this,
			operateType = '1';
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		if(!id) {
			JShell.Msg.error("请先选择出库单后再操作!");
			return;
		}
		if(!me.reaReportClass || me.reaReportClass != "Frx") {
			JShell.Msg.error("请先选择Frx模板后再操作!");
			return;
		}
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择清单模板后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchBusinessReportOfPdfById");
		var params = [];
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("operateType=" + operateType);
		params.push("id=" + id);
		params.push("breportType=" + me.breportType);
		if(me.pdfFrx) {
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
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		if(!id) {
			JShell.Msg.error("请先选择出库单后再操作!");
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