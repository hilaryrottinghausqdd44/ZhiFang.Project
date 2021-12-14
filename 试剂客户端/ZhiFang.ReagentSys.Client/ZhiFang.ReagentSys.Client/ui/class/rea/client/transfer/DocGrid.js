/**
 * 移库总单
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.transfer.DocGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '移库总单列表',

	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsTransferDocByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsTransferDoc',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDocByField',
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
		property: 'ReaBmsTransferDoc_DataAddTime',
		direction: 'DESC'
	}],
	/**默认时间为当天*/
	defaultAddDate: null,
	/**移库管理类型：1-直接移库 ，2-移库管理(申请)，3-移库管理(全部）*/
	TYPE: '1',
	/**移库总单状态Key*/
	ReaBmsTransferDocStatus: 'ReaBmsTransferDocStatus',
	defaultStatus: '',
	/**状态查询按钮选中值*/
	searchStatusValue: null,
	/**PDF报表模板*/
	pdfFrx: null,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 8,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",
	/**是否要求确认审核人,1是,0否*/
	IsCheck: '0',
	/**是否按权限移库*/
	IsTransferDocIsUse: false,
	/**1：移库申请   2：直接移库管理  3:移库管理(申请)  4:移库管理(全部）*/
	typeByHQL: '4',
	/**移库扫码模式(严格模式:1,混合模式：2)*/
	TransferScanCode: '2',
	/**是否按移库人权限移库*/
	TransferDocIsUse: '2',
	/**用户UI配置Key*/
	userUIKey: 'transfer.DocGrid',
	/**用户UI配置Name*/
	userUIName: "移库列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		me.onSearch();
		me.changeBtnDisable();
	},

	initComponent: function() {
		var me = this;
		//初始化数据
		me.iniDatas();
		me.addEvents('checkclick');
		JShell.REA.StatusList.getStatusList(me.ReaBmsTransferDocStatus, false, true, null);
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
			dataIndex: 'ReaBmsTransferDoc_DataAddTime',
			text: '移库时间',
			align: 'center',
			width: 135,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsTransferDoc_DeptName',
			text: '使用部门',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDoc_Id',
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
			hidden: true,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}, {
			dataIndex: 'ReaBmsTransferDoc_TakerName',
			text: '领用人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDoc_Status',
			text: '单据状态',
			width: 65,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].Enum != null)
					v = JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].FColor[value];
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
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDataTypeToolbarItems());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createDefaultButtonToolbarItems());
		items.push(me.createPrintButtonToolbarItems());
		//		items.push(me.createSearchToolbarItems());	
		//      items.push(me.createStorageToolbarItems());
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
			tooltip: '按近30天查',
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
			emptyText: '移库总单号/使用部门',
			itemId: 'search',
			flex: 1,
			isLike: true,
			fields: ['reabmstransferdoc.TransferDocNo', 'reabmstransferdoc.DeptName']
		};
		var StatusList = JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].List;
		StatusList = me.removeSomeStatusList();
		items.push({
			fieldLabel: '移库状态',
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
	/**审请部门,申请人*/
	createSearchToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
			fieldLabel: '使用部门',
			name: 'ReaBmsTransferDoc_DeptName',
			itemId: 'ReaBmsTransferDoc_DeptName',
			xtype: 'uxCheckTrigger',
			labelWidth: 60,
			labelAlign: 'right',
			className: 'Shell.class.rea.client.CheckOrgTree',
			classConfig: {
				title: '部门选择',
				checkOne: true,
				ISOWN: true,
				rootVisible: false
			},
			listeners: {
				check: function(p, record) {
					if(record && record.get("tid") == 0) {
						JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
						return;
					}
					me.onDeptAccept(record);
					me.onGridSearch();
					p.close();
				}

			},
			width: 180
		}, {
			fieldLabel: '部门id',
			name: 'ReaBmsTransferDoc_DeptID',
			itemId: 'ReaBmsTransferDoc_DeptID',
			hidden: true,
			colspan: 1,
			width: 180,
			xtype: 'textfield'
		}, {
			fieldLabel: '领用人id',
			name: 'ReaBmsTransferDoc_TakerID',
			itemId: 'ReaBmsTransferDoc_TakerID',
			hidden: true,
			width: 180,
			xtype: 'textfield'
		}, {
			fieldLabel: '领用人',
			name: 'ReaBmsTransferDoc_TakerName',
			itemId: 'ReaBmsTransferDoc_TakerName',
			labelWidth: 50,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckGrid',
			flex: 1,
			classConfig: {
				title: '领用人选择',
				checkOne: true,
				width: 330,
				height: 350

			},
			listeners: {
				check: function(p, record) {
					me.onUserAccept(record);
					me.onGridSearch();
					p.close();
				},
				beforetriggerclick: function(p) {
					var buttonsToolbar = me.getComponent('buttonsToolbar4'),
						DeptID = buttonsToolbar.getComponent('ReaBmsTransferDoc_DeptID');
					if(DeptID.getValue()) me.setGoodstemplateClassConfig();
					if(!p.classConfig || !p.classConfig.DeptId) {
						JShell.Msg.warning('获取领用人信息为空,请选择领用部门后再操作!');
						return false;
					}
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar4',
			items: items
		});
	},
	/**部门选择改变或部门选择改变后更新领用人选择的配置项*/
	setGoodstemplateClassConfig: function() {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar4");
		if(buttonsToolbar) {
			var cbo = buttonsToolbar.getComponent('ReaBmsTransferDoc_TakerName');
			var DeptID = buttonsToolbar.getComponent('ReaBmsTransferDoc_DeptID');
			if(cbo) {
				cbo.setValue("");
				cbo.changeClassConfig({
					DeptId: DeptID.getValue()
					//					"defaultWhere": "and hremployee.DeptID=" +
				});
			}
		}
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = [];
		buttonToolbarItems.push('refresh', '-');
		buttonToolbarItems.push({
			text: '新增移库',
			tooltip: '新增移库',
			iconCls: 'button-add',
			itemId: 'Add',
			handler: function() {
				me.onAddClick();
			}
		}, {
			text: '确认移库',
			tooltip: '确认移库',
			iconCls: 'button-accept',
			itemId: 'btnAdd',
			handler: function() {
				me.onCheckAddClick();
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
		var me = this,
			arr = [];
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
		var date = dateareaToolbar.getComponent('date');
		var Status = buttonsToolbar2.getComponent('Status');
		var where = [];
		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					where.push('reabmstransferdoc.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push('reabmstransferdoc.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		if(Status) {
			var StatusV = Status.getValue();
			if(StatusV) {
				where.push('reabmstransferdoc.Status=' + StatusV);
			} else {
				var ids = me.getAllStatusID();
				where.push('reabmstransferdoc.Status in (' + ids + ')');
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
	getAllStatusID: function() {
		var me = this;
		var ids = '';
		for(var i = 0; i < me.searchStatusValue.length; i++) {
			if(me.searchStatusValue[i][0]) {
				ids += "," + me.searchStatusValue[i][0];
			}
		}
		ids = 0 == ids.indexOf(",") ? ids.substr(1) : ids;
		return ids;
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
		var id = record.get("ReaBmsTransferDoc_Id");
		var config = {
			title: '出库单操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: "ReaBmsTransferDocStatus"
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
		//		me.onSearch();
	},
	initFilterListeners: function(dateAreaValue) {
		var me = this;
		if(!me.defaultAddDate) return;
		var dateAreaValue = {
			start: me.defaultAddDate,
			end: me.defaultAddDate
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
		//sdate=Ext.Date.format(sdate,"Y-m-d");
		//edate=Ext.Date.format(edate,"Y-m-d");
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
	},
	onCheckAddClick: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;

		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var config = {
			resizable: false,
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '2',
			PK: records[0].get(me.PKField),
			IsCheck: me.IsCheck,
			IsTransferDocIsUse: me.IsTransferDocIsUse,
			TransferScanCode: me.TransferScanCode,
			listeners: {
				save: function(p, records) {
					if(p) p.close();
					me.onSearch();
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.transfer.accept.AddPanel', config).show();
	},
	/**新增*/
	onAddClick: function() {
		var me = this;
		me.showForm();
	},
	/**显示表单*/
	showForm: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: false,
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '1',
			IsCheck: me.IsCheck,
			IsTransferDocIsUse: me.IsTransferDocIsUse,
			TransferScanCode: me.TransferScanCode,
			listeners: {
				//save: function(p, records) {
					
				//longfc 2019-12-26
				save: function(p) {
					if(p) p.close();
					me.onSearch();
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.transfer.AddPanel', config).show();
	},
	changeBtnDisable: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			Add = buttonsToolbar.getComponent('Add'),
			btnAdd = buttonsToolbar.getComponent('btnAdd');
		Add.setVisible(false);
		btnAdd.setVisible(false);
		switch(me.TYPE) {
			case '1':
				Add.setVisible(true);
				btnAdd.setVisible(false);
				break;
			case '2':
				Add.setVisible(false);
				btnAdd.setVisible(true);
				break;
			case '3':
				Add.setVisible(true);
				btnAdd.setVisible(true);
				break;
			default:
				Add.setVisible(true);
				btnAdd.setVisible(false);
				break;
		}
	},

	onBtnChange: function(rec) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			accept = buttonsToolbar.getComponent('btnAdd');
		accept.setDisabled(true);
		if(!rec) return;
		var Status = rec.get('ReaBmsTransferDoc_Status');
		//已申请
		if(Status == '3' || Status == '4' || Status == '5') {
			accept.setDisabled(false);
		}
	},
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].List));
		var removeArr = [];

		if(me.TYPE == '2' || me.TYPE == '3') {
			//暂存,已申请
			if(tempList[1]) removeArr.push(tempList[1]);
			if(tempList[2]) removeArr.push(tempList[2]);
		} else {
			if(tempList[1]) removeArr.push(tempList[1]);
			if(tempList[2]) removeArr.push(tempList[2]);
			if(tempList[3]) removeArr.push(tempList[3]);
			if(tempList[4]) removeArr.push(tempList[4]);
			if(tempList[5]) removeArr.push(tempList[5]);
		}
		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});

		me.searchStatusValue = tempList;
		return tempList;
	},
	/**获取移库是否要确认审核系统参数信息*/
	getIsCheckVal: function(callback) {
		var me = this;
		var paraVal = 0;
		//移库审核是否需要确认
		JcallShell.REA.RunParams.getRunParamsValue("ReaBmsTransferDocIsCheck", false, function(data) {
			if(data.success) {
				var paraValue = "2";
				var obj = data.value;
				if(obj.ParaValue) {
					paraValue = obj.ParaValue;
					me.IsCheck = paraValue; // parseInt(paraValue);
					if(callback) callback(me.IsCheck);
				}
			}
		});
	},
	/**获取移库权限参数
	 * 1*/
	getIsTransferDocIsUse: function(callback) {
		var me = this;
		//是否按移库人权限移库
		JcallShell.REA.RunParams.getRunParamsValue("ReaBmsTransferDocIsUseEmpOut", false, function(data) {
			if(data.success) {
				var paraValue = "2";
				var obj = data.value;
				if(obj.ParaValue) {
					paraValue = obj.ParaValue;
					me.TransferDocIsUse = paraValue; // parseInt(paraValue);
					if(callback) callback(me.TransferDocIsUse);
				}
			}
		});
	},
	/**获取移库扫码模式参数信息*/
	getTransferScanCodeModel: function(callback) {
		var me = this;
		//移库货品扫码 严格模式:1,混合模式：2"
		JcallShell.REA.RunParams.getRunParamsValue("TransferScanCode", false, function(data) {
			if(data.success) {
				var paraValue = "2";
				var obj = data.value;
				if(obj.ParaValue) {
					paraValue = obj.ParaValue;
					me.TransferScanCode = paraValue; // parseInt(paraValue);
					if(callback) callback(me.TransferScanCode);
				}
			}
		});
	},
	/**综合查询*/
	onGridSearch: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.onSearch();
		}, 100);
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
	//初始化数据
	iniDatas: function() {
		var me = this;
		//是否启用(0:不启用;1:启用)
		me.getIsCheckVal(function(val) {
			val = val + '';
			if(val == '1') {
				me.IsCheck = '1';
			}
		});
		//是否启用(0:不启用;1:启用)
		me.getIsTransferDocIsUse(function(val) {
			val = val + '';
			if(val == '1') {
				me.IsTransferDocIsUse = true;
			}
		});

		me.getTransferScanCodeModel(function(val) {
			me.TransferScanCode = val + '';
		});
		me.changeType();
		var isUseEmpOut = me.IsTransferDocIsUse ? 1 : 2;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.selectUrl += '&empId=' + userId + '&type=' + me.typeByHQL + '&isUseEmpOut=' + isUseEmpOut;
	},
	//根据类型，赋值
	changeType: function() {
		var me = this;
		switch(me.TYPE) {
			case '1': //直接移库
				me.typeByHQL = '2';
				break;
			case '2': //直接移库
				me.typeByHQL = '3';
				break;
			case '3':
				me.typeByHQL = '4';
				break;
			default:
				me.typeByHQL = '2';
				break;
		}
	}
});