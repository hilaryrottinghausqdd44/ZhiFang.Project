/**
 * 货品盒条码操作记录
 * @author longfc
 * @version 2019-01-08
 */
Ext.define('Shell.class.rea.client.barcodeoperation.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	
	title: '货品盒条码操作记录',
	width: 800,
	height: 500,
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsBarcodeOperationByHQL?isPlanish=true',
	
	defaultOrderBy: [{
		property: 'ReaGoodsBarcodeOperation_BarcodeCreatType',
		direction: 'ASC'
	}, {
		property: 'ReaGoodsBarcodeOperation_DataAddTime',
		direction: 'ASC'
	}, {
		property: 'ReaGoodsBarcodeOperation_OperTypeID',
		direction: 'ASC'
	}, {
		property: 'ReaGoodsBarcodeOperation_DispOrder',
		direction: 'ASC'
	}],

	/**默认加载数据*/
	defaultLoad: false,
	/**当前操作记录操作类型*/
	searchOperTypeValue: null,

	/**操作记录操作类型*/
	OperTypeKey: "ReaGoodsBarcodeOperType",
	/**库房是否按库房员工权限获取*/
	isEmpPermission: false,
	
	/**用户UI配置Key*/
	userUIKey: 'barcodeoperation.Grid',
	/**用户UI配置Name*/
	userUIName: "盒条码操作记录列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initDateArea(-30);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.OperTypeKey, false, true, null);
		me.initIsUseEmp();
		me.selectUrl = me.selectUrl + "&isEmpPermission=" + me.isEmpPermission;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	initIsUseEmp: function() {
		var me = this;
		//系统运行参数"是否启用库存库房权限":1:是;2:否;
		var isUseEmp = JcallShell.REA.RunParams.Lists.ReaBmsQtyDtlIsUseEmp.Value;
		if(!isUseEmp) {
			JShell.REA.RunParams.getRunParamsValue("ReaBmsQtyDtlIsUseEmp", false, function(data) {
				isUseEmp = JcallShell.REA.RunParams.Lists.ReaBmsQtyDtlIsUseEmp.Value;
				if(isUseEmp && (isUseEmp == 1 || isUseEmp == "1" || isUseEmp == "true")) {
					me.isEmpPermission = true;
				}
			});
		} else {
			isUseEmp = "" + isUseEmp;
			if(isUseEmp == 1 || isUseEmp == "1" || isUseEmp == "true") {
				me.isEmpPermission = true;
			}
		}
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoodsBarcodeOperation_LotNo',
			text: '货品批号',
			width: 105,
			hidden: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_ReaGoodsNo',
			text: '货品编码',
			width: 110,
			renderer: function(value, meta, record) {
				var v = record.get("ReaGoodsBarcodeOperation_OverageQty");
				if(v == 0 || v == "0") {
					var style = 'font-weight:bold;';
					style = style + "background-color:red;";
					style = style + "color:#ffffff;";
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					meta.style = style;
				}
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_GoodsCName',
			text: '货品名称',
			width: 150,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoodsBarcodeOperation_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"')>=0)value=value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_CenOrgGoodsNo',
			text: '供应商货品编码',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_GoodsUnit',
			text: '包装单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_BarcodeCreatType',
			text: '生成类型',
			width: 70,
			renderer: function(value, meta, record) {
				var v = '否';
				var style = 'font-weight:bold;';
				meta.style = style;
				if(value && parseInt(value) == 1) {
					style = style + "background-color:#ffffff;color:#1c8f36;";
					v = '条码生成';
				} else {
					style = style + "background-color:#ffffff;color:#dd6572;";
					v = '货品扫码';
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_OperTypeID',
			text: '操作类型',
			width: 75,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.OperTypeKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.OperTypeKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.OperTypeKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.OperTypeKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.OperTypeKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.OperTypeKey].FColor[value];
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
			dataIndex: 'ReaGoodsBarcodeOperation_PrintCount',
			text: '已打印',
			width: 50,
			align: 'center',
			renderer: function(value, meta, record) {
				var v = '否';
				var style = 'font-weight:bold;';
				meta.style = style;
				if(value && parseInt(value) > 0) {
					style = style + "background-color:#ffffff;color:#1c8f36;";
					v = '是';
				} else {
					style = style + "background-color:#ffffff;color:red;";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_PUsePackSerial',
			sortable: false,
			width: 145,
			text: '父条码',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_UsePackSerial',
			sortable: false,
			width: 110,
			//hidden: true,
			text: '一维条码',
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_UsePackQRCode',
			sortable: false,
			width: 505,
			text: '二维条码',
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_DispOrder',
			text: '顺序号',
			width: 55,
			type: 'int',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_MinBarCodeQty',
			text: '最小单位库存数',
			width: 95,
			type: 'float',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_ScanCodeQty',
			text: '当次扫码数',
			width: 75,
			type: 'float',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_ScanCodeGoodsUnit',
			text: '扫码单位',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_OverageQty',
			text: '剩余库存数',
			width: 80,
			type: 'float',
			renderer: function(value, meta) {
				var v = value;
				if(v == 0 || v == "0") {
					var style = 'font-weight:bold;';
					style = style + "background-color:red;";
					style = style + "color:#ffffff;";
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					meta.style = style;
				}
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_GoodsQty',
			text: '条码数量',
			hidden: true,
			width: 70,
			type: 'float',
			align: 'right',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			isKey: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_DataAddTime',
			text: '操作时间',
			isDate: true,
			hasTime: true,
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_CreaterName',
			sortable: false,
			text: '操作人',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_BDocNo',
			sortable: false,
			text: '业务单据号',
			width: 145,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_BDocID',
			sortable: false,
			text: '业务主单ID',
			width: 145,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_BDtlID',
			sortable: false,
			text: '业务明细ID',
			width: 145,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_QtyDtlID',
			sortable: false,
			text: '库存ID',
			width: 145,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_StorageID',
			text: '库房ID',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_PlaceID',
			text: '货架ID',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_GoodsID',
			sortable: false,
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_Memo',
			text: '备注信息',
			width: 80,
			defaultRenderer: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];

		items.push('-', {
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
		});

		items.push('-', {
			fieldLabel: '',
			name: 'CompanyName',
			itemId: 'CompanyName',
			xtype: 'uxCheckTrigger',
			emptyText: '供应商选择',
			width: 155,
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
			fieldLabel: '供货商主键ID',
			hidden: true,
			xtype: 'uxCheckTrigger',
			name: 'CompanyID',
			itemId: 'CompanyID'
		}, {
			fieldLabel: '',
			labelWidth: 0,
			width: 155,
			labelSeparator: '',
			labelAlign: 'right',
			emptyText: '货品选择',
			name: 'GoodsName',
			itemId: 'GoodsName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goods2.basic.CheckGrid',
			style: {
				marginLeft: "5px"
			},
			classConfig: {
				width: 480,
				checkOne: true,
				title: '货品选择'
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsAccept(p, record);
				}
			}
		}, {
			fieldLabel: '货品主键ID',
			itemId: 'GoodsID',
			name: 'GoodsID',
			xtype: 'textfield',
			hidden: true
		});
		//条码查询框信息
		me.SearchSerial = {
			width: 175,
			isLike: false,
			itemId: 'SearchSerial',
			emptyText: '扫一维条码或二维条码检索',
			fields: ['reagoodsbarcodeoperation.UsePackSerial', 'reagoodsbarcodeoperation.UsePackQRCode']
		};
		items.push('-', {
			type: 'search',
			info: me.SearchSerial
		});

		//查询框信息
		me.searchInfo = {
			width: 165,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品名称/货品编码/批号',
			fields: ['reagoodsbarcodeoperation.GoodsCName', 'reagoodsbarcodeoperation.ReaGoodsNo', 'reagoodsbarcodeoperation.LotNo']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createOperTypeButtonToolbar());
		return items;
	},
	/**创建操作记录操作类型按钮查询栏*/
	createOperTypeButtonToolbar: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			text: '全部',
			tooltip: '全部',
			itemId: "AllStatus",
			handler: function(button, e) {
				me.onOperTypeSearch(null, button);
			}
		}, '-', {
			xtype: 'button',
			text: '供货',
			tooltip: '供货',
			hidden: true,
			itemId: "Availability",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(1, button);
			}
		}, {
			xtype: 'button',
			text: '验货接收',
			tooltip: '验货接收',
			itemId: "ConfirmAccept",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(2, button);
			}
		}, '-', {
			xtype: 'button',
			text: '验货拒收',
			tooltip: '验货拒收',
			itemId: "ConfirmRefuse",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(3, button);
			}
		}, '-', {
			xtype: 'button',
			text: '验货入库',
			tooltip: '验货入库',
			itemId: "Storage",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(4, button);
			}
		}, '-', {
			xtype: 'button',
			text: '库存初始化',
			tooltip: '库存初始化',
			itemId: "ManualInputStock",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(5, button);
			}
		}, '-', {
			xtype: 'button',
			text: '移库出库',
			tooltip: '移库领用',
			itemId: "TransferOut",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(15, button);
			}
		}, '-', {
			xtype: 'button',
			text: '移库入库',
			tooltip: '移库入库',
			itemId: "TransferIn",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(6, button);
			}
		}, '-', {
			xtype: 'button',
			text: '使用出库',
			tooltip: '使用出库',
			itemId: "OutDtl",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(7, button);
			}
		}, '-', {
			xtype: 'button',
			text: '退库入库',
			tooltip: '退库入库',
			itemId: "Withdrawal",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(8, button);
			}
		},{
			xtype: 'button',
			text: '盘库',
			tooltip: '盘库',
			itemId: "Stocktaking",
			enableToggle: false,
			hidden: true,
			handler: function(button, e) {
				me.onOperTypeSearch(9, button);
			}
		}, '-', {
			xtype: 'button',
			text: '退供应商',
			tooltip: '退供应商',
			itemId: "RetreatSuppliers",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(10, button);
			}
		}, '-', {
			xtype: 'button',
			text: '盘盈入库',
			tooltip: '盘盈入库',
			itemId: "Surplus",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(11, button);
			}
		},'-',{
			xtype: 'button',
			text: '盘亏出库',
			//hidden:true,
			tooltip: '盘亏出库',
			itemId: "Loss",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(12, button);
			}
		},{
			xtype: 'button',
			text: '借入入库',
			tooltip: '借入入库',
			itemId: "BorrowingStorage",
			enableToggle: false,
			hidden: true,
			handler: function(button, e) {
				me.onOperTypeSearch(13, button);
			}
		}, '-', {
			xtype: 'button',
			text: '报损出库',
			tooltip: '报损出库',
			itemId: "Damaged",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(14, button);
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'operTypeSearchButtonToolbar',
			items: items
		});
	},
	/**按操作记录操作类型快捷查询*/
	onOperTypeSearch: function(value, button) {
		var me = this;
		me.setButtonOperTypeToggle(button);
		me.searchOperTypeValue = value;
		me.onSearch();
	},
	setButtonOperTypeToggle: function(button) {
		var me = this;
		var buttonsToolbar = me.getComponent('operTypeSearchButtonToolbar');
		var items = buttonsToolbar.items.items;
		Ext.Array.forEach(items, function(item, index) {
			if(item && item.xtype == "button") item.toggle(false);
		});
		button.toggle(true);
	},
	/**供应商选择*/
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
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
	onGoodsAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var GoodsID = buttonsToolbar.getComponent('GoodsID');
		var GoodsName = buttonsToolbar.getComponent('GoodsName');
		GoodsID.setValue(record ? record.get('ReaGoods_Id') : '');
		GoodsName.setValue(record ? record.get('ReaGoods_CName') : '');
		p.close();
		me.onSearch();
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var where = [];

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var GoodsID = buttonsToolbar.getComponent('GoodsID').getValue();
		var CompanyID = buttonsToolbar.getComponent('CompanyID').getValue();

		var labId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID);
		if(labId) {
			where.push('reagoodsbarcodeoperation.LabID=' + labId);
		}
		if(me.searchOperTypeValue != null && parseInt(me.searchOperTypeValue) > -1)
			where.push("reagoodsbarcodeoperation.OperTypeID=" + me.searchOperTypeValue);
		//货品	
		if(GoodsID)
			where.push('reagoodsbarcodeoperation.GoodsID=' + GoodsID);
		//供应商	
		if(CompanyID)
			where.push('reagoodsbarcodeoperation.ReaCompanyID=' + CompanyID);

		var date = buttonsToolbar.getComponent('date');
		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					where.push('reagoodsbarcodeoperation.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push('reagoodsbarcodeoperation.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		var searchSerial = buttonsToolbar.getComponent('SearchSerial');
		if(searchSerial) {
			var value = searchSerial.getValue();
			if(value) {
				where.push("(" + me.getSearchSerial(value) + ")");
			}
		}
		var search = buttonsToolbar.getComponent('Search');
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
	/**获取查询框内容*/
	getSearchSerial: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.SearchSerial,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for(var i = 0; i < len; i++) {
			if(isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
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
		var dateareaToolbar = me.getComponent('buttonsToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateArea) date.setValue(dateArea);
	}
});