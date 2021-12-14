/**
 * 库存变化跟踪
 * @author longfc
 * @version 2018-03-15
 */
Ext.define('Shell.class.rea.client.qtyoperation.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '库存操作记录',
	width: 800,
	height: 500,

	/**查询数据*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlOperationByAllJoinHql?isPlanish=true',
	defaultOrderBy: [{
		property: 'ReaBmsQtyDtlOperation_GoodsID',
		direction: 'ASC'
	}, {
		property: 'ReaBmsQtyDtlOperation_DataAddTime',
		direction: 'ASC'
	}],

	/**默认加载数据*/
	defaultLoad: false,
	/**当前操作记录操作类型*/
	searchOperTypeValue: null,

	/**库存操作记录操作类型*/
	OperTypeKey: "ReaBmsQtyDtlOperationOperType",
	/**库房是否按库房员工权限获取*/
	isEmpPermission: false,
	/**用户UI配置Key*/
	userUIKey: 'qtyoperation.Grid',
	/**用户UI配置Name*/
	userUIName: "库存操作记录列表",

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
			dataIndex: 'ReaBmsQtyDtlOperation_BarCodeType',
			text: '条码类型',
			width: 60,
			hidden: true,
			renderer: function(value, meta, record) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_CompanyName',
			text: '供应商',
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_OperTypeID',
			text: '操作类型Id',
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_ReaGoodsNo',
			text: '货品编码',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_GoodsName',
			text: '货品名称',
			width: 140,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsQtyDtlOperation_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_GoodsUnit',
			text: '包装单位',
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_UnitMemo',
			text: '规格',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_OperTypeID',
			text: '变动类型',
			width: 90,
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
			dataIndex: 'ReaBmsQtyDtlOperation_DataAddTime',
			text: '变动日期',
			isDate: true,
			hasTime: true,
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_GoodsQty',
			text: '变动数量',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_Price',
			text: '单价',
			width: 65,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_SumTotal',
			text: '金额',
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_LotNo',
			text: '货品批号',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_InvalidDate',
			text: '有效期至',
			isDate: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_BDocNo',
			text: '业务单据号',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_CreaterName',
			text: '操作人',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_ProdDate',
			text: '生产日期',
			isDate: true,
			width: 85,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_StorageName',
			text: '库房',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_PlaceName',
			text: '货架',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_Memo',
			text: '备注',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_BDocID',
			text: '业务主单据Id',
			width: 145,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_BDtlID',
			text: '业务明细Id',
			width: 145,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_GoodsID',
			text: '试剂Id',
			width: 145,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_QtyDtlID',
			text: '库存记录Id',
			width: 145,
			hidden: true,
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
			width: 145,
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
		});
		items.push({
			emptyText: '一级分类',
			labelWidth: 0,
			width: 105,
			itemId: 'GoodsClass',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClass');
				}
			}
		}, {
			emptyText: '二级分类',
			labelWidth: 0,
			width: 105,
			itemId: 'GoodsClassType',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClassType');
				}
			}
		});
		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 105,
			labelSeparator: '',
			labelAlign: 'right',
			emptyText: '货品选择',
			name: 'GoodsName',
			itemId: 'GoodsName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goods2.basic.CheckGrid',
			style: {
				marginLeft: "5px",
				marginRight: "15px"
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
		//查询框信息
		me.searchInfo = {
			width: 155,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品名称/货品编码/批号',
			fields: ['reabmsqtydtloperation.GoodsName', 'reabmsqtydtloperation.ReaGoodsNo', 'reabmsqtydtloperation.LotNo']
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
			text: '库存初始化',
			tooltip: '库存初始化',
			itemId: "Availability",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(1, button);
			}
		}, '-', {
			xtype: 'button',
			text: '验货入库',
			tooltip: '验货入库',
			itemId: "ComfirmInStorage",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(2, button);
			}
		}, '-', {
			xtype: 'button',
			text: '移库入库',
			tooltip: '移库入库',
			itemId: "TransferIn",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(14, button);
			}
		}, '-', {
			xtype: 'button',
			text: '退库入库',
			tooltip: '退库入库',
			itemId: "OutOfInStorage",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(3, button);
			}
		}, {
			xtype: 'button',
			text: '借调入库',
			tooltip: '借调入库',
			hidden: true,
			itemId: "LendStorage",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(4, button);
			}
		}, {
			xtype: 'button',
			text: '借出出库',
			tooltip: '借出出库',
			itemId: "LendingOut",
			hidden: true,
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(5, button);
			}
		}, '-', {
			xtype: 'button',
			text: '移库出库',
			tooltip: '移库出库',
			itemId: "TransferOut",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(13, button);
			}
		}, '-', {
			xtype: 'button',
			text: '使用出库',
			tooltip: '使用出库',
			itemId: "OutQtyDtl",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(6, button);
			}
		},{
			xtype: 'button',
			text: '归还出库',
			tooltip: '归还出库',
			hidden:true,
			itemId: "Withdrawal",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(7, button);
			}
		}, {
			xtype: 'button',
			text: '借入入库',
			tooltip: '借入入库',
			itemId: "BorrowingStorage",
			enableToggle: false,
			hidden: true,
			handler: function(button, e) {
				me.onOperTypeSearch(8, button);
			}
		}, '-', {
			xtype: 'button',
			text: '退供应商',
			tooltip: '退供应商',
			itemId: "RetreatSuppliers",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(9, button);
			}
		}, '-', {
			xtype: 'button',
			text: '盘盈入库',
			tooltip: '盘盈入库',
			itemId: "Surplus",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(10, button);
			}
		},  '-',{
			xtype: 'button',
			text: '盘亏出库',
			//hidden: true,
			tooltip: '盘亏出库',
			itemId: "LossOut",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(11, button);
			}
		}, '-', {
			xtype: 'button',
			text: '报损出库',
			tooltip: '报损出库',
			itemId: "Damaged",
			enableToggle: false,
			handler: function(button, e) {
				me.onOperTypeSearch(12, button);
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'operTypeSearchButtonToolbar',
			items: items
		});
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
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

		if(me.searchOperTypeValue != null && parseInt(me.searchOperTypeValue) > -1)
			where.push("reabmsqtydtloperation.OperTypeID=" + me.searchOperTypeValue);
		//货品	
		if(GoodsID)
			where.push('reabmsqtydtloperation.GoodsID=' + GoodsID);
		//供应商	
		if(CompanyID)
			where.push('reabmsqtydtloperation.ReaCompanyID=' + CompanyID);

		var date = buttonsToolbar.getComponent('date');
		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					where.push('reabmsqtydtloperation.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push('reabmsqtydtloperation.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
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
	/**机构货品查询条件*/
	getReaGoodsHql: function() {
		var me = this,
			arr = [];
		var buttonsToolbar1 = me.getComponent('buttonsToolbar'),
			goodsClass = buttonsToolbar1.getComponent('GoodsClass').getValue(),
			goodsClassType = buttonsToolbar1.getComponent('GoodsClassType').getValue();
		//一级分类	
		if(goodsClass) {
			arr.push("reagoods.GoodsClass='" + goodsClass + "'");
		}
		//二级分类	
		if(goodsClassType) {
			arr.push("reagoods.GoodsClassType='" + goodsClassType + "'");
		}
		var where = "";
		if(arr && arr.length > 0) where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		return where;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		var url = me.callParent(arguments);
		//机构货品查询条件
		var reaGoodsHql = me.getReaGoodsHql();
		if(reaGoodsHql) {
			url += '&reaGoodsHql=' + JShell.String.encode(reaGoodsHql);
		}
		return url;
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