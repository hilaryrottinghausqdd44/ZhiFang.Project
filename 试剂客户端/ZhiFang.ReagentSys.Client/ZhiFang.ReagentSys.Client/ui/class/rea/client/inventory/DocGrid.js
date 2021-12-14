/**
 * 盘库管理
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.DocGrid', {
	extend: 'Shell.class.rea.client.SearchGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '盘库管理',
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCheckDocByHQL?isPlanish=true',
	/**取消盘库服务路径*/
	delUrl: "/ReaSysManageService.svc/ST_UDTO_DelReaBmsCheckDoc",

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
		property: 'ReaBmsCheckDoc_DataAddTime',
		direction: 'DESC'
	}],
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 9,
	/**状态查询按钮选中值*/
	searchStatusValue: null,
	/**客户端盘库单状态*/
	StatusKey: "ReaBmsCheckDocStatus",
	/**客户端盘库单盘库结果*/
	CheckResultKey: "ReaBmsCheckResult",
	/**用户UI配置Key*/
	userUIKey: 'inventory.DocGrid',
	/**用户UI配置Name*/
	userUIName: "盘库管理列表",
	/**PDF报表模板*/
	pdfFrx: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化
		me.initDateArea(-30);
		me.onSearch();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onPrint');
		//查询框信息
		me.searchInfo = {
			emptyText: '盘库单号/库房/货架/一级分类/二级分类',
			itemId: 'Search',
			//flex: 1,
			width: "82%",
			isLike: true,
			fields: ['reabmscheckdoc.CheckDocNo', 'reabmscheckdoc.StorageName', 'reabmscheckdoc.PlaceName', 'reabmscheckdoc.GoodsClass', 'reabmscheckdoc.GoodsClassType']
		};
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.CheckResultKey, false, true, null);
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
			dataIndex: 'ReaBmsCheckDoc_DataAddTime',
			text: '盘点日期',
			align: 'center',
			width: 95,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'ReaBmsCheckDoc_Status',
			text: '单据状态',
			width: 85,
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
			dataIndex: 'ReaBmsCheckDoc_CompanyName',
			text: '供货方',
			width: 120,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDoc_StorageName',
			text: '所属库房',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDoc_PlaceName',
			text: '所属货架',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDoc_GoodsClass',
			text: '一级分类',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDoc_GoodsClassType',
			text: '二级分类',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDoc_CheckDocNo',
			text: '盘库单号',
			width: 130,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDoc_CheckerName',
			text: '盘点人',
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDoc_ExaminerDateTime',
			text: '审核时间',
			width: 105,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCheckDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCheckDoc_BmsCheckResult',
			text: '盘库结果',
			width: 105,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.CheckResultKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.CheckResultKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.CheckResultKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.CheckResultKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.CheckResultKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.CheckResultKey].FColor[value];
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

		items.push(me.createDateAreaToolbarItems());
		items.push(me.createButtonToolbarItems3());
		items.push(me.createButtonsToolbarSearch());
		items.push(me.createButtonToolbarItems4());
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
			text: '新增盘库',
			tooltip: '新增盘库',
			handler: function() {
				me.onAddClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnDelete',
			iconCls: 'button-del',
			text: "取消盘库",
			tooltip: "对选择盘库单进行物理删除",
			handler: function() {
				me.onDeleteClick();
			}
		});
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnCheck",
			text: '预览PDF',
			tooltip: '预览PDF',
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
		var items = ['refresh'];
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
	/**创建功能 按钮栏*/
	createButtonToolbarItems3: function() {
		var me = this;
		var items = [];
		var tempStatus = me.removeSomeStatusList();
		items.push({
			fieldLabel: '状态选择',
			labelWidth: 65,
			width: 170,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocStatus',
			emptyText: '状态选择',
			data: tempStatus,
			value: me.defaultStatusValue,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			fieldLabel: '是否锁定',
			labelWidth: 65,
			width: 145,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'inventoryLock',
			emptyText: '锁定标志',
			style: {
				marginLeft: "5px"
			},
			data: [
				["", "请选择"],
				["1", "已锁定"],
				["2", "已解锁"]
			],
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar3',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems4: function() {
		var me = this;
		var items = [];
		me.createTemplate(items);
		items.push({
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
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar4',
			items: items
		});
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
			width: "75%",
			name: 'cboTemplate',
			itemId: 'cboTemplate',
			xtype: 'uxCheckTrigger',
			classConfig: {
				width: 195,
				height: 460,
				checkOne: true,
				/**BReportType:9*/
				breportType: me.breportType,
				/**模板分类:Excel模板,Frx模板*/
				publicTemplateDir: 'Frx模板'
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
	onTemplateCheck: function(p, record) {
		var me = this;
		me.pdfFrx = "";
		var buttonsToolbar = me.getComponent("buttonsToolbar4");
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
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		return tempList;
	},
	/**@description 获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**@description 获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');

		var buttonsToolbar3 = me.getComponent('buttonsToolbar3'),
			status = buttonsToolbar3.getComponent('DocStatus'),
			lock = buttonsToolbar3.getComponent('inventoryLock');

		var buttonsToolbarSearch = me.getComponent('buttonsToolbarSearch'),
			search = buttonsToolbarSearch.getComponent('Search');

		var where = [];

		if(status) {
			var value = status.getValue();
			if(value)
				where.push("reabmscheckdoc.Status=" + value);
		}
		if(lock) {
			var value = lock.getValue();
			if(value)
				where.push("reabmscheckdoc.IsLock=" + value);
		}

		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start)
					where.push("reabmscheckdoc.DataAddTime>='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				if(dateValue.end)
					where.push("reabmscheckdoc.DataAddTime<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
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
	/**@description 设置日期范围值*/
	onSetDateArea: function(day) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day);
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateAreaValue) date.setValue(dateAreaValue);
	},
	/**@description 初始化日期范围*/
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
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	/**@description 取消盘库按钮*/
	onDeleteClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var status = records[0].get("ReaBmsCheckDoc_Status");
		if(status != "1") {
			var StatusEnum = JShell.REA.StatusList.Status[me.StatusKey].Enum;
			var statusName = "";
			if(StatusEnum)
				statusName = StatusEnum[status];
			JShell.Msg.alert("当前状态为【" + statusName + "】,不能执行继续盘库操作!", null, 2000);
			return;
		}
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">取消盘库确认</div>',
			msg: '系统会直接将盘库单的数据进行物理删除!<br />请确认是否继续执行?按【确定】继续',
			closable: false,
			multiline: false
		}, function(but, text) {
			if(but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;
			me.autoSelect = true;
			var id = records[0].get(me.PKField);
			me.delOneById(1, id);
		});
	},
	/**@description 预览PDF盘库信息*/
	onPrintClick: function() {
		var me = this;
		me.fireEvent('onPrint', me);
		//		var records = me.getSelectionModel().getSelection();
		//		if(records.length != 1) {
		//			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
		//			return;
		//		}
		//		if(!me.pdfFrx) {
		//			JShell.Msg.error("请先选择清单模板后再操作!");
		//			return;
		//		}
		//		//var frx = me.getComponent("buttonsToolbar4").getComponent("cboTemplate").getValue();
		//		var id = records[0].get("ReaBmsCheckDoc_Id");
		//		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_GetReaBmsCheckDocAndDtlOfPdf");
		//		url += '?operateType=1&id=' + id;
		//		var sort = [{
		//			property: 'ReaBmsCheckDtl_StorageID',
		//			direction: 'ASC'
		//		}, {
		//			property: 'ReaBmsCheckDtl_PlaceID',
		//			direction: 'ASC'
		//		}, {
		//			property: 'ReaBmsCheckDtl_ReaGoodsNo',
		//			direction: 'ASC'
		//		}, {
		//			property: 'ReaBmsCheckDtl_GoodsSort',
		//			direction: 'ASC'
		//		}, {
		//			property: 'ReaBmsCheckDtl_LotNo',
		//			direction: 'ASC'
		//		}];
		//		sort = JShell.JSON.encode(sort);
		//		if(sort){
		//			url += '&sort=' + sort;
		//		}
		//		if(me.pdfFrx) {
		//			url += '&frx=' + JShell.String.encode(me.pdfFrx);
		//		}
		//		window.open(url);
	}
});