/**
 * 出库总单
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.out.basic.DocGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '出库总单列表',
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsOutDocByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocByField',

	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsOutDoc_DataAddTime',
		direction: 'DESC'
	}],

	/**类型1,使用出库*/
	defaluteOutType: '1',
	/**PDF报表模板*/
	pdfFrx: null,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 7,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",
	/**移库总单状态Key*/
	ReaBmsOutDocStatus: 'ReaBmsOutDocStatus',
	/**出库单状态默认选择值*/
	defaultStatus: '',
	/**1：出库申请   2：直接出库管理  3:出库管理(申请)  4:出库管理(全部）*/
	typeByHQL: '2',
	/**库存新增仪器是否允许为空,1是,0否*/
	IsEquip: '1',
	/**是否按出库人权限出库 false否,TRUE是*/
	IsEmpOut: false,
	/**出库领用人是否为当前登录人*/
	OutboundIsLogin: "2",
	/**出库扫码模式(严格模式:1,混合模式：2)*/
	OutScanCodeModel: '2',
	/**状态查询按钮选中值*/
	searchStatusValue: null,
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**出库数据接口标志Key*/
	ReaBmsOutDocIOFlag: 'ReaBmsOutDocIOFlag',
	/**第三方标志Key*/
	ReaBmsOutDocThirdFlag:'ReaBmsOutDocThirdFlag',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsOutDocStatus, false, true, null);
		JShell.REA.StatusList.getStatusList(me.ReaBmsOutDocIOFlag, false, true, null);
		JShell.REA.StatusList.getStatusList(me.ReaBmsOutDocThirdFlag, false, false, null);
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsOutDoc_DataAddTime',
			text: '登记时间',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsOutDoc_OutDocNo',
			text: '出库单号',
			hidden: true,
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDoc_DeptName',
			text: '使用部门',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDoc_IsNeedBOpen',
			text: '开瓶管理',
			width: 75,
			hidden:true,
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsOutDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hideable: false,
			sortable: false,
			//hidden: true,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}, {
			dataIndex: 'ReaBmsOutDoc_Visible',
			text: '作废',
			width: 55,
			renderer: function(value, meta) {
				var v = value + '';
				if(v == 'false') {
					meta.style = 'color:red';
					v = JShell.All.TRUE;
				} else if(v == 'true') {
					meta.style = 'color:green';
					v = JShell.All.FALSE;
				} else {
					v == '';
				}

				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDoc_TakerName',
			text: '领用人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDoc_Status',
			text: '单据状态',
			width: 65,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].Enum != null)
					v = JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].FColor[value];
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
			dataIndex: 'ReaBmsOutDoc_ConfirmTime',
			text: '确认时间',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsOutDoc_CheckTime',
			text: '审核时间',
			width: 135,
			hidden: true,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsOutDoc_ApprovalTime',
			text: '审批时间',
			hidden: true,
			width: 135,
			isDate: true,
			hasTime: true
		},{
			dataIndex: 'ReaBmsOutDoc_IOFlag',
			text: '数据标志',
			width: 135,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.ReaBmsOutDocIOFlag].Enum != null)
					v = JShell.REA.StatusList.Status[me.ReaBmsOutDocIOFlag].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsOutDocIOFlag].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.ReaBmsOutDocIOFlag].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsOutDocIOFlag].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.ReaBmsOutDocIOFlag].FColor[value];
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
		},{
			dataIndex: 'ReaBmsOutDoc_IsThirdFlag',
			text: '第三方标记',
			width: 135,
			hidden:true,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.ReaBmsOutDocThirdFlag].Enum != null)
					v = JShell.REA.StatusList.Status[me.ReaBmsOutDocThirdFlag].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsOutDocThirdFlag].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.ReaBmsOutDocThirdFlag].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsOutDocThirdFlag].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.ReaBmsOutDocThirdFlag].FColor[value];
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
		}];

		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDataTypeToolbarItems());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createDefaultButtonToolbarItems());
		return items;
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
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = [];
		//查询框信息
		me.searchInfo = {
			emptyText: '出库单号/使用部门',
			itemId: 'search',
			flex: 1,
			isLike: true,
			fields: ['reabmsoutdoc.OutDocNo', 'reabmsoutdoc.DeptName']
		};
		var StatusList = me.removeSomeStatusList();
		items.push({
			fieldLabel: '单据状态',
			name: 'Status',
			itemId: 'Status',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			labelWidth: 60,
			labelAlign: 'right',
			data: StatusList,
			width: 160,
			value: me.defaultStatus,
			listeners: {
				change: function() {
					me.onSearch();
				}
			}
		}, '-', {
			type: 'search',
			info: me.searchInfo
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
			buttonToolbarItems = [];
		buttonToolbarItems.push('refresh', '-', 'add');
		return buttonToolbarItems;
	},
	/**创建功能 按钮栏*/
	createDateAreaToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 75,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'dateType',
			value: "reabmsoutdoc.DataAddTime",
			data: [
				["", "请选择"],
				["reabmsoutdoc.DataAddTime", "登记日期"],
				["reabmsoutdoc.ConfirmTime", "确认日期"],
				["reabmsoutdoc.CheckTime", "审核日期"]
			],
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
					var dateareaToolbar = me.getComponent('dateareaToolbar');
					var date = dateareaToolbar.getComponent('date');
					if(!records[0].data.value)
						date.disable();
					else
						date.enable();
				}
			}
		}, {
			xtype: 'uxdatearea',
			itemId: 'date',
			fieldLabel: '',
			labelWidth: 0,
			width: 185,
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

		var buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			buttonsToolbar3 = me.getComponent('buttonsToolbar3'),
			dateareaToolbar = me.getComponent('dateareaToolbar');

		var search = buttonsToolbar2.getComponent('search');
		var dateType = dateareaToolbar.getComponent('dateType');
		var date = dateareaToolbar.getComponent('date');
		var where = [];
		where.push("reabmsoutdoc.OutType=" + me.defaluteOutType);

		//		if(date) {
		//			var dateValue = date.getValue();
		//			if(dateValue) {
		//				if(dateValue.start) {
		//					where.push('reabmsoutdoc.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
		//				}
		//				if(dateValue.end) {
		//					where.push('reabmsoutdoc.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
		//				}
		//			}
		//		}
		if(dateType&&date) {
			var dateValue = date.getValue();
			var dateTypeValue = dateType.getValue();
			if(dateValue && dateTypeValue) {
				if(dateValue.start) {
					where.push(dateTypeValue + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push(dateTypeValue + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		var status = buttonsToolbar2.getComponent('Status');
		if(status) {
			var statusV = status.getValue();
			if(statusV) {
				where.push('reabmsoutdoc.Status=' + statusV);
			} else {
				var statusStr = me.getAllStatusID();
				if(statusStr) where.push('reabmsoutdoc.Status in (' + statusStr + ')');
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
		var id = record.get("ReaBmsOutDoc_Id");
		var config = {
			title: '操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: me.ReaBmsOutDocStatus
		};
		var win = JShell.Win.open('Shell.class.rea.client.scoperation.SCOperation', config);
		win.show();
	},
	/**供货方选择*/
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var Id = buttonsToolbar.getComponent('CompanyID');
		var CName = buttonsToolbar.getComponent('CompanyName');
		if(record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			me.onSearch();
			return;
		}
		if(record.data) {
			CName.setValue(record.data ? record.data.text : '');
			Id.setValue(record.data ? record.data.tid : '');
			p.close();
			me.onSearch();
		}
	},
	onSetDateArea: function(day) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day);
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateAreaValue) date.setValue(dateAreaValue);
		me.onSearch();
	},
	initFilterListeners: function(dateAreaValue) {
		var me = this;
		var sysdate = JcallShell.System.Date.getDate();
		var dateAreaValue = {
			start: sysdate,
			end: sysdate
		}
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateAreaValue) date.setValue(dateAreaValue);
		date.on({
			change: function() {
				me.onSearch();
			}
		});
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
	/**新增*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	initRunParams: function() {
		var me = this;
		//是否按出库人权限出库
		me.getOutIsEmpVal(function(val) {
			val = val + '';
			if(val == '1') { //按出库人权限出库
				me.IsEmpOut = true;
			}
		});
		// 出库扫码模式
		me.getOutScanCodeModel(function(val) {
			me.OutScanCodeModel = val + '';
		});
	},
	/**获取出库人领用人为登录人系统参数信息*/
	getOutTakerParaVal: function(callback) {
		var me = this;
		var paraVal = 0;
		//出库领用人是否为登录人
		JcallShell.REA.RunParams.getRunParamsValue("OutboundIsLogin", false, function(data) {
			if(data.success) {
				var paraValue = "2";
				var obj = data.value;
				if(obj.ParaValue) {
					paraValue = obj.ParaValue;
					me.OutboundIsLogin = paraValue; // parseInt(paraValue);
					if(callback) callback(me.OutboundIsLogin);
				}
			}
		});
	},
	/**获取参数信息,是否按出库人权限出库*/
	getOutIsEmpVal: function(callback) {
		var me = this;
		//是否按出库人权限出库
		JcallShell.REA.RunParams.getRunParamsValue("ReaBmsOutDocUseIsEmpOut", false, function(data) {
			if(data.success) {
				var paraValue = "2";
				var obj = data.value;
				if(obj.ParaValue) {
					if(obj.ParaValue == '1') {
						me.IsEmpOut = true;
					}
					if(callback) callback(obj.ParaValue);
				}
			}
		});
	},
	/**获取出库扫码模式参数信息*/
	getOutScanCodeModel: function(callback) {
		var me = this;
		//出库货品扫码 严格模式:1,混合模式：2"
		JcallShell.REA.RunParams.getRunParamsValue("OutScanCode", false, function(data) {
			if(data.success) {
				var paraValue = "2";
				var obj = data.value;
				if(obj.ParaValue) {
					paraValue = obj.ParaValue;
					me.OutScanCodeModel = paraValue; // parseInt(paraValue);
					if(callback) callback(me.OutScanCodeModel);
				}
			}
		});
	},
	/**获取使用出库仪器是否必填系统参数信息*/
	getIsEquipParaVal: function(callback) {
		var me = this;
		//使用出库仪器是否必填
		JcallShell.REA.RunParams.getRunParamsValue("ReaBmsOutDocUseIsEquip", false, function(data) {
			if(data.success) {
				var paraValue = "0";
				var obj = data.value;
				if(obj.ParaValue) {
					paraValue = obj.ParaValue;
					me.IsEquip = paraValue; // parseInt(paraValue);
					if(callback) callback(me.IsEquip);
				}
			} else {
				JShell.Msg.error('获取系统参数(使用出库仪器是否必填)出错！' + data.msg);
			}
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
			JShell.Msg.error("请先选择出库单后再操作!");
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
	},
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List));
		var removeArr = [];
		me.searchStatusValue = tempList;
		return tempList;
	},
	getAllStatusID: function() {
		var me = this;
		var ids = [];
		for(var i = 0; i < me.searchStatusValue.length; i++) {
			if(me.searchStatusValue[i][0]) {
				ids.push(me.searchStatusValue[i][0]);
			}
		}
		if(ids.length > 0) ids = ids.join(",");
		else ids = "";
		return ids;
	},
	//根据类型，赋值
	changeType: function() {
		var me = this;
		me.typeByHQL = '2';
	}
});