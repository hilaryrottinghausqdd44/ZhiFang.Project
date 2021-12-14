/**
 * 输血过程记录:发血记录主单列表
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.out.DocGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '发血记录信息',
	//width: 480,

	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	hasPagingtoolbar: false,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutFormOfBloodTransByHQL?isPlanish=true',
	/**只能获取到可配置的系统参数*/
	defaultWhere: "",
	/**默认每页数量*/
	defaultPageSize: 100,
	autoSelect: true,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 10,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",

	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodBOutForm_OperTime',
		direction: 'DESC'
	}, {
		property: 'BloodBOutForm_Id',
		direction: 'ASC'
	}],
	/**His调用传入的就诊号*/
	AdmId: "",
	/**用户UI配置Key*/
	userUIKey: 'transrecord.out.DocGrid',
	/**用户UI配置Name*/
	userUIName: "发血记录信息",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initDateArea(-7);
		me.onSearch();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onAddTrans');
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];

		if (me.hasRefresh) items.push('refresh', '-');
		//查询框信息
		me.searchInfo = {
			width: 145,
			itemId: "search",
			emptyText: '发血单号/病历号/姓名',
			isLike: true,
			fields: ['bloodboutform.Id', 'bloodboutform.BloodBReqForm.PatNo', 'bloodboutform.BloodBReqForm.CName']
		};
		items = me.createSearchButtonsToolbar(items);

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createSearchButtonsToolbar: function(items) {
		var me = this;

		if (!items) items = [];

		items.push({
			xtype: 'uxdatearea',
			itemId: 'date',
			width: 185,
			labelWidth: 0,
			labelAlign: 'right',
			fieldLabel: '',
			listeners: {
				enter: function() {
					me.onSearch();
				},
				change: function(com, newValue, oldValue, eOpts) {
					//me.onSearch();
				}
			}
		});
		items.push({
			fieldLabel: '',
			name: 'HandoverCompletion',
			itemId: 'HandoverCompletion',
			xtype: 'uxSimpleComboBox',
			width: 105,
			labelWidth: 0,
			value: '',
			emptyText: '',
			hasStyle: true,
			data: [
				['', '请选择', 'color:black;'],
				['1', '未交接', 'color:#FFB800;'],
				['2', '部分交接', 'color:#00BFFF;'],
				['3', '交接完成', 'color:#009688;']
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			fieldLabel: '',
			name: 'CourseCompletion',
			itemId: 'CourseCompletion',
			xtype: 'uxSimpleComboBox',
			width: 85,
			labelWidth: 0,
			value: '',
			emptyText: '',
			hasStyle: true,
			data: [
				['', '请选择', 'color:black;'],
				['1', '未登记', 'color:#FFB800;'],
				['2', '部分登记', 'color:#00BFFF;'],
				['3', '登记完成', 'color:#009688;']
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push('-', {
			type: 'search',
			info: me.searchInfo
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
		}, {
			xtype: 'button',
			iconCls: 'button-add',
			text: '登记',
			hidden: true,
			tooltip: '对选中的第血袋进行登记',
			listeners: {
				click: function(but) {
					me.onAddTrans();
				}
			}
		});
		items.push("->");
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
			className: 'Shell.class.blood.template.CheckGrid',
			listeners: {
				check: function(p, record) {
					me.onTemplateCheck(p, record);
				}
			}
		});
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '发血单号',
			dataIndex: 'BloodBOutForm_Id',
			width: 120,
			isKey: true,
			sortable: true,
			defaultRenderer: true
		}, {
			text: '姓名',
			dataIndex: 'BloodBOutForm_BloodBReqForm_CName',
			width: 70,
			sortable: true,
			defaultRenderer: true
		}, {
			text: '交接完成度',
			dataIndex: 'BloodBOutForm_HandoverCompletion',
			width: 75,
			renderer: function(value, meta) {
				var v = "";
				if (value == "2") {
					v = "部分交接";
					meta.style = "background-color:#00BFFF;color:#ffffff;";
				} else if (value == "3") {
					v = "交接完成";
					meta.style = "background-color:#009688;color:#ffffff;";
				} else {
					v = "未交接";
					meta.style = "background-color:#FFB800;color:#ffffff;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '输血登记完成度',
			dataIndex: 'BloodBOutForm_CourseCompletion',
			width: 95,
			renderer: function(value, meta) {
				var v = "";
				if (value == "2") {
					v = "部分登记";
					meta.style = "background-color:#00BFFF;color:#ffffff;";
				} else if (value == "3") {
					v = "登记完成";
					meta.style = "background-color:#009688;color:#ffffff;";
				} else {
					v = "未登记";
					meta.style = "background-color:#FFB800;color:#ffffff;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '病历号',
			dataIndex: 'BloodBOutForm_BloodBReqForm_PatNo',
			width: 70,
			defaultRenderer: true
		}, {
			text: '就诊号',
			dataIndex: 'BloodBOutForm_BloodBReqForm_AdmID',
			width: 70,
			//hidden: true,
			defaultRenderer: true
		}, {
			text: '性别',
			dataIndex: 'BloodBOutForm_BloodBReqForm_Sex',
			width: 55,
			defaultRenderer: true
		}, {
			text: '出生日期',
			dataIndex: 'BloodBOutForm_BloodBReqForm_Birthday',
			width: 90,
			isDate: true,
			hasTime: false,
			defaultRenderer: true
		}, {
			text: '年龄',
			dataIndex: 'BloodBOutForm_BloodBReqForm_AgeALL',
			width: 50,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '床号',
			dataIndex: 'BloodBOutForm_BloodBReqForm_Bed',
			width: 50,
			defaultRenderer: true
		}, {
			text: '输血史',
			dataIndex: 'BloodBOutForm_BloodBReqForm_BeforUse',
			width: 60,
			defaultRenderer: true
		}, {
			text: '发血时间',
			dataIndex: 'BloodBOutForm_OperTime',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '终止输血',
			align: 'center',
			width: 55,
			hideable: false,
			sortable: false,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				getClass: function(v, meta, record) {
					meta.tdAttr = 'data-qtip="<b>手工将输血登记完成度标记为登记完成</b>"';
					var courseCompletion = record.get("BloodBOutForm_CourseCompletion");
					var endBloodReason = record.get("BloodBOutForm_EndBloodReason");
					var endBloodReasonCName = record.get("BloodBOutForm_BDEndBReason_CName");
					if (!courseCompletion) courseCompletion = "";
					if (!endBloodReason) endBloodReason = "";
					if (courseCompletion == "2" && (endBloodReasonCName == "" || endBloodReason == "")) {
						return 'button-edit hand';
					} else if (courseCompletion == "3"){
						return 'button-search hand';
					} else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var courseCompletion = rec.get("BloodBOutForm_CourseCompletion");
					var endBloodReason = rec.get("BloodBOutForm_EndBloodReason");
					var endBloodReasonCName = rec.get("BloodBOutForm_BDEndBReason_CName");
					if (!courseCompletion) courseCompletion = "";
					if (!endBloodReason) endBloodReason = "";
					var formtype = "show";
					if (courseCompletion == "2" && (endBloodReasonCName == "" || endBloodReason == "")) {
						formtype = "edit";
					}else if (courseCompletion == "3"){
						formtype = "show";
					} else{
						return;
					}
					me.onOpenForm(rec, formtype);
				}
			}]
		}, {
			text: '终止输血操作人',
			dataIndex: 'BloodBOutForm_EndBloodOperName',
			width: 95,
			defaultRenderer: true
		}, {
			text: '终止输血时间',
			dataIndex: 'BloodBOutForm_EndBloodOperTime',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '终止输血原因',
			dataIndex: 'BloodBOutForm_BDEndBReason_CName',
			width: 90,
			defaultRenderer: true
		}, {
			text: '终止输血备注',
			dataIndex: 'BloodBOutForm_EndBloodReason',
			width: 90,
			renderer: function(value, meta) {
				var v = "";
				if (value.length > 10) {
					v = value.substr(1, 10) + "...";
				}
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			text: '诊断',
			dataIndex: 'BloodBOutForm_BloodBReqForm_Diag',
			width: 80,
			flex: 1,
			defaultRenderer: true
		}];
		return columns;
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
		var dateareaToolbar = me.getComponent('buttonsToolbar'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateArea) date.setValue(dateArea);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = me.getInternalWhere();
		//内部条件
		me.internalWhere = params.join(" and ");
		return me.callParent(arguments);
	},
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		var handoverCompletion = buttonsToolbar.getComponent('HandoverCompletion');
		var courseCompletion = buttonsToolbar.getComponent('CourseCompletion'),
			date = buttonsToolbar.getComponent('date');
		var params = [];
		//如果HIS调用传入了就诊号
		if (me.AdmId) {
			params.push("bloodboutform.BloodBReqForm.AdmID='" + me.AdmId + "'");
		} else {
			//按科室过滤
			var deptNo = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
			if (deptNo) {
				params.push("bloodboutform.BloodBReqForm.DeptNo=" + deptNo);
			}
		}
		if (handoverCompletion) {
			var value = handoverCompletion.getValue();
			if (value) {
				if (value == "1") { //未接收
					params.push(
						"(bloodboutform.HandoverCompletion=0 or bloodboutform.HandoverCompletion=1 or bloodboutform.HandoverCompletion is null)"
					);
				} else {
					params.push("bloodboutform.HandoverCompletion=" + value);
				}
			}
		}
		if (courseCompletion) {
			var value = courseCompletion.getValue();
			if (value) {
				if (value == "1") { //未输血登记
					params.push(
						"(bloodboutform.CourseCompletion=0 or bloodboutform.CourseCompletion=1 or bloodboutform.CourseCompletion is null)"
					);
				} else {
					params.push("bloodboutform.CourseCompletion=" + value);
				}
			}
		}
		if (date) {
			var dateValue = date.getValue();
			var dateTypeValue = "bloodboutform.OperTime"; //发血时间
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
	onAddTrans: function() {
		var me = this;
		me.fireEvent('onAddTrans', me);
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
	onPrintClick: function() {
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
			JShell.Msg.error("请先选择Frx模板后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/BloodTransfusionManageService.svc/RS_UDTO_SearchBusinessReportOfPdfById");
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
			JShell.Msg.error("请先选择出库单后再操作!");
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
		var url = JShell.System.Path.getRootUrl(
			"/BloodTransfusionManageService.svc/RS_UDTO_SearchBusinessReportOfExcelById");
		var params = [];
		params.push("operateType=" + operateType);
		params.push("id=" + id);
		params.push("breportType=" + me.breportType);
		if (me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	},
	onOpenForm: function(rec, formtype) {
		var me = this;
		var outId = rec.get("BloodBOutForm_Id");
		var maxWidth = 380; // document.body.clientWidth - 10; //* 0.98;
		var height1 = 300; // document.body.clientHeight * 0.96;
		var win1 = JShell.Win.open("Shell.class.blood.nursestation.transrecord.out.Form", {
			resizable: true,
			width: maxWidth,
			height: height1,
			formtype: formtype,
			PK: outId,
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
					//me.fireEvent('save', me);
				}
			}
		}).show();
	},
	/**
	 * 按发血单ID手工标记发血主单及明细的输血登记完成度
	 * @param {Object} rec
	 */
	onUpdateCourseCompletion: function(rec, endBloodReasonStr) {
		var me = this;
		var courseCompletion = rec.get("BloodBOutForm_CourseCompletion");
		if (courseCompletion != "2") {
			JShell.Msg.error("当前选择的发血单的不能手工标识输血登记完成度！");
			return;
		}
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var updateValue = "3";
		var outId = rec.get("BloodBOutForm_Id");
		var url = JShell.System.Path.ROOT +
			"/BloodTransfusionManageService.svc/BT_UDTO_UpdateBOutCourseCompletionByEndBloodOper";
		var updateEntity = {
			"Id": outId,
			"EndBloodOperName": empName,
			"EndBloodReason": endBloodReasonStr
		};
		if (empID) {
			updateEntity.EndBloodOperId = empID;
		}
		var params = {
			"entity": updateEntity,
			"updateValue": updateValue,
			"empID": empID,
			"empName": empName
		};
		params = JShell.JSON.encode(params);
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				me.onSearch();
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});
