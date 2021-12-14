/**
 * 输血申请综合查询:输血申请主单列表
 * @author longfc
 * @version 2020-02-27
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.req.DocGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '输血申请信息',
	//width: 480,

	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: false,
	hasPagingtoolbar: true,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormListByBBagCodeAndHql?isPlanish=true',
	/**默认获取审批完成的用血申请单*/
	defaultWhere: "bloodbreqform.CheckCompleteFlag=1",
	/**默认每页数量*/
	defaultPageSize: 100,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodBReqForm_ReqTime',
		direction: 'DESC'
	}, {
		property: 'BloodBReqForm_Id',
		direction: 'ASC'
	}],
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 13,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",
	/**his调用时获取到的病区Id*/
	wardId:"",	
	/**用户UI配置Key*/
	userUIKey: 'statistics.reqntegrated.req.DocGrid',
	/**用户UI配置Name*/
	userUIName: "输血申请信息",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initDateArea(-7);
		me.onSearch();
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.callParent(arguments);
		items.push(me.createDataTypeToolbarItems());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createButtonToolbarItems2());
		items.push(me.createPrintButtonToolbarItems());
		return items;
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
			width: 135,
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
			className: 'Shell.class.blood.template.CheckGrid',
			listeners: {
				check: function(p, record) {
					me.onTemplateCheck(p, record);
				}
			}
		});
		return items;
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
				me.onPrintClick();
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
			hidden: false,
			itemId: 'buttonsToolbarPrint',
			items: items
		});
	},
	/**日期范围按钮*/
	createDataTypeToolbarItems: function() {
		var me = this;
		var items = [];
	
		items.push({
			xtype: 'button',
			text: '今天',
			tooltip: '按当天查',
			handler: function() {
				me.onSetDateArea(0);
			}
		}, {
			xtype: 'button',
			text: '10天内',
			tooltip: '按近10天查',
			handler: function() {
				me.onSetDateArea(-10);
			}
		}, {
			xtype: 'button',
			text: '20天内',
			tooltip: '按近20天查',
			handler: function() {
				me.onSetDateArea(-20);
			}
		}, {
			xtype: 'button',
			text: '30天内',
			tooltip: '按近30天查	',
			handler: function() {
				me.onSetDateArea(-30);
			}
		}, {
			xtype: 'button',
			text: '60天内',
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
	/**创建功能 按钮栏*/
	createDateAreaToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'uxdatearea',
			itemId: 'date',
			width: 275,
			labelWidth: 65,
			labelAlign: 'right',
			fieldLabel: '申请日期',
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
	/**创建功能 按钮栏*/
	createButtonToolbarItems2: function() {
		var me = this;
		var items = [];
		//查询框信息
		me.searchInfo = {
			width: 145,
			itemId: "search",
			emptyText: '住院号/登记号/姓名',
			isLike: true,
			fields: ['bloodbreqform.Id', 'bloodbreqform.PatNo', 'bloodbreqform.AdmID', 'bloodbreqform.CName']
		};
		if (!items) items = [];
		items.push({
			type: 'search',
			info: me.searchInfo
		});
		items.push({
			xtype: 'textfield',
			width: 130,
			fieldWidth: 0,
			fieldLabel: "",
			emptyText: "血袋号/惟一码",
			name: "BBagCode",
			itemId: "BBagCode"
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			listeners: {
				click: function(but) {
					me.onSearch();
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '申请时间',
			dataIndex: 'BloodBReqForm_ApplyTime',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '申请单号',
			dataIndex: 'BloodBReqForm_Id',
			width: 100,
			isKey: true,
			sortable: true,
			defaultRenderer: true
		}, {
			text: '姓名',
			dataIndex: 'BloodBReqForm_CName',
			width: 80,
			sortable: true,
			defaultRenderer: true
		}, {
			text: '住院号',
			dataIndex: 'BloodBReqForm_PatNo',
			width: 80,
			defaultRenderer: true
		}, {
			text: '登记号',
			dataIndex: 'BloodBReqForm_AdmID',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '性别',
			dataIndex: 'BloodBReqForm_Sex',
			width: 60,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '出生日期',
			dataIndex: 'BloodBReqForm_Birthday',
			width: 90,
			isDate: true,
			hasTime: false,
			defaultRenderer: true
		}, {
			text: '年龄',
			dataIndex: 'BloodBReqForm_AgeALL',
			width: 60,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '输血史',
			dataIndex: 'BloodBReqForm_BeforUse',
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '科室',
			dataIndex: 'BloodBReqForm_DeptCName',
			width: 60,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '科室编码',
			dataIndex: 'BloodBReqForm_DeptNo',
			hidden: true,
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '床号',
			dataIndex: 'BloodBReqForm_Bed',
			width: 60,
			defaultRenderer: true
		}, {
			text: '医生',
			dataIndex: 'BloodBReqForm_ApplyName',
			width: 60,
			defaultRenderer: true
		}];
		return columns;
	},
	onSetDateArea: function(day) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day);
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateAreaValue) date.setValue(dateAreaValue);
		me.onSearch();
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
		if (!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateArea) date.setValue(dateArea);

	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = me.getInternalWhere();
		//内部条件
		if (params && params.length > 0) me.internalWhere = params.join(" and ");
		var url = me.callParent(arguments);
		var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
		var bbagCode = buttonsToolbar2.getComponent('BBagCode').getValue();
		if (bbagCode) url = url + "&bbagCode=" + bbagCode;
		//病区Id
		if (me.wardId) url = url + "&wardId=" + me.wardId;
		//var deptNo = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		return url;
	},
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var dateareaToolbar = me.getComponent('dateareaToolbar');
		var date = dateareaToolbar.getComponent('date');
		var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
		var search = buttonsToolbar2.getComponent('search');

		var params = [];
		if (date) {
			var dateValue = date.getValue();
			var dateTypeValue = "bloodbreqform.ApplyTime"; //申请时间
			if (dateValue && dateTypeValue) {
				if (dateValue.start) {
					params.push(dateTypeValue + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if (dateValue.end) {
					params.push(dateTypeValue + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		if (search) {
			var value = search.getValue();
			if (value) {
				params.push("(" + me.getSearchWhere(value) + ")");
			}
		}
		return params;
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
			JShell.Msg.error("请先选择申请单后再操作!");
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
			JShell.Msg.error("请先选择申请单后再操作!");
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
