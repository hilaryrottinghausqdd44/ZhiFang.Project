/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.basic.add.DocGrid', {
	extend: 'Shell.class.rea.client.reasale.basic.DocGrid',

	title: '供货信息',
	/**是否为实验室应用*/
	isLab:false,
	defaultStatusValue:'',
	defaultIOFlagValue:'',
	
	/**PDF报表模板*/
	pdfFrx: null,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 3,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Frx",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Frx模板",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onConfirmClick', 'onUnConfirmClick', 'onCheckClick', 'onUnCheckClick', 'onCreateBarCodeClick');
		//查询框信息
		me.searchInfo = {
			emptyText: '供货单号/送货人',
			itemId: 'Search',
			//flex: 1,
			width: "72%",
			isLike: true,
			fields: ['reabmscensaledoc.SaleDocNo', 'reabmscensaledoc.Carrier']
		};
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		//me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createSynchOutToolbar());
		if(me.isLab==false)
		items.push(me.createOperateButtonToolbar());	
		items.push(me.createQuickSearchButtonToolbar());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createButtonToolbarItems2());
		items.push(me.createButtonsToolbarSearch());
		//items.push(me.createPrintButtonToolbarItems());
		return items;
	},
	/**创建同步出库单功能 按钮栏*/
	createSynchOutToolbar: function() {
		var me = this;
		var items = [];
		// 新增同步出库单按钮，注意这里的按钮都是会显示的，区别与实验室的按钮
		items.push({
			xtype: 'button',
			iconCls: 'button-config',
			text: '同步出库单',
			tooltip: '同步出库单',
			handler: function() {
				me.onSynchOutClick();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'SynchOutToolbar',
			items: items
		});
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
			tooltip: '新增供货单',
			handler: function() {
				me.onAddClick();
			}
		});
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnConfirm",
			text: '确认提交',
			tooltip: '确认提交',
			handler: function() {
				me.onConfirmClick();
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnUnConfirm",
			text: '取消提交',
			tooltip: '取消提交',
			handler: function() {
				me.onUnConfirmClick();
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
	/**创建操作按钮栏*/
	createOperateButtonToolbar: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '审核通过',
			tooltip: '审核通过',
			handler: function() {
				me.onCheckClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-reset',
			itemId: "btnUnCheck",
			text: '取消审核',
			tooltip: '取消审核',
			handler: function() {
				me.onUnCheckClick();
			}
		});
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnCreatePrint",
			text: '生成条码',
			tooltip: '对审核通过的供货单重新生成条码',
			handler: function() {
				me.onCreatePrintClick();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'operateButtonToolbar',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems2: function() {
		var me = this;
		var items = [];
		var tempStatus = JShell.REA.StatusList.Status[me.StatusKey].List;
		tempStatus = me.removeSomeStatusList();

		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 95,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocStatus',
			data: tempStatus,
			value: me.defaultStatusValue,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 75,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocIOFlag',
			value: me.defaultIOFlagValue,
			data: JShell.REA.StatusList.Status[me.IOFlagKey].List,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 65,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocUrgentFlag',
			data: [
				["", "请选择"],
				["0", "正常"],
				["1", "紧急"]
			],
			value: '',
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		}, {
			fieldLabel: '',
			labelWidth: 0,
			width: 65,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DeleteFlag',
			disabled: me.disabledDeleteFlag,
			data: [
				["", "请选择"],
				["0", "启用"],
				["1", "禁用"]
			],
			value: "0",
			listeners: {
				select: function(com, records, eOpts) {
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
			//hidden: true,
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
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	/**确认提交*/
	onConfirmClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = records[0].get("ReaBmsCenSaleDoc_Status");
		//暂存,取消提交,取消审核
		if(status != "1" && status != "3" && status != "5") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.StatusKey].Enum != null)
				statusName = JShell.REA.StatusList.Status[me.StatusKey].Enum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能执行确认提交!");
			return;
		}
		me.fireEvent('onConfirmClick', me, records[0]);
	},
	/**取消提交*/
	onUnConfirmClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = "" + records[0].get("ReaBmsCenSaleDoc_Status");
		//确认提交
		if(status != "2") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.StatusKey].Enum != null)
				statusName = JShell.REA.StatusList.Status[me.StatusKey].Enum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能执行审核通过!");
			return;
		}
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">取消提交操作确认</div>',
			msg: '请确认是否继续进行取消提交?',
			closable: false,
			multiline: false
		}, function(but, text) {
			if(but != "ok") return;

			me.fireEvent('onUnConfirmClick', me, records[0]);
		});
	},
	/**审核通过*/
	onCheckClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = "" + records[0].get("ReaBmsCenSaleDoc_Status");
		//确认提交,取消审核
		if(status != "2" && status != "5") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.StatusKey].Enum != null)
				statusName = JShell.REA.StatusList.Status[me.StatusKey].Enum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能执行审核通过!");
			return;
		}
		me.fireEvent('onCheckClick', me, records[0]);
	},
	/**取消审核*/
	onUnCheckClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = "" + records[0].get("ReaBmsCenSaleDoc_Status");
		//审核通过
		if(status != "4") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.StatusKey].Enum != null)
				statusName = JShell.REA.StatusList.Status[me.StatusKey].Enum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能执行取消审核!");
			return;
		}

		JShell.Msg.confirm({
			title: '<div style="text-align:center;">取消审核操作确认</div>',
			msg: '请确认是否继续进行取消审核?',
			closable: false,
			multiline: false
		}, function(but, text) {
			if(but != "ok") return;

			me.fireEvent('onUnCheckClick', me, records[0]);
		});
	},
	/**生成条码*/
	onCreatePrintClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var status = "" + records[0].get("ReaBmsCenSaleDoc_Status");
		//审核通过
		if(status != "4") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.StatusKey].Enum != null)
				statusName = JShell.REA.StatusList.Status[me.StatusKey].Enum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能执行生成条码!");
			return;
		}
		me.fireEvent('onCreateBarCodeClick', me, records[0].get(me.PKField));
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
	/**@description 选择某一供货单,预览PDF清单*/
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
			JShell.Msg.error("请先选择供货单后再操作!");
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
	/**选择供货单,导出EXCEL*/
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
			JShell.Msg.error("请先选择供货单后再操作!");
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
	},
	/**新增同步出库单功能,点击同步出库单弹出一个窗体**/
	onSynchOutClick: function() {
		var me = this;
		var config = {};
		JShell.Win.open('Shell.class.rea.client.reasale.basic.add.SynchForm', config).show();
	}
	
});