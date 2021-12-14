/**
 * 订单汇总统计
 * @author longfc
 * @version 2018-09-10
 */
Ext.define('Shell.class.rea.client.statistics.order.summary.DtlGrid', {
	extend: 'Shell.class.rea.client.statistics.basic.SearchGrid',

	title: '订单汇总统计',

	/**获取数据服务路径*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchReaBmsCenOrderDtlSummaryByHQL?isPlanish=true',
	
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 15,
	features: [{
		ftype: 'summary'
	}],
	/**用户UI配置Key*/
	userUIKey: 'statistics.order.summary.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "订单汇总统计列表",
	/**后台排序*/
	remoteSort: true,
	/**订单明细报表合并选择项Key */
	ReaBmsCenOrderDtlStatisticalType: "ReaBmsCenOrderDtlStatisticalType",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},
	initComponent: function() {
		var me = this;
		// 订单明细报表合并选项
		JShell.REA.StatusList.getStatusList(me.ReaBmsCenOrderDtlStatisticalType, false, false, null);
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
		me.initDateArea(-30);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createButtonToolbar1Items());
		items.push(me.createDateAreaButtonToolbar());
		// 需求调整：增加功能栏
		items.push(me.createButtonToolbar3());
		return items;
	},
	/**默认按钮栏*/
	createButtonToolbar1Items: function() {
		var me = this;
		var items = [];
		items = me.createDeptNameItems(items);
		items = me.createCompanyNameItems(items);
		items = me.createLabcNameItems(items);
		items = me.createGoodsClassItems(items);
		items = me.createGoodsClassTypeItems(items);
		items = me.createProdOrgNameItems(items);
		items = me.createGoodsNameItems(items);
		items = me.createSearchItems(items);
		items.push({
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
			border: false,
			itemId: 'buttonsToolbar1',
			items: items
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsCenOrderDtl_DeptName',
			text: '申请部门',
			width: 100,
			defaultRenderer: true,
			doSort: function(state) {
				//var field = this.getSortParam();
				var field="ReaBmsCenOrderDoc_DeptName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_CompanyName',
			text: '供应商',
			width: 90,
			defaultRenderer: true,
			doSort: function(state) {
				//var field = this.getSortParam();
				var field="ReaBmsCenOrderDoc_CompanyName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_OrderDocNo',
			text: '订货单号',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ReaGoodsName',
			text: '货品名称',
			width: 120,
			minWidth: 100,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsCenOrderDtl_BarCodeType");
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
			dataIndex: 'ReaBmsCenOrderDtl_GoodSName',
			text: '简称',
			width: 90,
			defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_SName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ProdOrgName',
			text: '品牌',
			width: 90,
			defaultRenderer: true,
			sortable: false
		},{
			dataIndex: 'ReaBmsCenOrderDtl_BarCodeType',
			text: 'BarCodeType',
			sortable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_GoodsUnit',
			text: '单位',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_UnitMemo',
			text: '规格',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_MonthlyUsage',
			text: '理论月用量',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ReqGoodsQty',
			text: '申请订货数',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_GoodsQty',
			text: '审批订货数',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_Price',
			text: '单价',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_DataAddTime',
			text: '时间',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_SumTotal',
			text: '金额',
			width: 130,
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			},
			summaryRenderer: function(value, summaryData, dataIndex) {
				return '<span style="font-weight:bold;font-size:12px;color:blue;">共' + Ext.util.Format.currency(value, '￥', 2) + '元</span>';
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_Memo',
			text: 'Memo',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_RegistNo',
			text: '注册号',
			width: 80,
			defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_RegistNo";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_NetGoodsNo',
			text: '挂网流水号',
			width: 80,
			defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_NetGoodsNo";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}];

		return columns;
	},
	// 需求调整
	/**按钮栏3*/
	createButtonToolbar3: function() {
		var me = this;
		var items = [];
		var list = JShell.REA.StatusList.Status[me.ReaBmsCenOrderDtlStatisticalType].List;
	
		items.push({
			fieldLabel: '',
			emptyText: '报表合并方式',
			labelWidth: 0,
			width: 175,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'cmReaBmsStatisticalType',
			name: 'cmReaBmsStatisticalType',
			value: "1",
			data: list,
			listeners: {
				select: function(com, records, eOpts) {
					//先按报表合并方式更新显示列信息
					me.changeGridColumns();
					me.onSearch();
				}
			}
		});
		
		items.push({
			xtype: 'textSearchTrigger',
			name: 'txtSearchOrderDoc',
			itemId: 'txtSearchOrderDoc',
			emptyText: '订货单号',
			width: 165,
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				onSearchClick: function(field, value) {
					me.onSearch();
				},
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						me.onSearch();
					}
				}
			}
		});
		items.push({
			xtype: 'textSearchTrigger',
			name: 'txtSearchSName',
			itemId: 'txtSearchSName',
			emptyText: '简称',
			width: 165,
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				onSearchClick: function(field, value) {
					me.onSearch();
				},
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						me.onSearch();
					}
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar3',
			items: items
		});
	},
	// 需求调整：报表合并下拉框触发，改变数据列显示
	changeGridColumns: function() {
		var me = this;
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		var groupType = buttonsToolbar3.getComponent('cmReaBmsStatisticalType').getValue();
		var txtSearchOrderDoc=buttonsToolbar3.getComponent('txtSearchOrderDoc');
		var txtSearchSName = buttonsToolbar3.getComponent('txtSearchSName');
		//按入库明细:领用部门ID+供应商ID+使用仪器ID+货品产品编码+包装单位+规格+批号+效期+出库人ID+使用时间
		if (!groupType) groupType = "1";
		switch (groupType) {
			case "1": //订单明细汇总查询
				me.changeGridColumns1();
				txtSearchOrderDoc.setValue("");
				txtSearchOrderDoc.hide();
				txtSearchSName.show();
				break;
			case "3"://按订单号汇总统计
				me.changeGridColumns3();
				txtSearchOrderDoc.show();
				txtSearchSName.setValue("");
				txtSearchSName.hide();
				break;
			default:
				me.changeGridColumns2();
				txtSearchOrderDoc.show();
				txtSearchSName.show();
				break;
		}
	},
	//订单明细汇总查询
	changeGridColumns1: function() {
		var me = this;
		var columns = ['ReaBmsCenOrderDtl_DeptName', 'ReaBmsCenOrderDtl_CompanyName','ReaBmsCenOrderDtl_ReaGoodsNo',
			'ReaBmsCenOrderDtl_ReaGoodsName', 'ReaBmsCenOrderDtl_GoodSName','ReaBmsCenOrderDtl_ProdOrgName','ReaBmsCenOrderDtl_GoodsUnit', 'ReaBmsCenOrderDtl_UnitMemo','ReaBmsCenOrderDtl_MonthlyUsage',
			'ReaBmsCenOrderDtl_ReqGoodsQty','ReaBmsCenOrderDtl_GoodsQty',
			'ReaBmsCenOrderDtl_Price', 'ReaBmsCenOrderDtl_SumTotal'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if (Ext.Array.contains(columns, item.dataIndex)) {
				me.columns[index].show();
			} else {
				me.columns[index].hide();
			}
			index = index + 1;
		});
	},
	//订单明细查询
	changeGridColumns2: function() {
		var me = this;
		var columns = ['ReaBmsCenOrderDtl_DeptName', 'ReaBmsCenOrderDtl_CompanyName','ReaBmsCenOrderDtl_ReaGoodsNo', 'ReaBmsCenOrderDtl_ReaGoodsName',
			'ReaBmsCenOrderDtl_GoodsUnit', 'ReaBmsCenOrderDtl_GoodSName','ReaBmsCenOrderDtl_ProdOrgName', 'ReaBmsCenOrderDtl_UnitMemo','ReaBmsCenOrderDtl_MonthlyUsage',
			'ReaBmsCenOrderDtl_ReqGoodsQty','ReaBmsCenOrderDtl_GoodsQty',
			'ReaBmsCenOrderDtl_Price', 'ReaBmsCenOrderDtl_SumTotal', 'ReaBmsCenOrderDtl_OrderDocNo',
			'ReaBmsCenOrderDtl_RegistNo','ReaBmsCenOrderDtl_NetGoodsNo'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if (Ext.Array.contains(columns, item.dataIndex)) {
				me.columns[index].show();
			} else {
				me.columns[index].hide();
			}
			index = index + 1;
		});
	},
	//新增统计：按订单号汇总统计
	changeGridColumns3: function() {
		var me = this;
		var columns = ['ReaBmsCenOrderDtl_CompanyName', 'ReaBmsCenOrderDtl_OrderDocNo', 'ReaBmsCenOrderDtl_DataAddTime',
			'ReaBmsCenOrderDtl_SumTotal'
		];
		var index = 0;
		Ext.Array.forEach(me.columns, function(item) {
			if (Ext.Array.contains(columns, item.dataIndex)) {
				me.columns[index].show();
				me.columns[index].width=200;
			} else {
				me.columns[index].hide();
			}
			index = index + 1;
		});
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
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		var reaGoodsHql = me.getReaGoodsHql();
		if(!docHql) docHql = "";
		if(!dtlHql) dtlHql = "";
		if(!reaGoodsHql) reaGoodsHql = "";
		// 需求调整
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		//加入的查询框订货单号
		var txtSearchOrderDoc=buttonsToolbar3.getComponent('txtSearchOrderDoc');
		txtSearchOrderDoc = txtSearchOrderDoc.getValue();
		//加入的查询框简称
		var txtSearchSName=buttonsToolbar3.getComponent('txtSearchSName');
		txtSearchSName = txtSearchSName.getValue();
		if(docHql==""){
			if (txtSearchOrderDoc!="") {
				docHql += "reabmscenorderdoc.OrderDocNo='"+txtSearchOrderDoc+"'";
			}
			if (txtSearchSName!="") {
				docHql += "reagoods.SName='"+txtSearchSName+"'";
			}
		}else{
			if (txtSearchOrderDoc!="") {
				docHql += " and reabmscenorderdoc.OrderDocNo='"+txtSearchOrderDoc+"'";
			}
			if (txtSearchSName!="") {
				docHql += " and reagoods.SName='"+txtSearchSName+"'";
			}
		}
		var startDate = "",
			endDate = "";
		var dateAre = me.getDateAreaValue();
		startDate = dateAre.start || "";
		endDate = dateAre.end || "";
		url += (url.indexOf('?') == -1 ? '?' : '&') + "docHql=" + JShell.String.encode(docHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "dtlHql=" + JShell.String.encode(dtlHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "reaGoodsHql=" + JShell.String.encode(reaGoodsHql);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "startDate=" + startDate + "&endDate=" + endDate;
		
		var groupType = buttonsToolbar3.getComponent('cmReaBmsStatisticalType').getValue();
		//按入库明细
		if (!groupType) groupType = 1;
		me.changeGridColumns();
		
		url += (url.indexOf('?') == -1 ? '?' : '&') + "groupType=" + groupType;
		return url;
	},
	/**获取日期范围值*/
	getDateAreaValue: function() {
		var me = this;
		var dateareaToolbar = me.getComponent('dateareaToolbar');
		var dateArea = dateareaToolbar.getComponent('date');
		if(dateArea && dateArea.getValue()) {
			var date = dateArea.getValue();
			if(date.start) date.start = Ext.Date.format(date.start, "Y-m-d");
			if(date.end) date.end = Ext.Date.format(date.end, "Y-m-d");
			return date;
		} else {
			return {
				"start": "",
				"end": ""
			};
		}
	},
	/**获取主单查询条件*/
	getParamsDocHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var companyID = buttonsToolbar.getComponent('CompanyID');
		var deptID = buttonsToolbar.getComponent('DeptID');
		var dateArea = me.getDateAreaValue();
		var companyID = buttonsToolbar.getComponent('CompanyID');
		var reaServerLabcCode = buttonsToolbar.getComponent('ReaServerLabcCode');

		var docHql = [];
		//订单状态不等于暂存,申请,审核退回
		docHql.push('reabmscenorderdoc.Status not in(0,1,2)');
		//docHql.push('reabmscenorderdoc.Visible=1');
		var labID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID) || "";
		if(labID) {
			docHql.push('reabmscenorderdoc.LabID=' + labID);
		}

		if(deptID) {
			var value = deptID.getValue();
			if(value) {
				docHql.push('reabmscenorderdoc.DeptID=' + value);
			}
		}
		if(companyID) {
			var value = companyID.getValue();
			if(value) {
				docHql.push('reabmscenorderdoc.ReaCompID=' + value);
			}
		}
		if(reaServerLabcCode) {
			var value = reaServerLabcCode.getValue();
			if(value) {
				docHql.push("reabmscenorderdoc.ReaServerLabcCode='" + value + "'");
			}
		}
		if(dateArea) {
			if(dateArea.start) {
				docHql.push("reabmscenorderdoc.DataAddTime>='" + dateArea.start + " 00:00:00'");
			}
			if(dateArea.end) {
				docHql.push("reabmscenorderdoc.DataAddTime<='" + dateArea.end + " 23:59:59'");
			}
		}

		if(docHql && docHql.length > 0) {
			docHql = docHql.join(" and ");
		} else {
			docHql = "";
		}
		return docHql;
	},
	/**获取明细查询条件*/
	getParamsDtlHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var prodId = buttonsToolbar.getComponent('ProdId');
		var prodOrgName = buttonsToolbar.getComponent('ProdOrgName');
		var reaGoodsNo = buttonsToolbar.getComponent('ReaGoodsNo');
		var dtlHql = [];
		var labID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID) || "";
		if(labID) {
			dtlHql.push('reabmscenorderdtl.LabID=' + labID);
		}

		if(reaGoodsNo) {
			var value = reaGoodsNo.getValue();
			if(value) {
				dtlHql.push("reabmscenorderdtl.ReaGoodsNo='" + value + "'");
			}
		}
		if(prodOrgName) {
			var value = prodOrgName.getValue();
			if(value) {
				dtlHql.push("reabmscenorderdtl.ProdOrgName='" + value + "'");
			}
		}
		if(dtlHql && dtlHql.length > 0) {
			dtlHql = dtlHql.join(" and ");
		} else {
			dtlHql = "";
		}
		return dtlHql;
	},
	/**@description 获取公共查询参数*/
	getSearchParams: function() {
		var me = this;
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		var reaGoodsHql = me.getReaGoodsHql();
		if (!docHql) docHql = "";
		if (!dtlHql) dtlHql = "";
		if (!reaGoodsHql) reaGoodsHql = "";
		if (!docHql && !dtlHql) {
			JShell.Msg.error("请先选择统计条件后再操作!");
			return [];
		}
		var startDate = "",
			endDate = "";
		var dateAre = me.getDateAreaValue();
		startDate = dateAre.start || "";
		endDate = dateAre.end || "";
	
		var params = [];
		// 需求调整：动态获取groupType的值
		var buttonsToolbar3 = me.getComponent('buttonsToolbar3');
		var groupType = buttonsToolbar3.getComponent('cmReaBmsStatisticalType').getValue();
		params.push("groupType=" + groupType) ;
		
		//加入的查询框订货单号
		var txtSearchOrderDoc=buttonsToolbar3.getComponent('txtSearchOrderDoc');
		txtSearchOrderDoc = txtSearchOrderDoc.getValue();
		if(docHql==""){
			if (txtSearchOrderDoc!="") {
				docHql += "reabmscenorderdoc.OrderDocNo='"+txtSearchOrderDoc+"'";
			}
		}else{
			if (txtSearchOrderDoc!="") {
				docHql += " and reabmscenorderdoc.OrderDocNo='"+txtSearchOrderDoc+"'";
			}
		}
		var txtSearchSName=buttonsToolbar3.getComponent('txtSearchSName');
		txtSearchSName = txtSearchSName.getValue();
		if(docHql==""){
			if (txtSearchSName!="") {
				docHql += "reagoods.SName='"+txtSearchSName+"'";
			}
		}else{
			if (txtSearchSName!="") {
				docHql += " and reagoods.SName='"+txtSearchSName+"'";
			}
		}
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("breportType=" + me.breportType);
		params.push("frx=" + JShell.String.encode(me.pdfFrx));
		
		params.push("startDate=" + startDate);
		params.push("endDate=" + endDate);
		if (docHql)
			params.push("docHql=" + docHql);
		if (dtlHql)
			params.push("dtlHql=" + JShell.String.encode(dtlHql));
		if (reaGoodsHql)
			params.push("reaGoodsHql=" + JShell.String.encode(reaGoodsHql));
	
		var curOrderBy = me.curOrderBy;
		if (curOrderBy.length <= 0 && me.defaultOrderBy && me.defaultOrderBy.length > 0)
			curOrderBy = me.defaultOrderBy;
		for(var i=0;i<curOrderBy.length;i++){
			if(curOrderBy[i].property == "ReaBmsCenOrderDtl_GoodSName"){
				curOrderBy[i].property ="ReaGoods_SName";
			}
		}
		params.push("sort=" + JShell.JSON.encode(curOrderBy));
		
		return params;
	}
});