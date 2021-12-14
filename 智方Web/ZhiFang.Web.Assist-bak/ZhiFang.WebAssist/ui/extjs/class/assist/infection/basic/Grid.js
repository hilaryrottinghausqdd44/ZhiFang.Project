/**
 * 环境监测送检样本登记
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.infection.basic.Grid', {
	extend: 'Shell.class.assist.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],

	title: '环境监测送检样本登记列表 ',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormAndDtlByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateGKSampleRequestFormByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_DelGKSampleRequestForm ',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 30,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'GKSampleRequestForm_DataAddTime',
		direction: 'DESC'
	}],
	/**客户端电脑上的打印机集合信息*/
	PrinterList: [],
	/**默认选择的打印机*/
	DefaultPrinter: "",
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**当前查询日期范围*/
	dateArea: null,
	/**样本状态状态Key*/
	StatusKey: "GKSampleFormStatus",
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 3,
	/**当前选择的监测类型值*/
	CurRecordTypeValue: "",
	/**是否显示科室查询项*/
	IsHaveDept: false,
	/**监测类型集合信息*/
	RecordTypeItemList: [],
	//是否需要更新样品列信息
	hasSetColumnsText:false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.on({
			itemdblclick: function(view, record) {
				me.onEditClick();
			}
		});
		me.initDateArea();
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 135,
			emptyText: '监测类型/条码号',
			isLike: true,
			fields: ['gksamplerequestform.SCRecordType.CName', 'gksamplerequestform.BarCode']
		};

		JcallShell.BLTF.StatusList.getStatusList(me.StatusKey, false, true, null);

		me.initDefaultInfo();
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		//me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var me = this;
		var columns = [{
			text: '申请单Id',
			dataIndex: 'GKSampleRequestForm_Id',
			isKey: true,
			hidden: true
		}, {
			text: '监测级别',
			dataIndex: 'GKSampleRequestForm_MonitorType',
			width: 80,
			hidden: true,
			renderer: function(value, meta) {
				var v = value;
				var style = 'font-weight:bold;color:';
				if (value == "1") {
					style = style + "#ffffff;background-color:#2F4056;";
					v = "感控监测";
				} else {
					style = style + "#ffffff;background-color:#009688;";
					v = "科室监测";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			text: '登记日期',
			dataIndex: 'GKSampleRequestForm_DataAddTime',
			width: 140,
			isDate: true,
			hasTime: true
		}, {
			text: '申请单号',
			dataIndex: 'GKSampleRequestForm_ReqDocNo',
			width: 110,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '条码号',
			dataIndex: 'GKSampleRequestForm_BarCode',
			width: 110,
			defaultRenderer: true
		}, {
			text: '样本状态',
			dataIndex: 'GKSampleRequestForm_StatusID',
			width: 90,
			renderer: function(value, meta) {
				var v = value;
				if (JcallShell.BLTF.StatusList.Status[me.StatusKey].Enum != null)
					v = JcallShell.BLTF.StatusList.Status[me.StatusKey].Enum[value];
				var bColor = "";
				if (JcallShell.BLTF.StatusList.Status[me.StatusKey].BGColor != null)
					bColor = JcallShell.BLTF.StatusList.Status[me.StatusKey].BGColor[value];
				var fColor = "";
				if (JcallShell.BLTF.StatusList.Status[me.StatusKey].FColor != null)
					fColor = JcallShell.BLTF.StatusList.Status[me.StatusKey].FColor[value];
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
			text: '样本号',
			dataIndex: 'GKSampleRequestForm_SampleNo',
			width: 80,
			defaultRenderer: true
		}, {
			text: '是否自动核收',
			dataIndex: 'GKSampleRequestForm_IsAutoReceive',
			width: 80,
			hidden: true,
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: 'LIS核收',
			style: 'font-weight:bold;color:white;background:orange;',
			align: 'center',
			width: 65,
			//hidden:true,
			hideable: false,
			sortable: false,
			items: [{
				iconCls: 'button-show hand',
				getClass: function(v, meta, record) {
					var statusID = "" + record.get("GKSampleRequestForm_StatusID");
					var isAutoReceive = "" + record.get("GKSampleRequestForm_IsAutoReceive");
					if (isAutoReceive == "1" || isAutoReceive == "true") isAutoReceive = true;
					if (isAutoReceive == true && statusID == "1") {
						return 'button-check hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onAutoReceive(rec);
				}
			}]
		}, {
			text: '科室',
			dataIndex: 'GKSampleRequestForm_DeptCName',
			width: 110,
			defaultRenderer: true
		}, {
			text: '监测类型编码',
			dataIndex: 'GKSampleRequestForm_SCRecordType_Id',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '监测类型',
			dataIndex: 'GKSampleRequestForm_SCRecordType_CName',
			width: 110,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		}, {
			text: '样品信息1',
			dataIndex: 'GKSampleRequestForm_ItemResult1',
			width: 90,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		}, {
			text: '样品信息2',
			dataIndex: 'GKSampleRequestForm_ItemResult2',
			width: 110,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		}, {
			text: '样品信息3',
			dataIndex: 'GKSampleRequestForm_ItemResult3',
			width: 110,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		}, {
			text: '样品信息4',
			dataIndex: 'GKSampleRequestForm_ItemResult4',
			width: 100,
			renderer: function(value, meta, record) {
				var value2 = record.get("GKSampleRequestForm_SCRecordType_Id");
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = me.getCellStyle(value2);
				return value;
			}
		},  {
			text: '检验结果',
			dataIndex: 'GKSampleRequestForm_TestResult',
			width: 100,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 55,
			hideable: false,
			sortable: false,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}, {
			text: '样品信息',
			dataIndex: 'GKSampleRequestForm_DtlJArray',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '采样者',
			dataIndex: 'GKSampleRequestForm_Sampler',
			width: 90,
			defaultRenderer: true
		}, {
			text: '采样日期',
			dataIndex: 'GKSampleRequestForm_SampleDate',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			text: '采样时间',
			dataIndex: 'GKSampleRequestForm_SampleTime',
			width: 65,
			renderer: function(value, meta, record) {
				if (value) {
					value = Ext.util.Format.date(value, 'H:i');
					meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				}
				return value;
			}
		}, {
			text: '检验日期',
			dataIndex: 'GKSampleRequestForm_TestTime',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			text: '评估判定',
			dataIndex: 'GKSampleRequestForm_Judgment',
			width: 100,
			renderer: function(value, meta) {
				var v = value;
				var style = 'font-weight:bold;color:';
				if (value == "1" || value == "true" || value == "合格") {
					style = style + "#ffffff;background-color:#009688;";
					v = "合格";
				} else if (value == "2" || value == "false" || value == "不合格") {
					style = style + "#ffffff;background-color:#FF5722;";
					v = "不合格";
				} else {
					style = style + "#ffffff;background-color:#FFB800;";
					v = "未评估";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			text: '评估者',
			dataIndex: 'GKSampleRequestForm_Evaluators',
			width: 100,
			defaultRenderer: true
		}, {
			text: '评估日期',
			dataIndex: 'GKSampleRequestForm_EvaluationDate',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			text: '检验者',
			dataIndex: 'GKSampleRequestForm_TesterName',
			width: 100,
			defaultRenderer: true
		}, {
			text: '显示次序',
			dataIndex: 'GKSampleRequestForm_DispOrder',
			width: 60,
			hidden: true,
			defaultRenderer: true,
			type: 'int'
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'GKSampleRequestForm_Visible',
			width: 40,
			align: 'center',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			dataIndex: 'GKSampleRequestForm_ReceiveFlag',
			text: '核收标志',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			dataIndex: 'GKSampleRequestForm_ResultFlag',
			text: '结果回写',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			dataIndex: 'GKSampleRequestForm_EvaluatorFlag',
			text: '评估标志',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}, {
			dataIndex: 'GKSampleRequestForm_Archived',
			text: '归档标志',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}];

		return columns;
	},
	/**
	 * @description 监测类型及样品列信息样式处理
	 * @param {Object} value2
	 */
	getCellStyle: function(value2) {
		//var style = 'font-weight:bold;';color:#1c8f36;
		var style = ''; //font-weight:bold;
		if (value2 == "11") {
			style = style + "background-color:#C0FFC0;";
		} else if (value2 == "12") {
			style = style + "background-color:#FFE0C0;";
		} else if (value2 == "13") {
			style = style + "background-color:#FFC0FF;";
		} else if (value2 == "14") {
			style = style + "background-color:#C0FFFF;";
		} else if (value2 == "15") {
			style = style + "background-color:#C0C0FF;";
		} else if (value2 == "16") {
			style = style + "background-color:#FFFFC0;";
		} else if (value2 == "17") {
			style = style + "background-color:#00C0C0;";
		} else if (value2 == "18") {
			style = style + "background-color:#C0C000";
		} else {
			style = style + "background-color:#C0C000";
		}
		//style = style + "color:#ffffff;";
		return style;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		//var tempStatus = JcallShell.BLTF.StatusList.Status[me.StatusKey].List;
		tempStatus = me.removeSomeStatusList();

		if (me.hasRefresh) items.push('refresh');

		items.push({
			xtype: 'uxSimpleComboBox',
			itemId: 'chooseRecordType',
			labelWidth: 0,
			width: 120,
			hasStyle: true,
			value: "",
			data: [
				['', '所有监测类型', 'background-color:#FFC0C0;font-weight:bold;'],
				['11', '手卫生', 'background-color:#C0FFC0;'],
				['12', '空气培养', 'background-color:#FFE0C0;'],
				['13', '物体表面', 'background-color:#FFC0FF;'],
				['14', '消毒剂', 'background-color:#C0FFFF;'],
				['15', '透析液及透析用水', 'background-color:#C0C0FF;'],
				['16', '医疗器材', 'background-color:#FFFFC0;'],
				['17', '污水', 'background-color:#00C0C0;'],
				['18', '其它', 'background-color:#C0C000;']
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.CurRecordTypeValue = newValue;
					me.onSearch();
				}
			}
		});

		items.push({
			xtype: 'combobox',
			itemId: 'gkSampleFormStatus',
			emptyText: "样本状态选择",
			labelWidth: 0,
			width: 90,
			multiSelect: true,
			queryMode: 'local',
			displayField: 'Name',
			valueField: 'Id',
			store: me.getStatusStore(),
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					JShell.Action.delay(function() {
						me.onSearch();
					}, null, 800);
				}
			}
		});
		items.push({
			xtype: 'combobox',
			itemId: 'cboMonitorType',
			labelWidth: 0,
			width: 85,
			emptyText: "监测级别选择",
			multiSelect: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			store: Ext.create('Ext.data.Store', {
				fields: ['value', 'text'],
				data: [{
						"value": "1",
						"text": "感控监测"
					},
					{
						"value": "2",
						"text": "科室监测"
					}
				]
			}),
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					JShell.Action.delay(function() {
						me.onSearch();
					}, null, 800);
				}
			}
		});
		//感控评估查询项
		if (me.IsHaveDept == true) {
			items = me.createDeptItem(items);
		}
		items.push({
			xtype: 'combobox',
			itemId: 'cboJudgment',
			labelWidth: 0,
			width: 75,
			emptyText: "评估判定",
			multiSelect: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			store: Ext.create('Ext.data.Store', {
				fields: ['value', 'text'],
				data: [{
						"value": "1",
						"text": "合格"
					},
					{
						"value": "2",
						"text": "不合格"
					}
				]
			}),
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					JShell.Action.delay(function() {
						me.onSearch();
					}, null, 800);
				}
			}
		});
		items.push('-', {
			fieldLabel: '',
			labelWidth: 0,
			width: 80,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'dateType',
			value: "gksamplerequestform.DataAddTime",
			data: [
				["", "请选择"],
				["gksamplerequestform.DataAddTime", "登记日期"],
				["gksamplerequestform.SampleDate", "采样日期"],
				["gksamplerequestform.TestTime", "检验日期"]
			],
			listeners: {
				select: function(com, records, eOpts) {
					JShell.Action.delay(function() {
						me.onSearch();
					}, null, 800);
				}
			}
		}, {
			xtype: 'uxdatearea',
			itemId: 'date',
			width: 205,
			labelWidth: 0,
			labelAlign: 'right',
			fieldLabel: '',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		});

		items.push({
			boxLabel: '包含归档',
			name: 'cboArchived',
			itemId: 'cboArchived',
			xtype: 'checkboxfield',
			inputValue: true,
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					JShell.Action.delay(function() {
						me.onSearch();
					}, null, 800);
				}
			}
		});
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});

		items.push({
			xtype: 'splitbutton',
			text: '预览/导出',
			menu: new Ext.menu.Menu({
				items: [{
					text: '导出登记清单',
					tooltip: '导出登记清单',
					iconCls: 'file-excel',
					handler: function() {
						me.onExpExcel(1);
					}
				}, {
					text: '导出送检清单',
					tooltip: '导出送检清单',
					//hidden: true,
					iconCls: 'file-excel',
					handler: function() {
						me.onExpExcel(2);
					}
				}, {
					iconCls: 'file-excel',
					text: '导出报告',
					tooltip: '导出报告',
					handler: function() {
						me.onExpExcel(3);
					}
				}]
			})
		});

		items.push({
			xtype: 'button',
			iconCls: 'file-excel',
			text: '打印报告',
			tooltip: '打印报告',
			handler: function() {
				//环境卫生学监测报告按监测类型分组
				me.onPrintPDF(30);
			}
		});

		return items;
	},
	/**
	 * @param {Object} items
	 */
	createDeptItem: function(items) {
		var me = this;
		if (!items) items = [];

		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 120,
			name: 'DeptCName',
			itemId: 'DeptCName',
			emptyText: '科室选择',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.department.CheckGrid',
			classConfig: {
				title: '科室选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					var data="";
					if(record)data=record.data;
					me.onDepCheck(p, data);
				}
			}
		}, {
			fieldLabel: '科室Id',
			hidden: true,
			xtype: "textfield",
			name: 'DeptId',
			itemId: 'DeptId'
		});

		return items;
	},
	/**
	 * @description 弹出人员选择器选择确认后处理
	 * @param {Object} p
	 * @param {Object} data
	 */
	onDepCheck: function(p, data) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var DeptId = buttonsToolbar.getComponent('DeptId');
		var DeptCName = buttonsToolbar.getComponent('DeptCName');
		DeptCName.setValue(data ? data["Department_CName"] : '');
		DeptId.setValue(data ? data["Department_Id"] : '');
		if (p) p.close();

		me.onSearch();
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JcallShell.BLTF.StatusList.Status[me.StatusKey].List));
		var itemArr = [];
		//所有
		if (tempList[0]) itemArr.push(tempList[0]);

		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	},
	getStatusStore: function() {
		var me = this;
		var tempData = JShell.JSON.decode(JShell.JSON.encode(JcallShell.BLTF.StatusList.Status[me.StatusKey].Data));
		if (!tempData) tempData = [];
		var store = Ext.create('Ext.data.Store', {
			fields: ['Id', 'Name'],
			data: tempData
		});
		return store;
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this;
	},
	/**导出按钮点击处理方法*/
	onExpExcel: function(groupType) {
		var me = this;

		var url = JShell.System.Path.getRootUrl(
			"/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormOfExcelByHql");
		var params = [];
		var where = me.getWhere();
		var sort = "";
		if (me.curOrderBy && me.curOrderBy.length > 0) JShell.JSON.encode(me.curOrderBy); //me.store.sorters;

		var operateType = '0';
		params.push("operateType=" + operateType);
		params.push("breportType=" + me.breportType);
		params.push("groupType=" + groupType);
		params.push("where=" + where);
		if (sort) params.push("sort=" + sort);
		var docVO = me.getDocVO();
		if (docVO) {
			params.push("docVO=" + JShell.JSON.encode(docVO));
		}
		/* if (me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		} */

		url += "?" + params.join("&");
		window.open(url);
	},
	/**初始化默认信息*/
	initDefaultInfo: function() {
		var me = this;
		
	},
	/**PDF*/
	onPrintPDF: function(groupType) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(
			"/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormOfPdfByHQL");
		var params = [];
		var where = me.getWhere();
		var sort = "";
		if (me.curOrderBy && me.curOrderBy.length > 0) JShell.JSON.encode(me.curOrderBy); //me.store.sorters;
		
		var operateType = '1';
		params.push("operateType=" + operateType);
		params.push("breportType=" + me.breportType);
		params.push("groupType=" + groupType);
		params.push("where=" + where);
		if (sort) params.push("sort=" + sort);
		var docVO = me.getDocVO();
		if (docVO) {
			params.push("docVO=" + JShell.JSON.encode(docVO));
		}
		/* if (me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		} */
		
		url += "?" + params.join("&");
		window.open(url);
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;

		var edate = JcallShell.System.Date.getDate();
		if (!day) day = -Ext.Date.getDayOfYear(edate);
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			date = buttonsToolbar.getComponent('date');
		if (date && dateArea) date.setValue(dateArea);
		me.onSearch();
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			me.updateOne(i, rec);
		}
	},
	updateOne: function(index, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var id = rec.get(me.PKField);
		var Visible = rec.get('GKSampleRequestForm_Visible');
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				Visible: Visible
			},
			fields: 'Id,Visible'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('addclick', me);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		me.fireEvent('editclick', me, records[0]);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	getWhere: function() {
		var me = this;
		var arr = [];
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		where = JShell.String.encode(where);
		return where;
	},
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var otherWhere = buttonsToolbar.getComponent('otherWhere');

		var chooseRecordType = buttonsToolbar.getComponent('chooseRecordType');
		var gkSampleFormStatus = buttonsToolbar.getComponent('gkSampleFormStatus');
		var deptId = buttonsToolbar.getComponent('DeptId');
		var search = buttonsToolbar.getComponent('search');
		var dateType = buttonsToolbar.getComponent('dateType');
		var date = buttonsToolbar.getComponent('date');
		var cboMonitorType = buttonsToolbar.getComponent('cboMonitorType'); //监测级别
		var cboJudgment = buttonsToolbar.getComponent('cboJudgment'); //评估判定
		var cboArchived = buttonsToolbar.getComponent('cboArchived'); //归档标志

		var params = [];

		//感控评估:按选择科室过滤
		if (deptId && deptId.getValue()) {
			params.push("gksamplerequestform.DeptId=" + deptId.getValue());
		}
		//监测类型
		if (chooseRecordType && chooseRecordType.getValue()) {
			params.push("gksamplerequestform.SCRecordType.Id=" + chooseRecordType.getValue());
		}
		//样本状态
		if (gkSampleFormStatus && gkSampleFormStatus.getValue()) {
			var value3 = gkSampleFormStatus.getValue();
			if (value3 && value3.length > 0) params.push("(gksamplerequestform.StatusID in (" + value3 + "))");
		}
		//监测级别:1:感控监测;0:科室监测;
		if (cboMonitorType && cboMonitorType.getValue()) {
			var value3 = cboMonitorType.getValue();
			if (value3.length > 0 && value3.length != 2) { //不是全选
				params.push("gksamplerequestform.MonitorType=" + value3);
			}
		}
		//评估判定:合格或不合格
		if (cboJudgment && cboJudgment.getValue()) {
			var value4 = cboJudgment.getValue();
			if (value4.length > 0 && value4.length != 2) { //不是全选
				params.push("gksamplerequestform.Judgment='" + value4 + "'");
			}
		}
		//包含已归档,如果勾选,表示包含已归档的也显示
		if (cboArchived) {
			var check1 = cboArchived.getValue();
			if (check1 == false) { //不包含已归档
				params.push("gksamplerequestform.Archived=0");
			}
		}
		//日期范围
		if (dateType && date) {
			var dateTypeValue = dateType.getValue();
			var dateValue = date.getValue();
			if (dateValue && dateTypeValue) {
				if (dateValue.start) {
					params.push(dateTypeValue + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if (dateValue.end) {
					params.push(dateTypeValue + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		if (params.length > 0) params = params.join(" and ");

		return params;
	},
	/**获取封装的VO查询条件*/
	getDocVO: function() {
		var me = this;

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var chooseRecordType = buttonsToolbar.getComponent('chooseRecordType');
		var deptId = buttonsToolbar.getComponent('DeptId');
		var cboYear = buttonsToolbar.getComponent('cboYear');
		var dateType = buttonsToolbar.getComponent('dateType');
		var date = buttonsToolbar.getComponent('date');

		var cboHospitalSense = buttonsToolbar.getComponent('cboHospitalSense'); //感控监测
		var cboDept = buttonsToolbar.getComponent('cboDept'); //科室监测

		var monitorType = "",
			recordTypeNo = "",
			deptIdStr = "",
			year = "",
			startDate = "",
			endDate = "";
		//感控评估:按选择科室过滤
		if (deptId && deptId.getValue()) {
			deptIdStr = deptId.getValue();
		}
		//监测类型
		if (chooseRecordType && chooseRecordType.getValue()) {
			recordTypeNo = chooseRecordType.getValue();
		}
		//监测类型
		if (cboYear && cboYear.getValue()) {
			year = cboYear.getValue();
		}
		//感控监测类型:1:感控监测;0:科室监测;
		if (cboHospitalSense && cboDept) {
			var check1 = cboHospitalSense.getValue(); //感控监测
			var check2 = cboDept.getValue(); //科室监测
			if (check1 == true && check2 == false) { //感控监测
				monitorType = "1";
			} else if (check1 == false && check2 == true) { //科室监测
				monitorType = "0";
			}
		}
		//日期范围
		if (dateType && date) {
			var dateTypeValue = dateType.getValue();
			var dateValue = date.getValue();
			if (dateValue && dateTypeValue) {
				if (dateValue.start) {
					startDate = JShell.Date.toString(dateValue.start, true);
				}
				if (dateValue.end) {
					endDate = JShell.Date.toString(dateValue.end, true);
					year = endDate.split('-')[0];
				}
			}
		}

		var vo = {
			"MonitorType": monitorType,
			"RecordTypeNo": recordTypeNo,
			"deptId": deptIdStr,
			"Year": year,
			"StartDate": startDate,
			"EndDate": endDate,
			"Quarterly": ""
		};
		return vo;
	},
	/**
	 * 操作记录查看
	 * @param {Object} record
	 */
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
		var id = record.get(me.PKField);
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.WebAssist', //类域
			className: 'GKSampleFormStatus', //类名
			title: '操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='GKSampleRequestForm'"
		};
		var win = JShell.Win.open('Shell.class.assist.scoperation.SCOperation', config);
		win.show();
	},
	/**
	 * @description 批量确认处理前验证
	 */
	verifyBatchSubmit: function() {
		var me = this;

		var records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return false;
		}

		var msg = "";
		if (JcallShell.BLTF.StatusList.Status[me.StatusKey]) {
			for (var i in records) {
				var id = records[i].get(me.PKField);
				var status = "" + records[i].get("GKSampleRequestForm_StatusID");
				if (status != "0") {
					var statusName = JcallShell.BLTF.StatusList.Status[me.StatusKey].Enum[status];
					msg = msg + "条码号为:" + records[i].get("GKSampleRequestForm_BarCode") + ",样本状态为【" + statusName +
						"】,不能进行批量确认!<br/>";
					break;
				}
			}
		}
		if (msg.length > 0) {
			JShell.Msg.error(msg);
			return false;
		}

		return true;
	},
	/**
	 * @description 批量确认处理
	 */
	onBatchSubmit: function(callback) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return callback();
		}

		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;

		for (var i = 0; i < records.length; i++) {
			var rec = records[i];
			me.updateStatus(i, rec, 1, callback);
		}
	},
	/**
	 * @description 批量确认处理前验证
	 */
	verifyBatchObsolete: function() {
		var me = this;

		var records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return false;
		}

		var msg = "";
		if (JcallShell.BLTF.StatusList.Status[me.StatusKey]) {
			for (var i in records) {
				var id = records[i].get(me.PKField);
				var status = "" + records[i].get("GKSampleRequestForm_StatusID");
				if (status != "0" && status != "1") {
					var statusName = JcallShell.BLTF.StatusList.Status[me.StatusKey].Enum[status];
					msg = msg + "条码号为:" + records[i].get("GKSampleRequestForm_BarCode") + ",样本状态为【" + statusName + "】,不能作废!<br/>";
					break;
				}
			}
		}
		if (msg.length > 0) {
			JShell.Msg.error(msg);
			return false;
		}

		return true;
	},
	/**
	 * @description 作废处理
	 */
	onBatchObsolete: function(callback) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return callback();
		}

		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
		for (var i = 0; i < records.length; i++) {
			var rec = records[i];
			me.updateStatus(i, rec, 7, callback);
		}
	},
	/**
	 * @param {Object} index
	 * @param {Object} rec
	 */
	updateStatus: function(index, rec, statusID, callback) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";

		var id = rec.get(me.PKField);
		var params = Ext.JSON.encode({
			"entity": {
				Id: id,
				StatusID: statusID
			},
			"fields": 'Id,StatusID',
			"empID": empID,
			"empName": empName
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					if (!callback) me.hideMask(); //隐藏遮罩层
					if (callback) callback();
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**
	 * LIS核收:自动核收失败后的操作
	 * @param {Object} rec
	 */
	onAutoReceive: function(rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";

		var id = rec.get(me.PKField);
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				StatusID: "2" //已核收
			},
			fields: 'Id,StatusID',
			"empID": empID,
			"empName": empName
		});

		JShell.Server.post(url, params, function(data) {
			if (data.success) {
				me.saveCount++;
			} else {
				me.saveErrorCount++;
			}
			if (me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if (me.saveErrorCount == 0) me.onSearch();
			}
		});
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		
		me.setcolumns1(records);
	},
	setcolumns1:function(records){
		var me = this;
		if (records && records.length > 0 && me.CurRecordTypeValue != "") {
			var dtlJArray = records[0].get("GKSampleRequestForm_DtlJArray");
			var recordTypeId = records[0].get("GKSampleRequestForm_SCRecordType_Id");
			if (dtlJArray) dtlJArray = JShell.JSON.decode(dtlJArray);
			
			if (dtlJArray && dtlJArray.length > 0) {
				for (var i = 0; i < dtlJArray.length; i++) {
					var item = dtlJArray[i];
					if(!item)continue;
					
					for (var j = 0; j < me.columns.length; j++) {
						if (item["dataIndex"] == me.columns[j]["dataIndex"]) {
							me.hasSetColumnsText=true;
							me.columns[j].setText (item["CName"]);
							
							//记录项是否开单可见
							var isBillVisible=""+item["IsBillVisible"];
							//console.log(item["CName"]+isBillVisible);
							
							if(isBillVisible=="1"||isBillVisible=="true"){
								isBillVisible=true;
							}else{
								isBillVisible=false;
							}
							me.columns[j].setVisible(isBillVisible);
							break;
						}
					}
				}
			} else {
				me.setColumns2();
			}
		} else {
			me.setColumns2();
		}
	},
	setColumns2: function() {
		var me = this;
		var isBreak=false;
		for (var j = 0; j < me.columns.length; j++) {
			if(isBreak==true){
				me.hasSetColumnsText=false;
				break;
			}
			var dataIndex = me.columns[j]["dataIndex"];
			switch (dataIndex) {
				case "GKSampleRequestForm_ItemResult1":
					me.columns[j].setText("样品信息1");
					me.columns[j].setVisible(true);
					break;
				case "GKSampleRequestForm_ItemResult2":
					me.columns[j].setText("样品信息2");
					me.columns[j].setVisible(true);
					break;
				case "GKSampleRequestForm_ItemResult3":
					me.columns[j].setText("样品信息3");
					me.columns[j].setVisible(true);
					break;
				case "GKSampleRequestForm_ItemResult4":
					me.columns[j].setText( "样品信息4");
					me.columns[j].setVisible(true);
					isBreak=true;
					break;	
				default:
					break;
			}
		}
	}

});
